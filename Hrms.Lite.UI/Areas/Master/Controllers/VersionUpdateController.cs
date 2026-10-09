using ExcelDataReader;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.Master;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.Master;
using Hrms.Lite.Shared.General;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Spire.Xls;
using ClosedXML.Excel;

namespace Hrms.Lite.UI.Areas.Master.Controllers
{
    [Area("Master")]
    [SessionAuthorize]
    public class VersionUpdateController : Controller
    {
        public readonly IVersionUpdateService _service;
        private readonly IWebHostEnvironment _environment;
        private readonly IHelpers _helpers;
        public readonly ICommonService _commonService;
        private readonly IDropdownService _dropdownService;
        //public static class Global
        //{
        //    public static int Month;
        //    public static int Deduction;
        //    public static Filter GlobalFilter;

        //}
        public VersionUpdateController(IVersionUpdateService service, IWebHostEnvironment environment, IHelpers helpers,ICommonService commonService, IDropdownService dropdownService)
        {
            _service = service;
            _commonService = commonService;
            _environment = environment;
            _helpers = helpers;
            _commonService = commonService;
            _dropdownService = dropdownService;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                VersionUpdate model = new VersionUpdate();

                ViewBag.VersionName = new SelectList(await _dropdownService.GetVersionDropDown(), "Code", "Name");
                model = await _service.GetVersionUpdateList(0);
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
        public async Task<IActionResult> GetVersionUpdateList(int VersionCode)
        {
            try
            {
                VersionUpdate model = new VersionUpdate();
                if (VersionCode != 0)
                    model = await _service.GetVersionUpdateList(VersionCode);

                return PartialView("_VersionUpdateList", model);
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
        public async Task<IActionResult> GetNewVersionPopUp()
        {


            VersionUpdate model = await _service.GetVersionsList();
            if (model.NewVersionList.Count == 0)
            {
                model.NewVersionList = new List<NewVersion>();
                NewVersion lst = new NewVersion()
                {
                    SlNo = 1,
                    Name = ""
                };
                model.NewVersionList.Add(lst);
            }

            return PartialView("~/Areas/Master/Views/VersionUpdate/_NewVersionPopUp.cshtml", model);
        }
       
        [HttpPost]
        public async Task<IActionResult> SaveNewVersion(VersionUpdate input)
        {
            var response = await _service.NewVersionSave(input.NewVersionList);
            return Json(response);

        }
        public async Task<IActionResult> Create()
        {
            try
            {
                VersionUpdate model = new VersionUpdate();
                model.Mode = "CREATE";
                model.Active = true;
                ViewBag.AddVersionDropdown = new SelectList(await _dropdownService.GetVersionDropDown(), "Code", "Name");
                ViewBag.ModuleDropdown = new SelectList(await _dropdownService.GetModules(), "Code", "Name");
                ViewBag.TypeDropdown = new SelectList(await _dropdownService.GetVersionUpdateTypeDropDown(), "Code", "Name");

                return PartialView("~/Areas/Master/Views/VersionUpdate/_CreateOrEdit.cshtml", model);
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
        public async Task<IActionResult> Edit(Guid VersionUpdateGI)
        {
            try
            {
                VersionUpdate model = await _service.GetVersionUpdateDetails(VersionUpdateGI);
                model.Mode = "MODIFY";
                ViewBag.AddVersionDropdown = new SelectList(await _dropdownService.GetVersionDropDown(), "Code", "Name");
                ViewBag.ModuleDropdown = new SelectList(await _dropdownService.GetModules(), "Code", "Name");
                ViewBag.TypeDropdown = new SelectList(await _dropdownService.GetVersionUpdateTypeDropDown(), "Code", "Name");

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
        public async Task<ActionResult> Save(VersionUpdate input)
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
        public async Task<ActionResult> Delete(Guid VersionUpdateGI)
        {
            try
            {
                VersionUpdate model = await _service.GetVersionUpdateDetails(VersionUpdateGI);
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
        public async Task<ActionResult> DeleteVersionUpdate(Guid GI)
        {
            try
            {
                var response = await _service.Delete(GI);
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
        public async Task<IActionResult> ImportFromExcel(VersionUpdate model)
        {
            string path = Path.Combine(this._environment.WebRootPath, "Uploads");
            string fileName = model.Excel.FileData.FileName;
            try
            {
                model.VersionUpdateList = new List<VersionUpdate>();
                string wwwPath = this._environment.WebRootPath;
                string contentPath = this._environment.ContentRootPath;


                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                {
                    model.Excel.FileData.CopyTo(stream);

                }
                try
                {
                    using (var stream = System.IO.File.Open(path + "\\" + fileName, FileMode.Open, FileAccess.Read))
                    {

                        using (var reader = ExcelReaderFactory.CreateReader(stream))
                        {
                            List<VersionUpdate> List = new List<VersionUpdate>();
                            var readerrr = ExcelReaderFactory.CreateReader(stream);
                            while (reader.Read()) //Each row of the file
                            {
                                var item = new VersionUpdate();
                                item.Version = new MasterBase();
                                item.Module = new MasterBase();
                                item.Type = new MasterBase();

                                if (reader.GetValue(0) != null)
                                {
                                    item.Version.Name = reader.GetValue(0).ToString();
                                }
                                if (reader.GetValue(1) != null)
                                {
                                    item.Module.Name = reader.GetValue(1).ToString();
                                }
                                if (reader.GetValue(2) != null)
                                {
                                    item.Type.Name = reader.GetValue(2).ToString();
                                }
                                if (reader.GetValue(3) != null)
                                {
                                    item.Description = reader.GetValue(3).ToString();
                                }
                               
                                    List.Add(item);
                               
                            }
                            if (List[0].Version.Name != "Version" || List[0].Module.Name != "Module" || List[0].Type.Name != "Type" || List[0].Description != "Description")
                            {
                                reader.Close();
                                // Check if file exists with its full path    
                                if (System.IO.File.Exists(path + "\\" + fileName))
                                {
                                    System.IO.File.Delete(path + "\\" + fileName);
                                }
                                return Json(new ResponseEntity("Excel File not in the prescribed format", false));
                            }
                            model.VersionUpdateList = List;

                            model.VersionUpdateList.RemoveAt(0);
                        }

                    }
                }
                catch (Exception e)
                {
                    // Check if file exists with its full path    
                    if (System.IO.File.Exists(path + "\\" + fileName))
                    {
                        System.IO.File.Delete(path + "\\" + fileName);
                    }
                    return Json(new ResponseEntity("Excel File not in the prescribed format", false));
                }

                model.Excel.FileData = null;
                var response = await _service.ImportFromExcel(model);
                // Check if file exists with its full path    
                if (System.IO.File.Exists(path + "\\" + fileName))
                {
                    System.IO.File.Delete(path + "\\" + fileName);
                }
                
                if (response.Message != "UPDATED")
                {
                    model.VersionUpdateList = response.Data;
                    model.Message = response.Message;
                    return PartialView("_UnmatchedList", model);
                }
                else
                {
                    response.Message = "Uploaded Successfully";
                    return Json(response);
                }
            }
            catch (UnauthorizedAccessException e)
            {

                return RedirectToAction("Login", "Account", new { Area = "" });
            }
            catch (Exception e)
            {
                // Check if file exists with its full path    
                if (System.IO.File.Exists(path + "\\" + fileName))
                {
                    System.IO.File.Delete(path + "\\" + fileName);
                }
                return RedirectToAction("Error", "Account", new { Area = "" });
            }


        }

        public async Task<IActionResult> ExportToExcel(int VersionCode, int totalRows,string VersionName)
        {
            try
            {
                VersionUpdate model = new VersionUpdate();
                model = await _service.GetVersionUpdateList(VersionCode);
                int slno = 0;
                string currDateTime = await _helpers.GetCurrentDate();
                
                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Version Update Details", "Excel");

               
                {

                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("Version Update List");
                        int count = model.VersionUpdateList.Count + 6;


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
                        wsReportNameHeaderRange.Value = "Version :" + "(" + VersionName + ")";
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
                        worksheet.Cell(5, 2).Value = model.VersionUpdateList.Count;

                        var currentRow = 6;
                        #region Header
                        worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 1).Value = "Sl.No";
                        worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 2).Value = "Module";
                        worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 3).Value = "Type";
                        worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 4).Value = "Description";
                        #endregion

                        #region Body
                        foreach (var item in model.VersionUpdateList)
                        {
                            currentRow++;
                            slno = slno + 1;
                            worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 1).Value = slno;
                            worksheet.Cell(currentRow, 2).Value = item.Module.Name;
                            worksheet.Cell(currentRow, 3).Value = item.Type.Name;
                            worksheet.Cell(currentRow, 4).Value = item.Description;
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
                               "Version Update Details.xlsx"
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

