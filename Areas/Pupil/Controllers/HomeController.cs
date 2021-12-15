using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Microsoft.Data.SqlClient;
using DigeraitMIS.Models;
using DigeraitMIS.Areas.Pupil;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;

namespace DigeraitMIS.Areas.Pupil.Controllers
{
    [Area("Pupil")]
    public class HomeController : Controller
    {
        private readonly IConfiguration configuration;

        public HomeController(IConfiguration config)
        {
            this.configuration = config;
        }

        public IActionResult Index()
        {

            if (HttpContext.Session.GetInt32("id") == null)
            {
                return RedirectToAction("Login", "Account", new { area = "" });
            }

            string connString = configuration.GetConnectionString("connString");

            SqlConnection dbConn = new SqlConnection(connString);

            dbConn.Open();

            SqlCommand dbComm = new SqlCommand("sp_GetUserById", dbConn);
            dbComm.CommandType = CommandType.StoredProcedure;
            int? id = HttpContext.Session.GetInt32("id");
            dbComm.Parameters.AddWithValue("@userId", id);

            SqlDataAdapter dbAdapter = new SqlDataAdapter(dbComm);
            DataTable dt = new DataTable();
            dbAdapter.Fill(dt);
            dbConn.Close();

            ViewData.Model = dt.AsEnumerable();
            return View();
        }
    }
}
