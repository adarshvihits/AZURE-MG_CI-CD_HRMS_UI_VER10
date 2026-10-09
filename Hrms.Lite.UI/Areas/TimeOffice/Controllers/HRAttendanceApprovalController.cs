using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.TimeOffice;
using Hrms.Lite.Shared.Account;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TimeOffice;
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

namespace Hrms.Lite.UI.Areas.TimeOffice.Controllers
{
    [Area("TimeOffice")]
    [SessionAuthorize]
    public class HRAttendanceApprovalController : Controller
    {
        private readonly IHRAttendanceApprovalService _hRAttendanceApprovalService;
        private readonly IDropdownService _dropdownService;
        private readonly IHelpers _helpers;
        private readonly ICommonService _commonService;
        public HRAttendanceApprovalController(IHRAttendanceApprovalService hRAttendanceApprovalService, IDropdownService dropdownService, ICommonService commonService, IHelpers helpers)
        {
            _hRAttendanceApprovalService = hRAttendanceApprovalService;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = helpers;
        }
      
        public async Task<IActionResult> Index(string FirstTab = "All")
        {
            try { 
            HRAttendanceApproval model = new HRAttendanceApproval();            
       
            ViewBag.AuthorityLevel = new SelectList(await _dropdownService.GetRequestLevelDropDown(0), "Code", "Name");             
            ViewBag.Authority = new SelectList(await _dropdownService.HRAttendanceApprovalListAuthDropdown("ALL",0,0,0), "Code", "Name");
                Filter filter = new Filter();
                filter.length = 10;
                model = await _hRAttendanceApprovalService.HRAttendanceApprovalList(" ", 0, 0, 0, 0,0, filter);
                await fillfilterDropdown();
            model.FirstTab = FirstTab;
        
            ViewBag.PayrollGroup = new SelectList(await _dropdownService.GetPayrollGroupWithGrpCmpyCheckDropdown(), "Code", "Name");
            ViewBag.Month = new SelectList(await _dropdownService.GetAttendanceUnLockMonthDropdown(0), "Code", "Name");
       
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
       
        public async Task<IActionResult> PREVIOUSHISTORY()
        {
            try
            {
                HRAttendanceApproval model = new HRAttendanceApproval();                   
                ViewBag.Month = new SelectList(await _dropdownService.GetMonthDropdown(), "Code", "Name");               
                var currMonth = await _commonService.GetCurrentMonthDropdown();
                model.Month = new MasterBase();
                model.Month.Code = currMonth.Data;
                await fillfilterDropdown();
                return View("History_Index", model);
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
        public async Task<IActionResult> GetList(Filter filter1,string TabIndex,  int monthCode, int level,int AuthorityCode,int PayrollCode, HRAttendanceApproval _model)
        {
            try
            {
                HRAttendanceApproval model = new HRAttendanceApproval();          


                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }
               
               model = await _hRAttendanceApprovalService.HRAttendanceApprovalList(TabIndex, 0, level, monthCode,AuthorityCode, PayrollCode, filter1);
                if (TabIndex != "PREVIOUS_HISTORY")
                {
                    if (_model.HRAttendanceApprovalList != null && _model.HRAttendanceApprovalList.Count != 0)
                    {
                        for (int i = 0; i < model.HRAttendanceApprovalList.Count; i++)
                        {
                            for (int j = 0; j < _model.HRAttendanceApprovalList.Count; j++)
                            {
                                if (model.HRAttendanceApprovalList[i].LeaveApplicationGI == _model.HRAttendanceApprovalList[j].LeaveApplicationGI)
                                {
                                    
                                    model.HRAttendanceApprovalList[i].Applicable = _model.HRAttendanceApprovalList[j].Applicable;
                          
                                }
                            }
                        }
                    }
                }
                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.HRAttendanceApprovalList == null || model.HRAttendanceApprovalList.Count() == 0)
                {                  


                    return Json(new
                    {
                        data = "",
                        draw = filter1.draw,
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows,
                          PreviousHistoryCount = model.PreviousHistoryCount,
                        AttendanceRegularizationCount = model.AttendanceRegularizationCount,
                        PermissionHoursCount = model.PermissionHoursCount,
                        OnDutyCount = model.OnDutyCount,
                        LeaveCount = model.LeaveCount,
                        AllCount = model.AllCount,
                        WFHCount = model.WFHCount,
                        COFFCount = model.COFFCount,
                        MaternityLeaveCount = model.MaternityLeaveCount,
                        AuthorityChangeCount = model.AuthorityChangeCount
                    });
                }
                if (model.HRAttendanceApprovalList.Count > 0)
                {
                    totalrows = model.HRAttendanceApprovalList[0].active;

                }

                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = model.HRAttendanceApprovalList[0].totalRecords;
                    return Json(new
                    {
                        data = model.HRAttendanceApprovalList,
                        draw = filter1.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows,//totalrowsafterfiltering

                        PreviousHistoryCount=model.PreviousHistoryCount,
                        AttendanceRegularizationCount = model.AttendanceRegularizationCount,
                        PermissionHoursCount = model.PermissionHoursCount,
                        OnDutyCount= model.OnDutyCount,
                        LeaveCount= model.LeaveCount,
                        AllCount = model.AllCount,
                        WFHCount = model.WFHCount,
                        COFFCount = model.COFFCount,
                        MaternityLeaveCount = model.MaternityLeaveCount,
                        AuthorityChangeCount = model.AuthorityChangeCount


                    });
                }
                else
                {
                    return Json(new
                    {
                        data = model.HRAttendanceApprovalList,
                        draw = filter1.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows,
                        PreviousHistoryCount = model.PreviousHistoryCount,
                        AttendanceRegularizationCount = model.AttendanceRegularizationCount,
                        PermissionHoursCount = model.PermissionHoursCount,
                        OnDutyCount = model.OnDutyCount,
                        LeaveCount = model.LeaveCount,
                        AllCount = model.AllCount,
                        WFHCount = model.WFHCount,
                        COFFCount = model.COFFCount,
                        MaternityLeaveCount = model.MaternityLeaveCount,
                        AuthorityChangeCount = model.AuthorityChangeCount

                    });
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

     
       
        
        [HttpPost]
        public async Task<IActionResult> ApproveOrReject(HRAttendanceApproval model)
        {
            try { 
            var response= new object();

            //var list = from li in model.HRAttendanceApprovalList
            //           where li.Applicable == true
            //           select li;
            //model.HRAttendanceApprovalList = list.ToList();
             if(model.Mode=="APPROVE")
             response = await _hRAttendanceApprovalService.HRAttendanceApprove(model);
             else
            response = await _hRAttendanceApprovalService.HRAttendanceReject(model);
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
        [HttpGet]
        public async Task<JsonResult> GetAuthority(string TabIndex,int LeavePeriodCode,int Level,int Month)
        {
            var Authority = new SelectList(await _dropdownService.HRAttendanceApprovalListAuthDropdown(TabIndex, LeavePeriodCode,Level, Month), "Code", "Name");
            return Json(Authority);
        }
        public async Task<IActionResult> ExportToExcel_Approval(Filter Filter,string TabName, int totRecords, int Month,int Level, int PayrollCode)
        {
            try
            {
                Filter.length = totRecords;
                Filter.start = 0;
               
                string FilterValues = await _helpers.GetFilterValues(Filter);

                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("HRAttendance Approval List (" + TabName + ")", "Excel");

                if (FilterValues == "")
                {
                    FilterValues = "All";
                }

                string currDateTime = await _helpers.GetCurrentDate();
                

                //Get Datset
                var dataset = await _hRAttendanceApprovalService.HRAttendanceApprovalList_Excel(TabName,0, Level, Month, PayrollCode, Filter);
                DataTable dt = dataset.Tables[1];


                //set EXCEL Column Heading 
                for (int i = 0; i < dt.Columns.Count; i++)
                {                   
                    dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
                }

                IXLWorksheet worksheet;

                using (var workbook = new XLWorkbook())
                {
                    if (TabName != "PREVIOUS_HISTORY")
                         worksheet = workbook.Worksheets.Add("HRAttendanceApprovalList");
                    else
                        worksheet = workbook.Worksheets.Add("Previous History List");
                    int rowcount = dt.Rows.Count + 5;


                    string columnName = "";
                    //int Columncount = model.MainList.Header.Count + 3;
                    int Columncount = dt.Columns.Count;

                    while (Columncount > 0)
                    {
                        int modulo = (Columncount - 1) % 26;
                        columnName = Convert.ToChar('A' + modulo) + columnName;
                        Columncount = (Columncount - modulo) / 26;
                    }

                    string colName = columnName;

                    worksheet.Range("A1", colName + rowcount).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1", colName + rowcount).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1", colName + rowcount).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1", colName + rowcount).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1", colName + rowcount).Style.Border.LeftBorder = XLBorderStyleValues.Thin;


                    var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, colName));

                    wsReportNameHeaderRange.Style.Font.Bold = true;
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Merge();
                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191); ;
                    if(TabName!="PREVIOUS_HISTORY")
                    wsReportNameHeaderRange.Value = "HR Attendance ApprovalList (" + TabName + ")";else
                        wsReportNameHeaderRange.Value = "HR Attendance Previous History List";

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
                    worksheet.Range("B4:L4").Merge();
                    worksheet.Cell("B4").Value = FilterValues;



                    var currentRow = 5;

                    worksheet.Cell(currentRow, 1).InsertTable(dt);
                    worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                    worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                    worksheet.Range("A5", colName + 5).Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Range("A5", colName + 5).Style.Font.Bold = true;


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
                        if (TabName != "PREVIOUS_HISTORY")
                            return File(
                            content,
                            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                            "HRAttendance Approval List(" + TabName + ").xlsx"
                            );
                        else
 return File(
                            content,
                            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                            "HR Attendance Previos History List.xlsx"
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

        [HttpGet]
        public async Task<IActionResult> GetMonth(int PayrollGrp)
        {
            try
            {
                var Months = await _dropdownService.GetAttendanceUnLockMonthDropdown(PayrollGrp);
                return Json(Months);
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
