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
    }
}