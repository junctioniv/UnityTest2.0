using UnityEngine;
using System.Collections;
using Microsoft.Win32;
using UnityEditor;

public class TestPlayerPrefs : MonoBehaviour {

    public float floatOne = 0f;
    public float floatTwo = 0f;
    public float floatThree = 0f;
    public string stringOne = "";
    public int intOne = 0;
    bool temp = false;
    bool temp2 = false;
	// Use this for initialization
	public void UpdatePlayerPrefs()
    {
        PlayerPrefs.SetFloat("FloatOne", floatOne);
        PlayerPrefs.SetFloat("FloatTwo", floatTwo);
        PlayerPrefs.SetFloat("FloatThree", floatThree);
        PlayerPrefs.SetString("stringOne", stringOne);
        PlayerPrefs.SetInt("IntOne", intOne);
    }

    void OnGUI()
    {
        if(GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height - 80, 100, 30), (temp? "Saved": "Save Player Prefs")))
        {
            temp = true;
            UpdatePlayerPrefs();
            DataSerialization.Save();
        }
        if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height - 130, 100, 30), (temp2 ? "Loaded" : "Load Player Prefs")))
        {
            temp2 = true;
			DataSerialization.Load();
            floatOne = PlayerPrefs.GetFloat("FloatOne");
            floatTwo = PlayerPrefs.GetFloat("FloatTwo");
            floatThree = PlayerPrefs.GetFloat("FloatThree");
            stringOne = PlayerPrefs.GetString("stringOne");
            intOne = PlayerPrefs.GetInt("IntOne");
            
        }
    }
}
