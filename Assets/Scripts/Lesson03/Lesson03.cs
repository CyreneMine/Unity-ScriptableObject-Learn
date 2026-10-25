using System;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Lesson03 : MonoBehaviour
{
    public Toggle togMusic,togSound;
    public Slider sldMusicVolume, sldSoundVolume;
    private MyData _musicData;

    private void Awake()
    {
        _musicData = ScriptableObject.CreateInstance<MyData>();
        togMusic.isOn = _musicData.musicOpen;
        togSound.isOn = _musicData.soundOpen;
        sldMusicVolume.value = _musicData.musicVolume;
        sldSoundVolume.value = _musicData.soundVolume;
        sldMusicVolume.onValueChanged.AddListener(arg0 =>
        {
            _musicData.musicVolume = arg0;
        });
        sldSoundVolume.onValueChanged.AddListener(arg0 =>
        {
            _musicData.soundVolume = arg0;
        });
        togMusic.onValueChanged.AddListener(arg0 =>
        {
            _musicData.musicOpen = arg0;
        });
        togSound.onValueChanged.AddListener(arg0 =>
        {
            _musicData.soundOpen = arg0;
        });
    }

    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            _musicData.SaveData();
        }
    }
}
