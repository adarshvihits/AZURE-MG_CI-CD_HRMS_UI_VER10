using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TimeOffice;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.TimeOffice;
using Hrms.Lite.Shared.Master;
using Newtonsoft.Json;
using Hrms.Lite.Shared.Account;
using System.Data;
using ClosedXML.Excel;
using System.IO;
using Spire.Xls;

namespace Hrms.Lite.UI.Areas.TimeOffice.Controllers
{
    [Area("TimeOffice")]
    [SessionAuthorize]
    public class LateHourConfirmation_GEOController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly IAttendanceWagesConfirmationService _attendanceWagesConfirmationService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        public LateHourConfirmation_GEOController(IDropdownService dropdownService, ICommonService commonService, IAttendanceWagesConfirmationService attendanceWagesConfirmationService, IHelpers helpers)
        {
            _attendanceWagesConfirmationService = attendanceWagesConfirmationService;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = helpers;

        }

        public async Task<IActionResult> Index()
        {
            try
            {
                AttendanceWagesConfirmation model = new AttendanceWagesConfirmation();
            ViewBag.Month = new SelectList(await _dropdownService.GetSalaryProcessing_MonthDropdown(0), "Code", "Name");
               

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
        //public async Task<IActionResult> ConfirmationList(string Tab, Filter filter, int Month, int PayrollGroupCode)
        //{
        //    try
        //    {
        //        AttendanceWagesConfirmation model = new AttendanceWagesConfirmation();
        //        model = await _attendanceWagesConfirmationService.ConfirmationList_GEO(Tab, filter, Month, PayrollGroupCode);
        //        model.Tab = Tab;
        //        model.MonthCode = Month;

        //        return PartialView("_List", model);
        //    }
        //    catch (UnauthorizedAccessException e)
        //    {
        //        return RedirectToAction("Login", "Account", new { Area = "" });
        //    }
        //    catch (Exception e)
        //    {
        //        return RedirectToAction("Error", "Account", new { Area = "" });
        //    }
        //}


        [HttpPost]
        public async Task<IActionResult> ConfirmationList(string Tab, Filter filter1, int Month)
        {
            try
            {
                AttendanceWagesConfirmation model = new AttendanceWagesConfirmation();
                AttendanceWagesConfirmation m = new AttendanceWagesConfirmation();
                //model.EmployeeHeader = new EmployeeHeader();

                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }
                

                    model = await _attendanceWagesConfirmationService.ConfirmationList_GEO(Tab, filter1, Month);

                m.ConfirmationList = model.ConfirmationList;
                int totalrows = 0;
                int PendingCount = 0;
                int ConfirmedCount = 0;
                int totalrowsafterfiltering = 0;



                if (model == null || model.ConfirmationList == null || model.ConfirmationList.Count() == 0)
                {
                    ConfirmedCount = model.ConfirmedCount;
                    PendingCount = model.PendingCount;
                    m.ConfirmationList = new List<AttendanceWagesConfirmationDetails>();
                    model = new AttendanceWagesConfirmation();
                    model.ConfirmationList = new List<AttendanceWagesConfirmationDetails>();
                }
                else
                {
                    m.ConfirmationList = model.ConfirmationList;
                }



                if (model.ConfirmationList == null || model.ConfirmationList.Count() == 0)
                {

                    return Json(new
                    {
                        data = "",
                        draw = filter1.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows,
                        PendingCount= PendingCount,
                        ConfirmedCount= ConfirmedCount
                    });
                }




                if (model.ConfirmationList.Count > 0)
                {
                    totalrows = m.ConfirmationList[0].active;
                    ConfirmedCount = m.ConfirmationList[0].ConfirmedCount;
                    PendingCount = m.ConfirmationList[0].PendingCount;

                }


                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = m.ConfirmationList[0].totalRecords;
                    return Json(new
                    {
                        data = model.ConfirmationList,
                        draw = filter1.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows,//totalrowsafterfiltering
                        PendingCount = PendingCount,
                        ConfirmedCount = ConfirmedCount,
                    });
                }
                else
                {
                    return Json(new
                    {
                        data = model.ConfirmationList,
                        draw = filter1.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows,
                        PendingCount = PendingCount,
                        ConfirmedCount = ConfirmedCount,
                    });
                }


            }
            catch (Exception ex)
            {
                return View();
            }


        }
        public async Task<IActionResult> EditUpdate(string Tab, Filter filter, int Month,  decimal TotalMinToCutEdit, int EmployeeCode, Guid EmployeeGI, string Mode)
        {
            try
            {
                AttendanceWagesConfirmation model = new AttendanceWagesConfirmation();
                model.Tab = Tab;
                model.Month = new Shared.TimeOffice.Generic.Master.Month();
                model.Month.Code = Month;
                model.TotalMinToCutEditDays = TotalMinToCutEdit;
                model.EmployeeCode = EmployeeCode;
                model.EmployeeGI = EmployeeGI;
                model.Mode = Mode;
                var response = await _attendanceWagesConfirmationService.EditUpdate(model);
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
        public async Task<IActionResult> ConfirmLateHour( Filter filter1, int Month)
        {
            try
            {
                
                var response = await _attendanceWagesConfirmationService.LessHourConfirmationSave( filter1, Month);
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
        public async Task<IActionResult> ExportToExcel(string Tab, Filter filter1, int Month, int totalRecords,string MonthName)
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
                EP = await _commonService.GetExcelPassword("Late Hour Confirmation (" + Tab + ")", "Excel");

                if (FilterValues == "")
                {
                    FilterValues = "All";
                }
                string currDateTime = await _helpers.GetCurrentDate();
                

                //Get Dataset
                var dataset = await _attendanceWagesConfirmationService.GetConfirmationList_Excel(Tab, filter1, Month);
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
                    var worksheet = workbook.Worksheets.Add("Late Hour Conf");

                    int count = dt.Rows.Count + 5;


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
                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191); ;
                    wsReportNameHeaderRange.Value = "Late Hour Confirmation (" + Tab + ")";

                    worksheet.Cell(2, 1).Style.Font.Bold = true;
                    worksheet.Cell(2, 1).Value = "Group Company Name";
                    worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                    worksheet.Cell(2, 3).Style.Font.Bold = true;
                    worksheet.Cell(2, 3).Value = "Month";
                    worksheet.Cell(2, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(2, 4).Value = MonthName;             

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
                    worksheet.Range("B4:I4").Merge();
                    worksheet.Cell("B4").Value = FilterValues;

                    var currentRow = 5;

                    worksheet.Cell(currentRow, 1).InsertTable(dt);
                    worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                    worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                    worksheet.Range("A5:I5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Range("A5:I5").Style.Font.Bold = true;



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
                            "Late Hour Confirmation (" + Tab + ").xlsx"
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

        public async Task<IActionResult> LHCDetails(int Month, int EmployeeCode)
        {
            try
            {
                AttendanceWagesConfirmation model = new AttendanceWagesConfirmation();
                model = await _attendanceWagesConfirmationService.LHCDetails(Month, EmployeeCode);
                return PartialView("_Details", model);


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
