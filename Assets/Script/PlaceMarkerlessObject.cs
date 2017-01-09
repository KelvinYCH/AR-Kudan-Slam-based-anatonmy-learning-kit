using System.Collections;
using UnityEngine;


namespace Kudan.AR
{
	public class PlaceMarkerlessObject : MonoBehaviour
    {
		public KudanTracker _kudanTracker;
        public GameObject prefab;
        public GameObject MarkerlessObject;
        private bool first = true;
        // from the floor placer.
        private Vector3 floorPosition;          // The current position in 3D space of the floor
        private Quaternion floorOrientation;    // The current orientation of the floor in 3D space, relative to the device
 


        public void PlaceClick()
        {

            _kudanTracker.FloorPlaceGetPose(out floorPosition, out floorOrientation);   // Gets the position and orientation of the floor and assigns the referenced Vector3 and Quaternion those values
            if (first)
            {
                _kudanTracker.ArbiTrackStart(floorPosition, floorOrientation);              // Starts markerless tracking based upon the given floor position and orientations
                first = false;
            }
            else if (!first)
            {
                foreach (Transform child in MarkerlessObject.transform)
                {
                    if (Vector3.Distance(child.position, floorPosition) < 200)
                    {
                        AndroidToast.ShowToastNotification("There are models nearby. Please choose another location", AndroidToast.LENGTH_LONG);
                        return;
                    }
                }
                CreateModel();
            }
        }

        public void CreateModel() {
            floorPosition.y = floorPosition.y - 50;
            GameObject temp = Instantiate(prefab, floorPosition, floorOrientation);
            temp.transform.parent = MarkerlessObject.transform;
        }


    }
	
}


