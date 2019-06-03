using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleSound : MonoBehaviour
{
    public void OnValueChanged(bool value)
    {
        FXService.Instance.PlayClick();
        PlayerPrefs.SetString("sound", value ? "on" : "off");
        if (value)
            AudioService.Instance.Play();
        else
            AudioService.Instance.Stop();
    }
}
