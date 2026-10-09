using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.PayRoll.Reports;
using Hrms.Lite.Shared.General;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hrms.Lite.Services.IServices.PayRoll.Reports;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Hrms.Lite.Shared.Account;
using System.Data;
using ClosedXML.Excel;
using Spire.Xls;
using System.IO;

namespace Hrms.Lite.UI.Areas.PayRoll.Reports.Controllers
{
    [Area("PayRoll")]
    [SessionAuthorize]
    public class LOPReportController : Controller
    {
        private readonly ILOPReportService _LOPReportService;
        private readonly IDropdownService _dropdownService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;

       
        public LOPReportController( IDropdownService dropdownService, ICommonService commonService, IHelpers helpers, ILOPReportService LOPReportService)
        {
            _LOPReportService = LOPReportService;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = helpers;

        }
        public async Task<IActionResult> Index()
        {

            LOPReport model = new LOPReport();

            Filter filter = new Filter();
            ViewBag.Month = new SelectList(await _dropdownService.GetMonthDropdown(), "Code", "Name");
            int Month = 0;
            //model.Type = 0;
            model = await _LOPReportService.GetList(Month, 0);
            model.Filter = await _commonService.GetFilterDropdowns();
            
            
            
            
            return View("~/Areas/PayRoll/Reports/Views/LOPReport/Index.cshtml",model);

        }

      



        
        public async Task<ActionResult> GetList( int Month,int Type)
        {
            try
            {
                LOPReport model = new LOPReport();
                
                if (Month != 0)
                {
                    model = await _LOPReportService.GetList(Month,Type);
                }
                model.Type = Type;
                if (Type == 0)
                {
                    return PartialView("~/Areas/PayRoll/Reports/Views/LOPReport/_LOPReportList.cshtml", model);

                }
                else
                {
                    return PartialView("~/Areas/PayRoll/Reports/Views/LOPReport/_LOPAmountReportList.cshtml", model);

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





        //[HttpPost]
        //public async Task<IActionResult> GetLOPReportList(int Month, Filter filter1,int Type)
        //{
        //    try
        //    {
        //        LOPReport model = new LOPReport();


        //        model.Filter = filter1;

        //        if (filter1.searchValue == "undefined" || filter1.searchValue == null)
        //        {
        //            filter1.searchValue = "";
        //        }

        //        model = await _LOPReportService.GetList(Month, filter1,Type);
 
        //        int totalrows = 0;
        //        int totalrowsafterfiltering = 0;

        //        if (model.LOPReportList == null || model.LOPReportList.Count() == 0)
        //        {


        //            return Json(new
        //            {
        //                data = "",
        //                draw = filter1.draw,//Request.Form["draw"], 
        //                recordsTotal = totalrows,
        //                recordsFiltered = totalrows
        //            });
        //        }




        //        if (model.LOPReportList.Count > 0)
        //        {
        //            totalrows = model.LOPReportList[0].active;


        //        }

        //        if (filter1.searchValue != "")
        //        {
        //            totalrowsafterfiltering = model.LOPReportList[0].totalRecords;
        //            return Json(new
        //            {
        //                data = model.LOPReportList,
        //                draw = filter1.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
        //                recordsTotal = totalrows,
        //                recordsFiltered = totalrows//totalrowsafterfiltering
        //            });
        //        }
        //        else
        //        {
        //            // return Json(new { data = model.EmployeeProfileList, draw = filter1.draw, recordsTotal = totalrows, recordsFiltered = totalrows });
        //            return Json(new
        //            {
        //                data = model.LOPReportList,
        //                draw = filter1.draw,//Request.Form["draw"], 
        //                recordsTotal = totalrows,
        //                recordsFiltered = totalrows
        //            });
        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        return View();
        //    }




        //}


        public static string ConvertSubstringToUpperCase(string originalString, int startIndex, int length)
        {
            string upperCaseSubstring = originalString.Substring(startIndex, length).ToUpper();
            return originalString.Substring(0, startIndex) + upperCaseSubstring + originalString.Substring(startIndex + length);
        }






        //public async Task<IActionResult> ExportToexcel(int Month, string MonthName,int Type,  int TotalRows,Filter filter)
        //{
        //    try
        //    {

        //        var LoginUser = HttpContext.Session.GetString("LoginUser");
        //        var CompanyName = HttpContext.Session.GetString("CompanyName");
        //        var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
        //        var GroupCompanyName = User.authUser.GroupCompanyName;
        //        ExcelPassword EP = new ExcelPassword();
        //        EP = await _commonService.GetExcelPassword("LOP Report", "Excel");


        //        filter.length = TotalRows;
        //        filter.start = 0;
        //        string FilterValues = await _helpers.GetFilterValues(filter);
        //        if (FilterValues == "")
        //        {
        //            FilterValues = "All";
        //        }
        //        int index = 0;
        //        //Get Dataset
        //        var dataset = await _LOPReportService.GetLOPReport_Excel(Month, Type, filter);
        //        DataTable dt = dataset.Tables[0];


        //        //set EXCEL Column Heading 
        //        //for (int i = 0; i < dt.Columns.Count; i++)
        //        //{
        //        //    if (dt.Columns[i].ColumnName.Contains("esi"))
        //        //    {
        //        //        index = dt.Columns[i].ColumnName.IndexOf("esi");
        //        //        dt.Columns[i].ColumnName = ConvertSubstringToUpperCase(dt.Columns[i].ColumnName, index, 3);
        //        //    }

        //        //    else
        //        //        dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
        //        //}
        //        for (int i = 0; i < dt.Columns.Count; i++)
        //        {
        //            //if (dt.Columns[i].ColumnName == "doj" || dt.Columns[i].ColumnName == "dob" || dt.Columns[i].ColumnName == "y/s")
        //            //    dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.ToUpper();
        //            //else
        //            dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
        //        }


        //        string currDateTime = await _helpers.GetCurrentDate();
        //        
        //        using (var workbook = new XLWorkbook())
        //        {

        //            var worksheet = workbook.Worksheets.Add("LOP Report");


        //            int count = dt.Rows.Count + 5;

        //            string columnName = "";
        //            int Columncount = dt.Columns.Count;

        //            while (Columncount > 0)
        //            {
        //                int modulo = (Columncount - 1) % 26;
        //                columnName = Convert.ToChar('A' + modulo) + columnName;
        //                Columncount = (Columncount - modulo) / 26;
        //            }

        //            string colName = columnName;

        //            worksheet.Range("A1", colName + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
        //            worksheet.Range("A1", colName + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
        //            worksheet.Range("A1", colName + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
        //            worksheet.Range("A1", colName + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
        //            worksheet.Range("A1", colName + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;




        //            var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, colName));
        //            wsReportNameHeaderRange.Style.Font.Bold = true;
        //            wsReportNameHeaderRange.Style.Font.FontSize = 15;
        //            wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        //            wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
        //            wsReportNameHeaderRange.Merge();
        //            wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
        //            wsReportNameHeaderRange.Value = "LOP Report";

        //            worksheet.Cell(2, 1).Style.Font.Bold = true;
        //            worksheet.Cell(2, 1).Value = "Group Company Name";
        //            worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
        //            worksheet.Cell(2, 3).Style.Font.Bold = true;
        //            worksheet.Cell(2, 3).Value = "Month";
        //            worksheet.Cell(2, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //            worksheet.Cell(2, 4).Value = MonthName;
        //            //worksheet.Cell(2, 5).Style.Font.Bold = true;
        //            //worksheet.Cell(2, 5).Value = "ESI Group";
        //            //worksheet.Cell(2, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //            //worksheet.Cell(2, 6).Value = ESiName;

        //            worksheet.Cell(3, 1).Style.Font.Bold = true;
        //            worksheet.Cell(3, 1).Value = "Login User";
        //            worksheet.Cell(3, 2).Value = LoginUser;
        //            worksheet.Cell(3, 3).Style.Font.Bold = true;
        //            worksheet.Cell(3, 3).Value = "Date & Time";
        //            worksheet.Cell(3, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //            worksheet.Cell(3, 4).Style.DateFormat.Format = "dd-MMM-yyyy   h:mm AM/PM";
        //            worksheet.Cell(3, 4).Value = currDateTime.ToString();
        //            worksheet.Cell(3, 5).Style.Font.Bold = true;
        //            worksheet.Cell(3, 5).Value = "Total Count";
        //            worksheet.Cell(3, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //            worksheet.Cell(3, 6).Value = dt.Rows.Count;
        //            worksheet.Cell(4, 1).Style.Font.Bold = true;
        //            worksheet.Cell(4, 1).Value = "Filter Applied";
        //            var filterapplied = worksheet.Range("B4", colName + 4).Merge();

        //            filterapplied.Value = FilterValues;


        //            var currentRow = 5;

        //            worksheet.Cell(currentRow, 1).InsertTable(dt);
        //            worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

        //            worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
        //            worksheet.Range("A5", colName + 5).Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
        //            worksheet.Range("A5", colName + 5).Style.Font.Bold = true;


        //            worksheet.Columns().AdjustToContents();
        //            worksheet.Rows().AdjustToContents();
        //            //worksheet.Protect("123");
        //            using (var stream = new MemoryStream())
        //            {
        //                workbook.SaveAs(stream);
        //                if (EP.NeedPassword)
        //                {
        //                    Workbook book = new Workbook();
        //                    book.LoadFromStream(stream);
        //                    //Protect Workbook
        //                    book.Protect(EP.Password, false, false);
        //                    book.SaveToStream(stream);
        //                }
        //                var content = stream.ToArray();
        //                return File(
        //                    content,
        //                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
        //                    "LOP Report List.xlsx"
        //                    );
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
        public async Task<IActionResult> ExportToExcel(int Month, string MonthName, int Type, int TotalRows)
        {
            try
            {
                LOPReport model = new LOPReport();
                model = await _LOPReportService.GetList(Month, Type);
                //var dataSet = await _LOPReportService.GetLOPReport_Excel(Month, Type, filter);
                int slno = 0;
                string currDateTime = await _helpers.GetCurrentDate();
                
                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("LOP Report List", "Excel");
                if (Type == 0)
                {

                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("LOP Report List");
                        int TotalCount = model.LOPReportList.Count + 5;


                        worksheet.Range("A1:O" + TotalCount).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:O" + TotalCount).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:O" + TotalCount).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:O" + TotalCount).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:O" + TotalCount).Style.Border.LeftBorder = XLBorderStyleValues.Thin;



                        var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:O{0}", 1, Char.ConvertFromUtf32(65 + 3)));
                        wsReportNameHeaderRange.Style.Font.Bold = true;
                        wsReportNameHeaderRange.Style.Font.FontSize = 15;
                        wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                        wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        wsReportNameHeaderRange.Merge();


                        wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                        wsReportNameHeaderRange.Style.Font.FontSize = 15;
                        wsReportNameHeaderRange.Value = "LOP Report List";
                        worksheet.Range("A6:O6").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                        worksheet.Cell(2, 1).Style.Font.Bold = true;
                        worksheet.Cell(2, 1).Value = "Company Name";
                        worksheet.Cell(2, 2).Value = CompanyName.ToUpper();
                        worksheet.Cell(3, 1).Style.Font.Bold = true;
                        worksheet.Cell(3, 1).Value = "Login User";
                        worksheet.Cell(3, 2).Value = LoginUser;
                        worksheet.Cell(4, 1).Style.Font.Bold = true;
                        worksheet.Cell(4, 1).Value = "Date & Time";
                        worksheet.Cell(4, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(4, 2).Style.DateFormat.Format = "dd-MMM-yyyy   h:mm AM/PM";
                        worksheet.Cell(4, 2).Value = currDateTime.ToString();
                        //worksheet.Cell(5, 1).Style.Font.Bold = true;
                        //worksheet.Cell(5, 1).Value = "Total Count";
                        //worksheet.Cell(5, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        //worksheet.Cell(5, 2).Value = model.PendingCount;
                        var currentRow = 6;

                        #region Header
                        worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 1).Value = "ID";
                        worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 2).Value = "Employee Name";
                        worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 3).Value = "Designation";
                        worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 4).Value = "Department";
                        worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 5).Value = "Location";
                        worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 6).Value = "LOPAbsent";
                        worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 7).Value = "ESI Leave";
                        worksheet.Cell(currentRow, 8).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 8).Value = "LJP";
                        worksheet.Cell(currentRow, 9).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 9).Value = "LTP";
                        worksheet.Cell(currentRow, 10).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 10).Value = "Previous LOP";
                        worksheet.Cell(currentRow, 11).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 11).Value = "Total Days To Cut";
                        worksheet.Cell(currentRow, 12).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 12).Value = "Suspension";
                        worksheet.Cell(currentRow, 13).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 13).Value = "Lay Off";
                        worksheet.Cell(currentRow, 14).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 14).Value = "Less Hours";
                        worksheet.Cell(currentRow, 15).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 15).Value = "Total Amount To Cut";


                        #endregion

                        #region Body
                        foreach (var item in model.LOPReportList)
                        {
                            currentRow++;
                            slno = slno + 1;
                            worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 1).Value = item.EmployeeID;
                            worksheet.Cell(currentRow, 2).Value = item.EmployeeName;
                            worksheet.Cell(currentRow, 3).Value = item.Designation;
                            worksheet.Cell(currentRow, 4).Value = item.Department;
                            worksheet.Cell(currentRow, 5).Value = item.Location;
                            worksheet.Cell(currentRow, 6).Value = item.LOPAbsent;
                            worksheet.Cell(currentRow, 7).Value = item.ESILeave;
                            worksheet.Cell(currentRow, 8).Value = item.LJP;
                            worksheet.Cell(currentRow, 9).Value = item.LTP;
                            worksheet.Cell(currentRow, 10).Value = item.PrevLOP;
                            worksheet.Cell(currentRow, 11).Value = item.TotCutDays;
                            worksheet.Cell(currentRow, 12).Value = item.Suspension;
                            worksheet.Cell(currentRow, 13).Value = item.Layoff;
                            worksheet.Cell(currentRow, 14).Value = item.Latehrs;
                            worksheet.Cell(currentRow, 15).Value = item.TotCutAmt;
                            worksheet.Columns(1, 5).Style.Alignment.WrapText = true;

                        }
                        #endregion
                        worksheet.Columns().AdjustToContents();
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
                                "LOP Report List.xlsx"
                                );
                        }
                    }

                }

                else
                {

                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("LOP Report List");
                        int count = model.LOPReportList.Count + 5;


                        worksheet.Range("A1:W" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:W" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:W" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:W" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:W" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:W{0}", 1, Char.ConvertFromUtf32(65 + 2)));
                        wsReportNameHeaderRange.Style.Font.Bold = true;
                        wsReportNameHeaderRange.Style.Font.FontSize = 15;
                        wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                        wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        wsReportNameHeaderRange.Merge();
                        wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                        wsReportNameHeaderRange.Value = "LOP Report List";
                        worksheet.Range("A6:O6").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                        worksheet.Cell(2, 1).Style.Font.Bold = true;
                        worksheet.Cell(2, 1).Value = "Company Name";
                        worksheet.Cell(2, 2).Value = CompanyName.ToUpper();
                        worksheet.Cell(3, 1).Style.Font.Bold = true;
                        worksheet.Cell(3, 1).Value = "Login User";
                        worksheet.Cell(3, 2).Value = LoginUser;
                        worksheet.Cell(4, 1).Style.Font.Bold = true;
                        worksheet.Cell(4, 1).Value = "Date & Time";
                        worksheet.Cell(4, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(4, 2).Style.DateFormat.Format = "dd-MMM-yyyy   h:mm AM/PM";
                        worksheet.Cell(4, 2).Value = currDateTime.ToString();
                        worksheet.Cell(5, 1).Style.Font.Bold = true;
                        worksheet.Cell(5, 1).Value = "Total Count";
                        worksheet.Cell(5, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(5, 2).Value = model.LOPReportList.Count;

                        var currentRow = 6;
                        #region Header
                        worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 1).Value = "ID";
                        worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 2).Value = "Employee Name";
                        worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 3).Value = "Designation";
                        worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 4).Value = "Department";
                        worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 5).Value = "Location";
                        worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 6).Value = "LOPAbsent";
                        worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 7).Value = "Lop Absent Amount";
                        worksheet.Cell(currentRow, 8).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 8).Value = "ESI Leave";
                        worksheet.Cell(currentRow, 9).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 9).Value = "ESI Leave Amount";
                        worksheet.Cell(currentRow, 10).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 10).Value = "LJP";
                        worksheet.Cell(currentRow, 11).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 11).Value = "LJP Amount";
                        worksheet.Cell(currentRow, 12).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 12).Value = "LTP";
                        worksheet.Cell(currentRow, 13).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 13).Value = "LTP Amount";
                        worksheet.Cell(currentRow, 14).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 14).Value = "Previous LOP";
                        worksheet.Cell(currentRow, 15).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 15).Value = "Previous LOP Amount";
                        worksheet.Cell(currentRow, 16).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 16).Value = "Total Days To Cut";
                        //worksheet.Cell(currentRow, 17).Style.Font.Bold = true;
                        //worksheet.Cell(currentRow, 17).Value = "Total Days To Cut Amount";
                        worksheet.Cell(currentRow, 17).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 17).Value = "Suspension";
                        worksheet.Cell(currentRow, 18).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 18).Value = "Suspension Amount";
                        worksheet.Cell(currentRow, 19).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 19).Value = "Lay Off";
                        worksheet.Cell(currentRow, 20).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 20).Value = "Lay Off Amount";
                        worksheet.Cell(currentRow, 21).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 21).Value = "Less Hours";
                        worksheet.Cell(currentRow, 22).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 22).Value = "Less Hours Amount";
                        worksheet.Cell(currentRow, 23).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 23).Value = "Total Amount To Cut";



                        #endregion

                        #region Body
                        foreach (var item in model.LOPReportList)
                        {

                            currentRow++;
                            slno = slno + 1;
                            worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 1).Value = item.EmployeeID;
                            worksheet.Cell(currentRow, 2).Value = item.EmployeeName;
                            worksheet.Cell(currentRow, 3).Value = item.Designation;
                            worksheet.Cell(currentRow, 4).Value = item.Department;
                            worksheet.Cell(currentRow, 5).Value = item.Location;
                            worksheet.Cell(currentRow, 6).Value = item.LOPAbsent;
                            worksheet.Cell(currentRow, 7).Value = item.LOPAbsentAmount;
                            worksheet.Cell(currentRow, 8).Value = item.ESILeave;
                            worksheet.Cell(currentRow, 9).Value = item.ESILeaveAmount;
                            worksheet.Cell(currentRow, 10).Value = item.LJP;
                            worksheet.Cell(currentRow, 11).Value = item.LJPAmount;
                            worksheet.Cell(currentRow, 12).Value = item.LTP;
                            worksheet.Cell(currentRow, 13).Value = item.LTPAmount;
                            worksheet.Cell(currentRow, 14).Value = item.PrevLOP;
                            worksheet.Cell(currentRow, 15).Value = item.PreviosLOPAmount;
                            worksheet.Cell(currentRow, 16).Value = item.TotCutDays;
                            worksheet.Cell(currentRow, 17).Value = item.Suspension;
                            worksheet.Cell(currentRow, 18).Value = item.SuspensionAmount;
                            worksheet.Cell(currentRow, 19).Value = item.Layoff;
                            worksheet.Cell(currentRow, 20).Value = item.LayOffAmount;
                            worksheet.Cell(currentRow, 21).Value = item.Latehrs;
                            worksheet.Cell(currentRow, 22).Value = item.LessHoursAmount;
                            worksheet.Cell(currentRow, 23).Value = item.TotCutAmt;
                            worksheet.Columns(1, 5).Style.Alignment.WrapText = true;

                        }
                        #endregion
                        worksheet.Columns().AdjustToContents();
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
                                "LOP Report List.xlsx"
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
    }
    }
