using DigeraitMIS.Data;
using DigeraitMIS.Models;
using DigeraitMIS.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DigeraitMIS.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ApplicationDBContext _db;
        public TransactionController(ApplicationDBContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Transaction> objList = _db.tblTransaction.OrderBy(t => t.Date);
            return View(objList);
        }

        //GET- Create
        public IActionResult Create()
        {
            TransactionVM transactionVM = new TransactionVM()
            {
                Transaction = new Transaction(),
                TypeDropDown = _db.tblCentre.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.CentreNo.ToString()
                })
            };
            transactionVM.Transaction.Date.ToString("yyyy/MM/dd");
            return View(transactionVM);
        }
        // POST-Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TransactionVM obj)
        {
            if (ModelState.IsValid)
            {
                _db.tblTransaction.Add(obj.Transaction);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET Update
        public IActionResult Update(int? id)
        {
            TransactionVM transactionVM = new TransactionVM()
            {
                Transaction = new Transaction(),
                TypeDropDown = _db.tblCentre.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.CentreNo.ToString()
                })
            };

            if (id == null || id == 0)
            {
                return NotFound();
            }

            transactionVM.Transaction = _db.tblTransaction.Find(id);
            if (transactionVM.Transaction == null)
            {
                return NotFound();
            }
            transactionVM.Transaction.Date.ToString("yyyy/MM/dd");
            return View(transactionVM);
        }

        // POST Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(TransactionVM obj)
        {

            if (ModelState.IsValid)
            {
                _db.tblTransaction.Update(obj.Transaction);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult CentreManagerHomeView()
        {
            return View();
        }

    }
}
