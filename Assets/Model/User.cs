using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model
{
    [Serializable]
    public class User
    {
        public DateTime CreatedAt;
        public DateTime DeletedAt;
        public int Code;
        public string Username;
        public string Email;
        public bool Premium;
        public string Image;
        public string Password;
        public string Description;

        public User() { }

        public User(string username, string email, string password)
        {
            this.Username = username;
            this.Email = email;
            this.Password = password;
        }
    }

}