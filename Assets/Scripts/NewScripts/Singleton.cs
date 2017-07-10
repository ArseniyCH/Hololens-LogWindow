using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>{

    private static T instance;
    public static T Instance
    {
        get
        {
            return instance;
        }
    }

    public static bool IsInitialized
    {
        get
        {
            return instance != null;
        }
    }

    protected virtual void Awake()
    {
        if(instance != null)
        {
            Debug.Log("The instance of singleton is already created.");
            return;
        }

        instance = (T)this;
    }

    protected virtual void OnDestroy()
    {
        if (instance != null)
            instance = null;
    }
}
