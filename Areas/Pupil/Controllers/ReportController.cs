using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using System.Data;
using DigeraitMIS.Areas.Pupil.Models;
using Microsoft.AspNetCore.Http;

namespace DigeraitMIS.Areas.Pupil.Controllers
{
    [Area("Pupil")]
    public class ReportController : Controller
    {
        private readonly IConfiguration configuration;

        public ReportController(IConfiguration config)
        {
            this.configuration = config;
        }

        //[HttpGet]
        //public IActionResult Display()
        //{
        //    return View();
        //}


        [HttpGet]
        public IActionResult Display(string term = "Term1")
        {
            if (HttpContext.Session.GetInt32("id") == null)
            {
                return RedirectToAction("Login", "Account", new { area = "" });
            }
            string connString = configuration.GetConnectionString("connString");

            SqlConnection dbConn = new SqlConnection(connString);
            ReportViewModel model = new ReportViewModel();
            model.Term = term;

            dbConn.Open();

            SqlCommand dbComm = new SqlCommand("sp_GetPupilReport ", dbConn);
            dbComm.CommandType = CommandType.StoredProcedure;
            int id = (int)HttpContext.Session.GetInt32("id");
            dbComm.Parameters.AddWithValue("@pupilId", id);
            dbComm.Parameters.AddWithValue("@term", model.Term);
            SqlDataAdapter dbAdapter = new SqlDataAdapter(dbComm);
            DataTable dt = new DataTable();
            dbAdapter.Fill(dt);

            dbConn.Close();

            List<ReportViewModel> reports = new List<ReportViewModel>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ReportViewModel report = new ReportViewModel();

                report.ReportID = int.Parse(dt.Rows[i]["ReportID"].ToString());
                report.Name = dt.Rows[i]["Name"].ToString();
                report.FirstName = dt.Rows[i]["FirstName"].ToString();
                report.Surname = dt.Rows[i]["Surname"].ToString();
                report.Term = dt.Rows[i]["Term"].ToString();
                report.Date = dt.Rows[i]["Date"].ToString();
                report.Days = int.Parse(dt.Rows[i]["DaysAbsent"].ToString());
                report.Comments = dt.Rows[i]["Comments"].ToString();
                report.Centre = dt.Rows[i]["Centre"].ToString();
                report.Ratings = dt.Rows[i]["OverallRatings"].ToString();

                reports.Add(report);
            }

            ViewBag.Name = "";
            ViewBag.Surname = "";
            ViewBag.Date = "";
            ViewBag.Centre = "";
            ViewBag.Term = "";

            if (dt.Rows.Count != 0)
            {
                for (int i = 0; i < 1; i++)
                {
                    ViewBag.Name = dt.Rows[i]["FirstName"].ToString();
                    ViewBag.Surname = dt.Rows[i]["Surname"].ToString();
                    ViewBag.Date = dt.Rows[i]["Date"].ToString();
                    ViewBag.Centre = dt.Rows[i]["Centre"].ToString();
                    ViewBag.Term = dt.Rows[i]["Term"].ToString();
                }
            }


            ViewBag.Report = reports.ToList();


            return View();
        }
    }
}
