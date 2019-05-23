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
    public Canvas canvas;

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
            if (user.Image != "") {
                restService.GetImage(user.Image, sprite => {
                    Image image = window.transform.Find("Content").Find("ImageButton").Find("Icon").GetComponent<Image>();
                    image.sprite = sprite;
                });
            }
        });
    }


}
