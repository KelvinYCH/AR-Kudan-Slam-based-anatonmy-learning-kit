using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIWidgets;

public class ListViewController : MonoBehaviour {

    public ListView bodySystemListView;
    private BodySystem bodySystem;
    private List<string> listContent;
    private bool updating = false;
    // Use this for initialization
    void Start () {
        var temp = bodySystemListView.DataSource;
        temp.Clear();
        listContent = new List<string>();
        listContent.Add("BodySkin");
        listContent.Add("Circulatory System");
        listContent.Add("Digestive System");
        listContent.Add("Lymphatic System");
        listContent.Add("Muscular System");
        listContent.Add("Nervous System");
        listContent.Add("Reproductive System");
        listContent.Add("Respiratory System");
        listContent.Add("Skeletal System");
        listContent.Add("Urinary System");
        temp.AddRange(listContent);
        bodySystemListView.OnSelect.AddListener(updateSelectData);
        bodySystemListView.OnDeselect.AddListener(updateDeSelectData);
    }

    // Update is called once per frame
    void Update()
    {
        if (RayCast.aimingObject)
        {
            bodySystem = RayCast.aimingObject.transform.parent.gameObject.GetComponent<BodySystem>();
            if (bodySystem)
            {
                updating = true;
                bodySystemListView.gameObject.SetActive(true);
                if (RayCast.aimChanged)
                {
                    var buffer = new List<string>(bodySystem.systemSetting.Keys);
                    foreach (string temp in buffer)
                    {

                        if (bodySystem.systemSetting[temp])
                        {
                            bodySystemListView.Select(listContent.IndexOf(temp));

                        }
                        else
                        {
                            bodySystemListView.Deselect(listContent.IndexOf(temp));
                        }
                    }
                }
                updating = false;
            }
        }else
        {
            bodySystemListView.gameObject.SetActive(false);
            bodySystem = null;
        }
    }


    void updateSelectData(int index, ListViewItem item)
    {

        if (RayCast.aimingObject&&!updating)
        {
            bodySystem.systemSetting[listContent[index]] = true;
            foreach (Transform child in RayCast.aimingObject.transform.parent)
            {
                
                if (child.name.StartsWith(listContent[index].Split(' ')[0]))
                {
                    
                    child.gameObject.SetActive(true);

                }
            }
        }
    }

    void updateDeSelectData(int index, ListViewItem item)
    {

        if (RayCast.aimingObject && !updating && checkWillNotAllInactive())
        {
            bodySystem.systemSetting[listContent[index]] = false;
            foreach (Transform child in RayCast.aimingObject.transform.parent)
            {
                if (child.name.StartsWith(listContent[index].Split(' ')[0]))
                {
                    
                    child.gameObject.SetActive(false);
                }
            }
        }else
        {
            AndroidToast.ShowToastNotification("You cannot deselect all system.", AndroidToast.LENGTH_LONG);
        }
    }
    bool checkWillNotAllInactive()
    {
        int numberOfTrue=0;
        foreach (bool child in bodySystem.systemSetting.Values)
        {
            if (child)
            {
                numberOfTrue++;
            }
        }
        return numberOfTrue>1?true:false;
    }


}
