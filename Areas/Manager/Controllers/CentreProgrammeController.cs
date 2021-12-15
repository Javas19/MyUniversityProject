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
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DigeraitMIS.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class CentreProgrammeController : Controller
    {
        private readonly IConfiguration configuration;

        public CentreProgrammeController(IConfiguration config)
        {
            this.configuration = config;
        }
        public ActionResult Index()
        {
            if (HttpContext.Session.GetInt32("managerID") == null)
            {
                return RedirectToAction("Login", "Account");
            }

            string connString = configuration.GetConnectionString("connString");

            SqlConnection dbConn = new SqlConnection(connString);

            dbConn.Open();

            SqlCommand dbComm = new SqlCommand("sp_GetAllCentreProgrammes", dbConn);
            dbComm.CommandType = CommandType.StoredProcedure;
            int cenId = (int)HttpContext.Session.GetInt32("CentreNo");
            dbComm.Parameters.AddWithValue("@CentreNo", cenId);

            SqlDataAdapter dbAdapter = new SqlDataAdapter(dbComm);
            DataTable dt = new DataTable();
            dbAdapter.Fill(dt);
            dbConn.Close();

            ViewData.Model = dt.AsEnumerable();
            return View();
        }
        // GET: CentreProgrammesController/Add
        [HttpGet]
        public ActionResult Add()
        {
            if (HttpContext.Session.GetInt32("managerID") == null)
            {
                return RedirectToAction("Login", "Account");
            }
            string connString = configuration.GetConnectionString("connString");
            SqlConnection dbConn = new SqlConnection(connString);
            dbConn.Open();

            SqlCommand dbComm = new SqlCommand("sp_GetAllTeachersByID", dbConn);
            dbComm.CommandType = CommandType.StoredProcedure;
            int cenId = (int)HttpContext.Session.GetInt32("CentreNo");
            dbComm.Parameters.AddWithValue("@CentreNo", cenId);

            SqlDataAdapter dbAdapter = new SqlDataAdapter(dbComm);
            DataTable dt = new DataTable();
            dbAdapter.Fill(dt);
            dbConn.Close();

            List<TeacherViewModel> teachers = new List<TeacherViewModel>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                TeacherViewModel teacher = new TeacherViewModel();
                teacher.TeacherID = Convert.ToInt32(dt.Rows[i]["TeacherID"]);
                teacher.FullNames = dt.Rows[i]["FirstName"].ToString() + " " + dt.Rows[i]["Surname"].ToString();
                teachers.Add(teacher);
            }

            ViewBag.Teachers = teachers.ToList();

            // for Programmes
            SqlConnection dbConn1 = new SqlConnection(connString);

            dbConn1.Open();

            SqlCommand dbComm1 = new SqlCommand("sp_GetProgrammes", dbConn1);
            dbComm1.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter dbAdapter1 = new SqlDataAdapter(dbComm1);
            DataTable dt1 = new DataTable();
            dbAdapter1.Fill(dt1);
            dbConn1.Close();

            List<Programme> programmes = new List<Programme>();
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                Programme programme = new Programme();
                programme.ProgrammeID = Convert.ToInt32(dt1.Rows[i]["ProgrammeID"]);
                programme.Name = dt1.Rows[i]["Name"].ToString();

                programmes.Add(programme);
            }

            ViewBag.Programmes = programmes.ToList();
            return View();
        }

        // POST: CentreProgrammesController/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(CentreProgramme model)
        {
            if (ModelState.IsValid)
            {
                string connString = configuration.GetConnectionString("connString");

                SqlConnection dbConn = new SqlConnection(connString);

                dbConn.Open();

                SqlCommand dbComm = new SqlCommand("sp_AddCentreProgramme", dbConn);
                dbComm.CommandType = CommandType.StoredProcedure;

                int cenId = (int)HttpContext.Session.GetInt32("CentreNo");
                model.CentreNo = cenId;
                string status = "active";
                model.Status = status;

                dbComm.Parameters.AddWithValue("@ProgrammeID", model.ProgrammeID);
                dbComm.Parameters.AddWithValue("@TeacherID", model.TeacherID);
                dbComm.Parameters.AddWithValue("@CentreNo", model.CentreNo);
                dbComm.Parameters.AddWithValue("@Date", model.Date);
                dbComm.Parameters.AddWithValue("@Status", model.Status);

                int x = dbComm.ExecuteNonQuery();
                dbConn.Close();

                return RedirectToAction("Index", "CentreProgramme", new { area = "Manager" });
            }
            else
            {
                return View(model);
            }
        }
    }
}
