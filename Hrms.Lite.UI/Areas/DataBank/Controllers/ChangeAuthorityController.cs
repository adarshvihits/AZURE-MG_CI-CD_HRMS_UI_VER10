using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.DataBank;
using Hrms.Lite.Shared.General;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.DataBank.Controllers
{
    [Area("DataBank")]
    [SessionAuthorize]
    public class ChangeAuthorityController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly ICommonService _commonService;
        private readonly IChangeAuthorityService _service;
        private readonly IHelpers _helpers;

        public ChangeAuthorityController(IDropdownService dropdownService, ICommonService commonService, IChangeAuthorityService ChangeAuthorityService, IHelpers helpers)
        {
            _dropdownService = dropdownService;
            _commonService = commonService;
            _service = ChangeAuthorityService;
            _helpers = helpers;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                ChangeAuthority model = new ChangeAuthority();
                model.filter = await _commonService.GetFilterDropdowns();
                ViewBag.WorkFlow = new SelectList(await _dropdownService.GetChangeAuthorityWorkFlowDropdown(), "Code", "Name");
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
        public async Task<IActionResult> GetList(Filter filter,int WorkflowCode)
        {
            try
            {
                ChangeAuthority model = new ChangeAuthority();
                model = await _service.GetList(WorkflowCode,filter);
                return PartialView("_List", model);
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
        public async Task<IActionResult> GetAuthorityPopup(int TrnCode,Guid EmployeeGI, int WorkflowCode)
        {
            try
            {
                ChangeAuthority model = new ChangeAuthority();
                ViewBag.Authority = new SelectList(await _dropdownService.GetEmployeeDropDown(), "Code", "Name");
                model = await _service.GetAuthorityDetails(TrnCode,WorkflowCode);
                model.EmployeeHeader = await _commonService.GetEmployeeHeader(EmployeeGI);
                model.TrnCode = TrnCode;
                model.EmployeeGI = EmployeeGI;
                model.WorkflowCode = WorkflowCode;
                return PartialView("_AuthorityPopup", model);
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
        public async Task<ActionResult> AuthorityUpdate(int TrnCode, int LevelCode, int AuthorityCode, int HDCode, int WorkflowCode)
        {
            try
            {
                ChangeAuthority model = new ChangeAuthority();
                var response = await _service.AuthorityUpdate(TrnCode, LevelCode, AuthorityCode, HDCode,WorkflowCode);
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
