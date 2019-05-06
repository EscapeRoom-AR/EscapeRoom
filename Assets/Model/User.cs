using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model
{

    public class User
    {
        private DateTime createdAt;
        private DateTime deletedAt;
        private int code;
        private string username;
        private string email;
        private bool premium;
        private string image;
        private string password;
        private string description;

        public DateTime CreatedAt { get => createdAt; set => createdAt = value; }
        public DateTime DeletedAt { get => deletedAt; set => deletedAt = value; }
        public int Code { get => code; set => code = value; }
        public string Username { get => username; set => username = value; }
        public string Email { get => email; set => email = value; }
        public bool Premium { get => premium; set => premium = value; }
        public string Image { get => image; set => image = value; }
        public string Password { get => password; set => password = value; }
        public string Description { get => description; set => description = value; }
    }

}