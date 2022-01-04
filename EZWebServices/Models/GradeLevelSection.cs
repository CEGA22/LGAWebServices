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