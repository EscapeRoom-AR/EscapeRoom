using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsStart : MonoBehaviour
{
    public Toggle toggleSound;
    public Toggle toggleTutorial;


    void Start()
    {
        print(PlayerPrefs.GetString("sound"));
        toggleSound.isOn = PlayerPrefs.GetString("sound") == "on";
        toggleTutorial.isOn = PlayerPrefs.GetString("tutorial") == "on";
    }
}
