using System.Linq;
using Microsoft.AspNetCore.Mvc;
using AIS.Models;

namespace AIS.Controllers
{
    public class StaffPositionController : Controller
    {
        private readonly DBContext _context;

        public StaffPositionController(DBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var list = _context.StaffPositions.ToList();
            return View("~/Views/FAD/StaffPositionIndex.cshtml", list);
        }

        public IActionResult Create()
        {
            return View("~/Views/FAD/StaffPositionCreate.cshtml");
        }

        [HttpPost]
        public IActionResult Create(StaffPosition model)
        {
            if(ModelState.IsValid)
            {
                _context.StaffPositions.Add(model);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("~/Views/FAD/StaffPositionCreate.cshtml", model);
        }
    }
}
