using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleSound : MonoBehaviour
{
    public void OnValueChanged(bool value)
    {
        PlayerPrefs.SetString("sound", value ? "on" : "off");
    }
}
