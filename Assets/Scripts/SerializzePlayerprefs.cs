using UnityEngine;
using System.Collections;
using Microsoft.Win32;
using UnityEditor;
using System;

public class SerializzePlayerprefs : MonoBehaviour
{
	void start()
	{
		ReadRegistryKeyValues();
	}
    public void ReadRegistryKeyValues()
    {
        //RegistryKey rk = Registry.LocalMachine.OpenSubKey("Software\\" + PlayerSettings.companyName +"\\" + PlayerSettings.productName , false);
        RegistryKey rk = Registry.CurrentUser.OpenSubKey("Software\\" + "Team Gladiator" + "\\" + "PlayerPrefsSeralizer", false);
        foreach (string key in rk.GetValueNames())
        {
            var currentKey = rk.GetValue(key);
            if (currentKey != null)
            {
                if (currentKey.GetType().Name == "Int32")
                {
                    int r = Convert.ToInt32(currentKey);
                }
                if (currentKey.GetType().Name == "Int64")
                {
                    float r = Convert.ToInt64(currentKey);
                }
                if (currentKey.GetType().Name == "String")
                {
                    string r = currentKey.ToString();
                }
            }

        }
    }
}
