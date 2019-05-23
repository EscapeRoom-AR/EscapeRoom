using UnityEngine;
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

    public bool IsAuthenticated()
    {
        return PlayerPrefs.GetString("token") != "";
    }

    public void Login()
    {
        if (usernameField.text == "" || passwordField.text == "")
            modalService.ShowModal("All fields are required");
        else if (passwordField.text.Length < 8)
            modalService.ShowModal("Password must be at least 8 characters long.");
        else {
            APIResponse<RESTService.TokenHolder> apiResponse = restService.Login(usernameField.text, passwordField.text);
            if (apiResponse.IsError())
                modalService.ShowModal(apiResponse.message);
            else
                Authenticated(apiResponse.data.token);
        }
    }

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
        {
            User user = new User(usernameField.text, emailField.text, passwordField.text);
            restService.Register(user, apiResponse =>
            {
                if (apiResponse.IsError())
                    modalService.ShowModal(apiResponse.message);
                else
                    Authenticated(apiResponse.data.token);
            });
        }
    }

    public void Logout()
    {
        PlayerPrefs.DeleteKey("token");
        SceneManager.LoadScene("Login");
    }

    private void Authenticated(string token)
    {
        PlayerPrefs.SetString("token", token);
        SceneManager.LoadScene("Main");
    }
}


