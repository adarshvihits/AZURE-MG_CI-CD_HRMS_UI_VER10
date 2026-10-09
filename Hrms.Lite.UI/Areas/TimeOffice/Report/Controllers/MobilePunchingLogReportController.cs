using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.TimeOffice.Report;
using Hrms.Lite.Shared.Account;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TimeOffice.Report;
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

namespace Hrms.Lite.UI.Areas.TimeOffice.Report.Controllers
{
    [Area("TimeOffice")]
    [SessionAuthorize]
    public class MobilePunchingLogReportController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly IMobilePunchingLogReportService _mobilePunchingLogReportService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        public static class Global
        {
            public static Filter GlobalFilter;
        }
        public MobilePunchingLogReportController(IMobilePunchingLogReportService mobilePunchingLogReportService, IDropdownService dropdownService, IHelpers helpers, ICommonService commonService)
        {
            _mobilePunchingLogReportService = mobilePunchingLogReportService;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = helpers;
        }
        public async Task <IActionResult> Index()
        {
            try
            {
                ReaderLogReport model = new ReaderLogReport();
            DateTime now = DateTime.Now;
            var startDate = new DateTime(now.Year, now.Month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);
            model.From = startDate;
            model.To = endDate;
            await fillfilterDropdown();
            return View("~/Areas/TimeOffice/Report/Views/MobilePunchingLogReport/Index.cshtml",model);
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
        public async Task<IActionResult> GetMobilePunchingLogList(Filter filter1,int ClassificationCode)
        {
            try
            {
                ReaderLogReport model = new ReaderLogReport();
                ReaderLogReport m = new ReaderLogReport();

                Filter filter = new Filter();
                Global.GlobalFilter = filter1;
                model.filter = filter1;
                model.From = filter1.F_From;
                model.To = filter1.F_To;
                
                model.Classification = ClassificationCode;

                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }

                model = await _mobilePunchingLogReportService.GetMobileLogListReport(model);
                if (model == null || model.ReaderLogReportList == null || model.ReaderLogReportList.Count() == 0)
                {
                    m.ReaderLogReportList = new List<ReaderLogReportList>();
                    model = new ReaderLogReport();
                    model.ReaderLogReportList = new List<ReaderLogReportList>();
                }
                else
                {
                    m.ReaderLogReportList = model.ReaderLogReportList;
                }

                m.ReaderLogReportList = model.ReaderLogReportList;
                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.ReaderLogReportList == null || model.ReaderLogReportList.Count() == 0)
                {


                    return Json(new
                    {
                        data = "",
                        draw = filter1.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows
                    });
                }

                if (model.ReaderLogReportList.Count > 0)
                {
                    totalrows = m.ReaderLogReportList[0].active;
                }
                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = m.ReaderLogReportList[0].totalRecords;

                    return Json(new
                    {
                        data = model.ReaderLogReportList,
                        draw = filter1.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows//totalrowsafterfiltering
                    });
                }
                else
                {
                    return Json(new
                    {
                        data = model.ReaderLogReportList,
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



        public async Task<IActionResult> ExportToExcel(DateTime From, DateTime To, int ClassificationCode, string ClassificationName, int TotalRows)
        {
            ReaderLogReport model = new ReaderLogReport();
            var LoginUser = HttpContext.Session.GetString("LoginUser");
            var CompanyName = HttpContext.Session.GetString("CompanyName");
            var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
            var GroupCompanyName = User.authUser.GroupCompanyName;
            ExcelPassword EP = new ExcelPassword();
            EP = await _commonService.GetExcelPassword("Mobile Punching Log Report", "Excel");

            if (Global.GlobalFilter == null)
                Global.GlobalFilter = new Filter();
            {
                Global.GlobalFilter.length = TotalRows;
                Global.GlobalFilter.start = 0;
            }
            string FilterValues = await _helpers.GetFilterValues(Global.GlobalFilter);
            if (FilterValues == "")
            {
                FilterValues = "All";
            }
            model.filter = Global.GlobalFilter;
            model.From = From;
            model.To = To;
         
            model.Classification = ClassificationCode;

            string currDateTime = await _helpers.GetCurrentDate();
            

            //Get Dataset
            var dataset = await _mobilePunchingLogReportService.GetMobileLogListReport_Excel(model);
            DataTable dt = dataset.Tables[0];
            dataset.Tables[0].Rows.RemoveAt(0);
            if ((dataset.Tables[0]?.Rows?.Count ?? 0) > 0)
            {
                dt = dataset.Tables[0];
            }

            //set EXCEL Column Heading 
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
            }

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Mobile Punching Log Report");
                int count = dt.Rows.Count + 6;


                worksheet.Range("A1:J" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1:J" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1:J" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1:J" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1:J" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;



                var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 9)));
                wsReportNameHeaderRange.Style.Font.Bold = true;
                wsReportNameHeaderRange.Style.Font.FontSize = 15;
                wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                wsReportNameHeaderRange.Merge();
                wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                wsReportNameHeaderRange.Value = "Mobile Punching Log Report";

                worksheet.Cell(2, 1).Style.Font.Bold = true;
                worksheet.Cell(2, 1).Value = "Group Company Name";
                worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                worksheet.Cell(2, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);

              
                
                worksheet.Cell(3, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                worksheet.Cell(3, 1).Style.Font.Bold = true;
                worksheet.Cell(3, 1).Value = "Classification";
                worksheet.Cell(3, 2).Value = ClassificationName;
                worksheet.Cell(3, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);



                worksheet.Cell(3, 3).Style.Font.Bold = true;
                worksheet.Cell(3, 3).Value = "From";
                worksheet.Cell(3, 4).Value = From;
                worksheet.Cell(3, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                worksheet.Cell(3, 4).Style.DateFormat.Format = "dd-MMM-yyyy";

                worksheet.Cell(3, 5).Style.Font.Bold = true;
                worksheet.Cell(3, 5).Value = "To";
                worksheet.Cell(3, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                worksheet.Cell(3, 6).Value = To;
                worksheet.Cell(3, 6).Style.DateFormat.Format = "dd-MMM-yyyy";




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
                var filterapplied = worksheet.Range("B5:J5").Merge();

                filterapplied.Value = FilterValues;


                var currentRow = 6;


                worksheet.Cell(currentRow, 1).InsertTable(dt);
                worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                worksheet.Range("A6:J6").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                worksheet.Range("A6:J6").Style.Font.Bold = true;


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
                        "Mobile Punching Log Report List.xlsx"
                        );
                }
            }

            //}
        }

    }
}
