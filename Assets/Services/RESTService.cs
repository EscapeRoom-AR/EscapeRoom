using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System;
using System.IO;
using Model;

namespace Services
{
    public class RESTService : MonoBehaviour
    {
        private static string HOST = "http://stucom.flx.cat/alu/dam2t02";
        private static string LOGIN = HOST + "/login";
        private static string REGISTER = "/register";
        private static string USER = "/user";
        private static string ROOMS = "/room";
        private static string ROOM = "/room";
        private static string RANKING = "/ranking";

        private string token;


        public APIResponse<string> login(string username, string password)
        {
            Debug.Log("login");
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(String.Format(LOGIN + "?username={0}&password={1}", username, password));
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string jsonResponse = reader.ReadToEnd();
            APIResponse<string> apiResponse = JsonUtility.FromJson<APIResponse<string>>(jsonResponse);
            Debug.Log(jsonResponse);
            Debug.Log("DATA:" +apiResponse.Data);
            return apiResponse;
        }
    }
}