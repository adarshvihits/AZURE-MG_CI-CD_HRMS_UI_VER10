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
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.PayRoll.Reports.Controllers
{
    [Area("PayRoll")]
    [SessionAuthorize]
    public class YearlySalaryController : Controller
    {
           private readonly IYearlySalayService _YearlySalaryService;
            private readonly ICommonService _commonService;
            private readonly IHelpers _helpers;
            private readonly IDropdownService _dropdownService;

            public static class Global
            {
                public static Filter GlobalFilter;


            }
            public YearlySalaryController(IYearlySalayService YearlySalayService, ICommonService commonService, IHelpers Helpers, IDropdownService dropdownService)
            {
                 _YearlySalaryService = YearlySalayService;
                _commonService = commonService;
                _helpers = Helpers;
                _dropdownService = dropdownService;
            }
            public async Task <IActionResult> Index()
        {
            try
            {
                var currentFinancialYr = await _commonService.GetCurrentLFinancialYear();
            ViewBag.FinYear = new SelectList(await _dropdownService.GetFinancialYearDropdown(), "Code", "Name", currentFinancialYr.Data);
            var EmpList = await _dropdownService.GetYearlySalaryEmployee(currentFinancialYr.Data);
            ViewBag.Emp = new SelectList(EmpList, "Code", "Name",EmpList[0].Code);
            YearlySalary model = new YearlySalary();
            model.FinancialYear = new FinancialYear();
            model.FinancialYear.Code = currentFinancialYr.Data;
            model.EmployeeCode = EmpList[0].Code;
                model = await _YearlySalaryService.GetYearlySalaryDetails(model);
                return View("~/Areas/PayRoll/Reports/Views/YearlySalary/Index.cshtml",model);
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
        public async Task<JsonResult> GetEmployeeVsFinancialYear(int FinancialYeaR)
        {
            var data = await _dropdownService.GetYearlySalaryEmployee(FinancialYeaR);
            return Json(data);

        }       


        public async Task<IActionResult> GetYearlySalaryDetails(int finCode,int EmpCode)
        {
            try
            {
                YearlySalary model = new YearlySalary();
                model.FinancialYear = new FinancialYear();
                model.FinancialYear.Code = finCode;
                model.EmployeeCode = EmpCode;

                if (finCode != 0 && EmpCode != 0)
                {
                    model = await _YearlySalaryService.GetYearlySalaryDetails(model);
                    model.EmployeeHeader = await _commonService.GetEmployeeCard(EmpCode);
                }
                return PartialView("~/Areas/PayRoll/Reports/Views/YearlySalary/_YearlySalaryEmployeeDetails.cshtml", model);
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

        public async Task<IActionResult> ExportToExcel(int FinYear, string FinYearName, int EmpCode)
        {
            try
            {
                YearlySalary model = new YearlySalary();

                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Yearly Salary", "Excel");
                int slNo = 0;

                model.FinancialYear = new FinancialYear();
                model.FinancialYear.Code = FinYear;
                model.EmployeeCode = EmpCode;
               
                model = await _YearlySalaryService.GetYearlySalaryDetails(model);
                model.EmployeeHeader = new Shared.General.EmployeeHeader();
                model.EmployeeHeader.Department = new Shared.Master.Department();
                model.EmployeeHeader.Designation = new Shared.Master.Designation();
                model.EmployeeHeader.Location = new Shared.Master.Location();
                model.EmployeeHeader.Category = new Shared.Master.Category();
                model.EmployeeHeader = await _commonService.GetEmployeeCard(EmpCode);
                string EmpName = model.EmployeeHeader.EmployeeName;
                string currDateTime = await _helpers.GetCurrentDate();
                
                using (var workbook = new XLWorkbook())
                {

                    var worksheet = workbook.Worksheets.Add("Yearly Salary Report");


                    int count = model.Data.Count + 4;

                    string columnName = "";
                    int Columncount = model.MainList.Header.Count;

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
                    wsReportNameHeaderRange.Value =" Yearly Salary Report";

                    worksheet.Cell(2, 1).Style.Font.Bold = true;
                    worksheet.Cell(2, 1).Value = "Group Company Name";
                    worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                    worksheet.Cell(2, 3).Style.Font.Bold = true;
                    worksheet.Cell(2, 3).Value = "Financial Year";
                    worksheet.Cell(2, 4).Value = FinYearName;
                    worksheet.Cell(2, 4).Style.DateFormat.Format = "MMM-yyyy";
                    worksheet.Cell(2, 5).Style.Font.Bold = true;
                    worksheet.Cell(2, 5).Value = "Employee Name";
                    worksheet.Cell(2, 6).Value = EmpName; 






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
                    worksheet.Cell(3, 6).Value = model.Data.Count-1;
                   

                    var currentRow = 4;
                    #region Header
                    worksheet.Range("A4", colName + 4).Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Cell(currentRow, 1).Value = "Sl.No";
                    worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 2).Value = "Name";
                    worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 3).Value = "Total";
                    worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                   
                    var a = 4;
                    for (int i = 3; i < model.MainList.Header.Count; i++)
                    {
                        worksheet.Cell(currentRow, a).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, a).Value = model.MainList.Header[i].FieldName;
                        worksheet.Cell(currentRow, a).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, a).Style.DateFormat.Format = "MMM-yyyy";
                        a++;
                    }


                    #endregion

                    #region Body
                    for (int i = 0; i < model.Data.Count; i++)
                    {
                        currentRow++;
                        slNo = slNo + 1;
                        worksheet.Columns().AdjustToContents();
                        worksheet.Rows().AdjustToContents();
                        worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 1).Value = slNo;

                        worksheet.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 2).Value = Convert.ToString(model.Data[i]["Employee"].title);
                            
                        worksheet.Cell(currentRow, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 3).Value = Convert.ToString(model.Data[i]["Employee"].total);
                        
                       

                        var b = 4;
                        for (int j = 3; j < model.MainList.Header.Count; j++)
                        {
                            worksheet.Cell(currentRow, b).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, b).Value = Convert.ToString(model.Data[i][model.MainList.Header[j].FieldName]);
                            b++;
                        }

                    }
                    #endregion
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
                            "Yearly Salary Report List.xlsx"
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
