using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigeraitMIS.Controllers.Manager
{
    public class ManagerController : Controller
    {
        public IActionResult Welcome()
        {
            return View();
        }
    }
}
