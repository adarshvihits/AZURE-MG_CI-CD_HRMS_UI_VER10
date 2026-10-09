using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.TimeOffice.Report;
using Hrms.Lite.Shared.Account;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TimeOffice.Generic;
using Hrms.Lite.Shared.TimeOffice.Report;
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

namespace Hrms.Lite.UI.Areas.TimeOffice.Report.Controllers
{ 
    [Area("TimeOffice")]
    [SessionAuthorize]  
    public class LeaveApplicationReportController : Controller
    {

        private readonly IDropdownService _dropdownService;
        private readonly ILeaveApplicationReportService _leaveapplicationService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        public LeaveApplicationReportController(ILeaveApplicationReportService service, IDropdownService dropdownService, ICommonService commonService, IHelpers Helpers)
        {
            _leaveapplicationService = service;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = Helpers;
        }
        public static class Global
        {
            public static Filter GlobalFilter;
        }
        public async Task<IActionResult> Index()
        {
            LeaveApplicationReport model = new LeaveApplicationReport();
            await fillfilterDropdown();
            Filter filter = new Filter();
            var response = await _commonService.GetCurrentLeavePeriod();
            int currntLp = Convert.ToInt32(response.Data);
            ViewBag.LeavePeriod = new SelectList(await _dropdownService.GetLeavePeriodFilterDropdown(), "Code", "Name", response.Data);      
          
            return View("~/Areas/TimeOffice/Report/Views/LeaveApplicationReport/Index.cshtml",model);
        
        }
       



        [HttpPost]
        public async Task<IActionResult> GetLeaveApplicationList(Filter filter1, int leaveperiod, string TabIndex,string fromDate, string toDate, string Type)
        {
            try
            {
                LeaveApplicationReport model = new LeaveApplicationReport();
                LeaveApplicationReport m = new LeaveApplicationReport();
         



                Filter filter = new Filter();
                Global.GlobalFilter = filter1;


                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }
                model = await _leaveapplicationService.GetLeaveApplicationList(leaveperiod, TabIndex,fromDate,toDate,Type,filter1);
                m.LeaveApplicationReportList = model.LeaveApplicationReportList;
                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.LeaveApplicationReportList == null || model.LeaveApplicationReportList.Count() == 0)
                {
                    return Json(new
                    {
                        data = "",
                        draw = filter1.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        tabcount = model,
                        recordsFiltered = totalrows
                    });
                }

                if (model.LeaveApplicationReportList.Count > 0)
                {
                    totalrows = m.LeaveApplicationReportList[0].active;               
                }


                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = m.LeaveApplicationReportList[0].totalRecords;
               
                    return Json(new
                    {
                        data = model.LeaveApplicationReportList,
                        draw = filter1.draw,
                        recordsTotal = totalrows,
                        tabcount=model,
                        recordsFiltered = totalrows//totalrowsafterfiltering
                    });
                }
                else
                {                   
                    return Json(new
                    {
                        data = model.LeaveApplicationReportList,
                        draw = filter1.draw,
                        recordsTotal = totalrows,
                        tabcount = model,

                        recordsFiltered = totalrows
                    });
                }


            }
            catch (Exception ex)
            {
                return View();
            }




        }


        public async Task<IActionResult>GetApplicationTracker(Guid EmployeeGI,Guid LeaveApplicationGI, string LeaveType)
        {
            try
            {
            LeaveApplicationReport model = new LeaveApplicationReport();     
            model = await _leaveapplicationService.GetTrackerDetails(LeaveApplicationGI, LeaveType);
            model.LeaveApplication.EmployeeHeader = await _commonService.GetEmployeeHeader(EmployeeGI);
            return PartialView("~/Areas/TimeOffice/Report/Views/LeaveApplicationReport/_LeaveApplicationTracker.cshtml",model);
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
        public async Task<IActionResult> ApplyFilter(Filter filter,  int leaveperiod, string Tab,string fromDate, string Type, string toDate)
        {
            try
            {

                LeaveApplicationReport model = new LeaveApplicationReport();
                Global.GlobalFilter = new Filter();
                model = await _leaveapplicationService.GetLeaveApplicationList(leaveperiod, Tab,fromDate,toDate,Type,filter);
                return PartialView("~/Areas/TimeOffice/Report/Views/LeaveApplicationReport/_LeaveApplicationReportList.cshtml", model);
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


        public async Task<IActionResult> ExportToExcel( string TabIndex, int leaveperiod,string leave,int TotalRows,string fromDate,string toDate, string Type)
        {
            try
            {              
                Filter filter = new Filter();
                if (Global.GlobalFilter == null)
                    Global.GlobalFilter = new Filter();
                else
                {
                    Global.GlobalFilter.length = TotalRows;
                    Global.GlobalFilter.start = 0;
                }           
          
              
                string FilterValues = await _helpers.GetFilterValues(Global.GlobalFilter);
                if (FilterValues == "")
                {
                    FilterValues = "All";
                }
                string currDateTime = await _helpers.GetCurrentDate();
                
                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Leave Application Report List (" + TabIndex + ")", "Excel");

                //Get Dataset
                var Dataset = await _leaveapplicationService.LeaveApplicationReport_Excel(leaveperiod, TabIndex, fromDate,toDate,Type,Global.GlobalFilter);
                DataTable dt = new DataTable();




                Dataset.Tables[0].Rows.RemoveAt(0);
                if ((Dataset.Tables[0]?.Rows?.Count ?? 0) > 0)
                {
                    dt = Dataset.Tables[0];
                }






                //set EXCEL Column Heading 
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    
                        dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
                }


                using (var workbook = new XLWorkbook())
                    {

                        var worksheet = workbook.Worksheets.Add("Leave Application Report  List");

                        int PendingCountnum =dt.Rows.Count + 5;

                        worksheet.Range("A1:Q" + PendingCountnum).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:Q" + PendingCountnum).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:Q" + PendingCountnum).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:Q" + PendingCountnum).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:Q" + PendingCountnum).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 16)));
                        wsReportNameHeaderRange.Style.Font.Bold = true;
                        wsReportNameHeaderRange.Style.Font.FontSize = 15;
                        wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                        wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        wsReportNameHeaderRange.Merge();


                        wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                        wsReportNameHeaderRange.Style.Font.FontSize = 15;
                        wsReportNameHeaderRange.Value = "Leave Application " + "(" + TabIndex + ") List ";
                        worksheet.Range("A5:Q5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                        worksheet.Cell(2, 1).Style.Font.Bold = true;
                        worksheet.Cell(2, 1).Value = "Group Company Name";
                        worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                        worksheet.Cell(2, 3).Style.Font.Bold = true;
                        worksheet.Cell(2, 3).Value = "Type";
                        worksheet.Cell(2, 4).Value = Type;
                        //worksheet.Cell(2, 5).Style.Font.Bold = true;
                        //worksheet.Cell(2, 5).Value = "Type";
                        //worksheet.Cell(2, 6).Value = Type;


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
                        var filterapplied = worksheet.Range("B4:Q4").Merge();

                        filterapplied.Value = FilterValues;

                        var currentRow = 5;

                    worksheet.Cell(currentRow, 1).InsertTable(dt);
                    worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                    worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                    worksheet.Range("A5:Q5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Range("A5:Q5").Style.Font.Bold = true;



                    worksheet.Columns().AdjustToContents();
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
                                "Leave Application Report List " + TabIndex + " List.xlsx"
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

