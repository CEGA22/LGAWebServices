using EZWebServices.Helpers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EZWebServices.Models
{
    public class StudentBalance
    {
        public int id { get; set; }

        public string StudentNumber { get; set; }

        public string Lastname { get; set; }

        public string Middlename { get; set; }

        public string Firstname { get; set; }

        public int Total { get; set; }

        public int Balance { get; set; }

        public string PaymentMode { get; set; }

        public int DownPayment { get; set; }

        public string Description { get; set; }

        public int Studentid { get; set; }

        public int Amount { get; set; }

        public DateTime TransactionDate { get; set; }

        public string Note { get; set; }

        public List<StudentBalance> GetStudentBalance()
        {
            var listReturn = new List<StudentBalance>();

            using (var cn = new SqlConnection(ConnectionHelper.LGAConnection()))
            {
                cn.Open();
                var cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT StudentAccount.ID, StudentAccount.StudentNumber,StudentAccount.Lastname, StudentAccount.Middlename, StudentAccount.Firstname, StudentBalance.Total, StudentBalance.Balance, PaymentScheme.PaymentMode, PaymentScheme.Downpayment, PaymentScheme.Description FROM StudentBalance JOIN StudentAccount ON StudentBalance.StudentID = StudentAccount.ID JOIN PaymentScheme ON StudentBalance.PaymentMode = PaymentScheme.SchemeID";
                var dr = cmd.ExecuteReader();
                listReturn = PopulateReturnList(dr);
            }

            return listReturn;
        }

        public List<StudentBalance> PopulateReturnList(SqlDataReader dr)
        {

            try
            {
                var listReturn = new List<StudentBalance>();

                while (dr.Read())
                {

                    listReturn.Add(new StudentBalance
                    {
                        id = int.Parse(dr["ID"].ToString()),
                        StudentNumber = dr["StudentNumber"].ToString(),
                        Lastname = dr["Lastname"].ToString(),
                        Middlename = dr["Middlename"].ToString(),
                        Firstname = dr["Firstname"].ToString(),
                        Total = int.Parse((dr["Total"].ToString())),
                        Balance = int.Parse((dr["Balance"]).ToString()),
                        PaymentMode = (dr["PaymentMode"].ToString()),
                        DownPayment = int.Parse((dr["DownPayment"].ToString())),
                        Description = dr["Description"].ToString(),
                    });
                }

                return listReturn;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public List<StudentBalance> GetStudentBalanceByAccount(int ID)
        {
            var listReturn = new List<StudentBalance>();

            using (var cn = new SqlConnection(ConnectionHelper.LGAConnection()))
            {
                cn.Open();
                var cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT StudentAccount.ID, StudentAccount.StudentNumber, StudentAccount.Lastname, StudentAccount.Middlename, StudentAccount.Firstname, StudentBalance.Total,StudentBalance.Balance, PaymentScheme.PaymentMode,PaymentScheme.Downpayment, PaymentScheme.Description FROM StudentBalance JOIN StudentAccount ON StudentBalance.StudentID = StudentAccount.ID JOIN PaymentScheme ON StudentBalance.PaymentMode = PaymentScheme.SchemeID WHERE StudentBalance.StudentID = @ID and StudentBalance.SchoolYear = YEAR(GETDATE())";
                cmd.Parameters.AddWithValue("@ID", ID);
                var dr = cmd.ExecuteReader();
                listReturn = PopulateReturnLists(dr);
            }

            return listReturn;
        }

        public List<StudentBalance> PopulateReturnLists(SqlDataReader dr)
        {

            try
            {
                var listReturn = new List<StudentBalance>();

                while (dr.Read())
                {

                    listReturn.Add(new StudentBalance
                    {
                        id = int.Parse(dr["ID"].ToString()),
                        StudentNumber = dr["StudentNumber"].ToString(),
                        Lastname = dr["Lastname"].ToString(),
                        Middlename = dr["Middlename"].ToString(),
                        Firstname = dr["Firstname"].ToString(),
                        Total = int.Parse((dr["Total"].ToString())),
                        Balance = int.Parse((dr["Balance"]).ToString()),
                        PaymentMode = (dr["PaymentMode"].ToString()),
                        DownPayment = int.Parse((dr["DownPayment"].ToString())),
                        Description = dr["Description"].ToString(),
                    });
                }

                return listReturn;
            }
            catch (Exception e)
            {

                throw;
            }
        }
    }
}