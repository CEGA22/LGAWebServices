using EZWebServices.Helpers;
using EZWebServices.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EZWebServices.Services
{
    public class GradeLevelRequestService
    {
        public bool CreateStudentBalanceInformation(GradeLevelRequest request)
        {
            try
            {
                var con = new SqlConnection(ConnectionHelper.LGAConnection());

                using (SqlCommand cmd = new SqlCommand("INSERT INTO YearLevel VALUES(@GradeLevel)", con))
                {
                    cmd.Parameters.AddWithValue("@GradeLevel", request.GradeLevel);
                    con.Open();

                    cmd.ExecuteScalar();
                    return true;

                    con.Close();
                }
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}