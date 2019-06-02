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
    public ModalService modalService;
    public GameObject windowLoggedPrefab;
    public GameObject windowUnLoggedPrefab;
    public GameObject modalPickSourcePrefab;
    public Canvas canvas;
    private bool isModalActive;

    private InputField emailField;
    private InputField usernameField;
    private InputField descriptionField;


    void Start()
    {

        GameObject window;
        if (authController.IsAuthenticated()) {
            
            window = Instantiate(windowLoggedPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            window.transform.SetParent(canvas.transform, false);
            window.transform.Find("SaveButtonContent").Find("Button").GetComponent<Button>().onClick.AddListener(() => Post());
            FillUserInfo(window);
        }
        else { 
            window = Instantiate(windowUnLoggedPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            window.transform.SetParent(canvas.transform, false);
        }

        

    }

    private void FillUserInfo(GameObject window) {
        restService.GetUser(apiResponse => {
            User user = apiResponse.data;
            Transform content = window.transform.Find("Content");
            
            usernameField = content.Find("UsernameField").GetComponent<InputField>();
            usernameField.text = user.Username;

            emailField = content.Find("EmailField").GetComponent<InputField>();
            emailField.text = user.Email;

            descriptionField = content.Find("DescriptionField").GetComponent<InputField>();
            descriptionField.text = user.Description;

            Button imageButton = content.Find("ImageButton").GetComponent<Button>();
            Image image = imageButton.gameObject.transform.Find("Mask").Find("Icon").GetComponent<Image>();
            imageButton.onClick.AddListener(() => SelectProfileImage(image));
            /*if (user.Image != "")
                restService.GetImage(user.Image, sprite => image.sprite = sprite);*/
                
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
        profileImage.modalService.canvas = canvas;
        modal.transform.Find("RemoveButton").Find("ActionButton").GetComponent<Button>().onClick.AddListener(() => {
            isModalActive = false;
            Destroy(modal);
        });
        modal.transform.SetParent(canvas.transform, false);
    }

    public void Post()
    {
        User user = new User();
        user.Description = descriptionField.text;
        user.Email = emailField.text;
        user.Username = usernameField.text;
        print("Username: " + user.Username);
        print("Email: " + user.Email);
        modalService.ShowModal("Username: " + user.Username);
        restService.UpdateUser(user, apiResponse => {
            modalService.ShowModal(apiResponse.message);
        });
    }



}
