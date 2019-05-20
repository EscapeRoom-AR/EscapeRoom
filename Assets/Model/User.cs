using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model
{
    [Serializable]
    public class User
    {
        public DateTime createdAt;
        public DateTime deletedAt;
        public int code;
        public string username;
        public string email;
        public bool premium;
        public string image;
        public string password;
        public string description;

        public User(string username, string email, string password)
        {
            this.username = username;
            this.email = email;
            this.password = password;
        }
    }

}