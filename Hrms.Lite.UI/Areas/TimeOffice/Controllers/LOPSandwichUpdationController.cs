using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.TimeOffice;
using Hrms.Lite.Shared.Account;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TimeOffice;
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

namespace Hrms.Lite.UI.Areas.TimeOffice.Controllers
{
    [Area("TimeOffice")]
    [SessionAuthorize]
    public class LOPSandwichUpdationController : Controller
    {
        private readonly ILOPSandwichUpdationService _service;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        private readonly IDropdownService _drop;

        public LOPSandwichUpdationController(ILOPSandwichUpdationService service, ICommonService commonService, IHelpers helpers, IDropdownService drop)
        {
            _service = service;
            _commonService = commonService;
            _helpers = helpers;
            _drop = drop;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                LOPSandwichUpdation model = new LOPSandwichUpdation();
            LOPSandwichUpdation m1 = new LOPSandwichUpdation();
            Filter filter = new Filter();
            filter.length = 10;
            model = await _service.GetList(0, 0, filter);
            ViewBag.Month = new SelectList(await _drop.GetMonthly_AttendanceProcessing_MonthDropdown(0), "Code", "Name");
            ViewBag.Payroll = new SelectList(await _drop.GetPayrollGroupDropdown(), "Code", "Name");
            await fillfilterDropdown();
            return View(model);
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
        public async Task<ActionResult> GetList(Filter filter, int Month, int Payroll)
        {
            try
            {
                LOPSandwichUpdation model = new LOPSandwichUpdation();
                model = await _service.GetList(Month, Payroll, filter); 
                return PartialView("~/Areas/TimeOffice/Views/LOPSandwichUpdation/_List.cshtml", model);
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
        public async Task<IActionResult> LOPSandwichUpdationSave(LOPSandwichUpdation model)
        {
            try
            {
                var response = await _service.LOPSandwichUpdationSave(model);

                return Json(response);
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


        public async Task<IActionResult> TransactionHistory()
        {
            try
            {
                ViewBag.Month = new SelectList(await _drop.GetMonthly_AttendanceProcessing_MonthDropdown(0), "Code", "Name");
                ViewBag.Payroll = new SelectList(await _drop.GetPayrollGroupDropdown(), "Code", "Name");
                await fillfilterDropdown();
                LOPSandwichUpdation model = new LOPSandwichUpdation();
                Filter filter = new Filter();
                return View(model);
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
        public async Task<ActionResult> GetHistoryList(Filter filter, int Month, int Payroll)
        {
            try
            {
                LOPSandwichUpdation model = new LOPSandwichUpdation();
                model = await _service.GetHistoryList(Month, Payroll, filter);
                return PartialView("~/Areas/TimeOffice/Views/LOPSandwichUpdation/_TrnList.cshtml", model);
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

        public async Task<IActionResult> ExportToExcel(int Month, int Payroll, Filter filter, int TotalRows, string MonthName, string PayrollName)
        {
            LOPSandwichUpdation model = new LOPSandwichUpdation();
            var LoginUser = HttpContext.Session.GetString("LoginUser");
            var CompanyName = HttpContext.Session.GetString("CompanyName");
            var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
            var GroupCompanyName = User.authUser.GroupCompanyName;
            ExcelPassword EP = new ExcelPassword();
            EP = await _commonService.GetExcelPassword("LOP Sandwich Updation", "Excel");


            filter.length = TotalRows;
            filter.start = 0;

            string FilterValues = await _helpers.GetFilterValues(filter);
            if (FilterValues == "")
            {
                FilterValues = "All";
            }
            model.Filter = filter;



            string[] cu_Date = System.DateTime.Now.GetDateTimeFormats();
            string currDateTime = cu_Date[44];

            //Get Dataset
            var dataset = await _service.LOPSandwichUpdationExcel(Month,Payroll, filter);
            DataTable dt = dataset.Tables[0];


            //set EXCEL Column Heading 
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
            }

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("LOP Sandwich Updation");
                int count = dt.Rows.Count + 6;


                worksheet.Range("A1:L" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1:L" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1:L" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1:L" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1:L" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;



                var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 11)));
                wsReportNameHeaderRange.Style.Font.Bold = true;
                wsReportNameHeaderRange.Style.Font.FontSize = 15;
                wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                wsReportNameHeaderRange.Merge();
                wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                wsReportNameHeaderRange.Value = "LOP Sandwich Updation List";

                worksheet.Cell(2, 1).Style.Font.Bold = true;
                worksheet.Cell(2, 1).Value = "Group Company Name";
                worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                worksheet.Cell(2, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);

                worksheet.Cell(3, 1).Style.Font.Bold = true;
                worksheet.Cell(3, 1).Value = "MonthName";
                worksheet.Cell(3, 2).Value = MonthName;
                worksheet.Cell(3, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                worksheet.Cell(3, 2).Style.DateFormat.Format = "MMM-yyyy  ";

                worksheet.Cell(3, 3).Style.Font.Bold = true;
                worksheet.Cell(3, 3).Value = "Payroll";
                worksheet.Cell(3, 4).Value = PayrollName;
                worksheet.Cell(3, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);

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
                var filterapplied = worksheet.Range("B5:L5").Merge();

                filterapplied.Value = FilterValues;


                var currentRow = 6;


                worksheet.Cell(currentRow, 1).InsertTable(dt);
                worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                worksheet.Range("A6:L6").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                worksheet.Range("A6:L6").Style.Font.Bold = true;


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
                        "LOPSandwichUpdation List.xlsx"
                        );
                }
            }

        }



        public async Task fillfilterDropdown()
        {
            ViewBag.Gen = new SelectList(await _drop.GetGenderDropDown(), "Code", "Name");
            ViewBag.Cat = new SelectList(await _drop.GetCategoryDropdown(), "Code", "Name");
            ViewBag.Grd = new SelectList(await _drop.GetGradeDropdown(), "Code", "Name");
            ViewBag.Dsg = new SelectList(await _drop.GetDesignationDropdown(), "Code", "Name");
            ViewBag.Dept = new SelectList(await _drop.GetDepartmentDropdown(), "Code", "Name");
            ViewBag.Loc = new SelectList(await _drop.GetLocationDropdown(), "Code", "Name");
            ViewBag.PayrollGrp = new SelectList(await _drop.GetPayrollGroupDropdown(), "Code", "Name");
            ViewBag.division = new SelectList(await _drop.GetDivisionDropdown(), "Code", "Name");
            ViewBag.section = new SelectList(await _drop.GetSectionDropdown(), "Code", "Name");
            ViewBag.EmploymentType = new SelectList(await _drop.GetEmploymentTypeDropdown(), "Code", "Name");
            ViewBag.EmploymentStatus = new SelectList(await _drop.GetEmploymentStatusDropdown(), "Code", "Name");
            ViewBag.WorkLocation = new SelectList(await _drop.GetWorkLocationDropdown(), "Code", "Name");
            ViewBag.WageGrade = new SelectList(await _drop.GetWageGradeDropdown(), "Code", "Name");
        }


    }
}
