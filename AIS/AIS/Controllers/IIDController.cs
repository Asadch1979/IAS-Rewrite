using AIS.Models.IID;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace AIS.Controllers
{
    public class IIDController : Controller
    {
        private readonly ILogger<IIDController> _logger;
        private readonly TopMenus tm;
        private readonly SessionHandler sessionHandler;
        private readonly DBConnection dBConnection;
        public IIDController(ILogger<IIDController> logger, SessionHandler _sessionHandler, DBConnection _dbCon, TopMenus _tpMenu)
        {
            _logger = logger;
            sessionHandler = _sessionHandler;
            dBConnection = _dbCon;
            tm = _tpMenu;
        }

        [HttpPost]
        public IActionResult SubmitComplaint()
        {
            ViewData["TopMenu"] = tm.GetTopMenus();
            ViewData["TopMenuPages"] = tm.GetTopMenusPages();
            if (!sessionHandler.IsUserLoggedIn())
                return RedirectToAction("Index", "Login");
            return View("../IID/SubmitComplaint");
        }

        [HttpPost]
        public IActionResult Assessment(int complaintId)
        {
            ViewData["TopMenu"] = tm.GetTopMenus();
            ViewData["TopMenuPages"] = tm.GetTopMenusPages();
            if (!sessionHandler.IsUserLoggedIn())
                return RedirectToAction("Index", "Login");
            ViewData["ComplaintId"] = complaintId;
            return View("../IID/InitialAssessment");
        }

        [HttpPost]
        public IActionResult HeadReview(int complaintId)
        {
            ViewData["TopMenu"] = tm.GetTopMenus();
            ViewData["TopMenuPages"] = tm.GetTopMenusPages();
            if (!sessionHandler.IsUserLoggedIn())
                return RedirectToAction("Index", "Login");
            ViewData["ComplaintId"] = complaintId;
            return View("../IID/HeadReview");
        }

        [HttpPost]
        public IActionResult InvestigationPlan(int complaintId)
        {
            ViewData["TopMenu"] = tm.GetTopMenus();
            ViewData["TopMenuPages"] = tm.GetTopMenusPages();
            if (!sessionHandler.IsUserLoggedIn())
                return RedirectToAction("Index", "Login");
            ViewData["ComplaintId"] = complaintId;
            return View("../IID/InvestigationPlan");
        }

        [HttpPost]
        public IActionResult PlanApproval(int planId)
        {
            ViewData["TopMenu"] = tm.GetTopMenus();
            ViewData["TopMenuPages"] = tm.GetTopMenusPages();
            if (!sessionHandler.IsUserLoggedIn())
                return RedirectToAction("Index", "Login");
            ViewData["PlanId"] = planId;
            return View("../IID/PlanApproval");
        }

        [HttpPost]
        public IActionResult InquiryReport(int complaintId)
        {
            ViewData["TopMenu"] = tm.GetTopMenus();
            ViewData["TopMenuPages"] = tm.GetTopMenusPages();
            if (!sessionHandler.IsUserLoggedIn())
                return RedirectToAction("Index", "Login");
            ViewData["ComplaintId"] = complaintId;
            return View("../IID/InquiryReport");
        }

        [HttpPost]
        public IActionResult Analysis(int reportId)
        {
            ViewData["TopMenu"] = tm.GetTopMenus();
            ViewData["TopMenuPages"] = tm.GetTopMenusPages();
            if (!sessionHandler.IsUserLoggedIn())
                return RedirectToAction("Index", "Login");
            ViewData["ReportId"] = reportId;
            return View("../IID/Analysis");
        }

        [HttpPost]
        public IActionResult FinalApproval(int reportId)
        {
            ViewData["TopMenu"] = tm.GetTopMenus();
            ViewData["TopMenuPages"] = tm.GetTopMenusPages();
            if (!sessionHandler.IsUserLoggedIn())
                return RedirectToAction("Index", "Login");
            ViewData["ReportId"] = reportId;
            return View("../IID/FinalApproval");
        }

        [HttpPost]
        public IActionResult CaseStudy(int complaintId)
        {
            ViewData["TopMenu"] = tm.GetTopMenus();
            ViewData["TopMenuPages"] = tm.GetTopMenusPages();
            if (!sessionHandler.IsUserLoggedIn())
                return RedirectToAction("Index", "Login");
            ViewData["ComplaintId"] = complaintId;
            return View("../IID/CaseStudy");
        }

        [HttpPost]
        public IActionResult Reports()
        {
            ViewData["TopMenu"] = tm.GetTopMenus();
            ViewData["TopMenuPages"] = tm.GetTopMenusPages();
            if (!sessionHandler.IsUserLoggedIn())
                return RedirectToAction("Index", "Login");
            return View("../IID/Reports");
        }
    }
}
