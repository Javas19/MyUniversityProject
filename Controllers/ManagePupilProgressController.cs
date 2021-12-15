using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DigeraitMIS.Models;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;

namespace DigeraitMIS.Controllers
{
    public class ManagePupilProgressController : Controller
    { 
        private readonly IConfiguration configuration;
        public ManagePupilProgressController(IConfiguration config)
        {
            this.configuration = config;
        }

        [HttpGet]
        public IActionResult AddPupilProgress()
        {
            string connString = configuration.GetConnectionString("connString");

            SqlConnection GetTeacherConn = new SqlConnection(connString);

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

            SqlConnection GetRegistrationdbconn = new SqlConnection(connString);

            GetRegistrationdbconn.Open();

            SqlCommand RegistrationdbComm = new SqlCommand("sp_GetRegisterInfo", GetRegistrationdbconn);
            RegistrationdbComm.CommandType = CommandType.StoredProcedure;



            SqlDataAdapter PRegistrationdbAdapter = new SqlDataAdapter(RegistrationdbComm);

            DataTable Registrationdt = new DataTable();
            PRegistrationdbAdapter.Fill(Registrationdt);

            List<RegisterViewModel> Registrations = new List<RegisterViewModel>();

            for (int i = 0; i < Registrationdt.Rows.Count; i++)
            {
                RegisterViewModel registration = new RegisterViewModel();
                registration.RegisterID = Convert.ToInt32(Registrationdt.Rows[i]["RegisterID"]);
                registration.Status = Registrationdt.Rows[i]["Status"].ToString();
                Registrations.Add(registration);
            }
            ViewBag.Registrations = Registrations;

            GetRegistrationdbconn.Close();



            return View();
            
        }

        [HttpPost]

        public IActionResult AddPupilProgress(ManagePupilProgress pupil) 
            
        {

            if (ModelState.IsValid)

            {
                string connString = configuration.GetConnectionString("connString");

                SqlConnection dbconn = new SqlConnection(connString);

                dbconn.Open();

                pupil.Status = "active";

                

                SqlCommand dbComm = new SqlCommand("sp_ProgressReport", dbconn);
                dbComm.CommandType = CommandType.StoredProcedure;
                dbComm.Parameters.AddWithValue("@programmeID", pupil.ProgrammeID);
                dbComm.Parameters.AddWithValue("@pupilID", pupil.PupilID);
                dbComm.Parameters.AddWithValue("@ratings", pupil.Ratings);
                dbComm.Parameters.AddWithValue("@date", pupil.Date);
                dbComm.Parameters.AddWithValue("@status", pupil.Status);
                dbComm.Parameters.AddWithValue("@registerID", pupil.RegsiterID);
                dbComm.Parameters.AddWithValue("@term", pupil.Term);
                dbComm.Parameters.AddWithValue("@Year", DateTime.Now.Year.ToString());

                int x = dbComm.ExecuteNonQuery();

                dbconn.Close();

                return RedirectToAction("AddPupilProgress", "ManagePupilProgress");


            }

            else
            {
                string connString = configuration.GetConnectionString("connString");

                SqlConnection GetPupildbconn = new SqlConnection(connString);

                GetPupildbconn.Open();

                SqlCommand PupilProgressdbComm = new SqlCommand("sp_GetPupil", GetPupildbconn);
                PupilProgressdbComm.CommandType = CommandType.StoredProcedure;



                SqlDataAdapter PupilProgressdbAdapter = new SqlDataAdapter(PupilProgressdbComm);

                DataTable PupilProgressdt = new DataTable();
                PupilProgressdbAdapter.Fill(PupilProgressdt);

                List<PupilViewModel> Pupils = new List<PupilViewModel>();

                for (int i = 0; i < PupilProgressdt.Rows.Count; i++)
                {
                    PupilViewModel pupil1 = new PupilViewModel();
                    pupil1.PupilID = Convert.ToInt32(PupilProgressdt.Rows[i]["UserID"]);
                    pupil1.Name = PupilProgressdt.Rows[i]["FirstName"].ToString();
                    Pupils.Add(pupil1);
                }
                ViewBag.Pupils = Pupils;

                GetPupildbconn.Close();

                SqlConnection GetTeacherConn = new SqlConnection(connString);

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

                SqlConnection GetRegistrationdbconn = new SqlConnection(connString);

                GetRegistrationdbconn.Open();

                SqlCommand RegistrationdbComm = new SqlCommand("sp_GetRegisterInfo", GetRegistrationdbconn);
                RegistrationdbComm.CommandType = CommandType.StoredProcedure;



                SqlDataAdapter PRegistrationdbAdapter = new SqlDataAdapter(RegistrationdbComm);

                DataTable Registrationdt = new DataTable();
                PRegistrationdbAdapter.Fill(Registrationdt);

                List<RegisterViewModel> Registrations = new List<RegisterViewModel>();

                for (int i = 0; i < Registrationdt.Rows.Count; i++)
                {
                    RegisterViewModel registration = new RegisterViewModel();
                    registration.RegisterID = Convert.ToInt32(Registrationdt.Rows[i]["RegisterID"]);
                    registration.Status = Registrationdt.Rows[i]["Status"].ToString();
                    Registrations.Add(registration);
                }
                ViewBag.Registrations = Registrations;

                GetRegistrationdbconn.Close();


                return View(pupil);

            }


        }
    }
}
