using ClosedXML.Excel;

using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.TimeOffice;
using Hrms.Lite.Services.Services.TimeOffice;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TimeOffice;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Spire.Xls;
using System.Data;
using Newtonsoft.Json;
using Hrms.Lite.Shared.Account;

namespace Hrms.Lite.UI.Areas.TimeOffice.Controllers
{
    [Area("TimeOffice")]
    [SessionAuthorize]
    public class MonthlyAttendanceProcessingController : Controller
    {
        private readonly IMonthlyAttendanceProcessingService _MonthlyAttendanceProcessingService;
        private readonly ICommonService _commonService;
        private readonly IDropdownService _dropdownService;
        private readonly IHelpers _helpers;
        public MonthlyAttendanceProcessingController(IMonthlyAttendanceProcessingService MonthlyAttendanceProcessingService, ICommonService commonService, IDropdownService dropdownService, IHelpers helpers)
        {
            _MonthlyAttendanceProcessingService = MonthlyAttendanceProcessingService;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = helpers;
        }
        public static class Global
        {
            public static Shared.General.Filter GlobalFilter;
            public static string Period;
            public static string month;
            public static int monthCode;
            public static int PayrollCode;
            public static string payroll;
            public static int Tabindex;
            public static string rowName;
            public static string tabName;
        }

        public async Task<IActionResult> Index()
        {
            try
            { 
                MonthlyAttendanceProcessing model = new MonthlyAttendanceProcessing();
                model.Filter = new Filter();             
      
                ViewBag.PayrollGroup = new SelectList(await _dropdownService.GetPayrollGroupWithGrpCmpyCheckDropdown(), "Code", "Name");
                ViewBag.Month = new SelectList(await _dropdownService.GetMonthly_AttendanceProcessing_MonthDropdown(0), "Code", "Name");
                Filter filter = new Filter();
                filter.length = 10;
                model = await _MonthlyAttendanceProcessingService.MonthlyAttendanceProcessingList(0, 0, 0, filter);
                await fillfilterDropdown();
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
        public async Task<IActionResult> GetMonth(int PayrollGrp)
        {
            try
            {
                var Months = await _dropdownService.GetMonthly_AttendanceProcessing_MonthDropdown(PayrollGrp);
                return Json(Months);
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
        public async Task<IActionResult> ApplyFilter(Filter filter, int Month, int PayrollGroup)
        {
            try
            {
                Global.GlobalFilter = filter;
                MonthlyAttendanceProcessing model = new MonthlyAttendanceProcessing();
                model = await _MonthlyAttendanceProcessingService.MonthlyAttendanceProcessingList(Month, PayrollGroup, 5, filter);
                return PartialView("_AttendanceProcessing", model);
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
        public async Task<IActionResult> MonthlyAttendanceProcessingList(int Month, int PayrollGroup, int Tabindex)
        {
            try
            { 
           MonthlyAttendanceProcessing model = new MonthlyAttendanceProcessing();
            Filter filter = new Filter();
                Global.Tabindex = Tabindex;
            model = await _MonthlyAttendanceProcessingService.MonthlyAttendanceProcessingList(Month, PayrollGroup,Tabindex, filter);
                Global.Period = model.Period;
                Global.month = model.Month.Name;
                Global.payroll = model.PayrollGroup.Name;
                if (Tabindex == 1) //AttendanceSettings
                    return PartialView("_AttendanceSettings", model);
                else if (Tabindex == 2)//AttendanceStatus
                    return PartialView("_AttendanceStatus", model);
                else if (Tabindex == 3)//Leave
                    return PartialView("_Leave", model);
                else if (Tabindex == 4) //Wages
                    return PartialView("_AttendanceWages", model);
                else if (Tabindex == 5)//Processing
                {                  
                return PartialView("_AttendanceProcessing", model);
                }
                else //Selection
                    return PartialView("_AttendanceSelection", model);
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
        public async Task<IActionResult> GetAttendanceProcessingList(Filter filter1, int Month, int PayrollGroup, int Tabindex)
        {
            try
            {
                MonthlyAttendanceProcessing model = new MonthlyAttendanceProcessing();
                MonthlyAttendanceProcessing m = new MonthlyAttendanceProcessing();

                Filter filter = new Filter();
                Global.GlobalFilter = filter1;
           

                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }

                model = await _MonthlyAttendanceProcessingService.MonthlyAttendanceProcessingList(Month, PayrollGroup, Tabindex, filter1);
                m.MonthlyAttendanceProcessing_Data = model.MonthlyAttendanceProcessing_Data;
                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.MonthlyAttendanceProcessing_Data == null || model.MonthlyAttendanceProcessing_Data.Count() == 0)
                {
                    

                    return Json(new
                    {
                        data = "",
                        draw = filter1.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows,
                        Take = model.TotalDays
                    });
                }

                if (model.MonthlyAttendanceProcessing_Data.Count > 0)
                {
                    totalrows = m.MonthlyAttendanceProcessing_Data[0].active;
                }


                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = m.MonthlyAttendanceProcessing_Data[0].totalRecords;

                    return Json(new
                    {
                        data = model.MonthlyAttendanceProcessing_Data,
                        draw = filter1.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows,//totalrowsafterfiltering
                        Take = model.TotalDays
                    }) ;
                }
                else
                {
                    
                    return Json(new
                    {
                        data = model.MonthlyAttendanceProcessing_Data,
                        draw = filter1.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows,
                        Take = model.TotalDays
                    });
                }


            }
            catch (Exception ex)
            {
                return View();
            }




        }

        public async Task<IActionResult> MonthlyAttendanceProcessingApprovalPending(Filter filter, int Month, int PayrollGroup, int Tabindex)
        {
            try
            {
                MonthlyAttendanceProcessing model = new MonthlyAttendanceProcessing();
                MonthlyAttendanceProcessing m = new MonthlyAttendanceProcessing();

                

                model = await _MonthlyAttendanceProcessingService.MonthlyAttendanceProcessingApprovalPendingPopUp(Month, PayrollGroup, Tabindex, filter);
                m.AttendanceApprovalPending_popup = model.AttendanceApprovalPending_popup;
                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.AttendanceApprovalPending_popup == null || model.AttendanceApprovalPending_popup.Count() == 0)
                {


                    return Json(new
                    {
                        data = "",
                        draw = filter.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows,                        
                    });
                }

                if (model.AttendanceApprovalPending_popup.Count > 0)
                {
                    totalrows = m.AttendanceApprovalPending_popup[0].active;
                }


                if (filter.searchValue != "")
                {
                    totalrowsafterfiltering = m.AttendanceApprovalPending_popup[0].totalRecords;

                    return Json(new
                    {
                        data = model.AttendanceApprovalPending_popup,
                        draw = filter.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows,//totalrowsafterfiltering                        
                    });
                }
                else
                {

                    return Json(new
                    {
                        data = model.AttendanceApprovalPending_popup,
                        draw = filter.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows,                        
                    });
                }


            }
            catch (Exception ex)
            {
                return View();
            }
        }

        public IActionResult GetProcessingList()
        {
            try { 
                MonthlyAttendanceProcessing model = new MonthlyAttendanceProcessing();
            
                model.Month = new Shared.TimeOffice.Generic.Master.Month();
                model.PayrollGroup = new Shared.Master.PayrollGroup();
                //model.Month.Name = Month;
                model.Period = Global.Period;
                model.Month.Name = Global.month;
                model.PayrollGroup.Name=Global.payroll;
           
                return PartialView("_AttendanceProcessing", model);
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

        public async Task<IActionResult> GetMonthlyAttendanceSelectionList(int Month, int PayrollGroup)
        {
            try
            { 
            MonthlyAttendanceProcessing model = new MonthlyAttendanceProcessing();
            Filter filter = new Filter();
            model = await _MonthlyAttendanceProcessingService.MonthlyAttendanceProcessingList(Month, PayrollGroup, 0, filter);
      
            return PartialView("_AttendanceSelection", model);
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



        public async Task<ActionResult> GetEmployeeDetails(int Month, int PayrollGroup, int slno, string RowName, string TabName,int tab_index)
        {
            try
            {
                MonthlyAttendanceProcessing model = new MonthlyAttendanceProcessing();
                Filter filter = new Filter();
                Global.monthCode = Month;
                Global.PayrollCode = PayrollGroup;
                ViewBag.TabName = TabName;
                ViewBag.RowName = RowName;
                Global.rowName = RowName;
                Global.tabName = TabName;
                
                model = await _MonthlyAttendanceProcessingService.MonthlyAttendanceProcessingListPopUp(Month, PayrollGroup, tab_index, RowName,filter);
                if (TabName == "ATTENDANCE_SETTINGS")
                {
                    if (RowName == "Daily Attendance Processing Pending Days")
                    {
                        return PartialView("_DailyAttnProcessingPopUp", model);
                    }
                    else
                    {
                        return PartialView("_AttendanceSettingsPopUp", model);

                    }
                }

                if (TabName == "STATUS")
                    return PartialView("_AttendanceStatusPopUp", model);
                if (TabName == "LEAVE")
                    return PartialView("_LeavePopUp", model);

                else
                    return PartialView("_AttendanceWagesPopUp", model);
          
                   

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

        public async Task<IActionResult> ExportToExcel(int TotalRows, int Payroll, int month, string PayrollGroupName, string MonthName)
        { 
            try
            {
                MonthlyAttendanceProcessing model = new MonthlyAttendanceProcessing();


                if (Global.GlobalFilter == null)
                    Global.GlobalFilter = new Filter();
                else
                {
                    Global.GlobalFilter.length = TotalRows;
                    Global.GlobalFilter.start = 0;
                }
                string FilterValues = await _helpers.GetFilterValues(Global.GlobalFilter);

                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Monthly Attendance Processing Popup", "Excel");
                int slNo = 0;

                Filter filter = new Filter();
                string currDateTime = await _helpers.GetCurrentDate();
                
                int count = 6;
                string columnName = "";
                int colcount = 0;
                int Columncount = 0;
                model = await _MonthlyAttendanceProcessingService.MonthlyAttendanceProcessingListPopUp(Global.monthCode, Global.PayrollCode, Global.Tabindex, Global.rowName, filter);
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Monthly Salary Processing");
                    if (Global.tabName == "ATTENDANCE_SETTINGS")
                    {
                        if (Global.rowName == "Daily Attendance Processing Pending Days")
                        {
                            count = 2;
                        }

                    }

                   
                        colcount = model.MonthlyAttendanceProcessing_Validations_popup.Count + 4;
                    if (Global.tabName == "ATTENDANCE_SETTINGS")
                    {
                        if (Global.rowName == "Daily Attendance Processing Pending Days")
                        {
                            colcount = model.MonthlyAttendanceProcessing_Validations_popup.Count + 8;

                        }
                    }

                            Columncount = count;
                    while (Columncount > 0)
                    {
                        int modulo = (Columncount - 1) % 26;
                        columnName = Convert.ToChar('A' + modulo) + columnName;
                        Columncount = (Columncount - modulo) / 26;
                    }

                    string colName = columnName;


                    worksheet.Range("A1", colName + colcount).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1", colName + colcount).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1", colName + colcount).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1", colName + colcount).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1", colName + colcount).Style.Border.LeftBorder = XLBorderStyleValues.Thin;

                    var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, colName));
                    wsReportNameHeaderRange.Style.Font.Bold = true;
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Merge();
                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191); ;
                    wsReportNameHeaderRange.Value = "Monthly Attendance Processing " + Global.tabName;



                    if (Global.tabName == "ATTENDANCE_SETTINGS")
                    {
                        if (Global.rowName == "Daily Attendance Processing Pending Days")
                        {
                            worksheet.Cell(2, 1).Style.Font.Bold = true;
                            worksheet.Cell(2, 1).Value = "Group Company Name";
                            worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();


                            worksheet.Cell(3, 1).Style.Font.Bold = true;
                            worksheet.Cell(3, 1).Value = "PayRoll Group";
                            worksheet.Cell(3, 2).Value = PayrollGroupName;
                            worksheet.Cell(4, 1).Style.Font.Bold = true;
                            worksheet.Cell(4, 1).Value = "Month";
                            worksheet.Cell(4, 2).Value = MonthName.ToString();


                            worksheet.Cell(5, 1).Style.Font.Bold = true;
                            worksheet.Cell(5, 1).Value = "Login User";
                            worksheet.Cell(5, 2).Value = LoginUser;
                            worksheet.Cell(6, 1).Style.Font.Bold = true;
                            worksheet.Cell(6, 1).Value = "Date & Time";
                            worksheet.Cell(6, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(6, 2).Style.DateFormat.Format = "dd-MMM-yyyy   h:mm AM/PM";
                            worksheet.Cell(6, 2).Value = currDateTime.ToString();
                            worksheet.Cell(7, 1).Style.Font.Bold = true;
                            worksheet.Cell(7, 1).Value = "Total Count";
                            worksheet.Cell(7, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(7, 2).Value = model.MonthlyAttendanceProcessing_Validations_popup.Count;
                        }
                        else
                        {
                            worksheet.Cell(2, 1).Style.Font.Bold = true;
                            worksheet.Cell(2, 1).Value = "Company Name";
                            worksheet.Cell(2, 2).Value = CompanyName.ToUpper();


                            worksheet.Cell(2, 3).Style.Font.Bold = true;
                            worksheet.Cell(2, 3).Value = "PayRoll Group";
                            worksheet.Cell(2, 4).Value = PayrollGroupName;
                            worksheet.Cell(2, 5).Style.Font.Bold = true;
                            worksheet.Cell(2, 5).Value = "Month";
                            worksheet.Cell(2, 6).Value = MonthName.ToString();


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
                            worksheet.Cell(3, 6).Value = model.MonthlyAttendanceProcessing_Validations_popup.Count;
                        }
                    }
                  
                      
                    
                    var currentRow = 4;
                    if (Global.tabName == "ATTENDANCE_SETTINGS")
                    {
                        if (Global.rowName == "Daily Attendance Processing Pending Days")
                        {
                             currentRow = 8;
                        }
                    }



                    #region Header
                    worksheet.Range("A"+ currentRow, colName + currentRow).Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);

                    if (Global.tabName == "ATTENDANCE_SETTINGS")
                    {
                        if (Global.rowName == "Daily Attendance Processing Pending Days")
                        {
                            worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                            worksheet.Cell(currentRow, 1).Value = "Sl.No";
                            worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                            worksheet.Cell(currentRow, 2).Value = "Date";
                        }
                    }
                    
                        worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 1).Value = "Sl.No";
                        worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 2).Value = "ID";
                        worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 3).Value = "Employee Name";
                        worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 4).Value = "Designation";
                        worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 5).Value = "Department";
                        worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 6).Value = "Location";

                    



                    foreach (var item in model.MonthlyAttendanceProcessing_Validations_popup)
                    {
                        currentRow++;
                        slNo = slNo + 1;

                        if (Global.tabName == "ATTENDANCE_SETTINGS")
                        {
                            if (Global.rowName == "Daily Attendance Processing Pending Days")
                            {
                                worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                                worksheet.Cell(currentRow, 1).Value = item.SlNo;
                                worksheet.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                                worksheet.Cell(currentRow, 2).Value = item.PendingDates;
                                worksheet.Cell(currentRow, 2).Style.DateFormat.Format = "dd-MMM-yyyy";

                            }
                        }
                      
                            worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 1).Value = slNo;
                            worksheet.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 2).Value = item.EmployeeID;
                            worksheet.Cell(currentRow, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 3).Value = item.FullName;
                            worksheet.Cell(currentRow, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 4).Value = item.Designation;
                            worksheet.Cell(currentRow, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 5).Value = item.Department;
                            worksheet.Cell(currentRow, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 6).Value = item.Location;
                        



                        worksheet.Columns(1, 20).Style.Alignment.WrapText = true;
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
                          "Monthly Attendance processing " + " List.xlsx"
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

        public async Task<IActionResult> GetBlankStatDaysPopList(Guid EmployeeGI,int month)
        {
            try
            {
                MonthlyAttendanceProcessing model = new MonthlyAttendanceProcessing();
                model = await _MonthlyAttendanceProcessingService.GetBlankStatDaysPopList(EmployeeGI,month);

                return PartialView("_DaysPopUp", model);
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
        public async Task<IActionResult> AttendanceProcessingSave(Filter filter, int Month, int PayrollGroup)
        { 
            try
            {               
                var response = await _MonthlyAttendanceProcessingService.ProcessingSave(Month, PayrollGroup, 5, filter);
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
        public async Task<IActionResult> ProcessingPeriodLock(Filter filter, int Month, int PayrollGroup)
        {
            try
            {
                var response = await _MonthlyAttendanceProcessingService.ProcessingPeriodLock(Month, PayrollGroup, 5, filter);
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
        public async Task<IActionResult> OnExportToExcelClick(int Month, int PayrollGroup, string MonthName, string PayrollGpName, string Period, int TotalRows)
        {
            try
            {

                if (Global.GlobalFilter == null)
                    Global.GlobalFilter = new Filter();
                else
                {
                    Global.GlobalFilter.length = TotalRows;
                    Global.GlobalFilter.start = 0;
                }
                string FilterValues = await _helpers.GetFilterValues(Global.GlobalFilter);

                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Monthly Attendance Processing", "Excel");

                if (FilterValues == "")
                {
                    FilterValues = "All";
                }

                string currDateTime = await _helpers.GetCurrentDate();
                

                //Get Datset
                var dataset = await _MonthlyAttendanceProcessingService.MonthlyAttendanceProcessingList_Excel(Month, PayrollGroup,5, Global.GlobalFilter);
                DataTable dt = dataset.Tables[0];


                //set EXCEL Column Heading 
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (dt.Columns[i].ColumnName == "doj" || dt.Columns[i].ColumnName == "dob" || dt.Columns[i].ColumnName == "y/s")
                        dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.ToUpper();
                    else
                        dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
                }

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Monthly Attendance Processing");
                    int count = dt.Rows.Count + 5;


                    worksheet.Range("A1:Z" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:Z" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:Z" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:Z" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:Z" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 22)));
                    wsReportNameHeaderRange.Style.Font.Bold = true;
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Merge();
                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191); ;
                    wsReportNameHeaderRange.Value = "Monthly Attendance Processing";

                    worksheet.Cell(2, 1).Style.Font.Bold = true;
                    worksheet.Cell(2, 1).Value = "Group Company Name";
                    worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                    worksheet.Cell(2, 3).Style.Font.Bold = true;
                    worksheet.Cell(2, 3).Value = "PayRoll Group";
                    worksheet.Cell(2, 4).Value = PayrollGpName;
                    worksheet.Cell(2, 5).Style.Font.Bold = true;
                    worksheet.Cell(2, 5).Value = "Month";
                    worksheet.Cell(2, 6).Value = MonthName;
                    worksheet.Cell(3, 4).Style.DateFormat.Format = "dd-MMM";
                    worksheet.Cell(2, 7).Style.Font.Bold = true;
                    worksheet.Cell(2, 7).Value = "Period";
                    worksheet.Cell(2, 8).Value = Period;

                    worksheet.Cell(3, 1).Style.Font.Bold = true;
                    worksheet.Cell(3, 1).Value = "Login User";
                    worksheet.Cell(3, 2).Value = LoginUser;
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
                    worksheet.Cell(3, 6).Value = dt.Rows.Count;
                    worksheet.Cell(4, 1).Style.Font.Bold = true;
                    worksheet.Cell(4, 1).Value = "Filter Applied";
                    worksheet.Range("B4:Z4").Merge();
                    worksheet.Cell("B4").Value = FilterValues;



                    var currentRow = 5;

                    worksheet.Cell(currentRow, 1).InsertTable(dt);
                    worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                    worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                    worksheet.Range("A5:Z5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Range("A5:Z5").Style.Font.Bold = true;


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
                            "Monthly Attendance Processing List.xlsx"
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

        public async Task<IActionResult> ExportToExcel_PopUp(Filter filter, int Month, int PayrollGroup, int Tabindex,string MonthName,string PGName, int totRecords)
        {
            try
            {
                filter.length = totRecords;
                string FilterValues = await _helpers.GetFilterValues(filter);
                


                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Attendance Approval Pending Employees ", "Excel");



                if (FilterValues == "")
                {
                    FilterValues = "All";
                }
                string currDateTime = await _helpers.GetCurrentDate();
                

                var dataset = await _MonthlyAttendanceProcessingService.MonthlyAttendanceProcessingApprovalPendingPopUp_Excel(Month,PayrollGroup,Tabindex,filter);
                DataTable dt = dataset.Tables[0];
                

                //set EXCEL Column Heading Format
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (dt.Columns[i].ColumnName == "doj" || dt.Columns[i].ColumnName == "dob")
                        dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.ToUpper();
                    else
                        dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
                }

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Attendance Approval Pending Emp");
                    int count = dt.Rows.Count + 5;

                    worksheet.Range("A1:I" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:I" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:I" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:I" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:I" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 8)));
                    wsReportNameHeaderRange.Style.Font.Bold = true;
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Merge();
                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191); ;
                    wsReportNameHeaderRange.Value = "Attendance Approval Pending Employees";

                    worksheet.Cell(2, 1).Style.Font.Bold = true;
                    worksheet.Cell(2, 1).Value = "Group Company Name";
                    worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                    worksheet.Cell(2, 3).Style.Font.Bold = true;
                    worksheet.Cell(2, 3).Value = "Month";
                    worksheet.Cell(2, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(2, 4).Value = MonthName;
                    worksheet.Cell(2, 4).Style.DateFormat.Format = "MMM-yyyy";
                    //worksheet.Cell(2, 4).Value = currDateTime.ToString();
                    worksheet.Cell(2, 5).Style.Font.Bold = true;
                    worksheet.Cell(2, 5).Value = "Payroll Group";
                    worksheet.Cell(2, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(2, 6).Value = PGName;                    

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
                    worksheet.Cell(3, 6).Value = dt.Rows.Count;
                    worksheet.Cell(4, 1).Style.Font.Bold = true;
                    worksheet.Cell(4, 1).Value = "Filter Applied";
                    worksheet.Range("B4:I4").Merge();
                    worksheet.Cell("B4").Value = FilterValues;



                    var currentRow = 5;

                    worksheet.Cell(currentRow, 1).InsertTable(dt);


                    worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                    worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                    worksheet.Range("A5:I5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Range("A5:I5").Style.Font.Bold = true;


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
                            book.Protect(EP.Password, false, false);
                            book.SaveToStream(stream);
                        }
                        var content = stream.ToArray();
                        return File(
                            content,
                            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                            "Attendance Approval Pending Employees List.xlsx"
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
