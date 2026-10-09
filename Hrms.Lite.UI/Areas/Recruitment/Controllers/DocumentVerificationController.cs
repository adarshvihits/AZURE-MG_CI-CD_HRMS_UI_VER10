using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.EMMA;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.Recruitment;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Recruitment;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Spire.Xls;

namespace Hrms.Lite.UI.Areas.Recruitment.Controllers
{
    [Area("Recruitment")]
    [SessionAuthorize]

    public class DocumentVerificationController : Controller
    {
        #region private

        private readonly IDocumentVerificationService _documentVerificationService;
        private readonly IDropdownService _dropdownService;
        private readonly ICommonService _commonService;
        private readonly IBlob _blob;
        private readonly IHelpers _helpers;
        #endregion
        #region constr
        public DocumentVerificationController(IDocumentVerificationService documentVerificationService,ICommonService commonService, IDropdownService dropdownService,IBlob blob, IHelpers helpers)
        {
            _documentVerificationService = documentVerificationService;
            _commonService = commonService;
            _dropdownService = dropdownService;
            _blob = blob;
            _helpers = helpers;
        }
        #endregion
        public async Task<IActionResult> Index()
        {
            try { 
            DocumentVerification model = new DocumentVerification();
            model = await _documentVerificationService.GetDocumentVerificationList();
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
        public async Task<IActionResult> GetDocumentVerification(Guid VacancyManagementGI,int CandidateId, int TrackingCode)
        {
            try { 
            DocumentVerification model = new DocumentVerification();
            DocumentVerification m = new DocumentVerification();
            model = await _documentVerificationService.GetDocumentVerificationDetails(VacancyManagementGI, CandidateId, TrackingCode);
            m = await _documentVerificationService.GetOtherDocumentList(model.EmployeeGI);
            model.OtherDocList = new List<OtherDocList>();
            model.OtherDocList = m.OtherDocList;
            model.SslcDescription = m.SslcDescription;
            model.HseDescription = m.HseDescription;
            model.GraduationDescription = m.GraduationDescription;
            model.PgDescription = m.PgDescription;
            model.SslcStatus = m.SslcStatus;
            model.HseStatus = m.HseStatus;
            model.GraduationStatus = m.GraduationStatus;
            model.PgStatus = m.PgStatus;
            return View("~/Areas/Recruitment/Views/DocumentVerification/Create.cshtml",model);
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
        public async Task<IActionResult> GetOtherDocList(Guid EmployeeGI)
        {
            try { 
            DocumentVerification model = new DocumentVerification();
            model = await _documentVerificationService.GetOtherDocumentList(EmployeeGI);
            return PartialView("_OtherDocumentsList", model);
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
        public async Task<IActionResult> UploadAadharOrPan(string DocumentName, Guid ApplicantGI)
        {
            try { 
            DocumentVerification model = new DocumentVerification();
            model = await _documentVerificationService.GetIdentityDocumentDetails(ApplicantGI, DocumentName);

            if (DocumentName == "Aadhaar")
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
            model.EmployeeGI = ApplicantGI;
            model.DocType = DocumentName;
            return PartialView("_AadharOrPan", model);
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

        public async Task<IActionResult> ViewAadharOrPan(string DocumentName, Guid ApplicantGI,string imagePath, int TrackingCode)
        {
            try { 
            DocumentVerification model = new DocumentVerification();
            model = await _documentVerificationService.GetIdentityDocumentDetails(ApplicantGI, DocumentName);
            if (DocumentName == "Aadhaar")
            {
                model.AadharUrl = imagePath;
                model.SliderTitle = "Aadhaar";
            }
            else if(DocumentName == "Pan")
            {
                model.PANUrl = imagePath;
                model.SliderTitle = "Pan Card";
            }
           else if (DocumentName == "Driving License")
            {
                model.DLUrl = imagePath;
                model.SliderTitle = "Driving License";
            }
            else if (DocumentName == "Passport")
            {
                model.PassportUrl = imagePath;
                model.SliderTitle = "Passport";
            }
               
            model.EmployeeGI = ApplicantGI;
            model.DocType = DocumentName;
                model.ApplicantTrackingCode = TrackingCode;
            return PartialView("_documentView", model);
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

        public async Task<IActionResult> UploadDrivingLicenseOrPassport(string DocumentName, Guid ApplicantGI, int TrackingCode )
        {
            try { 
            DocumentVerification model = new DocumentVerification();
            model = await _documentVerificationService.GetIdentityDocumentDetails(ApplicantGI, DocumentName);
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
            model.EmployeeGI = ApplicantGI;
            model.DocType = DocumentName;
                model.ApplicantTrackingCode = TrackingCode;
            return PartialView("_LicenseOrPassport", model);
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

        public async Task<IActionResult> AddDocument(Guid EmployeeGI)
        {
            try { 
            ViewBag.DocumentType = new SelectList(await _dropdownService.GetDocUploadDocTypeDropDown("OTHER_DOCUMENTS",null), "Code", "Name");
            DocumentVerification model = new DocumentVerification();
            model.EmployeeGI = EmployeeGI;
            return PartialView("_AddDocument", model);
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
        public async Task<IActionResult> EditOtherDocuments(string OtherDocName,Guid EmployeeGI, int TrackingCode)
        {
            try { 
            DocumentVerification model = new DocumentVerification();
            model = await _documentVerificationService.GetOtherDocumentDetails(OtherDocName, EmployeeGI);
            model.Mode = "MODIFY";
            //model.SliderDocumentGI = OtherDocGI;  
            ViewBag.DocumentType = new SelectList(await _dropdownService.GetDocUploadDocTypeDropDown("OTHER_DOCUMENTS",null), "Code", "Name");
            model.SliderDocumentType.Code = 6;
            model.DocType = OtherDocName;
                model.ApplicantTrackingCode = TrackingCode;
            return PartialView("_OtherDocumentView", model);
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



        public async Task<IActionResult> IdentityDocumentSave(DocumentVerification model)
        {
            var StorageType = await _commonService.GetFileStorageType();
            try { 
            string filename = null;

                if (model.File != null)
                {
                    if (model.File.FileData != null)
                    {
                        if (StorageType.Data == "BLOB")
                        {

                            var fileobj = await _commonService.UploadIformFileToBLOB(model.File, "Documents");
                            if (fileobj.Success == false)
                            {
                                fileobj.Message = "FILE UPLOAD ERROR";
                                return Json(fileobj);
                            }
                            model.File = fileobj.Data;

                        }
                        else if (StorageType.Data == "IIS")
                        {
                            var fileobj = await _commonService.UploadIformFileToIIS(model.File, "Documents");
                            if (fileobj.Success == false)
                            {
                                fileobj.Message = "FILE UPLOAD ERROR";
                                return Json(fileobj);
                            }
                            model.File = fileobj.Data;

                        }
                        else
                        {
                            var fileobj = await _commonService.UploadIformFileToAWS(model.File, "Documents");
                            if (fileobj.Success == false)
                            {
                                fileobj.Message = "FILE UPLOAD ERROR";
                                return Json(fileobj);
                            }
                            model.File = fileobj.Data;

                        }
                    }
                }



                // model.File = await _blob.UploadToAzureAsync(model.File);
                //filename = model.File.FileName;
                //model.File.FileData = null;
            

            if (model.Mode != "MODIFY")
                model.Mode = "CREATE";

            var response = await _documentVerificationService.ApplicantDocumentUploadSave(model,model.DocType);
            //if (response.Success == true)
            //{
            //    if (model.Mode == "CREATE")
            //        TempData["Message"] = "Saved Successfully";
            //    else
            //        TempData["Message"] = "Modified Successfully";
            //    //return RedirectToAction("Create",new { EmployeeGI = model.EmployeeGI });
            //    return Json(response);
            //}
            //else
            //{
            //    TempData["Message"] = response.Message;
            //    bool val = await _blob.Delete(filename);
            //    //return RedirectToAction("Create", new { EmployeeGI = model.EmployeeGI });
            //    return Json(response);
            //}
            //return View("~/Areas/DataBank/Views/DocumentUpload/Create.cshtml", model);
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

        public async Task<IActionResult> DocumentApprove(DocumentVerification model)
        {
            try { 
             var response = await _documentVerificationService.DocumentApprove(model, model.DocType, model.ApplicantTrackingCode);
            
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
        public async Task<IActionResult> OtherDocumentSave(DocumentVerification model)
        {
            var StorageType = await _commonService.GetFileStorageType();
            try { 
            string filename = null;

                if (model.File != null)
                {
                    if (model.File.FileData != null)
                    {
                        if (StorageType.Data == "BLOB")
                        {

                            var fileobj = await _commonService.UploadIformFileToBLOB(model.File, "Documents");
                            if (fileobj.Success == false)
                            {
                                fileobj.Message = "FILE UPLOAD ERROR";
                                return Json(fileobj);
                            }
                            model.File = fileobj.Data;

                        }
                        else if (StorageType.Data == "IIS")
                        {
                            var fileobj = await _commonService.UploadIformFileToIIS(model.File, "Documents");
                            if (fileobj.Success == false)
                            {
                                fileobj.Message = "FILE UPLOAD ERROR";
                                return Json(fileobj);
                            }
                            model.File = fileobj.Data;

                        }
                        else
                        {
                            var fileobj = await _commonService.UploadIformFileToAWS(model.File, "Documents");
                            if (fileobj.Success == false)
                            {
                                fileobj.Message = "FILE UPLOAD ERROR";
                                return Json(fileobj);
                            }
                            model.File = fileobj.Data;

                        }
                    }
                }
                
               
                //model.File = await _blob.UploadToAzureAsync(model.File);
                //filename = model.File.FileName;
                //model.File.FileData = null;
          

            if (model.Mode != "MODIFY")
                model.Mode = "CREATE";

            var response = await _documentVerificationService.ApplicantOtherDocumentUploadSave(model);
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
            catch (UnauthorizedAccessException e)
            {

                return RedirectToAction("Login", "Account", new { Area = "" });
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", "Account", new { Area = "" });
            }
        }

        public async Task<IActionResult> DeleteIdentityDocuments(Guid EmployeeGI, string SliderTitle)
        {
            try { 
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
            var response = await _documentVerificationService.IdentityDocumentDelete(EmployeeGI, DocType);
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

        public async Task<IActionResult> DeleteOtherDocuments(Guid OtherDocGI)
        {
            try { 
            var response = await _documentVerificationService.OtherDocumentDelete(OtherDocGI);
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


        public async Task<IActionResult> Verify(int CandidateID,int status)
        {
            try { 
            DocumentVerification model = new DocumentVerification();
            model.CandidateID = CandidateID;
            model.Verficationstatus = status;
            var response = await _documentVerificationService.Verify(model);
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
            try { 
            DocumentVerification model = new DocumentVerification();
            Filter filter = new Filter();
            int slNo = 0;
            //if (Global.GlobalFilter == null)
            //    Global.GlobalFilter = new Filter();
            //string FilterValues = await _helpers.GetFilterValues(Global.GlobalFilter);
            model = await _documentVerificationService.GetDocumentVerificationList();
            //if (FilterValues == "")
            //{
            //    FilterValues = "All";
            //}
            string currDateTime = await _helpers.GetCurrentDate();
            

            var LoginUser = HttpContext.Session.GetString("LoginUser");
            var CompanyName = HttpContext.Session.GetString("CompanyName");
            ExcelPassword EP = new ExcelPassword();
            EP = await _commonService.GetExcelPassword("Document Verification", "Excel");
           

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Document Verification List");
                    int count = model.Document_List.Count + 4;


                    worksheet.Range("A1:N" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:N" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:N" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:N" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:N" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;

                    var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 13)));
                    wsReportNameHeaderRange.Style.Font.Bold = true;
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Merge();
                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191); ;
                    wsReportNameHeaderRange.Value = "Document Verification";

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
                    worksheet.Cell(3, 5).Style.Font.Bold = true;
                    worksheet.Cell(3, 5).Value = "Total Count";
                    worksheet.Cell(3, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(3, 6).Value = model.Document_List.Count;
                    //worksheet.Cell(4, 1).Style.Font.Bold = true;
                    ////worksheet.Cell(4, 1).Value = "Filter Applied";
                    //worksheet.Range("B4:M4").Merge();
                    //worksheet.Cell("B4").Value = FilterValues;
                    var currentRow = 4;
                    #region Header
                    worksheet.Cell(currentRow, 1).Value = "Slno.";
                    worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 2).Value = "Appli.ID";
                    worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 3).Value = " Applicant Name";
                    worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 4).Value = "MRF No.";
                    worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 5).Value = "Job Title";
                    worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 6).Value = "Deadline";
                    worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 7).Value = "D.L.";
                    worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 8).Value = "Location";
                    worksheet.Cell(currentRow, 8).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 9).Value = "Age";
                    worksheet.Cell(currentRow, 9).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 10).Value = "Y / E";
                    worksheet.Cell(currentRow, 10).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 11).Value = "Aadhar";
                    worksheet.Cell(currentRow, 11).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 12).Value = " PAN ";
                    worksheet.Cell(currentRow, 12).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 13).Value = "DL.";
                    worksheet.Cell(currentRow, 13).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 14).Value = "Other";
                    worksheet.Cell(currentRow, 14).Style.Font.Bold = true;





                    #endregion

                    #region Body
                    foreach (var item in model.Document_List)
                    {
                        currentRow++;
                        slNo = slNo + 1;
                        //DateTime RequestDte = (DateTime)item.IssuedDate;
                        worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Range("A4:N4").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                        worksheet.Cell(currentRow, 1).Value = slNo;
                        worksheet.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 2).Value = item.RefNo;
                        worksheet.Cell(currentRow, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 3).Value = item.CandidateName;
                        worksheet.Cell(currentRow, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 4).Value = item.JobCode;
                        worksheet.Cell(currentRow, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 5).Value = item.JobTitle;
                        worksheet.Cell(currentRow, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 6).Style.DateFormat.Format = "dd-MMM-yyyy";

                        worksheet.Cell(currentRow, 6).Value = item.NeedByDate;
                        worksheet.Cell(currentRow, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 7).Value = item.DaysLeft;
                        worksheet.Cell(currentRow, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 8).Value = item.Location.Name;
                        worksheet.Cell(currentRow, 9).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 9).Value = item.Age;
                        worksheet.Cell(currentRow, 10).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 10).Value = item.Experience;
                        worksheet.Cell(currentRow, 11).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 11).Value = item.Aadhar;
                        worksheet.Cell(currentRow, 12).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 12).Value = item.PAN;
                        worksheet.Cell(currentRow, 13).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 13).Value = item.DrivingLicense;
                        worksheet.Cell(currentRow, 14).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 14).Value = item.Others;
                        worksheet.Columns(1, 14).Style.Alignment.WrapText = true;

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
                            "Document Verification List.xlsx"
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
