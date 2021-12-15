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
using System.Net.Mail;
using System.Net;

namespace DigeraitMIS.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class StaffController : Controller
    {
        private readonly IConfiguration configuration;

        public StaffController(IConfiguration config)
        {
            this.configuration = config;
        }

        // GET: TransactionController
        public ActionResult Index()
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


            int? id = HttpContext.Session.GetInt32("managerID");
            // dbComm.Parameters.AddWithValue("@ManagerID", id);

            SqlDataAdapter dbAdapter = new SqlDataAdapter(dbComm);
            DataTable dt = new DataTable();
            dbAdapter.Fill(dt);
            dbConn.Close();

            ViewData.Model = dt.AsEnumerable();
            return View();
        }
        // GET: TransactionController/View
        public ActionResult Detail(int id)
        {
            if (HttpContext.Session.GetInt32("managerID") == null)
            {
                return RedirectToAction("Login", "Account");
            }

            string connString = configuration.GetConnectionString("connString");

            SqlConnection dbConn = new SqlConnection(connString);

            dbConn.Open();

            SqlCommand dbComm = new SqlCommand("sp_GetTeacherByID", dbConn);
            dbComm.CommandType = CommandType.StoredProcedure;

            int teacherID = id;
            TempData["id"] = teacherID;
            dbComm.Parameters.AddWithValue("@TeacherID", teacherID);

            SqlDataAdapter dbAdapter = new SqlDataAdapter(dbComm);
            DataTable dt = new DataTable();
            dbAdapter.Fill(dt);

            string data = dt.Rows[0]["FirstName"].ToString();
            data = data+" "+ dt.Rows[0]["Surname"].ToString();
            //TeacherVM teacherVM = new TeacherVM();

            //teacherVM.UserID = int.Parse(dt.Rows[0]["UserID"].ToString());
            //teacherVM.FirstName = dt.Rows[0]["FirstName"].ToString();
            //teacherVM.Surname = dt.Rows[0]["Surname"].ToString();
            //teacherVM.EmailAddress = dt.Rows[0]["EmailAddress"].ToString();
            //teacherVM.UserType = dt.Rows[0]["UserType"].ToString(); 
            //teacherVM.Status = dt.Rows[0]["UserStatus"].ToString();

            TempData["data"] = $"{data}";
            dbConn.Close();

           ViewData.Model = dt.AsEnumerable();
            return View();
            //return View(teacherVM);
        }
        // GET: StaffController/Edit/5
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

            SqlCommand dbComm = new SqlCommand("sp_GetTeacherByID", dbConn);
            dbComm.CommandType = CommandType.StoredProcedure;

            int teacherID = id;
            TempData["id"] = teacherID;
            dbComm.Parameters.AddWithValue("@TeacherID", teacherID);

            SqlDataAdapter dbAdapter = new SqlDataAdapter(dbComm);
            DataTable dt = new DataTable();
            dbAdapter.Fill(dt);
            EditTeacherDetailsVM editTeacher = new EditTeacherDetailsVM();

            string data = dt.Rows[0]["FirstName"].ToString();
            data = data + " " + dt.Rows[0]["Surname"].ToString();

            editTeacher.UserID = int.Parse(dt.Rows[0]["UserID"].ToString());
            editTeacher.FirstName = dt.Rows[0]["FirstName"].ToString();
            editTeacher.Surname = dt.Rows[0]["Surname"].ToString();
            editTeacher.EmailAddress = dt.Rows[0]["EmailAddress"].ToString();
            editTeacher.UserType = dt.Rows[0]["UserType"].ToString();
            editTeacher.Status = dt.Rows[0]["UserStatus"].ToString();

            TempData["data"] = $"{data}";
            dbConn.Close();

            return View(editTeacher);
        }

        // POST: StaffController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditTeacherDetailsVM model)
        {
            if (ModelState.IsValid)
            {
                string connString = configuration.GetConnectionString("connString");

                SqlConnection dbConn = new SqlConnection(connString);

                dbConn.Open();

                SqlCommand dbComm = new SqlCommand("sp_EditTeacherDetails", dbConn);
                dbComm.CommandType = CommandType.StoredProcedure;
                int id = int.Parse(TempData["id"].ToString());
                dbComm.Parameters.AddWithValue("@TeacherID", id);
                dbComm.Parameters.AddWithValue("@Surname", model.Surname);
                dbComm.Parameters.AddWithValue("@UserStatus", model.Status);

                int x = dbComm.ExecuteNonQuery();
                dbConn.Close();
                TempData["message"] = $"Successfully edited {model.Surname} details";
                return RedirectToAction("Index", "Staff", new { area = "Manager" });

            }
            else
            {
                return View(model);
            }
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
        public IActionResult Add(TeacherVM model)
        {
            if (ModelState.IsValid)
            {
                string connString = configuration.GetConnectionString("connString");

                SqlConnection dbConn = new SqlConnection(connString);

                dbConn.Open();

                SqlCommand dbComm = new SqlCommand("sp_AddTeacher", dbConn);
                dbComm.CommandType = CommandType.StoredProcedure;

                int cenId = (int)HttpContext.Session.GetInt32("CentreNo");
                model.CentreNo = cenId;
                string status = "Active";
                model.Status = status;
                model.Password = GeneratePassword();
                model.UserType = "Teacher";

                dbComm.Parameters.AddWithValue("@FirstName", model.FirstName);
                dbComm.Parameters.AddWithValue("@Surname", model.Surname);
                dbComm.Parameters.AddWithValue("@EmailAddress", model.EmailAddress);
                dbComm.Parameters.AddWithValue("@UserType", model.UserType);
                dbComm.Parameters.AddWithValue("@Password", model.Password);
                dbComm.Parameters.AddWithValue("@UserStatus", model.Status.ToString().ToLower() );
                dbComm.Parameters.AddWithValue("@IDNumber", model.IDNumber);
                dbComm.Parameters.AddWithValue("@CentreNo", model.CentreNo);

                int x = dbComm.ExecuteNonQuery();
                dbConn.Close();

                string centreEmail = HttpContext.Session.GetString("CentreEmailAddress");
                string cenEmailassword = HttpContext.Session.GetString("CentreEmailPassword");

                SendEmail(model.EmailAddress, model.Password, centreEmail, cenEmailassword);

                return RedirectToAction("Index", "Staff", new { area = "Manager" });
            }
            else
            {
                return View(model);
            }
        }
        public string GeneratePassword()
        {
            string values = "abcdefghijklmnopqrstuvwxyFGSKDSUW234789@#$%*()!><.,";
            Random random = new Random();
            string password = "";
            for (int x = 0; x <= 7; x++)
            {
                int i = random.Next(values.Length);
                password += values[i].ToString();
            }
            return password;
        }
        public void SendEmail(string teacherEmail, string teacherPassword, string centreEmail, string passsword)
        {
            string fromaddr = centreEmail;
            string toaddr = teacherEmail;//TO ADDRESS HERE
            string password = passsword;

            MailMessage msg = new MailMessage();
            msg.Subject = "Registration details";
            msg.From = new MailAddress(fromaddr);
            msg.Body = "This is to notify you that you have been suceesfully registered." +
                " \nHere are your ECDC MIS login details, You can change your password anytime\n\nUsername:\t" + toaddr
              + "\n\nPassword:\t" + teacherPassword+"\n\nECDC MIS\nThis email is sent from an unattended mailbox";
            msg.To.Add(new MailAddress(teacherEmail));
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";

            smtp.UseDefaultCredentials = false;

            NetworkCredential nc = new NetworkCredential(fromaddr, password);
            smtp.EnableSsl = true;
            smtp.Port = 587;
            smtp.Credentials = nc;
            smtp.Send(msg);
        }

    }
}
