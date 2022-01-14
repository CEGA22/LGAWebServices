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
        public bool CreateClassScheduleRequest(IEnumerable<ClassScheduleRequest> request)
        {
            try
            {
                foreach (var item in request)
                {
                    var con = new SqlConnection(ConnectionHelper.LGAConnection());

                    using (SqlCommand cmd = new SqlCommand("INSERT INTO ClassSchedule VALUES(@Subject, @StartTime, @EndTime,@TeacherID,@GradeLevel, @Weekday)", con))
                    {
                        cmd.Parameters.AddWithValue("@Subject", item.Subject);
                        cmd.Parameters.AddWithValue("@StartTime", item.StartTime);
                        cmd.Parameters.AddWithValue("@EndTime", item.EndTime);
                        cmd.Parameters.AddWithValue("@TeacherID", item.TeacherID);
                        cmd.Parameters.AddWithValue("@GradeLevel", item.GradeLevel);
                        cmd.Parameters.AddWithValue("@Weekday", item.WeekDay);
                        con.Open();

                        cmd.ExecuteScalar();

                        con.Close();
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }


        public bool UpdateClassScheduleRequest(IEnumerable<ClassScheduleRequest> request)
        {          
            try
            {
                var teacherId = request.Select(x => x.TeacherID).FirstOrDefault();
                var subjectId = request.Select(x => x.Subject).FirstOrDefault();
                var gradeLevel = request.Select(x => x.GradeLevel).FirstOrDefault();

                using (var cn = new SqlConnection(ConnectionHelper.LGAConnection()))
                {
                    cn.Open();
                    var cmd = cn.CreateCommand();
                    cmd.CommandText = "DELETE FROM ClassSchedule WHERE Teacher=@TeacherID AND Subject = @subjectId AND GradeLevel = @GradeLevel";
                    cmd.Parameters.AddWithValue("@TeacherID ", teacherId);
                    cmd.Parameters.AddWithValue("@subjectId ", subjectId);
                    cmd.Parameters.AddWithValue("@GradeLevel ", gradeLevel);
                    cmd.ExecuteNonQuery();
                }

                foreach (var item in request)
                {
                    var con = new SqlConnection(ConnectionHelper.LGAConnection());

                    using (SqlCommand cmd = new SqlCommand("INSERT INTO ClassSchedule VALUES(@Subject, @StartTime, @EndTime,@TeacherID,@GradeLevel, @Weekday)", con))
                    {
                        cmd.Parameters.AddWithValue("@Subject", item.Subject);
                        cmd.Parameters.AddWithValue("@StartTime", item.StartTime);
                        cmd.Parameters.AddWithValue("@EndTime", item.EndTime);
                        cmd.Parameters.AddWithValue("@TeacherID", item.TeacherID);
                        cmd.Parameters.AddWithValue("@GradeLevel", item.GradeLevel);
                        cmd.Parameters.AddWithValue("@Weekday", item.WeekDay);
                        con.Open();

                        cmd.ExecuteScalar();

                        con.Close();
                    }
                }

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool DeleteClassScheduleRequest(int teacherId, int subjectId, int gradeLevel)
        {
            try
            {              
                using (var cn = new SqlConnection(ConnectionHelper.LGAConnection()))
                {
                    cn.Open();
                    var cmd = cn.CreateCommand();
                    cmd.CommandText = "DELETE FROM ClassSchedule WHERE Teacher=@TeacherID AND Subject = @subjectId AND GradeLevel = @GradeLevel";
                    cmd.Parameters.AddWithValue("@TeacherID ", teacherId);
                    cmd.Parameters.AddWithValue("@subjectId ", subjectId);
                    cmd.Parameters.AddWithValue("@GradeLevel ", gradeLevel);
                    cmd.ExecuteNonQuery();
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