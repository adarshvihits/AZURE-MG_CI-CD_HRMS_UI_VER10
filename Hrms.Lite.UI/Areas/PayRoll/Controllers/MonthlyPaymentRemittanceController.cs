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
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.PayRoll.Controllers
{
    [Area("PayRoll")]
    [SessionAuthorize]
    public class MonthlyPaymentRemittanceController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly IMonthlyPaymentRemittanceService _service;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;

        public MonthlyPaymentRemittanceController(IDropdownService dropdownService, IMonthlyPaymentRemittanceService service, ICommonService commonService, IHelpers Helpers)
        {
            _dropdownService = dropdownService;
            _service = service;
            _commonService = commonService;
            _helpers = Helpers;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                MonthlyPaymentRemittance model = new MonthlyPaymentRemittance();
                //await fillfilterDropdown();
                //Filter filter = new Filter();
                model = await _service.GetList("PF");
                return View("~/Areas/PayRoll/Views/MonthlyPaymentRemittance/Index.cshtml", model);
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

        public async Task<ActionResult> GetPFList(string TabIndex, Filter filter)
        {

            try
            {
                if (TabIndex == "PF")
                    return PartialView("_PFList", await _service.GetList(TabIndex));
                else if (TabIndex == "ESI")
                    return PartialView("_ESIList", await _service.GetList(TabIndex));
                else if (TabIndex == "LWF")
                    return PartialView("_LWFList", await _service.GetList(TabIndex));
                else if (TabIndex == "PT")
                    return PartialView("_PTList", await _service.GetList(TabIndex));
                 else if (TabIndex == "ER")
                    return PartialView("_EmpRemittanceList", await _service.GetList(TabIndex));
                else
                    return PartialView("_TDSList", await _service.GetList(TabIndex));
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

        public async Task<IActionResult> Create(string TabIndex)
        {
            try
            {
                MonthlyPaymentRemittance model = new MonthlyPaymentRemittance();
                ViewBag.Month = new SelectList(await _dropdownService.GetMonthly_AttendanceProcessing_MonthDropdown(0), "Code", "Name");
                ViewBag.Employee = new SelectList(await _dropdownService.GetEmployeeDropDown(), "Code", "Name");
                ViewBag.RemittanceType = new SelectList(await _dropdownService.GetRemittanceTypeDropDown(), "Code", "Name");
                model.Mode = "CREATE";
                model.Active = true;
                if (TabIndex == "PF")
                    return PartialView("~/Areas/PayRoll/Views/MonthlyPaymentRemittance/_CreateOrEdit.cshtml", model);
                else if (TabIndex == "ESI")
                    return PartialView("~/Areas/PayRoll/Views/MonthlyPaymentRemittance/_CreateOrEditESI.cshtml", model);
                else if (TabIndex == "LWF")
                    return PartialView("~/Areas/PayRoll/Views/MonthlyPaymentRemittance/_CreateOrEditLWF.cshtml", model);
                else if (TabIndex == "PT")
                    return PartialView("~/Areas/PayRoll/Views/MonthlyPaymentRemittance/_CreateOrEditPT.cshtml", model);
                else if (TabIndex == "ER")
                    return PartialView("~/Areas/PayRoll/Views/MonthlyPaymentRemittance/_CreateOrEditER.cshtml", model);
                else
                    return PartialView("~/Areas/PayRoll/Views/MonthlyPaymentRemittance/_CreateOrEditTDS.cshtml", model);
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
        public async Task<ActionResult> Save(string TabIndex,MonthlyPaymentRemittance input)
        {
            try
            {
                var response = await _service.Create(TabIndex,input);
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

        public async Task<IActionResult> ExportToExcel(string TabIndex, Filter filter, int TotalRows)
        {
            MonthlyPaymentRemittance model = new MonthlyPaymentRemittance();
            var LoginUser = HttpContext.Session.GetString("LoginUser");
            var CompanyName = HttpContext.Session.GetString("CompanyName");
            var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
            var GroupCompanyName = User.authUser.GroupCompanyName;
            ExcelPassword EP = new ExcelPassword();
            EP = await _commonService.GetExcelPassword("Attrition Report List", "Excel");

            string FilterValues = await _helpers.GetFilterValues(filter);
            if (FilterValues == "")
            {
                FilterValues = "All";
            }
            model.filter = filter;

            string[] cu_Date = System.DateTime.Now.GetDateTimeFormats();
            string currDateTime = cu_Date[44];

            //Get Dataset
            var dataset = await _service.MonthlyPay_Excel(TabIndex,filter);
            DataTable dt = dataset.Tables[0];
           
           

            //set EXCEL Column Heading 

            for (int i = 0; i < dt.Columns.Count; i++)
            {
                if (dt.Columns[i].ColumnName == "PF Employee")
                    dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.ToUpper();
                if (dt.Columns[i].ColumnName == "VPF" || dt.Columns[i].ColumnName == "Pf 3.67")
                    dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.ToUpper();
                else
                    dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
                if (dt.Columns[i].ColumnName == "Name")
                    dt.Columns[i].ColumnName = "Employee Name";
            }

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Monthly Payment Remittance list");
                int count = dt.Rows.Count + 5;

                worksheet.Range("A1:L" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1:L" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1:L" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1:L" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1:L" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;

                var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 11)));
                wsReportNameHeaderRange.Style.Font.Bold = true;
                wsReportNameHeaderRange.Style.Font.FontSize = 15;
                wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                wsReportNameHeaderRange.Merge();
                wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                wsReportNameHeaderRange.Value = "Monthly Payment Remittance List";

                worksheet.Cell(2, 1).Style.Font.Bold = true;
                worksheet.Cell(2, 1).Value = "Group Company Name";
                worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                worksheet.Cell(2, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);

                worksheet.Cell(3, 1).Style.Font.Bold = true;
                worksheet.Cell(3, 1).Value = "Tab";
                worksheet.Cell(3, 2).Value = TabIndex;
                worksheet.Cell(3, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);

                worksheet.Cell(4, 1).Style.Font.Bold = true;
                worksheet.Cell(4, 1).Value = "Login User";
                worksheet.Cell(4, 2).Value = LoginUser;
                worksheet.Cell(4, 3).Style.Font.Bold = true;
                worksheet.Cell(4, 3).Value = "Date & Time";
                worksheet.Cell(4, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                worksheet.Cell(4, 4).Style.DateFormat.Format = "dd-MMM-yyyy";
                worksheet.Cell(4, 4).Value = currDateTime.ToString();
                worksheet.Cell(4, 5).Style.Font.Bold = true;
                worksheet.Cell(4, 5).Value = "Total Count";
                worksheet.Cell(4, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                worksheet.Cell(4, 6).Value = dt.Rows.Count;
                //worksheet.Cell(5, 1).Style.Font.Bold = true;
                //worksheet.Cell(5, 1).Value = "Filter Applied";
                //var filterapplied = worksheet.Range("B5:K5").Merge();

                //filterapplied.Value = FilterValues;

                var currentRow = 5;

                worksheet.Cell(currentRow, 1).InsertTable(dt);
                worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                worksheet.Range("A5:L5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                worksheet.Range("A5:L5").Style.Font.Bold = true;

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
                        book.Protect(EP.Password);
                        book.SaveToStream(stream);
                    }
                    var content = stream.ToArray();
                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "MonthlyPaymentRemittance.xlsx"
                        );
                }
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
    }
}
