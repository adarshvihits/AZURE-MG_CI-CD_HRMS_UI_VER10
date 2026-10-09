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
    public class BonusProcessingController : Controller
    {

        private readonly IBonusProcessingService _service;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        private readonly IDropdownService _dropdownService;


        public BonusProcessingController(IBonusProcessingService service, ICommonService commonService, IHelpers Helpers, IDropdownService dropdownService)
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
                BonusProcessing model = new BonusProcessing();

                ViewBag.processMonth = new SelectList(await _dropdownService.GetMonthDropdown(), "Code", "Name");
                ViewBag.Period = new SelectList(await _dropdownService.GetBonusProcessingMonthDropdown(0), "Code", "Name");
                ViewBag.BonusGroup = new SelectList(await _dropdownService.GetBonusGroupDropdown(), "Code", "Name");
                Filter filter = new Filter();
                filter.length = 10;
                model = await _service.GetBonusProcessingList(0, 0, filter);
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
        public async Task<JsonResult> GetMonthDropDown(int Month)
        {

            var data = await _dropdownService.GetBonusProcessingMonthDropdown(Month);
            return Json(data);
        }
        [HttpPost]
        public async Task<IActionResult> Process(BonusProcessing Model)
        {
            try
            {
                var response = await _service.BonusProcessing(Model);
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
        [HttpGet]
        public async Task<ActionResult> GetEmployeeDetails(int EmployeeCode, int MonthCode, Guid EmployeeGI, int Bonus )
        {
            try
            {
                BonusProcessing model = new BonusProcessing();

                model = await _service.GetBonusProcessingDetails(EmployeeCode, MonthCode, Bonus);
                model.EmployeeHeader = await _commonService.GetEmployeeHeader(EmployeeGI);
                return PartialView("_DetailsPopup", model);
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
            public async Task<IActionResult> ProcessedIndex(int P_Month, int F_Month, int T_Month, int B_Group)
            {
                try
            {
                BonusProcessing model = new BonusProcessing();
                model.ProcessMonth = new Dropdown();
                model.PeriodFrom = new Dropdown();
                model.PeriodTo = new Dropdown();
                model.BonusGroup = new Dropdown();

                model.ProcessMonth.Code = P_Month;
                model.PeriodFrom.Code = F_Month;
                model.PeriodTo.Code = T_Month;
                model.BonusGroup.Code = B_Group;
                await fillfilterDropdown();           
                return View("Processing", model);
               
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
        public async Task<IActionResult> GetList(Filter filter1, BonusProcessing _model, int P_MONTH)
        {
            try
            {
                BonusProcessing model = new BonusProcessing();
                _model.ProcessMonth = new Dropdown();
                _model.ProcessMonth.Code = P_MONTH;

                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }
               
                    model = await _service.GetBonusProcessingList(_model.ProcessMonth.Code, _model.BonusGroup.Code,filter1);

                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.BonusProcessingList == null || model.BonusProcessingList.Count() == 0)
                {


                    return Json(new
                    {
                        data = "",
                        draw = filter1.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows

                    });
                }

                if (model.BonusProcessingList.Count > 0)
                {
                    totalrows = model.BonusProcessingList[0].active;
                }


                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = model.BonusProcessingList[0].totalRecords;

                    return Json(new
                    {
                        data = model.BonusProcessingList,

                        p_month = model.ProcessMonth.Name,
                        from_month = model.PeriodFrom.Name,
                        to_month = model.PeriodTo.Name,
                        bonus = model.BonusGroup.Name,
                        grid_bonus = model.BonusTotal,
                        grid_salary = model.SalaryTotal,
                        grid_lop=model.LopTotal,

                        draw = filter1.draw,
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows

                    }); ;
                }
                else
                {

                    return Json(new
                    {
                        data = model.BonusProcessingList,

                        p_month = model.ProcessMonth.Name,
                        from_month = model.PeriodFrom.Name,
                        to_month = model.PeriodTo.Name,
                        bonus = model.BonusGroup.Name,
                        grid_bonus = model.BonusTotal,
                        grid_salary = model.SalaryTotal,
                        grid_lop = model.LopTotal,

                        draw = filter1.draw,
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


        public async Task<IActionResult> ExportToexcel(Filter Filter,int Month, int Bonus, int Total)
        {
            try
            {

                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Arrear Processing Report", "Excel");


                Filter.length = Total;

                string FilterValues = await _helpers.GetFilterValues(Filter);
                if (FilterValues == "")
                {
                    FilterValues = "All";
                }
              
                //Get Dataset
                var dataset = await _service.GetBonusProcessingList_Excel(Month, Bonus, Filter);
                DataTable dt = dataset.Tables[0];

                DataTable header = dataset.Tables[1];
                //set EXCEL Column Heading 
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    
                    dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
                }


                string currDateTime = await _helpers.GetCurrentDate();
                
                using (var workbook = new XLWorkbook())
                {

                    var worksheet = workbook.Worksheets.Add("Bonus Processing Employee List");


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
                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                    wsReportNameHeaderRange.Value = "Bonus Processing Employee List";

                    worksheet.Cell(2, 1).Style.Font.Bold = true;
                    worksheet.Cell(2, 1).Value = "Group Company Name";
                    worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                    worksheet.Cell(2, 3).Style.Font.Bold = true;
                    worksheet.Cell(2, 3).Value = "Process Month";
                    worksheet.Cell(2, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);

                    var procMonth = header.Rows[0]["procMonth"];

                    if (procMonth != DBNull.Value && procMonth != null)
                    {
                        worksheet.Cell(2, 4).Value = Convert.ToDateTime(procMonth);
                        worksheet.Cell(2, 4).Style.DateFormat.Format = "MMM-yyyy";
                    }

                    worksheet.Cell(2, 5).Style.Font.Bold = true;
                    worksheet.Cell(2, 5).Value = "Period From";
                    worksheet.Cell(2, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);

                    var periodFrom = header.Rows[0]["periodFrom"];

                    if (periodFrom != DBNull.Value && periodFrom != null)
                    {
                        worksheet.Cell(2, 6).Value = Convert.ToDateTime(periodFrom);
                        worksheet.Cell(2, 6).Style.DateFormat.Format = "MMM-yyyy";
                    }



                    worksheet.Cell(2, 7).Style.Font.Bold = true;
                    worksheet.Cell(2, 7).Value = "Period From";
                    worksheet.Cell(2, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);

                    worksheet.Cell(2, 6).Value = header.Rows[0]["periodFrom"].ToString();
                    worksheet.Cell(2, 6).Style.DateFormat.Format = "MMM-yyyy";

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
                    var filterapplied = worksheet.Range("B4", colName + 4).Merge();

                    filterapplied.Value = FilterValues;


                    var currentRow = 5;

                    worksheet.Cell(currentRow, 1).InsertTable(dt);
                    worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                    worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                    worksheet.Range("A5", colName + 5).Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Range("A5", colName + 5).Style.Font.Bold = true;


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
                            "Bonus Proc Employee List.xlsx"
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
