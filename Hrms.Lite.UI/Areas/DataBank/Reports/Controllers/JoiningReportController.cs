using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.DataBank.Reports;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.Account;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.DataBank.Reports;
using Hrms.Lite.Shared.General;
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

namespace Hrms.Lite.UI.Areas.DataBank.Reports.Controllers
{
    [Area("DataBank")]
    [SessionAuthorize]

    public class JoiningReportController : Controller
    {
        
        private readonly IJoiningReportService _JoiningReportService;
        private readonly ICommonService _commonService;
        private readonly IDropdownService _dropdownService;
        private readonly IHelpers _helpers;
        public static class Global
        {
            public static Filter GlobalFilter;
            public static DateTime fromDate;
            public static DateTime toDate;
            public static bool IsRefreshed = true;
        }
        public JoiningReportController(IJoiningReportService JoiningReportService,ICommonService commonService, IDropdownService dropdownService, IHelpers Helpers)
        {
            _JoiningReportService = JoiningReportService; 
            _commonService = commonService;
            _dropdownService = dropdownService;
            _helpers = Helpers;
        }

        public async Task<IActionResult> Index()
        {
            try { 
            JoiningReport model = new JoiningReport();
                if (!Global.IsRefreshed)
                {
                    Global.IsRefreshed = true;
                    if (Global.fromDate != DateTime.Parse("0001-01-01 12:00:00 AM") && Global.toDate != DateTime.Parse("0001-01-01 12:00:00 AM"))
                    {
                        if (Global.GlobalFilter == null)
                            Global.GlobalFilter = new Filter();
                        string lastdayofmonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1).ToString("yyyy-MMM-dd");
                        string firstdate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("yyyy-MMM-dd");
                        //model = await _JoiningReportService.GetJoiningReport(firstdate, lastdayofmonth, Global.GlobalFilter);
                        model.From = Global.fromDate;
                        model.To = Global.toDate;
                    }
                }
                else
                {
                    Global.fromDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                    Global.toDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1);
                    model.From = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                    model.To = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1);
                }
            
                //await _commonService.GetFilterDropdowns();
            ViewBag.groupBy = new SelectList(await _dropdownService.GetJoiningAndSeprationgionGrouping(), "Code", "Name");
            model.groupbyname = "Designation";
            model.SliderFilter = new Filter();
                model.SliderFilter = await _commonService.GetFilterDropdowns();                
            return View("~/Areas/DataBank/Reports/Views/JoiningReport/Index.cshtml",model);
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
        public async Task<IActionResult> JoiningReportList() //For employee profile breadcrumb
        {
            Global.IsRefreshed = false;
            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> GetList(Filter filter1)
        {
            try
            {
                JoiningReport model = new JoiningReport();
                JoiningReport m = new JoiningReport();
                //model.EmployeeHeader = new EmployeeHeader();




                Filter filter = new Filter();
                Global.GlobalFilter = filter1;
                Global.fromDate = filter1.F_From;
                Global.toDate = filter1.F_To;

                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }

                  model = await _JoiningReportService.GetJoiningReport(Global.fromDate.ToString("yyyy-MMM-dd"), Global.toDate.ToString("yyyy-MMM-dd"), filter1);
                
                m.JoiningReportList = model.JoiningReportList;
                int totalrows = 0;
                int totalrowsafterfiltering = 0;
              
                if (model.JoiningReportList == null || model.JoiningReportList.Count() == 0)
                {
                    //model.RequestVsAuthorityList = new List<RequestVsAuthorityList>();
                    //model.RequestVsAuthorityList.Add(new RequestVsAuthorityList());


                    return Json(new
                    {
                        data = "",
                        draw = filter1.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows
                    });
                }




                if (model.JoiningReportList.Count > 0)
                {
                    totalrows = m.JoiningReportList[0].active;

                    //m.OnBoardingList.ToList<Hrms.Lite.Shared.Master.Test>().Count; ;
                }

                //model.AllTabCount = m.AllTabCount;
                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = m.JoiningReportList[0].totalRecords;
                    return Json(new
                    {
                        data = model.JoiningReportList,
                        draw = filter1.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows//totalrowsafterfiltering
                    });
                }
                else
                {
                    // return Json(new { data = model.EmployeeProfileList, draw = filter1.draw, recordsTotal = totalrows, recordsFiltered = totalrows });
                    return Json(new
                    {
                        data = model.JoiningReportList,
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







      

        public async Task<IActionResult> GetGroupSummary(DateTime fromDate, DateTime toDate)
        {
            try { 
            JoiningReport model = new JoiningReport();
            var GroupByLIst = await _dropdownService.GetJoiningAndSeprationgionGrouping();
            ViewBag.groupBy = new SelectList(GroupByLIst, "Code", "Name");
            int firstgroupById = GroupByLIst[0].Code;
            model = await _JoiningReportService.GetGroupSummaryDetails(firstgroupById, fromDate.ToString("yyyy-MMM-dd"), toDate.ToString("yyyy-MMM-dd"), Global.GlobalFilter);
            ViewBag.TotalCount = model.EmployeeProfileSummary.Sum(x => x.Count);
            ViewBag.TotalCTC = model.EmployeeProfileSummary.Sum(x => x.CTC);
           model.groupbyname = GroupByLIst[0].Name;
            return PartialView("~/Areas/DataBank/Reports/Views/JoiningReport/_GroupSummary.cshtml", model);
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
        public async Task<IActionResult> GroupByBasedSummary(int id, string groupbyname, DateTime fromDate, DateTime toDate)
        {
            try { 
        JoiningReport model = new JoiningReport();

            if (Global.GlobalFilter == null)
                Global.GlobalFilter = new Filter();
           
            model = await _JoiningReportService.GetGroupSummaryDetails(id, fromDate.ToString("yyyy-MMM-dd"), toDate.ToString("yyyy-MMM-dd"), Global.GlobalFilter);
            model.groupbyname = groupbyname;
           
            return PartialView("~/Areas/DataBank/Reports/Views/JoiningReport/_SummaryList.cshtml", model);
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


        public async Task<IActionResult> ExportToExcelDataset(DateTime fromDate, DateTime toDate, int totRecords)
        {
            try
            {

                if (Global.GlobalFilter == null)
                    Global.GlobalFilter = new Filter();
                else
                {
                    Global.GlobalFilter.length = totRecords;
                    Global.GlobalFilter.start = 0;
                }
                string FilterValues = await _helpers.GetFilterValues(Global.GlobalFilter);
                var GroupByLIst = await _dropdownService.GetEmployeeProfileSummaryGroupByDropDown();
                ViewBag.groupBy = new SelectList(GroupByLIst, "Code", "Name");
                int firstgroupById = GroupByLIst[0].Code;
               
                
                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Joining Report (" + fromDate.ToShortDateString() + " to " + toDate.ToShortDateString() + ")", "Excel");
             


                if (FilterValues == "")
                {
                    FilterValues = "All";
                }
                string currDateTime = await _helpers.GetCurrentDate();
                

                var dataset = await _JoiningReportService.GetExcelJoiningReport(fromDate.ToString("yyyy-MMM-dd"), toDate.ToString("yyyy-MMM-dd"), Global.GlobalFilter);               
                DataTable dt = dataset.Tables[0];
                dataset.Tables[0].Rows.RemoveAt(0);
                if ((dataset.Tables[0]?.Rows?.Count ?? 0) > 0)
                {
                    dt = dataset.Tables[0];
                }

                //set EXCEL Column Heading Format
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (dt.Columns[i].ColumnName == "doj" || dt.Columns[i].ColumnName == "dob")
                        dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.ToUpper();
                    else
                    dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);                  
                }              

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Joining Report");                   
                    int count = dt.Rows.Count + 5;              

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
                    wsReportNameHeaderRange.Value = "Joining Report";

                    worksheet.Cell(2, 1).Style.Font.Bold = true;
                    worksheet.Cell(2, 1).Value = "Group Company Name";
                    worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                    worksheet.Cell(2, 3).Style.Font.Bold = true;
                    worksheet.Cell(2, 3).Value = "From";
                    worksheet.Cell(2, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(2, 4).Value = fromDate;
                    worksheet.Cell(2, 4).Style.DateFormat.Format = "dd-MMM-yyyy";
                    //worksheet.Cell(2, 4).Value = currDateTime.ToString();
                    worksheet.Cell(2, 5).Style.Font.Bold = true;
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
                    worksheet.Range("B4:K4").Merge();
                    worksheet.Cell("B4").Value = FilterValues;



                    var currentRow = 5;

                    worksheet.Cell(currentRow, 1).InsertTable(dt);
                               
                               
                    worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                    worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                    worksheet.Range("A5:K5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Range("A5:K5").Style.Font.Bold = true;
         
            
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
                            "Joining Report List.xlsx"
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




        public async Task<IActionResult> GroupSummaryExportToExcel(int id,DateTime fromDate, DateTime toDate,string type)
        {
            try { 
            JoiningReport model = new JoiningReport();
            model = await _JoiningReportService.GetGroupSummaryDetails(id, fromDate.ToString("yyyy-MMM-dd"), toDate.ToString("yyyy-MMM-dd"), Global.GlobalFilter);
            
            var LoginUser = HttpContext.Session.GetString("LoginUser");
            var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
            EP = await _commonService.GetExcelPassword("Joining Report Summary (" + type + ")", "Excel");
            int slNo = 0;
            
            string currDateTime = await _helpers.GetCurrentDate();
            
                if (User.authUser.SalaryVisibility != 0)
                {
                    using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Joining Report");
                int count = model.EmployeeProfileSummary.Count + 6;


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
                wsReportNameHeaderRange.Value = "Joining Report";

                worksheet.Cell(2, 1).Style.Font.Bold = true;
                worksheet.Cell(2, 1).Value = "Group Company Name";
                worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                worksheet.Cell(2, 3).Style.Font.Bold = true;
                worksheet.Cell(2, 3).Value = "From";
                    worksheet.Cell(2, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);

                    worksheet.Cell(2, 4).Value = fromDate;
                worksheet.Cell(2, 4).Style.DateFormat.Format = "dd-MMM-yyyy";
                
                worksheet.Cell(2, 5).Style.Font.Bold = true;
                worksheet.Cell(2, 5).Value = "To";
                worksheet.Cell(2, 6).Value = toDate;
                    worksheet.Cell(2, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);

                    worksheet.Cell(2, 6).Style.DateFormat.Format = "dd-MMM-yyyy";
             
                worksheet.Cell(3, 1).Style.Font.Bold = true;
                worksheet.Cell(3, 1).Value = "Group By";
                worksheet.Cell(3, 2).Value = type;
                
                worksheet.Cell(4, 1).Style.Font.Bold = true;
                    worksheet.Cell(4, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);

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

                    }
                        decimal TotalCount = 0;
                        decimal TotalCTC = 0;
                        int rowcount = model.EmployeeProfileSummary.Count + 6;
                            if (model.EmployeeProfileSummary != null && model.EmployeeProfileSummary.Count > 0)
                        {

                            for (int i = 0; i < model.EmployeeProfileSummary.Count; i++)
                            {
                                currentRow++;
                                TotalCount += model.EmployeeProfileSummary[i].Count;
                                TotalCTC += model.EmployeeProfileSummary[i].CTC;



                            }
                        worksheet.Cell(rowcount, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(rowcount, 3).Value = TotalCount;
                        worksheet.Cell(rowcount, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(rowcount, 5).Value = TotalCTC;
                        worksheet.Columns(1, 6).Style.Alignment.WrapText = true;
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
                        book.Protect(EP.Password,false,false);
                        book.SaveToStream(stream);
                    }
                    var content = stream.ToArray();
                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "Joining Report Group summary List.xlsx"
                        );
                }
            }
                }
                else
                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("Joining Report");
                        int count = model.EmployeeProfileSummary.Count + 6;


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
                        wsReportNameHeaderRange.Value = "Joining Report";

                        worksheet.Cell(2, 1).Style.Font.Bold = true;
                        worksheet.Cell(2, 1).Value = "Group Company Name";
                        worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                        worksheet.Cell(2, 3).Style.Font.Bold = true;
                        worksheet.Cell(2, 3).Value = "From";
                        worksheet.Cell(2, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);

                        worksheet.Cell(2, 4).Value = fromDate;
                        worksheet.Cell(2, 4).Style.DateFormat.Format = "dd-MMM-yyyy";

                        worksheet.Cell(2, 5).Style.Font.Bold = true;
                        worksheet.Cell(2, 5).Value = "To";
                        worksheet.Cell(2, 6).Value = toDate;
                        worksheet.Cell(2, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);

                        worksheet.Cell(2, 6).Style.DateFormat.Format = "dd-MMM-yyyy";

                        worksheet.Cell(3, 1).Style.Font.Bold = true;
                        worksheet.Cell(3, 1).Value = "Group By";
                        worksheet.Cell(3, 2).Value = type;

                        worksheet.Cell(4, 1).Style.Font.Bold = true;
                        worksheet.Cell(4, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);

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


                        }
                        decimal TotalCount = 0;
                        decimal TotalCTC = 0;
                        int rowcount = model.EmployeeProfileSummary.Count + 6;
                        if (model.EmployeeProfileSummary != null && model.EmployeeProfileSummary.Count > 0)
                        {

                            for (int i = 0; i < model.EmployeeProfileSummary.Count; i++)
                            {
                                currentRow++;
                                TotalCount += model.EmployeeProfileSummary[i].Count;
                                TotalCTC += model.EmployeeProfileSummary[i].CTC;



                            }
                            worksheet.Cell(rowcount, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(rowcount, 3).Value = TotalCount;
                           
                            worksheet.Columns(1, 6).Style.Alignment.WrapText = true;
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
                                "Joining Report Group summary List.xlsx"
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
