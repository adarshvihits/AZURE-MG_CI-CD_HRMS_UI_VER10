using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.DataBank.Reports;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.Account;
using Hrms.Lite.Shared.DataBank.Reports;
using Hrms.Lite.Shared.General;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Spire.Xls;

namespace Hrms.Lite.UI.Areas.DataBank.Reports.Controllers
{
    [Area("DataBank")]
    [SessionAuthorize]
    public class SeparationReportController : Controller
    {
        private readonly ISeparationReportService _SeparationReportService;
        private readonly ICommonService _commonService;
        private readonly IDropdownService _dropdownService;
        private readonly IHelpers _helpers;

        public static class Global
        {
            public static Filter GlobalFilter;
            public static DateTime FromDate;
            public static DateTime ToDate;
            public static bool IsRefreshed = true;

        }
        public SeparationReportController(ISeparationReportService _separationReportService, ICommonService commonService, IDropdownService dropdownService, IHelpers Helpers)
        {
            _SeparationReportService = _separationReportService;
            _commonService = commonService;
            _dropdownService = dropdownService;
            _helpers = Helpers;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                SeparationReport model = new SeparationReport();
                if (!Global.IsRefreshed)
                {
                    Global.IsRefreshed = true;
                    if (Global.FromDate != DateTime.Parse("0001-01-01 12:00:00 AM") && Global.ToDate != DateTime.Parse("0001-01-01 12:00:00 AM"))
                    {
                        if (Global.GlobalFilter == null)
                            Global.GlobalFilter = new Filter();
                        //model = await _SeparationReportService.GetSeparationReport(Global.FromDate.ToString("yyyy-MMM-dd"), Global.ToDate.ToString("yyyy-MMM-dd"), Global.GlobalFilter);
                        model.From = Global.FromDate;
                        model.To = Global.ToDate;
                    }
                }
                else
                {
                    Global.FromDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                    Global.ToDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1);
                    model.From = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                    model.To = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1);
                }
                ViewBag.GrpBy = new SelectList(await _dropdownService.GetJoiningAndSeprationgionGrouping(), "Code", "Name");
                model.GroupByName = "Designation";
                model.Filter = await _commonService.GetFilterDropdowns();


                return View("~/Areas/DataBank/Reports/Views/SeparationReport/Index.cshtml", model);
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
        public  IActionResult SeparationReportList() //For employee profile breadcrumb
        {
            Global.IsRefreshed = false;
            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> GetSeparationList(Filter filter1)
        {
            try
            {
                SeparationReport model = new SeparationReport();
                SeparationReport m = new SeparationReport();


                Filter filter = new Filter();
                Global.GlobalFilter = filter1;
                Global.FromDate = filter1.F_From;
                Global.ToDate = filter1.F_To;

                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }

                    model = await _SeparationReportService.GetSeparationReport(filter1.F_From.ToString("yyyy-MMM-dd"), filter1.F_To.ToString("yyyy-MMM-dd"), filter1);

                m.SeparationReportList = model.SeparationReportList;
                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.SeparationReportList == null || model.SeparationReportList.Count() == 0)
                {
                   

                    return Json(new
                    {
                        data = "",
                        draw = filter1.draw,
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows
                    });
                }

                if (model.SeparationReportList.Count > 0)
                {
                    totalrows = m.SeparationReportList[0].active;

                   
                }

            
                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = m.SeparationReportList[0].totalRecords;
                    
                    return Json(new
                    {
                        data = model.SeparationReportList,
                        draw = filter1.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows//totalrowsafterfiltering
                    });
                }
                else
                {
                 
                    return Json(new
                    {
                        data = model.SeparationReportList,
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
       
        public async Task<IActionResult> GroupSummary(int GrpByCode,string GrpByName)
        {
            try { 
            SeparationReport model = new SeparationReport();
            model = await _SeparationReportService.GetSeparationGroupSummary(GrpByCode,Global.FromDate.ToString("yyyy-MMM-dd"), Global.ToDate.ToString("yyyy-MMM-dd"));
            model.GroupByName = GrpByName;
            return PartialView("~/Areas/DataBank/Reports/Views/SeparationReport/_GroupSummaryList.cshtml", model);
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

        public async Task<IActionResult> ExportToExcel(DateTime fromDate, DateTime toDate,int totalRecords )
        {
            try { 
          
            if (Global.GlobalFilter == null)
                Global.GlobalFilter = new Filter();
                else
                {
                    Global.GlobalFilter.length = totalRecords;
                    Global.GlobalFilter.start = 0;
                }
                //var GroupByLIst = await _dropdownService.GetEmployeeProfileSummaryGroupByDropDown();
                //ViewBag.groupBy = new SelectList(GroupByLIst, "Code", "Name");
                //int firstgroupById = GroupByLIst[0].Code;
                
            string FilterValues = await _helpers.GetFilterValues(Global.GlobalFilter);
          
            var LoginUser = HttpContext.Session.GetString("LoginUser");
            var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Separation Report (" + fromDate + " to " + fromDate + ")", "Excel");
               
            if (FilterValues == "")
            {
                FilterValues = "All";
            }
            string currDateTime = await _helpers.GetCurrentDate();
            
                
                //Get Dataset
                var dataset = await _SeparationReportService.GetSeparationReport_Excel(fromDate.ToString("yyyy-MMM-dd"), toDate.ToString("yyyy-MMM-dd"), Global.GlobalFilter);
                DataTable dt = dataset.Tables[0];
                dataset.Tables[0].Rows.RemoveAt(0);
                if ((dataset.Tables[0]?.Rows?.Count ?? 0) > 0)
                {
                    dt = dataset.Tables[0];
                }

                //set EXCEL Column Heading 
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (dt.Columns[i].ColumnName == "doj" || dt.Columns[i].ColumnName == "lwd" || dt.Columns[i].ColumnName == "y/s")
                        dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.ToUpper();
                    else
                        dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
                }


                using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Separation Report");
                int count = dt.Rows.Count+ 5;


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
                wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191); ;
                wsReportNameHeaderRange.Value = "Separation Report";

                worksheet.Cell(2, 1).Style.Font.Bold = true;
                worksheet.Cell(2, 1).Value = "Group Company Name";
                worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                worksheet.Cell(2, 3).Style.Font.Bold = true;
              
                worksheet.Cell(2, 3).Value = "From";
                worksheet.Cell(2, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                worksheet.Cell(2, 4).Value = fromDate;
                worksheet.Cell(2, 4).Style.DateFormat.Format = "dd-MMM-yyyy";
    
                worksheet.Cell(2, 5).Style.Font.Bold = true;
                worksheet.Cell(2, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                worksheet.Cell(2, 5).Value = "To";
                worksheet.Cell(2, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                worksheet.Cell(2, 6).Value = toDate;
                worksheet.Cell(2, 6).Style.DateFormat.Format = "dd-MMM-yyyy";
    
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
                worksheet.Cell(3, 6).Value = dt.Rows.Count;
                worksheet.Cell(4, 1).Style.Font.Bold = true;
                worksheet.Cell(4, 1).Value = "Filter Applied";
                worksheet.Range("B4:R4").Merge();
                worksheet.Cell("B4").Value = FilterValues;



                var currentRow = 5;


                    worksheet.Cell(currentRow, 1).InsertTable(dt);
                    worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                    worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                    worksheet.Range("A5:R5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Range("A5:R5").Style.Font.Bold = true;

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
                        "Separation Report List.xlsx"
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
        public async Task<IActionResult> GroupSummaryExportToExcel(int id, DateTime fromDate, DateTime toDate, string type)
        {
            try {
                SeparationReport model = new SeparationReport();
                model = await _SeparationReportService.GetSeparationGroupSummary(id, fromDate.ToString("yyyy-MMM-dd"), toDate.ToString("yyyy-MMM-dd"));
                var TotalCount = model.EmployeeProfileSummary.Sum(x => x.Count);
                var TotalCTC = model.EmployeeProfileSummary.Sum(x => x.CTC);
                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Separation Report Summary(" + type + ")", "Excel");
                int slNo = 0;

                string currDateTime = await _helpers.GetCurrentDate();
                
                if(User.authUser.SalaryVisibility != 0)
            {

                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("Separation Report");
                        int count = model.EmployeeProfileSummary.Count + 5;


                        worksheet.Range("A1:F" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:F" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:F" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:F" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:F" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 5)));
                        wsReportNameHeaderRange.Style.Font.Bold = true;
                        wsReportNameHeaderRange.Style.Font.FontSize = 15;
                        wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                        wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        wsReportNameHeaderRange.Merge();
                        wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191); ;
                        wsReportNameHeaderRange.Value = "Separation Report";

                        worksheet.Cell(2, 1).Style.Font.Bold = true;
                        worksheet.Cell(2, 1).Value = "Group Company Name";
                        worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                        worksheet.Cell(2, 3).Style.Font.Bold = true;
                        worksheet.Cell(2, 3).Value = "From";
                        worksheet.Cell(2, 4).Value = fromDate;
                        worksheet.Cell(2, 4).Style.DateFormat.Format = "dd-MMM-yyyy";

                        worksheet.Cell(2, 5).Style.Font.Bold = true;
                        worksheet.Cell(2, 5).Value = "To";
                        worksheet.Cell(2, 6).Value = toDate;
                        worksheet.Cell(2, 6).Style.DateFormat.Format = "dd-MMM-yyyy";

                        worksheet.Cell(3, 1).Style.Font.Bold = true;
                        worksheet.Cell(3, 1).Value = "Group By";
                        worksheet.Cell(3, 2).Value = type;

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
                        worksheet.Cell(4, 6).Value = model.EmployeeProfileSummary.Count;




                        var currentRow = 5;
                        #region Header
                        worksheet.Range("A5:F5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                        worksheet.Cell(currentRow, 1).Value = "Sl.No";
                        worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 2).Value = "Group By";
                        worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 3).Value = "Count";
                        worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 4).Value = " % ";
                        worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 5).Value = "CTC";
                        worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 6).Value = "%";
                        worksheet.Cell(currentRow, 6).Style.Font.Bold = true;



                        #endregion

                        #region Body
                        foreach (var item in model.EmployeeProfileSummary)
                        {
                            currentRow++;
                            slNo = slNo + 1;
                            worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 1).Value = slNo;
                            worksheet.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 2).Value = item.Name;
                            worksheet.Cell(currentRow, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 3).Value = item.Count;
                            worksheet.Cell(currentRow, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 4).Value = item.Count_Perc;
                            worksheet.Cell(currentRow, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 5).Value = item.CTC;
                            worksheet.Cell(currentRow, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 6).Value = item.CTC_Perc;




                            worksheet.Columns(1, 6).Style.Alignment.WrapText = true;
                            worksheet.Rows().Style.Alignment.WrapText = true;
                        }
                        #endregion
                        var wsReportNameFooterRange = worksheet.Range(string.Format("A{0}:{1}{0}", currentRow + 1, Char.ConvertFromUtf32(65 + 5)));
                        wsReportNameFooterRange.Style.Font.Bold = true;
                        wsReportNameFooterRange.Style.Font.FontSize = 15;
                        wsReportNameFooterRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                        wsReportNameFooterRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        wsReportNameFooterRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191); ;
                        worksheet.Cell(currentRow + 1, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow + 1, 3).Value = TotalCount;
                        worksheet.Cell(currentRow + 1, 5).Value = TotalCTC;
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
                                "Separation Report Group summary List.xlsx"
                                );
                        }
                    }
                } 
            else
                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("Separation Report");
                        int count = model.EmployeeProfileSummary.Count + 5;


                        worksheet.Range("A1:F" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:F" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:F" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:F" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:F" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 5)));
                        wsReportNameHeaderRange.Style.Font.Bold = true;
                        wsReportNameHeaderRange.Style.Font.FontSize = 15;
                        wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                        wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        wsReportNameHeaderRange.Merge();
                        wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191); ;
                        wsReportNameHeaderRange.Value = "Separation Report";

                        worksheet.Cell(2, 1).Style.Font.Bold = true;
                        worksheet.Cell(2, 1).Value = "Group Company Name";
                        worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                        worksheet.Cell(2, 3).Style.Font.Bold = true;
                        worksheet.Cell(2, 3).Value = "From";
                        worksheet.Cell(2, 4).Value = fromDate;
                        worksheet.Cell(2, 4).Style.DateFormat.Format = "dd-MMM-yyyy";

                        worksheet.Cell(2, 5).Style.Font.Bold = true;
                        worksheet.Cell(2, 5).Value = "To";
                        worksheet.Cell(2, 6).Value = toDate;
                        worksheet.Cell(2, 6).Style.DateFormat.Format = "dd-MMM-yyyy";

                        worksheet.Cell(3, 1).Style.Font.Bold = true;
                        worksheet.Cell(3, 1).Value = "Group By";
                        worksheet.Cell(3, 2).Value = type;

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
                        worksheet.Cell(4, 6).Value = model.EmployeeProfileSummary.Count;




                        var currentRow = 5;
                        #region Header
                        worksheet.Range("A5:F5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                        worksheet.Cell(currentRow, 1).Value = "Sl.No";
                        worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 2).Value = "Group By";
                        worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 3).Value = "Count";
                        worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 4).Value = " % ";
                        worksheet.Cell(currentRow, 4).Style.Font.Bold = true;



                        #endregion

                        #region Body
                        foreach (var item in model.EmployeeProfileSummary)
                        {
                            currentRow++;
                            slNo = slNo + 1;
                            worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 1).Value = slNo;
                            worksheet.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 2).Value = item.Name;
                            worksheet.Cell(currentRow, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 3).Value = item.Count;
                            worksheet.Cell(currentRow, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 4).Value = item.Count_Perc;
                      



                            worksheet.Columns(1, 6).Style.Alignment.WrapText = true;
                            worksheet.Rows().Style.Alignment.WrapText = true;
                        }
                        #endregion
                        var wsReportNameFooterRange = worksheet.Range(string.Format("A{0}:{1}{0}", currentRow + 1, Char.ConvertFromUtf32(65 + 5)));
                        wsReportNameFooterRange.Style.Font.Bold = true;
                        wsReportNameFooterRange.Style.Font.FontSize = 15;
                        wsReportNameFooterRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                        wsReportNameFooterRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        wsReportNameFooterRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191); ;
                        worksheet.Cell(currentRow + 1, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow + 1, 3).Value = TotalCount;
                    
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
                                "Separation Report Group summary List.xlsx"
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
