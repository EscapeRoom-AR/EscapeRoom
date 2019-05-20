using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model
{
    public class APIResponse
    {
        private int _code;
        private string _message;
       // private T data;

        public int code { get => _code; set => _code = value; }
        public string message { get => _message; set => _message = value; }
      //  public T Data { get => data; set => data = value; }
    }
}
