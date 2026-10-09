using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.MIS;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.DataBank.Reports;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.MIS;
using Hrms.Lite.Shared.Master;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Spire.Xls;
using Hrms.Lite.Shared.TimeOffice;
using Hrms.Lite.Services.Services.DataBank;
using Hrms.Lite.Shared.DashBoard;

namespace Hrms.Lite.UI.Areas.MIS.Controllers
{
    [Area("MIS")]
    [SessionAuthorize]
    public class CompanyMISController : Controller
    {


        private IDropdownService _dropdownService;
        private readonly ICompanyMISService _CompanyMISService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        public string codeName = "";
        public static class Global
        {
            public static Filter GlobalFilter;


        }


        public CompanyMISController(ICompanyMISService CompanyMISService, ICommonService commonService, IDropdownService dropdownService, IHelpers helper)
        {
            _CompanyMISService = CompanyMISService;
            _dropdownService = dropdownService;
            _commonService = commonService;

            _helpers = helper;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                CompanyMIS model = new CompanyMIS();

                var Manpower = await _CompanyMISService.GetPayRollGP("Manpower");
                model.PayrollGP = Manpower.PayrollGP;

                ViewBag.Module = new SelectList(await _dropdownService.GetModules(), "Code", "Name");
                ViewBag.SubModule = new SelectList(await _dropdownService.GetMisReports(0), "Code", "Name");

                return View("~/Areas/MIS/Views/CompanyMIS/Index.cshtml", model);
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

        public async Task<IActionResult> GetDepCount(int report)
        {
            try
            {
                CompanyMIS model = new CompanyMIS();
                model = await _CompanyMISService.GetList(report);

                return Json(model.MISDetailsList);



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


        public async Task<IActionResult> getPatRollGP()
        {

            try
            {
                CompanyMIS model = new CompanyMIS();

                var Manpower = await _CompanyMISService.GetPayRollGP("Manpower");
                model.PayrollGP = Manpower.PayrollGP;
                return Json(model.PayrollGP);
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



        public async Task<CompanyMIS> GetList(int report)
        {
            CompanyMIS model = new CompanyMIS();
            try
            {


                model = await _CompanyMISService.GetList(report);
                return model;
            }
            catch (UnauthorizedAccessException e)
            {

                return model;
            }
            catch (Exception e)
            {
                return model;
            }
        }


        [HttpGet]
        public async Task<JsonResult> GetDropDown(int moduleCode)
        {
            SelectList data = new SelectList("", "Code", "Name");
            data = new SelectList(await _dropdownService.GetMisReports(moduleCode), "Code", "Name");
            return Json(data);
        }

    }
}
