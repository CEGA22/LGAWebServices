﻿using EZWebServices.Helpers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EZWebServices.Models
{
    public class StudentAccount
    {
        public int ID { get; set; }

        public string Lastname { get; set; }

        public string Middlename { get; set; }

        public string Firstname { get; set; }

        public string Gender { get; set; }

        public string Grade_Level { get; set; }

        public string SectionName { get; set; }

        public string StudentNumber { get; set; }

        public string Password { get; set; }

        public byte[] StudentProfile { get; set; }

        public string mobileNumber { get; set; }

        public string SchoolYearStart { get; set; }

        public string SchoolYearEnd { get; set; }

        public List<StudentAccount> GetStudentAccountDetails()
        {
            var listReturn = new List<StudentAccount>();

            using (var cn = new SqlConnection(ConnectionHelper.LGAConnection()))
            {
                cn.Open();
                var cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT StudentAccount.ID, StudentAccount.Lastname, StudentAccount.Middlename, StudentAccount.Firstname, StudentAccount.Gender,YearLevel.Grade_Level AS 'Grade Level', Section.SectionName AS 'Section Name', StudentAccount.StudentNumber, StudentAccount.Password,StudentAccount.StudentProfile, StudentAccount.MobileNumber, Students.SchoolYearStart, Students.SchoolYearEnd FROM StudentAccount JOIN Students ON StudentAccount.ID = Students.StudentID JOIN Section ON Section.ID = Students.Grade_Level JOIN YearLevel ON Section.Grade_Level = YearLevel.ID WHERE Lastname != 'Highest possible score'";
                //SELECT StudentAccount.ID, StudentAccount.Lastname, StudentAccount.Middlename, StudentAccount.Firstname, StudentAccount.Gender, YearLevel.Grade_Level AS 'Grade Level', Section.SectionName AS 'Section Name', StudentAccount.StudentNumber, StudentAccount.Password, Students.SchoolYearStart, Students.SchoolYearEnd FROM StudentAccount JOIN Students ON StudentAccount.ID = Students.StudentID JOIN YearLevel ON YearLevel.ID = Students.Grade_Level JOIN Section ON Section.Grade_Level = YearLevel.ID
                //cmd.Parameters.AddWithValue("@ID", ID);
                var dr = cmd.ExecuteReader();
                listReturn = PopulateReturnList(dr);
            }

            return listReturn;
        }

        public List<StudentAccount> PopulateReturnList(SqlDataReader dr)
        {

            var listReturn = new List<StudentAccount>();

            while (dr.Read())
            {

                listReturn.Add(new StudentAccount
                {
                    ID = int.Parse(dr["ID"].ToString()),
                    Lastname = dr["Lastname"].ToString(),
                    Middlename = dr["Middlename"].ToString(),
                    Firstname = dr["Firstname"].ToString(),
                    Gender = dr["Gender"].ToString(),
                    Grade_Level = dr["Grade Level"].ToString(),
                    SectionName = dr["Section Name"].ToString(),
                    StudentNumber = dr["StudentNumber"].ToString(),
                    Password = dr["Password"].ToString(),
                    StudentProfile = (byte[])dr["StudentProfile"],
                    mobileNumber  = dr["MobileNumber"].ToString(),
                    SchoolYearStart = dr["SchoolYearStart"].ToString(),
                    SchoolYearEnd = dr["SchoolYearEnd"].ToString(),
                });
            }

            return listReturn;
        }

        public List<StudentAccount> GetStudentAccountDetailsByLastname(string lastname)
        {

            StudentAccount studentAccount = new StudentAccount();
            var listReturn = new List<StudentAccount>();

            using (var cn = new SqlConnection(ConnectionHelper.LGAConnection()))
            {
                cn.Open();
                var cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT StudentAccount.ID, StudentAccount.Lastname, StudentAccount.Middlename, StudentAccount.Firstname, StudentAccount.Gender, YearLevel.Grade_Level AS 'Grade Level', Section.SectionName AS 'Section Name', StudentAccount.StudentNumber, StudentAccount.Password, Students.SchoolYearStart, Students.SchoolYearEnd FROM StudentAccount JOIN Students ON StudentAccount.ID = Students.StudentID JOIN YearLevel ON YearLevel.ID = Students.Grade_Level JOIN Section ON Section.Grade_Level = YearLevel.ID WHERE StudentAccount.Lastname = @lastname";
                cmd.Parameters.AddWithValue("@lastname", lastname);
                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    listReturn.Add(new StudentAccount
                    {
                        ID = (int)dr["ID"],
                        Lastname = dr["Lastname"].ToString(),
                        Middlename = dr["Middlename"].ToString(),
                        Firstname = dr["Firstname"].ToString(),
                        Gender = dr["Gender"].ToString(),
                        Grade_Level = dr["Grade Level"].ToString(),
                        SectionName = dr["Section Name"].ToString(),
                        StudentNumber = dr["StudentNumber"].ToString(),
                        Password = dr["Password"].ToString(),
                        SchoolYearStart = dr["SchoolYearStart"].ToString(),
                        SchoolYearEnd = dr["SchoolYearEnd"].ToString(),                                             
                    });
                }
            }

            return listReturn;
        }


        public List<StudentAccount> GetStudentAccountDetailsByGradeLevel(string gradelevel)
        {

            StudentAccount studentAccount = new StudentAccount();
            var listReturn = new List<StudentAccount>();

            using (var cn = new SqlConnection(ConnectionHelper.LGAConnection()))
            {
                cn.Open();
                var cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT StudentAccount.ID, StudentAccount.Lastname, StudentAccount.Middlename, StudentAccount.Firstname, StudentAccount.Gender, YearLevel.Grade_Level AS 'Grade Level', Section.SectionName AS 'Section Name', StudentAccount.StudentNumber, StudentAccount.Password, Students.SchoolYearStart, Students.SchoolYearEnd FROM StudentAccount JOIN Students ON StudentAccount.ID = Students.StudentID JOIN YearLevel ON YearLevel.ID = Students.Grade_Level JOIN Section ON Section.Grade_Level = YearLevel.ID WHERE YearLevel.Grade_Level = @gradelevel";
                cmd.Parameters.AddWithValue("@gradelevel", gradelevel);
                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    listReturn.Add(new StudentAccount
                    {
                        ID = (int)dr["ID"],
                        Lastname = dr["Lastname"].ToString(),
                        Middlename = dr["Middlename"].ToString(),
                        Firstname = dr["Firstname"].ToString(),
                        Gender = dr["Gender"].ToString(),
                        Grade_Level = dr["Grade Level"].ToString(),
                        SectionName = dr["Section Name"].ToString(),
                        StudentNumber = dr["StudentNumber"].ToString(),
                        Password = dr["Password"].ToString(),
                        SchoolYearStart = dr["SchoolYearStart"].ToString(),
                        SchoolYearEnd = dr["SchoolYearEnd"].ToString(),
                    });
                }
            }

            return listReturn;
        }

        public List<StudentAccount> GetStudentAccountDetailsByGradeLevelFilter()
        {

            StudentAccount studentAccount = new StudentAccount();
            var listReturn = new List<StudentAccount>();


            using (var cn = new SqlConnection(ConnectionHelper.LGAConnection()))
            {
                cn.Open();
                var cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT Grade_Level FROM YearLevel";               
                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    listReturn.Add(new StudentAccount
                    {
                        
                        Grade_Level = dr["Grade_Level"].ToString()
                       
                    });
                }
            }

            return listReturn;
        }

        public List<StudentAccount> GetStudentAccountDetailsBySection(string section)
        {

            StudentAccount studentAccount = new StudentAccount();
            var listReturn = new List<StudentAccount>();

            using (var cn = new SqlConnection(ConnectionHelper.LGAConnection()))
            {
                cn.Open();
                var cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT StudentAccount.ID, StudentAccount.Lastname, StudentAccount.Middlename, StudentAccount.Firstname, StudentAccount.Gender, YearLevel.Grade_Level AS 'Grade Level', Section.SectionName AS 'Section Name', StudentAccount.StudentNumber, StudentAccount.Password, Students.SchoolYearStart, Students.SchoolYearEnd FROM StudentAccount JOIN Students ON StudentAccount.ID = Students.StudentID JOIN YearLevel ON YearLevel.ID = Students.Grade_Level JOIN Section ON Section.Grade_Level = YearLevel.ID WHERE Section.SectionName = @section";
                cmd.Parameters.AddWithValue("@section", section);
                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    listReturn.Add(new StudentAccount
                    {
                        ID = (int)dr["ID"],
                        Lastname = dr["Lastname"].ToString(),
                        Middlename = dr["Middlename"].ToString(),
                        Firstname = dr["Firstname"].ToString(),
                        Gender = dr["Gender"].ToString(),
                        Grade_Level = dr["Grade Level"].ToString(),
                        SectionName = dr["Section Name"].ToString(),
                        StudentNumber = dr["StudentNumber"].ToString(),
                        Password = dr["Password"].ToString(),
                        SchoolYearStart = dr["SchoolYearStart"].ToString(),
                        SchoolYearEnd = dr["SchoolYearEnd"].ToString(),
                    });
                }
            }

            return listReturn;
        }

        public List<StudentAccount> GetStudentAccountDetailsBySectionFilter()
        {

            StudentAccount studentAccount = new StudentAccount();
            var listReturn = new List<StudentAccount>();


            using (var cn = new SqlConnection(ConnectionHelper.LGAConnection()))
            {
                cn.Open();
                var cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT SectionName FROM Section";
                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    listReturn.Add(new StudentAccount
                    {

                        SectionName = dr["SectionName"].ToString()

                    });
                }
            }

            return listReturn;
        }
    }

}