using AIS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;
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


        public IActionResult Edit()
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
                    return View("~/Views/Email/Edit.cshtml");
                }
            }

        public IActionResult Send()
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
                    return View("~/Views/Email/Send.cshtml");
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
        public IActionResult StaffPositionIndex()
        {
            ViewData["TopMenu"] = tm.GetTopMenus();
            ViewData["TopMenuPages"] = tm.GetTopMenusPages();
            if (!sessionHandler.IsUserLoggedIn())
                return RedirectToAction("Index", "Login");
            var zone = sessionHandler.GetSessionUser().UserPostingAuditZone ?? 0;
            var list = dBConnection.GetStaffPositions(zone);
            return View("~/Views/FAD/StaffPositionIndex.cshtml", list);
        }

        public IActionResult StaffPositionCreate()
        {
            ViewData["TopMenu"] = tm.GetTopMenus();
            ViewData["TopMenuPages"] = tm.GetTopMenusPages();
            if (!sessionHandler.IsUserLoggedIn())
                return RedirectToAction("Index", "Login");
            return View("~/Views/FAD/StaffPositionCreate.cshtml");
        }

        [HttpPost]
        public IActionResult StaffPositionCreate(StaffPosition model)
        {
            if (!sessionHandler.IsUserLoggedIn())
                return RedirectToAction("Index", "Login");
            dBConnection.AddStaffPosition(model);
            return RedirectToAction("StaffPositionIndex");
        }

        public IActionResult ManpowerDemandIndex()
        {
            ViewData["TopMenu"] = tm.GetTopMenus();
            ViewData["TopMenuPages"] = tm.GetTopMenusPages();
            if (!sessionHandler.IsUserLoggedIn())
                return RedirectToAction("Index", "Login");
            var list = dBConnection.GetDemandSummary("ZTBL");
            return View("~/Views/FAD/ManpowerDemandIndex.cshtml", list);
        }

        public IActionResult ManpowerDemandCreate()
        {
            ViewData["TopMenu"] = tm.GetTopMenus();
            ViewData["TopMenuPages"] = tm.GetTopMenusPages();
            if (!sessionHandler.IsUserLoggedIn())
                return RedirectToAction("Index", "Login");
            return View("~/Views/FAD/ManpowerDemandCreate.cshtml");
        }

        [HttpPost]
        public IActionResult ManpowerDemandCreate(ManpowerDemand model)
        {
            if (!sessionHandler.IsUserLoggedIn())
                return RedirectToAction("Index", "Login");
            dBConnection.AddManpowerDemand(model);
            return RedirectToAction("ManpowerDemandIndex");
        }

        public IActionResult ManpowerDemandReview(int id)
        {
            ViewData["TopMenu"] = tm.GetTopMenus();
            ViewData["TopMenuPages"] = tm.GetTopMenusPages();
            if (!sessionHandler.IsUserLoggedIn())
                return RedirectToAction("Index", "Login");
            var dem = dBConnection.GetDemandSummary("ZTBL").FirstOrDefault(x => x.Id == id);
            return View("~/Views/FAD/ManpowerDemandReview.cshtml", dem);
        }

        [HttpPost]
        public IActionResult ManpowerDemandReview(ManpowerDemand model)
        {
            if (!sessionHandler.IsUserLoggedIn())
                return RedirectToAction("Index", "Login");
            dBConnection.UpdateManpowerDemandStatus(model.Id, model.Status, model.HeadFadRemarks);
            return RedirectToAction("ManpowerDemandIndex");
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

        public IActionResult Index()
        {
            ViewData["TopMenu"] = tm.GetTopMenus();
            ViewData["TopMenuPages"] = tm.GetTopMenusPages();
            if (!sessionHandler.IsUserLoggedIn())
                return RedirectToAction("Index", "Login");
            else
                return View("~/Views/FAD/Index.cshtml");
        }


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


        public IActionResult AssignDepartment()
        {
            ViewData["TopMenu"] = tm.GetTopMenus();
            ViewData["TopMenuPages"] = tm.GetTopMenusPages();
            if (!sessionHandler.IsUserLoggedIn())
                return RedirectToAction("Index", "Login");
            else
                return View("~/Views/FAD/AssignDepartment.cshtml");
        }


        public IActionResult EnterResponse()
        {
            ViewData["TopMenu"] = tm.GetTopMenus();
            ViewData["TopMenuPages"] = tm.GetTopMenusPages();
            if (!sessionHandler.IsUserLoggedIn())
                return RedirectToAction("Index", "Login");
            else
                return View("~/Views/FAD/EnterResponse.cshtml");
        }


        public IActionResult ReviewResponse()
        {
            ViewData["TopMenu"] = tm.GetTopMenus();
            ViewData["TopMenuPages"] = tm.GetTopMenusPages();
            if (!sessionHandler.IsUserLoggedIn())
                return RedirectToAction("Index", "Login");
            else
                return View("~/Views/FAD/ReviewResponse.cshtml");
        }


        public IActionResult ForwardToCompliance()
        {
            ViewData["TopMenu"] = tm.GetTopMenus();
            ViewData["TopMenuPages"] = tm.GetTopMenusPages();
            if (!sessionHandler.IsUserLoggedIn())
                return RedirectToAction("Index", "Login");
            else
                return View("~/Views/FAD/ForwardToCompliance.cshtml");
        }


        public IActionResult ReviewHistory()
        {
            ViewData["TopMenu"] = tm.GetTopMenus();
            ViewData["TopMenuPages"] = tm.GetTopMenusPages();
            if (!sessionHandler.IsUserLoggedIn())
                return RedirectToAction("Index", "Login");
            else
                return View("~/Views/FAD/ReviewHistory.cshtml");
        }


        public IActionResult AuditValidation(int observationId)
        {
            ViewData["TopMenu"] = tm.GetTopMenus();
            ViewData["TopMenuPages"] = tm.GetTopMenusPages();
            if (!sessionHandler.IsUserLoggedIn())
                return RedirectToAction("Index", "Login");
            else
                return View("~/Views/FAD/AuditValidation.cshtml", new AuditValidationModel { ObservationId = observationId });
        }


        public IActionResult AuditValidation(AuditValidationModel model)
        {
            ViewData["TopMenu"] = tm.GetTopMenus();
            ViewData["TopMenuPages"] = tm.GetTopMenusPages();
            if (!sessionHandler.IsUserLoggedIn())
                return RedirectToAction("Index", "Login");

            dBConnection.ProcessSBPAuditValidation(model.ObservationId, model.Action, model.Remarks);
            return RedirectToAction("ReviewHistory");
        }


        public IActionResult ViewHistory(int observationId)
        {
            ViewData["TopMenu"] = tm.GetTopMenus();
            ViewData["TopMenuPages"] = tm.GetTopMenusPages();
            if (!sessionHandler.IsUserLoggedIn())
                return RedirectToAction("Index", "Login");
            var history = dBConnection.GetSBPReviewHistory(observationId);
            return View("~/Views/FAD/ViewHistory.cshtml", history);
        }

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

        public IActionResult Error()
            {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }
    }
