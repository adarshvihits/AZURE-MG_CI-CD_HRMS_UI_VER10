using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Spire.Xls;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Hrms.Lite.Shared.Account;
using Newtonsoft.Json;
using System.Data;
using System.Collections.Generic;

namespace Hrms.Lite.UI.Areas.DataBank.Controllers
{
    [Area("DataBank")]
    [SessionAuthorize]
    public class InterviewSchedulingController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly ICommonService _commonService;
        private readonly IInterviewSchedulingService _InterviewSchedulingService;
        private readonly IBlob _blob;
        private readonly IHelpers _helpers;
        private readonly IApplicantTrackingService _ApplicantTracking;
        public static class Global
        {
            public static Filter GlobalFilter;

        }
        public InterviewSchedulingController(IDropdownService dropdownService, ICommonService commonService, IInterviewSchedulingService InterviewSchedulingService, IBlob blob, IHelpers helpers, IApplicantTrackingService ApplicantTracking)
        {
            _dropdownService = dropdownService;
            _commonService = commonService;
            _InterviewSchedulingService = InterviewSchedulingService;
            _blob = blob;
            _helpers = helpers;
            _ApplicantTracking = ApplicantTracking;
        }
        public async Task<IActionResult> Index()
        {
            ApplicantTracking model = new ApplicantTracking();
            return View(model);
        }

        public async Task<IActionResult> ApplyFilter(Filter filter)
        {
            try
            {
                ApplicantTracking model = new ApplicantTracking();
                Global.GlobalFilter = filter;
                model = await _InterviewSchedulingService.GetInterviewSchedulingList(0);
                return PartialView("_InterviewSchedulingList", model);
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
        public async Task<IActionResult> GetInterviewSchedulingList(int TabIndex)
        {
            try
            {
                ApplicantTracking model = new ApplicantTracking();
                ApplicantTracking m = new ApplicantTracking();

                model = await _InterviewSchedulingService.GetInterviewSchedulingList(TabIndex);
                return PartialView("_InterviewSchedulingList", model);
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        public async Task<IActionResult> GetList()
        {
            try
            {
                ApplicantTracking model = new ApplicantTracking();
                Filter filter = new Filter();


                model = await _InterviewSchedulingService.GetInterviewSchedulingList( 0);


                return PartialView("_InterviewSchedulingList", model);
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
        public async Task<IActionResult> GetEditDetails(Guid ResumeDataBankGI,int ApplicantTrackingCode)
        {
            try
            {
                ApplicantTracking model = new ApplicantTracking();
                model.ApplicantTrackingDT = await _ApplicantTracking.GetEvaluationDetails(ResumeDataBankGI);
                ViewBag.interviewLevel = new SelectList(await _dropdownService.GetInterviewLevelDropdown(), "Code", "Name");
                ViewBag.interviewTyp = new SelectList(await _dropdownService.GetInterviewTypeDropdown(), "Code", "Name");
                ViewBag.interviewMod = new SelectList(await _dropdownService.GetInterviewModeDropdown(), "Code", "Name");
                ViewBag.interviewer = new SelectList(await _dropdownService.GetEmployeeDropDown(), "Code", "Name");
                ViewBag.selection = new SelectList(await _dropdownService.GetInterviewSelectionDropdown(), "Code", "Name");
                ViewBag.RejectReason = new SelectList(await _dropdownService.CandidateRejectReasonDropdown(), "Code", "Name");
                model.ApplicantTrackingDTList = new List<ApplicantTracking_Details>();
                model.ApplicantTrackingDTList.Add(new ApplicantTracking_Details());
                model.ResumeDataBankGI = ResumeDataBankGI;
                model.ApplicantTrackingCode = ApplicantTrackingCode;
                return PartialView("_ScheduleInterview", model);
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
        public async Task<IActionResult> Schedule(ApplicantTracking model)
        {
            var response = await _InterviewSchedulingService.EmployeeWiseInterviewScheduling(model);
            return Json(response);
        }
        public async Task<IActionResult> ExportToExcel(int TabIndex,int totRecords)
        {
            try
            {

                if (Global.GlobalFilter == null)
                    Global.GlobalFilter = new Filter();
                else
                {
                    Global.GlobalFilter.length = totRecords;
                    Global.GlobalFilter.start = 0;
                }
                string FilterValues = await _helpers.GetFilterValues(Global.GlobalFilter);

                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Manual COFF Credit", "Excel");

                if (FilterValues == "")
                {
                    FilterValues = "All";
                }

                string currDateTime = await _helpers.GetCurrentDate();
                

                //Get Datset

                var dataset = await _InterviewSchedulingService.GetInterviewSchedulingList_Excel(TabIndex);
                DataTable dt = dataset.Tables[0];


                //set EXCEL Column Heading 
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (dt.Columns[i].ColumnName == "doj" || dt.Columns[i].ColumnName == "dob" || dt.Columns[i].ColumnName == "y/s")
                        dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.ToUpper();
                    else
                        dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
                }

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Manual COFF Credit");
                    int count = dt.Rows.Count + 5;


                    worksheet.Range("A1:G" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:G" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:G" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:G" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:G" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 7)));
                    wsReportNameHeaderRange.Style.Font.Bold = true;
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Merge();
                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191); ;
                    wsReportNameHeaderRange.Value = "Manual COFF Credit";

                    worksheet.Cell(2, 1).Style.Font.Bold = true;
                    worksheet.Cell(2, 1).Value = "Group Company Name";
                    worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
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
                    worksheet.Range("B4:G4").Merge();
                    worksheet.Cell("B4").Value = FilterValues;



                    var currentRow = 5;

                    worksheet.Cell(currentRow, 1).InsertTable(dt);
                    worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                    worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                    worksheet.Range("A5:G5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Range("A5:G5").Style.Font.Bold = true;


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
                            "Manual COFF Credit List.xlsx"
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

