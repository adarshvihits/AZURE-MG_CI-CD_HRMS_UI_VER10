using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.PayRoll.Reports;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll.Generic.Master;
using Hrms.Lite.Shared.PayRoll.Reports;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using Spire.Xls;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Hrms.Lite.Shared.Account;
using Newtonsoft.Json;

namespace Hrms.Lite.UI.Areas.PayRoll.Reports.Controllers
{
    [Area("PayRoll")]
    [SessionAuthorize]
    public class SalaryReportDraftController : Controller
    {
        private readonly ISalaryReportDraftService _SalaryDraftReportService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        private readonly IDropdownService _dropdownService;
       
        public SalaryReportDraftController(ISalaryReportDraftService SalaryDraftReportService, ICommonService commonService, IHelpers Helpers, IDropdownService dropdownService)
        {
            _SalaryDraftReportService = SalaryDraftReportService;
            _commonService = commonService;
            _helpers = Helpers;
            _dropdownService = dropdownService;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                ViewBag.month = new SelectList(await _dropdownService.GetMonthDropdown(), "Code", "Name");
            ViewBag.Pgroup = new SelectList(await _dropdownService.GetPayrollGroupDropdown(), "Code", "Name");
            SalaryReportDraft model = new SalaryReportDraft();
            Filter filter = new Filter();
            model.Filter = new Filter();
            model.PayrollMonth = new PayrollMonth();
                filter.length = 10;
                model = await _SalaryDraftReportService.GetList(0, 0, filter);
                var currMonth = await _commonService.GetCurrentMonthDropdown();
            model.PayrollMonth.Code = currMonth.Data;
            model.Filter = await _commonService.GetFilterDropdowns();
            return View("~/Areas/PayRoll/Reports/Views/SalaryReportDraft/Index.cshtml", model);
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
                SalaryReportDraft model = new SalaryReportDraft();
                Filter filter = new Filter();
                

                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }
                if (Month != 0)
                    model = await _SalaryDraftReportService.GetList(Month, PayrollGroup,filter1);

              
                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.SalaryDraft == null || model.SalaryDraft.Count() == 0)
                {


                    return Json(new
                    {
                        data = "",
                        draw = filter1.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows
                    });
                }




                if (model.SalaryDraft.Count > 0)
                {
                    totalrows = model.SalaryDraft[0].active;


                }

                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = model.SalaryDraft[0].totalRecords;
                    return Json(new
                    {
                        data = model.SalaryDraft,
                        draw = filter1.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows//totalrowsafterfiltering
                    });
                }
                else
                {
                    return Json(new
                    {
                        data = model.SalaryDraft,
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

        public async Task<IActionResult> SalaryReportSplitupDetails(Guid EmployeeGI, int SalaryPeriod,int PayrollGroup)
        {

            NetPaySplitUp model = new NetPaySplitUp();
         
            //model.EmployeeGuid = EmployeeGI;


            model = await _SalaryDraftReportService.GetNetPaySplitUpDetails(EmployeeGI,SalaryPeriod,PayrollGroup);

            model.EmployeeHeader = await _commonService.GetEmployeeHeader(EmployeeGI);

            return PartialView("~/Areas/PayRoll/Reports/Views/SalaryReportDraft/_SalarySplitUpPopUp.cshtml", model);

        }


        public static string ConvertSubstringToUpperCase(string originalString, int startIndex, int length)
        {
            string upperCaseSubstring = originalString.Substring(startIndex, length).ToUpper();
            return originalString.Substring(0, startIndex) + upperCaseSubstring + originalString.Substring(startIndex + length);
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
                EP = await _commonService.GetExcelPassword("Salary Report Draft", "Excel");

                if (FilterValues == "")
                {
                    FilterValues = "All";
                }
                string currDateTime = await _helpers.GetCurrentDate();
                

                int index = 0;
                //Get Dataset
                var dataset = await _SalaryDraftReportService.GetSalaryReportDraft_Excel(Month, PayrollGroup, filter);
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
                    var worksheet = workbook.Worksheets.Add("Salary Report Draft");
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
                    wsReportNameHeaderRange.Value = "Salary Report Draft";

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
                    worksheet.Range("B4:"+ colName+"4").Merge();
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
                            book.Protect(EP.Password,false,false);
                            book.SaveToStream(stream);
                        }
                        var content = stream.ToArray();
                        return File(
                            content,
                            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                            "Salary Report Draft.xlsx"
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
