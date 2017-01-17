using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HighlightingSystem;

public class Highlight : MonoBehaviour {

    private GameObject aimingObject;
	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        aimingObject = RayCast.aimingObject;
	}

    // Update is called once per frame
    void LateUpdate()
    {
        //adding aiming object with not in list
        if (aimingObject != null)
        {
            if (aimingObject.GetComponent<Highlighter>() == null)
                aimingObject.AddComponent<Highlighter>();
            aimingObject.GetComponent<Highlighter>().On(Color.white);
        }
    }
}
