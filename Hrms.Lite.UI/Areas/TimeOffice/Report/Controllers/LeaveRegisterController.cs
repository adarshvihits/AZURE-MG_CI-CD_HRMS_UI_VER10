using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.Services.TimeOffice.Report;
using Microsoft.AspNetCore.Mvc.Rendering;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TimeOffice.Report;
using Microsoft.AspNetCore.Http;
using ClosedXML.Excel;
using System.IO;
using Spire.Xls;
using System.Data;
using Newtonsoft.Json;
using Hrms.Lite.Shared.Account;

namespace Hrms.Lite.UI.Areas.TimeOffice.Report.Controllers
{
    [Area("TimeOffice")]
    [SessionAuthorize]
    public class LeaveRegisterController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly ILeaveRegisterService _leaveRegisterService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        public LeaveRegisterController(ILeaveRegisterService leaveRegisterService, IDropdownService dropdownService, ICommonService commonService, IHelpers helpers)
        {
            _leaveRegisterService = leaveRegisterService;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = helpers;

        }
        public static class Global
        {
            public static Filter Filter;
            public static Filter GlobalFilter;
            //public static List<LeaveRegisterReport_GridSettings> grid;
        }




        public async Task<IActionResult> Index()
        {
            try
            {
                ViewBag.LeavePeriod = new SelectList(await _dropdownService.GetLeavePeriodFilterDropdown(), "Code", "Name");
                var response = await _commonService.GetCurrentLeavePeriod();
                var currntLp = Convert.ToInt32(response.Data);
                LeaveRegister model = new LeaveRegister();
                LeaveRegister model1 = new LeaveRegister();
                model1 = await _leaveRegisterService.GetGridSettings();
                model.filter = new Filter();
                model.GridSettings = model1.GridSettings;
                //Global.grid = model.GridSettings;
                Global.Filter = new Filter();
                //model = await _leaveRegisterService.GetLeaveRegisterReports(currntLp, model);
                model.LeavePeriod = new Shared.Master.LeavePeriod();
                model.LeavePeriod.Code = currntLp;
                await fillfilterDropdown();
                return View("~/Areas/TimeOffice/Report/Views/LeaveRegister/Index.cshtml", model);
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
        public async Task<IActionResult> GetList(Filter filter1,int LeavePeriod, int TabIndex, List<LeaveRegisterReport_GridSettings> gridSettings)
        {
            try
            {
                LeaveRegister model = new LeaveRegister();
                LeaveRegister m = new LeaveRegister();
                //model.EmployeeHeader = new EmployeeHeader();




                Filter filter = new Filter();
                Global.GlobalFilter = filter1;

                m.GridSettings = new List<LeaveRegisterReport_GridSettings>();
                m.GridSettings = gridSettings;
                ///m = await _leaveRegisterService.GetGridSettings();
                //model.GridSettings.Add(new LeaveRegisterReport_GridSettings());
                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }
                model.filter = filter1;
                model.GridSettings = m.GridSettings;
                model = await _leaveRegisterService.GetLeaveRegisterReports(LeavePeriod, TabIndex, model);
               
                m.LeaveRegisterList = model.LeaveRegisterList;
                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.LeaveRegisterList == null)
                {
                    


                    return Json(new
                    {
                        data = "",
                        draw = filter1.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows
                    });
                }

                if (model.LeaveRegisterList.Count > 0)
                {
                    totalrows = m.LeaveRegisterList[0].totalRecords;

                }


                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = m.LeaveRegisterList[0].totalRecords;
 
                    return Json(new
                    {
                        data = model.LeaveRegisterList,
                        draw = filter1.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows//totalrowsafterfiltering
                    });
                }
                else
                {
                  
                    return Json(new
                    {
                        data = model.LeaveRegisterList,
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

        public IActionResult GetGridSettings()
        {
            try
            {
                LeaveRegister model = new LeaveRegister();
                //model.GridSettings = Global.grid;
                return View("~/Areas/TimeOffice/Report/Views/LeaveRegister/_GridSettingsList.cshtml", model);
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
        public async Task<IActionResult> ApplyGetGridSettings(LeaveRegister model, int LeavePeriodCode,int TabIndex)
        {
            try
            {
                if (Global.Filter == null)
                    Global.Filter = new Filter();
                model.filter = Global.Filter;
                //Global.grid = model.GridSettings;
                List<LeaveRegisterReport_GridSettings> gridSettings = model.GridSettings;
                var list = from li in model.GridSettings
                           where li.Applicability == true
                           select li;
                model.GridSettings = list.ToList();

                model = await _leaveRegisterService.GetLeaveRegisterReports(LeavePeriodCode, TabIndex, model);
                model.LeavePeriod = new Shared.Master.LeavePeriod();
                model.LeavePeriod.Code = LeavePeriodCode;
                model.GridSettings = gridSettings;           
                await fillfilterDropdown();
                ViewBag.LeavePeriod = new SelectList(await _dropdownService.GetLeavePeriodFilterDropdown(), "Code", "Name");
                return View("~/Areas/TimeOffice/Report/Views/LeaveRegister/Index.cshtml", model);
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
        public async Task<IActionResult> ApplyFilter(Filter filter, int LeavePeriodCode,int TabIndex)
        {
            try
            {
                LeaveRegister model = new LeaveRegister();
                model.filter = filter;
                //model.GridSettings = Global.grid;
                model = await _leaveRegisterService.GetLeaveRegisterReports(LeavePeriodCode, TabIndex, model);
                if (Global.Filter == null)
                    Global.Filter = new Filter();
                else
                    Global.Filter = filter;
                return PartialView("~/Areas/TimeOffice/Report/Views/LeaveRegister/_LeaveRegisterList.cshtml", model);
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




        public async Task<IActionResult> OnExportToExcelClick(LeaveRegister grid,int LeavePeriodCode, string LeavePeriodName, int totalrow,int TabIndex)
        {
            try
            {
                LeaveRegister model = new LeaveRegister();
                var list = from li in grid.GridSettings
                           where li.Applicability == true
                           select li;
                model.GridSettings = list.ToList();
                if (Global.GlobalFilter == null)
                    Global.GlobalFilter = new Filter();
                else
                {
                    Global.GlobalFilter.length = totalrow;
                    Global.GlobalFilter.start = 0;
                }
                model.filter = Global.GlobalFilter;

                string FilterValues = await _helpers.GetFilterValues(Global.GlobalFilter);

                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Leave Register Report", "Excel");

                if (FilterValues == "")
                {
                    FilterValues = "All";
                }

                string currDateTime = await _helpers.GetCurrentDate();
                

                //Get Datset
                var dataset = await _leaveRegisterService.GetLeaveRegisterReports_Excel(LeavePeriodCode, TabIndex, model);
                DataTable dt = dataset.Tables[0];


                //set EXCEL Column Heading 
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (dt.Columns[i].ColumnName == "doj" || dt.Columns[i].ColumnName == "dob" || dt.Columns[i].ColumnName == "y/s")
                        dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.ToUpper();
                    else
                        dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
                }

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Leave Register Report");
                    int count = dt.Rows.Count + 5;


                    worksheet.Range("A1:O" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:O" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:O" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:O" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:O" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 14)));
                    wsReportNameHeaderRange.Style.Font.Bold = true;
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Merge();
                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191); ;
                    wsReportNameHeaderRange.Value = "Leave Register Report";

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
                    worksheet.Range("B4:O4").Merge();
                    worksheet.Cell("B4").Value = FilterValues;



                    var currentRow = 5;

                    worksheet.Cell(currentRow, 1).InsertTable(dt);
                    worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                    worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                    worksheet.Range("A5:O5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Range("A5:O5").Style.Font.Bold = true;


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
                            "Leave Register Report List.xlsx"
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









        //public async Task<ActionResult> OnExportToExcelClick(int LeavePeriodCode, string LeavePeriodName, int totalrow)
        //{
        //    try
        //    {
        //        LeaveRegister model = new LeaveRegister();
        //        var list = from li in Global.grid
        //                   where li.Applicability == true
        //                   select li;
        //        model.GridSettings = list.ToList();

        //        if (Global.GlobalFilter == null)
        //            Global.GlobalFilter = new Filter();
        //        else
        //        {
        //            Global.GlobalFilter.length = totalrow;
        //            Global.GlobalFilter.start = 0;
        //        }

        //        //model.GridSettings = Global.grid;
        //        model.filter = Global.GlobalFilter;
        //        var LoginUser = HttpContext.Session.GetString("LoginUser");
        //        var CompanyName = HttpContext.Session.GetString("CompanyName");
        //        ExcelPassword EP = new ExcelPassword();
        //        EP = await _commonService.GetExcelPassword("Leave Register Report", "Excel");
        //        int slNo = 0;

        //        string FilterValues = await _helpers.GetFilterValues(Global.Filter);
        //        model = await _leaveRegisterService.GetLeaveRegisterReports(LeavePeriodCode, model);

        //        if (FilterValues == "")
        //        {
        //            FilterValues = "All";
        //        }
        //        string currDateTime = await _helpers.GetCurrentDate();
        //        

        //        using (var workbook = new XLWorkbook())
        //        {
        //            var worksheet = workbook.Worksheets.Add("Leave Register Report");

        //            int count = model.LeaveRegisterList.Count + 5;


        //            worksheet.Range("A1:O" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
        //            worksheet.Range("A1:O" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
        //            worksheet.Range("A1:O" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
        //            worksheet.Range("A1:O" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
        //            worksheet.Range("A1:O" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;

        //            var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 14)));
        //            wsReportNameHeaderRange.Style.Font.Bold = true;
        //            wsReportNameHeaderRange.Style.Font.FontSize = 15;
        //            wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        //            wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
        //            wsReportNameHeaderRange.Merge();
        //            wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191); ;
        //            wsReportNameHeaderRange.Value = "Leave Register Report";

        //            worksheet.Cell(2, 1).Style.Font.Bold = true;
        //            worksheet.Cell(2, 1).Value = "Company Name";
        //            worksheet.Cell(2, 3).Style.Font.Bold = true;
        //            worksheet.Cell(2, 2).Value = CompanyName.ToUpper();
        //            worksheet.Cell(2, 3).Value = "Leave Period";
        //            worksheet.Cell(2, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);

        //            worksheet.Cell(2, 4).Value = LeavePeriodName;


        //            worksheet.Cell(3, 1).Style.Font.Bold = true;
        //            worksheet.Cell(3, 1).Value = "Login User";
        //            worksheet.Cell(3, 2).Value = LoginUser;
        //            worksheet.Cell(3, 3).Style.Font.Bold = true;
        //            worksheet.Cell(3, 3).Value = "Date & Time";
        //            worksheet.Cell(3, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //            worksheet.Cell(3, 4).Style.DateFormat.Format = "dd-MMM-yyyy   h:mm AM/PM";
        //            worksheet.Cell(3, 4).Value = currDateTime.ToString();



        //            worksheet.Cell(4, 1).Style.Font.Bold = true;
        //            worksheet.Cell(4, 1).Value = "Filter Applied";
        //            worksheet.Range("B4:O4").Merge();
        //            worksheet.Cell("B4").Value = FilterValues;
        //            var currentRow = 5;
        //            #region Header
        //            worksheet.Range("A5:O5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
        //            worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
        //            worksheet.Cell(currentRow, 1).Value = "Sl.No";
        //            worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
        //            worksheet.Cell(currentRow, 2).Value = "ID";
        //            worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
        //            worksheet.Cell(currentRow, 3).Value = "Employee Name";
        //            worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
        //            worksheet.Cell(currentRow, 4).Value = "Designation";
        //            worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
        //            worksheet.Cell(currentRow, 5).Value = "Department";
        //            worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
        //            worksheet.Cell(currentRow, 6).Value = "Location";
        //            worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
        //            worksheet.Cell(currentRow, 7).Value = "Leave";
        //            worksheet.Cell(currentRow, 8).Style.Font.Bold = true;
        //            worksheet.Cell(currentRow, 8).Value = "Opening";
        //            worksheet.Cell(currentRow, 9).Style.Font.Bold = true;
        //            worksheet.Cell(currentRow, 9).Value = "CF";
        //            worksheet.Cell(currentRow, 10).Style.Font.Bold = true;
        //            worksheet.Cell(currentRow, 10).Value = "Credit";
        //            worksheet.Cell(currentRow, 11).Style.Font.Bold = true;
        //            worksheet.Cell(currentRow, 11).Value = "Debit";
        //            worksheet.Cell(currentRow, 12).Style.Font.Bold = true;
        //            worksheet.Cell(currentRow, 12).Value = "Encash";
        //            worksheet.Cell(currentRow, 13).Style.Font.Bold = true;
        //            worksheet.Cell(currentRow, 13).Value = "Taken";
        //            worksheet.Cell(currentRow, 14).Style.Font.Bold = true;
        //            worksheet.Cell(currentRow, 14).Value = "Balance";
        //            worksheet.Cell(currentRow, 15).Style.Font.Bold = true;
        //            worksheet.Cell(currentRow, 15).Value = "Applied";


        //            var groups = model.LeaveRegisterList.GroupBy(x => x.EmployeeCode);
        //            int Next_EmployeeCode = 0;
        //            int a = currentRow + 1;                   
        //            var EmpWiseLeaveRowCount = 1;
        //            var nxtEmpWiseLeaveRowCount = 6;
        //            var firstIteration = 0;
        //            var secondIteration = 0;
        //            var EmpCount = 0;
        //            foreach (var group in groups)
        //            {
        //                int rowspanCount = group.Count(); 

        //                foreach (var item in group)
        //                {
        //                    var EmployeeCode = @item.EmployeeCode;
        //                    if (EmployeeCode != Next_EmployeeCode)
        //                    {
        //                        Next_EmployeeCode = EmployeeCode;
        //                        currentRow++;
        //                        slNo++;
        //                        //a = rowspanCount + currentRow - 1;
        //                        EmpCount++;
        //                        worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //                        worksheet.Cell(currentRow, 1).Value = slNo;


        //                        //worksheet.Range(worksheet.Cell(currentRow, 7).Address, worksheet.Cell(currentRow+1, 7).Address).Style.Border.InsideBorder = XLBorderStyleValues.Dotted;


        //                        worksheet.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //                        worksheet.Cell(currentRow, 2).Value = item.EmployeeID;
        //                        worksheet.Cell(currentRow, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //                        worksheet.Cell(currentRow, 3).Value = item.EmployeeName;

        //                        worksheet.Cell(currentRow, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //                        worksheet.Cell(currentRow, 4).Value = item.Designation;
        //                        worksheet.Cell(currentRow, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //                        worksheet.Cell(currentRow, 5).Value = item.Department;

        //                        worksheet.Cell(currentRow, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //                        worksheet.Cell(currentRow, 6).Value = item.Location;
        //                        worksheet.Cell(currentRow, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);



        //                        if(firstIteration ==1)
        //                        {
        //                            if(secondIteration ==1)
        //                            {
        //                                nxtEmpWiseLeaveRowCount = nxtEmpWiseLeaveRowCount + EmpWiseLeaveRowCount;
        //                                worksheet.Range(worksheet.Cell("A" + nxtEmpWiseLeaveRowCount).Address, worksheet.Cell("A" + ((nxtEmpWiseLeaveRowCount - 1) + EmpWiseLeaveRowCount)).Address).Merge();
        //                                worksheet.Range(worksheet.Cell("A" + nxtEmpWiseLeaveRowCount).Address, worksheet.Cell("A" + ((nxtEmpWiseLeaveRowCount - 1) + EmpWiseLeaveRowCount)).Address).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        //                                worksheet.Range(worksheet.Cell("A" + nxtEmpWiseLeaveRowCount).Address, worksheet.Cell("A" + ((nxtEmpWiseLeaveRowCount - 1) + EmpWiseLeaveRowCount)).Address).Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
        //                                worksheet.Range(worksheet.Cell("B" + nxtEmpWiseLeaveRowCount).Address, worksheet.Cell("B" + ((nxtEmpWiseLeaveRowCount - 1) + EmpWiseLeaveRowCount)).Address).Merge();
        //                                worksheet.Range(worksheet.Cell("B" + nxtEmpWiseLeaveRowCount).Address, worksheet.Cell("B" + ((nxtEmpWiseLeaveRowCount - 1) + EmpWiseLeaveRowCount)).Address).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        //                                worksheet.Range(worksheet.Cell("B" + nxtEmpWiseLeaveRowCount).Address, worksheet.Cell("B" + ((nxtEmpWiseLeaveRowCount - 1) + EmpWiseLeaveRowCount)).Address).Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
        //                                worksheet.Range(worksheet.Cell("C" + nxtEmpWiseLeaveRowCount).Address, worksheet.Cell("C" + ((nxtEmpWiseLeaveRowCount - 1) + EmpWiseLeaveRowCount)).Address).Merge();
        //                                worksheet.Range(worksheet.Cell("C" + nxtEmpWiseLeaveRowCount).Address, worksheet.Cell("C" + ((nxtEmpWiseLeaveRowCount - 1) + EmpWiseLeaveRowCount)).Address).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        //                                worksheet.Range(worksheet.Cell("C" + nxtEmpWiseLeaveRowCount).Address, worksheet.Cell("C" + ((nxtEmpWiseLeaveRowCount - 1) + EmpWiseLeaveRowCount)).Address).Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
        //                                worksheet.Range(worksheet.Cell("D" + nxtEmpWiseLeaveRowCount).Address, worksheet.Cell("D" + ((nxtEmpWiseLeaveRowCount - 1) + EmpWiseLeaveRowCount)).Address).Merge();
        //                                worksheet.Range(worksheet.Cell("D" + nxtEmpWiseLeaveRowCount).Address, worksheet.Cell("D" + ((nxtEmpWiseLeaveRowCount - 1) + EmpWiseLeaveRowCount)).Address).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        //                                worksheet.Range(worksheet.Cell("D" + nxtEmpWiseLeaveRowCount).Address, worksheet.Cell("D" + ((nxtEmpWiseLeaveRowCount - 1) + EmpWiseLeaveRowCount)).Address).Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
        //                                worksheet.Range(worksheet.Cell("E" + nxtEmpWiseLeaveRowCount).Address, worksheet.Cell("E" + ((nxtEmpWiseLeaveRowCount - 1) + EmpWiseLeaveRowCount)).Address).Merge();
        //                                worksheet.Range(worksheet.Cell("E" + nxtEmpWiseLeaveRowCount).Address, worksheet.Cell("E" + ((nxtEmpWiseLeaveRowCount - 1) + EmpWiseLeaveRowCount)).Address).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        //                                worksheet.Range(worksheet.Cell("E" + nxtEmpWiseLeaveRowCount).Address, worksheet.Cell("E" + ((nxtEmpWiseLeaveRowCount - 1) + EmpWiseLeaveRowCount)).Address).Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
        //                                worksheet.Range(worksheet.Cell("F" + nxtEmpWiseLeaveRowCount).Address, worksheet.Cell("F" + ((nxtEmpWiseLeaveRowCount - 1) + EmpWiseLeaveRowCount)).Address).Merge();
        //                                worksheet.Range(worksheet.Cell("F" + nxtEmpWiseLeaveRowCount).Address, worksheet.Cell("F" + ((nxtEmpWiseLeaveRowCount - 1) + EmpWiseLeaveRowCount)).Address).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        //                                worksheet.Range(worksheet.Cell("F" + nxtEmpWiseLeaveRowCount).Address, worksheet.Cell("F" + ((nxtEmpWiseLeaveRowCount - 1) + EmpWiseLeaveRowCount)).Address).Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
        //                            }
        //                            else
        //                            {
        //                                worksheet.Range(worksheet.Cell("A6").Address, worksheet.Cell("A" + (EmpWiseLeaveRowCount + 5)).Address).Merge();
        //                                worksheet.Range(worksheet.Cell("A6").Address, worksheet.Cell("A" + (EmpWiseLeaveRowCount + 5)).Address).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        //                                worksheet.Range(worksheet.Cell("A6").Address, worksheet.Cell("A" + (EmpWiseLeaveRowCount + 5)).Address).Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
        //                                worksheet.Range(worksheet.Cell("B6").Address, worksheet.Cell("B" + (EmpWiseLeaveRowCount + 5)).Address).Merge();
        //                                worksheet.Range(worksheet.Cell("B6").Address, worksheet.Cell("B" + (EmpWiseLeaveRowCount + 5)).Address).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        //                                worksheet.Range(worksheet.Cell("B6").Address, worksheet.Cell("B" + (EmpWiseLeaveRowCount + 5)).Address).Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
        //                                worksheet.Range(worksheet.Cell("C6").Address, worksheet.Cell("C" + (EmpWiseLeaveRowCount + 5)).Address).Merge();
        //                                worksheet.Range(worksheet.Cell("C6").Address, worksheet.Cell("C" + (EmpWiseLeaveRowCount + 5)).Address).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        //                                worksheet.Range(worksheet.Cell("C6").Address, worksheet.Cell("C" + (EmpWiseLeaveRowCount + 5)).Address).Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
        //                                worksheet.Range(worksheet.Cell("D6").Address, worksheet.Cell("D" + (EmpWiseLeaveRowCount + 5)).Address).Merge();
        //                                worksheet.Range(worksheet.Cell("D6").Address, worksheet.Cell("D" + (EmpWiseLeaveRowCount + 5)).Address).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        //                                worksheet.Range(worksheet.Cell("D6").Address, worksheet.Cell("D" + (EmpWiseLeaveRowCount + 5)).Address).Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
        //                                worksheet.Range(worksheet.Cell("E6").Address, worksheet.Cell("E" + (EmpWiseLeaveRowCount + 5)).Address).Merge();
        //                                worksheet.Range(worksheet.Cell("E6").Address, worksheet.Cell("E" + (EmpWiseLeaveRowCount + 5)).Address).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        //                                worksheet.Range(worksheet.Cell("E6").Address, worksheet.Cell("E" + (EmpWiseLeaveRowCount + 5)).Address).Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
        //                                worksheet.Range(worksheet.Cell("F6").Address, worksheet.Cell("F" + (EmpWiseLeaveRowCount + 5)).Address).Merge();
        //                                worksheet.Range(worksheet.Cell("F6").Address, worksheet.Cell("F" + (EmpWiseLeaveRowCount + 5)).Address).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        //                                worksheet.Range(worksheet.Cell("F6").Address, worksheet.Cell("F" + (EmpWiseLeaveRowCount + 5)).Address).Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
        //                                secondIteration = 1;
        //                            }
        //                        }
        //                        EmpWiseLeaveRowCount = 1;
        //                        firstIteration = 1;
        //                    }
        //                    else
        //                    {                                
        //                        currentRow++;
        //                        EmpWiseLeaveRowCount++;                                

        //                    }

        //                    worksheet.Cell(currentRow, 7).Value = item.LEAVE;

        //                    worksheet.Cell(currentRow, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //                    worksheet.Cell(currentRow, 8).Value = item.OPENING;
        //                    worksheet.Cell(currentRow, 9).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //                    worksheet.Cell(currentRow, 9).Value = item.CF;
        //                    worksheet.Cell(currentRow, 10).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //                    worksheet.Cell(currentRow, 10).Value = item.CREDIT;
        //                    worksheet.Cell(currentRow, 11).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //                    worksheet.Cell(currentRow, 11).Value = item.DEBIT;
        //                    worksheet.Cell(currentRow, 12).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //                    worksheet.Cell(currentRow, 12).Value = item.SURRENDER;
        //                    worksheet.Cell(currentRow, 13).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //                    worksheet.Cell(currentRow, 13).Value = item.TAKEN;
        //                    worksheet.Cell(currentRow, 14).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //                    worksheet.Cell(currentRow, 14).Value = item.BALANCE;
        //                    worksheet.Cell(currentRow, 15).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //                    worksheet.Cell(currentRow, 15).Value = item.APPLIED;
        //                    worksheet.Columns(1, 15).Style.Alignment.WrapText = true;

        //                }                        
        //            }

        //            var LastMerge = nxtEmpWiseLeaveRowCount + EmpWiseLeaveRowCount;
        //            worksheet.Range(worksheet.Cell("A" + LastMerge).Address, worksheet.Cell("A" + ((LastMerge - 1) + EmpWiseLeaveRowCount)).Address).Merge();
        //            worksheet.Range(worksheet.Cell("A" + LastMerge).Address, worksheet.Cell("A" + ((LastMerge - 1) + EmpWiseLeaveRowCount)).Address).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        //            worksheet.Range(worksheet.Cell("A" + LastMerge).Address, worksheet.Cell("A" + ((LastMerge - 1) + EmpWiseLeaveRowCount)).Address).Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
        //            worksheet.Range(worksheet.Cell("B" + LastMerge).Address, worksheet.Cell("B" + ((LastMerge - 1) + EmpWiseLeaveRowCount)).Address).Merge();
        //            worksheet.Range(worksheet.Cell("B" + LastMerge).Address, worksheet.Cell("B" + ((LastMerge - 1) + EmpWiseLeaveRowCount)).Address).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        //            worksheet.Range(worksheet.Cell("B" + LastMerge).Address, worksheet.Cell("B" + ((LastMerge - 1) + EmpWiseLeaveRowCount)).Address).Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
        //            worksheet.Range(worksheet.Cell("C" + LastMerge).Address, worksheet.Cell("C" + ((LastMerge - 1) + EmpWiseLeaveRowCount)).Address).Merge();
        //            worksheet.Range(worksheet.Cell("C" + LastMerge).Address, worksheet.Cell("C" + ((LastMerge - 1) + EmpWiseLeaveRowCount)).Address).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        //            worksheet.Range(worksheet.Cell("C" + LastMerge).Address, worksheet.Cell("C" + ((LastMerge - 1) + EmpWiseLeaveRowCount)).Address).Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
        //            worksheet.Range(worksheet.Cell("D" + LastMerge).Address, worksheet.Cell("D" + ((LastMerge - 1) + EmpWiseLeaveRowCount)).Address).Merge();
        //            worksheet.Range(worksheet.Cell("D" + LastMerge).Address, worksheet.Cell("D" + ((LastMerge - 1) + EmpWiseLeaveRowCount)).Address).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        //            worksheet.Range(worksheet.Cell("D" + LastMerge).Address, worksheet.Cell("D" + ((LastMerge - 1) + EmpWiseLeaveRowCount)).Address).Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
        //            worksheet.Range(worksheet.Cell("E" + LastMerge).Address, worksheet.Cell("E" + ((LastMerge - 1) + EmpWiseLeaveRowCount)).Address).Merge();
        //            worksheet.Range(worksheet.Cell("E" + LastMerge).Address, worksheet.Cell("E" + ((LastMerge - 1) + EmpWiseLeaveRowCount)).Address).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        //            worksheet.Range(worksheet.Cell("E" + LastMerge).Address, worksheet.Cell("E" + ((LastMerge - 1) + EmpWiseLeaveRowCount)).Address).Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
        //            worksheet.Range(worksheet.Cell("F" + LastMerge).Address, worksheet.Cell("F" + ((LastMerge - 1) + EmpWiseLeaveRowCount)).Address).Merge();
        //            worksheet.Range(worksheet.Cell("F" + LastMerge).Address, worksheet.Cell("F" + ((LastMerge - 1) + EmpWiseLeaveRowCount)).Address).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        //            worksheet.Range(worksheet.Cell("F" + LastMerge).Address, worksheet.Cell("F" + ((LastMerge - 1) + EmpWiseLeaveRowCount)).Address).Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);

        //            worksheet.Cell(3, 5).Style.Font.Bold = true;
        //            worksheet.Cell(3, 5).Value = "Total Count";
        //            worksheet.Cell(3, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //            worksheet.Cell(3, 6).Value = EmpCount;

        //            #endregion
        //            worksheet.Columns().AdjustToContents();
        //            worksheet.Rows().AdjustToContents();

        //            using (var stream = new MemoryStream())
        //            {
        //                workbook.SaveAs(stream);
        //                if (EP.NeedPassword)
        //                {
        //                    Workbook book = new Workbook();
        //                    book.LoadFromStream(stream);
        //                    //Protect Workbook
        //                    book.Protect(EP.Password,false,false);
        //                    book.SaveToStream(stream);
        //                }
        //                var content = stream.ToArray();
        //                return File(
        //                    content,
        //                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
        //                  "Leave Register Report.xlsx"
        //                    );
        //            }
        //        }


        //    }

        //    catch (UnauthorizedAccessException e)
        //    {

        //        return RedirectToAction("Login", "Account", new { Area = "" });
        //    }
        //    catch (Exception e)
        //    {
        //        return RedirectToAction("Error", "Account", new { Area = "" });
        //    }





        //    //var workbook = new XLWorkbook();
        //    //var ws = workbook.Worksheets.Add("Defining a Range");


        //    //int i = 1;
        //    //int j=1

        //    //var range78 = ws.Range(ws.Cell(1, 1).Address, ws.Cell(4, 1).Address);
        //    //range78.Cell(1, 1).Value = " anju";
        //    //range78.Merge();

        //    //ws.Column("A").AdjustToContents();

        //    //workbook.SaveAs("DefiningRanges.xlsx");
        //    //using (var stream = new MemoryStream())
        //    //{
        //    //    workbook.SaveAs(stream);

        //    //    var content = stream.ToArray();
        //    //    return File(
        //    //        content,
        //    //        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
        //    //      "DefiningRanges.xlsx"
        //    //        );
        //    //}
        //}
    }
}

 



           