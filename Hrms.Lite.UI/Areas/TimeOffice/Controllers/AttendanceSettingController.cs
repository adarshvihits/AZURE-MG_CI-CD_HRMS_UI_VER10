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
    public class AttendanceSettingController : Controller
    {
        private readonly IAttendanceSettingService _attendanceSettingService;
        private readonly ICommonService _commonService;
        private readonly IDropdownService _dropdownService;
        private readonly IHelpers _helpers;
        public static class Global
        {
            public static Filter GlobalFilter;
        }
        public AttendanceSettingController(IAttendanceSettingService attendanceSettingService, ICommonService commonService, IDropdownService dropdownService, IHelpers Helpers)
        {
            _attendanceSettingService = attendanceSettingService;
            _commonService = commonService;
            _dropdownService = dropdownService;
            _helpers = Helpers;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
            AttendanceSetting model = new AttendanceSetting();
            ViewBag.Classification = new SelectList(await _dropdownService.GetPunchingMethodDropdown(), "Code", "Name");            
            await fillfilterDropdown();
                Filter filter = new Filter();
                filter.length = 10;
                model.Type = "Assigned";
                model = await _attendanceSettingService.AttendanceSettingList(0, 0, 0, filter);
                // model = await _attendanceSettingService.AttendanceSettingList(0, 0, filter);
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
         public async Task<IActionResult> GetList(int groupbyCode, int ClassificationCode,int TypeCode)
        {
            try
            {
                AttendanceSetting model = new AttendanceSetting();
            Filter filter = new Filter();
            model = await _attendanceSettingService.AttendanceSettingList(groupbyCode,ClassificationCode, TypeCode,filter);
            return View("_List", model);
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
        public async Task<IActionResult> GetAttendanceList(Filter filter1, int groupbyCode, int ClassificationCode,int TypeCode)
        {
            try
            {
                AttendanceSetting model = new AttendanceSetting();
                AttendanceSetting m = new AttendanceSetting();
                //model.EmployeeHeader = new EmployeeHeader();
                Filter filter = new Filter();
                Global.GlobalFilter = filter1;


                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }

                model = await _attendanceSettingService.AttendanceSettingList(groupbyCode, ClassificationCode, TypeCode, filter1);
                m.AttendanceSettingList = model.AttendanceSettingList;
                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.AttendanceSettingList == null)
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

                if (model.AttendanceSettingList.Count > 0)
                {
                    totalrows = m.AttendanceSettingList[0].active;

                    //m.OnBoardingList.ToList<Hrms.Lite.Shared.Master.Test>().Count; ;
                }


                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = m.AttendanceSettingList[0].totalRecords;
                    //return Json(new { data = model.EmployeeProfileList, draw = filter1.draw, recordsTotal = totalrows, recordsFiltered = totalrowsafterfiltering });
                    return Json(new
                    {
                        data = model.AttendanceSettingList,
                        draw = filter1.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows//totalrowsafterfiltering
                    });
                }
                else
                {
                    // return Json(new { data = model.EmployeeProfileList, draw = filter1.draw, recordsTotal = totalrows, recordsFiltered = totalrows });
                    return Json(new
                    {
                        data = model.AttendanceSettingList,
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

        [HttpGet]
        public async Task<JsonResult> GetDropDown(int id)
        {
            SelectList data = new SelectList("", "Code", "Name");
            switch (id)
            {
                case 0:
                     data = new SelectList(await _dropdownService.GetPunchingMethodDropdown(), "Code", "Name");
                    break;
                case 1:
                    data = new SelectList(await _dropdownService.GetShiftTypeDropDown(), "Code", "Name");
                    break;
                case 2:
                    data = new SelectList(await _dropdownService.GetHolidayGroupDropdown(), "Code", "Name");
                    break;
                case 3:
                    data = new SelectList(await _dropdownService.GetOffDaySettings_Dropdown(), "Code", "Name");
                    break;
                case 4:
                    data = new SelectList(await _dropdownService.GetLeaveGroupFilterDropdown(), "Code", "Name");
                    break;
            }
            return Json(data);
        }
        public async Task<IActionResult> Edit(Guid EmpGI)
        {
            try
            {
                AttendanceSetting model = new AttendanceSetting();
            model = await _attendanceSettingService.GetAttendanceSettingsEditDetails(EmpGI);
                if (model.ShiftType == 1)   
                {
                    model.ShiftSelect = model.AttendanceSettingShtSelection[0];
                }
                model.EmployeeHeader = await _commonService.GetEmployeeHeader(EmpGI);
            ViewBag.punchingMethod = new SelectList(await _dropdownService.GetPunchingMethodDropdown(), "Code", "Name");
            ViewBag.ShiftType = new SelectList(await _dropdownService.GetShiftTypeDropDown(), "Code", "Name");
            ViewBag.ShiftDetails = new SelectList(await _dropdownService.GetShiftDetailsDropdown(), "Code", "Name");
            ViewBag.HolidayGroup = new SelectList(await _dropdownService.GetHolidayGroupDropdown(), "Code", "Name");
            ViewBag.OffDaySettings = new SelectList(await _dropdownService.GetOffDaySettings_Dropdown(), "Code", "Name");
            ViewBag.LeaveGroup = new SelectList(await _dropdownService.GetLeaveGroupFilterDropdown(), "Code", "Name");
            ViewBag.ApplLeaves = new SelectList(await _dropdownService.GetLeaveGroupApplicableLeavesDropdown(model.LeaveGroup), "Code", "Name");
            ViewBag.Circle = new SelectList(await _dropdownService.GetDutyRosterCircleDropdown(), "Code", "Name");

            return PartialView("_EditSlider", model);
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
        public async Task<IActionResult> AttendanceSettingSave(AttendanceSetting model)
        {
            //assign singleselect data to multiselect variable
            if (model.ShiftType == 1)
            {
                model.AttendanceSettingShtSelection = new int?[1];
                model.AttendanceSettingShtSelection[0] = model.ShiftSelect;
            }
            try
            {
                var response = await _attendanceSettingService.AttendanceSettingSave(model);
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
        public async Task<IActionResult> ApplyFilter(Filter filter,int GroupBy,int Classification,int TypeCode)
        {
            try
            {
                AttendanceSetting model = new AttendanceSetting();
            Global.GlobalFilter = filter;
            model = await _attendanceSettingService.AttendanceSettingList(GroupBy, Classification, TypeCode, filter);
            ViewBag.GrpBy = new SelectList(await _dropdownService.GetJoiningAndSeprationgionGrouping(), "Code", "Name");
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
        public async Task<JsonResult> GetApplicableLeaves(int id)
        {
            var Leaves = new SelectList(await _dropdownService.GetLeaveGroupApplicableLeavesDropdown(id), "Code", "Name");
            return Json(Leaves);
        }
        public async Task<IActionResult> ExportToexcel(string ClassificationName, string GroupByName, int Classification, int GroupByCode,int TypeCode,int TotalRows)
        {
            try
            {
                AttendanceSetting model = new AttendanceSetting();

                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Employee Attendance Settings", "Excel");
                int slNo = 0;
                var Type = "";
                if (TypeCode == 0) Type = "Not Assigned";
                else Type = "Assigned";


                //Filter filter = new Filter();
                //filter = JsonConvert.DeserializeObject<Filter>(TempData["FilterModel"].ToString());
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
                var dataset = await _attendanceSettingService.GetList_Excel(GroupByCode, Classification, TypeCode, Global.GlobalFilter);
                DataTable dt = dataset.Tables[0];


                for (int i = 0; i < dt.Columns.Count; i++)
                {
                        dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
                }


                string currDateTime = await _helpers.GetCurrentDate();
                
                using (var workbook = new XLWorkbook())
                {

                    var worksheet = workbook.Worksheets.Add("Employee Attendance Settings");


                    int count = dt.Rows.Count;


                    worksheet.Range("A1:N" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:N" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:N" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:N" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:N" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;






                    var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 13)));
                    wsReportNameHeaderRange.Style.Font.Bold = true;
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Merge();
                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                    wsReportNameHeaderRange.Value = "Employee Attendance Settings";

                    worksheet.Cell(2, 1).Style.Font.Bold = true;
                    worksheet.Cell(2, 1).Value = "Group Company Name";
                    worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                    worksheet.Cell(2, 3).Style.Font.Bold = true;
                    worksheet.Cell(2, 3).Value = "GroupByName";
                    worksheet.Cell(2, 4).Value = GroupByName;
                    worksheet.Cell(2, 5).Style.Font.Bold = true;
                    worksheet.Cell(2, 5).Value = "ClassificationName";
                    worksheet.Cell(2, 6).Value = ClassificationName;
                    worksheet.Cell(2, 7).Style.Font.Bold = true;
                    worksheet.Cell(2, 7).Value = "Type";
                    worksheet.Cell(2, 8).Value = Type;

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
                    var filterapplied = worksheet.Range("B4:L4").Merge();

                    filterapplied.Value = FilterValues;


                    var currentRow = 5;

                    worksheet.Cell(currentRow, 1).InsertTable(dt);
                    worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                    worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                    worksheet.Range("A5:N5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Range("A5:N5").Style.Font.Bold = true;


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
                            book.Protect(EP.Password,false,false);
                            book.SaveToStream(stream);
                        }
                        var content = stream.ToArray();
                        return File(
                            content,
                            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                            "Employee Attendance Settings.xlsx"
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

