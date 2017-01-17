using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodySystem : MonoBehaviour {

    public Dictionary<string, bool> systemSetting{get; set;}
    
    void Start () {
		systemSetting = new Dictionary<string, bool>();
        systemSetting.Add("BodySkin",true);
        systemSetting.Add("Circulatory System", true);
        systemSetting.Add("Digestive System", true);
        systemSetting.Add("Lymphatic System", true);
        systemSetting.Add("Muscular System", true);
        systemSetting.Add("Nervous System", true);
        systemSetting.Add("Reproductive System", true);
        systemSetting.Add("Respiratory System", true);
        systemSetting.Add("Skeletal System", true);
        systemSetting.Add("Urinary System", true);
    }
	


    // Update is called once per frame
    void Update () {
		
	}
}
