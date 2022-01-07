using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EZWebServices.Models
{
    public class LoginResult
    {
        public bool IsSuccess { get; set; }

        public int ID { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public int IsAdmin { get; set; }

        public int IsFaculty { get; set; }

        public string Password { get; set; }

        public byte[] TeacherProfile { get; set; }

        public string Fullname 
        {
            get 
            {
                return $"{Firstname} {Lastname}";
            }
        }
    }
}