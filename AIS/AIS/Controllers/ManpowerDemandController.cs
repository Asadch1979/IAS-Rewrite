using System.Linq;
using Microsoft.AspNetCore.Mvc;
using AIS.Models;

namespace AIS.Controllers
{
    public class ManpowerDemandController : Controller
    {
        private readonly DBContext _context;

        public ManpowerDemandController(DBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var list = _context.ManpowerDemands.ToList();
            return View("~/Views/FAD/ManpowerDemandIndex.cshtml", list);
        }

        public IActionResult Create()
        {
            var model = new ManpowerDemand { SubmittedOn = System.DateTime.Now };
            return View("~/Views/FAD/ManpowerDemandCreate.cshtml", model);
        }

        [HttpPost]
        public IActionResult Create(ManpowerDemand model)
        {
            if(ModelState.IsValid)
            {
                model.SubmittedOn = System.DateTime.Now;
                _context.ManpowerDemands.Add(model);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("~/Views/FAD/ManpowerDemandCreate.cshtml", model);
        }

        public IActionResult Review(int id)
        {
            var demand = _context.ManpowerDemands.FirstOrDefault(m => m.Id == id);
            if(demand == null) return NotFound();
            return View("~/Views/FAD/ManpowerDemandReview.cshtml", demand);
        }

        [HttpPost]
        public IActionResult Review(ManpowerDemand model)
        {
            if(ModelState.IsValid)
            {
                model.LastUpdatedOn = System.DateTime.Now;
                _context.ManpowerDemands.Update(model);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("~/Views/FAD/ManpowerDemandReview.cshtml", model);
        }
    }
}
