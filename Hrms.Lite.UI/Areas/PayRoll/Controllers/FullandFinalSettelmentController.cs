using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.PayRoll;
using Hrms.Lite.Shared.Account;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Spire.Xls;

namespace Hrms.Lite.UI.Areas.PayRoll.Controllers
{
    [Area("PayRoll")]
    [SessionAuthorize]
    public class FullandFinalSettelmentController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly IFullandFinalSettelmentService _service;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        public static class Global
        {
            public static Filter GlobalFilter;
        }
        public FullandFinalSettelmentController(IFullandFinalSettelmentService service, IDropdownService dropdownService, ICommonService commonService, IHelpers helpers)
        {
            _service = service;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = helpers;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                FullandFinalSettelment model = new FullandFinalSettelment();
                Filter filter = new Filter();
                filter.length = 10;
                model = await _service.GetEmployeeList_FullandFinal(filter,0,0);
                ViewBag.Month = new SelectList(await _dropdownService.GetFullAndFinalMonthDropdown(0), "Code", "Name");
                ViewBag.PayrollGroup = new SelectList(await _dropdownService.GetPayrollGroupWithGrpCmpyCheckDropdown(), "Code", "Name");
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
        public async Task<IActionResult> Create(Guid EmployeeGI,int Month)
        {
            FullandFinalSettelment model = new FullandFinalSettelment();
            FullandFinalSettelment model1 = new FullandFinalSettelment();
            FullandFinalSettelment model2 = new FullandFinalSettelment();
            model1.Details=new FullandFinalSettelment_Details();
            model1.Details.OtherAdditions = new List<FullandFinalSettelment_OtherAmounts>();
            model1.Details.OtherDeduction = new List<FullandFinalSettelment_OtherAmounts>();
            model1.HeldAmountDetaisl = new List<FullandFinalSettelment_HeldAmountDetaisl>();
            //FullandFinalSettelment_OtherAmounts list = new FullandFinalSettelment_OtherAmounts()
            //{
            //    Code = 0,
            //    Narration = "",
            //    Amount = 0

            //};
            FullandFinalSettelment_HeldAmountDetaisl list1 = new FullandFinalSettelment_HeldAmountDetaisl()
            {
                Month = "June 2022",
                Amount = 2000
            };

            //model1.Details.OtherAdditions.Add(list);
            //model1.Details.OtherDeduction.Add(list);
            //model1.HeldAmountDetaisl.Add(list1);
            model2=await _service.GetHeldAmountDetails(EmployeeGI);
            //model = await _service.GetDetails(EmployeeGI,"Create");
            model = await _service.GetDetailsFullandFinal(EmployeeGI, Month, "Create");
            //model.Details.OtherAdditions = model1.Details.OtherAdditions;
            //model.Details.OtherDeduction = model1.Details.OtherDeduction;
            model.HeldAmountDetaisl = model2.HeldAmountDetaisl;
            model.EmployeeGI = EmployeeGI;
            model.EmployeeHeader = await _commonService.GetEmployeeHeader(EmployeeGI);
            model.Month = new Dropdown();
            model.Month.Code = Month;
            return View(model);
        }
        public async Task<IActionResult> ApplyFilter(Filter filter,int PayrollGroup,int Month)
        {
            FullandFinalSettelment model = new FullandFinalSettelment();
            Global.GlobalFilter = filter;
            model = await _service.GetEmployeeList_FullandFinal(Global.GlobalFilter, PayrollGroup, Month);
            return PartialView("_EmpList", model);
        }
        [HttpGet]
        public async Task<JsonResult> GetMonth(int PayrollGrpCode)
        {
            var result = await _dropdownService.GetFullAndFinalMonthDropdown(PayrollGrpCode);
            return Json(result);
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

       
        public async Task<JsonResult> GetDetailsProcess(Guid EmployeeGI, decimal EncashmentLeaves, decimal NoticeDedDays)
        {
            FullandFinalSettelment model = new FullandFinalSettelment();
            var response= await _service.GetDetailsProcess(EmployeeGI, EncashmentLeaves, NoticeDedDays);
            return Json(response.Details);
        }

        [HttpPost]
        public async Task<IActionResult> SaveSettelment(FullandFinalSettelment model)
        {
            try
            {
                var response = await _service.SaveFullAndFinalSettelment(model);
                return Json(response);
                //if (response.Success == true)
                //{
                //    TempData["Message"] = "Saved Successfully";
                //    return RedirectToAction("Index", "HolidayWageSettings");
                //}
                //else
                //{
                //    TempData["Message"] = response.Message;

                //    return View("~/Areas/PayRoll/Views/HolidayWageSettings/_ApplicableAllowance.cshtml", model);
                //}
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

        public async Task<ActionResult> ExportToExcel(int PayrollGroup, int Month)
        {
            try
            {
                FullandFinalSettelment model = new FullandFinalSettelment();
                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Full&Final Settelment", "Excel");
                int slNo = 0;
                if (Global.GlobalFilter == null)
                    Global.GlobalFilter = new Filter();
                string FilterValues = await _helpers.GetFilterValues(Global.GlobalFilter);
                model = await _service.GetEmployeeList_FullandFinal(Global.GlobalFilter, PayrollGroup, Month);
                if (FilterValues == "")
                {
                    FilterValues = "All";
                }
                string currDateTime = await _helpers.GetCurrentDate();
                

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Full&Final Settlement" + " List");

                    int count = model.EmployeeList.Count + 5;
                    worksheet.Range("A1:G" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:G" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:G" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:G" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:G" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;

                    var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 6)));
                    wsReportNameHeaderRange.Style.Font.Bold = true;
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Merge();
                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191); ;
                    wsReportNameHeaderRange.Value = "Full & Final Settlement " + " List";

                    worksheet.Cell(2, 1).Style.Font.Bold = true;
                    worksheet.Cell(2, 1).Value = "Group Company Name";
                    worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
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
                    worksheet.Cell(3, 6).Value = model.EmployeeList.Count;
                    worksheet.Cell(4, 1).Style.Font.Bold = true;
                    worksheet.Cell(4, 1).Value = "Filter Applied";
                    worksheet.Range("B4:G4").Merge();
                    worksheet.Cell("B4").Value = FilterValues;
                    var currentRow = 5;
                    #region Header
                    worksheet.Range("A5:G5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 1).Value = "Sl.No";
                    worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 2).Value = "ID";
                    worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 3).Value = "Employee Name";
                    worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 4).Value = "Designation";
                    worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 5).Value = "Department";
                    worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 6).Value = "Location";
                    worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 7).Value = "Resignation Date";
                   

                 


                    foreach (var item in model.EmployeeList)
                    {
                        currentRow++;
                        slNo = slNo + 1;


                        worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 1).Value = slNo;
                        worksheet.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 2).Value = item.EmployeeID;
                        worksheet.Cell(currentRow, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 3).Value = item.EmployeeName;
                        worksheet.Cell(currentRow, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 4).Value = item.Designation;
                        worksheet.Cell(currentRow, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 5).Value = item.Department;
                        worksheet.Cell(currentRow, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 6).Value = item.Location;
                        worksheet.Cell(currentRow, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 7).Value = item.ResignationDate;
                        worksheet.Cell(currentRow, 7).Style.DateFormat.Format = "dd-MMM-yyyy";
                       

                        worksheet.Columns(1, 7).Style.Alignment.WrapText = true;
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
                          "Full&Final Settlement " + " List.xlsx"
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
