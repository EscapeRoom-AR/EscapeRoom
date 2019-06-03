using Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using System.Threading.Tasks;

namespace Services
{

    public class RESTService : MonoBehaviour
    {
        // These are callbacks definitions (like functional interfaces in java).
        public delegate void ResponseCallback<T>(APIResponse<T> apiResponse);
        public delegate void ImageCallBack(Sprite sprite);

        // Endpoints of the webservice.
        private static readonly string HOST = "http://stucom.flx.cat/alu/dam2t02";
        private static readonly string LOGIN = HOST + "/login?username={0}&password={1}";
        private static readonly string REGISTER = HOST + "/register?username={0}&email={1}&password={2}";
        private static readonly string USER = HOST + "/user";
        private static readonly string UPDATE_USER = USER + "?username={0}&email={1}&description={2}";
        private static readonly string UPDATE_USER_PHOTO = USER + "?photo={0}";
        private static readonly string ROOMS = HOST + "/room";
        private static readonly string ROOM = HOST + "/room";
        private static readonly string PHOTO = "http://alexraspberry.ddns.net:8080/post.php";
        private static readonly string RANKING = HOST + "/paginated-ranking?room={0}&offset={1}";
        private static readonly string PASSWORD = HOST + "/password?password={0}&password-old={1}";
        private static readonly string DELETE_ACCOUNT = HOST + "/user?password={0}";
        private static readonly string HINTS = HOST + "/hints/{0}";
        private static readonly string GAME = HOST + "/game?hints={0}&time={1}&room={2}";


        public void Login(User user,ResponseCallback<string> listener)
        {
            StartCoroutine(Request(String.Format(LOGIN, user.Username, user.Password), "GET", listener));
        }

        public void Register(User user, ResponseCallback<string> listener)
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

        public void UpdateUser(User user, ResponseCallback<User> listener)
        {
            StartCoroutine(Request(String.Format(UPDATE_USER,user.Username,user.Email,user.Description), "PUT", listener));
        }

        public void ChangePassword(string oldPassword, string password, ResponseCallback<string> listener)
        {
            StartCoroutine(Request(String.Format(PASSWORD, password, oldPassword), "PUT", listener));
        }

        public void DeleteAccount(string password, ResponseCallback<string> listener)
        {
            StartCoroutine(Request(String.Format(DELETE_ACCOUNT, password), "DELETE", listener));
        }

        public void GetRanking(int room,int offset, ResponseCallback<RankingHolder> listener)
        {
            StartCoroutine(Request(String.Format(RANKING, room,offset), "GET", listener));
        }

        public void UpdateUserImage(string photoUrl, ResponseCallback<User> listener)
        {
            StartCoroutine(Request(String.Format(UPDATE_USER_PHOTO, photoUrl), "PUT", listener));
        }

        public void PostPhoto(string photo, ResponseCallback<String> listener)
        {
            StartCoroutine(PostImage(photo,listener));
        }

        public void PostGame(Game game, ResponseCallback<String> listener)
        {
            StartCoroutine(Request(String.Format(GAME, game.HintsUsed, game.Time, game.Room),"POST",listener));
        }

        public void GetHints(int roomId, ResponseCallback<List<GameHint>> listener)
        {
            StartCoroutine(Request(String.Format(HINTS, roomId), "GET", listener));
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

        // Generic method for making a request to the web service, unfortunately
        // parameters must be embedded in url in case of POST or PUT. (should be fixed)
        IEnumerator Request<T>(string URI, string method, ResponseCallback<T> callBack)
        {
            UnityWebRequest www = new UnityWebRequest(URI, method) { downloadHandler = new DownloadHandlerBuffer() };
            www.SetRequestHeader("Authorization", PlayerPrefs.GetString("token"));
            //www.SetRequestHeader("Authorization", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.NzY.7FWNYxsywRKDcWgyLsQ3QSbFyBTaZ0JIB3dZ1eyIflM");
            yield return www.SendWebRequest();
            APIResponse<T> apiResponse;
            print(www.downloadHandler.text);
            if (www.isNetworkError || www.isHttpError)
                apiResponse = new APIResponse<T>(0, www.error);
            else
                apiResponse = JsonUtility.FromJson<APIResponse<T>>(www.downloadHandler.text);
            callBack(apiResponse);
        }


        IEnumerator PostImage(string base64, ResponseCallback<string> callBack)
        {
            WWWForm form = new WWWForm();
            form.AddField("photo", base64);
            UnityWebRequest www = UnityWebRequest.Post(PHOTO, form);
            www.SetRequestHeader("Authorization", PlayerPrefs.GetString("token"));
            www.chunkedTransfer = false;
            yield return www.SendWebRequest();
            APIResponse<string> apiResponse;
            if (www.isNetworkError || www.isHttpError)
                apiResponse = new APIResponse<string>(0, www.error);
            else
                apiResponse = JsonUtility.FromJson<APIResponse<string>>(www.downloadHandler.text);
            callBack(apiResponse);
        }

        /*
        IEnumerator UglyPost<T>(string URI, string body, ResponseCallback<T> callBack)
        {
            UnityWebRequest www = new UnityWebRequest(URI, "POST");
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Authorization", PlayerPrefs.GetString("token"));
            //string part1 = body.Substring(0, body.Length / 2);
            //string part2 = body.Substring(body.Length / 2 + 1);
            //www.SetRequestHeader("body1", part1);
            //www.SetRequestHeader("body2", part2);
            yield return www.SendWebRequest();
            APIResponse<T> apiResponse;
            print(www.downloadHandler.text);
            if (www.isNetworkError || www.isHttpError)
                apiResponse = new APIResponse<T>(0, www.error);
            else
                apiResponse = JsonUtility.FromJson<APIResponse<T>>(www.downloadHandler.text);
            callBack(apiResponse);
        }*/
        /*
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

        async Task Post4Async(string URI, string base64)
        {
            print("starting post...");
            HttpClient client = new HttpClient();
            var values = new Dictionary<string, string>{ { "photo", base64 } };

            var content = new FormUrlEncodedContent(values);

            var response = await client.PutAsync(URI, content);

            var responseString = await response.Content.ReadAsStringAsync();

            print(responseString + ",");
        }

        IEnumerator Upload()
        {
            var formData = Encoding.UTF8.GetBytes("{ 'photo': 'aaa'}");
            UploadHandler uploader = new UploadHandlerRaw(formData);
            uploader.contentType = "application/json";
            DownloadHandler downloader = new DownloadHandlerBuffer();
            UnityWebRequest www = new UnityWebRequest();
            www.method = UnityWebRequest.kHttpVerbPOST;
            www.uploadHandler = uploader;
            www.downloadHandler = downloader;
            www.chunkedTransfer = false;
            www.url = PHOTO;
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                print("Response: " + www.downloadHandler.text);
                print("Data: " + Encoding.UTF8.GetString(www.uploadHandler.data));
            }
        }

        void Post6()
        {
            UnityWebRequest webRequest = new UnityWebRequest(PHOTO, "POST");

            byte[] encodedPayload = new System.Text.UTF8Encoding().GetBytes("{ 'photo': 'aaa'}");
            webRequest.uploadHandler = (UploadHandler)new UploadHandlerRaw(encodedPayload);
            webRequest.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            webRequest.SetRequestHeader("Content-Type", "application/json");
            webRequest.SetRequestHeader("cache-control", "no-cache");
            webRequest.chunkedTransfer = false;
            UnityWebRequestAsyncOperation requestHandel = webRequest.SendWebRequest();
            requestHandel.completed += delegate (AsyncOperation pOperation) {
                print("Response: " + webRequest.downloadHandler.text);
                print("Data: " + Encoding.UTF8.GetString(webRequest.uploadHandler.data));
            };
        } */





    }
}