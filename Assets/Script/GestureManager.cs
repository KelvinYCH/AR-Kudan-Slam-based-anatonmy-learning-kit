using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRubyShared;
using System.Text;
using SimpleJSON;
using TMPro;
using System;
using Kudan.AR;
public class GestureManager : MonoBehaviour
{
    public GameObject container;
    public TMP_Text textContainer;
    public GameObject walkingPrefab = null;
    public GameObject MarkerlessObject;
    

    private ScaleGestureRecognizer scaleGesture;
    private PanGestureRecognizer panGesture;
    private TapGestureRecognizer doubleTapGesture;
    private LongPressGestureRecognizer longPressGesture;

    private List<List<Vector2>> lineSet = new List<List<Vector2>>();
    private List<Vector2> currentPointList;
    private ImageGestureImage lastImage;
    public FingersScript FingersScript;
    public ImageGestureRecognizer imageGesture = new ImageGestureRecognizer();

    private GameObject temp = null;
    private Vector3 tempPos = new Vector3(0,0,0);
    private Quaternion tempQuat = new Quaternion(0, 0, 0 , 0);
    private static readonly Dictionary<ImageGestureImage, string> recognizableImages = new Dictionary<ImageGestureImage, string>
        {
            //P
            { new ImageGestureImage(new ulong[] { 0x0000000000000003, 0x0000000000000003, 0x0000000000000003, 0x0000000000000007, 0x0000000000000007, 0x0000000000000007, 0x000000000000000F, 0x000000000000000F, 0x0000000000007F8E, 0x0000000000007FFE, 0x0000000000007FFE, 0x00000000000071FE, 0x000000000000783E, 0x0000000000007E7E, 0x0000000000007FF8, 0x0000000000001FF0 }, 16),"P"},
            { new ImageGestureImage(new ulong[] { 0x000000000000000F, 0x000000000000000F, 0x0000000000000007, 0x0000000000000007, 0x0000000000000007, 0x0000000000000007, 0x0000000000000007, 0x0000000000000003, 0x0000000000000003, 0x00000000000007FF, 0x0000000000000FFF, 0x0000000000000FFF, 0x0000000000000F07, 0x0000000000000783, 0x00000000000007FF, 0x00000000000007FF }, 16),"P" },
            { new ImageGestureImage(new ulong[] { 0x0000000000000003, 0x0000000000000003, 0x0000000000000003, 0x0000000000000003, 0x0000000000000003, 0x0000000000000003, 0x0000000000000003, 0x0000000000000003, 0x0000000000000003, 0x0000000000007FFF, 0x0000000000007FFF, 0x0000000000007FFF, 0x0000000000007F03, 0x0000000000003FC7, 0x0000000000000FFF, 0x00000000000003FF }, 16),"P" },
            { new ImageGestureImage(new ulong[] { 0x0000000000000003, 0x0000000000000003, 0x0000000000000003, 0x0000000000000003, 0x0000000000000003, 0x0000000000000003, 0x0000000000000003, 0x0000000000000003, 0x0000000000000003, 0x0000000000001FFF, 0x0000000000003FFF, 0x0000000000003FFF, 0x0000000000003E07, 0x0000000000003F0F, 0x0000000000001FFF, 0x00000000000007FE }, 16),"P" },
            { new ImageGestureImage(new ulong[] { 0x0000000000000003, 0x0000000000000003, 0x0000000000000003, 0x0000000000000003, 0x0000000000000003, 0x0000000000000003, 0x0000000000000003, 0x0000000000000003, 0x000000000000007F, 0x00000000000001FF, 0x0000000000000FFF, 0x0000000000003FE3, 0x000000000000FF83, 0x000000000000FFFF, 0x000000000000FFFF, 0x000000000000FFFF }, 16),"P" },
            { new ImageGestureImage(new ulong[] { 0x0000000000000003, 0x0000000000000003, 0x0000000000000003, 0x0000000000000003, 0x0000000000000003, 0x0000000000000003, 0x0000000000000003, 0x0000000000000003, 0x0000000000000FFF, 0x0000000000001FFF, 0x0000000000001FFF, 0x0000000000001C03, 0x0000000000001C03, 0x0000000000001F07, 0x0000000000001FFF, 0x0000000000001FFF }, 16),"P" },
            { new ImageGestureImage(new ulong[] { 0x0000000000000003, 0x0000000000000003, 0x0000000000000003, 0x0000000000000003, 0x0000000000000003, 0x000000000000FFFF, 0x000000000000FFFF, 0x000000000000FFFF, 0x000000000000C00F, 0x000000000000C003, 0x000000000000E007, 0x000000000000F007, 0x000000000000FC0F, 0x0000000000007FFF, 0x0000000000007FFE, 0x0000000000000FFC }, 16),"P" },
            { new ImageGestureImage(new ulong[] { 0x0000000000000007, 0x0000000000000007, 0x0000000000000007, 0x0000000000000007, 0x0000000000000007, 0x0000000000000007, 0x0000000000007FFF, 0x000000000000FFFF, 0x000000000000FFFF, 0x000000000000E007, 0x000000000000E00F, 0x000000000000FFFF, 0x000000000000FFFE, 0x0000000000007FFC, 0x0000000000000038, 0x0000000000000000 }, 16),"P" },
            { new ImageGestureImage(new ulong[] { 0x0000000000000003, 0x0000000000000003, 0x0000000000000003, 0x0000000000000003, 0x0000000000000003, 0x0000000000000003, 0x0000000000000003, 0x0000000000000003, 0x0000000000000003, 0x0000000000000FFF, 0x0000000000003FFF, 0x0000000000003FFF, 0x0000000000003F03, 0x0000000000003F83, 0x0000000000000FFF, 0x00000000000003FF }, 16),"P" },
            { new ImageGestureImage(new ulong[] { 0x0000000000000003, 0x0000000000000003, 0x0000000000000003, 0x0000000000000003, 0x0000000000000003, 0x0000000000000003, 0x0000000000000003, 0x0000000000000003, 0x00000000000007FF, 0x00000000000007FF, 0x00000000000007FF, 0x0000000000000703, 0x0000000000000703, 0x0000000000000703, 0x0000000000000FFF, 0x0000000000000FFF }, 16),"P" },
            { new ImageGestureImage(new ulong[] { 0x0000000000000007, 0x0000000000000007, 0x0000000000000003, 0x0000000000000003, 0x0000000000000003, 0x0000000000000003, 0x0000000000000003, 0x00000000000003FF, 0x00000000000003FF, 0x00000000000007FF, 0x0000000000000F83, 0x0000000000000F03, 0x0000000000000E03, 0x0000000000000FFF, 0x0000000000000FFF, 0x00000000000007FF }, 16),"P" },
            { new ImageGestureImage(new ulong[] { 0x0000000000000003, 0x0000000000000003, 0x0000000000000003, 0x0000000000000003, 0x0000000000000003, 0x0000000000000003, 0x0000000000000003, 0x0000000000001FFF, 0x0000000000001FFF, 0x0000000000001FFF, 0x0000000000001E07, 0x0000000000001F07, 0x0000000000000F87, 0x00000000000007DF, 0x00000000000003FF, 0x00000000000003FE }, 16),"P" },
            { new ImageGestureImage(new ulong[] { 0x0000000000000003, 0x0000000000000003, 0x0000000000000003, 0x0000000000000003, 0x0000000000000003, 0x0000000000000003, 0x0000000000000003, 0x00000000000003FF, 0x00000000000007FF, 0x00000000000007FF, 0x0000000000000783, 0x0000000000000783, 0x0000000000000783, 0x00000000000003FF, 0x00000000000003FF, 0x00000000000003FF }, 16),"P" },
            //vertical line
            { new ImageGestureImage(new ulong[] { 0x0000000000000003, 0x0000000000000003, 0x0000000000000003, 0x0000000000000003, 0x0000000000000003, 0x0000000000000003, 0x0000000000000003, 0x0000000000000003, 0x0000000000000003, 0x0000000000000003, 0x0000000000000003, 0x0000000000000003, 0x0000000000000003, 0x0000000000000003, 0x0000000000000003, 0x0000000000000003 }, 16), "Vertical" },
            // diagonal line (top left to bottom right)
            { new ImageGestureImage(new ulong[] { 0x0000000000001E00, 0x0000000000001F00, 0x0000000000000F80, 0x0000000000000780, 0x00000000000003C0, 0x00000000000003E0, 0x00000000000001F0, 0x00000000000000F0, 0x0000000000000078, 0x000000000000007C, 0x000000000000003E, 0x000000000000001E, 0x000000000000000F, 0x000000000000000F, 0x0000000000000007, 0x0000000000000003 }, 16), "\\" },
            { new ImageGestureImage(new ulong[] { 0x00000000000003C0, 0x00000000000003E0, 0x00000000000001E0, 0x00000000000000F0, 0x00000000000000F0, 0x0000000000000078, 0x0000000000000078, 0x000000000000003C, 0x000000000000003E, 0x000000000000001E, 0x000000000000000F, 0x000000000000000F, 0x0000000000000007, 0x0000000000000007, 0x0000000000000003, 0x0000000000000003 }, 16), "\\" },
            { new ImageGestureImage(new ulong[] { 0x000000000000E000, 0x000000000000F000, 0x000000000000F800, 0x0000000000007C00, 0x0000000000003E00, 0x0000000000001F00, 0x0000000000000F80, 0x00000000000007C0, 0x00000000000003E0, 0x00000000000001F0, 0x00000000000000F8, 0x000000000000007C, 0x000000000000003E, 0x000000000000001F, 0x000000000000000F, 0x0000000000000007 }, 16), "\\" },
            { new ImageGestureImage(new ulong[] { 0x000000000000FE00, 0x000000000000FF80, 0x0000000000001FC0, 0x00000000000007F0, 0x00000000000001FC, 0x00000000000000FF, 0x000000000000003F, 0x000000000000000F, 0x0000000000000003, 0x0000000000000000, 0x0000000000000000, 0x0000000000000000, 0x0000000000000000, 0x0000000000000000, 0x0000000000000000, 0x0000000000000000 }, 16), "\\" },
            { new ImageGestureImage(new ulong[] { 0x0000000000000078, 0x0000000000000078, 0x000000000000003C, 0x000000000000003C, 0x000000000000001C, 0x000000000000001E, 0x000000000000001E, 0x000000000000000E, 0x000000000000000E, 0x000000000000000F, 0x000000000000000F, 0x0000000000000007, 0x0000000000000007, 0x0000000000000007, 0x0000000000000003, 0x0000000000000003 }, 16), "\\" },
            { new ImageGestureImage(new ulong[] { 0x000000000000F000, 0x000000000000FC00, 0x0000000000007E00, 0x0000000000003F80, 0x0000000000000FC0, 0x00000000000007F0, 0x00000000000001FC, 0x00000000000000FE, 0x000000000000003F, 0x000000000000000F, 0x0000000000000007, 0x0000000000000003, 0x0000000000000000, 0x0000000000000000, 0x0000000000000000, 0x0000000000000000 }, 16), "\\" },

            //w
            { new ImageGestureImage(new ulong[] { 0x0000000000000F9F, 0x0000000000001F9F, 0x0000000000001FFF, 0x0000000000003FFF, 0x0000000000003DFF, 0x00000000000079FF, 0x00000000000078F3, 0x000000000000F0F3, 0x000000000000F0F3, 0x000000000000E003, 0x000000000000E000, 0x0000000000000000, 0x0000000000000000, 0x0000000000000000, 0x0000000000000000, 0x0000000000000000 }, 16) ,"W"},
            {new ImageGestureImage(new ulong[] { 0x0000000000000F0F, 0x0000000000001F1F, 0x0000000000001F3F, 0x0000000000003F7F, 0x0000000000003FFF, 0x0000000000007FF7, 0x0000000000007BF7, 0x000000000000F3E3, 0x000000000000F3C3, 0x000000000000E383, 0x000000000000E003, 0x0000000000000003, 0x0000000000000000, 0x0000000000000000, 0x0000000000000000, 0x0000000000000000 }, 16) ,"W"},
            {new ImageGestureImage(new ulong[] { 0x00000000000003FC, 0x00000000000007FC, 0x0000000000000FFE, 0x0000000000001FFE, 0x0000000000003FFE, 0x0000000000007CFF, 0x000000000000F8FF, 0x000000000000F0E7, 0x000000000000E007, 0x000000000000C003, 0x0000000000000003, 0x0000000000000003, 0x0000000000000000, 0x0000000000000000, 0x0000000000000000, 0x0000000000000000 }, 16) ,"W"},
            {new ImageGestureImage(new ulong[] { 0x0000000000000F1E, 0x0000000000000F1E, 0x0000000000001F1E, 0x0000000000001F1F, 0x0000000000003F3F, 0x0000000000003F3F, 0x0000000000003FFF, 0x0000000000007FFF, 0x0000000000007BF7, 0x000000000000F3F3, 0x000000000000F3E3, 0x000000000000E3E3, 0x000000000000E3C3, 0x000000000000C380, 0x0000000000000000, 0x0000000000000000 }, 16) ,"W"},
            {new ImageGestureImage(new ulong[] { 0x000000000000000F, 0x00000000000003EF, 0x00000000000003FF, 0x00000000000007FF, 0x00000000000007FF, 0x0000000000000FFF, 0x0000000000000FFF, 0x0000000000001EFF, 0x0000000000001EF7, 0x0000000000003CF3, 0x0000000000007CE3, 0x0000000000007803, 0x0000000000007803, 0x0000000000007003, 0x0000000000000003, 0x0000000000000003 }, 16) ,"W"},
            //a
            {new ImageGestureImage(new ulong[] { 0x000000000000FFFF, 0x000000000000FFFF, 0x000000000000CFC7, 0x000000000000CFC3, 0x0000000000000783, 0x0000000000000783, 0x00000000000003C3, 0x00000000000003C3, 0x00000000000001E7, 0x00000000000001FF, 0x00000000000007FF, 0x00000000000007FE, 0x00000000000007F0, 0x0000000000000000, 0x0000000000000000, 0x0000000000000000 }, 16) ,"A"},
            {new ImageGestureImage(new ulong[] { 0x000000000000FE7F, 0x000000000000FF7F, 0x000000000000CFF3, 0x00000000000007F3, 0x00000000000003F3, 0x00000000000003E3, 0x00000000000003E3, 0x00000000000003E7, 0x0000000000007FCF, 0x0000000000007FFF, 0x0000000000007FFE, 0x00000000000007FC, 0x00000000000000E0, 0x0000000000000000, 0x0000000000000000, 0x0000000000000000 }, 16) ,"A"},
            {new ImageGestureImage(new ulong[] { 0x000000000000FFFF, 0x000000000000FFFF, 0x000000000000C7E3, 0x000000000000C7C3, 0x00000000000007CF, 0x000000000000079F, 0x000000000000FFBF, 0x000000000000FFFC, 0x000000000000FFF8, 0x0000000000001FF0, 0x0000000000000380, 0x0000000000000000, 0x0000000000000000, 0x0000000000000000, 0x0000000000000000, 0x0000000000000000 }, 16) ,"A"},
            {new ImageGestureImage(new ulong[] { 0x000000000000FFFF, 0x000000000000FFFF, 0x000000000000FFE3, 0x000000000000C7E3, 0x000000000000C7C3, 0x0000000000003FC7, 0x0000000000003FFF, 0x0000000000003FFF, 0x00000000000007FE, 0x0000000000000038, 0x0000000000000000, 0x0000000000000000, 0x0000000000000000, 0x0000000000000000, 0x0000000000000000, 0x0000000000000000 }, 16) ,"A"},
            {new ImageGestureImage(new ulong[] { 0x000000000000FFFF, 0x000000000000FFFF, 0x000000000000C3E3, 0x000000000000C3C3, 0x00000000000003C7, 0x00000000000003DF, 0x00000000000003FF, 0x0000000000000FFE, 0x0000000000000FFC, 0x0000000000000FE0, 0x0000000000000000, 0x0000000000000000, 0x0000000000000000, 0x0000000000000000, 0x0000000000000000, 0x0000000000000000 }, 16) ,"A"},
            {new ImageGestureImage(new ulong[] { 0x000000000000FEFF, 0x000000000000FFFF, 0x000000000000CFF3, 0x0000000000000FC3, 0x0000000000000FC3, 0x0000000000000F83, 0x0000000000000F83, 0x0000000000000F83, 0x0000000000001F9F, 0x0000000000001FFF, 0x0000000000001FFF, 0x0000000000001FFC, 0x00000000000007F0, 0x0000000000000000, 0x0000000000000000, 0x0000000000000000 }, 16) ,"A"}
        };



    private void ScaleGestureCallback(GestureRecognizer gesture, ICollection<GestureTouch> touches)
    {
        if (gesture.State == GestureRecognizerState.Executing && RayCast.aimingObject)
        {
            RayCast.aimingObject.transform.parent.localScale *= scaleGesture.ScaleMultiplier;
        }
    }

    private void CreateScaleGesture()
    {
        scaleGesture = new ScaleGestureRecognizer();
        scaleGesture.Updated += ScaleGestureCallback;
        FingersScript.Instance.AddGesture(scaleGesture);
    }

    private void CreatePanGesture()
    {
        panGesture = new PanGestureRecognizer();
        panGesture.MinimumNumberOfTouchesToTrack = 2;
        panGesture.Updated += PanGestureCallback;
        FingersScript.Instance.AddGesture(panGesture);
    }

    private void DoubleTapGestureCallback(GestureRecognizer gesture, ICollection<GestureTouch> touches)
    {
        if (gesture.State == GestureRecognizerState.Ended && RayCast.aimingObject)
        {
            //Debug.Log("Double Tap");
            RayCast.aimingObject.transform.parent.gameObject.GetComponent<BodySystem>().expand();
        }
    }

    private void CreateDoubleTapGesture()
    {
        doubleTapGesture = new TapGestureRecognizer();
        doubleTapGesture.NumberOfTapsRequired = 2;
        doubleTapGesture.Updated += DoubleTapGestureCallback;
        FingersScript.Instance.AddGesture(doubleTapGesture);
    }


    private void PanGestureCallback(GestureRecognizer gesture, ICollection<GestureTouch> touches)
    {

        if (RayCast.aimingObject && touches.Count == 2)
        {
            RayCast.aimingObject.transform.parent.Rotate(0.0f, -gesture.DeltaX / 3, 0.0f);
        }
    }

    private void Tap_Updated(GestureRecognizer gesture, ICollection<GestureTouch> touches)
    {
        if (gesture.State == GestureRecognizerState.Ended)
        {
            UnityEngine.Debug.Log("Tap Gesture Ended");
        }
    }

    private void AddTouches(ICollection<GestureTouch> touches)
    {
        GestureTouch? t = null;
        foreach (GestureTouch tt in touches)
        {
            t = tt;
            break;
        }
        if (t != null)
        {
            Vector3 v = new Vector3(t.Value.X, t.Value.Y, 0.0f);
            v = Camera.main.ScreenToWorldPoint(v);

            // Debug.LogFormat("STW: {0},{1} = {2},{3}", t.Value.X, t.Value.Y, v.x, v.y);

            currentPointList.Add(v);
        }
    }

    private void UpdateImage()
    {

        if (imageGesture.MatchedGestureImage == null)
        {
            Debug.Log("No match");
        }
        else
        {
            Debug.Log("Match: " + recognizableImages[imageGesture.MatchedGestureImage]);
            switch (recognizableImages[imageGesture.MatchedGestureImage])
            {
                case "P":
                    TTSsystem.read(RayCast.aimingObject.transform.name.Replace("_"," "));
                    break;
                case "Vertical":
                    if (RayCast.aimingObject)
                    {
                        Destroy(RayCast.aimingObject.transform.parent.gameObject);
                    }
                    break;
                case "\\":
                    if (RayCast.aimingObject)
                    {
                        Destroy(RayCast.aimingObject);
                    }
                    break;
                case "W":
                    if (RayCast.aimingObject && !container.gameObject.activeSelf)
                    {
                        container.gameObject.SetActive(true);
                        String keyword;
                        if (RayCast.aimingObject.transform.name.Contains("ystem"))
                        {
                            keyword = RayCast.aimingObject.transform.name.Substring(0, RayCast.aimingObject.transform.name.IndexOf("_")).Replace(" ", "_");
                        }
                        else
                        {
                            keyword = RayCast.aimingObject.transform.name.Substring(RayCast.aimingObject.transform.name.IndexOf("_") + 1).Replace(" ", "_");
                        }
                        StartCoroutine(GetWiki("https://en.wikipedia.org/w/api.php?format=json&action=query&prop=extracts&exintro=&redirects=1&explaintext=&titles=" + keyword.ToLower(), (text) =>
                        {
                            Debug.Log(keyword);
                            string myString = text;
                            byte[] bytes = Encoding.Default.GetBytes(myString);
                            myString = Encoding.UTF8.GetString(bytes);
                            JSONNode tempJson = JSONNode.Parse(myString);


                            myString = tempJson["query"]["pages"][0]["extract"];
                            if (myString == null || myString.Trim().Length <= 1)
                            {
                                textContainer.SetText("Data Not Found!");
                            }
                            else
                            {
                                textContainer.SetText("\t" + myString);
                            }
                        }));
                    }
                    break;
                case "A":
                    tempPos = PlaceMarkerlessObject.GetCurrentPosition();
                    tempPos.y = tempPos.y - 100;
                    tempQuat = PlaceMarkerlessObject.GetCurrentOrientation();
                    GameObject temp = Instantiate(walkingPrefab, tempPos, tempQuat);
                    temp.transform.parent = MarkerlessObject.transform;
                    break;
            }
        }


    }

    public IEnumerator GetWiki(string url, Action<string> callback)
    {
        if (callback == null)
            Debug.LogError("You must supply a callback with GetWWWAsync");

        WWW result = new WWW(url);
        while (!result.isDone)
            yield return null;

        if (!string.IsNullOrEmpty(result.error))
            Debug.LogError(string.Format("Error while downloading from {0}: {1}", url, result.error));
        else
            callback(result.text);
    }


    private void ImageGestureUpdated(GestureRecognizer imageGesture, ICollection<GestureTouch> touches)
    {
        if (imageGesture.State == GestureRecognizerState.Ended)
        {
            AddTouches(touches);
            UpdateImage();
            imageGesture.Reset();
            // note - if you have received an image you care about, you should reset the image gesture, i.e. imageGesture.Reset()
            // the ImageGestureRecognizer doesn't automaticaly Reset like other gestures when it ends because some images need multiple paths
            // which requires lifting the mouse or finger and drawing again
        }
        else if (imageGesture.State == GestureRecognizerState.Began)
        {
            // began
            currentPointList = new List<Vector2>();
            lineSet.Add(currentPointList);
            AddTouches(touches);
        }
        else if (imageGesture.State == GestureRecognizerState.Executing)
        {
            // moving
            AddTouches(touches);
        }
    }

    private GestureTouch FirstTouch(ICollection<GestureTouch> touches)
    {
        foreach (GestureTouch t in touches)
        {
            return t;
        }
        return new GestureTouch();
    }


    private void DragTo(float screenX, float screenY)
    {

    }

    private void LongPressGestureCallback(GestureRecognizer gesture, ICollection<GestureTouch> touches)
    {
        GestureTouch t = FirstTouch(touches);
        if (gesture.State == GestureRecognizerState.Began)
        {
            temp = RayCast.aimingObject.transform.parent.gameObject;
        }
        else if (gesture.State == GestureRecognizerState.Executing)
        {
            tempPos = PlaceMarkerlessObject.GetCurrentPosition();
            tempPos.y = tempPos.y - 100;
            temp.transform.position = tempPos;
        }
        else if (gesture.State == GestureRecognizerState.Ended)
        {
            temp = null;
        }
    }

    private void CreateLongPressGesture()
    {
        longPressGesture = new LongPressGestureRecognizer();
        longPressGesture.MaximumNumberOfTouchesToTrack = 1;
        longPressGesture.Updated += LongPressGestureCallback;
        FingersScript.Instance.AddGesture(longPressGesture);
    }



    // Use this for initialization
    void Start()
    {
        CreateScaleGesture();
        CreatePanGesture();
        CreateLongPressGesture();
        CreateDoubleTapGesture();
        TapGestureRecognizer tap = new TapGestureRecognizer();
        tap.Updated += Tap_Updated;
        FingersScript.AddGesture(tap);
        imageGesture.MaximumPathCount = 1;
        imageGesture.Updated += ImageGestureUpdated;

        imageGesture.GestureImages = new List<ImageGestureImage>(recognizableImages.Keys);
        FingersScript.AddGesture(imageGesture);

    }

    // Update is called once per frame
    void Update()
    {

    }



}
