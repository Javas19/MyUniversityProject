using DigeraitMIS.Data;
using DigeraitMIS.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace DigeraitMIS.Controllers
{
    public class ManagerProfileController : Controller
    {
        private readonly ApplicationDBContext _db;

        public ManagerProfileController(ApplicationDBContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            int? id = HttpContext.Session.GetInt32("managerID");
            IEnumerable<User> objList = _db.tblUser.Where(i => i.UserID == id);

            //IEnumerable<User> objList = _db.tblUser.Where(i => i.UserType == "Manager" && i.UserStatus == "active");
           // IEnumerable < Manager >manager=_db.tblManager.Where(x => x.ManagerID== )
            return View(objList);
        }


        //GET Update
        //public IActionResult Update(int? id)
        //{
        //    User user = new User();

        //    if (id == null || id == 0)
        //    {
        //        return NotFound();
        //    }

        //    IEnumerable<User> objList = _db.tblTransaction.Find(id);
        //    if (transactionVM.Transaction == null)
        //    {
        //        return NotFound();
        //    }
        //    transactionVM.Transaction.Date.ToString("yyyy/MM/dd");
        //    return View(transactionVM);
        //}

        //// POST Update
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Update(TransactionVM obj)
        //{

        //    if (ModelState.IsValid)
        //    {
        //        _db.tblTransaction.Update(obj.Transaction);
        //        _db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View();
        //}

        //public IActionResult CentreManagerHomeView()
        //{
        //    return View();
        //}










        //public IActionResult Create()
        //{
            
        //    var user = new User();
        //    if (ModelState.IsValid)
        //    {
        //        _db.tblUser.Add(user);
        //        if (user.UserType == "Manager")
        //        {
        //            var manager = new DigeraitMIS.Models.Manager { ManagerID = user.UserID };
        //            _db.tblManager.Add(manager);
        //            _db.SaveChanges();
        //        }
        //        return RedirectToAction("Index");
        //    }
        //    return View(user);
        //}

    }
}