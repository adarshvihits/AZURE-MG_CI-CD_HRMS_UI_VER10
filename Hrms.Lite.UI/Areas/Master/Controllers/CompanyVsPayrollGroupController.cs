using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.Master;
using Hrms.Lite.Shared.Master;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hrms.Lite.UI.Areas.Master.Controllers
{
    [Area("Master")]
    [SessionAuthorize]
    public class CompanyVsPayrollGroupController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly ICompanyVsPayrollGroupService _service;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        

        public CompanyVsPayrollGroupController(IDropdownService dropdownService, ICompanyVsPayrollGroupService service, ICommonService commonService, IHelpers helpers)
        {
            _dropdownService = dropdownService;
            _service = service;
            _commonService = commonService;
            _helpers = helpers;
            
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                CompanyVsPayrollGroup model = new CompanyVsPayrollGroup();
            model = await _service.GetList(0);
            ViewBag.GroupCompany = new SelectList(await _dropdownService.GetGroupCompanyDropDown(), "Code", "Name");
            
            
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

        public async Task<IActionResult> GetList(int GroupCompanyCode)
        {
            try
            {
                CompanyVsPayrollGroup model = new CompanyVsPayrollGroup();
                model = await _service.GetList(GroupCompanyCode);
                
                return PartialView("~/Areas/Master/Views/CompanyVsPayrollGroup/_CompanyVsPayrollGroupList.cshtml", model);
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


        public async Task<IActionResult> Save(CompanyVsPayrollGroup model)
        {
            try
            {
                var response = await _service.Save(model);
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
