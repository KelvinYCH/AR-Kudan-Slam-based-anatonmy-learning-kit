using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HighlightingSystem;


public class NameBox : MonoBehaviour {
    private GUIStyle boxTextSize;
    private bool initBoxStyle = false;
    private GUIContent content ;
    private Vector2 size;
    private bool enable = true;
    // Use this for initialization
    void Start () {
        boxTextSize = new GUIStyle();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Enable()
    {
        enable = true;
    }

    public void Disable()
    {
        enable = false;
    }

    void OnGUI()
    {
        if (!initBoxStyle)
        {
            boxTextSize = GUI.skin.box;
            boxTextSize.alignment = TextAnchor.MiddleCenter;
            boxTextSize.fontSize = 36;
        }
        if (enable && RayCast.aimingObject && !RayCast.aimingObject.transform.parent.CompareTag("Animation"))
        {
            content = new GUIContent(RayCast.aimingObject.transform.name.Replace("_", " "));
            size = boxTextSize.CalcSize(content);
            GUI.Box(new Rect(Screen.width / 2 + 50, Screen.height / 2 + 25, 1.5f * size.x, 1.2f * size.y), content , boxTextSize);
        }
        else if (enable && RayCast.aimingObject && RayCast.aimingObject.transform.parent.CompareTag("Animation"))
        {
            Animator animator = RayCast.aimingObject.transform.parent.GetComponent<Animator>();
            bool Crouch = animator.GetBool("Crouch");
            content = new GUIContent((Crouch? "Crouching" : "Standing") + " Model");
            size = boxTextSize.CalcSize(content);
            GUI.Box(new Rect(Screen.width / 2 + 50, Screen.height / 2 + 25, 1.5f * size.x, 1.2f * size.y), content, boxTextSize);
        }
    }
}
