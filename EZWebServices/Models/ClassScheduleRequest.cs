using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EZWebServices.Models
{
    public class ClassScheduleRequest
    {
        public int ID { get; set; }

        public string Subject { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public int TeacherID { get; set; }

        public int GradeLevel { get; set; }

        public string WeekDay { get; set; }
    }
}