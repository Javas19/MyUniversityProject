using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Data;
using DigeraitMIS.Controllers.Regional.Regional.Models;
using DigeraitMIS.Data;

namespace DigeraitMIS.Controllers.Regional.Controllers
{
    public class ManageController : Controller
    {
        private readonly IConfiguration configuration;

        public ManageController(IConfiguration config)
        {
            this.configuration = config;
        }

        [HttpGet]
        public IActionResult ManageEcd()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ManageEcd(ManageEcdViewModel model)
        {
            if (ModelState.IsValid)
            {
                string connString = configuration.GetConnectionString("connString");

                SqlConnection dbConn = new SqlConnection(connString);

                dbConn.Open();

                SqlCommand dbComm = new SqlCommand("sp_Centre_Result", dbConn);
                dbComm.CommandType = CommandType.StoredProcedure;

                model.CentreNo = HttpContext.Session.GetInt32("id");
                model.Name = HttpContext.Session.GetString("name");

                /*dbComm.Parameters.AddWithValue("@userId", model.UserId);
                dbComm.Parameters.AddWithValue("@email", model.Email);
                dbComm.Parameters.AddWithValue("@password", model.Password);*/

                int x = dbComm.ExecuteNonQuery();
                dbConn.Close();

                TempData["message"] = $"Successfully retrieved {model.Name}";

                return RedirectToAction("Index", "Home", new { area = "Pupil" });

            }
            else
            {
                return View(model);
            }
        }
    }
}



