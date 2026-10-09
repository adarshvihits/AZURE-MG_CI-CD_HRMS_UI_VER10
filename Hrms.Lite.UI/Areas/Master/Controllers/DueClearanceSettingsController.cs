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
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.Master.Controllers
{
    [Area("Master")]
    [SessionAuthorize]
    public class DueClearanceSettingsController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly IDueClearanceSettingsService _service;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        public DueClearanceSettingsController(IDueClearanceSettingsService Service, IDropdownService dropdownService, ICommonService commonService, IHelpers Helpers)
        {
            _service = Service;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = Helpers;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                DueClearanceSettings model = new DueClearanceSettings();
                model= await  _service.GetDueClearanceSettingsList();
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
        public  async Task<ActionResult> Create()
        {
            try
            {
                DueClearanceSettings model = new DueClearanceSettings();
                ViewBag.Dept = new SelectList(await _dropdownService.GetDepartmentDropdown(), "Code", "Name");
                ViewBag.ResponsiblePerson = new SelectList(await _dropdownService.GetDueClearanceResponsiblePersonDropdown(0), "Code", "Name");
                model.Mode = "CREATE";
                return PartialView("~/Areas/Master/Views/DueClearanceSettings/_AddOrEditSlider.cshtml", model);
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
        public async Task<IActionResult> GetDueClearanceSettingsList()
        {
            DueClearanceSettings model = new DueClearanceSettings();
            model = await _service.GetDueClearanceSettingsList();
            return PartialView("_List",model);
        }
        public async Task<ActionResult> Edit(Guid ParameterGI,string MODE)
        {
            try
            {
                DueClearanceSettings model = await _service.GetDueClearanceSettingsDetails(ParameterGI);
                ViewBag.Dept = new SelectList(await _dropdownService.GetDepartmentDropdown(), "Code", "Name");
                ViewBag.ResponsiblePerson = new SelectList(await _dropdownService.GetDueClearanceResponsiblePersonDropdown(model.Department.Code), "Code", "Name");
                model.Mode = MODE;
                return PartialView("_AddOrEditSlider", model);
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
        public async Task<JsonResult> GetResponsiblePerson(int id)
        {
            var ResponsiblePerson = new SelectList(await _dropdownService.GetDueClearanceResponsiblePersonDropdown(id), "Code", "Name");
            return Json(ResponsiblePerson);
        }
        [HttpPost]

        public async Task<ActionResult> Save(DueClearanceSettings model)
        {
            try
            {
                var response = await _service.Create(model);

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
        public async Task<ActionResult> Delete(string Mode,Guid ParameterGI)
        {
            try
            {
                DueClearanceSettings model = await _service.GetDueClearanceSettingsDetails(ParameterGI);
                model.Mode = Mode;
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
        public async Task<ActionResult> Delete(DueClearanceSettings model)
        {
            try
            {
                var response = await _service.Delete(model.ParameterGI);
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
        public async Task<IActionResult> ExportToExcel()
        {
            DueClearanceSettings model = new DueClearanceSettings();
            model = await _service.GetDueClearanceSettingsList();
            ExcelPassword EP = new ExcelPassword();
            EP = await _commonService.GetExcelPassword("Dues Clearance Settings", "Excel");
            int slNo = 0;
            string currDateTime = await _helpers.GetCurrentDate();
            
            var LoginUser = HttpContext.Session.GetString("LoginUser");
            var CompanyName = HttpContext.Session.GetString("CompanyName");


            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Dues Clearance Settings");
                int count = model.List.Count + 5;


                worksheet.Range("A1:E" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1:E" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1:E" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1:E" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1:E" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;

                var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 4)));
                wsReportNameHeaderRange.Style.Font.Bold = true;
                wsReportNameHeaderRange.Style.Font.FontSize = 15;
                wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                wsReportNameHeaderRange.Merge();
                wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191); ;
                wsReportNameHeaderRange.Value = "Dues Clearance Settings";

                worksheet.Cell(2, 1).Style.Font.Bold = true;
                worksheet.Cell(2, 1).Value = "Company Name";
                worksheet.Cell(2, 2).Value = CompanyName.ToUpper();
                worksheet.Cell(3, 1).Style.Font.Bold = true;
                worksheet.Cell(3, 1).Value = "Login User";
                worksheet.Cell(3, 2).Value = LoginUser;
                worksheet.Cell(3, 3).Style.Font.Bold = true;
                worksheet.Cell(3, 3).Value = "Date & Time";
                worksheet.Cell(3, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                worksheet.Cell(3, 4).Style.DateFormat.Format = "dd-MMM-yyyy   h:mm AM/PM";
                worksheet.Cell(3, 4).Value = currDateTime.ToString();
                worksheet.Cell(2, 3).Style.Font.Bold = true;
                worksheet.Cell(2, 3).Value = "Total Count";
                worksheet.Cell(2, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                worksheet.Cell(2, 4).Value = model.List.Count;
                worksheet.Range("B4:E4").Merge();





                var currentRow = 5;
                #region Header
                worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 1).Value = "Sl.No";
                worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 2).Value = "Parameter Name";
                worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 3).Value = "Department";
                worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 4).Value = "Responsible Person";
                worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 5).Value = "Default Settings";
                #endregion

                #region Body
                foreach (var item in model.List)
                {
                    currentRow++;
                    slNo = slNo + 1;
                    worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Range("A5:E5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Cell(currentRow, 1).Value = slNo;
                    worksheet.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(currentRow, 2).Value = item.ParameterName;
                    worksheet.Cell(currentRow, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(currentRow, 3).Value = item.Department.Name;
                    worksheet.Cell(currentRow, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(currentRow, 4).Style.Alignment.WrapText = true;
                    worksheet.Cell(currentRow, 4).Value = item.ResponsiblePerson.Name;
                    worksheet.Cell(currentRow, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(currentRow, 5).Value = (item.DefaultSettings == true) ? "Yes" : "No";

                    worksheet.Columns(1, 5).Style.Alignment.WrapText = true;
                    worksheet.Rows().Style.Alignment.WrapText = true;
                }
                #endregion
                worksheet.Columns().AdjustToContents();
                worksheet.Rows().AdjustToContents();
                //worksheet.Column(4).Style.Alignment.WrapText = true;
                //worksheet.Column(4).Style.Alignment.Vertical = XLAlignmentVerticalValues.Top;
                //worksheet.Column(4).Width = 100;
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    if (EP.NeedPassword)
                    {
                        Workbook book = new Workbook();
                        book.LoadFromStream(stream);
                        Worksheet sheet = book.Worksheets[0];
                        sheet.SetColumnWidth(4, 80);
                        for (int i = 5; i <= currentRow; i++)
                        { 
                            sheet.AutoFitRow(i);
                            //sheet.SetRowHeight(i, 20);
                        }
                        //Protect Workbook
                        book.Protect(EP.Password,false,false);
                        book.SaveToStream(stream);
                    }
                    var content = stream.ToArray();
                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "DueClearance Settings.xlsx"
                        );
                }
            }

        }
    }
}
