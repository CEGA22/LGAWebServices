using EZWebServices.Helpers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EZWebServices.Models
{
    public class FinalGrade
    {
        public int StudentID { get; set; }

        public string StudentName { get; set; }

        public int SubjectID { get; set; }

        public string SubjectName { get; set; }

        public double finalGrade { get; set; }

        public double Average { get; set; }

        public DateTime DateModified { get; set; }

        public List<FinalGrade> GetFinalGrades(int ID)

        {
            var listReturn = new List<FinalGrade>();

            using (var cn = new SqlConnection(ConnectionHelper.LGAConnection()))
            {
                cn.Open();
                var cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT DISTINCT FinalGrade.studentid, studentname, subjectid, subjectname, finalgrade, average, CONVERT(varchar, FinalGrade.datemodified, 100) AS 'Date modified' FROM FinalGrade JOIN ClassRecords ON FinalGrade.studentid = ClassRecords.Learnersname JOIN Students ON FinalGrade.studentid = Students.StudentID JOIN SectionsHandled ON Students.Grade_Level = SectionsHandled.Gradelevel WHERE SectionsHandled.Teacher = @ID ORDER BY FinalGrade.studentname asc";               
                cmd.Parameters.AddWithValue("@ID", ID);
                var dr = cmd.ExecuteReader();
                listReturn = PopulateReturnList(dr);
            }

            //var highestPossibleScore = GetHighestPossibleScore(ID);
            //listReturn.AddRange(highestPossibleScore);

            return listReturn;
        }

        public List<FinalGrade> PopulateReturnList(SqlDataReader dr)
        {

            try
            {
                var listReturn = new List<FinalGrade>();

                while (dr.Read())
                {

                    listReturn.Add(new FinalGrade
                    {
                        StudentID = int.Parse(dr["studentid"].ToString()),
                        StudentName = dr["studentname"].ToString(),
                        SubjectID  = int.Parse(dr["subjectid"].ToString()),
                        SubjectName = dr["subjectname"].ToString(),
                        finalGrade = (double)dr["finalgrade"],
                        Average = (double)dr["average"],
                        DateModified = Convert.ToDateTime(dr["Date modified"].ToString())
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