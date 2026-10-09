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
using System.IO;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.Master.Controllers
{
    [Area("Master")]
    [SessionAuthorize]
    public class VenueController : Controller
    {
        public readonly IVenueService _service;
        private readonly IDropdownService _dropdownService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        public VenueController(IVenueService service, IDropdownService dropdownService, ICommonService commonService, IHelpers helpers)
        {
            _service = service;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = helpers;
        }
        public async Task<IActionResult> Index()
        {
            try
            {

                ViewBag.RegionDropDown = new SelectList(await _dropdownService.GetAllRegionsDropDown(), "Code", "Name");

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
        public async Task<IActionResult> GetVenueList(string TabIndex)
        {
            try
            {
                if (TabIndex == "APPROVAL_PENDING")
                    return PartialView("_PendingList", await _service.GetList(TabIndex));
                else
                    return PartialView("_VenueList", await _service.GetList(TabIndex));
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
                Venue model = new Venue();
                model.Mode = "CREATE";
                model.Active = true;

                ViewBag.RegionDropDown = new SelectList(await _dropdownService.GetAllRegionsDropDown(), "Code", "Name");


                return PartialView("~/Areas/Master/Views/Venue/_CreateOrEdit.cshtml", model);
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
        public async Task<ActionResult> Save(Venue Input)
        {
            try
            {
                var response = await _service.Create(Input);

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
        public async Task<IActionResult> Edit(Guid VenueGI, Guid LogGI)
        {
            try
            {
                Venue model = await _service.GetVenueDetails(VenueGI, LogGI, "MODIFY");
                model.Mode = "MODIFY";

              
                ViewBag.RegionDropDown = new SelectList(await _dropdownService.GetRegionDropDown(), "Code", "Name");
              

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
        public async Task<IActionResult> Delete(Guid VenueGI, Guid LogGI)
        {
            try
            {
                Venue model = await _service.GetVenueDetails(VenueGI, LogGI, "DELETE");
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
        [HttpPost]
        public async Task<ActionResult> Delete(Venue model)
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
        public async Task<IActionResult> Approve(Guid VenueGI, Guid LogGI)
        {
            try
            {
                Venue model = await _service.GetVenueDetails(VenueGI, LogGI, "APPROVAL");
                //model.Mode = "Approval";
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
        public async Task<ActionResult> Approve1(Venue model)
        {
            try
            {
                model.ApproveMode = "APPROVE";
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







        [HttpPost]
        public async Task<ActionResult> Reject(Venue model)
        {
            try
            {
                model.ApproveMode = "REJECT";
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


public async Task<IActionResult> ExportToExcel(string TabIndex)
{
    try
    {
        Venue model = new Venue();
        model = await _service.GetList(TabIndex);

        int slno = 0;

        string currDateTime = await _helpers.GetCurrentDate();

        var LoginUser = HttpContext.Session.GetString("LoginUser");
        var CompanyName = HttpContext.Session.GetString("CompanyName");
        ExcelPassword EP = new ExcelPassword();

        if (TabIndex == "APPROVAL_PENDING")
        {
            EP = await _commonService.GetExcelPassword("Venue Pending List", "Excel");
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("PendingList");
                int Count = model.VenueList.Count + 6;
                string columnName = "";

                int Columncount = 11;

                while (Columncount > 0)
                {
                    int modulo = (Columncount - 1) % 26;
                    columnName = Convert.ToChar('A' + modulo) + columnName;
                    Columncount = (Columncount - modulo) / 26;
                }

                string colName = columnName;

                worksheet.Range("A1", colName + Count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1", colName + Count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1", colName + Count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1", colName + Count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1", colName + Count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;

                var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, colName));
                wsReportNameHeaderRange.Style.Font.Bold = true;
                wsReportNameHeaderRange.Style.Font.FontSize = 15;
                wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                wsReportNameHeaderRange.Merge();

                wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                wsReportNameHeaderRange.Style.Font.FontSize = 15;
                wsReportNameHeaderRange.Value = "Venue " + "(" + TabIndex + ")";
                worksheet.Range("A6", colName + 6).Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
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
                worksheet.Cell(5, 2).Value = model.VenueList.Count;

                #region Header
                var currentRow = 6;
                worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 1).Value = "Sl.No";

                worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 2).Value = "Name";

                worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 3).Value = "Short Name";

                worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 4).Value = "DoorNo & Building";

                worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 5).Value = "Street";

                worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 6).Value = "Area";

                worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 7).Value = "Region";

                worksheet.Cell(currentRow, 8).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 8).Value = "State & Country";

                worksheet.Cell(currentRow, 9).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 9).Value = "Email";

                worksheet.Cell(currentRow, 10).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 10).Value = "Web Address";

                worksheet.Cell(currentRow, 11).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 11).Value = "Entry Type";
                #endregion

                #region Body
                foreach (var item in model.VenueList)
                {
                    currentRow++;
                    slno = slno + 1;
                    worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(currentRow, 1).Value = slno;
                    worksheet.Cell(currentRow, 2).Value = item.Name;
                    worksheet.Cell(currentRow, 3).Value = item.ShortName;
                    worksheet.Cell(currentRow, 4).Value = item.DoorNumAndBuilding;
                    worksheet.Cell(currentRow, 5).Value = item.Street;
                    worksheet.Cell(currentRow, 6).Value = item.AreaName;
                    worksheet.Cell(currentRow, 7).Value = item.Region?.Name;
                    worksheet.Cell(currentRow, 8).Value = item.StateAndCountry;
                    worksheet.Cell(currentRow, 9).Value = item.EmailId;
                    worksheet.Cell(currentRow, 10).Value = item.WebAdderss;
                    worksheet.Cell(currentRow, 11).Value = item.Mode;
                }
                worksheet.Columns(1, 11).Style.Alignment.WrapText = true;
                #endregion
                worksheet.Columns().AdjustToContents();
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    if (EP.NeedPassword)
                    {
                        using (var input = new MemoryStream(content))
                        using (var output = new MemoryStream())
                        {
                            Workbook book = new Workbook();
                            book.LoadFromStream(input);
                            //Protect Workbook
                            book.Protect(EP.Password, false, false);
                                    book.SaveToStream(output);
                                    content = output.ToArray();
                        }
                    }
                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "Venue Pending List.xlsx"
                        );
                }
            }
        }

        else
        {
            EP = await _commonService.GetExcelPassword("Venue List (" + TabIndex + ")", "Excel");
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add(TabIndex + " List");
                int count = model.VenueList.Count + 6;

                string columnName = "";

                int Columncount = 10;

                while (Columncount > 0)
                {
                    int modulo = (Columncount - 1) % 26;
                    columnName = Convert.ToChar('A' + modulo) + columnName;
                    Columncount = (Columncount - modulo) / 26;
                }

                string colName = columnName;

                worksheet.Range("A1", colName + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1", colName + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1", colName + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1", colName + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1", colName + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, colName));
                wsReportNameHeaderRange.Style.Font.Bold = true;
                wsReportNameHeaderRange.Style.Font.FontSize = 15;
                wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                wsReportNameHeaderRange.Merge();
                wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                wsReportNameHeaderRange.Value = "Venue" + "(" + TabIndex + ")";
                worksheet.Range("A6", colName + 6).Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
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
                worksheet.Cell(5, 2).Value = model.VenueList.Count;

                var currentRow = 6;
                #region Header

                worksheet.Cell(currentRow, 1).Value = "Sl.No";
                worksheet.Cell(currentRow, 1).Style.Font.Bold = true;

                worksheet.Cell(currentRow, 2).Value = "Name";
                worksheet.Cell(currentRow, 2).Style.Font.Bold = true;

                worksheet.Cell(currentRow, 3).Value = "Short Name";
                worksheet.Cell(currentRow, 3).Style.Font.Bold = true;

                worksheet.Cell(currentRow, 4).Value = "DoorNo & Building";
                worksheet.Cell(currentRow, 4).Style.Font.Bold = true;

                worksheet.Cell(currentRow, 5).Value = "Street";
                worksheet.Cell(currentRow, 5).Style.Font.Bold = true;

                worksheet.Cell(currentRow, 6).Value = "Area";
                worksheet.Cell(currentRow, 6).Style.Font.Bold = true;

                worksheet.Cell(currentRow, 7).Value = "Region";
                worksheet.Cell(currentRow, 7).Style.Font.Bold = true;

                worksheet.Cell(currentRow, 8).Value = "Phone No";
                worksheet.Cell(currentRow, 8).Style.Font.Bold = true;

                worksheet.Cell(currentRow, 9).Value = "Email";
                worksheet.Cell(currentRow, 9).Style.Font.Bold = true;

                worksheet.Cell(currentRow, 10).Value = "Web Address";
                worksheet.Cell(currentRow, 10).Style.Font.Bold = true;

                #endregion
                #region Body
                foreach (var item in model.VenueList)
                {
                    currentRow++;
                    slno = slno + 1;
                    worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(currentRow, 1).Value = slno;
                    worksheet.Cell(currentRow, 2).Value = item.Name;
                    worksheet.Cell(currentRow, 3).Value = item.ShortName;
                    worksheet.Cell(currentRow, 4).Value = item.DoorNumAndBuilding;
                    worksheet.Cell(currentRow, 5).Value = item.Street;
                    worksheet.Cell(currentRow, 6).Value = item.AreaName;
                    worksheet.Cell(currentRow, 7).Value = item.Region?.Name;
                    worksheet.Cell(currentRow, 8).Value = item.PhoneNo;
                    worksheet.Cell(currentRow, 9).Value = item.EmailId;
                    worksheet.Cell(currentRow, 10).Value = item.WebAdderss;
                }
                worksheet.Columns(1, 10).Style.Alignment.WrapText = true;
                #endregion
                worksheet.Columns().AdjustToContents();
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    if (EP.NeedPassword)
                    {
                        using (var input = new MemoryStream(content))
                        using (var output = new MemoryStream())
                        {
                            Workbook book = new Workbook();
                            book.LoadFromStream(input);
                            //Protect Workbook
                            book.Protect(EP.Password, false, false);
                                    book.SaveToStream(output);
                                    content = output.ToArray();
                        }
                    }
                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "Venue List.xlsx"
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
}    }
}
