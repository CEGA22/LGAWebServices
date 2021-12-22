using EZWebServices.Helpers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EZWebServices.Models
{
    public class SectionsHandled
    {
        public int ID { get; set; }

        public int TeacherID { get; set; }

        public int GradeLevelID { get; set; }

        public List<SectionsHandled> GetSectionsHandled(int ID)

        {
            var listReturn = new List<SectionsHandled>();

            using (var cn = new SqlConnection(ConnectionHelper.LGAConnection()))
            {
                cn.Open();
                var cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT * FROM SectionsHandled WHERE Teacher = @TeacherID";                
                cmd.Parameters.AddWithValue("@TeacherID", ID);
                var dr = cmd.ExecuteReader();
                listReturn = PopulateReturnList(dr);
            }         
            return listReturn;
        }

        public List<SectionsHandled> PopulateReturnList(SqlDataReader dr)
        {

            try
            {
                var listReturn = new List<SectionsHandled>();

                while (dr.Read())
                {

                    listReturn.Add(new SectionsHandled
                    {
                        ID = int.Parse(dr["ID"].ToString()),
                        TeacherID = int.Parse(dr["Teacher"].ToString()),
                        GradeLevelID = int.Parse(dr["Gradelevel"].ToString())
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