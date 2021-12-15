using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Microsoft.Data.SqlClient;
using DigeraitMIS.Models;
using Microsoft.AspNetCore.Http;

namespace DigeraitMIS.Controllers
{
    public class TeacherProfileController : Controller

    {
        private readonly IConfiguration configuration;

        public TeacherProfileController(IConfiguration config)
        {
            this.configuration = config;
        }



        [HttpGet]
        public IActionResult Edit()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Edit(EditTeacherProfile model)
        {
            if (ModelState.IsValid)
            {
                string connString = configuration.GetConnectionString("connString");

                SqlConnection dbConn = new SqlConnection(connString);

                dbConn.Open();

                SqlCommand dbComm = new SqlCommand("sp_EditTeacherProfile", dbConn);
                dbComm.CommandType = CommandType.StoredProcedure;

                int ? id = HttpContext.Session.GetInt32("TeacherID");

                dbComm.Parameters.AddWithValue("@TeacherID", id);
                dbComm.Parameters.AddWithValue("@emailAddress", model.emailAddress);
         

                int x = dbComm.ExecuteNonQuery();
                dbConn.Close();
                string Name = HttpContext.Session.GetString("TeacherName");
                TempData["TeacherSuccess"] = $"Successfully updated {Name} profile";

                return RedirectToAction("Display", "TeacherProfile");

            }
            else
            {
                return View(model);
            } 
            
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Display()
        {
            int id = 7;

            string connString = configuration.GetConnectionString("connString");

            SqlConnection dbConn = new SqlConnection(connString);

            dbConn.Open();

            SqlCommand dbComm = new SqlCommand("sp_GetTeacher", dbConn);
            dbComm.CommandType = CommandType.StoredProcedure;
            

            dbComm.Parameters.AddWithValue("teacherID", id);
            SqlDataAdapter dtAdapter = new SqlDataAdapter(dbComm);
            DataTable dt = new DataTable();
            dtAdapter.Fill(dt);


            dbConn.Close();
            ViewData.Model = dt.AsEnumerable();

            return View();



        }

    }
}