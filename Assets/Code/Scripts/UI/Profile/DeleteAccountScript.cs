using Services;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeleteAccountScript : MonoBehaviour
{
    private readonly string LOGIN_SCENE_NAME = "Login";
    public InputField Password;
    public RESTService rest;
    public ModalService modalService;

    public void DeleteAccount()
    {
        rest.DeleteAccount(Password.text, resp =>
        {
            FXService.Instance.PlayClick();
            if (resp.IsError())
                modalService.ShowModal(resp.message);
            else ChangeScene();
        });
    }

    private void ChangeScene()
    {
        SceneManager.LoadScene(LOGIN_SCENE_NAME);
    }
}
