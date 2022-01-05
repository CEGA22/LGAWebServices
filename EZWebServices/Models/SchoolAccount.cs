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

        public DateTime Birthday { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

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
                cmd.CommandText = "SELECT * FROM SchoolAccount";
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
                    Address = dr["Address"].ToString(),
                    Birthday = Convert.ToDateTime(dr["Birthday"].ToString()),
                    Email = dr["Email"].ToString(),
                    password = dr["Password"].ToString(),
                    TeacherProfile = (byte[])dr["TeacherProfile"],
                    mobileNumber = dr["MobileNumber"].ToString(),
                    gender = dr["Gender"].ToString(),
                    isAdmin = (int)dr["IsAdmin"]

                });
            }

            return listReturn;
        }

        public List<SchoolAccount> GetStudentAccountOnly()
        {
            var listReturn = new List<SchoolAccount>();

            using (var cn = new SqlConnection(ConnectionHelper.LGAConnection()))
            {
                cn.Open();
                var cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT ID, Lastname, Middlename,Firstname,Birthday,Address,Email,MobileNumber,SchoolNumber,Password,Gender,IsAdmin,IsFaculty FROM SchoolAccount";
                var dr = cmd.ExecuteReader();
                listReturn = populateReturnLists(dr);
            }

            return listReturn;
        }

        public List<SchoolAccount> populateReturnLists(SqlDataReader dr)
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
                    Address = dr["Address"].ToString(),
                    Birthday = Convert.ToDateTime(dr["Birthday"].ToString()),
                    Email = dr["Email"].ToString(),
                    password = dr["Password"].ToString(),                   
                    mobileNumber = dr["MobileNumber"].ToString(),
                    gender = dr["Gender"].ToString(),
                    isAdmin = (int)dr["IsAdmin"]

                });
            }

            return listReturn;
        }

        public List<SchoolAccount> GetSchoolAccountPassword(string email)

        {
            var listReturn = new List<SchoolAccount>();

            using (var cn = new SqlConnection(ConnectionHelper.LGAConnection()))
            {
                cn.Open();
                var cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT Password FROM SchoolAccount WHERE Email = @Email";
                cmd.Parameters.AddWithValue("@Email", email);
                var dr = cmd.ExecuteReader();
                listReturn = PopulateReturnLists(dr);
            }
            return listReturn;
        }

        public List<SchoolAccount> PopulateReturnLists(SqlDataReader dr)
        {
            try
            {
                var listReturn = new List<SchoolAccount>();
                while (dr.Read())
                {
                    listReturn.Add(new SchoolAccount
                    {
                        password = dr["Password"].ToString()
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