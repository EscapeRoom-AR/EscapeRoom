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
            restService.GetUser(apiResponse => {
                User user = apiResponse.data;
                window.transform.SetParent(canvas.transform, false);
                window.transform.Find("Content").Find("EmailField").GetComponent<InputField>().text = user.Email;
                window.transform.Find("Content").Find("UsernameField").GetComponent<InputField>().text = user.Username;
                Image image = window.transform.Find("Content").Find("ImageButton").Find("Icon").GetComponent<Image>();
                StartCoroutine(SetImage("https://upload.wikimedia.org/wikipedia/commons/thumb/7/7e/Circle-icons-profile.svg/1024px-Circle-icons-profile.svg.png", image));
            });
        } else { 
            window = Instantiate(windowUnLoggedPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            window.transform.SetParent(canvas.transform, false);
        }

    }

    IEnumerator SetImage(string url, Image image)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        yield return www.SendWebRequest();

        Texture2D texture = ((DownloadHandlerTexture)www.downloadHandler).texture as Texture2D;
        Sprite sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
        image.sprite = sprite;
    }
}
