using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Spire.Xls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.DataBank.Controllers
{
    [Area("DataBank")]
    [SessionAuthorize]
    public class SuspensionController : Controller
    {
        private readonly ICommonService _commonService;
        private readonly IDropdownService _dropdownService;
        private readonly ISuspensionService _service;
        private readonly IHelpers _helpers;
        public SuspensionController(ICommonService commonService, ISuspensionService service,IDropdownService dropdownService,IHelpers helpers)
        {
            _commonService = commonService;
            _service = service;
            _dropdownService = dropdownService;
            _helpers = helpers;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                Suspension model = new Suspension();
                model.Filter = new Filter();
                model = await _service.GetList(1,model.Filter);
                model.Filter = await _commonService.GetFilterDropdowns();
                model.Tab = "Pending";
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
        public async Task<IActionResult> GetList(int TabIndex,Filter filter)
        {
            try
            {
                Suspension model = new Suspension();
                model = await _service.GetList(TabIndex, filter);
                if (TabIndex == 1)
                    model.Tab = "Pending";
                else
                    model.Tab = "Completed";
                return View("_List",model);
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
        public async Task<IActionResult> GetDetails(Guid IncidentGI,string EmpName)
        {
            try
            {
                Suspension model = new Suspension();
                model = await _service.GetDetails(IncidentGI);
                if (model.Details == null)
                {
                    model.Details = new SuspensionDetails();
                    model.Details.EmployeeName = EmpName;
                }
                else
                {
                    model.Details.EmployeeName = EmpName;
                }
                if (model.SuspensionSalarySplit == null || model.SuspensionSalarySplit.Count ==0)
                { 
                    model.SuspensionSalarySplit = new List<SuspensionSalarySplit>();
                    model.SuspensionSalarySplit.Add(new SuspensionSalarySplit { FromDate = null, ToDate = null });
                }
                ViewBag.AttnStatus = new SelectList(await _dropdownService.GetAttendanceSuspensionDropDown(), "Code", "Name");

                return View("Create", model);
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
        public async Task<IActionResult> Save(Suspension model)
        {
            try
            {

                var response = await _service.Save(model);
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
        public async Task<IActionResult> Delete(Guid IncidentGI)
        {
            try
            {

                var response = await _service.Delete(IncidentGI);
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
        public async Task<IActionResult> TransactionHistory()
        {
            try
            {
                Suspension model = new Suspension();
                Filter filter = new Filter();
                DateTime From = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                DateTime To = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1);
                model = await _service.GetTransactionHistoryList(From.ToString("dd-MMM-yyyy"),To.ToString("dd-MMM-yyyy"), filter);
                model.FromDate = From;
                model.ToDate = To;
                model.Filter = await _commonService.GetFilterDropdowns();
                return View("TransactionHistory", model);
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
        public async Task<IActionResult> GetTransactionHistoryList(string FromDate, string ToDate,Filter filter)
        {
            try
            {
                Suspension model = new Suspension();
                model = await _service.GetTransactionHistoryList(FromDate, ToDate,filter);               
                return PartialView("_TransactionHistoryList", model);
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
        public async Task<IActionResult> ExportToExcel(int TabIndex, string TabName,Filter filter)
        {
            Suspension model = new Suspension();
            //Filter filter = new Filter();
            var LoginUser = HttpContext.Session.GetString("LoginUser");
            var CompanyName = HttpContext.Session.GetString("CompanyName");
            ExcelPassword EP = new ExcelPassword();
            EP = await _commonService.GetExcelPassword("Suspension (" + TabName + ")", "Excel");
            int slNo = 0;

            string FilterValues = await _helpers.GetFilterValues(filter);
            model = await _service.GetList(TabIndex, filter);
            if (FilterValues == "")
            {
                FilterValues = "All";
            }
            string currDateTime = await _helpers.GetCurrentDate();
            



            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Suspension");
                int count = model.SuspensionList.Count + 5;


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
                wsReportNameHeaderRange.Value = "Suspension(" + TabName + ")";

                worksheet.Cell(2, 1).Style.Font.Bold = true;
                worksheet.Cell(2, 1).Value = "Company Name";
                worksheet.Cell(2, 2).Value = CompanyName.ToUpper();
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
                worksheet.Cell(3, 6).Value = model.SuspensionList.Count;
                worksheet.Cell(4, 1).Style.Font.Bold = true;
                worksheet.Cell(4, 1).Value = "Filter Applied";
                worksheet.Range("B4:J4").Merge();
                worksheet.Cell("B4").Value = FilterValues;
                var currentRow = 5;

                #region Header
                worksheet.Cell(currentRow, 1).Value = "Sl.No";
                worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 2).Value = "ID";
                worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 3).Value = "Employee Name";
                worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 4).Value = "Fun.Designation";
                worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 5).Value = "Department";
                worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 6).Value = "Location";
                worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 7).Value = "DOJ";
                worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 8).Value = "Incident";
                worksheet.Cell(currentRow, 8).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 9).Value = "Occured Date";
                worksheet.Cell(currentRow, 9).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 10).Value = "Reported By";
                worksheet.Cell(currentRow, 10).Style.Font.Bold = true;

                #endregion

                #region Body
                foreach (var item in model.SuspensionList)
                {
                    currentRow++;
                    slNo = slNo + 1;
                    worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Range("A5:J5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Cell(currentRow, 1).Value = slNo;
                    worksheet.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(currentRow, 2).Value = item.EmployeeID;
                    worksheet.Cell(currentRow, 3).Value = item.EmployeeName;
                    worksheet.Cell(currentRow, 4).Value = item.Designation;
                    worksheet.Cell(currentRow, 5).Value = item.Department;
                    worksheet.Cell(currentRow, 6).Value = item.Location;

                    worksheet.Cell(currentRow, 7).Style.DateFormat.Format = "dd-MMM-yyyy";
                    worksheet.Cell(currentRow, 7).Value = item.DOJ;
                    worksheet.Cell(currentRow, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(currentRow, 8).Value = item.Incident;
                    worksheet.Cell(currentRow, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(currentRow, 9).Value = item.OccuredDate;
                    worksheet.Cell(currentRow, 9).Style.DateFormat.Format = "dd-MMM-yyyy";
                    worksheet.Cell(currentRow, 9).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(currentRow, 10).Value = item.ReportedBy;
                    worksheet.Cell(currentRow, 10).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
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
                        "Suspension List.xlsx"
                        );
                }
            }

            //}
        }
        public async Task<IActionResult> TransactionHistoryExportToExcel(string FromDate,string ToDate, Filter filter)
        {
            Suspension model = new Suspension();
            //Filter filter = new Filter();
            var LoginUser = HttpContext.Session.GetString("LoginUser");
            var CompanyName = HttpContext.Session.GetString("CompanyName");
            ExcelPassword EP = new ExcelPassword();
            EP = await _commonService.GetExcelPassword("Suspension (Transaction History)", "Excel");
            int slNo = 0;

            string FilterValues = await _helpers.GetFilterValues(filter);
            model = await _service.GetTransactionHistoryList(FromDate, ToDate, filter);
            if (FilterValues == "")
            {
                FilterValues = "All";
            }
            string currDateTime = await _helpers.GetCurrentDate();
            



            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Suspension");
                int count = model.SuspensionList.Count + 5;


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
                wsReportNameHeaderRange.Value = "Suspension(Transaction History)";

                worksheet.Cell(2, 1).Style.Font.Bold = true;
                worksheet.Cell(2, 1).Value = "Company Name";
                worksheet.Cell(2, 2).Value = CompanyName.ToUpper();
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
                worksheet.Cell(3, 6).Value = model.SuspensionList.Count;
                worksheet.Cell(4, 1).Style.Font.Bold = true;
                worksheet.Cell(4, 1).Value = "Filter Applied";
                worksheet.Range("B4:J4").Merge();
                worksheet.Cell("B4").Value = FilterValues;
                var currentRow = 5;

                #region Header
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
                worksheet.Cell(currentRow, 7).Value = "DOJ";
                worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 8).Value = "Incident";
                worksheet.Cell(currentRow, 8).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 9).Value = "Occured Date";
                worksheet.Cell(currentRow, 9).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 10).Value = "Reported By";
                worksheet.Cell(currentRow, 10).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 11).Value = "Entry User";
                worksheet.Cell(currentRow, 11).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 12).Value = "Entry Date";
                worksheet.Cell(currentRow, 12).Style.Font.Bold = true;

                #endregion

                #region Body
                foreach (var item in model.SuspensionList)
                {
                    currentRow++;
                    slNo = slNo + 1;
                    worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Range("A5:J5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Cell(currentRow, 1).Value = slNo;
                    worksheet.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(currentRow, 2).Value = item.EmployeeID;
                    worksheet.Cell(currentRow, 3).Value = item.EmployeeName;
                    worksheet.Cell(currentRow, 4).Value = item.Designation;
                    worksheet.Cell(currentRow, 5).Value = item.Department;
                    worksheet.Cell(currentRow, 6).Value = item.Location;

                    worksheet.Cell(currentRow, 7).Style.DateFormat.Format = "dd-MMM-yyyy";
                    worksheet.Cell(currentRow, 7).Value = item.DOJ;
                    worksheet.Cell(currentRow, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(currentRow, 8).Value = item.Incident;
                    worksheet.Cell(currentRow, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(currentRow, 9).Value = item.OccuredDate;
                    worksheet.Cell(currentRow, 9).Style.DateFormat.Format = "dd-MMM-yyyy";
                    worksheet.Cell(currentRow, 9).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(currentRow, 10).Value = item.ReportedBy;
                    worksheet.Cell(currentRow, 10).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(currentRow, 11).Value = item.EntryUser;
                    worksheet.Cell(currentRow, 11).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(currentRow, 12).Value = item.OccuredDate;
                    worksheet.Cell(currentRow, 12).Style.DateFormat.Format = "dd-MMM-yyyy";
                    worksheet.Cell(currentRow, 12).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Columns(1, 12).Style.Alignment.WrapText = true;
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
                        "Suspension Transaction History List.xlsx"
                        );
                }
            }

            //}
        }
    }
}
