using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EZWebServices.Models
{
    public class StudentRequest
    {
        public int ID { get; set; }

        public string Lastname { get; set; }

        public string Middlename { get; set; }

        public string Firstname { get; set; }

        public string Address { get; set; }

        public DateTime Birthday { get; set; }

        public string ParentsName { get; set; }

        public string StudentNumber { get; set; }

        public string Password { get; set; }

        public string mobileNumber { get; set; }

        public string Email { get; set; }

        public string Gender { get; set; }

        public string Grade_Level { get; set; }

        public string SectionName { get; set; }

        public byte[] StudentProfile { get; set; }

        public int SchoolYearStart { get; set; }

        public int SchoolYearEnd { get; set; }

        public int GradeLevelid { get; set; }

        public int SubjectsName { get; set; }

    }
}