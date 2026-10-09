using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.PayRoll;
using Hrms.Lite.Shared.Account;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
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
    public class GratuityProcessingReportController : Controller
    {

        private readonly IDropdownService _dropdownService;
        private readonly IGratuityProcessingService _GratuityProcessingService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;

     

        public GratuityProcessingReportController(IGratuityProcessingService gratuityProcessingService, IDropdownService dropdownService, ICommonService commonService, IHelpers helpers)
        {
            _GratuityProcessingService = gratuityProcessingService;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = helpers;
        }
        public static class Global
        {

            public static string url;
            public static string FromDate;
            public static string Todate;


        }

        public async Task<IActionResult> Index()
        {
            GratuityProcessing model = new GratuityProcessing();
            Filter filter = new Filter();
            model.Filter = filter;
            await fillfilterDropdown();
            DateTime now = DateTime.Now;
            int year = DateTime.Now.Year;      
            var startDate = new DateTime(year, 1, 1);
            var endDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1);

            //model = await _EmployeeCompanyDocumentsService.GetEmployeeCompanyList(1, startDate.ToString("yyyy-MMM-dd"), endDate.ToString("yyyy-MMM-dd"), filter);
            Global.FromDate = startDate.ToString("yyyy-MMM-dd");
            Global.Todate = endDate.ToString("yyyy-MMM-dd");
            model.From = startDate;
            model.To = endDate;
            // model = await _GratuityProcessingService.GetGratuityList(filter);
            return View("~/Areas/PayRoll/Reports/Views/GratuityProcessingReport/Index.cshtml",model);
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

        [HttpPost]
        public async Task<IActionResult> GetgratuityReportList(Filter filter1,string From,string To)
       {
            try
            {
                GratuityProcessing model = new GratuityProcessing();
                GratuityProcessing m = new GratuityProcessing();

                Filter filter = new Filter();
                if(From!=null & To!= null)
                {
                    model = await _GratuityProcessingService.GetGratuityList(From, To, filter1);

                }
               

                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }


                {
                    m.GratuityReport = model.GratuityReport;
                }
                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.GratuityReport == null || model.GratuityReport.Count() == 0)
                {

                    return Json(new
                    {
                        data = "",
                        draw = filter1.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows
                    });
                }




                if (model.GratuityReport.Count > 0)
                {
                    m.GratuityReport = model.GratuityReport;
                    totalrows = m.GratuityReport[0].GratuityEmpDetails.Active;

                }


                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = m.GratuityReport[0].GratuityEmpDetails.totalRecords;
                    return Json(new
                    {
                        data = model.GratuityReport,
                        draw = filter1.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows//totalrowsafterfiltering
                    });
                }
                else
                {
                    return Json(new
                    {
                        data = model.GratuityReport,
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

        public async Task<IActionResult> ExportToExcel(string From, string To, Filter Filter, int TotalRows)
        {
            GratuityProcessing model = new GratuityProcessing();
            var LoginUser = HttpContext.Session.GetString("LoginUser");
            var CompanyName = HttpContext.Session.GetString("CompanyName");
            var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
            var GroupCompanyName = User.authUser.GroupCompanyName;
            ExcelPassword EP = new ExcelPassword();
            EP = await _commonService.GetExcelPassword("Gratuity Processing Report", "Excel");


            Filter.length = TotalRows;
            Filter.start = 0;

            string FilterValues = await _helpers.GetFilterValues(Filter);
            if (FilterValues == "")
            {
                FilterValues = "All";
            }
            model.Filter = Filter;



            string[] cu_Date = System.DateTime.Now.GetDateTimeFormats();
            string currDateTime = cu_Date[44];

            //Get Dataset
            var dataset = await _GratuityProcessingService.GratuityProcessingReport_Excel(From, To, Filter);
            DataTable dt = dataset.Tables[0];


            //set EXCEL Column Heading 
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                if (dt.Columns[i].ColumnName == "pf" || dt.Columns[i].ColumnName == "esi" || dt.Columns[i].ColumnName == "uan" || dt.Columns[i].ColumnName == "lic"||
                    dt.Columns[i].ColumnName == "lwd" || dt.Columns[i].ColumnName == "doj"|| dt.Columns[i].ColumnName == "dob")
                    dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.ToUpper();
                else
                    dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
            }

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Gratuity Processing Report List");
                int count = dt.Rows.Count + 6;

                string columnName = "";
                int Columncount = dt.Columns.Count;

                while (Columncount > 0)
                {
                    int modulo = (Columncount - 1) % 26;
                    columnName = Convert.ToChar('A' + modulo) + columnName;
                    Columncount = (Columncount - modulo) / 26;
                }
                string colName = columnName;
                worksheet.Range("A1", colName + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1", colName + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1", colName + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1", colName + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1", colName + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;



                var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, colName));
                wsReportNameHeaderRange.Style.Font.Bold = true;
                wsReportNameHeaderRange.Style.Font.FontSize = 15;
                wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                wsReportNameHeaderRange.Merge();
                wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                wsReportNameHeaderRange.Value = "Gratuity Processing Report List";

                worksheet.Cell(2, 1).Style.Font.Bold = true;
                worksheet.Cell(2, 1).Value = "Group Company Name";
                worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                worksheet.Cell(2, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);

                worksheet.Cell(3, 1).Style.Font.Bold = true;
                worksheet.Cell(3, 1).Value = "From";
                worksheet.Cell(3, 2).Value = From;
                worksheet.Cell(3, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                worksheet.Cell(3, 2).Style.DateFormat.Format = "dd-MMM-yyyy";
                worksheet.Cell(3, 3).Style.Font.Bold = true;
                worksheet.Cell(3, 3).Value = "To";
                worksheet.Cell(3, 4).Value = To;
                worksheet.Cell(3, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                worksheet.Cell(3, 4).Style.DateFormat.Format = "dd-MMM-yyyy";





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
                worksheet.Cell(5, 1).Style.Font.Bold = true;
                worksheet.Cell(5, 1).Value = "Filter Applied";
                var filterapplied = worksheet.Range("B5", colName + 5).Merge();

                filterapplied.Value = FilterValues;


                var currentRow = 6;


                worksheet.Cell(currentRow, 1).InsertTable(dt);
                worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                worksheet.Range("A6", colName + 6).Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                worksheet.Range("A6", colName + 6).Style.Font.Bold = true;


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
                        "GratuityProcessingReport List.xlsx"
                        );
                }
            }

            //}
        }
        public async Task<IActionResult> GetGratuityProcessingPDF(int EmpCode)
        {
            KendoReport model = new KendoReport();
            model.EmpCode = EmpCode;
            model.Url = await _commonService.GetUrl();
            Global.url = model.Url.Urllink;
            model.Url.Urllink = Global.url;
            return View("~/Areas/PayRoll/Reports/Views/GratuityProcessingReport/EmployeeGratuityReport.cshtml", model);


        }
    }
}
