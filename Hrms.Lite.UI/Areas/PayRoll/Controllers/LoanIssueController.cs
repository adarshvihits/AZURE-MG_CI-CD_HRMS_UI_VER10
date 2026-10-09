using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Services.IServices.PayRoll;

using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.PayRoll;

using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Globalization;
using Microsoft.AspNetCore.Http;
using Spire.Xls;
using Newtonsoft.Json;
using Hrms.Lite.Shared.Account;
using Microsoft.AspNetCore.Hosting;
using ExcelDataReader;

namespace Hrms.Lite.UI.Areas.PayRoll.Controllers
{
    [Area("PayRoll")]
    [SessionAuthorize]
    public class LoanIssueController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly ILoanIssueService _loanIssueService;
        private readonly ICommonService _commonService;
        private readonly IBlob _blob;
        private readonly IHelpers _helpers;
        private readonly IWebHostEnvironment _environment;
        private readonly IEmployeeRequestLoanIssueService _service;


        public LoanIssueController(ILoanIssueService LoanIssueService, IEmployeeRequestLoanIssueService service, IDropdownService dropdownService, ICommonService commonService, IBlob blob, IHelpers helpers, IWebHostEnvironment environment)
        {
            _loanIssueService = LoanIssueService;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _blob = blob;
            _helpers = helpers;
            _environment = environment;
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                LoanIssue model = new LoanIssue();
                LoanIssue m = new LoanIssue();
                Filter Filter = new Filter();
                m.LoanIssueListTabs = (List<LoanIssueListTabs>)await _loanIssueService.GetLoanIssueListTabs();
                model = await _loanIssueService.GetLoanIssueList(0, null, Filter);
                model.LoanIssueListTabs = m.LoanIssueListTabs;

                model.EmployeeHeader = new EmployeeHeader();
                model.EmployeeHeader.Department = new Department();
                model.EmployeeHeader.Designation = new Designation();
                model.EmployeeHeader.Location = new Location();
                model.EmployeeHeader.Category = new Category();

                ViewBag.LoanType = new SelectList(await _dropdownService.GetLoanTypeDropdown(), "Code", "Name");
                ViewBag.RepaymentStartMonth = new SelectList(await _dropdownService.GetRepaymentMonthDropdown(null), "Code", "Name");
                ViewBag.RepaymentMethod = new SelectList(await _dropdownService.GetRepaymentMethodhDropdown(0), "Code", "Name");

                ViewBag.Emp = new SelectList(await _dropdownService.GetEmployeeDropDown(), "Code", "Name");
                ViewBag.LoanSettings = new SelectList(await _dropdownService.GetLoanSettingsDropDown(), "Code", "Name");
                // ViewBag.Employee = new SelectList(await _dropdownService.GetEmployeeDropDown(), "Code", "Name");
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

        public async Task<IActionResult> ApplyFilter(int TabIndex, int? LoanSettingsCode, Filter filter)
        {
            try
            {
                LoanIssue model = new LoanIssue();
                model.Filter = filter;
                model = await _loanIssueService.GetLoanIssueList(TabIndex, LoanSettingsCode, filter);
                if (TabIndex == 0)
                {
                    return PartialView("_AllList", model);
                }
                else
                {
                    return PartialView("_ApprovalPendingList", model);
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



        public async Task<IActionResult> GetLoanIssueList(int TabIndex, int? LoanSettingsCode)
        {
            try
            {
                LoanIssue model = new LoanIssue();
                Filter filter = new Filter();
                if (TabIndex == 0)
                {
                    model = await _loanIssueService.GetLoanIssueList(TabIndex, LoanSettingsCode, filter);

                    return PartialView("_AllList", model);
                }
                else if (TabIndex == 2)
                {
                    return PartialView("bulkupload", model);
                }
                //else if (TabIndex == 1)
                //{
                //    return PartialView("_PersonalLoanList", model);
                //}
                //else if (TabIndex == 2)
                //{
                //    return PartialView("_CarLoanList", model);
                //}
                //else if (TabIndex == 3)
                //{
                //    return PartialView("_HomeLoanList", model);
                //}
                //else if (TabIndex == 1000)
                //{
                //    return PartialView("_MoreList", model);
                //}
                //else if (TabIndex == 1000)
                //{
                //    return PartialView("_MoreList", model);
                //}
                else
                {
                    model = await _loanIssueService.GetLoanIssueList(TabIndex, LoanSettingsCode, filter);

                    return PartialView("_ApprovalPendingList", model);
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
        public async Task<ActionResult> EmployeeCard(int EmpCode)
        {
            try
            {
                LoanIssue model = new LoanIssue();

                //model = await _assetAllocationService.GetEmployeeCard(EmpCode);
                model.EmployeeHeader = await _commonService.GetEmployeeCard(EmpCode);

                return PartialView("_EmployeeCard", model.EmployeeHeader);
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

        public async Task<IActionResult> GetLoanIssueDetails(Guid EmployeeGI, Guid LoanIssueGI)
        {
            try
            {
                LoanIssue model = new LoanIssue();

                model = await _loanIssueService.GetLoanIssueDetails(LoanIssueGI);
                model.LoanType = model.LoanIssueDetails.LoanType.Name;
                model.LoanAmount = model.LoanIssueDetails.LoanAmount;

                ViewBag.Remarks = model.LoanIssueDetails.Remarks;
                ViewBag.Pourpose = model.LoanIssueDetails.Pourpose;
                ViewBag.RepaymentMethod = model.LoanIssueDetails.RepaymentMethod.Name;
                ViewBag.MonthlyEMI = model.LoanIssueDetails.MonthlyEMI;
                ViewBag.Tenure = model.LoanIssueDetails.Tenure;
                ViewBag.RepaymentStartMonth = model.LoanIssueDetails.PaymentStartMonth.Name;
                //ViewBag.DisbursementDate = model.LoanIssueDetails.DisbursementDate;
                ViewBag.DisbursementDate = model.LoanIssueDetails.DisbursementDate.ToString("dd-MMM-yyyy", CultureInfo.InvariantCulture);

                model.EmployeeHeader = await _commonService.GetEmployeeHeader(EmployeeGI);
                if (model.EmployeeHeader == null)
                {
                    model.EmployeeHeader = new Shared.General.EmployeeHeader();
                    model.EmployeeHeader.Department = new Shared.Master.Department();
                    model.EmployeeHeader.Designation = new Shared.Master.Designation();
                    model.EmployeeHeader.Location = new Shared.Master.Location();
                    model.EmployeeHeader.Category = new Shared.Master.Category();
                    //model.EmployeeHeader.Department = new Shared.Master.Department();
                }
                if (model.Tabindex > 4)
                    return PartialView("_LoanIssueApproveReject", model);
                else
                    return PartialView("_EditLoanIssue", model);
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
        public async Task<IActionResult> ApproveDetails(Guid RequestGI, Guid EmployeeGI)
        {
            try
            {
                LoanIssue model = new LoanIssue();

                model.EmployeeHeader = await _commonService.GetEmployeeHeader(EmployeeGI);
                model.LoanIssueRequest = await _loanIssueService.GetEmployeeLoanIssueDetails(RequestGI);
                model.LoanIssueLogList = (List<LoanIssueLog>)await _loanIssueService.GetLoanIssueLogList(EmployeeGI);
                ViewBag.LoanType = new SelectList(await _dropdownService.GetLoanTypeDropdown(), "Code", "Name");
                ViewBag.RepaymentStartMonth = new SelectList(await _dropdownService.GetRepaymentMonthDropdown(null), "Code", "Name");
                ViewBag.RepaymentMethod = new SelectList(await _dropdownService.GetRepaymentMethodhDropdown(0), "Code", "Name");
                model.LoanIssueRequest.DisbursementDate = null;
                return View("~/Areas/PayRoll/Views/LoanIssue/Approve.cshtml", model);

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
        public async Task<IActionResult> NewRequest()
        {
            ViewBag.Emp = new SelectList(await _dropdownService.GetEmployeeDropDown(), "Code", "Name");
            return PartialView("_AddNew");
        }

        public async Task<IActionResult> Create(Guid EmployeeGI)
        {
            try
            {
                LoanIssue model = new LoanIssue();

                model.EmployeeHeader = await _commonService.GetEmployeeHeader(EmployeeGI);
                model.LoanIssueLogList = (List<LoanIssueLog>)await _loanIssueService.GetLoanIssueLogList(EmployeeGI);

                ViewBag.LoanType = new SelectList(await _dropdownService.GetLoanTypeDropdown(), "Code", "Name");
                ViewBag.RepaymentStartMonth = new SelectList(await _dropdownService.GetRepaymentMonthDropdown(null), "Code", "Name");
                ViewBag.RepaymentMethod = new SelectList(await _dropdownService.GetRepaymentMethodhDropdown(0), "Code", "Name");
                return View("~/Areas/PayRoll/Views/LoanIssue/Create.cshtml", model);

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
        public async Task<JsonResult> GetRepaymentMethod(int LoanType)
        {
            var data = await _loanIssueService.GetRepaymentMethod(LoanType);
            return Json(data);
        }
        public async Task<IActionResult> LoanIssueLogDetails(Guid LoanIssueGI)
        {
            try
            {
                LoanIssue model = new LoanIssue();
                model = await _loanIssueService.GetLoanIssueDetails(LoanIssueGI);

                return PartialView("_LoanIssueLogDetails", model);
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
        public async Task<IActionResult> Create(LoanIssue model)
        {

            try
            {
                var response = await _loanIssueService.LoanIssueSave(model);
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
        public async Task<IActionResult> SaveLoanIssueData(EmployeeRequestLoanIssue model)
        {
            try
            {

                var response = await _service.SaveLoanIssueData(model);
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

        public async Task<IActionResult> LoanIssueApproveRejectDetails(Guid LoanIssueGI, Guid EmployeeGI)
        {
            try
            {
                LoanIssue model = new LoanIssue();
                model = await _loanIssueService.GetLoanIssueDetails(LoanIssueGI);
                model.EmployeeHeader = await _commonService.GetEmployeeHeader(EmployeeGI);
                return PartialView("_LoanIssueApproveReject", model);
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
        public async Task<JsonResult> GetRepaymentStartMonth(DateTime dte)
        {
            var month = new SelectList(await _dropdownService.GetRepaymentMonthDropdown(dte.ToString("yyyy-MM-dd HH:mm:ss")), "Code", "Name");
            return Json(month);
        }
        [HttpGet]
        public async Task<JsonResult> RepaymentMethod(int loanType)
        {
            var RepayMethod = new SelectList(await _dropdownService.GetRepaymentMethodhDropdown(loanType), "Code", "Name");
            return Json(RepayMethod);
        }
        [HttpGet]
        public async Task<JsonResult> GetLoanAmountDetails(int loanType)
        {
            var LoanAmtDetails = await _loanIssueService.GetLoanAmountDetails(loanType);
            return Json(LoanAmtDetails);
        }

        [HttpPost]
        public async Task<IActionResult> ApproveOrReject(Guid LoanIssueGI, string Mode)
        {
            try
            {
                LoanIssue model = new LoanIssue();
                model.LoanIssueGI = LoanIssueGI;
                model.Mode = Mode;
                var response = await _loanIssueService.Approve(model);


                return Json(response);

                //if (response.Success == true)
                //{
                //    TempData["Message"] = "Saved Successfully";
                //  //  return RedirectToAction("Index", "LoanIssue");
                //    return RedirectToAction("Index");
                //}
                //else
                //{
                //    TempData["Message"] = response.Message;
                //    return PartialView("~/Areas/PayRoll/Views/LoanIssue/_LoanIssueApproveReject.cshtml", model);
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

        public async Task<IActionResult> ExportToExcel(int TabIndex, string TabName, int? LoanSettingsCode, Filter filter)
        {
            try
            {
                LoanIssue model = new LoanIssue();

                string FilterValues = await _helpers.GetFilterValues(filter);
                model = await _loanIssueService.GetLoanIssueList(TabIndex, LoanSettingsCode, filter);
                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword(TabName + " List", "Excel");
                int slNo = 0;
                if (FilterValues == "")
                {
                    FilterValues = "All";
                }
                string currDateTime = await _helpers.GetCurrentDate();
                

                if (TabIndex == 0)
                {
                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add(TabName + " List");
                        int count = model.LoanIssuesList.Count + 5;


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

                        wsReportNameHeaderRange.Value = "Loan Processing";
                        worksheet.Cell(2, 1).Style.Font.Bold = true;
                        worksheet.Cell(2, 1).Value = "Group Company Name";
                        worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();

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
                        worksheet.Cell(3, 6).Value = model.LoanIssuesList.Count;
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
                        worksheet.Cell(currentRow, 3).Value = "EmployeeName";
                        worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 4).Value = "Fun.Designation";
                        worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 5).Value = "Department";
                        worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 6).Value = "Location";
                        worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 7).Value = "Loan Type";
                        worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 8).Value = "Loan Amount";
                        worksheet.Cell(currentRow, 8).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 9).Value = "Total Tenure";
                        worksheet.Cell(currentRow, 9).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 10).Value = "Employee Status";
                        worksheet.Cell(currentRow, 10).Style.Font.Bold = true;



                        #endregion

                        #region Body
                        foreach (var item in model.LoanIssuesList)
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
                            worksheet.Cell(currentRow, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 7).Value = item.LoanType;
                            worksheet.Cell(currentRow, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 8).Value = item.LoanAmount;
                            worksheet.Cell(currentRow, 9).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 9).Value = item.TotalTenure;
                            worksheet.Cell(currentRow, 10).Value = item.EmployeeStatus;

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
                                "Loan Processing" + TabName + " List.xlsx"
                                );
                        }
                    }
                }
                //else if (TabIndex == 1 || TabIndex == 2 || TabIndex == 3)
                //{
                //    using (var workbook = new XLWorkbook())
                //    {
                //        var worksheet = workbook.Worksheets.Add(TabName + " List");
                //        var currentRow = 1;
                //        #region Header
                //        worksheet.Cell(currentRow, 1).Value = "ID";
                //        worksheet.Cell(currentRow, 2).Value = "EmployeeName";
                //        worksheet.Cell(currentRow, 3).Value = "Designation";
                //        worksheet.Cell(currentRow, 4).Value = "Department";
                //        worksheet.Cell(currentRow, 5).Value = "Location";
                //        worksheet.Cell(currentRow, 6).Value = "DOJ";
                //        worksheet.Cell(currentRow, 7).Value = "Y/S";
                //        worksheet.Cell(currentRow, 8).Value = "LWD";
                //        worksheet.Cell(currentRow, 9).Value = "Reason";


                //        #endregion

                //        #region Body
                //        foreach (var item in model.ResignationList)
                //        {
                //            currentRow++;
                //            worksheet.Cell(currentRow, 1).Value = item.EmployeeID;
                //            worksheet.Cell(currentRow, 2).Value = item.EmployeeName;
                //            worksheet.Cell(currentRow, 3).Value = item.Designation.Name;
                //            worksheet.Cell(currentRow, 4).Value = item.Department.Name;
                //            worksheet.Cell(currentRow, 5).Value = item.Location.Name;
                //            worksheet.Cell(currentRow, 6).Value = item.DOJ;
                //            worksheet.Cell(currentRow, 7).Value = item.YOS;
                //            worksheet.Cell(currentRow, 8).Value = item.LWD;
                //            worksheet.Cell(currentRow, 9).Value = item.Reason;

                //        }
                //        #endregion

                //        using (var stream = new MemoryStream())
                //        {
                //            workbook.SaveAs(stream);
                //            var content = stream.ToArray();
                //            return File(
                //                content,
                //                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                //                TabName + " List.xlsx"
                //                );
                //        }
                //    }
                //}
                else
                {
                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add(TabName + " List");
                        int count = model.LoanIssuesList.Count + 5;


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
                        wsReportNameHeaderRange.Value = "Loan Processing";
                        worksheet.Cell(2, 1).Style.Font.Bold = true;
                        worksheet.Cell(2, 1).Value = "Group Company Name";
                        worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();

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
                        worksheet.Cell(3, 6).Value = model.LoanIssuesList.Count;
                        worksheet.Cell(4, 1).Style.Font.Bold = true;
                        worksheet.Cell(4, 1).Value = "Filter Applied";
                        worksheet.Range("B4:J4").Merge();
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
                        worksheet.Cell(currentRow, 7).Value = "Loan Type";
                        worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 8).Value = "Loan Amount";
                        worksheet.Cell(currentRow, 8).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 9).Value = "Loan Type";
                        worksheet.Cell(currentRow, 9).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 10).Value = "Loan Amount";
                        worksheet.Cell(currentRow, 10).Style.Font.Bold = true;

                        #endregion

                        #region Body
                        foreach (var item in model.LoanIssuesList)
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
                            worksheet.Cell(currentRow, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 7).Value = item.LoanType;
                            worksheet.Cell(currentRow, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 8).Value = item.LoanAmount;
                            worksheet.Cell(currentRow, 9).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 9).Value = item.TotalTenure;
                            worksheet.Cell(currentRow, 10).Value = item.EmployeeStatus;
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
                                book.Protect(EP.Password, false, false);
                                book.SaveToStream(stream);
                            }
                            var content = stream.ToArray();
                            return File(
                                content,
                                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                                "Loan Processing" + TabName + " List.xlsx"
                                );
                        }
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




        public async Task<IActionResult> ImportFromExcel(LoanIssue model)
        {
            string path = Path.Combine(this._environment.WebRootPath, "Uploads");
            string fileName = model.Excel.FileData.FileName;
            try
            {
                model.LoanIssuesList = new List<LoanIssue>();
                string wwwPath = this._environment.WebRootPath;
                string contentPath = this._environment.ContentRootPath;

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                {
                    model.Excel.FileData.CopyTo(stream);
                }
                try
                {
                    using (var stream = System.IO.File.Open(path + "\\" + fileName, FileMode.Open, FileAccess.Read))
                    {
                        using (var reader = ExcelReaderFactory.CreateReader(stream))
                        {
                            List<LoanIssue> List = new List<LoanIssue>();
                            var readerrr = ExcelReaderFactory.CreateReader(stream);
                            while (reader.Read()) //Each row of the file
                            {
                                var item = new LoanIssue();
                                if (reader.GetValue(0) != null)
                                {
                                    item.EmployeeID = reader.GetValue(0).ToString();
                                }
                                if (reader.GetValue(1) != null)
                                {
                                    item.Name = reader.GetValue(1).ToString();
                                }
                                if (reader.GetValue(2) != null)
                                {
                                    item.Amount = reader.GetValue(2).ToString();
                                }
                                if (reader.GetValue(3) != null)
                                {
                                    item.EMI = reader.GetValue(3).ToString();
                                }
                                List.Add(item);
                            }
                            if (List[0].EmployeeID != "Employee ID" || List[0].Name != "Employee Name" || List[0].Amount != "Amount" || List[0].EMI != "EMI")
                            {
                                // Check if file exists with its full path    
                                if (System.IO.File.Exists(path + "\\" + fileName))
                                {
                                    System.IO.File.Delete(path + "\\" + fileName);
                                }
                                return Json(new ResponseEntity("Excel File not in the prescribed format", false));
                            }
                            model.LoanIssuesList = List;
                            model.LoanIssuesList.RemoveAt(0);
                        }
                    }
                }
                catch (Exception e)
                {
                    // Check if file exists with its full path    
                    if (System.IO.File.Exists(path + "\\" + fileName))
                    {
                        System.IO.File.Delete(path + "\\" + fileName);
                    }
                    return Json(new ResponseEntity("Excel File not in the prescribed format", false));
                }

                model.Excel.FileData = null;
                var response = await _loanIssueService.ImportFromExcel(model);
                if (System.IO.File.Exists(path + "\\" + fileName))
                {
                    System.IO.File.Delete(path + "\\" + fileName);
                }

                if (response.Success != true)
                {
                    model.LoanIssuesList = response.Data;
                    model.Message = response.Message;
                    return PartialView("_UnMatchedList", model);
                }
                else
                {
                    //response.Data = model.LoanIssuesList;
                    var res = new ResponseEntity<LoanIssue>("", false, model)
                    {
                        Data = model,
                        Message = response.Message,
                        Success = response.Success
                    };
                    return Json(res);
                }
                // Check if file exists with its full path    
            }
            catch (UnauthorizedAccessException e)
            {
                return RedirectToAction("Login", "Account", new { Area = "" });
            }
            catch (Exception e)
            {
                // Check if file exists with its full path    
                if (System.IO.File.Exists(path + "\\" + fileName))
                {
                    System.IO.File.Delete(path + "\\" + fileName);
                }
                return RedirectToAction("Error", "Account", new { Area = "" });
            }
        }

        public async Task<IActionResult> BulkUploadpage(LoanIssue model)
        {
            return PartialView("bulkupload", model);
        }
        public async Task<IActionResult> GetEmpProfilePrint(int RequestCode)
        {
            LoanIssue model = new LoanIssue();
            model.RequestCode = RequestCode;
            model.Url = await _commonService.GetUrl();
            return View("~/Areas/PayRoll/Views/LoanIssue/EmpProfilePrint.cshtml", model);


        }
    }
}
