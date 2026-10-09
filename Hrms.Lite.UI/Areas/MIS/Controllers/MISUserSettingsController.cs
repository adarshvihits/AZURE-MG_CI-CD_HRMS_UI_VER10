using ClosedXML.Excel;
using DocumentFormat.OpenXml.EMMA;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.MIS;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.DataBank.Reports;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using Hrms.Lite.Shared.MIS;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Spire.Xls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.MIS.Controllers
{
    [Area("MIS")]
    [SessionAuthorize]
    public class MISUserSettingsController : Controller
    {

        private readonly IMISUserSettingsService _service;
        private readonly ICommonService _commonService;
        private readonly IDropdownService _dropdownService;
        private readonly IWebHostEnvironment _environment;
        public static class Global
        {
            public static Filter GlobalFilter;

        }
        public MISUserSettingsController(IDropdownService dropdownService, IMISUserSettingsService MISUserSettingsService, ICommonService commonService, IHelpers helpers, IWebHostEnvironment env)
        {
            _dropdownService = dropdownService;
            _commonService = commonService;
            _service = MISUserSettingsService;
        }
        public async Task<IActionResult> Index(Filter filter1)
        {
            try
            {
                MISUserSettings model = new MISUserSettings();
                await fillfilterDropdown();
                Global.GlobalFilter = new Filter();
                model = await _service.GetList(filter1);
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
        [HttpPost]
        public async Task<IActionResult> GetList(Filter filter1)

        {
            try
            {
                MISUserSettings model = new MISUserSettings();
                MISUserSettings m = new MISUserSettings();
                //model.EmployeeHeader = new EmployeeHeader();


                Filter filter = new Filter();
                Global.GlobalFilter = filter1;


                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }


                model = await _service.GetList(filter1);

                if (model == null || model.MISUserDetailsList == null || model.MISUserDetailsList.Count() == 0)
                {
                    m.MISUserDetailsList = new List<MISUserSettings>();
                    model = new MISUserSettings();
                    model.MISUserDetailsList = new List<MISUserSettings>();
                }
                else
                {
                    m.MISUserDetailsList = model.MISUserDetailsList;
                }
                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.MISUserDetailsList == null || model.MISUserDetailsList.Count() == 0)
                {

                    return Json(new
                    {
                        data = "",
                        draw = filter1.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows
                    });
                }




                if (model.MISUserDetailsList.Count > 0)
                {
                    totalrows = m.MISUserDetailsList[0].TotalCount;

                }


                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = 100;//m.MISUserDetailsList[0].totalRecords;
                    return Json(new
                    {
                        data = model.MISUserDetailsList,
                        draw = filter1.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows//totalrowsafterfiltering
                    });
                }
                else
                {
                    return Json(new
                    {
                        data = model.MISUserDetailsList,
                        draw = filter1.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows
                    });
                }


            }
            catch (Exception ex)
            {
                return View();
            }




        }



        [HttpPost]
        public async Task<ActionResult> Save(int EmployeeCode)
        {
            MISUserSettings model = new MISUserSettings();
            model.EmployeeCode = EmployeeCode;
            try
            {
                var response = await _service.Create(model);

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

        public async Task fillfilterDropdown()
        {

            ViewBag.Gen = new SelectList(await _dropdownService.GetGenderDropDown(), "Code", "Name");
            ViewBag.Cat = new SelectList(await _dropdownService.GetCategoryDropdown(), "Code", "Name");
            ViewBag.Grd = new SelectList(await _dropdownService.GetGradeDropdown(), "Code", "Name");
            ViewBag.Dsg = new SelectList(await _dropdownService.GetDesignationDropdown(), "Code", "Name");
            ViewBag.Dept = new SelectList(await _dropdownService.GetDepartmentDropdown(), "Code", "Name");
            ViewBag.Loc = new SelectList(await _dropdownService.GetLocationDropdown(), "Code", "Name");
            ViewBag.PayrollGrp = new SelectList(await _dropdownService.GetPayrollGroupDropdown(), "Code", "Name");

            ViewBag.division = new SelectList(await _dropdownService.GetDivisionDropdown(), "Code", "Name");
            ViewBag.section = new SelectList(await _dropdownService.GetSectionDropdown(), "Code", "Name");
            ViewBag.EmploymentType = new SelectList(await _dropdownService.GetEmploymentTypeDropdown(), "Code", "Name");
            ViewBag.EmploymentStatus = new SelectList(await _dropdownService.GetEmploymentStatusDropdown(), "Code", "Name");
            ViewBag.WorkLocation = new SelectList(await _dropdownService.GetWorkLocationDropdown(), "Code", "Name");
            ViewBag.WageGrade = new SelectList(await _dropdownService.GetWageGradeDropdown(), "Code", "Name");

        }

        public ActionResult GetSettingsList()
        {
            MISUserSettings model = new MISUserSettings();

            return PartialView("_CreateOrDelete");
        }
        [HttpPost]
        public async Task<ActionResult> Delete(int EmployeeCode)
        {
            MISUserSettings model = new MISUserSettings();
            model.EmployeeCode = EmployeeCode;
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
