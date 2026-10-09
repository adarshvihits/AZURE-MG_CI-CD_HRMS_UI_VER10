using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.PayRoll.Reports;
using Hrms.Lite.Services.Services.PayRoll.Reports;
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
    public class PreFullandFinalSettlementReportController : Controller
    {
        private readonly IPreFullandFinalSettlementReportService _PreFullandFinalService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        private readonly IDropdownService _dropdownService;
        public PreFullandFinalSettlementReportController(IPreFullandFinalSettlementReportService PreFullandFinalService, ICommonService commonService, IHelpers Helpers, IDropdownService dropdownService)
        {
            _PreFullandFinalService = PreFullandFinalService;
            _commonService = commonService;
            _helpers = Helpers;
            _dropdownService = dropdownService;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                PreFullandFinalSettlement model = new PreFullandFinalSettlement();
            ViewBag.Month = new SelectList(await _dropdownService.GetMonthDropdown(), "Code", "Name");
            model.Filter = await _commonService.GetFilterDropdowns();
                Filter filter = new Filter();
                filter.length = 10;
                model = model = await _PreFullandFinalService.FandFDetailedReport(0, filter);
                return View("~/Areas/PayRoll/Reports/Views/PreFullandFinalSettlementReport/Index.cshtml", model);
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
        public async Task<IActionResult> GetList(Filter filter1, int MONTH)
        {
            try
            {
                PreFullandFinalSettlement model = new PreFullandFinalSettlement();


                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }
                if (MONTH != 0 )
                    model = await _PreFullandFinalService.FandFDetailedReport(MONTH, filter1);

                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.PreFullandFinalSettlementList == null || model.PreFullandFinalSettlementList.Count() == 0)
                {


                    return Json(new
                    {
                        data = "",
                        draw = filter1.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows

                    });
                }

                if (model.PreFullandFinalSettlementList.Count > 0)
                {
                    totalrows = model.PreFullandFinalSettlementList[0].active;
                }


                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = model.PreFullandFinalSettlementList[0].totalRecords;

                    return Json(new
                    {
                        data = model.PreFullandFinalSettlementList,
                        draw = filter1.draw,
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows

                    });
                }
                else
                {

                    return Json(new
                    {
                        data = model.PreFullandFinalSettlementList,
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
     
        public async Task<IActionResult> ExportToExcel(int Month, string MonthName,int totRecords, Filter filter)
        {

            try
            {
                SalaryRelease model = new SalaryRelease();

                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Pre FandF Report", "Excel");
                int slNo = 0;


                string FilterValues = await _helpers.GetFilterValues(filter);
                filter.length = totRecords;
                var dataset = await _PreFullandFinalService.FandFDetailedReport_EXCEL(Month,  filter);
                DataTable dt = dataset.Tables[0];
                if (FilterValues == "")
                {
                    FilterValues = "All";
                }

               

                string currDateTime = await _helpers.GetCurrentDate();
                
                //set EXCEL Column Heading Format
                for (int i = 0; i < dt.Columns.Count; i++)
                {

                    dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
                    if (dt.Columns[i].ColumnName == "Doj" || dt.Columns[i].ColumnName == "Lwd")
                        dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.ToUpper();

                }

                using (var workbook = new XLWorkbook())
                {

                    var worksheet = workbook.Worksheets.Add("Pre F & F Report");


                    int count = dt.Rows.Count + 6;


                    worksheet.Range("A1:R" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:R" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:R" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:R" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:R" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;

                    var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 17)));
                    wsReportNameHeaderRange.Style.Font.Bold = true;
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Merge();
                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                    wsReportNameHeaderRange.Value = "Pre Full & Final Report";

                    worksheet.Cell(2, 1).Style.Font.Bold = true;
                    worksheet.Cell(2, 1).Value = "Group Company Name";
                    worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                    worksheet.Cell(2, 3).Style.Font.Bold = true;


                    worksheet.Cell(3, 1).Style.Font.Bold = true;
                    worksheet.Cell(3, 1).Value = "Month";
                    worksheet.Cell(3, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(3, 2).Style.DateFormat.Format = "MMM-yyyy";
                    worksheet.Cell(3, 2).Value = MonthName;


                 


                    worksheet.Cell(4, 1).Style.Font.Bold = true;
                    worksheet.Cell(4, 1).Value = "Login User";
                    worksheet.Cell(4, 2).Value = LoginUser;
                    worksheet.Cell(4, 3).Style.Font.Bold = true;
                    worksheet.Cell(4, 3).Value = "Date & Time";
                    worksheet.Cell(4, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(4, 4).Style.DateFormat.Format = "dd-MMM-yyyy   h:mm AM/PM";
                    worksheet.Cell(4, 4).Value = currDateTime.ToString();
                    worksheet.Cell(4, 5).Style.Font.Bold = true;
                    worksheet.Cell(4, 5).Value = "Total Count";
                    worksheet.Cell(4, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(4, 6).Value = totRecords;
                    worksheet.Cell(5, 1).Style.Font.Bold = true;
                    worksheet.Cell(5, 1).Value = "Filter Applied";
                    var filterapplied = worksheet.Range("B5:R5").Merge();

                    filterapplied.Value = FilterValues;


                    var currentRow = 6;
                    worksheet.Cell(currentRow, 1).InsertTable(dt);


                    worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                    worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                    worksheet.Range("A6:R6").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Range("A6:R6").Style.Font.Bold = true;





                    worksheet.Columns().AdjustToContents();
                    worksheet.Rows().AdjustToContents();
                    //worksheet.Protect("123");
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
                            "Pre FandF Settlement Report.xlsx"
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
