using UnityEngine;
using UnityEngine.UI;
using Services;
using Model;
using System.Collections;
using UnityEngine.Networking;

public class ProfileStart : MonoBehaviour
{
    public AuthController authController;
    public RESTService restService;
    public GameObject windowLoggedPrefab;
    public GameObject windowUnLoggedPrefab;
    public GameObject modalPickSourcePrefab;
    public Canvas canvas;
    private bool isModalActive;


    void Start()
    {
        GameObject window;
        if (authController.IsAuthenticated()) {
            window = Instantiate(windowLoggedPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            window.transform.SetParent(canvas.transform, false);
            FillUserInfo(window);
        } else { 
            window = Instantiate(windowUnLoggedPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            window.transform.SetParent(canvas.transform, false);
        }

    }

    private void FillUserInfo(GameObject window) {
        restService.GetUser(apiResponse => {
            User user = apiResponse.data;
            window.transform.Find("Content").Find("EmailField").GetComponent<InputField>().text = user.Email;
            window.transform.Find("Content").Find("UsernameField").GetComponent<InputField>().text = user.Username;
            Button imageButton = window.transform.Find("Content").Find("ImageButton").GetComponent<Button>();
            Image image = imageButton.gameObject.transform.Find("Mask").Find("Icon").GetComponent<Image>();
            imageButton.onClick.AddListener(() => SelectProfileImage(image));
            if (user.Image != "")
                restService.GetImage(user.Image, sprite => image.sprite = sprite);
        });
    }

    private void SelectProfileImage(Image image)
    {
        if (isModalActive)
            return;
        isModalActive = true;
        GameObject modal = Instantiate(modalPickSourcePrefab, new Vector3(0, 0, 0), Quaternion.identity);
        ProfileImage profileImage = modal.transform.Find("SceneController").GetComponent<ProfileImage>();
        profileImage.image = image;
        profileImage.modal = modal;
        modal.transform.Find("RemoveButton").Find("ActionButton").GetComponent<Button>().onClick.AddListener(() => {
            isModalActive = false;
            Destroy(modal);
        });
        modal.transform.SetParent(canvas.transform, false);
    }


}
