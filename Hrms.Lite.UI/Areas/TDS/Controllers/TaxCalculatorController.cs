using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.TDS;
using Hrms.Lite.Shared.Account;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TDS;
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

namespace Hrms.Lite.UI.Areas.TDS.Controllers
{
    [Area("TDS")]
    [SessionAuthorize]
    public class TaxCalculatorController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly ITaxCalculatorService _TaxCalculatorService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        public TaxCalculatorController(ITaxCalculatorService TaxCalculatorService, IDropdownService dropdownService, ICommonService commonService, IHelpers helpers)
        {
            _TaxCalculatorService = TaxCalculatorService;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = helpers;
        }
        public async Task<IActionResult> Index()
        {
            try
            {

                TaxCalculator model = new TaxCalculator();
                var AssessmentYear = await _commonService.GetCurrentAssessmentYear();
                ViewBag.AssessmentYear = new SelectList(await _dropdownService.GetAssessmentYearDropdown(), "Code", "Name");
                model.filter = await _commonService.GetFilterDropdowns();
                model.AssessmentYr = AssessmentYear.Data;
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
        public async Task<IActionResult> GetTDSEmployeeList(Filter filter1, int AssessmentYear)
        {
            try
            {
                TaxCalculator model = new TaxCalculator();
                TaxCalculator m = new TaxCalculator();
                Filter filter = new Filter();


                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }

                if (AssessmentYear != 0)
                {
                    model = await _TaxCalculatorService.GetList(AssessmentYear, filter1);

                }
                m.TDSEmployeeList = model.TDSEmployeeList;
                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.TDSEmployeeList == null || model.TDSEmployeeList.Count() == 0)
                {


                    return Json(new
                    {
                        data = "",
                        draw = filter1.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows
                    });
                }




                if (model.TDSEmployeeList.Count > 0)
                {
                    totalrows = m.TDSEmployeeList[0].active;


                }

                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = m.TDSEmployeeList[0].totalRecords;
                    return Json(new
                    {
                        data = model.TDSEmployeeList,
                        draw = filter1.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows//totalrowsafterfiltering
                    });
                }
                else
                {
                    return Json(new
                    {
                        data = model.TDSEmployeeList,
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
        public async Task<ActionResult> GetEmployeeTaxComparisonDetails(Guid EmployeeGI, int AssessmentYear, int Parameter)
        {
            TaxCalculator model = new TaxCalculator();

            model = await _TaxCalculatorService.GetTaxComparisonDetails(EmployeeGI, AssessmentYear, Parameter);
            model.EmployeeGuid = EmployeeGI;
            model.AssessmentYr = AssessmentYear;
            model.Parameter = Parameter;
            model.EmployeeHeader = await _commonService.GetEmployeeHeader(model.EmployeeGuid);
            return View("TaxComparisonDetails", model);
        }
        public async Task<IActionResult> TdsCalculatorExporttoExcel(Guid EmployeeGI, int AssessmentYear, int Parameter)
        {
            try
            {

                Login CurrentUser = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));

                TaxCalculator model = new TaxCalculator();
                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("TDS Calculator-Old Regime", "Excel");

                string currDateTime = await _helpers.GetCurrentDate();
                

                int slNo = 0;



                int count = 5;
                string columnName = "";
                int colcount = 0;
                int Columncount = 0;
                model = await _TaxCalculatorService.GetTaxComparisonDetails(EmployeeGI, AssessmentYear, Parameter);
                model.EmployeeHeader = await _commonService.GetEmployeeHeader(EmployeeGI);


                //set EXCEL Column Heading 

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("TDS Calculator-Old Regime");
                    var worksheet2 = workbook.Worksheets.Add("TDS Calculator-New Regime");

                    colcount = model.TDSComponentSplitUpOld.Count + 5;
                    Columncount = count;
                    while (Columncount > 0)
                    {
                        int modulo = (Columncount - 1) % 26;
                        columnName = Convert.ToChar('A' + modulo) + columnName;
                        Columncount = (Columncount - modulo) / 26;
                    }

                    string colName = columnName;


                    worksheet.Range("A1", colName + colcount).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1", colName + colcount).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1", colName + colcount).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1", colName + colcount).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1", colName + colcount).Style.Border.LeftBorder = XLBorderStyleValues.Thin;

                    var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, colName));
                    wsReportNameHeaderRange.Style.Font.Bold = true;
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Merge();
                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191); ;
                    wsReportNameHeaderRange.Value = "TDS Calculator-Old Regime";

                    worksheet.Cell(2, 1).Style.Font.Bold = true;
                    worksheet.Cell(2, 1).Value = "Company Name";
                    worksheet.Cell(2, 2).Value = CompanyName.ToUpper();





                    worksheet.Cell(3, 1).Style.Font.Bold = true;
                    worksheet.Cell(3, 1).Value = "Login User";
                    worksheet.Cell(3, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(3, 2).Value = LoginUser;
                    worksheet.Cell(3, 3).Style.Font.Bold = true;
                    worksheet.Cell(3, 3).Value = "Date & Time";
                    worksheet.Cell(3, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(3, 4).Style.DateFormat.Format = "dd-MMM-yyyy   h:mm AM/PM";
                    worksheet.Cell(3, 4).Value = currDateTime.ToString();
                    worksheet.Cell(3, 5).Style.Font.Bold = true;
                    worksheet.Cell(3, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(3, 5).Value = "Total Count:" + model.TDSComponentSplitUpOld.Count;

                    worksheet.Cell(4, 1).Style.Font.Bold = true;
                    worksheet.Cell(4, 1).Value = "Employee Name";
                    worksheet.Cell(4, 2).Value = model.EmployeeHeader.EmployeeName;
                    worksheet.Cell(4, 3).Style.Font.Bold = true;
                    worksheet.Cell(4, 3).Value = "Designation";
                    worksheet.Cell(4, 4).Value = model.EmployeeHeader.Designation.Name;

                    var currentRow = 5;
                    #region Header
                    worksheet.Range("A5", colName + 5).Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 1).Value = "Sl.No.";
                    worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 2).Value = "Head";
                    worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);
                    worksheet.Cell(currentRow, 3).Value = "Processed Salary";
                    worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);
                    worksheet.Cell(currentRow, 4).Value = "Projected Salary";
                    worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);
                    worksheet.Cell(currentRow, 5).Value = "Total";




                    foreach (var item in model.TDSComponentSplitUpOld)
                    {
                        currentRow++;
                        slNo = slNo + 1;


                        worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 1).Value = slNo;
                        worksheet.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 2).Value = item.Head;
                        worksheet.Cell(currentRow, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);
                        worksheet.Cell(currentRow, 3).Value = item.ProcessedSalary;
                        worksheet.Cell(currentRow, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);
                        worksheet.Cell(currentRow, 4).Value = item.ProjectedSalary;
                        worksheet.Cell(currentRow, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);
                        worksheet.Cell(currentRow, 5).Value = item.Total;





                        worksheet.Columns(1, 20).Style.Alignment.WrapText = true;
                    }
                    #endregion
                    worksheet.Columns().AdjustToContents();
                    worksheet.Rows().AdjustToContents();


                    //-----------------------   NEW Regime Table
                    int colcountNew = 0;
                    int slNoNew = 0;
                    colcountNew = model.TDSComponentSplitUpNew.Count + 5;
                    Columncount = count;
                    while (Columncount > 0)
                    {
                        int modulo = (Columncount - 1) % 26;
                        columnName = Convert.ToChar('A' + modulo) + columnName;
                        Columncount = (Columncount - modulo) / 26;
                    }

                    string coluName = columnName;


                    worksheet2.Range("A1", coluName + colcountNew).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet2.Range("A1", coluName + colcountNew).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet2.Range("A1", coluName + colcountNew).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet2.Range("A1", coluName + colcountNew).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet2.Range("A1", coluName + colcountNew).Style.Border.LeftBorder = XLBorderStyleValues.Thin;

                    var wsReportNameHeaderRange2 = worksheet2.Range(string.Format("A{0}:{1}{0}", 1, colName));
                    wsReportNameHeaderRange2.Style.Font.Bold = true;
                    wsReportNameHeaderRange2.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange2.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    wsReportNameHeaderRange2.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange2.Merge();
                    wsReportNameHeaderRange2.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191); ;
                    wsReportNameHeaderRange2.Value = "TDS Calculator-New Regime";

                    worksheet2.Cell(2, 1).Style.Font.Bold = true;
                    worksheet2.Cell(2, 1).Value = "Company Name";
                    worksheet2.Cell(2, 2).Value = CompanyName.ToUpper();





                    worksheet2.Cell(3, 1).Style.Font.Bold = true;
                    worksheet2.Cell(3, 1).Value = "Login User";
                    worksheet2.Cell(3, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet2.Cell(3, 2).Value = LoginUser;
                    worksheet2.Cell(3, 3).Style.Font.Bold = true;
                    worksheet2.Cell(3, 3).Value = "Date & Time";
                    worksheet2.Cell(3, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet2.Cell(3, 4).Style.DateFormat.Format = "dd-MMM-yyyy   h:mm AM/PM";
                    worksheet2.Cell(3, 4).Value = currDateTime.ToString();
                    worksheet2.Cell(3, 5).Style.Font.Bold = true;
                    worksheet2.Cell(3, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet2.Cell(3, 5).Value = "Total Count:" + model.TDSComponentSplitUpNew.Count;

                    worksheet2.Cell(4, 1).Style.Font.Bold = true;
                    worksheet2.Cell(4, 1).Value = "Employee Name";
                    worksheet2.Cell(4, 2).Value = model.EmployeeHeader.EmployeeName;
                    worksheet2.Cell(4, 3).Style.Font.Bold = true;
                    worksheet2.Cell(4, 3).Value = "Designation";
                    worksheet2.Cell(4, 4).Value = model.EmployeeHeader.Designation.Name;

                    var currentRow2 = 5;
                    #region Header
                    worksheet2.Range("A5", coluName + 5).Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet2.Cell(currentRow2, 1).Style.Font.Bold = true;
                    worksheet2.Cell(currentRow2, 1).Value = "Sl.No.";
                    worksheet2.Cell(currentRow2, 2).Style.Font.Bold = true;
                    worksheet2.Cell(currentRow2, 2).Value = "Head";
                    worksheet2.Cell(currentRow2, 3).Style.Font.Bold = true;
                    worksheet2.Cell(currentRow2, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);
                    worksheet2.Cell(currentRow2, 3).Value = "Processed Salary";
                    worksheet2.Cell(currentRow2, 4).Style.Font.Bold = true;
                    worksheet2.Cell(currentRow2, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);
                    worksheet2.Cell(currentRow2, 4).Value = "Projected Salary";
                    worksheet2.Cell(currentRow2, 5).Style.Font.Bold = true;
                    worksheet2.Cell(currentRow2, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);
                    worksheet2.Cell(currentRow2, 5).Value = "Total";




                    foreach (var item in model.TDSComponentSplitUpNew)
                    {
                        currentRow2++;
                        slNoNew = slNoNew + 1;


                        worksheet2.Cell(currentRow2, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet2.Cell(currentRow2, 1).Value = slNoNew;
                        worksheet2.Cell(currentRow2, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet2.Cell(currentRow2, 2).Value = item.Head;
                        worksheet2.Cell(currentRow2, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);
                        worksheet2.Cell(currentRow2, 3).Value = item.ProcessedSalary;
                        worksheet2.Cell(currentRow2, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);
                        worksheet2.Cell(currentRow2, 4).Value = item.ProjectedSalary;
                        worksheet2.Cell(currentRow2, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);
                        worksheet2.Cell(currentRow2, 5).Value = item.Total;





                        worksheet2.Columns(1, 20).Style.Alignment.WrapText = true;
                    }
                    #endregion
                    worksheet2.Columns().AdjustToContents();
                    worksheet2.Rows().AdjustToContents();
                    //----------------------


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
                          "Tax Calculator" + ".xlsx"
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
