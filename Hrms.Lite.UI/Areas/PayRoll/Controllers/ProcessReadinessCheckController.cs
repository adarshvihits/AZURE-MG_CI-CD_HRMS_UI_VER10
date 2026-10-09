using ClosedXML.Excel;
using ExcelDataReader;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.PayRoll;
using Hrms.Lite.Shared.Account;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll;
using Microsoft.AspNetCore.Hosting;
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
using Hrms.Lite.Shared.TimeOffice;
using Hrms.Lite.Services.IServices.TimeOffice;

namespace Hrms.Lite.UI.Areas.PayRoll.Controllers
{
    [Area("PayRoll")]
    [SessionAuthorize]
    public class ProcessReadinessCheckController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly ICommonService _commonService;
        private readonly IProcessReadinessCheckService _Service;
        private readonly IHelpers _helpers;
        private readonly IWebHostEnvironment _environment;

        public ProcessReadinessCheckController(IDropdownService dropdownService, ICommonService commonService, IProcessReadinessCheckService processReadinessCheckService, IHelpers Helpers, IWebHostEnvironment environment)
        {
            _dropdownService = dropdownService;
            _commonService = commonService;
            _Service = processReadinessCheckService;
            _helpers = Helpers;
            _environment = environment;
        }

        public static class Global
        {
            public static Shared.General.Filter GlobalFilter;
            public static string TabName;
            public static string RowName;

        }


        public async Task<IActionResult> Index()
        {
            try
            {
                ProcessReadinessCheck model = new ProcessReadinessCheck();
                model.Filter = new Filter();
                ViewBag.PayrollGroup = new SelectList(await _dropdownService.GetPayrollGroupDropdown(), "Code", "Name");
                ViewBag.Month = new SelectList(await _dropdownService.GetSalaryProcessing_MonthDropdown(0), "Code", "Name");
                model = await _Service.GetProcessReadinessCheckList(0, 0);
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


        public async Task<IActionResult> GetProcessReadinessCheckList(int PayrollGroup,int Month)
        {
            try
            {
                ProcessReadinessCheck model = new ProcessReadinessCheck();

        
                    model = await _Service.GetProcessReadinessCheckList(Month, PayrollGroup);
                    ViewBag.PayrollGroup = new SelectList(await _dropdownService.GetPayrollGroupDropdown(), "Code", "Name");
                    ViewBag.Month = new SelectList(await _dropdownService.GetSalaryProcessing_MonthDropdown(0), "Code", "Name");
                         
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


        [HttpGet]
        public async Task<ActionResult> GetEmployeeDetails(int Month, int PayrollGroup, int Code, string RowName, string TabName)
        {
            try
            {
                ProcessReadinessCheck model = new ProcessReadinessCheck();
                ViewBag.TabName = TabName;
                ViewBag.RowName = RowName;
               
                model = await _Service.SalaryProcessReadinessCheckPopUp(Month, PayrollGroup, TabName, RowName);
               
                if (TabName == "HR_TIMEOFFICE")
                    return PartialView("_HrTimeOfficePopUp", model);
                if (TabName == "STATUTORY")
                    return PartialView("_StatutoryPopUp", model);
                if (TabName == "PAYROLL_SETTINGS")
                    return PartialView("_PayRollSettingsPopUp", model);
                else
                    return PartialView("_PayRollTrnPopUp", model);

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

        public async Task<ActionResult> GetPopUpDetails(int Month, int PayrollGroup, string RowName, string ProcessType, int AttendType, string SalaryType)
        {
            try
            {
                ProcessReadinessCheck model = new ProcessReadinessCheck();
                Filter filter = new Filter();               
             
                ViewBag.RowName = RowName;
                ViewBag.SalaryType = SalaryType;


                if (ProcessType== "Attendance")
                {
                        model = await _Service.MonthlyAttendanceProcessingListPopUp(Month, PayrollGroup, AttendType, RowName, filter);
                        if (AttendType == 1) //ATTENDANCE_SETTINGS
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

                    if (AttendType == 2) //Attendance Statuus
                        return PartialView("_AttendanceStatusPopUp", model);
                    if (AttendType == 3)//LEAVE
                        return PartialView("_LeavePopUp", model);

                    else
                        return PartialView("_AttendanceWagesPopUp", model);
                }
                if (ProcessType == "Salary")
                {
                    model = await _Service.SalaryProcessReadinessCheckPopUp(Month, PayrollGroup, SalaryType, RowName);
                    if (SalaryType == "HR_TIMEOFFICE")
                        return PartialView("_HrTimeOfficePopUp", model);
                    if (SalaryType == "STATUTORY")
                        return PartialView("_StatutoryPopUp", model);
                    if (SalaryType == "PAYROLL_SETTINGS")
                        return PartialView("_PayRollSettingsPopUp", model);
                    else
                        return PartialView("_PayRollTrnPopUp", model);
                }
                else
                {
                    return RedirectToAction("Index");
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
        public async Task<IActionResult> GetProcessReadinessCheckList_Excel(int SalaryPeriodCode, int PayrollGroup, string SalPeriodName, string PayrollName, int totRecords)

        {
            try
            {


                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("AttendanceProcessReadinessCheck", "Excel");




                string currDateTime = await _helpers.GetCurrentDate();
                

                var dataset = await _Service.GetProcessReadinessCheckList_Excel(SalaryPeriodCode, PayrollGroup);
               
                DataTable dt = dataset.Tables[0];
               

             
                //set EXCEL Column Heading Format
                for (int i = 0; i < dt.Columns.Count; i++)
                {

                    dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
                }

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if(dt.Columns[0].ColumnName=="slNo")
                    {
                        dt.Rows[i][0] = (i+1);
                    }
                  
                }

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Process Readiness Check List");
                    int count = dt.Rows.Count + 4;

                    worksheet.Range("A1:F" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:F" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:F" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:F" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:F" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 7)));
                    wsReportNameHeaderRange.Style.Font.Bold = true;
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Merge();
                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191); ;
                    wsReportNameHeaderRange.Value = "Process Readiness Check List";

                    worksheet.Cell(2, 1).Style.Font.Bold = true;
                    worksheet.Cell(2, 1).Value = "Group Company Name";
                    worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();

                    worksheet.Cell(2, 3).Style.Font.Bold = true;
                    worksheet.Cell(2, 3).Value = "Payroll Group";
                    worksheet.Cell(2, 4).Value = PayrollName;
                    worksheet.Cell(2, 5).Style.Font.Bold = true;
                    worksheet.Cell(2, 5).Value = "Month";
                    worksheet.Cell(2, 6).Style.DateFormat.Format = "MMM-yyyy";
                    worksheet.Cell(2, 6).Value = SalPeriodName;


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

                    var currentRow = 4;
                    worksheet.Cell(currentRow, 1).InsertTable(dt);
                    worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                    worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                    worksheet.Range("A4:F4").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Range("A4:F4").Style.Font.Bold = true;


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
                            "Process Readiness Check List.xlsx"
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


         public async Task<IActionResult> GetBlankStatDaysPopList(Guid EmployeeGI, int month, string Description)
        {
            try
            {
                ProcessReadinessCheck model = new ProcessReadinessCheck();

                model = await _Service.GetBlankStatDaysPopList(EmployeeGI, month, Description);

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

        public async Task<IActionResult> ExportToExcelAttendancePopup(int TotalRows, int Payroll, int month, string PayrollGroupName, string MonthName, string tabName, string rowName, int tab_index)
        {
            try
            {
                ProcessReadinessCheck model = new ProcessReadinessCheck();


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
                EP = await _commonService.GetExcelPassword(rowName, "Excel");
                int slNo = 0;

                Filter filter = new Filter();
                string currDateTime = await _helpers.GetCurrentDate();
                
                int count = 6;
                string columnName = "";
                int colcount = 0;
                int Columncount = 0;
              model = await _Service.MonthlyAttendanceProcessingListPopUp(month, Payroll, tab_index, rowName, filter);
                using (var workbook = new XLWorkbook())
                {
                 

                    
                       var   worksheet = workbook.Worksheets.Add("Check List-Detailed");
                    if (tabName == "ATTENDANCE_SETTINGS")
                    {
                        if (rowName == "Daily Attendance Processing Pending Days")
                        {
                            count = 2;
                        }

                    }


                    colcount = model.MonthlyAttendanceProcessing.MonthlyAttendanceProcessing_Validations_popup.Count + 4;
                    if (tabName == "ATTENDANCE_SETTINGS")
                    {
                        if (rowName == "Daily Attendance Processing Pending Days")
                        {
                            colcount = model.MonthlyAttendanceProcessing.MonthlyAttendanceProcessing_Validations_popup.Count + 8;

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
                    wsReportNameHeaderRange.Value = rowName;



                    if (tabName == "ATTENDANCE_SETTINGS")
                    {
                        if (rowName == "Daily Attendance Processing Pending Days")
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
                            worksheet.Cell(4, 2).Style.DateFormat.Format = "MMM-yyyy";

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
                            worksheet.Cell(7, 2).Value = model.MonthlyAttendanceProcessing.MonthlyAttendanceProcessing_Validations_popup.Count;
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
                            worksheet.Cell(2, 6).Style.DateFormat.Format = "MMM-yyyy";
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
                            worksheet.Cell(3, 6).Value = model.MonthlyAttendanceProcessing.MonthlyAttendanceProcessing_Validations_popup.Count;
                        }
                    }



                    var currentRow = 4;
                    if (tabName == "ATTENDANCE_SETTINGS")
                    {
                        if (rowName == "Daily Attendance Processing Pending Days")
                        {
                            currentRow = 8;
                        }
                    }



                    #region Header
                    worksheet.Range("A" + currentRow, colName + currentRow).Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);

                    if (tabName == "ATTENDANCE_SETTINGS")
                    {
                        if (rowName == "Daily Attendance Processing Pending Days")
                        {
                            worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                            worksheet.Cell(currentRow, 1).Value = "Sl.No";
                            worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                            worksheet.Cell(currentRow, 2).Value = "Date";
                        }
                        else
                        {
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
                        }
                    }
                  
                  





                    foreach (var item in model.MonthlyAttendanceProcessing.MonthlyAttendanceProcessing_Validations_popup)
                    {
                        currentRow++;
                        slNo = slNo + 1;

                        if (tabName == "ATTENDANCE_SETTINGS")
                        {
                            if (rowName == "Daily Attendance Processing Pending Days")
                            {
                                worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                                worksheet.Cell(currentRow, 1).Value = item.SlNo;
                                worksheet.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                                worksheet.Cell(currentRow, 2).Value = item.PendingDates;
                                worksheet.Cell(currentRow, 2).Style.DateFormat.Format = "dd-MMM-yyyy";

                            }
                            else
                            {
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

                            }
                        }
                        


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
                            book.Protect(EP.Password, false, false);
                            book.SaveToStream(stream);
                        }
                        var content = stream.ToArray();
                        return File(
                            content,
                            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                          "Attendance Readiness Check" + " List.xlsx"
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


        public async Task<IActionResult> ExportToExcelSalaryPopUp(int TotalRows, int Payroll, int month, string PayrollGroupName, string MonthName,string TabName,string RowName)
        {
            try
            {
                ProcessReadinessCheck model = new ProcessReadinessCheck();


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
                EP = await _commonService.GetExcelPassword(RowName, "Excel");
                int slNo = 0;


                string currDateTime = await _helpers.GetCurrentDate();
                
                int count = 6;
                string columnName = "";
                int colcount = 0;
                int Columncount = 0;
             
                model =  await _Service.SalaryProcessReadinessCheckPopUp(month, Payroll, TabName, RowName);
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Check List - Detailed");

                    if (TabName == "HR_TIMEOFFICE")
                    {



                        if (RowName == "Resignation")
                        {
                            count = 8;
                        }
                        if (RowName == "Loss Of Pay" || RowName == "Joining Pending" || RowName == "New Joins" || RowName == "Job Transition Pending")
                        {
                            count = 7;

                        }
                        if (RowName == "Resignation Pending")
                        {
                            count = 9;

                        }

                    }


                    else if (TabName == "STATUTORY")
                    {
                        if (RowName == "Payment Mode Bank But Account Details Missing")
                        {
                            count = 9;
                        }
                        if (RowName == "Account Number Variation With Previous Month" || RowName == "Payroll Group Varience" || RowName == "Bank Account Number Duplication")
                        {
                            count = 8;
                        }

                    }
                    else if (TabName == "PAYROLL_SETTINGS")
                    {
                        if (RowName == "ESI Not Applicable Salary Less Than 21000" || RowName == "ESI Applicable Salary Greater Than 21000")
                        {
                            count = 7;
                        }
                        if (RowName == "PT Varience With Previous Month" ||RowName == "ESI Varience With Previous Month")
                        {
                            count = 10;
                        }

                    }


                    colcount = model.ProcessReadinessCheck_Popup.Count + 4;
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
                    wsReportNameHeaderRange.Value = RowName;

                    worksheet.Cell(2, 1).Style.Font.Bold = true;
                    worksheet.Cell(2, 1).Value = "Group Company Name";
                    worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();


                    worksheet.Cell(2, 3).Style.Font.Bold = true;
                    worksheet.Cell(2, 3).Value = "PayRoll Group";
                    worksheet.Cell(2, 4).Value = PayrollGroupName;
                    worksheet.Cell(2, 5).Style.Font.Bold = true;
                    worksheet.Cell(2, 5).Value = "Month";
                    worksheet.Cell(2, 6).Style.DateFormat.Format = "MMM-yyyy";
                    worksheet.Cell(2, 6).Value = MonthName;
                 

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
                    worksheet.Cell(3, 6).Value = model.ProcessReadinessCheck_Popup.Count;

                    var currentRow = 4;
                    #region Header
                    worksheet.Range("A4", colName + 4).Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
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

                    if (TabName == "HR_TIMEOFFICE")
                    {
                        if (RowName == "New Joins")
                        {
                            worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                            worksheet.Cell(currentRow, 7).Value = "DOJ";
                        }
                        if (RowName == "Resignation")
                        {
                            worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                            worksheet.Cell(currentRow, 7).Value = "Resignation Date";
                            worksheet.Cell(currentRow, 8).Style.Font.Bold = true;
                            worksheet.Cell(currentRow, 8).Value = "LWD";

                        }
                        if (RowName == "Loss Of Pay")
                        {

                            worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                            worksheet.Cell(currentRow, 7).Value = "LOP Count";
                        }
                        if (RowName == "Joining Pending")
                        {

                            worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                            worksheet.Cell(currentRow, 7).Value = "Ex.DOJ";
                        }
                        if (RowName == "Resignation Pending")
                        {
                            worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                            worksheet.Cell(currentRow, 7).Value = "Resignation Date";
                            worksheet.Cell(currentRow, 8).Style.Font.Bold = true;
                            worksheet.Cell(currentRow, 8).Value = "LWD";
                            worksheet.Cell(currentRow, 9).Style.Font.Bold = true;
                            worksheet.Cell(currentRow, 9).Value = "Stage";
                        }
                        if (RowName == "Job Transition Pending")
                        {

                            worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                            worksheet.Cell(currentRow, 7).Value = "Type";
                        }
                    }
                    if (TabName == "STATUTORY")
                    {
                        if (RowName == "Payment Mode Bank But Account Details Missing")
                        {
                            worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                            worksheet.Cell(currentRow, 7).Value = "Bank";
                            worksheet.Cell(currentRow, 8).Style.Font.Bold = true;
                            worksheet.Cell(currentRow, 8).Value = "Account";
                            worksheet.Cell(currentRow, 9).Style.Font.Bold = true;
                            worksheet.Cell(currentRow, 9).Value = "IFSC";
                        }
                        if (RowName == "Account Number Variation With Previous Month")
                        {
                            worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                            worksheet.Cell(currentRow, 7).Value = "Previous";
                            worksheet.Cell(currentRow, 8).Style.Font.Bold = true;
                            worksheet.Cell(currentRow, 8).Value = "Current";
                        }
                        if (RowName == "Payroll Group Varience")
                        {
                            worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                            worksheet.Cell(currentRow, 7).Value = "Previous";
                            worksheet.Cell(currentRow, 8).Style.Font.Bold = true;
                            worksheet.Cell(currentRow, 8).Value = "Current";
                        }
                        if (RowName == "Bank Account Number Duplication")
                        {
                            worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                            worksheet.Cell(currentRow, 7).Value = "Bank";
                            worksheet.Cell(currentRow, 8).Style.Font.Bold = true;
                            worksheet.Cell(currentRow, 8).Value = "Account";
                        }

                    }
                    if (TabName == "PAYROLL_SETTINGS")
                    {
                        if (RowName == "PT Varience With Previous Month")
                        {
                            worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                            worksheet.Cell(currentRow, 7).Value = "Previous PFGroup";
                            worksheet.Cell(currentRow, 8).Style.Font.Bold = true;
                            worksheet.Cell(currentRow, 8).Value = "Current PFGroup";
                            worksheet.Cell(currentRow, 9).Style.Font.Bold = true;
                            worksheet.Cell(currentRow, 9).Value = "Previous Applicability";
                            worksheet.Cell(currentRow, 10).Style.Font.Bold = true;
                            worksheet.Cell(currentRow, 10).Value = "Current Applicability";
                        }
                        if (RowName == "ESI Not Applicable Salary Less Than 21000")
                        {
                            worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                            worksheet.Cell(currentRow, 7).Value = "Gross Salary";
                        }
                        if (RowName == "ESI Applicable Salary Greater Than 21000")
                        {
                            worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                            worksheet.Cell(currentRow, 7).Value = "Gross Salary";

                        }
                        if (RowName == "ESI Varience With Previous Month")
                        {
                            worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                            worksheet.Cell(currentRow, 7).Value = "Previous ESIGroup";
                            worksheet.Cell(currentRow, 8).Style.Font.Bold = true;
                            worksheet.Cell(currentRow, 8).Value = "Current ESIGroup";
                            worksheet.Cell(currentRow, 9).Style.Font.Bold = true;
                            worksheet.Cell(currentRow, 9).Value = "Previous Applicability";
                            worksheet.Cell(currentRow, 10).Style.Font.Bold = true;
                            worksheet.Cell(currentRow, 10).Value = "Current Applicability";
                        }
                    }



                    foreach (var item in model.ProcessReadinessCheck_Popup)
                    {
                        currentRow++;
                        slNo = slNo + 1;


                        worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 1).Value = slNo;
                        worksheet.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 2).Value = item.ID;
                        worksheet.Cell(currentRow, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 3).Value = item.Name;
                        worksheet.Cell(currentRow, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 4).Value = item.Designation;
                        worksheet.Cell(currentRow, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 5).Value = item.Department;
                        worksheet.Cell(currentRow, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 6).Value = item.Location;


                        if (TabName == "HR_TIMEOFFICE")
                        {
                            if (RowName == "New Joins")
                            {
                                worksheet.Cell(currentRow, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                                worksheet.Cell(currentRow, 7).Value = item.DOJ;
                                worksheet.Cell(currentRow, 7).Style.DateFormat.Format = "dd-MMM-yyyy";

                            }
                            if (RowName == "Resignation")
                            {
                                worksheet.Cell(currentRow, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                                worksheet.Cell(currentRow, 7).Value = item.ResignationDate;
                                worksheet.Cell(currentRow, 7).Style.DateFormat.Format = "dd-MMM-yyyy";

                                worksheet.Cell(currentRow, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                                worksheet.Cell(currentRow, 8).Value = item.LWD;
                                worksheet.Cell(currentRow, 8).Style.DateFormat.Format = "dd-MMM-yyyy";


                            }
                            if (RowName == "Loss Of Pay")
                            {

                                worksheet.Cell(currentRow, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                                worksheet.Cell(currentRow, 7).Value = item.LOP;
                            }
                            if (RowName == "Joining Pending")
                            {

                                worksheet.Cell(currentRow, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                                worksheet.Cell(currentRow, 7).Value = item.ExDOJ;
                                worksheet.Cell(currentRow, 7).Style.DateFormat.Format = "dd-MMM-yyyy";

                            }
                            if (RowName == "Resignation Pending")
                            {
                                worksheet.Cell(currentRow, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                                worksheet.Cell(currentRow, 7).Value = item.ResignationDate;
                                worksheet.Cell(currentRow, 7).Style.DateFormat.Format = "dd-MMM-yyyy";

                                worksheet.Cell(currentRow, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                                worksheet.Cell(currentRow, 8).Value = item.LWD;
                                worksheet.Cell(currentRow, 8).Style.DateFormat.Format = "dd-MMM-yyyy";

                                worksheet.Cell(currentRow, 9).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                                worksheet.Cell(currentRow, 9).Value = item.Stage;
                            }
                            if (RowName == "Job Transition Pending")
                            {

                                worksheet.Cell(currentRow, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                                worksheet.Cell(currentRow, 7).Value = item.JobTransitionType;
                            }
                        }

                        if (TabName == "STATUTORY")
                        {
                            if (RowName == "Payment Mode Bank But Account Details Missing")
                            {
                                worksheet.Cell(currentRow, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                                worksheet.Cell(currentRow, 7).Value = item.Bank;
                                worksheet.Cell(currentRow, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                                worksheet.Cell(currentRow, 8).Value = item.AccNo;
                                worksheet.Cell(currentRow, 9).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                                worksheet.Cell(currentRow, 9).Value = item.IFSC;
                            }
                            if (RowName == "Account Number Variation With Previous Month")
                            {
                                worksheet.Cell(currentRow, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                                worksheet.Cell(currentRow, 7).Value = item.PrevAccNo;
                                worksheet.Cell(currentRow, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                                worksheet.Cell(currentRow, 8).Value = item.CurrAccNo;
                            }
                            if (RowName == "Payroll Group Varience")
                            {
                                worksheet.Cell(currentRow, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                                worksheet.Cell(currentRow, 7).Value = item.PrevPayrollGroup;
                                worksheet.Cell(currentRow, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                                worksheet.Cell(currentRow, 8).Value = item.CurrPayrollGroup;
                            }
                            if (RowName == "Bank Account Number Duplication")

                            {
                                worksheet.Cell(currentRow, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                                worksheet.Cell(currentRow, 7).Value = item.Bank;
                                worksheet.Cell(currentRow, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                                worksheet.Cell(currentRow, 8).Value = item.AccNo;
                            }

                        }
                        if (TabName == "PAYROLL_SETTINGS")
                        {
                            if (RowName == "PT Varience With Previous Month")
                            {
                                worksheet.Cell(currentRow, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                                worksheet.Cell(currentRow, 7).Value = item.OldPFGroup;
                                worksheet.Cell(currentRow, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                                worksheet.Cell(currentRow, 8).Value = item.NewPFGroup;
                                worksheet.Cell(currentRow, 9).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                                worksheet.Cell(currentRow, 9).Value = item.PrevPFApplicability;
                                worksheet.Cell(currentRow, 10).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                                worksheet.Cell(currentRow, 10).Value = item.CurrPFApplicability;
                            }
                            if (RowName == "ESI Not Applicable Salary Less Than 21000")
                            {
                                worksheet.Cell(currentRow, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                                worksheet.Cell(currentRow, 7).Value = item.GrossSalary;
                            }
                            if (RowName == "ESI Applicable Salary Greater Than 21000")
                            {
                                worksheet.Cell(currentRow, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                                worksheet.Cell(currentRow, 7).Value = item.GrossSalary;

                            }
                            if (RowName == "ESI Varience With Previous Month")
                            {
                                worksheet.Cell(currentRow, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                                worksheet.Cell(currentRow, 7).Value = item.OldEsiGroup;
                                worksheet.Cell(currentRow, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                                worksheet.Cell(currentRow, 8).Value = item.NewEsiGroup;
                                worksheet.Cell(currentRow, 9).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                                worksheet.Cell(currentRow, 9).Value = item.PrevEsiApplicability;
                                worksheet.Cell(currentRow, 10).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                                worksheet.Cell(currentRow, 10).Value = item.CurrEsiApplicability;
                            }
                        }

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
                            book.Protect(EP.Password, false, false);
                            book.SaveToStream(stream);
                        }
                        var content = stream.ToArray();
                        return File(
                            content,
                            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                          "Salary Readiness Check " + " List.xlsx"
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
