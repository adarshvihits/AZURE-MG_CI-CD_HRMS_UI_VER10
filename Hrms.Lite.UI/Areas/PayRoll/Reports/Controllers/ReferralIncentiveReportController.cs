using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.PayRoll.Reports;
using Hrms.Lite.Shared.Account;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll.Reports;
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
    [SessionAuthorize]
    [Area("PayRoll")]
    public class ReferralIncentiveReportController : Controller
    {
        private readonly IReferralIncentiveReportService _service;
        private readonly ICommonService _commonService;
        private readonly IDropdownService _dropdownService;
        private readonly IHelpers _helpers;
        public ReferralIncentiveReportController(IReferralIncentiveReportService service, IDropdownService dropdownService, ICommonService commonService, IHelpers helpers)
        {
            _service = service;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = helpers;
        }
        public static class Global
        {
            public static FilterMultiSelect GlobalFilter;

        }
        public async Task<IActionResult> Index()
        {
            try
            {
                var currMonth = await _commonService.GetCurrentMonthDropdown();
                ViewBag.month = new SelectList(await _dropdownService.GetPayrollReportMonthDropdown(), "Code", "Name", Convert.ToInt32(currMonth.Data));
                await fillfilterDropdown();
                ReferralIncentiveReport model = new ReferralIncentiveReport();
                model = await _service.GetList(Global.GlobalFilter, 0);
                model.filter = await _commonService.GetMultiSelectFilterDropdowns();
                return View("~/Areas/PayRoll/Reports/Views/ReferralIncentiveReport/Index.cshtml", model);
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


        public async Task<IActionResult> GetList( int MonthCode)
        {
            ReferralIncentiveReport model = new ReferralIncentiveReport();
            model.filter = new FilterMultiSelect();
            model = await _service.GetList(Global.GlobalFilter, MonthCode);
            return PartialView("~/Areas/PayRoll/Reports/Views/ReferralIncentiveReport/_List.cshtml", model);

        }


        public async Task<IActionResult> ExportToExcel(int MonthCode)
        {
            try
            {

                //if (Global.GlobalFilter == null)
                //    Global.GlobalFilter = new Filter();

                //string FilterValues = await _helpers.GetFilterValues(Global.GlobalFilter);


                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Referral Incentive Report ", "Excel");

                //if (FilterValues == "")
                //{
                //    FilterValues = "All";
                //}
                string currDateTime = await _helpers.GetCurrentDate();
                

                //Get Dataset
                var datset = await _service.GetList_Excel(MonthCode);
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
                    var worksheet = workbook.Worksheets.Add("Referral Incentive Report");

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

                    //worksheet.Cell(3, 1).Style.Font.Bold = true;
                    //worksheet.Cell(3, 1).Value = "From Date";
                    //worksheet.Cell(3, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    //worksheet.Cell(3, 2).Style.DateFormat.Format = "dd-MMM-yyyy";
                    //worksheet.Cell(3, 2).Value = FromDte.ToString();

                    //worksheet.Cell(3, 3).Style.Font.Bold = true;
                    //worksheet.Cell(3, 3).Value = "To Date";
                    //worksheet.Cell(3, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    //worksheet.Cell(3, 4).Style.DateFormat.Format = "dd-MMM-yyyy";
                    //worksheet.Cell(3, 4).Value = ToDte.ToString();

                    //worksheet.Cell(4, 1).Style.Font.Bold = true;
                    //worksheet.Cell(4, 1).Value = "Filter Applied";
                    //worksheet.Range("B4:K4").Merge();
                    //worksheet.Cell("B4").Value = FilterValues;



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
                            book.Protect(EP.Password, false, false);
                            book.SaveToStream(stream);
                        }
                        var content = stream.ToArray();
                        return File(
                            content,
                            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                            "Referral Incentive Report List.xlsx"
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
