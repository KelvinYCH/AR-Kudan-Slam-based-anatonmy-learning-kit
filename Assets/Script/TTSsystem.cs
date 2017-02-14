using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TTSsystem : MonoBehaviour {
	// Use this for initialization
	void Start () {
        EasyTTSUtil.Initialize(EasyTTSUtil.UnitedStates);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public static void read(string content)
    {
        EasyTTSUtil.SpeechAdd(content);
    }

    void OnApplicationQuit()
    {
        EasyTTSUtil.Stop();
    }
}
