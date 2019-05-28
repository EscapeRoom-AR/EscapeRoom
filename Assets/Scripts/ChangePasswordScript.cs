﻿using Services;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangePasswordScript : MonoBehaviour
{
    public InputField OldPassword;
    public InputField Password;
    public InputField PasswordRepeat;

    public RESTService restService;
    public ModalService modalService;

    private readonly string PROFILE_SCENE_NAME = "Profile";

    public void ChangePassword()
    {
        if (Password.text.Length < 8)
        {
            modalService.ShowModal("Password must be at least 8 characters long");
        }

        if (Password.text.Equals(PasswordRepeat.text))
        {
            restService.ChangePassword(OldPassword.text, Password.text, resp =>
             {
                 if (resp.IsError())
                 {
                     modalService.ShowModal(resp.message);
                 }
                 else ChangeScene();
             });
        }
        else modalService.ShowModal("Passwords doesn't match");
    }

    private void ChangeScene()
    {
        SceneManager.LoadScene(PROFILE_SCENE_NAME);
    }
}
