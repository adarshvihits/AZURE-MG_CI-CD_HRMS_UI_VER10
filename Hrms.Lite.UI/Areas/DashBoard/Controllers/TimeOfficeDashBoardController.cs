using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.DashBoard;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.Account;
using Hrms.Lite.Shared.DashBoard;
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

namespace Hrms.Lite.UI.Areas.DashBoard.Controllers
{
    [Area("DashBoard")]
    [SessionAuthorize]
    public class TimeOfficeDashBoardController : Controller
    {
        private readonly ITimeOfficeDashBoardService _service;
        private readonly IDropdownService _dropdownService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;

        public TimeOfficeDashBoardController(ITimeOfficeDashBoardService service, IDropdownService dropdownService, ICommonService commonService,IHelpers Helpers)
        {
            _service = service;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = Helpers;
        }
        public static class Global
        {
            public static string Type;
            public static Filter GlobalFilter;
        }
        public async Task<IActionResult> Index()
        {
            TimeOfficeDashBoardNew model = new TimeOfficeDashBoardNew();
            model = await _service.GetDashBoardTileDetails();

            return View(model);
        }
        public async Task<IActionResult> IndexV3()
        {
            TimeOfficeDashBoardNew model = new TimeOfficeDashBoardNew();
            model = await _service.GetDashBoardTileDetails();

            return View(model);
        }

        //public async Task<IActionResult> GetGraph()
        //{
        //    try
        //    {
        //        TimeOfficeDashBoardNew model = new TimeOfficeDashBoardNew();
        //        model = await _service.GetDashBoardDiagrams();
        //        return PartialView("_Chart", model.TimeOfficeDashBoardDiagram);
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
        public async Task<IActionResult> GetGraph(string Classification)
        {
            try
            {
                TimeOfficeDashBoardNew model = new TimeOfficeDashBoardNew();
                model = await _service.GetDashBoardDiagrams(Classification);
                ViewBag.Classsification = Classification;
                return PartialView("_AttendanceChart", model.TimeOfficeDashBoardDiagram);                
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
        public async Task<IActionResult> GetWorkStatus(string type)
        {
            TimeOfficeDashBoardNew model = new TimeOfficeDashBoardNew();
            model = await _service.GetDashBoardWorkStatusDetails();
            if(type =="v2")
                return PartialView("_WorkStatus", model);
            else
                return PartialView("_AttendanceStatus", model);
        }
        public async Task<IActionResult> GetNotifications(string type)
        {
            TimeOfficeDashBoardNew model = new TimeOfficeDashBoardNew();
            model = await _service.GetDashBoardNotificationDetails("All");
            for (int i = 0; i < model.DashBoardNotificationList.Count; i++)
            {
                if (model.DashBoardNotificationList[i].Type == "Shift missing employees")
                {
                    model.DashBoardNotificationList[i].Action = "/TimeOffice/AttendanceSetting/Index";
                }
                if (model.DashBoardNotificationList[i].Type == "Off day not allocated")
                {
                    model.DashBoardNotificationList[i].Action = "/TimeOffice/AttendanceSetting/Index";
                }
                if (model.DashBoardNotificationList[i].Type == "Employees completing ML/ESI leave in next 7 Days")
                {
                    model.DashBoardNotificationList[i].Action = "/DashBoard/TimeOfficeDashBoard/MaternityESILeaveCompletion";
                }
                if (model.DashBoardNotificationList[i].Type == "Punching ID missing")
                {
                    model.DashBoardNotificationList[i].Action = "/TimeOffice/AttendanceSetting/Index";
                }
                if (model.DashBoardNotificationList[i].Type == "Leave group missing")
                {
                    model.DashBoardNotificationList[i].Action = "/TimeOffice/AttendanceSetting/Index";
                }
                if (model.DashBoardNotificationList[i].Type == "Holiday group missing")
                {
                    model.DashBoardNotificationList[i].Action = "/TimeOffice/AttendanceSetting/Index";
                }
                if (model.DashBoardNotificationList[i].Type == "Over time approval missing")
                {
                    model.DashBoardNotificationList[i].Action = "/TimeOffice/DailyOTConfirmation/Index";
                }
                if (model.DashBoardNotificationList[i].Type == "Leave approval missing")
                {
                    model.DashBoardNotificationList[i].Action = "/TimeOffice/HRAttendanceApproval/Index?FirstTab=LeaveGroupMissing";
                }
                if (model.DashBoardNotificationList[i].Type == "On duty approval")
                {
                    model.DashBoardNotificationList[i].Action = "/TimeOffice/HRAttendanceApproval/Index?FirstTab=OnDutyMissing";
                }
                if (model.DashBoardNotificationList[i].Type == "WFH approval")
                {
                    model.DashBoardNotificationList[i].Action = "/TimeOffice/HRAttendanceApproval/Index?FirstTab=WFHApproval";
                }
                if (model.DashBoardNotificationList[i].Type == "Attendance Regularization approval")
                {
                    model.DashBoardNotificationList[i].Action = "/TimeOffice/HRAttendanceApproval/Index?FirstTab=AttendaceRegularisation";
                }
                if (model.DashBoardNotificationList[i].Type == "Permission hours approval")
                {
                    model.DashBoardNotificationList[i].Action = "/TimeOffice/HRAttendanceApproval/Index?FirstTab=PermissionHoursApproval";
                }
            }
            if(type =="v2")
                return PartialView("_Notifications", model);
            else
                return PartialView("_NotificationsV3", model);
        }


        public async Task<IActionResult> GetNotificationsPopUp(int NotificationCode)
        {

            try
            {
                TimeOfficeDashBoardNew model = new TimeOfficeDashBoardNew();
                model = await _service.GetDashBoardNotificationDetails_PopUp(NotificationCode);
                ViewBag.NotificationCode = NotificationCode;
                return PartialView("_NotificationsPopUp", model);
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



        //public async Task<IActionResult> Index()
        //{
        //    TimeOfficeDashBoardNew model = new TimeOfficeDashBoardNew();
        //    model = await _service.GetNewDashBoardDetails();
        //    for (int i = 0; i < model.DashBoardNotificationList.Count; i++)
        //    {
        //        if (model.DashBoardNotificationList[i].Type == "Shift missing employees")
        //        {
        //            model.DashBoardNotificationList[i].Action = "/TimeOffice/AttendanceSetting/Index";
        //        }
        //        if (model.DashBoardNotificationList[i].Type == "Off day not allocated")
        //        {
        //            model.DashBoardNotificationList[i].Action = "/TimeOffice/AttendanceSetting/Index";
        //        }
        //        if (model.DashBoardNotificationList[i].Type == "Employees completing ML/ESI leave in next 7 Days")
        //        {
        //            model.DashBoardNotificationList[i].Action = "/DashBoard/TimeOfficeDashBoard/MaternityESILeaveCompletion";
        //        }
        //        if (model.DashBoardNotificationList[i].Type == "Punching ID missing")
        //        {
        //            model.DashBoardNotificationList[i].Action = "/TimeOffice/AttendanceSetting/Index";
        //        }
        //        if (model.DashBoardNotificationList[i].Type == "Leave group missing")
        //        {
        //            model.DashBoardNotificationList[i].Action = "/TimeOffice/AttendanceSetting/Index";
        //        }
        //        if (model.DashBoardNotificationList[i].Type == "Holiday group missing")
        //        {
        //            model.DashBoardNotificationList[i].Action = "/TimeOffice/AttendanceSetting/Index";
        //        }
        //        if (model.DashBoardNotificationList[i].Type == "Over time approval missing")
        //        {
        //            model.DashBoardNotificationList[i].Action = "/TimeOffice/DailyOTConfirmation/Index";
        //        }
        //        if (model.DashBoardNotificationList[i].Type == "Leave approval missing")
        //        {
        //            model.DashBoardNotificationList[i].Action = "/TimeOffice/HRAttendanceApproval/Index?FirstTab=LeaveGroupMissing";
        //        }
        //        if (model.DashBoardNotificationList[i].Type == "On duty approval")
        //        {
        //            model.DashBoardNotificationList[i].Action = "/TimeOffice/HRAttendanceApproval/Index?FirstTab=OnDutyMissing";
        //        }
        //        if (model.DashBoardNotificationList[i].Type == "WFH approval")
        //        {
        //            model.DashBoardNotificationList[i].Action = "/TimeOffice/HRAttendanceApproval/Index?FirstTab=WFHApproval";
        //        }
        //        if (model.DashBoardNotificationList[i].Type == "Attendance Regularization approval")
        //        {
        //            model.DashBoardNotificationList[i].Action = "/TimeOffice/HRAttendanceApproval/Index?FirstTab=AttendaceRegularisation";
        //        }
        //        if (model.DashBoardNotificationList[i].Type == "Permission hours approval")
        //        {
        //            model.DashBoardNotificationList[i].Action = "/TimeOffice/HRAttendanceApproval/Index?FirstTab=PermissionHoursApproval";
        //        }
        //    }
        //    return View(model);
        //}

        public async Task<IActionResult> IndexV1()
        {

            try
            {
                TimeOfficeDashBoard model = new TimeOfficeDashBoard();
            model = await _service.GetDashBoardDetails();
            var Absenteeism_by_Day = await _service.GetDashBoardDiagrams("Absenteeism_by_Day",null);
            ViewBag.Shift = new SelectList(await _dropdownService.GetShiftDetailsDropdown(),"Code","Name");
            var Today_Check_In_Time_Status = await _service.GetDashBoardDiagrams("Today_Check_In_Time_Status",2);
            ViewBag.Absenteeism= JsonConvert.SerializeObject(Absenteeism_by_Day.TimeOfficeDashBoardList);
            ViewBag.CheckIn = JsonConvert.SerializeObject(Today_Check_In_Time_Status.TimeOfficeDashBoardList);
            model.Absenteeism_by_Day = Absenteeism_by_Day.TimeOfficeDashBoardList;
            model.Today_Check_In_Time_Status = Today_Check_In_Time_Status.TimeOfficeDashBoardList;
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

        public async Task<IActionResult> GetDashBoardList(string Type)
        {

            try
            {
                TimeOfficeDashBoard model = new TimeOfficeDashBoard();
            model = await _service.GetDashBoardList(Type);
            model.Popup_Type = Type;
            return PartialView("_ApprPendingAndAttendMissingList",model);
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

        public async Task<IActionResult> GetCheckInTimeChartDetails(int ShiftCode)
        {
            try
            {
                TimeOfficeDashBoard model = new TimeOfficeDashBoard();
                model = await _service.GetDashBoardDiagrams("Today_Check_In_Time_Status", ShiftCode);
                model.Today_Check_In_Time_Status = model.TimeOfficeDashBoardList;
                model.TimeOfficeDashBoardList = null;
                return PartialView("_CheckInChart", model);
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

        public async Task<IActionResult> GetDashBoardDetailsList(string Type)
        {

            try
            {
                TimeOfficeDashBoard model = new TimeOfficeDashBoard();
            LowAttendanceRate lowAttendanceRate = new LowAttendanceRate();
            if (Type!= "LowAttendanceRate")
            model = await _service.GetDashBoardDetailsList(Type,"");
            else
                lowAttendanceRate = await _service.GetLowAttendanceRateDetails();
            model.Popup_Type = Type;
            if(Type== "LESSHOURS")
            return PartialView("_LessHours", model);
           else if(Type== "AUTHORISED LOP" || Type == "UN AUTHORISED LOP")
                return PartialView("_AuthAndUnAuthorizedLop", model);
            else if (Type == "PreviousMonthAdjustments")
                return PartialView("_PrevMonthAdjustments", model);
            else if (Type == "Duty Roster Variance")
                return PartialView("_DutyRosterVarience", model);
            else if (Type == "Upcoming Leaves")
                return PartialView("_UpComingLeaveDetails", model);
            else if (Type == "ContinousAbsenteesim")
                return PartialView("_Absenteeism", model);
                else if (Type == "Employees completing ML/ESI leave in next 7 days")
                    return PartialView("MaternityESILeaveCompletion", model);
                else
                return PartialView("_LowAttendanceRate", lowAttendanceRate);
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
        public async Task<IActionResult> GetDashBoardEmployeewiseList(string Type, string SubType)
        {

            try
            {
                TimeOfficeDashBoard model = new TimeOfficeDashBoard();
            model = await _service.GetDashBoardDetailsList(Type, SubType);
            model.Popup_Type = Type;
            if (Type == "Upcoming Leaves")
                return PartialView("_UpComingLeaveDetails", model);
            else if (Type == "APPROVAL PENDING")
                return PartialView("_PendingLeaveDetails", model);
            else
                return PartialView("_AttendSettingsmissing", model);
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







        //                             New_TimeOffice_Dashboard
        //****************************************************************************************


        public async Task<IActionResult> GetDashBoardDetailsListNew(string Type)
        {

            try
            {
                TimeOfficeDashBoardNew model = new TimeOfficeDashBoardNew();
                LowAttendanceRate lowAttendanceRate = new LowAttendanceRate();
                Global.Type = Type;
                //if (Type != "LowAttendanceRate")
                //    model = await _service.GetDashBoardDetailsList(Type, "");
                //else
                //    lowAttendanceRate = await _service.GetLowAttendanceRateDetails();
                model.Popup_Type = Type;
                if (Type == "LESSHOURS")
                    return PartialView("_LessHoursNew", model);
                else if (Type == "PendingReg")
                    return PartialView("_PendingRegularization", model);
                else if (Type == "AUTHORISED LOP" || Type == "UN AUTHORISED LOP")
                    return PartialView("_AuthAndUnAuthorizedLopNew", model);
                else if (Type == "PreviousMonthAdjustments")
                    return PartialView("_PrevMonthAdjustments", model);
                else if (Type == "Duty Roster Variance")
                    return PartialView("_DutyRosterVarienceNew", model);
                else if (Type == "Upcoming Leaves" || Type == "WFH Leaves")
                    return PartialView("_UpComingLeaveDetailsNew", model);
                else if (Type == "ContinousAbsenteesim")
                    return PartialView("_AbsenteeismNew", model);
                else if (Type == "Employees completing ML/ESI leave in next 7 days")
                    return PartialView("MaternityESILeaveCompletion", model);
                else if (Type == "LOP")
                    return PartialView("_LopDetailsList", model);
                else
                    return PartialView("_LowAttendanceRate", lowAttendanceRate);
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
        public async Task<IActionResult> GetDashboardPopUpList(Filter filter1)
        {
            try
            {
                TimeOfficeDashBoardNew model = new TimeOfficeDashBoardNew();

                Filter filter = new Filter();


                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }
             
                    model = await _service.GetDashBoardDetailsList(Global.Type, "", filter1);

                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.TimeOfficeDashBoardDetailsList == null || model.TimeOfficeDashBoardDetailsList.Count() == 0)
                {


                    return Json(new
                    {
                        data = "",
                        draw = filter1.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        month=model.CurrentMonth,
                        recordsFiltered = totalrows

                    });
                }

                if (model.TimeOfficeDashBoardDetailsList.Count > 0)
                {
                    totalrows = model.TimeOfficeDashBoardDetailsList[0].active;
                }


                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = model.TimeOfficeDashBoardDetailsList[0].totalRecords;

                    return Json(new
                    {
                        data = model.TimeOfficeDashBoardDetailsList,
                        draw = filter1.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        recordsTotal = totalrows,
                        month = model.CurrentMonth,
                        recordsFiltered = totalrows//totalrowsafterfiltering

                    });
                }
                else
                {

                    return Json(new
                    {
                        data = model.TimeOfficeDashBoardDetailsList,
                        draw = filter1.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        month = model.CurrentMonth,
                        recordsFiltered = totalrows

                    });
                }


            }
            catch (Exception ex)
            {
                return View();
            }




        }
        public async Task<IActionResult> EmployeeLeaveAndLopDaysPopUp(Guid EmployeeGI,string Type)
        {
            try
            {
                TimeOfficeDashBoardNew model = new TimeOfficeDashBoardNew();
                if (Type != "LOP")
                    model = await _service.GetUnAuthorizedLopList(EmployeeGI);
                else
                    model = await _service.GetLopList(EmployeeGI);

                return PartialView("_DaysPopUp", model);
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


        public async Task<IActionResult> GetABDate(int EmployeeCode)
        {
            try
            {
                TimeOfficeDashBoardNew model = new TimeOfficeDashBoardNew();              
                model = await _service.GetABDateList(EmployeeCode);          

                return PartialView("_DaysPopUp", model);
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




        public async Task<IActionResult> ExportToExcel(string Type, string? SubType, Filter filterdata, int TotalRows)
        {
            try {
            TimeOfficeDashBoardNew model = new TimeOfficeDashBoardNew();
                filterdata.length = TotalRows;
            model = await _service.GetDashBoardDetailsListExcel(Type, SubType, filterdata);
            int slno = 0;
            string currDateTime = await _helpers.GetCurrentDate();
            
            var LoginUser = HttpContext.Session.GetString("LoginUser");
            var CompanyName = HttpContext.Session.GetString("CompanyName");
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Duty Roster Variance", "Excel");
                
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Duty Roster Variance");
                    int count = model.TimeOfficeDashBoardDetailsList.Count + 6;


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
                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Value = "Duty Roster Variance ";
                    worksheet.Range("A6:I6").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Cell(2, 1).Style.Font.Bold = true;
                    worksheet.Cell(2, 1).Value = "Company Name";
                    worksheet.Cell(2, 2).Value = CompanyName.ToUpper();
                    worksheet.Cell(3, 1).Style.Font.Bold = true;
                    worksheet.Cell(3, 1).Value = "Login User";
                    worksheet.Cell(3, 2).Value = LoginUser;
                    worksheet.Cell(4, 1).Style.Font.Bold = true;
                    worksheet.Cell(4, 1).Value = "Date & Time";
                    worksheet.Cell(4, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(4, 2).Style.DateFormat.Format = "dd-MMM-yyyy   h:mm AM/PM";
                    worksheet.Cell(4, 2).Value = currDateTime.ToString();
                    worksheet.Cell(5, 1).Style.Font.Bold = true;
                    worksheet.Cell(5, 1).Value = "Total Count";
                    worksheet.Cell(5, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(5, 2).Value = model.TimeOfficeDashBoardDetailsList.Count;
                    var currentRow = 6;

                    #region Header
                    worksheet.Cell(currentRow, 1).Value = "Sl.No";
                    worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 2).Value = "Employee ID";
                    worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 3).Value = "Employee Name";
                    worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 4).Value = "Designation";
                    worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 5).Value = "Department";
                    worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 6).Value = "Location";
                    worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 7).Value = "Date";
                    worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 8).Value = "Assigned Shift";
                    worksheet.Cell(currentRow, 8).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 9).Value = "Worked Shift";
                    worksheet.Cell(currentRow, 9).Style.Font.Bold = true;



                        #endregion

                        #region Body
                        foreach (var item in model.TimeOfficeDashBoardDetailsList)
                    {
                        currentRow++;
                        slno = slno + 1;
                        worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 1).Value = slno;
                        worksheet.Cell(currentRow, 2).Value = item.EmployeeID;
                        worksheet.Cell(currentRow, 3).Value = item.FullName;
                        worksheet.Cell(currentRow, 4).Value = item.Designation;
                        worksheet.Cell(currentRow, 5).Value = item.Department;
                        worksheet.Cell(currentRow, 6).Value = item.Location;
                        worksheet.Cell(currentRow, 7).Value = item.DateofApplication;
                        worksheet.Cell(currentRow, 8).Value = item.AssignedShift;
                        worksheet.Cell(currentRow, 9).Value = item.WorkedShift;
                        worksheet.Columns(1, 5).Style.Alignment.WrapText = true;
                    }
                    #endregion
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
                            "Duty Roster Variance List.xlsx"
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

        public async Task<IActionResult> ExportToExcelpendingreg(string Type, string SubType, Filter filterdata, int TotalRows)
        {
            try
            {

                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");

                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Unauthorized Absence ", "Excel");
                int slNo = 0;

                filterdata.length = TotalRows;
                filterdata.start = 0;

                string FilterValues = await _helpers.GetFilterValues(filterdata);

                //if (FilterValues == "")
                //{
                //    FilterValues = "All";
                //}

                string currDateTime = await _helpers.GetCurrentDate();
                

                //Get Dataset
                var dataset = await _service.GetListExcel(Type, SubType, filterdata);
                DataTable dt = dataset.Tables[0];
                dataset.Tables[0].Rows.RemoveAt(0);
                if ((dataset.Tables[0]?.Rows?.Count ?? 0) > 0)
                {
                    dt = dataset.Tables[0];
                }

                //set EXCEL Column Heading 
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (dt.Columns[i].ColumnName == "id" || dt.Columns[i].ColumnName == "fullName" || dt.Columns[i].ColumnName == "noOfDays")
                        dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.ToUpper();
                    else
                        dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
                }
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Pending Regularization");
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


                    var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:G{0}", 1, Char.ConvertFromUtf32(65 + 7)));
                    wsReportNameHeaderRange.Style.Font.Bold = true;
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Merge();
                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191); ;
                    wsReportNameHeaderRange.Value = "Unauthorized Absence";

                    worksheet.Cell(2, 1).Style.Font.Bold = true;
                    worksheet.Cell(2, 1).Value = "Group Company Name";
                    worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                    worksheet.Cell(2, 3).Style.Font.Bold = true;



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
                    //worksheet.Cell(4, 1).Style.Font.Bold = true;
                    //worksheet.Cell(4, 1).Value = "Filter Applied";
                    worksheet.Range("B4:H4").Merge();
                    worksheet.Cell("B4").Value = FilterValues;

                    var currentRow = 5;
                    worksheet.Cell(currentRow, 1).InsertTable(dt);
                    worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                    worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                    worksheet.Range("A5:" + colName + "5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Range("A5:" + colName + "5").Style.Font.Bold = true;


                    worksheet.Columns().AdjustToContents();
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
                            "Unauthorized Absence.xlsx"
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

        public async Task<IActionResult> ExportToExcelcontinousabsenteesim(string Type, string SubType, Filter filterdata, int TotalRows)
        {
            try
            {

                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");

                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Continous Absenteesim ", "Excel");
                int slNo = 0;

                filterdata.length = TotalRows;
                filterdata.start = 0;

                string FilterValues = await _helpers.GetFilterValues(filterdata);

                //if (FilterValues == "")
                //{
                //    FilterValues = "All";
                //}

                string currDateTime = await _helpers.GetCurrentDate();
                

                //Get Dataset
                var dataset = await _service.GetListExcel(Type, SubType, filterdata);
                DataTable dt = dataset.Tables[0];
                dataset.Tables[0].Rows.RemoveAt(0);
                if ((dataset.Tables[0]?.Rows?.Count ?? 0) > 0)
                {
                    dt = dataset.Tables[0];
                }



                //set EXCEL Column Heading 
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (dt.Columns[i].ColumnName == "id" || dt.Columns[i].ColumnName == "fullName" ||   dt.Columns[i].ColumnName == "from" || dt.Columns[i].ColumnName == "noOfDays")
                        dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.ToUpper();
                    else
                        dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
                }


                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Continous Absenteesim");
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


                    var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:H{0}", 1, Char.ConvertFromUtf32(65 + 7)));
                    wsReportNameHeaderRange.Style.Font.Bold = true;
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Merge();
                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191); ;
                    wsReportNameHeaderRange.Value = "Continous Absenteesim";

                    worksheet.Cell(2, 1).Style.Font.Bold = true;
                    worksheet.Cell(2, 1).Value = "Group Company Name";
                    worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                    worksheet.Cell(2, 3).Style.Font.Bold = true;



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
                    //worksheet.Cell(4, 1).Style.Font.Bold = true;
                    //worksheet.Cell(4, 1).Value = "Filter Applied";
                    worksheet.Range("B4:H4").Merge();
                    worksheet.Cell("B4").Value = FilterValues;

                    var currentRow = 5;
                    worksheet.Cell(currentRow, 1).InsertTable(dt);
                    worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                    worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                    worksheet.Range("A5:" + colName + "5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Range("A5:" + colName + "5").Style.Font.Bold = true;


                    worksheet.Columns().AdjustToContents();
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
                            "Continous Absenteesim.xlsx"
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

        public async Task<IActionResult> ExportToExcelupcomingleavedetails(string Type, string SubType, Filter filterdata, int TotalRows)
        {
            try
            {

                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");

                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Upcoming Leaves ", "Excel");
                int slNo = 0;

                filterdata.length = TotalRows;
                filterdata.start = 0;

                string FilterValues = await _helpers.GetFilterValues(filterdata);

                //if (FilterValues == "")
                //{
                //    FilterValues = "All";
                //}

                string currDateTime = await _helpers.GetCurrentDate();
                

                //Get Dataset
                var dataset = await _service.GetListExcel(Type, SubType, filterdata);
                DataTable dt = dataset.Tables[0];
                dataset.Tables[0].Rows.RemoveAt(0);
                if ((dataset.Tables[0]?.Rows?.Count ?? 0) > 0)
                {
                    dt = dataset.Tables[0];
                }

                //set EXCEL Column Heading 
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (dt.Columns[i].ColumnName == "id" || dt.Columns[i].ColumnName == "fullName" || dt.Columns[i].ColumnName == "noOfDays")
                        dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.ToUpper();
                    else
                        dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
                }
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Upcoming Leaves");
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


                    var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:I{0}", 1, Char.ConvertFromUtf32(65 + 10)));
                    wsReportNameHeaderRange.Style.Font.Bold = true;
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Merge();
                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191); ;
                    wsReportNameHeaderRange.Value = "Upcoming Leaves";

                    worksheet.Cell(2, 1).Style.Font.Bold = true;
                    worksheet.Cell(2, 1).Value = "Group Company Name";
                    worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                    worksheet.Cell(2, 3).Style.Font.Bold = true;



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
                    //worksheet.Cell(4, 1).Style.Font.Bold = true;
                    //worksheet.Cell(4, 1).Value = "Filter Applied";
                    worksheet.Range("B4:H4").Merge();
                    worksheet.Cell("B4").Value = FilterValues;

                    var currentRow = 5;
                    worksheet.Cell(currentRow, 1).InsertTable(dt);
                    worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                    worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                    worksheet.Range("A5:" + colName + "5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Range("A5:" + colName + "5").Style.Font.Bold = true;


                    worksheet.Columns().AdjustToContents();
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
                            "Upcoming Leaves.xlsx"
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

        public async Task<IActionResult> ExportToExcelunauthorisedabsence(string Type, string SubType, Filter filterdata, int TotalRows)
        {
            try
            {

                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");

                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Unauthorized Absence ", "Excel");
                int slNo = 0;

                filterdata.length = TotalRows;
                filterdata.start = 0;

                string FilterValues = await _helpers.GetFilterValues(filterdata);

                //if (FilterValues == "")
                //{
                //    FilterValues = "All";
                //}

                string currDateTime = await _helpers.GetCurrentDate();
                

                //Get Dataset
                var dataset = await _service.GetListExcel(Type, SubType, filterdata);
                DataTable dt = dataset.Tables[0];
                dataset.Tables[0].Rows.RemoveAt(0);
                if ((dataset.Tables[0]?.Rows?.Count ?? 0) > 0)
                {
                    dt = dataset.Tables[0];
                }

                //set EXCEL Column Heading 
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (dt.Columns[i].ColumnName == "id" || dt.Columns[i].ColumnName == "fullName" ||  dt.Columns[i].ColumnName == "noOfDays")
                        dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.ToUpper();
                    else
                        dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
                }
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Unauthorized Absence");
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


                    var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:G{0}", 1, Char.ConvertFromUtf32(65 + 7)));
                    wsReportNameHeaderRange.Style.Font.Bold = true;
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Merge();
                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191); ;
                    wsReportNameHeaderRange.Value = "Unauthorized Absence";

                    worksheet.Cell(2, 1).Style.Font.Bold = true;
                    worksheet.Cell(2, 1).Value = "Group Company Name";
                    worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                    worksheet.Cell(2, 3).Style.Font.Bold = true;



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
                    //worksheet.Cell(4, 1).Style.Font.Bold = true;
                    //worksheet.Cell(4, 1).Value = "Filter Applied";
                    worksheet.Range("B4:H4").Merge();
                    worksheet.Cell("B4").Value = FilterValues;

                    var currentRow = 5;
                    worksheet.Cell(currentRow, 1).InsertTable(dt);
                    worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                    worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                    worksheet.Range("A5:" + colName + "5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Range("A5:" + colName + "5").Style.Font.Bold = true;


                    worksheet.Columns().AdjustToContents();
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
                            "Unauthorized Absence.xlsx"
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

       
        public async Task<IActionResult> ExportToExcellesshours(string Type, string SubType, Filter filterdata, int TotalRows)
        {
            try
            {

                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");

                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Less Hours ", "Excel");
                int slNo = 0;

                filterdata.length = TotalRows;
                filterdata.start = 0;

                string FilterValues = await _helpers.GetFilterValues(filterdata);

                //if (FilterValues == "")
                //{
                //    FilterValues = "All";
                //}

                string currDateTime = await _helpers.GetCurrentDate();
                

                //Get Dataset
                var dataset = await _service.GetListExcel(Type, SubType, filterdata);
                DataTable dt = dataset.Tables[0];
                dataset.Tables[0].Rows.RemoveAt(0);
                if ((dataset.Tables[0]?.Rows?.Count ?? 0) > 0)
                {
                    dt = dataset.Tables[0];
                }
                //set EXCEL Column Heading 
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (dt.Columns[i].ColumnName == "id" || dt.Columns[i].ColumnName == "fullName"  || dt.Columns[i].ColumnName == "dateofApplication" || dt.Columns[i].ColumnName == "lessHours" || dt.Columns[i].ColumnName == "type")
                        dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.ToUpper();
                    else
                        dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
                }
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Less Hours");
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


                    var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 7)));
                    wsReportNameHeaderRange.Style.Font.Bold = true;
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Merge();
                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191); ;
                    wsReportNameHeaderRange.Value = "Less Hours";

                    worksheet.Cell(2, 1).Style.Font.Bold = true;
                    worksheet.Cell(2, 1).Value = "Group Company Name";
                    worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                    worksheet.Cell(2, 3).Style.Font.Bold = true;



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
                    //worksheet.Cell(4, 1).Style.Font.Bold = true;
                    //worksheet.Cell(4, 1).Value = "Filter Applied";
                    worksheet.Range("B4:H4").Merge();
                    worksheet.Cell("B4").Value = FilterValues;

                    var currentRow = 5;
                    worksheet.Cell(currentRow, 1).InsertTable(dt);
                    worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                    worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                    worksheet.Range("A5:" + colName + "5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Range("A5:" + colName + "5").Style.Font.Bold = true;


                    worksheet.Columns().AdjustToContents();
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
                            "Less Hours.xlsx"
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

        //public async Task<IActionResult> ExportToExcel(string Type, string? SubType, Filter filterdata, int TotalRows)
        //{
        //    TimeOfficeDashBoardNew model = new TimeOfficeDashBoardNew();
        //    var LoginUser = HttpContext.Session.GetString("LoginUser");
        //    var CompanyName = HttpContext.Session.GetString("CompanyName");
        //    var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
        //    var GroupCompanyName = User.authUser.GroupCompanyName;
        //    ExcelPassword EP = new ExcelPassword();
        //    EP = await _commonService.GetExcelPassword("Attrition Report List", "Excel");


        //    filterdata.length = TotalRows;
        //    filterdata.start = 0;

        //    string FilterValues = await _helpers.GetFilterValues(filterdata);
        //    if (FilterValues == "")
        //    {
        //        FilterValues = "All";
        //    }
        //    model.filter = filterdata;



        //    string[] cu_Date = System.DateTime.Now.GetDateTimeFormats();
        //    string currDateTime = cu_Date[44];

        //    //Get Dataset
        //    var dataset = await _service.GetDashBoardDetailsListExcel(Type, SubType, filterdata);
        //    DataTable dt = dataset.Tables[0];
        //    dataset.Tables[0].Rows.RemoveAt(0);
        //    if ((dataset.Tables[0]?.Rows?.Count ?? 0) > 0)
        //    {
        //        dt = dataset.Tables[0];
        //    }

        //    //set EXCEL Column Heading 
        //    for (int i = 0; i < dt.Columns.Count; i++)
        //    {
        //        dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
        //    }

        //    using (var workbook = new XLWorkbook())
        //    {
        //        var worksheet = workbook.Worksheets.Add("Duty Roster Variance List");
        //        int count = dt.Rows.Count + 6;


        //        worksheet.Range("A1:K" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
        //        worksheet.Range("A1:K" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
        //        worksheet.Range("A1:K" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
        //        worksheet.Range("A1:K" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
        //        worksheet.Range("A1:K" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;



        //        var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 10)));
        //        wsReportNameHeaderRange.Style.Font.Bold = true;
        //        wsReportNameHeaderRange.Style.Font.FontSize = 15;
        //        wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        //        wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
        //        wsReportNameHeaderRange.Merge();
        //        wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
        //        wsReportNameHeaderRange.Value = "Duty Roster Variance List";

        //        worksheet.Cell(2, 1).Style.Font.Bold = true;
        //        worksheet.Cell(2, 1).Value = "Group Company Name";
        //        worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
        //        worksheet.Cell(2, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);

        //        worksheet.Cell(3, 1).Style.Font.Bold = true;
        //        worksheet.Cell(3, 1).Value = "FromDate";
        //        worksheet.Cell(3, 2).Value = Type;
        //        worksheet.Cell(3, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //        worksheet.Cell(3, 2).Style.DateFormat.Format = "dd-MMM-yyyy";
        //        worksheet.Cell(3, 3).Style.Font.Bold = true;
        //        worksheet.Cell(3, 3).Value = "ToDate";
        //        worksheet.Cell(3, 4).Value = SubType;
        //        worksheet.Cell(3, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //        worksheet.Cell(3, 4).Style.DateFormat.Format = "dd-MMM-yyyy";



        //        worksheet.Cell(4, 1).Style.Font.Bold = true;
        //        worksheet.Cell(4, 1).Value = "Login User";
        //        worksheet.Cell(4, 2).Value = LoginUser;
        //        worksheet.Cell(4, 3).Style.Font.Bold = true;
        //        worksheet.Cell(4, 3).Value = "Date & Time";
        //        worksheet.Cell(4, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //        worksheet.Cell(4, 4).Style.DateFormat.Format = "dd-MMM-yyyy";
        //        worksheet.Cell(4, 4).Value = currDateTime.ToString();
        //        worksheet.Cell(4, 5).Style.Font.Bold = true;
        //        worksheet.Cell(4, 5).Value = "Total Count";
        //        worksheet.Cell(4, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //        worksheet.Cell(4, 6).Value = dt.Rows.Count;
        //        worksheet.Cell(5, 1).Style.Font.Bold = true;
        //        worksheet.Cell(5, 1).Value = "Filter Applied";
        //        var filterapplied = worksheet.Range("B5:K5").Merge();

        //        filterapplied.Value = FilterValues;


        //        var currentRow = 6;


        //        worksheet.Cell(currentRow, 1).InsertTable(dt);
        //        worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

        //        worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
        //        worksheet.Range("A6:K6").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
        //        worksheet.Range("A6:K6").Style.Font.Bold = true;


        //        worksheet.Columns().AdjustToContents();
        //        worksheet.Rows().AdjustToContents();
        //        using (var stream = new MemoryStream())
        //        {
        //            workbook.SaveAs(stream);
        //            if (EP.NeedPassword)
        //            {
        //                Workbook book = new Workbook();
        //                book.LoadFromStream(stream);
        //                //Protect Workbook
        //                book.Protect(EP.Password);
        //                book.SaveToStream(stream);
        //            }
        //            var content = stream.ToArray();
        //            return File(
        //                content,
        //                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
        //                "DutyRosterVariance List.xlsx"
        //                );
        //        }
        //    }

        //    //}
        //}






    }
}
