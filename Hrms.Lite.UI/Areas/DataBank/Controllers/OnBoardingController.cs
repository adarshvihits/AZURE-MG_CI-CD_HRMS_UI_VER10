using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Spire.Xls;
using Hrms.Lite.Shared.DataBank.Generic;
using Hrms.Lite.Shared.Master;
using Newtonsoft.Json;
using Hrms.Lite.Shared.Account;

namespace Hrms.Lite.UI.Areas.DataBank.Controllers
{
    [Area("DataBank")]
    [SessionAuthorize]
    public class OnBoardingController : Controller
    {
        private readonly IOnBoardingService  _onBoardingService;
        private readonly ICommonService _commonService;
        private readonly IEnrollmentService _enrollmentService;
        private readonly IPreEnrollmentService _preenrollmentService;
        private readonly IDropdownService _dropdownService;
        private readonly IHelpers _helpers;
       
        public OnBoardingController(IOnBoardingService onBoardingService, ICommonService commonService, IEnrollmentService EnrollmentService, IPreEnrollmentService preEnrollmentService, IDropdownService dropdownService, IHelpers helpers)
        {
            _onBoardingService = onBoardingService;
            _commonService = commonService;
            _enrollmentService = EnrollmentService;
            _preenrollmentService = preEnrollmentService;
            _dropdownService = dropdownService;
            _helpers = helpers;
        }
 

        public async Task<IActionResult> Index(string FirstTab ="All")
        {

            try
            {
                OnBoarding model = new OnBoarding();
                OnBoarding m = new OnBoarding();
                UserRole u = new UserRole();
                Filter filter = new Filter();
                model.OnBoardingListTabs = (List<OnBoardingListTabs>)await _onBoardingService.GetOnBoardingTabs();
                m = await _onBoardingService.GetOnBoardingList(0, filter);
                model.OnBoardingList = m.OnBoardingList;
                model.AllCount = m.AllCount;
                model.PreenrollmentPendingCount = m.PreenrollmentPendingCount;
                model.PreenrollmentAppCount = m.PreenrollmentAppCount;
                model.EnrollmentCount = m.EnrollmentCount;
                model.CheckListCount = m.CheckListCount;
                model.JoiningConfCount = m.JoiningConfCount;
                model.OverDueCount = m.OverDueCount;
                model.ActiveVacancyCount = m.ActiveVacancyCount;
                ViewBag.DurationWiseFilter = new SelectList(await _commonService.GetDurationWiseFilterSlabDropDown(), "Code", "Name");
                model.Filter = await _commonService.GetFilterDropdowns();
                ViewBag.RedirectigFRom = "Direct";
                ViewBag.Tabs = new SelectList(model.OnBoardingListTabs, "Code", "Name");
                u = await _commonService.GetTabApplicability("Onboarding");
                model.UserVsMenuTab = u.UserVsMenuTab;
                model.FirstTab = FirstTab;
            
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

           


        public async Task<IActionResult> ApplyFilter(Filter filter)
        {
            try
            {
        
                OnBoarding model = new OnBoarding();
                UserRole u = new UserRole();
                model = await _onBoardingService.GetOnBoardingList(filter.Tabindex, filter);
                
                u = await _commonService.GetTabApplicability("Onboarding");
                model.UserVsMenuTab = u.UserVsMenuTab;
                model.TabIndex = filter.Tabindex;
                if (filter.Tabindex == 0)
                    return View("_OnBoardingList", model);
                else if (filter.Tabindex == 4)
                    return View("_CheckList", model);
                else if(filter.Tabindex == 1)
                    return View("_PendingList", model);
                else if(filter.Tabindex == 8)
                    return View("_DocumentsList", model);
                else if (filter.Tabindex == 9)
                    return View("_OfferLetter", model);
                else
                    return View("_Enrollment_PreEnrollment_JC_ODList", model);
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

      
        public async Task<ActionResult> OnBoardingTabs(Filter Filter)
        {
            try
            {
                
                OnBoarding model = new OnBoarding();
                OfferLetter model1= new OfferLetter();
                UserRole u = new UserRole();
                if(Filter.Tabindex!=9)
                {
                    model = await _onBoardingService.GetOnBoardingList(Filter.Tabindex, Filter);
                }

            

                model.TabIndex = Filter.Tabindex;
                u = await _commonService.GetTabApplicability("Onboarding");
                model.UserVsMenuTab = u.UserVsMenuTab;

                if (Filter.Tabindex == 0)               
                    return View("_OnBoardingList", model);
                //if (Filter.Tabindex == 1)
                //    return View("_PendingList", model);
                else if (Filter.Tabindex == 4)
                    return View("_CheckList", model);
                else if (Filter.Tabindex == 7)
                    return View("_ActiveVacancyList", model);
                else if (Filter.Tabindex == 8)
                    return View("_DocumentsList", model);

                else if (Filter.Tabindex == 9)
                    return View("_OfferLetter", model1);
                else
                    return View("_Enrollment_PreEnrollment_JC_ODList", model);
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



        public async Task<IActionResult> GetOfferLetterPDF(int PreEnrollCode)
        {
            KendoReport model = new KendoReport();
         
            model.PreEnrollCode = PreEnrollCode;
            model.Url = await _onBoardingService.GetUrl();
            //Global.url = model.Url.Urllink;
            model.type = "Offer Letter";

            model.Url = await _onBoardingService.GetOfferLetterFileName(model.type);
            //model.Url.Urllink = Global.url;
            
           return View("OfferLetterPDFView", model);
        }





        //DirectOfferFromActiveVancyTab

        public async Task<IActionResult> GetOnboardingActiveVacanciesValidation(int MRFCode)
        {
            try
            {
                var response = await _commonService.GetOnboardingActiveVacanciesValidation(MRFCode);
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
        public async Task<IActionResult> DirectOffer(int Dept, int Desgn, int Loc, int MRFCode)
        {
            try
            {
                PreEnrollment model = new PreEnrollment();

                //var response = await _commonService.GetOnboardingActiveVacanciesValidation();
                model = await _commonService.GetMandatoryFiledValidation("PREENROLL");
                model.JobDetails = new EmployeeClassification();
                model.JobDetails.Department = new Department();
                model.JobDetails.Designation = new Shared.Master.Designation();
                model.JobDetails.Location = new Location();

                model.JobDetails.Designation.Code = Desgn;
                model.JobDetails.Department.Code = Dept;
                model.JobDetails.Location.Code = Loc;
                model.VacancyManagementCode = MRFCode;

                await fillDropdown();
                ViewBag.St = new SelectList(await _dropdownService.GetStateDropDown(0), "Code", "Name");
                ViewBag.Dist = new SelectList(await _dropdownService.GetDistrictDropdown(0), "Code", "Name");
                //model = await _commonService.GetMandatoryFiledValidation("PREENROLL");
                model.FormName = "DirectOfferFromActiveVancyTab";
                model.Compensation = new Shared.DataBank.Generic.CompensationStructure();
                return View("~/Areas/DataBank/Views/PreEnrollmentTest/Create.cshtml", model);
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
        public async Task fillDropdown()
        {
            ViewBag.Sal = new SelectList(await _dropdownService.GetSalutationDropDown(), "Code", "Name");
            ViewBag.Gen = new SelectList(await _dropdownService.GetGenderDropDown(), "Code", "Name");
            ViewBag.Cat = new SelectList(await _dropdownService.GetCategoryDropdown(), "Code", "Name");
            ViewBag.Dep = new SelectList(await _dropdownService.GetDepartmentDropdown(), "Code", "Name");
            ViewBag.Gra = new SelectList(await _dropdownService.GetGradeDropdown(), "Code", "Name");
            ViewBag.Loc = new SelectList(await _dropdownService.GetLocationDropdown(), "Code", "Name");
            ViewBag.Cou = new SelectList(await _dropdownService.GetCountryDropDown(), "Code", "Name");
            ViewBag.Des = new SelectList(await _dropdownService.GetDesignationDropdown(), "Code", "Name");
            ViewBag.Pay = new SelectList(await _dropdownService.GetPayrollGroupDropdown(), "Code", "Name");
            ViewBag.SlryStruct = new SelectList(await _dropdownService.GeSalaryStructureDropDown(), "Code", "Name");

        }
        public async Task<ActionResult> GetPreEnrollmentApproval(Guid preEnrollGI)
        {
            try
            {
                Enrollment model = new Enrollment();
                model = await _enrollmentService.GetPreEnrollmentDetails(preEnrollGI);
                model.PreEnrollment.EmployeeHeader = await _commonService.GetEmployeeHeader(preEnrollGI);
                model.PreEnrollment.PreEnrollGI = preEnrollGI;
                return View("~/Areas/DataBank/Views/OnBoarding/PreEnrollmentApproval.cshtml", model);
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
        public async Task<ActionResult> GetCheckList(Guid preEnrollGI)
        {
            try
            {
                OnBoarding model = new OnBoarding();
                OnBoarding m = new OnBoarding();
                m.EmployeeHeader = await _commonService.GetEmployeeHeader(preEnrollGI);
                model = await _onBoardingService.GetJoiningChecklistDetail(preEnrollGI);
                model.EmployeeHeader = m.EmployeeHeader;
                ViewBag.ChecklistStatus = new SelectList(await _dropdownService.GetJoiningcheckStatusDropdown(), "Code", "Name");
                //model.PreEnrollment.PreEnrollGI = preEnrollGI;
                return View("~/Areas/DataBank/Views/OnBoarding/CheckListCreate.cshtml", model);
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
        public async Task<ActionResult> ApprovePreEnrollment(Enrollment model)
        {
            model.PreEnrollment.Mode = "APPROVE";
            var response = await _preenrollmentService.ApproveOrReject(model);
            TempData["Message"] = response.Message;
            return RedirectToAction("Index", "OnBoarding");
        }
        [HttpPost]
        public async Task<IActionResult> CheckListSave(OnBoarding model)
        {
            try
            {
                var response = await _onBoardingService.JoiningCheckSave(model);
                if (response.Success == true)
                    response.Message = "Saved Successfully";
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
        public async Task<ActionResult> ApproveRejectPreEnrollment(Guid preEnrollGI,string mode)
        {
            try
            {
                Enrollment model = new Enrollment();
                model.PreEnrollment = new PreEnrollment();
                model.PreEnrollment.Mode = mode;
                model.PreEnrollment.PreEnrollGI = preEnrollGI;
                var response = await _preenrollmentService.ApproveOrReject(model);
                if (response.Message == "APPROVED")
                {
                    response.Message = "Approved Successfully";
                }
                else if (response.Message == "REJECTED")
                {
                    response.Message = "Rejected Successfully";
                }
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
        public async Task<ActionResult> onExportToExcelClick(Filter Filter, int TabIndex, string TabName)
        {
            try
            {
                OnBoarding model = new OnBoarding();
                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Onboarding (" + TabName + ")", "Excel");
                int slNo = 0;
             
                string FilterValues = await _helpers.GetFilterValues(Filter);
                model = await _onBoardingService.GetOnBoardingList(TabIndex, Filter);
                if (FilterValues == "")
                {
                    FilterValues = "All";
                }
                string currDateTime = await _helpers.GetCurrentDate();
                
                if (TabIndex == 0)
                {
                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add(TabName + " List");

                        int count = model.OnBoardingList.Count + 5;


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
                        wsReportNameHeaderRange.Value = "OnBoarding (" + TabName + ")";

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
                        worksheet.Cell(3, 6).Value = model.OnBoardingList.Count;
                        worksheet.Cell(4, 1).Style.Font.Bold = true;
                        worksheet.Cell(4, 1).Value = "Filter Applied";
                        worksheet.Range("B4:I4").Merge();
                        worksheet.Cell("B4").Value = FilterValues;
                        var currentRow = 5;
                        #region Header
                        worksheet.Range("A5:I5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                        worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 1).Value = "Sl.No";
                        worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 2).Value = "Candidate Name";
                        worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 3).Value = "Designation";
                        worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 4).Value = "Department";
                        worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 5).Value = "Location";
                        worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 6).Value = "Due days";
                        worksheet.Cell(currentRow, 7).Style.Font.Bold = true;

                        worksheet.Cell(currentRow, 7).Value = "E:DOJ";
                        worksheet.Cell(currentRow, 8).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 8).Value = "Checklist";
                        worksheet.Cell(currentRow, 9).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 9).Value = "Pending Status";

                        foreach (var item in model.OnBoardingList)
                        {
                            currentRow++;
                            slNo = slNo + 1;


                            worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 1).Value = slNo;
                            worksheet.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 2).Value = item.CandidateName;
                            worksheet.Cell(currentRow, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 3).Value = item.Designation.Name;
                            worksheet.Cell(currentRow, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 4).Value = item.Department.Name;
                            worksheet.Cell(currentRow, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 5).Value = item.Location.Name;
                            worksheet.Cell(currentRow, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 6).Value = item.DueDays;
                            worksheet.Cell(currentRow, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 7).Style.DateFormat.Format = "dd-MMM-yyyy";
                            worksheet.Cell(currentRow, 7).Value = item.ExpectedDOJ;
                            worksheet.Cell(currentRow, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 8).Value = item.CheckList;
                            worksheet.Cell(currentRow, 9).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 9).Value = item.PendingStatus;



                            worksheet.Columns(1, 9).Style.Alignment.WrapText = true;
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
                                book.Protect(EP.Password,false,false);
                                book.SaveToStream(stream);
                            }
                            var content = stream.ToArray();
                            return File(
                                content,
                                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                              "OnBoarding (" + TabName + ") List.xlsx"
                                );
                        }
                    }

                }
                else if (TabIndex == 3)
                {
                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add(TabName + " List");
                        int count = model.OnBoardingList.Count + 5;


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
                        wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191); ;
                        wsReportNameHeaderRange.Value = TabName + " List";

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
                        worksheet.Cell(3, 6).Value = model.OnBoardingList.Count;
                        worksheet.Cell(4, 1).Style.Font.Bold = true;
                        worksheet.Cell(4, 1).Value = "Filter Applied";
                        worksheet.Range("B4:H4").Merge();
                        worksheet.Cell("B4").Value = FilterValues;




                        var currentRow = 5;
                        #region Header
                        worksheet.Range("A5:H5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                        worksheet.Cell(currentRow, 1).Value = "Sl.No";
                        worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 2).Value = "Applicant ID";
                        worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 3).Value = "Candidate Name";
                        worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 4).Value = "Designation";
                        worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 5).Value = "Department";
                        worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 6).Value = "Location";
                        worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 7).Value = "Due days";
                        worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 8).Value = "E:DOJ";
                        worksheet.Cell(currentRow, 8).Style.Font.Bold = true;


                        #endregion

                        #region Body
                        foreach (var item in model.OnBoardingList)
                        {
                            currentRow++;
                            slNo = slNo + 1;
                            worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 1).Value = slNo;
                            worksheet.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 2).Value = item.ApplicantID;
                            worksheet.Cell(currentRow, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 3).Value = item.CandidateName;
                            worksheet.Cell(currentRow, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 4).Value = item.Designation.Name;
                            worksheet.Cell(currentRow, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 5).Value = item.Department.Name;
                            worksheet.Cell(currentRow, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 6).Value = item.Location.Name;
                            worksheet.Cell(currentRow, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 7).Value = item.DueDays;
                            worksheet.Cell(currentRow, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 8).Style.DateFormat.Format = "dd-MMM-yyyy";
                            worksheet.Cell(currentRow, 8).Value = item.ExpectedDOJ;

                            worksheet.Columns(1, 8).Style.Alignment.WrapText = true;
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
                                book.Protect(EP.Password,false,false);
                                book.SaveToStream(stream);
                            }
                            var content = stream.ToArray();
                            return File(
                                content,
                                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                                "OnBoarding (" + TabName + ") List.xlsx"
                                );
                        }
                    }
                }
                else if (TabIndex == 7)
                {
                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add(TabName + " List");
                        int count = model.ActiveVacancyList.Count + 5;


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
                        wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191); ;
                        wsReportNameHeaderRange.Value = TabName + " List";

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
                        worksheet.Cell(3, 6).Value = model.ActiveVacancyList.Count;
                        worksheet.Cell(4, 1).Style.Font.Bold = true;
                        worksheet.Cell(4, 1).Value = "Filter Applied";
                        worksheet.Range("B4:J4").Merge();
                        worksheet.Cell("B4").Value = FilterValues;




                        var currentRow = 5;
                        #region Header
                        worksheet.Range("A5:J5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                        worksheet.Cell(currentRow, 1).Value = "Sl.No";
                        worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 2).Value = "MRF No.";
                        worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 3).Value = "Job Title";
                        worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 4).Value = "Designation";
                        worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 5).Value = "Department";
                        worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 6).Value = "Location";
                        worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 7).Value = "Work Location";
                        worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 8).Value = "Need By Date";
                        worksheet.Cell(currentRow, 8).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 9).Value = "D.L.";
                        worksheet.Cell(currentRow, 9).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 10).Value = "No of Req.";
                        worksheet.Cell(currentRow, 10).Style.Font.Bold = true;


                        #endregion

                        #region Body
                        foreach (var item in model.ActiveVacancyList)
                        {
                            currentRow++;
                            slNo = slNo + 1;
                            worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);

                            worksheet.Cell(currentRow, 1).Value = slNo;
                            worksheet.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 2).Value = item.JobCode;
                            worksheet.Cell(currentRow, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 3).Value = item.JobTitle;
                            worksheet.Cell(currentRow, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 4).Value = item.Designation.Name;
                            worksheet.Cell(currentRow, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 5).Value = item.Department.Name;
                            worksheet.Cell(currentRow, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 6).Value = item.Location.Name;
                            worksheet.Cell(currentRow, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 7).Value = item.WorkLocation.Name;
                            worksheet.Cell(currentRow, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 8).Value = item.NeedByDate;
                            worksheet.Cell(currentRow, 8).Style.DateFormat.Format = "dd-MMM-yyyy";
                            worksheet.Cell(currentRow, 9).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 9).Value = item.Aging;
                            worksheet.Cell(currentRow, 10).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 10).Value = item.NoOfRequirements;

                            worksheet.Columns(1, 8).Style.Alignment.WrapText = true;
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
                                book.Protect(EP.Password,false,false);
                                book.SaveToStream(stream);
                            }
                            var content = stream.ToArray();
                            return File(
                                content,
                                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                                "OnBoarding (" + TabName + ") List.xlsx"
                                );

                        }
                    }

                }
                else
                {

                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add(TabName + " List");
                        int count = model.OnBoardingList.Count + 5;


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
                        wsReportNameHeaderRange.Value = TabName + " List";

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
                        worksheet.Cell(3, 6).Value = model.OnBoardingList.Count;
                        worksheet.Cell(4, 1).Style.Font.Bold = true;
                        worksheet.Cell(4, 1).Value = "Filter Applied";
                        worksheet.Range("B4:I4").Merge();
                        worksheet.Cell("B4").Value = FilterValues;




                        var currentRow = 5;
                        #region Header
                        worksheet.Range("A5:I5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                        worksheet.Cell(currentRow, 1).Value = "Sl.No";
                        worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 2).Value = "Applicant ID";
                        worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 3).Value = "Candidate Name";
                        worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 4).Value = "Designation";
                        worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 5).Value = "Department";
                        worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 6).Value = "Location";
                        worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 7).Value = "Due days";
                        worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 8).Value = "E:DOJ";
                        worksheet.Cell(currentRow, 8).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 9).Value = "Checklist";
                        worksheet.Cell(currentRow, 9).Style.Font.Bold = true;


                        #endregion

                        #region Body
                        foreach (var item in model.OnBoardingList)
                        {
                            currentRow++;
                            slNo = slNo + 1;
                            worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 1).Value = slNo;
                            worksheet.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 2).Value = item.ApplicantID;
                            worksheet.Cell(currentRow, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 3).Value = item.CandidateName;
                            worksheet.Cell(currentRow, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 4).Value = item.Designation.Name;
                            worksheet.Cell(currentRow, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 5).Value = item.Department.Name;
                            worksheet.Cell(currentRow, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 6).Value = item.Location.Name;
                            worksheet.Cell(currentRow, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 7).Value = item.DueDays;
                            worksheet.Cell(currentRow, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 8).Style.DateFormat.Format = "dd-MMM-yyyy";
                            worksheet.Cell(currentRow, 9).Value = item.ExpectedDOJ;
                            worksheet.Cell(currentRow, 9).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 9).Value = item.CheckList;

                            worksheet.Columns(1, 9).Style.Alignment.WrapText = true;
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
                                book.Protect(EP.Password,false,false);
                                book.SaveToStream(stream);
                            }
                            var content = stream.ToArray();
                            return File(
                                content,
                                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                                "OnBoarding (" + TabName + ") List.xlsx"
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

    }

}

