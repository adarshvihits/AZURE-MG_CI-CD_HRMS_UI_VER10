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
    public class DailyAttendanceLogController : Controller
    {
        private readonly IHelpers _helpers;
        private readonly IDropdownService _dropdownService;
        private readonly IDailyAttendanceLogService _service;
        private readonly ICommonService _commonservice;
        public static class Global
        {
            public static Filter GlobalFilter;

        }
        public DailyAttendanceLogController(IDailyAttendanceLogService service, IHelpers helpers, IDropdownService dropdownService, ICommonService commonservice)
        {
            _dropdownService = dropdownService;
            _service = service;
            _helpers = helpers;
            _commonservice = commonservice;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                DailyAttendanceLog model = new DailyAttendanceLog();
                Filter filter = new Filter();
                filter.length = 10;
                model = await _service.DailyAttendanceLogList(DateTime.Now.ToString("yyyy-MMM-dd"), Global.GlobalFilter);
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


        [HttpGet]
        public async Task<IActionResult> CheckifAlreadyProcessed(DateTime Date)
        {
            try
            {
                var details = await _commonservice.GetSalaryPeriodLockDatewiseValidation(Date.ToString("yyyy-MMM-dd"),"",null);
                return Json(details);
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

        [HttpPost]
        public async Task<IActionResult> GetList(Filter filter1, DateTime SelectedDate)
        {
            try
            {
                DailyAttendanceLog model = new DailyAttendanceLog();
                DailyAttendanceLog m = new DailyAttendanceLog();
                //model.EmployeeHeader = new EmployeeHeader();




                Filter filter = new Filter();
                Global.GlobalFilter = filter1;


                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }
                if(SelectedDate.ToString("yyyy-MMM-dd") !="0001-Jan-01")
                model = await _service.DailyAttendanceLogList(SelectedDate.ToString("yyyy-MMM-dd"), Global.GlobalFilter);
           


                m.LogList = model.LogList;
                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.LogList == null || model.LogList.Count() == 0)
                {

                    return Json(new
                    {
                        data = "",
                        draw = filter1.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows
                    });
                }




                if (model.LogList.Count > 0)
                {
                    totalrows = m.LogList[0].active;


                }


                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = m.LogList[0].totalRecords;
                    return Json(new
                    {
                        data = model.LogList,
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
                        data = model.LogList,
                        draw = filter1.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows,
                      
                    });
                }


            }
            catch (Exception ex)
            {
                return View();
            }




        }



   
        [HttpPost]
        public async Task<IActionResult> EmployeeWiseDailyAttendanceLogSave(Guid EmployeeGI, string InTime, string OutTime, DateTime SelectedDate)
        {
            try
            {
                DailyAttendanceLog model = new DailyAttendanceLog();
                model.EmployeeGI = EmployeeGI;
                model.InTime = InTime;
                model.OutTime = OutTime;
                model.SelectedDate = SelectedDate;   
                var response = await _service.EmployeeWiseDailyAttendanceLogSave(model);
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
        public async Task<IActionResult> DailyAttendanceLogSave(DailyAttendanceLog model)
        {
            try
            {
                var response = await _service.DailyAttendanceLogSave(model);
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


     
        public IActionResult BatchUpdate()
        {
            try
            {
                return PartialView("_BatchUpdate");
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
        public async Task<IActionResult> DailyAttendanceLogBatchUpdate(DailyAttendanceLog model, DateTime SelectedDate)
        {
            try
            {
                model.Filter = Global.GlobalFilter;
                model.SelectedDate = SelectedDate;
                var response = await _service.DailyAttendanceLogBatchUpdate(model);
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

        public async Task<IActionResult> ExportToExcel(DateTime SelectedDate,int TableLength)
        {
            try
            {
                DailyAttendanceLog model = new DailyAttendanceLog();
                if (Global.GlobalFilter == null)
                    Global.GlobalFilter = new Filter();
                else
                {
                    Global.GlobalFilter.length = TableLength;
                    Global.GlobalFilter.start = 0;
                }
                string FilterValues = await _helpers.GetFilterValues(Global.GlobalFilter);
                //model = await _service.DailyAttendanceLogList(SelectedDate.ToString("yyyy-MMM-dd"), Global.GlobalFilter);

              


                      var dataset = await _service.GetApplicantTrackingReport_Excel(SelectedDate.ToString("yyyy-MMM-dd"),Global.GlobalFilter);
                DataTable dt = dataset.Tables[0];

                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (dt.Columns[i].ColumnName == "doj" || dt.Columns[i].ColumnName == "dob" || dt.Columns[i].ColumnName == "y/s")
                        dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.ToUpper();
                    else
                        dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
                }

                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonservice.GetExcelPassword("Daily Attendance Log List", "Excel");
                int slNo = 0;
                if (FilterValues == "")
                {
                    FilterValues = "All";
                }
                string currDateTime = await _helpers.GetCurrentDate();
                
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Daily Attendance Log List");
                    int count = dt.Rows.Count + 5;


                    worksheet.Range("A1:H" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:H" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:H" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:H" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:H" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;

                    var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 7)));
                    wsReportNameHeaderRange.Style.Font.Bold = true;
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Merge();
                    wsReportNameHeaderRange.Value = "Daily Attendance Log List";

                    worksheet.Cell(2, 1).Style.Font.Bold = true;
                    worksheet.Cell(2, 1).Value = "Group Company Name";
                    worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                    worksheet.Cell(2, 3).Style.Font.Bold = true;
                    worksheet.Cell(2, 3).Value = "Selected Date";
                    worksheet.Cell(2, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(2, 4).Style.DateFormat.Format = "MMM-yyyy";
                    worksheet.Cell(2, 4).Value = SelectedDate;
                    worksheet.Cell(2, 5).Style.Font.Bold = true;
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
                    worksheet.Range("B4:H4").Merge();
                    worksheet.Cell("B4").Value = FilterValues;


                    var currentRow = 5;

                    worksheet.Cell(currentRow, 1).InsertTable(dt);
                    worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                    worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                    worksheet.Range("A5:H5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Range("A5:H5").Style.Font.Bold = true;


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
                            "Daily Attendance Log List.xlsx"
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


