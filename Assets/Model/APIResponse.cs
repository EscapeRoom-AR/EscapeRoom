using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model
{
    [Serializable]
    public class APIResponse<T>
    {
        public APIResponse(int code, string message)
        {
            this.code = code;
            this.message = message;
        }

        public APIResponse() { }

        public int code;
        public string message;
        public T data;

        public bool IsError()
        {
            return code == 0;
        }
    }
}
