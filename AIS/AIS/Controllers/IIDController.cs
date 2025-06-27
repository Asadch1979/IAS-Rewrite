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

        [HttpGet("iid/submit-complaint")]
        public IActionResult SubmitComplaint()
        {
            ViewData["TopMenu"] = tm.GetTopMenus();
            ViewData["TopMenuPages"] = tm.GetTopMenusPages();
            if (!sessionHandler.IsUserLoggedIn())
                return RedirectToAction("Index", "Login");
            return View("../IID/SubmitComplaint");
        }

        [HttpGet("iid/assessment/{complaintId}")]
        public IActionResult Assessment(int complaintId)
        {
            ViewData["TopMenu"] = tm.GetTopMenus();
            ViewData["TopMenuPages"] = tm.GetTopMenusPages();
            if (!sessionHandler.IsUserLoggedIn())
                return RedirectToAction("Index", "Login");
            ViewData["ComplaintId"] = complaintId;
            return View("../IID/InitialAssessment");
        }

        [HttpGet("iid/head-review/{complaintId}")]
        public IActionResult HeadReview(int complaintId)
        {
            ViewData["TopMenu"] = tm.GetTopMenus();
            ViewData["TopMenuPages"] = tm.GetTopMenusPages();
            if (!sessionHandler.IsUserLoggedIn())
                return RedirectToAction("Index", "Login");
            ViewData["ComplaintId"] = complaintId;
            return View("../IID/HeadReview");
        }

        [HttpGet("iid/inv-plan/{complaintId}")]
        public IActionResult InvestigationPlan(int complaintId)
        {
            ViewData["TopMenu"] = tm.GetTopMenus();
            ViewData["TopMenuPages"] = tm.GetTopMenusPages();
            if (!sessionHandler.IsUserLoggedIn())
                return RedirectToAction("Index", "Login");
            ViewData["ComplaintId"] = complaintId;
            return View("../IID/InvestigationPlan");
        }

        [HttpGet("iid/plan-approval/{planId}")]
        public IActionResult PlanApproval(int planId)
        {
            ViewData["TopMenu"] = tm.GetTopMenus();
            ViewData["TopMenuPages"] = tm.GetTopMenusPages();
            if (!sessionHandler.IsUserLoggedIn())
                return RedirectToAction("Index", "Login");
            ViewData["PlanId"] = planId;
            return View("../IID/PlanApproval");
        }

        [HttpGet("iid/inquiry-report/{complaintId}")]
        public IActionResult InquiryReport(int complaintId)
        {
            ViewData["TopMenu"] = tm.GetTopMenus();
            ViewData["TopMenuPages"] = tm.GetTopMenusPages();
            if (!sessionHandler.IsUserLoggedIn())
                return RedirectToAction("Index", "Login");
            ViewData["ComplaintId"] = complaintId;
            return View("../IID/InquiryReport");
        }

        [HttpGet("iid/analysis/{reportId}")]
        public IActionResult Analysis(int reportId)
        {
            ViewData["TopMenu"] = tm.GetTopMenus();
            ViewData["TopMenuPages"] = tm.GetTopMenusPages();
            if (!sessionHandler.IsUserLoggedIn())
                return RedirectToAction("Index", "Login");
            ViewData["ReportId"] = reportId;
            return View("../IID/Analysis");
        }

        [HttpGet("iid/final-approval/{reportId}")]
        public IActionResult FinalApproval(int reportId)
        {
            ViewData["TopMenu"] = tm.GetTopMenus();
            ViewData["TopMenuPages"] = tm.GetTopMenusPages();
            if (!sessionHandler.IsUserLoggedIn())
                return RedirectToAction("Index", "Login");
            ViewData["ReportId"] = reportId;
            return View("../IID/FinalApproval");
        }

        [HttpGet("iid/case-study/{complaintId}")]
        public IActionResult CaseStudy(int complaintId)
        {
            ViewData["TopMenu"] = tm.GetTopMenus();
            ViewData["TopMenuPages"] = tm.GetTopMenusPages();
            if (!sessionHandler.IsUserLoggedIn())
                return RedirectToAction("Index", "Login");
            ViewData["ComplaintId"] = complaintId;
            return View("../IID/CaseStudy");
        }

        [HttpGet("iid/reports")]
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
