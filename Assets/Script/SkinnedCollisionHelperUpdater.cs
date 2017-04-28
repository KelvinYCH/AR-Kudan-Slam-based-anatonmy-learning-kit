using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinnedCollisionHelperUpdater : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        SkinnedCollisionHelper[] items = FindObjectsOfType<SkinnedCollisionHelper>();
        foreach (SkinnedCollisionHelper item in items)
        {
            //item.UpdateCollisionMesh();
        }
    }
}
