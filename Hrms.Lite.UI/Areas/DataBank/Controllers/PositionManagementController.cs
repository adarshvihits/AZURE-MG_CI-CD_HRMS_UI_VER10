using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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
    public class PositionManagementController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly IPositionManagementService _positionManagementService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        public PositionManagementController(IPositionManagementService PositionManagementService, IDropdownService dropdownService, ICommonService commonService, IHelpers helpers)
        {
            _positionManagementService = PositionManagementService;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = helpers;
        }
        public async Task<IActionResult> Index()
        {
            PositionManagement model = new PositionManagement();
            PositionManagement m = new PositionManagement();
            Filter filter = new Filter();
            m = await _positionManagementService.GetPositionManagementDetails("TotalPosition",filter);
            model = await _positionManagementService.PositionManagementInfo();
            model.PositionDetailedList = m.PositionDetailedList;
            model.Filter = await _commonService.GetFilterDropdowns();
            model.type = "Total Position";
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> GetPositionDetails(string type, PositionManagement model)
        {
            model = await _positionManagementService.GetPositionManagementDetails(type,model.Filter);
            model.type = type;
            return PartialView("_PositionDetails", model);
        }
        public async Task<IActionResult> Index_Approve()
        {
            PositionManagement model = new PositionManagement();
            model = await _positionManagementService.GetPositionApprovalDetails();
            return View(model);
        }
        public async Task<IActionResult> GetPositionApprovalDetails()
        {
            PositionManagement model = new PositionManagement();
            model = await _positionManagementService.GetPositionApprovalDetails();
            return PartialView("_ApproveList", model);
        }
        public async Task<IActionResult> AddNew()
        {
            PositionManagement model = new PositionManagement();
            ViewBag.Location = new SelectList(await _dropdownService.GetLocationDropdown(), "Code", "Name");
            ViewBag.Department = new SelectList(await _dropdownService.GetDepartmentDropdown(), "Code", "Name");
            ViewBag.Designation = new SelectList(await _dropdownService.GetDesignationDropdown(), "Code", "Name");
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CheckIfAlreadyExist(PositionManagement model)
        {
            try
            {
                var response = await _positionManagementService.GetPositionProceedValidation(model);
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
        public async Task<IActionResult> GetPositionDetailsList(int LocationCode, int DepartmentCode, int Designation, int NoofPosition)
        {
            try
            {
                PositionManagement model = new PositionManagement();
                model = await _positionManagementService.GetPositionDetailsList(LocationCode, DepartmentCode, Designation, NoofPosition);
                return PartialView("_NewPositionDetails", model);
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
        public async Task<IActionResult> Save(PositionManagement model)
        {

            var response = await _positionManagementService.Save(model);
            return Json(response);


        }
        public async Task<ActionResult> Delete(Guid PositionGI, int PositionCode)
        {
            try
            {
                PositionManagement model = await _positionManagementService.GetPositionDetails(PositionGI, PositionCode, "DELETE");
                model.Mode = "DELETE";
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
        public async Task<ActionResult> Approve_Delete(Guid PositionGI, int PositionCode)
        {
            try
            {
                PositionManagement model = await _positionManagementService.GetPositionDetails(PositionGI, PositionCode, "DELETE");
                model.Mode = "DELETE";
                return PartialView("_ApproveDelete", model);
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
        public async Task<ActionResult> GetPositionDetailsApprove(Guid PositionGI, int PositionCode)
        {
            try
            {
                PositionManagement model = await _positionManagementService.GetPositionDetails(PositionGI, PositionCode, "APPROVE");
                model.Mode = "APPROVE";
                return PartialView("_ApprovePosition", model);
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
        public async Task<ActionResult> DeletePosition(Guid GI)
        {
            try
            {
                var response = await _positionManagementService.Delete(GI);
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
        public async Task<ActionResult> Approve(PositionManagement model)
        {
            try
            {
                model.ApproveMode = "APPROVE";
                var response = await _positionManagementService.Approve(model);
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
        public async Task<IActionResult> ExportToExcel(string type,int totRecords, PositionManagement model)
        {
            try
            {
                model.Filter.length = totRecords;
                model.Filter.start = 0;
                string FilterValues = await _helpers.GetFilterValues(model.Filter);
                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Position Management (" + type + ")", "Excel");

                if (FilterValues == "")
                {
                    FilterValues = "All";
                }

                string currDateTime = await _helpers.GetCurrentDate();
                
                //Get Datset
                var dataset = await _positionManagementService.GetPositionManagementDetails_Excel(type, model.Filter);
                DataTable dt = dataset.Tables[0];

                //set EXCEL Column Heading 
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                        dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
                }

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Position Management");
                    int count = dt.Rows.Count + 5;

                    worksheet.Range("A1:F" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:F" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:F" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:F" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:F" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 5)));
                    wsReportNameHeaderRange.Style.Font.Bold = true;
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Merge();
                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191); ;
                    wsReportNameHeaderRange.Value = "Position Management (" + type + ")";

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
                    worksheet.Cell(3, 6).Value = dt.Rows.Count;
                    worksheet.Cell(4, 1).Style.Font.Bold = true;
                    worksheet.Cell(4, 1).Value = "Filter Applied";
                    worksheet.Range("B4:F4").Merge();
                    worksheet.Cell("B4").Value = FilterValues;

                    var currentRow = 5;
                    worksheet.Cell(currentRow, 1).InsertTable(dt);
                    worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;
                    worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                    worksheet.Range("A5:F5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Range("A5:F5").Style.Font.Bold = true;
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
                            "Position Management List(" + type + ").xlsx"
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
