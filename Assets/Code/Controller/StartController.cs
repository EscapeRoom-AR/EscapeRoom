using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartController : MonoBehaviour
{
    public AuthController authController;

    void Start()
    {
        string nameScene = authController.IsAuthenticated() ? "Main" : "Login";
        AudioService.Instance.Play();
        StartCoroutine(Timeout(nameScene));
    }

    IEnumerator Timeout(string nameScene)
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(nameScene);
    }

}
