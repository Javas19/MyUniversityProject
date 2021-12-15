using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigeraitMIS.Controllers.Provincial
{
    public class ProvincialController : Controller
    {
        public IActionResult Welcome()
        {
            return View();
        }
    }
}
