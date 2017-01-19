using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRubyShared;


public class GestureManager : MonoBehaviour
{

    private ScaleGestureRecognizer scaleGesture;
    private PanGestureRecognizer panGesture;

    private void ScaleGestureCallback(GestureRecognizer gesture, ICollection<GestureTouch> touches)
    {
        if (gesture.State == GestureRecognizerState.Executing && RayCast.aimingObject)
        {
            RayCast.aimingObject.transform.parent.localScale *= scaleGesture.ScaleMultiplier;
        }
    }

    private void CreateScaleGesture()
    {
        scaleGesture = new ScaleGestureRecognizer();
        scaleGesture.Updated += ScaleGestureCallback;
        FingersScript.Instance.AddGesture(scaleGesture);
    }

    private void CreatePanGesture()
    {
        panGesture = new PanGestureRecognizer();
        panGesture.MinimumNumberOfTouchesToTrack = 2;
        panGesture.Updated += PanGestureCallback;
        FingersScript.Instance.AddGesture(panGesture);
    }
    

    private void PanGestureCallback(GestureRecognizer gesture, ICollection<GestureTouch> touches)
    {

        if (RayCast.aimingObject)
        {
            RayCast.aimingObject.transform.parent.Rotate(0.0f, -gesture.DeltaX / 3, 0.0f);
        }
    }

    // Use this for initialization
    void Start()
    {
        CreateScaleGesture();
        CreatePanGesture();

    }

    // Update is called once per frame
    void Update()
    {

    }



}
