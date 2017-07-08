using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.VR.WSA.Input;

public class DragPanelScript : MonoBehaviour, IDragHandler, IBeginDragHandler
{
    private GestureRecognizer gesture;
    Camera mainCamera;
    private float OffsetX, OffsetY;
    Vector3 clickOffset = Vector3.zero;


    public void OnBeginDrag(PointerEventData eventData)
    {
        OffsetX = transform.position.x - eventData.position.x;
        OffsetY = transform.position.y - eventData.position.y;
    }


    private static int i = 0;
    Vector2 prev = new Vector2(0, 0);
    public void OnDrag(PointerEventData eventData)
    {
        transform.parent.parent.position += new Vector3(OffsetX + eventData.position.x, OffsetY + eventData.position.y) - transform.parent.parent.position;

        //Vector2 delta;
        //if (i == 0)
        //    delta = new Vector2(0, 0);
        //else
        //    delta = eventData.position - prev;
        //i++;
        //prev = eventData.position;
        //this.transform.position += new Vector3(delta.x, delta.y, 0);
        ////  this.transform.parent.parent.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        i = 0;
    }

    // Use this for initialization
    void Start()
    {
        gesture = new GestureRecognizer();
        
    }

    // Update is called once per frame
    void Update()
    {
        //       RaycastWorldUI();
    }

    private List<RaycastResult> results;

    void RaycastWorldUI()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PointerEventData pointerData = new PointerEventData(EventSystem.current);

            pointerData.position = Input.mousePosition;

            results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, results);

            if (results.Count > 0)
            {
                Debug.Log(results[0].gameObject.name);
                //WorldUI is my layer name
                if (results[0].gameObject.name == "DragPanel")
                {

                    string dbg = "Root Element: {0} \n GrandChild Element: {1}";
                    Debug.Log(string.Format(dbg, results[results.Count - 1].gameObject.name,
                        results[0].gameObject.name));
                    //Debug.Log("Root Element: "+results[results.Count-1].gameObject.name);
                    //Debug.Log("GrandChild Element: "+results[0].gameObject.name);
                    results.Clear();
                }
            }
        }
    }
}
