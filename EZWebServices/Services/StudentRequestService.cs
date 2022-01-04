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
                var gradelevelid = CreateStudentDetails(studentId, request);
                var subjectsList =  GetSubjects(gradelevelid);
                CreateClassRecord(studentId, subjectsList, request);
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

        private int CreateStudentDetails(int studentId, StudentRequest request)
        {
            var con = new SqlConnection(ConnectionHelper.LGAConnection());

            using (SqlCommand cmd = new SqlCommand("INSERT INTO Students VALUES(@StudentID, @GradeLevel, @SchoolYearStart, @SchoolYearEnd)", con))
            {
                cmd.Parameters.AddWithValue("@StudentID", studentId);
                cmd.Parameters.AddWithValue("@GradeLevel", request.GradeLevelid);
                cmd.Parameters.AddWithValue("@SchoolYearStart", request.SchoolYearStart);
                cmd.Parameters.AddWithValue("@SchoolYearEnd", request.SchoolYearEnd);
                con.Open();
                cmd.ExecuteScalar();
                con.Close();
            }
            return request.GradeLevelid;
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

        //private int GetSubjects(int subjects)
        //{
        //    int subjectscount = 0;
        //    using (var cn = new SqlConnection(ConnectionHelper.LGAConnection()))
        //    {
        //        cn.Open();
        //        var cmd = cn.CreateCommand();
        //        cmd.CommandText = "SELECT COUNT(DISTINCT SubjectName) FROM Subjects JOIN Section ON Section.Grade_Level = Subjects.Grade_Level WHERE Section.Grade_Level = @Subjects";
        //        var dr = cmd.ExecuteReader();
        //        cmd.Parameters.AddWithValue("@Subjects", subjects);
        //    }

        //    return subjectscount;
        //}

        public List<Subjects> GetSubjects(int gradelevelid)
        {
            var listReturn = new List<Subjects>();

            using (var cn = new SqlConnection(ConnectionHelper.LGAConnection()))
            {
                cn.Open();
                var cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT DISTINCT Subjects.ID, SubjectName FROM Subjects JOIN Section ON Section.Grade_Level = Subjects.Grade_Level WHERE Section.Grade_Level = @GradeLevelID";
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

        private void CreateClassRecord(int studentId, List<Subjects>subjectsList, StudentRequest request)
        {
            int periodCount = 4;

            foreach(var subject in subjectsList)
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
            //decimal studentId = 0;
            var con = new SqlConnection(ConnectionHelper.LGAConnection());

            using (SqlCommand cmd = new SqlCommand("UPDATE StudentAccount SET Lastname = @Lastname, Middlename = @Middlename, Firstname = @Firstname, Address = @Address, Birthday = @Birthday, ParentsName = @ParentsName, StudentNumber = @StudentNumber, Password = @Password, MobileNumber = @MobileNumber, Email = @Email, Gender = @Gender, StudentProfile = @StudentProfile WHERE ID = @ID", con))
            {
                cmd.Parameters.AddWithValue("@ID", request.ID);
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
                
                cmd.Parameters.AddWithValue("@GradeLevel", request.GradeLevelid);
                cmd.Parameters.AddWithValue("@SchoolYearStart", request.SchoolYearStart);
                cmd.Parameters.AddWithValue("@SchoolYearEnd", request.SchoolYearEnd);
                cmd.Parameters.AddWithValue("@ID", studentId);
                con.Open();
                cmd.ExecuteScalar();
                con.Close();
            }         
        }

        public bool DeleteStudentInformation(int ID)
        {
            try
            {
                var con = new SqlConnection(ConnectionHelper.LGAConnection());
                using (SqlCommand cmd = new SqlCommand("DELETE StudentAccount WHERE ID = @ID", con))
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