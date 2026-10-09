using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.Account;
using Hrms.Lite.Shared.DataBank.DTO;
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
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.DataBank.Controllers
{
    [Area("DataBank")]
    [SessionAuthorize]
    public class VacancyManagementController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly ICommonService _commonService;
        private readonly IVacancyManagementService _service;
        private readonly IHelpers _helpers;
        public VacancyManagementController(IDropdownService dropdownService, ICommonService commonService, IVacancyManagementService service, IHelpers helpers)
        {
            _dropdownService = dropdownService;
            _commonService = commonService;
            _service = service;
            _helpers = helpers;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                VacancyManagement Requirements = new VacancyManagement();
                VacancyManagement Resume = new VacancyManagement();
                VacancyManagement model = new VacancyManagement();
                VacancyManagement Status = new VacancyManagement();
                ViewBag.RedirctFrom = "Vacancy";
                model = await _service.GetList(1);
                Resume = await _service.GetGraphDetails();
                Requirements = await _service.GetGraphDetailsRequirements();

                model.GraphResume = Resume.GraphResume;
                model.GraphRequrements = Requirements.GraphRequrements;

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

        public async Task<IActionResult> FromRecruitmentDashBoard()
        {
            try
            {
                VacancyManagement Requirements = new VacancyManagement();
                VacancyManagement Resume = new VacancyManagement();
                VacancyManagement model = new VacancyManagement();
                VacancyManagement Status = new VacancyManagement();

                model = await _service.GetList(3);
                //Resume = await _service.GetGraphDetails();
                //Requirements = await _service.GetGraphDetailsRequirements();

                //model.GraphResume = Resume.GraphResume;
                //model.GraphRequrements = Requirements.GraphRequrements;
                ViewBag.RedirctFrom = "FromRecruitmentDashBoard";
                return View("Index", model);
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
        public async Task<IActionResult> Getlist(int TabIndex)
        {
            try
            {
                VacancyManagement model = new VacancyManagement();
                model = await _service.GetList(TabIndex);
                if (TabIndex == 1)
                {
                    return PartialView("_ActiveVacancyList", model);
                }
                else if (TabIndex == 2)
                {
                    return PartialView("_AcceptPendingList", model);
                }
                else if (TabIndex == 3)
                {
                    return PartialView("_ShortlistedAgedMRFList", model);
                }
                else if (TabIndex == 5 || TabIndex == 6)
                {
                    return PartialView("_ClosedOrCancelledList", model);
                }
                else
                {
                    return PartialView("_EmployeeInNoticeList", model);
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
        public async Task<IActionResult> Create(string TabIndex)
        {
            try
            {
                VacancyManagement model = new VacancyManagement();
                ViewBag.Gender = new SelectList(await _dropdownService.GetGenderDropDown(), "Code", "Name");
                ViewBag.Department = new SelectList(await _dropdownService.GetDepartmentDropdown(), "Code", "Name");
                ViewBag.Location = new SelectList(await _dropdownService.GetLocationDropdown(), "Code", "Name");
                ViewBag.Designation = new SelectList(await _dropdownService.GetDesignationDropdown(), "Code", "Name");
                ViewBag.RequestedBy = new SelectList(await _dropdownService.GetMRFRequestedByDropdown(), "Code", "Name");
                model = await _service.GetRefNo();
                return View("_MRFCreateSlider", model);
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
        public async Task<IActionResult> GetVSJD(int DesignationCode)
        {
            VacancyManagement model = new VacancyManagement();
            ViewBag.Gender = new SelectList(await _dropdownService.GetGenderDropDown(), "Code", "Name");
            ViewBag.Location = new SelectList(await _dropdownService.GetLocationDropdown(), "Code", "Name");
            model = await _service.GetDesignationVSJD(DesignationCode);
            return PartialView("_MRFRequestSliderSub", model);
        }



        public async Task<IActionResult> GetVacancyTypePopUp(Guid VacancyGI)
        {
            try
            {
                //VacancyManagement model = new VacancyManagement();
                EmployeeDT model = new EmployeeDT();

                model = await _service.GetVacancyTypePopUp(VacancyGI);

                return PartialView("_VacancyTypeDetails", model);
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
        public async Task<IActionResult> Create(VacancyManagement model)
        {
            try
            {

                var response = await _service.SaveVacancyManagemenSave(model);
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

        public async Task<IActionResult> DeleteVacancy(VacancyManagement model)
        {
            try
            {

                var response = await _service.DeleteVacancy(model);
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
        public async Task<IActionResult> FilledVacancySave(VacancyManagement model)
        {
            try
            {

                var response = await _service.FilledVacancySave(model);
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
        public async Task<IActionResult> ApproveReject(VacancyManagement model)
        {
            try
            {

                var response = await _service.ApproveReject(model);
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
        public async Task<IActionResult> GetApproveDetails(Guid VacancyManagementGI, int SlNo)
        {
            try
            {
                VacancyManagement model = new VacancyManagement();
                model = await _service.GetVacancyManagementDetails(VacancyManagementGI);
                model.VacancyManagementGI = VacancyManagementGI;
                if (SlNo == 1)
                    return PartialView("_MRFApproveRejectSlider", model);
                else if (SlNo == 2)
                    return PartialView("_ActiveVacancydetails", model);
                else
                    return PartialView("_FilledvacancydetailsSlider", model);
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
        public async Task<IActionResult> ExportToExcel(int TabIndex, string TabnName)
        {
            try
            {
                VacancyManagement model = new VacancyManagement();
                model = await _service.GetList(TabIndex);
                int slno = 0;
                string currDateTime = await _helpers.GetCurrentDate();
                
                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Vacancy Register (" + TabnName + ")", "Excel");


                if (TabIndex == 4)
                {

                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("Vacancy Register");
                        int PendingCountnum = model.EmployeeDtList.Count + 4;


                        worksheet.Range("A1:J" + PendingCountnum).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:J" + PendingCountnum).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:J" + PendingCountnum).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:J" + PendingCountnum).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:J" + PendingCountnum).Style.Border.LeftBorder = XLBorderStyleValues.Thin;



                        var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 9)));
                        wsReportNameHeaderRange.Style.Font.Bold = true;
                        wsReportNameHeaderRange.Style.Font.FontSize = 15;
                        wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                        wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        wsReportNameHeaderRange.Merge();


                        wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                        wsReportNameHeaderRange.Style.Font.FontSize = 15;
                        wsReportNameHeaderRange.Value = "Vacancy Register " + "(" + TabnName + ")";
                        worksheet.Range("A4:J4").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
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
                        worksheet.Cell(3, 6).Value = model.EmployeeDtList.Count;

                        var currentRow = 4;
                        #region Header
                        worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 1).Value = "Sl.No";
                        worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 2).Value = "Employee ID";
                        worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 3).Value = "Name";
                        worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 4).Value = "Designation";
                        worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 5).Value = "Department";
                        worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 6).Value = "Location";
                        worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 7).Value = "Gender";
                        worksheet.Cell(currentRow, 8).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 8).Value = "LWD";
                        worksheet.Cell(currentRow, 9).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 9).Value = "Reason";
                        worksheet.Cell(currentRow, 10).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 10).Value = "Replacement";

                        #endregion

                        #region Body
                        foreach (var item in model.EmployeeDtList)
                        {
                            currentRow++;
                            slno = slno + 1;
                            worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 1).Value = slno;
                            worksheet.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 2).Value = item.EmployeeID;
                            worksheet.Cell(currentRow, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 3).Value = item.FullName;
                            worksheet.Cell(currentRow, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 4).Value = item.Designation.Name;
                            worksheet.Cell(currentRow, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 5).Value = item.Department.Name;
                            worksheet.Cell(currentRow, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 6).Value = item.Location.Name;
                            worksheet.Cell(currentRow, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 7).Value = item.Gender.Name;
                            worksheet.Cell(currentRow, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 8).Value = item.LWD;
                            worksheet.Cell(currentRow, 8).Style.DateFormat.Format = "dd-MMM-yyyy";
                            worksheet.Cell(currentRow, 9).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 9).Value = item.Reason;
                            worksheet.Cell(currentRow, 10).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            if (item.Replacement == "True")
                                item.Replacement = "Yes";
                            else
                                item.Replacement = "No";

                            worksheet.Cell(currentRow, 10).Value = item.Replacement;
                            worksheet.Columns(1, 10).Style.Alignment.WrapText = true;



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
                                book.Protect(EP.Password, false, false);
                                book.SaveToStream(stream);
                            }
                            var content = stream.ToArray();
                            return File(
                                content,
                                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                                "Vacancy Register.xlsx"
                                );
                        }
                    }

                }
                else if (TabIndex == 1 || TabIndex == 2)
                {

                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add(TabIndex + " List");
                        int count = model.VacancyManagement_List.Count + 4;
                        if (TabIndex == 1)
                        {
                            worksheet.Range("A1:R" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                            worksheet.Range("A1:R" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                            worksheet.Range("A1:R" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                            worksheet.Range("A1:R" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                            worksheet.Range("A1:R" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                            var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 17)));
                            wsReportNameHeaderRange.Style.Font.Bold = true;
                            wsReportNameHeaderRange.Style.Font.FontSize = 15;
                            wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                            wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                            wsReportNameHeaderRange.Merge();
                            wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                            wsReportNameHeaderRange.Value = "Vacancy Register" + "(" + TabnName + ")";
                            worksheet.Range("A4:R4").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
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
                            worksheet.Cell(3, 6).Value = model.VacancyManagement_List.Count;
                        }

                        if (TabIndex == 2)
                        {
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
                            wsReportNameHeaderRange.Value = "Vacancy Register" + "(" + TabnName + ")";
                            worksheet.Range("A4:I4").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
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
                            worksheet.Cell(3, 6).Value = model.VacancyManagement_List.Count;
                        }


                        var currentRow = 4;


                        #region Header
                        worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 1).Value = "Sl.No";
                        worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 2).Value = "MRF No.";
                        worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 3).Value = "Job Title";
                        worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 4).Value = "Vacancy Type";
                        worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 5).Value = "Location";
                        worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 6).Value = "Region";
                        worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 7).Value = "Deadline";
                        worksheet.Cell(currentRow, 8).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 8).Value = "N/Req";
                        worksheet.Cell(currentRow, 9).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 9).Value = "D.L.";

                        if (TabIndex == 1)
                        {
                            worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                            worksheet.Cell(currentRow, 1).Value = "Sl.No";
                            worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                            worksheet.Cell(currentRow, 2).Value = "MRF No.";
                            worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                            worksheet.Cell(currentRow, 3).Value = "Job Title";
                            worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                            worksheet.Cell(currentRow, 4).Value = "Designation";
                            worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                            worksheet.Cell(currentRow, 5).Value = "Department";
                            worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
                            worksheet.Cell(currentRow, 6).Value = "Vacancy Type";
                            worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                            worksheet.Cell(currentRow, 7).Value = "Location";
                            worksheet.Cell(currentRow, 8).Style.Font.Bold = true;
                            worksheet.Cell(currentRow, 8).Value = "Region";
                            worksheet.Cell(currentRow, 9).Style.Font.Bold = true;
                            worksheet.Cell(currentRow, 9).Value = "Deadline";
                            worksheet.Cell(currentRow, 10).Style.Font.Bold = true;
                            worksheet.Cell(currentRow, 10).Value = "Gender";
                            worksheet.Cell(currentRow, 11).Style.Font.Bold = true;
                            //worksheet.Cell(currentRow, 11).Style.NumberFormat.Format = "@";
                            worksheet.Cell(currentRow, 11).Value = "Y/E";
                            worksheet.Cell(currentRow, 12).Style.Font.Bold = true;
                            worksheet.Cell(currentRow, 12).Value = "Salary Range";
                            worksheet.Cell(currentRow, 13).Style.Font.Bold = true;
                            worksheet.Cell(currentRow, 13).Value = "N/Req";
                            worksheet.Cell(currentRow, 14).Style.Font.Bold = true;
                            worksheet.Cell(currentRow, 14).Value = "D.L.";
                            worksheet.Cell(currentRow, 15).Style.Font.Bold = true;
                            worksheet.Cell(currentRow, 15).Value = "Resumes";
                            worksheet.Cell(currentRow, 16).Style.Font.Bold = true;
                            worksheet.Cell(currentRow, 16).Value = "Initiated By";
                            worksheet.Cell(currentRow, 17).Style.Font.Bold = true;
                            worksheet.Cell(currentRow, 17).Value = "MRF Created Date";
                            worksheet.Cell(currentRow, 18).Style.Font.Bold = true;
                            worksheet.Cell(currentRow, 18).Value = "MRF Approved Date";
                        }
                        #endregion

                        #region Body
                        foreach (var item in model.VacancyManagement_List)
                        {
                            model.Details = new VacancyManagement_Details();
                            currentRow++;
                            slno = slno + 1;
                            worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 1).Value = slno;
                            worksheet.Cell(currentRow, 2).Value = item.JobCode;
                            worksheet.Cell(currentRow, 3).Value = item.JobTitle;
                            worksheet.Cell(currentRow, 4).Value = item.VacancyType;
                            worksheet.Cell(currentRow, 5).Value = item.Location;
                            worksheet.Cell(currentRow, 6).Value = item.Region;
                            worksheet.Cell(currentRow, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 7).Style.DateFormat.Format = "dd-MMM-yyyy";
                            worksheet.Cell(currentRow, 7).Value = item.NeedByDate;
                            worksheet.Cell(currentRow, 8).Value = item.NoOfRequirements;
                            worksheet.Cell(currentRow, 9).Value = item.Aging;
                            if (TabIndex == 1)
                            {
                                model.Details = new VacancyManagement_Details();
                                worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                                worksheet.Cell(currentRow, 1).Value = slno;
                                worksheet.Cell(currentRow, 2).Value = item.JobCode;
                                worksheet.Cell(currentRow, 3).Value = item.JobTitle;
                                worksheet.Cell(currentRow, 4).Value = item.Designation;
                                worksheet.Cell(currentRow, 5).Value = item.Department;
                                worksheet.Cell(currentRow, 6).Value = item.VacancyType;
                                worksheet.Cell(currentRow, 7).Value = item.Location;
                                worksheet.Cell(currentRow, 8).Value = item.Region;
                                worksheet.Cell(currentRow, 9).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                                worksheet.Cell(currentRow, 9).Style.DateFormat.Format = "dd-MMM-yyyy";
                                worksheet.Cell(currentRow, 9).Value = item.NeedByDate;
                                worksheet.Cell(currentRow, 10).Value = item.Gender;
                                worksheet.Cell(currentRow, 11).Value = "'" + item.Experience;
                                worksheet.Cell(currentRow, 12).Value = item.ExGrossPay;
                                worksheet.Cell(currentRow, 13).Value = item.NoOfRequirements;
                                worksheet.Cell(currentRow, 14).Value = item.Aging;
                                worksheet.Cell(currentRow, 15).Value = item.MappedResumes;
                                worksheet.Cell(currentRow, 16).Value = item.FullName;
                                worksheet.Cell(currentRow, 17).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                                worksheet.Cell(currentRow, 17).Style.DateFormat.Format = "dd-MMM-yyyy";
                                worksheet.Cell(currentRow, 17).Value = item.MRFCreatedDate;
                                worksheet.Cell(currentRow, 18).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                                worksheet.Cell(currentRow, 18).Style.DateFormat.Format = "dd-MMM-yyyy";
                                worksheet.Cell(currentRow, 18).Value = item.MRFApprovedDate;
                            }

                            worksheet.Columns(1, 10).Style.Alignment.WrapText = true;
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
                                book.Protect(EP.Password, false, false);
                                book.SaveToStream(stream);
                            }
                            var content = stream.ToArray();
                            return File(
                                content,
                                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                               "Vacancy Register.xlsx"
                                );
                        }
                    }

                }
                else if (TabIndex == 5 || TabIndex == 6)
                {

                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add(TabnName + " List");
                        int count = model.VacancyManagement_List.Count + 4;

                        worksheet.Range("A1:R" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:R" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:R" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:R" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:R" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 17)));
                        wsReportNameHeaderRange.Style.Font.Bold = true;
                        wsReportNameHeaderRange.Style.Font.FontSize = 15;
                        wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                        wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        wsReportNameHeaderRange.Merge();
                        wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                        wsReportNameHeaderRange.Value = "Vacancy Register" + "(" + TabnName + ")";
                        worksheet.Range("A4:R4").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
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
                        worksheet.Cell(3, 6).Value = model.VacancyManagement_List.Count;





                        var currentRow = 4;


                        #region Header
                        worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 1).Value = "Sl.No";
                        worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 2).Value = "MRF No.";
                        worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 3).Value = "Job Title";
                        worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 4).Value = "Designation";
                        worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 5).Value = "Department";
                        worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 6).Value = "Vacancy Type";
                        worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 7).Value = "Location";
                        worksheet.Cell(currentRow, 8).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 8).Value = "Region";
                        worksheet.Cell(currentRow, 9).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 9).Value = "Deadline";
                        worksheet.Cell(currentRow, 10).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 10).Value = "Gender";
                        worksheet.Cell(currentRow, 11).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 11).Value = "Y/E";
                        worksheet.Cell(currentRow, 12).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 12).Value = "Salary Range";
                        worksheet.Cell(currentRow, 13).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 13).Value = "N/Req";
                        worksheet.Cell(currentRow, 14).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 14).Value = "Resumes";
                        worksheet.Cell(currentRow, 15).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 15).Value = "Initiated By";
                        worksheet.Cell(currentRow, 16).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 16).Value = "MRF Created Date";
                        worksheet.Cell(currentRow, 17).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 17).Value = "MRF Approved Date";
                        if (TabIndex == 5)

                        {
                            worksheet.Cell(currentRow, 18).Style.Font.Bold = true;
                            worksheet.Cell(currentRow, 18).Value = "MRF Canceled Date";
                        }
                        if (TabIndex == 6)
                        {
                            worksheet.Cell(currentRow, 18).Style.Font.Bold = true;
                            worksheet.Cell(currentRow, 18).Value = "MRF Closed Date";
                        }
                        #endregion

                        #region Body
                        foreach (var item in model.VacancyManagement_List)
                        {
                            model.Details = new VacancyManagement_Details();
                            currentRow++;
                            slno = slno + 1;
                            worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 1).Value = slno;
                            worksheet.Cell(currentRow, 2).Value = item.JobCode;
                            worksheet.Cell(currentRow, 3).Value = item.JobTitle;
                            worksheet.Cell(currentRow, 4).Value = item.Designation;
                            worksheet.Cell(currentRow, 5).Value = item.Department;
                            worksheet.Cell(currentRow, 6).Value = item.VacancyType;
                            worksheet.Cell(currentRow, 7).Value = item.Location;
                            worksheet.Cell(currentRow, 8).Value = item.Region;
                            worksheet.Cell(currentRow, 9).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 9).Style.DateFormat.Format = "dd-MMM-yyyy";
                            worksheet.Cell(currentRow, 9).Value = item.NeedByDate;
                            worksheet.Cell(currentRow, 10).Value = item.Gender;
                            worksheet.Cell(currentRow, 11).Value = "'" + item.Experience;
                            worksheet.Cell(currentRow, 12).Value = item.ExGrossPay;
                            worksheet.Cell(currentRow, 13).Value = item.NoOfRequirements;
                            worksheet.Cell(currentRow, 14).Value = item.MappedResumes;
                            worksheet.Cell(currentRow, 15).Value = item.FullName;
                            worksheet.Cell(currentRow, 16).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 16).Style.DateFormat.Format = "dd-MMM-yyyy";
                            worksheet.Cell(currentRow, 16).Value = item.MRFCreatedDate;
                            worksheet.Cell(currentRow, 17).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 17).Style.DateFormat.Format = "dd-MMM-yyyy";
                            worksheet.Cell(currentRow, 17).Value = item.MRFApprovedDate;
                            if (TabIndex == 5)
                            {
                                worksheet.Cell(currentRow, 18).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                                worksheet.Cell(currentRow, 18).Style.DateFormat.Format = "dd-MMM-yyyy";
                                worksheet.Cell(currentRow, 18).Value = item.MRFCanceledDate;
                            }
                            if (TabIndex == 6)
                            {
                                worksheet.Cell(currentRow, 18).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                                worksheet.Cell(currentRow, 18).Style.DateFormat.Format = "dd-MMM-yyyy";
                                worksheet.Cell(currentRow, 18).Value = item.MRFClosedDate;
                            }
                            worksheet.Columns(1, 9).Style.Alignment.WrapText = true;
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
                                book.Protect(EP.Password, false, false);
                                book.SaveToStream(stream);
                            }
                            var content = stream.ToArray();
                            return File(
                                content,
                                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                               "Vacancy Register.xlsx"
                                );
                        }
                    }

                }
                else
                {

                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add(TabIndex + " List");
                        int count = model.VacancyManagement_List.Count + 4;


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
                        wsReportNameHeaderRange.Value = "Vacancy Register" + "(" + TabnName + ")";
                        worksheet.Range("A4:I4").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
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
                        worksheet.Cell(3, 6).Value = model.VacancyManagement_List.Count;

                        var currentRow = 4;
                        #region Header
                        worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 1).Value = "Sl.No";
                        worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 2).Value = "MRF No.";
                        worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 3).Value = "Job Title";
                        worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 4).Value = "Location";
                        worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 5).Value = "Region";
                        worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 6).Value = "Deadline";
                        worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 7).Value = "D.L.";
                        worksheet.Cell(currentRow, 8).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 8).Value = "N/Req";
                        worksheet.Cell(currentRow, 9).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 9).Value = "Resumes";
                        #endregion

                        #region Body
                        foreach (var item in model.VacancyManagement_List)
                        {
                            currentRow++;
                            slno = slno + 1;
                            worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 1).Value = slno;
                            worksheet.Cell(currentRow, 2).Value = item.JobCode;
                            worksheet.Cell(currentRow, 3).Value = item.JobTitle;
                            worksheet.Cell(currentRow, 4).Value = item.Location;
                            worksheet.Cell(currentRow, 5).Value = item.Region;
                            worksheet.Cell(currentRow, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 6).Style.DateFormat.Format = "dd-MMM-yyyy";
                            worksheet.Cell(currentRow, 6).Value = item.NeedByDate;
                            worksheet.Cell(currentRow, 7).Value = item.Aging;
                            worksheet.Cell(currentRow, 8).Value = item.NoOfRequirements;
                            worksheet.Cell(currentRow, 9).Value = item.MappedResumes;
                            worksheet.Columns(1, 6).Style.Alignment.WrapText = true;
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
                                book.Protect(EP.Password, false, false);
                                book.SaveToStream(stream);
                            }
                            var content = stream.ToArray();
                            return File(
                                content,
                                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                               "Vacancy Register.xlsx"
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
