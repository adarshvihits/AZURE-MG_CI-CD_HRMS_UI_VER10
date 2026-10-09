using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.Account;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.DataBank.Generic.Master;
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
    public class RewardsAndRecognitionController : Controller
    {
        private IDropdownService _dropdownService;
        private readonly IRewardsAndRecognitionService _rewardsAndRecognitionService;
        private readonly ICommonService _commonService;
        private readonly IBlob _blob;
        private readonly IHelpers _helpers;
        
        public RewardsAndRecognitionController(IRewardsAndRecognitionService rewardsAndRecognitionService, ICommonService commonService, IBlob blob,IDropdownService dropdownService, IHelpers helpers)
        {
            _rewardsAndRecognitionService = rewardsAndRecognitionService;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = helpers;
            _blob = blob;
        }
         
        public async Task<IActionResult> Index()
        {
            try 
            { 
                TempData.Remove("Filter_RewardsRecognition");
                RewardsAndRecognition model = new RewardsAndRecognition();
                Filter filter = new Filter();
                model.RewardsRequest = new RewardsRequest();
                model.RewardsRequest.Rewards = new Rewards();
                model.RewardsRequest.PointCriteria = new PointCriteria();
                RewardsAndRecognition m = new RewardsAndRecognition();
                model.EmployeeHeader = new Shared.General.EmployeeHeader();
                model.EmployeeHeader.Department = new Shared.Master.Department();
                model.EmployeeHeader.Designation = new Shared.Master.Designation();
                model.EmployeeHeader.Location = new Shared.Master.Location();
                model.EmployeeHeader.Category = new Shared.Master.Category();
                model.RewardsAndRecognitionFilterCombo = new RewardsAndRecognitionFilterCombo();
            
                model.RewardandRecognitionTabs = (List<RewardandRecognitionTabs>)await _rewardsAndRecognitionService.GetRewardsAndRecognitionTabs();
                ViewBag.Employee = new SelectList(await _dropdownService.GetEmployeeDropDown(), "Code", "Name");
                ViewBag.Filter = new SelectList(await _dropdownService.GetRewardsAndRecognitionFilterDropDown(), "Code", "Name");
                ViewBag.Rewards = new SelectList(await _dropdownService.GetRewardsnRecognitionRewardsDropdown(), "Code", "Name");
            
                model.Tabindex = 0;
                model.SearchId = 0;  //Last 3 Months//

                m = await _rewardsAndRecognitionService.GetRewardsAndRecognitionList(model.Tabindex, model.SearchId,null, filter);
                model.RewardsAndRecognitionList = m.RewardsAndRecognitionList;
                model.AllTabCount = m.AllTabCount;
                model.Tab1Count = m.Tab1Count;
                model.Tab2Count = m.Tab2Count;
                model.MoreTabCount = m.MoreTabCount;
                model.PendingTabCount = m.PendingTabCount;
                model.PointSummaryCount = m.PointSummaryCount;
                model.RewardsAndRecognitionList = m.RewardsAndRecognitionList;
                model.RewardsAndRecognitionFilterCombo.Code = 0; //Last 3 Months//
                model.Filter = await _commonService.GetFilterDropdowns();
                ViewBag.Tabs = new SelectList(model.RewardandRecognitionTabs, "Code", "Name");
                return View("~/Areas/DataBank/Views/RewardsAndRecognition/Index.cshtml",model);
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
        public async Task<IActionResult> Tab(int Tabindex,int SearchId,int RewardCode)
        {
            try
            {
            
            RewardsAndRecognition model = new RewardsAndRecognition();
            Filter filter = new Filter();
            if (TempData["Filter_RewardsRecognition"] != null)
            {
                filter = JsonConvert.DeserializeObject<Filter>(TempData["Filter_RewardsRecognition"].ToString());
            }
            model = await _rewardsAndRecognitionService.GetRewardsAndRecognitionList(Tabindex, SearchId, RewardCode,filter);
            model.Tabindex = Tabindex;
            model.SearchId = SearchId;
            TempData["Filter_RewardsRecognition"] = JsonConvert.SerializeObject(filter);
            if (Tabindex==1)
                return PartialView("_PointsSummaryList", model);
             else if(Tabindex == -2)
                return PartialView("_PendingRecommendations", model);
            else
                return PartialView("_RewardsList", model);
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
        public async Task<ActionResult> RewardsAndRecognitionDetails(Guid RewardsAndRecognitionGI, Guid EmployeeGI)
        {
            try { 
            RewardsAndRecognition model = new RewardsAndRecognition();
           
            model = await _rewardsAndRecognitionService.GetRewardsAndRecognitionDetails(RewardsAndRecognitionGI);
            model.EmployeeHeader = await _commonService.GetEmployeeHeader(EmployeeGI);
            return PartialView("_RewardDetails", model);
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
       
       
     
        public async Task<IActionResult> ApplyFilter(int TabIndex, int SearchId,int RewardCode, Filter filter)
        {
            try { 
            RewardsAndRecognition model = new RewardsAndRecognition();
            model.Filter = filter;
            model = await _rewardsAndRecognitionService.GetRewardsAndRecognitionList(TabIndex, SearchId, RewardCode,filter);
            if (TabIndex == 1)
                return PartialView("_PointsSummaryList", model);
            else if (TabIndex == -2)
                return PartialView("_PendingRecommendations", model);
            else
                return PartialView("_RewardsList", model);
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



        public async Task<ActionResult> EmployeeSummary(Guid EmployeeGI,int SearchId)
        {
            try { 
            RewardsAndRecognition model = new RewardsAndRecognition();
            model = await _rewardsAndRecognitionService.GetEmployeeSummary(EmployeeGI,SearchId);
            model.EmployeeHeader = await _commonService.GetEmployeeHeader(EmployeeGI);
            return PartialView("_PointSummaryDetails", model);
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
            try { 
            fillPointCriteriaDropdown();
            RewardsRequest model = new RewardsRequest();
            model.Rewards = new Rewards();
            model.PointCriteria = new PointCriteria();
            model.EmployeeGI = EmployeeGI;
            ViewBag.Rewards = new SelectList(await _dropdownService.GetRewardsDropdown(EmployeeGI), "Code", "Name");
            ViewBag.RecommendedBy = new SelectList(await _dropdownService.GetRewardsRecommendedByDropdown(EmployeeGI), "Code", "Name");
            model.EmployeeHeader = await _commonService.GetEmployeeHeader(EmployeeGI);
            model.RewardsLog = new List<RewardsLog>();
            model.RewardsLog = (List<RewardsLog>) await _rewardsAndRecognitionService.GetRewardLogList(EmployeeGI);
            if (model.RewardsLog.Count == 0)
                model.TotalPoints = 0;
            else
              model.TotalPoints = model.RewardsLog[0].RewardsRequest.TotalPoints;
               
            return View("~/Areas/DataBank/Views/RewardsAndRecognition/Create.cshtml", model);
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
        public async Task<IActionResult> Save(RewardsRequest model)
        {
            

            var StorageType = await _commonService.GetFileStorageType();
            try
            {
                if (model.File != null)
                {
                    if (model.File.FileData != null)
                    {
                        if (StorageType.Data == "BLOB")
                        {

                            var fileobj = await _commonService.UploadIformFileToBLOB(model.File, "Documents");
                            if (fileobj.Success == false)
                            {
                                fileobj.Message = "FILE UPLOAD ERROR";
                                return Json(fileobj);
                            }
                            model.File = fileobj.Data;

                        }
                        else if (StorageType.Data == "IIS")
                        {
                            var fileobj = await _commonService.UploadIformFileToIIS(model.File, "Documents");
                            if (fileobj.Success == false)
                            {
                                fileobj.Message = "FILE UPLOAD ERROR";
                                return Json(fileobj);
                            }
                            model.File = fileobj.Data;

                        }
                        else
                        {
                            var fileobj = await _commonService.UploadIformFileToAWS(model.File, "Documents");
                            if (fileobj.Success == false)
                            {
                                fileobj.Message = "FILE UPLOAD ERROR";
                                return Json(fileobj);
                            }
                            model.File = fileobj.Data;

                        }
                    }
                }

             
                var response = await _rewardsAndRecognitionService.NewRequest(model);
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
        public async Task<IActionResult> RewardsApprove(Guid RewardsAndRecognitionGI, Guid EmployeeGI)
        {
            try { 
            RewardsAndRecognition model = new RewardsAndRecognition();
            model = await  _rewardsAndRecognitionService.GetRewardsAndRecognitionDetails(RewardsAndRecognitionGI);
            model.EmployeeHeader = await _commonService.GetEmployeeHeader(EmployeeGI);
            ViewBag.PointCriteria = new SelectList(await _dropdownService.GetRewardPointsCriteriaDropdown(model.RewardsRequest.Rewards.Code), "Code", "Name");
            return PartialView("_RewardsApprove",model);
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
        public async Task<IActionResult> Approve (RewardsRequest model)
        {
            try { 
            var response = await _rewardsAndRecognitionService.Approve(model);
            if (response.Success == true)
            {
                TempData["Message"] = "Saved Successfully";
                return RedirectToAction("Index", "RewardsAndRecognition");
            }
            else
            {
                TempData["Message"] = response.Message;
                return View("~/Areas/DataBank/Views/RewardsAndRecognition/Create.cshtml", model);
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
        public async Task<ActionResult> Delete(Guid RewardsAndRecognitionGI)
        {
            try { 
            var response = await _rewardsAndRecognitionService.Delete(RewardsAndRecognitionGI);
            if (response.Success == true)
            {
                TempData["Message"] = "Deleted Successfully";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Message"] = response.Message;
                return RedirectToAction("Index");
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
        public IActionResult NewRequest()
        {
         

            return PartialView("_NewRequestRewards.cshtml");
        }
       

        public async Task<IActionResult> EmployeeCard(int EmpCode)
        {
            try { 
            RewardsRequest model = new RewardsRequest();
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
        public  async Task<IActionResult>RewardLogDetails(Guid RewardsAndRecognitionGI)
        {
            try { 
            RewardsRequest model = new RewardsRequest();
            model = await _rewardsAndRecognitionService.GetRewardsLogDetails(RewardsAndRecognitionGI);

            return PartialView("_RewardLogDetails",model);
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

        private async void fillPointCriteriaDropdown(int RewardCode = 0)
        {
            ViewBag.PointCriteria = new SelectList(await _dropdownService.GetRewardPointsCriteriaDropdown(RewardCode), "Code", "Name");

        }
        [HttpGet]
        public async Task<JsonResult> GetPointCriteria(int RewardCode)
        {
            var PointCriterias = await _dropdownService.GetRewardPointsCriteriaDropdown(RewardCode);
            return Json(PointCriterias);

        }
        [HttpGet]
        public async Task<JsonResult> GetPoints(int PointCriteriaCode)
        {
            var Point = await _rewardsAndRecognitionService.GetRewardPoints(PointCriteriaCode);
            return Json(Point);

        }
    

    
        public async Task<IActionResult> ExportToExcel(int TabIndex, int SearchId, string SearchName,string TabName,int RewardCode)
        {
            try { 
            RewardsAndRecognition model = new RewardsAndRecognition();
            Filter filter = new Filter();
            var LoginUser = HttpContext.Session.GetString("LoginUser");
            var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
            EP = await _commonService.GetExcelPassword("Rewards and Recognition (" + TabName + ")", "Excel");
            int slNo = 0;
            
            string FilterValues = await _helpers.GetFilterValues(filter);
                
            model = await _rewardsAndRecognitionService.GetRewardsAndRecognitionList(TabIndex, SearchId, RewardCode, filter);
            if (FilterValues == "")
            {
                FilterValues = "All";
            }
            string currDateTime = await _helpers.GetCurrentDate();
            
                //if (TabIndex == -2) //Pending
                //{

                //    using (var workbook = new XLWorkbook())
                //    {
                //        var worksheet = workbook.Worksheets.Add("Pending Recommendations");
                //        int count = model.RewardsAndRecognitionList.Count + 5;


                //        worksheet.Range("A1:G" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                //        worksheet.Range("A1:G" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                //        worksheet.Range("A1:G" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                //        worksheet.Range("A1:G" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                //        worksheet.Range("A1:G" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;

                //        var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 7)));
                //        wsReportNameHeaderRange.Style.Font.Bold = true;
                //        wsReportNameHeaderRange.Style.Font.FontSize = 15;
                //        wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                //        wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                //        wsReportNameHeaderRange.Merge();
                //        wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191); 
                //        wsReportNameHeaderRange.Value = "Pending Recommendations";

                //        worksheet.Cell(2, 1).Style.Font.Bold = true;
                //        worksheet.Cell(2, 1).Value = "Company Name";
                //        worksheet.Cell(2, 2).Value = CompanyName.ToUpper();
                //        worksheet.Cell(3, 1).Style.Font.Bold = true;
                //        worksheet.Cell(3, 1).Value = "Login User";
                //        worksheet.Cell(3, 2).Value = LoginUser;
                //        worksheet.Cell(3, 3).Style.Font.Bold = true;
                //        worksheet.Cell(3, 3).Value = "Date & Time";
                //        worksheet.Cell(3, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                //        worksheet.Cell(3, 4).Style.DateFormat.Format = "dd-MMM-yyyy   h:mm AM/PM";
                //        worksheet.Cell(3, 4).Value = currDateTime.ToString();
                //        worksheet.Cell(3, 5).Style.Font.Bold = true;
                //        worksheet.Cell(3, 5).Value = "Total Count";
                //        worksheet.Cell(3, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                //        worksheet.Cell(3, 6).Value = model.RewardsAndRecognitionList.Count;
                //        worksheet.Cell(4, 1).Style.Font.Bold = true;
                //        worksheet.Cell(4, 1).Value = "Filter Applied";
                //        worksheet.Range("B4:G4").Merge();
                //        worksheet.Cell("B4").Value = FilterValues;


                //        #region Header
                //        var currentRow = 5;

                //        worksheet.Cell(currentRow, 1).Value = "Sl.No";
                //        worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                //        worksheet.Cell(currentRow, 2).Value = "ID";
                //        worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                //        worksheet.Cell(currentRow, 3).Value = "EmployeeName";
                //        worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                //        worksheet.Cell(currentRow, 4).Value = "Designation";
                //        worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                //        worksheet.Cell(currentRow, 5).Value = "Department";
                //        worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                //        worksheet.Cell(currentRow, 6).Value = "Location";
                //        worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
                //        worksheet.Cell(currentRow, 7).Value = "Rewards Type";
                //        worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                //        worksheet.Cell(currentRow, 8).Value = "Request Date";
                //        worksheet.Cell(currentRow, 8).Style.Font.Bold = true;

                //        #endregion

                //        #region Body
                //        foreach (var item in model.RewardsAndRecognitionList)
                //        {
                //            currentRow++;
                //            slNo = slNo + 1;
                //            worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                //            worksheet.Range("A5:G5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                //            worksheet.Cell(currentRow, 1).Value = slNo;
                //            worksheet.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                //            worksheet.Cell(currentRow, 2).Value = item.EmpId;
                //            worksheet.Cell(currentRow, 3).Value = item.EmployeeName;
                //            worksheet.Cell(currentRow, 4).Value = item.Designation.Name;
                //            worksheet.Cell(currentRow, 5).Value = item.Department.Name;
                //            worksheet.Cell(currentRow, 6).Value = item.Location.Name;
                //            worksheet.Cell(currentRow, 7).Value = item.RewardsRequest.Rewards.Name;
                //            worksheet.Cell(currentRow, 8).Value = item.RewardsRequest.RewardDate;
                //            worksheet.Columns(1, 8).Style.Alignment.WrapText = true;
                //            worksheet.Rows().Style.Alignment.WrapText = true;
                //        }
                //        #endregion
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
                //                book.Protect(EP.Password,false,false);
                //                book.SaveToStream(stream);
                //            }
                //            var content = stream.ToArray();
                //            return File(
                //                content,
                //                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                //                "Pending Recommendations.xlsx"
                //                );
                //        }
                //    }

                //}


                if (TabIndex == 0) //Pending
                {

                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("Rewards and Recognition");
                        int count = model.RewardsAndRecognitionList.Count + 5;


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
                        wsReportNameHeaderRange.Value = "Rewards and Recognition (" + TabName + ")";

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
                        worksheet.Cell(3, 6).Value = model.RewardsAndRecognitionList.Count;
                        worksheet.Cell(4, 1).Style.Font.Bold = true;
                        worksheet.Cell(4, 1).Value = "Filter Applied";
                        worksheet.Range("B4:I4").Merge();
                        worksheet.Cell("B4").Value = FilterValues;


                        #region Header
                        var currentRow = 5;

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
                        worksheet.Cell(currentRow, 7).Value = "Rewards Type";
                        worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 8).Value = "Point Criteria";
                        worksheet.Cell(currentRow, 8).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 9).Value = "Reward Date";
                        worksheet.Cell(currentRow, 9).Style.Font.Bold = true;

                        #endregion

                        #region Body
                        foreach (var item in model.RewardsAndRecognitionList)
                        {
                            currentRow++;
                            slNo = slNo + 1;
                            worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Range("A5:I5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                            worksheet.Cell(currentRow, 1).Value = slNo;
                            worksheet.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 2).Value = item.EmpId;
                            worksheet.Cell(currentRow, 3).Value = item.EmployeeName;
                            worksheet.Cell(currentRow, 4).Value = item.Designation.Name;
                            worksheet.Cell(currentRow, 5).Value = item.Department.Name;
                            worksheet.Cell(currentRow, 6).Value = item.Location.Name;
                            worksheet.Cell(currentRow, 7).Value = item.RewardsRequest.Rewards.Name;
                            worksheet.Cell(currentRow, 8).Value = item.RewardsRequest.PointCriteria.Name;
                            worksheet.Cell(currentRow, 9).Value = item.RewardsRequest.RewardDate;
                            worksheet.Cell(currentRow, 9).Style.DateFormat.Format = "dd-MMM-yyyy ";
                            worksheet.Columns(1, 9).Style.Alignment.WrapText = true;
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
                                "Rewards and Recognition (" + TabName + ").xlsx"
                                );
                        }
                    }

                }

               





















            else  if( TabIndex==-3) //Point Summary
            {

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Rewards and Recognition");
                    int count = model.RewardsAndRecognitionList.Count + 5;


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
                    wsReportNameHeaderRange.Value = "Rewards and Recognition (" + TabName + ")";

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
                    worksheet.Cell(3, 6).Value = model.RewardsAndRecognitionList.Count;
                    worksheet.Cell(4, 1).Style.Font.Bold = true;
                    worksheet.Cell(4, 1).Value = "Filter Applied";
                    worksheet.Range("B4:G4").Merge();
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
                    worksheet.Cell(currentRow, 7).Value = "Total Points";
                    worksheet.Cell(currentRow, 7).Style.Font.Bold = true;

                    #endregion

                    #region Body
                    foreach (var item in model.RewardsAndRecognitionList)
                    {
                        currentRow++;
                        slNo = slNo + 1;
                        worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Range("A5:G5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                        worksheet.Cell(currentRow, 1).Value = slNo;
                        worksheet.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 2).Value = item.EmpId;
                        worksheet.Cell(currentRow, 3).Value = item.EmployeeName;
                        worksheet.Cell(currentRow, 4).Value = item.Designation.Name;
                        worksheet.Cell(currentRow, 5).Value = item.Department.Name;
                        worksheet.Cell(currentRow, 6).Value = item.Location.Name;
                        worksheet.Cell(currentRow, 7).Value = item.RewardsRequest.TotalPoints;
                        worksheet.Columns(1, 7).Style.Alignment.WrapText = true;

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
                            "Rewards and Recognition (" + TabName + ").xlsx"
                            );
                    }
                }

            }
            else
            {
               
                    using (var workbook = new XLWorkbook())
                    {
                    var worksheet = workbook.Worksheets.Add("Rewards And Recognition List");
                    int count = model.RewardsAndRecognitionList.Count + 5;
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
                    wsReportNameHeaderRange.Value =" Rewards And Recognition  ( " + TabName + " )";


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
                    worksheet.Cell(3, 6).Value = model.RewardsAndRecognitionList.Count;
                    worksheet.Cell(4, 1).Style.Font.Bold = true;
                    worksheet.Cell(4, 1).Value = "Filter Applied";
                    worksheet.Range("B4:G4").Merge();
                    worksheet.Cell("B4").Value = FilterValues;

                    var currentRow = 5;
                    #region Header
                    worksheet.Cell(currentRow, 1).Value = "Sl.No";
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
                        worksheet.Cell(currentRow, 7).Value = "Total points";
                        worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                        //worksheet.Cell(currentRow, 7).Value = "Rewards Type";
                        //worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                        //worksheet.Cell(currentRow, 8).Value = "Point Criteria";
                        //worksheet.Cell(currentRow, 8).Style.Font.Bold = true;
                        //worksheet.Cell(currentRow, 9).Value = "Reward Date";
                        //worksheet.Cell(currentRow, 9).Style.Font.Bold = true;

                        #endregion
                        worksheet.Columns().AdjustToContents();
                    worksheet.Rows().AdjustToContents();
                    #region Body
                    foreach (var item in model.RewardsAndRecognitionList)
                        {
                            currentRow++;
                            slNo = slNo + 1;
                            worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Range("A5:G5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                            worksheet.Cell(currentRow, 1).Value = slNo;
                            worksheet.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 2).Value = item.EmpId;
                            worksheet.Cell(currentRow, 3).Value = item.EmployeeName;
                            worksheet.Cell(currentRow, 4).Value = item.Designation.Name;
                            worksheet.Cell(currentRow, 5).Value = item.Department.Name;
                            worksheet.Cell(currentRow, 6).Value = item.Location.Name;
                            worksheet.Cell(currentRow, 7).Value = item.RewardsRequest.TotalPoints;
                            //worksheet.Cell(currentRow, 8).Value = item.RewardsRequest.PointCriteria.Name;
                            //worksheet.Cell(currentRow, 9).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            //worksheet.Cell(currentRow, 9).Value = item.RewardsRequest.RewardDate;
                            //worksheet.Cell(currentRow, 9).Style.DateFormat.Format = "dd-MMM-yyyy";
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
                                "Rewards And Recognition  ( " + TabName + " ).xlsx"
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
