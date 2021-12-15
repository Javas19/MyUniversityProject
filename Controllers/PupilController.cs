using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Microsoft.Data.SqlClient;
using DigeraitMIS.Models.Login;
using DigeraitMIS.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;

namespace DigeraitMIS.Controllersø
{
    public class PupilController : Controller
    {

        private readonly IConfiguration configuration; // you need all code from line 18 till line 50 

        public PupilController(IConfiguration config)
        {
            this.configuration = config;
        }
        public IActionResult Pupils()
        {
            string connString = configuration.GetConnectionString("connString");

            SqlConnection dbConn = new SqlConnection(connString);

            dbConn.Open();

            SqlCommand dbComm = new SqlCommand("sp_GetAllPupils", dbConn);
            dbComm.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter dbAdapter = new SqlDataAdapter(dbComm); // 1. Getting data from the db
            DataTable dt = new DataTable();
            dbAdapter.Fill(dt);
            dbConn.Close();

            List<Pupil> pupils = new List<Pupil>();
            Pupil pupil = new Pupil();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                
                pupil.PupilID = Convert.ToInt32(dt.Rows[i]["PupilID"]);
                pupil.FirstName = dt.Rows[i]["GuardianName"].ToString(); //2. mapping data from the dataTable to the class
                pupils.Add(pupil); //adding data to a list 
            }


            return View(pupil);
        }
    }
}
