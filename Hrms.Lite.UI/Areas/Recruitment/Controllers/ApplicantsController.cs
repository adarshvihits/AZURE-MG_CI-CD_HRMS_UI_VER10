using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.Recruitment;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Recruitment;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.Recruitment.Controllers
{
    [Area("Recruitment")]
    [SessionAuthorize]
    public class ApplicantsController : Controller
    {
        private readonly IApplicantsService _service;
        private readonly ICommonService _commonService;

        public ApplicantsController(IApplicantsService service, ICommonService commonService)
        {
            _service = service;
            _commonService = commonService;
        }

        // Export to PDF from the applicant quick view - Telerik report Reports/ApplicantProfile.trdp
        public async Task<IActionResult> ApplicantProfilePrint(Guid ApplicantGI)
        {
            try
            {
                ApplicantProfile model = new ApplicantProfile();
                model.ApplicantGI = ApplicantGI;
                model.Url = await _commonService.GetUrl();
                return View("~/Areas/Recruitment/Views/Applicants/ApplicantProfilePrint.cshtml", model);
            }
            catch (UnauthorizedAccessException)
            {
                return RedirectToAction("Login", "Account", new { Area = "" });
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Account", new { Area = "" });
            }
        }

        public IActionResult Index()
        {
            try
            {
                // rows and tab counts are loaded by the server-side DataTable (GetApplicantsList)
                return View(new Applicants { TabIndex = 1, ApplicantList = new List<ApplicantListItem>() });
            }
            catch (UnauthorizedAccessException)
            {
                return RedirectToAction("Login", "Account", new { Area = "" });
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Account", new { Area = "" });
            }
        }

        // full profile - opens in the same window from the quick view
        public async Task<IActionResult> Profile(Guid ApplicantGI)
        {
            try
            {
                ApplicantProfile model = await _service.GetApplicantProfile(ApplicantGI);
                if (model == null)
                    return RedirectToAction("Index");
                return View(model);
            }
            catch (UnauthorizedAccessException)
            {
                return RedirectToAction("Login", "Account", new { Area = "" });
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Account", new { Area = "" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetApplicantsList(Filter filter1, int TabIndex)
        {
            try
            {
                filter1 = filter1 ?? new Filter();
                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                    filter1.searchValue = "";
                if (filter1.length == 0)
                    filter1.length = 10; // -1 ("All") is passed through to the procedure

                Applicants model = await _service.GetApplicantsList(TabIndex, filter1) ?? new Applicants();

                return Json(new
                {
                    data = model.ApplicantList ?? new List<ApplicantListItem>(),
                    draw = filter1.draw,
                    recordsTotal = model.TotalCount,
                    recordsFiltered = model.FilteredCount,
                    activeCount = model.ActiveCount,
                    shortlistedCount = model.ShortlistedCount,
                    evaluationCount = model.EvaluationCount
                });
            }
            catch (UnauthorizedAccessException)
            {
                return RedirectToAction("Login", "Account", new { Area = "" });
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Account", new { Area = "" });
            }
        }
    }
}
