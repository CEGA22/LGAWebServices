using EZWebServices.Helpers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EZWebServices.Models
{
    public class GradeLevelSection
    {
        public int Id { get; set; }

        public int GradeLevel { get; set; }

        public string GradeLevels { get; set; }

        public string SectionName { get; set; }

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
     
        public byte[] StudentProfile { get; set; }

        public int SchoolYearStart { get; set; }

        public int SchoolYearEnd { get; set; }

        public int GradeLevelid { get; set; }

        public int SubjectsName { get; set; }

        public List<GradeLevelSection> GetGradeLevel()
        {
            var listReturn = new List<GradeLevelSection>();

            using (var cn = new SqlConnection(ConnectionHelper.LGAConnection()))
            {
                cn.Open();
                var cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT Section.ID,Section.Grade_Level AS 'GradeLevelid', YearLevel.Grade_Level, Section.SectionName FROM Section JOIN YearLevel ON Section.Grade_Level = YearLevel.ID";   
                var dr = cmd.ExecuteReader();
                listReturn = PopulateReturnList(dr);
            }

            return listReturn;
        }

        public List<GradeLevelSection> PopulateReturnList(SqlDataReader dr)
        {

            var listReturn = new List<GradeLevelSection>();

            while (dr.Read())
            {

                listReturn.Add(new GradeLevelSection
                {
                    Id = int.Parse(dr["ID"].ToString()),
                    GradeLevel = int.Parse(dr["GradeLevelid"].ToString()),
                    GradeLevels = dr["Grade_Level"].ToString(),
                    SectionName = dr["SectionName"].ToString(),                  
                });
            }

            return listReturn;
        }
    }
}