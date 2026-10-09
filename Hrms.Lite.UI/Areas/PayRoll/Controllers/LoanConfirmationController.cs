using ClosedXML.Excel;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.PayRoll;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll;
using Hrms.Lite.Shared.PayRoll.Generic.Master;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Spire.Xls;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Shared.Account;
using Newtonsoft.Json;

namespace Hrms.Lite.UI.Areas.PayRoll.Controllers
{
    [Area("PayRoll")]
    [SessionAuthorize]
    public class LoanConfirmationController : Controller
    {

        private readonly IDropdownService _dropdownService;
        private readonly ILoanConfirmationService _LoanConfirmationService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;

        

        public LoanConfirmationController(IDropdownService dropdownService, ILoanConfirmationService LoanConfirmationService, ICommonService commonService, IHelpers Helpers)
        {
            _LoanConfirmationService = LoanConfirmationService;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = Helpers;
        }
        public async Task<IActionResult> Index()
        {
            try { 
            LoanConfirmation model = new LoanConfirmation();
                var currMonth = await _commonService.GetCurrentMonthDetails();
                //model.LoanMonth.Code = currMonth.MonthCode;
                Filter filter = new Filter();
                model.Filter = await _commonService.GetFilterDropdowns();
                model.Filter.length = 10;
                model = await _LoanConfirmationService.GetList(model.Filter, 0, 0, "All");
                model.LoanMonth = new PayrollMonth();
                ViewBag.LoanMonth = new SelectList(await _dropdownService.GetSalaryProcessing_MonthDropdown(0), "Code", "Name");
            ViewBag.LoanType = new SelectList(await _dropdownService.GetLoanTypeDropdown(), "Code", "Name");
                model.Filter = await _commonService.GetFilterDropdowns();
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

        public async Task<IActionResult> GetList(int month, int loanType,string type)
        {
            try { 
            LoanConfirmation model = new LoanConfirmation();
            Filter filter = new Filter();
            model = await _LoanConfirmationService.GetList(filter,month, loanType,type);
            return PartialView("_LoanConfirmationList", model);
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


        public async Task<IActionResult> GetLoanConfirmationDetails(Guid LoanIssueGI, Guid EmployeeGI)
        {
            try { 
            LoanConfirmation model = new LoanConfirmation();
            model = await _LoanConfirmationService.GetLoanConfirmationDetails(LoanIssueGI, EmployeeGI);
            model.EmployeeHeader = await _commonService.GetEmployeeHeader(EmployeeGI);
            model.EmployeeGI = EmployeeGI;
            model.LoanIssueGI = LoanIssueGI;
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

        public async Task<IActionResult> ApplyFilter(Filter filter, int month, int loanType, string type)
        {
            try { 
            LoanConfirmation model = new LoanConfirmation();
            model.Filter = filter;
            model = await _LoanConfirmationService.GetList(filter,month, loanType, type);
            ViewBag.GrpBy = new SelectList(await _dropdownService.GetJoiningAndSeprationgionGrouping(), "Code", "Name");
            return PartialView("_LoanConfirmationList", model);
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

        
        public async Task<IActionResult> LoanConfirmationSave(LoanConfirmation model)
        {
            try { 
            model.LoanMonth = new PayrollMonth();
            model.LoanType = new Shared.Master.LoanSettings();
            model.LoanMonth.Code = model.MonthCode;
            model.LoanType.Code = model.LoanTypeCode;
            var response = await _LoanConfirmationService.LoanConfirmationSave(model);
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
        public async Task<IActionResult> LoanConfirmationEmployeeWiseSave(LoanConfirmation model,int MonthCode,int TypeCode,string Mode)
        {
            try { 
            model.MonthCode = MonthCode;
            model.LoanTypeCode = TypeCode;
            model.MODE = Mode;
            var response = await _LoanConfirmationService.LoanConfirmationEmployeeWiseSave(model);
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
        public async Task<IActionResult> ExportToExcel(int MonthCode, int TypeCode, string Tab,string TabName, string MonName, string loantype)
        {
            try
            {
                LoanConfirmation model = new LoanConfirmation();
                Filter filter = new Filter();
                string FilterValues = await _helpers.GetFilterValues(filter);
                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                if (TabName == "tab2")
                    TabName = "Confirmed";
                else
                    TabName = "Pending";
                EP = await _commonService.GetExcelPassword("Loan Confirmation (" + TabName + ")", "Excel");
                int slNo = 0;
                
                //string FilterValues = GetFilterValues(Global.GlobalFilter);
                model = await _LoanConfirmationService.GetList(filter, MonthCode, TypeCode,Tab);
                if (FilterValues == "")
                {
                    FilterValues = "All";
                }
                string currDateTime = await _helpers.GetCurrentDate();
                
                //if (Tab == "0") //Pending
                //{

                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("Loan Confirmation List");
                        int count = model.LoanConfirmationList.Count + 5;


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
                        wsReportNameHeaderRange.Value = "Loan Confirmation ("+TabName+")";

                        worksheet.Cell(2, 1).Style.Font.Bold = true;
                        worksheet.Cell(2, 1).Value = "Group Company Name";
                        worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();

                    worksheet.Cell(2, 3).Style.Font.Bold = true;
                    worksheet.Cell(2, 3).Value = "Month";
                    worksheet.Cell(2, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(2, 4).Style.DateFormat.Format = "MMM-yyyy";
                    worksheet.Cell(2, 4).Value = MonName;

                    worksheet.Cell(2, 5).Style.Font.Bold = true;
                    worksheet.Cell(2, 5).Value = "Loan Type";
                    worksheet.Cell(2, 6).Value =loantype;


                    worksheet.Cell(3, 1).Style.Font.Bold = true;
                        worksheet.Cell(3, 1).Value = "Login User";
                    worksheet.Cell(3, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(3, 2).Value = LoginUser;
                        worksheet.Cell(3, 3).Style.Font.Bold = true;
                        worksheet.Cell(3, 3).Value = "Date & Time";
                        worksheet.Cell(3, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(3, 4).Style.DateFormat.Format = "dd-MMM-yyyy   h:mm AM/PM";
                        worksheet.Cell(3, 4).Value = currDateTime.ToString();
                        worksheet.Cell(3, 5).Style.Font.Bold = true;
                        worksheet.Cell(3, 5).Value = "Total Count";
                        worksheet.Cell(3, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(3, 6).Value = model.LoanConfirmationList.Count;
                        worksheet.Cell(4, 1).Style.Font.Bold = true;
                        worksheet.Cell(4, 1).Value = "Filter Applied";
                        worksheet.Range("B4:J4").Merge();
                        worksheet.Cell("B4").Value = FilterValues;




                        var currentRow = 5;
                        #region Header
                        worksheet.Range("A5:J5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                        worksheet.Cell(currentRow, 1).Value = "Sl.No";
                        worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 2).Value = "ID";
                        worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 3).Value = "Employee Name";
                        worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 4).Value = "Designation";
                        worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 5).Value = "Department";
                        worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 6).Value = "Location";
                        worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 7).Value = "LoanType";
                        worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 8).Value = "LoanBalance";
                        worksheet.Cell(currentRow, 8).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 9).Value = "MthlyEMI";
                        worksheet.Cell(currentRow, 9).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 10).Value = "ConfAmount";
                        worksheet.Cell(currentRow, 10).Style.Font.Bold = true;

                        #endregion

                        #region Body
                        foreach (var item in model.LoanConfirmationList)
                        {
                            currentRow++;                            
                            slNo = slNo + 1;

                            worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 1).Value = slNo;

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

                            worksheet.Cell(currentRow, 7).Value = item.LoanName;
                            worksheet.Cell(currentRow, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);

                            worksheet.Cell(currentRow, 8).Value = item.LoanBalance;
                            worksheet.Cell(currentRow, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);

                            worksheet.Cell(currentRow, 9).Value = item.MthlyEMI;
                            worksheet.Cell(currentRow, 9).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);

                            worksheet.Cell(currentRow, 10).Value = item.ConfAmount;
                            worksheet.Cell(currentRow, 10).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);

                            worksheet.Columns(1, 10).Style.Alignment.WrapText = true;
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
                                "Loan Confirmation " +TabName+" List.xlsx"
                                );
                        }
                    }

                //}
                //else
                //{

                //    using (var workbook = new XLWorkbook())
                //    {
                //        var worksheet = workbook.Worksheets.Add("Loan Confirmation");
                //        int count = model.LoanConfirmationList.Count + 5;


                //        worksheet.Range("A1:J" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                //        worksheet.Range("A1:J" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                //        worksheet.Range("A1:J" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                //        worksheet.Range("A1:J" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                //        worksheet.Range("A1:J" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;

                //        var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 9)));
                //        wsReportNameHeaderRange.Style.Font.Bold = true;
                //        wsReportNameHeaderRange.Style.Font.FontSize = 15;
                //        wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                //        wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                //        wsReportNameHeaderRange.Merge();
                //        wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191); ;
                //        wsReportNameHeaderRange.Value = TabName;

                //        worksheet.Cell(2, 1).Style.Font.Bold = true;
                //        worksheet.Cell(2, 1).Value = "Company Name";
                //        worksheet.Cell(2, 2).Value = CompanyName.ToUpper();
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
                //        worksheet.Cell(3, 6).Value = model.LoanConfirmationList.Count;
                //        worksheet.Cell(4, 1).Style.Font.Bold = true;
                //        worksheet.Cell(4, 1).Value = "Filter Applied";
                //        worksheet.Range("B4:J4").Merge();
                //        worksheet.Cell("B4").Value = "no";




                //        var currentRow = 5;
                //        #region Header
                //        worksheet.Cell(currentRow, 1).Value = "Sl.No";
                //        worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                //        worksheet.Cell(currentRow, 2).Value = "ID";
                //        worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                //        worksheet.Cell(currentRow, 3).Value = "Employee Name";
                //        worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                //        worksheet.Cell(currentRow, 4).Value = "Designation";
                //        worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                //        worksheet.Cell(currentRow, 5).Value = "Department";
                //        worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                //        worksheet.Cell(currentRow, 6).Value = "Location";
                //        worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
                //        worksheet.Cell(currentRow, 7).Value = "Incident";
                //        worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                //        worksheet.Cell(currentRow, 8).Value = "Incident Type";
                //        worksheet.Cell(currentRow, 8).Style.Font.Bold = true;
                //        worksheet.Cell(currentRow, 9).Value = "Incident Date";
                //        worksheet.Cell(currentRow, 9).Style.Font.Bold = true;
                //        worksheet.Cell(currentRow, 10).Value = "Action Taken";
                //        worksheet.Cell(currentRow, 10).Style.Font.Bold = true;
                //        #endregion

                //        #region Body
                //        foreach (var item in model.LoanConfirmationList)
                //        {
                //            currentRow++;
                //            slNo = slNo + 1;
                //            worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                //            worksheet.Range("A5:J5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                //            worksheet.Cell(currentRow, 1).Value = slNo;
                //            worksheet.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                //            worksheet.Cell(currentRow, 2).Value = item.EmployeeID;
                //            worksheet.Cell(currentRow, 3).Value = item.EmployeeName;
                //            worksheet.Cell(currentRow, 4).Value = item.Designation.Name;
                //            worksheet.Cell(currentRow, 5).Value = item.Department.Name;
                //            worksheet.Cell(currentRow, 6).Value = item.Location.Name;
                //            worksheet.Cell(currentRow, 7).Value = item.MthlyEMI;
                //            worksheet.Cell(currentRow, 8).Value = item.LoanName;
                //            worksheet.Cell(currentRow, 9).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                //            worksheet.Cell(currentRow, 9).Value = item.LoanBalance;
                //            worksheet.Cell(currentRow, 10).Value = item.ConfAmount;
                //            worksheet.Columns(1, 10).Style.Alignment.WrapText = true;
                //        }
                //        #endregion
                //        worksheet.Columns().AdjustToContents();
                //        worksheet.Rows().AdjustToContents();
                //        using (var stream = new MemoryStream())
                //        {
                //            workbook.SaveAs(stream);
                //            if (EP.NeedPassword)
                //            {
                //                Workbook book = new Workbook();
                //                book.LoadFromStream(stream);
                //                //Protect Workbook
                //                book.Protect(EP.Password,false,false);
                //                book.SaveToStream(stream);
                //            }
                //            var content = stream.ToArray();
                //            return File(
                //                content,
                //                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                //                "Loan Confirmation.xlsx"
                //                );
                //        }
                //    }

                //}
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
