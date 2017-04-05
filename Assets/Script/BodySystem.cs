using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodySystem : MonoBehaviour
{

    public Dictionary<string, bool> systemSetting { get; set; }

    void Start()
    {
        systemSetting = new Dictionary<string, bool>();
        systemSetting.Add("BodySkin", true);
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

    bool checkValid()
    {
        foreach (string temp in systemSetting.Keys)
        {
            if (systemSetting[temp] && temp != "Muscular System" && temp != "Skeletal System")
            {
                Debug.Log(temp);
                return false;
            }
        }
        return true;
    }
    public void expand()
    {
        if (!checkValid())
        {
            Debug.Log("Invalid");
            AndroidToast.ShowToastNotification("Expand work for muscular and skeletal system only", AndroidToast.LENGTH_LONG);
            return;
        }
        
        Debug.Log("Valid");
        int count = 0;
        Vector3 center = new Vector3(0, 0, 0);
        foreach (Transform child in transform)
        {
            if (child.gameObject.activeSelf && !child.CompareTag("light"))
            {
                try
                {
                    center += child.GetComponent<Renderer>().bounds.center;
                    count++;
                }
                catch (Exception ex) {
                    //AndroidToast.ShowToastNotification(child.name, AndroidToast.LENGTH_SHORT);
                }
            }
        }
        center = center / count;
        //AndroidToast.ShowToastNotification(center+"", AndroidToast.LENGTH_LONG);
        foreach (Transform child in transform)
        {

            if (child.gameObject.activeSelf && !child.CompareTag("light") && !child.CompareTag("expanded"))
            {
                Vector3 temp = child.GetComponent<Renderer>().bounds.center - center;
                child.position += temp;
                child.tag = "expanded";
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
    }
}
