using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EZWebServices.Models
{
    public class SubjectsHandled
    {
        public int Id { get; set; }

        public int TeacherId { get; set; }

        public string TeacherName { get; set; }

        public int SubjectId { get; set; }

        public string SubjectName { get; set; }

        public string SubjectCode { get; set; }

        public int GradeLevelId { get; set; }

        public string GradeLevel { get; set; }
    }
}