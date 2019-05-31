using Model;
using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace Services
{

    public class RESTService : MonoBehaviour
    {
        // These are callbacks definitions (like functional interfaces in java).
        public delegate void ResponseCallback<T>(APIResponse<T> apiResponse);
        public delegate void ImageCallBack(Sprite sprite);

        // Endpoints of the webservice.
        private static string HOST = "http://stucom.flx.cat/alu/dam2t02";
        private static string LOGIN = HOST + "/login?username={0}&password={1}";
        private static string REGISTER = HOST + "/register?username={0}&email={1}&password={2}";
        private static string USER = HOST + "/user";
        private static string UPDATE_USER = USER + "?username={0}&email={1}&description={2}";
        private static string ROOMS = HOST + "/room";
        private static string ROOM = HOST + "/room";
        private static string RANKING = HOST + "/ranking";
        private static string PHOTO = HOST + "/photo";

        public void Login(User user,ResponseCallback<string> listener)
        {
            StartCoroutine(Request(String.Format(LOGIN, user.Username, user.Password), "GET", listener));
        }

        public void Register(User user, ResponseCallback<string> listener)
        {
            StartCoroutine(Request(String.Format(REGISTER,user.Username,user.Email,user.Password),"POST", listener));
        }

        public void GetUser(ResponseCallback<User> listener)
        {
            StartCoroutine(Request(USER,"GET",listener));
        }

        public void GetImage(string url, ImageCallBack callBack)
        {
            StartCoroutine(ImageCoroutine(url, callBack));
        }

        public void UpdateUser(User user, ResponseCallback<User> listener)
        {
            StartCoroutine(Request(String.Format(UPDATE_USER,user.Username,user.Email,user.Description), "PUT", listener));
        }

        public void PostPhoto(string photo, ResponseCallback<string> listener)
        {
            StartCoroutine(Post3(PHOTO,photo, listener));
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

        IEnumerator Post<T>(string URI,string base64, ResponseCallback<T> callBack)
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes("{ 'photo': '" + base64 + "'}");
            UnityWebRequest www = new UnityWebRequest
            {
                url = URI,
                method = "PUT",
                downloadHandler = new DownloadHandlerBuffer(),
                uploadHandler = new UploadHandlerRaw(bodyRaw),
                chunkedTransfer = false
            };
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Accept", "application/json");
            www.SetRequestHeader("Authorization", PlayerPrefs.GetString("token"));
            yield return www.SendWebRequest();
            print(www.downloadHandler.text);
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
            while (!www.downloadHandler.isDone) {  }
            Texture2D texture = ((DownloadHandlerTexture)www.downloadHandler).texture as Texture2D;
            callback(Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f));
        }

        IEnumerator Post2<T>(string URI, string base64, ResponseCallback<T> callBack)
        {
            WWW www;
            Hashtable postHeader = new Hashtable();
            postHeader.Add("Content-Type", "application/json");
            postHeader.Add("Authorization", PlayerPrefs.GetString("token"));
            var formData = Encoding.UTF8.GetBytes("{ 'photo': '" + base64 + "'}");
            print(Encoding.UTF8.GetString(formData));
            www = new WWW(URI, formData, postHeader);
            yield return www;
            APIResponse<T> apiResponse;
            print(www.text + "," + www.error);
            if (www.error != "")
                apiResponse = new APIResponse<T>(0, www.error);
            else
                apiResponse = JsonUtility.FromJson<APIResponse<T>>(www.text);
            callBack(apiResponse);
        }

        IEnumerator Post3<T>(string URI, string base64, ResponseCallback<T> callBack)
        {
            WWWForm form = new WWWForm();
            form.AddField("photo", base64);
            UnityWebRequest www = UnityWebRequest.Post(URI, form);
            www.SetRequestHeader("Authorization", PlayerPrefs.GetString("token"));
            www.chunkedTransfer = false;
            yield return www.SendWebRequest();
            APIResponse<T> apiResponse;
            print(www.downloadHandler.text);
            print(www.isDone);
            if (www.error != "")
                apiResponse = new APIResponse<T>(0, www.error);
            else
                apiResponse = JsonUtility.FromJson<APIResponse<T>>(www.downloadHandler.text);
            callBack(apiResponse);
        }
    }
}