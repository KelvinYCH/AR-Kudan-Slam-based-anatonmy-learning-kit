using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HighlightingSystem;

public class HighlightForRectusFemoris : MonoBehaviour {
    
    private Color CrouchColor = new Color(1, 1, 0, 1);
    private Color StandColor = new Color(0, 1, 1, 1);
    private Highlighter highlighter;

    // Use this for initialization
    void Start () {
        this.gameObject.AddComponent<Highlighter>();
        highlighter = gameObject.GetComponent<Highlighter>();
    }
	
	// Update is called once per frame
	void Update() { 
        bool Crouch = gameObject.transform.parent.gameObject.GetComponent<Animator>().GetBool("Crouch");
        if (Crouch)
        {
            highlighter.On(CrouchColor);
        }
        else if(!Crouch)
        {
            highlighter.On(StandColor);
        }
    }
}
