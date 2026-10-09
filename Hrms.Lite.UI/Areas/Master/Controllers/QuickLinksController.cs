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
    public class QuickLinksController : Controller
    {
        private readonly IQuickLinksService _service;
        private readonly ICommonService _commonService;
        private readonly IDropdownService _dropdownService;
        public QuickLinksController(IQuickLinksService service, ICommonService commonService, IDropdownService dropdownService)
        {
            _service = service;
            _commonService = commonService;
            _dropdownService = dropdownService;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
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
        public async Task<ActionResult> GetQuicklinksList(string TabIndex)
        {
            try
            {
                if (TabIndex != "APPROVAL_PENDING")
                    return PartialView("_QuicklinksList", await _service.GetList(TabIndex));
                else
                    return PartialView("_PendingList", await _service.GetList(TabIndex));
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


        public async Task<ActionResult> Create()
        {
            try
            {
                QuickLinks model = new QuickLinks();
                ViewBag.MultipleCombinationQucklink = new SelectList(await _dropdownService.GetHRPolicyCombinationDropDown(), "Code", "Name");
                model.Mode = "CREATE";
                model.Active = true;
                //return PartialView("~/Areas/Master/Views/Quicklinks/_CreateOrEdit.cshtml", model);
                return View("Create",model);
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



        public async Task<IActionResult> GetCombinationDetails(int id, int type, string combinationName)
        {
            try
            {
                ViewBag.CombinationName = combinationName;
                if (type == 0)
                {
                    QuickLinks model = new QuickLinks();
                    if (id != 0)
                    {
                        model = await _service.GetCombinationDetails(id);
                        model.Combinations1 = model.CombinationsList;
                    }
                    return PartialView("_CombinationList1", model);
                }
                else
                {
                    QuickLinks model1 = new QuickLinks();
                    if (id != 0)
                    {
                        model1 = await _service.GetCombinationDetails(id);
                        model1.Combinations2 = model1.CombinationsList;
                    }
                    return PartialView("_CombinationList2", model1);
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






        public async Task<IActionResult> Edit(Guid QuicklinksGI)
        {
            QuickLinks model = new QuickLinks();
            ViewBag.MultipleCombinationQucklink = new SelectList(await _dropdownService.GetHRPolicyCombinationDropDown(), "Code", "Name");

       
            model = await _service.GetQuickLinksDetails(QuicklinksGI);
            model.Mode = "Modify";
            if (model.Combinations1[0].EmployeeID != "")
            {
                ViewBag.CombinationName = "Employee";
            }
            return View("Create", model);
        }



        [HttpPost]
        public async Task<IActionResult> Create(QuickLinks model)
        {
          
            
            try
            {
                
                if (model.Combinations1 != null)
                {
                    var cmb1 = from li in model.Combinations1
                               where li.Applicable == true
                               select li;
                    model.Combinations1 = cmb1.ToList();
                }
                if (model.Combinations2 != null)
                {
                    var cmb2 = from li in model.Combinations2
                               where li.Applicable == true
                               select li;
                    model.Combinations2 = cmb2.ToList();
                }
                if (model.Mode != "Modify")
                {
                    var response = await _service.QuicklinkSave(model);
                    return Json(response);
                }

                else
                {
                    var response = await _service.QuicklinkUpdate(model);
                    return Json(response);
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





        //public async Task<ActionResult> Delete(Guid QuicklinksGI, Guid LogGI)
        //{
        //    try
        //    {
        //        QuickLinks model = await _service.GetQuickLinksDetails(QuicklinksGI, LogGI, "DELETE");
        //        model.Mode = "DELETE";
        //        return PartialView("_Delete", model);
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
        //[HttpPost]
        //public async Task<ActionResult> Delete(QuickLinks model)
        //{
        //    try
        //    {
        //        var response = await _service.Delete(model);
        //        return Json(response);
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


        [HttpPost]
        public async Task<ActionResult> Delete(Guid QuicklinksGI)
        {
            try
            {
                QuickLinks model = new QuickLinks();

                model.GI = QuicklinksGI;
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






        //public async Task<ActionResult> Approve(Guid QuicklinksGI, Guid LogGI)
        //{
        //    try
        //    {
        //        QuickLinks model = await _service.GetQuickLinksDetails(QuicklinksGI, LogGI, "APPROVAL");
        //        return PartialView("_Approve", model);
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
        [HttpPost]
        public async Task<ActionResult> Approve(QuickLinks model)
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
        public async Task<ActionResult> Reject(QuickLinks model)
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

        //public async Task<IActionResult> ExportToExcel(string TabIndex)
        //{
        //    try
        //    {
        //        QuickLinks model = new QuickLinks();
        //        model = await _service.GetList(TabIndex);
        //        int slno = 0;

        //        string currDateTime = await _helpers.GetCurrentDate();
        //        
        //        var LoginUser = HttpContext.Session.GetString("LoginUser");
        //        var CompanyName = HttpContext.Session.GetString("CompanyName");
        //        ExcelPassword EP = new ExcelPassword();


        //        if (TabIndex == "APPROVAL_PENDING")
        //        {
        //            EP = await _commonService.GetExcelPassword("Quicklinks Pending List", "Excel");
        //            using (var workbook = new XLWorkbook())
        //            {
        //                var worksheet = workbook.Worksheets.Add("PendingList");
        //                int PendingCountnum = model.PendingCount + 6;


        //                worksheet.Range("A1:D" + PendingCountnum).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
        //                worksheet.Range("A1:D" + PendingCountnum).Style.Border.TopBorder = XLBorderStyleValues.Thin;
        //                worksheet.Range("A1:D" + PendingCountnum).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
        //                worksheet.Range("A1:D" + PendingCountnum).Style.Border.RightBorder = XLBorderStyleValues.Thin;
        //                worksheet.Range("A1:D" + PendingCountnum).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
        //                var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 3)));
        //                wsReportNameHeaderRange.Style.Font.Bold = true;
        //                wsReportNameHeaderRange.Style.Font.FontSize = 15;
        //                wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        //                wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
        //                wsReportNameHeaderRange.Merge();
        //                wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
        //                wsReportNameHeaderRange.Style.Font.FontSize = 15;
        //                wsReportNameHeaderRange.Value = "Quicklinks " + "(" + TabIndex + ")";
        //                worksheet.Range("A6:D6").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
        //                worksheet.Cell(2, 1).Style.Font.Bold = true;
        //                worksheet.Cell(2, 1).Value = "Company Name";
        //                worksheet.Cell(2, 2).Value = CompanyName.ToUpper();
        //                worksheet.Cell(3, 1).Style.Font.Bold = true;
        //                worksheet.Cell(3, 1).Value = "Login User";
        //                worksheet.Cell(3, 2).Value = LoginUser;
        //                worksheet.Cell(4, 1).Style.Font.Bold = true;
        //                worksheet.Cell(4, 1).Value = "Date & Time";
        //                worksheet.Cell(4, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //                worksheet.Cell(4, 2).Style.DateFormat.Format = "dd-MMM-yyyy   h:mm AM/PM";
        //                worksheet.Cell(4, 2).Value = currDateTime.ToString();
        //                worksheet.Cell(5, 1).Style.Font.Bold = true;
        //                worksheet.Cell(5, 1).Value = "Total Count";
        //                worksheet.Cell(5, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //                worksheet.Cell(5, 2).Value = model.PendingCount;







        //                var currentRow = 6;

        //                #region Header
        //                worksheet.Cell(currentRow, 1).Value = "Sl.No";
        //                worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
        //                worksheet.Cell(currentRow, 2).Value = "Url Description";
        //                worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
        //                worksheet.Cell(currentRow, 3).Value = "Url";
        //                worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
        //                worksheet.Cell(currentRow, 4).Value = "Entry Type";
        //                worksheet.Cell(currentRow, 4).Style.Font.Bold = true;



        //                #endregion

        //                #region Body
        //                foreach (var item in model.QuickLinksList)
        //                {
        //                    currentRow++;
        //                    slno = slno + 1;
        //                    worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);

        //                    worksheet.Cell(currentRow, 1).Value = slno;
        //                    worksheet.Cell(currentRow, 2).Value = item.UrlDescriptions;
        //                    worksheet.Cell(currentRow, 3).Value = item.Url;
        //                    worksheet.Cell(currentRow, 4).Value = item.Mode;
        //                    worksheet.Columns(1, 5).Style.Alignment.WrapText = true;

        //                }
        //                #endregion
        //                worksheet.Columns().AdjustToContents();
        //                using (var stream = new MemoryStream())
        //                {
        //                    workbook.SaveAs(stream);
        //                    if (EP.NeedPassword)
        //                    {
        //                        Workbook book = new Workbook();
        //                        book.LoadFromStream(stream);
        //                        //Protect Workbook
        //                        book.Protect(EP.Password,false,false);
        //                        book.SaveToStream(stream);
        //                    }
        //                    var content = stream.ToArray();
        //                    return File(
        //                        content,
        //                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
        //                        "Quicklinks Pending List.xlsx"
        //                        );
        //                }
        //            }

        //        }

        //        else
        //        {
        //            EP = await _commonService.GetExcelPassword("Quicklinks List(" + TabIndex + ")", "Excel");
        //            using (var workbook = new XLWorkbook())
        //            {
        //                var worksheet = workbook.Worksheets.Add(TabIndex + " List");
        //                int count = model.QuickLinksList.Count + 6;


        //                worksheet.Range("A1:C" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
        //                worksheet.Range("A1:C" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
        //                worksheet.Range("A1:C" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
        //                worksheet.Range("A1:C" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
        //                worksheet.Range("A1:C" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
        //                var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 2)));
        //                wsReportNameHeaderRange.Style.Font.Bold = true;
        //                wsReportNameHeaderRange.Style.Font.FontSize = 15;
        //                wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        //                wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
        //                wsReportNameHeaderRange.Merge();
        //                wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
        //                wsReportNameHeaderRange.Value = "Quicklinks" + "(" + TabIndex + ")";
        //                worksheet.Range("A6:C6").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
        //                worksheet.Cell(2, 1).Style.Font.Bold = true;
        //                worksheet.Cell(2, 1).Value = "Company Name";
        //                worksheet.Cell(2, 2).Value = CompanyName.ToUpper();
        //                worksheet.Cell(3, 1).Style.Font.Bold = true;
        //                worksheet.Cell(3, 1).Value = "Login User";
        //                worksheet.Cell(3, 2).Value = LoginUser;
        //                worksheet.Cell(4, 1).Style.Font.Bold = true;
        //                worksheet.Cell(4, 1).Value = "Date & Time";
        //                worksheet.Cell(4, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //                worksheet.Cell(4, 2).Style.DateFormat.Format = "dd-MMM-yyyy   h:mm AM/PM";
        //                worksheet.Cell(4, 2).Value = currDateTime.ToString();
        //                worksheet.Cell(5, 1).Style.Font.Bold = true;
        //                worksheet.Cell(5, 1).Value = "Total Count";
        //                worksheet.Cell(5, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //                worksheet.Cell(5, 2).Value = model.QuickLinksList.Count;




        //                var currentRow = 6;
        //                #region Header
        //                worksheet.Cell(currentRow, 1).Value = "Sl.No";
        //                worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
        //                worksheet.Cell(currentRow, 2).Value = "Url Description";
        //                worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
        //                worksheet.Cell(currentRow, 3).Value = "Url";
        //                worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
        //                #endregion

        //                #region Body
        //                foreach (var item in model.QuickLinksList)
        //                {
        //                    currentRow++;
        //                    slno = slno + 1;
        //                    worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //                    worksheet.Cell(currentRow, 1).Value = slno;
        //                    worksheet.Cell(currentRow, 2).Value = item.UrlDescriptions;
        //                    worksheet.Cell(currentRow, 3).Value = item.Url;
        //                    worksheet.Columns(1, 5).Style.Alignment.WrapText = true;
        //                }
        //                #endregion
        //                worksheet.Columns().AdjustToContents();
        //                using (var stream = new MemoryStream())
        //                {
        //                    workbook.SaveAs(stream);
        //                    if (EP.NeedPassword)
        //                    {
        //                        Workbook book = new Workbook();
        //                        book.LoadFromStream(stream);
        //                        //Protect Workbook
        //                        book.Protect(EP.Password,false,false);
        //                        book.SaveToStream(stream);
        //                    }
        //                    var content = stream.ToArray();
        //                    return File(
        //                        content,
        //                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
        //                        "Quicklinks List.xlsx"
        //                        );
        //                }
        //            }

        //        }

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

    }
}
