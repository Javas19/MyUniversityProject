using DigeraitMIS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DigeraitMIS.Controllers.Teacher
{
    public class TermReportController : Controller
    {
        private readonly IConfiguration configuration;

        public TermReportController(IConfiguration config)
        {
            this.configuration = config;
        }

        [HttpGet]
        public IActionResult Add()
        {
            string connString = configuration.GetConnectionString("connString");

            SqlConnection GetTeacherConn = new SqlConnection(connString);
            //

            //Get teacher to get centre number.
            GetTeacherConn.Open();

            SqlCommand TeacherdbComm = new SqlCommand("sp_GetTeacherByID", GetTeacherConn);
            TeacherdbComm.CommandType = CommandType.StoredProcedure;
            int? id = HttpContext.Session.GetInt32("TeacherID");
            TeacherdbComm.Parameters.AddWithValue("@teacherID", id);

            SqlDataAdapter TeacherdbAdapter = new SqlDataAdapter(TeacherdbComm);

            DataTable Teacherdt = new DataTable();
            TeacherdbAdapter.Fill(Teacherdt);

            int CentreNo = int.Parse(Teacherdt.Rows[0]["CentreNo"].ToString());
            GetTeacherConn.Close();


            // Get pupil for dropdown list

            SqlConnection GetPupildbconn = new SqlConnection(connString);

            GetPupildbconn.Open();

            SqlCommand PupilProgressdbComm = new SqlCommand("sp_GetPupil", GetPupildbconn);
            PupilProgressdbComm.CommandType = CommandType.StoredProcedure;
            PupilProgressdbComm.Parameters.AddWithValue("@CentreNo", CentreNo);


            SqlDataAdapter PupilProgressdbAdapter = new SqlDataAdapter(PupilProgressdbComm);

            DataTable PupilProgressdt = new DataTable();
            PupilProgressdbAdapter.Fill(PupilProgressdt);

            List<PupilViewModel> Pupils = new List<PupilViewModel>();

            for (int i = 0; i < PupilProgressdt.Rows.Count; i++)
            {
                PupilViewModel pupil = new PupilViewModel();
                pupil.PupilID = Convert.ToInt32(PupilProgressdt.Rows[i]["UserID"]);
                pupil.Name = PupilProgressdt.Rows[i]["FirstName"].ToString();
                Pupils.Add(pupil);
            }
            ViewBag.Pupils = Pupils;

            GetPupildbconn.Close();

            //Get programme for dropdown list

            SqlConnection GetProgrammeDBConn = new SqlConnection(connString);

            GetProgrammeDBConn.Open();

            SqlCommand ProgrammedbComm = new SqlCommand("sp_GetCentreProgram", GetProgrammeDBConn);
            ProgrammedbComm.CommandType = CommandType.StoredProcedure;

            ProgrammedbComm.Parameters.AddWithValue("CentreNo", CentreNo);

            SqlDataAdapter ProgramdbAdapter = new SqlDataAdapter(ProgrammedbComm);

            DataTable Programdt = new DataTable();
            ProgramdbAdapter.Fill(Programdt);

            List<ProgramViewModel> Programs = new List<ProgramViewModel>();

            for (int i = 0; i < Programdt.Rows.Count; i++)
            {
                ProgramViewModel program = new ProgramViewModel();
                program.ProgrammeID = Convert.ToInt32(Programdt.Rows[i]["ProgrammeID"]);
                program.ProgrammeName = Programdt.Rows[i]["Name"].ToString();
                Programs.Add(program);
            }
            ViewBag.Programs = Programs;

            GetProgrammeDBConn.Close();

            return View();
        }


        [HttpPost]
        public IActionResult Add(TermReportVM model)
        {
            if(ModelState.IsValid)
            {
                string connString = configuration.GetConnectionString("connString");

                SqlConnection dbconn = new SqlConnection(connString);
                //

                //Get teacher to get centre number.
                dbconn.Open();

                SqlCommand dbComm = new SqlCommand("sp_GetProgressReport", dbconn);
                dbComm.CommandType = CommandType.StoredProcedure;
                dbComm.Parameters.AddWithValue("@ProgrammeId", model.ProgrammeId);
                dbComm.Parameters.AddWithValue("@PupilId", model.PupilID);
                dbComm.Parameters.AddWithValue("@Term", model.Term);
                dbComm.Parameters.AddWithValue("@Date", DateTime.Now.Year.ToString());

                SqlDataAdapter dbAdapter = new SqlDataAdapter(dbComm);
                DataTable dt = new DataTable();
                dbAdapter.Fill(dt);

                dbconn.Close();

                double ratings = double.Parse(dt.Rows[0]["Ratings"].ToString());



                //Get Absent Days
                SqlConnection Absentconn = new SqlConnection(connString);
              
                Absentconn.Open();

                SqlCommand AbsentComm = new SqlCommand("sp_GetTotalDays", Absentconn);
                AbsentComm.CommandType = CommandType.StoredProcedure;
                AbsentComm.Parameters.AddWithValue("@ProgrammeId", model.ProgrammeId);
                AbsentComm.Parameters.AddWithValue("@PupilId", model.PupilID);
                AbsentComm.Parameters.AddWithValue("@Term", model.Term);
                AbsentComm.Parameters.AddWithValue("@Date", DateTime.Now.Year.ToString());

                SqlDataAdapter AbsentAdapter = new SqlDataAdapter(AbsentComm);
                DataTable Absentdt = new DataTable();
                AbsentAdapter.Fill(Absentdt);

                Absentconn.Close();
                model.AbsentDays = int.Parse(Absentdt.Rows[0]["AbsentDays"].ToString());



                //Get total Days
                SqlConnection Totalconn = new SqlConnection(connString);

                Totalconn.Open();

                SqlCommand TotalComm = new SqlCommand("sp_TotalDays", Totalconn);
                TotalComm.CommandType = CommandType.StoredProcedure;
                TotalComm.Parameters.AddWithValue("@ProgrammeId", model.ProgrammeId);
                TotalComm.Parameters.AddWithValue("@PupilId", model.PupilID);
                TotalComm.Parameters.AddWithValue("@Term", model.Term);
                TotalComm.Parameters.AddWithValue("@Date", DateTime.Now.Year.ToString());

                SqlDataAdapter totalAdapter = new SqlDataAdapter(TotalComm);
                DataTable Totaldt = new DataTable();
                totalAdapter.Fill(Totaldt);

                Totalconn.Close();
                int days= int.Parse(Totaldt.Rows[0]["TotalDays"].ToString());




                double totalRatings = days * 100;

                double overrallRatings = (ratings / totalRatings) * 100;

                model.OverallRating = overrallRatings.ToString();



               


                // Add report

                SqlConnection reportDbConn = new SqlConnection(connString);
               
                reportDbConn.Open();

                SqlCommand reportDbComm = new SqlCommand("sp_AddFinalReport", reportDbConn);
                reportDbComm.CommandType = CommandType.StoredProcedure;

                model.Date = DateTime.Now.ToString();
                model.Status = "active";

                reportDbComm.Parameters.AddWithValue("@ProgrammeId",model.ProgrammeId);
                reportDbComm.Parameters.AddWithValue("@PupilId", model.PupilID);
                reportDbComm.Parameters.AddWithValue("@Term", model.Term);
                reportDbComm.Parameters.AddWithValue("@DaysAbsent", model.AbsentDays);
                reportDbComm.Parameters.AddWithValue("@Ratings", model.OverallRating);
                reportDbComm.Parameters.AddWithValue("@Comments", model.Comments);
                reportDbComm.Parameters.AddWithValue("@Date", model.Date);
                reportDbComm.Parameters.AddWithValue("@Status", model.Status);

                int x = reportDbComm.ExecuteNonQuery();

                return RedirectToAction("Add", "TermReport");
            }
            else
            {
                string connString = configuration.GetConnectionString("connString");

                SqlConnection GetTeacherConn = new SqlConnection(connString);
                //

                //Get teacher to get centre number.
                GetTeacherConn.Open();

                SqlCommand TeacherdbComm = new SqlCommand("sp_GetTeacherByID", GetTeacherConn);
                TeacherdbComm.CommandType = CommandType.StoredProcedure;
                int? id = HttpContext.Session.GetInt32("TeacherID");
                TeacherdbComm.Parameters.AddWithValue("@teacherID", id);

                SqlDataAdapter TeacherdbAdapter = new SqlDataAdapter(TeacherdbComm);

                DataTable Teacherdt = new DataTable();
                TeacherdbAdapter.Fill(Teacherdt);

                int CentreNo = int.Parse(Teacherdt.Rows[0]["CentreNo"].ToString());
                GetTeacherConn.Close();


                // Get pupil for dropdown list

                SqlConnection GetPupildbconn = new SqlConnection(connString);

                GetPupildbconn.Open();

                SqlCommand PupilProgressdbComm = new SqlCommand("sp_GetPupil", GetPupildbconn);
                PupilProgressdbComm.CommandType = CommandType.StoredProcedure;
                PupilProgressdbComm.Parameters.AddWithValue("@CentreNo", CentreNo);


                SqlDataAdapter PupilProgressdbAdapter = new SqlDataAdapter(PupilProgressdbComm);

                DataTable PupilProgressdt = new DataTable();
                PupilProgressdbAdapter.Fill(PupilProgressdt);

                List<PupilViewModel> Pupils = new List<PupilViewModel>();

                for (int i = 0; i < PupilProgressdt.Rows.Count; i++)
                {
                    PupilViewModel pupil = new PupilViewModel();
                    pupil.PupilID = Convert.ToInt32(PupilProgressdt.Rows[i]["UserID"]);
                    pupil.Name = PupilProgressdt.Rows[i]["FirstName"].ToString();
                    Pupils.Add(pupil);
                }
                ViewBag.Pupils = Pupils;

                GetPupildbconn.Close();

                //Get programme for dropdown list

                SqlConnection GetProgrammeDBConn = new SqlConnection(connString);

                GetProgrammeDBConn.Open();

                SqlCommand ProgrammedbComm = new SqlCommand("sp_GetCentreProgram", GetProgrammeDBConn);
                ProgrammedbComm.CommandType = CommandType.StoredProcedure;

                ProgrammedbComm.Parameters.AddWithValue("CentreNo", CentreNo);

                SqlDataAdapter ProgramdbAdapter = new SqlDataAdapter(ProgrammedbComm);

                DataTable Programdt = new DataTable();
                ProgramdbAdapter.Fill(Programdt);

                List<ProgramViewModel> Programs = new List<ProgramViewModel>();

                for (int i = 0; i < Programdt.Rows.Count; i++)
                {
                    ProgramViewModel program = new ProgramViewModel();
                    program.ProgrammeID = Convert.ToInt32(Programdt.Rows[i]["ProgrammeID"]);
                    program.ProgrammeName = Programdt.Rows[i]["Name"].ToString();
                    Programs.Add(program);
                }
                ViewBag.Programs = Programs;

                GetProgrammeDBConn.Close();

                return View(model);
            }
        }
    }
}
