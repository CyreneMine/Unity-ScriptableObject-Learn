using System;
using System.IO;
using UnityEngine;

[CreateAssetMenu(fileName = "SettingData", menuName = "SO/音乐设置")]
public class MyData : ScriptableObject
{
    public bool musicOpen, soundOpen;
    public float musicVolume,soundVolume;

    private void Awake()
    {
        if (File.Exists(Application.persistentDataPath+"/MusicData.json"))
        {
            string str = File.ReadAllText(Application.persistentDataPath+"/MusicData.json");
            JsonUtility.FromJsonOverwrite(str, this);
        }
    }

    public void SaveData()
    {
        string str = JsonUtility.ToJson(this);
        File.WriteAllText(Application.persistentDataPath+"/MusicData.json", str);
    }
}
