using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class AutoMovement : MonoBehaviour {

    public const float moveDelay = 4f;
    Coroutine moveCoroutine;

    public delegate void AutoMovements();
    public static event AutoMovements onAutoMoveStart;
    public static event AutoMovements onAutoMoveStop;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoad;
        StartCoroutine(InitializeMovement());
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoad;

    }

    void Start ()
    {
    }

    void OnSceneLoad(Scene _level, LoadSceneMode _mode)
    {
        InitializeMovement();
    }
    
    IEnumerator InitializeMovement()
    {
        yield return new WaitForEndOfFrame();
        StopMovement();
     
        if (!OptionsTracker.instance.touchToMove)
        {
            //Debug.Log("Start Moving automaticaly");
            if (moveCoroutine == null)
            {
                //Debug.Log("There is no moveCoroutine running, so I'll start one.");
                moveCoroutine = StartCoroutine(MoveAfterDelay());
            }
        }
        yield return null;
    }

    IEnumerator MoveAfterDelay ()
    {
        //Debug.Log("Commencing automovent in " + moveDelay + " seconds.");

        yield return new WaitForSeconds(moveDelay);
        //Debug.Log("Commencing automovent now.");
        StartMovement();    
    }

	public void StartMovement()
    {
        if (onAutoMoveStart != null)
        {
            onAutoMoveStart();
        }
    }

    public void StopMovement()
    {   
        if (moveCoroutine != null) {
            //Debug.Log("There is a moveCoroutine, so I'll stop it.");
            StopCoroutine(moveCoroutine);
            moveCoroutine = null;
        }
        
        if (onAutoMoveStop != null)
        {
            onAutoMoveStop();
        }
    }
}
