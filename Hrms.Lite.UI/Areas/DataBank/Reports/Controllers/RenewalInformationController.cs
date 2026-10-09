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
    public class RenewalInformationController : Controller
    {
        private readonly IRenewalInformationService _renewalInformationService;
        private readonly ICommonService _commonService;
        private readonly IDropdownService _dropdownService;
        private readonly IHelpers _helpers;

        
        public RenewalInformationController(IRenewalInformationService RenewalInformationService, ICommonService commonService, IDropdownService dropdownService, IHelpers Helpers)
        {
            _renewalInformationService = RenewalInformationService;
            _commonService = commonService;
            _dropdownService = dropdownService;
            _helpers = Helpers;
        }
        public async  Task<IActionResult> Index()
        {
            try {
                DateTime now = DateTime.Now;
                var startDate = new DateTime(now.Year, now.Month, 1);
                var endDate = startDate.AddMonths(1).AddDays(-1);
                RenewalInformation model = new RenewalInformation();
                Filter filter = new Filter();
                //model = await _renewalInformationService.RenewalInformationList(0, startDate.ToString("yyyy-MMM-dd"), endDate.ToString("yyyy-MMM-dd"), filter);
                model.From = startDate;
                model.To = endDate;
            ViewBag.Rit= new SelectList(await _dropdownService.GetRenewalInformationDropdown(), "Code", "Name");
                model.Filter = await _commonService.GetFilterDropdowns();
                return View("~/Areas/DataBank/Reports/Views/RenewalInformation/Index.cshtml",model);
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
        public async Task<IActionResult> GetList(Filter filter1, int type)
        {
            try
            {
                RenewalInformation model = new RenewalInformation();
                RenewalInformation m = new RenewalInformation();
                //model.EmployeeHeader = new EmployeeHeader();




                Filter filter = new Filter();
                model.Filter = filter1;


                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }
                model = await _renewalInformationService.RenewalInformationList(type, filter1.F_From.ToString("yyyy-MMM-dd"), filter1.F_To.ToString("yyyy-MMM-dd"), filter1);



                m.RenewalInformationList = model.RenewalInformationList;
                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.RenewalInformationList == null)
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




                if (model.RenewalInformationList.Count > 0)
                {
                    totalrows = m.RenewalInformationList[0].active;

                    //m.OnBoardingList.ToList<Hrms.Lite.Shared.Master.Test>().Count; ;
                }

                //model.AllTabCount = m.AllTabCount;
                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = m.RenewalInformationList[0].totalRecords;
                    return Json(new
                    {
                        data = model.RenewalInformationList,
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
                        data = model.RenewalInformationList,
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
        public async Task<IActionResult> GetSummary(DateTime fromDate, DateTime toDate)
        {
            try { 
            RenewalInformation model = new RenewalInformation();
            model = await _renewalInformationService.RenewalInformationReport(fromDate.ToString("yyyy-MMM-dd"), toDate.ToString("yyyy-MMM-dd"));
            ViewBag.TotalCount = model.EmployeeProfileSummary.Sum(x => x.Count);
            ViewBag.TotalCTC = model.EmployeeProfileSummary.Sum(x => x.CTC);
            return PartialView("~/Areas/DataBank/Reports/Views/RenewalInformation/_Summarylist.cshtml", model);
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
        public async Task<IActionResult> RenewalInformationList(int type, string fromDate, string toDate)
        {
            try { 
            RenewalInformation model = new RenewalInformation();
            Filter filter = new Filter();
            //Global.FromDate = fromDate;
            //Global.ToDate = toDate;
            //Global.type = type;
            model = await _renewalInformationService.RenewalInformationList(type,fromDate,toDate,filter);
            return PartialView("~/Areas/DataBank/Reports/Views/RenewalInformation/_RenewalInfoList.cshtml", model);
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
        public async Task<IActionResult> ApplyFilter(int type, string fromDate, string toDate, Filter filter)
        {
            try { 
            RenewalInformation model = new RenewalInformation();
            model.Filter = filter;
            model = await _renewalInformationService.RenewalInformationList(type, fromDate, toDate, filter);
            ViewBag.GrpBy = new SelectList(await _dropdownService.GetJoiningAndSeprationgionGrouping(), "Code", "Name");
            return PartialView("~/Areas/DataBank/Reports/Views/RenewalInformation/_RenewalInfoList.cshtml", model);
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
      


        public async Task<IActionResult> ExportToExcel(string fromDate, string toDate,string dues,int due, int totRecords,Filter filter)
        {
            try {

                filter.length = totRecords;
                filter.start = 0;
                string FilterValues = await _helpers.GetFilterValues(filter);

           
            var LoginUser = HttpContext.Session.GetString("LoginUser");
            var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Renewal Information (" + fromDate + " to " + toDate + ")", "Excel");
              
            if (FilterValues == "")
            {
                FilterValues = "All";
            }
            string currDateTime = await _helpers.GetCurrentDate();
            

                //GET DATASET
               var dataset = await _renewalInformationService.GetRenewalInformationList_Excel(due, fromDate, toDate, filter);
                DataTable dt = dataset.Tables[0];
                dataset.Tables[0].Rows.RemoveAt(0);
                if ((dataset.Tables[0]?.Rows?.Count ?? 0) > 0)
                {
                    dt = dataset.Tables[0];
                }

                //set EXCEL Column Heading 
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (dt.Columns[i].ColumnName == "doj" || dt.Columns[i].ColumnName == "dob" || dt.Columns[i].ColumnName == "y/s")
                        dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.ToUpper();
                    else
                        dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
                }
                using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Renewal Information List");
                int count = dt.Rows.Count+6;


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
                wsReportNameHeaderRange.Value = "Renewal Information";

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
                worksheet.Cell(3, 1).Value = "Dues";
                worksheet.Cell(3, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                worksheet.Cell(3, 2).Value = dues;

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
                    worksheet.Cell(4, 6).Value = dt.Rows.Count;
                worksheet.Cell(5, 1).Style.Font.Bold = true;
                worksheet.Cell(5, 1).Value = "Filter Applied";
                worksheet.Range("B5:K5").Merge();
                worksheet.Cell("B5").Value = FilterValues;



                var currentRow = 6;

                    worksheet.Cell(currentRow, 1).InsertTable(dt);
                    worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;
                    worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                    worksheet.Range("A6:K6").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Range("A6:K6").Style.Font.Bold = true;

                    worksheet.Range("H6:H" + (dt.Rows.Count + 6)).Style.DateFormat.Format = "dd-MMM-yyyy";
                    worksheet.Range("J6:J" + (dt.Rows.Count + 6)).Style.DateFormat.Format = "dd-MMM-yyyy";
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
                        "Renewal Information List.xlsx"
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
        public async Task<IActionResult> SummaryExportToExcel( DateTime fromDate, DateTime toDate)
        {
            try
            {
                RenewalInformation model = new RenewalInformation();
                model = await _renewalInformationService.RenewalInformationReport(fromDate.ToString("yyyy-MMM-dd"), toDate.ToString("yyyy-MMM-dd"));
                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                var TotalCount = model.EmployeeProfileSummary.Sum(x => x.Count);
                var TotalCTC = model.EmployeeProfileSummary.Sum(x => x.CTC);
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Renewal Information Summary(" + fromDate + " to " + toDate + ")", "Excel");
                int slNo = 0;

                string currDateTime = await _helpers.GetCurrentDate();
                
                if (User.authUser.SalaryVisibility != 0)
                {
                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("RenewalInformationSummary List");
                        int count = model.EmployeeProfileSummary.Count + 4;


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
                        wsReportNameHeaderRange.Value = "Renewal Information Summary";

                        worksheet.Cell(2, 1).Style.Font.Bold = true;
                        worksheet.Cell(2, 1).Value = "Group Company Name";
                        worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                        worksheet.Cell(2, 3).Style.Font.Bold = true;
                        worksheet.Cell(2, 3).Value = "From";
                        worksheet.Cell(2, 4).Value = fromDate;
                        worksheet.Cell(2, 4).Style.DateFormat.Format = "dd-MMM-yyyy";
                        worksheet.Cell(2, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);

                        worksheet.Cell(2, 5).Style.Font.Bold = true;
                        worksheet.Cell(2, 5).Value = "To";
                        worksheet.Cell(2, 6).Value = toDate;
                        worksheet.Cell(2, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
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
                        worksheet.Cell(3, 6).Value = model.EmployeeProfileSummary.Count;




                        var currentRow = 4;
                        #region Header
                        worksheet.Range("A4:F4").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                        worksheet.Cell(currentRow, 1).Value = "Sl.No";
                        worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 2).Value = "Group Name";
                        worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 3).Value = "Count";
                        worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 4).Value = " % ";
                        worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 5).Value = "Gross Pay";
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
                                "Renewal Information summary List.xlsx"
                                );
                        }
                    }
                }
                else
                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("RenewalInformationSummary List");
                        int count = model.EmployeeProfileSummary.Count + 4;


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
                        wsReportNameHeaderRange.Value = "Renewal Information Summary";

                        worksheet.Cell(2, 1).Style.Font.Bold = true;
                        worksheet.Cell(2, 1).Value = "Group Company Name";
                        worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                        worksheet.Cell(2, 3).Style.Font.Bold = true;
                        worksheet.Cell(2, 3).Value = "From";
                        worksheet.Cell(2, 4).Value = fromDate;
                        worksheet.Cell(2, 4).Style.DateFormat.Format = "dd-MMM-yyyy";
                        worksheet.Cell(2, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);

                        worksheet.Cell(2, 5).Style.Font.Bold = true;
                        worksheet.Cell(2, 5).Value = "To";
                        worksheet.Cell(2, 6).Value = toDate;
                        worksheet.Cell(2, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
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
                        worksheet.Cell(3, 6).Value = model.EmployeeProfileSummary.Count;




                        var currentRow = 4;
                        #region Header
                        worksheet.Range("A4:F4").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                        worksheet.Cell(currentRow, 1).Value = "Sl.No";
                        worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 2).Value = "Group Name";
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
                                "Renewal Information summary List.xlsx"
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
