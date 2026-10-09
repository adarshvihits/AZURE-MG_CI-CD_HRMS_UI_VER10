using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.PayRoll;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Spire.Xls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.PayRoll.Controllers
{
    [Area("PayRoll")]
    [SessionAuthorize]
    public class SlabBasedDeductionConfirmtionController : Controller
    {
        private readonly ISlabBasedDeductionConfirmationService _service;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        private readonly IDropdownService _dropdownService;
        public SlabBasedDeductionConfirmtionController(ISlabBasedDeductionConfirmationService service, ICommonService commonService, IHelpers Helpers, IDropdownService dropdownService)
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
                // ViewBag.FinancialYr = new SelectList(await _dropdownService.GetFinancialYearDropdown(), "Code", "Name");
                SlabDeductionConfirmation model = new SlabDeductionConfirmation();
                model.filter = new Filter();
                Filter filter = new Filter();
                filter.length = 10;
                model = await _service.GetList(0, 0, 0, filter);
                ViewBag.DeductionMonth = new SelectList(await _dropdownService.GetSalaryProcessing_MonthDropdown(0), "Code", "Name");
                ViewBag.Deduction = new SelectList(await _dropdownService.GetDeductionSettingsDropDown(), "Code", "Name");
                await fillfilterDropdown();
                return View("~/Areas/PayRoll/Views/SlabBasedDeductionConfirmation/Index.cshtml", model);
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

     public async Task<IActionResult> GetList(int MonthCode, int DeductionCode, int Confirmstatus)
        {
            try
            {
                SlabDeductionConfirmation model = new SlabDeductionConfirmation();
                Filter filter = new Filter();
                if(MonthCode!=0 && DeductionCode!=0)
                model = await _service.GetList(MonthCode, DeductionCode, Confirmstatus, filter);
                return PartialView("~/Areas/PayRoll/Views/SlabBasedDeductionConfirmation/_List.cshtml", model);
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
        public async Task<IActionResult> GetConfirmationList(Filter filter1, int MonthCode, int  ConfirmStatus,int DeductionCode)
        {
            try
            {
                SlabDeductionConfirmation model = new SlabDeductionConfirmation();              

        
                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }
            
                if (MonthCode != 0 && DeductionCode != 0)
                {
                    model = await _service.DedConfirmationList(MonthCode, DeductionCode, ConfirmStatus, filter1);
                }
             
                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.SlabDeductionConfirmationList == null)
                {
                    //model.RequestVsAuthorityList = new List<RequestVsAuthorityList>();
                    //model.RequestVsAuthorityList.Add(new RequestVsAuthorityList());


                    return Json(new
                    {
                        data = "",
                        draw = filter1.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows
                    });
                }

                if (model.SlabDeductionConfirmationList.Count > 0)
                {
                    totalrows = model.SlabDeductionConfirmationList[0].active;

                    //m.OnBoardingList.ToList<Hrms.Lite.Shared.Master.Test>().Count; ;
                }


                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = model.SlabDeductionConfirmationList[0].totalRecords;
                    //return Json(new { data = model.EmployeeProfileList, draw = filter1.draw, recordsTotal = totalrows, recordsFiltered = totalrowsafterfiltering });
                    return Json(new
                    {
                        data = model.SlabDeductionConfirmationList,
                        draw = filter1.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows//totalrowsafterfiltering
                    });
                }
                else
                {

                    return Json(new
                    {
                        data = model.SlabDeductionConfirmationList,
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

        [HttpPost]
        public async Task<IActionResult> ConfirmAll(SlabDeductionConfirmation model)
        {
            try
            {
              
        
                var response = await _service.ConfirmAll(model);
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

        [HttpPost]
        public async Task<ActionResult> EmployeeWiseSave(int UpdatedAmount, int MonthCode, int DeductionCode, int  EmployeeCode,string Remarks)
        {
            try
            {
                SlabDeductionConfirmation model = new SlabDeductionConfirmation();
               
                model.Month = MonthCode;
                model.Amount = UpdatedAmount;
                model.DeductionCode = DeductionCode;
                model.EmployeeCode = EmployeeCode;
                model.Remarks = Remarks;

                var response = await _service.EmployeeWiseSave(model);
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



        public async Task<IActionResult> ExportToExcel(Filter filter, int MonthCode, int DeductionCode, string MonthName, string DeductionName, int Confirmstatus, string TabIndex)
        {
            try
            {
                SlabDeductionConfirmation model = new SlabDeductionConfirmation();

                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Slab Based deduction confirmation", "Excel");
                int slNo = 0;
                model = await _service.GetList(MonthCode, DeductionCode, Confirmstatus, filter);

                string currDateTime = await _helpers.GetCurrentDate();
                
                using (var workbook = new XLWorkbook())
                {

                    var worksheet = workbook.Worksheets.Add("SlabBased ded-confirmation");


                    int count = model.SlabDeductionConfirmationList.Count + 4;

                    worksheet.Range("A1:I" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:I" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:I" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:I" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:I" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 8)));
                    wsReportNameHeaderRange.Style.Font.Bold = true;
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Merge();
                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                    wsReportNameHeaderRange.Value = "Slab based deduction confirmation(" + TabIndex + ")";





                    worksheet.Cell(2, 1).Style.Font.Bold = true;
                    worksheet.Cell(2, 1).Value = "Company Name";
                    worksheet.Cell(2, 2).Value = CompanyName.ToUpper();

                    worksheet.Cell(2, 3).Style.Font.Bold = true;
                    worksheet.Cell(2, 3).Value = "Month";

                    worksheet.Cell(2, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(2, 4).Style.DateFormat.Format = "MMM yyyy";
                    worksheet.Cell(2, 4).Value = MonthName;

                    worksheet.Cell(2, 5).Style.Font.Bold = true;
                    worksheet.Cell(2, 5).Value = "Deduction";
                    worksheet.Cell(2, 6).Value = DeductionName;

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
                    worksheet.Cell(3, 6).Value = model.SlabDeductionConfirmationList.Count();


                    var currentRow = 4;
                    #region Header
                    worksheet.Range("A4:I4").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Cell(currentRow, 1).Value = "Sl.No";
                    worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 2).Value = "ID";
                    worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 3).Value = "Employee Name";
                    worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 4).Value = "Designation";
                    worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 5).Value = "Department";
                    worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 6).Value = "Location";
                    worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 7).Value = "Gross Pay";
                    worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 8).Value = "Amount";
                    worksheet.Cell(currentRow, 8).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 9).Value = "Remarks";
                    worksheet.Cell(currentRow, 9).Style.Font.Bold = true;





                    #endregion

                    #region Body
                    foreach (var item in model.SlabDeductionConfirmationList)
                    {
                        currentRow++;
                        slNo = slNo + 1;
                        worksheet.Columns().AdjustToContents();
                        worksheet.Rows().AdjustToContents();
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
                        worksheet.Cell(currentRow, 7).Value = item.GrossPay;
                        worksheet.Cell(currentRow, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 8).Value = item.Amount;
                        worksheet.Cell(currentRow, 9).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 9).Value = item.Remarks;


                        worksheet.Columns(1, 9).Style.Alignment.WrapText = true;
                        worksheet.Rows().Style.Alignment.WrapText = true;
                    }
                    #endregion
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
                            "Slab based decuction confirmation.xlsx"
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








        public async Task<IActionResult> ApplyFilter(Filter filter,int  Month,int DeductionCode, int ConfirmStatus)
        {
            try
            {
                SlabDeductionConfirmation model = new SlabDeductionConfirmation();

                model = await _service.GetList(Month, DeductionCode, ConfirmStatus, filter);

                return PartialView("~/Areas/PayRoll/Views/SlabBasedDeductionConfirmation/_List.cshtml", model);
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
