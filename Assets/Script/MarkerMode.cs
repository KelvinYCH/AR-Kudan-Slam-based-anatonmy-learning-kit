using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kudan.AR
{
    public class MarkerMode : MonoBehaviour
    {
        public KudanTracker _kudanTracker;  // The tracker to be referenced in the inspector. This is the Kudan Camera object.
        public TrackingMethodMarker _markerTracking;    // The reference to the marker tracking method that lets the tracker know which method it is using
                                                        // Use this for initialization
        void Start()
        {
            _kudanTracker.ChangeTrackingMethod(_markerTracking);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
