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


namespace DigeraitMIS.Controllers
{
    

    public class SuggestionController : Controller
    {

        private readonly IConfiguration configuration; // you need all code from line 18 till line 50 

        public SuggestionController(IConfiguration config)
        {
            this.configuration = config;
        }
        public IActionResult List()
        {
            string connString = configuration.GetConnectionString("connString");

            SqlConnection dbConn = new SqlConnection(connString);

            dbConn.Open();

            SqlCommand TeacherdbComm = new SqlCommand("sp_GetTeacherByID", dbConn);
            TeacherdbComm.CommandType = CommandType.StoredProcedure;
            int? id = HttpContext.Session.GetInt32("TeacherID");
            TeacherdbComm.Parameters.AddWithValue("@teacherID", id);

            SqlDataAdapter TeacherdbAdapter = new SqlDataAdapter(TeacherdbComm);

            DataTable Teacherdt = new DataTable();
            TeacherdbAdapter.Fill(Teacherdt);

            int CentreNo = int.Parse(Teacherdt.Rows[0]["CentreNo"].ToString());

            SqlCommand dbComm = new SqlCommand("sp_ListSuggestions", dbConn);
            dbComm.CommandType = CommandType.StoredProcedure;
            dbComm.Parameters.AddWithValue("@centreNo", CentreNo);
            SqlDataAdapter dbAdapter = new SqlDataAdapter(dbComm); // 1. Getting data from the db
            DataTable dt = new DataTable();
            dbAdapter.Fill(dt);
            dbConn.Close();

            List<Suggestions> suggestions = new List<Suggestions>();
          
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Suggestions suggestion = new Suggestions();
                suggestion.SuggestionsID = Convert.ToInt32(dt.Rows[i]["SuggestionID"]);
                suggestion.PupilID = Convert.ToInt32(dt.Rows[i]["PupilID"]); //2. mapping data from the dataTable to the class
                suggestion.Date = dt.Rows[i]["Date"].ToString();
                suggestion.Description = dt.Rows[i]["Description"].ToString(); //2. mapping data from the dataTable to the class
                suggestion.Status = dt.Rows[i]["Status"].ToString();
                suggestion.FirstName = dt.Rows[i]["FirstName"].ToString(); //2. mapping data from the dataTable to the class
                suggestion.Surname = dt.Rows[i]["Surname"].ToString();

                suggestions.Add(suggestion); //adding data to a list 
            }


            return View(suggestions);
        }
    }
}
