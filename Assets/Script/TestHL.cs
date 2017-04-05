using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HighlightingSystem;

public class TestHL : MonoBehaviour {

    private Color color = new Color(1,1,1,1);
    private Highlighter highlighter;
    // Use this for initialization
    void Start () {
        gameObject.AddComponent<Highlighter>();
        highlighter = gameObject.GetComponent<Highlighter>();
    }
	
	// Update is called once per frame
	void Update () {
        color.a = Mathf.Abs(Mathf.Cos(Time.time));
        Debug.Log(color);
        highlighter.On(color);
    }
}
