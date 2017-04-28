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
        if (RayCast.aimingObject && !RayCast.aimingObject.transform.parent.CompareTag("Animation"))
        {
            if (RayCast.aimingObject.GetComponent<Highlighter>() == null)
                RayCast.aimingObject.AddComponent<Highlighter>();
            RayCast.aimingObject.GetComponent<Highlighter>().On(Color.white);
        }
    }

}
