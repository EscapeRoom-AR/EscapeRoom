using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model
{
    public class APIResponse<T>
    {
        private int code;
        private string message;
        private T data;

        public int Code { get => code; set => code = value; }
        public string Message { get => message; set => message = value; }
        public T Data { get => data; set => data = value; }
    }
}
