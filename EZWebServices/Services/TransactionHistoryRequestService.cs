using EZWebServices.Helpers;
using EZWebServices.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EZWebServices.Services
{
    public class TransactionHistoryRequestService
    {

        public bool CreateStudentBalanceInformation(TransactionHistoryRequest request)
        {
            try
            {
                var studentId = CreateStudentTransactionHistory(request);
                UpdateStudentBalance(studentId, request);
                return true;
            }
            catch (Exception e)
            {
                return false;

            }
        }


        public int CreateStudentTransactionHistory(TransactionHistoryRequest request)
        {
            decimal studentId = 0;
            DateTime da = DateTime.UtcNow;

            var con = new SqlConnection(ConnectionHelper.LGAConnection());

            using (SqlCommand cmd = new SqlCommand("INSERT INTO TransactionHistory VALUES(@ReferenceNumber, @StudentID, @Amount, @TransactionDate, @DateRecorded, @Note);SELECT SCOPE_IDENTITY()", con))
            {
                cmd.Parameters.AddWithValue("@ReferenceNumber", request.ReferenceNumber);
                cmd.Parameters.AddWithValue("@StudentID", request.Studentid);
                cmd.Parameters.AddWithValue("@Amount", request.Amount);
                cmd.Parameters.AddWithValue("@TransactionDate", request.TransactionDate);
                cmd.Parameters.AddWithValue("@DateRecorded", request.DateRecorded);
                cmd.Parameters.AddWithValue("@Note", request.Note);
                con.Open();

                cmd.ExecuteScalar();
                studentId = request.Studentid;

                con.Close();
            }

            return Convert.ToInt32(studentId);
        }


        public void UpdateStudentBalance(int studentId, TransactionHistoryRequest request)
        {

            var con = new SqlConnection(ConnectionHelper.LGAConnection());

            using (SqlCommand cmd = new SqlCommand("UPDATE StudentBalance SET Balance = @Balance WHERE StudentID = @ID", con))
            {
                cmd.Parameters.AddWithValue("@ID", studentId);
                cmd.Parameters.AddWithValue("@Balance", request.Balance);
                con.Open();
                cmd.ExecuteScalar();
                con.Close();
            }
        }


        public List<TransactionHistoryRequest> GetTransactionHistory()
        {
            var listReturn = new List<TransactionHistoryRequest>();

            using (var cn = new SqlConnection(ConnectionHelper.LGAConnection()))
            {
                cn.Open();
                var cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT TransactionHistory.TransactionId AS 'Transaction ID', TransactionHistory.ReferenceNumber AS 'Reference Number', StudentAccount.StudentNumber AS 'Student Number', StudentAccount.Lastname, StudentAccount.Middlename, StudentAccount.Firstname, TransactionHistory.Amount, TransactionHistory.TransactionDate AS 'Transaction Date', TransactionHistory.DateRecorded AS 'Date Recorded', TransactionHistory.Note FROM TransactionHistory JOIN StudentAccount ON TransactionHistory.StudentId = StudentAccount.ID JOIN StudentBalance ON TransactionHistory.StudentId = StudentBalance.StudentID ORDER BY TransactionDate DESC";
                var dr = cmd.ExecuteReader();
                listReturn = PopulateReturnList(dr);
            }

            return listReturn;
        }

        public List<TransactionHistoryRequest> PopulateReturnList(SqlDataReader dr)
        {

            try
            {
                var listReturn = new List<TransactionHistoryRequest>();

                while (dr.Read())
                {

                    listReturn.Add(new TransactionHistoryRequest
                    {
                        Transactionid = int.Parse(dr["Transaction ID"].ToString()),
                        ReferenceNumber = dr["Reference Number"].ToString(),
                        StudentNumber = dr["Student Number"].ToString(),
                        Lastname = dr["Lastname"].ToString(),
                        Middlename = dr["Middlename"].ToString(),
                        Firstname = dr["Firstname"].ToString(),
                        Amount = int.Parse(dr["Amount"].ToString()),
                        TransactionDate = Convert.ToDateTime(dr["Transaction Date"].ToString()),
                        DateRecorded = Convert.ToDateTime(dr["Date Recorded"].ToString()),
                        Note = dr["Note"].ToString(),                      
                    });
                }

                return listReturn;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}