using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuiManage : MonoBehaviour {

    public GameObject WRButton;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (RayCast.aimingObject!=null && RayCast.aimingObject.transform.parent.CompareTag("Animation")&&WRButton!=null)
        {
            WRButton.SetActive(true);
        }else
        {
            WRButton.SetActive(false);
        }
        
	}
}
