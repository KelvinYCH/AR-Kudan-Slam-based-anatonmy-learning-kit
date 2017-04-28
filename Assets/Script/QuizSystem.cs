using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using TMPro;
using System;
using SimpleJSON;
using System.Text.RegularExpressions;

public class QuizSystem : MonoBehaviour {

    public GameObject QuizPanel;
    public GameObject QuizButton;
    public TMP_Text Question;
    public TMP_Text[] Answer;
    public int QuestionNum;
    public NameBox namebox;
    public GameObject resultTextPanel;
    public TMP_Text resultTextContainer;


    private static int TYPE_POINT = 0;
    private static int TYPE_ANSWER = 1;
    private int CorrectCount;
    private int AnswerCount;
    private int Lastanswer;
    private GameObject TestingModel;

    // Use this for initialization
    void Start () {
        CorrectCount = 0;
        AnswerCount = 0;
    }

    private void Update()
    {
        if (RayCast.aimingObject && RayCast.aimingObject.transform.parent.gameObject.CompareTag("BodyModel"))
        {
            QuizButton.SetActive(true);
        }
        else
        {
            QuizButton.SetActive(false);
        }
    }

    // Update is called once per frame
    public void StartQuiz() {
        QuizPanel.SetActive(true);
        if(AnswerCount ==0 )
        {
            BeforeQuiz();
        }
        generateQuestion();
    }

    public void AnswerQuestion(int resultLoc)
    {
        if(resultLoc == Lastanswer)
        {
            CorrectCount++;
        }
        AnswerCount++;
        generateQuestion();
    }

    void generateQuestion()
    {
        if(AnswerCount == QuestionNum)
        {
            AfterQuiz();
            return;
        }

        int rand = UnityEngine.Random.Range(0, 2);
        if (rand == TYPE_POINT)
        {
            //BodySystem bs = TestingModel.GetComponent<BodySystem>();
            GameObject result = DrawPart();
            if (result)
            {
                Question.SetText("Where is " + result.name.Substring(result.name.IndexOf("_") + 1).Replace("_", " "));
                for (int i = 0; i < Answer.Length; i++)
                {
                    Answer[i].SetText("Press any button when you point to it");
                }
            }

            
        }
        else if ( rand == TYPE_ANSWER)
        {
                GameObject result = DrawPart();
                if (result)
                {
                    int answerLoc = UnityEngine.Random.Range(0, Answer.Length);
                    Lastanswer = answerLoc;
                    Debug.Log("Loc : "+answerLoc + " Name : "+result.name);
                
                    //Question Part
                    string keyword;
                    keyword = result.name.Substring(result.name.IndexOf("_") + 1).Replace(" ", "_");
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
                            Question.SetText("Error in fetching data");
                        }
                        else
                        {
                            string resultString = Regex.Replace(myString, keyword.Replace("_"," "), "_______", RegexOptions.IgnoreCase);
                            Question.SetText("Which part of body as describe : " + resultString.Split('.')[0]);
                        }
                    }));

                    //AnswerPart
                    for (int i = 0; i < Answer.Length; i++)
                    {
                        if (i != answerLoc)
                        {
                            GameObject temp = DrawPart();
                            Answer[i].SetText(temp.name.Substring(temp.name.IndexOf("_") + 1).Replace("_", " "));
                        }
                        else
                        {
                            Answer[i].SetText(result.name.Substring(result.name.IndexOf("_") + 1).Replace("_", " "));
                        }
                    }
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

    GameObject DrawPart()
    {
        GameObject result = null;
        int tryCount = 100;
        if (TestingModel) {
            while (tryCount-->0)
            {
                result = TestingModel.transform.GetChild(UnityEngine.Random.Range(0,TestingModel.transform.childCount)).gameObject;
                if (result.activeSelf && (result.name.StartsWith("Muscular") || result.name.StartsWith("Respiratory") || result.name.StartsWith("Skeletal"))) {
                    break;
                }
            }
        }
        return result;
    }

    void BeforeQuiz()
    {
        namebox.Disable();
        TestingModel = RayCast.aimingObject.transform.parent.gameObject;

    }

    void AfterQuiz()
    {
        resultTextPanel.SetActive(true);
        resultTextContainer.SetText("Your score is " + CorrectCount + "/" + QuestionNum + "\nWell Done!" );
        QuizPanel.SetActive(false);
        namebox.Enable();
        CorrectCount = 0;
        AnswerCount = 0;

    }


}
