using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Microsoft.Data.SqlClient;
using DigeraitMIS.Models;
using DigeraitMIS.Areas.Manager;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;

namespace DigeraitMIS.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class HomeController : Controller
    {
        private readonly IConfiguration configuration;

        public HomeController(IConfiguration config)
        {
            this.configuration = config;
        }
        // GET: HomeController
        public IActionResult Index()
        {  
            if (HttpContext.Session.GetInt32("managerID") == null)
            {
                return RedirectToAction("Login", "Account");
            }

            string connString = configuration.GetConnectionString("connString");

            SqlConnection dbConn = new SqlConnection(connString);

            dbConn.Open();

            SqlCommand dbComm = new SqlCommand("sp_GetUserById", dbConn);
            dbComm.CommandType = CommandType.StoredProcedure;
            int? id = HttpContext.Session.GetInt32("managerID");
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
