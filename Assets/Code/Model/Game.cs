using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model
{
    [Serializable]
    public class Game
    {
        public DateTime CreatedAt;
        public DateTime DeletedAt;
        public int Code;
        public int HintsUsed;
        public int Time;
        public User User;
        public int Room;
        public int Score;
    }
}
