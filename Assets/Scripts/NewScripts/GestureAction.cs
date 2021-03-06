﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Input = UnityEngine.VR.WSA.Input;
using HoloToolkit.Unity.InputModule;

public class GestureAction : MonoBehaviour {

    private Vector3 startPosition;

    private void PerfomAction()
    {
        if (GestureDetector.Instance.CurrentGameObject == null)
            return;

        if (GestureDetector.Instance.IsManipulating)
            PerfomManipulation();
    }

     private void PerfomManipulation()
    {
        GameObject currentGameObject = GestureDetector.Instance.CurrentGameObject;

        if (currentGameObject == gameObject)
        {
            Debug.Log("Manipulating wtih " + currentGameObject.name + " == " + gameObject.name);
            gameObject.transform.position = GestureDetector.Instance.ManipulationPosition;
        }
    }

    // Use this for initialization
    void Start () {
        Debug.Log("GestureAction started for " + gameObject.name);
	}
	 
	// Update is called once per frame
	void Update () {
        PerfomAction();
	}
}
