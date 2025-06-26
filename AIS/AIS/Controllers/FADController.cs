using AIS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Reflection;

namespace AIS.Controllers
    {

    public class FADController : Controller
        {
        private readonly ILogger<FADController> _logger;
        private readonly TopMenus tm;
        private readonly SessionHandler sessionHandler;
        private readonly DBConnection dBConnection;
        public FADController(ILogger<FADController> logger, SessionHandler _sessionHandler, DBConnection _dbCon, TopMenus _tpMenu)
            {
            _logger = logger;
            sessionHandler = _sessionHandler;
            dBConnection = _dbCon;
            tm = _tpMenu;
            }

        public IActionResult observation_review()
        {
            ViewData["TopMenu"] = tm.GetTopMenus();
            ViewData["TopMenuPages"] = tm.GetTopMenusPages();
            ViewData["Userrelationship"] = dBConnection.Getrealtionshiptype();
            ViewData["statusList"] = dBConnection.GetObservationReversalStatus();

            if (!sessionHandler.IsUserLoggedIn())
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                if (!sessionHandler.HasPermissionToViewPage(MethodBase.GetCurrentMethod().Name))
                {
                    return RedirectToAction("Index", "PageNotFound");
                }
                else
                    return View("~/Views/FAD/observation_review.cshtml");
            }
        }
        public IActionResult review_gist_recommendation()
        {
            ViewData["TopMenu"] = tm.GetTopMenus();
            ViewData["TopMenuPages"] = tm.GetTopMenusPages();
            ViewData["ZonesList"] = dBConnection.GetZonesoldparamointoring();
            if (!sessionHandler.IsUserLoggedIn())
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                if (!sessionHandler.HasPermissionToViewPage(MethodBase.GetCurrentMethod().Name))
                {
                    return RedirectToAction("Index", "PageNotFound");
                }
                else
                    return View("~/Views/FAD/review_gist_recommendation.cshtml");
            }
        }
        public IActionResult Para_shifting()
        {
            ViewData["TopMenu"] = tm.GetTopMenus();
            ViewData["TopMenuPages"] = tm.GetTopMenusPages();
            ViewData["Userrelationship"] = dBConnection.Getrealtionshiptype();
            ViewData["ZonesList"] = dBConnection.GetZonesoldparamointoring();

            if (!sessionHandler.IsUserLoggedIn())
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                if (!sessionHandler.HasPermissionToViewPage(MethodBase.GetCurrentMethod().Name))
                {
                    return RedirectToAction("Index", "PageNotFound");
                }
                else
                    return View("~/Views/FAD/para_shifting.cshtml");
            }
        }

        public IActionResult staff_position()
        {
            ViewData["TopMenu"] = tm.GetTopMenus();
            ViewData["TopMenuPages"] = tm.GetTopMenusPages();
            if (!sessionHandler.IsUserLoggedIn())
                return RedirectToAction("Index", "Login");
            else
            {
                if (!sessionHandler.HasPermissionToViewPage(MethodBase.GetCurrentMethod().Name))
                    return RedirectToAction("Index", "PageNotFound");
                else
                    return View("~/Views/FAD/staff_position.cshtml");
            }
        }

        public IActionResult manpower_position()
        {
            ViewData["TopMenu"] = tm.GetTopMenus();
            ViewData["TopMenuPages"] = tm.GetTopMenusPages();
            if (!sessionHandler.IsUserLoggedIn())
                return RedirectToAction("Index", "Login");
            else
            {
                if (!sessionHandler.HasPermissionToViewPage(MethodBase.GetCurrentMethod().Name))
                    return RedirectToAction("Index", "PageNotFound");
                else
                    return View("~/Views/FAD/manpower_position.cshtml");
            }
        }
        

        public IActionResult risk_register()
        {
            ViewData["TopMenu"] = tm.GetTopMenus();
            ViewData["TopMenuPages"] = tm.GetTopMenusPages();
            if (!sessionHandler.IsUserLoggedIn())
                return RedirectToAction("Index", "Login");
            else
            {
                if (!sessionHandler.HasPermissionToViewPage(MethodBase.GetCurrentMethod().Name))
                    return RedirectToAction("Index", "PageNotFound");
                else
                    return View("~/Views/FAD/risk_register.cshtml");
            }
        }

        [HttpGet("SBPCompliance")]
        public IActionResult Index()
        {
            ViewData["TopMenu"] = tm.GetTopMenus();
            ViewData["TopMenuPages"] = tm.GetTopMenusPages();
            if (!sessionHandler.IsUserLoggedIn())
                return RedirectToAction("Index", "Login");
            else
                return View("~/Views/FAD/Index.cshtml");
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
                return View("~/Views/FAD/AddObservation.cshtml");
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
                return View("~/Views/FAD/AssignDivision.cshtml");
        }

        [HttpGet("SBPCompliance/AssignDepartment")]
        public IActionResult AssignDepartment()
        {
            ViewData["TopMenu"] = tm.GetTopMenus();
            ViewData["TopMenuPages"] = tm.GetTopMenusPages();
            if (!sessionHandler.IsUserLoggedIn())
                return RedirectToAction("Index", "Login");
            else
                return View("~/Views/FAD/AssignDepartment.cshtml");
        }

        [HttpGet("SBPCompliance/EnterResponse")]
        public IActionResult EnterResponse()
        {
            ViewData["TopMenu"] = tm.GetTopMenus();
            ViewData["TopMenuPages"] = tm.GetTopMenusPages();
            if (!sessionHandler.IsUserLoggedIn())
                return RedirectToAction("Index", "Login");
            else
                return View("~/Views/FAD/EnterResponse.cshtml");
        }

        [HttpGet("SBPCompliance/ReviewResponse")]
        public IActionResult ReviewResponse()
        {
            ViewData["TopMenu"] = tm.GetTopMenus();
            ViewData["TopMenuPages"] = tm.GetTopMenusPages();
            if (!sessionHandler.IsUserLoggedIn())
                return RedirectToAction("Index", "Login");
            else
                return View("~/Views/FAD/ReviewResponse.cshtml");
        }

        [HttpGet("SBPCompliance/ForwardToCompliance")]
        public IActionResult ForwardToCompliance()
        {
            ViewData["TopMenu"] = tm.GetTopMenus();
            ViewData["TopMenuPages"] = tm.GetTopMenusPages();
            if (!sessionHandler.IsUserLoggedIn())
                return RedirectToAction("Index", "Login");
            else
                return View("~/Views/FAD/ForwardToCompliance.cshtml");
        }

        [HttpGet("SBPCompliance/ReviewHistory")]
        public IActionResult ReviewHistory()
        {
            ViewData["TopMenu"] = tm.GetTopMenus();
            ViewData["TopMenuPages"] = tm.GetTopMenusPages();
            if (!sessionHandler.IsUserLoggedIn())
                return RedirectToAction("Index", "Login");
            else
                return View("~/Views/FAD/ReviewHistory.cshtml");
        }

        [HttpGet("SBPCompliance/AuditValidation/{observationId}")]
        public IActionResult AuditValidation(int observationId)
        {
            ViewData["TopMenu"] = tm.GetTopMenus();
            ViewData["TopMenuPages"] = tm.GetTopMenusPages();
            if (!sessionHandler.IsUserLoggedIn())
                return RedirectToAction("Index", "Login");
            else
                return View("~/Views/FAD/AuditValidation.cshtml", new AuditValidationModel { ObservationId = observationId });
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
            return View("~/Views/FAD/ViewHistory.cshtml", history);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult financial_budget()
        {
            ViewData["TopMenu"] = tm.GetTopMenus();
            ViewData["TopMenuPages"] = tm.GetTopMenusPages();
            if (!sessionHandler.IsUserLoggedIn())
                return RedirectToAction("Index", "Login");
            else
            {
                if (!sessionHandler.HasPermissionToViewPage(MethodBase.GetCurrentMethod().Name))
                    return RedirectToAction("Index", "PageNotFound");
                else
                    return View("~/Views/FAD/financial_budget.cshtml");
            }
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
            {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }
    }
