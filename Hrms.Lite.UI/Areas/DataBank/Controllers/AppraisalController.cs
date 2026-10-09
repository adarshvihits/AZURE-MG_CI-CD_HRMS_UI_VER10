using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.DataBank.Generic.Master;
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
    public class AppraisalController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly ICommonService _commonService;
        private readonly IAppraisalService _appraisalService;
        private readonly IHelpers _helpers;
       
        public AppraisalController(IDropdownService dropdownService, ICommonService commonService, IAppraisalService appraisalService, IHelpers helpers)
        {
            _dropdownService = dropdownService;
            _commonService = commonService;
            _appraisalService = appraisalService;
            _helpers = helpers;
        }
        public async Task<IActionResult> Index()
        {
            try { 
            Appraisal ap = new Appraisal();
            Appraisal m = new Appraisal();
            Filter filter = new Filter(); ap.AppraisalRequest = new AppraisalRequest();
            ap.AppraisalRequest.EvaluationPeriod = new EvaluationPeriod();
            ViewBag.Emp = new SelectList(await _dropdownService.GetEmployeeDropDown(), "Code", "Name");
            ViewBag.AppraisalType = new SelectList(await _dropdownService.GetAppraisalTypeDropDown(), "Code", "Name");
            ap = await _appraisalService.GetAppraisalEvaluationPeriod();
            ap.AppraisalTabs = (List<AppraisalTabs>)await _appraisalService.GetAppraisalTabs();
            ap.DurationwiseFilterSlab = new FilterSlabComboFill();
            ap.DurationwiseFilterSlab.Code = ap.AppraisalRequest.EvaluationPeriod.Code;
            m = await _appraisalService.GetAppraisalList(0, ap.DurationwiseFilterSlab.Code, null,filter);
            ViewBag.Filter = new SelectList(await _dropdownService.GetAppraisalEvaluationPeriodDropDown(), "Code", "Name");
            //di.IncidentsAction.Incidents = new Incidents();
            ap.AppraisalRequest.AppraisalType = new AppraisalType();
            ap.AppraisalRequest.Recommendation = new Recommendation();
            ap.AppraisalRequest.Appraiser = new Evaluator();
            ap.AppraisalRequest.Reviewer = new Evaluator();
            ap.EmployeeHeader = new EmployeeHeader();
            ap.EmployeeHeader.Department = new Shared.Master.Department();
            ap.EmployeeHeader.Designation = new Shared.Master.Designation();
            ap.EmployeeHeader.Location = new Shared.Master.Location();
            ap.EmployeeHeader.Category = new Shared.Master.Category();
            //ap.DurationwiseFilterSlab.Code = 1;
            ap.AppraisalList = m.AppraisalList;
            ap.AllTabCount = m.AllTabCount;
            ap.Tab1Count = m.Tab1Count;
            ap.Tab2Count = m.Tab2Count;
            ap.Tab3Count = m.Tab3Count;
            ap.Tab4Count = m.Tab4Count;
            ap.MoreTabCount = m.MoreTabCount;
             ap.Filter=   await _commonService.GetFilterDropdowns();
                ViewBag.Tabs = new SelectList(ap.AppraisalTabs, "Code", "Name");
            return View(ap);
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

        public async Task<IActionResult> ApplyFilter(int TabIndex, int SearchId, Filter filter,int AppraisalTypeCode)
        {
            try { 
            Appraisal model = new Appraisal();
            model.Filter = filter;
            model = await _appraisalService.GetAppraisalList(TabIndex, SearchId, AppraisalTypeCode, filter);
            return PartialView("_AppraisalList", model);
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

        public async Task<ActionResult> EmployeeCard(int EmpCode)
        {
            try { 
            Appraisal model = new Appraisal();

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
        public async Task<IActionResult> Create(Guid EmployeeGI)
        {
            try { 
            Appraisal model = new Appraisal();
            //model.IncidentsAction = new IncidentsAction();
            model.EmployeeGI = EmployeeGI;
            //model.EmployeeHeader= await _commonService.GetEmployeeHeader();
            model.EmployeeHeader = await _commonService.GetEmployeeHeader(EmployeeGI);
            model.AppraisalRequest = new AppraisalRequest();
            model.AppraisalRequest.EvaluationPeriod = new EvaluationPeriod();
            model.AppraisalRequest.AppraisalType = new AppraisalType();
            model.AppraisalRequest.Recommendation = new Recommendation();
            model.AppraisalRequest.Appraiser = new Evaluator();
            model.AppraisalRequest.Reviewer = new Evaluator();
            ViewBag.Aev = new SelectList(await _dropdownService.GetAppraisalEvaluatorDropDown(EmployeeGI), "Code", "Name");
            ViewBag.Aep = new SelectList(await _dropdownService.GetAppraisalEvaluationPeriodDropDown(), "Code", "Name");
            ViewBag.Aty = new SelectList(await _dropdownService.GetAppraisalTypeDropDown(), "Code", "Name");
            ViewBag.Apr = new SelectList(await _dropdownService.GetAppraisalRatingDropDown(), "Code", "Name");
            ViewBag.Emp = new SelectList(await _dropdownService.GetEmployeeDropDown(), "Code", "Name");
            ViewBag.Are = new SelectList(await _dropdownService.GetAppraisalRecommendationDropDown(), "Code", "Name");
            //model.IncidentsAction.Incidents = new Incidents();
            //model.IncidentsAction.IncidentLog = new List<IncidentLog>();
            model.AppraisalRequest.AppraisalLog = (List<AppraisalLog>)await _appraisalService.GetAppraisalLogList(EmployeeGI);
            //model.AssetRequest.AssetLog = (List<AssetLog>)await _assetAllocationService.GetAssetLogList(EmployeeGI);
            //ViewBag.Dut = new SelectList(await _dropdownService.GetIncidentDropDown(), "Code", "Name");
            //ViewBag.Ity = new SelectList(await _dropdownService.GetIncidentTypeDropDown(), "Code", "Name");

            return View("~/Areas/DataBank/Views/Appraisal/Create.cshtml", model);
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
        public async Task<IActionResult> Create(Appraisal model)
        {
            try { 
            var response = await _appraisalService.AppraisalSave(model);
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

        public async Task<IActionResult> AppraisalLogDetails(string AppraisalTrnGI, Guid EmployeeGI)
        {
            try { 
            Appraisal model = new Appraisal();
            //model.AppraisalRequest.EvaluationPeriod = new EvaluationPeriod();
            //model.AppraisalRequest.AppraisalType = new AppraisalType();
            //model.AppraisalRequest.Recommendation = new Recommendation();
            //model.AppraisalRequest.Appraiser = new Evaluator();
            //model.AppraisalRequest.Reviewer = new Evaluator();
            model.AppraisalRequest = await _appraisalService.GetAppraisalLogDetails(Guid.Parse(AppraisalTrnGI));
            return PartialView("_AppraisalLogDetails", model.AppraisalRequest);
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
        public async Task<ActionResult> AppraisalTabs(int TabIndex, int SearchId,int AppraisalTypeCode)
        {
            try { 
            Appraisal model = new Appraisal();
            Filter filter = new Filter();
            model = await _appraisalService.GetAppraisalList(TabIndex, SearchId, AppraisalTypeCode, filter);
            model.Tabindex = TabIndex;
            model.SearchId = SearchId;
            model.AppraisalRequest = new AppraisalRequest();
            return View("_AppraisalList", model);
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
        public async Task<JsonResult> GetAppraisalType(int AppraisalTypeCode)
        {
            var TypeDetails = await _appraisalService.GetAppraisalAssesmentMode(AppraisalTypeCode);
            return Json(TypeDetails);
        }
        public async Task<ActionResult> AppraisalDetails(Guid AppraisalGI, Guid EmployeeGI)
        {
            try { 
            Appraisal model = new Appraisal();
            model.AppraisalRequest = new AppraisalRequest();
            model.AppraisalRequest = await _appraisalService.GetAppraisalDetails(AppraisalGI);
            model.EmployeeHeader = await _commonService.GetEmployeeHeader(EmployeeGI);
            //model.AssetRequest.AssetReturnDate = model.AssetRequest.AssetReturnDate == DateTime.Parse("01/Jan/0001 12:00:00 AM") ? null : model.AssetRequest.AssetReturnDate;
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
        public async Task<ActionResult> AppraisalDelete(Guid AppraisalGI)
        {
            try { 
            var response = await _appraisalService.AppraisalDelete(AppraisalGI);
            if (response.Success == true)
            {
                TempData["Message"] = "Deleted Successfully";
                return RedirectToAction("Index", "Appraisal");
            }
            return PartialView("_DisciplinaryIncidentsDetails");
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
      
        public async Task<IActionResult> ExportToExcel(int TabIndex, int SearchId, string TabName, string SearchName,int AppraisalTypeCode)
        {
            try { 
            Appraisal model = new Appraisal();
            Filter filter = new Filter();
            int slNo = 0;
            
            string FilterValues =await _helpers.GetFilterValues(filter);
            model = await _appraisalService.GetAppraisalList(TabIndex,SearchId, AppraisalTypeCode,filter);
            var LoginUser = HttpContext.Session.GetString("LoginUser");
            var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
            EP = await _commonService.GetExcelPassword("Appraisal List(" + TabName + ")", "Excel");
            string currDateTime = await _helpers.GetCurrentDate();
            
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add(TabName+"List");

               



                int count = model.AppraisalList.Count + 5;


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
                wsReportNameHeaderRange.Value = "Appraisal List";

                worksheet.Cell(2, 1).Style.Font.Bold = true;
                worksheet.Cell(2, 1).Value = "Group Company Name";
                worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();

                worksheet.Cell(2, 3).Style.Font.Bold = true;
                worksheet.Cell(2, 3).Value = "Evaluation Period";
                worksheet.Cell(2, 4).Value = SearchName;



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
                worksheet.Cell(3, 6).Value = model.AppraisalList.Count();

                worksheet.Cell(4, 1).Style.Font.Bold = true;
                worksheet.Cell(4, 1).Value = "Filter Applied";
                worksheet.Range("B4:I4").Merge();
                worksheet.Cell("B4").Value = FilterValues;


                var currentRow = 5;
                #region Header
                worksheet.Range("A5:I5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
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
                worksheet.Cell(currentRow, 7).Value = "Evaluation Period";
                worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 8).Value = "Appraisal Type";
                worksheet.Cell(currentRow, 8).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 9).Value = "Rev.Score";
                worksheet.Cell(currentRow, 9).Style.Font.Bold = true;



                #endregion

                #region Body
                foreach (var item in model.AppraisalList)
                {
                    currentRow++;
                    slNo = slNo + 1;
                    worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                   
                    worksheet.Cell(currentRow, 1).Value = slNo;
                    worksheet.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(currentRow, 2).Value = item.EmpId;
                    worksheet.Cell(currentRow, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(currentRow, 3).Value = item.EmployeeName;
                    worksheet.Cell(currentRow, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(currentRow, 4).Value = item.Designation.Name;
                    worksheet.Cell(currentRow, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(currentRow, 5).Value = item.Department.Name;
                    worksheet.Cell(currentRow, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(currentRow, 6).Value = item.Location.Name;
                    worksheet.Cell(currentRow, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(currentRow, 7).Value = item.AppraisalRequest.EvaluationPeriod.Name;
                    worksheet.Cell(currentRow, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(currentRow, 8).Value = item.AppraisalRequest.AppraisalType.Name;
                    worksheet.Cell(currentRow, 9).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(currentRow, 9).Value = item.AppraisalRequest.ReviewerScore;
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
                        "Appraisal List.xlsx"
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
