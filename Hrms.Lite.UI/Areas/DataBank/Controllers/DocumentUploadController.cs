using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.Account;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.General;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.StaticFiles;
using Newtonsoft.Json;
using Spire.Xls;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.DataBank.Controllers
{
    [Area("DataBank")]
    [SessionAuthorize]
    public class DocumentUploadController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly IDocumentUploadService _documentUploadService;
        private readonly ICommonService _commonService;
        private readonly IBlob _blob;
        private readonly IEnrollmentService _enrollmentService;
        private readonly IHelpers _helpers;

        public DocumentUploadController(IEnrollmentService enrollmentService, IDocumentUploadService documentUploadService, IDropdownService dropdownService, ICommonService commonService, IBlob blob,IHelpers helpers)
        {
            _enrollmentService = enrollmentService;
            _documentUploadService = documentUploadService;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _blob = blob;
            _helpers = helpers;
        }
        public async Task<IActionResult> Index()
        {
            try { 
            DocumentUpload model = new DocumentUpload();
            DocumentUpload m = new DocumentUpload();
            Filter filter = new Filter();
            ViewBag.DocumentFilter = new SelectList(await _dropdownService.GetDocUploadGridDropDown(), "Code", "Name");
            model.FilterCombo = new FilterSlabComboFill();
            model.FilterCombo.Code = 0;
            filter.length = 10;
            m = await _documentUploadService.GetDocumentUploadList(0, 0, filter);
            model.Filter = await _commonService.GetFilterDropdowns();
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
       
      
        public async Task<IActionResult> Create(Guid EmployeeGI,string BreadCrumb,string BreadCrumbName,string TabName)
        {
            DocumentUpload model = new DocumentUpload();
            model = await _documentUploadService.GetIdentityDocumentDetailsList(EmployeeGI);
            model.EmployeeHeader = await _commonService.GetEmployeeHeader(EmployeeGI);
            model.EmployeeGI = model.EmployeeHeader.EmployeeGuid;
            DocumentUpload m = new DocumentUpload();
            m = await _documentUploadService.GetOtherDocumentList(EmployeeGI);
            model.OtherDocList = m.OtherDocList;
            model.BreadCrumb = BreadCrumb;
            model.BreadCrumbName = BreadCrumbName;
            model.TabName = TabName;
            ViewBag.DocumentType = new SelectList(await _dropdownService.GetDocUploadDocTypeDropDown("OTHER_DOCUMENTS",null), "Code", "Name");
            return View("~/Areas/DataBank/Views/DocumentUpload/Create.cshtml",model);
        }    


        [HttpPost]
        public async Task<IActionResult> GetList(Filter filter1, int Tabindex, int SearchId,int filtercode)
        {
            try
            {
                DocumentUpload model = new DocumentUpload();
                DocumentUpload m = new DocumentUpload();
 
                Filter filter = new Filter();
                model.Filter= filter1;


                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }

                model = await _documentUploadService.GetDocumentUploadList(Tabindex, filtercode, filter1);
                model.AllTabCount = model.AllTabCount;
  
                m.DocumentUploadList = model.DocumentUploadList;
                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.DocumentUploadList == null || model.DocumentUploadList.Count() == 0)
                {
                   
                    return Json(new
                    {
                        data = "",
                        draw = filter1.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows
                    });
                }




                if (model.DocumentUploadList.Count > 0)
                {
                    totalrows = m.DocumentUploadList[0].active;

                   
                }

             
                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = m.DocumentUploadList[0].totalRecords;
                    return Json(new
                    {
                        data = model.DocumentUploadList,
                        draw = filter1.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        recordsTotal = totalrows,
                        allTabCount = model.AllTabCount,
                        update = model.Update,
                        notUpdate = model.NotUpdate,
                        recordsFiltered = totalrows//totalrowsafterfiltering
                    });
                }
                else
                {
                    return Json(new
                    {
                        data = model.DocumentUploadList,
                        draw = filter1.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows,
                        allTabCount = model.AllTabCount,
                        update=model.Update,
                        notUpdate=model.NotUpdate
                    });
                }


            }
            catch (Exception ex)
            {
                return View();
            }




        }



     
        public async Task<IActionResult> AddDocument(Guid EmployeeGI)
        {
            ViewBag.Document = new SelectList(await _dropdownService.GetOtherDocumentsDropDown(0), "Code", "Name");
            DocumentUpload model = new DocumentUpload();
            model.EmployeeGI = EmployeeGI;
            return PartialView("_AddDocument", model);
        }
        #region OTHER DOC+UMENTS
        public async Task<IActionResult> GetOtherDocumentsList(Guid EmployeeGI)
        {
            DocumentUpload model = new DocumentUpload();
            model = await _documentUploadService.GetOtherDocumentList(EmployeeGI);
            return PartialView("_OtherDocumentsList", model);
        }
        public async Task<IActionResult> DeleteOtherDocuments(Guid LogGI, Guid OtherDocGI, Guid EmployeeGI)
        {
            var response = await _documentUploadService.OtherDocumentDelete(LogGI, OtherDocGI);
            if (response.Success == true)
            {
                TempData["Message"] = "Deleted Successfully";
                return RedirectToAction("Create", new { EmployeeGI = EmployeeGI });
            }
            return RedirectToAction("Create", new { EmployeeGI = EmployeeGI });
        }
        public async Task<IActionResult> EditOtherDocuments(Guid OtherDocGI,Guid LogGI, Guid EmployeeGI)
        {
            DocumentUpload model = new DocumentUpload();
            model = await _documentUploadService.GetOtherDocumentDetails(OtherDocGI, LogGI);
            model.Mode = "MODIFY";
            model.EmployeeGI = EmployeeGI;
            model.SliderDocumentGI = OtherDocGI;
            model.SliderDocumentLogGI = LogGI;
            var docname = (await _dropdownService.GetDocUploadDocTypeDropDown("", model.SliderDocument.Code));
            model.docname = docname[0].Name;
            ViewBag.Document = new SelectList(await _dropdownService.GetOtherDocumentsDropDown(model.SliderDocument.Code), "Code", "Name");
            return PartialView("_EditOtherDocument", model);
        }
        [HttpPost]
        public async Task<IActionResult> OtherDocumentSave(DocumentUpload model)
        {
            var StorageType = await _commonService.GetFileStorageType();
            //string filename = null;

            if (model.File != null)
            {
                if (model.File.FileData != null)
                {
                    model.File.fdata = await _helpers.CompressFile(model.File.FileData);
                    model.File.FileName = model.File.FileData.FileName;
                    model.File.FileData = null;
                }
            }
            if (model.Mode != "MODIFY")
                model.Mode = "CREATE";

            var response = await _documentUploadService.OtherDocumentSave(model);
            if (response.Success == true)
            {
                if (model.Mode == "CREATE")
                    response.Message = "Saved Successfully";
                else
                    response.Message = "Modified Successfully";
                return Json(response);
            }
            else
            {
                //bool val = await _blob.Delete(filename);
                return Json(response);
            }
        }
        #endregion
        #region IDENTITY DOCUMENTS0
        public async Task<IActionResult> DeleteIdentityDocuments(Guid EmployeeGI, string SliderTitle)
        {
            string DocType;
            if (SliderTitle == "Aadhar Upload")
            {
                DocType = "Aadhaar";
            }
            else if (SliderTitle == "Pan Card")
            {
                DocType = "Pan";
            }
            else if (SliderTitle == "Passport")
            {
                DocType = "Passport";
            }
            else if (SliderTitle == "Driving License")
            {
                DocType = "Driving License";
            }
            else
            {
                DocType = "Other";
            }
            var response = await _documentUploadService.IdentityDocumentDelete(EmployeeGI, DocType);
            if (response.Success == true)
            {
                TempData["Message"] = "Deleted Successfully";
                return RedirectToAction("Create", new { EmployeeGI = EmployeeGI });
            }
            return RedirectToAction("Create", new { EmployeeGI = EmployeeGI });
        }
        public async Task<IActionResult> UploadAadharOrPan(string DocumentName,Guid EmployeeGI)
        {
            DocumentUpload model = new DocumentUpload();
            model = await _documentUploadService.GetIdentityDocumentDetails(EmployeeGI, DocumentName);
           
            if(DocumentName== "Aadhaar")
            {
                if (model.NameInAadhar != null)
                {
                    model.Mode = "MODIFY";
                }
                model.SliderTitle = "Aadhar Upload";
            }
            else
            {
                if (model.NameInPAN != null)
                {
                    model.Mode = "MODIFY";
                }
                model.SliderTitle = "Pan Card";
            }            
            model.EmployeeGI = EmployeeGI;
            return PartialView("_UploadAadharOrPan",model);
        }
        [HttpGet]
        public async Task<JsonResult> AlreadyExistCheck(Guid EmployeeGI, string PAN, string Aadhar)
        {
            var result = await _enrollmentService.AlreadyExistCheck(EmployeeGI, "", PAN, Aadhar,"","","","","","");
            return Json(result);
        }
        public async Task<IActionResult> UploadDrivingLicenseOrPassport(string DocumentName, Guid EmployeeGI)
        {
            DocumentUpload model = new DocumentUpload();
            model = await _documentUploadService.GetIdentityDocumentDetails(EmployeeGI, DocumentName);
            if (DocumentName == "Driving License")
            {
                if (model.NameInDrivingLicense != null)
                {
                    model.Mode = "MODIFY";
                }
                model.SliderTitle = "Driving License";
            }
            else
            {
                if (model.NameInPassport != null)
                {
                    model.Mode = "MODIFY";
                }
                model.SliderTitle = "Passport";
            }
            model.EmployeeGI = EmployeeGI;
            return PartialView("_UploadLicenseOrPassport", model);
        }


        [HttpPost]
        public async Task<IActionResult> IdentityDocumentSave(DocumentUpload model)
        {
            var StorageType = await _commonService.GetFileStorageType();
            try
            {
                if (model.File != null)
                {
                    if (model.File.FileData != null)
                    {
                        model.File.fdata = await _helpers.CompressFile(model.File.FileData);
                        model.File.FileName = model.File.FileData.FileName;
                        model.File.FileData = null;
                    }
                }

                if (model.Mode != "MODIFY")
                    model.Mode = "CREATE";

                var response = await _documentUploadService.IdentityDocumentSave(model);

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
        #endregion
      
        public async Task<IActionResult> ViewDoc(Guid EmployeeGI, string DocType, string IdentityDocType, Guid OtherDocGI)
        {
            var fileData = await _documentUploadService.GetDocumentFileData(EmployeeGI, DocType, IdentityDocType, OtherDocGI);
            if (fileData == null || fileData.fdata == null)
                return BadRequest(new { message = "No File Uploaded" });
            var fileBytes = await _helpers.DecompressIfNeeded(fileData.fdata);
            //var decompressedBytes = await _helpers.DecompressFile(fileData.fdata);
            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(fileData.FileName, out string contentType))
            {
                // default if unknown
                contentType = "application/octet-stream";
            }
            Response.Headers["Content-Disposition"] = $"inline; filename=\"{fileData.FileName}\"";
            return File(fileBytes, contentType);
        }    



        [HttpGet]
        public async Task<JsonResult> GetDocument(int id)
        {
            var Qualification = (await _dropdownService.GetDocUploadDocTypeDropDown("",id));
            return Json(Qualification[0].Name);
        }


      
        public async Task<IActionResult> ExportToExcel(int TabIndex, int SearchId, int totRecords,Filter filter)
        {
            try
            {

                
                    filter.length = totRecords;
                    filter.start = 0;
               
                string FilterValues = await _helpers.GetFilterValues(filter);

                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();

                string HeaderName;

                   if (TabIndex == 0)
                       HeaderName = "Documentation(All)";
                else if (TabIndex == 1)
                        HeaderName = "Documentation(Completed)";
                    else
                        HeaderName = "Documentation(Pending)";

                EP = await _commonService.GetExcelPassword("Documentation("+ HeaderName + ")", "Excel");

                if (FilterValues == "")
                {
                    FilterValues = "All";
                }

                string currDateTime = await _helpers.GetCurrentDate();
                

                //Get Datset
                var dataset = await _documentUploadService.GetDocumentUploadList_Excel(TabIndex, SearchId,filter);
                DataTable dt = dataset.Tables[0];


                //set EXCEL Column Heading 
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (dt.Columns[i].ColumnName == "doj" || dt.Columns[i].ColumnName == "dob" || dt.Columns[i].ColumnName == "y/s")
                        dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.ToUpper();
                    else
                        dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
                }

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add(HeaderName);
                    int count = dt.Rows.Count + 5;


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
                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191); ;
                    wsReportNameHeaderRange.Value = HeaderName ;

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
                    worksheet.Range("B4:K4").Merge();
                    worksheet.Cell("B4").Value = FilterValues;



                    var currentRow = 5;

                    worksheet.Cell(currentRow, 1).InsertTable(dt);
                    worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                    worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                    worksheet.Range("A5:K5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Range("A5:K5").Style.Font.Bold = true;


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
                            "(" + HeaderName + ")  List.xlsx"
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


        //public async Task<IActionResult> DownloadAllPan()
        //{
        //    // Fetch all PAN documents
        //    var panList = await _documentUploadService.GetAllPanDocuments();

        //    if (panList == null || !panList.Any())
        //        return BadRequest("No PAN files found");

        //    // Folder inside project
        //    string folderPath = Path.Combine(Directory.GetCurrentDirectory(),
        //                                     "wwwroot",
        //                                     "PanFiles");

        //    // Create folder if not exists
        //    if (!Directory.Exists(folderPath))
        //        Directory.CreateDirectory(folderPath);

        //    foreach (var item in panList)
        //    {
        //        if (item.fdata == null)
        //            continue;

        //        byte[] fileBytes = await _helpers.DecompressIfNeeded(item.fdata);

        //        string extension = Path.GetExtension(item.FileName);

        //        // EmployeeCode as filename
        //        string fileName = item.EmployeeCode + extension;

        //        string fullPath = Path.Combine(folderPath, fileName);

        //        await System.IO.File.WriteAllBytesAsync(fullPath, fileBytes);
        //    }

        //    // Create ZIP
        //    string zipPath = Path.Combine(Directory.GetCurrentDirectory(),
        //                                  "wwwroot",
        //                                  "PanFiles.zip");

        //    if (System.IO.File.Exists(zipPath))
        //        System.IO.File.Delete(zipPath);

        //    ZipFile.CreateFromDirectory(folderPath, zipPath);

        //    byte[] zipBytes = await System.IO.File.ReadAllBytesAsync(zipPath);

        //    return File(zipBytes, "application/zip", "PanFiles.zip");
        //}


public async Task<IActionResult> DownloadAllPan()
    {
        var panList = await _documentUploadService.GetAllPanDocuments();

        if (panList == null || !panList.Any())
            return BadRequest("No PAN files found");

        using (var zipStream = new MemoryStream())
        {
            using (var archive = new ZipArchive(zipStream, ZipArchiveMode.Create, true))
            {
                foreach (var item in panList)
                {
                    if (item.fdata == null)
                        continue;

                    byte[] fileBytes = await _helpers.DecompressIfNeeded(item.fdata);

                    string extension = Path.GetExtension(item.FileName);

                    string fileName = item.EmployeeCode + extension;

                    var zipEntry = archive.CreateEntry(fileName, CompressionLevel.Fastest);

                    using (var entryStream = zipEntry.Open())
                    {
                        await entryStream.WriteAsync(fileBytes, 0, fileBytes.Length);
                    }
                }
            }

            zipStream.Position = 0;

            return File(
                zipStream.ToArray(),
                "application/zip",
                "PanFiles.zip"
            );
        }
    }
}
}
