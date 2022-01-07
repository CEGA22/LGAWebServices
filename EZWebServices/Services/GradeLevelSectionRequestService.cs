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
            var gradelevelsectionid = CreateGradeLevelSection(request);
            var studentid = CreateStudentAccount(request);
            CreateStudentDetails(studentid, gradelevelsectionid, request);
            var subjectsList = GetSubjects(gradelevelsectionid);
            CreateClassRecord(studentid, subjectsList, request);
            return true;
        }

        public int CreateGradeLevelSection(GradeLevelSection request)
        {
            decimal gradelevelsectionid = 0;
            var con = new SqlConnection(ConnectionHelper.LGAConnection());
            using (SqlCommand cmd = new SqlCommand("INSERT INTO Section VALUES(@GradeLevelID, @SectionName); SELECT SCOPE_IDENTITY()", con))
            {
                cmd.Parameters.AddWithValue("@GradeLevelID", request.GradeLevel);
                cmd.Parameters.AddWithValue("@SectionName", request.SectionName);
                con.Open();
                gradelevelsectionid = (decimal)cmd.ExecuteScalar();
                con.Close();                
            }

            return Convert.ToInt32(gradelevelsectionid);
        }

        private int CreateStudentAccount(GradeLevelSection request)
        {
            decimal studentId = 0;

            var con = new SqlConnection(ConnectionHelper.LGAConnection());

            using (SqlCommand cmd = new SqlCommand("INSERT INTO StudentAccount (Lastname,Middlename,Firstname,Address, Birthday,ParentsName,StudentNumber,Password,MobileNumber,Email,Gender,StudentProfile) VALUES(@Lastname,@Middlename,@Firstname,@Address, @Birthday,@ParentsName,@StudentNumber,@Password,@MobileNumber,@Email,@Gender,@StudentProfile); SELECT SCOPE_IDENTITY()", con))
            {
                cmd.Parameters.AddWithValue("@Lastname", request.Lastname);
                cmd.Parameters.AddWithValue("@Middlename", request.Middlename);
                cmd.Parameters.AddWithValue("@Firstname", request.Firstname);
                cmd.Parameters.AddWithValue("@Address", request.Address);
                cmd.Parameters.AddWithValue("@Birthday", request.Birthday);
                cmd.Parameters.AddWithValue("@ParentsName", request.ParentsName);
                cmd.Parameters.AddWithValue("@StudentNumber", request.StudentNumber);
                cmd.Parameters.AddWithValue("@Password", request.Password);
                cmd.Parameters.AddWithValue("@MobileNumber", request.mobileNumber);
                cmd.Parameters.AddWithValue("@Email", request.Email);
                cmd.Parameters.AddWithValue("@Gender", request.Gender);
                cmd.Parameters.AddWithValue("@StudentProfile", request.StudentProfile);
                con.Open();

                studentId = (decimal)cmd.ExecuteScalar();

                con.Close();
            }

            return Convert.ToInt32(studentId);
        }

        private void CreateStudentDetails(int studentId, int gradelevelsectionid, GradeLevelSection request)
        {
            var con = new SqlConnection(ConnectionHelper.LGAConnection());

            using (SqlCommand cmd = new SqlCommand("INSERT INTO Students VALUES(@StudentID, @GradeLevel, @SchoolYearStart, @SchoolYearEnd)", con))
            {
                cmd.Parameters.AddWithValue("@StudentID", studentId);
                cmd.Parameters.AddWithValue("@GradeLevel", gradelevelsectionid);
                cmd.Parameters.AddWithValue("@SchoolYearStart", request.SchoolYearStart);
                cmd.Parameters.AddWithValue("@SchoolYearEnd", request.SchoolYearEnd);
                con.Open();
                cmd.ExecuteScalar();
                con.Close();
            }            
        }

        public List<Subjects> GetSubjects(int gradelevelid)
        {
            var listReturn = new List<Subjects>();

            using (var cn = new SqlConnection(ConnectionHelper.LGAConnection()))
            {
                cn.Open();
                var cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT Subjects.ID, Section.Grade_Level, Section.SectionName, SubjectName FROM Subjects JOIN Section ON Section.Grade_Level = Subjects.Grade_Level WHERE Section.ID = @GradeLevelID";
                cmd.Parameters.AddWithValue("@GradeLevelID", gradelevelid);
                var dr = cmd.ExecuteReader();
                listReturn = PopulateReturnList(dr);
            }

            return listReturn;
        }

        public List<Subjects> PopulateReturnList(SqlDataReader dr)
        {
            var listReturn = new List<Subjects>();

            while (dr.Read())
            {

                listReturn.Add(new Subjects
                {
                    ID = int.Parse(dr["ID"].ToString()),
                    SubjectName = dr["SubjectName"].ToString(),
                });
            }

            return listReturn;
        }

        private void CreateClassRecord(int studentId, List<Subjects> subjectsList, GradeLevelSection request)
        {
            int periodCount = 4;

            try
            {
                foreach (var subject in subjectsList)
                {
                    for (var period = 0; period < periodCount; period++)
                    {
                        var con = new SqlConnection(ConnectionHelper.LGAConnection());

                        using (SqlCommand cmd = new SqlCommand("INSERT INTO ClassRecords VALUES(@learnersname, @writtenwork1,@writtenwork2,@writtenwork3,@writtenwork4,@writtenwork5,@writtenwork6,@writtenwork7,@writtenwork8,@writtenwork9,@writtenwork10,@writtenworkTotal,@writtentWorkPercentage,@TaskPerformance1,@TaskPerformance2,@TaskPerformance3,@TaskPerformance4,@TaskPerformance5,@TaskPerformance6,@TaskPerformance7,@TaskPerformance8,@TaskPerformance9,@TaskPerformance10,@TaskPerformanceTotal,@TaskPerformancePercentage,@InitialGrade, @QuarterlyGrade,@GradingPeriod,@SaveasDraft,@SaveDraft, @SubjectsName)", con))
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
                            cmd.Parameters.AddWithValue("@GradingPeriod", period + 1);
                            cmd.Parameters.AddWithValue("@SaveasDraft", 1);
                            cmd.Parameters.AddWithValue("@SaveDraft", 0);
                            cmd.Parameters.AddWithValue("@SubjectsName", subject.ID);
                            con.Open();
                            cmd.ExecuteScalar();
                            con.Close();
                        }
                    }
                }
            }
            catch (Exception e)
            {
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

        public bool DeleteSection(int ID)
        {
            try
            {
                var con = new SqlConnection(ConnectionHelper.LGAConnection());
                using (SqlCommand cmd = new SqlCommand("DELETE FROM Section WHERE ID = @ID", con))
                {
                    cmd.Parameters.AddWithValue("@ID", ID);
                    con.Open();
                    cmd.ExecuteNonQuery();
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