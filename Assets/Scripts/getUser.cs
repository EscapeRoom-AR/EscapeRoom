using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Services;

public class getUser : MonoBehaviour
{
    private RESTService restService = new RESTService();
    // Start is called before the first frame update
    void Start()
    {
        restService.GetUser();
    }
}
