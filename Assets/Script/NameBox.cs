using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameBox : MonoBehaviour {
    private GUIStyle boxTextSize;
    private bool initBoxStyle = false;
    private GUIContent content ;
    private Vector2 size;
    // Use this for initialization
    void Start () {
        boxTextSize = new GUIStyle();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnGUI()
    {
        if (!initBoxStyle)
        {
            boxTextSize = GUI.skin.box;
            boxTextSize.alignment = TextAnchor.MiddleCenter;
            boxTextSize.fontSize = 36;
        }
        if (RayCast.aimingObject)
        {
            content = new GUIContent(RayCast.aimingObject.transform.name.Replace("_", " "));
            size = boxTextSize.CalcSize(content);
            GUI.Box(new Rect(Screen.width / 2 + 50, Screen.height / 2 + 25, 1.5f * size.x, 1.2f * size.y), content , boxTextSize);
        }
    }
}
