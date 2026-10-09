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
    public class ResignationTrackingReportController : Controller
    {
        private readonly IResignationTrackerReportService _ResignationTrackerReportService;
        private readonly IHelpers _helpers;
        private readonly ICommonService _commonService;
        private readonly IDropdownService _dropdownService;

        public static class Global
        {
            public static Filter GlobalFilter;
            
        }
        public ResignationTrackingReportController(IDropdownService dropdownService, IResignationTrackerReportService ResignationTrackerReportService,IHelpers Helpers, ICommonService commonService)
        {
            _dropdownService = dropdownService;
            _ResignationTrackerReportService = ResignationTrackerReportService;    
            _helpers = Helpers;
            _commonService = commonService;

        }
        public async Task<IActionResult> Index()
        {
            try { 
            ResignationTracker model = new ResignationTracker();
            model.Filter = new Filter();
            Global.GlobalFilter = new Filter();

            model.FromDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-4);
            model.ToDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            Global.GlobalFilter.F_From = model.FromDate;
            Global.GlobalFilter.F_To = model.ToDate;

            model = await _ResignationTrackerReportService.GetResignationTrackingList(Global.GlobalFilter,"All");

            model.FromDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-4);
            model.ToDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            //model.ToDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddMonths(1).AddDays(-1);
            model.Filter = await _commonService.GetFilterDropdowns();
            ViewBag.ResignationStatus = new SelectList(await _dropdownService.GetResignationStatusDropdown(), "Code", "Name");
            return View("~/Areas/DataBank/Reports/Views/ResignationTrackingReport/Index.cshtml",model);
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


        public async Task<IActionResult>GetList(DateTime fromDate,DateTime toDate,string status)
        {
            ResignationTracker model = new ResignationTracker();
            model.Filter = new Filter();
            Global.GlobalFilter = new Filter();
            Global.GlobalFilter.F_From = fromDate;
            Global.GlobalFilter.F_To = toDate;
            model = await _ResignationTrackerReportService.GetResignationTrackingList(Global.GlobalFilter, status);
            return PartialView("~/Areas/DataBank/Reports/Views/ResignationTrackingReport/_List.cshtml", model);

        }


       
        public async Task<IActionResult> ApplyFilter(Filter filter,string status)
        {
            try
            {
                ResignationTracker model = new ResignationTracker();
                Global.GlobalFilter = filter;
           
                model = await _ResignationTrackerReportService.GetResignationTrackingList(Global.GlobalFilter, status);
                return PartialView("~/Areas/DataBank/Reports/Views/ResignationTrackingReport/_List.cshtml", model);
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
        public async Task<IActionResult> GetResignationTracker(Guid ResignationGI,Guid EmployeeGI, string status)
        {
            ResignationTracker model = new ResignationTracker();
           
            model.EmployeeHeader = new EmployeeHeader();
            
            if (status== "Resignation Withdrawal") {
               
                model = (await _ResignationTrackerReportService.GetWithdrawTrackerDetails(ResignationGI, EmployeeGI));
                model.EmployeeHeader = await _commonService.GetEmployeeHeader(EmployeeGI);
                //model = await _ResignationTrackerReportService.GetResignationTracker(ResignationGI);
                return PartialView("~/Areas/DataBank/Reports/Views/ResignationTrackingReport/_WithdrawTracker.cshtml", model);   
            }
            else {
                
                model = await _ResignationTrackerReportService.GetTrackerDetails(ResignationGI, EmployeeGI);
                model.EmployeeHeader = await _commonService.GetEmployeeHeader(EmployeeGI);
                return PartialView("~/Areas/DataBank/Reports/Views/ResignationTrackingReport/_Tracker.cshtml", model);
            }
            

            
        }

        public async Task<IActionResult> ExportToExcel(string status,string FromDte,string ToDte)
        {
            try
            {
               
                if (Global.GlobalFilter == null)
                    Global.GlobalFilter = new Filter();
              
                string FilterValues = await _helpers.GetFilterValues(Global.GlobalFilter);

                
                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Resignation Tracker Report ", "Excel");
               
                if (FilterValues == "")
                {
                    FilterValues = "All";
                }
                string currDateTime = await _helpers.GetCurrentDate();
                

                //Get Dataset
                var datset = await _ResignationTrackerReportService.GetResignationTrackingList_Excel(Global.GlobalFilter, status);
                DataTable dt = datset.Tables[0];
                datset.Tables[0].Rows.RemoveAt(0);
                if ((datset.Tables[0]?.Rows?.Count ?? 0) > 0)
                {
                    dt = datset.Tables[0];
                }

                //set EXCEL Column Heading 
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (dt.Columns[i].ColumnName == "doj" || dt.Columns[i].ColumnName == "dob" || dt.Columns[i].ColumnName == "lwd")
                        dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.ToUpper();
                    else
                        dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
                }




                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Resignation Tracker Report");
                
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
                    wsReportNameHeaderRange.Value = "Resignation Tracker Report";

                    worksheet.Cell(2, 1).Style.Font.Bold = true;
                    worksheet.Cell(2, 1).Value = "Group Company Name";
                    worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                 
                    worksheet.Cell(2, 3).Style.Font.Bold = true;
                    worksheet.Cell(2, 3).Value = "Login User";
                    worksheet.Cell(2, 4).Value = LoginUser;
                    worksheet.Cell(2, 5).Style.Font.Bold = true;
                    worksheet.Cell(2, 5).Value = "Date & Time";
                    worksheet.Cell(2, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(2, 6).Style.DateFormat.Format = "dd-MMM-yyyy   h:mm AM/PM";
                    worksheet.Cell(2, 6).Value = currDateTime.ToString();
                    worksheet.Cell(2, 7).Style.Font.Bold = true;
                    worksheet.Cell(2, 7).Value = "Total Count";
                    worksheet.Cell(2, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(2, 8).Value = dt.Rows.Count;

                    worksheet.Cell(3, 1).Style.Font.Bold = true;
                    worksheet.Cell(3, 1).Value = "From Date";
                    worksheet.Cell(3, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(3, 2).Style.DateFormat.Format = "dd-MMM-yyyy";
                    worksheet.Cell(3, 2).Value = FromDte.ToString();

                    worksheet.Cell(3, 3).Style.Font.Bold = true;
                    worksheet.Cell(3, 3).Value = "To Date";
                    worksheet.Cell(3, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(3, 4).Style.DateFormat.Format = "dd-MMM-yyyy";
                    worksheet.Cell(3, 4).Value = ToDte.ToString();

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
                            "Resignation Tracker Report List.xlsx"
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
