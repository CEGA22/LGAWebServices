using EZWebServices.Helpers;
using EZWebServices.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EZWebServices.Services
{
    public class SubjectsHandledRequestService
    {
        public bool CreateSubjectsHandledInformation(SubjectsHandledRequest request)
        {
            try
            {

                using (var cn = new SqlConnection(ConnectionHelper.LGAConnection()))
                {
                    cn.Open();
                    var cmd = cn.CreateCommand();
                    cmd.CommandText = "DELETE FROM SubjectsHandled WHERE TeacherID=@TeacherID AND Grade_Level=@Grade_Level";
                    cmd.Parameters.AddWithValue("@TeacherID ", request.TeacherID);
                    cmd.Parameters.AddWithValue("@Grade_Level  ", request.GradeLevelID);
                    cmd.ExecuteNonQuery();
                }

                if (!request.SubjectsHandled.Any())
                    return true;

                foreach (var item in request.SubjectsHandled)
                {
                    using (var cn = new SqlConnection(ConnectionHelper.LGAConnection()))
                    {
                        cn.Open();
                        var cmd = cn.CreateCommand();
                        cmd.CommandText = "INSERT INTO SubjectsHandled VALUES(@TeacherID, @Subject, @Grade_Level)";
                        cmd.Parameters.AddWithValue("@TeacherID",item.TeacherId);
                        cmd.Parameters.AddWithValue("@Subject", item.SubjectId);
                        cmd.Parameters.AddWithValue("@Grade_Level", item.GradeLevelId);
                        cmd.ExecuteNonQuery();
                    }
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