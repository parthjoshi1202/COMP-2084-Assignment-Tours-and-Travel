using System;
using System.Collections.Generic;
using System.Linq;
using ToursandTravel.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToursandTravel.Data;

namespace ToursandTravel.Controllers
{
    public class TypeController : Controller
    {
        private readonly ApplicationDbContext _context;
        public TypeController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            //var categories = _context.PackageTypes.ToList();
            return View();
        }

        // /Type/AddType
        public IActionResult AddType()
        {
            return View();
        }
    }
}
