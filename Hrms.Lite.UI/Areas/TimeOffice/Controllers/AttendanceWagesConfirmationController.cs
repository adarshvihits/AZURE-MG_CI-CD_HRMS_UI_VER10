
using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.TimeOffice;
using Hrms.Lite.Shared.Account;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TimeOffice;
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
    public class AttendanceWagesConfirmationController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly IAttendanceWagesConfirmationService _attendanceWagesConfirmationService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        public AttendanceWagesConfirmationController(IDropdownService dropdownService, ICommonService commonService, IAttendanceWagesConfirmationService attendanceWagesConfirmationService,IHelpers helpers)
        {
            _attendanceWagesConfirmationService = attendanceWagesConfirmationService;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = helpers;

        }
        public static class Global
        {
         
            public static string TabValues;
            public static Filter GlobalFilter;
        }
        public async Task<IActionResult> Index()
        {
            try { 
            AttendanceWagesConfirmation model = new AttendanceWagesConfirmation();
            Filter filter = new Filter();               
            await fillfilterDropdown();             
            ViewBag.Month = new SelectList(await _dropdownService.GetSalaryProcessing_MonthDropdown(0), "Code", "Name");
                
                var payrollGroups = await _dropdownService.GetPayrollGroupDropdown();           
             ViewBag.PGroup = new SelectList(payrollGroups, "Code", "Name");
             model.ConfirmationStatus = 0;
            model.TabValues = "Holiday";
            
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

        //[HttpGet]
        public async Task<IActionResult> GetAttendancewagesList(int PGroup,int month, int ConfirmationStatus, string TabIndex)
        {
            try
            {


                AttendanceWagesConfirmation model = new AttendanceWagesConfirmation();

                ViewBag.MonthDropdown = new SelectList(await _dropdownService.GetMonthDropdown(), "Code", "Name");
                if (Global.GlobalFilter == null)
                    Global.GlobalFilter = new Filter();
                if(PGroup!=0 && month !=0)
                model = await _attendanceWagesConfirmationService.GetConfirmationList(TabIndex, PGroup, month, ConfirmationStatus, Global.GlobalFilter);
                model.TabValues = TabIndex;
                model.ConfirmationStatus = ConfirmationStatus;
                return PartialView("_AttendanceWagesConfirmationList", model);
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

        public async Task<IActionResult> EmployeeWiseAttendanceWagesConfirmedValueUpdate(string tabValue,int month,decimal confirmvalue, Guid EmployeeGI,string Mode)
        {
            AttendanceWagesConfirmation model = new AttendanceWagesConfirmation();
            model.TabValues = tabValue;
            model.Details = new AttendanceWagesConfirmationDetails();
            model.Details.EmployeeGI = EmployeeGI;
            model.Month = new Shared.TimeOffice.Generic.Master.Month();
            model.Month.Code = month;
            model.Details.ConfirmedValues = confirmvalue;
            model.Mode = Mode;
            var response = await _attendanceWagesConfirmationService.EmployeeWiseAttendanceWagesConfirmedValueUpdate(model);
            return Json(response);
        }


        [HttpPost]
        public async Task<IActionResult> AttendanceWagesConfirmationSave(AttendanceWagesConfirmation model)
        {
            try { 
            var response = new object();

            var list = from li in model.ConfirmationList
                       //where li.Applicable == true
                       select li;
            model.ConfirmationList = list.ToList();
            //model.Month.Code = model.MonthCode;
            response = await _attendanceWagesConfirmationService.AttendanceWagesConfirmationSave(model);
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

        [HttpGet]
        public ActionResult BatchUpdate(int Month, string MonName, string TabIndex, int PGroup, string PGroupName)
        {
            try
            {
                AttendanceWagesConfirmation model = new AttendanceWagesConfirmation();
                model.Month = new Shared.TimeOffice.Generic.Master.Month
                {
                    Code = Month,
                    Name = MonName
                };
                model.PGroup = new Dropdown
                {
                    Code = PGroup,
                    Name = PGroupName
                };
                model.Type = TabIndex;
              return PartialView("_BatchUpdate",model);
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
        public async Task<IActionResult> BatchUpdate(AttendanceWagesConfirmation model, Filter Filter)
        {
            try
            {
                model.Filter = Filter;
               var response = await _attendanceWagesConfirmationService.BatchUpdate(model);
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

        public async Task<IActionResult> ApplyFilter(string Tab,int month,int PGroup,int ConfirmationStatus, Filter filter)
        {
            try
            {
         
            AttendanceWagesConfirmation model = new AttendanceWagesConfirmation();

                if (filter == null)
                    filter = new Filter();            
           
                model = await _attendanceWagesConfirmationService.GetConfirmationList(Tab, PGroup, month, ConfirmationStatus, filter);
                Global.GlobalFilter = filter;
                model.TabValues = Tab;
                model.ConfirmationStatus = ConfirmationStatus;
            return PartialView("_AttendanceWagesConfirmationList", model);
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
        public async Task<IActionResult> GetEmployeeWisePopUpDetails(string TabIndex, int month, int Value, Guid EmployeeGI,string tabValue)
        {
            try { 
            AttendanceWagesConfirmation model = new AttendanceWagesConfirmation();
            EmployeeHeader header = new EmployeeHeader();
            header =await _commonService.GetEmployeeHeader(EmployeeGI);
            model = await _attendanceWagesConfirmationService.EmployeeWiseDetails(TabIndex,month, Value, EmployeeGI);
                var currMonth = await _commonService.GetCurrentMonthDropdown();
                int currentmonth = Convert.ToInt32(currMonth.Data);
                ViewBag.month = new SelectList(await _dropdownService.GetMonthDropdown(), "Code", "Name", currMonth.Data);
                model.EmployeeId = header.EmployeeId;
                model.EmployeeGI = EmployeeGI;
                if (model.Type == "Shift")
                    model.IDName = model.EmployeeName + "(" + model.EmployeeId + ")";
            return PartialView("_AttendanceWagesConfimationPopUp", model);
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
        public async Task<IActionResult> GetEditConfirmation(string TabIndex, int month, int ConfirmationStatus, Guid EmployeeGI)
        {
            try { 
            ViewBag.MonthDropdown = new SelectList(await _dropdownService.GetMonthDropdown(), "Code", "Name");
            AttendanceWagesConfirmation model = new AttendanceWagesConfirmation();
            model = await _attendanceWagesConfirmationService.GetEmployeeWiseData(month,TabIndex, ConfirmationStatus,EmployeeGI);
            model.Details.EmployeeGI = EmployeeGI;
            model.TabValues = TabIndex;
            model.Details.EmployeeHeader = await _commonService.GetEmployeeHeader(EmployeeGI);
            return PartialView("_EditAttendanceConfirmation",model);
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
        public async Task<IActionResult> EmployeeWiseSave(string TabIndex, Guid EmployeeGI,int? month,decimal Value,string Remarks)
        {
            try { 
            AttendanceWagesConfirmation model = new AttendanceWagesConfirmation();
            model.Details = new AttendanceWagesConfirmationDetails();
            model.Month = new Shared.TimeOffice.Generic.Master.Month();
            model.Details.EmployeeGI = EmployeeGI;
            model.TabValues = TabIndex;
            model.Month.Code = month;
            model.Details.ConfirmedValues = Value;
            model.Details.Remarks = Remarks;
            var response = await _attendanceWagesConfirmationService.EmployeeWiseSave(model);
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

        public async Task<IActionResult> ExportToExcel(string TabIndex,int PGroup,int month, int ConfirmationStatus,string MonthName, string PGroupName)
        {
            AttendanceWagesConfirmation model = new AttendanceWagesConfirmation();
            Filter filter = new Filter();

            int slno = 0;
            if (Global.GlobalFilter == null)
                Global.GlobalFilter = new Filter();
            string FilterValues = await _helpers.GetFilterValues(Global.GlobalFilter);
            if (FilterValues == "")
            {
                FilterValues = "All";
            }
            model = await _attendanceWagesConfirmationService.GetConfirmationList(TabIndex, PGroup,month, ConfirmationStatus, Global.GlobalFilter);

            string currDateTime = await _helpers.GetCurrentDate();
            
            var LoginUser = HttpContext.Session.GetString("LoginUser");
            var CompanyName = HttpContext.Session.GetString("CompanyName");

            var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
            var GroupCompanyName = User.authUser.GroupCompanyName;
            using (var workbook = new XLWorkbook())
            {

                var worksheet = workbook.Worksheets.Add("AttendanceWagesConfirmation");

                int PendingCountnum = model.ConfirmationList.Count + 5;

                string columnName = "";
                int Columncount = 0;
                if (TabIndex == "OTHours" || TabIndex == "LessHours")
                    Columncount = 10;
                else
                    Columncount = 8;


                while (Columncount > 0)
                {
                    int modulo = (Columncount - 1) % 26;
                    columnName = Convert.ToChar('A' + modulo) + columnName;
                    Columncount = (Columncount - modulo) / 26;
                }

                string colName = columnName;

                worksheet.Range("A1", colName + PendingCountnum).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1", colName + PendingCountnum).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1", colName + PendingCountnum).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1", colName + PendingCountnum).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1", colName + PendingCountnum).Style.Border.LeftBorder = XLBorderStyleValues.Thin;

                var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, colName));
                wsReportNameHeaderRange.Style.Font.Bold = true;
                wsReportNameHeaderRange.Style.Font.FontSize = 15;
                wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                wsReportNameHeaderRange.Merge();


                wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                wsReportNameHeaderRange.Style.Font.FontSize = 15;
                wsReportNameHeaderRange.Value = "AttendanceWagesConfirmation " + "(" + TabIndex + ")";

                worksheet.Range("A5:" + colName + "5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                worksheet.Range("A5:" + colName + "5").Style.Font.Bold = true;
                worksheet.Cell(2, 1).Style.Font.Bold = true;
                worksheet.Cell(2, 1).Value = "Group Company Name";
                worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                worksheet.Cell(2, 3).Style.Font.Bold = true;
                worksheet.Cell(2, 3).Value = "Month";
                worksheet.Cell(2, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                worksheet.Cell(2, 4).Value = MonthName;
                worksheet.Cell(2, 4).Style.DateFormat.Format = "MMM-yyyy";

                worksheet.Cell(2, 5).Style.Font.Bold = true;
                worksheet.Cell(2, 5).Value = "Payroll Group";
                worksheet.Cell(2, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                worksheet.Cell(2, 6).Value = PGroupName;


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
                worksheet.Cell(3, 6).Value = model.ConfirmationList.Count;
                worksheet.Cell(4, 1).Style.Font.Bold = true;
                worksheet.Cell(4, 1).Value = "Filter Applied";
                var filterapplied = worksheet.Range("B4", colName + 4).Merge();

                filterapplied.Value = FilterValues;

                var currentRow = 5;
                #region Header
                worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 1).Value = "Sl.No";
                worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 2).Value = "ID";
                worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 3).Value = "Employee Name";
                worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 4).Value = "Designaion";
                worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 5).Value = "Department";
                worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 6).Value = "Location";
                if (TabIndex == "Shift")
                {
                    worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 7).Value = "No.of Days";
                    worksheet.Cell(currentRow, 8).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 8).Value = "Amount";
                }
                if (TabIndex== "Holiday")
                {
                    worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 7).Value = "Holiday Worked";
                    worksheet.Cell(currentRow, 8).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 8).Value = "Conf Days";
                }
                if (TabIndex == "OTHours")
                {
                    worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 7).Value = "Act.OT Hours";
                    worksheet.Cell(currentRow, 8).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 8).Value = "Act.OT Mnts";
                    worksheet.Cell(currentRow, 9).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 9).Value = "Conf.OT Mnts";
                    worksheet.Cell(currentRow, 10).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 10).Value = "Conf.OT Mnts";
                }
                if (TabIndex == "OffDay")
                {
                    worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 7).Value = "Offday Worked";
                    worksheet.Cell(currentRow, 8).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 8).Value = "Conf Days";
                }
                if (TabIndex == "LessHours")
                {
                    worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 7).Value = "Act.Less Hours";
                    worksheet.Cell(currentRow, 8).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 8).Value = "Act.Less Mnts";
                    worksheet.Cell(currentRow, 9).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 9).Value = "Conf.Less Hours";
                    worksheet.Cell(currentRow, 10).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 10).Value = "Conf.Less Mnts";
                }
              




                #endregion

                #region Body
                foreach (var item in model.ConfirmationList)
                {
                    currentRow++;
                    slno = slno + 1;
                    worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(currentRow, 1).Value = slno;
                    worksheet.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(currentRow, 2).Value = item.EmployeeID;
                    worksheet.Cell(currentRow, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(currentRow, 3).Value = item.EmployeeName;
                    worksheet.Cell(currentRow, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(currentRow, 4).Value = item.Designation.Name;
                    worksheet.Cell(currentRow, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(currentRow, 5).Value = item.Department.Name;
                    worksheet.Cell(currentRow, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(currentRow, 6).Value = item.Location.Name;
                    if (TabIndex == "Shift")
                    {
                        worksheet.Cell(currentRow, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 7).Value = item.NoOfDays;
                        worksheet.Cell(currentRow, 8).Value = item.ConfirmedValues;
                    }
                  else  if (TabIndex == "Holiday"|| TabIndex == "OffDay")
                    {
                        worksheet.Cell(currentRow, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 7).Value = item.ActualValue;
                        worksheet.Cell(currentRow, 8).Value = item.ConfirmedValues;
                    }
                    else
                    {
                        worksheet.Cell(currentRow, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 7).Value = item.ActHrs;
                        worksheet.Cell(currentRow, 8).Value = item.ActMin;
                        worksheet.Cell(currentRow, 9).Value = item.ConfHrs;
                        worksheet.Cell(currentRow, 10).Value = item.ConfMin;

                    }
                    worksheet.Columns(1, 11).Style.Alignment.WrapText = true;

                }
                #endregion
                worksheet.Columns().AdjustToContents();
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        " Attendance wages confirmation " + TabIndex + " List.xlsx"
                        );
                }
            }

        }

        public async Task<IActionResult> Pop_Up_ExportToExcel(string TabIndex, int month, int ConfirmationStatus, string MonthName, Guid EmployeeGI,string Name)
        {

            try
            {

            
            AttendanceWagesConfirmation model = new AttendanceWagesConfirmation();
            Filter filter = new Filter();

            int slno = 0;
            if (Global.GlobalFilter == null)
                Global.GlobalFilter = new Filter();
            string FilterValues = await _helpers.GetFilterValues(Global.GlobalFilter);
            if (FilterValues == "")
            {
                FilterValues = "All";
            }
            var dataset = await _attendanceWagesConfirmationService.GetEmployeeAttnDetails_Excel(month, TabIndex, ConfirmationStatus, EmployeeGI);
            DataTable dt = dataset.Tables[0];

            for (int i = 0; i < dt.Columns.Count; i++)
            {
                if (dt.Columns[i].ColumnName == "doj" || dt.Columns[i].ColumnName == "dob" || dt.Columns[i].ColumnName == "y/s")
                    dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.ToUpper();
                else
                    dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
            }

            var LoginUser = HttpContext.Session.GetString("LoginUser");
            var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
            EP = await _commonService.GetExcelPassword("Attendance Wages Confirmation", "Excel");


            string currDateTime = await _helpers.GetCurrentDate();
            



            using (var workbook = new XLWorkbook())
            {

                var worksheet = workbook.Worksheets.Add(TabIndex);

                    int count = dt.Rows.Count;


                worksheet.Range("A1:G" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1:G" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1:G" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1:G" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1:G" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 6)));
                wsReportNameHeaderRange.Style.Font.Bold = true;
                wsReportNameHeaderRange.Style.Font.FontSize = 15;
                wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                wsReportNameHeaderRange.Merge();


                wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                wsReportNameHeaderRange.Style.Font.FontSize = 15;
                wsReportNameHeaderRange.Value = "AttendanceWagesConfirmation " + "(" + TabIndex + ")";
                worksheet.Range("A4:F4").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                worksheet.Cell(2, 1).Style.Font.Bold = true;
                worksheet.Cell(2, 1).Value = "Group Company Name";
                worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                worksheet.Cell(2, 3).Style.Font.Bold = true;
                worksheet.Cell(2, 3).Value = "Month";
                worksheet.Cell(2, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                worksheet.Cell(2, 4).Value = MonthName;
                worksheet.Cell(2, 4).Style.DateFormat.Format = "dd-MMM-yyyy";


                    worksheet.Cell(2, 5).Style.Font.Bold = true;
                    worksheet.Cell(2, 5).Value = "Employee Name";
                    worksheet.Cell(2, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(2, 6).Value = Name;
       



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
                    worksheet.Cell(3, 7).Value = dt.Rows.Count;

                   

                var currentRow = 4;


                worksheet.Cell(currentRow, 1).InsertTable(dt);
                worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                worksheet.Range("A4:G4").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                worksheet.Range("A4:G4").Style.Font.Bold = true;


                worksheet.Columns().AdjustToContents();
                worksheet.Rows().AdjustToContents();

                //using (var stream = new MemoryStream())
                //{
                //    workbook.SaveAs(stream);
                //    var content = stream.ToArray();
                //    return File(
                //        content,
                //        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                //        " Attendance wages confirmation " + TabIndex + " List.xlsx"
                //        );
                //}

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
                           "Attendance wages confirmation" + TabIndex + "List.xlsx"
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
