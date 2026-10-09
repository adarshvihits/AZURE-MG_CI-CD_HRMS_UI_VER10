using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.PayRoll.Reports;
using Hrms.Lite.Shared.Account;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
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
    [Area("PayRoll")]
    [SessionAuthorize]
    public class PFFormExitReportController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        private readonly IPFFormExitReportService _PFExitReport;
        public PFFormExitReportController(IDropdownService dropdownService, ICommonService commonService, IHelpers helpers, IPFFormExitReportService PFExitReport)
        {

            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = helpers;
            _PFExitReport = PFExitReport;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                PFExitReport model = new PFExitReport();
            model.Filter = new Filter();
            await fillfilterDropdown();
            ViewBag.SalaryPeriod = new SelectList(await _dropdownService.GetMonthDropdown(), "Code", "Name");
            ViewBag.PfGp = new SelectList(await _dropdownService.GetPFGroupDropdown(), "Code", "Name");
                Filter filter = new Filter();
                filter.length = 10;
                model = await _PFExitReport.GetEmployeeList(0, 0, filter);

                return View("~/Areas/PayRoll/Reports/Views/PFFormExitReport/Index.cshtml",model);
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

        public async Task<IActionResult> GetList(int SalaryPeriod,int PfGroup,Filter filter1)
        {
            try
            {
                PFExitReport model = new PFExitReport();
                PFExitReport m = new PFExitReport();
                //model.EmployeeHeader = new EmployeeHeader();

                Filter filter = new Filter();

                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }
                if(SalaryPeriod!=0 && PfGroup!=0)
                {
                    model = await _PFExitReport.GetEmployeeList(SalaryPeriod, PfGroup, filter1);
                }


                m.PFExitReportSummary = model.PFExitReportSummary;
                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.PFExitReportSummary == null || model.PFExitReportSummary.Count() == 0)
                {
                    return Json(new
                    {
                        data = "",
                        draw = filter1.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows
                    });
                }

                if (model.PFExitReportSummary.Count > 0)
                {
                    totalrows = m.PFExitReportSummary[0].active;
                }

                //model.AllTabCount = m.AllTabCount;
                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = m.PFExitReportSummary[0].totalRecords;
                    return Json(new
                    {
                        data = model.PFExitReportSummary,
                        draw = filter1.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows//totalrowsafterfiltering
                    });
                }
                else
                {
                    return Json(new
                    {
                        data = model.PFExitReportSummary,
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

        public async Task<IActionResult> ExportToExcel(Filter filter, int SalaryPeriod,int PfGroup,string SalaryMonth,string PfGroupName,int TotalRecords)
        {
            try
            {


             
                filter.start = 0;
                string FilterValues = await _helpers.GetFilterValues(filter);
                filter.length = TotalRecords;

                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("PF Exit Upload ECR Report", "Excel");

                if (FilterValues == "")
                {
                    FilterValues = "All";
                }

                string currDateTime = await _helpers.GetCurrentDate();
                

                //Get Datset
                var dataset = await _PFExitReport.GetEmployeeList_Excel(SalaryPeriod, PfGroup, filter);
                DataTable dt = dataset.Tables[0];


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
                    var worksheet = workbook.Worksheets.Add("PF Exit Upload ECR Report");
                    int count = dt.Rows.Count + 5;


                    worksheet.Range("A1:F" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:F" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:F" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:F" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:F" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 8)));
                    wsReportNameHeaderRange.Style.Font.Bold = true;
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Merge();
                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191); ;
                    wsReportNameHeaderRange.Value = "PF Exit Upload ECR Report";
                    worksheet.Cell(2, 1).Style.Font.Bold = true;
                    worksheet.Cell(2, 1).Value = "Group Company Name";
                    worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();

                    worksheet.Cell(2, 3).Style.Font.Bold = true;
                    worksheet.Cell(2, 3).Value = "Salary Period";
                    worksheet.Cell(2, 4).Value = SalaryMonth;


                    worksheet.Cell(2, 5).Style.Font.Bold = true;
                    worksheet.Cell(2, 5).Value = "PF Group";
                    worksheet.Cell(2, 6).Value = PfGroupName;                  
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
                    worksheet.Range("B4:F4").Merge();
                    worksheet.Cell("B4").Value = FilterValues;


                    var currentRow = 5;

                    worksheet.Cell(currentRow, 1).InsertTable(dt);
                    worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                    worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                    worksheet.Range("A5:I5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Range("A5:I5").Style.Font.Bold = true;


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
                            "PF Exit Upload ECR Report.xlsx"
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
        public async Task<IActionResult> ExportToNotePad(Filter filter,int SalaryPeriod, int PfGroup,int TableLength)
        {



            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            PFExitReport model = new PFExitReport();
            model.Month = SalaryPeriod;
            model.PFGroup = PfGroup;
            filter.length = TableLength;
            filter.start = 0;

            var dataset = await _PFExitReport.GetEmployeeList_Excel(SalaryPeriod, PfGroup, filter);
                DataTable dt = dataset.Tables[0];

                if (dt.Rows.Count != 0)
                {
                    //foreach (DataRow dr in dt.Rows)
                    //{
                    //    for (int i = 0; i < dt.Columns.Count; i++)
                    //    {
                    //    // Append the data from each column and ensure proper formatting
                    //    //sb.Append(dr[i].ToString().Trim());
                    //    sb.Append(dr[i].ToString().Trim() + "#~#");

                    //}

                    
                    //    sb.AppendLine();
                    //}
                foreach (DataRow dr in dt.Rows)
                {
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        sb.Append(dr[i].ToString().Trim());

                        // Append "#~#" only if it's not the last column
                        if (i < dt.Columns.Count - 1)
                        {
                            sb.Append("#~#");
                        }
                    }

                    sb.AppendLine(); // Move to the next line after each row (optional)
                }

            }

            return File(System.Text.Encoding.UTF8.GetBytes(sb.ToString()), "text/csv", "PF_Exit_ECR_Report.txt");
            
        }



    }
}
