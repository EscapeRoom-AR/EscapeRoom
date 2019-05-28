﻿using Model;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace Services
{

    public class RESTService : MonoBehaviour
    {
        // These are callbacks definitions (like functional interfaces in java).
        public delegate void ResponseCallback<T>(APIResponse<T> apiResponse);
        public delegate void ImageCallBack(Sprite sprite);

        // Holds token as web service returns { ... , data: { token: "tokenValue"} }
        // Useful for using APIResponse.
        [Serializable]
        public class TokenHolder
        {
            public string token;
        }

        // Endpoints of the webservice.
        private static readonly string HOST = "http://stucom.flx.cat/alu/dam2t02";
        private static readonly string LOGIN = HOST + "/login?username={0}&password={1}";
        private static readonly string REGISTER = HOST + "/register?username={0}&email={1}&password={2}";
        private static readonly string USER = HOST + "/user";
        private static readonly string UPDATE_USER = USER + "?username={0}&email={1}&description={2}";
        private static readonly string ROOMS = HOST + "/room";
        private static readonly string ROOM = HOST + "/room";
        private static readonly string RANKING = HOST + "/ranking";
        private static readonly string PASSWORD = HOST + "/password?password={0}&password-old={1}";

        public void Login(User user, ResponseCallback<TokenHolder> listener)
        {
            StartCoroutine(Request(String.Format(LOGIN, user.Username, user.Password), "GET", listener));
        }

        public void Register(User user, ResponseCallback<TokenHolder> listener)
        {
            StartCoroutine(Request(String.Format(REGISTER, user.Username, user.Email, user.Password), "POST", listener));
        }

        public void GetUser(ResponseCallback<User> listener)
        {
            StartCoroutine(Request(USER, "GET", listener));
        }

        public void GetImage(string url, ImageCallBack callBack)
        {
            StartCoroutine(ImageCoroutine(url, callBack));
        }

        public void UpdateUser(User user, ResponseCallback<string> listener)
        {
            StartCoroutine(Request(String.Format(UPDATE_USER, user.Username, user.Email, user.Description), "PUT", listener));
        }

        public void ChangePassword(string oldPassword, string password, ResponseCallback<string> listener)
        {
            StartCoroutine(Request(String.Format(PASSWORD, oldPassword, password), "PUT", listener));
        }

        // Generic method for making a request to the web service, unfortunately
        // parameters must be embedded in url in case of POST or PUT. (should be fixed)
        IEnumerator Request<T>(string URI, string method, ResponseCallback<T> callBack)
        {
            UnityWebRequest www = new UnityWebRequest(URI, method);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Authorization", PlayerPrefs.GetString("token"));
            yield return www.SendWebRequest();
            APIResponse<T> apiResponse;
            if (www.isNetworkError || www.isHttpError)
                apiResponse = new APIResponse<T>(0, www.error);
            else
                apiResponse = JsonUtility.FromJson<APIResponse<T>>(www.downloadHandler.text);
            callBack(apiResponse);
        }

        // Downloads a sprite given a url.
        IEnumerator ImageCoroutine(string url, ImageCallBack callback)
        {
            UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
            yield return www.SendWebRequest();
            while (!www.downloadHandler.isDone) { }
            Texture2D texture = ((DownloadHandlerTexture)www.downloadHandler).texture as Texture2D;
            callback(Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f));
        }
    }
}