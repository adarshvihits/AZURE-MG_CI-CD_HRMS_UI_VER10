using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.TimeOffice.Report;
using Hrms.Lite.Shared.Account;
using Hrms.Lite.Shared.General;
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
    public class AbsenteeismReportController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly IAbsenteeismReportService _AbsenteeismReportService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;

        public static class Global
        {
            public static DateTime fromDate;
            public static DateTime Todate;
            public static DateTime date;
            public static int Type;
            public static Filter GlobalFilter;
            public static Guid EmployeeGI;
            public static int column;
            public static int value;

        }
        public AbsenteeismReportController(IAbsenteeismReportService AbsenteeismReportService, IDropdownService dropdownService, ICommonService commonService, IHelpers Helpers)
        {
            _AbsenteeismReportService = AbsenteeismReportService;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = Helpers;

        }
        public async Task<IActionResult> Index()
        {
            AbsenteeismReport model = new AbsenteeismReport();
            await fillfilterDropdown();
            
            Filter filter = new Filter();
            ViewBag.Type = new SelectList(await _dropdownService.GetAbsenteeismReportType_Dropdown(), "Code", "Name");
            var currMonth = await _commonService.GetCurrentMonthDropdown();

            string lastdayofmonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1).ToString("yyyy-MMM-dd");
            string firstdate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("yyyy-MMM-dd");
            //model = await _JoiningReportService.GetJoiningReport(firstdate, lastdayofmonth, Global.GlobalFilter);

            model.FromDate = Convert.ToDateTime(firstdate);
            model.ToDate = DateTime.Now;


            return View("~/Areas/TimeOffice/Report/Views/AbsenteeismReport/Index.cshtml", model);
        }



        [HttpPost]
        public async Task<IActionResult> GetAbsenteeismList(Filter filter1, string fromDate, string Todate, int Type)
        {
            try
            {
                AbsenteeismReport model = new AbsenteeismReport();
                AbsenteeismReport m = new AbsenteeismReport();
                //model.EmployeeHeader = new EmployeeHeader();

                Filter filter = new Filter();
                Global.GlobalFilter = filter1;


                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }
                if (fromDate != null && Todate != null && Type != 0)
                {
                    model = await _AbsenteeismReportService.GetAbsenteeismReportList(fromDate, Todate, Type, Global.GlobalFilter);

                }
                if (Type == 1 || Type == 3)
                {

                    m.AttendanceStatus = model.AttendanceStatus;
                    int totalrows = 0;
                    int totalrowsafterfiltering = 0;

                    if (model.AttendanceStatus == null || model.AttendanceStatus.Count() == 0)
                    {


                        return Json(new
                        {
                            data = "",
                            draw = filter1.draw,
                            recordsTotal = totalrows,
                            recordsFiltered = totalrows
                        });
                    }


                    if (model.AttendanceStatus.Count > 0)
                    {
                        totalrows = m.AttendanceStatus[0].active;

                    }


                    if (filter1.searchValue != "")
                    {
                        totalrowsafterfiltering = m.AttendanceStatus[0].totalRecords;
                     
                        return Json(new
                        {
                            data = model.AttendanceStatus,
                            draw = filter1.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                            recordsTotal = totalrows,
                            recordsFiltered = totalrows//totalrowsafterfiltering
                        });
                    }
                    else
                    {
                      
                        return Json(new
                        {
                            data = model.AttendanceStatus,
                            draw = filter1.draw,//Request.Form["draw"], 
                            recordsTotal = totalrows,
                            recordsFiltered = totalrows
                        });
                    }


                }
                else
                {


                    m.ReasonWise = model.ReasonWise;
                    int totalrows = 0;
                    int totalrowsafterfiltering = 0;

                    if (model.ReasonWise == null || model.ReasonWise.Count() == 0)
                    {
                      


                        return Json(new
                        {
                            data = "",
                            draw = filter1.draw,//Request.Form["draw"], 
                            recordsTotal = totalrows,
                            recordsFiltered = totalrows
                        });
                    }





                    if (model.ReasonWise.Count > 0)
                    {
                        totalrows = m.ReasonWise[0].active;

                        
                    }


                    if (filter1.searchValue != "")
                    {
                        totalrowsafterfiltering = m.ReasonWise[0].totalRecords;
                     
                        return Json(new
                        {
                            data = model.ReasonWise,
                            draw = filter1.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                            recordsTotal = totalrows,
                            recordsFiltered = totalrows//totalrowsafterfiltering
                        });
                    }
                    else
                    {
                       
                        return Json(new
                        {
                            data = model.ReasonWise,
                            draw = filter1.draw,//Request.Form["draw"], 
                            recordsTotal = totalrows,
                            recordsFiltered = totalrows
                        });
                    }


                }


            }

            catch (Exception ex)
            {
                return View();
            }

        }

        public IActionResult GetFillDataList(int TypeCode)
        {
            AbsenteeismReport model = new AbsenteeismReport();
            ViewBag.Type = TypeCode;
            if (TypeCode == 1)
            {


                return PartialView("~/Areas/TimeOffice/Report/Views/AbsenteeismReport/_DateWiseStatusList.cshtml");
            }
            else if(TypeCode==2)
            {
                return PartialView("~/Areas/TimeOffice/Report/Views/AbsenteeismReport/_DateWiseReasonList.cshtml");

            }
            else if(TypeCode==3)
            {
                return PartialView("~/Areas/TimeOffice/Report/Views/AbsenteeismReport/_EmpWiseStatusList.cshtml");

            }
            else
            {
                return PartialView("~/Areas/TimeOffice/Report/Views/AbsenteeismReport/_EmpWiseReasonList.cshtml");
            }
        }


        [HttpPost]
        public async Task<IActionResult> GetEmpPopUpList(Filter Filter, DateTime date, int Value, int column, Guid EmployeeGI, DateTime fromDate, DateTime Todate, int type)
        {
            try
            {
                await fillfilterDropdown();
                AbsenteeismReport model = new AbsenteeismReport();
                Filter filter = new Filter();
                Global.GlobalFilter = Filter;
                Global.date = date;
                Global.value = Value;
                Global.column = column;
                Global.EmployeeGI = EmployeeGI;


                if (Filter.searchValue == "undefined" || Filter.searchValue == null)
                {
                    Filter.searchValue = "";
                }

                if (type == 3)
                {
                    model = await _AbsenteeismReportService.EmpWiseStatusPopUp(EmployeeGI,fromDate.ToString("yyyy-MMM-dd"), Todate.ToString("yyyy-MMM-dd"), Filter);
                }
                else if (type == 4)
                {
                    model = await _AbsenteeismReportService.EmpWiseReasonPopUp(EmployeeGI, fromDate.ToString("yyyy-MMM-dd"), Todate.ToString("yyyy-MMM-dd"), column, Filter);
                }


                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model == null || model.Details == null || model.Details.Count() == 0)
                {


                    return Json(new
                    {
                        data = "",
                        draw = Filter.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        popupHead = model.PopPupHead,
                        type = type,
                        recordsFiltered = totalrows
                    });
                }




                if (model.Details.Count > 0)
                {
                    totalrows = model.Details[0].active;


                }

                //model.AllTabCount = m.AllTabCount;
                if (Filter.searchValue != "")
                {
                    totalrowsafterfiltering = model.Details[0].totalRecords;
                    return Json(new
                    {
                        data = model.Details,
                        draw = Filter.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        recordsTotal = totalrows,
                        popupHead = model.PopPupHead,
                        type = type,
                        recordsFiltered = totalrows//totalrowsafterfiltering
                    });
                }
                else
                {
                    return Json(new
                    {
                        data = model.Details,
                        draw = Filter.draw,//Request.Form["draw"], 
                        recordsTotal = model.Details.Count,
                        popupHead = model.PopPupHead,
                        type = type,
                        recordsFiltered = totalrows
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
        public async Task<IActionResult> GetDatePopupList(Filter Filter, DateTime date, Guid EmployeeGI, DateTime fromDate, DateTime Todate, int Value, int column, int Type)
        {
            try
            {
                await fillfilterDropdown();
                AbsenteeismReport model = new AbsenteeismReport();
                Filter filter = new Filter();
                Global.GlobalFilter = Filter;
                Global.date = date;
                Global.EmployeeGI = EmployeeGI;
                Global.value = Value;
                Global.column = column;


                if (Filter.searchValue == "undefined" || Filter.searchValue == null)
                {
                    Filter.searchValue = "";
                }

                if (Type == 1)
                {
                    model = await _AbsenteeismReportService.DateWiseStatusPopUp(date.ToString("yyyy-MMM-dd"), Filter);
                }
                else if (Type == 2)
                {

                    model = await _AbsenteeismReportService.DateWiseReasonPopUp(date.ToString("yyyy-MMM-dd"), column, Global.GlobalFilter);



                    //model = await _AbsenteeismReportService.EmpWiseStatusPopUp(EmployeeGI,fromDate.ToString("yyyy-MMM-dd"), Todate.ToString("yyyy-MMM-dd"), Global.GlobalFilter);
                }

                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model == null || model.Details == null || model.Details.Count() == 0)
                {


                    return Json(new
                    {
                        data = "",
                        draw = Filter.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        popupHead = model.PopPupHead,
                        type = Type,
                        recordsFiltered = totalrows
                    });
                }




                if (model.Details.Count > 0)
                {
                    totalrows = model.Details[0].active;


                }

                //model.AllTabCount = m.AllTabCount;
                if (Filter.searchValue != "")
                {
                    totalrowsafterfiltering = model.Details[0].totalRecords;
                    return Json(new
                    {
                        data = model.Details,
                        draw = Filter.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        recordsTotal = totalrows,
                        popupHead = model.PopPupHead,
                        type = Type,
                        recordsFiltered = totalrows//totalrowsafterfiltering
                    });
                }
                else
                {
                    return Json(new
                    {
                        data = model.Details,
                        draw = Filter.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        popupHead = model.PopPupHead,
                        type = Type,
                        recordsFiltered = totalrows
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
        public async Task<IActionResult> ApplyFilter(DateTime fromDate, DateTime Todate, int Type, Filter filter)
        {
            try
            {

                AbsenteeismReport model = new AbsenteeismReport();
                Global.GlobalFilter = new Filter();
                model = await _AbsenteeismReportService.GetAbsenteeismReportList(fromDate.ToString("yyyy-MMM-dd"), Todate.ToString("yyyy-MMM-dd"), Type, Global.GlobalFilter);
                if (Type == 1 || Type == 3)
                {
                    return PartialView("~/Areas/TimeOffice/Report/Views/AbsenteeismReport/_AttendanceStatusList.cshtml", model);
                }
                else
                {
                    return PartialView("~/Areas/TimeOffice/Report/Views/AbsenteeismReport/_AbsenteeismReasonList.cshtml", model);
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

        public async Task<IActionResult> ExportToexcelPopUp(DateTime fromDate, DateTime Todate, int Type, string selectedVal, int TotalRows)
        {
            try
            {
                AbsenteeismReport model = new AbsenteeismReport();

                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Absenteeism Report", "Excel");
                int slNo = 0;

                if (Global.GlobalFilter == null)
                    Global.GlobalFilter = new Filter();
                else
                {
                    Global.GlobalFilter.length = TotalRows;
                    Global.GlobalFilter.start = 0;
                }


                if (Type == 1)
                {
                    model = await _AbsenteeismReportService.DateWiseStatusPopUp(Global.date.ToString("yyyy-MMM-dd"), Global.GlobalFilter);
                }
                else if (Type == 3)
                {
                    model = await _AbsenteeismReportService.EmpWiseStatusPopUp(Global.EmployeeGI, fromDate.ToString("yyyy-MMM-dd"), Todate.ToString("yyyy-MMM-dd"), Global.GlobalFilter);
                }

                if (Type == 2)
                {
                    model = await _AbsenteeismReportService.DateWiseReasonPopUp(Global.date.ToString("yyyy-MMM-dd"), Global.column, Global.GlobalFilter);
                }
                else if (Type == 4)
                {
                    model = await _AbsenteeismReportService.EmpWiseReasonPopUp(Global.EmployeeGI, fromDate.ToString("yyyy-MMM-dd"), Todate.ToString("yyyy-MMM-dd"), Global.column, Global.GlobalFilter);
                }
                string currDateTime = await _helpers.GetCurrentDate();
                



                using (var workbook = new XLWorkbook())
                {

                    var worksheet = workbook.Worksheets.Add("AbsenteeismReport List");


                    if (Type == 3 || Type == 4)
                    {
                        int count = model.Details.Count + 5;


                        worksheet.Range("A1:G" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:G" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:G" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:G" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:G" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 6)));

                        wsReportNameHeaderRange.Style.Font.Bold = true;
                        wsReportNameHeaderRange.Style.Font.FontSize = 15;
                        wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                        wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        wsReportNameHeaderRange.Merge();
                        wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                        wsReportNameHeaderRange.Value = "Absenteeism Report";
                        worksheet.Cell(2, 1).Style.Font.Bold = true;
                        worksheet.Cell(2, 1).Value = "Group Company Name";
                        worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                        worksheet.Cell(2, 3).Style.Font.Bold = true;
                        worksheet.Cell(2, 3).Value = "From";
                        worksheet.Cell(2, 4).Value = fromDate;
                        worksheet.Cell(2, 4).Style.DateFormat.Format = "dd-MMM-yyyy";
                        worksheet.Cell(2, 5).Style.Font.Bold = true;
                        worksheet.Cell(2, 5).Value = "To";
                        worksheet.Cell(2, 6).Value = Todate;
                        worksheet.Cell(2, 6).Style.DateFormat.Format = "dd-MMM-yyyy";
                        worksheet.Cell(3, 1).Style.Font.Bold = true;
                        worksheet.Cell(3, 1).Value = "Based on";
                        worksheet.Cell(3, 2).Value = selectedVal;


                        worksheet.Cell(4, 1).Style.Font.Bold = true;
                        worksheet.Cell(4, 1).Value = "Login User";
                        worksheet.Cell(4, 2).Value = LoginUser;
                        worksheet.Cell(4, 3).Style.Font.Bold = true;
                        worksheet.Cell(4, 3).Value = "Date & Time";
                        worksheet.Cell(4, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(4, 4).Style.DateFormat.Format = "dd-MMM-yyyy   h:mm AM/PM";
                        worksheet.Cell(4, 4).Value = currDateTime.ToString();
                        worksheet.Cell(4, 5).Style.Font.Bold = true;
                        worksheet.Cell(4, 5).Value = "Total Count";
                        worksheet.Cell(4, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(4, 6).Value = model.Details.Count;
                      
                    }

                    else
                    {
                        int count = model.Details.Count + 5;


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
                        wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                        wsReportNameHeaderRange.Value = "Absenteeism Report";
                        worksheet.Cell(2, 1).Style.Font.Bold = true;
                        worksheet.Cell(2, 1).Value = "Group Company Name";
                        worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                        worksheet.Cell(2, 3).Style.Font.Bold = true;
                        worksheet.Cell(2, 3).Value = "From";
                        worksheet.Cell(2, 4).Value = fromDate;
                        worksheet.Cell(2, 4).Style.DateFormat.Format = "dd-MMM-yyyy";
                        worksheet.Cell(2, 5).Style.Font.Bold = true;
                        worksheet.Cell(2, 5).Value = "To";
                        worksheet.Cell(2, 6).Value = Todate;
                        worksheet.Cell(2, 6).Style.DateFormat.Format = "dd-MMM-yyyy";
                        worksheet.Cell(2, 7).Style.Font.Bold = true;
                        worksheet.Cell(3, 1).Value = "Based on";
                        worksheet.Cell(3, 2).Value = selectedVal;


                        worksheet.Cell(4, 1).Style.Font.Bold = true;
                        worksheet.Cell(4, 1).Value = "Login User";
                        worksheet.Cell(4, 2).Value = LoginUser;
                        worksheet.Cell(4, 3).Style.Font.Bold = true;
                        worksheet.Cell(4, 3).Value = "Date & Time";
                        worksheet.Cell(4, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(4, 4).Style.DateFormat.Format = "dd-MMM-yyyy   h:mm AM/PM";
                        worksheet.Cell(4, 4).Value = currDateTime.ToString();
                        worksheet.Cell(4, 5).Style.Font.Bold = true;
                        worksheet.Cell(4, 5).Value = "Total Count";
                        worksheet.Cell(4, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(4, 6).Value = model.Details.Count;
                       
                    }


                    var currentRow = 5;

                    #region Header
                    if (Type == 1 || Type == 2)
                        
                    {
                       
                            worksheet.Range("A5:H5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);

                        


                        worksheet.Cell(currentRow, 1).Value = "Sl.No";
                        worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 2).Value = "ID";
                        worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 3).Value = "Employee Name";
                        worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 4).Value = "Shift";
                        worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 5).Value = "IN";
                        worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 6).Value = "OUT";
                        worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 7).Value = "Att. Status";
                        worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                       
                            worksheet.Cell(currentRow, 8).Value = "Reason";
                            worksheet.Cell(currentRow, 8).Style.Font.Bold = true;
                        


                        #endregion

                        #region Body
                        foreach (var item in model.Details)
                        {
                            currentRow++;
                            slNo = slNo + 1;
                            worksheet.Columns().AdjustToContents();
                            worksheet.Rows().AdjustToContents();
                            worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 1).Value = slNo;

                            worksheet.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 2).Value = item.EmployeeID;

                            worksheet.Cell(currentRow, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 3).Value = item.EmployeeName;



                            worksheet.Cell(currentRow, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 4).Value = item.Shift;

                            worksheet.Cell(currentRow, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 5).Value = item.IN;

                            worksheet.Cell(currentRow, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 6).Value = item.Out;

                            worksheet.Cell(currentRow, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 7).Value = item.AttnStatus;
                           
                                worksheet.Cell(currentRow, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                                worksheet.Cell(currentRow, 8).Value = item.Reason;
                            
                            worksheet.Columns(1, 8).Style.Alignment.WrapText = true;
                            worksheet.Rows().Style.Alignment.WrapText = true;
                        }
                    }








                    else if (Type == 3 || Type == 4)
                    {



                             worksheet.Range("A5:G5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);

                        

                        worksheet.Cell(currentRow, 1).Value = "Sl.No";
                        worksheet.Cell(currentRow, 1).Style.Font.Bold = true;


                        worksheet.Cell(currentRow, 2).Value = "Date";
                        worksheet.Cell(currentRow, 2).Style.Font.Bold = true;

                        worksheet.Cell(currentRow, 3).Value = "Shift";
                        worksheet.Cell(currentRow, 3).Style.Font.Bold = true;

                        worksheet.Cell(currentRow, 4).Value = "IN";
                        worksheet.Cell(currentRow, 4).Style.Font.Bold = true;

                        worksheet.Cell(currentRow, 5).Value = "OUT";
                        worksheet.Cell(currentRow, 5).Style.Font.Bold = true;

                        worksheet.Cell(currentRow, 6).Value = "Att. Status";
                        worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
                       
                            worksheet.Cell(currentRow, 7).Value = "Reason";
                            worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                        


                        foreach (var item in model.Details)
                        {
                            currentRow++;
                            slNo = slNo + 1;
                            worksheet.Columns().AdjustToContents();
                            worksheet.Rows().AdjustToContents();
                            worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 1).Value = slNo;

                            worksheet.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 2).Value = item.Date;

                          
                            worksheet.Cell(currentRow, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 3).Value = item.Shift;

                            worksheet.Cell(currentRow, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 4).Value = item.IN;

                            worksheet.Cell(currentRow, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 5).Value = item.Out;

                            worksheet.Cell(currentRow, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 6).Value = item.AttnStatus;
                           
                                worksheet.Cell(currentRow, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                                worksheet.Cell(currentRow, 7).Value = item.Reason;
                            
                            worksheet.Columns(1, 8).Style.Alignment.WrapText = true;
                            worksheet.Rows().Style.Alignment.WrapText = true;
                        }



                    }
                    #endregion
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
                            "Absenteeism Report List.xlsx"
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



        public async Task<IActionResult> ExportToExcel(DateTime fromDate, DateTime Todate, int Type, string selectedVal, int TotalRows)
        {
            try
            {

                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Absenteeism Report", "Excel");

                var currentRow = 0;
                int count = 0;

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
                

                //Get Dataset
                var dataset = await _AbsenteeismReportService.AbsenteeismReport_Excel(fromDate.ToString("yyyy-MMM-dd"), Todate.ToString("yyyy-MMM-dd"), Type, Global.GlobalFilter);
                DataTable dt = dataset.Tables[0];


                //set EXCEL Column Heading 

                if (Type == 1 || Type == 3) // DATEWISE ATTNDNCE STATUS AND EMPLOYEE WISE ATTNDNCE STATUS
                {
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        if (dt.Columns[i].ColumnName == "lop" || dt.Columns[i].ColumnName == "ab" || dt.Columns[i].ColumnName == "ab %" || dt.Columns[i].ColumnName == "od")
                            dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.ToUpper();
                        else
                            dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
                    }
                }
                else
                {
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
                    }
                }
                using (var workbook = new XLWorkbook())
                {

                    var worksheet = workbook.Worksheets.Add("AbsenteeismReport List");
                    IXLRange wsReportNameHeaderRange;
                    if (Type == 1 || Type == 3) // DATEWISE ATTNDNCE STATUS AND EMPLOYEE WISE ATTNDNCE STATUS
                    {


                        if (Type == 1) //DATEWISE
                        {
                            count = dt.Rows.Count + 5;
                        }
                        else if (Type == 3)
                        {
                            count = dt.Rows.Count + 6;
                        }


                        if (Type == 1)
                        {
                            worksheet.Range("A1:K" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                            worksheet.Range("A1:K" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                            worksheet.Range("A1:K" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                            worksheet.Range("A1:K" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                            worksheet.Range("A1:K" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                            wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 10)));
                        }
                        else
                        {
                            worksheet.Range("A1:L" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                            worksheet.Range("A1:L" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                            worksheet.Range("A1:L" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                            worksheet.Range("A1:L" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                            worksheet.Range("A1:L" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;

                            wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 11)));
                        }
                        wsReportNameHeaderRange.Style.Font.Bold = true;
                        wsReportNameHeaderRange.Style.Font.FontSize = 15;
                        wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                        wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        wsReportNameHeaderRange.Merge();
                        wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                        wsReportNameHeaderRange.Value = "Absenteeism Report";
                        worksheet.Cell(2, 1).Style.Font.Bold = true;
                        worksheet.Cell(2, 1).Value = "Group Company Name";
                        worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                        worksheet.Cell(2, 3).Style.Font.Bold = true;
                        worksheet.Cell(2, 3).Value = "From";
                        worksheet.Cell(2, 4).Value = fromDate;
                        worksheet.Cell(2, 4).Style.DateFormat.Format = "dd-MMM-yyyy";
                        worksheet.Cell(2, 5).Style.Font.Bold = true;
                        worksheet.Cell(2, 5).Value = "To";
                        worksheet.Cell(2, 6).Value = Todate;
                        worksheet.Cell(2, 6).Style.DateFormat.Format = "dd-MMM-yyyy";




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

                        worksheet.Cell(4, 1).Value = "Based on";
                        worksheet.Cell(4, 2).Value = selectedVal;
                        worksheet.Cell(4, 1).Style.Font.Bold = true;
                        if (Type == 3)
                        {
                            worksheet.Cell(5, 1).Style.Font.Bold = true;
                            worksheet.Cell(5, 1).Value = "Filter Applied";
                            var filterapplied = worksheet.Range("B5:K5").Merge();

                            filterapplied.Value = FilterValues;
                        }
                    }
                    else
                    {
                        if (Type == 2)
                        {
                            count = dt.Rows.Count + 5;
                        }
                        else if (Type == 4)
                        {
                            count = dt.Rows.Count + 6;
                        }
                        if (Type == 2)
                        {
                            worksheet.Range("A1:L" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                            worksheet.Range("A1:L" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                            worksheet.Range("A1:L" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                            worksheet.Range("A1:L" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                            worksheet.Range("A1:L" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                            wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 11)));
                        }
                        else
                        {
                            worksheet.Range("A1:M" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                            worksheet.Range("A1:M" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                            worksheet.Range("A1:M" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                            worksheet.Range("A1:M" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                            worksheet.Range("A1:M" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                            wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 12)));
                        }
                        wsReportNameHeaderRange.Style.Font.Bold = true;
                        wsReportNameHeaderRange.Style.Font.FontSize = 15;
                        wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                        wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        wsReportNameHeaderRange.Merge();
                        wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                        wsReportNameHeaderRange.Value = "Absenteeism Report";
                        worksheet.Cell(2, 1).Style.Font.Bold = true;
                        worksheet.Cell(2, 1).Value = "Group Company Name";
                        worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                        worksheet.Cell(2, 3).Style.Font.Bold = true;
                        worksheet.Cell(2, 3).Value = "From";
                        worksheet.Cell(2, 4).Value = fromDate;
                        worksheet.Cell(2, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(2, 4).Style.DateFormat.Format = "dd-MMM-yyyy";
                        worksheet.Cell(2, 5).Style.Font.Bold = true;
                        worksheet.Cell(2, 5).Value = "To";
                        worksheet.Cell(2, 6).Value = Todate;
                        worksheet.Cell(2, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);

                        worksheet.Cell(2, 6).Style.DateFormat.Format = "dd-MMM-yyyy";
                        worksheet.Cell(2, 7).Style.Font.Bold = true;


                        worksheet.Cell(3, 1).Style.Font.Bold = true;
                        worksheet.Cell(3, 1).Value = "Login User";
                        worksheet.Cell(3, 2).Value = LoginUser;
                        worksheet.Cell(3, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);

                        worksheet.Cell(3, 3).Style.Font.Bold = true;
                        worksheet.Cell(3, 3).Value = "Date & Time";
                        worksheet.Cell(3, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(3, 4).Style.DateFormat.Format = "dd-MMM-yyyy   h:mm AM/PM";
                        worksheet.Cell(3, 4).Value = currDateTime.ToString();
                        worksheet.Cell(3, 5).Style.Font.Bold = true;
                        worksheet.Cell(3, 5).Value = "Total Count";
                        worksheet.Cell(3, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(3, 6).Value = dt.Rows.Count;
                        worksheet.Cell(4, 1).Value = "Based on";
                        worksheet.Cell(4, 2).Value = selectedVal;
                        worksheet.Cell(4, 1).Style.Font.Bold = true;
                        if (Type == 4)
                        {
                            worksheet.Cell(5, 1).Style.Font.Bold = true;
                            worksheet.Cell(5, 1).Value = "Filter Applied";
                            var filterapplied = worksheet.Range("B5:L5").Merge();

                            filterapplied.Value = FilterValues;
                        }


                    }



                    if (Type == 1 || Type == 3)
                    {
                        if (Type == 1)
                        {
                            currentRow = 5;
                        }
                        else if (Type == 3)
                        {
                            currentRow = 6;
                        }
                        worksheet.Cell(currentRow, 1).InsertTable(dt);
                        worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;
                        worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                        worksheet.Range("A" + currentRow + ":L" + currentRow).Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                        worksheet.Range("A" + currentRow + ":L" + currentRow).Style.Font.Bold = true;


                        worksheet.Columns().AdjustToContents();
                        worksheet.Rows().AdjustToContents();
                    }
                    else if (Type == 2 || Type == 4)
                    {
                        if (Type == 2)
                        {
                            currentRow = 5;
                        }
                        else if (Type == 4)
                        {
                            currentRow = 6;
                        }

                        worksheet.Cell(currentRow, 1).InsertTable(dt);
                        worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;


                        worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                        worksheet.Range("A" + currentRow + ":M" + currentRow).Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                        worksheet.Range("A" + currentRow + ":M" + currentRow).Style.Font.Bold = true;


                        worksheet.Columns().AdjustToContents();
                        worksheet.Rows().AdjustToContents();



                    }
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
                            "Absenteeism Report List.xlsx"
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
