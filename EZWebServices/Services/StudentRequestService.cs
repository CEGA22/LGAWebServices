using EZWebServices.Helpers;
using EZWebServices.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EZWebServices.Services
{
    public class StudentRequestService
    {
        public bool CreateStudentInformation(StudentRequest request)
        {
            try
            {
                var studentId = CreateStudentAccount(request);
                CreateStudentDetails(studentId, request);
                CreateClassRecord(studentId);
                return true;
            }
            catch(Exception e)
            {
                return false;

            }
        }

        private int CreateStudentAccount(StudentRequest request)
        {
            decimal studentId = 0;

            var con = new SqlConnection(ConnectionHelper.LGAConnection());

            using (SqlCommand cmd = new SqlCommand("INSERT INTO StudentAccount (Lastname,Middlename,Firstname,StudentNumber,Password,StudentProfile,MobileNumber,Gender) VALUES(@Lastname,@Middlename,@Firstname,@StudentNumber,@Password,@StudentProfile ,@MobileNumber,@Gender); SELECT SCOPE_IDENTITY()", con))
            {
                cmd.Parameters.AddWithValue("@Lastname", request.Lastname);
                cmd.Parameters.AddWithValue("@Middlename", request.Middlename);
                cmd.Parameters.AddWithValue("@Firstname", request.Firstname);
                cmd.Parameters.AddWithValue("@StudentNumber", request.StudentNumber);
                cmd.Parameters.AddWithValue("@Password", request.Password);
                cmd.Parameters.AddWithValue("@StudentProfile", request.StudentProfile);
                cmd.Parameters.AddWithValue("@MobileNumber", request.MobileNumber);
                cmd.Parameters.AddWithValue("@Gender", request.Gender);
                con.Open();

                studentId = (decimal)cmd.ExecuteScalar();

                con.Close();
            }

            return Convert.ToInt32(studentId);
        }

        private void CreateStudentDetails(int studentId, StudentRequest request)
        {
            var con = new SqlConnection(ConnectionHelper.LGAConnection());

            using (SqlCommand cmd = new SqlCommand("INSERT INTO Students VALUES(@StudentID, @GradeLevel, @SchoolYearStart, @SchoolYearEnd)", con))
            {
                cmd.Parameters.AddWithValue("@StudentID", studentId);
                cmd.Parameters.AddWithValue("@GradeLevel", request.GradeLevelId);
                cmd.Parameters.AddWithValue("@SchoolYearStart", request.SchoolYearStart);
                cmd.Parameters.AddWithValue("@SchoolYearEnd", request.SchoolYearEnd);
                con.Open();

                cmd.ExecuteScalar();

                con.Close();
            }
        }

        private int GetFacultyIDByGradeLevel(int gradelevel)
        {
            int facultyID = 0;
            using (var cn = new SqlConnection(ConnectionHelper.LGAConnection()))
            {
                cn.Open();
                var cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT Teacher FROM SectionsHandled WHERE Gradelevel = @gradelevel";
                var dr = cmd.ExecuteReader();
                cmd.Parameters.AddWithValue("@gradelevel", gradelevel);

                while (dr.Read())
                {
                    facultyID = (int)dr["Teacher"];

                }
            }

            return facultyID;
        }

        private void CreateClassRecord(int studentId)
        {
            int periodCount = 4;

            for (var period = 0; period < periodCount; period++)
            {
                var con = new SqlConnection(ConnectionHelper.LGAConnection());

                using (SqlCommand cmd = new SqlCommand("INSERT INTO ClassRecords VALUES(@learnersname, @writtenwork1,@writtenwork2,@writtenwork3,@writtenwork4,@writtenwork5,@writtenwork6,@writtenwork7,@writtenwork8,@writtenwork9,@writtenwork10,@writtenworkTotal,@writtentWorkPercentage,@TaskPerformance1,@TaskPerformance2,@TaskPerformance3,@TaskPerformance4,@TaskPerformance5,@TaskPerformance6,@TaskPerformance7,@TaskPerformance8,@TaskPerformance9,@TaskPerformance10,@TaskPerformanceTotal,@TaskPerformancePercentage,@InitialGrade, @QuarterlyGrade,@GradingPeriod,@SaveasDraft,@SaveDraft)", con))
                {
                    cmd.Parameters.AddWithValue("@learnersname", studentId);
                    cmd.Parameters.AddWithValue("@writtenwork1", 0);
                    cmd.Parameters.AddWithValue("@writtenwork2", 0);
                    cmd.Parameters.AddWithValue("@writtenwork3", 0);
                    cmd.Parameters.AddWithValue("@writtenwork4", 0);
                    cmd.Parameters.AddWithValue("@writtenwork5", 0);
                    cmd.Parameters.AddWithValue("@writtenwork6", 0);
                    cmd.Parameters.AddWithValue("@writtenwork7", 0);
                    cmd.Parameters.AddWithValue("@writtenwork8", 0);
                    cmd.Parameters.AddWithValue("@writtenwork9", 0);
                    cmd.Parameters.AddWithValue("@writtenwork10", 0);
                    cmd.Parameters.AddWithValue("@writtenworkTotal", 0);
                    cmd.Parameters.AddWithValue("@writtentWorkPercentage", 0);
                    cmd.Parameters.AddWithValue("@TaskPerformance1", 0);
                    cmd.Parameters.AddWithValue("@TaskPerformance2", 0);
                    cmd.Parameters.AddWithValue("@TaskPerformance3", 0);
                    cmd.Parameters.AddWithValue("@TaskPerformance4", 0);
                    cmd.Parameters.AddWithValue("@TaskPerformance5", 0);
                    cmd.Parameters.AddWithValue("@TaskPerformance6", 0);
                    cmd.Parameters.AddWithValue("@TaskPerformance7", 0);
                    cmd.Parameters.AddWithValue("@TaskPerformance8", 0);
                    cmd.Parameters.AddWithValue("@TaskPerformance9", 0);
                    cmd.Parameters.AddWithValue("@TaskPerformance10", 0);
                    cmd.Parameters.AddWithValue("@TaskPerformanceTotal", 0);
                    cmd.Parameters.AddWithValue("@TaskPerformancePercentage", 0);
                    cmd.Parameters.AddWithValue("@InitialGrade", 0);
                    cmd.Parameters.AddWithValue("@QuarterlyGrade", 0);
                    cmd.Parameters.AddWithValue("@SaveasDraft", 1);
                    cmd.Parameters.AddWithValue("@SaveDraft", 0);
                    cmd.Parameters.AddWithValue("@GradingPeriod", period + 1);
                    con.Open();
                    cmd.ExecuteScalar();
                    con.Close();
                }

            }
        }


        public bool UpdateStudentInformation(StudentRequest request)
        {
            try
            {
                var studentId = UpdateStudentAccount(request);
                UpdateStudentDetials(studentId, request);               
                return true;
            }
            catch (Exception e)
            {
                return false;

            }
        }

        public int UpdateStudentAccount(StudentRequest request)
        {            
            decimal studentId = 0;
            var con = new SqlConnection(ConnectionHelper.LGAConnection());

            using (SqlCommand cmd = new SqlCommand("UPDATE StudentAccount SET Lastname = @Lastname, Middlename = @Middlename, Firstname = @Firstname, StudentNumber = @StudentNumber, Password = @Password, MobileNumber = @MobileNumber, Gender = @Gender WHERE ID = @ID", con))
            {
                cmd.Parameters.AddWithValue("@ID", request.ID);
                cmd.Parameters.AddWithValue("@Lastname", request.Lastname);
                cmd.Parameters.AddWithValue("@Middlename", request.Middlename);
                cmd.Parameters.AddWithValue("@Firstname", request.Firstname);
                cmd.Parameters.AddWithValue("@StudentNumber", request.StudentNumber);
                cmd.Parameters.AddWithValue("@Password", request.Password);
                //cmd.Parameters.AddWithValue("@StudentProfile", request.StudentProfile);
                cmd.Parameters.AddWithValue("@MobileNumber", request.MobileNumber);
                cmd.Parameters.AddWithValue("@Gender", request.Gender);
                con.Open();
                cmd.ExecuteNonQuery();

                //studentId = (decimal)cmd.ExecuteScalar();

                con.Close();
            }

            return Convert.ToInt32(request.ID);
        }

        public void UpdateStudentDetials(int studentId, StudentRequest request)
        {
            
            var con = new SqlConnection(ConnectionHelper.LGAConnection());

            using (SqlCommand cmd = new SqlCommand("UPDATE Students SET Grade_Level = @GradeLevel, SchoolYearStart = @SchoolYearStart, SchoolYearEnd = @SchoolYearEnd WHERE StudentID = @ID", con))
            {
                
                cmd.Parameters.AddWithValue("@GradeLevel", request.GradeLevelId);
                cmd.Parameters.AddWithValue("@SchoolYearStart", request.SchoolYearStart);
                cmd.Parameters.AddWithValue("@SchoolYearEnd", request.SchoolYearEnd);
                cmd.Parameters.AddWithValue("@ID", studentId);
                con.Open();
                cmd.ExecuteScalar();
                con.Close();
            }         
        }

    }
}