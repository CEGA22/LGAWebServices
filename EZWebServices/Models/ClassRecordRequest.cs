using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EZWebServices.Models
{
    public class ClassRecordRequest
    {
        public int ID { get; set; }

        public int TeacherID { get; set; }

        public string Lastname { get; set; }

        public string Firstname { get; set; }

        public double WrittenWork1 { get; set; }

        public double WrittenWork2 { get; set; }

        public double WrittenWork3 { get; set; }

        public double WrittenWork4 { get; set; }

        public double WrittenWork5 { get; set; }

        public double WrittenWork6 { get; set; }

        public double WrittenWork7 { get; set; }

        public double WrittenWork8 { get; set; }

        public double WrittenWork9 { get; set; }

        public double WrittenWork10 { get; set; }

        public double WrittenWorkTotal { get; set; }

        public double WrittenWorkPercentage { get; set; }

        public double TaskPerformance1 { get; set; }

        public double TaskPerformance2 { get; set; }

        public double TaskPerformance3 { get; set; }

        public double TaskPerformance4 { get; set; }

        public double TaskPerformance5 { get; set; }

        public double TaskPerformance6 { get; set; }

        public double TaskPerformance7 { get; set; }

        public double TaskPerformance8 { get; set; }

        public double TaskPerformance9 { get; set; }

        public double TaskPerformance10 { get; set; }

        public double TaskPerformanceTotal { get; set; }

        public double TaskPerformancePercentage { get; set; }

        public double InitialGrade { get; set; }

        public double QuarterlyGrade { get; set; }

        public int SubjectsName { get; set; }

        public string SchoolYearStart { get; set; }

        public int Grade_Level { get; set; }

        public string SectionName { get; set; }

        public int SaveasDraft { get; set; }

        public int SaveDraft { get; set; }

        public int GradingPeriod { get; set; }

        public string studentname { get; set; }

        public string subjectname { get; set; }

        public double finalgrade { get; set; }

        public double average { get; set; }

        public DateTime datemodified { get; set; }
    }
}