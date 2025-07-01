using AIS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace AIS.Controllers.CA
{
    public class AuditObservationController : Controller
    {
        private readonly ILogger<AuditObservationController> _logger;
        private readonly TopMenus tm;
        private readonly SessionHandler sessionHandler;
        private readonly DBConnection dBConnection;

        public AuditObservationController(ILogger<AuditObservationController> logger, SessionHandler _sessionHandler, DBConnection _dbCon, TopMenus _tpMenu)
        {
            _logger = logger;
            sessionHandler = _sessionHandler;
            dBConnection = _dbCon;
            tm = _tpMenu;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewData["TopMenu"] = tm.GetTopMenus();
            ViewData["TopMenuPages"] = tm.GetTopMenusPages();

            if (!sessionHandler.IsUserLoggedIn())
                return RedirectToAction("Index", "Login");
            else if (!sessionHandler.HasPermissionToViewPage(MethodBase.GetCurrentMethod().Name))
                return RedirectToAction("Index", "PageNotFound");
            else
                return View("~/Views/CA/Index.cshtml");
        }

        [HttpGet]
        public IActionResult CreateObservation()
        {
            ViewData["TopMenu"] = tm.GetTopMenus();
            ViewData["TopMenuPages"] = tm.GetTopMenusPages();
            ViewData["DivisionList"] = dBConnection.GetDivisions(false);
            ViewData["DepartmentList"] = dBConnection.GetDepartments(0, false);

            if (!sessionHandler.IsUserLoggedIn())
                return RedirectToAction("Index", "Login");
            else if (!sessionHandler.HasPermissionToViewPage(MethodBase.GetCurrentMethod().Name))
                return RedirectToAction("Index", "PageNotFound");
            else
                return View("~/Views/CA/CreateObservation.cshtml");
        }

        [HttpGet]
        public IActionResult EditObservation(int id)
        {
            ViewData["TopMenu"] = tm.GetTopMenus();
            ViewData["TopMenuPages"] = tm.GetTopMenusPages();
            // TODO: load observation by id

            if (!sessionHandler.IsUserLoggedIn())
                return RedirectToAction("Index", "Login");
            else if (!sessionHandler.HasPermissionToViewPage(MethodBase.GetCurrentMethod().Name))
                return RedirectToAction("Index", "PageNotFound");
            else
                return View("~/Views/CA/EditObservation.cshtml");
        }

        [HttpGet]
        public IActionResult ObservationDetails(int id)
        {
            ViewData["TopMenu"] = tm.GetTopMenus();
            ViewData["TopMenuPages"] = tm.GetTopMenusPages();
            // TODO: load observation details by id

            if (!sessionHandler.IsUserLoggedIn())
                return RedirectToAction("Index", "Login");
            else if (!sessionHandler.HasPermissionToViewPage(MethodBase.GetCurrentMethod().Name))
                return RedirectToAction("Index", "PageNotFound");
            else
                return View("~/Views/CA/ObservationDetails.cshtml");
        }

        [HttpGet]
        public IActionResult AssignObservation(int id)
        {
            ViewData["TopMenu"] = tm.GetTopMenus();
            ViewData["TopMenuPages"] = tm.GetTopMenusPages();
            ViewData["DivisionList"] = dBConnection.GetDivisions(false);

            if (!sessionHandler.IsUserLoggedIn())
                return RedirectToAction("Index", "Login");
            else if (!sessionHandler.HasPermissionToViewPage(MethodBase.GetCurrentMethod().Name))
                return RedirectToAction("Index", "PageNotFound");
            else
                return View("~/Views/CA/AssignObservation.cshtml");
        }

        [HttpGet]
        public IActionResult AssignToDepartment(int id)
        {
            ViewData["TopMenu"] = tm.GetTopMenus();
            ViewData["TopMenuPages"] = tm.GetTopMenusPages();
            ViewData["DepartmentList"] = dBConnection.GetDepartments(0, false);

            if (!sessionHandler.IsUserLoggedIn())
                return RedirectToAction("Index", "Login");
            else if (!sessionHandler.HasPermissionToViewPage(MethodBase.GetCurrentMethod().Name))
                return RedirectToAction("Index", "PageNotFound");
            else
                return View("~/Views/CA/AssignToDepartment.cshtml");
        }

        [HttpGet]
        public IActionResult DepartmentResponse(int id)
        {
            ViewData["TopMenu"] = tm.GetTopMenus();
            ViewData["TopMenuPages"] = tm.GetTopMenusPages();

            if (!sessionHandler.IsUserLoggedIn())
                return RedirectToAction("Index", "Login");
            else if (!sessionHandler.HasPermissionToViewPage(MethodBase.GetCurrentMethod().Name))
                return RedirectToAction("Index", "PageNotFound");
            else
                return View("~/Views/CA/DepartmentResponse.cshtml");
        }

        [HttpGet]
        public IActionResult ReviewObservation(int id)
        {
            ViewData["TopMenu"] = tm.GetTopMenus();
            ViewData["TopMenuPages"] = tm.GetTopMenusPages();

            if (!sessionHandler.IsUserLoggedIn())
                return RedirectToAction("Index", "Login");
            else if (!sessionHandler.HasPermissionToViewPage(MethodBase.GetCurrentMethod().Name))
                return RedirectToAction("Index", "PageNotFound");
            else
                return View("~/Views/CA/ReviewObservation.cshtml");
        }

        [HttpGet]
        public IActionResult HeadFADReview(int id)
        {
            ViewData["TopMenu"] = tm.GetTopMenus();
            ViewData["TopMenuPages"] = tm.GetTopMenusPages();

            if (!sessionHandler.IsUserLoggedIn())
                return RedirectToAction("Index", "Login");
            else if (!sessionHandler.HasPermissionToViewPage(MethodBase.GetCurrentMethod().Name))
                return RedirectToAction("Index", "PageNotFound");
            else
                return View("~/Views/CA/HeadFADReview.cshtml");
        }

        [HttpGet]
        public IActionResult LegacyEntry()
        {
            ViewData["TopMenu"] = tm.GetTopMenus();
            ViewData["TopMenuPages"] = tm.GetTopMenusPages();
            ViewData["DivisionList"] = dBConnection.GetDivisions(false);
            ViewData["DepartmentList"] = dBConnection.GetDepartments(0, false);

            if (!sessionHandler.IsUserLoggedIn())
                return RedirectToAction("Index", "Login");
            else if (!sessionHandler.HasPermissionToViewPage(MethodBase.GetCurrentMethod().Name))
                return RedirectToAction("Index", "PageNotFound");
            else
                return View("~/Views/CA/LegacyEntry.cshtml");
        }

        [HttpGet]
        public IActionResult ObservationAuditTrail(int id)
        {
            ViewData["TopMenu"] = tm.GetTopMenus();
            ViewData["TopMenuPages"] = tm.GetTopMenusPages();

            if (!sessionHandler.IsUserLoggedIn())
                return RedirectToAction("Index", "Login");
            else if (!sessionHandler.HasPermissionToViewPage(MethodBase.GetCurrentMethod().Name))
                return RedirectToAction("Index", "PageNotFound");
            else
                return View("~/Views/CA/ObservationAuditTrail.cshtml");
        }
    }
}
