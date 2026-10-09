using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
    public class BranchGradingController : Controller
    {
        private IDropdownService _dropdownService;
        public readonly IBranchGradingService _Service;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        public BranchGradingController(ICommonService commonService, IDropdownService dropdownService, IBranchGradingService Service, IHelpers helpers)
        {
            _Service = Service;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = helpers;
        }

        public async Task<IActionResult> Index()
        {
            BranchGrading model = new BranchGrading();
            ViewBag.Region = new SelectList(await _dropdownService.GetRegionDropDown(), "Code", "Name");
            ViewBag.Grade = new SelectList(await _dropdownService.GetBudgetGradingGradeDropdown(), "Code", "Name");
            ViewBag.Designation = new SelectList(await _dropdownService.GetBudgetGradingDesignationDropdown(), "Code", "Name");
            var Des = await _dropdownService.GetBudgetGradingDesignationDropdown();           
            if (Des!= null && Des.Count > 0)            
                model = await _Service.GetMainList(Des[0].Code, 0, 0);           
            return View(model);
        }

        public async Task<IActionResult> GetList(int DesignationCode, int RegionCode, int GradeCode)
        {
            BranchGrading model = new BranchGrading();
            ViewBag.Region = new SelectList(await _dropdownService.GetRegionDropDown(), "Code", "Name");
            ViewBag.Grade = new SelectList(await _dropdownService.GetBudgetGradingGradeDropdown(), "Code", "Name");
            ViewBag.Designation = new SelectList(await _dropdownService.GetBudgetGradingDesignationDropdown(), "Code", "Name");
            model = await _Service.GetMainList(DesignationCode, RegionCode, GradeCode);
            return PartialView("_BranchGradingList", model);
        }


        public async Task<IActionResult> GetPopUp(int DesignationCode, int RegionCode, int GradeCode, int LocationCode)
        {
            try
            {
                BranchGrading model = new BranchGrading();

                ViewBag.Grade = new SelectList(await _dropdownService.GetBudgetGradingGradeDropdown(), "Code", "Name");

                model = await _Service.GetBranchGradingPopUp(DesignationCode, RegionCode, GradeCode, LocationCode);

                return PartialView("_ChangeBranchGradePopUp", model);
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
        public async Task<IActionResult> GetGradeList()
        {
            BranchGrading model = new BranchGrading();
            ViewBag.Grade = new SelectList(await _dropdownService.GetBudgetGradingGradeDropdown(), "Code", "Name");
            //model = await _Service.GetGradeList(0);
            return PartialView("_GradeListSlider");

        }
        public async Task<IActionResult> GetPopUpListOnchange(int GradeCode)
        {
            BranchGrading model = new BranchGrading();
            ViewBag.Grade = new SelectList(await _dropdownService.GetBudgetGradingGradeDropdown(), "Code", "Name");
            model = await _Service.GetGradeList(GradeCode);
            return PartialView("_GradingSliderList", model);
        }
        [HttpPost]
        public async Task<IActionResult> SaveBranchgradingList(BranchGrading model)
        {
            try
            {
                var response = await _Service.SaveGradeList(model);
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
        public async Task<IActionResult> SavePopUpDetails(BranchGrading model)
        {
            try
            {

                var response = await _Service.ChangeBranchGrade(model);
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

        //public async Task<IActionResult> GetPlusPopUp(int DesignationCode, int RegionCode, int GradeCode, int LocationCode, string Type)
        //{
        //    try
        //    {
        //        BranchGrading model = new BranchGrading();

        //        model = await _Service.GetPlusPopUpDetails(DesignationCode, RegionCode, GradeCode, LocationCode,Type);

        //        return PartialView("_ChangeBranchGradePopUp", model);
        //    }
        //    catch (UnauthorizedAccessException e)
        //    {

        //        return RedirectToAction("Login", "Account", new { Area = "" });
        //    }
        //    catch (Exception e)
        //    {
        //        return RedirectToAction("Error", "Account", new { Area = "" });
        //    }

        //}


        public async Task<IActionResult> GetGPPopUp(int DesignationCode, int RegionCode, int GradeCode, int LocationCode, string Type)
        {
            try
            {
                BranchGrading model = new BranchGrading();

                model = await _Service.GetGpPopUpDetails(DesignationCode, RegionCode, GradeCode, LocationCode, Type);

                return PartialView("_GpEmpDetailsPopUp", model);
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

        public async Task<IActionResult> ExportToExcel(int DesignationCode, int RegionCode, int GradeCode,string DesName,string RegName,string GradeName)
        {
            try
            {
                BranchGrading model = new BranchGrading();

                string LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                int slNo = 0;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Branch Grading", "Excel");
                //Filter filter = new Filter();
                //filter = JsonConvert.DeserializeObject<Filter>(TempData["FilterModel"].ToString());


                model = await _Service.GetMainList(DesignationCode, RegionCode, GradeCode);

                int no = model.MainList.Count();
                string currDateTime = await _helpers.GetCurrentDate();
                
                using (var workbook = new XLWorkbook())
                {

                    var worksheet = workbook.Worksheets.Add("Branch Grading List");


                    int count = model.MainList.Count + 6;


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
                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                    wsReportNameHeaderRange.Value = "Branch Grading List";

                    worksheet.Cell(2, 1).Style.Font.Bold = true;
                    worksheet.Cell(2, 1).Value = "Company Name";
                    worksheet.Cell(2, 2).Value = CompanyName.ToUpper();

                    worksheet.Cell(3, 1).Style.Font.Bold = true;
                    worksheet.Cell(3, 1).Value = "Designation Name";
                    worksheet.Cell(3, 2).Value = DesName;

                    worksheet.Cell(3, 3).Style.Font.Bold = true;
                    worksheet.Cell(3, 3).Value = "Region Name";
                    worksheet.Cell(3, 4).Value = RegName;
                    worksheet.Cell(3, 5).Style.Font.Bold = true;
                    worksheet.Cell(3, 5).Value = "Grade Name";
                    worksheet.Cell(3, 6).Value = GradeName;

                  

                    worksheet.Cell(4, 1).Style.Font.Bold = true;
                    worksheet.Cell(4, 1).Value = "Login User";
                    worksheet.Cell(4, 2).Value = LoginUser;
                    worksheet.Cell(4, 3).Style.Font.Bold = true;

                    worksheet.Cell(4, 3).Value = "Date & Time";
                    worksheet.Cell(4, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(4, 3).Style.DateFormat.Format = "dd-MMM-yyyy   h:mm AM/PM";
                    worksheet.Cell(4, 4).Value = currDateTime.ToString();


                    worksheet.Cell(5, 1).Style.Font.Bold = true;
                    worksheet.Cell(5, 1).Value = "Total Count";
                    worksheet.Cell(5, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(5, 2).Value = model.MainList.Count();


                 


                    var currentRow = 6;
                    #region Header
                    worksheet.Range("A6:K6").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Cell(currentRow, 1).Value = "Br.Code";
                    worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 2).Value = "Branch Name";
                    worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 3).Value = "Grade";
                    worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 4).Value = "Region";
                    worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 5).Value = "Std No.";
                    worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 6).Value = "Cur. No.";
                    worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 7).Value = "Var. Coun";
                    worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 8).Value = "Std. GP";
                    worksheet.Cell(currentRow, 8).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 9).Value = "GP+";
                    worksheet.Cell(currentRow, 9).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 10).Value = "GP-";
                    worksheet.Cell(currentRow, 10).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 11).Value = "MRF";
                    worksheet.Cell(currentRow, 11).Style.Font.Bold = true;
                   

                    #endregion

                    #region Body

                    foreach (var item in model.MainList)
                    {
                        currentRow++;
                        slNo = slNo + 1;

                        worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 1).Value = item.Location.UserCode;
                        worksheet.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 2).Value = item.Location.Name;

                        worksheet.Cell(currentRow, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 3).Value = item.Grade.Name;
                        worksheet.Cell(currentRow, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 4).Value = item.Region.Name;
                        worksheet.Cell(currentRow, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 5).Value = item.StdNumbers;
                        worksheet.Cell(currentRow, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 6).Value = item.CurrentNumbers;

                        worksheet.Cell(currentRow, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 7).Value = item.VarianceCount;
                        worksheet.Cell(currentRow, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 8).Value = item.StdGP;
                        worksheet.Cell(currentRow, 9).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 9).Value = item.GP_PLUS;

                        worksheet.Cell(currentRow, 10).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 10).Value = item.GP_NEG;
                        worksheet.Cell(currentRow, 11).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 11).Value = item.MRF;
                        

                        worksheet.Columns(1, 12).Style.Alignment.WrapText = true;
                        worksheet.Rows().Style.Alignment.WrapText = true;
                    }
                    #endregion
                    worksheet.Columns().AdjustToContents();
                    worksheet.Rows().AdjustToContents();
                    //worksheet.Protect("123");
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
                            "Branch Grading List.xlsx"
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
