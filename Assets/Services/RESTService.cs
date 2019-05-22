using System.Collections;
using UnityEngine;
using System.Net;
using System;
using System.IO;
using System.Net.Http;
using Model;
using UnityEngine.Networking;

namespace Services
{

    public class RESTService : MonoBehaviour
    {
        private static readonly HttpClient client = new HttpClient();

        private static string HOST = "http://stucom.flx.cat/alu/dam2t02";
        private static string LOGIN = HOST + "/login";
        private static string REGISTER = HOST + "/register";
        private static string USER = HOST + "/user";
        private static string ROOMS = HOST + "/room";
        private static string ROOM = HOST + "/room";
        private static string RANKING = HOST + "/ranking";

        private TokenHolder token;

        public APIResponse<TokenHolder> login(string username, string password)
        {
            Debug.Log("login");
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(String.Format(LOGIN + "?username={0}&password={1}", username, password));
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string jsonResponse = reader.ReadToEnd();
            APIResponse<TokenHolder> apiResponse = JsonUtility.FromJson<APIResponse<TokenHolder>>(jsonResponse);
            Debug.Log(jsonResponse);
            Debug.Log("DATA:" + apiResponse.data.token);
            return apiResponse;
        }

        public void Register(User user)
        {
            StartCoroutine(Upload(user));
        }

        IEnumerator Upload(User user)
        {
            UnityWebRequest www = UnityWebRequest.Post(REGISTER + "?username=" + user.username + "&email=" + user.email + "&password=" + user.password, "");
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

        [Serializable]
        public class TokenHolder
        {
            public string token;
        }
    }
}