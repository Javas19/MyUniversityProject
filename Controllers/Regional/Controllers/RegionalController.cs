using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DigeraitMIS.Controllers.Regional.Regional.Models;

namespace DigeraitMIS.Controllers.Reginal
{
    public class RegionalController : Controller
    {
        private readonly IConfiguration configuration;

        public RegionalController(IConfiguration config)
        {
            this.configuration = config;
        }

        public IActionResult Welcome()
        {
            return View();
        }
        public IActionResult Index(HomePageModel model)
        {
            if (ModelState.IsValid)
            {
                string connString = configuration.GetConnectionString("connString");

                SqlConnection dbConn = new SqlConnection(connString);

                dbConn.Open();

                SqlCommand dbComm = new SqlCommand("sp_GetUserById", dbConn);
                dbComm.CommandType = CommandType.StoredProcedure;

                dbComm.Parameters.AddWithValue("@UserID", model.userID);


                int id = model.userID;
                string name = model.FirstName;
                string surname = model.Surname;

                SqlDataAdapter dbAdapter = new SqlDataAdapter(dbComm);
                DataTable dt = new DataTable();
                dbAdapter.Fill(dt);
                dbConn.Close();
                HttpContext.Session.SetInt32("id", id);
                //HttpContext.Session.SetString("name", model.FirstName);
                //HttpContext.Session.SetString("surname", surname);
                // model.userID = int.Parse(dt.PrimaryKey.GetValue(id).ToString());
                //model.FirstName = int.Parse(dt.["FirstName"].GetValue(id).ToString());
                //model.FirstName = dt.Rows[0]["FirstName"].ToString();
                //model.Surname = dt.Rows[0]["Surname"].ToString();
                //model.Email = dt.Rows[0]["Email"].ToString();

                // int id = int.Parse(dt.Rows[0]["UserID"].ToString());
                //string name = dt.Rows[0]["FirstName"].ToString();
                //string surname = dt.Rows[0]["Surname"].ToString();
            }
            return View(model);
            
        }
        [HttpGet]
        public IActionResult HomePage()
        {
           
            return View();
        }
        [HttpPost]
        public IActionResult HomePage(HomePageModel model)
        {
            if (ModelState.IsValid) 
            {
                string connString = configuration.GetConnectionString("connString");

                SqlConnection dbConn = new SqlConnection(connString);

                dbConn.Open();

                SqlCommand dbComm = new SqlCommand("sp_GetUserById", dbConn);
                dbComm.CommandType = CommandType.StoredProcedure;

                dbComm.Parameters.AddWithValue("@UserID", model.userID);
                dbComm.Parameters.AddWithValue("@Email", model.Email);
                dbComm.Parameters.AddWithValue("@password", model.Password);
                dbComm.Parameters.AddWithValue("@FirstName", model.FirstName);
                dbComm.Parameters.AddWithValue("@Surname", model.Surname);
               


                SqlDataAdapter dbAdapter = new SqlDataAdapter(dbComm);
                DataTable dt = new DataTable();
                dbAdapter.Fill(dt);
                dbConn.Close();
                model.userID = int.Parse(dt.Rows[0]["UserID"].ToString());
                model.FirstName = dt.Rows[0]["FirstName"].ToString();
                model.Surname = dt.Rows[0]["Surname"].ToString();
                model.Email = dt.Rows[0]["Email"].ToString();

                int id = int.Parse(dt.Rows[0]["UserID"].ToString());
                string name = dt.Rows[0]["FirstName"].ToString();
                string surname = dt.Rows[0]["Surname"].ToString();
            }
            return View(model);
        }
        public IActionResult HomePage2(HomePageModel model)
        {
            string connString = configuration.GetConnectionString("connString");

            SqlConnection dbConn = new SqlConnection(connString);

            dbConn.Open();

            SqlCommand dbComm = new SqlCommand("sp_GetUserById", dbConn);
            dbComm.CommandType = CommandType.StoredProcedure;
            int? id = HttpContext.Session.GetInt32("id");
            dbComm.Parameters.AddWithValue("@userId", id);
           
            SqlDataAdapter dbAdapter = new SqlDataAdapter(dbComm);
            DataTable dt = new DataTable();
            int userid = 9;//int.Parse(dt.Rows[0]["UserID"].ToString());
            string Name = dt.Rows[0]["FirstName"].ToString();
            string Surname = dt.Rows[0]["Surname"].ToString();
            if (dt.Rows.Count > 0)
            {

                /*if (dt.Rows[0]["UserType"].ToString() == "Regional")
                {
                    HttpContext.Session.SetInt32("id", userid);
                    HttpContext.Session.SetString("name", Name);
                    HttpContext.Session.SetString("surname", Surname);

                    return RedirectToAction("Index", "Home", new { area = "Pupil" });
                }
                model.UserID = int.Parse(dt.Rows[0]["UserID"].ToString());*/
                if (dt.Columns["UserID"].ToString() == userid.ToString()) 
                {
                    HttpContext.Session.SetInt32("id", userid);
                    HttpContext.Session.SetString("name", Name);
                    HttpContext.Session.SetString("surname", Surname);

                    return RedirectToAction("Index", "Home", new { area = "Pupil" });
                }

            }
            //dbAdapter.Fill(dt);
            dbConn.Close();
            
            ViewData.Model = dt.AsEnumerable();
            return View();
        }
        public IActionResult Report()
        {

            return View();
        }
    }
}
