using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.Master;
using Hrms.Lite.Shared.Master;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.Master.Controllers
{
    [Area("Master")]
    [SessionAuthorize]
    public class NightAllowanceAmountSettingsController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly ICommonService _commonService;
        private readonly INightAllowanceAmountSettingsService _service;
        private readonly IHelpers _helpers;
        public NightAllowanceAmountSettingsController(INightAllowanceAmountSettingsService service, IDropdownService dropdownService, ICommonService commonService, IHelpers helpers)
        {
            _service = service;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = helpers;
        }


        public async Task<ActionResult> Index()
        {
            try
            {
                NightAllowanceAmountSettings model = new NightAllowanceAmountSettings();

                ViewBag.Department = new SelectList(await _dropdownService.GetDepartmentNAASDropdown(), "Code", "Name");
                ViewBag.Purpose = new SelectList(await _dropdownService.GetPurposeNAASDropdown(0), "Code", "Name");
                ViewBag.Grade = new SelectList(await _dropdownService.GetGradeDropdown(), "Code", "Name");
                model = await _service.GetList(0, 0, 0);
                return View(model);
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

        [HttpGet]
        public async Task<JsonResult> GetPurposeNAASDropdown(int DepartmentCode)
        {
            SelectList data = new SelectList("", "Code", "Name");
            data = new SelectList(await _dropdownService.GetPurposeNAASDropdown(DepartmentCode), "Code", "Name");
            return Json(data);
        }


        public async Task<ActionResult> GetNAASList(int DepartmentCode,int PurposeCode,int GradeCode)
        {
            try
            {
                
                    return PartialView("_List", await _service.GetList(DepartmentCode, PurposeCode, GradeCode));
                
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


        public async Task<ActionResult> Create()
        {
            try
            {
                NightAllowanceAmountSettings model = new NightAllowanceAmountSettings();
                model.Mode = "CREATE";

                ViewBag.Department = new SelectList(await _dropdownService.GetDepartmentNAASDropdown(), "Code", "Name");
                ViewBag.Purpose = new SelectList(await _dropdownService.GetPurposeNAASDropdown(0), "Code", "Name");
                ViewBag.Grade = new SelectList(await _dropdownService.GetGradeDropdown(), "Code", "Name");

                return PartialView("~/Areas/Master/Views/NightAllowanceAmountSettings/_CreateOrEdit.cshtml", model);
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

        public async Task<ActionResult> Edit(int Code,int DepartmentCode, int PurposeCode, int GradeCode)
        {
            try
            {
                NightAllowanceAmountSettings model = await _service.GetDetails(Code,DepartmentCode, PurposeCode, GradeCode, "MODIFY");
                model.Mode = "MODIFY";
                ViewBag.Department = new SelectList(await _dropdownService.GetDepartmentNAASDropdown(), "Code", "Name");
                ViewBag.Purpose = new SelectList(await _dropdownService.GetPurposeNAASDropdown(DepartmentCode), "Code", "Name");
                ViewBag.Grade = new SelectList(await _dropdownService.GetGradeDropdown(), "Code", "Name");
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

        public async Task<ActionResult> Save(NightAllowanceAmountSettings input)
        {
            try
            {
                var response = await _service.Create(input);

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



        public async Task<ActionResult> Delete(int Code,int DepartmentCode, int PurposeCode, int GradeCode)
        {
            try
            {
                NightAllowanceAmountSettings model = await _service.GetDetails(Code,DepartmentCode, PurposeCode, GradeCode, "DELETE");
                model.Mode = "DELETE";
                return PartialView("_Delete", model);
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
        public async Task<ActionResult> Delete(NightAllowanceAmountSettings model)
        {
            try
            {
                var response = await _service.Delete(model);
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
