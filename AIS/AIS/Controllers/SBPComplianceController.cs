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
                return View("../SBPCompliance/Index");
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
                return View("../SBPCompliance/AddObservation");
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
                return View("../SBPCompliance/AssignDivision");
        }

        [HttpGet("SBPCompliance/AssignDepartment")]
        public IActionResult AssignDepartment()
        {
            ViewData["TopMenu"] = tm.GetTopMenus();
            ViewData["TopMenuPages"] = tm.GetTopMenusPages();
            if (!sessionHandler.IsUserLoggedIn())
                return RedirectToAction("Index", "Login");
            else
                return View("../SBPCompliance/AssignDepartment");
        }

        [HttpGet("SBPCompliance/EnterResponse")]
        public IActionResult EnterResponse()
        {
            ViewData["TopMenu"] = tm.GetTopMenus();
            ViewData["TopMenuPages"] = tm.GetTopMenusPages();
            if (!sessionHandler.IsUserLoggedIn())
                return RedirectToAction("Index", "Login");
            else
                return View("../SBPCompliance/EnterResponse");
        }

        [HttpGet("SBPCompliance/ReviewResponse")]
        public IActionResult ReviewResponse()
        {
            ViewData["TopMenu"] = tm.GetTopMenus();
            ViewData["TopMenuPages"] = tm.GetTopMenusPages();
            if (!sessionHandler.IsUserLoggedIn())
                return RedirectToAction("Index", "Login");
            else
                return View("../SBPCompliance/ReviewResponse");
        }

        [HttpGet("SBPCompliance/ForwardToCompliance")]
        public IActionResult ForwardToCompliance()
        {
            ViewData["TopMenu"] = tm.GetTopMenus();
            ViewData["TopMenuPages"] = tm.GetTopMenusPages();
            if (!sessionHandler.IsUserLoggedIn())
                return RedirectToAction("Index", "Login");
            else
                return View("../SBPCompliance/ForwardToCompliance");
        }

        [HttpGet("SBPCompliance/ReviewHistory")]
        public IActionResult ReviewHistory()
        {
            ViewData["TopMenu"] = tm.GetTopMenus();
            ViewData["TopMenuPages"] = tm.GetTopMenusPages();
            if (!sessionHandler.IsUserLoggedIn())
                return RedirectToAction("Index", "Login");
            else
                return View("../SBPCompliance/ReviewHistory");
        }

        [HttpGet("SBPCompliance/AuditValidation/{observationId}")]
        public IActionResult AuditValidation(int observationId)
        {
            ViewData["TopMenu"] = tm.GetTopMenus();
            ViewData["TopMenuPages"] = tm.GetTopMenusPages();
            if (!sessionHandler.IsUserLoggedIn())
                return RedirectToAction("Index", "Login");
            else
                return View("../SBPCompliance/AuditValidation", new AuditValidationModel { ObservationId = observationId });
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
            return View("../SBPCompliance/ViewHistory", history);
        }

        }
    }

