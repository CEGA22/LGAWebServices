using EZWebServices.Helpers;
using EZWebServices.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EZWebServices.Services
{
    public class LoginService
    {
        public LoginResult AccountLogin(LoginRequest request) 
        {

            using (var cn = new SqlConnection(ConnectionHelper.LGAConnection()))
            {
                cn.Open();
                var cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT * FROM SchoolAccount WHERE SchoolNumber=@SchoolNumber AND Password=@Password";
                cmd.Parameters.AddWithValue("@SchoolNumber", request.Username);
                cmd.Parameters.AddWithValue("@Password", request.Password);
                var dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        return new LoginResult
                        {
                            IsSuccess = dr.HasRows,
                            ID = (int)dr["ID"],
                            Firstname = dr["Firstname"].ToString(),
                            Lastname = dr["Lastname"].ToString(),
                            IsAdmin = (int)dr["IsAdmin"],
                            IsFaculty = (int)dr["IsFaculty"],
                            TeacherProfile = (byte[])dr["TeacherProfile"]

                        };
                    }
                }
                else 
                {
                    return new LoginResult
                    {
                        IsSuccess = false
                    };
                }
            }

            return null;
        }


        public StudentLoginResult StudentAccountLogin(StudentLoginRequest studentrequest)
        {

            using (var cn = new SqlConnection(ConnectionHelper.LGAConnection()))
            {
                cn.Open();
                var cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT * FROM StudentAccount WHERE StudentNumber=@StudentNumber AND Password=@Password";
                cmd.Parameters.AddWithValue("@StudentNumber", studentrequest.StudentNumber);
                cmd.Parameters.AddWithValue("@Password", studentrequest.Password);
                var dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        return new StudentLoginResult
                        {
                            IsSuccess = dr.HasRows,
                            ID = (int)dr["ID"],
                            Firstname = dr["Firstname"].ToString(),
                            Lastname = dr["Lastname"].ToString(),
                            StudentProfile = (byte[])dr["StudentProfile"]

                        };
                    }
                }
                else
                {
                    return new StudentLoginResult
                    {
                        IsSuccess = false
                    };
                }
            }

            return null;
        }
    }
}