using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model
{

    public class Item
    {
        private int code;
        private string name;
        private string qrCode;
        private Room room;

        public Room Room { get => room; set => room = value; }
        public string QrCode { get => qrCode; set => qrCode = value; }
        public string Name { get => name; set => name = value; }
        public int Code { get => code; set => code = value; }
    }
}