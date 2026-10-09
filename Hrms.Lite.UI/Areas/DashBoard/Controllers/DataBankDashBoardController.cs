using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.DashBoard;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.Account;
using Hrms.Lite.Shared.DashBoard;
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

namespace Hrms.Lite.UI.Areas.DashBoard.Controllers
{
    [Area("DashBoard")]
    [SessionAuthorize]
    public class DataBankDashBoardController : Controller
    {
        private readonly IDataBankDashBoardService _service;
        private readonly IOnBoardingService _onBoardingService;
        private readonly ICommonService _commonService;
        private readonly IDropdownService _dropdownService;
        private readonly IHelpers _helpers;
        public DataBankDashBoardController(IDataBankDashBoardService service, IOnBoardingService onBoardingService, ICommonService commonService, IDropdownService dropdownService, IHelpers helpers)
        {
            _service = service;
            _onBoardingService = onBoardingService;
            _commonService = commonService;
            _dropdownService = dropdownService;
            _helpers = helpers;
        }
        public async Task<IActionResult> Index()
        {
            DataBankDashBoardNew model = new DataBankDashBoardNew();
            DataBankDashBoardNew _model = new DataBankDashBoardNew();
            model = await _service.GetNewDashBoardDetails();
            _model = await _service.GetDatabankNotifications();
            model.DashBoardNotificationList = _model.DashBoardNotificationList;

            for (int i = 0; i < model.DashBoardNotificationList.Count; i++)
            {
                if (model.DashBoardNotificationList[i].Type == "Resignation Acceptance pending by HR")
                {
                    model.DashBoardNotificationList[i].Action = "/DataBank/Resignation/Index?FirstTab=ResignationPending";
                }
                if (model.DashBoardNotificationList[i].Type == "Personal details approval pending")
                {
                    model.DashBoardNotificationList[i].Action = "/DataBank/PersonalDetailsApprove/Index";
                }
                if (model.DashBoardNotificationList[i].Type == "Job transition approval pending")
                {
                    model.DashBoardNotificationList[i].Action = "/DataBank/JobTransition/Index";
                }
                if (model.DashBoardNotificationList[i].Type == "Employee activation approval pending")
                {
                    model.DashBoardNotificationList[i].Action = "/DataBank/OnBoarding/Index?FirstTab=EmployeeActivation";
                }
                if (model.DashBoardNotificationList[i].Type == "Preenrollment approval pending")
                {
                    model.DashBoardNotificationList[i].Action = "/DataBank/OnBoarding/Index?FirstTab=PreEnrollmentApproval";
                }
                if (model.DashBoardNotificationList[i].Type == "Joining confirmation pending")
                {
                    model.DashBoardNotificationList[i].Action = "/DataBank/OnBoarding/Index?FirstTab=JoiningConfirmation";
                }
                if (model.DashBoardNotificationList[i].Type == "Employee edit approval pending")
                {
                    model.DashBoardNotificationList[i].Action = "/DataBank/EditEmployeeInfoApprove/Index";
                }
                if (model.DashBoardNotificationList[i].Type == "Job transition dues pending")
                {
                    model.DashBoardNotificationList[i].Action = "/DataBank/JobTransitionDueList/Index";
                }
                if (model.DashBoardNotificationList[i].Type == "Recent separations")
                {
                    model.DashBoardNotificationList[i].Action = "/DataBank/SeparationReport/Index";
                }
                if (model.DashBoardNotificationList[i].Type == "Job transition confirmation pending")
                {
                    model.DashBoardNotificationList[i].Action = "/DataBank/JobTransitionConfirmation/Index";
                }
                if (model.DashBoardNotificationList[i].Type == "Probation completion pending")
                {
                    model.DashBoardNotificationList[i].Action = "/DataBank/JobTransitionDueList/Index?TabIndex=4";
                }
                if (model.DashBoardNotificationList[i].Type == "Training completion pending")
                {
                    model.DashBoardNotificationList[i].Action = "/DataBank/JobTransitionDueList/Index?TabIndex=2";
                }
                if (model.DashBoardNotificationList[i].Type == "Contract renewal pending")
                {
                    model.DashBoardNotificationList[i].Action = "/DataBank/JobTransitionDueList/Index?TabIndex=5";
                }
                if (model.DashBoardNotificationList[i].Type == "Retirement due")
                {
                    //model.DashBoardNotificationList[i].Action = "/DataBank/JobTransitionDueList/Index?TabIndex=1000";
                    model.DashBoardNotificationList[i].Action = "/DataBank/RetirementDueReport/Index";
                }
                if (model.DashBoardNotificationList[i].Type == "Social Wall Approval Pending")
                {
                    model.DashBoardNotificationList[i].Action = "/DataBank/SocialWall/Index?TabIndex=PENDING";
                }
                if (model.DashBoardNotificationList[i].Type == "Resignation Approval pending by RM")
                {
                    model.DashBoardNotificationList[i].Action = "/DataBank/Resignation/Index?FirstTab=ApprovalPending";
                }
            }
            return View(model);
        }
        public async Task<IActionResult> IndexV3()
        {
            DataBankDashBoardNew model = new DataBankDashBoardNew();
            DataBankDashBoardNew _model = new DataBankDashBoardNew();
            model = await _service.GetNewDashBoardDetails();
            _model = await _service.GetDatabankNotifications();
            model.DashBoardNotificationList = _model.DashBoardNotificationList;
            //model.DataBankDashBoardDiagram = _service.GetDashBoardDiagrams("DESIGNATION").Result.DataBankDashBoardDiagram;
            for (int i = 0; i < model.DashBoardNotificationList.Count; i++)
            {
                if (model.DashBoardNotificationList[i].Type == "Resignation Acceptance pending by HR")
                {
                    model.DashBoardNotificationList[i].Action = "/DataBank/Resignation/Index?FirstTab=ResignationPending";
                }
                if (model.DashBoardNotificationList[i].Type == "Personal details approval pending")
                {
                    model.DashBoardNotificationList[i].Action = "/DataBank/PersonalDetailsApprove/Index";
                }
                if (model.DashBoardNotificationList[i].Type == "Job transition approval pending")
                {
                    model.DashBoardNotificationList[i].Action = "/DataBank/JobTransition/Index";
                }
                if (model.DashBoardNotificationList[i].Type == "Employee activation approval pending")
                {
                    model.DashBoardNotificationList[i].Action = "/DataBank/OnBoarding/Index?FirstTab=EmployeeActivation";
                }
                if (model.DashBoardNotificationList[i].Type == "Preenrollment approval pending")
                {
                    model.DashBoardNotificationList[i].Action = "/DataBank/OnBoarding/Index?FirstTab=PreEnrollmentApproval";
                }
                if (model.DashBoardNotificationList[i].Type == "Joining confirmation pending")
                {
                    model.DashBoardNotificationList[i].Action = "/DataBank/OnBoarding/Index?FirstTab=JoiningConfirmation";
                }
                if (model.DashBoardNotificationList[i].Type == "Employee edit approval pending")
                {
                    model.DashBoardNotificationList[i].Action = "/DataBank/EditEmployeeInfoApprove/Index";
                }
                if (model.DashBoardNotificationList[i].Type == "Job transition dues pending")
                {
                    model.DashBoardNotificationList[i].Action = "/DataBank/JobTransitionDueList/Index";
                }
                if (model.DashBoardNotificationList[i].Type == "Recent separations")
                {
                    model.DashBoardNotificationList[i].Action = "/DataBank/SeparationReport/Index";
                }
                if (model.DashBoardNotificationList[i].Type == "Job transition confirmation pending")
                {
                    model.DashBoardNotificationList[i].Action = "/DataBank/JobTransitionConfirmation/Index";
                }
                if (model.DashBoardNotificationList[i].Type == "Social Wall Approval pending")
                {
                    model.DashBoardNotificationList[i].Action = "/DataBank/SocialWall/Index?TabIndex=PENDING";
                }
                if (model.DashBoardNotificationList[i].Type == "Social Wall Approval pending")
                {
                    model.DashBoardNotificationList[i].Action = "/DataBank/SocialWall/Index?TabIndex=PENDING";
                }
                if (model.DashBoardNotificationList[i].Type == "Resignation Approval pending by RM")
                {
                    model.DashBoardNotificationList[i].Action = "/DataBank/Resignation/Index?FirstTab=ApprovalPending";
                }
                if (model.DashBoardNotificationList[i].Type == "Retirement due")
                {
                    //model.DashBoardNotificationList[i].Action = "/DataBank/JobTransitionDueList/Index?TabIndex=1000";
                    model.DashBoardNotificationList[i].Action = "/DataBank/RetirementDueReport/Index";
                }



            }
            return View(model);
        }
        public async Task<IActionResult> GetNotificationsPopUp(string Type)
        {

            try
            {
                DataBankDashBoardNew model = new DataBankDashBoardNew();
                model = await _service.GetDashBoardNotificationDetails_PopUp(Type);
                //model.Type = Type;
                if (Type == "Certificate Renewal")
                {
                    return PartialView("~/Areas/DashBoard/Views/DataBankDashBoardNew/_NotificationsPopUp.cshtml", model);
                }
                else
                {
                    return PartialView("~/Areas/DashBoard/Views/DataBankDashBoardNew/_DateMayWrongPopup.cshtml", model);

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
        public async Task<IActionResult> GetGraph(string Classification)
        {
            try {
                DataBankDashBoardNew model = new DataBankDashBoardNew();
                model = await _service.GetDashBoardDiagrams(Classification);
                return PartialView("_Chart", model.DataBankDashBoardDiagram);
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
        public async Task<IActionResult> IndexV1()
        {
            try
            {
                DataBankDashBoard model = new DataBankDashBoard();                   
                model = await _service.GetDashBoardDetails();
                var manpower = await _service.GetDashBoardDiagrams("Manpower", "YESTERDAY", -1);
                var events = await _service.GetDashBoardDetailsList("Events","");
                var AgeGroup = await _service.GetDashBoardDiagrams("AGE","AGE", 1); //1= age, 2=gender, 3=YrsOf Service
                var GenderGroup = await _service.GetDashBoardDiagrams("AGE", "AGE", 2); 
                var YOSGroup = await _service.GetDashBoardDiagrams("AGE", "AGE", 3);
                ViewBag.chartData = JsonConvert.SerializeObject(AgeGroup.DataBankDashBoardList);
                model.DataBankDashBoardDetailsList = events.DataBankDashBoardDetailsList;
                ViewBag.Manpower = JsonConvert.SerializeObject(manpower.DataBankDashBoardList);
                model.Classification = 1;
                if (manpower.DataBankDashBoardList != null)
                { 
                    model.ManPowerChart = manpower.DataBankDashBoardList;                
                    for (int i = 0; i < model.ManPowerChart.Count; i++)
                    {
                        model.ManPowerTotal += model.ManPowerChart[i].count;
                    }
                }
                if (AgeGroup.DataBankDashBoardList != null)
                {
                    model.AgeChart = AgeGroup.DataBankDashBoardList;                    
                }
                if (GenderGroup.DataBankDashBoardList != null)
                {
                    model.GenderChart = GenderGroup.DataBankDashBoardList;                   
                }
                if (YOSGroup.DataBankDashBoardList != null)
                {
                    model.YOSChart = YOSGroup.DataBankDashBoardList;                    
                }
                

                ViewBag.Classsification = "Age";
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
                DataBankDashBoard model = new DataBankDashBoard();
                model = await _service.GetDashBoardList(Type);
                model.ListType = Type;
               if(Type== "HR APPROVAL PENDING" ||Type== "APPROVAL TRACKER")
                return PartialView("_PendingTracker", model);
               else if(Type == "MISSING" || Type== "DOCUMENTS MISSING" || Type == "UPCOMING RENEWALS" || Type == "OVERDUE RENEWALS")
                    return PartialView("_ListCount", model);
                else
                    return PartialView("_MasterChanges", model);
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
        public async Task<IActionResult> GetDashBoardDetailsList(string Type,string SubType)
        {
            try
            {
                DataBankDashBoard model = new DataBankDashBoard();
                model = await _service.GetDashBoardDetailsList(Type, SubType);
                if (Type == "NOTICE PERIOD")
                    return PartialView("_NoticePeriodDetails", model);
                else if (Type == "MISSING" || Type == "DOCUMENTS MISSING")
                    return PartialView("_InfoMissing", model);
                else if (Type == "Events")
                    return PartialView("_Events", model);

                else if (Type == "UPCOMING RENEWALS" || Type == "OVERDUE RENEWALS")
                {
                    ViewBag.Type = Type;
                    return PartialView("_UpcomingRenewels", model);
                }
                else
                    return PartialView("_Celebrations", model);
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
        
        public async Task<IActionResult> PreEnrollApprovalListAndJoining(string mode)
        {
            try
            {
                OnBoarding model = new OnBoarding();
                OnBoarding m = new OnBoarding();
                Filter filter = new Filter();
                model.OnBoardingListTabs = (List<OnBoardingListTabs>)await _onBoardingService.GetOnBoardingTabs();
                if (mode == "preEnrollment")
                {
                    ViewBag.RedirectigFRom = "FromDashBoardPre";
                    m = await _onBoardingService.GetOnBoardingList(1, filter);
                }

             else
                {
                    ViewBag.RedirectigFRom = "FromDashBoardJoining";
                    m = await _onBoardingService.GetOnBoardingList(4, filter);
                }
                model.OnBoardingList = m.OnBoardingList;
                model.AllCount = m.AllCount;
                model.PreenrollmentAppCount = m.PreenrollmentAppCount;
                model.EnrollmentCount = m.EnrollmentCount;
                model.CheckListCount = m.CheckListCount;
                model.JoiningConfCount = m.JoiningConfCount;
                model.OverDueCount = m.OverDueCount;
                ViewBag.DurationWiseFilter = new SelectList(await _commonService.GetDurationWiseFilterSlabDropDown(), "Code", "Name");
                await fillfilterDropdown();
                ViewBag.Tabs = new SelectList(model.OnBoardingListTabs, "Code", "Name");
               
                return View("~/Areas/DataBank/Views/OnBoarding/Index.cshtml", model);
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

        public async Task<IActionResult> GetManpowerChartDetails(string ToggleActive)
        {
            try
            {
                DataBankDashBoard model = new DataBankDashBoard();
                model = await _service.GetDashBoardDiagrams("MANPOWER", ToggleActive,-1);
                ViewBag.Manpower = JsonConvert.SerializeObject(model.DataBankDashBoardList);                
                if (model.DataBankDashBoardList != null)
                {
                    model.ManPowerChart = model.DataBankDashBoardList;
                    model.DataBankDashBoardList = null;
                    for (int i = 0; i < model.ManPowerChart.Count; i++)
                    {
                        model.ManPowerTotal += model.ManPowerChart[i].count;
                    }
                }
                return PartialView("_ManpowerChart",model);
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


        public async Task<IActionResult> ExportToExcel()
        {
            var LoginUser = HttpContext.Session.GetString("LoginUser");
            var CompanyName = HttpContext.Session.GetString("CompanyName");
            var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
            var GroupCompanyName = User.authUser.GroupCompanyName;
            ExcelPassword EP = new ExcelPassword();
            EP = await _commonService.GetExcelPassword("Certificate Renewal", "Excel");

            string currDateTime = await _helpers.GetCurrentDate();
            

            DataBankDashBoardNew model = new DataBankDashBoardNew();
            var dataset = await _service.GetDashBoardNotificationDetailsPopUp_Excel();
            ;

            DataTable dt = dataset.Tables[0];

            //set EXCEL Column Heading 
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                if (dt.Columns[i].ColumnName == "ID")

                    dt.Columns[i].ColumnName = "ID";

                else

                    dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                var worksheet = wb.Worksheets.Add("Certificate Renewal");
                int rowcount = dt.Rows.Count + 4;
                string columnName = "";
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

                var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 7)));
                wsReportNameHeaderRange.Style.Font.Bold = true;
                wsReportNameHeaderRange.Style.Font.FontSize = 15;
                wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);

                wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                wsReportNameHeaderRange.Merge();
                wsReportNameHeaderRange.Value = "Certificate Renewal";

                worksheet.Cell(2, 1).Style.Font.Bold = true;
                worksheet.Cell(2, 1).Value = "Group Company Name";
                worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();

                worksheet.Cell(2, 3).Style.Font.Bold = true;
                worksheet.Cell(2, 3).Value = "Login User";
                worksheet.Cell(2, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                worksheet.Cell(2, 4).Value = LoginUser.ToString();

                worksheet.Cell(3, 1).Style.Font.Bold = true;
                worksheet.Cell(3, 1).Value = "Date & Time";
                worksheet.Cell(3, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                worksheet.Cell(3, 2).Style.DateFormat.Format = "dd-MMM-yyyy   h:mm AM/PM";
                worksheet.Cell(3, 2).Value = currDateTime.ToString();
                worksheet.Cell(3, 3).Style.Font.Bold = true;
                worksheet.Cell(3, 3).Value = "Total Count";
                worksheet.Cell(3, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                worksheet.Cell(3, 4).Value = dt.Rows.Count;

                var currentRow = 4;

                worksheet.Cell(currentRow, 1).InsertTable(dt);
                worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                worksheet.Range("A4:" + colName + "4").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                worksheet.Range("A4:" + colName + "4").Style.Font.Bold = true;

                worksheet.Columns().AdjustToContents();
                worksheet.Rows().AdjustToContents();
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    if (EP.NeedPassword)
                    {
                        Workbook book = new Workbook();
                        book.LoadFromStream(stream);
                        //Protect Workbook
                        book.Protect(EP.Password, false, false);
                        book.SaveToStream(stream);
                    }
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", 
                        "Certificate Renewal.xlsx");
                }
            }
        }
    }
}
