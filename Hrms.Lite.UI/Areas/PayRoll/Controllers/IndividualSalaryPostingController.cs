using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.PayRoll;
using Hrms.Lite.Services.IServices.PayRoll.Reports;
using Hrms.Lite.Shared.Account;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll;
using Hrms.Lite.Shared.PayRoll.Generic.Master;
using Hrms.Lite.Shared.PayRoll.Reports;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Spire.Xls;

namespace Hrms.Lite.UI.Areas.PayRoll.Controllers
{
    [Area("PayRoll")]
    [SessionAuthorize]
    public class IndividualSalaryPostingController : Controller
    {
        private readonly ISalaryPostingService _service;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        private readonly IDropdownService _dropdownService;       
        public IndividualSalaryPostingController(ISalaryPostingService service, ICommonService commonService, IHelpers Helpers, IDropdownService dropdownService)
        {
            _service = service;
            _commonService = commonService;
            _helpers = Helpers;
            _dropdownService = dropdownService;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                ViewBag.month = new SelectList(await _dropdownService.GetIndividualSalaryPostingMonthDropdown(0), "Code", "Name");
                ViewBag.Pgroup = new SelectList(await _dropdownService.GetPayrollGroupDropdown(), "Code", "Name");
                SalaryPosting model = new SalaryPosting();
                Filter filter = new Filter();
                model.Filter = new Filter();
                filter.length = 10;
                model = await _service.GetIndividualSalaryPostingList(0, 0, filter);
                model.PayrollMonth = new PayrollMonth();
                model.Filter = await _commonService.GetFilterDropdowns();
                var currMonth = await _commonService.GetCurrentMonthDropdown();
                model.PayrollMonth.Code = currMonth.Data;
                return View("Index", model);
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
        public async Task<IActionResult> GetSalaryReportDraftList(Filter filter1, int Month, int PayrollGroup)
        {
            try
            {
                SalaryPosting model = new SalaryPosting();
                Filter filter = new Filter();                

                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }
                if (Month != 0)
                    model = await _service.GetIndividualSalaryPostingList(Month, PayrollGroup, filter1);


                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.SalaryPostList == null || model.SalaryPostList.Count() == 0)
                {


                    return Json(new
                    {
                        data = "",
                        draw = filter1.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows
                    });
                }




                if (model.SalaryPostList.Count > 0)
                {
                    totalrows = model.SalaryPostList[0].active;


                }

                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = model.SalaryPostList[0].totalRecords;
                    return Json(new
                    {
                        data = model.SalaryPostList,
                        draw = filter1.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows//totalrowsafterfiltering
                    });
                }
                else
                {
                    return Json(new
                    {
                        data = model.SalaryPostList,
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

        public async Task<IActionResult> SalaryReportSplitupDetails(Guid EmployeeGI, int SalaryPeriod, int PayrollGroup,int EmployeeCode)
        {

            NetPaySplitUp model = new NetPaySplitUp();

            //model.EmployeeGuid = EmployeeGI;


            model = await _service.GetIndividualPostingSplitUpDetails(EmployeeGI, SalaryPeriod, PayrollGroup);

            model.EmployeeHeader = new EmployeeHeader();
            model.EmployeeHeader.EmployeeCode = EmployeeCode;
            return PartialView("_SalarySplitUpPopUp", model);

        }


        public static string ConvertSubstringToUpperCase(string originalString, int startIndex, int length)
        {
            string upperCaseSubstring = originalString.Substring(startIndex, length).ToUpper();
            return originalString.Substring(0, startIndex) + upperCaseSubstring + originalString.Substring(startIndex + length);
        }
        [HttpGet]
        public async Task<JsonResult> GetMonth(int id)
        {
            var Month = new SelectList(await _dropdownService.GetIndividualSalaryPostingMonthDropdown(id), "Code", "Name");
            return Json(Month);
        }
        public async Task<IActionResult> SalaryPosting(int EmployeeCode, int SalaryPeriodCode)
        {
            try {
                var response = await _service.IndividualSalaryPosting(SalaryPeriodCode, EmployeeCode);
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
        public async Task<IActionResult> ExportToExcel(int Month, int PayrollGroup, string MonthName, string PayrollGroupName, int totRecords,Filter filter)
        {
            try
            {
                
                filter.length = totRecords;
                filter.start = 0;
                string FilterValues = await _helpers.GetFilterValues(filter);

                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Individual Salary Posting", "Excel");

                if (FilterValues == "")
                {
                    FilterValues = "All";
                }
                string currDateTime = await _helpers.GetCurrentDate();
                

                int index = 0;
                //Get Dataset
                var dataset = await _service.GetIndividualSalaryPostingList_Excel(Month, PayrollGroup, filter);
                DataTable dt = dataset.Tables[0];

                //set EXCEL Column Heading 
                for (int i = 0; i < dt.Columns.Count; i++)
                {

                    if (dt.Columns[i].ColumnName.Contains("id"))
                    {
                        index = dt.Columns[i].ColumnName.IndexOf("id");
                        dt.Columns[i].ColumnName = ConvertSubstringToUpperCase(dt.Columns[i].ColumnName, index, 2);
                    }


                    else
                        dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
                }

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Individual Salary Posting");
                    int count = dt.Rows.Count + 5;
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
                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191); ;
                    wsReportNameHeaderRange.Value = "Individual Salary Posting";

                    worksheet.Cell(2, 1).Style.Font.Bold = true;
                    worksheet.Cell(2, 1).Value = "Group Company Name";
                    worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                    worksheet.Cell(2, 3).Style.Font.Bold = true;
                    worksheet.Cell(2, 3).Value = "Month";
                    worksheet.Cell(2, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(2, 4).Style.DateFormat.Format = "MMM-yyyy";
                    worksheet.Cell(2, 4).Value = MonthName;
                    worksheet.Cell(2, 5).Value = "Payroll Group";
                    worksheet.Cell(2, 5).Style.Font.Bold = true;
                    worksheet.Cell(2, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(2, 6).Value = PayrollGroupName;

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
                    worksheet.Range("B4:" + colName + "4").Merge();
                    worksheet.Cell("B4").Value = FilterValues;



                    var currentRow = 5;

                    worksheet.Cell(currentRow, 1).InsertTable(dt);
                    worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                    worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                    worksheet.Range("A5:" + colName + "5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Range("A5:" + colName + "5").Style.Font.Bold = true;


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
                            "Individual Salary Posting.xlsx"
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
