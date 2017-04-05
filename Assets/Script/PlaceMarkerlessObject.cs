using System.Collections;
using UnityEngine;
using UnityEngine.UI;


namespace Kudan.AR
{
	public class PlaceMarkerlessObject : MonoBehaviour
    {
		public KudanTracker _kudanTracker;
        public static KudanTracker kudanTracker;
        public GameObject prefab;
        public GameObject MarkerlessObject;
        public GameObject buttonText;
        private bool first = true;
        // from the floor placer.
        private static Vector3 floorPosition;          // The current position in 3D space of the floor
        private static Quaternion floorOrientation;    // The current orientation of the floor in 3D space, relative to the device

 


        void Start()
        {
            kudanTracker = _kudanTracker;
        }

        public void PlaceClick()
        {

            _kudanTracker.FloorPlaceGetPose(out floorPosition, out floorOrientation);   // Gets the position and orientation of the floor and assigns the referenced Vector3 and Quaternion those values
            if (first)
            {
                first = false;
                _kudanTracker.ArbiTrackStart(floorPosition, floorOrientation);              // Starts markerless tracking based upon the given floor position and orientations
                buttonText.GetComponent<Text>().text="Place Model";
                
            }
            else if (!first)
            {
                foreach (Transform child in MarkerlessObject.transform)
                {
                    if (Vector3.Distance(child.position, floorPosition) < 150)
                    {
                        AndroidToast.ShowToastNotification("There are models nearby. Please choose another location", AndroidToast.LENGTH_LONG);
                        return;
                    }
                }
                CreateModel();
            }
        }
        public void Update()
        {
            if (!first)
            {
                //_kudanTracker
            }
        }

        public void CreateModel() {
            floorPosition.y = floorPosition.y - 100;
            GameObject temp = Instantiate(prefab, floorPosition, floorOrientation);
            temp.transform.parent = MarkerlessObject.transform;

        }

        public static Vector3 GetCurrentPosition()
        {
            kudanTracker.FloorPlaceGetPose(out floorPosition, out floorOrientation);
            return floorPosition;
        }
        public static Quaternion GetCurrentOrientation()
        {
            kudanTracker.FloorPlaceGetPose(out floorPosition, out floorOrientation);
            return floorOrientation;
        }


    }
	
}


