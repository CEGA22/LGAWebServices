using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EZWebServices.Models
{
    public class StudentLoginRequest
    {
        
        public string StudentNumber { get; set; }

        public string Password { get; set; }

    }
}