using EZWebServices.Helpers;
using EZWebServices.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EZWebServices.Services
{
    public class GradeLevelSectionRequestService
    {
        public bool CreateGradeLevelSectionInformation(GradeLevelSection request)
        {
            try
            {
                var con = new SqlConnection(ConnectionHelper.LGAConnection());

                using (SqlCommand cmd = new SqlCommand("INSERT INTO Section VALUES(@GradeLevelID, @SectionName)", con))
                {
                    cmd.Parameters.AddWithValue("@GradeLevelID", request.GradeLevel);
                    cmd.Parameters.AddWithValue("@SectionName", request.SectionName);
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

        public bool UpdateGradeLevelSectionInformation(GradeLevelSection request)
        {
            try
            {
                var con = new SqlConnection(ConnectionHelper.LGAConnection());
                using (SqlCommand cmd = new SqlCommand("UPDATE Section SET Grade_Level = @GradeLevel, SectionName = @SectionName WHERE ID = @ID", con))
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("@ID", request.Id);
                    cmd.Parameters.AddWithValue("@GradeLevel", request.GradeLevel);
                    cmd.Parameters.AddWithValue("@SectionName", request.SectionName);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    return true;
                }

            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}