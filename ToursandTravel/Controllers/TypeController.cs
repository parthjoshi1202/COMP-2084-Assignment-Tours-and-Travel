using System;
using System.Collections.Generic;
using System.Linq;
using ToursandTravel.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ToursandTravel.Controllers
{
    public class TypeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        // /Type/AddType
        public IActionResult AddType()
        {
            return View();
        }
    }
}
