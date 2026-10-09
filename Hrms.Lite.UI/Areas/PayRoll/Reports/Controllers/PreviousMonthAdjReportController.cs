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
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace Hrms.Lite.UI.Areas.PayRoll.Reports.Controllers
{
    [Area("PayRoll")]
    [SessionAuthorize]
    public class PreviousMonthAdjReportController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly ICommonService _commonService;
        private readonly IPreviousMonthAdjustmentsService _Service;
        private readonly IHelpers _helpers;


        public PreviousMonthAdjReportController(IDropdownService dropdownService, ICommonService commonService, IPreviousMonthAdjustmentsService
            PreviousMonthAdjustmentsService, IHelpers Helpers)
        {
            _dropdownService = dropdownService;
            _commonService = commonService;
            _Service = PreviousMonthAdjustmentsService;
            _helpers = Helpers;
           
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                PreviousMonthAdjustment model = new PreviousMonthAdjustment();
        
            ViewBag.Month = new SelectList(await _dropdownService.GetMonthDropdown(), "Code", "Name");
            model.AdjustmentType = "LOP";
                Filter filter = new Filter();
                filter.length = 10;
                model = await _Service.GetPrevMonthAdjReport(0, "LOP", filter);
                model.Filter = await _commonService.GetFilterDropdowns();
            return View("~/Areas/PayRoll/Reports/Views/PreviousMonthAdjReport/Index.cshtml", model);
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
        public async Task<IActionResult> GetReport(Filter filter1, int Month, string AdjustmentType)
        {
            try
            {
                PreviousMonthAdjustment model = new PreviousMonthAdjustment();


                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }
                if (Month != 0)
                    model = await _Service.GetPrevMonthAdjReport(Month, AdjustmentType, filter1);

                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.PrevMonthAdjList == null || model.PrevMonthAdjList.Count() == 0)
                {
                    return Json(new
                    {
                        data = "",
                        draw = filter1.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows

                    });
                }

                if (model.PrevMonthAdjList.Count > 0)
                {
                    totalrows = model.PrevMonthAdjList[0].active;
                }


                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = model.PrevMonthAdjList[0].totalRecords;

                    return Json(new
                    {
                        data = model.PrevMonthAdjList,
                        draw = filter1.draw,
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows

                    });
                }
                else
                {

                    return Json(new
                    {
                        data = model.PrevMonthAdjList,
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
        public async Task<IActionResult> ExportToExcel(Filter filter,int Month,  string MonthName, string adjtype, string adjtypename, 
            int totalRecord)
        {

            var LoginUser = HttpContext.Session.GetString("LoginUser");
            var CompanyName = HttpContext.Session.GetString("CompanyName");
            var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
            var GroupCompanyName = User.authUser.GroupCompanyName;
            ExcelPassword EP = new ExcelPassword();
            EP = await _commonService.GetExcelPassword("Arrear Allowance / Deduction Report", "Excel");

            string FilterValues = await _helpers.GetFilterValues(filter);
            string currDateTime = await _helpers.GetCurrentDate();
            

            filter.length = totalRecord;
            filter.start = 0;

            var dataset = await _Service.GetPrevMonthAdjReportExcel(Month, adjtype, filter);

            DataTable dt = dataset.Tables[0];

            dataset.Tables[0].Rows.RemoveAt(0);
            if ((dataset.Tables[0]?.Rows?.Count ?? 0) > 0)
            {
                dt = dataset.Tables[0];
            }

            //set EXCEL Column Heading 
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
            }

            using (XLWorkbook wb = new XLWorkbook())           {

                var worksheet = wb.Worksheets.Add("Arrear Allw Or Ded Report");
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
                wsReportNameHeaderRange.Value = "Arrear Allowance / Deduction Report";

                worksheet.Cell(2, 1).Style.Font.Bold = true;
                worksheet.Cell(2, 1).Value = "Group Company Name";
                worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();

                worksheet.Cell(2, 3).Style.Font.Bold = true;
                worksheet.Cell(2, 3).Value = "Month";
                worksheet.Cell(2, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                worksheet.Cell(2, 4).Style.DateFormat.Format = "MMM-yyyy";
                worksheet.Cell(2, 4).Value = MonthName;

                worksheet.Cell(2, 5).Style.Font.Bold = true;
                worksheet.Cell(2, 5).Value = "Adjustment Type";
                worksheet.Cell(2, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                worksheet.Cell(2, 6).Style.DateFormat.Format = "MMM-yyyy";
                worksheet.Cell(2, 6).Value = adjtypename;

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

                worksheet.Range("H5:K" + (dt.Rows.Count + 5)).Style.DateFormat.Format = "dd-MMM-yyyy";

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
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Arrear Allowance Or Deduction Report.xlsx");
                }
            }
        }
    }
}
