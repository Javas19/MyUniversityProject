using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Microsoft.Data.SqlClient;
using DigeraitMIS.Areas.Manager.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using DigeraitMIS.Data;

namespace DigeraitMIS.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class ProfileController : Controller
    {

        private readonly IConfiguration configuration;

        public ProfileController(IConfiguration config)
        {
            this.configuration = config;
        }
        [HttpGet]
        public IActionResult Edit()
        {
            if (HttpContext.Session.GetInt32("managerID") == null)
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }
        [HttpPost]
        public IActionResult Edit(EditProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                string connString = configuration.GetConnectionString("connString");

                SqlConnection dbConn = new SqlConnection(connString);

                dbConn.Open();

                SqlCommand dbComm = new SqlCommand("sp_UpdateManager", dbConn);
                dbComm.CommandType = CommandType.StoredProcedure;

                model.UserId = HttpContext.Session.GetInt32("managerID");
                model.Name = HttpContext.Session.GetString("managerName");
                model.Surname = HttpContext.Session.GetString("managerSurname");

                dbComm.Parameters.AddWithValue("@userId", model.UserId);
                dbComm.Parameters.AddWithValue("@email", model.Email);
                dbComm.Parameters.AddWithValue("@password", model.Password);

                int x = dbComm.ExecuteNonQuery();
                dbConn.Close();

                TempData["message"] = $"Successfully updated {model.Name} profile";

                return RedirectToAction("Index", "Home", new { area = "Manager" });

            }
            else
            {
                return View(model);
            }
        }
    }
}
