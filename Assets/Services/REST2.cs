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
        string json = JsonUtility.ToJson(user);
        Debug.Log(json);
        // RestClient.DefaultRequestHeaders.Add("Content-Type", "application/json");

        //RestClient.Post<APIResponse>(REGISTER, json)
        //    .Then(resp => Debug.Log("resp:" +resp.message))
        //    .Catch(err => Debug.Log("err:" +err.Message));

        RestClient.Request(new RequestHelper
        {
            Uri = REGISTER,
            Method = "POST",
            Timeout = 30,
            Headers = new Dictionary<string, string>
    {
        { "Content-Type","application/json" }
    },
            Body = json
        }).Then(resp => Debug.Log("resp:" + resp.Text)).Catch(err => Debug.Log(err.Message));
    }


}