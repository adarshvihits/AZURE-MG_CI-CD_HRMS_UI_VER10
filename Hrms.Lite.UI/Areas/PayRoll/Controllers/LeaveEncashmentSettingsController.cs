using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.PayRoll;
using Hrms.Lite.Shared.Account;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using Hrms.Lite.Shared.PayRoll.AllowanceSettings;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Spire.Xls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.PayRoll.Controllers
{
    [Area("PayRoll")]
    [SessionAuthorize]
    public class LeaveEncashmentSettingsController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly ILeaveEncashmentSettingsService _service;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        public static class Global
        {
            public static int Allowance;
            public static Filter GlobalFilter;

        }
        public LeaveEncashmentSettingsController(ILeaveEncashmentSettingsService service, IDropdownService dropdownService, ICommonService commonService, IBlob blob, IHelpers helpers)
        {
            _service = service;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = helpers;
        }
        public async Task<IActionResult> Index()    
        {
            try
            {
                ApplicableAllowance model = new ApplicableAllowance();
                ViewBag.LeaveAllowance = new SelectList(await _dropdownService.GetLeaveEncashmentSettingsAllowanceDropdown(), "Code", "Name");
                ViewBag.CalculationType = new SelectList(await _dropdownService.GetCalculationTypeDropdown(), "Code", "Name");
                await fillfilterDropdown();
                model = await _service.GetApplicableAllowanceList();
                model.Allowance = new Allowance();
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
        public async Task<IActionResult> GetApplicableAllowanceList(int AttendanceStatus)
        {
            try
            {
                ApplicableAllowance model = new ApplicableAllowance();
                model.Allowance = new Allowance();
                model = await _service.GetApplicableAllowanceList();
                model.AttendanceStatus = new AttendanceStatus();
                model.AttendanceStatus.Code = AttendanceStatus;
                return PartialView("_ApplicableAllowance", model);
                //return PartialView("_addApplicableAllowance", model);
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
        public async Task<IActionResult> ApplyFilter(Filter filter)
        {
            try
            {
                AllowanceSettingsEmployeeList model = new AllowanceSettingsEmployeeList();
                Global.GlobalFilter = filter;
                model.SettingsEmployeeList = await _service.GetSettingsEmployeeList(Global.Allowance, filter);
                ViewBag.CalculationType = new SelectList(await _dropdownService.GetCalculationTypeDropdown(), "Code", "Name");
                return PartialView("_LeaveEncashList", model);
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
        [HttpPost]
        public async Task<IActionResult> SaveApplicableAllowance(ApplicableAllowance model)
        {
            try
            {
                if (model.Allowance.AllowanceList != null)
                {
                    var abc = from li in model.Allowance.AllowanceList
                              where li.Applicable.Equals(true)
                              select li;
                    model.Allowance.AllowanceList = abc.ToList();
                }



                var response = await _service.SaveApplicableAllowance(model);


                if (response.Success == true)
                {
                    response.Message = "Saved Successfully";
                    return Json(response);
                    //TempData["Message"] = "Saved Successfully";
                    //return RedirectToAction("Index", "HolidayWageSettings");
                }
                else
                {
                    return Json(response);
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
        //public async Task<IActionResult> GetSettingsEmployeeList(int Allowance)
        //{
        //    try { 
        //    AllowanceSettingsEmployeeList model = new AllowanceSettingsEmployeeList();
        //    ViewBag.CalculationType = new SelectList(await _dropdownService.GetCalculationTypeDropdown(), "Code", "Name");
        //    Global.Allowance = Allowance;
        //    Filter filter = new Filter();
        //    model.SettingsEmployeeList = (List<AllowanceSettingsEmployeeList>)await _OvertimeSettingsService.GetSettingsEmployeeList(Allowance, filter);

        //    TempData["AllowanceToFilter"] = Allowance;

        //    return PartialView("_SettingsEmployeeList", model);
        //    }
        //    catch (UnauthorizedAccessException e)
        //    {

        //        return RedirectToAction("Login", "Account", new { Area = "" });
        //    }
        //    catch (Exception e)
        //    {
        //        return RedirectToAction("Error", "Account", new { Area = "" });
        //    }
        //}


        [HttpPost]
        public async Task<IActionResult> GetSettingsList(Filter filter1, int LeaveCode)
        {
            try
            {
                AllowanceSettingsEmployeeList model = new AllowanceSettingsEmployeeList();
                AllowanceSettingsEmployeeList m = new AllowanceSettingsEmployeeList();
                //model.EmployeeHeader = new EmployeeHeader();




                Filter filter = new Filter();
                Global.GlobalFilter = filter1;


                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }
                if (LeaveCode != 0)
                {

                    model.SettingsEmployeeList = (List<AllowanceSettingsEmployeeList>)await _service.GetSettingsEmployeeList(LeaveCode, filter1);
                }
                m.SettingsEmployeeList = model.SettingsEmployeeList;
                int totalrows = 0;
                int totalrowsafterfiltering = 0;



                if (model == null || model.SettingsEmployeeList == null || model.SettingsEmployeeList.Count() == 0)
                {
                    m.SettingsEmployeeList = new List<AllowanceSettingsEmployeeList>();
                    model = new AllowanceSettingsEmployeeList();
                    model.SettingsEmployeeList = new List<AllowanceSettingsEmployeeList>();
                }
                else
                {
                    m.SettingsEmployeeList = model.SettingsEmployeeList;
                }



                if (model.SettingsEmployeeList == null || model.SettingsEmployeeList.Count() == 0)
                {

                    return Json(new
                    {
                        data = "",
                        draw = filter1.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows
                    });
                }




                if (model.SettingsEmployeeList.Count > 0)
                {
                    totalrows = m.SettingsEmployeeList[0].active;

                }


                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = m.SettingsEmployeeList[0].totalRecords;
                    return Json(new
                    {
                        data = model.SettingsEmployeeList,
                        draw = filter1.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows//totalrowsafterfiltering
                    });
                }
                else
                {
                    return Json(new
                    {
                        data = model.SettingsEmployeeList,
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






        public async Task<IActionResult> GetBatchUpdateDetails(int LeaveCode, string LeaveName)
        {
            try
            {
                AllowanceSettingsBatchUpdate model = new AllowanceSettingsBatchUpdate();
                model.Allowance = new Allowance();

                ViewBag.CalculationType = new SelectList(await _dropdownService.GetCalculationTypeDropdown(), "Code", "Name");

                Filter filter = new Filter();
                model.AttendanceStatusCode = LeaveCode;
                model.Allowance.Name = LeaveName;

                return PartialView("_BatchUpdate", model);
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
        public async Task<IActionResult> GetDefaultSettingsList()
        {
            try
            {
                ViewBag.CalculationType = new SelectList(await _dropdownService.GetCalculationTypeDropdown(), "Code", "Name");





                AllowanceSettingsBatchUpdate model = new AllowanceSettingsBatchUpdate();

                model = await _service.GetDefaultSettingsDetails();

                return PartialView("_DefaultSettings", model);
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
        public async Task<ActionResult> SaveEmployeeWiseSettings(int UpdatedRate, int UpdatedCalcType, Guid EmployeeGI, int AllowanceCode)
        {
            try
            {
                AllowanceSettingsSave model = new AllowanceSettingsSave();

                model.EmployeeGI = EmployeeGI;
                model.Rate = UpdatedRate;
                model.CalcTypeCode = UpdatedCalcType;
                model.AttendanceStatusCode = AllowanceCode;


                var response = await _service.SaveEmployeeWiseSettings(model);
                if (response.Success == true)
                    response.Message = "Updated Successfully";
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
        public async Task<IActionResult> BatchUpdateEmployeeWiseSettings(AllowanceSettingsBatchUpdate model)
        {

            try
            {
                Guid EmployeeGI = model.EmployeeGI;
                if (Global.GlobalFilter == null)
                    model.filter = new Filter();
                else
                    model.filter = Global.GlobalFilter;

                var response = await _service.BatchUpdateEmployeeWiseSettings(model);
                if (response.Success == true)
                    response.Message = "Updated Successfully";
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
        public async Task<ActionResult> SaveDefaultSettings(AllowanceSettingsBatchUpdate model)
        {
            try
            {
                //AllowanceSettingsSave model = new AllowanceSettingsSave();

                //  model.EmployeeGI = EmployeeGI;
                //model.Rate = Rate;
                //model.CalcTypeCode = CalcType;
                //   model.AllowanceCode = AllowanceCode;


                var response = await _service.SaveDefaultSettings(model);
                if (response.Success == true)
                    response.Message = "Saved Successfully";
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
        public async Task<IActionResult> ExportToExcel(int Allowance, string AllowanceName, int Totalrow)
        {
            try
            {
                AllowanceSettingsEmployeeList model = new AllowanceSettingsEmployeeList();
                if (Global.GlobalFilter == null)
                    Global.GlobalFilter = new Filter();
                else
                {
                    Global.GlobalFilter.length = Totalrow;
                    Global.GlobalFilter.start = 0;
                }
                string FilterValues = await _helpers.GetFilterValues(Global.GlobalFilter);

                model.SettingsEmployeeList = (List<AllowanceSettingsEmployeeList>)await _service.GetSettingsEmployeeList(Allowance, Global.GlobalFilter);
                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("LeaveEncashment Settings List (" + AllowanceName + ")", "Excel");
                int slNo = 0;
                if (FilterValues == "")
                {
                    FilterValues = "All";
                }
                string currDateTime = await _helpers.GetCurrentDate();
                
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("LeaveEncashment Settings List");
                    int count = model.SettingsEmployeeList.Count + 5;


                    worksheet.Range("A1:H" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:H" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:H" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:H" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:H" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 7)));
                    wsReportNameHeaderRange.Style.Font.Bold = true;
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Merge();
                    wsReportNameHeaderRange.Value = "LeaveEncashment Settings";

                    worksheet.Cell(2, 1).Style.Font.Bold = true;
                    worksheet.Cell(2, 1).Value = "Group Company Name";
                    worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                    worksheet.Cell(2, 3).Style.Font.Bold = true;
                    worksheet.Cell(2, 3).Value = "Holiday Wage Allowance";
                    worksheet.Cell(2, 4).Value = AllowanceName;

                    worksheet.Cell(3, 1).Style.Font.Bold = true;
                    worksheet.Cell(3, 1).Value = "Login User";
                    worksheet.Cell(3, 2).Value = LoginUser;
                    worksheet.Cell(3, 3).Style.Font.Bold = true;
                    worksheet.Cell(3, 3).Value = "Date & Time";
                    worksheet.Cell(3, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(3, 4).Style.DateFormat.Format = "dd-MMM-yyyy   h:mm AM/PM";
                    worksheet.Cell(3, 4).Value = currDateTime.ToString();
                    worksheet.Cell(3, 5).Style.Font.Bold = true;
                    worksheet.Cell(3, 5).Value = "Total Count";
                    worksheet.Cell(3, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(3, 6).Value = model.SettingsEmployeeList.Count;
                    worksheet.Cell(4, 1).Style.Font.Bold = true;
                    worksheet.Cell(4, 1).Value = "Filter Applied";
                    worksheet.Range("B4:H4").Merge();
                    worksheet.Cell("B4").Value = FilterValues;



                    var currentRow = 5;
                    #region Header
                    worksheet.Range("A5:H5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Cell(currentRow, 1).Value = "Sl.No";
                    worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 2).Value = "ID";
                    worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 3).Value = "EmployeeName";
                    worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 4).Value = "Designation";
                    worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 5).Value = "Department";
                    worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 6).Value = "Location";
                    worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 7).Value = "Calculation Type";
                    worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 8).Value = "Amt/RT in %";
                    worksheet.Cell(currentRow, 8).Style.Font.Bold = true;


                    #endregion

                    #region Body
                    foreach (var item in model.SettingsEmployeeList)
                    {
                        currentRow++;
                        slNo = slNo + 1;
                        worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 1).Value = slNo;
                        worksheet.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 2).Value = item.Employee.EmployeeID;
                        worksheet.Cell(currentRow, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 3).Value = item.Employee.FullName;
                        worksheet.Cell(currentRow, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 4).Value = item.DesignationName;
                        worksheet.Cell(currentRow, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 5).Value = item.DepartmentName;
                        worksheet.Cell(currentRow, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 6).Value = item.LocationName;
                        worksheet.Cell(currentRow, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 7).Value = item.CalcType.Name;
                        //worksheet.Cell(currentRow, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 8).Value = item.Rate;

                        worksheet.Columns(1, 8).Style.Alignment.WrapText = true;
                        worksheet.Rows().Style.Alignment.WrapText = true;
                    }
                    #endregion
                    worksheet.Columns().AdjustToContents();
                    worksheet.Rows().AdjustToContents();
                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        if (EP.NeedPassword)
                        {
                            Workbook book = new Workbook();
                            book.LoadFromStream(stream);
                            //Protect Workbook
                            book.Protect(EP.Password,false,false);
                            book.SaveToStream(stream);
                        }
                        var content = stream.ToArray();
                        return File(
                            content,
                            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                            "LeaveEncashmentSettingsList.xlsx"
                            );
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

    }
}
