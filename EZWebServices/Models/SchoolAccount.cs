using EZWebServices.Helpers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EZWebServices.Models
{
    public class SchoolAccount
    {
        public int id { get; set; }

        public string lastName { get; set; }

        public string middlename { get; set; }

        public string firstname { get; set; }

        public string schoolNumber { get; set; }

        public string password { get; set; }

        public byte[] TeacherProfile { get; set; }

        public string mobileNumber { get; set; }

        public string gender { get; set; }

        public int isAdmin { get; set; }

        public List<SchoolAccount> GetSchoolAccountDetails()
        {
            var listReturn = new List<SchoolAccount>();

            using (var cn = new SqlConnection(ConnectionHelper.LGAConnection()))
            {
                cn.Open();
                var cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT ID, Lastname, Middlename,Firstname, SchoolNumber,Password,TeacherProfile, MobileNumber,Gender,IsAdmin FROM SchoolAccount";
                var dr = cmd.ExecuteReader();
                listReturn = PopulateReturnList(dr);
            }

            return listReturn;
        }

        public List<SchoolAccount> PopulateReturnList(SqlDataReader dr)
        {

            var listReturn = new List<SchoolAccount>();

            while (dr.Read())
            {

                listReturn.Add(new SchoolAccount
                {
                    id = int.Parse(dr["ID"].ToString()),                  
                    lastName = dr["Lastname"].ToString(),
                    middlename = dr["Middlename"].ToString(),
                    firstname = dr["Firstname"].ToString(),
                    schoolNumber = dr["SchoolNumber"].ToString(),
                    password = dr["Password"].ToString(),
                    TeacherProfile = (byte[])dr["TeacherProfile"],
                    mobileNumber = dr["MobileNumber"].ToString(),
                    gender = dr["Gender"].ToString(),
                    isAdmin = (int)dr["IsAdmin"]

                });
            }

            return listReturn;
        }     
    }
}