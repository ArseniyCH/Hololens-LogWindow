using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Input = UnityEngine.VR.WSA.Input;

public class GestureAction : MonoBehaviour {

    private Vector3 startPosition;

    private void PerfomManipulation()
    {
        if (GestureDetector.Instance.IsManipulating &&
            GazeDetector.Instance.HitObject == gameObject)

            gameObject.transform.position = GestureDetector.Instance.ManipulationPosition;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        PerfomManipulation();
	}
}
