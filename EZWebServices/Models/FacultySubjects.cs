using EZWebServices.Helpers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EZWebServices.Models
{
    public class FacultySubjects
    {
        public int TeacherID { get; set; }

        public string SubjectName { get; set; }

        public string GradeLevel { get; set; }

        public string SectionName { get; set; }

        public List<FacultySubjects> GetFacultySubjects(int ID)
        {
            var listReturn = new List<FacultySubjects>();

            using (var cn = new SqlConnection(ConnectionHelper.LGAConnection()))
            {
                cn.Open();
                var cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT TeacherID, Subjects.SubjectName, YearLevel.Grade_Level, Section.SectionName FROM SubjectsHandled JOIN Subjects ON SubjectsHandled.Subject = Subjects.ID JOIN Section ON SubjectsHandled.Grade_Level = Section.ID JOIN YearLevel ON Section.Grade_Level = YearLevel.ID WHERE TeacherID = @ID";
                
                cmd.Parameters.AddWithValue("@ID", ID);
                var dr = cmd.ExecuteReader();
                listReturn = PopulateReturnList(dr);
            }
         
            return listReturn;
        }

        public List<FacultySubjects> PopulateReturnList(SqlDataReader dr)
        {
            try
            {
                var listReturn = new List<FacultySubjects>();

                while (dr.Read())
                {

                    listReturn.Add(new FacultySubjects
                    {
                        TeacherID = int.Parse(dr["TeacherID"].ToString()),
                        SubjectName = dr["SubjectName"].ToString(),
                        GradeLevel = dr["Grade_Level"].ToString(), 
                        SectionName = dr["SectionName"].ToString()
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