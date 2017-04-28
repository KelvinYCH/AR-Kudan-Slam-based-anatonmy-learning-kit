using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeToMarkerOrLess : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void changeToScene(int changeTheScene)
    {
        SceneManager.LoadScene(changeTheScene);
    }
}
