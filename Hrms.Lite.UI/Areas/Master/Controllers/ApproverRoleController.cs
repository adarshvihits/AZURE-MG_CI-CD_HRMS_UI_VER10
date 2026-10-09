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
    public class ApproverRoleController : Controller
    {
        private readonly IApproverRoleService _service;
        private readonly ICommonService _commonService;
        private readonly IDropdownService _dropdownService;
        private readonly IHelpers _helpers;
        public ApproverRoleController(IApproverRoleService service, ICommonService commonService, IDropdownService dropdownService, IHelpers helpers)
        {
            _service = service;
            _commonService = commonService;
            _dropdownService = dropdownService;
            _helpers = helpers;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                ViewBag.Country = new SelectList(await _dropdownService.GetCountryDropDown(), "Code", "Name");
                //ViewBag.ClassificationApplicability = new SelectList(await _dropdownService.GetClassificationApplicabilityDropdown(), "Code", "Name");
                return View(await _service.GetList("ACTIVE",0,0));
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
        public async Task<ActionResult> GetApproverRoleList(string TabIndex, int ClassificationType, int country)
        {
            try
            {

                return PartialView("_ApproverRoleList", await _service.GetList(TabIndex,  ClassificationType,  country));
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
        public async Task<JsonResult> GetApplicabilityDropdown(int? id,int? Country)
        {
            var data = await _service.GetApplicabilityDropdown(id, Country);
            return Json(new
            {
                code = data.ApplicabilityCode,
                name = data.ApplicabilityName,
                dropdown = data.DropdownData
            });
        }
        [HttpGet]
        public async Task<JsonResult> GetCountryVsLocationDropDown(int id)
        {
            var data = await _dropdownService.GetCountryVsLocationDropDown(id);
            var result = data.Select(x => new { value = x.Code, text = x.Name });
            return Json(result);
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


        public async Task<IActionResult> Create()
        {
            try
            {
                ApproverRole model = new ApproverRole();
                model.Mode = "CREATE";
                model.Active = true;
                ViewBag.Role = new SelectList(await _dropdownService.GetRoleDropdown(), "Code", "Name");
                ViewBag.Applicability = new SelectList(Enumerable.Empty<Dropdown>(), "Code", "Name");
                ViewBag.Section = new SelectList(await _dropdownService.GetSectionDropdown(), "Code", "Name");
                ViewBag.Location = new SelectList(await _dropdownService.GetLocationDropdown(), "Code", "Name");
                ViewBag.Country = new SelectList(await _dropdownService.GetCountryDropDown(), "Code", "Name");
                ViewBag.Department = new SelectList(await _dropdownService.GetDepartmentDropdown(), "Code", "Name");
                ViewBag.Authority = new SelectList(await _dropdownService.GetEmployeeDropDown(), "Code", "Name");
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
        public async Task<ActionResult> Edit(int AuthorityEntityCode)
        {

            try
            {
                ApproverRole model = await _service.GetApproverRoleDetails(AuthorityEntityCode, "MODIFY");
                ViewBag.Role = new SelectList(await _dropdownService.GetRoleDropdown(), "Code", "Name");
                ViewBag.Applicability = new SelectList(await _service.GetEditApplicabilityDropdown(model.Role.Code??null,model.Country.Code??null), "Code", "Name");
                //ViewBag.Location = new SelectList(await _dropdownService.GetLocationDropdown(), "Code", "Name");
                //ViewBag.Location = new SelectList(await _dropdownService.GetCountryVsLocationDropDown(model.Country.Code), "Code", "Name");
                ViewBag.Country = new SelectList(await _dropdownService.GetCountryDropDown(), "Code", "Name");
                ViewBag.Department = new SelectList(await _dropdownService.GetDepartmentDropdown(), "Code", "Name");
                ViewBag.Authority = new SelectList(await _dropdownService.GetEmployeeDropDown(), "Code", "Name");
                model.Mode = "MODIFY";
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
        public async Task<ActionResult> Save(ApproverRole input)
        {
            try
            {
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
        public async Task<ActionResult> View(int AuthorityEntityCode)
        {
            try
            {
                ApproverRole model = await _service.GetApproverRoleDetails(AuthorityEntityCode, "DELETE");
                return PartialView("_View", model);
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

        public async Task<ActionResult> Delete(int AuthorityEntityCode)
        {
            try
            {
                ApproverRole model = await _service.GetApproverRoleDetails(AuthorityEntityCode, "DELETE");
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
        public async Task<ActionResult> Delete(ApproverRole model)
        {
            try
            {
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
        public async Task<IActionResult> ExportToExcel(string TabIndex,int ClassificationType,int country)
        {
            try
            {
                ApproverRole model = new ApproverRole();
                model = await _service.GetList(TabIndex, ClassificationType, country);
                int slno = 0;
                string currDateTime = await _helpers.GetCurrentDate();
                
                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Approver Role (" + TabIndex + ")", "Excel");



                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add(TabIndex + " List");
                    int count = model.ApproverRoleList.Count + 6;


                    worksheet.Range("A1:C" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:C" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:C" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:C" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:C" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 2)));
                    wsReportNameHeaderRange.Style.Font.Bold = true;
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Merge();
                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                    wsReportNameHeaderRange.Value = "Job Group" + "(" + TabIndex + ")";
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
                    worksheet.Cell(5, 2).Value = model.ApproverRoleList.Count;

                    var currentRow = 6;

                    #region Header
                    worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 1).Value = "Sl.No";
                    worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 2).Value = "Name";
                    worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 3).Value = "Short Name";
                    #endregion

                    #region Body
                    foreach (var item in model.ApproverRoleList)
                    {
                        currentRow++;
                        slno = slno + 1;
                        worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 1).Value = slno;
                        worksheet.Cell(currentRow, 2).Value = item.Name;
                        worksheet.Cell(currentRow, 3).Value = item.ShortName;
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
                            book.Protect(EP.Password, false, false);
                            book.SaveToStream(stream);
                        }
                        var content = stream.ToArray();
                        return File(
                            content,
                            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                            "Approver Role List.xlsx"
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
