using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model
{

    public class Game
    {
        private DateTime createdAt;
        private DateTime deletedAt;
        private int code;
        private int hintsUsed;
        private int time;
        private User user;
        private Room room;

        public DateTime CreatedAt { get => createdAt; set => createdAt = value; }
        public DateTime DeletedAt { get => deletedAt; set => deletedAt = value; }
        public int Code { get => code; set => code = value; }
        public int HintsUsed { get => hintsUsed; set => hintsUsed = value; }
        public int Time { get => time; set => time = value; }
        public User User { get => user; set => user = value; }
        public Room Room { get => room; set => room = value; }
    }
}
