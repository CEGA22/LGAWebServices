using EZWebServices.Helpers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EZWebServices.Models
{
    public class PaymentScheme
    {
        public int SchemeID { get; set; }

        public string PaymentMode { get; set; }

        public string Description { get; set; }

        public int DownPayment { get; set; }

        public int Total { get; set; }

        public List<PaymentScheme> GetPaymentScheme()
        {
            var listReturn = new List<PaymentScheme>();

            using (var cn = new SqlConnection(ConnectionHelper.LGAConnection()))
            {
                cn.Open();
                var cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT * FROM PaymentScheme";
                var dr = cmd.ExecuteReader();
                listReturn = PopulateReturnList(dr);
            }

            return listReturn;
        }

        public List<PaymentScheme> PopulateReturnList(SqlDataReader dr)
        {

            try
            {
                var listReturn = new List<PaymentScheme>();

                while (dr.Read())
                {

                    listReturn.Add(new PaymentScheme
                    {
                        SchemeID = int.Parse(dr["SchemeID"].ToString()),
                        PaymentMode = dr["PaymentMode"].ToString(),
                        Description = dr["Description"].ToString(),
                        DownPayment = int.Parse(dr["DownPayment"].ToString()),
                        Total = int.Parse(dr["Total"].ToString()),                       
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