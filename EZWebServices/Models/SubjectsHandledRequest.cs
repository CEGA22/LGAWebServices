using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EZWebServices.Models
{
    public class SubjectsHandledRequest
    {
        public int TeacherID { get; set; }

        public int SubjectID { get; set; }

        public int GradeLevelID { get; set; }
    }
}