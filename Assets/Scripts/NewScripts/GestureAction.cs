using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Input = UnityEngine.VR.WSA.Input;

public class GestureAction : MonoBehaviour {

    private Vector3 startPosition;

    private void PerfomAction()
    {
        if(GazeDetector.Instance.HitObject == null)
        {
            //Debug.Log("There is no object to act with.");
            return;
        }

        Debug.Log("Perfoming action with " + GazeDetector.Instance.HitObject.name);

        if (GestureDetector.Instance.IsManipulating)
            PerfomManipulation();
    }

    private void PerfomManipulation()
    {
        if (GazeDetector.Instance.HitObject == gameObject)
        {
            Debug.Log("Perfoming manipulation with " + GazeDetector.Instance.HitObject.name);
            gameObject.transform.position = GestureDetector.Instance.ManipulationPosition;
        }
    }
	
	// Update is called once per frame
	void Update () {
        PerfomAction();
	}
}
