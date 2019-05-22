using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Services;
using UnityEngine.SceneManagement;
using Model;
using UnityEditor;

public class AuthController : MonoBehaviour
{
    public RESTService restService;
    //public 
    public InputField usernameField;
    public InputField passwordField;

    public bool IsAuthenticated()
    {
        return PlayerPrefs.GetString("token") != "";
    }

    public void Login()
    {
        print("username: " + usernameField.text);
        print("password: " + passwordField.text);
        APIResponse<RESTService.TokenHolder> apiResponse =  restService.login(usernameField.text, passwordField.text);
        if (apiResponse.IsError())
        {
            
        }
        else
        {
            PlayerPrefs.SetString("token", apiResponse.data.token);
            SceneManager.LoadScene("Main");
        }
    }
}
