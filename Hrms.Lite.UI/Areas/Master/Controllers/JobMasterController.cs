using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.Master;
using Hrms.Lite.Shared.Master;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.Master.Controllers
{
    [Area("Master")]
    [SessionAuthorize]
    public class JobMasterController : Controller
    {
        public readonly IJobMasterService _service;
        private readonly IDropdownService _dropdownService;
        public JobMasterController(IJobMasterService service, IDropdownService dropdownService)
        {
            _service = service;
            _dropdownService = dropdownService;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                ViewBag.DepartmentDropDown = new SelectList(await _dropdownService.GetDepartmentDropdown(), "Code", "Name");
                return View(await _service.GetList(0, ""));
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
        public async Task<IActionResult> GetJobMasterList(int DepartmentCode, string Status)
        {
            try
            {
                return PartialView("_JobMasterList", await _service.GetList(DepartmentCode, Status));
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
        public async Task<IActionResult> Create()
        {
            try
            {
                JobMaster model = new JobMaster();
                model.Mode = "CREATE";
                model.Active = true;
                await LoadDropdowns();
                return PartialView("_CreateOrEdit", model);
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
        public async Task<IActionResult> Edit(Guid JobGI, Guid LogGI)
        {
            try
            {
                JobMaster model = await _service.GetJobMasterDetails(JobGI, LogGI, "MODIFY");
                model.Mode = "MODIFY";
                await LoadDropdowns();
                return PartialView("_CreateOrEdit", model);
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
        public async Task<ActionResult> Save(JobMaster Input)
        {
            try
            {
                Input.Qualifications ??= new int?[0];
                var response = await _service.Create(Input);
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
        public async Task<IActionResult> Approve(Guid JobGI, Guid LogGI)
        {
            try
            {
                JobMaster model = await _service.GetJobMasterDetails(JobGI, LogGI, "APPROVAL");
                return PartialView("_Approve", model);
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
        public async Task<ActionResult> Approve1(JobMaster model)
        {
            try
            {
                model.ApproveMode = "APPROVE";
                var response = await _service.ApproveOrReject(model);
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
        [HttpPost]
        public async Task<ActionResult> Reject(JobMaster model)
        {
            try
            {
                model.ApproveMode = "REJECT";
                var response = await _service.ApproveOrReject(model);
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
        private async Task LoadDropdowns()
        {
            ViewBag.DepartmentDropDown = new SelectList(await _dropdownService.GetDepartmentDropdown(), "Code", "Name");
            ViewBag.SectionDropDown = new SelectList(await _dropdownService.GetSectionDropdown(), "Code", "Name");
            ViewBag.GradeDropDown = new SelectList(await _dropdownService.GetGradeDropdown(), "Code", "Name");
            ViewBag.BusinessTypeDropDown = new SelectList(await _dropdownService.GetBusinessTypeDropDown(), "Code", "Name");
            ViewBag.WorkModeDropDown = new SelectList(await _dropdownService.GetWorkModeDropdown(), "Code", "Name");
            ViewBag.EmploymentTypeDropDown = new SelectList(await _dropdownService.GetJobEmploymentTypeDropdown(), "Code", "Name");
            ViewBag.ExperienceLevelDropDown = new SelectList(await _dropdownService.GetExperienceLevelDropdown(), "Code", "Name");
            ViewBag.IndustryDropDown = new SelectList(await _dropdownService.GetJobAreaDropdown(), "Code", "Name");
            ViewBag.QualificationDropDown = new SelectList(await _dropdownService.GetJobQualificationDropdown(), "Code", "Name");
        }
    }
}
