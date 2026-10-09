using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.Master;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using Spire.Xls;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Data;
using Hrms.Lite.Shared.Account;

namespace Hrms.Lite.UI.Areas.Master.Controllers
{
    [Area("Master")]
    [SessionAuthorize]
    public class RoleMatrixController : Controller
    {
        public readonly IRoleMatrixService _service;
        public readonly ICommonService _commonService;
        private readonly IDropdownService _dropdownService;
        private readonly IHelpers _helpers;
        public static class Global
        {
            public static Filter GlobalFilter;
        }
        public RoleMatrixController(IRoleMatrixService service, ICommonService commonService, IDropdownService dropdownService, IHelpers helpers)
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
                RoleMatrix model = new RoleMatrix();
                model.EmployeeHeader = new EmployeeHeader();
                model.EmployeeHeader.Department = new Shared.Master.Department();
                model.EmployeeHeader.Designation = new Shared.Master.Designation();
                model.EmployeeHeader.Location = new Shared.Master.Location();
                model.EmployeeHeader.Category = new Shared.Master.Category();
                model.EmployeeHeader.Position = new Shared.DataBank.DTO.PositionManagement();
                ViewBag.Employee = new SelectList(await _dropdownService.GetEmployeeDropDown(), "Code", "Name");

                ViewBag.Role = new SelectList(await _dropdownService.GetRoleMatrixRoleDropdown(), "Code", "Name");
   
                model.ColumnCaption = "Parameter";
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
        public async Task<ActionResult> GetList(int RoleCode)
        {
            try
            {
                RoleMatrix model = new RoleMatrix();
                model = await _service.GetList(RoleCode);
                return PartialView("_RoleMatrixList", model);
             
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
        public async Task<IActionResult> AddAuthority(int ParameterCode, int index)
        {
            RoleMatrix model = new RoleMatrix();
            model.EmployeeHeader = new EmployeeHeader();
            model.EmployeeHeader.Department = new Shared.Master.Department();
            model.EmployeeHeader.Designation = new Shared.Master.Designation();
            model.EmployeeHeader.Location = new Shared.Master.Location();
            model.EmployeeHeader.Category = new Shared.Master.Category();
            model.EmployeeHeader.Position = new Shared.DataBank.DTO.PositionManagement();
            ViewBag.Employee = new SelectList(await _dropdownService.GetEmployeeDropDown(), "Code", "Name");
            //model.EmployeeHeader = await _commonService.GetEmployeeCard(0);

            return PartialView("_AddAuthority",model);
        }
        public async Task<IActionResult> EmployeeCard(int EmpCode)
        {
            try
            {
                RoleMatrix model = new RoleMatrix();
                model.EmployeeHeader = await _commonService.GetEmployeeCard(EmpCode);
                var sg = await _service.GetSectionGrade(EmpCode);
                model.Section = sg?.Section;
                model.Grade = sg?.Grade;

                return PartialView("_EmployeeCard", model);
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
        public async Task<ActionResult> SaveAuth(int Position,int RoleCode,int ParameterCode)
        {
            try
            {
                RoleMatrix RoleMatrix = new RoleMatrix();
                RoleMatrix.Role = new Role();

                RoleMatrix.PositionCode = Position;
                RoleMatrix.Role.Code = RoleCode;
                RoleMatrix.ParameterCode = ParameterCode;
                var response = await _service.SaveAuth(RoleMatrix);

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
        public async Task<IActionResult> ExportToexcel(string RoleName, int RoleCode, int TotalRows)
        {
            try
            {
                RoleMatrix model = new RoleMatrix();

                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                model = await _service.GetList(RoleCode);
                int slno = 0;
                string currDateTime = await _helpers.GetCurrentDate();
                
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("RoleMatrixList", "Excel");
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Role Matrix List");
                    int count = model.RoleMatrixList.Count + 6;


                    worksheet.Range("A1:D" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:D" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:D" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:D" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:D" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 3)));
                    wsReportNameHeaderRange.Style.Font.Bold = true;
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Merge();
                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                    wsReportNameHeaderRange.Value = "Role Matrix List :"+ RoleName;
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
                    worksheet.Cell(5, 2).Value = model.RoleMatrixList.Count;

                    var currentRow = 6;
                    #region Header
                    worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 1).Value = "Sl.No";
                    worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 2).Value = model.ColumnCaption;
                    worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 3).Value = "Authority";
                    worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 4).Value = "Position";
                    #endregion

                    #region Body
                    foreach (var item in model.RoleMatrixList)
                    {
                        currentRow++;
                        slno = slno + 1;
                        worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 1).Value = slno;
                        worksheet.Cell(currentRow, 2).Value = item.ParameterName;
                        worksheet.Cell(currentRow, 3).Value = item.Authority;
                        worksheet.Cell(currentRow, 4).Value = item.Position;
                        worksheet.Columns(1, 6).Style.Alignment.WrapText = true;

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
                           "Role Matrix.xlsx"
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
