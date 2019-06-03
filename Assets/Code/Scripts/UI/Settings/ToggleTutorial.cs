using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleTutorial : MonoBehaviour
{
    public void OnValueChanged(bool value)
    {
        FXService.Instance.PlayClick();
        PlayerPrefs.SetString("tutorial", value ? "on" : "off");
    }
}
