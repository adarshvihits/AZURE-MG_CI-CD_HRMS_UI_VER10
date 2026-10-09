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
    public class TrainingController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly ITrainingService _trainingService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        public static class Global
        {
            public static Filter GlobalFilter;
        }
        public TrainingController(ITrainingService TrainingService, IDropdownService dropdownService, ICommonService commonService, IHelpers helpers)
        {
            _trainingService = TrainingService;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = helpers;
        }
        public async Task<IActionResult> Index()
        {
            try { 
            Training model = new Training();
            Filter filter = new Filter();
            model = await _trainingService.GetTrainingList(0, 1, null,filter);
            model.EmployeeHeader = new EmployeeHeader();
            model.EmployeeHeader.Department = new Department();
            model.EmployeeHeader.Designation = new Designation();
            model.EmployeeHeader.Location = new Location();
            model.EmployeeHeader.Category = new Category();
            model.TrainingRequest = new TrainingRequest();
            model.TrainingRequest.Mode = new TrainingMode();
            model.TrainingListTabs = (List<TrainingListTabs>)await _trainingService.GetTrainingTabs();
            ViewBag.Filter = new SelectList(await _commonService.GetDurationWiseFilterSlabDropDown(), "Code", "Name");
            ViewBag.Emp = new SelectList(await _dropdownService.GetEmployeeDropDown(), "Code", "Name");
            ViewBag.Training = new SelectList(await _dropdownService.GetTrainingDropDown(), "Code", "Name");
            model.FilterList = new AssetAllocationFilterCombo();
            model.FilterList.Code = 1;
            await fillfilterDropdown();
            ViewBag.Tabs = new SelectList(model.TrainingListTabs, "Code", "Name");
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

        public async Task<IActionResult> ApplyFilter(int TabIndex, int SearchId, Filter filter, int TrainingTabCode)
        {
            try { 
            Training model = new Training();
                Global.GlobalFilter = filter;
            model = await _trainingService.GetTrainingList(TabIndex, SearchId, TrainingTabCode, filter);       
            return PartialView("_TrainingList", model);
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

        public async Task<ActionResult> EmployeeCard(int EmpCode)
        {
            try { 
            Training model = new Training();

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
        //public async Task<IActionResult> GetTrainingList(int TabIndex,int SearchId)
        //{
        //    Training model = new Training();
        //    Filter filter = new Filter();
        //    model = await _trainingService.GetTrainingList(TabIndex, SearchId, filter);
        //    model.EmployeeHeader = new EmployeeHeader();
        //    model.EmployeeHeader.Department = new Department();
        //    model.EmployeeHeader.Designation = new Designation();
        //    model.EmployeeHeader.Location = new Location();
        //    model.EmployeeHeader.Category = new Category();
        //    model.TrainingRequest = new TrainingRequest();
        //    model.TrainingRequest.Mode = new TrainingMode();
        //    return PartialView("_TrainingList", model);
        //}
        public async Task<IActionResult> GetTrainingLogList(Guid EmployeeGI,int SearchId)
        {
            try { 
            Training model = new Training();
            model.TrainingRequest = new TrainingRequest();
            model.TrainingRequest.FilterLog = new AssetAllocationFilterCombo();
            model.TrainingRequest.TrainingLog = (List<TrainingLog>)await _trainingService.GetTrainingLogList(EmployeeGI, SearchId);
            model.TrainingRequest.FilterLog.Code = SearchId;
            ViewBag.LogFilter = new SelectList(await _dropdownService.GetTrainingLogSearchDropDown(), "Code", "Name");
            for (int i = 0; i < model.TrainingRequest.TrainingLog.Count; i++)
            {
                model.TrainingRequest.TrainingLog[i].TrainingRequest.Duration = decimal.Round(model.TrainingRequest.TrainingLog[i].TrainingRequest.Duration, 0, MidpointRounding.AwayFromZero);
            }
            return PartialView("_TrainingLog", model);
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
            Training model = new Training();
            model.TrainingRequest = new TrainingRequest();
            model.EmployeeHeader = await _commonService.GetEmployeeHeader(EmployeeGI);           
            model.TrainingRequest.TrainingLog = (List<TrainingLog>)await _trainingService.GetTrainingLogList(EmployeeGI, 1);
            model.EmployeeGI = EmployeeGI;
            ViewBag.TrainingName = new SelectList(await _dropdownService.GetTrainingDropDown(), "Code", "Name");
            ViewBag.Mode = new SelectList(await _dropdownService.GetTrainingModeDropDown(), "Code", "Name");
            ViewBag.Score = new SelectList(await _dropdownService.GetPostAssessmentScoreDropDown(), "Code", "Name");
            ViewBag.LogFilter = new SelectList(await _dropdownService.GetTrainingLogSearchDropDown(), "Code", "Name");
            model.TrainingRequest.FilterLog = new AssetAllocationFilterCombo();
            model.TrainingRequest.FilterLog.Code = 1;           
            for (int i = 0; i < model.TrainingRequest.TrainingLog.Count; i++)
            {
                model.TrainingRequest.TrainingLog[i].TrainingRequest.Duration = decimal.Round(model.TrainingRequest.TrainingLog[i].TrainingRequest.Duration, 0, MidpointRounding.AwayFromZero);
            }
            return View("~/Areas/DataBank/Views/Training/Create.cshtml", model);
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
        public async Task<ActionResult> TrainingDetails(Guid EmployeeTrainingGI, Guid EmployeeGI)
        {
            try { 
            Training model = new Training();
            model = await _trainingService.GetTrainingDetails(EmployeeTrainingGI);
            model.EmployeeHeader = new EmployeeHeader();
            model.EmployeeHeader = await _commonService.GetEmployeeHeader(EmployeeGI);
            //ViewBag.ReturnCondition = new SelectList(await _dropdownService.GetAssetReturnConditionDropDown(), "Code", "Name");
            model.TrainingRequest.Duration = decimal.Round(model.TrainingRequest.Duration, 0, MidpointRounding.AwayFromZero);

            return PartialView("_TrainingDetails",model);
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
        public async Task<ActionResult> TrainingDelete(Guid EmployeeTrainingGI)
        {
            try { 
            Training model = new Training();
            model.EmployeeTrainingGI = EmployeeTrainingGI;
            var response = await _trainingService.TrainingDetailsDelete(EmployeeTrainingGI);
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
            //if (response.Success == true)
            //{
            //    TempData["Message"] = "Deleted Successfully";
            //    return RedirectToAction("Index", "Training");
            //}
            //return PartialView("_TrainingDetails", model);
        }
        #region POST
        [HttpPost]
        public async Task<ActionResult> Create(Training model)
        {
            try { 
            if (model.TrainingRequest.PostAssessmentScore == 0)
            {
                model.TrainingRequest.PostAssessmentScore = (decimal)model.TrainingRequest.TrainingAssessmentMode.Code;
            }
            var response = await _trainingService.TrainingSave(model);
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
            //if (response.Success == true)
            //{
            //    TempData["Message"] = "Saved Successfully";
            //    return RedirectToAction("Index", "Training");
            //}
            //ViewBag.TrainingName = new SelectList(await _dropdownService.GetTrainingDropDown(), "Code", "Name");
            //ViewBag.Mode = new SelectList(await _dropdownService.GetTrainingModeDropDown(), "Code", "Name");
            //ViewBag.Score = new SelectList(await _dropdownService.GetPostAssessmentScoreDropDown(), "Code", "Name");
            //return View("~/Areas/DataBank/Views/Training/Create.cshtml", model);
        }
        #endregion
        #region GET
        [HttpGet]
        public async Task<JsonResult> GetTrainingType(int TrainingCode)
        {
            var TypeDetails = await _trainingService.GetTrainingTypeDetails(TrainingCode);
            MasterBase model = new MasterBase();
            model.Name = TypeDetails.TrainingRequest.Type;
            model.Code = TypeDetails.TrainingRequest.TrainingAssessmentMode.Code;
            return Json(model);
        }
        #endregion
        [HttpPost]
        public JsonResult ExportExcel(Filter filter)
        {
            TempData["FilterModel"] = JsonConvert.SerializeObject(filter);
            return Json(true);
        }        
        public async Task<IActionResult> ExportToExcel(int TabIndex, int SearchId,string TabName, int TrainingTabCode)
        {
            try { 
            Training model = new Training();
            Filter filter = new Filter();
              
                //string password = "123";
                //bool needPassword = true;
                ExcelPassword EP = new ExcelPassword();
            EP = await _commonService.GetExcelPassword("Training (" + TabName + ")","Excel");
            //var abcd = TempData["FilterModel"];
        
                if (Global.GlobalFilter == null)
                    Global.GlobalFilter = new Filter();
                string FilterValues = await _helpers.GetFilterValues(filter);
            if(FilterValues == "")
            {
                FilterValues = "All";
            }
            
            model = await _trainingService.GetTrainingList(TabIndex, SearchId, TrainingTabCode, Global.GlobalFilter);
            var LoginUser = HttpContext.Session.GetString("LoginUser");
            var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                int slNo = 0;
           
            string currDateTime = await _helpers.GetCurrentDate();
            

            using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Training List");

                int count = model.TrainingList.Count + 5;


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
                wsReportNameHeaderRange.Value = "Training (" + TabName +")";

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
                worksheet.Cell(3, 6).Value = model.TrainingList.Count;
                worksheet.Cell(4, 1).Style.Font.Bold = true;
                worksheet.Cell(4, 1).Value = "Filter Applied";
                worksheet.Range("B4:H4").Merge();
                worksheet.Cell("B4").Value = FilterValues;
              
                //worksheet.Row(3).AdjustToContents();
                var currentRow = 5;
                #region Header
                worksheet.Columns("A", "H").AdjustToContents();
                worksheet.Rows().AdjustToContents();

                worksheet.Cell(currentRow, 1).Value = "Sl.No";
                 worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 2).Value = "ID";
                worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 3).Value = "EmployeeName";
                worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 4).Value = "Designation";
                worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 5).Value = "Department";
                worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 6).Value = "Location";
                worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 7).Value = "Training Name";
                worksheet.Cell(currentRow, 8).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 8).Value = "Date";
               
                

                #endregion

               
               
                #region Body
                foreach (var item in model.TrainingList)
                    {
                        currentRow++;
                        slNo = slNo + 1;
                        worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Range("A5:H5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                        worksheet.Cell(currentRow, 1).Value = slNo;
                        worksheet.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 2).Value = item.EmployeeID;
                        worksheet.Cell(currentRow, 3).Value = item.EmployeeName;
                        worksheet.Cell(currentRow, 4).Value = item.Designation.Name;
                        worksheet.Cell(currentRow, 5).Value = item.Department.Name;
                        worksheet.Cell(currentRow, 6).Value = item.Location.Name;
                        worksheet.Cell(currentRow, 7).Value = item.TrainingName;
                        worksheet.Cell(currentRow, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 8).Style.DateFormat.Format = "dd-MMM-yyyy";
                        worksheet.Cell(currentRow, 8).Value = item.AttendedDate.ToString();

                   
                    worksheet.Columns(1, 8).Style.Alignment.WrapText = true;
                   

                }
                #endregion
             
                worksheet.Columns().AdjustToContents();
               
                
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                     if(EP.NeedPassword)
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
                            "Training List.xlsx"
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
