using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.TimeOffice.Report;
using Hrms.Lite.Shared.TimeOffice.Report;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using Hrms.Lite.Shared.Account;
using Hrms.Lite.Shared.General;
using System.Data;
using ClosedXML.Excel;
using System.IO;
using Spire.Xls;

namespace Hrms.Lite.UI.Areas.TimeOffice.Report.Controllers
{
    [Area("TimeOffice")]
    [SessionAuthorize]
    public class LessHourReportController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly ILessHourReportService _service;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;

        public LessHourReportController(IDropdownService dropdownService, ICommonService commonService, ILessHourReportService LessHourReportService, IHelpers helper)
        {
            _service = LessHourReportService;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = helper;
        }
        public async Task<IActionResult> Index()
        {
            LessHourReport model = new LessHourReport();
            model.FromDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            model.ToDate = DateTime.Now;
            model.Filter = await _commonService.GetFilterDropdowns();
            return View("~/Areas/TimeOffice/Report/Views/LessHourReport/Index.cshtml", model);
        }
        public async Task<IActionResult> GetLessHourReportList(DateTime FromDate, DateTime ToDate, Filter filter1)
        {
            try
            {
                LessHourReport model = new LessHourReport();
                Filter filter = new Filter();
                model.Filter = filter1;

                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }

                model = await _service.LessHourReportList(FromDate.ToString("yyyy-MMM-dd"), ToDate.ToString("yyyy-MMM-dd"), filter1);

                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.LessHourReportList == null || model.LessHourReportList.Count() == 0)
                {


                    return Json(new
                    {
                        data = "",
                        draw = filter1.draw,
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows
                    });
                }

                if (model.LessHourReportList.Count > 0)
                {
                    totalrows = model.LessHourReportList[0].active;
                }
                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = model.LessHourReportList[0].totalRecords;

                    return Json(new
                    {
                        data = model.LessHourReportList,
                        draw = filter1.draw,
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows//totalrowsafterfiltering
                    });
                }
                else
                {
                    return Json(new
                    {
                        data = model.LessHourReportList,
                        draw = filter1.draw,
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
        public async Task<IActionResult> ExportToExcel(string FromDate, string ToDate, Filter filter, int totRecords)
        {
            var LoginUser = HttpContext.Session.GetString("LoginUser");
            var CompanyName = HttpContext.Session.GetString("CompanyName");
            var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
            var GroupCompanyName = User.authUser.GroupCompanyName;
            ExcelPassword EP = new ExcelPassword();
            EP = await _commonService.GetExcelPassword("Less Hour Report", "Excel");

            string currDateTime = await _helpers.GetCurrentDate();
            

            filter.length = totRecords;
            var dataset = await _service.GetLessHourReportList_Excel(FromDate, ToDate, filter);

            DataTable dt = dataset.Tables[0];

            //set EXCEL Column Heading 
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                if (dt.Columns[i].ColumnName == "doj")

                    dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.ToUpper();
                if (dt.Columns[i].ColumnName == "employee Id")

                    dt.Columns[i].ColumnName = "Employee ID";

                else

                    dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
            }


            using (XLWorkbook wb = new XLWorkbook())
            {


                var worksheet = wb.Worksheets.Add("Less Hour Report");
                int rowcount = dt.Rows.Count + 4;
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
                wsReportNameHeaderRange.Value = "Less Hour Report";

                worksheet.Cell(2, 1).Style.Font.Bold = true;
                worksheet.Cell(2, 1).Value = "Group Company Name";
                worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();

                worksheet.Cell(2, 3).Style.Font.Bold = true;
                worksheet.Cell(2, 3).Value = "From";
                worksheet.Cell(2, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                worksheet.Cell(2, 4).Value = FromDate;
                worksheet.Cell(2, 4).Style.DateFormat.Format = "dd-MMM-yyyy";
                worksheet.Cell(2, 5).Style.Font.Bold = true;
                worksheet.Cell(2, 5).Value = "To";
                worksheet.Cell(2, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                worksheet.Cell(2, 6).Value = ToDate;
                worksheet.Cell(2, 6).Style.DateFormat.Format = "dd-MMM-yyyy";
                worksheet.Cell(2, 7).Style.Font.Bold = true;
                worksheet.Cell(2, 7).Value = "Total Count";
                worksheet.Cell(2, 8).Value = dt.Rows.Count;
                worksheet.Cell(2, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);


                worksheet.Cell(3, 1).Style.Font.Bold = true;
                worksheet.Cell(3, 1).Value = "Login User";
                worksheet.Cell(3, 2).Value = LoginUser.ToString();
                worksheet.Cell(3, 3).Style.Font.Bold = true;
                worksheet.Cell(3, 3).Value = "Date & Time";
                worksheet.Cell(3, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                worksheet.Cell(3, 4).Style.DateFormat.Format = "dd-MMM-yyyy   h:mm AM/PM";
                worksheet.Cell(3, 4).Value = currDateTime.ToString();
                //worksheet.Cell(3, 5).Style.Font.Bold = true;
                //worksheet.Cell(3, 5).Value = "Total Count";
                //worksheet.Cell(3, 6).Value = dt.Rows.Count;
                //worksheet.Cell(3, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);


                var currentRow = 4;

                worksheet.Cell(currentRow, 1).InsertTable(dt);
                worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                worksheet.Range("A4:" + colName + "4").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                worksheet.Range("A4:" + colName + "4").Style.Font.Bold = true;

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
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Less Hour Report.xlsx");
                }
            }
        }
    }
}
