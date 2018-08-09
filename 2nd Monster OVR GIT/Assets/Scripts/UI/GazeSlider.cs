using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class GazeSlider : MonoBehaviour {

    public delegate void SliderAction();
    public static event SliderAction OnSliderFull;

    [SerializeField] private float m_Duration = 2f;                     // The length of time it takes for the bar to fill.
    [SerializeField] private Slider m_Slider;                           // Optional reference to the UI slider (unnecessary if using a standard Renderer).
    [SerializeField] private GameObject m_customSlider;                 // Optional reference to the UI slider (unnecessary if using a standard Renderer).

    private bool m_BarFilled;                                           // Whether the bar is currently filled.
    private bool m_GazeOver;                                            // Whether the user is currently looking at the bar.
    private float m_Timer;                                              // Used to determine how much of the bar should be filled.
    private Coroutine m_FillBarRoutine;                                 // Reference to the coroutine that controls the bar filling up, used to stop it if required.
    private Renderer m_customSliderRenderer;
    private float m_fillPercentage;

    private const string k_SliderMaterialPropertyName = "_SliderValue"; // The name of the property on the SlidingUV shader that needs to be changed in order for it to fill.


    // Use this for initialization
    void Start()
    {
        m_customSliderRenderer = m_customSlider.GetComponent<Renderer>();
        SetSliderValue(m_fillPercentage);
        SetAlphaValue(m_fillPercentage);
        Debug.Log("GazeSliderEnabled");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnEnable()
    {
        m_fillPercentage = 0f;
       
    }

    private IEnumerator FillBar()
    {
        // When the bar starts to fill, reset the timer.
        m_Timer = 0f;

        // The amount of time it takes to fill is either the duration set in the inspector, or the duration of the radial.
        float fillTime = m_Duration;

        // Until the timer is greater than the fill time...
        while (m_Timer < fillTime)
        {
            // ... add to the timer the difference between frames.
            m_Timer += Time.deltaTime;

            // Set the value of the slider or the UV based on the normalised time.
            m_fillPercentage = m_Timer / fillTime;
            SetSliderValue(m_fillPercentage);
            SetAlphaValue(m_fillPercentage);

            // Wait until next frame.
            yield return null;

            // If the user is still looking at the bar, go on to the next iteration of the loop.
            if (m_GazeOver)
                continue;

            // If the user is no longer looking at the bar, reset the timer and bar and leave the function.
            m_Timer = 0f;
            SetSliderValue(0f);
            SetAlphaValue(0f);
            yield break;
        }

        // If the loop has finished the bar is now full.
        m_BarFilled = true;

        // If anything has subscribed to OnSliderFilled call it now.
        Debug.Log("Bar is Full");
        if (OnSliderFull != null)
            OnSliderFull();

        // Make a custom GUI-Event Component react to the Full Bar
        gameObject.SendMessage("ReactToGuiElement");
    }

    private void SetSliderValue(float sliderValue)
    {
        // If there is a slider component set it's value to the given slider value.
        if (m_Slider)
            m_Slider.value = sliderValue;

    }

    private void SetAlphaValue (float alphaValue)
    {
        // If there is a Material set it's alpha value to the given slider value.
        m_customSliderRenderer.sharedMaterial.SetFloat("_Cutoff", Mathf.Lerp(1f, 0.001f, alphaValue));
    }

    //Interface Implemantation
    public void OnGazeEnabled()
    {
        m_GazeOver = true;
        Debug.Log("Gaze Enabled on" + gameObject);
        if (m_GazeOver)
        {
            m_FillBarRoutine = StartCoroutine(FillBar());
        }

    }

    public void OnGazeDisabled()
    {
        m_GazeOver = false;
        Debug.Log("Gaze Disabled on" + gameObject);
        // If the coroutine has been started (and thus we have a reference to it) stop it.
        if (m_FillBarRoutine != null)
            StopCoroutine(m_FillBarRoutine);

        // Reset the timer and bar values.
        m_Timer = 0f;
        SetSliderValue(0f);
        SetAlphaValue(0f);

    }

    public void OnGazeStart(Camera camera, GameObject targetObject, Vector3 intersectionPosition)
    {
        throw new NotImplementedException();
    }

    public void OnGazeStay(Camera camera, GameObject targetObject, Vector3 intersectionPosition)
    {
        throw new NotImplementedException();
    }

    public void OnGazeExit(Camera camera, GameObject targetObject)
    {
        throw new NotImplementedException();
    }

    public void OnGazeTriggerStart(Camera camera)
    {
        throw new NotImplementedException();
    }

    public void OnGazeTriggerEnd(Camera camera)
    {
        throw new NotImplementedException();
    }
}
