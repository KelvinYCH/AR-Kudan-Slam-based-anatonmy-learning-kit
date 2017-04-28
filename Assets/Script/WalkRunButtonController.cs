using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkRunButtonController : MonoBehaviour {

    public GameObject WR;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (RayCast.aimingObject !=null && RayCast.aimingObject.transform.parent.CompareTag("Animation")) {
            WR.SetActive(true);
        }
        else
        {
            WR.SetActive(false);
        }
	}

    static void ChangeState()
    {
        if (RayCast.aimingObject != null && RayCast.aimingObject.transform.parent.CompareTag("Animation") && RayCast.aimingObject.transform.parent.GetComponent<RunWalkController>()!=null)
        {
            RayCast.aimingObject.transform.parent.GetComponent<RunWalkController>().ChangeState();
        }
    }
}
