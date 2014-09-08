using UnityEngine;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.IO;
using System.Xml;
using UnityEditor;

public class DataSerialization : MonoBehaviour
{
    public static void Save()
	{
		if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor) {

			String fileName = "C:\\Temp\\prefs.xml"; //read User Input, store at game base url
			SaveKeysToFile (fileName, ReadRegistryKeyValues ());
		}
	}
    public static void Load()
    {
		if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor) {
			String fileName = "C:\\Temp\\prefs.xml"; //read User Input, store at game base url
			LoadPlayerPrefs (LoadKeysFromnFile (fileName));
		}
    }

    private static void LoadPlayerPrefs(List<PrefrenceKey> keys)
    {
        PlayerPrefs.DeleteAll();
        foreach(PrefrenceKey key in keys)
        {
            if(key.Type == "Int32")
            {
                PlayerPrefs.SetInt(key.Name, Convert.ToInt32(key.Value));
            }
            if (key.Type == "Float")
            {
                PlayerPrefs.SetFloat(key.Name, Convert.ToSingle(key.Value));
            }
            if (key.Type == "String")
            {
                PlayerPrefs.SetString(key.Name, key.Value);
            }
        }
    }
    public static List<PrefrenceKey> ReadRegistryKeyValues()
    {
        List<PrefrenceKey> keys = new List<PrefrenceKey>();
        //RegistryKey rk = Registry.LocalMachine.OpenSubKey("Software\\" + PlayerSettings.companyName +"\\" + PlayerSettings.productName , false);
        RegistryKey rk = Registry.CurrentUser.OpenSubKey("Software\\" + "Team Gladiator" + "\\" + "PlayerPrefsSeralizer", false);
        if (rk == null)
		{
			return keys;
		}
		foreach (string key in rk.GetValueNames())
        {
            //var currentKey = rk.GetValue(key);
            //if (currentKey != null)
            //{
            //    Debug.Log(currentKey.GetType());
            //    if(currentKey.GetType().Name == "Int32")
            //    {
            //        keys.Add(new PrefrenceKey(TrimKeyName(key), currentKey.GetType().Name, PlayerPrefs.GetInt(TrimKeyName(key)).ToString()));
            //    }
            //    if (currentKey.GetType().Name == "Int64")
            //    {
            //        keys.Add(new PrefrenceKey(TrimKeyName(key), currentKey.GetType().Name, PlayerPrefs.GetFloat(TrimKeyName(key)).ToString()));
            //    }
            //    if (currentKey.GetType().Name == "String")
            //    {
            //        keys.Add(new PrefrenceKey(TrimKeyName(key), currentKey.GetType().Name, PlayerPrefs.GetString(TrimKeyName(key)).ToString()));
            //    }
            //}
            if (rk.GetValue(key) != null)
            {
                Debug.Log(rk.GetValue(key).GetType());
                if (rk.GetValue(key).GetType().Name == "Int32")
                {
					if(PlayerPrefs.GetInt(TrimKeyName(key), Int32.MinValue) != Int32.MinValue)
					{
						keys.Add(new PrefrenceKey(TrimKeyName(key), "Int32", PlayerPrefs.GetInt(TrimKeyName(key)).ToString()));
					}
					else if(PlayerPrefs.GetFloat(TrimKeyName(key), float.MinValue) != float.MinValue)
					{
						keys.Add(new PrefrenceKey(TrimKeyName(key), "Float", PlayerPrefs.GetFloat(TrimKeyName(key)).ToString()));
					}
					else
					{
						keys.Add(new PrefrenceKey(TrimKeyName(key), "Int32", PlayerPrefs.GetInt(TrimKeyName(key),0).ToString()));
					}
                }
                /*if (rk.GetValue(key).GetType().Name == "Int64")
                {
                    keys.Add(new PrefrenceKey(TrimKeyName(key), rk.GetValue(key).GetType().Name, PlayerPrefs.GetFloat(TrimKeyName(key)).ToString()));
                }*/
                if (rk.GetValue(key).GetType().Name == "String")
                {
                    keys.Add(new PrefrenceKey(TrimKeyName(key), rk.GetValue(key).GetType().Name, PlayerPrefs.GetString(TrimKeyName(key)).ToString()));
                }
            }
        }
        return keys;
    }
    public static void SaveKeysToFile(string fileName, List<PrefrenceKey> keys)
    {
        FileStream writer = new FileStream(fileName, FileMode.Create);
        DataContractSerializer serializer = new DataContractSerializer(typeof(List<PrefrenceKey>));
        serializer.WriteObject(writer, keys);
        writer.Close();
    }
    public static List<PrefrenceKey> LoadKeysFromnFile(string fileName)
    {
        List<PrefrenceKey> keys = new List<PrefrenceKey>();
        if (File.Exists(fileName))
        {
            FileStream fileStream = new FileStream(fileName, FileMode.Open);
            XmlDictionaryReader reader = XmlDictionaryReader.CreateTextReader(fileStream, new XmlDictionaryReaderQuotas());
            DataContractSerializer serializer = new DataContractSerializer(typeof(List<PrefrenceKey>));
            keys = (List<PrefrenceKey>)serializer.ReadObject(reader, true);
            reader.Close();
            fileStream.Close();
        }
        return keys;
    }
    
    public static string TrimKeyName(string key)
    {
        return key.Substring(0, key.LastIndexOf('_'));
    }
}


[Serializable]
public class PrefrenceKey
{
    public String Name = "";
    public String Type = "";
    public String Value = "";

    public PrefrenceKey(string name, string type, string value)
    {
        Name = name;
        Type = type;
        Value = value;
    }
}