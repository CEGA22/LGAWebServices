using EZWebServices.Helpers;
using EZWebServices.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EZWebServices.Services
{
    public class ClassRecordRequestService
    {
        public bool UpdateClassRecord(ClassRecordRequest request)
        {
            try
            {
                var con = new SqlConnection(ConnectionHelper.LGAConnection());

                using (SqlCommand cmd = new SqlCommand("UPDATE ClassRecords SET WrittenWork1 = @WrittenWork1,WrittenWork2 = @WrittenWork2,WrittenWork3 = @WrittenWork3,WrittenWork4 = @WrittenWork4,WrittenWork5 = @WrittenWork5,WrittenWork6 = @WrittenWork6,WrittenWork7 = @WrittenWork7,WrittenWork8 = @WrittenWork8,WrittenWork9 = @WrittenWork9,WrittenWork10 = @WrittenWork10,WrittenWorkTotal = @WrittenWorkTotal, WrittentWorkPercentage = @WrittenWorkPercentage,TaskPerformance1 = @TaskPerformance1,TaskPerformance2 = @TaskPerformance2,TaskPerformance3 = @TaskPerformance3,TaskPerformance4 = @TaskPerformance4,TaskPerformance5 = @TaskPerformance5,TaskPerformance6 = @TaskPerformance6,TaskPerformance7 = @TaskPerformance7,TaskPerformance8 = @TaskPerformance8,TaskPerformance9 = @TaskPerformance9,TaskPerformance10 = @TaskPerformance10,TaskPerformanceTotal = @TaskPerformanceTotal,TaskPerformancePercentage = @TaskPerformancePercentage,InitialGrade = @InitialGrade,QuarterlyGrade = @QuarterlyGrade,GradingPeriod = @GradingPeriod, SaveasDraft = @SaveasDraft, SaveDraft = @SaveDraft FROM ClassRecords JOIN  Students ON ClassRecords.Learnersname = Students.StudentID JOIN StudentAccount ON Students.StudentID = StudentAccount.ID JOIN Section ON Students.Grade_Level = Section.ID JOIN SectionsHandled ON Section.ID = SectionsHandled.Gradelevel JOIN SubjectsHandled ON SectionsHandled.Gradelevel = SubjectsHandled.Grade_Level JOIN SchoolAccount ON SectionsHandled.ID = SchoolAccount.ID JOIN Subjects ON SubjectsHandled.Subject = Subjects.ID WHERE Subjects.SubjectName = @SubjectName AND GradingPeriod = @GradingPeriod AND Learnersname = @StudentID AND SectionsHandled.Teacher = @TeacherID", con))
                {
                    cmd.Parameters.AddWithValue("@StudentID", request.ID);
                    cmd.Parameters.AddWithValue("@TeacherID", request.TeacherID);
                    cmd.Parameters.AddWithValue("@WrittenWork1", request.WrittenWork1);
                    cmd.Parameters.AddWithValue("@WrittenWork2", request.WrittenWork2);
                    cmd.Parameters.AddWithValue("@WrittenWork3", request.WrittenWork3);
                    cmd.Parameters.AddWithValue("@WrittenWork4", request.WrittenWork4);
                    cmd.Parameters.AddWithValue("@WrittenWork5", request.WrittenWork5);
                    cmd.Parameters.AddWithValue("@WrittenWork6", request.WrittenWork6);
                    cmd.Parameters.AddWithValue("@WrittenWork7", request.WrittenWork7);
                    cmd.Parameters.AddWithValue("@WrittenWork8", request.WrittenWork8);
                    cmd.Parameters.AddWithValue("@WrittenWork9", request.WrittenWork9);
                    cmd.Parameters.AddWithValue("@WrittenWork10", request.WrittenWork10);
                    cmd.Parameters.AddWithValue("@WrittenWorkTotal", request.WrittenWorkTotal);
                    cmd.Parameters.AddWithValue("@WrittenWorkPercentage", request.WrittenWorkPercentage);
                    cmd.Parameters.AddWithValue("@TaskPerformance1", request.TaskPeformance1);
                    cmd.Parameters.AddWithValue("@TaskPerformance2", request.TaskPeformance2);
                    cmd.Parameters.AddWithValue("@TaskPerformance3", request.TaskPeformance3);
                    cmd.Parameters.AddWithValue("@TaskPerformance4", request.TaskPeformance4);
                    cmd.Parameters.AddWithValue("@TaskPerformance5", request.TaskPeformance5);
                    cmd.Parameters.AddWithValue("@TaskPerformance6", request.TaskPeformance6);
                    cmd.Parameters.AddWithValue("@TaskPerformance7", request.TaskPeformance7);
                    cmd.Parameters.AddWithValue("@TaskPerformance8", request.TaskPeformance8);
                    cmd.Parameters.AddWithValue("@TaskPerformance9", request.TaskPeformance9);
                    cmd.Parameters.AddWithValue("@TaskPerformance10", request.TaskPeformance10);
                    cmd.Parameters.AddWithValue("@TaskPerformanceTotal", request.TaskPeformanceTotal);
                    cmd.Parameters.AddWithValue("@TaskPerformancePercentage", request.TaskPeformancePercentage);
                    cmd.Parameters.AddWithValue("@InitialGrade", request.InitialGrade);
                    cmd.Parameters.AddWithValue("@QuarterlyGrade", request.QuarterlyGrade);
                    cmd.Parameters.AddWithValue("@SubjectName", request.SubjectName);
                    cmd.Parameters.AddWithValue("@SaveasDraft", request.SaveasDraft);
                    cmd.Parameters.AddWithValue("@SaveDraft", request.SaveDraft);
                    cmd.Parameters.AddWithValue("@GradingPeriod", request.GradingPeriod);
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