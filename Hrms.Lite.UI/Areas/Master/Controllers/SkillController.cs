using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.Master;
using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.General;
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
    public class SkillController : Controller
    {
        private readonly ISkillService _service;
        private readonly IDropdownService _dropdownService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        public SkillController(ISkillService service, IDropdownService dropdownService,ICommonService commonService, IHelpers helpers)
        {
            _service = service;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = helpers;
        }
        public async Task<IActionResult> Index()
        {
            try { 
            return View(await _service.GetList("ACTIVE"));
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
        public async Task<ActionResult> GetSkillList(string TabIndex)
        {
            try { 
            if (TabIndex == "APPROVAL_PENDING")
                return PartialView("_PendingList", await _service.GetList(TabIndex));
            else
                return PartialView("_SkillList", await _service.GetList(TabIndex));
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
        public async Task<IActionResult> CreateSkill()
        {
            try { 
            Skill model = new Skill();
            model.Mode = "CREATE";
            model.Active = true;
            ViewBag.SkillType= new SelectList(await _dropdownService.GetSkillType_Dropdown(), "Code", "Name");
            return PartialView("_CreateOrEdit", model);
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
        public async Task<ActionResult> Create(Skill input)
        {
            try { 
            var response = await _service.Create(input);
           
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






        public async Task<ActionResult> GetActiveInactiveValidation(string name, int code)
        {
            try
            {
                var response = await _service.GetMasterInactiveValidation(name, code);

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
        public async Task<ActionResult> Approve(Guid SkillGI, Guid LogGI)
        {
            try { 
            Skill model = await _service.GetSkillDetails(SkillGI, LogGI, "APPROVAL");
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
        [HttpPost]
        public async Task<ActionResult> Approve(Skill model)
        {
            try { 
            model.ApproveMode = "Approve";
            var response = await _service.ApproveOrReject(model);
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
        public async Task<ActionResult> Reject(Skill model)
        {
            try { 
            model.ApproveMode = "Reject";
            var response = await _service.ApproveOrReject(model);
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
        public async Task<ActionResult> Edit(Guid SkillGI, Guid LogGI)
        {
            try { 

            Skill model = await _service.GetSkillDetails(SkillGI, LogGI, "MODIFY");
            model.Mode = "MODIFY";
            ViewBag.SkillType = new SelectList(await _dropdownService.GetSkillType_Dropdown(), "Code", "Name");
            return PartialView("_CreateOrEdit", model);
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
        public async Task<ActionResult> Delete(Guid SkillGI, Guid LogGI)
        {
            try { 
            Skill model = await _service.GetSkillDetails(SkillGI, LogGI, "DELETE");
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
        public async Task<ActionResult> Delete(Skill model)
        {

            //if (response.Success == true)
            //{
            //    TempData["Message"] = response.Message;
            //    return RedirectToAction("Index");
            //}
            //else
            //{
            //    TempData["Message"] = response.Message;
            //    return RedirectToAction("Index");
            //}

            try { 
            var response = await _service.Delete(model);
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
            Skill model = new Skill();
            model = await _service.GetList(TabIndex);
            int slno = 0;
            string currDateTime = await _helpers.GetCurrentDate();
            
            var LoginUser = HttpContext.Session.GetString("LoginUser");
            var CompanyName = HttpContext.Session.GetString("CompanyName");
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Skill (" + TabIndex + ")", "Excel");
                if (TabIndex == "APPROVAL_PENDING")
            {

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("PendingList");
                    int PendingCountnum = model.PendingCount + 6;


                    worksheet.Range("A1:D" + PendingCountnum).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:D" + PendingCountnum).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:D" + PendingCountnum).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:D" + PendingCountnum).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:D" + PendingCountnum).Style.Border.LeftBorder = XLBorderStyleValues.Thin;



                    var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 4)));
                    wsReportNameHeaderRange.Style.Font.Bold = true;
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Merge();


                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Value = "Skill " + "(" + TabIndex + ")";
                    worksheet.Range("A6:D6").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
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
                    worksheet.Cell(currentRow, 2).Value = "Name";
                    worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 3).Value = "ShortName";
                        worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 4).Value = "Entry Type";

                        worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 5).Value = "Skill Type";




                    #endregion

                    #region Body
                    foreach (var item in model.SkillList)
                    {
                        currentRow++;
                        slno = slno + 1;
                        worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 1).Value = slno;
                        worksheet.Cell(currentRow, 2).Value = item.Name;
                        worksheet.Cell(currentRow, 3).Value = item.ShortName;
                            worksheet.Cell(currentRow, 4).Value = item.SkillType.Name;
                            worksheet.Cell(currentRow, 5).Value = item.Mode;
                        worksheet.Columns(1, 5).Style.Alignment.WrapText = true;


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
                            "Skill Pending List.xlsx"
                            );
                    }
                }



            }
            else
            {
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Skill List");
                    int count = model.SkillList.Count + 6;


                    worksheet.Range("A1:C" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:C" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:C" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:C" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:C" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 3)));
                    wsReportNameHeaderRange.Style.Font.Bold = true;
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Merge();
                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                    wsReportNameHeaderRange.Value = "Skill" + "(" + TabIndex + ")";
                    worksheet.Range("A6:C6").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
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
                    worksheet.Cell(5, 2).Value = model.SkillList.Count;

                    var currentRow = 6;

                    #region Header
                    worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 1).Value = "Sl.No";
                    worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 2).Value = "Name";
                    worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 3).Value = "Short Name";
                        worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 4).Value = "Skill Type";
                        #endregion

                        #region Body
                        foreach (var item in model.SkillList)
                    {

                        currentRow++;
                        slno = slno + 1;
                        worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 1).Value = slno;
                        worksheet.Cell(currentRow, 2).Value = item.Name;
                        worksheet.Cell(currentRow, 3).Value = item.ShortName;
                            worksheet.Cell(currentRow, 4).Value = item.SkillType.Name;
                            worksheet.Columns(1, 5).Style.Alignment.WrapText = true;


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
                            "Skill master List.xlsx"
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
