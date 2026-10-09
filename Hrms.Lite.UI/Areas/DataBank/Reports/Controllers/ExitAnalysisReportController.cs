using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.DataBank.Reports;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.Account;
using Hrms.Lite.Shared.DataBank.Reports;
using Hrms.Lite.Shared.General;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Spire.Xls;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.DataBank.Reports.Controllers
{
    [Area("DataBank")]
    [SessionAuthorize]
    public class ExitAnalysisReportController : Controller
    {
        private readonly IExitAnalysisReportService _ExitAnalysisReportService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        private readonly IDropdownService _dropdownService;

        public ExitAnalysisReportController(IExitAnalysisReportService ExitAnalysisReportService, ICommonService commonService, IHelpers Helpers, IDropdownService dropdownService)
        {
            _ExitAnalysisReportService = ExitAnalysisReportService;
            _commonService = commonService;
            _helpers = Helpers;
            _dropdownService = dropdownService;
        }

        public async Task <IActionResult> Index()
        {
            try
            {

            ExitAnalysisReport model = new ExitAnalysisReport();
            model.filter = await _commonService.GetFilterDropdowns();
            DateTime fDate = new DateTime(DateTime.Now.Year, 1, 1);
                DateTime tDate = new DateTime(DateTime.Now.Year, 12, 31);

            model = await _ExitAnalysisReportService.GetList(fDate.ToString("dd-MMM-yyyy"), tDate.ToString("dd-MMM-yyyy"), "DESIGNATION", model.filter);
                model.FromDate = fDate;
                model.ToDate = tDate;
                model.Type = "DESIGNATION";
                return View("~/Areas/DataBank/Reports/Views/ExitAnalysisReport/Index.cshtml", model);
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

        public async Task<IActionResult> GetList( string FromDate, string ToDate, string Type, Filter filter)
        {
            ExitAnalysisReport model = new ExitAnalysisReport();
            model = await _ExitAnalysisReportService.GetList(FromDate, ToDate, Type, filter);
            return PartialView("~/Areas/DataBank/Reports/Views/ExitAnalysisReport/_List.cshtml", model);
        }

        public async Task<IActionResult> GetPopUp( string FromDate, string ToDate, Guid GI, string Type)
        {
            ExitAnalysisReport model = new ExitAnalysisReport();
            //model.filter = await _commonService.GetFilterDropdowns();
            model = await _ExitAnalysisReportService.GetPopupList(FromDate, ToDate, GI, Type);
            return View("~/Areas/DataBank/Reports/Views/ExitAnalysisReport/_Popup.cshtml", model);
        }


        public async Task<IActionResult> ExportToExcel(Filter filter, int TotalRows)
        {
            ExitAnalysisReport model = new ExitAnalysisReport();
            var LoginUser = HttpContext.Session.GetString("LoginUser");
            var CompanyName = HttpContext.Session.GetString("CompanyName");
            var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
            var GroupCompanyName = User.authUser.GroupCompanyName;
            ExcelPassword EP = new ExcelPassword();
            EP = await _commonService.GetExcelPassword("Exit Analysis Report List", "Excel");


            //filter.length = TotalRows;
            //filter.start = 0;

            //string FilterValues = await _helpers.GetFilterValues(filter);
            //if (FilterValues == "")
            //{
            //    FilterValues = "All";
            //}
            //model.filter = filter;



            string[] cu_Date = System.DateTime.Now.GetDateTimeFormats();
            string currDateTime = cu_Date[44];

            //Get Dataset
            var dataset = await _ExitAnalysisReportService.ExitAnalysisReport_Excel(filter);
            DataTable dt = dataset.Tables[0];
            dataset.Tables[0].Rows.RemoveAt(0);
            if ((dataset.Tables[0]?.Rows?.Count ?? 0) > 0)
            {
                dt = dataset.Tables[0];
            }

            //set EXCEL Column Heading 

            for (int i = 0; i < dt.Columns.Count; i++)
            {
                if (dt.Columns[i].ColumnName == "id")
                    dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.ToUpper();
                if (dt.Columns[i].ColumnName == "doj" || dt.Columns[i].ColumnName == "lwd")
                    dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.ToUpper();
                else
                    dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
                if (dt.Columns[i].ColumnName == "Name")
                    dt.Columns[i].ColumnName = "Employee Name";

                //dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
            }

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Exit Analysis Report list");
                int count = dt.Rows.Count + 6;


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
                wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                wsReportNameHeaderRange.Value = "Exit Analysis Report list ";

                worksheet.Cell(2, 1).Style.Font.Bold = true;
                worksheet.Cell(2, 1).Value = "Group Company Name";
                worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                worksheet.Cell(2, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);

                //worksheet.Cell(3, 1).Style.Font.Bold = true;
                //worksheet.Cell(3, 1).Value = "FromDate";
                //worksheet.Cell(3, 2).Value = FromDate;
                //worksheet.Cell(3, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                //worksheet.Cell(3, 2).Style.DateFormat.Format = "dd-MMM-yyyy";
                //worksheet.Cell(3, 3).Style.Font.Bold = true;
                //worksheet.Cell(3, 3).Value = "ToDate";
                //worksheet.Cell(3, 4).Value = ToDate;
                //worksheet.Cell(3, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                //worksheet.Cell(3, 4).Style.DateFormat.Format = "dd-MMM-yyyy";


             


                worksheet.Cell(4, 1).Style.Font.Bold = true;
                worksheet.Cell(4, 1).Value = "Login User";
                worksheet.Cell(4, 2).Value = LoginUser;
                worksheet.Cell(4, 3).Style.Font.Bold = true;
                worksheet.Cell(4, 3).Value = "Date & Time";
                worksheet.Cell(4, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                worksheet.Cell(4, 4).Style.DateFormat.Format = "dd-MMM-yyyy";
                worksheet.Cell(4, 4).Value = currDateTime.ToString();
                worksheet.Cell(4, 5).Style.Font.Bold = true;
                worksheet.Cell(4, 5).Value = "Total Count";
                worksheet.Cell(4, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                worksheet.Cell(4, 6).Value = dt.Rows.Count;
                //worksheet.Cell(5, 1).Style.Font.Bold = true;
                //worksheet.Cell(5, 1).Value = "Filter Applied";
                //var filterapplied = worksheet.Range("B5:K5").Merge();

                //filterapplied.Value = FilterValues;


                var currentRow = 6;


                worksheet.Cell(currentRow, 1).InsertTable(dt);
                worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                worksheet.Range("A6:K6").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                worksheet.Range("A6:K6").Style.Font.Bold = true;


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
                        book.Protect(EP.Password);
                        book.SaveToStream(stream);
                    }
                    var content = stream.ToArray();
                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "ExitAnalysisReport.xlsx"
                        );
                }
            }         
        }






        //public async Task<IActionResult> ExportToExcel(string Type)
        //{
        //    try
        //    {
        //        ExitAnalysisReport model = new ExitAnalysisReport();
        //        model = await _ExitAnalysisReportService.ExitAnalysisReport_Excel(Type);
        //        int slno = 0;
        //        string currDateTime = await _helpers.GetCurrentDate();
        //        
        //        var LoginUser = HttpContext.Session.GetString("LoginUser");
        //        var CompanyName = HttpContext.Session.GetString("CompanyName");
        //        ExcelPassword EP = new ExcelPassword();
        //        EP = await _commonService.GetExcelPassword("Exit Analysis Report (" + Type + ")", "Excel");
        //        if (Type == "DESIGNATION")
        //        {

        //            using (var workbook = new XLWorkbook())
        //            {
        //                var worksheet = workbook.Worksheets.Add("District PendingList");
        //                ///int PendingCountnum = model.PendingCount + 6;


        //                worksheet.Range("A1:F").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
        //                worksheet.Range("A1:F").Style.Border.TopBorder = XLBorderStyleValues.Thin;
        //                worksheet.Range("A1:F").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
        //                worksheet.Range("A1:F").Style.Border.RightBorder = XLBorderStyleValues.Thin;
        //                worksheet.Range("A1:F").Style.Border.LeftBorder = XLBorderStyleValues.Thin;


        //                var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 5)));
        //                wsReportNameHeaderRange.Style.Font.Bold = true;
        //                wsReportNameHeaderRange.Style.Font.FontSize = 15;
        //                wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        //                wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
        //                wsReportNameHeaderRange.Merge();
        //                wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
        //                wsReportNameHeaderRange.Style.Font.FontSize = 15;
        //                wsReportNameHeaderRange.Value = "Exit Analysis Report " + "(" + Type + ")";
        //                worksheet.Range("A6:F6").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
        //                worksheet.Cell(2, 1).Style.Font.Bold = true;
        //                worksheet.Cell(2, 1).Value = "Company Name";
        //                worksheet.Cell(2, 2).Value = CompanyName.ToUpper();
        //                worksheet.Cell(3, 1).Style.Font.Bold = true;
        //                worksheet.Cell(3, 1).Value = "Login User";
        //                worksheet.Cell(3, 2).Value = LoginUser;
        //                worksheet.Cell(4, 1).Style.Font.Bold = true;
        //                worksheet.Cell(4, 1).Value = "Date & Time";
        //                worksheet.Cell(4, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //                worksheet.Cell(4, 2).Style.DateFormat.Format = "dd-MMM-yyyy   h:mm AM/PM";
        //                worksheet.Cell(4, 2).Value = currDateTime.ToString();
        //                worksheet.Cell(5, 1).Style.Font.Bold = true;
        //                worksheet.Cell(5, 1).Value = "Total Count";
        //                //worksheet.Cell(5, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //                //worksheet.Cell(5, 2).Value = model.PendingCount;
        //                var currentRow = 6;

        //                #region Header
        //                worksheet.Cell(currentRow, 1).Value = "Sl.No";
        //                worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
        //                worksheet.Cell(currentRow, 2).Value = "Name";
        //                worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
        //                worksheet.Cell(currentRow, 3).Value = "Short Name";
        //                worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
        //                worksheet.Cell(currentRow, 4).Value = "State";
        //                worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
        //                worksheet.Cell(currentRow, 5).Value = "Country";
        //                worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
        //                worksheet.Cell(currentRow, 6).Value = "Entry Type";
        //                worksheet.Cell(currentRow, 6).Style.Font.Bold = true;



        //                #endregion

        //                #region Body
        //                foreach (var item in model.ExitAnalysisReportList)
        //                {
        //                    currentRow++;
        //                    slno = slno + 1;
        //                    worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //                    worksheet.Cell(currentRow, 1).Value = slno;
        //                    worksheet.Cell(currentRow, 2).Value = item.EmployeeID;
        //                    worksheet.Cell(currentRow, 3).Value = item.EmployeeName;
        //                    worksheet.Cell(currentRow, 4).Value = item.Designation;
        //                    worksheet.Cell(currentRow, 5).Value = item.Department;
        //                    worksheet.Cell(currentRow, 6).Value = item.Location;
        //                    worksheet.Cell(currentRow, 7).Value = item.DOJ;
        //                    worksheet.Cell(currentRow, 8).Value = item.LWD;

        //                    worksheet.Columns(1, 5).Style.Alignment.WrapText = true;
        //                }
        //                #endregion
        //                worksheet.Columns().AdjustToContents();
        //                using (var stream = new MemoryStream())
        //                {
        //                    workbook.SaveAs(stream);
        //                    if (EP.NeedPassword)
        //                    {
        //                        Workbook book = new Workbook();
        //                        book.LoadFromStream(stream);
        //                        //Protect Workbook
        //                        book.Protect(EP.Password, false, false);
        //                        book.SaveToStream(stream);
        //                    }
        //                    var content = stream.ToArray();
        //                    return File(
        //                        content,
        //                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
        //                        "District Pending List.xlsx"
        //                        );
        //                }
        //            }



        //        }

        //        else
        //        {

        //            using (var workbook = new XLWorkbook())
        //            {
        //                var worksheet = workbook.Worksheets.Add("District List");
        //                int count = model.ExitAnalysisReportList.Count + 6;


        //                worksheet.Range("A1:E" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
        //                worksheet.Range("A1:E" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
        //                worksheet.Range("A1:E" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
        //                worksheet.Range("A1:E" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
        //                worksheet.Range("A1:E" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
        //                var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 4)));
        //                wsReportNameHeaderRange.Style.Font.Bold = true;
        //                wsReportNameHeaderRange.Style.Font.FontSize = 15;
        //                wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        //                wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
        //                wsReportNameHeaderRange.Merge();
        //                wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
        //                wsReportNameHeaderRange.Value = "Exit Analysis Report" + "(" + Type + ")";
        //                worksheet.Range("A6:E6").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
        //                worksheet.Cell(2, 1).Style.Font.Bold = true;
        //                worksheet.Cell(2, 1).Value = "Company Name";
        //                worksheet.Cell(2, 2).Value = CompanyName.ToUpper();
        //                worksheet.Cell(3, 1).Style.Font.Bold = true;
        //                worksheet.Cell(3, 1).Value = "Login User";
        //                worksheet.Cell(3, 2).Value = LoginUser;
        //                worksheet.Cell(4, 1).Style.Font.Bold = true;
        //                worksheet.Cell(4, 1).Value = "Date & Time";
        //                worksheet.Cell(4, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //                worksheet.Cell(4, 2).Style.DateFormat.Format = "dd-MMM-yyyy   h:mm AM/PM";
        //                worksheet.Cell(4, 2).Value = currDateTime.ToString();
        //                worksheet.Cell(5, 1).Style.Font.Bold = true;
        //                worksheet.Cell(5, 1).Value = "Total Count";
        //                worksheet.Cell(5, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //                worksheet.Cell(5, 2).Value = model.ExitAnalysisReportList.Count;

        //                //worksheet.Cell(3, 2).Value = System.DateTime.Now.ToShortDateString();




        //                var currentRow = 6;

        //                #region Header
        //                worksheet.Cell(currentRow, 1).Value = "Sl.No";
        //                worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
        //                worksheet.Cell(currentRow, 2).Value = "Name";
        //                worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
        //                worksheet.Cell(currentRow, 3).Value = "Short Name";
        //                worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
        //                worksheet.Cell(currentRow, 4).Value = "State";
        //                worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
        //                worksheet.Cell(currentRow, 5).Value = "Country";
        //                worksheet.Cell(currentRow, 5).Style.Font.Bold = true;

        //                #endregion

        //                #region Body
        //                foreach (var item in model.ExitAnalysisReportList)
        //                {
        //                    currentRow++;
        //                    slno = slno + 1;
        //                    worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //                    worksheet.Cell(currentRow, 1).Value = slno;
        //                    worksheet.Cell(currentRow, 2).Value = item.Name;
        //                    worksheet.Cell(currentRow, 3).Value = item.ShortName;
        //                    worksheet.Cell(currentRow, 4).Value = item.StateName;
        //                    worksheet.Cell(currentRow, 5).Value = item.CountryName;
        //                    worksheet.Columns(1, 5).Style.Alignment.WrapText = true;

        //                }
        //                #endregion
        //                worksheet.Columns().AdjustToContents();
        //                using (var stream = new MemoryStream())
        //                {
        //                    workbook.SaveAs(stream);
        //                    if (EP.NeedPassword)
        //                    {
        //                        Workbook book = new Workbook();
        //                        book.LoadFromStream(stream);
        //                        //Protect Workbook
        //                        book.Protect(EP.Password, false, false);
        //                        book.SaveToStream(stream);
        //                    }
        //                    var content = stream.ToArray();
        //                    return File(
        //                        content,
        //                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
        //                        "District master List.xlsx"
        //                        );
        //                }
        //            }
        //        }
        //    }
        //    catch (UnauthorizedAccessException e)
        //    {

        //        return RedirectToAction("Login", "Account", new { Area = "" });
        //    }
        //    catch (Exception e)
        //    {
        //        return RedirectToAction("Error", "Account", new { Area = "" });
        //    }
        //}


    }
}
