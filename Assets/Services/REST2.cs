using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Model;
using UnityEditor;
using Proyecto26;
using UnityEngine.Networking;
using System.Text;

public class REST2 : MonoBehaviour
{
    private static string HOST = "http://stucom.flx.cat/alu/dam2t02";
    private static string LOGIN = HOST + "/login";
    private static string REGISTER = HOST + "/register";
    private static string USER = HOST + "/user";
    private static string ROOMS = HOST + "/room";
    private static string ROOM = HOST + "/room";
    private static string RANKING = HOST + "/ranking";

    public void Start()
    {
        register(new User("usernam", "mail", "password"));
    }

    IEnumerator Upload(User user)
    {
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormDataSection("username=" + user.username + "&email=" + user.email + "&password=" + user.password));

        string json = JsonUtility.ToJson(user);
        byte[] byteArray = Encoding.UTF8.GetBytes(json);
        Debug.Log(json);
        UnityWebRequest www = UnityWebRequest.Post(REGISTER, formData);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Form upload complete!" + (www.downloadHandler.text));
        }
    }

    public void register(User user)
    {
        RestClient.Post<APIResponse>(REGISTER, user)
            .Then((resp) => Debug.Log(JsonUtility.ToJson(resp, true)))
            .Catch((err) => Debug.Log("err" + err.Message));

        RestClient.Get<APIResponse>(LOGIN)
            .Then((resp) => Debug.Log("login" + JsonUtility.ToJson(resp)));
    }
}