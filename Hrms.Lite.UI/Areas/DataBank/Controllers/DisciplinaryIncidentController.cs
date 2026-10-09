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
using Newtonsoft.Json;
using Hrms.Lite.Shared.Account;

namespace Hrms.Lite.UI.Areas.DataBank.Controllers
{
    [Area("DataBank")]
    [SessionAuthorize]
    public class DisciplinaryIncidentController : Controller
    {
        private readonly ICommonService _commonService;
        private readonly IDropdownService _dropdownService;
        private readonly IDisciplinaryIncidentsService _disciplinaryIncidentsService;
        private readonly IBlob _blob;
        private readonly IHelpers _helpers;
       
        public DisciplinaryIncidentController(ICommonService commonService, IDropdownService dropdownService, IDisciplinaryIncidentsService disciplinaryIncidentsService, IBlob blob,IHelpers helpers)
        {
            _commonService = commonService;
            _dropdownService = dropdownService;
            _disciplinaryIncidentsService = disciplinaryIncidentsService;
            _blob = blob;
            _helpers = helpers;
        }
        public async Task<IActionResult> Index()
        {
            try
            {

                DisciplinaryIncidents di = new DisciplinaryIncidents();
                DisciplinaryIncidents m = new DisciplinaryIncidents();
                di.IncidentsAction = new IncidentsAction();
                di.IncidentsAction.Incidents = new Incidents();
                di.EmployeeHeader = new EmployeeHeader();
                di.EmployeeHeader.Department = new Shared.Master.Department();
                di.EmployeeHeader.Designation = new Shared.Master.Designation();
                di.EmployeeHeader.Location = new Shared.Master.Location();
                di.EmployeeHeader.Category = new Shared.Master.Category();
                Filter filter = new Filter();
                //di.EmployeeHeader = await _commonService.GetEmployeeHeader(Guid.Parse("2A3D150F-9051-4FF8-85B8-E0578D52C0C3"));
                ViewBag.Emp = new SelectList(await _dropdownService.GetEmployeeDropDown(), "Code", "Name");
                ViewBag.Incident = new SelectList(await _dropdownService.GetIncidentDropDown(), "Code", "Name");
                di.DisciplinaryIncidentsTabs = (List<DisciplinaryIncidentsTabs>)await _disciplinaryIncidentsService.GetDisciplinaryIncidentsTabs();
                m = await _disciplinaryIncidentsService.GetDisciplinaryIncidentsList(0, 1,null,filter);
                di.DurationwiseFilterSlab = new Shared.DataBank.Generic.Master.FilterSlabComboFill();
                di.DurationwiseFilterSlab.Code = 1;
                ViewBag.Data = new SelectList(await _commonService.GetDurationWiseFilterSlabDropDown(), "Code", "Name");
                di.DisciplinaryIncidentsList = m.DisciplinaryIncidentsList;
                di.AllTabCount = m.AllTabCount;
                di.Tab1Count = m.Tab1Count;
                di.Tab2Count = m.Tab2Count;
                di.Tab3Count = m.Tab3Count;
                di.MoreTabCount = m.MoreTabCount;
                di.PendingTabCount = m.PendingTabCount;
                ViewBag.Tabs = new SelectList(di.DisciplinaryIncidentsTabs, "Code", "Name");
                di.Filter = await _commonService.GetFilterDropdowns();
                return View(di);
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

        public async Task<IActionResult> ApplyFilter(int TabIndex, int SearchId, Filter filter,int IncidentCode)
        {
            try { 
            DisciplinaryIncidents model = new DisciplinaryIncidents();
            model.Filter = filter;
            model = await _disciplinaryIncidentsService.GetDisciplinaryIncidentsList(TabIndex, SearchId, IncidentCode,filter);
            if (TabIndex == -2)
            {
                return View("_PendingList", model);
            }
            else
            {
                return View("_DisciplinaryIncidentsList", model);
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

        public async Task<IActionResult> Create(Guid EmployeeGI)
        {
            try
            {

                DisciplinaryIncidents model = new DisciplinaryIncidents();
                //model.IncidentsAction = new IncidentsAction();
                model.EmployeeGI = EmployeeGI;
                //model.EmployeeHeader= await _commonService.GetEmployeeHeader();
                model.EmployeeHeader = await _commonService.GetEmployeeHeader(EmployeeGI);
                model.IncidentsAction = new IncidentsAction();
                model.IncidentsAction.Incidents = new Incidents();
                model.IncidentsAction.IncidentLog = new List<IncidentLog>();
                //List<IncidentLog> IL = new List<IncidentLog>();
                //IL = (List<IncidentLog>)await _disciplinaryIncidentsService.GetDisciplinaryIncidentsLogList(EmployeeGI);
                model.IncidentsAction.IncidentLog = (List<IncidentLog>)await _disciplinaryIncidentsService.GetDisciplinaryIncidentsLogList(EmployeeGI);
                //model.AssetRequest.AssetLog = (List<AssetLog>)await _assetAllocationService.GetAssetLogList(EmployeeGI);
                ViewBag.Inc = new SelectList(await _dropdownService.GetIncidentDropDown(), "Code", "Name");
                ViewBag.Ity = new SelectList(await _dropdownService.GetIncidentTypeDropDown(), "Code", "Name");
                ViewBag.Atk = new SelectList(await _dropdownService.GetActionTakenDropDown(), "Code", "Name");

                return View("~/Areas/DataBank/Views/DisciplinaryIncident/Create.cshtml", model);
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
        public async Task<IActionResult> Create(DisciplinaryIncidents model)
        {
            Shared.File file1 = new Shared.File();
            file1 = model.IncidentsAction.File;

            var StorageType = await _commonService.GetFileStorageType();

            try
            {
                if (model.IncidentsAction.File != null)
                {
                    if (model.IncidentsAction.File.FileData != null)
                    {
                        if (StorageType.Data == "BLOB")
                        {

                            var fileobj = await _commonService.UploadIformFileToBLOB(model.IncidentsAction.File, "Documents");
                            if (fileobj.Success == false)
                            {
                                fileobj.Message = "FILE UPLOAD ERROR";
                                return Json(fileobj);
                            }
                            model.IncidentsAction.File = fileobj.Data;

                        }
                        else if (StorageType.Data == "IIS")
                        {
                            var fileobj = await _commonService.UploadIformFileToIIS(model.IncidentsAction.File, "Documents");
                            if (fileobj.Success == false)
                            {
                                fileobj.Message = "FILE UPLOAD ERROR";
                                return Json(fileobj);
                            }
                            model.IncidentsAction.File = fileobj.Data;

                        }
                        else
                        {
                            var fileobj = await _commonService.UploadIformFileToAWS(model.IncidentsAction.File, "Documents");
                            if (fileobj.Success == false)
                            {
                                fileobj.Message = "FILE UPLOAD ERROR";
                                return Json(fileobj);
                            }
                            model.IncidentsAction.File = fileobj.Data;

                        }
                    }
                }

            //try
            //{
            //    if (file1 != null)
            //    {
            //        model.IncidentsAction.File = await _blob.UploadToAzureAsync(model.IncidentsAction.File);
            //        string filename = model.IncidentsAction.File.FileName;
            //        model.IncidentsAction.File.FileData = null;
            //    }
                var response = await _disciplinaryIncidentsService.DisciplinaryIncidentsSave(model);
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
        public IActionResult NewRequest()
        {
            return PartialView("_NewRequest.cshtml");
        }

        public async Task<ActionResult> DisciplinaryTabs(int TabIndex, int SearchId,int IncidentCode)
        {
            try
            {
                DisciplinaryIncidents model = new DisciplinaryIncidents();
                model = await _disciplinaryIncidentsService.GetDisciplinaryIncidentsList(TabIndex, SearchId, IncidentCode,model.Filter);
                model.Tabindex = TabIndex;
                model.SearchId = SearchId;
                model.IncidentsAction = new IncidentsAction();
                model.IncidentsAction.Incidents = new Incidents();
                
                if (TabIndex == -2)
                {
                    return View("_PendingList", model);
                }
                else
                {
                    return View("_DisciplinaryIncidentsList", model);
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
        public async Task<ActionResult> EmployeeCard(int EmpCode)
        {
            try
            {
                DisciplinaryIncidents model = new DisciplinaryIncidents();

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
        public async Task<IActionResult> DisciplinaryIncidentsLog(string DisciplinaryIncidentGI, Guid EmployeeGI)
        {
            try
            {
                DisciplinaryIncidents model = new DisciplinaryIncidents();
                model = await _disciplinaryIncidentsService.GetDisciplinaryIncidentsLogDetails(Guid.Parse(DisciplinaryIncidentGI));
                return PartialView("_DisciplinaryIncidentsLog", model);
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
        public async Task<ActionResult> Pending(int TabIndex)
        {
            try
            {
                DisciplinaryIncidents model = new DisciplinaryIncidents();
                Filter filter = new Filter();
                model.Tabindex = TabIndex;
                model.SearchId = 1;
                model = await _disciplinaryIncidentsService.GetDisciplinaryIncidentsList(model.Tabindex, model.SearchId,null,filter);
                return PartialView("_PendingRequest", model);
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
        public async Task<ActionResult> DisciplinaryIncidentsDetails(Guid DisciplinaryIncidentGI, Guid EmployeeGI)
        {
            try
            {
                DisciplinaryIncidents model = new DisciplinaryIncidents();
                model.IncidentsAction = new IncidentsAction();
                model.IncidentsAction.Incidents = new Incidents();
                model = await _disciplinaryIncidentsService.GetDisciplinaryIncidentsDetails(DisciplinaryIncidentGI);
                model.EmployeeHeader = await _commonService.GetEmployeeHeader(EmployeeGI);
                //model.AssetRequest.AssetReturnDate = model.AssetRequest.AssetReturnDate == DateTime.Parse("01/Jan/0001 12:00:00 AM") ? null : model.AssetRequest.AssetReturnDate;
                return PartialView("_DisciplinaryIncidentsDetails", model);
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
        //Excel
        public async Task<IActionResult> ExportToExcel(int TabIndex, int SearchId, string TabName, int IncidentCode, Filter filter)
        {
            try
            {
                DisciplinaryIncidents model = new DisciplinaryIncidents();
                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Incident Management(" + TabName + ")", "Excel");
                int slNo = 0;
                string FilterValues = await _helpers.GetFilterValues(filter);
                model = await _disciplinaryIncidentsService.GetDisciplinaryIncidentsList(TabIndex, SearchId, IncidentCode, filter);
                if (FilterValues == "")
                {
                    FilterValues = "All";
                }
                string currDateTime = await _helpers.GetCurrentDate();
                
                if (TabIndex == -2) //Pending
                {

                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("Pending List");
                        int count = model.DisciplinaryIncidentsList.Count + 5;


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
                        wsReportNameHeaderRange.Value = "Pending Action";

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
                        worksheet.Cell(3, 6).Value = model.DisciplinaryIncidentsList.Count;
                        worksheet.Cell(4, 1).Style.Font.Bold = true;
                        worksheet.Cell(4, 1).Value = "Filter Applied";
                        worksheet.Range("B4:H4").Merge();
                        worksheet.Cell("B4").Value = FilterValues;




                        var currentRow = 5;
                        #region Header
                        worksheet.Cell(currentRow, 1).Value = "Sl.No";
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
                        //worksheet.Cell(currentRow, 7).Value = "Category";
                        //worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                        //worksheet.Cell(currentRow, 8).Value = "Request Date";
                        //worksheet.Cell(currentRow, 8).Style.Font.Bold = true;

                        #endregion

                        #region Body
                        foreach (var item in model.DisciplinaryIncidentsList)
                        {
                            currentRow++;
                            currentRow++;
                            slNo = slNo + 1;

                            worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Range("A5:H5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                            worksheet.Cell(currentRow, 1).Value = slNo;
                            worksheet.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 2).Value = item.EmpId;
                            worksheet.Cell(currentRow, 3).Value = item.EmployeeName;
                            worksheet.Cell(currentRow, 4).Value = item.Designation.Name;
                            worksheet.Cell(currentRow, 5).Value = item.Department.Name;
                            worksheet.Cell(currentRow, 6).Value = item.Location.Name;
                            //worksheet.Cell(currentRow, 6).Value = item.c;
                            worksheet.Columns(1, 10).Style.Alignment.WrapText = true;
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
                                "Pending List.xlsx"
                                );
                        }
                    }

                }
                else
                {

                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("Incident Management");
                        int count = model.DisciplinaryIncidentsList.Count + 5;


                        worksheet.Range("A1:K" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:K" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:K" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:K" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:K" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;

                        var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:K{0}", 1, Char.ConvertFromUtf32(65 + 9)));
                        wsReportNameHeaderRange.Style.Font.Bold = true;
                        wsReportNameHeaderRange.Style.Font.FontSize = 15;
                        wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                        wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        wsReportNameHeaderRange.Merge();
                        wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191); ;
                        wsReportNameHeaderRange.Value = "Incident Management(" + TabName + ")";

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
                        worksheet.Cell(3, 6).Value = model.DisciplinaryIncidentsList.Count;
                        worksheet.Cell(4, 1).Style.Font.Bold = true;
                        worksheet.Cell(4, 1).Value = "Filter Applied";
                        worksheet.Range("B4:K4").Merge();
                        worksheet.Cell("B4").Value = FilterValues;




                        var currentRow = 5;
                        #region Header
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
                        worksheet.Cell(currentRow, 7).Value = "Incident";
                        worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 8).Value = "Category";
                        worksheet.Cell(currentRow, 8).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 9).Value = "Occured Date";
                        worksheet.Cell(currentRow, 9).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 10).Value = "Action Taken";
                        worksheet.Cell(currentRow, 10).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 11).Value = "Remarks";
                        worksheet.Cell(currentRow, 11).Style.Font.Bold = true;


                        #endregion

                        #region Body
                        foreach (var item in model.DisciplinaryIncidentsList)
                        {
                          
                            currentRow++;
                            slNo = slNo + 1;
                            worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Range("A5:K5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                            worksheet.Cell(currentRow, 1).Value = slNo;
                            worksheet.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 2).Value = item.EmpId;
                            worksheet.Cell(currentRow, 3).Value = item.EmployeeName;
                            worksheet.Cell(currentRow, 4).Value = item.Designation.Name;
                            worksheet.Cell(currentRow, 5).Value = item.Department.Name;
                            worksheet.Cell(currentRow, 6).Value = item.Location.Name;
                            worksheet.Cell(currentRow, 7).Value = item.Incident;
                            worksheet.Cell(currentRow, 8).Value = item.IncidentType;
                            worksheet.Cell(currentRow, 9).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 9).Style.DateFormat.Format = "dd-MMM-yyyy";
                            worksheet.Cell(currentRow, 9).Value = item.Incidentdate;
                            worksheet.Cell(currentRow, 10).Value = item.ActionTaken;
                            worksheet.Cell(currentRow, 11).Value = item.Narration;
                      
                            worksheet.Columns(1, 11).Style.Alignment.WrapText = true;
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
                                "Incident Management.xlsx"
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


        public async Task<ActionResult> DisciplinaryIncidentsDelete(Guid DisciplinaryIncidentGI)
        {
            try
            {
                DisciplinaryIncidents model = new DisciplinaryIncidents();
                model.DisciplinaryIncidentGI = DisciplinaryIncidentGI;
                var response = await _disciplinaryIncidentsService.DisciplinaryIncidentsDelete(DisciplinaryIncidentGI);
                if (response.Success == true)
                {
                    TempData["Message"] = "Deleted Successfully";
                    return RedirectToAction("Index", "DisciplinaryIncident");
                }
                return PartialView("_DisciplinaryIncidentsDetails", model);
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
