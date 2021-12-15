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
    public class StaffController : Controller
    {
        private readonly ApplicationDBContext _db;

        public StaffController(ApplicationDBContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<User> objList = _db.tblUser.Where(t => t.UserType == "Teacher");
            return View(objList);
           // return View();
        }

        //GET Update
        public IActionResult Update(int? id)
        {
            ManageStaffVM staffVM = new ManageStaffVM()
            {
            };

            if (id == null || id == 0)
            {
                return NotFound();
            }
            staffVM.User = _db.tblUser.Find(id);
            if (staffVM.User == null)
            {
                return NotFound();
            }
            return View(staffVM);
        }

        // POST Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(ManageStaffVM obj)
        {
            if (ModelState.IsValid)
            {
                _db.tblUser.Update(obj.User);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
