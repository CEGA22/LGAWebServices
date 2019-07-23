using EZWebServices.Helpers;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace EZWebServices.Models.ReportIt
{
    public class PostConcern
    {
        public string report_no { get; set; }
        public string description { get; set; }
        public string floor { get; set; }
        public string segment { get; set; }
        public string specific_location { get; set; }
        public string comment { get; set; }
        public byte[] image1 { get; set; }
        public byte[] image2 { get; set; }
        public byte[] image3 { get; set; }
        public string status { get; set; }

        public PostReportRemarks SaveConcern(PostConcern concern)
        {
            var objReturn = new PostReportRemarks();

            try
            {

                var ticket_no = "T" + RandomizeHelper.GenerateRandomString(7);
                var concern_no = "C" + RandomizeHelper.GenerateRandomString(7);

                string img1 = string.Empty, img2 = string.Empty, img3 = string.Empty;

                if (concern.image1 != null)
                {
                    img1 = "../imageReport/" + concern.report_no + "/" + concern_no + "_img1.png";
                }

                if (concern.image2 != null)
                {
                    img2 = "../imageReport/" + concern.report_no + "/" + concern_no + "_img2.png";
                }

                if (concern.image3 != null)
                {
                    img3 = "../imageReport/" + concern.report_no + "/" + concern_no + "_img3.png";
                }

                using (var con = new MySqlConnection(ConnectionHelper.ServicesConnection()))
                {
                    try
                    {
                        con.Open();
                        var cmd = new MySqlCommand("INSERT INTO reportit_concern (report_no, ticket_no, concern_no, description, floor, segment, specific_location, comment, image1, image2, image3, imagesLocation, status) VALUES(@report_no, @ticket_no, @concern_no, @description, @floor, @segment, @specific_location, @comment, @image1, @image2, @image3, @imagesLocation, @status)", con);

                        cmd.Parameters.AddWithValue("@report_no", concern.report_no);
                        cmd.Parameters.AddWithValue("@ticket_no", ticket_no);
                        cmd.Parameters.AddWithValue("@concern_no", concern_no);
                        cmd.Parameters.AddWithValue("@description", concern.description);
                        cmd.Parameters.AddWithValue("@floor", concern.floor);
                        cmd.Parameters.AddWithValue("@segment", concern.segment);
                        cmd.Parameters.AddWithValue("@specific_location", concern.specific_location);
                        cmd.Parameters.AddWithValue("@comment", concern.comment);
                        cmd.Parameters.AddWithValue("@image1", img1);
                        cmd.Parameters.AddWithValue("@image2", img2);
                        cmd.Parameters.AddWithValue("@image3", img3);
                        cmd.Parameters.AddWithValue("@imagesLocation", "imageReport/" + concern.report_no);
                        cmd.Parameters.AddWithValue("@date_created", DateTime.Now);
                        cmd.Parameters.AddWithValue("@status", "Draft");


                        if (cmd.ExecuteNonQuery() > 0)
                        {
                            var path = @"C:\xampp\htdocs\reportit\imageReport\" + concern.report_no;
                            //var path = AppDomain.CurrentDomain.BaseDirectory + concern.report_no;
                            bool exists = Directory.Exists(path);

                            if (!exists)
                                System.IO.Directory.CreateDirectory(path);

                            if (concern.image1 != null)
                            {
                                using (var ms = new MemoryStream(concern.image1))
                                {
                                    var img = Image.FromStream(ms);
                                    var bm = new Bitmap(500, 500);
                                    var g = Graphics.FromImage(bm);
                                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                                    g.DrawImage(img, 0, 0, 500, 500);
                                    g.Dispose();
                                    img = bm;

                                    var i2 = new Bitmap(img);
                                    i2.Save(path + @"\" + concern_no + "_img1.png", System.Drawing.Imaging.ImageFormat.Png);
                                }
                            }

                            if (concern.image2 != null)
                            {
                                using (var ms = new MemoryStream(concern.image2))
                                {
                                    var img = Image.FromStream(ms);
                                    var bm = new Bitmap(500, 500);
                                    var g = Graphics.FromImage(bm);
                                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                                    g.DrawImage(img, 0, 0, 500, 500);
                                    g.Dispose();
                                    img = bm;
                                    img.Save(path + @"\" + concern_no + "_img2.png", System.Drawing.Imaging.ImageFormat.Png);
                                }
                            }

                            if (concern.image3 != null)
                            {
                                using (var ms = new MemoryStream(concern.image3))
                                {
                                    var img = Image.FromStream(ms);
                                    var bm = new Bitmap(500, 500);
                                    var g = Graphics.FromImage(bm);
                                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                                    g.DrawImage(img, 0, 0, 500, 500);
                                    g.Dispose();
                                    img = bm;
                                    img.Save(path + @"\" + concern_no + "_img3.png", System.Drawing.Imaging.ImageFormat.Png);
                                }
                            }



                            objReturn = new PostReportRemarks
                            {
                                remarks = "Successfully added new concern!",
                                subject = "Concern"
                            };
                        }

                        else
                        {
                            objReturn = new PostReportRemarks
                            {
                                remarks = "Error on adding of new concern!",
                                subject = "Error"
                            };
                        }
                    }

                    catch (Exception e)
                    {
                        objReturn = new PostReportRemarks
                        {
                            remarks = e.InnerException.ToString(),
                            subject = "Error"
                        };
                    }
                }
            }

            catch(Exception ex)
            {
                objReturn = new PostReportRemarks
                {
                    remarks = ex.InnerException.ToString(),
                    subject = "Error"
                };
            }
            return objReturn;
        }

    }
}