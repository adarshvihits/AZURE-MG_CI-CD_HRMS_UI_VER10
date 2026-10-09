using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Services.IServices.General;
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
    public class HRPlanningController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly IHRPlannningService _hRPlannningService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        public HRPlanningController(IHRPlannningService hRPlannningService, IDropdownService dropdownService, ICommonService commonService,IHelpers helpers)
        {
            _hRPlannningService = hRPlannningService;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = helpers;
        }
        public async Task<IActionResult> Index()
        {
            try { 
            HRPlanning model = new HRPlanning();
            ViewBag.PlanningName = new SelectList(await _dropdownService.GetHRPlanningDropDown(), "Code", "Name");
               
              var Planning = await _dropdownService.GetHRPlanningDropDown();
                int? firstVal = 0;
                if (Planning != null && Planning.Count>0)
                    firstVal = Planning[0].Code;
               


                ViewBag.region = new SelectList(await _dropdownService.GetRegionDropDown(), "Code", "Name");
               ViewBag.state = new SelectList(await _dropdownService.GetStateDropDown(), "Code", "Name");
                model = await _hRPlannningService.GetCurrentMonthPlanningDetails();
                model.Filter = await _commonService.GetFilterDropdowns();

                if (model.HRPlanningNameMain != null)
                    ViewBag.PlanningCombination1 = new SelectList(await _dropdownService.GetHRPlanningCombination1DropDown(model.HRPlanningNameMain.Code), "Code", "Name");
                else
                    ViewBag.PlanningCombination1 = new SelectList(await _dropdownService.GetHRPlanningCombination1DropDown(firstVal), "Code", "Name");
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

        public async Task<IActionResult> PlanningSummary(int HRPlanningCode,int StateCode, int RegionCode)
        {
            try { 
            HRPlanning model = new HRPlanning();
            var list = await _dropdownService.GetHRPlanningDropDown();
        
            ViewBag.PlanningName = new SelectList(list, "Code", "Name");
            ViewBag.PlanningCombination1 = new SelectList(await _dropdownService.GetHRPlanningCombination1DropDown(HRPlanningCode), "Code", "Name");

            model = await _hRPlannningService.GetPlanningDetails(HRPlanningCode);
                model.HRPlanningNameMain = new HRPlanningDropdwn();
                     model.State = new Shared.DataBank.Generic.Master.State();
                model.Region = new Shared.Master.Region();
                model.HRPlanningNameMain.Code = HRPlanningCode;
                model.State.Code = StateCode;
                model.Region.Code = RegionCode;
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
        public async Task<IActionResult> Create()
        {
            try { 
            fillHRAddNewPlanningCombination1DropDown();
            HRPlanning model = new HRPlanning();
            ViewBag.PlanningName = new SelectList(await _dropdownService.GetHRPlanningDropDown(), "Code", "Name");
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
        [HttpPost]
        public async Task<IActionResult> Save(HRPlanning model)
        {
            try { 
            var response = await _hRPlannningService.HRPlanningSave(model);
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
        public async Task<IActionResult> Delete(HRPlanning model)
        {
            try { 
            var response = await _hRPlannningService.HRPlanningDelete(model.HRPlanningName.Code ,model.Combination1.Code, model.HRPlanningSettingsList[0].Combination2TrnCode);          
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

        public async Task<IActionResult> FillHRPlanningCombination2Grid(int HRPlanningCode, int Combination1TrnCode)
        {
            try { 
            HRPlanning model = new HRPlanning();
            if (Combination1TrnCode != 0)
                model = await _hRPlannningService.GetHRPlanningCombination2GridDetails(HRPlanningCode, Combination1TrnCode);
            
            return PartialView("_Combination2PlanningGrid",model);
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
        public async Task<IActionResult> GetHRPlanningList(int HRPlanningCode, int Combination1TrnCode, int BasedOn, DateTime monthCode, Filter Filter)
        {
            try { 
            HRPlanning model = new HRPlanning();
            if(Combination1TrnCode!=0)
            model = await _hRPlannningService.GetHRPlanningList(HRPlanningCode, Combination1TrnCode,BasedOn, monthCode.ToString("yyyy-MMM-dd"),Filter);
            model.HRPlanningNameMain = new HRPlanningDropdwn();
            model.Combination1Code = Combination1TrnCode;
            model.HRPlanningNameMain.Code = HRPlanningCode;
            if (model.HRPlanningList != null)
                model.ListCount = model.HRPlanningList.Count;
            return PartialView("_HRPlanningList", model);
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

        public async Task<IActionResult> GetHRplanningSummaryChart(int HRPlanningCode, int Combination1TrnCode,string Mode,Filter Filter)
        {
            try { 
            HRPlanning model = new HRPlanning();
            var date = DateTime.Now;
            model = await _hRPlannningService.GetHRPlanningList(HRPlanningCode, Combination1TrnCode, 0, date.ToString("yyyy-MMM-dd"),Filter);

                if (model.HRPlanningList != null)
                {
                    //ViewBag.XAxislabel = model.Combination2Label;
                    if (Mode == "Manpower")
                    {
                        //ViewBag.chartMode = "Manpower";
                        //ViewBag.chartData = JsonConvert.SerializeObject(model.HRPlanningList.Select(m => new { m.PlannedManPower, m.CurrentManPower, m.Combination2Name }));
                        return PartialView("_ManpowerChart", model.HRPlanningList);

                    }
                    else
                    {
                        //ViewBag.chartMode = "Budget";
                        //ViewBag.chartData = JsonConvert.SerializeObject(model.HRPlanningList.Select(m => new { m.PlannedBudget, m.CurrentGP, m.Combination2Name }));
                        return PartialView("_Chart", model.HRPlanningList);
                    }
                }
                    return PartialView("_Chart", model.HRPlanningList);
                
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
        public async Task<IActionResult> GetHRPlanningDetailsForModify(int HRPlanningCode, int Combination1TrnCode, int Combination2TrnCode, string Mode)
        {
            try { 
            HRPlanning model = new HRPlanning();
            model = await _hRPlannningService.GetHRPlanningDetailsForModify(HRPlanningCode, Combination1TrnCode, Combination2TrnCode);
            //model.Combination1 = new HRPlanningDropdwn();
            //model.Combination1.Code = Combination1TrnCode;
            if (Mode=="edit")
             return PartialView("_Modify", model);
            else
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
      



        [HttpGet]
        private async void fillHRAddNewPlanningCombination1DropDown(int HRPlannningCode=0)
        {
            ViewBag.AddNewCombination1 = new SelectList(await _dropdownService.GetHRPlanningAddNewCombination1DropDown(HRPlannningCode), "Code", "Name");

        }
        [HttpGet]
        private async void fillHRPlanningCombination1DropDown(int HRPlannningCode = 0)
        {
            ViewBag.PlanningCombination1 = new SelectList(await _dropdownService.GetHRPlanningCombination1DropDown(HRPlannningCode), "Code", "Name");

        }
        [HttpGet]
        public async Task<JsonResult> GetHRPlanningAddNewCombination1DropDown(int HRPlanningCode)
        {
            var Combination = await _dropdownService.GetHRPlanningAddNewCombination1DropDown(HRPlanningCode);
            return Json(Combination);

        }
        [HttpGet]
        public async Task<JsonResult> GetHRPlanningCombination1DropDown(int HRPlanningCode)
        {
            var Combination = await _dropdownService.GetHRPlanningCombination1DropDown(HRPlanningCode);
            return Json(Combination);

        }
        [HttpGet]
        public async Task<JsonResult> GetPlanningDetails(int HRPlanningCode)
        {
            var PlanningDetails = await _hRPlannningService.GetPlanningDetails(HRPlanningCode);
            return Json(PlanningDetails);

        }
        //public async Task<IActionResult> ExportToExcel(int HRPlanningCode, int Combination1TrnCode, int BasedOn, DateTime monthCode,string planname, string planningPeriod, string depart,string basedon)
        //{
        public async Task<IActionResult> ExportToExcel(int HRPlanningCode, int Combination1TrnCode, string combn1Name,int BasedOn, DateTime monthCode, Filter filter,string HrplanningName)
        {
            try { 
            HRPlanning model = new HRPlanning();
            string basedon;
               

            model = await _hRPlannningService.GetHRPlanningList(HRPlanningCode, Combination1TrnCode, BasedOn, monthCode.ToString("yyyy-MMM-dd"),filter);
            var LoginUser = HttpContext.Session.GetString("LoginUser");
            var CompanyName = HttpContext.Session.GetString("CompanyName");
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("HR Planning List (" + model.HRPlanningNameMain.Name + ")", "Excel");
                int slNo = 0;
            string currDateTime = await _helpers.GetCurrentDate();
            
                string FilterValues = await _helpers.GetFilterValues(filter);
                if (FilterValues == "")
                {
                    FilterValues = "All";
                }
                using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("HR Planning List");

                if (BasedOn == 1)
                {
                    basedon = "Previous Month";
                    worksheet.Cell(3, 3).Style.Font.Bold = true;
                    worksheet.Cell(3, 3).Value = "Month";
                    worksheet.Cell(3, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(3, 4).Style.DateFormat.Format = "MMM-yyyy";
                    worksheet.Cell(3, 4).Value = monthCode;
                }
                else
                {
                    basedon = "Current";
                }




                int count = model.HRPlanningList.Count + 5;


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
                wsReportNameHeaderRange.Value = "HR Planning";

                worksheet.Cell(2, 1).Style.Font.Bold = true;
                worksheet.Cell(2, 1).Value = "Company Name";
                worksheet.Cell(2, 2).Value = CompanyName.ToUpper();
                worksheet.Cell(2, 3).Style.Font.Bold = true;
                worksheet.Cell(2, 3).Value = "Planning Name";
                worksheet.Cell(2, 4).Value = HrplanningName;
                    worksheet.Cell(2, 4).Style.DateFormat.Format = "MMM-yyyy";
                    worksheet.Cell(2, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                worksheet.Cell(2, 5).Style.Font.Bold = true;
                worksheet.Cell(2, 5).Value = "Planning Period";
                worksheet.Cell(2, 6).Value = "( "+model.PlanningPeriod+ " )";
                worksheet.Cell(2, 7).Style.Font.Bold = true;
                worksheet.Cell(2, 7).Value = model.Combination1Label;
                worksheet.Cell(2, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                worksheet.Cell(2, 8).Value = combn1Name;
                worksheet.Cell(3, 1).Style.Font.Bold = true;
                worksheet.Cell(3, 1).Value = "Based On";
                worksheet.Cell(3, 2).Value = basedon;
               
                   
              
                worksheet.Cell(3, 3).Style.Font.Bold = true;
                worksheet.Cell(3, 3).Value = "Login User";
                worksheet.Cell(3, 4).Value = LoginUser;
                worksheet.Cell(3, 4).Style.Font.Bold = true;
                worksheet.Cell(3, 5).Value = "Date & Time";
                worksheet.Cell(3, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                worksheet.Cell(3, 5).Style.DateFormat.Format = "dd-MMM-yyyy   h:mm AM/PM";
                worksheet.Cell(3, 5).Value = currDateTime.ToString();
                worksheet.Cell(3, 5).Style.Font.Bold = true;
                worksheet.Cell(3, 6).Value = "Total Count";
                worksheet.Cell(3, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                worksheet.Cell(3, 7).Value = model.HRPlanningList.Count();
                    worksheet.Cell(4, 1).Style.Font.Bold = true;
                    worksheet.Cell(4, 1).Value = "Filter Applied";
                    var filterapplied = worksheet.Range("B4:K4").Merge();


                    var currentRow = 5;
                #region Header
                worksheet.Cell(currentRow, 1).Value = "Sl.No";
                worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 2).Value =model.Combination2Label;
                worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 3).Value = "Current Manpower";
                worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 4).Value = "Planned Manpower";
                worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 5).Value = "Manpower Variance";
                worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 6).Value = "Current GP";
                worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 7).Value = "Planned Budget";
                worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 8).Value = "Budget Variance";
                worksheet.Cell(currentRow, 8).Style.Font.Bold = true;




                #endregion

                #region Body
                foreach (var item in model.HRPlanningList)
                {
                    currentRow++;
                    slNo = slNo + 1;
                    worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Range("A5:H5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Cell(currentRow, 1).Value = slNo;
                    worksheet.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(currentRow, 2).Value = item.Combination2Name;
                    worksheet.Cell(currentRow, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(currentRow, 3).Value = item.CurrentManPower;
                    worksheet.Cell(currentRow, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(currentRow, 4).Value = item.PlannedManPower;
                    worksheet.Cell(currentRow, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(currentRow, 5).Value = item.ManPowerVariance;
                    worksheet.Cell(currentRow, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(currentRow, 6).Value = item.CurrentGP;
                    worksheet.Cell(currentRow, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(currentRow, 7).Value = item.PlannedBudget;
                    worksheet.Cell(currentRow, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(currentRow, 8).Value = item.BudgetVariance;
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
                        "HR Planning List.xlsx"
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




        [HttpGet]
        public async Task<ActionResult> GetEmployeeDetails(int HRplanCode, int Combination1Code, int Combination2Code)
        {
            try
            {
                HRPlanning model = new HRPlanning();
               
                model = await _hRPlannningService.GetEmployeeList(HRplanCode, Combination1Code, Combination2Code);
                return PartialView("_PopUpList", model);
                //return Json(response);
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
