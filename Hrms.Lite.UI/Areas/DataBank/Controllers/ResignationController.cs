using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.Account;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.DataBank.Reports;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Spire.Xls;

namespace Hrms.Lite.UI.Areas.DataBank.Controllers
{
    [Area("DataBank")]
    [SessionAuthorize]
    public class ResignationController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly IResignationService _resignationService;
        private readonly ICommonService _commonService;
        private readonly IBlob _blob;
        private readonly IHelpers _helpers;
       
        public ResignationController(IResignationService ResignationService, IDropdownService dropdownService, ICommonService commonService, IBlob blob,IHelpers helpers)
        //public ResignationController(IDropdownService dropdownService, ICommonService commonService, IBlob blob)
        {
            _resignationService = ResignationService;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _blob = blob;
            _helpers = helpers;
        }
        public async Task<IActionResult> Index(string FirstTab = "All")
        {
            try { 
            Resignation model = new Resignation();
            Resignation m = new Resignation();
            UserRole u = new UserRole();
            Filter filter = new Filter();
            m.ResignationListTabs = (List<ResignationListTabs>)await _resignationService.GetResignationTabs();
            model = await _resignationService.GetResignationList(0,filter);
            model.ResignationListTabs = m.ResignationListTabs;
            model.EmployeeHeader = new EmployeeHeader();
            model.EmployeeHeader.Department = new Department();
            model.EmployeeHeader.Designation = new Designation();
            model.EmployeeHeader.Location = new Location();
            model.EmployeeHeader.Category = new Category();
            ViewBag.Emp = new SelectList(await _dropdownService.GetResignationEmployeeDropDown(), "Code", "Name");
                model.SliderFilter = await _commonService.GetFilterDropdowns();
                ViewBag.Tabs = new SelectList(model.ResignationListTabs, "Code", "Name");
            u = await _commonService.GetTabApplicability("Resignation");
            model.UserVsMenuTab = u.UserVsMenuTab;
            model.FirstTab = FirstTab;
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
       

        public async Task<IActionResult> ApplyFilter(int TabIndex ,Filter filter)
        {
            try { 
                Resignation model = new Resignation();
                UserRole u = new UserRole();
               model.SliderFilter = filter;
                model = await _resignationService.GetResignationList(TabIndex, filter);
                u = await _commonService.GetTabApplicability("Resignation");
                model.UserVsMenuTab = u.UserVsMenuTab;
                if (TabIndex == 0)
                {
                    return PartialView("_InNoticePeriodList", model);
                }
                else if (TabIndex == 1)
                {
                    return PartialView("_ResignationAcceptList", model);
                }
                else if (TabIndex == 2)
                {
                    return PartialView("_ResignationCheckListPendingList", model);
                }
                else if (TabIndex == 3)
                {
                    return PartialView("_ResignationRelivingPendingList", model);
                }
                else if (TabIndex == 4)
                    {
                    return PartialView("_ResignationRetirementDueList", model);
                }
                else if (TabIndex == 5)
                {
                    return PartialView("_ApprovalPending", model);
                }
                else //if (TabIndex == 6)
                {
                    return PartialView("_ResignationAmendmentList", model);
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

        public async Task<IActionResult> GetRelievingTracker(Guid ResignationGI, Guid EmployeeGI)
        {
            ResignationTracker model = new ResignationTracker();

            model.EmployeeHeader = new EmployeeHeader();
            model = await _resignationService.GetRelievingTracker(ResignationGI);
            model.EmployeeHeader = await _commonService.GetEmployeeHeader(EmployeeGI);
            model.Details.EmployeeID = model.EmployeeHeader.EmployeeId;
            model.Details.EmployeeName = model.EmployeeHeader.EmployeeName;
            model.Details.EmployeeAbsoluteUri = model.EmployeeHeader.EmployeeAbsoluteUri;
            model.Details.Department = model.EmployeeHeader.Department.Name;
            model.Details.Designation = model.EmployeeHeader.Designation.Name;
            return PartialView("_Tracker", model);
        }
        public async Task<ActionResult> EmployeeCard(int EmpCode)
        {
            try { 
            Resignation model = new Resignation();

            //model = await _assetAllocationService.GetEmployeeCard(EmpCode);
            model.EmployeeHeader = await _commonService.GetEmployeeCard(EmpCode);

            return PartialView("_EmployeeCard", model.EmployeeHeader);
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
        public async Task<IActionResult> AddNew()
        {
            return PartialView("_AddNew");
        }
        public async Task<IActionResult> Create(Guid EmployeeGI)
        {
            try { 
            Resignation model = new Resignation();
            Resignation m = new Resignation();

            model.EmployeeHeader = await _commonService.GetEmployeeHeader(EmployeeGI);
            model.ApprovalPendingDetails = (List<ApprovalPendingDetails>)await _resignationService.GetPendingApprovalsList(EmployeeGI);
            m = await _resignationService.GetNoticePeriodDetails(EmployeeGI,"","");
            ViewBag.ResignationType = new SelectList(await _dropdownService.GetResignationType(), "Code", "Name");
            ViewBag.ResignationReason = new SelectList(await _dropdownService.GetResignationReason(0), "Code", "Name");
            model.NoticePeriodDetails = m.NoticePeriodDetails;
            return View("~/Areas/DataBank/Views/Resignation/Create.cshtml",model);
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
        public async Task<IActionResult> GetResignationAcceptance(Guid EmployeeGI,Guid ResignationGI)
        {
            try { 
            Resignation model = new Resignation();          
            model = await _resignationService.GetResignationAcceptanceDetails(ResignationGI);         
            model.ResignationDetails.IsReplacement = 0;

                if (model.DueClearanceSettings != null)
                {
                    foreach (var item in model.DueClearanceSettings)
                    {
                        if (item.Department == null)
                        {
                            item.Department = new Department();
                            item.Department.Code = 0;
                        }
                        item.ResponsiblePersonDropdown = await _dropdownService.GetDueClearanceResponsiblePersonDropdown(item.Department.Code);
                    }
                }
                model.EmployeeHeader = await _commonService.GetEmployeeHeader(EmployeeGI);
                return View("~/Areas/DataBank/Views/Resignation/ResignationAcceptance.cshtml", model);
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
        public async Task<IActionResult> GetExitCheckList(Guid EmployeeGI, Guid ResignationGI)
        {
            try { 
            Resignation model = new Resignation();
            Resignation m = new Resignation();
            m.EmployeeHeader = await _commonService.GetEmployeeHeader(EmployeeGI);
            model = await _resignationService.GetExitChecklistDetail(ResignationGI);
            model.EmployeeHeader = m.EmployeeHeader;
            ViewBag.Status = new SelectList(await _dropdownService.GetExitcheckStatus(), "Code", "Name");
            return View("~/Areas/DataBank/Views/Resignation/ExitCheckList.cshtml", model);
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
        public async Task<IActionResult> GetEmployeeRelieving(Guid EmployeeGI, Guid ResignationGI)
        {
            try { 
            Resignation model = new Resignation();
            Resignation m = new Resignation();
               
            m.EmployeeHeader = await _commonService.GetEmployeeHeader(EmployeeGI);
            model = await _resignationService.GetRelievingDetails(ResignationGI);
            model.EmployeeHeader = m.EmployeeHeader;
            model.ApprovalPendingDetails = (List<ApprovalPendingDetails>)await _resignationService.GetPendingApprovalsList(EmployeeGI);
            m = await _resignationService.GetNoticePeriodDetails(EmployeeGI, model.ResignationDetails.ResignationDate.Value.ToString("yyyy-MMM-dd"),model.ResignationDetails.LastWorkingDate.Value.ToString("yyyy-MMM-dd"));
            model.NoticePeriodDetails = m.NoticePeriodDetails;
            return View("~/Areas/DataBank/Views/Resignation/EmployeeRelieve.cshtml", model);
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
        public async Task<IActionResult> Create(Resignation model)
        {
            var StorageType = await _commonService.GetFileStorageType();
            try { 
            string filename = null;

                if (model.ResignationDetails.File != null)
                {
                    if (model.ResignationDetails.File.FileData != null)
                    {
                        if (StorageType.Data == "BLOB")
                        {

                            var fileobj = await _commonService.UploadIformFileToBLOB(model.ResignationDetails.File, "Documents");
                            if (fileobj.Success == false)
                            {
                                fileobj.Message = "FILE UPLOAD ERROR";
                                return Json(fileobj);
                            }
                            model.ResignationDetails.File = fileobj.Data;

                        }
                        else if (StorageType.Data == "IIS")
                        {
                            var fileobj = await _commonService.UploadIformFileToIIS(model.ResignationDetails.File, "Documents");
                            if (fileobj.Success == false)
                            {
                                fileobj.Message = "FILE UPLOAD ERROR";
                                return Json(fileobj);
                            }
                            model.ResignationDetails.File = fileobj.Data;

                        }
                        else
                        {
                            var fileobj = await _commonService.UploadIformFileToAWS(model.ResignationDetails.File, "Documents");
                            if (fileobj.Success == false)
                            {
                                fileobj.Message = "FILE UPLOAD ERROR";
                                return Json(fileobj);
                            }
                            model.ResignationDetails.File = fileobj.Data;

                        }
                    }
                }
            var response = await _resignationService.NewRequestSave(model);
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
        public async Task<IActionResult> Approve(Resignation model)
        {
            try { 
            model.ApproveMode = "Approve";

                List<DueClearanceAuthority> DueClearanceAuthority = new List<DueClearanceAuthority>();
                int i = 0;
                if (model.ResignationDetails.SkipDuesClearance != true) //when not skipping
                {
                    if (model.DueClearanceSettings != null)
                    {
                        foreach (var item in model.DueClearanceSettings)
                        {
                            if (item.DefaultSettings == true)
                            {
                                for (var j = 0; j < item.ResponsiblePersons.Length; j++)
                                {
                                    var authority = new DueClearanceAuthority();
                                    authority.ParameterCode = item.ParameterCode;
                                    authority.EmployeeCode = item.ResponsiblePersons[j];
                                    DueClearanceAuthority.Add(authority);
                                }
                            }
                        }
                    }

                    model.DueClearanceAuthority = DueClearanceAuthority;
                }
                var response = await _resignationService.RequestAccepatance(model);
                return Json(response);
            }
            catch (UnauthorizedAccessException e)
            {

                return RedirectToAction("Login", "Account", new { Area = "" });
            }
            catch (Exception e)
            {
                var E = e.Message.ToString();
                var abc = E;
                return RedirectToAction("Error", "Account", new { Area = "" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Reject(Resignation model)
        {
            try { 
            model.ApproveMode = "Reject";
            var response = await _resignationService.RequestAccepatance(model);
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
        public async Task<IActionResult> ExiCheckListSave(Resignation model)
        {
            try { 
            var response = await _resignationService.ExitCheck(model);
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
        public async Task<IActionResult> EmployeeRelieveSave(Resignation model,string Mode)
        {
            try {
                model.ApproveMode = Mode;
            var response = await _resignationService.EmployeeRelievingSave(model);
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




      


        [HttpGet]
        public async Task<JsonResult> GetResignationReason(int id)
        {
            var ResignationReason = new SelectList(await _dropdownService.GetResignationReason(id), "Code", "Name");
            return Json(ResignationReason);
        }

        [HttpGet]
        public async Task<JsonResult> GetNoticePeriodDetails(Guid EmployeeGI,DateTime ResignationDate,DateTime LastWorkingDate)
        {
            var NoticePeriodDetails = await _resignationService.GetNoticePeriodDetails(EmployeeGI, ResignationDate.ToString("yyyy-MMM-dd"), LastWorkingDate.ToString("yyyy-MMM-dd"));
            return Json(NoticePeriodDetails);
        }
        
        public async Task<IActionResult> ExportToExcel(int TabIndex, string TabName, Filter filter)
        {
            try { 
            Resignation model = new Resignation();
            var LoginUser = HttpContext.Session.GetString("LoginUser");
            var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
            EP = await _commonService.GetExcelPassword("Resignation (" + TabName + ")", "Excel");
           
            string FilterValues = await _helpers.GetFilterValues(filter);
            model = await _resignationService.GetResignationList(TabIndex, filter);
            if (FilterValues == "")
            {
                FilterValues = "All";
            }
            string currDateTime = await _helpers.GetCurrentDate();
            
            if (TabIndex ==0) //in notice period
            {
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add(TabName+" List");
                    int count = model.ResignationList.Count + 5;


                    worksheet.Range("A1:K" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:K" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:K" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:K" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:K" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;

                    var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 10)));
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
                    worksheet.Cell(3, 6).Value = model.ResignationList.Count;
                    worksheet.Cell(4, 1).Style.Font.Bold = true;
                    worksheet.Cell(4, 1).Value = "Filter Applied";
                    worksheet.Range("B4:K4").Merge();
                    worksheet.Cell("B4").Value = FilterValues;


                    var currentRow = 5;
                    #region Header
                    worksheet.Cell(currentRow, 1).Value = "Slno.";
                    worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 2).Value = "ID";
                    worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 3).Value = "EmployeeName";
                    worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 4).Value = "Fun.Designation";
                    worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 5).Value = "Department";
                    worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 6).Value = "Location";
                    worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 7).Value = "DOJ";
                    worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 8).Value = "Y/S";
                    worksheet.Cell(currentRow, 8).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 9).Value = "LWD";
                    worksheet.Cell(currentRow, 9).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 10).Value = "Reason";
                    worksheet.Cell(currentRow, 10).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 11).Value = "Stage";
                    worksheet.Cell(currentRow, 11).Style.Font.Bold = true;

                    #endregion
                    int i = 1;
                    #region Body
                    foreach (var item in model.ResignationList)
                {
                    currentRow++;
                        worksheet.Range("A5:K5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                        worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 1).Value = i;
                        worksheet.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 2).Value = item.EmployeeID;
                        worksheet.Cell(currentRow, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 3).Value = item.EmployeeName;
                        worksheet.Cell(currentRow, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 4).Value = item.Designation.Name;
                        worksheet.Cell(currentRow, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 5).Value = item.Department.Name;
                        worksheet.Cell(currentRow, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 6).Value = item.Location.Name;
                        worksheet.Cell(currentRow, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);                            
                        worksheet.Cell(currentRow, 7).Style.DateFormat.Format = "dd-MMM-yyyy";
                        worksheet.Cell(currentRow, 7).Value = item.DOJ;
                        worksheet.Cell(currentRow, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 8).Value = item.YOS;
                        worksheet.Cell(currentRow, 9).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 9).Style.DateFormat.Format = "dd-MMM-yyyy";
                        worksheet.Cell(currentRow, 9).Value = item.LWD;
                        worksheet.Cell(currentRow, 10).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 10).Value = item.Reason;
                        worksheet.Cell(currentRow, 11).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 11).Value = item.Stage;
                        i++;
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
                            book.Protect(EP.Password,false,false);
                            book.SaveToStream(stream);
                        }
                        var content = stream.ToArray();
                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        TabName+" List.xlsx"
                        );
                }
            }
            }
            else if(TabIndex == 1 || TabIndex == 2 || TabIndex == 3) //accept,checklist,relieving
            {
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add(TabName + " List");
                    int count = model.ResignationList.Count + 5;


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
                    worksheet.Cell(3, 6).Value = model.ResignationList.Count;
                    worksheet.Cell(4, 1).Style.Font.Bold = true;
                    worksheet.Cell(4, 1).Value = "Filter Applied";
                    worksheet.Range("B4:J4").Merge();
                    worksheet.Cell("B4").Value = FilterValues;

                    var currentRow = 5;
                    #region Header
                    worksheet.Cell(currentRow, 1).Value = "Slno.";
                    worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 2).Value = "ID";
                    worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 3).Value = "EmployeeName";
                    worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 4).Value = "Designation";
                    worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 5).Value = "Department";
                    worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 6).Value = "Location";
                    worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 7).Value = "DOJ";
                    worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 8).Value = "Y/S";
                    worksheet.Cell(currentRow, 8).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 9).Value = "LWD";
                    worksheet.Cell(currentRow, 9).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 10).Value = "Reason";
                    worksheet.Cell(currentRow, 10).Style.Font.Bold = true;


                    #endregion
                    int i = 1;
                    #region Body
                    foreach (var item in model.ResignationList)
                    {
                        currentRow++;
                        worksheet.Range("A5:J5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                        worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 1).Value = i;
                        worksheet.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 2).Value = item.EmployeeID;
                        worksheet.Cell(currentRow, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 3).Value = item.EmployeeName;
                        worksheet.Cell(currentRow, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 4).Value = item.Designation.Name;
                        worksheet.Cell(currentRow, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 5).Value = item.Department.Name;
                        worksheet.Cell(currentRow, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 6).Value = item.Location.Name;
                        worksheet.Cell(currentRow, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 7).Style.DateFormat.Format = "dd-MMM-yyyy";
                        worksheet.Cell(currentRow, 7).Value = item.DOJ;
                        worksheet.Cell(currentRow, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 8).Value = item.YOS;
                        worksheet.Cell(currentRow, 9).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 9).Style.DateFormat.Format = "dd-MMM-yyyy";
                        worksheet.Cell(currentRow, 9).Value = item.LWD;
                        worksheet.Cell(currentRow, 10).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 10).Value = item.Reason;
                        i++;

                        worksheet.Columns(1, 10).Style.Alignment.WrapText = true;
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
                                book.Protect(EP.Password,false,false);
                                book.SaveToStream(stream);
                            }
                            var content = stream.ToArray();
                            return File(
                            content,
                            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                            TabName + " List.xlsx"
                            );
                    }
                }
            }
            else
            {
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add(TabName + " List");
                    int count = model.ResignationList.Count + 5;


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
                    worksheet.Cell(3, 6).Value = model.ResignationList.Count;
                    worksheet.Cell(4, 1).Style.Font.Bold = true;
                    worksheet.Cell(4, 1).Value = "Filter Applied";
                    worksheet.Range("B4:H4").Merge();
                    worksheet.Cell("B4").Value = FilterValues;


                   

                  

                    var currentRow = 5;

                    #region Header
                    worksheet.Cell(currentRow, 1).Value = "Slno.";
                    worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 2).Value = "ID";
                    worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 3).Value = "EmployeeName";
                    worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 4).Value = "Designation";
                    worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 5).Value = "Department";
                    worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 6).Value = "Location";
                    worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 7).Value = "Category";
                    worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 8).Value = "Y/S";
                    worksheet.Cell(currentRow, 8).Style.Font.Bold = true;

                    #endregion
                    int i = 0;
                    #region Body
                    foreach (var item in model.ResignationList)
                    {
                        currentRow++;
                        worksheet.Range("A5:H5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                        worksheet.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 1).Value = i;
                        worksheet.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 2).Value = item.EmployeeID;
                        worksheet.Cell(currentRow, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 3).Value = item.EmployeeName;
                        worksheet.Cell(currentRow, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 4).Value = item.Designation.Name;
                        worksheet.Cell(currentRow, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 5).Value = item.Department.Name;
                        worksheet.Cell(currentRow, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 6).Value = item.Location.Name;
                        worksheet.Cell(currentRow, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 7).Style.DateFormat.Format = "dd-MMM-yyyy";
                        worksheet.Cell(currentRow, 7).Value = item.Category.Name;
                        worksheet.Cell(currentRow, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 8).Value = item.YOS;
                        i++;
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
                                book.Protect(EP.Password,false,false);
                                book.SaveToStream(stream);
                            }
                            var content = stream.ToArray();
                            return File(
                            content,
                            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                            TabName + " List.xlsx"
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

        public async Task<IActionResult> GetResignationAmendmentDetails(Guid EmployeeGI, Guid ResignationGI)
        {
            try
            {
                Resignation model = new Resignation();
                model.ResignationAmendmentDetails = new ResignationList();

                model = await _resignationService.GetResignationAmendmentDetails(ResignationGI);
                model.EmployeeHeader = await _commonService.GetEmployeeHeader(EmployeeGI);
                return PartialView("_AmendmentCreate", model);
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

        public async Task<IActionResult> SaveResignationAmendment(Resignation model)
        {
            try
            {
               
                var response = await _resignationService.SaveResignationAmendment(model);
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

    }
}
