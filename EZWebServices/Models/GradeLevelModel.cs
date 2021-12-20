using EZWebServices.Helpers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EZWebServices.Models
{
    public class GradeLevelModel
    {
        public int ID { get; set; }
        public string GradeLevels { get; set; }

        public List<GradeLevelModel> GetGradeLevel()
        {
            var listReturn = new List<GradeLevelModel>();

            using (var cn = new SqlConnection(ConnectionHelper.LGAConnection()))
            {
                cn.Open();
                var cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT * FROM YearLevel";
                var dr = cmd.ExecuteReader();
                listReturn = PopulateReturnList(dr);
            }

            return listReturn;
        }

        public List<GradeLevelModel> PopulateReturnList(SqlDataReader dr)
        {

            var listReturn = new List<GradeLevelModel>();

            while (dr.Read())
            {

                listReturn.Add(new GradeLevelModel
                {
                    ID = int.Parse(dr["ID"].ToString()),
                    GradeLevels = dr["Grade_Level"].ToString(),
                });
            }

            return listReturn;
        }
    }

    
}