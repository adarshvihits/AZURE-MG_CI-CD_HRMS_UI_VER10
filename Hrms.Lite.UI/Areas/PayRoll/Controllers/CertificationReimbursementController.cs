using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Services.IServices.PayRoll;
using Hrms.Lite.Shared.PayRoll.AllowanceSettings;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.PayRoll;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Spire.Xls;
using System.Data;
using Hrms.Lite.Shared.Account;

namespace Hrms.Lite.UI.Areas.PayRoll.Controllers
{
    [Area("PayRoll")]
    [SessionAuthorize]
    public class CertificationReimbursementController : Controller
    {
        private readonly ICertificationReimbursementService _CertificationReimbursementService;
        private readonly IHelpers _helpers;
        private readonly IDropdownService _dropdownService;
        private readonly ICommonService _commonService;
        public static class Global
        {

            public static Filter GlobalFilter;

        }

        public CertificationReimbursementController(ICertificationReimbursementService CertificationReimbursementService, IDropdownService dropdownService, IHelpers Helpers, ICommonService commonService)
        {
            _CertificationReimbursementService = CertificationReimbursementService;
            _dropdownService = dropdownService;
            _helpers = Helpers;
            _commonService = commonService;
        }
        public async Task<IActionResult> Index()
        {
            CertificationReimbursement model = new CertificationReimbursement();
            ViewBag.Month = new SelectList(await _dropdownService.GetCertificationReimbursementMonthDropdown(), "Code", "Name");
            model.Filter = await _commonService.GetFilterDropdowns();
            return View("~/Areas/PayRoll/Views/CertificationReimbursement/Index.cshtml", model);
        }
        public async Task<IActionResult> GetCertificationReimbursementList(int MonthCode,int TabIndex, Filter filter1)
        {
            try
            {
                CertificationReimbursement model = new CertificationReimbursement();
                CertificationReimbursement m = new CertificationReimbursement();

                Filter filter = new Filter();
                Global.GlobalFilter = filter1;

                model.TabIndex = TabIndex;

                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }


                if (MonthCode != 0)
                {
                    model = await _CertificationReimbursementService.GetCertificationReimbursementList(MonthCode, TabIndex, filter1);
                }
                if (model == null /*|| model.CertificationReimbursementList == null || model.CertificationReimbursementList.Count() == 0*/)
                {
                    m.CertificationReimbursementList = new List<CertificationReimbursement>();
                    model = new CertificationReimbursement();
                    model.CertificationReimbursementList = new List<CertificationReimbursement>();
                }
                else
                {
                    m.CertificationReimbursementList = model.CertificationReimbursementList;
                }
                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.CertificationReimbursementList == null || model.CertificationReimbursementList.Count() == 0)
                {

                    return Json(new
                    {
                        data = "",
                        draw = filter1.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        pendingCount = model.PendingCount,
                        confirmedCount = model.ConfirmedCount,
                        tabIndex = model.TabIndex,
                        recordsFiltered = totalrows
                    });
                }




                if (model.CertificationReimbursementList.Count > 0)
                {
                    totalrows = m.CertificationReimbursementList[0].active;

                }


                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = m.CertificationReimbursementList[0].totalRecords;
                    return Json(new
                    {
                        data = model.CertificationReimbursementList,
                        draw = filter1.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        recordsTotal = totalrows,
                        pendingCount = model.PendingCount,
                        confirmedCount = model.ConfirmedCount,
                        tabIndex = model.TabIndex,
                        recordsFiltered = totalrows//totalrowsafterfiltering
                    });
                }
                else
                {
                    return Json(new
                    {
                        data = model.CertificationReimbursementList,
                        draw = filter1.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        pendingCount = model.PendingCount,
                        confirmedCount = model.ConfirmedCount,
                        tabIndex = model.TabIndex,
                        recordsFiltered = totalrows
                    });
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
        [HttpPost]
        public async Task<ActionResult> CertificationEmployeeWiseSave(decimal UpdatedAmount, int Month, Guid EmployeeGI, int EmployeeCode, Guid RequestGI, int RequestCode)
        {
            try
            {
                CertificationReimbursement model = new CertificationReimbursement();
                model.Month = new Dropdown();
                model.EmployeeGI = EmployeeGI;
                model.EmployeeCode = EmployeeCode;
                model.RequestGI = RequestGI;
                model.RequestCode = RequestCode;
                model.ExamFee = UpdatedAmount;
                model.Month.Code = Month;
                var response = await _CertificationReimbursementService.EmployeeWiseCertificationReimbursementSave(model, Month);
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
        public async Task<IActionResult> SaveCertificationDetails(int Month,CertificationReimbursement Model)
        {

            var response = await _CertificationReimbursementService.EmployeeWiseCertificationReimbursementSave(Model, Month);
            return Json(response);
        }
        public async Task<IActionResult> ExportToExcel(Filter filter,int Month, string MonthName, int TabIndex, string tabName, int TotalRows)
        {

            var LoginUser = HttpContext.Session.GetString("LoginUser");
            var CompanyName = HttpContext.Session.GetString("CompanyName");
            var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
            var GroupCompanyName = User.authUser.GroupCompanyName;
            ExcelPassword EP = new ExcelPassword();
            EP = await _commonService.GetExcelPassword("Certification Reimbursement List", "Excel");

            string FilterValues = await _helpers.GetFilterValues(filter);
            string currDateTime = await _helpers.GetCurrentDate();
            if (FilterValues == "")
                FilterValues = "All";
            

            filter.length = TotalRows;
            filter.start = 0;

            var dataset = await _CertificationReimbursementService.GetCertificationReimbursementList_Excel(Month, TabIndex, filter);

            DataTable dt = dataset.Tables[0];

            //set EXCEL Column Heading 
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {

                var worksheet = wb.Worksheets.Add("Certification Reimbursement");
                int rowcount = dt.Rows.Count + 5;
                string columnName = "";
                int Columncount = dt.Columns.Count;

                while (Columncount > 0)
                {
                    int modulo = (Columncount - 1) % 26;
                    columnName = Convert.ToChar('A' + modulo) + columnName;
                    Columncount = (Columncount - modulo) / 26;
                }

                string colName = columnName;

                worksheet.Range("A1", colName + rowcount).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1", colName + rowcount).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1", colName + rowcount).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1", colName + rowcount).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1", colName + rowcount).Style.Border.LeftBorder = XLBorderStyleValues.Thin;

                var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, colName));
                wsReportNameHeaderRange.Style.Font.Bold = true;
                wsReportNameHeaderRange.Style.Font.FontSize = 15;
                wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);

                wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                wsReportNameHeaderRange.Merge();
                wsReportNameHeaderRange.Value = "Certification Reimbursement List ("+ tabName + ")";

                worksheet.Cell(2, 1).Style.Font.Bold = true;
                worksheet.Cell(2, 1).Value = "Group Company Name";
                worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();



                worksheet.Cell(2, 3).Style.Font.Bold = true;
                worksheet.Cell(2, 3).Value = "Month";
                worksheet.Cell(2, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                worksheet.Cell(2, 4).Style.DateFormat.Format = "MMM-yyyy";
                worksheet.Cell(2, 4).Value = MonthName;

                worksheet.Cell(3, 1).Style.Font.Bold = true;
                worksheet.Cell(3, 1).Value = "Login User";
                worksheet.Cell(3, 2).Value = LoginUser.ToString();
                worksheet.Cell(3, 3).Style.Font.Bold = true;
                worksheet.Cell(3, 3).Value = "Date & Time";
                worksheet.Cell(3, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                worksheet.Cell(3, 4).Style.DateFormat.Format = "dd-MMM-yyyy   h:mm AM/PM";
                worksheet.Cell(3, 4).Value = currDateTime.ToString();
                worksheet.Cell(3, 5).Style.Font.Bold = true;
                worksheet.Cell(3, 5).Value = "Total Count";
                worksheet.Cell(3, 6).Value = dt.Rows.Count;
                worksheet.Cell(4, 1).Style.Font.Bold = true;
                worksheet.Cell(4, 1).Value = "Filter Applied";
                worksheet.Range("B4:" + colName + "4").Merge();
                worksheet.Cell("B4").Value = FilterValues;

                var currentRow = 5;

                worksheet.Cell(currentRow, 1).InsertTable(dt);
                worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                worksheet.Range("A5:" + colName + "5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                worksheet.Range("A5:" + colName + "5").Style.Font.Bold = true;

                worksheet.Columns().AdjustToContents();
                worksheet.Rows().AdjustToContents();
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    if (EP.NeedPassword)
                    {
                        Workbook book = new Workbook();
                        book.LoadFromStream(stream);
                        //Protect Workbook
                        book.Protect(EP.Password, false, false);
                        book.SaveToStream(stream);
                    }
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Certification Reimbursement List.xlsx");
                }
            }
        }
    }
}
