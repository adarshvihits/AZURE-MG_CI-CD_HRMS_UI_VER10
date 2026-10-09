using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.Master;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;
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
    public class AssetMasterController : Controller
    {
        public readonly IAssetService _service;
        public readonly IDropdownService _dropdownService;
        public readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        public AssetMasterController(IAssetService service, IDropdownService dropdownService,ICommonService commonService, IHelpers helpers)
        {
            _service = service;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = helpers;
        }
        public async Task<IActionResult> Index()
        {
            try { 
            Asset model = new Asset();
            model = await _service.GetList("ACTIVE");
            ViewBag.Asc = new SelectList(await _dropdownService.GetAssetMasterCategoryDropDown(), "Code", "Name");
            ViewBag.Asd = new SelectList(await _dropdownService.GetAssetDepartmentDropDown(), "Code", "Name");
            ViewBag.Aco = new SelectList(await _dropdownService.GetAssetConditionDropDown(), "Code", "Name");
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
        public async Task<ActionResult> GetAssetList(string TabIndex)
        {
            try { 
            if (TabIndex == "APPROVAL_PENDING")
                return PartialView("_PendingList", await _service.GetList(TabIndex));
            else
                return PartialView("_ActiveList", await _service.GetList(TabIndex));
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
        public async Task<IActionResult> Create()
        {
            try { 
            Asset model = new Asset();
            model.Mode = "Create";
            model.Active = true;
            ViewBag.Asc = new SelectList(await _dropdownService.GetAssetMasterCategoryDropDown(), "Code", "Name");
            ViewBag.Asd = new SelectList(await _dropdownService.GetAssetDepartmentDropDown(), "Code", "Name");
            ViewBag.Aco = new SelectList(await _dropdownService.GetAssetConditionDropDown(), "Code", "Name");
            return PartialView("~/Areas/Master/Views/AssetMaster/_AssetSave.cshtml", model);
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
        public async Task<ActionResult> Create(Asset input)
        {
            try { 
            var response = await _service.Create(input);
            TempData["Message"] = response.Message;

            return RedirectToAction("Index");
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
        public async Task<ActionResult> Save(Asset model)
        {
            try { 
            //Asset model = new Asset();
            //model.AssetCategory = new Category();
            //model.Condition = new Shared.DataBank.Generic.Master.Condition();
            //model.IssuingDepartment = new Department();
            //dynamic item = JObject.Parse(json);
            //model.Name = item.Name;
            //model.ShortName = item.shortname;
            //model.Mode = item.mode;
            //    model.AssetCategory.Code =(int) item.assetcategorycode;
            //model.AssetCode = item.assetcode;

            //model.DateOfPurchase =Convert.ToDateTime(item.dop);
            //model.Brand = item.brand;
            //model.Cost = item.cost;
            //model.IssuingDepartment.Code = item.issuingdept;
            //model.Condition.Code = item.conditioncode;
            //model.Active = item.status;
            //model.Description = item.description;
            //if (item.gi == "")
            //{
            //    model.GI = Guid.Parse("00000000-0000-0000-0000-000000000000");
            //}
            //else
            //{
            //    model.GI = item.gi;
            //}
            ////model.LogGI = item.loggi;
            //model.ModelNo = item.model;
            var response = await _service.Create(model);
            //if (response.Success == true)
            //{
            //    if (model.Mode == "MODIFY")
            //    {
            //        response.Message = "Updated Successfully";
            //        return Json(response);
            //    }
            //    else
            //    {
            //        response.Message = "Saved Successfully";
            //        return Json(response);
            //    }
            //}
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
            public async Task<ActionResult> Edit(Guid AssetGI, Guid LogGI)
        {
            try { 
            ViewBag.Asc = new SelectList(await _dropdownService.GetAssetMasterCategoryDropDown(), "Code", "Name");
            ViewBag.Asd = new SelectList(await _dropdownService.GetAssetDepartmentDropDown(), "Code", "Name");
            ViewBag.Aco = new SelectList(await _dropdownService.GetAssetConditionDropDown(), "Code", "Name");
            Asset model = await _service.GetAssetDetails(AssetGI, LogGI, "MODIFY");
            model.Mode = "MODIFY";

            return PartialView("_AssetSave", model);
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
        public async Task<IActionResult> GetDeleteAssetSlider(Guid AssetGI, Guid LogGI)
        {
            try { 
            Asset model = await _service.GetAssetDetails(AssetGI, LogGI, "DELETE");
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
        public async Task<ActionResult> Delete(Guid GI)
        {
            try { 
            var response = await _service.Delete(GI);
            return Json(response);
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
        public async Task<IActionResult> Approve(Guid AssetGI, Guid LogGI)
        {
            try { 
            Asset model = await _service.GetAssetDetails(AssetGI, LogGI, "APPROVAL");
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
        public async Task<ActionResult> Approve(Asset model)
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
        public async Task<ActionResult> Reject(Asset model)
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


        public async Task<IActionResult> ExportToExcel(string TabIndex)
        {
            try { 
            Asset model = new Asset();
            int slno = 0;
            string currDateTime = await _helpers.GetCurrentDate();
            
            model = await _service.GetList(TabIndex);
            var LoginUser = HttpContext.Session.GetString("LoginUser");
            var CompanyName = HttpContext.Session.GetString("CompanyName");
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Asset Master (" + TabIndex + ")", "Excel");
                if (TabIndex == "APPROVAL_PENDING")
            {

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("PendingList");
                    int PendingCountnum = model.PendingCount + 6;

                    worksheet.Range("A1:F" + PendingCountnum).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:F" + PendingCountnum).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:F" + PendingCountnum).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:F" + PendingCountnum).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:F" + PendingCountnum).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 5)));
                    wsReportNameHeaderRange.Style.Font.Bold = true;
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Merge();
                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);

                    wsReportNameHeaderRange.Value = "Asset " + "(" + TabIndex + ")";
                    worksheet.Range("A6:F6").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
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
                    
                    worksheet.Cell(currentRow, 1).Value = "Sl.No";
                    worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 2).Value = "Name";
                    worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 3).Value = "Short Name";
                    worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 4).Value = "Category";
                    worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 5).Value = "Code";
                    worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 6).Value = "Entry Type";
                    worksheet.Cell(currentRow, 6).Style.Font.Bold = true;




                    #endregion

                    #region Body
                    foreach (var item in model.AssetList)
                    {
                        currentRow++;
                        slno = slno + 1;
                        worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 1).Value = slno;
                        worksheet.Cell(currentRow, 2).Value = item.Name;
                        worksheet.Cell(currentRow, 3).Value = item.ShortName;
                        worksheet.Cell(currentRow, 4).Value = item.AssetCategory.Name;
                        worksheet.Cell(currentRow, 5).Value = item.AssetCode;
                        worksheet.Cell(currentRow, 6).Value = item.EntryType;


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
                            "Asset Pending List.xlsx"
                            );
                    }
                }



            }
            else
            {
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Asset List");
                    int count = model.AssetList.Count + 6;


                    worksheet.Range("A1:G" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:G" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:G" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:G" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:G" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 6)));
                    wsReportNameHeaderRange.Style.Font.Bold = true;
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Merge();
                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                    wsReportNameHeaderRange.Value = "Asset" + "(" + TabIndex + ")";

                    worksheet.Range("A6:G6").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
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
                    worksheet.Cell(5, 2).Value = model.AssetList.Count;





                    var currentRow = 6;

                    #region Header

                    worksheet.Cell(currentRow, 1).Value = "Sl.No";
                    worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 2).Value = "Name";
                    worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 3).Value = "Short Name";
                    worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 4).Value = "Category";
                    worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 5).Value = "Asset Code";
                    worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 6).Value = "Status";
                    worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 7).Value = "Condition";
                    worksheet.Cell(currentRow, 7).Style.Font.Bold = true;


                    #endregion

                    #region Body
                    foreach (var item in model.AssetList)
                    {
                        currentRow++;
                        slno = slno + 1;
                        worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 1).Value = slno;
                        worksheet.Cell(currentRow, 2).Value = item.Name;
                        worksheet.Cell(currentRow, 3).Value = item.ShortName;
                        worksheet.Cell(currentRow, 4).Value = item.AssetCategory.Name;
                        worksheet.Cell(currentRow, 5).Value = item.Code;
                        worksheet.Cell(currentRow, 6).Value = item.Status;
                        worksheet.Cell(currentRow, 7).Value = item.Condition.Name;
                        worksheet.Columns(1, 7).Style.Alignment.WrapText = true;

                    }
                    #endregion
                    //worksheet.Range("A:G").Style.Alignment.WrapText = true;
                    worksheet.Columns().AdjustToContents();
                    worksheet.Rows().AdjustToContents();

                 
                    //worksheet.Cell(1,7).Style.Alignment.WrapText = true;
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
                            "Asset master List.xlsx"
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
