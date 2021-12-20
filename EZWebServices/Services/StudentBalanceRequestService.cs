using EZWebServices.Helpers;
using EZWebServices.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EZWebServices.Services
{
    public class StudentBalanceRequestService
    {
        public bool CreateStudentBalanceInformation(StudentBalanceRequest request)
        {
            try
            {
                var con = new SqlConnection(ConnectionHelper.LGAConnection());

                using (SqlCommand cmd = new SqlCommand("INSERT INTO StudentBalance VALUES(@StudentID,@Total,@Balance, @PaymentMode, @SchoolYear)", con))
                {
                    cmd.Parameters.AddWithValue("@StudentID", request.StudentID);
                    cmd.Parameters.AddWithValue("@Total", request.Total);
                    cmd.Parameters.AddWithValue("@Balance", request.Balance);
                    cmd.Parameters.AddWithValue("@PaymentMode", request.PaymentMode);
                    cmd.Parameters.AddWithValue("@SchoolYear", request.SchoolYear);
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