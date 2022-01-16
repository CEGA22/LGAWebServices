using EZWebServices.Helpers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EZWebServices.Models
{
    public class Subjects
    {
        public int ID { get; set; }

        public string SubjectCode { get; set; }

        public string SubjectName { get; set; }

        public int GradeLevel { get; set; }

        public string Grade_Level { get; set; }
     
        public List<Subjects> GetSubjects()
        {
            var listReturn = new List<Subjects>();

            using (var cn = new SqlConnection(ConnectionHelper.LGAConnection()))
            {
                cn.Open();
                var cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT Subjects.ID, Subjects.SubjectCode, Subjects.SubjectName, Subjects.Grade_Level, YearLevel.Grade_Level AS 'GradeLevel' FROM Subjects JOIN YearLevel ON Subjects.Grade_Level = YearLevel.ID";
                //cmd.Parameters.AddWithValue("@ID", ID);
                var dr = cmd.ExecuteReader();
                listReturn = PopulateReturnList(dr);
            }

            return listReturn;
        }

        public IEnumerable<Subjects> GetSubjectsByGradeLevelId(int id) 
        {
            IEnumerable<Subjects> listReturn = Enumerable.Empty<Subjects>();

            using (var cn = new SqlConnection(ConnectionHelper.LGAConnection()))
            {
                cn.Open();
                var cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT Subjects.ID, Subjects.SubjectCode, Subjects.SubjectName, Subjects.Grade_Level, YearLevel.Grade_Level AS 'GradeLevel' FROM Subjects JOIN YearLevel ON Subjects.Grade_Level = YearLevel.ID WHERE YearLevel.ID=@Grade_Level";
                cmd.Parameters.AddWithValue("@Grade_Level", id);
                var dr = cmd.ExecuteReader();
                listReturn = PopulateReturnList(dr);
            }

            return listReturn;
        }

        public IEnumerable<SubjectsHandled> GetSubjectsHandled(int teacherId, int gradeLevelId) 
        {
            IEnumerable<SubjectsHandled> listReturn = Enumerable.Empty<SubjectsHandled>();

            using (var cn = new SqlConnection(ConnectionHelper.LGAConnection()))
            {
                cn.Open();
                var cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT SubjectsHandled.*, SchoolAccount.Lastname, SchoolAccount.Firstname, Subjects.SubjectCode, Subjects.SubjectName FROM SubjectsHandled INNER JOIN Subjects ON SubjectsHandled.Subject = Subjects.ID JOIN SchoolAccount ON SubjectsHandled.TeacherID = SchoolAccount.ID WHERE SubjectsHandled.TeacherID=@TeacherId AND SubjectsHandled.Grade_Level=@Grade_Level";
                cmd.Parameters.AddWithValue("@TeacherId", teacherId);
                cmd.Parameters.AddWithValue("@Grade_Level ", gradeLevelId);
                var dr = cmd.ExecuteReader();

                var itemsToReturn = new List<SubjectsHandled>();
                listReturn = itemsToReturn;

                while (dr.Read())
                {

                    itemsToReturn.Add(new SubjectsHandled
                    {
                        Id = int.Parse(dr["ID"].ToString()),
                        TeacherId = int.Parse(dr["TeacherID"].ToString()),
                        SubjectId = int.Parse(dr["Subject"].ToString()),
                        SubjectCode = dr["SubjectCode"].ToString(),
                        SubjectName = dr["SubjectName"].ToString(),
                        GradeLevelId = int.Parse(dr["Grade_Level"].ToString()),
                        Lastname = dr["Lastname"].ToString(),
                        Firstname = dr["Firstname"].ToString()
                        
                    });
                }

            }

            return listReturn;
        }

        public List<Subjects> PopulateReturnList(SqlDataReader dr)
        {

            var listReturn = new List<Subjects>();

            while (dr.Read())
            {

                listReturn.Add(new Subjects
                {
                    ID = int.Parse(dr["ID"].ToString()),
                    SubjectCode = dr["SubjectCode"].ToString(),
                    SubjectName = dr["SubjectName"].ToString(),
                    GradeLevel = int.Parse(dr["Grade_Level"].ToString()),
                    Grade_Level = dr["GradeLevel"].ToString()                   
                });
            }

            return listReturn;
        }


        public List<SubjectsHandled> GetSubjectsHandledAll()
        {
            var listReturn = new List<SubjectsHandled>();

            using (var cn = new SqlConnection(ConnectionHelper.LGAConnection()))
            {
                cn.Open();
                var cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT SubjectsHandled.*, SchoolAccount.Lastname, SchoolAccount.Firstname, Section.SectionName, Subjects.SubjectCode, Subjects.SubjectName FROM SubjectsHandled INNER JOIN Subjects ON SubjectsHandled.Subject = Subjects.ID JOIN SchoolAccount ON SubjectsHandled.TeacherID = SchoolAccount.ID JOIN Section ON SubjectsHandled.Grade_Level = Section.ID";               
                var dr = cmd.ExecuteReader();
                listReturn = PopulateReturnLists(dr);
            }

            return listReturn;
        }

        public List<SubjectsHandled> PopulateReturnLists(SqlDataReader dr)
        {

            var listReturn = new List<SubjectsHandled>();

            while (dr.Read())
            {

                listReturn.Add(new SubjectsHandled
                {
                    Id = int.Parse(dr["ID"].ToString()),
                    TeacherId = int.Parse(dr["TeacherID"].ToString()),
                    SubjectId = int.Parse(dr["Subject"].ToString()),
                    SubjectCode = dr["SubjectCode"].ToString(),
                    SubjectName = dr["SubjectName"].ToString(),
                    GradeLevelId = int.Parse(dr["Grade_Level"].ToString()),
                    Lastname = dr["Lastname"].ToString(),
                    Firstname = dr["Firstname"].ToString(),
                    SectionName = dr["SectionName"].ToString()
                });
            }

            return listReturn;
        }

        public List<UnhandledSubjects> GetAllUnhandledSubjects()
        {
            var listReturn = new List<UnhandledSubjects>();

            using (var cn = new SqlConnection(ConnectionHelper.LGAConnection()))
            {
                cn.Open();
                var cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT Subjects.SubjectName, YearLevel.Grade_Level,Section.SectionName FROM Subjects JOIN YearLevel ON Subjects.Grade_Level = YearLevel.ID JOIN Section ON YearLevel.ID = Section.Grade_Level WHERE Subjects.ID not in (SELECT SubjectsHandled.Subject FROM SubjectsHandled LEFT OUTER JOIN Subjects ON SubjectsHandled.Subject = Subjects.ID)";
                var dr = cmd.ExecuteReader();
                listReturn = populateReturnLists(dr);
            }

            return listReturn;
        }

        public List<UnhandledSubjects> populateReturnLists(SqlDataReader dr)
        {

            var listReturn = new List<UnhandledSubjects>();

            while (dr.Read())
            {

                listReturn.Add(new UnhandledSubjects
                {
                    
                    SubjectName = dr["SubjectName"].ToString(),
                    GradeLevel = dr["Grade_Level"].ToString(),
                    SectionName = dr["SectionName"].ToString()
                });
            }

            return listReturn;
        }
    }
}