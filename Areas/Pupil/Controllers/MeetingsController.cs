using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Http;

namespace DigeraitMIS.Areas.Pupil.Controllers
{
    [Area("Pupil")]
    public class MeetingsController : Controller
    {

        private readonly IConfiguration configuration;

        public MeetingsController(IConfiguration config)
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

            string connSt = configuration.GetConnectionString("connString");

            SqlConnection Conn = new SqlConnection(connSt);

            Conn.Open();

            SqlCommand Comm = new SqlCommand("sp_GetPupilMeetings", Conn);
            Comm.CommandType = CommandType.StoredProcedure;

            Comm.Parameters.AddWithValue("@centreNo", centreNo);
            Comm.Parameters.AddWithValue("@date", DateTime.Now);

            SqlDataAdapter Adapter = new SqlDataAdapter(Comm);
            DataTable data = new DataTable();
            Adapter.Fill(data);
            Conn.Close();
            ViewData.Model = data.AsEnumerable();

            return View();
        }
    }
}
