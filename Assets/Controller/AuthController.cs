using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuthController : MonoBehaviour
{

    public bool IsAuthenticated()
    {
        return PlayerPrefs.GetString("token") != null;
    }
}
