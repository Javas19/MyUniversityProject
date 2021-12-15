using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigeraitMIS.Controllers.Teacher
{
    public class TeacherController : Controller
    {
        public IActionResult Welcome()
        {
            return View();
        }
    }
}
