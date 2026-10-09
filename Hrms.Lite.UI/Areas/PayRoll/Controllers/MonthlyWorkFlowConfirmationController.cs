using ClosedXML.Excel;
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
    public class MonthlyWorkFlowConfirmationController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly IVariableAllowanceService _VariableAllowanceService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        private readonly IWebHostEnvironment _environment;
        public MonthlyWorkFlowConfirmationController(IDropdownService dropdownService, IVariableAllowanceService VariableAllowanceService, ICommonService commonService, IHelpers helpers, IWebHostEnvironment env)
        {
            _VariableAllowanceService = VariableAllowanceService;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = helpers;
            _environment = env;

        }
        public async Task<IActionResult> Index()
        {
            try
            {
                WorkFlowConfirmation model = new WorkFlowConfirmation();
                ViewBag.PayrollGroup = new SelectList(await _dropdownService.GetPayrollGroupDropdown(), "Code", "Name");
                ViewBag.Month = new SelectList(await _dropdownService.GetSalaryProcessing_MonthDropdown(0), "Code", "Name");
                ViewBag.Allowance = new SelectList(await _dropdownService.GetWorkFlowAllowanceDropDown(), "Code", "Name");
                Filter filter = new Filter();
                filter.length = 10;
                model = await _VariableAllowanceService.ConfirmationList_GEO("All", filter, 0, 0);
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

        public async Task<JsonResult> GetMonthDropDown(int PayrollGroup)
        {
            var PayrollMonth = await _dropdownService.GetSalaryProcessing_MonthDropdown(PayrollGroup);
            return Json(PayrollMonth);
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



        [HttpPost]
        public async Task<IActionResult> ConfirmationList(string Tab, Filter filter1, int Month, int AllowanceCode)
        {
            try
            {
                WorkFlowConfirmation model = new WorkFlowConfirmation();
                WorkFlowConfirmation m = new WorkFlowConfirmation();
                //model.EmployeeHeader = new EmployeeHeader();

                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }


                model = await _VariableAllowanceService.ConfirmationList_GEO(Tab, filter1, Month, AllowanceCode);

                m.WorkFlowConfirmationList = model.WorkFlowConfirmationList;
                int totalrows = 0;
                int PendingCount = 0;
                int ConfirmedCount = 0;
                int totalrowsafterfiltering = 0;
                Decimal TotalSum = 0;



                if (model == null || model.WorkFlowConfirmationList == null || model.WorkFlowConfirmationList.Count() == 0)
                {
                    ConfirmedCount = model.ConfirmedCount;
                    PendingCount = model.PendingCount;
                    m.WorkFlowConfirmationList = new List<WorkFlowConfirmation>();
                    model = new WorkFlowConfirmation();
                    model.WorkFlowConfirmationList = new List<WorkFlowConfirmation>();
                    TotalSum = m.TotalSum;
                }
                else
                {
                    m.WorkFlowConfirmationList = model.WorkFlowConfirmationList;
                    ConfirmedCount = m.WorkFlowConfirmationList[0].ConfirmedCount;
                    PendingCount = m.WorkFlowConfirmationList[0].PendingCount;
                    TotalSum = model.TotalSum;
                }



                if (model.WorkFlowConfirmationList == null || model.WorkFlowConfirmationList.Count() == 0)
                {

                    return Json(new
                    {
                        data = "",
                        draw = filter1.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows,
                        PendingCount = PendingCount,
                        ConfirmedCount = ConfirmedCount , TotalSum = model.TotalSum
                });
                }




                if (model.WorkFlowConfirmationList.Count > 0)
                {
                    totalrows = m.WorkFlowConfirmationList[0].active;

                }


                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = m.WorkFlowConfirmationList[0].totalRecords;
                    return Json(new
                    {
                        data = model.WorkFlowConfirmationList,
                        draw = filter1.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows,//totalrowsafterfiltering
                        PendingCount = PendingCount,
                        ConfirmedCount = ConfirmedCount,
                        TotalSum = model.TotalSum
                });
                }
                else
                {
                    return Json(new
                    {
                        data = model.WorkFlowConfirmationList,
                        draw = filter1.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows,
                        PendingCount = PendingCount,
                        ConfirmedCount = ConfirmedCount,
                        
                        TotalSum = model.TotalSum
                });
                }


            }
            catch (Exception ex)
            {
                return View();
            }


        }

        public async Task<IActionResult> EditUpdate(string Tab, Filter filter, int Month, decimal Amount, int EmployeeCode, Guid EmployeeGI, string Mode,int AllowanceCode)
        {
            try
            {
                WorkFlowConfirmation model = new WorkFlowConfirmation();
                model.Tab = Tab;
                model.Month = new Dropdown();
                model.Month.Code = Month;
                model.Allowance = new Dropdown();
                model.FinalAmount = Amount;
                model.EmployeeCode = EmployeeCode;
                model.EmployeeGI = EmployeeGI;
                model.Mode = Mode;
                model.Allowance.Code = AllowanceCode;
                var response = await _VariableAllowanceService.EditUpdate(model);
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
        public async Task<IActionResult> ConfirmWF(Filter filter1, int Month,int AllowanceCode)
        {
            try
            {

                var response = await _VariableAllowanceService.ConfirmWF(filter1, Month, AllowanceCode);
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



        public async Task<IActionResult> ExportToExcel(string Tab, Filter filter1, int Month, int totalRecords, string MonthName,int AllowanceCode, string Allowance)
        {
            try
            {

                filter1.length = totalRecords;
                string FilterValues = await _helpers.GetFilterValues(filter1);


                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Monthly Work Flow Allownce Confirmation (" + Tab + ")", "Excel");

                if (FilterValues == "")
                {
                    FilterValues = "All";
                }
                string currDateTime = await _helpers.GetCurrentDate();
                

                //Get Dataset
                var dataset = await _VariableAllowanceService.GetConfirmationList_Excel(Tab, filter1, Month,AllowanceCode);
                DataTable dt = dataset.Tables[0];

                if ((dataset.Tables[0]?.Rows?.Count ?? 0) > 0)
                {
                    dt = dataset.Tables[0];
                }

                //set EXCEL Column Heading 
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (dt.Columns[i].ColumnName == "doj")
                        dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.ToUpper();
                    else
                        dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
                }


                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("WorkFlow");

                    int count = dt.Rows.Count + 5;


                    worksheet.Range("A1:J" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:J" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:J" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:J" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:J" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 9)));
                    wsReportNameHeaderRange.Style.Font.Bold = true;
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Merge();
                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191); ;
                    wsReportNameHeaderRange.Value = "Monthly Work Flow Allownce Confirmation (" + Tab + ")";

                    worksheet.Cell(2, 1).Style.Font.Bold = true;
                    worksheet.Cell(2, 1).Value = "Group Company Name";
                    worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                    worksheet.Cell(2, 3).Style.Font.Bold = true;
                    worksheet.Cell(2, 3).Value = "Month";
                    worksheet.Cell(2, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(2, 4).Value = MonthName.ToString();
                    worksheet.Cell(2, 5).Style.Font.Bold = true;
                    worksheet.Cell(2, 5).Value = "Allowance";
                    worksheet.Cell(2, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(2, 6).Value = Allowance;

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
                    worksheet.Range("B4:J4").Merge();
                    worksheet.Cell("B4").Value = FilterValues;

                    var currentRow = 5;

                    worksheet.Cell(currentRow, 1).InsertTable(dt);
                    worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                    worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                    worksheet.Range("A5:J5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Range("A5:J5").Style.Font.Bold = true;



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
                            "Monthly Work Flow Allownce Confirmation (" + Tab + ").xlsx"
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
