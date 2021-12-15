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
    public class CentreProfileController : Controller
    {
        private readonly ApplicationDBContext _db;

        public CentreProfileController(ApplicationDBContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            // var centreProfile = _db.tblCentre.FromSqlRaw("spCentre").ToList();
            IEnumerable<CentreProfile> centre = _db.tblCentre.ToList();
            return View(centre);
        }
        //GET Update
        public IActionResult Update(int? id)
        {
            //var user = _db.tblUser.ToList();
            CentreProfileVM centreProfileVM = new CentreProfileVM()
            {
                CentreProfile = new CentreProfile(),
                TypeDropDown = _db.tblRegion.Select(i => new SelectListItem
                {
                    Text = i.RegionName,
                    Value = i.RegionID.ToString()
                }),
                //TypeDropDown1 =_db.tblManager.Where(t => t.ManagerID == user.).Select(i => new SelectListItem
                //{
                //    Text =i.Users.FirstName,
                //    Value =i.ManagerID.ToString()
                //})
            };
            //Where(t => t.date)
            if (id == null || id == 0)
            {
                return NotFound();
            }

            centreProfileVM.CentreProfile = _db.tblCentre.Find(id);
            if (centreProfileVM.CentreProfile == null)
            {
                return NotFound();
            }
            return View(centreProfileVM);
        }

        // POST Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(CentreProfileVM obj)
        {
            if (ModelState.IsValid)
            {
                _db.tblCentre.Update(obj.CentreProfile);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

    }
}
