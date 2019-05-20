using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Model;
using UnityEditor;
using Proyecto26;

public class REST2 : MonoBehaviour
{
    private static string HOST = "http://stucom.flx.cat/alu/dam2t02";
    private static string LOGIN = HOST + "/login";
    private static string REGISTER = HOST + "/register";
    private static string USER = HOST + "/user";
    private static string ROOMS = HOST + "/room";
    private static string ROOM = HOST + "/room";
    private static string RANKING = HOST + "/ranking";



    public void register(User user)
    {
        RestClient.Post<string>(REGISTER, JsonUtility.ToJson(user))
            .Then((resp) => Debug.Log(resp))
            .Catch((err) => Debug.Log(err));
    }

}
