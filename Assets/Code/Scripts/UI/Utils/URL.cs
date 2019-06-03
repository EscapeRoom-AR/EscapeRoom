using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class URL : MonoBehaviour
{

    public void OpenUrl(string url)
    {
        FXService.Instance.PlayClick();
        Application.OpenURL(url);
    }
}
