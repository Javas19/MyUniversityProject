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
    public class TransactionController : Controller
    {
        private readonly IConfiguration configuration;

        public TransactionController(IConfiguration config)
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

            SqlCommand dbComm = new SqlCommand("sp_GetAllTransactionByID", dbConn);
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

        // GET: TransactionController/Create
        public ActionResult Add()
        {
            if (HttpContext.Session.GetInt32("managerID") == null)
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        // POST: TransactionController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(TransactionVM model)
        {
            if (ModelState.IsValid)
            {
                string connString = configuration.GetConnectionString("connString");

                SqlConnection dbConn = new SqlConnection(connString);

                dbConn.Open();

                SqlCommand dbComm = new SqlCommand("sp_AddTransaction", dbConn);
                dbComm.CommandType = CommandType.StoredProcedure;

                int cenId = (int)HttpContext.Session.GetInt32("CentreNo");
                model.CentreNo = cenId;
                   
                dbComm.Parameters.AddWithValue("@Name", model.Name);
                dbComm.Parameters.AddWithValue("@Date", model.Date);
                dbComm.Parameters.AddWithValue("@ReferenceNo", model.ReferenceNo);
                dbComm.Parameters.AddWithValue("@TransactionType", model.TransactionType);
                dbComm.Parameters.AddWithValue("@CentreNo", model.CentreNo);
                dbComm.Parameters.AddWithValue("@Amount", model.Amount);
                dbComm.Parameters.AddWithValue("@Description", model.Description);
                
                int x = dbComm.ExecuteNonQuery();
                dbConn.Close();

                return RedirectToAction("Index", "Transaction", new { area = "Manager" });
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

            SqlCommand dbComm = new SqlCommand("sp_GetTransactionByID", dbConn);
            dbComm.CommandType = CommandType.StoredProcedure;

            int transactionId = id;
            TempData["id"] = transactionId;
            dbComm.Parameters.AddWithValue("@TransactionID", transactionId);

            SqlDataAdapter dbAdapter = new SqlDataAdapter(dbComm);
            DataTable dt = new DataTable();
            dbAdapter.Fill(dt);
            EditTransactionVM transactionVM = new EditTransactionVM();

            string data = dt.Rows[0]["Name"].ToString();
            transactionVM.Name = dt.Rows[0]["Name"].ToString();
            transactionVM.TransactionType = dt.Rows[0]["TransactionType"].ToString();
            transactionVM.Amount = double.Parse(dt.Rows[0]["Amount"].ToString());
            transactionVM.Description = dt.Rows[0]["Description"].ToString();

            TempData["data"] = $"{data}";
            dbConn.Close();

            return View(transactionVM);
        }

        // POST: TransactionController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditTransactionVM model)
        {
            if (ModelState.IsValid)
            {
                string connString = configuration.GetConnectionString("connString");

                SqlConnection dbConn = new SqlConnection(connString);

                dbConn.Open();

                SqlCommand dbComm = new SqlCommand("sp_EditTransaction", dbConn);
                dbComm.CommandType = CommandType.StoredProcedure;
                int id = int.Parse(TempData["id"].ToString());
                dbComm.Parameters.AddWithValue("@TransactionID", id);
                dbComm.Parameters.AddWithValue("@Name", model.Name);
                dbComm.Parameters.AddWithValue("@TransactionType", model.TransactionType);
                dbComm.Parameters.AddWithValue("@Amount", model.Amount);
                dbComm.Parameters.AddWithValue("@Description", model.Description);

                int x = dbComm.ExecuteNonQuery();
                dbConn.Close();
                TempData["message"] = $"Successfully edited {model.Name} transaction";
                return RedirectToAction("Index", "Transaction", new { area = "Manager" });

            }
            else
            {
                return View(model);
            }           
        }

        // GET: TransactionController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TransactionController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
