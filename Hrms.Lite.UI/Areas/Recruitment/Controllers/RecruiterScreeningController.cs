using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.Recruitment;
using Hrms.Lite.Shared.Recruitment;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.Recruitment.Controllers
{
    [Area("Recruitment")]
    [SessionAuthorize]
    public class RecruiterScreeningController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly IRecruiterScreeningService _recruiterScreeningService;
        public RecruiterScreeningController(IDropdownService dropdownService, IRecruiterScreeningService recruiterScreeningService)
        {
            _dropdownService = dropdownService;
            _recruiterScreeningService = recruiterScreeningService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                ViewBag.BusinessType = new SelectList(await _dropdownService.GetRecruiterScreeningBusinessTypeDropdown(), "Code", "Name");
                ViewBag.Department = new SelectList(await _dropdownService.GetRecruiterScreeningDepartmentDropdown(null), "Code", "Name");
                ViewBag.Section = new SelectList(await _dropdownService.GetRecruiterScreeningSectionDropdown(null, null), "Code", "Name");
                return View();
            }
            catch (UnauthorizedAccessException e)
            {
                return RedirectToAction("Login", "Account", new { Area = "" });
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", "Account", new { Area = "" });
            }
        }

        public async Task<IActionResult> GetDepartments(int? BusinessTypeCode)
        {
            try
            {
                var list = await _dropdownService.GetRecruiterScreeningDepartmentDropdown(BusinessTypeCode);
                return Json(list);
            }
            catch (UnauthorizedAccessException e)
            {
                return RedirectToAction("Login", "Account", new { Area = "" });
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", "Account", new { Area = "" });
            }
        }

        public async Task<IActionResult> GetSections(int? BusinessTypeCode, int? DepartmentCode)
        {
            try
            {
                var list = await _dropdownService.GetRecruiterScreeningSectionDropdown(BusinessTypeCode, DepartmentCode);
                return Json(list);
            }
            catch (UnauthorizedAccessException e)
            {
                return RedirectToAction("Login", "Account", new { Area = "" });
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", "Account", new { Area = "" });
            }
        }

        public async Task<IActionResult> GetInterviewPanels(int LocationCode, int DepartmentCode, int SectionCode, int GradeCode)
        {
            try
            {
                var list = await _dropdownService.GetInterviewPanelDropdown(LocationCode, DepartmentCode, SectionCode, GradeCode);
                return Json(list);
            }
            catch (UnauthorizedAccessException e)
            {
                return RedirectToAction("Login", "Account", new { Area = "" });
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", "Account", new { Area = "" });
            }
        }

        public async Task<IActionResult> GetJobs(int? BusinessTypeCode, int? DepartmentCode, int? SectionCode)
        {
            try
            {
                var list = await _dropdownService.GetRecruiterScreeningJobDropdown(BusinessTypeCode, DepartmentCode, SectionCode);
                return Json(list);
            }
            catch (UnauthorizedAccessException e)
            {
                return RedirectToAction("Login", "Account", new { Area = "" });
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", "Account", new { Area = "" });
            }
        }

        public async Task<IActionResult> GetOpenVacancies(int? BusinessTypeCode, int? DepartmentCode, int? SectionCode, int? JobCode)
        {
            try
            {
                var list = await _recruiterScreeningService.GetOpenVacancies(BusinessTypeCode, DepartmentCode, SectionCode, JobCode);
                return Json(list);
            }
            catch (UnauthorizedAccessException e)
            {
                return RedirectToAction("Login", "Account", new { Area = "" });
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", "Account", new { Area = "" });
            }
        }

        public async Task<IActionResult> GetCandidates(int LocationCode, int DepartmentCode, int SectionCode, int GradeCode)
        {
            try
            {
                var model = await _recruiterScreeningService.GetCandidates(LocationCode, DepartmentCode, SectionCode, GradeCode);
                return Json(model);
            }
            catch (UnauthorizedAccessException e)
            {
                return RedirectToAction("Login", "Account", new { Area = "" });
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", "Account", new { Area = "" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> MapCandidate(RecruiterScreeningMapRequest model)
        {
            try
            {
                var response = await _recruiterScreeningService.MapCandidate(model);
                return Json(response);
            }
            catch (UnauthorizedAccessException e)
            {
                return RedirectToAction("Login", "Account", new { Area = "" });
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", "Account", new { Area = "" });
            }
        }
    }
}
