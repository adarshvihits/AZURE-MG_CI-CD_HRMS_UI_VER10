using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.Master;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Spire.Xls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.Master.Controllers
{
    [Area("Master")]
    [SessionAuthorize]
    public class SpecialHolidayApplicabilityController : Controller
    {
         private readonly IDropdownService _dropdownService;
            private readonly ISpecialHolidayApplicabilityService _service;
            private readonly ICommonService _commonService;
            private readonly IHelpers _helpers;

            public SpecialHolidayApplicabilityController(IDropdownService dropdownService, ICommonService commonService, ISpecialHolidayApplicabilityService SpecialHolidayApplicabilityService, IHelpers helper)
            {
                _service = SpecialHolidayApplicabilityService;
                _dropdownService = dropdownService;
                _commonService = commonService;
                _helpers = helper;
            }
            public async Task<IActionResult> Index(Filter filter)
            {
                try
                {
                    SpecialHolidayApplicability model = new SpecialHolidayApplicability();
                    ViewBag.HolidayYear = new SelectList(await _dropdownService.GetCalendarYearDropdown(), "Code", "Name");
                    ViewBag.SpecialHoliday = new SelectList(await _dropdownService.GetSpecialHolidayDropdown(0), "Code", "Name");
                    //await fillfilterDropdown();
                model.Filter = await _commonService.GetFilterDropdowns();
                model = await _service.SpecialholidayEmpList(0,filter);
                return View("~/Areas/master/Views/SpecialHolidayApplicability/Index.cshtml", model);
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
        public async Task<JsonResult> GetSpecialHolidayDropdown(int TypeCode)
        {
           
          var  data = new SelectList(await _dropdownService.GetSpecialHolidayDropdown(TypeCode), "Code", "Name");
            return Json(data);
        }

        public async Task<IActionResult> GetSpecialholidayEmpList( int HolidayCode,Filter filter)
        {
            try
            {
                SpecialHolidayApplicability model = new SpecialHolidayApplicability();


                model = await _service.SpecialholidayEmpList(HolidayCode, filter);


                return PartialView("_List", model);
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
        public async Task<ActionResult> Save(SpecialHolidayApplicability input)
        {
            try
            {
                var response = await _service.Save(input);

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

        public async Task<IActionResult> ExportToExcel(string year ,string holiday ,int HolidayCode,Filter filter)
        {
            try
            {
                SpecialHolidayApplicability model = new SpecialHolidayApplicability();
                model = await _service.SpecialholidayEmpList(HolidayCode,filter);
                int slno = 0;

                string currDateTime = await _helpers.GetCurrentDate();
                
                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                ExcelPassword EP = new ExcelPassword();

                EP = await _commonService.GetExcelPassword("Meals List", "Excel");
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Meals List");
                    int count = model.SpecialHolidayEmpList.Count + 6;


                    worksheet.Range("A1:H" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:H" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:H" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:H" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:H" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 7)));
                    wsReportNameHeaderRange.Style.Font.Bold = true;
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Merge();
                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                    wsReportNameHeaderRange.Value = "Special Holiday List (" + holiday + " - " + year + ")";
                    worksheet.Range("A6:H6").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Cell(2, 1).Style.Font.Bold = true;
                    worksheet.Cell(2, 1).Value = "Company Name";
                    worksheet.Cell(2, 2).Value = CompanyName.ToUpper();
                    worksheet.Cell(3, 1).Style.Font.Bold = true;
                    worksheet.Cell(3, 1).Value = "Login User";
                    worksheet.Cell(3, 2).Value = LoginUser;
                    worksheet.Cell(4, 1).Style.Font.Bold = true;
                    worksheet.Cell(4, 1).Value = "Date & Time";
                    worksheet.Cell(4, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(4, 2).Style.DateFormat.Format = "dd-MMM-yyyy   h:mm AM/PM";
                    worksheet.Cell(4, 2).Value = currDateTime.ToString();
                    worksheet.Cell(5, 1).Style.Font.Bold = true;
                    worksheet.Cell(5, 1).Value = "Total Count";
                    worksheet.Cell(5, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(5, 2).Value = model.SpecialHolidayEmpList.Count;




                    var currentRow = 6;
                    #region Header
                    worksheet.Cell(currentRow, 1).Value = "SlNo";
                    worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 2).Value = "Id";
                    worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 3).Value = "Employee Name";
                    worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 4).Value = "Designation";
                    worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 5).Value = "Department";
                    worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 6).Value = "Location";
                    worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 7).Value = "DOJ";
                    worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 8).Value = "Applicable";
                    worksheet.Cell(currentRow, 8).Style.Font.Bold = true;

                    #endregion

                    #region Body
                    foreach (var item in model.SpecialHolidayEmpList)
                    {

                        currentRow++;
                        slno++;

                        worksheet.Cell(currentRow, 1).Value = slno;
                        worksheet.Cell(currentRow, 2).Value = item.EmpId;
                        worksheet.Cell(currentRow, 3).Value = item.EmpName;
                        worksheet.Cell(currentRow, 4).Value = item.Designation;
                        worksheet.Cell(currentRow, 5).Value = item.Department;
                        worksheet.Cell(currentRow, 6).Value = item.Location;
                        worksheet.Cell(currentRow, 7).Value = item.Doj;

                        if (item.Applicability)
                        {
                            worksheet.Cell(currentRow, 8).Value = "✔";
                            worksheet.Cell(currentRow, 8).Style.Font.FontColor = XLColor.Green;
                        }
                        else
                        {
                            worksheet.Cell(currentRow, 8).Value = "✖";
                            worksheet.Cell(currentRow, 8).Style.Font.FontColor = XLColor.Red;
                        }


                        worksheet.Cell(currentRow, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

                

                       

                        worksheet.Columns(1, 8).Style.Alignment.WrapText = true;


                    }
                    #endregion
                    worksheet.Columns().AdjustToContents();
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
                            "Special Holiday Applicability List (" + holiday + " - " + year + ").xlsx"
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
        public async Task fillfilterDropdown()
        {
            ViewBag.Gen = new SelectList(await _dropdownService.GetGenderDropDown(), "Code", "Name");
            ViewBag.Div = new SelectList(await _dropdownService.GetDivisionDropdown(), "Code", "Name");
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
