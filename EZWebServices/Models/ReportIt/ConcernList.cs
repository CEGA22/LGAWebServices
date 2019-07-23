using EZWebServices.Helpers;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace EZWebServices.Models.ReportIt
{
    public class ConcernList
    {
        public int id { get; set; }
        public string report_no { get; set; }
        public string ticket_no { get; set; }
        public string concern_no { get; set; }
        public string description { get; set; }
        public string floor { get; set; }
        public string segment { get; set; }
        public string specific_location { get; set; }
        public string comment { get; set; }
        public byte[] image1 { get; set; }
        public string image1Url { get; set; }
        public byte[] image2 { get; set; }
        public string image2Url { get; set; }
        public byte[] image3 { get; set; }
        public string image3Url { get; set; }
        public string imageLocation { get; set; }
        public string fm_ticket_no { get; set; }
        public DateTime date_created { get; set; }
        public DateTime date_ticketed { get; set; }
        public DateTime date_closed { get; set; }
        public string status { get; set; }
        public string wps_comment { get; set; }

        public List<ConcernListData> GetConcernList(string report_no)
        {
            var concernList = new List<ConcernList>();

            using (var con = new MySqlConnection(ConnectionHelper.ServicesConnection()))
            {
                con.Open();
                var cmd = new MySqlCommand("SELECT * FROM reportit_concern WHERE report_no=@report_no", con);
                cmd.Parameters.AddWithValue("@report_no", report_no);
                var dr = cmd.ExecuteReader();
                while (dr.Read())
                {

                    string img1Uri = !string.IsNullOrEmpty(dr["image1"].ToString()) ? Constants.reportItUri + dr["image1"].ToString() : "";
                    string img2Uri = !string.IsNullOrEmpty(dr["image2"].ToString()) ? Constants.reportItUri + dr["image2"].ToString() : "";
                    string img3Uri = !string.IsNullOrEmpty(dr["image3"].ToString()) ? Constants.reportItUri + dr["image3"].ToString() : "";

                    img1Uri = !string.IsNullOrWhiteSpace(img1Uri) ? img1Uri.Replace("..", "") : "";
                    img2Uri = !string.IsNullOrWhiteSpace(img2Uri) ? img2Uri.Replace("..", "") : "";
                    img3Uri = !string.IsNullOrWhiteSpace(img3Uri) ? img3Uri.Replace("..", "") : "";

                    //byte[] img1Bytes, img2Bytes, img3Bytes;

                    //using (var webClient = new WebClient())
                    //{
                    //    img1Bytes = !string.IsNullOrWhiteSpace(img1Uri) ? webClient.DownloadData(img1Uri) : null;
                    //    img2Bytes = !string.IsNullOrWhiteSpace(img2Uri) ? webClient.DownloadData(img2Uri) : null;
                    //    img3Bytes = !string.IsNullOrWhiteSpace(img3Uri) ? webClient.DownloadData(img3Uri) : null;
                    //}

                    concernList.Add(new ConcernList
                    {
                        comment = dr["comment"].ToString(),
                        concern_no = dr["concern_no"].ToString(),
                        date_closed = DateTimeHelper.ValidateDateTime(dr["date_closed"].ToString()),
                        date_created = DateTimeHelper.ValidateDateTime(dr["date_created"].ToString()),
                        date_ticketed = DateTimeHelper.ValidateDateTime(dr["date_ticketed"].ToString()),
                        description = dr["description"].ToString(),
                        floor = dr["floor"].ToString(),
                        fm_ticket_no = dr["fm_ticket_no"].ToString(),
                        id = int.Parse(dr["id"].ToString()),
                        //image1 = img1Bytes,
                        image1Url = img1Uri,
                        //image2 = img2Bytes,
                        image2Url = img2Uri,
                        //image3 = img3Bytes,
                        image3Url = img3Uri,
                        report_no = dr["report_no"].ToString(),
                        segment = dr["segment"].ToString(),
                        specific_location = dr["specific_location"].ToString(),
                        status = dr["status"].ToString(),
                        ticket_no = dr["ticket_no"].ToString(),
                        wps_comment = dr["wps_comment"].ToString(),
                    });
                }
            }

            return new List<ConcernListData>
            {
                new ConcernListData
                {
                    data = concernList
                }
            };
        }

        public List<ConcernList> GetMyConcerns(string report_no)
        {
            var listReturn = new List<ConcernList>();

            using (var con = new MySqlConnection(ConnectionHelper.ServicesConnection()))
            {
                con.Open();
                var cmd = new MySqlCommand("SELECT * FROM reportit_concern WHERE report_no=@report_no", con);

                if(report_no.ToLower() == "all")
                {
                    cmd = new MySqlCommand("SELECT * FROM reportit_concern", con);
                }

                cmd.Parameters.AddWithValue("@report_no", report_no);
                var dr = cmd.ExecuteReader();
                while (dr.Read())
                {

                    string img1Uri = !string.IsNullOrEmpty(dr["image1"].ToString()) ? Constants.reportItUri + dr["image1"].ToString() : "";
                    string img2Uri = !string.IsNullOrEmpty(dr["image2"].ToString()) ? Constants.reportItUri + dr["image2"].ToString() : "";
                    string img3Uri = !string.IsNullOrEmpty(dr["image3"].ToString()) ? Constants.reportItUri + dr["image3"].ToString() : "";

                    img1Uri = !string.IsNullOrWhiteSpace(img1Uri) ? img1Uri.Replace("..", "") : "";
                    img2Uri = !string.IsNullOrWhiteSpace(img2Uri) ? img2Uri.Replace("..", "") : "";
                    img3Uri = !string.IsNullOrWhiteSpace(img3Uri) ? img3Uri.Replace("..", "") : "";

                    //byte[] img1Bytes, img2Bytes, img3Bytes;

                    //using (var webClient = new WebClient())
                    //{
                    //    img1Bytes = !string.IsNullOrWhiteSpace(img1Uri) ? webClient.DownloadData(img1Uri) : null;
                    //    img2Bytes = !string.IsNullOrWhiteSpace(img2Uri) ? webClient.DownloadData(img2Uri) : null;
                    //    img3Bytes = !string.IsNullOrWhiteSpace(img3Uri) ? webClient.DownloadData(img3Uri) : null;
                    //}

                    listReturn.Add(new ConcernList
                    {
                        comment = dr["comment"].ToString(),
                        concern_no = dr["concern_no"].ToString(),
                        date_closed = DateTimeHelper.ValidateDateTime(dr["date_closed"].ToString()),
                        date_created = DateTimeHelper.ValidateDateTime(dr["date_created"].ToString()),
                        date_ticketed = DateTimeHelper.ValidateDateTime(dr["date_ticketed"].ToString()),
                        description = dr["description"].ToString(),
                        floor = dr["floor"].ToString(),
                        fm_ticket_no = dr["fm_ticket_no"].ToString(),
                        id = int.Parse(dr["id"].ToString()),
                        //image1 = img1Bytes,
                        image1Url = img1Uri,
                        //image2 = img2Bytes,
                        image2Url = img2Uri,
                        //image3 = img3Bytes,
                        image3Url = img3Uri,
                        report_no = dr["report_no"].ToString(),
                        segment = dr["segment"].ToString(),
                        specific_location = dr["specific_location"].ToString(),
                        status = dr["status"].ToString(),
                        ticket_no = dr["ticket_no"].ToString(),
                        wps_comment = dr["wps_comment"].ToString(),
                    });
                }
            }

            return listReturn;
        }

        public ConcernList GetConcernDetails(string report_no, string concern_no)
        {
            var objReturn = new ConcernList();

            using (var con = new MySqlConnection(ConnectionHelper.ServicesConnection()))
            {
                con.Open();
                var cmd = new MySqlCommand("SELECT * FROM reportit_concern WHERE report_no=@report_no AND concern_no=@concern_no", con);
                cmd.Parameters.AddWithValue("@report_no", report_no);
                cmd.Parameters.AddWithValue("@concern_no", concern_no);
                var dr = cmd.ExecuteReader();
                while (dr.Read())
                {

                    string img1Uri = !string.IsNullOrEmpty(dr["image1"].ToString()) ? Constants.reportItUri + dr["image1"].ToString() : "";
                    string img2Uri = !string.IsNullOrEmpty(dr["image2"].ToString()) ? Constants.reportItUri + dr["image2"].ToString() : "";
                    string img3Uri = !string.IsNullOrEmpty(dr["image3"].ToString()) ? Constants.reportItUri + dr["image3"].ToString() : "";

                    img1Uri = !string.IsNullOrWhiteSpace(img1Uri) ? img1Uri.Replace("..", "") : "";
                    img2Uri = !string.IsNullOrWhiteSpace(img2Uri) ? img2Uri.Replace("..", "") : "";
                    img3Uri = !string.IsNullOrWhiteSpace(img3Uri) ? img3Uri.Replace("..", "") : "";

                    //byte[] img1Bytes, img2Bytes, img3Bytes;

                    //using (var webClient = new WebClient())
                    //{
                    //    img1Bytes = !string.IsNullOrWhiteSpace(img1Uri) ? webClient.DownloadData(img1Uri) : null;
                    //    img2Bytes = !string.IsNullOrWhiteSpace(img1Uri) ? webClient.DownloadData(img2Uri) : null;
                    //    img3Bytes = !string.IsNullOrWhiteSpace(img1Uri) ? webClient.DownloadData(img3Uri) : null;
                    //}

                    objReturn = new ConcernList
                    {
                        comment = dr["comment"].ToString(),
                        concern_no = dr["concern_no"].ToString(),
                        date_closed = DateTimeHelper.ValidateDateTime(dr["date_closed"].ToString()),
                        date_created = DateTimeHelper.ValidateDateTime(dr["date_created"].ToString()),
                        date_ticketed = DateTimeHelper.ValidateDateTime(dr["date_ticketed"].ToString()),
                        description = dr["description"].ToString(),
                        floor = dr["floor"].ToString(),
                        fm_ticket_no = dr["fm_ticket_no"].ToString(),
                        id = int.Parse(dr["id"].ToString()),
                        //image1 = img1Bytes,
                        image1Url = img1Uri,
                        //image2 = img2Bytes,
                        image2Url = img2Uri,
                        //image3 = img3Bytes,
                        image3Url = img3Uri,
                        report_no = dr["report_no"].ToString(),
                        segment = dr["segment"].ToString(),
                        specific_location = dr["specific_location"].ToString(),
                        status = dr["status"].ToString(),
                        ticket_no = dr["ticket_no"].ToString(),
                        wps_comment = dr["wps_comment"].ToString(),
                    };
                }
            }

            return objReturn;
        }

    }

    public class ConcernListData
    {
        public List<ConcernList> data { get; set; }
    }
}