using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System;
using System.IO;
using System.Text;
using System.Net.Http;
using Model;
using System.Threading.Tasks;
using UnityEngine.Networking;

namespace Services
{
    /*
    public class RESTService : ScriptableObject
    {
        private static readonly HttpClient client = new HttpClient();

        private static string HOST = "http://stucom.flx.cat/alu/dam2t02";
        private static string LOGIN = HOST + "/login";
        private static string REGISTER = HOST + "/register";
        private static string USER = HOST + "/user";
        private static string ROOMS = HOST + "/room";
        private static string ROOM = HOST + "/room";
        private static string RANKING = HOST + "/ranking";

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
            Debug.Log("DATA:" + apiResponse.Data);
            return apiResponse;
        }

        public APIResponse<string> register(User user)
        {
            Debug.Log("register");
            HttpWebRequest request = this.post(REGISTER, user);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string jsonResponse = reader.ReadToEnd();
            APIResponse<string> apiResponse = JsonUtility.FromJson<APIResponse<string>>(jsonResponse);
            Debug.Log(jsonResponse);
            Debug.Log("DATA:" + apiResponse.Data);
            return apiResponse;
        }

        public HttpWebRequest post(string url, object data)
        {
            Debug.Log("post");
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/json";
            Stream reqStream = request.GetRequestStream();
            string json = JsonUtility.ToJson(data);
            byte[] byteArray = Encoding.UTF8.GetBytes(json);
            reqStream.Write(byteArray, 0, byteArray.Length);
            reqStream.Close();
            return request;
        }

        public void register2(User user)
        {
            Debug.Log("register2");
            StringContent content = new StringContent((JsonUtility.ToJson(user)), Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(REGISTER, content).Result;
            string responseString = response.Content.ReadAsStringAsync().Result;
            Debug.Log(responseString);
        }

        public async Task Register3(User user)
        {
            UnityWebRequest request = UnityWebRequest.Post(REGISTER, JsonUtility.ToJson(user));
            request.SetRequestHeader("Content-Type", "application/json");
          //  request.SendWebRequest().completed.value += Debug.Log("de");
        }

        public IEnumerator Upload(User user)
        {
            Debug.Log("UPLOAD");
            UnityWebRequest www = UnityWebRequest.Post(REGISTER, JsonUtility.ToJson(user));
            www.SetRequestHeader("Content-Type", "application/json");
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
            }
        }

    }
    */
}