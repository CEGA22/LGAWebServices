using EZWebServices.Helpers;
using EZWebServices.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EZWebServices.Services
{
    public class ClassScheduleRequestService
    {
        public bool CreateClassScheduleRequest(ClassScheduleRequest request)
        {
            try
            {
                var con = new SqlConnection(ConnectionHelper.LGAConnection());

                using (SqlCommand cmd = new SqlCommand("INSERT INTO ClassSchedule VALUES(@Subject, @StartTime, @EndTime,@TeacherID,@GradeLevel, @Weekday)", con))
                {
                    cmd.Parameters.AddWithValue("@Subject", request.Subject);
                    cmd.Parameters.AddWithValue("@StartTime", request.StartTime);
                    cmd.Parameters.AddWithValue("@EndTime", request.EndTime);
                    cmd.Parameters.AddWithValue("@TeacherID", request.TeacherID);
                    cmd.Parameters.AddWithValue("@GradeLevel", request.GradeLevel);
                    cmd.Parameters.AddWithValue("@Weekday", request.WeekDay);                 
                    con.Open();

                    cmd.ExecuteScalar();

                    con.Close();
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}