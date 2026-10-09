using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.DashBoard;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.DashBoard;
using Microsoft.AspNetCore.Mvc;
using Hrms.Lite.Shared.MIS;
using System.Threading.Tasks;
using Hrms.Lite.Services.IServices.MIS;
using Hrms.Lite.Services.Services.MIS;
using System;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Rendering;
using Hrms.Lite.Shared.DataBank.DTO;
using Kendo.Mvc.UI;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using Kendo.Mvc.Extensions;
using System.Linq;
using Hrms.Lite.Shared.PayRoll.Reports;
using System.Data;
using Hrms.Lite.Shared.Master;
using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.Office2010.Excel;
using Hrms.Lite.Services.IServices.PayRoll.Reports;
using Spire.Pdf.OPC;
using Hrms.Lite.Shared.TimeOffice.Report;
using Amazon.Runtime.Internal;

namespace Hrms.Lite.UI.Areas.MIS.Controllers
{
    [Area("MIS")]
    [SessionAuthorize]
    public class MISDashBoardController : Controller
    {
        private readonly IMISDashBoardService _service;

        private readonly ICommonService _commonService;
        private readonly IDropdownService _dropdownService;

        public MISDashBoardController(IMISDashBoardService service, ICommonService commonService, IDropdownService dropdownService)
        {
            _service = service;

            _commonService = commonService;
            _dropdownService = dropdownService;

        }
        public async Task<IActionResult> Index()
        {
            MISDashBoard model = new MISDashBoard();
            ViewBag.TransitionType = new SelectList(await _dropdownService.GetJobTransitionTypeDropdown(), "Code", "Name");
            ViewBag.GrpCompany = new SelectList(await _dropdownService.GetGroupCompanyDropDown(), "Code", "Name");
            ViewBag.LeavePeriod = new SelectList(await _dropdownService.GetLeavePeriodFilterDropdown(), "Code", "Name");
            ViewBag.Attendance = new SelectList(await _dropdownService.GetAttendanceDropdown(), "Code", "Name");

            var response = await _commonService.GetCurrentLeavePeriod();
            var finYear = await _commonService.GetCurrentLFinancialYear();
            var CMonthCode = await _commonService.GetCurrentMonthDetails();

            var currntLp = Convert.ToInt32(response.Data);
            var currntFY = Convert.ToInt32(finYear.Data);
            model = await _service.GetMonths(currntFY);
            model.CurrentSalaryPeriodCode = CMonthCode.LastProcessedMonth;
            model.LeavePeriod = new Shared.Master.LeavePeriod();
            model.LeavePeriod.Code = currntLp;
            model.finYear = currntFY;

            DateTime lastdayofmonth = Convert.ToDateTime(new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1).ToString("yyyy-MMM-dd"));
            DateTime firstdate = Convert.ToDateTime(new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-6).ToString("yyyy-MMM-dd"));
            model.To = DateTime.Now;
            model.From = firstdate;
            //model.To = lastdayofmonth;
            string id = HttpContext.Request.Query["id"].ToString();
            if (id == "6")
            {
                lastdayofmonth = Convert.ToDateTime(new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1).ToString("yyyy-MMM-dd"));
                firstdate = Convert.ToDateTime(new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("yyyy-MMM-dd"));
                model.From = firstdate;
                model.To = lastdayofmonth;
            }


            ViewBag.financialYear = new SelectList(await _dropdownService.GetFinancialYearDropdown(), "Code", "Name");


            return View("~/Areas/MIS/Views/MISDashBoard/Index.cshtml", model);
        }


        public async Task<MISDashBoard> GetList(string from, string to, int groupCom)
        {
            MISDashBoard model = new MISDashBoard();
            try
            {


                var resignations = await _service.GetResignationCount(from, to, groupCom);
                model.resignationCount = resignations.resignationCount;
                model.averageResignationCount = resignations.averageResignationCount;

                //var avgresignations = await _service.GetAverageResignationCount();
                //model.averageResignationCount = avgresignations.averageResignationCount;


                var joiners = await _service.GetJoinersCount(from, to, groupCom);
                model.joiningCount = joiners.joiningCount;
                model.averageJoinersCount = joiners.averageJoinersCount;
                model.badHires = joiners.badHires;

                var attritionRate = await _service.GetAttritionRate(from, to, groupCom);
                model.attritionRate = attritionRate.attritionRate;

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



        public async Task<IActionResult> Orders_ValueMapper(int[] values)
        {


            MISDashBoard model = new MISDashBoard();
            List<Employee_MIS_List> modellist = new List<Employee_MIS_List>();

            model = await _service.GetEmployeeList(0);

            modellist = model.Employee_MIS_List;

            var indices = new List<int>();

            if (values != null && values.Any())
            {
                var index = 0;

                foreach (var order in modellist)
                {
                    if (values.Contains(order.employeeCode))
                    {
                        indices.Add(index);
                    }

                    index += 1;
                }
            }

            return Json(indices);
        }

        public async Task<IActionResult> Orders_ValueMapperOv(int[] values)
        {


            MISDashBoard model = new MISDashBoard();
            List<Employee_MIS_List> modellist = new List<Employee_MIS_List>();

            model = await _service.GetEmployeeListOv(0, 0);

            modellist = model.Employee_MIS_List;

            var indices = new List<int>();

            if (values != null && values.Any())
            {
                var index = 0;

                foreach (var order in modellist)
                {
                    if (values.Contains(order.employeeCode))
                    {
                        indices.Add(index);
                    }

                    index += 1;
                }
            }

            return Json(indices);
        }

        #region Leave Analysis

        public async Task<MISDashBoard> GetLeave(int currntLp, int groupCom)
        {
            MISDashBoard model = new MISDashBoard();
            try
            {

                var leaveData = await _service.GetLeaveData(currntLp, groupCom);
                model.leaveCount = leaveData.leaveCount;
                model.avgLeave = leaveData.avgLeave;
                model.lopCount = leaveData.lopCount;
                model.absCount = leaveData.absCount;
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

        public async Task<IActionResult> GetLeaveMonthwiseCount(int currntLp, int groupCom)
        {
            try
            {
                MISDashBoard model = new MISDashBoard();
                model = await _service.GetLeaveMonthwiseCount(currntLp, groupCom);
                return Json(model.MonthWiseLeaveList);


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

        public async Task<IActionResult> GetLeaveageBandCount(int currntLp, int groupCom)
        {
            try
            {
                MISDashBoard model = new MISDashBoard();
                model = await _service.GetLeaveageBandCount(currntLp, groupCom);

                return Json(model.AgeBandLeaveCountList);



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

        public async Task<IActionResult> GetDepartmentCount(int currntLp, int groupCom)
        {
            try
            {
                MISDashBoard model = new MISDashBoard();
                model = await _service.GetDepartmentCount(currntLp, groupCom);

                return Json(model.DepartmentCountList);



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

        public async Task<IActionResult> GetGradeCount(int currntLp, int groupCom)
        {
            try
            {
                MISDashBoard model = new MISDashBoard();
                model = await _service.GetGradeCount(currntLp, groupCom);

                return Json(model.GradeCountList);



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
        public async Task<IActionResult> GetLocationCount(int currntLp, int groupCom)
        {
            try
            {
                MISDashBoard model = new MISDashBoard();
                model = await _service.GetLocationCount(currntLp, groupCom);

                return Json(model.LocationCountList);



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
        public async Task<IActionResult> GetRegionCount(int currntLp, int groupCom)
        {
            try
            {
                MISDashBoard model = new MISDashBoard();
                model = await _service.GetRegionCount(currntLp, groupCom);

                return Json(model.RegionCountList);



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
        public async Task<IActionResult> GetCategoryCount(int currntLp, int groupCom)
        {
            try
            {
                MISDashBoard model = new MISDashBoard();
                model = await _service.GetCategoryCount(currntLp, groupCom);

                return Json(model.CategoryCountList);



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
        public async Task<IActionResult> GetDivisionCount(int currntLp, int groupCom)
        {
            try
            {
                MISDashBoard model = new MISDashBoard();
                model = await _service.GetDivisionCount(currntLp, groupCom);

                return Json(model.DivisionCountList);



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

        public async Task<IActionResult> GetCatagoryLeaveCount(int currntLp, int groupCom)
        {
            try
            {
                MISDashBoard model = new MISDashBoard();
                model = await _service.GetCatagoryLeaveCount(currntLp, groupCom);

                return Json(model.CatagoryLeaveCountList);



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

        #endregion

        #region Resignation Analysis
        public async Task<IActionResult> GetGenderResignationCount(string from, string to, int groupCom)
        {
            try
            {
                MISDashBoard model = new MISDashBoard();
                model = await _service.GetGenderResignationCount(from, to, groupCom);

                return Json(model.GenderResignationCountList);



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
        public async Task<IActionResult> GetResignationMonthwiseCount(int groupCom)
        {
            try
            {
                MISDashBoard model = new MISDashBoard();
                model = await _service.GetResignationMonthwiseCount(groupCom);

                return Json(model.MonthWiseTerminationList);



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

        public async Task<IActionResult> GetResignationReasonCount(string from, string to, int groupCom)
        {
            try
            {
                MISDashBoard model = new MISDashBoard();
                model = await _service.GetResignationReasonCount(from, to, groupCom);

                return Json(model.GenderResignationReasonCountList);



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

        public async Task<IActionResult> GetResignationTenureCount(string from, string to, int groupCom)
        {
            try
            {
                MISDashBoard model = new MISDashBoard();
                model = await _service.GetResignationTenureCount(from, to, groupCom);

                return Json(model.TenureResignationCountList);



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

        public async Task<IActionResult> GetResignationageBandCount(string from, string to, int groupCom)
        {
            try
            {
                MISDashBoard model = new MISDashBoard();
                model = await _service.GetResignationageBandCount(from, to, groupCom);

                return Json(model.AgeBandResignationCountList);



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
        public ActionResult Excel_Export_Save(string contentType, string base64, string fileName)
        {
            var fileContents = Convert.FromBase64String(base64);

            return File(fileContents, contentType, fileName);
        }

        public async Task<IActionResult> GetTermDepartmentCount(string from, string to, int groupCom)
        {
            try
            {
                MISDashBoard model = new MISDashBoard();
                model = await _service.GetTermDepartmentCount(from, to, groupCom);

                return Json(model.TerminationsDepartmentCountList);



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

        public async Task<IActionResult> GetTermLocationCount(string from, string to, int groupCom)
        {
            try
            {
                MISDashBoard model = new MISDashBoard();
                model = await _service.GetTermLocationCount(from, to, groupCom);

                return Json(model.TerminationsLocationCountList);



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
        public async Task<IActionResult> GetTermDivisionCount(string from, string to, int groupCom)
        {
            try
            {
                MISDashBoard model = new MISDashBoard();
                model = await _service.GetTermDivisionCount(from, to, groupCom);

                return Json(model.TerminationsDivisionCountList);



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
        public async Task<IActionResult> GetTermGradeCount(string from, string to, int groupCom)
        {
            try
            {
                MISDashBoard model = new MISDashBoard();
                model = await _service.GetTermGradeCount(from, to, groupCom);

                return Json(model.TerminationsGradeCountList);



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
        public async Task<IActionResult> GetTermCategoryCount(string from, string to, int groupCom)
        {
            try
            {
                MISDashBoard model = new MISDashBoard();
                model = await _service.GetTermCategoryCount(from, to, groupCom);

                return Json(model.TerminationsCategoryCountList);



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
        public async Task<IActionResult> GetTermRegionCount(string from, string to, int groupCom)
        {
            try
            {
                MISDashBoard model = new MISDashBoard();
                model = await _service.GetTermRegionCount(from, to, groupCom);

                return Json(model.TerminationsRegionCountList);



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



        #region Resignation List

        [HttpPost]
        public async Task<IActionResult> GetResignationageBandCountList([DataSourceRequest] DataSourceRequest request, string from, string to, int groupCom, string age)
        {

            MISDashBoard model = new MISDashBoard();
            List<AgeResignationCountList> modellist = new List<AgeResignationCountList>();

            model = await _service.GetResignationageBandCountList(from, to, groupCom, age);

            modellist = model.AgeResignationCountList;

            var dataResult1 = modellist.ToDataSourceResult(request);


            return Json(dataResult1, new JsonSerializerSettings { ContractResolver = new DefaultContractResolver() });

        }

        [HttpPost]
        public async Task<IActionResult> GetResignationTenureCountList([DataSourceRequest] DataSourceRequest request, string from, string to, int groupCom, string age)
        {

            MISDashBoard model = new MISDashBoard();
            List<AgeResignationCountList> modellist = new List<AgeResignationCountList>();

            if (age == "10+")
            {
                age = "10-100";
            }
            else
            {
                age = age.Replace(" years", "");

            }

            model = await _service.GetResignationTenureCountList(from, to, groupCom, age);

            modellist = model.AgeResignationCountList;

            var dataResult1 = modellist.ToDataSourceResult(request);


            return Json(dataResult1, new JsonSerializerSettings { ContractResolver = new DefaultContractResolver() });

        }

        [HttpPost]
        public async Task<IActionResult> GetGenderResignationCountList([DataSourceRequest] DataSourceRequest request, string from, string to, int groupCom, string age)
        {

            MISDashBoard model = new MISDashBoard();
            List<AgeResignationCountList> modellist = new List<AgeResignationCountList>();

            model = await _service.GetGenderResignationCountList(from, to, groupCom, age);

            modellist = model.AgeResignationCountList;

            var dataResult1 = modellist.ToDataSourceResult(request);


            return Json(dataResult1, new JsonSerializerSettings { ContractResolver = new DefaultContractResolver() });

        }
        public async Task<IActionResult> GetDepartmentResignationCountList([DataSourceRequest] DataSourceRequest request, string from, string to, int groupCom, string type, string age)
        {

            MISDashBoard model = new MISDashBoard();
            List<AgeResignationCountList> modellist = new List<AgeResignationCountList>();

            model = await _service.GetDepartmentResignationCountList(from, to, groupCom, type, age);

            modellist = model.AgeResignationCountList;

            var dataResult1 = modellist.ToDataSourceResult(request);


            return Json(dataResult1, new JsonSerializerSettings { ContractResolver = new DefaultContractResolver() });

        }
        #endregion
        #endregion

        #region Joiners analysis

        public async Task<MISDashBoard> GetJoinersCount(string from, string to, int groupCom)
        {
            MISDashBoard model = new MISDashBoard();
            try
            {


                var joiners = await _service.GetJoinersCount(from, to, groupCom);
                model.averageJoinersCount = joiners.averageJoinersCount;
                model.joiningCount = joiners.joiningCount;
                model.badHires = joiners.badHires;
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

        public async Task<IActionResult> GetGenderJoinersCount(string from, string to, int groupCom)
        {
            try
            {
                MISDashBoard model = new MISDashBoard();
                model = await _service.GetGenderJoinersCount(from, to, groupCom);

                return Json(model.GenderJoinersCountList);



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

        public async Task<IActionResult> GetJoinersageBandCount(string from, string to, int groupCom)
        {
            try
            {
                MISDashBoard model = new MISDashBoard();
                model = await _service.GetJoinersageBandCount(from, to, groupCom);

                return Json(model.AgeBandJoinersCountList);



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

        public async Task<IActionResult> GetJoinersDepartmentCount(string from, string to, int groupCom)
        {
            try
            {
                MISDashBoard model = new MISDashBoard();
                model = await _service.GetJoinersDepartmentCount(from, to, groupCom);

                return Json(model.GenderJoinersDepartmentCountList);



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

        public async Task<IActionResult> GetJoinersLocation(string from, string to, int groupCom)
        {
            try
            {
                MISDashBoard model = new MISDashBoard();
                model = await _service.GetJoinersLocationCount(from, to, groupCom);

                return Json(model.GenderJoinersLocationCountList);



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

        public async Task<IActionResult> GetJoinersLocationCount(string from, string to, int groupCom)
        {
            try
            {
                MISDashBoard model = new MISDashBoard();
                model = await _service.GetJoinersLocationCount(from, to, groupCom);

                return Json(model.GenderJoinersLocationCountList);



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

        public async Task<IActionResult> GetJoinersDivisionCount(string from, string to, int groupCom)
        {
            try
            {
                MISDashBoard model = new MISDashBoard();
                model = await _service.GetJoinersDivisionCount(from, to, groupCom);

                return Json(model.GenderJoinersDivisionCountList);



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

        public async Task<IActionResult> GetJoinersGradeCount(string from, string to, int groupCom)
        {
            try
            {
                MISDashBoard model = new MISDashBoard();
                model = await _service.GetJoinersGradeCount(from, to, groupCom);

                return Json(model.GenderJoinersGradeCountList);



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

        public async Task<IActionResult> GetJoinersCategoryCount(string from, string to, int groupCom)
        {
            try
            {
                MISDashBoard model = new MISDashBoard();
                model = await _service.GetJoinersCategoryCount(from, to, groupCom);

                return Json(model.GenderJoinersCategoryCountList);



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

        public async Task<IActionResult> GetJoinersRegionCount(string from, string to, int groupCom)
        {
            try
            {
                MISDashBoard model = new MISDashBoard();
                model = await _service.GetJoinersRegionCount(from, to, groupCom);

                return Json(model.GenderJoinersRegionCountList);



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

        public async Task<IActionResult> GetBadHires(string from, string to, int groupCom)
        {

            try
            {
                MISDashBoard model = new MISDashBoard();
                model = await _service.GetBadHires(from, to, groupCom);

                return Json(model.BadHiresList);



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

        #region Joiners List

        public async Task<IActionResult> GetJoinersCountList([DataSourceRequest] DataSourceRequest request, string from, string to, int groupCom, string type, string age)
        {
            MISDashBoard model = new MISDashBoard();
            List<AgeResignationCountList> modellist = new List<AgeResignationCountList>();

            model = await _service.GetJoinersCountList(from, to, groupCom, type, age);

            modellist = model.AgeResignationCountList;

            var dataResult1 = modellist.ToDataSourceResult(request);


            return Json(dataResult1, new JsonSerializerSettings { ContractResolver = new DefaultContractResolver() });
        }

        #endregion



        #endregion

        #region Employee analysis

        #region List

        public async Task<IActionResult> EmployeeLeaveDetails([DataSourceRequest] DataSourceRequest request, int EmployeeCode)
        {


            MISDashBoard model = new MISDashBoard();
            List<MISEmployeeCarrerDetails> modellist = new List<MISEmployeeCarrerDetails>();

            model = await _service.GetEmployeeLeaveDetail(EmployeeCode);

            modellist = model.MISEmployeeCarrerDetails;

            var dataResult1 = modellist.ToDataSourceResult(request);


            return Json(dataResult1, new JsonSerializerSettings { ContractResolver = new DefaultContractResolver() });


        }


        public async Task<IActionResult> EmployeeRewardDetails([DataSourceRequest] DataSourceRequest request, int EmployeeCode)
        {


            MISDashBoard model = new MISDashBoard();
            List<MISEmployeeCarrerDetails> modellist = new List<MISEmployeeCarrerDetails>();

            model = await _service.GetEmployeeRewardDetail(EmployeeCode);

            modellist = model.MISEmployeeCarrerDetails;

            var dataResult1 = modellist.ToDataSourceResult(request);


            return Json(dataResult1, new JsonSerializerSettings { ContractResolver = new DefaultContractResolver() });


        }

        public async Task<IActionResult> EmployeeSkillDetails([DataSourceRequest] DataSourceRequest request, int EmployeeCode)
        {


            MISDashBoard model = new MISDashBoard();
            List<MISEmployeeCarrerDetails> modellist = new List<MISEmployeeCarrerDetails>();

            model = await _service.GetEmployeeSkillDetail(EmployeeCode);

            modellist = model.MISEmployeeCarrerDetails;

            var dataResult1 = modellist.ToDataSourceResult(request);


            return Json(dataResult1, new JsonSerializerSettings { ContractResolver = new DefaultContractResolver() });


        }

        #endregion


        public async Task<IActionResult> GetEmpList(int groupCom)
        {

            MISDashBoard model = new MISDashBoard();
            List<Employee_MIS_List> modellist = new List<Employee_MIS_List>();

            model = await _service.GetEmployeeList(groupCom);

            modellist = model.Employee_MIS_List;

            var dataResult1 = modellist;//.ToDataSourceResult();

            return Json(dataResult1, new JsonSerializerSettings { ContractResolver = new DefaultContractResolver() });

        }
        public async Task<IActionResult> GetEmployeesList([DataSourceRequest] DataSourceRequest request, int groupCom)
        {

            MISDashBoard model = new MISDashBoard();
            List<Employee_MIS_List> modellist = new List<Employee_MIS_List>();

            model = await _service.GetEmployeeList(groupCom);

            modellist = model.Employee_MIS_List;

            var dataResult1 = modellist.ToDataSourceResult(request);

            return Json(dataResult1, new JsonSerializerSettings { ContractResolver = new DefaultContractResolver() });

        }

        public async Task<MISDashBoard> GetEmployeeInformation(string empCode)
        {
            MISDashBoard model = new MISDashBoard();
            try
            {

                var empData = await _service.GetEmployeeInformation(Convert.ToInt32(empCode));
                model.fullName = empData.fullName;
                model.doj = empData.doj;
                model.dot = empData.dot;
                model.dob = empData.dob;
                model.department = empData.department;
                model.designation = empData.designation;
                model.location = empData.location;
                model.grossPay = empData.grossPay;
                model.ctc = empData.ctc;
                model.leave = empData.leave;
                model.leaveBalance = empData.leaveBalance;
                model.proPic = empData.proPic;
                model.geoTagging = empData.geoTagging;
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

        public async Task<IActionResult> GetEmployeeLeaveTrend(int empCode)
        {
            try
            {
                MISDashBoard model = new MISDashBoard();
                model = await _service.GetEmployeeLeaveTrend(empCode);

                return Json(model.MonthWiseLeaveTrend);



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

        public async Task<IActionResult> GetEmployeeMilestones(int empCode)
        {
            try
            {
                MISDashBoard model = new MISDashBoard();
                model = await _service.GetEmployeeMilestones(empCode);
                return Json(model.EmployeeMilestones);


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
        public async Task<IActionResult> GetEmployeeRewards(int empCode)
        {
            try
            {
                MISDashBoard model = new MISDashBoard();
                model = await _service.GetEmployeeRewards(empCode);
                return Json(model.EmployeeRewards);


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

        public async Task<IActionResult> GetEmployeeSkills(int empCode)
        {
            try
            {
                MISDashBoard model = new MISDashBoard();
                model = await _service.GetEmployeeSkills(empCode);
                return Json(model.EmployeeSkills);


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

        public async Task<IActionResult> GetEmployeeMap(int empCode)
        {
            try
            {
                MISDashBoard model = new MISDashBoard();
                model = await _service.GetEmployeeMap(empCode);
                return Json(model.EmployeeMap);


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

        #endregion

        #region Salary Analysis

        #region List

        public async Task<IActionResult> GetSalaryList([DataSourceRequest] DataSourceRequest request, int finCode, int groupCom, string type, string age)
        {


            MISDashBoard model = new MISDashBoard();
            List<SalaryList> modellist = new List<SalaryList>();

            model = await _service.GetSalaryList(finCode, groupCom, type, age);

            modellist = model.SalaryList;

            var dataResult1 = modellist.ToDataSourceResult(request);


            return Json(dataResult1, new JsonSerializerSettings { ContractResolver = new DefaultContractResolver() });


        }

        #endregion


        public async Task<MISDashBoard> GetSalaryInformation(string finCode, int groupCom)
        {
            MISDashBoard model = new MISDashBoard();
            try
            {

                var empData = await _service.GetSalaryInformation(Convert.ToInt32(finCode), groupCom);
                model.totalSalary = empData.totalSalary;
                model.avgSalary = empData.avgSalary;

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

        public async Task<MISDashBoard> GetSalaryIncrementInformation(string finCode, int groupCom)
        {
            MISDashBoard model = new MISDashBoard();
            try
            {

                var empData = await _service.GetSalaryIncrementInformation(Convert.ToInt32(finCode), groupCom);
                model.totalIncrement = empData.totalIncrement;
                model.avgIncrement = empData.avgIncrement;

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

        public async Task<IActionResult> GetSalaryMonthwise(int finCode, int groupCom)
        {
            try
            {
                MISDashBoard model = new MISDashBoard();
                model = await _service.GetSalaryTrend(finCode, groupCom);
                return Json(model.MonthWiseSalaryTrend);


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

        public async Task<IActionResult> GetSalaryIncrementRange(int finCode, int groupCom)
        {
            try
            {
                MISDashBoard model = new MISDashBoard();
                model = await _service.GetSalaryIncrementRange(finCode, groupCom);
                return Json(model.SalaryIncrementRange);


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
        public async Task<IActionResult> GetSalaryDepartmentWise(int finCode, int groupCom)
        {
            try
            {
                MISDashBoard model = new MISDashBoard();
                model = await _service.GetSalaryDepartmentWise(finCode, groupCom);
                return Json(model.SalaryDepartmentWise);


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
        public async Task<IActionResult> GetSalaryLocationWise(int finCode, int groupCom)
        {
            try
            {
                MISDashBoard model = new MISDashBoard();
                model = await _service.GetSalaryLocationWise(finCode, groupCom);
                return Json(model.SalaryLocationWise);


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

        public async Task<IActionResult> GetSalaryDivisionWise(int finCode, int groupCom)
        {
            try
            {
                MISDashBoard model = new MISDashBoard();
                model = await _service.GetSalaryDivisionWise(finCode, groupCom);
                return Json(model.SalaryDivisionWise);


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

        public async Task<IActionResult> GetSalaryGradeWise(int finCode, int groupCom)
        {
            try
            {
                MISDashBoard model = new MISDashBoard();
                model = await _service.GetSalaryGradeWise(finCode, groupCom);
                return Json(model.SalaryGradeWise);


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

        public async Task<IActionResult> GetSalaryCategoryWise(int finCode, int groupCom)
        {
            try
            {
                MISDashBoard model = new MISDashBoard();
                model = await _service.GetSalaryCategoryWise(finCode, groupCom);
                return Json(model.SalaryCategoryWise);


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

        public async Task<IActionResult> GetSalaryRegionWise(int finCode, int groupCom)
        {
            try
            {
                MISDashBoard model = new MISDashBoard();
                model = await _service.GetSalaryRegionWise(finCode, groupCom);
                return Json(model.SalaryRegionWise);


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

        public async Task<IActionResult> GetAllowanceMonthComparison(int month, int groupCom)
        {
            try
            {
                MISDashBoard model = new MISDashBoard();
                model = await _service.GetAllowanceMonthComparison(month, groupCom);
                return Json(model.AllowanceMonthComparison);


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

        public async Task<IActionResult> GetDeductionMonthComparison(int month, int groupCom)
        {
            try
            {
                MISDashBoard model = new MISDashBoard();
                model = await _service.GetDeductionMonthComparison(month, groupCom);
                return Json(model.DeductionMonthComparison);


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

        public async Task<IActionResult> GetSalaryAllowance(int finCode, int groupCom)
        {
            try
            {
                MISDashBoard model = new MISDashBoard();
                model = await _service.GetSalaryAllowance(finCode, groupCom);
                return Json(model.SalaryAllowanceList);


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

        public async Task<IActionResult> GetSalaryDeduction(int finCode, int groupCom)
        {
            try
            {
                MISDashBoard model = new MISDashBoard();
                model = await _service.GetSalaryDeduction(finCode, groupCom);
                return Json(model.SalaryDeductionList);


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

        #endregion

        #region Absenteesm
        public async Task<MISDashBoard> GetAbsRate(string from, string to)
        {
            MISDashBoard model = new MISDashBoard();
            try
            {


                var abs = await _service.GetAbsRate(from, to);
                model.absRate = abs.absRate;

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

        public async Task<IActionResult> GetAbsMonth(string from, string to)
        {
            try
            {
                MISDashBoard model = new MISDashBoard();
                model = await _service.GetAbsMonth(from, to);
                return Json(model.AbsMonthWise);


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

        public async Task<IActionResult> GetAbsAge(string from, string to)
        {
            try
            {
                MISDashBoard model = new MISDashBoard();
                model = await _service.GetAbsAge(from, to);
                return Json(model.AgeBandAbsList);


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

        public async Task<IActionResult> GetAbsTenure(string from, string to)
        {
            try
            {
                MISDashBoard model = new MISDashBoard();
                model = await _service.GetAbsTenure(from, to);
                return Json(model.TenureAbsList);


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
        public async Task<IActionResult> GetAbsDepartment(string from, string to)
        {
            try
            {
                MISDashBoard model = new MISDashBoard();
                model = await _service.GetAbsDepartment(from, to);
                return Json(model.AbsDepartmentList);


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
        public async Task<IActionResult> GetAbsLocation(string from, string to)
        {
            try
            {
                MISDashBoard model = new MISDashBoard();
                model = await _service.GetAbsLocation(from, to);
                return Json(model.AbsLocationList);


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
        public async Task<IActionResult> GetAbsDivision(string from, string to)
        {
            try
            {
                MISDashBoard model = new MISDashBoard();
                model = await _service.GetAbsDivision(from, to);
                return Json(model.AbsDivisionList);


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
        public async Task<IActionResult> GetAbsCategory(string from, string to)
        {
            try
            {
                MISDashBoard model = new MISDashBoard();
                model = await _service.GetAbsCategory(from, to);
                return Json(model.AbsCategoryList);


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
        public async Task<IActionResult> GetAbsGrade(string from, string to)
        {
            try
            {
                MISDashBoard model = new MISDashBoard();
                model = await _service.GetAbsGrade(from, to);
                return Json(model.AbsGradeList);


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
        public async Task<IActionResult> GetAbsRegion(string from, string to)
        {
            try
            {
                MISDashBoard model = new MISDashBoard();
                model = await _service.GetAbsRegion(from, to);
                return Json(model.AbsRegionList);


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

        public async Task<IActionResult> GetAbsDays(string from, string to)
        {
            try
            {
                MISDashBoard model = new MISDashBoard();
                model = await _service.GetAbsDays(from, to);
                return Json(model.GetAbsDaysList);


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
        //---------------------------
        public async Task<IActionResult> GetAbsMonthWiseDetail([DataSourceRequest] DataSourceRequest request, DateTime FROMDATE, DateTime TODATE, int GROUPCOMPANYCODE, int MONTHCODE)
        {


            MISDashBoard model = new MISDashBoard();
            List<ABSEENTISM> modellist = new List<ABSEENTISM>();

            model = await _service.GetAbsMonthWiseDetail(FROMDATE, TODATE, GROUPCOMPANYCODE, MONTHCODE);

            modellist = model.ABSEENTISM;

            var dataResult1 = modellist.ToDataSourceResult(request);


            return Json(dataResult1, new JsonSerializerSettings { ContractResolver = new DefaultContractResolver() });


        }

        public async Task<IActionResult> GetAbsAgeSlabWiseDetail([DataSourceRequest] DataSourceRequest request, DateTime FROMDATE, DateTime TODATE, int GROUPCOMPANYCODE, string AGESLAB)
        {


            MISDashBoard model = new MISDashBoard();
            List<ABSEENTISM> modellist = new List<ABSEENTISM>();

            model = await _service.GetAbsAgeSlabWiseDetail(FROMDATE, TODATE, GROUPCOMPANYCODE, AGESLAB);

            modellist = model.ABSEENTISM;

            var dataResult1 = modellist.ToDataSourceResult(request);


            return Json(dataResult1, new JsonSerializerSettings { ContractResolver = new DefaultContractResolver() });


        }

        public async Task<IActionResult> GetAbsServiceDetail([DataSourceRequest] DataSourceRequest request, DateTime FROMDATE, DateTime TODATE, int GROUPCOMPANYCODE, string YEARSLAB)
        {


            MISDashBoard model = new MISDashBoard();
            List<ABSEENTISM> modellist = new List<ABSEENTISM>();

            model = await _service.GetAbsServiceDetail(FROMDATE, TODATE, GROUPCOMPANYCODE, YEARSLAB);

            modellist = model.ABSEENTISM;

            var dataResult1 = modellist.ToDataSourceResult(request);


            return Json(dataResult1, new JsonSerializerSettings { ContractResolver = new DefaultContractResolver() });


        }

        public async Task<IActionResult> GetAbsDepartmentDetail([DataSourceRequest] DataSourceRequest request, DateTime FROMDATE, DateTime TODATE, int GROUPCOMPANYCODE, string DEPARTMENT)
        {


            MISDashBoard model = new MISDashBoard();
            List<ABSEENTISM> modellist = new List<ABSEENTISM>();

            model = await _service.GetAbsDepartmentDetail(FROMDATE, TODATE, GROUPCOMPANYCODE, DEPARTMENT);

            modellist = model.ABSEENTISM;

            var dataResult1 = modellist.ToDataSourceResult(request);


            return Json(dataResult1, new JsonSerializerSettings { ContractResolver = new DefaultContractResolver() });


        }

        public async Task<IActionResult> GetAbsLocationDetail([DataSourceRequest] DataSourceRequest request, DateTime FROMDATE, DateTime TODATE, int GROUPCOMPANYCODE, string LOCATION)
        {


            MISDashBoard model = new MISDashBoard();
            List<ABSEENTISM> modellist = new List<ABSEENTISM>();

            model = await _service.GetAbsLocationDetail(FROMDATE, TODATE, GROUPCOMPANYCODE, LOCATION);

            modellist = model.ABSEENTISM;

            var dataResult1 = modellist.ToDataSourceResult(request);


            return Json(dataResult1, new JsonSerializerSettings { ContractResolver = new DefaultContractResolver() });


        }

        public async Task<IActionResult> GetAbsWeekdayDetail([DataSourceRequest] DataSourceRequest request, DateTime FROMDATE, DateTime TODATE, int GROUPCOMPANYCODE, string DAY)
        {


            MISDashBoard model = new MISDashBoard();
            List<ABSEENTISM> modellist = new List<ABSEENTISM>();

            model = await _service.GetAbsWeekdayDetail(FROMDATE, TODATE, GROUPCOMPANYCODE, DAY);

            modellist = model.ABSEENTISM;

            var dataResult1 = modellist.ToDataSourceResult(request);


            return Json(dataResult1, new JsonSerializerSettings { ContractResolver = new DefaultContractResolver() });


        }


        public async Task<IActionResult> GetAbsDivisionDetail([DataSourceRequest] DataSourceRequest request, DateTime FROMDATE, DateTime TODATE, int GROUPCOMPANYCODE, string DIVISION)
        {


            MISDashBoard model = new MISDashBoard();
            List<ABSEENTISM> modellist = new List<ABSEENTISM>();

            model = await _service.GetAbsDivisionDetail(FROMDATE, TODATE, GROUPCOMPANYCODE, DIVISION);

            modellist = model.ABSEENTISM;

            var dataResult1 = modellist.ToDataSourceResult(request);


            return Json(dataResult1, new JsonSerializerSettings { ContractResolver = new DefaultContractResolver() });


        }
        public async Task<IActionResult> GetAbsCategoryDetail([DataSourceRequest] DataSourceRequest request, DateTime FROMDATE, DateTime TODATE, int GROUPCOMPANYCODE, string CATEGORY)
        {


            MISDashBoard model = new MISDashBoard();
            List<ABSEENTISM> modellist = new List<ABSEENTISM>();

            model = await _service.GetAbsCategoryDetail(FROMDATE, TODATE, GROUPCOMPANYCODE, CATEGORY);

            modellist = model.ABSEENTISM;

            var dataResult1 = modellist.ToDataSourceResult(request);


            return Json(dataResult1, new JsonSerializerSettings { ContractResolver = new DefaultContractResolver() });


        }
        public async Task<IActionResult> GetAbsRegionDetail([DataSourceRequest] DataSourceRequest request, DateTime FROMDATE, DateTime TODATE, int GROUPCOMPANYCODE, string REGION)
        {


            MISDashBoard model = new MISDashBoard();
            List<ABSEENTISM> modellist = new List<ABSEENTISM>();

            model = await _service.GetAbsRegionDetail(FROMDATE, TODATE, GROUPCOMPANYCODE, REGION);

            modellist = model.ABSEENTISM;

            var dataResult1 = modellist.ToDataSourceResult(request);


            return Json(dataResult1, new JsonSerializerSettings { ContractResolver = new DefaultContractResolver() });


        }
        public async Task<IActionResult> GetAbsGradeDetail([DataSourceRequest] DataSourceRequest request, DateTime FROMDATE, DateTime TODATE, int GROUPCOMPANYCODE, string GRADE)
        {


            MISDashBoard model = new MISDashBoard();
            List<ABSEENTISM> modellist = new List<ABSEENTISM>();

            model = await _service.GetAbsGradeDetail(FROMDATE, TODATE, GROUPCOMPANYCODE, GRADE);

            modellist = model.ABSEENTISM;

            var dataResult1 = modellist.ToDataSourceResult(request);


            return Json(dataResult1, new JsonSerializerSettings { ContractResolver = new DefaultContractResolver() });


        }
        #endregion

        #region Historical Trends
        public async Task<IActionResult> GetHistoricalJoiningCount(int groupCom)
        {
            try
            {
                MISDashBoard model = new MISDashBoard();
                model = await _service.GetHistoricalJoiningCount(groupCom);
                return Json(model.GetHistoricalJoiningCountList);


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
        public async Task<IActionResult> GetHistoricalWFH(int groupCom)
        {
            try
            {
                MISDashBoard model = new MISDashBoard();
                model = await _service.GetHistoricalWFH(groupCom);

                return Json(model.HistoricalWFHList);



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
        public async Task<IActionResult> GetHistoricalLOP(int groupCom)
        {
            try
            {
                MISDashBoard model = new MISDashBoard();
                model = await _service.GetHistoricalWFH(groupCom);

                return Json(model.HistoricalLOPList);



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
        public async Task<IActionResult> GetHistoricalSalary(int groupCom)
        {
            try
            {
                MISDashBoard model = new MISDashBoard();
                model = await _service.GetHistoricalSalary(groupCom);

                return Json(model.HistoricalSalaryList);



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

        public async Task<IActionResult> GetHistoricalSalaryInc(int groupCom)
        {
            try
            {
                MISDashBoard model = new MISDashBoard();
                model = await _service.GetHistoricalSalaryInc(groupCom);

                return Json(model.HistoricalSalaryIncList);



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

        public async Task<IActionResult> GetHistoricalTerminationCount(int groupCom)
        {
            try
            {
                MISDashBoard model = new MISDashBoard();
                model = await _service.GetHistoricalTerminationCount(groupCom);
                return Json(model.GetHistoricalTerminationCountList);


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

        public async Task<IActionResult> GetHistoricalBadHiresCount(int groupCom)
        {
            try
            {
                MISDashBoard model = new MISDashBoard();
                model = await _service.GetHistoricalBadHiresCount(groupCom);
                return Json(model.HistoricalBadHiresList);


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

        public async Task<IActionResult> GetHistoricalJoiningCountMonth()
        {
            try
            {
                MISDashBoard model = new MISDashBoard();
                model = await _service.GetHistoricalJoiningCountMonth();
                return Json(model.GetHistoricalJoiningCountMonthList);


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

        public async Task<IActionResult> GetHistoricalTerminationsCountMonth()
        {
            try
            {
                MISDashBoard model = new MISDashBoard();
                model = await _service.GetHistoricalTerminationsCountMonth();
                return Json(model.GetHistoricalTerminationsCountMonthList);


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

        public async Task<IActionResult> GetHistoricalAttritionMonth()
        {
            try
            {
                MISDashBoard model = new MISDashBoard();
                model = await _service.GetHistoricalAttritionMonth();
                return Json(model.GetHistoricalAttritionMonthList);


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

        #region List

        public async Task<IActionResult> GetHistoricalList([DataSourceRequest] DataSourceRequest request, int groupCom, string type, string age)
        {


            MISDashBoard model = new MISDashBoard();
            List<AgeResignationCountList> modellist = new List<AgeResignationCountList>();

            model = await _service.GetHistoricalList(groupCom, type, age);

            modellist = model.AgeResignationCountList;

            var dataResult1 = modellist.ToDataSourceResult(request);


            return Json(dataResult1, new JsonSerializerSettings { ContractResolver = new DefaultContractResolver() });


        }

        #endregion



        #endregion

        #region Recruitment Analysis

        public async Task<MISDashBoard> GetRecruitmentInformation(int finCode, int groupCom)
        {

            MISDashBoard model = new MISDashBoard();
            try
            {

                var empData = await _service.GetRecruitmentInformation(Convert.ToInt32(finCode), groupCom);
                model.vacancy = empData.vacancy;
                model.offered = empData.offered;
                model.interviewed = empData.interviewed;
                model.joined = empData.joined;

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

        public async Task<IActionResult> GetRecruitmentVacancyTrend(int finCode, int groupCom)
        {
            try
            {
                MISDashBoard model = new MISDashBoard();
                model = await _service.GetRecruitmentVacancyTrend(finCode, groupCom);
                return Json(model.VacancyTrendList);


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

        public async Task<IActionResult> GetTopVacancyDesignation(int finCode, int groupCom)
        {
            try
            {
                MISDashBoard model = new MISDashBoard();
                model = await _service.GetTopVacancyDesignation(finCode, groupCom);
                return Json(model.TopVacancyDesignationList);


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
        public async Task<IActionResult> GetVacancyDetails([DataSourceRequest] DataSourceRequest request, int FinancialYear, int GrpCompanyCode, string MONTH)
        {


            MISDashBoard model = new MISDashBoard();
            List<RECRUITMENT> modellist = new List<RECRUITMENT>();

            model = await _service.GetVacancyDetails(FinancialYear, GrpCompanyCode, MONTH);

            modellist = model.RECRUITMENT;

            var dataResult1 = modellist.ToDataSourceResult(request);


            return Json(dataResult1, new JsonSerializerSettings { ContractResolver = new DefaultContractResolver() });


        }


        public async Task<IActionResult> GetRecruitmentInformations([DataSourceRequest] DataSourceRequest request, int FinancialYear, int GrpCompanyCode, string TYPE)
        {


            MISDashBoard model = new MISDashBoard();
            List<RECRUITMENT> modellist = new List<RECRUITMENT>();

            model = await _service.GetRecruitmentInformations(FinancialYear, GrpCompanyCode, TYPE);

            modellist = model.RECRUITMENT;

            var dataResult1 = modellist.ToDataSourceResult(request);


            return Json(dataResult1, new JsonSerializerSettings { ContractResolver = new DefaultContractResolver() });


        }
        public async Task<IActionResult> GetVacancyDesignationDetails([DataSourceRequest] DataSourceRequest request, int FinancialYear, int GrpCompanyCode, string TYPE)
        {


            MISDashBoard model = new MISDashBoard();
            List<RECRUITMENT> modellist = new List<RECRUITMENT>();

            model = await _service.GetVacancyDesignationDetails(FinancialYear, GrpCompanyCode, TYPE);

            modellist = model.RECRUITMENT;

            var dataResult1 = modellist.ToDataSourceResult(request);


            return Json(dataResult1, new JsonSerializerSettings { ContractResolver = new DefaultContractResolver() });


        }

        #endregion

        #region HR at Glance

        public async Task<IActionResult> GetTimeToQuitJobHR(int groupCom)
        {


            try
            {

                var model = await _service.GetTimeToQuitJobHR(groupCom);

                return Json(model.HRTimetoQuitJob);
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

        public async Task<IActionResult> GetSalaryRange(int groupCom)
        {


            try
            {

                var model = await _service.GetSalaryRange(groupCom);

                return Json(model.SalaryRangeList);
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
        public async Task<IActionResult> GetGenderHR(int groupCom)
        {


            try
            {

                var model = await _service.GetGenderHR(groupCom);

                return Json(model.HRGenderList);
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

        public async Task<IActionResult> GetDepartmentHR(int groupCom)
        {


            try
            {

                var model = await _service.GetDepartmentHR(groupCom);

                return Json(model.HRDepartmentList);
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

        public async Task<IActionResult> GetRegionHR(int groupCom)
        {


            try
            {

                var model = await _service.GetRegionHR(groupCom);

                return Json(model.HRRegionList);
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

        public async Task<IActionResult> GetDivisionHR(int groupCom)
        {


            try
            {

                var model = await _service.GetDivisionHR(groupCom);

                return Json(model.HRDivisionList);
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

        public async Task<IActionResult> GetGradeHR(int groupCom)
        {


            try
            {

                var model = await _service.GetGradeHR(groupCom);

                return Json(model.HRGradeList);
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

        public async Task<IActionResult> GetCategoryHR(int groupCom)
        {


            try
            {

                var model = await _service.GetCategoryHR(groupCom);

                return Json(model.HRCategoryList);
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

        public async Task<IActionResult> GetAgeHR(int groupCom)
        {


            try
            {

                var model = await _service.GetAgeHR(groupCom);

                return Json(model.HRAgeList);
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

        public async Task<IActionResult> GetTenureHR(int groupCom)
        {


            try
            {

                var model = await _service.GetTenureHR(groupCom);

                return Json(model.HRTenureList);
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

        public async Task<IActionResult> GetLocationHR(int groupCom)
        {


            try
            {

                var model = await _service.GetLocationHR(groupCom);

                return Json(model.HRLocationList);
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

        #region List

        public async Task<IActionResult> GetActiveEmployeeCountList([DataSourceRequest] DataSourceRequest request, int groupCom, string type, string age)
        {


            MISDashBoard model = new MISDashBoard();
            List<AgeResignationCountList> modellist = new List<AgeResignationCountList>();

            model = await _service.GetActiveEmployeeCountList(groupCom, type, age);

            modellist = model.AgeResignationCountList;

            var dataResult1 = modellist.ToDataSourceResult(request);


            return Json(dataResult1, new JsonSerializerSettings { ContractResolver = new DefaultContractResolver() });


        }

        #endregion



        #endregion

        #region Workforce Time Trends

        public async Task<IActionResult> GetTimeOfficeHR(int groupCom)
        {


            try
            {

                var model = await _service.GetTimeOfficeHR(groupCom);

                return Json(model.HRTimeOfficeList);
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

        public async Task<IActionResult> GetTODepartmentHR(int groupCom)
        {


            try
            {

                var model = await _service.GetTODepartmentHR(groupCom);

                return Json(model.HRTODepartmentList);
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

        public async Task<IActionResult> GetTOLocationHR(int groupCom)
        {


            try
            {

                var model = await _service.GetTOLocationHR(groupCom);

                return Json(model.HRTOLocationList);
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

        public async Task<IActionResult> GetTODivisionHR(int groupCom)
        {


            try
            {

                var model = await _service.GetTODivisionHR(groupCom);

                return Json(model.HRTODivisionList);
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

        public async Task<IActionResult> GetTOCategoryHR(int groupCom)
        {


            try
            {

                var model = await _service.GetTOCategoryHR(groupCom);

                return Json(model.HRTOCategoryList);
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

        public async Task<IActionResult> GetTOGradeHR(int groupCom)
        {


            try
            {

                var model = await _service.GetTOGradeHR(groupCom);

                return Json(model.HRTOGradeList);
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

        public async Task<IActionResult> GetTORegionHR(int groupCom)
        {


            try
            {

                var model = await _service.GetTORegionHR(groupCom);

                return Json(model.HRTORegionList);
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

        #endregion


        #region Attendance OverView

        public async Task<IActionResult> GetMonths(int FinancialYrCode)
        {
            MISDashBoard model = new MISDashBoard();
            model = await _service.GetMonths(FinancialYrCode);
            ViewBag.FinancialYr = new SelectList(await _dropdownService.GetFinancialYearDropdown(), "Code", "Name");

            return PartialView("~/Areas/MIS/Views/MISDashBoard/_Months.cshtml", model);
        }

        public async Task<MISDashBoard> GetAttendanceMonthData(int month, int groupCom)
        {
            MISDashBoard model = new MISDashBoard();


            try
            {

                var empData = await _service.GetAttendanceMonthData(month, groupCom);

                model.netPay = empData.netPay;
                model.lateIn = empData.lateIn;
                model.earlyOut = empData.earlyOut;
                model.ot = empData.ot;
                model.th = empData.th;

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

        public async Task<IActionResult> GetAttendanceDayWise(int month, int groupCom)
        {


            try
            {

                var model = await _service.GetAttendanceDayWise(month, groupCom);

                return Json(model.AttendanceDayWiseList);
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

        public async Task<IActionResult> GetAttendanceDateWise(int day, int month, int groupCom)
        {


            try
            {

                var model = await _service.GetAttendanceDateWise(day, month, groupCom);

                return Json(model.AttendanceDateWiseList);
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

        #region List
        public async Task<IActionResult> GetAttendanceDateList([DataSourceRequest] DataSourceRequest request, int day, int month, int groupCom)
        {
            MISDashBoard model = new MISDashBoard();
            List<AttendanceDateList> modellist = new List<AttendanceDateList>();

            model = await _service.GetAttendanceDateList(day, month, groupCom);

            modellist = model.AttendanceDateList;

            var dataResult1 = modellist.ToDataSourceResult(request);


            return Json(dataResult1, new JsonSerializerSettings { ContractResolver = new DefaultContractResolver() });

        }

        #endregion

        #endregion


        #region PayRollOverView

        public async Task<MISDashBoard> GetEmpData(int month, int groupCom)
        {
            MISDashBoard model = new MISDashBoard();
            try
            {

                var empData = await _service.GetEmpData(month, groupCom);
                model.totalEmp = empData.totalEmp;
                model.totalPrvEmp = empData.totalPrvEmp;
                model.newHires = empData.newHires;
                model.resignations = empData.resignations;
                model.difference = empData.difference;
                model.totalEmpMale = empData.totalEmpMale;
                model.totalEmpFemale = empData.totalEmpFemale;
                model.pfEmployer = empData.pfEmployer;
                model.pfEmployee = empData.pfEmployee;
                model.esiEmployer = empData.esiEmployer;
                model.esiEmployee = empData.esiEmployee;

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

        public async Task<IActionResult> GetPRAllowance(int month, int groupCom)
        {


            try
            {

                var model = await _service.GetPRAllowance(month, groupCom);

                return Json(model.AllowanceOverView);
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

        public async Task<IActionResult> GetPRDeduction(int month, int groupCom)
        {


            try
            {

                var model = await _service.GetPRDeduction(month, groupCom);

                return Json(model.DeductionOverView);
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

        public async Task<IActionResult> GetPRCTC(int month, int groupCom)
        {


            try
            {

                var model = await _service.GetPRCTC(month, groupCom);

                return Json(model.CTCOverView);
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

        public async Task<IActionResult> GetPG(int month, int groupCom)
        {


            try
            {

                var model = await _service.GetPG(month, groupCom);

                return Json(model.PGOverView);
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

        #region List

        public async Task<IActionResult> GetPFDatas([DataSourceRequest] DataSourceRequest request, int month, int groupCom)
        {


            MISDashBoard model = new MISDashBoard();
            List<PFEmployeeCount> modellist = new List<PFEmployeeCount>();

            model = await _service.GetPFDatas(month, groupCom);

            modellist = model.PFEmployeeCountList;

            var dataResult1 = modellist.ToDataSourceResult(request);


            return Json(dataResult1, new JsonSerializerSettings { ContractResolver = new DefaultContractResolver() });


        }

        public async Task<IActionResult> GetAlwData([DataSourceRequest] DataSourceRequest request, int month, int groupCom, string type)
        {


            MISDashBoard model = new MISDashBoard();
            List<PFEmployeeCount> modellist = new List<PFEmployeeCount>();

            model = await _service.GetAlwData(month, groupCom, type);

            modellist = model.PFEmployeeCountList;

            var dataResult1 = modellist.ToDataSourceResult(request);


            return Json(dataResult1, new JsonSerializerSettings { ContractResolver = new DefaultContractResolver() });


        }

        #endregion

        #endregion


        #region Leave Overview

        public async Task<IActionResult> GetLeaveType(int month, int groupCom)
        {


            try
            {

                var model = await _service.GetLeaveType(month, groupCom);

                return Json(model.CatagoryLeaveCountList);
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
        public async Task<IActionResult> GetLeaveDays(int month, int groupCom)
        {


            try
            {

                var model = await _service.GetLeaveDays(month, groupCom);

                return Json(model.GetLeaveDaysList);
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
        public async Task<IActionResult> GetLeaveDepartment(int month, int groupCom)
        {


            try
            {

                var model = await _service.GetLeaveDepartment(month, groupCom);

                return Json(model.DepartmentCountList);
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

        public async Task<IActionResult> GetLeaveLocation(int month, int groupCom)
        {


            try
            {

                var model = await _service.GetLeaveLocation(month, groupCom);

                return Json(model.LocationCountList);
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

        public async Task<IActionResult> GetLeaveDivision(int month, int groupCom)
        {


            try
            {

                var model = await _service.GetLeaveDivision(month, groupCom);

                return Json(model.DivisionCountList);
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

        public async Task<IActionResult> GetLeaveCategory(int month, int groupCom)
        {


            try
            {

                var model = await _service.GetLeaveCategory(month, groupCom);

                return Json(model.CategoryCountList);
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

        public async Task<IActionResult> GetLeaveGrade(int month, int groupCom)
        {


            try
            {

                var model = await _service.GetLeaveGrade(month, groupCom);

                return Json(model.GradeCountList);
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

        public async Task<IActionResult> GetLeaveRegion(int month, int groupCom)
        {


            try
            {

                var model = await _service.GetLeaveRegion(month, groupCom);

                return Json(model.RegionCountList);
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

        #endregion
        #region Late In Overview

        public async Task<IActionResult> GetLateInDepartment(int month, int groupCom)
        {


            try
            {

                var model = await _service.GetLateInDepartment(month, groupCom);

                return Json(model.HRTODepartmentList);
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

        public async Task<IActionResult> GetLateInLocation(int month, int groupCom)
        {


            try
            {

                var model = await _service.GetLateInLocation(month, groupCom);

                return Json(model.HRTOLocationList);
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

        public async Task<IActionResult> GetLateInDivision(int month, int groupCom)
        {


            try
            {

                var model = await _service.GetLateInDivision(month, groupCom);

                return Json(model.HRTODivisionList);
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

        public async Task<IActionResult> GetLateInCategory(int month, int groupCom)
        {


            try
            {

                var model = await _service.GetLateInCategory(month, groupCom);

                return Json(model.HRTOCategoryList);
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

        public async Task<IActionResult> GetLateInGrade(int month, int groupCom)
        {


            try
            {

                var model = await _service.GetLateInGrade(month, groupCom);

                return Json(model.HRTOGradeList);
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

        public async Task<IActionResult> GetLateInRegion(int month, int groupCom)
        {


            try
            {

                var model = await _service.GetLateInRegion(month, groupCom);

                return Json(model.HRTORegionList);
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


        public async Task<IActionResult> GetAttendanceList([DataSourceRequest] DataSourceRequest request, int groupCom, int month, string type, int code)
        {
            MISDashBoard model = new MISDashBoard();
            List<AttendanceDateList> modellist = new List<AttendanceDateList>();

            model = await _service.GetAttendanceList(groupCom, month, type, code);

            modellist = model.AttendanceDateList;

            var dataResult1 = modellist.ToDataSourceResult(request);


            return Json(dataResult1, new JsonSerializerSettings { ContractResolver = new DefaultContractResolver() });

        }


        #endregion

        #region Employee Over View


        public async Task<MISDashBoard> GetAttendanceEmpMonthData(int month, int empCode)
        {
            MISDashBoard model = new MISDashBoard();
            try
            {

                var empData = await _service.GetAttendanceEmpMonthData(month, empCode);
                model.allowance = empData.allowance;
                model.deduction = empData.deduction;
                model.netPay = empData.netPay;
                model.lateIn = empData.lateIn;
                model.earlyOut = empData.earlyOut;
                model.ot = empData.ot;
                model.th = empData.th;

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

        public async Task<IActionResult> GetEmployeesListOv([DataSourceRequest] DataSourceRequest request, int month, int groupCom)
        {

            MISDashBoard model = new MISDashBoard();
            List<Employee_MIS_List> modellist = new List<Employee_MIS_List>();

            model = await _service.GetEmployeeListOv(month, groupCom);

            modellist = model.Employee_MIS_List;

            var dataResult1 = modellist.ToDataSourceResult(request);

            return Json(dataResult1, new JsonSerializerSettings { ContractResolver = new DefaultContractResolver() });

        }

        public async Task<IActionResult> GetAttendanceEmpDayWise(int month, int empCode)
        {


            try
            {

                var model = await _service.GetAttendanceEmpDayWise(month, empCode);

                model.AttendanceDayWiseList.Remove(null);

                return Json(model.AttendanceDayWiseList);
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

        public async Task<IActionResult> GetEmployeeLateInEarlyOut(int month, int empCode)
        {


            try
            {

                var model = await _service.GetEmployeeLateInEarlyOut(month, empCode);



                return Json(model.EmployeeLateInEarlyOut);
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


        #endregion

        #region Grievance



        public async Task<IActionResult> GetGrievance(int grpCode, int fYear)
        {
            try
            {
                MISDashBoard model = new MISDashBoard();
                model = await _service.GetGrievanceByFinancialYear(grpCode, fYear);

                return Json(model.Grievence);



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

        public async Task<IActionResult> GetGrievanceCompleted(int grpCode, int fYear)
        {
            try
            {
                MISDashBoard model = new MISDashBoard();
                model = await _service.GetGrievanceCompletedByFinancialYear(grpCode, fYear);

                return Json(model.Grievence);



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

        public async Task<IActionResult> GetGrievanceAccepted(int grpCode, int fYear)
        {
            try
            {
                MISDashBoard model = new MISDashBoard();
                model = await _service.GetGrievanceAcceptedByFinancialYear(grpCode, fYear);

                return Json(model.Grievence);



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
        public async Task<IActionResult> GetGrievanceNonAccepted(int grpCode, int fYear)
        {
            try
            {
                MISDashBoard model = new MISDashBoard();
                model = await _service.GetGrievanceNonAcceptedByFinancialYear(grpCode, fYear);

                return Json(model.Grievence);



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


        public async Task<IActionResult> GetGrievanceRejected(int grpCode, int fYear)
        {
            try
            {
                MISDashBoard model = new MISDashBoard();
                model = await _service.GetGrievanceRejectedByFinancialYear(grpCode, fYear);

                return Json(model.Grievence);



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

        #region List

        public async Task<IActionResult> GetTotalGrievanceEmployeeCountList([DataSourceRequest] DataSourceRequest request, int GrievanceCatid, int FinancialYear, int GrpCompanyCode, string TYPE)
        {


            MISDashBoard model = new MISDashBoard();
            List<MISGrievenceList> modellist = new List<MISGrievenceList>();

            model = await _service.GetGrievenceCountGraphDataView(GrievanceCatid, FinancialYear, GrpCompanyCode, TYPE);

            modellist = model.MISGrievenceList;

            var dataResult1 = modellist.ToDataSourceResult(request);


            return Json(dataResult1, new JsonSerializerSettings { ContractResolver = new DefaultContractResolver() });


        }

        #endregion

        #endregion

        #region Transition Type

        public async Task<IActionResult> GetTransitionTypeCount(int groupCom, int financialYr, int typeCode)
        {
            try
            {
                MISDashBoard model = new MISDashBoard();
                model = await _service.GetTransitionTypeCount(groupCom, financialYr, typeCode);

                return Json(model.TransitionTypeCountList);
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

        public async Task<IActionResult> GetTransitionTypeDepartment(int groupCom, int financialYr, int typeCode)
        {
            try
            {
                MISDashBoard model = new MISDashBoard();
                model = await _service.GetTransitionTypeDepartment(groupCom, financialYr, typeCode);

                return Json(model.TransitionTypeDepartmentList);
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
        public async Task<IActionResult> GetTransitionTypeLocation(int groupCom, int financialYr, int typeCode)
        {
            try
            {
                MISDashBoard model = new MISDashBoard();
                model = await _service.GetTransitionTypeLocation(groupCom, financialYr, typeCode);

                return Json(model.TransitionTypeLocationList);
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
        public async Task<IActionResult> GetTransitionTypeDivision(int groupCom, int financialYr, int typeCode)
        {
            try
            {
                MISDashBoard model = new MISDashBoard();
                model = await _service.GetTransitionTypeDivision(groupCom, financialYr, typeCode);

                return Json(model.TransitionTypeDivisionList);
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
        public async Task<IActionResult> GetTransitionTypeCategory(int groupCom, int financialYr, int typeCode)
        {
            try
            {
                MISDashBoard model = new MISDashBoard();
                model = await _service.GetTransitionTypeCategory(groupCom, financialYr, typeCode);

                return Json(model.TransitionTypeCategoryList);
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
        public async Task<IActionResult> GetTransitionTypeGrade(int groupCom, int financialYr, int typeCode)
        {
            try
            {
                MISDashBoard model = new MISDashBoard();
                model = await _service.GetTransitionTypeGrade(groupCom, financialYr, typeCode);

                return Json(model.TransitionTypeGradeList);
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
        public async Task<IActionResult> GetTransitionTypeRegion(int groupCom, int financialYr, int typeCode)
        {
            try
            {
                MISDashBoard model = new MISDashBoard();
                model = await _service.GetTransitionTypeRegion(groupCom, financialYr, typeCode);

                return Json(model.TransitionTypeRegionList);
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


        #region List

        public async Task<IActionResult> GetTransitionEmployeeCount([DataSourceRequest] DataSourceRequest request, int groupCom, int financialYr, int typeCode, string type, int text)
        {


            MISDashBoard model = new MISDashBoard();
            List<TransitionEmployeeCount> modellist = new List<TransitionEmployeeCount>();

            model = await _service.GetTransitionEmployeeCount(groupCom, financialYr, typeCode, type, text);

            modellist = model.TransitionEmployeeCountList;

            var dataResult1 = modellist.ToDataSourceResult(request);


            return Json(dataResult1, new JsonSerializerSettings { ContractResolver = new DefaultContractResolver() });


        }

        #endregion

        #endregion
        #region BAD_HIRES
        public async Task<IActionResult> GetBadHiresMonth(int groupCom, int financialYr)
        {
            try
            {
                MISDashBoard model = new MISDashBoard();
                model = await _service.GetBadHiresMonth(groupCom, financialYr);

                return Json(model.BadHiresMonthList);
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
        public async Task<IActionResult> GetBadHiresAge(int groupCom, int financialYr)
        {
            try
            {
                MISDashBoard model = new MISDashBoard();
                model = await _service.GetBadHiresAge(groupCom, financialYr);

                return Json(model.BadHiresAgeList);
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
        public async Task<IActionResult> GetBadHiresGender(int groupCom, int financialYr)
        {
            try
            {
                MISDashBoard model = new MISDashBoard();
                model = await _service.GetBadHiresGender(groupCom, financialYr);

                return Json(model.BadHiresGenderList);
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

        public async Task<IActionResult> GetBadHiresDepartment(int groupCom, int financialYr)
        {
            try
            {
                MISDashBoard model = new MISDashBoard();
                model = await _service.GetBadHiresDepartment(groupCom, financialYr);

                return Json(model.BadHiresDepartmentList);
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
        public async Task<IActionResult> GetBadHiresLocation(int groupCom, int financialYr)
        {
            try
            {
                MISDashBoard model = new MISDashBoard();
                model = await _service.GetBadHiresLocation(groupCom, financialYr);

                return Json(model.BadHiresLocationList);
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
        public async Task<IActionResult> GetBadHiresDivision(int groupCom, int financialYr)
        {
            try
            {
                MISDashBoard model = new MISDashBoard();
                model = await _service.GetBadHiresDivision(groupCom, financialYr);

                return Json(model.BadHiresDivisionList);
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
        public async Task<IActionResult> GetBadHiresCategory(int groupCom, int financialYr)
        {
            try
            {
                MISDashBoard model = new MISDashBoard();
                model = await _service.GetBadHiresCategory(groupCom, financialYr);

                return Json(model.BadHiresCategoryList);
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
        public async Task<IActionResult> GetBadHiresGrade(int groupCom, int financialYr)
        {
            try
            {
                MISDashBoard model = new MISDashBoard();
                model = await _service.GetBadHiresGrade(groupCom, financialYr);

                return Json(model.BadHiresGradeList);
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
        public async Task<IActionResult> GetBadHiresRegion(int groupCom, int financialYr)
        {
            try
            {
                MISDashBoard model = new MISDashBoard();
                model = await _service.GetBadHiresRegion(groupCom, financialYr);

                return Json(model.BadHiresRegionList);
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

        public async Task<IActionResult> GetBadHiresEmpList([DataSourceRequest] DataSourceRequest request, int groupCom, int financialYr, string type, string code)
        {


            MISDashBoard model = new MISDashBoard();
            List<BadHiresEmp> modellist = new List<BadHiresEmp>();

            model = await _service.GetBadHiresEmpList(groupCom, financialYr, type, code);

            modellist = model.BadHiresEmpList;

            var dataResult1 = modellist.ToDataSourceResult(request);


            return Json(dataResult1, new JsonSerializerSettings { ContractResolver = new DefaultContractResolver() });


        }
        #endregion

        public async Task<IActionResult> ReloadDiv(int id)
        {
            MISDashBoard model = new MISDashBoard();
            try
            {
                //var attendanceTask = _dropdownService.GetAttendanceDropdown();
                ViewBag.LeavePeriod = new SelectList(await _dropdownService.GetLeavePeriodFilterDropdown(), "Code", "Name");
                //ViewBag.Attendance = new SelectList(await attendanceTask, "Code", "Name");
                ViewBag.Attendance = new SelectList(await _dropdownService.GetAttendanceDropdown(), "Code", "Name");
                var code = new SelectList(await _dropdownService.GetAttendanceDropdown(), "Code", "Name").FirstOrDefault();

                

              
                var response = await _commonService.GetCurrentLeavePeriod();
                var finYear = await _commonService.GetCurrentLFinancialYear();
                var currntLp = Convert.ToInt32(response.Data);
                var currntFY = Convert.ToInt32(finYear.Data);
                model.LeavePeriod = new Shared.Master.LeavePeriod();
                model.LeavePeriod.Code = currntLp;
                model.finYear = currntFY;

                int AttendanceCode = int.Parse(code.Value);
                int financialYr = finYear.Data;

                var AttendanceData = await _service.GetAttendanceCount(financialYr, 0, AttendanceCode);
                model.TodayCount = AttendanceData.TodayCount;
                model.TomorrowCount = AttendanceData.TomorrowCount;
                model.YesterdayCount = AttendanceData.YesterdayCount;
                model.CurrentWeakCount = AttendanceData.CurrentWeakCount;
                model.CurrentMonthCount = AttendanceData.CurrentMonthCount;
                model.PreviousMonthCount = AttendanceData.PreviousMonthCount;


                DateTime lastdayofmonth = Convert.ToDateTime(new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1).ToString("yyyy-MMM-dd"));
                DateTime firstdate = Convert.ToDateTime(new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-6).ToString("yyyy-MMM-dd"));
                model.From = firstdate;
                model.To = lastdayofmonth;

                var resignations = await _service.GetResignationCount(firstdate.ToString("dd/MMM/yyyy"), lastdayofmonth.ToString("dd/MMM/yyyy"), 0);
                // var resignations = await _service.GetResignationCount("01/oct/2023", "31/oct/2023",0);
                model.resignationCount = resignations.resignationCount;
                model.averageResignationCount = resignations.averageResignationCount;

                //var avgresignations = await _service.GetAverageResignationCount();
                //model.averageResignationCount = avgresignations.averageResignationCount;


                var attritionRate = await _service.GetAttritionRate(firstdate.ToString("dd/MMM/yyyy"), lastdayofmonth.ToString("dd/MMM/yyyy"), 0);
                //var attritionRate = await _service.GetAttritionRate("01/oct/2023", "31/oct/2023",0);
                model.attritionRate = attritionRate.attritionRate;
                model.joiningCount = attritionRate.joiningCount;

                var joiners = await _service.GetJoinersCount(firstdate.ToString("dd/MMM/yyyy"), lastdayofmonth.ToString("dd/MMM/yyyy"), 0);
                //var joiners = await _service.GetJoinersCount("01/oct/2023", "31/oct/2023", 0);
                model.averageJoinersCount = joiners.averageJoinersCount;
                model.badHireDays = joiners.badHireDays;
                model.badHires = joiners.badHires;

                if (id == 6)
                {
                    lastdayofmonth = Convert.ToDateTime(new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1).ToString("yyyy-MMM-dd"));
                    firstdate = Convert.ToDateTime(new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("yyyy-MMM-dd"));
                    model.From = firstdate;
                    model.To = lastdayofmonth;
                }

                var abs = await _service.GetAbsRate(firstdate.ToString("dd/MMM/yyyy"), lastdayofmonth.ToString("dd/MMM/yyyy"));
                //  var abs = await _service.GetAbsRate("01/oct/2023", "31/oct/2023");
                model.absRate = abs.absRate;
                model.absenteesmCount = abs.absenteesmCount;


                var leaveData = await _service.GetLeaveData(currntLp, 0);
                model.leaveCount = leaveData.leaveCount;
                model.avgLeave = leaveData.avgLeave;
                model.lopCount = leaveData.lopCount;
                model.absCount = leaveData.absCount;


                ViewBag.financialYear = new SelectList(await _dropdownService.GetFinancialYearDropdown(), "Code", "Name");
                var empData = await _service.GetSalaryInformation(currntFY, 0);
                model.totalSalary = empData.totalSalary;
                model.avgSalary = empData.avgSalary;
                model.totalMonth = empData.totalMonth;
                model.avgCom = empData.avgCom;

                var incData = await _service.GetSalaryIncrementInformation(currntFY, 0);
                model.totalIncrement = incData.totalIncrement;
                model.avgIncrement = incData.avgIncrement;

                var recData = await _service.GetRecruitmentInformation(currntFY, 0);
                model.vacancy = recData.vacancy;
                model.offered = recData.offered;
                model.interviewed = recData.interviewed;
                model.joined = recData.joined;

                var hrData = await _service.GetHrInformation(0);
                model.employeeCount = hrData.employeeCount;
                model.avgAge = hrData.avgAge;
                model.avgTenure = hrData.avgTenure;
                model.employeeMaleCount = hrData.employeeMaleCount;
                model.employeeFeMaleCount = hrData.employeeFeMaleCount;
                model.avgGP = hrData.avgGP;

                if (id == 1)
                {
                    return PartialView("~/Areas/MIS/Views/MISDashBoard/_JoinersProfile.cshtml", model);
                }
                else if (id == 2)
                {
                    return PartialView("~/Areas/MIS/Views/MISDashBoard/_ResignationProfile.cshtml", model);
                }
                else if (id == 3)
                {
                    return PartialView("~/Areas/MIS/Views/MISDashBoard/_EmployeeProfile.cshtml", model);
                }
                else if (id == 4)
                {

                    return PartialView("~/Areas/MIS/Views/MISDashBoard/_LeaveProfile.cshtml", model);
                }
                else if (id == 5)
                {
                    return PartialView("~/Areas/MIS/Views/MISDashBoard/_SalaryProfile.cshtml", model);
                }
                else if (id == 6)
                {
                    return PartialView("~/Areas/MIS/Views/MISDashBoard/_AbsenteesmAnalysis.cshtml", model);
                }
                else if (id == 7)
                {
                    return PartialView("~/Areas/MIS/Views/MISDashBoard/_HistoricalTrends.cshtml", model);
                }
                else if (id == 8)
                {
                    return PartialView("~/Areas/MIS/Views/MISDashBoard/_RecruitmentAnalysis.cshtml", model);
                }
                else if (id == 9)
                {
                    return PartialView("~/Areas/MIS/Views/MISDashBoard/_HRatGlance.cshtml", model);
                }
                else if (id == 10)
                {
                    return PartialView("~/Areas/MIS/Views/MISDashBoard/_PunchingAnalysis.cshtml", model);
                }
                else if (id == 11)
                {
                    return PartialView("~/Areas/MIS/Views/MISDashBoard/_HRatGlanceSalary.cshtml", model);
                }
                else if (id == 12)
                {
                    var CMonthCode = await _commonService.GetCurrentMonthDetails();
                    model = await _service.GetMonths(currntFY);
                    model.CurrentSalaryPeriodCode = CMonthCode.LastProcessedMonth;
                    int curMonth = (int)CMonthCode.LastProcessedMonth;
                    var empAttData = await _service.GetAttendanceMonthData(curMonth, 0);

                    model.netPay = empAttData.netPay;
                    model.lateIn = empAttData.lateIn;
                    model.earlyOut = empAttData.earlyOut;
                    model.ot = empAttData.ot;
                    model.th = empAttData.th;

                    return PartialView("~/Areas/MIS/Views/MISDashBoard/_AttendanceOverView.cshtml", model);
                }
                else if (id == 13)
                {
                    var CMonthCode = await _commonService.GetCurrentMonthDetails();
                    model = await _service.GetMonths(currntFY);
                    model.CurrentSalaryPeriodCode = CMonthCode.LastProcessedMonth;
                    int curMonth = (int)CMonthCode.LastProcessedMonth;
                    var emplData = await _service.GetEmpData(curMonth, 0);
                    model.totalEmp = emplData.totalEmp;
                    model.totalPrvEmp = emplData.totalPrvEmp;
                    model.newHires = emplData.newHires;
                    model.resignations = emplData.resignations;
                    model.difference = emplData.difference;
                    model.totalEmpMale = emplData.totalEmpMale;
                    model.totalEmpFemale = emplData.totalEmpFemale;
                    model.pfEmployer = emplData.pfEmployer;
                    model.pfEmployee = emplData.pfEmployee;
                    model.esiEmployer = emplData.esiEmployer;
                    model.esiEmployee = emplData.esiEmployee;
                    return PartialView("~/Areas/MIS/Views/MISDashBoard/_PayRollOverView.cshtml", model);
                }
                else if (id == 14)
                {
                    var CMonthCode = await _commonService.GetCurrentMonthDetails();
                    model = await _service.GetMonths(currntFY);
                    model.CurrentSalaryPeriodCode = CMonthCode.LastProcessedMonth;
                    int curMonth = (int)CMonthCode.LastProcessedMonth;
                    return PartialView("~/Areas/MIS/Views/MISDashBoard/_LeaveOverView.cshtml", model);
                }
                else if (id == 15)
                {
                    var CMonthCode = await _commonService.GetCurrentMonthDetails();
                    model = await _service.GetMonths(currntFY);
                    model.CurrentSalaryPeriodCode = CMonthCode.LastProcessedMonth;
                    int curMonth = (int)CMonthCode.LastProcessedMonth;
                    return PartialView("~/Areas/MIS/Views/MISDashBoard/_LateInOverView.cshtml", model);
                }
                else if (id == 16)
                {
                    var CMonthCode = await _commonService.GetCurrentMonthDetails();
                    model = await _service.GetMonths(currntFY);
                    model.CurrentSalaryPeriodCode = CMonthCode.LastProcessedMonth;
                    int curMonth = (int)CMonthCode.LastProcessedMonth;

                    return PartialView("~/Areas/MIS/Views/MISDashBoard/_EmployeeOverView.cshtml", model);
                }
                else if (id == 17)
                {
                    return PartialView("~/Areas/MIS/Views/MISDashBoard/_ResignationAgeBandList.cshtml", model);
                }
                else if (id == 18)
                {
                    return PartialView("~/Areas/MIS/Views/MISDashBoard/_ResignationTenureList.cshtml", model);
                }
                else if (id == 19)
                {
                    return PartialView("~/Areas/MIS/Views/MISDashBoard/_ResignationGenderList.cshtml", model);
                }
                else if (id == 20 || id == 21 || id == 22 || id == 23 || id == 24 || id == 25)
                {
                    return PartialView("~/Areas/MIS/Views/MISDashBoard/_ResignationDepartmentList.cshtml", model);
                }
                else if (id >= 26 && id <= 34)
                {
                    return PartialView("~/Areas/MIS/Views/MISDashBoard/_JoinersProfileList.cshtml", model);
                }
                else if (id == 40)
                {
                    return PartialView("~/Areas/MIS/Views/MISDashBoard/_JobTransitionType.cshtml", model);
                }

                else if (id == 41)
                {
                    return PartialView("~/Areas/MIS/Views/MISDashBoard/_GrievenceByFinancialYear.cshtml", model);
                }
                else if (id == 42)
                {
                    return PartialView("~/Areas/MIS/Views/MISDashBoard/_GrievanceTotalList.cshtml", model);
                }
                else if (id == 45)
                {
                    return PartialView("~/Areas/MIS/Views/MISDashBoard/_TransitionEmployeeList.cshtml", model);
                }
                else if (id >= 53 && id <= 58)
                {
                    return PartialView("~/Areas/MIS/Views/MISDashBoard/_SalaryList.cshtml", model);
                }
                else if ((id >= 37 && id <= 39) || (id >= 50 && id <= 52))

                {
                    return PartialView("~/Areas/MIS/Views/MISDashBoard/_HREmployeeList.cshtml", model);
                }
                else if (id >= 54 && id <= 58)
                {
                    return PartialView("~/Areas/MIS/Views/MISDashBoard/_SalaryList.cshtml", model);
                }
                else if (id == 59)
                {
                    return PartialView("~/Areas/MIS/Views/MISDashBoard/_HistoricalTrendList.cshtml", model);
                }
                else if (id == 60)
                {
                    return PartialView("~/Areas/MIS/Views/MISDashBoard/_AttendenceOverViewList.cshtml", model);
                }
                else if (id == 80)
                {
                    return PartialView("~/Areas/MIS/Views/MISDashBoard/_PFList.cshtml", model);
                }
                else if (id == 70)
                {
                    return PartialView("~/Areas/MIS/Views/MISDashBoard/_EmployeeLeaveResult.cshtml", model);
                }
                else if (id == 71)
                {
                    return PartialView("~/Areas/MIS/Views/MISDashBoard/_EmployeeRewardResult.cshtml", model);
                }
                else if (id == 72)
                {
                    return PartialView("~/Areas/MIS/Views/MISDashBoard/_EmployeeSkillResult.cshtml", model);
                }


                else if (id > 80 && id <= 81)
                {
                    return PartialView("~/Areas/MIS/Views/MISDashBoard/_AllowanceList.cshtml", model);
                }
                else if (id == 110)
                {
                    return PartialView("~/Areas/MIS/Views/MISDashBoard/_AbssentismMonthWiseResult.cshtml", model);
                }
                else if (id == 111)
                {
                    return PartialView("~/Areas/MIS/Views/MISDashBoard/_AbssentismAgeWiseResult.cshtml", model);
                }
                else if (id == 112)
                {
                    return PartialView("~/Areas/MIS/Views/MISDashBoard/_AbsentissmServiceWise.cshtml", model);
                }
                else if (id == 113)
                {
                    return PartialView("~/Areas/MIS/Views/MISDashBoard/_AbsentissmDepartmentWise.cshtml", model);
                }
                else if (id == 114)
                {
                    return PartialView("~/Areas/MIS/Views/MISDashBoard/_AbsentissmLocationWise.cshtml", model);
                }
                else if (id == 115)
                {
                    return PartialView("~/Areas/MIS/Views/MISDashBoard/_AbsentissmWeeklyWise.cshtml", model);
                }
                else if (id == 116)
                {
                    return PartialView("~/Areas/MIS/Views/MISDashBoard/_AbsentissmDivisionWise.cshtml", model);
                }
                else if (id == 117)
                {
                    return PartialView("~/Areas/MIS/Views/MISDashBoard/_AbsentissmCategoryWise.cshtml", model);
                }
                else if (id == 118)
                {
                    return PartialView("~/Areas/MIS/Views/MISDashBoard/_AbsentissmRegionWise.cshtml", model);
                }
                else if (id == 119)
                {
                    return PartialView("~/Areas/MIS/Views/MISDashBoard/_AbsentissmGradeWise.cshtml", model);
                }
                else if (id == 120)
                {
                    return PartialView("~/Areas/MIS/Views/MISDashBoard/_RecruitmentAnalysisVacancy.cshtml", model);
                }
                else if (id == 121)
                {
                    return PartialView("~/Areas/MIS/Views/MISDashBoard/_RecruitmentAnalysisVacancyInformation.cshtml", model);
                }
                else if (id == 122)
                {
                    return PartialView("~/Areas/MIS/Views/MISDashBoard/_RecruitmentInterview.cshtml", model);
                }
                else if (id == 123)
                {
                    return PartialView("~/Areas/MIS/Views/MISDashBoard/_RecruitmentDesignationDetail.cshtml", model);
                }
                else if (id == 124)
                {
                    return PartialView("~/Areas/MIS/Views/MISDashBoard/_RecruitmentVacancyDetail.cshtml", model);
                }
                else if (id == 125)
                {
                    return PartialView("~/Areas/MIS/Views/MISDashBoard/_RecruitmentVacancyOfferedDetail.cshtml", model);
                }
                else if ((id >= 36 && id <= 39) || (id >= 50 && id <= 52))

                {
                    return PartialView("~/Areas/MIS/Views/MISDashBoard/_HREmployeeList.cshtml", model);
                }
                else if (id == 90)
                {
                    return PartialView("~/Areas/MIS/Views/MISDashBoard/_LeaveEmployeeList.cshtml", model);
                }
                else if (id == 128)
                {
                    return PartialView("~/Areas/MIS/Views/MISDashBoard/_BadHiresProfile.cshtml", model);
                }

                else if (id == 129)
                {
                    return PartialView("~/Areas/MIS/Views/MISDashBoard/_BadHiresList.cshtml", model);
                }
                else if (id == 130)
                {
                    return PartialView("~/Areas/MIS/Views/MISDashBoard/_TrainingProfile.cshtml", model);
                }
                else if (id == 131)
                {
                    return PartialView("~/Areas/MIS/Views/MISDashBoard/_TrainingEmpList.cshtml", model);
                }
                else if (id == 132)
                {
                    return PartialView("~/Areas/MIS/Views/MISDashBoard/_TrainingCostList.cshtml", model);
                }


                else
                {
                    return PartialView("~/Areas/MIS/Views/MISDashBoard/_LateInOveViewList.cshtml", model);
                }


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
        public ActionResult Pdf_Export_Save(string contentType, string base64, string fileName)
        {
            var fileContents = Convert.FromBase64String(base64);

            return File(fileContents, contentType, fileName);
        }
        public async Task<IActionResult> GetLeaveEmployeeList([DataSourceRequest] DataSourceRequest request, int crrntLp, int groupCom, string type, string code)
        {
            MISDashBoard model = new MISDashBoard();
            List<LeaveEmployeeList> modellist = new List<LeaveEmployeeList>();

            model = await _service.GetLeaveEmployeeList(crrntLp, groupCom, type, code);

            modellist = model.LeaveEmployeeList;

            var dataResult1 = modellist.ToDataSourceResult(request);


            return Json(dataResult1, new JsonSerializerSettings { ContractResolver = new DefaultContractResolver() });

        }
        public async Task<MISDashBoard> GetHrInformation(int groupCom)
        {
            MISDashBoard model = new MISDashBoard();
            try
            {

                var hrData = await _service.GetHrInformation(groupCom);
                model.employeeCount = hrData.employeeCount;
                model.avgAge = hrData.avgAge;
                model.avgTenure = hrData.avgTenure;
                model.employeeMaleCount = hrData.employeeMaleCount;
                model.employeeFeMaleCount = hrData.employeeFeMaleCount;
                model.avgGP = hrData.avgGP;

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
        public async Task<IActionResult> GetYearlyTrainingCost(int financialYr)
        {
            try
            {
                MISDashBoard model = new MISDashBoard();
                model = await _service.GetYearlyTrainingCost(financialYr);

                return Json(model.YearlyTrainingCostList);
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

        public async Task<IActionResult> GetPerHeadTrainingCost(int financialYr)
        {
            try
            {
                MISDashBoard model = new MISDashBoard();
                model = await _service.GetPerHeadTrainingCost(financialYr);

                return Json(model.PerHeadTrainingCostList);
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

        public async Task<IActionResult> GetTrainingEmployeeCount(int financialYr)
        {
            try
            {
                MISDashBoard model = new MISDashBoard();
                model = await _service.GetTrainingEmployeeCount(financialYr);

                return Json(model.TrainingEmployeeCountList);
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

        public async Task<IActionResult> GetMonthlyTrainings(int financialYr)
        {
            try
            {
                MISDashBoard model = new MISDashBoard();
                model = await _service.GetMonthlyTrainings(financialYr);

                return Json(model.MonthlyTrainingsList);
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
        public async Task<IActionResult> GetTrainingEmp([DataSourceRequest] DataSourceRequest request, int financialYr, int code)
        {
            MISDashBoard model = new MISDashBoard();
            List<TrainingEmp> modellist = new List<TrainingEmp>();

            model = await _service.GetTrainingEmp(financialYr, code);

            modellist = model.TrainingEmpList;

            var dataResult1 = modellist.ToDataSourceResult(request);


            return Json(dataResult1, new JsonSerializerSettings { ContractResolver = new DefaultContractResolver() });

        }
        public async Task<IActionResult> GetTrainingCostList([DataSourceRequest] DataSourceRequest request, int code)
        {
            MISDashBoard model = new MISDashBoard();
            List<TrainingCostList> modellist = new List<TrainingCostList>();

            model = await _service.GetTrainingCostList(code);

            modellist = model.TrainingCostList;

            var dataResult1 = modellist.ToDataSourceResult(request);


            return Json(dataResult1, new JsonSerializerSettings { ContractResolver = new DefaultContractResolver() });

        }





        public async Task<IActionResult> GetNewDashboardPage()
        {
            MISDashBoard model = new MISDashBoard();
            ViewBag.GrpCompany = new SelectList(await _dropdownService.GetGroupCompanyDropDown(), "Code", "Name");

            var hrData = await _service.GetHrInformation(0);
            model.employeeCount = hrData.employeeCount;
            model.avgAge = hrData.avgAge;
            model.avgTenure = hrData.avgTenure;
            model.employeeMaleCount = hrData.employeeMaleCount;
            model.employeeFeMaleCount = hrData.employeeFeMaleCount;
            model.avgGP = hrData.avgGP;
            model.noticeperiod = hrData.noticeperiod;
            model.LoanBalance = hrData.LoanBalance;


            DateTime lastdayofmonth = Convert.ToDateTime(new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1).ToString("yyyy-MMM-dd"));
            DateTime firstdate = Convert.ToDateTime(new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-6).ToString("yyyy-MMM-dd"));
            model.From = firstdate;
            model.To = lastdayofmonth;
            var resignations = await _service.GetResignationCount(firstdate.ToString("dd/MMM/yyyy"), lastdayofmonth.ToString("dd/MMM/yyyy"), 0);
            model.resignationCount = resignations.resignationCount;
            var attritionRate = await _service.GetAttritionRate(firstdate.ToString("dd/MMM/yyyy"), lastdayofmonth.ToString("dd/MMM/yyyy"), 0);
            model.attritionRate = attritionRate.attritionRate;
            model.joiningCount = attritionRate.joiningCount;

            var m = await _service.GetDepartmentHR(0);
            var model1 = await _service.GetAgeHR(0);
            model.HRAgeList= new List<HRAge>();
            model.HRAgeList = model1.HRAgeList;
            model.HRDepartmentList = new List<HRDepartment>();
            model.HRDepartmentList = m.HRDepartmentList;
            model.Type = 1;
            ///model.Category = 'ONE';
            return View("~/Areas/MIS/Views/MISDashBoard/_BIDashboard.cshtml", model);
        }


        public async Task<IActionResult> GetGraph(string Classification,int Type)
        {
            try
            {
                MISDashBoard model = new MISDashBoard();
               if (Type == 1)
               {
                    if (Classification == "AGE")
                    {
                        var model1 = await _service.GetAgeHR(0);
                        return PartialView("_Chart", model1.HRAgeList);
                    }
                    else if (Classification == "DEPARTMENT")
                    {
                        var model2 = await _service.GetDepartmentHR(0);
                        return PartialView("_ChartDept", model2.HRDepartmentList);
                    }
                    else if (Classification == "DIVISION")
                    {
                        var model5 = await _service.GetDivisionHR(0);
                        return PartialView("_ChartDivision", model5.HRDivisionList);
                    }
                    else if (Classification == "LOCATION")
                    {
                        var model3 = await _service.GetLocationHR(0);
                        return PartialView("_ChartLoc", model3.HRLocationList);
                    }
                    else if (Classification == "REGION")
                    {
                        var model4 = await _service.GetRegionHR(0);
                        return PartialView("_ChartRegion", model4.HRRegionList);
                    }
                    else
                    {
                        return PartialView("_Chart", model.HRAgeList);
                    }
               }
                else
                {
                    if (Classification == "AGE")
                    {
                        var model1 = await _service.GetAgeHR(0);
                        return PartialView("_ChartSalary", model1.HRAgeList);
                    }
                    else if (Classification == "DEPARTMENT")
                    {
                        var model2 = await _service.GetDepartmentHR(0);
                        return PartialView("_ChartDeptSal", model2.HRDepartmentList);
                    }
                    else if (Classification == "DIVISION")
                    {
                        var model5 = await _service.GetDivisionHR(0);
                        return PartialView("_ChartDivisionSal", model5.HRDivisionList);
                    }
                    else if (Classification == "LOCATION")
                    {
                        var model3 = await _service.GetLocationHR(0);
                        return PartialView("_ChartLocSal", model3.HRLocationList);
                    }
                    else if (Classification == "REGION")
                    {
                        var model4 = await _service.GetRegionHR(0);
                        return PartialView("_ChartRegionSal", model4.HRRegionList);
                    }
                    else
                    {
                        return PartialView("_ChartSalary", model.HRAgeList);
                    }
                }
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


        public async Task<ActionResult> GetGraphTwo(string Category)
        {
            try
            {
                MISDashBoard model = new MISDashBoard();
                if(Category == "ONE")
                {
                    model = await _service.GetHistoricalJoiningCountMonth();
                    return PartialView("_Chart2", model.GetHistoricalJoiningCountMonthList);
                }
                else
                {
                    model = await _service.GetHistoricalTerminationsCountMonth();
                    return PartialView("_Chart3", model.GetHistoricalTerminationsCountMonthList);
                }
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

        public async Task<IActionResult> BIReportDashboard()
        {
            try
            {
                return View("BIReportDashboard");
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

        public async Task<MISDashBoard> GetAttendanceCount(int financialYr, int groupCom,int AttendanceCode)
        {
            MISDashBoard model = new MISDashBoard();
            try
            {

                var AttendanceData = await _service.GetAttendanceCount(financialYr, groupCom, AttendanceCode);
                model.TodayCount = AttendanceData.TodayCount;
                model.TomorrowCount = AttendanceData.TomorrowCount;
                model.YesterdayCount = AttendanceData.YesterdayCount;
                model.CurrentWeakCount = AttendanceData.CurrentWeakCount;
                model.CurrentMonthCount = AttendanceData.CurrentMonthCount;
                model.PreviousMonthCount = AttendanceData.PreviousMonthCount;

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

    }
}
