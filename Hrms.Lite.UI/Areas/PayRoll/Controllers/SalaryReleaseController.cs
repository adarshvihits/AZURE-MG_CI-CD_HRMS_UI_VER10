using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using ExcelDataReader;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.PayRoll;
using Hrms.Lite.Shared.Account;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Spire.Xls;

namespace Hrms.Lite.UI.Areas.PayRoll.Controllers
{
    [Area("PayRoll")]
    [SessionAuthorize]
    public class SalaryReleaseController : Controller
    {

        private readonly IDropdownService _dropdownService;
        private readonly ISalaryReleaseService _SalaryReleaseService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        private readonly IWebHostEnvironment _environment;
     
        public SalaryReleaseController(ISalaryReleaseService SalaryReleaseService, IDropdownService dropdownService, ICommonService commonService, IHelpers Helpers, IWebHostEnvironment env)
        {
            _SalaryReleaseService = SalaryReleaseService;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = Helpers;
            _environment = env;

        }
        public async Task<IActionResult> Index()
        {
            try
            {
                SalaryRelease model = new SalaryRelease();
                Filter filter = new Filter();
                filter.length = 10;
                model.EmployeeHeader = new EmployeeHeader();
                ViewBag.Payroll = new SelectList(await _dropdownService.GetPayrollGroupDropdown(), "Code", "Name");
                ViewBag.month = new SelectList(await _dropdownService.GetSalaryReleaseMonthDropdown(0), "Code", "Name");
                model = await _SalaryReleaseService.GetList(0, 0, filter);
                model.filter = await _commonService.GetFilterDropdowns();
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
        [HttpGet]
        public async Task<IActionResult> fillMonth(int PayrollGrp)
        {
            var data = await _dropdownService.GetSalaryReleaseMonthDropdown(PayrollGrp);
            return Json(data);
        }
        public async Task<IActionResult> GetList(Filter filter1, int Month, int PayrollGroupCode)
        {
            try
            {
                SalaryRelease model = new SalaryRelease();
                SalaryRelease m = new SalaryRelease();

                Filter filter = new Filter();
          

                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }

                if (Month != 0 && PayrollGroupCode != 0)
                {
                    model = await _SalaryReleaseService.GetList(Month, PayrollGroupCode, filter1);
                }
                m.SalaryReleaseList = model.SalaryReleaseList;
                int totalrows = 0;
                int totalrowsafterfiltering = 0;
                decimal totalAmt = 0;
                if (model.SalaryReleaseList == null)
                {


                    return Json(new
                    {
                        data = "",
                        draw = filter1.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows,
                        TotalAmount = totalAmt
                    });
                }

                if (model.SalaryReleaseList.Count > 0)
                {
                    totalrows = m.SalaryReleaseList[0].active;
                    totalAmt = m.SalaryReleaseList[0].TotalAmount;

                }


                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = m.SalaryReleaseList[0].totalRecords;

                    return Json(new
                    {
                        data = model.SalaryReleaseList,
                        draw = filter1.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows,//totalrowsafterfiltering
                        TotalAmount = totalAmt
                      
                    });
                }
                else
                {

                    return Json(new
                    {
                        data = model.SalaryReleaseList,
                        draw = filter1.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows,
                        TotalAmount = totalAmt
                       

                    });
                }


            }
            catch (Exception ex)
            {
                return View();
            }
         
        }
        [HttpPost]
        public async Task<IActionResult> Save(decimal payable,int Month,int PayrollGroupCode,Guid EmployeeGI,decimal Recovery,string Remarks, decimal HoldAmount)
        {
            try
            {
                SalaryRelease model = new SalaryRelease();
                model.EmployeeHeader = new EmployeeHeader();
                model.Month = Month;
                model.Payable = payable;
                model.HoldAmount = HoldAmount;
                model.PayrollGroup = PayrollGroupCode;
                model.EmployeeHeader.EmployeeGuid = EmployeeGI;
                model.Recovery = Recovery;
                model.Remarks = Remarks;
                var response = await _SalaryReleaseService.Save(model);
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
        public async Task<IActionResult> ExportToExcel(int Month, string MName, int PGroup, string Pname, int totRecords, Filter filter)
        {
            try
            {
                SalaryRelease model = new SalaryRelease();

                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Hold Salary Release", "Excel");
                int slNo = 0;
              

                string FilterValues = await _helpers.GetFilterValues(filter);
                filter.length = totRecords;
                var dataset = await _SalaryReleaseService.GetSalaryReleaseList_EXCEL(Month, PGroup, filter);
                DataTable dt = dataset.Tables[0];
                if (FilterValues == "")
                {
                    FilterValues = "All";
                }

             

                string currDateTime = await _helpers.GetCurrentDate();
                
                //set EXCEL Column Heading Format
                for (int i = 0; i < dt.Columns.Count; i++)
                {

                    dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
                    if (dt.Columns[i].ColumnName == "Doj" )
                        dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.ToUpper();
                }

                using (var workbook = new XLWorkbook())
                {

                    var worksheet = workbook.Worksheets.Add("Hold Salary Release List");


                    int count = dt.Rows.Count + 6;


                    worksheet.Range("A1:N" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:N" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:N" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:N" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:N" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;






                    var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 13)));
                    wsReportNameHeaderRange.Style.Font.Bold = true;
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Merge();
                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                    wsReportNameHeaderRange.Value = "Hold Salary Release";

                    worksheet.Cell(2, 1).Style.Font.Bold = true;
                    worksheet.Cell(2, 1).Value = "Group Company Name";
                    worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                    worksheet.Cell(2, 3).Style.Font.Bold = true;


                    worksheet.Cell(3, 1).Style.Font.Bold = true;
                    worksheet.Cell(3, 1).Value = "Payroll Group";
                    worksheet.Cell(3, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(3, 2).Style.DateFormat.Format = "MMM-yyyy";
                    worksheet.Cell(3, 2).Value = Pname;


                    worksheet.Cell(3, 3).Style.Font.Bold = true;
                    worksheet.Cell(3, 3).Value = "Month";
                    worksheet.Cell(3, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(3, 4).Style.DateFormat.Format = "MMM-yyyy";
                    worksheet.Cell(3, 4).Value = MName;


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
                    worksheet.Cell(4, 6).Value = totRecords;
                    worksheet.Cell(5, 1).Style.Font.Bold = true;
                    worksheet.Cell(5, 1).Value = "Filter Applied";
                    var filterapplied = worksheet.Range("B5:N5").Merge();

                    filterapplied.Value = FilterValues;


                    var currentRow = 6;
                    worksheet.Cell(currentRow, 1).InsertTable(dt);


                    worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                    worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                    worksheet.Range("A6:N6").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Range("A6:N6").Style.Font.Bold = true;


             

                  
                    worksheet.Columns().AdjustToContents();
                    worksheet.Rows().AdjustToContents();
                    //worksheet.Protect("123");
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
                            "Hold Salary Release List.xlsx"
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
