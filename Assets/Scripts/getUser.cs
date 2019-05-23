using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Services;

public class getUser : MonoBehaviour
{
    public RESTService restService;
    // Start is called before the first frame update
    void Start()
    {
        restService.GetUser((response) =>
        {
            print(response.message);
            print(JsonUtility.ToJson(response.data));
        });
    }
}
