using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RayCast : MonoBehaviour {
    Camera camera;
    public GameObject debugText;
    private Ray ray;
    public static GameObject aimingObject;
    public static bool aimChanged = true;
    public static GameObject lastAimingObject;

    // Use this for initialization
    void Start () {
        camera = GetComponent<Camera>();
    }
	
	// Update is called once per frame
	void Update () {
        ray = camera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            aimingObject = hit.transform.gameObject;
            //Debug.DrawRay(transform.position, transform.forward, Color.green);
            //debugText.GetComponent<Text>().text = hit.transform.name;
            //print("I'm looking at " + hit.transform.name);
            if (aimingObject != lastAimingObject)
            {
                aimChanged = true;
                lastAimingObject = aimingObject;
            }else
            {
                aimChanged = false;
            }
        }
        else
        {
            aimingObject = null;
            print("I'm looking at nothing!");

        }
    }
}
