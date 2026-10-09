using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.Account;
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
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.DashBoard.Controllers
{
    [Area("DashBoard")]
    [SessionAuthorize]
    public class LandingPageController : Controller
    {
        private readonly ILandingPageService _service;
        private readonly ICommonService _commonService;
        private readonly IDropdownService _drop;
        private readonly IAccountService _account;
        private readonly ITimeOfficeDashBoardService _timeOfficeservice;
        private readonly IPayRollDashBoardService _PayRollservice;
        private readonly IHelpers _helpers;
        public LandingPageController(ILandingPageService landingPageService, ICommonService commonService,
            ITimeOfficeDashBoardService timeOfficeService, IDropdownService drop, IAccountService account, IPayRollDashBoardService PayRollservice, IHelpers helpers)
        {
            _service = landingPageService;
            _commonService = commonService;
            _timeOfficeservice = timeOfficeService;
            _drop = drop;
            _account = account;
            _PayRollservice = PayRollservice;
            _helpers = helpers;
        }
        public class MyModel
        {            
            public int GroupCompanyCode { get; set; }
            public string UserCode { get; set; }
            public string Password { get; set; }
            
        }
        public async Task<IActionResult> Index()
        {
            LandingPage model = new LandingPage();
            model = await _service.GetDashboardDetails();            
            return View("~/Areas/Dashboard/Views/LandingPage/Index.cshtml", model);
        }
        #region GROUP CMPANY CHANGE
        [HttpGet]
        public async Task<IActionResult> GroupCompnyChange(int  GrpCompanyCode)
        {
            try
            {              
                string token = HttpContext.Session.GetString("JwtToken");
                if (token != null)
                {
                    Login model = new Login();
                    model.MACAddress = getMacAddress();
                    model.software = "HRMS";
                    model.INOUT = "OUT";
                    await _account.LogHistory(model);
                }
                HttpContext.Session.Clear();
                foreach (var cookie in Request.Cookies.Keys)
                {
                    if (cookie != "Career_Portal_Cookie" && cookie != "Essp_Cookie")
                    {
                        HttpContext.Session.Remove(cookie);
                        HttpContext.Response.Cookies.Delete(cookie);
                    }

                }
              
              
                return Json(true);
            }
            catch(Exception ex)
            {
                return  Json(false);
            }
        }

      
            public string getMacAddress()
        {
            string hex = "";
            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (nic.NetworkInterfaceType == NetworkInterfaceType.Ethernet && nic.OperationalStatus == OperationalStatus.Up)
                {
                    hex = BitConverter.ToString(nic.GetPhysicalAddress().GetAddressBytes());
                }
                else if (nic.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 && nic.OperationalStatus == OperationalStatus.Up)
                {
                    hex = BitConverter.ToString(nic.GetPhysicalAddress().GetAddressBytes());
                }
            }
            return hex;
        }
        #endregion  #region GROUP CMPANY CHANGE
        public async Task<IActionResult> GetDatabankNotifications(string Type)
        {
            LandingPage model = new LandingPage();
            model = await _service.GetDatabankNotifications(Type);
            for (int i = 0; i < model.DatabankNotificationList.Count; i++)
            {
                if (model.DatabankNotificationList[i].Type == "Resignation Approval pending by RM")
                {
                    model.DatabankNotificationList[i].Action = "/DataBank/Resignation/Index?FirstTab=All";
                }
                if (model.DatabankNotificationList[i].Type == "Resignation Acceptance pending by HR")
                {
                    model.DatabankNotificationList[i].Action = "/DataBank/Resignation/Index?FirstTab=ResignationPending";
                }
                if (model.DatabankNotificationList[i].Type == "Personal details approval pending")
                {
                    model.DatabankNotificationList[i].Action = "/DataBank/PersonalDetailsApprove/Index";
                }
                if (model.DatabankNotificationList[i].Type == "Job transition approval pending")
                {
                    model.DatabankNotificationList[i].Action = "/DataBank/JobTransition/Index";
                }
                if (model.DatabankNotificationList[i].Type == "Employee activation approval pending")
                {
                    model.DatabankNotificationList[i].Action = "/DataBank/OnBoarding/Index?FirstTab=EmployeeActivation";
                }
                if (model.DatabankNotificationList[i].Type == "Preenrollment approval pending")
                {
                    model.DatabankNotificationList[i].Action = "/DataBank/OnBoarding/Index?FirstTab=PreEnrollmentApproval";
                }
                if (model.DatabankNotificationList[i].Type == "Joining confirmation pending")
                {
                    model.DatabankNotificationList[i].Action = "/DataBank/OnBoarding/Index?FirstTab=JoiningConfirmation";
                }
                if (model.DatabankNotificationList[i].Type == "Employee edit approval pending")
                {
                    model.DatabankNotificationList[i].Action = "/DataBank/EditEmployeeInfo/Index?FirstTab=EditEmployeePending";
                }
                if (model.DatabankNotificationList[i].Type == "Job transition dues pending")
                {
                    model.DatabankNotificationList[i].Action = "/DataBank/JobTransitionDueList/Index";
                }
                if (model.DatabankNotificationList[i].Type == "Recent separations")
                {
                    model.DatabankNotificationList[i].Action = "/DataBank/SeparationReport/Index";
                }
                if (model.DatabankNotificationList[i].Type == "Job transition confirmation pending")
                {
                    model.DatabankNotificationList[i].Action = "/DataBank/JobTransitionConfirmation/Index";
                }
                if (model.DatabankNotificationList[i].Type == "Social Wall Approval pending")
                {
                    model.DatabankNotificationList[i].Action = "/DataBank/SocialWall/Index?TabIndex=PENDING";
                }
                if (model.DatabankNotificationList[i].Type == "Probation completion pending")
                {
                    model.DatabankNotificationList[i].Action = "/DataBank/RenewalInformation/Index";
                }
                if (model.DatabankNotificationList[i].Type == "Retirement due")
                {
                    model.DatabankNotificationList[i].Action = "/DataBank/RenewalInformation/Index";
                }
                if (model.DatabankNotificationList[i].Type == "Training completion pending")
                {
                    model.DatabankNotificationList[i].Action = "/DataBank/RenewalInformation/Index";
                }
                if (model.DatabankNotificationList[i].Type == "Contract renewal pending")
                {
                    model.DatabankNotificationList[i].Action = "/DataBank/RenewalInformation/Index";
                }
            }
            return PartialView("~/Areas/Dashboard/Views/LandingPage/_DatabankNotification.cshtml", model);

        }

        public async Task<IActionResult> GetDataBankNotificationsPopUp(int NotificationCode, string Type)
        {
            try
            {
                return RedirectToAction("GetNotificationsPopUp", "DataBankDashBoard", new { Area = "DashBoard", NotificationCode = NotificationCode, Type = Type });
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
        public async Task<IActionResult> GetTimeOfficeNotifications(string Type)
        {
            LandingPage model = new LandingPage();
            TimeOfficeDashBoardNew m = new TimeOfficeDashBoardNew();
            m = await _timeOfficeservice.GetDashBoardNotificationDetails(Type);
            //m = await _service.GetTimeOfficeNotifications();
            model.TimeOfficeNotificationList=m.DashBoardNotificationList;
            for (int i = 0; i < model.TimeOfficeNotificationList.Count; i++)
            {
                if (model.TimeOfficeNotificationList[i].Type == "Shift missing employees")
                {
                    model.TimeOfficeNotificationList[i].Action = "/TimeOffice/AttendanceSetting/Index";
                }
                if (model.TimeOfficeNotificationList[i].Type == "Off day not allocated")
                {
                    model.TimeOfficeNotificationList[i].Action = "/TimeOffice/AttendanceSetting/Index";
                }
                if (model.TimeOfficeNotificationList[i].Type == "Employees completing ML/ESI leave in next 7 Days")
                {
                    model.TimeOfficeNotificationList[i].Action = "/DashBoard/TimeOfficeDashBoard/MaternityESILeaveCompletion";
                }
                if (model.TimeOfficeNotificationList[i].Type == "Punching ID missing")
                {
                    model.TimeOfficeNotificationList[i].Action = "/TimeOffice/AttendanceSetting/Index";
                }
                if (model.TimeOfficeNotificationList[i].Type == "Leave group missing")
                {
                    model.TimeOfficeNotificationList[i].Action = "/TimeOffice/AttendanceSetting/Index";
                }
                if (model.TimeOfficeNotificationList[i].Type == "Holiday group missing")
                {
                    model.TimeOfficeNotificationList[i].Action = "/TimeOffice/AttendanceSetting/Index";
                }
                if (model.TimeOfficeNotificationList[i].Type == "Over time approval missing")
                {
                    model.TimeOfficeNotificationList[i].Action = "/TimeOffice/DailyOTConfirmation/Index";
                }
                if (model.TimeOfficeNotificationList[i].Type == "Leave approval pending")
                {
                    model.TimeOfficeNotificationList[i].Action = "/TimeOffice/HRAttendanceApproval/Index?FirstTab=LeaveGroupMissing";
                }
                if (model.TimeOfficeNotificationList[i].Type == "On duty approval")
                {
                    model.TimeOfficeNotificationList[i].Action = "/TimeOffice/HRAttendanceApproval/Index?FirstTab=OnDutyMissing";
                }
                if (model.TimeOfficeNotificationList[i].Type == "WFH approval")
                {
                    model.TimeOfficeNotificationList[i].Action = "/TimeOffice/HRAttendanceApproval/Index?FirstTab=WFHApproval";
                }
                if (model.TimeOfficeNotificationList[i].Type == "Attendance Regularization approval")
                {
                    model.TimeOfficeNotificationList[i].Action = "/TimeOffice/HRAttendanceApproval/Index?FirstTab=AttendaceRegularisation";
                }
                if (model.TimeOfficeNotificationList[i].Type == "Permission hours approval")
                {
                    model.TimeOfficeNotificationList[i].Action = "/TimeOffice/HRAttendanceApproval/Index?FirstTab=PermissionHoursApproval";
                }
            }
            return PartialView("~/Areas/Dashboard/Views/LandingPage/_TimeofficeNotification.cshtml", model);

        }

        public async Task<IActionResult> GetTimeOfficeNotificationsPopUp(int NotificationCode)
        {
            try
            {
                return RedirectToAction("GetNotificationsPopUp", "TimeOfficeDashBoard", new { Area = "DashBoard" , NotificationCode = NotificationCode });
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
        public async Task<IActionResult> GetPayrollNotifications(string Type)
        {
            LandingPage model = new LandingPage();
            model = await _service.GetPayrollNotifications(Type);
            for (int i = 0; i < model.PayrollNotificationList.Count; i++)
            {
                if (model.PayrollNotificationList[i].Type == "Loan approval pending")
                {
                    model.PayrollNotificationList[i].Action = "/PayRoll/LoanIssue/Index";
                }
                if (model.PayrollNotificationList[i].Type == "Variable Allowance not updated")
                {
                    model.PayrollNotificationList[i].Action = "/PayRoll/VariableAllowance/Index";
                }
                if (model.PayrollNotificationList[i].Type == "Variable deductions not updated")
                {
                    model.PayrollNotificationList[i].Action = "/PayRoll/VariableDeduction/Index";
                }
                if (model.PayrollNotificationList[i].Type == "Offday wage applicable but settings missing")
                {
                    model.PayrollNotificationList[i].Action = "/PayRoll/OffdayWageSettings/Index";
                }
                if (model.PayrollNotificationList[i].Type == "Holiday wage applicable but settings missing")
                {
                    model.PayrollNotificationList[i].Action = "/PayRoll/HolidayWageSettings/Index";
                }
                if (model.PayrollNotificationList[i].Type == "Over time wage applicable but settings missing")
                {
                    model.PayrollNotificationList[i].Action = "/PayRoll/OverTimeSettings/Index";
                }
                if (model.PayrollNotificationList[i].Type == "PF pension fund expired employees")
                {
                    model.PayrollNotificationList[i].Action = "/PayRoll/PFSettings/Index";
                }
                if (model.PayrollNotificationList[i].Type == "PF general settings variance")
                {
                    model.PayrollNotificationList[i].Action = "/PayRoll/PFSettings/Index";
                }
                if (model.PayrollNotificationList[i].Type == "PF UAN missing")
                {
                    model.PayrollNotificationList[i].Action = "/PayRoll/PFSettings/Index";
                }
                if (model.PayrollNotificationList[i].Type == "PF group missing")
                {
                    model.PayrollNotificationList[i].Action = "/PayRoll/PFSettings/Index";



                }
                if (model.PayrollNotificationList[i].Type == "ESI expiry update")
                {
                    model.PayrollNotificationList[i].Action = "/PayRoll/ESISettings/Index";
                }
                if (model.PayrollNotificationList[i].Type == "ESI general settings variance")
                {
                    model.PayrollNotificationList[i].Action = "/PayRoll/ESISettings/Index";
                }
                if (model.PayrollNotificationList[i].Type == "ESI group missing")
                {
                    model.PayrollNotificationList[i].Action = "/PayRoll/ESISettings/Index";
                }
                if (model.PayrollNotificationList[i].Type == "ESI No missing")
                {
                    model.PayrollNotificationList[i].Action = "/PayRoll/ESISettings/Index";
                }
            }
            return PartialView("~/Areas/Dashboard/Views/LandingPage/_PayrollNotification.cshtml", model);
        }

        public async Task<IActionResult> GetPayRollNotificationsPopUp(int NotificationCode, string Type)
        {
            try
            {
                return RedirectToAction("GetEmployeeDetailsPopUp", "PayRollDashBoard", new { Area = "DashBoard", NotificationCode = NotificationCode, Type = Type });
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
        public async Task<IActionResult> GetPayrollApplicableCount()
        {
            LandingPage model = new LandingPage();
            PayRollDashBoardNew m = new PayRollDashBoardNew();
            m = await _service.GetPayrollApplicableCount();
            model.PayrollApplicableList = m.ApplicableCountList;
            return PartialView("~/Areas/Dashboard/Views/LandingPage/_PayrollApplicabilityList.cshtml", model);

        }
        public async Task<IActionResult> GetApplicableCountDetails(string Type, string SubType)
        {

            try
            {
                PayRollDashBoardNew model = new PayRollDashBoardNew();
                model = await _service.GetApplicableCountDetails(Type, SubType);
                model.ApplicableType = Type;
                model.ApplicableSubType = SubType;
                if (SubType == "YES" || SubType == "NO")
                    return PartialView("_ApplicabilityPopUp", model);
                else
                    return PartialView("_ApplicabilityVariancePopUp", model);
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
        public async Task<IActionResult> ExportToExcelApplicabilityCount(string Type, string SubType)
        {
            try
            {
                PayRollDashBoardNew model = new PayRollDashBoardNew();
                model = await _service.GetApplicableCountDetails(Type, SubType);
                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword(Type + " Applicability", "Excel");
                int slNo = 0;
                string currDateTime = await _helpers.GetCurrentDate();
                

                if (SubType == "YES" || SubType == "NO")
                {
                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add(Type + " Applicability");
                        int count = model.ApplicableList.Count + 4;


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
                        wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191); ;

                        wsReportNameHeaderRange.Value = Type + " Applicability";
                        worksheet.Cell(2, 1).Style.Font.Bold = true;
                        worksheet.Cell(2, 1).Value = "Company Name";
                        worksheet.Cell(2, 2).Value = CompanyName.ToUpper();

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
                        worksheet.Cell(3, 6).Value = model.ApplicableList.Count;
                        //worksheet.Cell(4, 1).Style.Font.Bold = true;
                        //worksheet.Cell(4, 1).Value = "Filter Applied";
                        //worksheet.Range("B3:H3").Merge();
                        //worksheet.Cell("B4").Value = FilterValues;

                        var currentRow = 4;
                        #region Header
                        worksheet.Range("A4:G4").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                        worksheet.Cell(currentRow, 1).Value = "Sl.No";
                        worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 2).Value = "ID";
                        worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 3).Value = "Employee Name";
                        worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 4).Value = "Designation";
                        worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 5).Value = "Department";
                        worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 6).Value = "Location";
                        worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 7).Value = Type + " Group";
                        worksheet.Cell(currentRow, 7).Style.Font.Bold = true;


                        #endregion

                        #region Body
                        foreach (var item in model.ApplicableList)
                        {
                            currentRow++;
                            slNo = slNo + 1;
                            worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 1).Value = slNo;
                            worksheet.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 2).Value = item.EmployeeID;
                            worksheet.Cell(currentRow, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 3).Value = item.FullName;
                            worksheet.Cell(currentRow, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 4).Value = item.Designation;
                            worksheet.Cell(currentRow, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 5).Value = item.Department;
                            worksheet.Cell(currentRow, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 6).Value = item.Location;
                            worksheet.Cell(currentRow, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 7).Value = item.GroupName;


                            worksheet.Columns(1, 8).Style.Alignment.WrapText = true;
                            worksheet.Rows().Style.Alignment.WrapText = true;
                        }
                        #endregion
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
                                Type + " Applicability.xlsx"
                                );
                        }
                    }
                }

                else
                {
                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add(Type + " Applicability");
                        int count = model.ESI_PF_PT_Variance_Applicable.Count + 4;


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
                        wsReportNameHeaderRange.Value = Type + " Applicability";
                        worksheet.Cell(2, 1).Style.Font.Bold = true;
                        worksheet.Cell(2, 1).Value = "Company Name";
                        worksheet.Cell(2, 2).Value = CompanyName.ToUpper();

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
                        worksheet.Cell(3, 6).Value = model.ESI_PF_PT_Variance_Applicable.Count;
                        //worksheet.Cell(4, 1).Style.Font.Bold = true;
                        //worksheet.Cell(4, 1).Value = "Filter Applied";
                        //worksheet.Range("B3:H3").Merge();
                        //worksheet.Cell("B4").Value = FilterValues;

                        var currentRow = 4;
                        #region Header
                        worksheet.Range("A4:J4").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                        worksheet.Cell(currentRow, 1).Value = "Sl.No";
                        worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 2).Value = "ID";
                        worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 3).Value = "Employee Name";
                        worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 4).Value = "Designation";
                        worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 5).Value = "Department";
                        worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 6).Value = "Location";
                        worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 7).Value = "Old" + Type + "Grp";
                        worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 8).Value = "New" + Type + "Grp";
                        worksheet.Cell(currentRow, 8).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 9).Value = "Old Appli.";
                        worksheet.Cell(currentRow, 9).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 10).Value = "New Appli.";
                        worksheet.Cell(currentRow, 10).Style.Font.Bold = true;

                        #endregion

                        #region Body
                        foreach (var item in model.ESI_PF_PT_Variance_Applicable)
                        {
                            currentRow++;
                            slNo = slNo + 1;
                            worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 1).Value = slNo;
                            worksheet.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 2).Value = item.EmployeeID;
                            worksheet.Cell(currentRow, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 3).Value = item.FullName;
                            worksheet.Cell(currentRow, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 4).Value = item.Designation;
                            worksheet.Cell(currentRow, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 5).Value = item.Department;
                            worksheet.Cell(currentRow, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 6).Value = item.Location;
                            worksheet.Cell(currentRow, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 7).Value = item.OldGroupName;
                            worksheet.Cell(currentRow, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 8).Value = item.NewGroupName;
                            worksheet.Cell(currentRow, 9).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 9).Value = item.OldApplicable;
                            worksheet.Cell(currentRow, 10).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 10).Value = item.NewApplicable;


                            worksheet.Columns(1, 11).Style.Alignment.WrapText = true;
                            worksheet.Rows().Style.Alignment.WrapText = true;
                        }
                        #endregion
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
                                Type + " Applicability.xlsx"
                                );
                        }
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

        public async Task<IActionResult> GetPopupDetails(string TileName)
        {
            LandingPage model = new LandingPage();
            if (TileName == "Transfers")
            {
                model = await _service.GetJobTransferPopupDetails();
                return PartialView("~/Areas/Dashboard/Views/LandingPage/_JobTransferPopup.cshtml", model);
            }
            else if (TileName == "Promotions")
            {
                model = await _service.GetPromotionPopupDetails();
                return PartialView("~/Areas/Dashboard/Views/LandingPage/_PromotionPopup.cshtml", model);
            }
            return null;
        }

        public async Task<IActionResult> LoginDetails(string fromDate, string  toDate)
        {
            try
            {
                LandingPage model = new LandingPage();
                model.fromDate = DateTime.Now;
                model.toDate = DateTime.Now;
                model = await _service.GetLoginDetails(model.fromDate.ToString("yyyy-MM-dd"), model.toDate.ToString("yyyy-MM-dd")); ;
                return View("~/Areas/Dashboard/Views/LandingPage/LoginDetails.cshtml", model);
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
        public async Task<IActionResult> GetLoginDetails(string fromDate, string toDate)
        {
            try
            {
                LandingPage model = new LandingPage();
                model = await _service.GetLoginDetails(fromDate, toDate);
                return PartialView("~/Areas/Dashboard/Views/LandingPage/_CombinedView.cshtml", model);
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
