using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model
{

    public class Room
    {
        private int code;
        private string name;
        private string image;
        private bool premium;

        public int Code { get => code; set => code = value; }
        public string Name { get => name; set => name = value; }
        public string Image { get => image; set => image = value; }
        public bool Premium { get => premium; set => premium = value; }
    }
}