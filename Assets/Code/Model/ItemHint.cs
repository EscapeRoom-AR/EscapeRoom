using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model
{
    [Serializable]
    public class GameHint
    {
        public int Id;
        public int RoomCode;
        public string Hint;
        public int Phase;

    }
}
