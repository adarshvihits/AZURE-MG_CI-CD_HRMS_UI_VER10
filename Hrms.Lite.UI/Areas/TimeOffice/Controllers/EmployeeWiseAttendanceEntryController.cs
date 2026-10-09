using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.TimeOffice;
using Hrms.Lite.Shared.Account;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TimeOffice;
using Hrms.Lite.Shared.TimeOffice.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Spire.Xls;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.TimeOffice.Controllers 
{
    [Area("TimeOffice")]
    [SessionAuthorize]
    public class EmployeeWiseAttendanceEntryController : Controller
    { 

        private readonly IDropdownService _dropdownService;
        private readonly IDailyAttendanceEntryService _dailyAttendanceEntryService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        public static class Global
        {
            public static Filter GlobalFilter;
        }

        public EmployeeWiseAttendanceEntryController(IDropdownService dropdownService, ICommonService commonService, IDailyAttendanceEntryService dailyAttendanceEntryService, IHelpers helpers)
        {
            _dailyAttendanceEntryService = dailyAttendanceEntryService;
              _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = helpers;
        }

        public static class Global_EmployeeWise
        {
            public static int MonthCode;
            public static Filter GlobalFilter;

        }

        public async Task<IActionResult> Index(int? Month) 
        {
            try { 
            DailyAttendanceEntry model = new DailyAttendanceEntry();            
            Filter filter = new Filter();
                filter.length = 10;
                var payrollList = await _dropdownService.GetPayrollGroupWithGrpCmpyCheckDropdown();
                if(payrollList.Count>0)
                ViewBag.Month = new SelectList(await _dropdownService.GetDailyAttendanceEntry_MonthDropdown(payrollList[0].Code), "Code", "Name");
                else
                ViewBag.Month = new SelectList(await _dropdownService.GetDailyAttendanceEntry_MonthDropdown(0), "Code", "Name");

                ViewBag.PGroup = new SelectList(payrollList, "Code", "Name");
                await fillfilterDropdown();
                model = await _dailyAttendanceEntryService.EmployeeWiseDailyAttendanceEntryList(500, filter, 0);
                return View("Index", model);
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
        public async Task<IActionResult> GetEmployeeWiseList(Filter filter1,int Month, int PgGroup)
        {
            try
            {
                DailyAttendanceEntry model = new DailyAttendanceEntry();
                DailyAttendanceEntry m = new DailyAttendanceEntry();

                Filter filter = new Filter();
                Global.GlobalFilter = filter1;


                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }
                if (Month != 0 && PgGroup!=0)
                {
                    model = await _dailyAttendanceEntryService.EmployeeWiseDailyAttendanceEntryList(Month,filter1, PgGroup);

                }

                m.EmployeeWiseAttendanceList = model.EmployeeWiseAttendanceList;
                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.EmployeeWiseAttendanceList == null)
                {

                    return Json(new
                    {
                        data = "",
                        draw = filter1.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows
                    });
                }

                if (model.EmployeeWiseAttendanceList.Count > 0)
                {
                    totalrows = m.EmployeeWiseAttendanceList[0].active;

                    //m.OnBoardingList.ToList<Hrms.Lite.Shared.Master.Test>().Count; ;
                }

                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = m.EmployeeWiseAttendanceList[0].totalRecords;

                    return Json(new
                    {
                        data = model.EmployeeWiseAttendanceList,
                        draw = filter1.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows//totalrowsafterfiltering
                    });
                }
                else
                {
                    return Json(new
                    {
                        data = model.EmployeeWiseAttendanceList,
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
        
        public async Task<IActionResult> EmployeeWiseAttendanceEdit(Guid EmployeeGI, int Month)
        {
            try { 
            DailyAttendanceEntry model = new DailyAttendanceEntry();
            Global_EmployeeWise.MonthCode = Month;
            int ABSTATUS = 0;
            model = await _dailyAttendanceEntryService.EmployeeDailyAttendanceEntryEditDetails(Month, ABSTATUS, EmployeeGI);                
             model.EmployeeHeader = await _commonService.GetEmployeeHeader(EmployeeGI);
             model.EmployeeGI = EmployeeGI;
           return View("EmployeeWiseAttendanceEditIndex", model);
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
        public async Task<IActionResult> EmployeeWiseAttendanceEditList(Guid EmployeeGI, int Month, int ABSTATUS)
        {
            try { 
            DailyAttendanceEntry model = new DailyAttendanceEntry();
            model = await _dailyAttendanceEntryService.EmployeeDailyAttendanceEntryEditDetails(Month, ABSTATUS, EmployeeGI);
                model.Month = new Shared.TimeOffice.Generic.Master.Month();
                model.Month.Code = Global_EmployeeWise.MonthCode;
                model.EmployeeGI = EmployeeGI;

                return PartialView("_EmployeeWiseAttendanceEditList", model);
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

        public async Task<IActionResult> BatchUpdate(Guid EmployeeGI, string MonthName)
        {
            try { 
            DailyAttendanceEntry model = new DailyAttendanceEntry();
            model.Employee_DailyAttendanceBatchUpdate = new Employee_DailyAttendanceBatchUpdate();
            model.Employee_DailyAttendanceBatchUpdate.FirstHalf = new AttnDayStatus();
            model.Employee_DailyAttendanceBatchUpdate.SecondHalf = new AttnDayStatus();
            model.Employee_DailyAttendanceBatchUpdate.Month = new Shared.TimeOffice.Generic.Master.Month();

            model.Employee_DailyAttendanceBatchUpdate.EmployeeGI = EmployeeGI;
            model.Employee_DailyAttendanceBatchUpdate.Month.Name = MonthName;

            ViewBag.AttendenceStatus = new SelectList(await _dropdownService.GetAttendanceStatusDropdown(), "Code", "Name");

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



        [HttpPost]
        public async Task<IActionResult> BatchUpdateSave(DailyAttendanceEntry model)
        {
            try { 
            var response = await _dailyAttendanceEntryService.EmployeeWiseAttendanceEntryBatchUpdate(model);
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

        public async Task<IActionResult> Edit(Guid EmployeeGI, string Date, string Mode)
        {
            try { 
            DailyAttendanceEntry model = new DailyAttendanceEntry();
            model = await _dailyAttendanceEntryService.DailyAttendanceEntryEditDetails(Date, EmployeeGI);
            ViewBag.AttendenceStatus = new SelectList(await _dropdownService.GetAttendanceStatusDropdown(), "Code", "Name");
                ViewBag.CoffEntitleDates = new SelectList(await _dropdownService.GetCOFFEntitlesDropDown(EmployeeGI, Date, Mode), "Name", "Name");
                return PartialView("_Edit", model);
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
        public async Task<IActionResult> EditSave(DailyAttendanceEntry model)
        {
            try { 
            var response = await _dailyAttendanceEntryService.DailyattendanceEntry(model);
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





        public async Task<IActionResult> ExportToExcel_EmployeeAttendanceList(int Month, string Mname, int totalRecords, int PgGRoup, string Pname)
        {
            try
            {

                if (Global.GlobalFilter == null)
                    Global.GlobalFilter = new Filter();
                else
                {
                    Global.GlobalFilter.length = totalRecords;
                    Global.GlobalFilter.start = 0;
                }
                string FilterValues = await _helpers.GetFilterValues(Global.GlobalFilter);

                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Employee Wise Attendance Summary(" + Mname + ")", "Excel");

                if (FilterValues == "")
                {
                    FilterValues = "All";
                }

                string currDateTime = await _helpers.GetCurrentDate();
                

                //Get DATASET
                var dataset = await _dailyAttendanceEntryService.EmployeeWiseDailyAttendanceEntryList_Excel(Month, Global.GlobalFilter, PgGRoup);
                DataTable dt = dataset.Tables[0];


                //set EXCEL Column Heading 
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (dt.Columns[i].ColumnName == "doj" || dt.Columns[i].ColumnName == "dob" || dt.Columns[i].ColumnName == "y/s"||
                        dt.Columns[i].ColumnName == "lop" || dt.Columns[i].ColumnName == "wo" || dt.Columns[i].ColumnName == "hd"
                        || dt.Columns[i].ColumnName == "op" || dt.Columns[i].ColumnName == "hp" || dt.Columns[i].ColumnName == "pr"
                        || dt.Columns[i].ColumnName == "od" || dt.Columns[i].ColumnName == "wfh" || dt.Columns[i].ColumnName == "ljp"
                        || dt.Columns[i].ColumnName == "ltp" || dt.Columns[i].ColumnName == "esi" || dt.Columns[i].ColumnName == "ab")
                        dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.ToUpper();
                    else
                        dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
                }

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("EmployeeWise Attendance");
                    int count = dt.Rows.Count + 5;


                    worksheet.Range("A1:T" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:T" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:T" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:T" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:T" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 19)));
                    wsReportNameHeaderRange.Style.Font.Bold = true;
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Merge();
                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191); ;
                    wsReportNameHeaderRange.Value = "Employee Wise Attendance Summary(" + Mname + ")";

                    worksheet.Cell(2, 1).Style.Font.Bold = true;
                    worksheet.Cell(2, 1).Value = "Group Company Name";
                    worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                    worksheet.Cell(2, 3).Style.Font.Bold = true;
                    worksheet.Cell(2, 3).Value = "Month";
                    worksheet.Cell(2, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(2, 4).Style.DateFormat.Format = "MMM-yyyy";
                    worksheet.Cell(2, 4).Value = Mname;
                    worksheet.Cell(2, 5).Style.Font.Bold = true;
                    worksheet.Cell(2, 5).Value = "Payroll Group";
                    worksheet.Cell(2, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);     
                    worksheet.Cell(2, 6).Value = Pname;
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
                    worksheet.Range("A4:T4").Merge();
                    worksheet.Cell("A4").Value = FilterValues;



                    var currentRow = 5;

                    worksheet.Cell(currentRow, 1).InsertTable(dt);
                    worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                    worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                    worksheet.Range("A5:T5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Range("A5:T5").Style.Font.Bold = true;
                    //// Wrap text for all columns to prevent unnecessary expansion
                    //worksheet.Columns().Style.Alignment.WrapText = true;
                    ////worksheet.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    //// Set column width based on the length of headers
                    //for (int col = 1; col <= dt.Columns.Count; col++)
                    //{
                    //    string headerText = worksheet.Cell(5, col).GetString(); // Assuming row 5 contains headers
                    //    //int headerLength = headerText.Length;

                    //    // Set column width with a small buffer for readability
                    //    if (!string.IsNullOrEmpty(headerText))
                    //    {
                    //        int headerLength = headerText.Length;

                    //        // Set a reasonable minimum and maximum width
                    //        int columnWidth = Math.Max(8, headerLength + 3); // Ensuring a minimum width of 10
                    //        worksheet.Column(col).Width = columnWidth;
                    //    }
                    //}

                    //// Adjust row height for headers (if necessary)
                    //worksheet.Row(5).Height = 20; // Adjust the height as per requirement


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
                            "Employee Wise Attendance Summary(" + Mname + ") List.xlsx"
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


        //public async Task<IActionResult> ExportToExcel_EmployeeAttendanceList(int  Month, string Mname,int totalRecords)
        //{
        //    try { 
        //    DailyAttendanceEntry model = new DailyAttendanceEntry();
        //    var LoginUser = HttpContext.Session.GetString("LoginUser");
        //    var CompanyName = HttpContext.Session.GetString("CompanyName");
        //        ExcelPassword EP = new ExcelPassword();
        //        EP = await _commonService.GetExcelPassword("Employee Wise Attendance Summary("+Mname+")", "Excel");
        //        int slNo = 0;
        //    if (Global.GlobalFilter == null)
        //        Global.GlobalFilter = new Filter();
        //        {
        //            Global.GlobalFilter.length = totalRecords;
        //            Global.GlobalFilter.start = 0;
        //        }
        //        string FilterValues = await _helpers.GetFilterValues(Global.GlobalFilter);

        //    model = await _dailyAttendanceEntryService.EmployeeWiseDailyAttendanceEntryList(Month, Global.GlobalFilter);
        //    if (FilterValues == "")
        //    {
        //        FilterValues = "All";
        //    }
        //    string currDateTime = await _helpers.GetCurrentDate();
        //    

        //    using (var workbook = new XLWorkbook())
        //    {
        //        var worksheet = workbook.Worksheets.Add("EmployeeWise Attendance Summary");
        //        int count = model.EmployeeWiseAttendanceList.Count + 5;


        //        worksheet.Range("A1:Q" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
        //        worksheet.Range("A1:Q" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
        //        worksheet.Range("A1:Q" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
        //        worksheet.Range("A1:Q" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
        //        worksheet.Range("A1:Q" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;

        //        var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 16)));
        //        wsReportNameHeaderRange.Style.Font.Bold = true;
        //        wsReportNameHeaderRange.Style.Font.FontSize = 15;
        //        wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        //        wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
        //        wsReportNameHeaderRange.Merge();
        //        wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191); ;
        //        wsReportNameHeaderRange.Value = "EmployeeWise Attendance Summary";
        //        worksheet.Cell(2, 1).Style.Font.Bold = true;
        //        worksheet.Cell(2, 1).Value = "Company Name";
        //        worksheet.Cell(2, 2).Value = CompanyName.ToUpper();
        //        worksheet.Cell(2, 3).Style.Font.Bold = true;
        //        worksheet.Cell(2, 3).Value = "Month";
        //        worksheet.Cell(2, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //        worksheet.Cell(2, 4).Style.DateFormat.Format = "MMM-yyyy";
        //        worksheet.Cell(2, 4).Value = Mname;
        //        worksheet.Cell(3, 1).Style.Font.Bold = true;
        //        worksheet.Cell(3, 1).Value = "Login User";
        //        worksheet.Cell(3, 2).Value = LoginUser;
        //        worksheet.Cell(3, 3).Style.Font.Bold = true;
        //        worksheet.Cell(3, 3).Value = "Date & Time";
        //        worksheet.Cell(3, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //        worksheet.Cell(3, 4).Style.DateFormat.Format = "dd-MMM-yyyy   h:mm AM/PM";
        //        worksheet.Cell(3, 4).Value = currDateTime.ToString();
        //        worksheet.Cell(3, 5).Style.Font.Bold = true;
        //        worksheet.Cell(3, 5).Value = "Total Count";
        //        worksheet.Cell(3, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //        worksheet.Cell(3, 6).Value = model.EmployeeWiseAttendanceList.Count;
        //        worksheet.Cell(4, 1).Style.Font.Bold = true;
        //        worksheet.Cell(4, 1).Value = "Filter Applied";
        //        worksheet.Range("B4:Q4").Merge();
        //        worksheet.Cell("B4").Value = FilterValues;







        //        var currentRow = 5;
        //        #region Header
        //        worksheet.Cell(currentRow, 1).Value = "Sl.No";
        //        worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
        //        worksheet.Cell(currentRow, 2).Value = "ID";
        //        worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
        //        worksheet.Cell(currentRow, 3).Value = "EmployeeName";
        //        worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
        //        worksheet.Cell(currentRow, 4).Value = "AB";
        //        worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
        //        worksheet.Cell(currentRow, 5).Value = "Leave";
        //        worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
        //        worksheet.Cell(currentRow, 6).Value = "LOP";
        //        worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
        //        worksheet.Cell(currentRow, 7).Value = "WO";
        //        worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
        //        worksheet.Cell(currentRow, 8).Value = "HD";
        //        worksheet.Cell(currentRow, 8).Style.Font.Bold = true;
        //        worksheet.Cell(currentRow, 9).Value = "OP";
        //        worksheet.Cell(currentRow, 9).Style.Font.Bold = true;
        //        worksheet.Cell(currentRow, 10).Value = "HP";
        //        worksheet.Cell(currentRow, 10).Style.Font.Bold = true;
        //        worksheet.Cell(currentRow, 11).Value = "CO";
        //        worksheet.Cell(currentRow, 11).Style.Font.Bold = true;
        //        worksheet.Cell(currentRow, 12).Value = "PR";
        //        worksheet.Cell(currentRow, 12).Style.Font.Bold = true;
        //        worksheet.Cell(currentRow, 13).Value = "OD";
        //        worksheet.Cell(currentRow, 13).Style.Font.Bold = true;
        //        worksheet.Cell(currentRow, 14).Value = "RA";
        //        worksheet.Cell(currentRow, 14).Style.Font.Bold = true;
        //        worksheet.Cell(currentRow, 15).Value = "WFH";
        //        worksheet.Cell(currentRow, 15).Style.Font.Bold = true;
        //        worksheet.Cell(currentRow, 16).Value = "LJP";
        //        worksheet.Cell(currentRow, 16).Style.Font.Bold = true;
        //        worksheet.Cell(currentRow, 17).Value = "LTP";
        //        worksheet.Cell(currentRow, 17).Style.Font.Bold = true;

        //        #endregion

        //        #region Body
        //        foreach (var item in model.EmployeeWiseAttendanceList)
        //        {
        //            currentRow++;
        //            slNo = slNo + 1;
        //            worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //            worksheet.Range("A5:Q5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
        //            worksheet.Cell(currentRow, 1).Value = slNo;
        //            worksheet.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //            worksheet.Cell(currentRow, 2).Value = item.EmployeeID;
        //            worksheet.Cell(currentRow, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //            worksheet.Cell(currentRow, 3).Value = item.FullName;
        //            worksheet.Cell(currentRow, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //            worksheet.Cell(currentRow, 4).Value = item.Absent;
        //            worksheet.Cell(currentRow, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //            worksheet.Cell(currentRow, 5).Value = item.Leave;
        //            worksheet.Cell(currentRow, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //            worksheet.Cell(currentRow, 6).Value = item.Lop;
        //            worksheet.Cell(currentRow, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //            worksheet.Cell(currentRow, 7).Value = item.OffDay;
        //            worksheet.Cell(currentRow, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //            worksheet.Cell(currentRow, 8).Value = item.HoliDay;
        //            worksheet.Cell(currentRow, 9).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //            worksheet.Cell(currentRow, 9).Value = item.OffdayPresent;
        //            worksheet.Cell(currentRow, 10).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //            worksheet.Cell(currentRow, 10).Value = item.HolidayPresent;
        //            worksheet.Cell(currentRow, 11).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);

        //            worksheet.Cell(currentRow, 11).Value = item.Coff;
        //            worksheet.Cell(currentRow, 12).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //            worksheet.Cell(currentRow, 12).Value = item.PresentDays;
        //            worksheet.Cell(currentRow, 13).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //            worksheet.Cell(currentRow, 13).Value = item.OnDuty;
        //            worksheet.Cell(currentRow, 14).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //            worksheet.Cell(currentRow, 14).Value = item.Ra;
        //            worksheet.Cell(currentRow, 15).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //            worksheet.Cell(currentRow, 15).Value = item.Wfh;
        //            worksheet.Cell(currentRow, 16).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //            worksheet.Cell(currentRow, 16).Value = item.Ljp;
        //            worksheet.Cell(currentRow, 17).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //            worksheet.Cell(currentRow, 17).Value = item.Ltp;
        //            worksheet.Columns(1, 17).Style.Alignment.WrapText = true;
        //            worksheet.Rows().Style.Alignment.WrapText = true;
        //        }
        //        #endregion
        //        worksheet.Columns().AdjustToContents();
        //        worksheet.Rows().AdjustToContents();
        //        using (var stream = new MemoryStream())
        //        {
        //            workbook.SaveAs(stream);
        //                if (EP.NeedPassword)
        //                {
        //                    Workbook book = new Workbook();
        //                    book.LoadFromStream(stream);
        //                    //Protect Workbook
        //                    book.Protect(EP.Password,false,false);
        //                    book.SaveToStream(stream);
        //                }
        //                var content = stream.ToArray();
        //            return File(
        //                content,
        //                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
        //                "EmployeeWise Attendance Summary.xlsx"
        //                );
        //        }
        //    }
        //}
        //    catch (UnauthorizedAccessException e)
        //    {

        //        return RedirectToAction("Login", "Account", new { Area = "" });
        //    }
        //    catch (Exception e)
        //    {
        //        return RedirectToAction("Error", "Account", new { Area = "" });
        //    }
        //    //}
        //}



        public async Task<IActionResult> ExportToExcel_EmployeeAttendanceEDitList(Guid EmployeeGI, int Month, int ABSTATUS,string Mname)
        {
            try { 
            DailyAttendanceEntry model = new DailyAttendanceEntry();      
            var LoginUser = HttpContext.Session.GetString("LoginUser");
            var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Monthly Attendace Summary (" + Mname + ")", "Excel");
                int slNo = 0;
           
            model = await _dailyAttendanceEntryService.EmployeeDailyAttendanceEntryEditDetails(Month, ABSTATUS, EmployeeGI);
           
            model.EmployeeHeader = await _commonService.GetEmployeeHeader(EmployeeGI);          
            string currDateTime = await _helpers.GetCurrentDate();
            

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Monthly Attendance Summary");
                int count = model.Employee_DailyAttendanceEditDetails.Count + 4;


                worksheet.Range("A1:J" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1:J" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1:J" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1:J" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1:J" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;

                var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 9)));
                wsReportNameHeaderRange.Style.Font.Bold = true;
                wsReportNameHeaderRange.Style.Font.FontSize = 15;
                wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                wsReportNameHeaderRange.Merge();
                wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191); ;

                if (ABSTATUS==1)
                wsReportNameHeaderRange.Value = "Monthly Attendance Summary(Absent Days Only)";
                else
                    wsReportNameHeaderRange.Value = "Monthly Attendance Summary";

                worksheet.Cell(2, 1).Style.Font.Bold = true;
                worksheet.Cell(2, 1).Value = "Group Company Name";
                worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                worksheet.Cell(2, 3).Style.Font.Bold = true;
                worksheet.Cell(2, 3).Value = "Month";
                worksheet.Cell(2, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                worksheet.Cell(2, 4).Style.DateFormat.Format = "MMM-yyyy";
                worksheet.Cell(2, 4).Value = Mname;
                worksheet.Cell(2, 5).Style.Font.Bold = true;
                worksheet.Cell(2, 5).Value = "Employee Name";
                worksheet.Cell(2, 6).Value = model.EmployeeHeader.EmployeeName;
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
                worksheet.Cell(3, 6).Value = model.Employee_DailyAttendanceEditDetails.Count;    
                var currentRow = 4;
                #region Header
                worksheet.Cell(currentRow, 1).Value = "Sl.No";
                worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 2).Value = "Date";
                worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 3).Value = "Day";
                worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 4).Value = "Sh.Code";
                worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 5).Value = "IN";
                worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 6).Value = "OUT";
                worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 7).Value = "Tot.Time";
                worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 8).Value = "FH";
                worksheet.Cell(currentRow, 8).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 9).Value = "SH";
                        worksheet.Cell(currentRow, 9).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 10).Value = "COFF Entitled Date";
                    worksheet.Cell(currentRow, 10).Style.Font.Bold = true;



                    #endregion

                    #region Body
                    foreach (var item in model.Employee_DailyAttendanceEditDetails)
                {
                    currentRow++;
                    slNo = slNo + 1;
                    worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(currentRow, 1).Value = slNo;
                    worksheet.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(currentRow, 2).Value = item.Date;
                        worksheet.Cell(currentRow, 2).Style.DateFormat.Format = "dd-MMM-yyyy";
                        worksheet.Cell(currentRow, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(currentRow, 3).Value = item.Day;
                    worksheet.Cell(currentRow, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(currentRow, 4).Value = item.Shift.Name;
                    worksheet.Cell(currentRow, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(currentRow, 5).Value = item.IN;
                    worksheet.Cell(currentRow, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(currentRow, 6).Value = item.OUT;
                    worksheet.Cell(currentRow, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(currentRow, 7).Value = item.TotalTime;
                        worksheet.Cell(currentRow, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 8).Value = item.FirstHalf.Name;
                        worksheet.Cell(currentRow, 9).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 9).Value = item.SecondHalf.Name;
                        worksheet.Cell(currentRow, 10).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                     worksheet.Cell(currentRow, 10).Style.DateFormat.Format = "dd-MMM-yyyy";
                     worksheet.Cell(currentRow, 10).Value = item.EntitleDate;

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
                        "Monthly Attendance Summary.xlsx"
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

