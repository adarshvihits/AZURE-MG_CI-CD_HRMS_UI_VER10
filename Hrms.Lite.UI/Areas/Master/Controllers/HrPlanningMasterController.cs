using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.Master;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Spire.Xls;

namespace Hrms.Lite.UI.Areas.Master.Controllers
{
    [Area("Master")]
    [SessionAuthorize]
    public class HrPlanningMasterController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly IHrPlanningMasterService _hrPlanningMasterService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        public HrPlanningMasterController(IHrPlanningMasterService HrPlanningMasterService, IDropdownService dropdownService, ICommonService commonService, IHelpers helpers)
        {
            _hrPlanningMasterService = HrPlanningMasterService;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = helpers;
        }
        public async Task<IActionResult> Index()
        {
            try {
            HRPlanningMaster model = new HRPlanningMaster();
                var planningPeriods = await _dropdownService.GetHRPlanningPeriodDropDown();
                ViewBag.PlanningPeriod = new SelectList(planningPeriods, "Code", "Name");
                ViewBag.Branch = new SelectList(await _dropdownService.GetPlanningLocationDropdown(planningPeriods?.FirstOrDefault()?.Code), "Code", "Name");
                ViewBag.Department = await _dropdownService.GetDepartmentDropdown();
                model = await _hrPlanningMasterService.GetHRPlanningList("ACTIVE");
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
        public async Task<IActionResult> GetHrPlanningList(string TabIndex, int? Branch, int? PlanningPeriod)
        {
            try {
            HRPlanningMaster model = new HRPlanningMaster();
            ViewBag.Department = await _dropdownService.GetDepartmentDropdown();
            model = await _hrPlanningMasterService.GetHRPlanningList(TabIndex, Branch, PlanningPeriod);
            if(TabIndex != "APPROVAL_PENDING")
                return PartialView("_ActiveList", model);
            else
                return PartialView("_PendingList", model);
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
        public async Task<JsonResult> GetPlanningLocation(int PlanningPeriod)
        {
            var Branches = await _dropdownService.GetPlanningLocationDropdown(PlanningPeriod);
            return Json(Branches);
        }

        [HttpGet]
        public async Task<JsonResult> GetAllLocation()
        {
            var Branches = await _dropdownService.GetLocationDropdown();
            return Json(Branches);
        }

        public async Task<ActionResult> GetActiveInactiveValidation(string name, int code)
        {
            try
            {
                var response = await _hrPlanningMasterService.GetMasterInactiveValidation(name, code);

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


        public async Task<IActionResult> Create()
        {
            try { 
            ViewBag.Section = new SelectList(await _dropdownService.GetSectionDropdown(), "Code", "Name");
            ViewBag.Department = new SelectList(await _dropdownService.GetDepartmentDropdown(), "Code", "Name");
            ViewBag.Grade = new SelectList(await _dropdownService.GetGradeDropdown(), "Code", "Name");
            HRPlanningMaster model = new HRPlanningMaster();
            model.Active = true;
            model.FromDate = null;
            model.ToDate = null;
            return PartialView("_Create",model);
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
        public async Task<IActionResult> Edit(int Logcode, int Branch, int PlanningPeriod)
        {
            try {
            HRPlanningMaster model = new HRPlanningMaster();
            ViewBag.Section = new SelectList(await _dropdownService.GetSectionDropdown(), "Code", "Name");
            ViewBag.Department = new SelectList(await _dropdownService.GetDepartmentDropdown(), "Code", "Name");
            ViewBag.Grade = new SelectList(await _dropdownService.GetGradeDropdown(), "Code", "Name");
            model = await _hrPlanningMasterService.GetHRPlanningMasterDetails(Logcode, Guid.Parse("00000000-0000-0000-0000-000000000000"), "MODIFY", Branch, PlanningPeriod);
            model.Mode = "MODIFY";
            model.Logcode = Logcode;
            model.Branch = new Dropdown { Code = Branch };
            model.PlanningPeriod = new Dropdown { Code = PlanningPeriod };
            return PartialView("_Create", model);
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
        public async Task<IActionResult> ApproveOrReject(int GI,Guid LogGI, string EntryType)
        {
            try { 
            HRPlanningMaster model = new HRPlanningMaster();
            model = await _hrPlanningMasterService.GetHRPlanningMasterDetails(GI, LogGI, "APPROVAL");
            model.EntryType = EntryType;
            return PartialView("_Approve", model);
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
        public async Task<IActionResult> DeleteHrPlanningDetails(int GI)
        {
            try { 
            HRPlanningMaster model = new HRPlanningMaster();
            model = await _hrPlanningMasterService.GetHRPlanningMasterDetails(GI, Guid.Parse("00000000-0000-0000-0000-000000000000"), "DELETE");
            return PartialView("_Delete", model);
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
        public async Task<IActionResult> Save(HRPlanningMaster model,int Branch,int PlanningPeriod)
        {
            try { 
            if(model.Mode == null)
                model.Mode = "CREATE";
            var response = await _hrPlanningMasterService.Create(model, Branch, PlanningPeriod);
       
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
        public async Task<IActionResult> GetCarryForwardPopup()
        {
            try {
            HRPlanningMaster model = new HRPlanningMaster();
            ViewBag.PlanningPeriod = new SelectList(await _dropdownService.GetHRPlanningPeriodDropDown(), "Code", "Name");
            return PartialView("_CarryForwardPopup", model);
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
        public async Task<IActionResult> CarryForward(int Branch, int FromPlanningPeriod, int ToPlanningPeriod)
        {
            try {
            var response = await _hrPlanningMasterService.CarryForwardPlan(Branch, FromPlanningPeriod, ToPlanningPeriod);
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
        public async Task<IActionResult> Approve(HRPlanningMaster model)
        {
            try { 
            model.ApproveMode = "Approve";
            var response = await _hrPlanningMasterService.ApproveOrReject(model);            
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
        public async Task<ActionResult> Reject(HRPlanningMaster model)
        {
            try { 
            model.ApproveMode = "REJECT";
            var response = await _hrPlanningMasterService.ApproveOrReject(model);
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
        public async Task<IActionResult> DeleteHrPlanning(Guid GI)
        {
            try { 
            var response = await _hrPlanningMasterService.Delete(GI);
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
        public async Task<IActionResult> ExportToExcel(string TabIndex)
        {
            try { 
            HRPlanningMaster model = new HRPlanningMaster();
            model = await _hrPlanningMasterService.GetHRPlanningList(TabIndex);


            int slno = 0;
            string currDateTime = await _helpers.GetCurrentDate();
            
            var LoginUser = HttpContext.Session.GetString("LoginUser");
            var CompanyName = HttpContext.Session.GetString("CompanyName");
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Hr Planning Master (" + TabIndex + ")", "Excel");
                if (TabIndex != "APPROVAL_PENDING")
            {
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add(TabIndex+" List");
                    int count = model.HRPlanningMasterList.Count + 6;


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
                    wsReportNameHeaderRange.Value = "Manpower Plan Grid" + "(" + TabIndex + ")";
                    worksheet.Range("A6:H6").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
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
                    worksheet.Cell(5, 2).Value = model.HRPlanningMasterList.Count;

                    var currentRow = 6;

                    #region Header
                    worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 1).Value = "Sl.No";
                    worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 2).Value = "Department";
                    worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 3).Value = "Section";
                    worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 4).Value = "Grade";
                    worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 5).Value = "Plan Count";
                    worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 6).Value = "Avg Salary";
                    worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 7).Value = "Total Salary";
                    worksheet.Cell(currentRow, 8).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 8).Value = "Status";


                    #endregion

                    #region Body
                    foreach (var item in model.HRPlanningMasterList)
                    {
                        currentRow++;
                        slno = slno + 1;
                        worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 1).Value = slno;
                        worksheet.Cell(currentRow, 2).Value = item.Department?.Name;
                        worksheet.Cell(currentRow, 3).Value = item.Section?.Name;
                        worksheet.Cell(currentRow, 4).Value = item.Grade?.Name;
                        worksheet.Cell(currentRow, 5).Value = item.PlanCount;
                        worksheet.Cell(currentRow, 6).Value = item.AvgSalary;
                        worksheet.Cell(currentRow, 7).Value = item.TotalSalary;
                        worksheet.Cell(currentRow, 8).Value = item.Status;
                        worksheet.Columns(1, 8).Style.Alignment.WrapText = true;


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
                            "HR Planning List.xlsx"
                            );
                    }
                }

            }
            else
            {
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Pending List");
                    int PendingCountnum = model.PendingCount + 6;


                    worksheet.Range("A1:H" + PendingCountnum).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:H" + PendingCountnum).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:H" + PendingCountnum).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:H" + PendingCountnum).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:H" + PendingCountnum).Style.Border.LeftBorder = XLBorderStyleValues.Thin;



                    var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 7)));
                    wsReportNameHeaderRange.Style.Font.Bold = true;
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Merge();


                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Value = "Manpower Plan Grid " + "(" + TabIndex + ")";
                    worksheet.Range("A6:H6").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
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
                    worksheet.Cell(5, 2).Value = model.PendingCount;
                    var currentRow = 6;

                    #region Header

                    worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 1).Value = "Sl.No";
                    worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 2).Value = "Department";
                    worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 3).Value = "Section";
                    worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 4).Value = "Grade";
                    worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 5).Value = "Plan Count";
                    worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 6).Value = "Avg Salary";
                    worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 7).Value = "Total Salary";
                    worksheet.Cell(currentRow, 8).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 8).Value = "Entry Type";



                    #endregion

                    #region Body
                    foreach (var item in model.HRPlanningMasterList)
                    {
                        currentRow++;
                        slno = slno + 1;
                        worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 1).Value = slno;
                        worksheet.Cell(currentRow, 2).Value = item.Department?.Name;
                        worksheet.Cell(currentRow, 3).Value = item.Section?.Name;
                        worksheet.Cell(currentRow, 4).Value = item.Grade?.Name;
                        worksheet.Cell(currentRow, 5).Value = item.PlanCount;
                        worksheet.Cell(currentRow, 6).Value = item.AvgSalary;
                        worksheet.Cell(currentRow, 7).Value = item.TotalSalary;
                        worksheet.Cell(currentRow, 8).Value = item.EntryType;
                        worksheet.Columns(1, 8).Style.Alignment.WrapText = true;

                       
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
                            "HR Planning List.xlsx"
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
