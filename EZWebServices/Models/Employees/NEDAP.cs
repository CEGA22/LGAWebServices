using EZWebServices.Helpers;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Web;

namespace EZWebServices.Models.Employees
{
    public class NEDAP
    {
        public string emp_id { get; set; }
        public string name { get; set; }
        public string name_ara { get; set; }
        public byte[] generated { get; set; }
        public string remarks { get; set; }

        public NEDAP GetNEDAPAccess(string emp_id) 
        
        {
            var objReturn = new NEDAP();

            using (var con = new MySqlConnection(ConnectionHelper.BaseXConnection()))
            {
                con.Open();

                using (var cmd = new MySqlCommand("SELECT name, name_ara, tap FROM nedap WHERE emp_id=@emp_id", con))
                {
                    cmd.Parameters.AddWithValue("@emp_id", emp_id);

                    using (var dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                var tap = dr["tap"].ToString().ToUpper();
                                var mace = "MACE:57000000380000000";

                                var qrImg = QRGeneratorHelper.GenerateQR(mace + "" + tap);
                                byte[] imageBytes = null;

                                var converter = new ImageConverter();
                                imageBytes = (byte[])converter.ConvertTo(qrImg, typeof(byte[]));

                                objReturn = new NEDAP
                                {
                                    emp_id = emp_id,
                                    generated = imageBytes,
                                    name = dr["name"].ToString(),
                                    name_ara = dr["name_ara"].ToString(),
                                    remarks = "record found"
                                };
                            }
                        }

                        else 
                        {
                            objReturn = new NEDAP
                            {
                                emp_id = emp_id,
                                remarks = "record not found"
                            };
                        }

                        dr.Close();
                    }
                }
                con.Close();
            }

           

            return objReturn;
        }

        public NEDAP GetNEDAPAEOSDBAcccess(string emp_id)
        {
            var objReturn = new NEDAP();

            using (var cn = new SqlConnection(ConnectionHelper.AEOSDBConnection()))
            {
                cn.Open();
                var cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT * FROM dbo.view_badgeadministration WHERE identifiertype = 'Mifare CSN' AND carrieroid=(SELECT TOP 1 objectid FROM dbo.employee WHERE personnelnr=@emp_id OR email=@emp_id)";
                cmd.Parameters.AddWithValue("@emp_id", emp_id);
                var dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        var tap = dr["badgenumber"].ToString().ToUpper();
                        var mace = "MACE:57000000380000000";
                        
                        var qrImg = QRGeneratorHelper.GenerateQR(mace + "" + tap);
                        byte[] imageBytes = null;

                        var converter = new ImageConverter();
                        imageBytes = (byte[])converter.ConvertTo(qrImg, typeof(byte[]));

                        objReturn = new NEDAP
                        {
                            emp_id = emp_id,
                            generated = imageBytes,
                            remarks = "record found"
                        };
                    }
                }

                else
                {
                    objReturn = new NEDAP
                    {
                        emp_id = emp_id,
                        remarks = "record not found"
                    };
                }
            }

            return objReturn;
        }
    }
}