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
        StartCoroutine(Timeout(nameScene));
    }

    IEnumerator Timeout(string nameScene)
    {
        print(Time.time);
        yield return new WaitForSeconds(3);
        print(nameScene);
        SceneManager.LoadScene(nameScene);
    }

}
