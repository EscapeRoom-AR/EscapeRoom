﻿using UnityEngine;
using UnityEngine.UI;
using Services;
using UnityEngine.SceneManagement;
using Model;
using System.Text.RegularExpressions;

public class AuthController : MonoBehaviour
{
    public RESTService restService;
    public ModalService modalService;
    public InputField usernameField;
    public InputField passwordField;
    public InputField passwordRepeatField;
    public InputField emailField;
    private Regex emailRegex = new Regex(@"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*@((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$");

    // Checks whether a user is logged in.
    public bool IsAuthenticated()
    {
        return PlayerPrefs.GetString("token") != "";
    }

    // Logs a user out.
    public void Logout()
    {
        PlayerPrefs.DeleteKey("token");
        SceneManager.LoadScene("Login");
    }

    // Logs a user in.
    public void Login()
    {
        if (usernameField.text == "" || passwordField.text == "")
            modalService.ShowModal("All fields are required");
        else if (passwordField.text.Length < 8)
            modalService.ShowModal("Password must be at least 8 characters long");
        else 
            restService.Login(new User(usernameField.text, passwordField.text), apiResponse => Authenticate(apiResponse));
    }

    // Registers a new user and logs him in.
    public void Register()
    {
        if (emailField.text == "" || usernameField.text == "" || passwordField.text == "" || passwordRepeatField.text == "")
            modalService.ShowModal("All fields are required");
        else if (!emailRegex.Match(emailField.text).Success)
            modalService.ShowModal("Invalid email");
        else if (passwordField.text.Length < 8)
            modalService.ShowModal("Password must be at least 8 characters long.");
        else if (passwordRepeatField.text != passwordField.text)
            modalService.ShowModal("Passwords don't match.");
        else
            restService.Register(new User(usernameField.text, emailField.text, passwordField.text), apiResponse => Authenticate(apiResponse));
    }

    // Handles an APIResponse with a token.
    private void Authenticate(APIResponse<string> apiResponse)
    {
        if (apiResponse.IsError())
            modalService.ShowModal(apiResponse.message);
        else {
            PlayerPrefs.SetString("token", apiResponse.data);
            SceneManager.LoadScene("Main");
        }
    }
}

