using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.PayRoll;
using Hrms.Lite.Shared.Account;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Spire.Xls;

namespace Hrms.Lite.UI.Areas.PayRoll.Controllers
{
    [Area("PayRoll")]
    [SessionAuthorize]
    public class SalaryProcessingController : Controller
    {
        private readonly ISalaryProcessingService _service;
        private readonly ICommonService _commonService;
        private readonly IDropdownService _dropdownService;
        private readonly IHelpers _helpers;
        public SalaryProcessingController(ISalaryProcessingService service, ICommonService commonService, IDropdownService dropdownService, IHelpers helpers)
        {
            _service = service;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = helpers;
        }

        public static class Global
        {
            public static Shared.General.Filter GlobalFilter;
            public static string TabName;
            public static string RowName;
          
        }








        public async Task<IActionResult> Create()
        {
            try
            {
                SalaryProcessing model = new SalaryProcessing();
                model.PayrollGroup = new Shared.Master.PayrollGroup();
                model.PayrollMonth = new Shared.PayRoll.Generic.Master.PayrollMonth();
                ViewBag.month = new SelectList(await _dropdownService.GetSalaryProcessingAttenLockMonthDropdown(0), "Code", "Name");
                ViewBag.PayrollGroup = new SelectList(await _dropdownService.GetPayrollGroupDropdown(), "Code", "Name");
                model = await _service.SalaryProcessingValidations(0, 0);

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


        public async Task<IActionResult> GetSalaryProcessingValidations(int SalaryPeriodCode, int PayrollGroup)
        {
            try
              { 
                SalaryProcessing model = new SalaryProcessing();
                ViewBag.month = new SelectList(await _dropdownService.GetSalaryProcessingAttenLockMonthDropdown(0), "Code", "Name");
                ViewBag.PayrollGroup = new SelectList(await _dropdownService.GetPayrollGroupWithGrpCmpyCheckDropdown(), "Code", "Name");
                model = await _service.SalaryProcessingValidations( SalaryPeriodCode,  PayrollGroup);

               

                return PartialView("_CommonDetails",model);
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
//From payroll  Transaction Next click
        public async Task<IActionResult> MonthlySalaryProcessingValidation(int SalaryPeriodCode, int PayrollGroup)
        {
            try
            {
                SalaryProcessing model = new SalaryProcessing();
                model.PayrollGroup = new Shared.Master.PayrollGroup();
                model.PayrollMonth = new Shared.PayRoll.Generic.Master.PayrollMonth();

                model.PayrollGroup.Code = PayrollGroup;
                model.PayrollMonth.Code = SalaryPeriodCode;
                var response = await _service.MonthlySalaryProcessingValidation(model);
                if (response.Success == false &&response.Data != null &&response.Data.SalaryProcessing_popup!=null)
                {
                    model = response.Data;
                    ViewBag.Heading = response.Message;
                    return PartialView("_ValidationPopup", model);
                }
                else

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

        
        public async Task<IActionResult> GetMonth(int Payrollgrp)
        {
            try
            {

                var Month = await _dropdownService.GetSalaryProcessingAttenLockMonthDropdown(Payrollgrp);
                return Json(Month);
                
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
        public async Task<IActionResult> SalaryProcessing(SalaryProcessing model)
        {
            try
            {
                var response = await _service.SalaryProcessing(model);
                if (response.Data != null && response.Data.SalaryProcessing_popup != null && response.Data.SalaryProcessing_popup.Count > 0)
                {
                    model = response.Data;                    
                    return PartialView("_NegativeSalary", model);
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


        [HttpGet]
        public async Task<ActionResult> GetEmployeeDetails(int Month, int PayrollGroup, int Code, string RowName,string TabName)
        {
            try
            {
                SalaryProcessing model = new SalaryProcessing();
                ViewBag.TabName = TabName;
                ViewBag.RowName = RowName;
                Global.TabName=TabName;
                Global.RowName=RowName;

                model = await _service.SalaryProcessingValidationsPopUp(Month, PayrollGroup, TabName, RowName);
                if(TabName== "HR_TIMEOFFICE")
                 return PartialView("_HrTimeOfficePopUp", model);
                if(TabName== "STATUTORY")
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

        public async Task<IActionResult> ExportToExcel(int TotalRows, int Payroll, int month, string PayrollGroupName, string MonthName)
        {
            try
            {
                SalaryProcessing model = new SalaryProcessing();


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
                EP = await _commonService.GetExcelPassword("Monthly Salary Processing Popup", "Excel");
                int slNo = 0;


                string currDateTime = await _helpers.GetCurrentDate();
                
                int count=6;
                string columnName = ""; 
                int colcount=0;
                int Columncount=0;
                model = await _service.SalaryProcessingValidationsPopUp(month, Payroll, Global.TabName, Global.RowName);
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Monthly Salary Processing");
                   
                  if (Global.TabName== "HR_TIMEOFFICE")
                    {
                      


                        if (Global.RowName == "Resignation")
                        {
                            count=8;
                        }
                        if (Global.RowName == "Loss Of Pay" || Global.RowName== "Joining Pending"|| Global.RowName== "New Joins" || Global.RowName == "Job Transition Pending")
                        {
                            count=7;
                           
                        }
                        if(Global.RowName=="Resignation Pending")
                        {
                            count=9;
                            
                        }
                      
                    }


                   else if (Global.TabName == "STATUTORY")
                    {
                        if (Global.RowName == "Payment Mode Bank But Account Details Missing")
                        {
                            count = 9;
                        }
                        if (Global.RowName == "Account Number Variation With Previous Month" || Global.RowName == "Payroll Group Varience"|| Global.RowName == "Bank Account Number Duplication")
                        {
                            count = 8;
                        }
                      
                    }
                   else if (Global.TabName == "PAYROLL_SETTINGS")
                    {
                        if (Global.RowName == "ESI Not Applicable Salary Less Than 21000"|| Global.RowName== "ESI Applicable Salary Greater Than 21000")
                        {
                            count = 7;
                        }
                        if (Global.RowName == "PT Varience With Previous Month" || Global.RowName == "ESI Varience With Previous Month")
                        {
                            count = 10;
                        }
                        
                    }
                 

                    colcount = model.SalaryProcessing_popup.Count + 4;
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
                    wsReportNameHeaderRange.Value = "Monthly Salary Processing " + Global.TabName;

                    worksheet.Cell(2, 1).Style.Font.Bold = true;
                    worksheet.Cell(2, 1).Value = "Group Company Name";
                    worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();


                    worksheet.Cell(2, 3).Style.Font.Bold = true;
                    worksheet.Cell(2, 3).Value = "PayRoll Group";
                    worksheet.Cell(2, 4).Value = PayrollGroupName;
                    worksheet.Cell(2, 5).Style.Font.Bold = true;
                    worksheet.Cell(2, 5).Value = "Month";
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
                    worksheet.Cell(3, 6).Value = model.SalaryProcessing_popup.Count;

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

                    if (Global.TabName == "HR_TIMEOFFICE")
                    {
                        if (Global.RowName == "New Joins")
                        {
                            worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                            worksheet.Cell(currentRow, 7).Value = "DOJ";
                        }
                        if (Global.RowName == "Resignation")
                        {
                            worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                            worksheet.Cell(currentRow, 7).Value = "Resignation Date";
                            worksheet.Cell(currentRow, 8).Style.Font.Bold = true;
                            worksheet.Cell(currentRow, 8).Value = "LWD";

                        }
                        if (Global.RowName == "Loss Of Pay")
                        {

                            worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                            worksheet.Cell(currentRow, 7).Value = "LOP Count";
                        }
                        if (Global.RowName == "Joining Pending")
                        {

                            worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                            worksheet.Cell(currentRow, 7).Value = "Ex.DOJ";
                        }
                        if (Global.RowName == "Resignation Pending")
                        {
                            worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                            worksheet.Cell(currentRow, 7).Value = "Resignation Date";
                            worksheet.Cell(currentRow, 8).Style.Font.Bold = true;
                            worksheet.Cell(currentRow, 8).Value = "LWD";
                            worksheet.Cell(currentRow, 9).Style.Font.Bold = true;
                            worksheet.Cell(currentRow, 9).Value = "Stage";
                        }
                        if (Global.RowName == "Job Transition Pending")
                        {

                            worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                            worksheet.Cell(currentRow, 7).Value = "Type";
                        }
                    }
                    if (Global.TabName == "STATUTORY")
                    {
                        if (Global.RowName == "Payment Mode Bank But Account Details Missing")
                        {
                            worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                            worksheet.Cell(currentRow, 7).Value = "Bank";
                            worksheet.Cell(currentRow, 8).Style.Font.Bold = true;
                            worksheet.Cell(currentRow, 8).Value = "Account";
                            worksheet.Cell(currentRow, 9).Style.Font.Bold = true;
                            worksheet.Cell(currentRow, 9).Value = "IFSC";
                        }
                        if (Global.RowName == "Account Number Variation With Previous Month")
                        {
                            worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                            worksheet.Cell(currentRow, 7).Value = "Previous";
                            worksheet.Cell(currentRow, 8).Style.Font.Bold = true;
                            worksheet.Cell(currentRow, 8).Value = "Current";
                        }
                        if (Global.RowName == "Payroll Group Varience")
                        {
                            worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                            worksheet.Cell(currentRow, 7).Value = "Previous";
                            worksheet.Cell(currentRow, 8).Style.Font.Bold = true;
                            worksheet.Cell(currentRow, 8).Value = "Current";
                        }
 if(Global.RowName == "Bank Account Number Duplication")
                        {
                            worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                            worksheet.Cell(currentRow, 7).Value = "Bank";
                            worksheet.Cell(currentRow, 8).Style.Font.Bold = true;
                            worksheet.Cell(currentRow, 8).Value = "Account";
                        }

                    }
                    if (Global.TabName == "PAYROLL_SETTINGS")
                    {
                        if (Global.RowName == "PT Varience With Previous Month")
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
                        if (Global.RowName == "ESI Not Applicable Salary Less Than 21000")
                        {
                            worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                            worksheet.Cell(currentRow, 7).Value = "Gross Salary";
                        }
                        if (Global.RowName == "ESI Applicable Salary Greater Than 21000")
                        {
                            worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                            worksheet.Cell(currentRow, 7).Value = "Gross Salary";

                        }
                        if (Global.RowName == "ESI Varience With Previous Month")
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



                    foreach (var item in model.SalaryProcessing_popup)
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


                        if (Global.TabName == "HR_TIMEOFFICE")
                        {
                            if (Global.RowName == "New Joins")
                            {
                                worksheet.Cell(currentRow, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                                worksheet.Cell(currentRow, 7).Value = item.DOJ;
                                worksheet.Cell(currentRow, 7).Style.DateFormat.Format = "dd-MMM-yyyy";

                            }
                            if (Global.RowName == "Resignation")
                            {
                                worksheet.Cell(currentRow, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                                worksheet.Cell(currentRow, 7).Value = item.ResignationDate;
                                worksheet.Cell(currentRow, 7).Style.DateFormat.Format = "dd-MMM-yyyy";

                                worksheet.Cell(currentRow, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                                worksheet.Cell(currentRow, 8).Value = item.LWD;
                                worksheet.Cell(currentRow, 8).Style.DateFormat.Format = "dd-MMM-yyyy";


                            }
                            if (Global.RowName == "Loss Of Pay")
                            {

                                worksheet.Cell(currentRow, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                                worksheet.Cell(currentRow, 7).Value = item.LOP;
                            }
                            if (Global.RowName == "Joining Pending")
                            {

                                worksheet.Cell(currentRow, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                                worksheet.Cell(currentRow, 7).Value = item.ExDOJ;
                                worksheet.Cell(currentRow, 7).Style.DateFormat.Format = "dd-MMM-yyyy";

                            }
                            if (Global.RowName == "Resignation Pending")
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
                            if (Global.RowName == "Job Transition Pending")
                            {

                                worksheet.Cell(currentRow, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                                worksheet.Cell(currentRow, 7).Value = item.JobTransitionType;
                            }
                        }

                        if (Global.TabName == "STATUTORY")
                        {
                            if (Global.RowName == "Payment Mode Bank But Account Details Missing")
                            {
                                worksheet.Cell(currentRow, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                                worksheet.Cell(currentRow, 7).Value = item.Bank;
                                worksheet.Cell(currentRow, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                                worksheet.Cell(currentRow, 8).Value = item.AccNo;
                                worksheet.Cell(currentRow, 9).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                                worksheet.Cell(currentRow, 9).Value = item.IFSC;
                            }
                            if (Global.RowName == "Account Number Variation With Previous Month")
                            {
                                worksheet.Cell(currentRow, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                                worksheet.Cell(currentRow, 7).Value = item.PrevAccNo;
                                worksheet.Cell(currentRow, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                                worksheet.Cell(currentRow, 8).Value = item.CurrAccNo;
                            }
                            if (Global.RowName == "Payroll Group Varience")
                            {
                                worksheet.Cell(currentRow, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                                worksheet.Cell(currentRow, 7).Value = item.PrevPayrollGroup;
                                worksheet.Cell(currentRow, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                                worksheet.Cell(currentRow, 8).Value = item.CurrPayrollGroup;
                            }
                            if (Global.RowName == "Bank Account Number Duplication")

                            {
                                worksheet.Cell(currentRow, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                                worksheet.Cell(currentRow, 7).Value = item.Bank;
                                worksheet.Cell(currentRow, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                                worksheet.Cell(currentRow, 8).Value = item.AccNo;
                            }

                        }
                        if (Global.TabName == "PAYROLL_SETTINGS")
                        {
                            if (Global.RowName == "PT Varience With Previous Month")
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
                            if (Global.RowName == "ESI Not Applicable Salary Less Than 21000")
                            {
                                worksheet.Cell(currentRow, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                                worksheet.Cell(currentRow, 7).Value = item.GrossSalary;
                            }
                            if (Global.RowName == "ESI Applicable Salary Greater Than 21000")
                            {
                                worksheet.Cell(currentRow, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                                worksheet.Cell(currentRow, 7).Value = item.GrossSalary;

                            }
                            if (Global.RowName == "ESI Varience With Previous Month")
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
                            book.Protect(EP.Password,false,false);
                            book.SaveToStream(stream);
                        }
                        var content = stream.ToArray();
                        return File(
                            content,
                            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                          "Monthly salary processing " + " List.xlsx"
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
