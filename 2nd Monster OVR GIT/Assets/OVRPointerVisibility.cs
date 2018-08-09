using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using ControllerSelection;


public class OVRPointerVisibility : MonoBehaviour {

    [SerializeField]
    private string[] menuScenes;

    [SerializeField]
    private LineRenderer lineRenderer;

    [SerializeField]
    private OVRPointerVisualizer oVRPointerVisualizer;

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Use this for initialization
    void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode) {
        if (System.Array.IndexOf(menuScenes, scene.name) != -1)
        {
            makeVisible();
        }
        else
        {
            makeInvisible();
        }
    }

    private void makeInvisible()
    {
        lineRenderer.enabled = false;
        oVRPointerVisualizer.enabled = false;
    }

    private void makeVisible()
    {
        lineRenderer.enabled = true;
        oVRPointerVisualizer.enabled = true;
    }
}
