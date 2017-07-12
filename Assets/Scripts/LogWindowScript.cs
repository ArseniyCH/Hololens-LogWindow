using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LogWindowScript : MonoBehaviour
{

    private void ApplicationOnLogMessageReceived(string condition, string stackTrace, LogType type)
    {
        gameObject.GetComponent<Text>().text += condition + Environment.NewLine;
    }

    // Use this for initialization
    void Start()
    {
        Application.logMessageReceived += ApplicationOnLogMessageReceived;
    }
}
