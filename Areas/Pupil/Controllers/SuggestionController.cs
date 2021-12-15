using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DigeraitMIS.Areas.Pupil.Models;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;

namespace DigeraitMIS.Areas.Pupil.Controllers
{
    [Area("Pupil")]
    public class SuggestionController : Controller
    {
        private readonly IConfiguration configuration;

        public SuggestionController(IConfiguration config)
        {
            this.configuration = config;
        }

        [HttpGet]
        public IActionResult List()
        {
            if (HttpContext.Session.GetInt32("id") == null)
            {
                return RedirectToAction("Login", "Account", new { area = "" });
            }
            string connString = configuration.GetConnectionString("connString");

            SqlConnection dbConn = new SqlConnection(connString);

            dbConn.Open();

            SqlCommand dbComm = new SqlCommand("sp_GetPupilByID ", dbConn);
            dbComm.CommandType = CommandType.StoredProcedure;
            int id = (int)HttpContext.Session.GetInt32("id");

            dbComm.Parameters.AddWithValue("@pupilId", id);

            SqlDataAdapter dbAdapter = new SqlDataAdapter(dbComm);
            DataTable dt = new DataTable();
            dbAdapter.Fill(dt);

            int centreNo = int.Parse(dt.Rows[0]["CentreNo"].ToString());

            dbConn.Close();
            dt.Clear();

            string connSt = configuration.GetConnectionString("connString");

            SqlConnection Conn = new SqlConnection(connSt);

            Conn.Open();

            SqlCommand Comm = new SqlCommand("sp_ListSuggestions", Conn);
            Comm.CommandType = CommandType.StoredProcedure;

            Comm.Parameters.AddWithValue("@centreNo", centreNo);

            SqlDataAdapter Adapter = new SqlDataAdapter(Comm);
            DataTable data = new DataTable();
            Adapter.Fill(data);
            Conn.Close();
            ViewData.Model = data.AsEnumerable();

            return View();
        }

        [HttpGet]
        public IActionResult Add()
        {
            if (HttpContext.Session.GetInt32("id") == null)
            {
                return RedirectToAction("Login", "Account", new { area = "" });
            }
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (HttpContext.Session.GetInt32("id") == null)
            {
                return RedirectToAction("Login", "Account", new { area = "" });
            }

            string connString = configuration.GetConnectionString("connString");

            SqlConnection dbConn = new SqlConnection(connString);

            dbConn.Open();

            SqlCommand dbComm = new SqlCommand("sp_GetSuggestionById", dbConn);
            dbComm.CommandType = CommandType.StoredProcedure;

            int userid = id;
            TempData["id"] = userid;
            dbComm.Parameters.AddWithValue("@suggestionId", userid);

            SqlDataAdapter dbAdapter = new SqlDataAdapter(dbComm);
            DataTable dt = new DataTable();
            dbAdapter.Fill(dt);

            EditSuggestionView edit = new EditSuggestionView();
            string data = dt.Rows[0]["Description"].ToString();
            edit.Suggestion = dt.Rows[0]["Description"].ToString();
            //for (int i = 0; i < 1; i++)
            //{
               
            //}
            ViewBag.Description = edit.Suggestion;
            //AddSuggestionViewModel n = new AddSuggestionViewModel();
            // n.Suggestion = ViewBag.Description;
            TempData["data"] = $"{data}";
            dbConn.Close();

            return View(edit);


        }

        [HttpPost]
        public IActionResult Edit(AddSuggestionViewModel model)
        {
            if (ModelState.IsValid)
            {
                string connString = configuration.GetConnectionString("connString");

                SqlConnection dbConn = new SqlConnection(connString);

                dbConn.Open();

                SqlCommand dbComm = new SqlCommand("sp_EditSuggestion", dbConn);
                dbComm.CommandType = CommandType.StoredProcedure;
                int id = int.Parse(TempData["id"].ToString());
                dbComm.Parameters.AddWithValue("@suggestionId", id);
                dbComm.Parameters.AddWithValue("@Description", model.Suggestion);

                int x = dbComm.ExecuteNonQuery();
                dbConn.Close();
                TempData["suggestion"] = $"Successfully edited your suggestion";
                return RedirectToAction("List", "Suggestion", new { area = "Pupil" });

            }
            else
            {
                return View(model);
            }
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            string connString = configuration.GetConnectionString("connString");

            SqlConnection dbConn = new SqlConnection(connString);

            dbConn.Open();

            SqlCommand dbComm = new SqlCommand("sp_RemoveSuggestion", dbConn);
            dbComm.CommandType = CommandType.StoredProcedure;

            dbComm.Parameters.AddWithValue("@suggestionId", id);

            int x = dbComm.ExecuteNonQuery();
            dbConn.Close();

            TempData["suggestionDelete"] = $"Successfully removed your suggestion";

            return RedirectToAction("List", "Suggestion", new { area = "Pupil" });

        }

        [HttpPost]
        public IActionResult Add(AddSuggestionViewModel model)
        {
            if (ModelState.IsValid)
            {
                string connString = configuration.GetConnectionString("connString");

                SqlConnection dbConn = new SqlConnection(connString);

                dbConn.Open();

                SqlCommand dbComm = new SqlCommand("sp_AddSuggestion", dbConn);
                dbComm.CommandType = CommandType.StoredProcedure;


                model.date = DateTime.Now.ToString();
                model.status = "active";
                model.PupilId = (int)HttpContext.Session.GetInt32("id");

                dbComm.Parameters.AddWithValue("@pupilId", model.PupilId);
                dbComm.Parameters.AddWithValue("@date", model.date);
                dbComm.Parameters.AddWithValue("@description", model.Suggestion);
                dbComm.Parameters.AddWithValue("@status", model.status);

                int x = dbComm.ExecuteNonQuery();
                dbConn.Close();

                return RedirectToAction("List", "Suggestion", new { area = "Pupil" });
            }
            else
            {
                return View(model);
            }
        }
    }
}
