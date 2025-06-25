using AIS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AIS.Controllers.Compliance
{
    public class SBPComplianceController : Controller
    {
        private readonly ILogger<SBPComplianceController> _logger;
        private readonly TopMenus tm;
        private readonly SessionHandler sessionHandler;
        private readonly DBConnection dBConnection;

        public SBPComplianceController(ILogger<SBPComplianceController> logger,
            SessionHandler _sessionHandler, DBConnection _dbCon, TopMenus _tpMenu)
        {
            _logger = logger;
            sessionHandler = _sessionHandler;
            dBConnection = _dbCon;
            tm = _tpMenu;
        }

        [HttpGet("Complaince/SBPCompliance")]
        public IActionResult Index()
        {
            ViewData["TopMenu"] = tm.GetTopMenus();
            ViewData["TopMenuPages"] = tm.GetTopMenusPages();
            if (!sessionHandler.IsUserLoggedIn())
                return RedirectToAction("Index", "Login");
            else
                return View("../Complaince/SBPCompliance/Index");
        }

        [HttpGet("Complaince/SBPCompliance/AddObservation")]
        public IActionResult AddObservation()
        {
            ViewData["TopMenu"] = tm.GetTopMenus();
            ViewData["TopMenuPages"] = tm.GetTopMenusPages();
            ViewData["DivisionList"] = dBConnection.GetDivisions(false);
            if (!sessionHandler.IsUserLoggedIn())
                return RedirectToAction("Index", "Login");
            else
                return View("../Complaince/SBPCompliance/AddObservation");
        }

        [HttpGet("Complaince/SBPCompliance/AssignDivision")]
        public IActionResult AssignDivision()
        {
            ViewData["TopMenu"] = tm.GetTopMenus();
            ViewData["TopMenuPages"] = tm.GetTopMenusPages();
            if (!sessionHandler.IsUserLoggedIn())
                return RedirectToAction("Index", "Login");
            else
                return View("../Complaince/SBPCompliance/AssignDivision");
        }

        [HttpGet("Complaince/SBPCompliance/AssignDepartment")]
        public IActionResult AssignDepartment()
        {
            ViewData["TopMenu"] = tm.GetTopMenus();
            ViewData["TopMenuPages"] = tm.GetTopMenusPages();
            if (!sessionHandler.IsUserLoggedIn())
                return RedirectToAction("Index", "Login");
            else
                return View("../Complaince/SBPCompliance/AssignDepartment");
        }

        [HttpGet("Complaince/SBPCompliance/EnterResponse")]
        public IActionResult EnterResponse()
        {
            ViewData["TopMenu"] = tm.GetTopMenus();
            ViewData["TopMenuPages"] = tm.GetTopMenusPages();
            if (!sessionHandler.IsUserLoggedIn())
                return RedirectToAction("Index", "Login");
            else
                return View("../Complaince/SBPCompliance/EnterResponse");
        }

        [HttpGet("Complaince/SBPCompliance/ReviewResponse")]
        public IActionResult ReviewResponse()
        {
            ViewData["TopMenu"] = tm.GetTopMenus();
            ViewData["TopMenuPages"] = tm.GetTopMenusPages();
            if (!sessionHandler.IsUserLoggedIn())
                return RedirectToAction("Index", "Login");
            else
                return View("../Complaince/SBPCompliance/ReviewResponse");
        }

        [HttpGet("Complaince/SBPCompliance/ForwardToCompliance")]
        public IActionResult ForwardToCompliance()
        {
            ViewData["TopMenu"] = tm.GetTopMenus();
            ViewData["TopMenuPages"] = tm.GetTopMenusPages();
            if (!sessionHandler.IsUserLoggedIn())
                return RedirectToAction("Index", "Login");
            else
                return View("../Complaince/SBPCompliance/ForwardToCompliance");
        }

        [HttpGet("Complaince/SBPCompliance/ReviewHistory")]
        public IActionResult ReviewHistory()
        {
            ViewData["TopMenu"] = tm.GetTopMenus();
            ViewData["TopMenuPages"] = tm.GetTopMenusPages();
            if (!sessionHandler.IsUserLoggedIn())
                return RedirectToAction("Index", "Login");
            else
                return View("../Complaince/SBPCompliance/ReviewHistory");
        }

        [HttpGet("Complaince/SBPCompliance/AuditValidation/{observationId}")]
        public IActionResult AuditValidation(int observationId)
        {
            ViewData["TopMenu"] = tm.GetTopMenus();
            ViewData["TopMenuPages"] = tm.GetTopMenusPages();
            if (!sessionHandler.IsUserLoggedIn())
                return RedirectToAction("Index", "Login");
            else
                return View("../Complaince/SBPCompliance/AuditValidation", new AuditValidationModel { ObservationId = observationId });
        }

        [HttpPost("Complaince/SBPCompliance/AuditValidation")]
        public IActionResult AuditValidation(AuditValidationModel model)
        {
            ViewData["TopMenu"] = tm.GetTopMenus();
            ViewData["TopMenuPages"] = tm.GetTopMenusPages();
            if (!sessionHandler.IsUserLoggedIn())
                return RedirectToAction("Index", "Login");

            dBConnection.ProcessSBPAuditValidation(model.ObservationId, model.Action, model.Remarks);
            return RedirectToAction("ReviewHistory");
        }
        [HttpGet("Complaince/SBPCompliance/ViewHistory/{observationId}")]
        public IActionResult ViewHistory(int observationId)
        {
            ViewData["TopMenu"] = tm.GetTopMenus();
            ViewData["TopMenuPages"] = tm.GetTopMenusPages();
            if (!sessionHandler.IsUserLoggedIn())
                return RedirectToAction("Index", "Login");
            var history = dBConnection.GetSBPReviewHistory(observationId);
            return View("../Complaince/SBPCompliance/ViewHistory", history);
        }

        }
    }
}
