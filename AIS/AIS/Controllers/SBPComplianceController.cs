using AIS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace AIS.Controllers
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

        [HttpGet("SBPCompliance")]
        public IActionResult Index()
        {
            ViewData["TopMenu"] = tm.GetTopMenus();
            ViewData["TopMenuPages"] = tm.GetTopMenusPages();
            if (!sessionHandler.IsUserLoggedIn())
                return RedirectToAction("Index", "Login");
            else
                return View("~/Views/Complaince/Index.cshtml");
        }

        [HttpGet("SBPCompliance/AddObservation")]
        public IActionResult AddObservation()
        {
            ViewData["TopMenu"] = tm.GetTopMenus();
            ViewData["TopMenuPages"] = tm.GetTopMenusPages();
            ViewData["DivisionList"] = dBConnection.GetDivisions(false);
            if (!sessionHandler.IsUserLoggedIn())
                return RedirectToAction("Index", "Login");
            else
                return View("~/Views/Complaince/AddObservation.cshtml");
        }

        [HttpGet("SBPCompliance/AssignDivision")]
        public IActionResult AssignDivision()
        {
            ViewData["TopMenu"] = tm.GetTopMenus();
            ViewData["TopMenuPages"] = tm.GetTopMenusPages();
            ViewData["DivisionList"] = dBConnection.GetDivisions(false);
            if (!sessionHandler.IsUserLoggedIn())
                return RedirectToAction("Index", "Login");
            else
                return View("~/Views/Complaince/AssignDivision.cshtml");
        }

        [HttpGet("SBPCompliance/AssignDepartment")]
        public IActionResult AssignDepartment()
        {
            ViewData["TopMenu"] = tm.GetTopMenus();
            ViewData["TopMenuPages"] = tm.GetTopMenusPages();
            if (!sessionHandler.IsUserLoggedIn())
                return RedirectToAction("Index", "Login");
            else
                return View("~/Views/Complaince/AssignDepartment.cshtml");
        }

        [HttpGet("SBPCompliance/EnterResponse")]
        public IActionResult EnterResponse()
        {
            ViewData["TopMenu"] = tm.GetTopMenus();
            ViewData["TopMenuPages"] = tm.GetTopMenusPages();
            if (!sessionHandler.IsUserLoggedIn())
                return RedirectToAction("Index", "Login");
            else
                return View("~/Views/Complaince/EnterResponse.cshtml");
        }

        [HttpGet("SBPCompliance/ReviewResponse")]
        public IActionResult ReviewResponse()
        {
            ViewData["TopMenu"] = tm.GetTopMenus();
            ViewData["TopMenuPages"] = tm.GetTopMenusPages();
            if (!sessionHandler.IsUserLoggedIn())
                return RedirectToAction("Index", "Login");
            else
                return View("~/Views/Complaince/ReviewResponse.cshtml");
        }

        [HttpGet("SBPCompliance/ForwardToCompliance")]
        public IActionResult ForwardToCompliance()
        {
            ViewData["TopMenu"] = tm.GetTopMenus();
            ViewData["TopMenuPages"] = tm.GetTopMenusPages();
            if (!sessionHandler.IsUserLoggedIn())
                return RedirectToAction("Index", "Login");
            else
                return View("~/Views/Complaince/ForwardToCompliance.cshtml");
        }

        [HttpGet("SBPCompliance/ReviewHistory")]
        public IActionResult ReviewHistory()
        {
            ViewData["TopMenu"] = tm.GetTopMenus();
            ViewData["TopMenuPages"] = tm.GetTopMenusPages();
            if (!sessionHandler.IsUserLoggedIn())
                return RedirectToAction("Index", "Login");
            else
                return View("~/Views/Complaince/ReviewHistory.cshtml");
        }

        [HttpGet("SBPCompliance/AuditValidation/{observationId}")]
        public IActionResult AuditValidation(int observationId)
        {
            ViewData["TopMenu"] = tm.GetTopMenus();
            ViewData["TopMenuPages"] = tm.GetTopMenusPages();
            if (!sessionHandler.IsUserLoggedIn())
                return RedirectToAction("Index", "Login");
            else
                return View("~/Views/Complaince/AuditValidation.cshtml", new AuditValidationModel { ObservationId = observationId });
        }

        [HttpPost("SBPCompliance/AuditValidation")]
        public IActionResult AuditValidation(AuditValidationModel model)
        {
            ViewData["TopMenu"] = tm.GetTopMenus();
            ViewData["TopMenuPages"] = tm.GetTopMenusPages();
            if (!sessionHandler.IsUserLoggedIn())
                return RedirectToAction("Index", "Login");

            dBConnection.ProcessSBPAuditValidation(model.ObservationId, model.Action, model.Remarks);
            return RedirectToAction("ReviewHistory");
        }
        [HttpGet("SBPCompliance/ViewHistory/{observationId}")]
        public IActionResult ViewHistory(int observationId)
        {
            ViewData["TopMenu"] = tm.GetTopMenus();
            ViewData["TopMenuPages"] = tm.GetTopMenusPages();
            if (!sessionHandler.IsUserLoggedIn())
                return RedirectToAction("Index", "Login");
            var history = dBConnection.GetSBPReviewHistory(observationId);
            return View("~/Views/Complaince/ViewHistory.cshtml", history);
        }

        }
    }

