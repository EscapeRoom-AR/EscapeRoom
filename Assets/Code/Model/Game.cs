using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model
{
    [Serializable]
    public class Game
    {
        public DateTime CreatedAt { get; set; }
        public DateTime DeletedAt { get; set; }
        public int Code { get; set; }
        public int HintsUsed { get; set; }
        public int Time { get; set; }
        public User User { get; set; }
        public int Room { get; set; }
    }
}
