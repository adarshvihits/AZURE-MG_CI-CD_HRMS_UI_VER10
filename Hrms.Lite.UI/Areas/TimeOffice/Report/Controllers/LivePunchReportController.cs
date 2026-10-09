using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.TimeOffice.Report;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TimeOffice.Report;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Hrms.Lite.Shared.Account;
using System.Data;
using System.IO;

using Filter = Hrms.Lite.Shared.General.Filter;
using ClosedXML.Excel;
using Spire.Xls;

namespace Hrms.Lite.UI.Areas.TimeOffice.Report.Controllers
{
    [Area("TimeOffice")]
    [SessionAuthorize]
    public class LivePunchReportController : Controller
    {

        private readonly ILivePunchReportService _LivePunchReportService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        private readonly IDropdownService _dropdownService;
        public static class Global
        {
            public static Filter GlobalFilter;
        }
        public LivePunchReportController(IDropdownService dropdownService, ILivePunchReportService LivePunchReportService, ICommonService commonService, IHelpers helpers)
        {
            _LivePunchReportService = LivePunchReportService;
            _commonService = commonService;
            _helpers = helpers;
            _dropdownService = dropdownService;
        }



        public async Task<IActionResult> Index()
        {
            LivePunchReport model = new LivePunchReport();
       
            await fillfilterDropdown();
            return View("/Areas/TimeOffice/Report/Views/LivePunchReport/Index.cshtml", model);
        }
        public IActionResult GetList(string Date)
        {
            LivePunchReport model = new LivePunchReport();
            return PartialView("/Areas/TimeOffice/Report/Views/LivePunchReport/_LivePunchReportList.cshtml", model);
        }

        public async Task fillfilterDropdown()
        {

            ViewBag.Gen = new SelectList(await _dropdownService.GetGenderDropDown(), "Code", "Name");
            ViewBag.Cat = new SelectList(await _dropdownService.GetCategoryDropdown(), "Code", "Name");
            ViewBag.Grd = new SelectList(await _dropdownService.GetGradeDropdown(), "Code", "Name");
            ViewBag.Dsg = new SelectList(await _dropdownService.GetDesignationDropdown(), "Code", "Name");
            ViewBag.Dept = new SelectList(await _dropdownService.GetDepartmentDropdown(), "Code", "Name");
            ViewBag.Loc = new SelectList(await _dropdownService.GetLocationDropdown(), "Code", "Name");
            ViewBag.PayrollGrp = new SelectList(await _dropdownService.GetPayrollGroupDropdown(), "Code", "Name");

            ViewBag.division = new SelectList(await _dropdownService.GetDivisionDropdown(), "Code", "Name");
            ViewBag.section = new SelectList(await _dropdownService.GetSectionDropdown(), "Code", "Name");
            ViewBag.EmploymentType = new SelectList(await _dropdownService.GetEmploymentTypeDropdown(), "Code", "Name");
            ViewBag.EmploymentStatus = new SelectList(await _dropdownService.GetEmploymentStatusDropdown(), "Code", "Name");
            ViewBag.WorkLocation = new SelectList(await _dropdownService.GetWorkLocationDropdown(), "Code", "Name");
            ViewBag.WageGrade = new SelectList(await _dropdownService.GetWageGradeDropdown(), "Code", "Name");

        }

        public async Task<IActionResult> ApplyFilter(string Date, Filter filter)
        {
            try
            {
                LivePunchReport model = new LivePunchReport();
                Global.GlobalFilter = filter;
                model = await _LivePunchReportService.GetList(Date, Global.GlobalFilter);
                return PartialView("/Areas/TimeOffice/Report/Views/LivePunchReport/_LivePunchReportList.cshtml", model);
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
        public async Task<IActionResult> GetReport([DataSourceRequest] DataSourceRequest request, string date, Filter filter1)
        {
            LivePunchReport model = new LivePunchReport();
            List<LivePunchReport> modellist = new List<LivePunchReport>();
            if (date != null)
            {
                model = await _LivePunchReportService.GetList(date, filter1);
                modellist = model.LivePunchReportList;
            }
           
            var dataResult1 = modellist.ToDataSourceResult(request);

            return Json(dataResult1, new JsonSerializerSettings { ContractResolver = new DefaultContractResolver() });

        }

        public async Task<IActionResult> ExportToExcel(Filter filter, string Date, string DEPARTMENT, int TotalRows)
        {
            LivePunchReport model = new LivePunchReport();
            var LoginUser = HttpContext.Session.GetString("LoginUser");
            var CompanyName = HttpContext.Session.GetString("CompanyName");
            var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
            var GroupCompanyName = User.authUser.GroupCompanyName;
            ExcelPassword EP = new ExcelPassword();
            EP = await _commonService.GetExcelPassword("Live Punch Report List", "Excel");


            filter.length = TotalRows;
            filter.start = 0;

            string FilterValues = await _helpers.GetFilterValues(filter);
            if (FilterValues == "")
            {
                FilterValues = "All";
            }
            model.filter = filter;



            string[] cu_Date = System.DateTime.Now.GetDateTimeFormats();
            string currDateTime = cu_Date[44];

            //Get Dataset
            var dataset = await _LivePunchReportService.LivePunchReport_Excel(filter, Date);
            DataTable dt = dataset.Tables[0];


            //set EXCEL Column Heading 
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
            }

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("LivePunchReport List");
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
                wsReportNameHeaderRange.Value = "LivePunchReport List";

                worksheet.Cell(2, 1).Style.Font.Bold = true;
                worksheet.Cell(2, 1).Value = "Group Company Name";
                worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                worksheet.Cell(2, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);

                worksheet.Cell(3, 1).Style.Font.Bold = true;
                worksheet.Cell(3, 1).Value = "Date";
                worksheet.Cell(3, 2).Value = Date;
                worksheet.Cell(3, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                worksheet.Cell(3, 2).Style.DateFormat.Format = "dd-MMM-yyyy";

                worksheet.Cell(3, 3).Style.Font.Bold = true;
                worksheet.Cell(3, 3).Value = "Department";
                worksheet.Cell(3, 4).Value = DEPARTMENT;
                worksheet.Cell(3, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);



                worksheet.Cell(2, 3).Style.Font.Bold = true;
                //worksheet.Cell(2, 3).Value = "MoodName";
                //worksheet.Cell(2, 4).Value = MoodName;
                worksheet.Cell(2, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                worksheet.Cell(2, 4).Style.DateFormat.Format = "dd-MMM-yyyy";



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
                var filterapplied = worksheet.Range("B5:K5").Merge();

                filterapplied.Value = FilterValues;


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
                        book.Protect(EP.Password, false, false);
                        book.SaveToStream(stream);
                    }
                    var content = stream.ToArray();
                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "LivePunchReport List.xlsx"
                        );
                }
            }
        }


    }
}
