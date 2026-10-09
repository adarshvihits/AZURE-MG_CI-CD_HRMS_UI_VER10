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
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.PayRoll.Reports.Controllers
{
    [Area("PayRoll")]
    [SessionAuthorize]
    public class LoanWFTrackingReportController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly IEmployeeRequestLoanIssueService _service;
        private readonly ILoanIssueService _LoanIssueService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        public LoanWFTrackingReportController(IDropdownService dropdownService, IEmployeeRequestLoanIssueService service, ILoanIssueService LoanIssueService, ICommonService commonService, IHelpers helpers)
        {
            _dropdownService = dropdownService;
            _service = service;
            _commonService = commonService;
            _helpers = helpers;
            _LoanIssueService = LoanIssueService;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                ViewBag.LoanType = new SelectList(await _dropdownService.GetLoanTypeDetailsDropdown(), "Code", "Name");

                EmployeeRequestLoanIssue model = new EmployeeRequestLoanIssue();
                model.Filter = await _commonService.GetFilterDropdowns();
                Filter filter = new Filter();
                filter.length = 10;
                model = await _service.GetLoanRequestWFTrackingReport(0, filter);
                return View("~/Areas/PayRoll/Reports/Views/LoanWFTrackingReport/Index.cshtml", model);
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

        public async Task<IActionResult> LoanIssueTracker(Guid RequestGI, Guid EmployeeGI)
        {
            try
            {

                EmployeeRequestLoanIssue model = new EmployeeRequestLoanIssue();
                model = await _service.LoanIssueTracker(RequestGI);
                model.EmployeeHeader = await _commonService.GetEmployeeHeader(EmployeeGI);
                return View("~/Areas/PayRoll/Reports/Views/LoanWFTrackingReport/_LoanIssueTracker.cshtml", model);
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


        public async Task<IActionResult> GetList_LoanIssue(Filter filter, int LoanTypeCode)
        {
            try
            {

                EmployeeRequestLoanIssue model = new EmployeeRequestLoanIssue();
                model = await _service.GetLoanRequestWFTrackingReport(LoanTypeCode, filter);
                return PartialView("~/Areas/PayRoll/Reports/Views/LoanWFTrackingReport/_LoanIssueList.cshtml", model);
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
        public async Task<IActionResult> ExportToExcelLoanIssue(int LoanTypeCode, string LoanTypeName, Filter filter)
        {
            try
            {
                EmployeeRequestLoanIssue model = new EmployeeRequestLoanIssue();
                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Loan Work Flow Tracking", "Excel");
                int slNo = 0;

                string FilterValues = await _helpers.GetFilterValues(filter);
                model = await _service.GetLoanRequestWFTrackingReport(LoanTypeCode, filter);
                if (FilterValues == "")
                {
                    FilterValues = "All";
                }
                string currDateTime = await _helpers.GetCurrentDate();
                

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Loan Work Flow Tracking");
                    int count = model.LoanIssueList.Count + 5;


                    worksheet.Range("A1:K" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:K" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:K" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:K" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:K" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;

                    var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 10)));
                    wsReportNameHeaderRange.Style.Font.Bold = true;
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Merge();
                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191); ;
                    wsReportNameHeaderRange.Value = "Loan Work Flow Tracking";

                    worksheet.Cell(2, 1).Style.Font.Bold = true;
                    worksheet.Cell(2, 1).Value = "Group Company Name";
                    worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                    worksheet.Cell(2, 3).Style.Font.Bold = true;
                    worksheet.Cell(2, 3).Value = "Login User";
                    worksheet.Cell(2, 4).Value = LoginUser;
                    worksheet.Cell(2, 5).Style.Font.Bold = true;
                    worksheet.Cell(2, 5).Value = "Date & Time";
                    worksheet.Cell(2, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(2, 6).Style.DateFormat.Format = "dd-MMM-yyyy   h:mm AM/PM";
                    worksheet.Cell(2, 6).Value = currDateTime.ToString();
                    worksheet.Cell(2, 7).Style.Font.Bold = true;
                    worksheet.Cell(2, 7).Value = "Total Count";
                    worksheet.Cell(2, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(2, 8).Value = model.LoanIssueList.Count;
                    //worksheet.Cell(3, 1).Style.Font.Bold = true;
                    //worksheet.Cell(3, 1).Value = "Month";
                    //worksheet.Cell(3, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    //worksheet.Cell(3, 2).Style.DateFormat.Format = "MMM-yyyy";
                    //worksheet.Cell(3, 2).Value = MonthName;
                    //worksheet.Cell(3, 3).Style.Font.Bold = true;
                    //worksheet.Cell(3, 3).Value = "Loan Type";
                    //worksheet.Cell(3, 4).Value = LoanTypeName;
                    worksheet.Cell(3, 1).Style.Font.Bold = true;
                    worksheet.Cell(3, 1).Value = "Loan Type";
                    worksheet.Cell(3, 2).Value = LoanTypeName;

                    worksheet.Cell(4, 1).Style.Font.Bold = true;

                    worksheet.Cell(4, 1).Value = "Filter Applied";
                    worksheet.Range("B4:K4").Merge();
                    worksheet.Cell("B4").Value = FilterValues;
                    var currentRow = 5;


                    #region Header
                    worksheet.Cell(currentRow, 1).Value = "Sl.No";
                    worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 2).Value = "Employee ID";
                    worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 3).Value = "EmployeeName";
                    worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 4).Value = "Gross Salary";
                    worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 5).Value = "Requested Date";
                    worksheet.Cell(currentRow, 5).Style.Font.Bold = true;

                    worksheet.Cell(currentRow, 6).Value = "Requested Date";
                    worksheet.Cell(currentRow, 6).Style.Font.Bold = true;

                    worksheet.Cell(currentRow, 7).Value = "Loan Purpose";
                    worksheet.Cell(currentRow, 7).Style.Font.Bold = true;

                    worksheet.Cell(currentRow, 8).Value = "Repayment Method";
                    worksheet.Cell(currentRow, 8).Style.Font.Bold = true;

                    worksheet.Cell(currentRow, 9).Value = "Loan Amount";
                    worksheet.Cell(currentRow, 9).Style.Font.Bold = true;

                    worksheet.Cell(currentRow, 10).Value = "Monthly EMI";
                    worksheet.Cell(currentRow, 10).Style.Font.Bold = true;

                    worksheet.Cell(currentRow, 11).Value = "Tenure";
                    worksheet.Cell(currentRow, 11).Style.Font.Bold = true;


                    #endregion

                    #region Body
                    foreach (var item in model.LoanIssueList)
                    {
                        currentRow++;
                        slNo = slNo + 1;
                        worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Range("A5:K5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                        worksheet.Cell(currentRow, 1).Value = slNo;
                        worksheet.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 2).Value = item.EmployeeID;

                        worksheet.Cell(currentRow, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 3).Value = item.EmployeeName;

                        worksheet.Cell(currentRow, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 4).Value = item.GrossSalary;

                        worksheet.Cell(currentRow, 5).Style.DateFormat.Format = "dd-MMM-yyyy";
                        worksheet.Cell(currentRow, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 5).Value = item.RequestedDate;

                        worksheet.Cell(currentRow, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 6).Value = item.LoanType;

                        worksheet.Cell(currentRow, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 7).Value = item.Purpose;

                        worksheet.Cell(currentRow, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 8).Value = item.RepaymentMethod;

                        worksheet.Cell(currentRow, 9).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);
                        worksheet.Cell(currentRow, 9).Value = item.LoanAmount;

                        worksheet.Cell(currentRow, 10).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);
                        worksheet.Cell(currentRow, 10).Value = item.MonthlyEMI;

                        worksheet.Cell(currentRow, 11).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 11).Value = item.Tenure;


                        worksheet.Columns(1, 10).Style.Alignment.WrapText = true;
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
                            book.Protect(EP.Password, false, false);
                            book.SaveToStream(stream);
                        }
                        var content = stream.ToArray();
                        return File(
                            content,
                            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                            "Loan Work Flow Tracking.xlsx"
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
