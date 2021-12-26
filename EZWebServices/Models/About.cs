using EZWebServices.Helpers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EZWebServices.Models
{
    public class About
    {
        public int ID { get; set; }

        public string SchoolInfo { get; set; }

        public string Mission { get; set; }

        public string Vision { get; set; }

        public string AppInfo { get; set; }

        public string AppInfoSOMS { get; set; }

        public List<About> GetAboutDetails()
        {
            var listReturn = new List<About>();

            using (var cn = new SqlConnection(ConnectionHelper.LGAConnection()))
            {
                cn.Open();
                var cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT * FROM About";               
                var dr = cmd.ExecuteReader();
                listReturn = PopulateReturnList(dr);
            }

            return listReturn;
        }

        public List<About> PopulateReturnList(SqlDataReader dr)
        {

            var listReturn = new List<About>();

            while (dr.Read())
            {

                listReturn.Add(new About
                {
                    ID = int.Parse(dr["ID"].ToString()),
                    SchoolInfo = dr["SchoolInfo"].ToString(),
                    Mission = dr["Mission"].ToString(),
                    Vision = dr["Vision"].ToString(),
                    AppInfo = dr["AppInfo"].ToString(), 
                    AppInfoSOMS = dr["AppinfoSOMS"].ToString(),
                });
            }

            return listReturn;
        }
    }
}