using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.Account;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
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

namespace Hrms.Lite.UI.Areas.DataBank.Controllers
{
    [Area("DataBank")]
    [SessionAuthorize]
    public class EmployeeVsAppraisalDateController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly ICommonService _commonService;
        private readonly IEmployeeVsAppraisalDateService _service;
        private readonly IHelpers _helpers;
        public static class Global
        {
            public static Filter GlobalFilter;
        }
        public EmployeeVsAppraisalDateController(IDropdownService dropdownService, ICommonService commonService, IEmployeeVsAppraisalDateService service, IHelpers helpers)
        {
            _dropdownService = dropdownService;
            _commonService = commonService;
            _service = service;
            _helpers = helpers;
        }
        public async Task<IActionResult> Index()
        {
            try
            {

                EmployeeVsAppraisalDate model = new EmployeeVsAppraisalDate();
                if (Global.GlobalFilter == null)
                    Global.GlobalFilter = new Filter();
                Filter filter = new Filter();
                filter.length = 10;
                model = await _service.GetList(filter);
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




        [HttpPost]
        public async Task<IActionResult> GetList(Filter filter1)
        {
            try
            {
                EmployeeVsAppraisalDate model = new EmployeeVsAppraisalDate();
                EmployeeVsAppraisalDate m = new EmployeeVsAppraisalDate();




                Filter filter = new Filter();
                Global.GlobalFilter = filter1;
              
                //Global.From = filter1.F_From;
                //Global.To = filter1.F_To;


                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }
                model = await _service.GetList(filter1);
                m.EmployeeVsAppraisalDateList = model.EmployeeVsAppraisalDateList;
                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.EmployeeVsAppraisalDateList == null || model.EmployeeVsAppraisalDateList.Count() == 0)
                {

                    return Json(new
                    {
                        data = "",
                        draw = filter1.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows
                    });
                }




                if (model.EmployeeVsAppraisalDateList.Count > 0)
                {
                    totalrows = m.EmployeeVsAppraisalDateList[0].active;


                }


                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = m.EmployeeVsAppraisalDateList[0].totalRecords;
                    return Json(new
                    {
                        data = model.EmployeeVsAppraisalDateList,
                        draw = filter1.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows//totalrowsafterfiltering
                    });
                }
                else
                {
                    return Json(new
                    {
                        data = model.EmployeeVsAppraisalDateList,
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

        [HttpPost]
        public async Task<IActionResult> Create(DateTime AppraisalDate, Guid EmployeeGI)
        {
            try
            {
                EmployeeVsAppraisalDate model = new EmployeeVsAppraisalDate();
                model.AppraisalDate = AppraisalDate;
                model.EmployeeGuid = EmployeeGI;
                model.Filter = Global.GlobalFilter;
                //if (EmployeeGI == Guid.Parse("00000000-0000-0000-0000-000000000000"))
                //{
                //    var response = await _service.BatchUpdate(model);
                //    response.Message = "Updated Successfully";
                //    return Json(response);
                //}
                //else
                //{
                    var response = await _service.Create(model);
                    response.Message = "Updated Successfully";
                    return Json(response);
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

        public async Task<IActionResult> ExportToExcel(int totRecords)
        {
            try
            {

                string FilterValues = await _helpers.GetFilterValues(Global.GlobalFilter);
                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Employee Vs Appraisal Date", "Excel");

                if (FilterValues == "")
                {
                    FilterValues = "All";
                }
                string currDateTime = await _helpers.GetCurrentDate();
                


                EmployeeVsAppraisalDate model = new EmployeeVsAppraisalDate();

                model.filter = Global.GlobalFilter;
                //Get Dataset
                //var dataset = await _SalaryDetailedReportService.GetSalaryDetailedReport_Excel(Global.model1);
                var length = Global.GlobalFilter.length;
                var start = Global.GlobalFilter.start;
                Global.GlobalFilter.length = totRecords;
                Global.GlobalFilter.start = 0;
                var dataset = await _service.GetList_Excel(Global.GlobalFilter);
                DataTable dt = dataset.Tables[0];
                Global.GlobalFilter.length = length;
                Global.GlobalFilter.start = start;


                //set EXCEL Column Heading 
                //for (int i = 0; i < dt.Columns.Count; i++)
                //{
                //    if (dt.Columns[i].ColumnName == "order")
                //        dt.Columns[i].ColumnName = "Sl.No";
                //    dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
                //}


                for (int i = 0; i < dt.Columns.Count; i++)
                {

                    dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);

                }

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Employee Vs Appraisal Date");
                    int Rowcount = dt.Rows.Count + 5;

                    string columnName = "";
                    int Columncount = dt.Columns.Count;

                    while (Columncount > 0)
                    {
                        int modulo = (Columncount - 1) % 26;
                        columnName = Convert.ToChar('A' + modulo) + columnName;
                        Columncount = (Columncount - modulo) / 26;
                    }

                    string colName = columnName;
                    worksheet.Range("A1", colName + Rowcount).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1", colName + Rowcount).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1", colName + Rowcount).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1", colName + Rowcount).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1", colName + Rowcount).Style.Border.LeftBorder = XLBorderStyleValues.Thin;


                    var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, colName));
                    wsReportNameHeaderRange.Style.Font.Bold = true;
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Merge();
                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191); ;
                    wsReportNameHeaderRange.Value = "Employee Vs Appraisal Date";

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
                    worksheet.Cell(3, 6).Value = dt.Rows.Count;
                    worksheet.Cell(4, 1).Style.Font.Bold = true;
                    worksheet.Cell(4, 1).Value = "Filter Applied";
                    worksheet.Range("B4", colName + 4).Merge();
                    worksheet.Cell("B4").Value = FilterValues;


                    var currentRow = 5;
                    worksheet.Cell(currentRow, 1).InsertTable(dt);
                    worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                    worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                    worksheet.Range("A5:" + colName + "5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Range("A5:" + colName + "5").Style.Font.Bold = true;


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
                            "Employee Vs Appraisal Date.xlsx"
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
