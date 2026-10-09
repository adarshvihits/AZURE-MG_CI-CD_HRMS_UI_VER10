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
    public class ProfileCTCReportController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly IProfileCTCReportService _profileCTCReportService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
       
        public ProfileCTCReportController(IProfileCTCReportService profileCTCReportService, IDropdownService dropdownService, ICommonService commonService, IHelpers Helpers)
        {
            _profileCTCReportService = profileCTCReportService;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = Helpers;

        }
        public async Task<IActionResult> Index()
        {
            try { 
            ProfileCTCReport model = new ProfileCTCReport();
            ViewBag.SalaryPeriod = new SelectList(await _dropdownService.GetMonthDropdown(), "Code", "Name");
            var currMonth = await _commonService.GetCurrentMonthDropdown();  
            model.Month = currMonth.Data;
            //model.Filter = new Shared.General.Filter();
            Filter filter = new Filter();
            filter.length = 10;
            model = await _profileCTCReportService.GetReportList(0,filter);
            model.Month = currMonth.Data;
            model.Filter = await _commonService.GetFilterDropdowns();
            return View("~/Areas/DataBank/Reports/Views/ProfileCTCReport/Index.cshtml",model);
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
        public async Task<IActionResult> GetProfileCTCList(Filter filter1, int SalaryPeriodCode)
        {
            try
            {
                ProfileCTCReport model = new ProfileCTCReport();
                ProfileCTCReport m = new ProfileCTCReport();
                Filter filter = new Filter();
               model.Filter = filter1;
               

                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }

                model = await _profileCTCReportService.GetReportList(SalaryPeriodCode, filter1);
                m.List = model.List;
                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.List == null || model.List.Count() == 0)
                {
                    

                    return Json(new
                    {
                        data = "",
                        draw = filter1.draw,
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows
                    });
                }




                if (model.List.Count > 0)
                {
                    totalrows = m.List[0].active;

                }

             
                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = m.List[0].totalRecords;
                    return Json(new
                    {
                        data = model.List,
                        draw = filter1.draw,
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows
                    });
                }
                else
                {
                    return Json(new
                    {
                        data = model.List,
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













        public async Task<IActionResult>GetList(int SalaryPeriodCode )
        {
            ProfileCTCReport model = new ProfileCTCReport();
            ViewBag.SalaryPeriod = new SelectList(await _dropdownService.GetMonthDropdown(), "Code", "Name");
            Filter filter = new Filter();
            if (SalaryPeriodCode != 0)
            {
                model = await _profileCTCReportService.GetReportList(SalaryPeriodCode, filter);
            }
            return PartialView("~/Areas/DataBank/Reports/Views/ProfileCTCReport/_ProfileCTCReportList.cshtml", model);
        }

        public async Task<IActionResult> ApplyFilter(int SalaryPeriodCode, Filter filter)
        {
            try
            {

                ProfileCTCReport model = new ProfileCTCReport();
                model.Filter = filter;
                model = await _profileCTCReportService.GetReportList(SalaryPeriodCode, filter);
                return PartialView("~/Areas/DataBank/Reports/Views/ProfileCTCReport/_ProfileCTCReportList.cshtml", model);


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


        
        public async Task<IActionResult> ExportToExcel(int SalaryPeriodCode,string Month,int TotalRec)
        {
            try
            {

                Filter filter = new Filter();
                filter.length = TotalRec;
                filter.start = 0;
                string FilterValues = await _helpers.GetFilterValues(filter);             
             
                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Profile CTC Report", "Excel");

                if (FilterValues == "")
                {
                    FilterValues = "All";
                }
                string currDateTime = await _helpers.GetCurrentDate();
                

                //Get Datset
                var dataset = await _profileCTCReportService.GetProfileCTCReport_Excel(SalaryPeriodCode, filter);
                DataTable dt = dataset.Tables[0];


                //set EXCEL Column Heading 
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (dt.Columns[i].ColumnName == "pfmc" || dt.Columns[i].ColumnName == "ctc" || dt.Columns[i].ColumnName == "esimc")
                        dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.ToUpper();
                    else
                        dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
                }


                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Profile CTC Report");

                    int count = dt.Rows.Count + 5;


                    worksheet.Range("A1:M" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:M" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:M" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:M" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:M" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 12)));
                    wsReportNameHeaderRange.Style.Font.Bold = true;
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Merge();
                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191); ;
                    wsReportNameHeaderRange.Value = "Profile CTC Report";

                    worksheet.Cell(2, 1).Style.Font.Bold = true;
                    worksheet.Cell(2, 1).Value = "Group Company Name";
                    worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                    worksheet.Cell(2, 3).Style.Font.Bold = true;
               
                    worksheet.Cell(2, 3).Value = "Month";
                    worksheet.Cell(2, 4).Value = Month;
                    worksheet.Cell(2, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(2, 4).Style.DateFormat.Format = "dd-MMM-yyyy";

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
                    worksheet.Range("B4:M4").Merge();
                    worksheet.Cell("B4").Value = FilterValues;



                    var currentRow = 5;

                    worksheet.Cell(currentRow, 1).InsertTable(dt);
                    worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                    worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                    worksheet.Range("A5:M5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Range("A5:M5").Style.Font.Bold = true;

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
                            "Profile CTC Report List.xlsx"
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
