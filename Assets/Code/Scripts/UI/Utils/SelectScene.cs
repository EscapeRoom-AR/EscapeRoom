using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectScene : MonoBehaviour
{
    public void changeScene(string scene)
    {
        FXService.Instance.PlayClick();
        SceneManager.LoadScene(scene);
    }
}