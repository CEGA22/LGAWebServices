using EZWebServices.Helpers;
using EZWebServices.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EZWebServices.Services
{
    public class GradeLevelHandledRequestService
    {
        public bool CreateGradeLevelHandledInformation(GradeLevelHandledRequest request)
        {
            try
            {
                var con = new SqlConnection(ConnectionHelper.LGAConnection());

                using (SqlCommand cmd = new SqlCommand("INSERT INTO SectionsHandled VALUES(@TeacherID,@GradeLevelID)", con))
                {
                    cmd.Parameters.AddWithValue("@TeacherID", request.TeacherID);
                    cmd.Parameters.AddWithValue("@GradeLevelID", request.GradeLevelID);
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