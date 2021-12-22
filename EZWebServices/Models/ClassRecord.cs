using EZWebServices.Helpers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EZWebServices.Models
{
    public class ClassRecord
    {
        public int ID { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

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

        public string SchoolYearStart { get; set; }

        public int Grade_Level { get; set; }

        public string SubjectName { get; set; }

        public string SectionName { get; set; }

        public int SaveasDraft { get; set; }

        public int SaveDraft { get; set; }

        public int GradingPeriod { get; set; }


        public List<ClassRecord> GetClassRecordsDetails(int ID)
        
        {
            var listReturn = new List<ClassRecord>();

            using (var cn = new SqlConnection(ConnectionHelper.LGAConnection()))
            {
                cn.Open();
                var cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT StudentAccount.ID, StudentAccount.Lastname, StudentAccount.Firstname,  ClassRecords.WrittenWork1, ClassRecords.WrittenWork2,ClassRecords.WrittenWork3, ClassRecords.WrittenWork4, ClassRecords.WrittenWork5,ClassRecords.WrittenWork6, ClassRecords.WrittenWork7,ClassRecords.WrittenWork8,ClassRecords.WrittenWork9,ClassRecords.WrittenWork10, ClassRecords.WrittenWorkTotal, ClassRecords.WrittentWorkPercentage,ClassRecords.TaskPerformance1,ClassRecords.TaskPerformance2,ClassRecords.TaskPerformance3,ClassRecords.TaskPerformance4,ClassRecords.TaskPerformance5,ClassRecords.TaskPerformance6,ClassRecords.TaskPerformance7,ClassRecords.TaskPerformance8,ClassRecords.TaskPerformance9, ClassRecords.TaskPerformance10,ClassRecords.TaskPerformanceTotal,ClassRecords.TaskPerformancePercentage, ClassRecords.InitialGrade, ClassRecords.QuarterlyGrade, Students.SchoolYearStart, Section.Grade_Level, Subjects.SubjectName, Section.SectionName, ClassRecords.SaveasDraft,ClassRecords.SaveDraft, ClassRecords.GradingPeriod FROM ClassRecords JOIN Students ON ClassRecords.Learnersname = Students.StudentID JOIN StudentAccount ON Students.StudentID = StudentAccount.ID  JOIN Section ON Students.Grade_Level = Section.ID JOIN SectionsHandled ON Section.ID = SectionsHandled.Gradelevel JOIN SchoolAccount ON SectionsHandled.Teacher = SchoolAccount.ID JOIN SubjectsHandled ON ClassRecords.SubjectsName = SubjectsHandled.Subject JOIN Subjects ON SubjectsHandled.Subject = Subjects.ID WHERE SectionsHandled.Teacher = @ID ORDER BY CASE WHEN StudentAccount.Lastname = 'Highest possible score' THEN 0 ELSE 1 END, StudentAccount.Lastname asc";

                //SELECT StudentAccount.ID, StudentAccount.Lastname, StudentAccount.Firstname,  ClassRecords.WrittenWork1, ClassRecords.WrittenWork2,ClassRecords.WrittenWork3, ClassRecords.WrittenWork4, ClassRecords.WrittenWork5,ClassRecords.WrittenWork6, ClassRecords.WrittenWork7,ClassRecords.WrittenWork8,ClassRecords.WrittenWork9,ClassRecords.WrittenWork10, ClassRecords.WrittenWorkTotal, ClassRecords.WrittentWorkPercentage,ClassRecords.TaskPerformance1,ClassRecords.TaskPerformance2,ClassRecords.TaskPerformance3,ClassRecords.TaskPerformance4,ClassRecords.TaskPerformance5,ClassRecords.TaskPerformance6,ClassRecords.TaskPerformance7,ClassRecords.TaskPerformance8,ClassRecords.TaskPerformance9,ClassRecords.TaskPerformance10,ClassRecords.TaskPerformanceTotal,ClassRecords.TaskPerformancePercentage, ClassRecords.InitialGrade, ClassRecords.QuarterlyGrade, Students.SchoolYearStart, Section.Grade_Level, Subjects.SubjectName, Section.SectionName, ClassRecords.SaveasDraft,ClassRecords.SaveDraft, ClassRecords.GradingPeriod FROM ClassRecords JOIN Students ON ClassRecords.Learnersname = Students.StudentID JOIN StudentAccount ON Students.StudentID = StudentAccount.ID  JOIN Section ON Students.Grade_Level = Section.ID JOIN SectionsHandled ON Section.ID = SectionsHandled.Gradelevel JOIN SubjectsHandled ON SectionsHandled.Gradelevel = SubjectsHandled.Grade_Level JOIN SchoolAccount ON SectionsHandled.ID = SchoolAccount.ID JOIN Subjects ON SubjectsHandled.Subject = Subjects.ID WHERE SectionsHandled.Teacher = @ID ORDER BY CASE WHEN StudentAccount.Lastname = 'Highest possible score' THEN 0 ELSE 1 END, Lastname asc
                cmd.Parameters.AddWithValue("@ID", ID);
                var dr = cmd.ExecuteReader();
                listReturn = PopulateReturnList(dr);
            }

            //var highestPossibleScore = GetHighestPossibleScore(ID);
            //listReturn.AddRange(highestPossibleScore);

            return listReturn;
        }

        public List<ClassRecord> PopulateReturnList(SqlDataReader dr)
        {

            try
            {
                var listReturn = new List<ClassRecord>();

                while (dr.Read())
                {

                    listReturn.Add(new ClassRecord
                    {
                        ID = int.Parse(dr["ID"].ToString()),
                        Lastname = dr["Lastname"].ToString(),
                        Firstname = dr["Firstname"].ToString(),
                        WrittenWork1 = (double)(dr["WrittenWork1"]),
                        WrittenWork2 = (double)dr["WrittenWork2"],
                        WrittenWork3 = (double)dr["WrittenWork3"],
                        WrittenWork4 = (double)dr["WrittenWork4"],
                        WrittenWork5 = (double)dr["WrittenWork5"],
                        WrittenWork6 = (double)dr["WrittenWork6"],
                        WrittenWork7 = (double)dr["WrittenWork7"],
                        WrittenWork8 = (double)dr["WrittenWork8"],
                        WrittenWork9 = (double)dr["WrittenWork9"],
                        WrittenWork10 = (double)dr["WrittenWork10"],
                        WrittenWorkTotal = (double)dr["WrittenWorkTotal"],
                        WrittenWorkPercentage = (double)dr["WrittentWorkPercentage"],
                        TaskPerformance1 = (double)dr["TaskPerformance1"],
                        TaskPerformance2 = (double)dr["TaskPerformance2"],
                        TaskPerformance3 = (double)dr["TaskPerformance3"],
                        TaskPerformance4 = (double)dr["TaskPerformance4"],
                        TaskPerformance5 = (double)dr["TaskPerformance5"],
                        TaskPerformance6 = (double)dr["TaskPerformance6"],
                        TaskPerformance7 = (double)dr["TaskPerformance7"],
                        TaskPerformance8 = (double)dr["TaskPerformance8"],
                        TaskPerformance9 = (double)dr["TaskPerformance9"],
                        TaskPerformance10 = (double)dr["TaskPerformance10"],
                        TaskPerformanceTotal = (double)dr["TaskPerformanceTotal"],
                        TaskPerformancePercentage = (double)dr["TaskPerformancePercentage"],
                        InitialGrade = (double)dr["InitialGrade"],
                        QuarterlyGrade = int.Parse(dr["QuarterlyGrade"].ToString()),
                        SubjectName = dr["SubjectName"].ToString(),
                        SchoolYearStart = dr["SchoolYearStart"].ToString(),
                        Grade_Level = int.Parse(dr["Grade_Level"].ToString()),
                        SectionName = dr["SectionName"].ToString(),
                        SaveasDraft = int.Parse(dr["SaveasDraft"].ToString()),
                        SaveDraft = int.Parse(dr["SaveDraft"].ToString()),
                        GradingPeriod = int.Parse(dr["GradingPeriod"].ToString()),
                    });
                }

                return listReturn;
            }
            catch (Exception e)
            {

                throw;
            }
        }


        public List<ClassRecord> GetClassRecordsDetailsStudent(int ID)
        {
            var listReturn = new List<ClassRecord>();

            using (var cn = new SqlConnection(ConnectionHelper.LGAConnection()))
            {
                cn.Open();
                var cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT distinct StudentAccount.ID, StudentAccount.Lastname, StudentAccount.Firstname,  ClassRecords.WrittenWork1, ClassRecords.WrittenWork2,ClassRecords.WrittenWork3, ClassRecords.WrittenWork4, ClassRecords.WrittenWork5,ClassRecords.WrittenWork6, ClassRecords.WrittenWork7,ClassRecords.WrittenWork8,ClassRecords.WrittenWork9,ClassRecords.WrittenWork10, ClassRecords.WrittenWorkTotal, ClassRecords.WrittentWorkPercentage,ClassRecords.TaskPerformance1,ClassRecords.TaskPerformance2,ClassRecords.TaskPerformance3,ClassRecords.TaskPerformance4,ClassRecords.TaskPerformance5,ClassRecords.TaskPerformance6,ClassRecords.TaskPerformance7,ClassRecords.TaskPerformance8,ClassRecords.TaskPerformance9,ClassRecords.TaskPerformance10,ClassRecords.TaskPerformanceTotal,ClassRecords.TaskPerformancePercentage, ClassRecords.InitialGrade, ClassRecords.QuarterlyGrade, Students.SchoolYearStart, Section.Grade_Level, Subjects.SubjectName, Section.SectionName, ClassRecords.SaveasDraft,ClassRecords.SaveDraft, ClassRecords.GradingPeriod FROM ClassRecords JOIN Students ON ClassRecords.Learnersname = Students.StudentID JOIN StudentAccount ON Students.StudentID = StudentAccount.ID  JOIN Section ON Students.Grade_Level = Section.ID JOIN SectionsHandled ON Section.ID = SectionsHandled.Gradelevel JOIN SchoolAccount ON SectionsHandled.Teacher = SchoolAccount.ID  JOIN SubjectsHandled ON ClassRecords.SubjectsName = SubjectsHandled.Subject JOIN Subjects ON SubjectsHandled.Subject = Subjects.ID WHERE StudentAccount.ID = @ID";

                //SELECT StudentAccount.ID, StudentAccount.Lastname, StudentAccount.Firstname,  ClassRecords.WrittenWork1, ClassRecords.WrittenWork2,ClassRecords.WrittenWork3, ClassRecords.WrittenWork4, ClassRecords.WrittenWork5,ClassRecords.WrittenWork6, ClassRecords.WrittenWork7,ClassRecords.WrittenWork8,ClassRecords.WrittenWork9,ClassRecords.WrittenWork10, ClassRecords.WrittenWorkTotal, ClassRecords.WrittentWorkPercentage,ClassRecords.TaskPerformance1,ClassRecords.TaskPerformance2,ClassRecords.TaskPerformance3,ClassRecords.TaskPerformance4,ClassRecords.TaskPerformance5,ClassRecords.TaskPerformance6,ClassRecords.TaskPerformance7,ClassRecords.TaskPerformance8,ClassRecords.TaskPerformance9,ClassRecords.TaskPerformance10,ClassRecords.TaskPerformanceTotal,ClassRecords.TaskPerformancePercentage, ClassRecords.InitialGrade, ClassRecords.QuarterlyGrade, Students.SchoolYearStart, Section.Grade_Level, Subjects.SubjectName, Section.SectionName, ClassRecords.SaveasDraft,ClassRecords.SaveDraft, ClassRecords.GradingPeriod FROM ClassRecords JOIN Students ON ClassRecords.Learnersname = Students.StudentID JOIN StudentAccount ON Students.StudentID = StudentAccount.ID  JOIN Section ON Students.Grade_Level = Section.ID JOIN SectionsHandled ON Section.ID = SectionsHandled.Gradelevel JOIN SubjectsHandled ON SectionsHandled.Gradelevel = SubjectsHandled.Grade_Level JOIN SchoolAccount ON SectionsHandled.ID = SchoolAccount.ID JOIN Subjects ON SubjectsHandled.Subject = Subjects.ID WHERE StudentAccount.ID = @ID
                cmd.Parameters.AddWithValue("@ID", ID);
                var dr = cmd.ExecuteReader();
                listReturn = PopulateReturnLists(dr);
            }

            //var highestPossibleScore = GetHighestPossibleScore(ID);
            //listReturn.AddRange(highestPossibleScore);

            return listReturn;
        }

        public List<ClassRecord> PopulateReturnLists(SqlDataReader dr)
        {

            try
            {
                var listReturn = new List<ClassRecord>();

                while (dr.Read())
                {

                    listReturn.Add(new ClassRecord
                    {
                        ID = int.Parse(dr["ID"].ToString()),
                        Lastname = dr["Lastname"].ToString(),
                        Firstname = dr["Firstname"].ToString(),
                        WrittenWork1 = (double)(dr["WrittenWork1"]),
                        WrittenWork2 = (double)dr["WrittenWork2"],
                        WrittenWork3 = (double)dr["WrittenWork3"],
                        WrittenWork4 = (double)dr["WrittenWork4"],
                        WrittenWork5 = (double)dr["WrittenWork5"],
                        WrittenWork6 = (double)dr["WrittenWork6"],
                        WrittenWork7 = (double)dr["WrittenWork7"],
                        WrittenWork8 = (double)dr["WrittenWork8"],
                        WrittenWork9 = (double)dr["WrittenWork9"],
                        WrittenWork10 = (double)dr["WrittenWork10"],
                        WrittenWorkTotal = (double)dr["WrittenWorkTotal"],
                        WrittenWorkPercentage = (double)dr["WrittentWorkPercentage"],
                        TaskPerformance1 = (double)dr["TaskPerformance1"],
                        TaskPerformance2 = (double)dr["TaskPerformance2"],
                        TaskPerformance3 = (double)dr["TaskPerformance3"],
                        TaskPerformance4 = (double)dr["TaskPerformance4"],
                        TaskPerformance5 = (double)dr["TaskPerformance5"],
                        TaskPerformance6 = (double)dr["TaskPerformance6"],
                        TaskPerformance7 = (double)dr["TaskPerformance7"],
                        TaskPerformance8 = (double)dr["TaskPerformance8"],
                        TaskPerformance9 = (double)dr["TaskPerformance9"],
                        TaskPerformance10 = (double)dr["TaskPerformance10"],
                        TaskPerformanceTotal = (double)dr["TaskPerformanceTotal"],
                        TaskPerformancePercentage = (double)dr["TaskPerformancePercentage"],
                        InitialGrade = (double)dr["InitialGrade"],
                        QuarterlyGrade = int.Parse(dr["QuarterlyGrade"].ToString()),
                        SubjectName = dr["SubjectName"].ToString(),
                        SchoolYearStart = dr["SchoolYearStart"].ToString(),
                        Grade_Level = int.Parse(dr["Grade_Level"].ToString()),
                        SectionName = dr["SectionName"].ToString(),
                        SaveasDraft = int.Parse(dr["SaveasDraft"].ToString()),
                        SaveDraft = int.Parse(dr["SaveDraft"].ToString()),
                        GradingPeriod = int.Parse(dr["GradingPeriod"].ToString()),
                    });
                }

                return listReturn;
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}