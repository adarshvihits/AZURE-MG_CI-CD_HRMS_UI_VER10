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
    public class LeaveBalanceAsOnDateController : Controller
    {
        private readonly ILeaveBalanceAsOnDateService _LeaveBalanceAsOnDateService;
        private readonly IDropdownService _dropdownService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;


    public LeaveBalanceAsOnDateController(IDropdownService dropdownService, ICommonService commonService, IHelpers helpers, ILeaveBalanceAsOnDateService LeaveBalanceAsOnDateService)
    {
        _LeaveBalanceAsOnDateService = LeaveBalanceAsOnDateService;
        _dropdownService = dropdownService;
        _commonService = commonService;
        _helpers = helpers;
    }

        public async Task<IActionResult> Index()
        {
            LeaveBalanceAsOnDate model = new LeaveBalanceAsOnDate();
            await fillfilterDropdown();
            Filter filter = new Filter();
            model.filter = filter;
            ViewBag.Leave = new SelectList(await _dropdownService.GetLeaveFilterDropdown(), "Code", "Name");
            model.Date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            model.Type = "ACTIVE";
            return View("~/Areas/TimeOffice/Report/Views/LeaveBalanceAsOnDate/Index.cshtml", model);
        }


        [HttpPost]
        public async Task<IActionResult> GetList(string Date, string Type, int Leave, Filter filter)
        {
            try
            {
                LeaveBalanceAsOnDate model = new LeaveBalanceAsOnDate();


                if (filter.searchValue == "undefined" || filter.searchValue == null)
                {
                    filter.searchValue = "";
                }

                model = await _LeaveBalanceAsOnDateService.GetList(Date, Type, Leave, filter);

                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.LeaveBalanceAsOnDatelist == null || model.LeaveBalanceAsOnDatelist.Count() == 0)
                {
                    return Json(new
                    {
                        data = "",
                        draw = filter.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows
                    });
                }
                if (model.LeaveBalanceAsOnDatelist.Count > 0)
                {
                    totalrows = model.LeaveBalanceAsOnDatelist[0].active;
                }
                if (filter.searchValue != "")
                {
                    totalrowsafterfiltering = model.LeaveBalanceAsOnDatelist[0].totalRecords;
                    return Json(new
                    {
                        data = model.LeaveBalanceAsOnDatelist,
                        draw = filter.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows//totalrowsafterfiltering
                    });
                }
                else
                {
                    return Json(new
                    {
                        data = model.LeaveBalanceAsOnDatelist,
                        draw = filter.draw,//Request.Form["draw"], 
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

        public async Task<IActionResult> ExportToExcel(string Date, string Type, int Leave, Filter filter, int TotalRows)
        {
            LeaveBalanceAsOnDate model = new LeaveBalanceAsOnDate();
            var LoginUser = HttpContext.Session.GetString("LoginUser");
            var CompanyName = HttpContext.Session.GetString("CompanyName");
            var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
            var GroupCompanyName = User.authUser.GroupCompanyName;
            ExcelPassword EP = new ExcelPassword();
            EP = await _commonService.GetExcelPassword("Attrition Report List", "Excel");

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
            var dataset = await _LeaveBalanceAsOnDateService.LeaveBalanceAsOnDate_Excel(Date,Type,Leave, filter);
            DataTable dt = dataset.Tables[0];
            //dataset.Tables[0].Rows.RemoveAt(0);
            //if ((dataset.Tables[0]?.Rows?.Count ?? 0) > 0)
            //{
            //    dt = dataset.Tables[0];
            //}

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
                var worksheet = workbook.Worksheets.Add("Leave Balance As On Date");
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
                wsReportNameHeaderRange.Value = "Leave Balance As On Date";

                worksheet.Cell(2, 1).Style.Font.Bold = true;
                worksheet.Cell(2, 1).Value = "Group Company Name";
                worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                worksheet.Cell(2, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);

                worksheet.Cell(3, 1).Style.Font.Bold = true;
                worksheet.Cell(3, 1).Value = "Date";
                worksheet.Cell(3, 2).Value = Date;
                worksheet.Cell(3, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                worksheet.Cell(3, 2).Style.DateFormat.Format = "dd-MMM-yyyy";

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
                        book.Protect(EP.Password);
                        book.SaveToStream(stream);
                    }
                    var content = stream.ToArray();
                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "LeaveBalanceAsOnDatelist.xlsx"
                        );
                }
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


    }
}
