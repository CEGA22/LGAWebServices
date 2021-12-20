using EZWebServices.Helpers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EZWebServices.Models
{
    public class ClassSchedule
    {
        public int ID { get; set; }

        public string Subject { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string StartTime { get; set; }

        public string EndTime { get; set; }

        public string Teacher { get; set; }

        public string WeekDay { get; set; }


        public List<ClassSchedule> GetClassScheduleDetails()
            {
                var listReturn = new List<ClassSchedule>();

                using (var cn = new SqlConnection(ConnectionHelper.LGAConnection()))
                {
                    cn.Open();
                    var cmd = cn.CreateCommand();
                    cmd.CommandText = "SELECT ClassSchedule.ID, Subjects.SubjectName AS 'Subject',  SchoolAccount.Firstname, SchoolAccount.Lastname, CONVERT(varchar, ClassSchedule.StartTime, 100) AS 'Start Time', CONVERT(varchar, ClassSchedule.EndTime, 100) AS 'End Time', ClassSchedule.WeekDay FROM ClassSchedule JOIN Subjects ON ClassSchedule.Subject = Subjects.ID JOIN Faculty ON ClassSchedule.Teacher = Faculty.ID JOIN SchoolAccount ON Faculty.FacultyID = SchoolAccount.ID ORDER BY CASE WHEN WeekDay = 'Sunday' THEN 1 WHEN WeekDay = 'Monday' THEN 2 WHEN WeekDay = 'Tuesday' THEN 3 WHEN WeekDay = 'Wednesday' THEN 4 WHEN WeekDay = 'Thursday' THEN 5 WHEN WeekDay = 'Friday' THEN 6 WHEN WeekDay = 'Saturday' THEN 7 END ASC";
                    //cmd.Parameters.AddWithValue("@ID", ID);
                    var dr = cmd.ExecuteReader();
                    listReturn = PopulateReturnList(dr);
                }

                return listReturn;
            }

            public List<ClassSchedule> PopulateReturnList(SqlDataReader dr)
            {

                var listReturn = new List<ClassSchedule>();

                while (dr.Read())
                {

                    listReturn.Add(new ClassSchedule
                    {
                        ID = int.Parse(dr["ID"].ToString()),
                        Subject = dr["Subject"].ToString(),                       
                        Firstname = dr["Firstname"].ToString(),
                        Lastname = dr["Lastname"].ToString(),
                        StartTime = dr["Start Time"].ToString(),
                        EndTime = dr["End Time"].ToString(),  
                        WeekDay = dr["WeekDay"].ToString(),
                    });
                }

                return listReturn;
            }


        public List<ClassSchedule> GetClassScheduleByWeekdayDetails(string weekday)
        {

            ClassSchedule studentAccount = new ClassSchedule();
            var listReturn = new List<ClassSchedule>();

            using (var cn = new SqlConnection(ConnectionHelper.LGAConnection()))
            {
                cn.Open();
                var cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT ClassSchedule.ID, Subjects.SubjectName AS 'Subject',  SchoolAccount.Firstname, SchoolAccount.Lastname, CONVERT(varchar, ClassSchedule.StartTime, 100) AS 'Start Time', CONVERT(varchar, ClassSchedule.EndTime, 100) AS 'End Time', ClassSchedule.WeekDay FROM ClassSchedule JOIN Subjects ON ClassSchedule.Subject = Subjects.ID JOIN Faculty ON ClassSchedule.Teacher = Faculty.ID JOIN SchoolAccount ON Faculty.FacultyID = SchoolAccount.ID WHERE WeekDay = @weekday";
                cmd.Parameters.AddWithValue("@weekday", weekday);
                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    listReturn.Add(new ClassSchedule
                    {
                        ID = int.Parse(dr["ID"].ToString()),
                        Subject = dr["Subject"].ToString(),
                        Firstname = dr["Firstname"].ToString(),
                        Lastname = dr["Lastname"].ToString(),
                        StartTime = dr["Start Time"].ToString(),
                        EndTime = dr["End Time"].ToString(),
                        WeekDay = dr["WeekDay"].ToString(),
                    });
                }
            }

            return listReturn;
        }

        public List<ClassSchedule> GetClassScheduleStudent(int ID)
        {

            ClassSchedule studentAccount = new ClassSchedule();
            var listReturn = new List<ClassSchedule>();

            using (var cn = new SqlConnection(ConnectionHelper.LGAConnection()))
            {
                cn.Open();
                var cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT ClassSchedule.ID, Subjects.SubjectName AS 'Subject' ,SchoolAccount.Firstname, SchoolAccount.Lastname,CONVERT(varchar, ClassSchedule.StartTime, 100) AS 'Start Time', CONVERT(varchar, ClassSchedule.EndTime, 100) AS 'End Time', ClassSchedule.WeekDay FROM ClassSchedule JOIN Students ON ClassSchedule.GradeLevel = Students.Grade_Level JOIN Faculty ON ClassSchedule.Teacher = Faculty.ID JOIN Subjects ON ClassSchedule.Subject = Subjects.ID JOIN SchoolAccount ON Faculty.FacultyID = SchoolAccount.ID WHERE Students.StudentID = @ID";
                cmd.Parameters.AddWithValue("@ID", ID);
                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    listReturn.Add(new ClassSchedule
                    {
                        ID = int.Parse(dr["ID"].ToString()),
                        Subject = dr["Subject"].ToString(),
                        Firstname = dr["Firstname"].ToString(),
                        Lastname = dr["Lastname"].ToString(),
                        StartTime = dr["Start Time"].ToString(),
                        EndTime = dr["End Time"].ToString(),
                        WeekDay = dr["WeekDay"].ToString(),
                    });
                }
            }

            return listReturn;
        }

        public List<ClassSchedule> GetClassScheduleDetailsFaculty(int ID)
        {

            ClassSchedule studentAccount = new ClassSchedule();
            var listReturn = new List<ClassSchedule>();

            using (var cn = new SqlConnection(ConnectionHelper.LGAConnection()))
            {
                cn.Open();
                var cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT ClassSchedule.ID, Subjects.SubjectName AS 'Subject',  SchoolAccount.Firstname, SchoolAccount.Lastname, CONVERT(varchar, ClassSchedule.StartTime, 100) AS 'Start Time', CONVERT(varchar, ClassSchedule.EndTime, 100) AS 'End Time', ClassSchedule.WeekDay FROM ClassSchedule JOIN Subjects ON ClassSchedule.Subject = Subjects.ID JOIN Faculty ON ClassSchedule.Teacher = Faculty.ID JOIN SchoolAccount ON Faculty.FacultyID = SchoolAccount.ID WHERE Faculty.FacultyID = @ID ORDER BY CASE WHEN WeekDay = 'Sunday' THEN 1 WHEN WeekDay = 'Monday' THEN 2 WHEN WeekDay = 'Tuesday' THEN 3 WHEN WeekDay = 'Wednesday' THEN 4 WHEN WeekDay = 'Thursday' THEN 5 WHEN WeekDay = 'Friday' THEN 6 WHEN WeekDay = 'Saturday' THEN 7 END ASC";
                cmd.Parameters.AddWithValue("@ID", ID);
                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    listReturn.Add(new ClassSchedule
                    {
                        ID = int.Parse(dr["ID"].ToString()),
                        Subject = dr["Subject"].ToString(),
                        Firstname = dr["Firstname"].ToString(),
                        Lastname = dr["Lastname"].ToString(),
                        StartTime = dr["Start Time"].ToString(),
                        EndTime = dr["End Time"].ToString(),
                        WeekDay = dr["WeekDay"].ToString(),
                    });
                }
            }

            return listReturn;
        }

        public List<ClassSchedule> GetClassScheduleByWeekdayDetailsFaculty(string weekday)
        {

            ClassSchedule studentAccount = new ClassSchedule();
            var listReturn = new List<ClassSchedule>();

            using (var cn = new SqlConnection(ConnectionHelper.LGAConnection()))
            {
                cn.Open();
                var cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT ClassSchedule.ID, Subjects.SubjectName AS 'Subject',  SchoolAccount.Firstname, SchoolAccount.Lastname, CONVERT(varchar, ClassSchedule.StartTime, 100) AS 'Start Time', CONVERT(varchar, ClassSchedule.EndTime, 100) AS 'End Time', ClassSchedule.WeekDay FROM ClassSchedule JOIN Subjects ON ClassSchedule.Subject = Subjects.ID JOIN Faculty ON ClassSchedule.Teacher = Faculty.ID JOIN SchoolAccount ON Faculty.FacultyID = SchoolAccount.ID WHERE WeekDay = @weekday";
                cmd.Parameters.AddWithValue("@weekday", weekday);
                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    listReturn.Add(new ClassSchedule
                    {
                        ID = int.Parse(dr["ID"].ToString()),
                        Subject = dr["Subject"].ToString(),
                        Firstname = dr["Firstname"].ToString(),
                        Lastname = dr["Lastname"].ToString(),
                        StartTime = dr["Start Time"].ToString(),
                        EndTime = dr["End Time"].ToString(),
                        WeekDay = dr["WeekDay"].ToString(),
                    });
                }
            }

            return listReturn;
        }

        public List<ClassSchedule> GetClassScheduleByWeekDetailsFaculty(int ID, string weekday)
        {

            ClassSchedule studentAccount = new ClassSchedule();
            var listReturn = new List<ClassSchedule>();

            using (var cn = new SqlConnection(ConnectionHelper.LGAConnection()))
            {
                cn.Open();
                var cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT ClassSchedule.ID, Subjects.SubjectName AS 'Subject',  SchoolAccount.Firstname, SchoolAccount.Lastname, CONVERT(varchar, ClassSchedule.StartTime, 100) AS 'Start Time', CONVERT(varchar, ClassSchedule.EndTime, 100) AS 'End Time', ClassSchedule.WeekDay FROM ClassSchedule JOIN Subjects ON ClassSchedule.Subject = Subjects.ID JOIN Faculty ON ClassSchedule.Teacher = Faculty.ID JOIN SchoolAccount ON Faculty.FacultyID = SchoolAccount.ID WHERE Faculty.FacultyID = @ID AND WeekDay = @weekday";
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@weekday", weekday);
                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    listReturn.Add(new ClassSchedule
                    {
                        ID = int.Parse(dr["ID"].ToString()),
                        Subject = dr["Subject"].ToString(),
                        Firstname = dr["Firstname"].ToString(),
                        Lastname = dr["Lastname"].ToString(),
                        StartTime = dr["Start Time"].ToString(),
                        EndTime = dr["End Time"].ToString(),
                        WeekDay = dr["WeekDay"].ToString(),
                    });
                }
            }

            return listReturn;
        }

        public List<ClassSchedule> GetClassScheduleByWeekdayDetailsStudent(int ID, string weekday)
        {

            ClassSchedule studentAccount = new ClassSchedule();
            var listReturn = new List<ClassSchedule>();

            using (var cn = new SqlConnection(ConnectionHelper.LGAConnection()))
            {
                cn.Open();
                var cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT ClassSchedule.ID, Subjects.SubjectName AS 'Subject' ,SchoolAccount.Firstname, SchoolAccount.Lastname,CONVERT(varchar, ClassSchedule.StartTime, 100) AS 'Start Time', CONVERT(varchar, ClassSchedule.EndTime, 100) AS 'End Time', ClassSchedule.WeekDay FROM ClassSchedule JOIN Students ON ClassSchedule.GradeLevel = Students.Grade_Level JOIN Faculty ON ClassSchedule.Teacher = Faculty.ID JOIN Subjects ON ClassSchedule.Subject = Subjects.ID JOIN SchoolAccount ON Faculty.FacultyID = SchoolAccount.ID WHERE Students.StudentID = @ID AND  WeekDay = @weekday";
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@weekday", weekday);
                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    listReturn.Add(new ClassSchedule
                    {
                        ID = int.Parse(dr["ID"].ToString()),
                        Subject = dr["Subject"].ToString(),
                        Firstname = dr["Firstname"].ToString(),
                        Lastname = dr["Lastname"].ToString(),
                        StartTime = dr["Start Time"].ToString(),
                        EndTime = dr["End Time"].ToString(),
                        WeekDay = dr["WeekDay"].ToString(),
                    });
                }
            }

            return listReturn;
        }

    }
    
}      