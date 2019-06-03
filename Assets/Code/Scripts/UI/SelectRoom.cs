using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectRoom : MonoBehaviour
{
    
    public void OnClick()
    {
        SceneManager.LoadScene(PlayerPrefs.GetString("tutorial").Equals("on") ? "Tutorial" : "ScapeRoomScene");
    }


}
