﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR.WSA.Input;
using HoloToolkit.Unity.InputModule;

public class GestureDetector : Singleton<GestureDetector> {

    public GameObject CurrentGameObject { get; private set; }

    public GestureRecognizer NavigationRecognizer { get; private set; }
    public GestureRecognizer ManipulationRecognizer { get; private set; }
    public GestureRecognizer ActiveRecognizer { get; private set; }

    public bool IsNavigating { get; private set; }
    public Vector3 NavigationPosition { get; private set; }

    public bool IsManipulating { get; private set; }
    public Vector3 StartManipulationPosition { get; private set; }
    public Vector3 ManipulationPosition { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        NavigationRecognizer = new GestureRecognizer();
        NavigationRecognizer.SetRecognizableGestures(
            GestureSettings.Tap |
            GestureSettings.NavigationX);

        NavigationRecognizer.TappedEvent += NavigationRecognizer_TappedEvent;
        NavigationRecognizer.NavigationStartedEvent += NavigationRecognizer_NavigationStartedEvent;
        NavigationRecognizer.NavigationUpdatedEvent += NavigationRecognizer_NavigationUpdatedEvent;
        NavigationRecognizer.NavigationCompletedEvent += NavigationRecognizer_NavigationCompletedEvent;
        NavigationRecognizer.NavigationCanceledEvent += NavigationRecognizer_NavigationCanceledEvent;

        ManipulationRecognizer = new GestureRecognizer();
        ManipulationRecognizer.SetRecognizableGestures(
            GestureSettings.ManipulationTranslate);
        ManipulationRecognizer.ManipulationStartedEvent += ManipulationRecognizer_ManipulationStartedEvent;
        ManipulationRecognizer.ManipulationUpdatedEvent += ManipulationRecognizer_ManipulationUpdatedEvent;
        ManipulationRecognizer.ManipulationCompletedEvent += ManipulationRecognizer_ManipulationCompletedEvent;
        ManipulationRecognizer.ManipulationCanceledEvent += ManipulationRecognizer_ManipulationCanceledEvent;

        ResetGestureRecognizers();
    }

    protected override void OnDestroy()
    {
        NavigationRecognizer.TappedEvent -= NavigationRecognizer_TappedEvent;
        NavigationRecognizer.NavigationStartedEvent -= NavigationRecognizer_NavigationStartedEvent;
        NavigationRecognizer.NavigationUpdatedEvent -= NavigationRecognizer_NavigationUpdatedEvent;
        NavigationRecognizer.NavigationCompletedEvent -= NavigationRecognizer_NavigationCompletedEvent;
        NavigationRecognizer.NavigationCanceledEvent -= NavigationRecognizer_NavigationCanceledEvent;
        
        ManipulationRecognizer.ManipulationStartedEvent -= ManipulationRecognizer_ManipulationStartedEvent;
        ManipulationRecognizer.ManipulationUpdatedEvent -= ManipulationRecognizer_ManipulationUpdatedEvent;
        ManipulationRecognizer.ManipulationCompletedEvent -= ManipulationRecognizer_ManipulationCompletedEvent;
        ManipulationRecognizer.ManipulationCanceledEvent -= ManipulationRecognizer_ManipulationCanceledEvent;
    }
    
    public void ResetGestureRecognizers()
    {
        Transition(ManipulationRecognizer);
    }
    
    public void Transition(GestureRecognizer newRecognizer)
    {
        if (newRecognizer == null)
            return;

        if (ActiveRecognizer != null)
        {
            if (ActiveRecognizer == newRecognizer)
                return;

            ActiveRecognizer.CancelGestures();
            ActiveRecognizer.StopCapturingGestures();
        }

        newRecognizer.StartCapturingGestures();
        ActiveRecognizer = newRecognizer;
    }

    private void NavigationRecognizer_NavigationStartedEvent(InteractionSourceKind source, Vector3 relativePosition, Ray ray)
    {
        IsNavigating = true;
        NavigationPosition = relativePosition;
    }

    private void NavigationRecognizer_NavigationUpdatedEvent(InteractionSourceKind source, Vector3 relativePosition, Ray ray)
    {
        IsNavigating = true;
        NavigationPosition = relativePosition;
    }

    private void NavigationRecognizer_NavigationCompletedEvent(InteractionSourceKind source, Vector3 relativePosition, Ray ray)
    {
        IsNavigating = false;
    }

    private void NavigationRecognizer_NavigationCanceledEvent(InteractionSourceKind source, Vector3 relativePosition, Ray ray)
    {
        IsNavigating = false;
    }

    private void ManipulationRecognizer_ManipulationStartedEvent(InteractionSourceKind source, Vector3 position, Ray ray)
    {
        Debug.Log("Try: Manipulation started");

        GazeManager.Instance.UpdateFocusedGameObject();

        if (GazeManager.Instance.HitObject != null)
        {
            Debug.Log("Manipulation started successfuly");
            IsManipulating = true;

            CurrentGameObject = GazeManager.Instance.HitObject;
            if (CurrentGameObject.name == "DragPanel")
                CurrentGameObject = (GameObject)CurrentGameObject.transform.parent.gameObject.transform.parent.gameObject;
            StartManipulationPosition = GazeManager.Instance.HitObject.transform.position;
            Debug.Log("Got the " + CurrentGameObject.name);
           
            ManipulationPosition = StartManipulationPosition + position;
        }
    }

    private void ManipulationRecognizer_ManipulationUpdatedEvent(InteractionSourceKind source, Vector3 position, Ray ray)
    {
            ManipulationPosition = StartManipulationPosition + position;
    }

    private void ManipulationRecognizer_ManipulationCompletedEvent(InteractionSourceKind source, Vector3 position, Ray ray)
    {
        Debug.Log("Manipulation completed");
        IsManipulating = false;
        CurrentGameObject = null;
    }

    private void ManipulationRecognizer_ManipulationCanceledEvent(InteractionSourceKind source, Vector3 position, Ray ray)
    {
        Debug.Log("Manipulation canceled");
        IsManipulating = false;
        CurrentGameObject = null;
    }

    private void NavigationRecognizer_TappedEvent(InteractionSourceKind source, int tapCount, Ray ray)
    {
        GameObject focusedObject = GazeManager.Instance.HitObject;

        if (focusedObject != null)Debug.Log("OnSelect");
    }
}
 