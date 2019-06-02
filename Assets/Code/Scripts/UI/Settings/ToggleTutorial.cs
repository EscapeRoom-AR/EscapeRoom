using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleTutorial : MonoBehaviour
{
    public void OnValueChanged(bool value)
    {
        PlayerPrefs.SetString("tutorial", value ? "on" : "off");
    }
}
