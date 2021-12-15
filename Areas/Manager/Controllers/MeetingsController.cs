using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Http;
using DigeraitMIS.Areas.Manager.Models;

namespace DigeraitMIS.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class MeetingsController : Controller
    {

        private readonly IConfiguration configuration;

        public MeetingsController(IConfiguration config)
        {
            this.configuration = config;
        }

        // GET: MeetingsController
        [HttpGet]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("managerID") == null)
            {
                return RedirectToAction("Login", "Account");
            }
  
            string connSt = configuration.GetConnectionString("connString");

            SqlConnection Conn = new SqlConnection(connSt);

            Conn.Open();

            SqlCommand Comm = new SqlCommand("sp_GetMeetings", Conn);
            Comm.CommandType = CommandType.StoredProcedure;

            int centreNo = (int)HttpContext.Session.GetInt32("CentreNo");
           // model.CentreNo = cenId;
            Comm.Parameters.AddWithValue("@centreNo", centreNo);

            SqlDataAdapter Adapter = new SqlDataAdapter(Comm);
            DataTable data = new DataTable();
            Adapter.Fill(data);
            Conn.Close();
            ViewData.Model = data.AsEnumerable();

            return View();
        } 
        // GET: MeetingsController/Add
        public ActionResult Add()
        {
            if (HttpContext.Session.GetInt32("managerID") == null)
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        // POST: MeetingsController/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(MeetingVM model)
        {
            if (ModelState.IsValid)
            {
                string connString = configuration.GetConnectionString("connString");

                SqlConnection dbConn = new SqlConnection(connString);

                dbConn.Open();

                SqlCommand dbComm = new SqlCommand("sp_AddMeeting", dbConn);
                dbComm.CommandType = CommandType.StoredProcedure;

                int cenId = (int)HttpContext.Session.GetInt32("CentreNo");
                model.CentreNo = cenId;
                string status = "active";
                model.Status = status;

                dbComm.Parameters.AddWithValue("@Title", model.Title);
                dbComm.Parameters.AddWithValue("@Date", model.Date);
                dbComm.Parameters.AddWithValue("@Description", model.Description);
                dbComm.Parameters.AddWithValue("@CentreNo", model.CentreNo);
                dbComm.Parameters.AddWithValue("@Status", model.Status);

                int x = dbComm.ExecuteNonQuery();
                dbConn.Close();

                return RedirectToAction("Index", "Meetings", new { area = "Manager" });
            }
            else
            {
                return View(model);
            }
        }
        // GET: TransactionController/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (HttpContext.Session.GetInt32("managerID") == null)
            {
                return RedirectToAction("Login", "Account");
            }
            string connString = configuration.GetConnectionString("connString");

            SqlConnection dbConn = new SqlConnection(connString);

            dbConn.Open();

            SqlCommand dbComm = new SqlCommand("sp_GetMeetingByID", dbConn);
            dbComm.CommandType = CommandType.StoredProcedure;

            int meetingID = id;
            TempData["id"] = meetingID;
            dbComm.Parameters.AddWithValue("@MeetingID", meetingID);

            SqlDataAdapter dbAdapter = new SqlDataAdapter(dbComm);
            DataTable dt = new DataTable();
            dbAdapter.Fill(dt);
            EditMeetingVM editMeetingVM = new EditMeetingVM();

            string data = dt.Rows[0]["Title"].ToString();
            editMeetingVM.Description = dt.Rows[0]["Description"].ToString();
            editMeetingVM.Title = dt.Rows[0]["Title"].ToString();
            editMeetingVM.Date = dt.Rows[0]["Date"].ToString();

            TempData["data"] = $"{data}";
            dbConn.Close();

            return View(editMeetingVM);
        }

        // POST: TransactionController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditMeetingVM model)
        {
            if (ModelState.IsValid)
            {
                string connString = configuration.GetConnectionString("connString");

                SqlConnection dbConn = new SqlConnection(connString);

                dbConn.Open();

                SqlCommand dbComm = new SqlCommand("sp_EditMeeting", dbConn);
                dbComm.CommandType = CommandType.StoredProcedure;
                int id = int.Parse(TempData["id"].ToString());
                dbComm.Parameters.AddWithValue("@MeetingID", id);
                dbComm.Parameters.AddWithValue("@Description", model.Description);

                int x = dbComm.ExecuteNonQuery();
                dbConn.Close();
                TempData["message"] = $"Successfully edited your meeting";
                return RedirectToAction("Index", "Meetings", new { area = "Manager" });
            }
            else
            {
                return View(model);
            }
        }
    }
}
