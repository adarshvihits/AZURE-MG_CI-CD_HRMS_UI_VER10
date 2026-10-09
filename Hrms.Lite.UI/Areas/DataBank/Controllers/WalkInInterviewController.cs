using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SkiaSharp;
using Spire.Xls;

namespace Hrms.Lite.UI.Areas.DataBank.Controllers
{
    [Area("DataBank")]
    [SessionAuthorize]
    public class WalkInInterviewController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly ICommonService _commonService;
        private readonly IBlob _blob;
        private readonly IWalkInInterviewService _walkInInterviewService;
        private IHostingEnvironment Environment;
        private readonly IHelpers _helpers;
        //public static class Global
        //{            
        //    public static string SavedFileName;
        //    public static WalkInInterView model;
        //    public static bool isRefreshed = true;
        //    public static Guid VacancyManagementGI;
        //}
        public WalkInInterviewController(IDropdownService dropdownService, ICommonService commonService, IWalkInInterviewService walkInInterviewService,IBlob blob, IHostingEnvironment _environment, IHelpers helpers)
        {
            _dropdownService = dropdownService;
            _commonService = commonService;
            _walkInInterviewService = walkInInterviewService;
            _blob = blob;
            Environment = _environment;
            _helpers = helpers;
        }
        public async Task<IActionResult> Index()
        {
            WalkInInterView model = new WalkInInterView();
            model = await _walkInInterviewService.GetVacancyList();
            return View(model);
        }
        public async Task<IActionResult> Create(Guid VacancyManagementGI)
        {
                            
            WalkInInterView model = new WalkInInterView();
            WalkInInterView m = new WalkInInterView();
            m = await _walkInInterviewService.GetVacancyManagementDetails(VacancyManagementGI);
                

            model.CandidateDetails = new WalkInInterView_CandidateDetails();
            model.CandidateDetails.FresherOrExperienced = 0;
            var RefNo = HttpContext.Session.GetString("RefNumber");
            if (RefNo == "" || RefNo == null)
            {
                var response = await _commonService.GetReferenceNumber("Resume Databank");
                RefNo = response.Data;
                HttpContext.Session.SetString("RefNumber", RefNo);

            }
                   
            //var refNo = await _commonService.GetReferenceNumber("Resume Databank");
            model.CandidateDetails.RefNo = RefNo;
            model.CandidateDetails.CandidatePhoto = new Shared.File();
              
            model.VacancyDetails = m.VacancyDetails;
            await fillDropdown();
            return View(model);
          
        }
        public IActionResult SaveProfileToFolder(WalkInInterView model)
        {
            
            //Global.SelectedTab = SelectedTab;
            string path = Path.Combine(this.Environment.WebRootPath, "Personal_Profile");

            string fileName = model.CandidateDetails.CandidatePhoto.FileData.FileName;
            var Imgstream = model.CandidateDetails.CandidatePhoto.FileData.OpenReadStream();
            dynamic originalImage;
            try
            {
                System.Drawing.Image oImage = System.Drawing.Image.FromStream(Imgstream);
                originalImage = oImage;
            }
            catch (Exception e)
            {
                var wImage = SKBitmap.Decode(Imgstream);
                var oImage = wImage;
                originalImage = oImage;
            }
            if (model.CandidateDetails.CandidatePhoto.FileData.Length > (1024 * 1024) || (originalImage.Width > 1920 && originalImage.Height > 1080))
            {
                //----------------compress image --------------------------------------
              

                // Set the maximum width and height for the compressed image
                int maxWidth = 800;
                int maxHeight = 600;

                // Calculate the new size while maintaining the aspect ratio
                int newWidth, newHeight;
                if (originalImage.Width > originalImage.Height)
                {
                    newWidth = maxWidth;
                    newHeight = (int)((double)originalImage.Height / originalImage.Width * maxWidth);
                }
                else
                {
                    newHeight = maxHeight;
                    newWidth = (int)((double)originalImage.Width / originalImage.Height * maxHeight);
                }
                var stream2 = model.CandidateDetails.CandidatePhoto.FileData.OpenReadStream();
                System.Drawing.Image resized = resizeImage(newWidth, newHeight, stream2);
                using var imageStream = new MemoryStream();
                resized.Save(imageStream, ImageFormat.Jpeg);
                var imageBytes = imageStream.ToArray();
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                System.Drawing.Image finalImage = System.Drawing.Image.FromStream(imageStream);
                finalImage.Save(Path.Combine(path, fileName), ImageFormat.Jpeg);
                ViewBag.JsonData = $"data:image/png;base64,{Convert.ToBase64String(imageBytes)}";
                //Global.jsonD = $"data:image/png;base64,{Convert.ToBase64String(imageBytes)}";
                //----------------------------------------------------------------------
            }
            else
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                {
                    model.CandidateDetails.CandidatePhoto.FileData.CopyTo(stream);
                }
            }            
            ViewBag.fileName = fileName;
            return PartialView("_Profile");
        }

        public System.Drawing.Image resizeImage(int width, int height, Stream resourceImage)
        {
            try
            {
                var image = System.Drawing.Image.FromStream(resourceImage);
                var thumb = image.GetThumbnailImage(width, height, () => false, IntPtr.Zero);

                return thumb;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public IActionResult LoadProfilePic_In_KendoUI(string fileName)
        {
            var serverpath = Path.Combine(Environment.WebRootPath, "Personal_Profile\\" + fileName);
            var contents = System.IO.File.ReadAllBytes(serverpath);
            return Json($"data:image/png;base64,{Convert.ToBase64String(contents)}");

        }

        [AcceptVerbs("Post")]
        public async Task<ActionResult> SaveProfilePicFromKendo(string contentType, string base64, string fileName)
        {
            var serverpath = Path.Combine(Environment.WebRootPath, "Personal_Profile\\" + fileName);
            try
            {
                var fileContents = Convert.FromBase64String(base64);
                WalkInInterView model = new WalkInInterView();
                //ResumeDataBank model1 = new ResumeDataBank();                
                model.CandidateDetails = new WalkInInterView_CandidateDetails();
                model.CandidateDetails.CandidatePhoto = new Shared.File();
                model.CandidateDetails.CandidatePhoto.fdata = fileContents;
                model.CandidateDetails.CandidatePhoto.FileName = fileName;
                model.CandidateDetails.CandidatePhoto.ContentType = contentType;
                model.CandidateDetails.CandidatePhoto.FileExtension = Path.GetExtension(fileName);


                var StorageType = await _commonService.GetFileStorageType();
                if (model.CandidateDetails.CandidatePhoto != null)
                {
                    if (model.CandidateDetails.CandidatePhoto.fdata != null)
                    {
                        if (StorageType.Data == "BLOB")
                        {

                            var fileobj = await _commonService.UploadByteArrayToBLOB(model.CandidateDetails.CandidatePhoto, "ProfilePhoto");
                            if (fileobj.Success == false)
                            {
                                fileobj.Message = "FILE UPLOAD ERROR";
                                return Json(fileobj);
                            }
                            model.CandidateDetails.CandidatePhoto = fileobj.Data;

                        }
                        else if (StorageType.Data == "IIS")
                        {
                            var fileobj = await _commonService.UploadByteArrayToIIS(model.CandidateDetails.CandidatePhoto, "ProfilePhoto");
                            if (fileobj.Success == false)
                            {
                                fileobj.Message = "FILE UPLOAD ERROR";
                                return Json(fileobj);
                            }
                            model.CandidateDetails.CandidatePhoto = fileobj.Data;

                        }
                        else
                        {
                            var fileobj = await _commonService.UploadByteArrayToAWS(model.CandidateDetails.CandidatePhoto, "ProfilePhoto");
                            if (fileobj.Success == false)
                            {
                                fileobj.Message = "FILE UPLOAD ERROR";
                                return Json(fileobj);
                            }
                            model.CandidateDetails.CandidatePhoto = fileobj.Data;

                        }
                    }
                }
                                          

                if (System.IO.File.Exists(serverpath))
                {
                    System.IO.File.Delete(serverpath);
                }


                //return Json(true);
                //return PartialView("_EnrollmentHeader", model);
                return Json(model.CandidateDetails.CandidatePhoto);
            }
            catch (UnauthorizedAccessException e)
            {
                if (System.IO.File.Exists(serverpath))
                {
                    System.IO.File.Delete(serverpath);
                }
                return RedirectToAction("Login", "Account", new { Area = "" });
            }
            catch (Exception e)
            {
                if (System.IO.File.Exists(serverpath))
                {
                    System.IO.File.Delete(serverpath);
                }
                return RedirectToAction("Error", "Account", new { Area = "" });
            }
        }

        public async Task<IActionResult> GetMappedResumePopUp(Guid VacancyGI)
        {
            try
            {
                //VacancyManagement model = new VacancyManagement();
                ResumeDataBank model = new ResumeDataBank();

                model = await _commonService.GetMappedResumeDetails(VacancyGI);

                return PartialView("_ResumeDetailsPopup", model);
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

        public async Task fillDropdown()
        {
            ViewBag.Salutation = new SelectList(await _dropdownService.GetSalutationDropDown(), "Code", "Name");
            ViewBag.Gen = new SelectList(await _dropdownService.GetGenderDropDown(), "Code", "Name");
            ViewBag.Qual = new SelectList(await _dropdownService.GetAllQualificationDropDown(), "Code", "Name");
            ViewBag.Interviewer = new SelectList(await _dropdownService.GetEmployeeDropDown(), "Code", "Name");
            ViewBag.Status = new SelectList(await _dropdownService.GetInterviewSelectionDropdown(), "Code", "Name");
        }
        public async Task<IActionResult> Save(WalkInInterView model)
        {
            var StorageType = await _commonService.GetFileStorageType();
            try
            {


                if (model.CandidateDetails.File != null && model.CandidateDetails.File.FileData != null)
                {
                    if (StorageType.Data == "BLOB")
                    {

                        var fileobj = await _commonService.UploadIformFileToBLOB(model.CandidateDetails.File, "Documents");
                        if (fileobj.Success == false)
                        {
                            fileobj.Message = "FILE UPLOAD ERROR";
                            return Json(fileobj);
                        }
                        model.CandidateDetails.File = fileobj.Data;

                    }
                    else if (StorageType.Data == "IIS")
                    {
                        var fileobj = await _commonService.UploadIformFileToIIS(model.CandidateDetails.File, "Documents");
                        if (fileobj.Success == false)
                        {
                            fileobj.Message = "FILE UPLOAD ERROR";
                            return Json(fileobj);
                        }
                        model.CandidateDetails.File = fileobj.Data;

                    }
                    else
                    {
                        var fileobj = await _commonService.UploadIformFileToAWS(model.CandidateDetails.File, "Documents");
                        if (fileobj.Success == false)
                        {
                            fileobj.Message = "FILE UPLOAD ERROR";
                            return Json(fileobj);
                        }
                        model.CandidateDetails.File = fileobj.Data;

                    }
                    //model.CandidateDetails.File = await _blob.UploadToAzureAsync(model.CandidateDetails.File);
                    //model.CandidateDetails.File.FileData = null;
                }
                if (model.CandidateDetails.CandidatePhoto != null && model.CandidateDetails.CandidatePhoto.FileData != null)
                {
                    if (StorageType.Data == "BLOB")
                    {

                        var fileobj = await _commonService.UploadIformFileToBLOB(model.CandidateDetails.CandidatePhoto, "Documents");
                        if (fileobj.Success == false)
                        {
                            fileobj.Message = "FILE UPLOAD ERROR";
                            return Json(fileobj);
                        }
                        model.CandidateDetails.CandidatePhoto = fileobj.Data;

                    }
                    else if (StorageType.Data == "IIS")
                    {
                        var fileobj = await _commonService.UploadIformFileToIIS(model.CandidateDetails.CandidatePhoto, "Documents");
                        if (fileobj.Success == false)
                        {
                            fileobj.Message = "FILE UPLOAD ERROR";
                            return Json(fileobj);
                        }
                        model.CandidateDetails.CandidatePhoto = fileobj.Data;

                    }
                    else
                    {
                        var fileobj = await _commonService.UploadIformFileToAWS(model.CandidateDetails.CandidatePhoto, "Documents");
                        if (fileobj.Success == false)
                        {
                            fileobj.Message = "FILE UPLOAD ERROR";
                            return Json(fileobj);
                        }
                        model.CandidateDetails.CandidatePhoto = fileobj.Data;

                    }



                    //model.CandidateDetails.CandidatePhoto = await _blob.UploadToAzureAsync(model.CandidateDetails.CandidatePhoto);
                    //model.CandidateDetails.CandidatePhoto.FileData = null;
                }
                if (model.CandidateDetails.Resume != null && model.CandidateDetails.Resume.FileData != null)
                {
                    if (StorageType.Data == "BLOB")
                    {

                        var fileobj = await _commonService.UploadIformFileToBLOB(model.CandidateDetails.Resume, "Documents");
                        if (fileobj.Success == false)
                        {
                            fileobj.Message = "FILE UPLOAD ERROR";
                            return Json(fileobj);
                        }
                        model.CandidateDetails.Resume = fileobj.Data;

                    }
                    else if (StorageType.Data == "IIS")
                    {
                        var fileobj = await _commonService.UploadIformFileToIIS(model.CandidateDetails.Resume, "Documents");
                        if (fileobj.Success == false)
                        {
                            fileobj.Message = "FILE UPLOAD ERROR";
                            return Json(fileobj);
                        }
                        model.CandidateDetails.Resume = fileobj.Data;

                    }
                    else
                    {
                        var fileobj = await _commonService.UploadIformFileToAWS(model.CandidateDetails.Resume, "Documents");
                        if (fileobj.Success == false)
                        {
                            fileobj.Message = "FILE UPLOAD ERROR";
                            return Json(fileobj);
                        }
                        model.CandidateDetails.Resume = fileobj.Data;

                    }





                    //model.CandidateDetails.Resume = await _blob.UploadToAzureAsync(model.CandidateDetails.Resume);
                    //model.CandidateDetails.Resume.FileData = null;
                }
                if (model.CandidateDetails.KYCDoc != null && model.CandidateDetails.KYCDoc.FileData != null)
                {
                    if (StorageType.Data == "BLOB")
                    {

                        var fileobj = await _commonService.UploadIformFileToBLOB(model.CandidateDetails.KYCDoc, "Documents");
                        if (fileobj.Success == false)
                        {
                            fileobj.Message = "FILE UPLOAD ERROR";
                            return Json(fileobj);
                        }
                        model.CandidateDetails.KYCDoc = fileobj.Data;

                    }
                    else if (StorageType.Data == "IIS")
                    {
                        var fileobj = await _commonService.UploadIformFileToIIS(model.CandidateDetails.KYCDoc, "Documents");
                        if (fileobj.Success == false)
                        {
                            fileobj.Message = "FILE UPLOAD ERROR";
                            return Json(fileobj);
                        }
                        model.CandidateDetails.KYCDoc = fileobj.Data;

                    }
                    else
                    {
                        var fileobj = await _commonService.UploadIformFileToAWS(model.CandidateDetails.KYCDoc, "Documents");
                        if (fileobj.Success == false)
                        {
                            fileobj.Message = "FILE UPLOAD ERROR";
                            return Json(fileobj);
                        }
                        model.CandidateDetails.KYCDoc = fileobj.Data;

                    }



                    //model.CandidateDetails.KYCDoc = await _blob.UploadToAzureAsync(model.CandidateDetails.KYCDoc);
                    //model.CandidateDetails.KYCDoc.FileData = null;
                }
            
                model.CandidateDetails.InterviewSelection = new InterviewSelection();
                model.CandidateDetails.InterviewSelection.Code = 2;//pass
                var response = await _walkInInterviewService.WalkInInterViewSave(model);
                if (response.Success == true)
                {
                    HttpContext.Session.SetString("RefNumber", "");
                }
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
            try
            {
                WalkInInterView model = new WalkInInterView();

                string LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                int slNo = 0;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Walk In Interview", "Excel");
                //Filter filter = new Filter();
                //filter = JsonConvert.DeserializeObject<Filter>(TempData["FilterModel"].ToString());


                model = await _walkInInterviewService.GetVacancyList();

                int no = model.VacancyList.Count();
                string currDateTime = await _helpers.GetCurrentDate();
                
                using (var workbook = new XLWorkbook())
                {

                    var worksheet = workbook.Worksheets.Add("Walk In Interview List");


                    int count = model.VacancyList.Count + 4;


                    worksheet.Range("A1:I" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:I" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:I" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:I" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:I" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;






                    var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 8)));
                    wsReportNameHeaderRange.Style.Font.Bold = true;
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Merge();
                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                    wsReportNameHeaderRange.Value = "Walk In interview List";

                    worksheet.Cell(2, 1).Style.Font.Bold = true;
                    worksheet.Cell(2, 1).Value = "Company Name";
                    worksheet.Cell(2, 2).Value = CompanyName.ToUpper();


                    worksheet.Cell(3, 1).Style.Font.Bold = true;
                    worksheet.Cell(3, 1).Value = "Login User";
                    worksheet.Cell(3, 2).Value = LoginUser;
                    worksheet.Cell(3, 3).Style.Font.Bold = true;

                    worksheet.Cell(3, 3).Value = "Date & Time";
                    worksheet.Cell(3, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(3, 3).Style.DateFormat.Format = "dd-MMM-yyyy   h:mm AM/PM";
                    worksheet.Cell(3, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(3, 4).Value = currDateTime.ToString();
                    worksheet.Cell(3, 5).Style.Font.Bold = true;
                    worksheet.Cell(3, 5).Value = "Total Count";
                    worksheet.Cell(3, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(3, 6).Value = model.VacancyList.Count();



                    var currentRow = 4;
                    #region Header
                    worksheet.Range("A4:I4").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Cell(currentRow, 1).Value = "Sl.No";
                    worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                    //worksheet.Cell(currentRow, 2).Value = "Appli. ID";
                    //worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 2).Value = " MRF No.";
                    worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 3).Value = "Job Title";
                    worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 4).Value = "Initiated On";
                    worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 5).Value = "Deadline";
                    worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 6).Value = " N/Req";
                    worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 7).Value = "Location";
                    worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 8).Value = " D.L..";
                    worksheet.Cell(currentRow, 8).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 9).Value = "Resumes";
                    worksheet.Cell(currentRow, 9).Style.Font.Bold = true;


                    //worksheet.Cell(currentRow, 14).Value = "Offer";
                    //worksheet.Cell(currentRow, 14).Style.Font.Bold = true;

                    #endregion

                    #region Body

                    foreach (var item in model.VacancyList)
                    {
                        currentRow++;
                        slNo = slNo + 1;
                        worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 1).Value = slNo;
                        //worksheet.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        //worksheet.Cell(currentRow, 2).Value = item.RefNo;
                        worksheet.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 2).Value = item.JobCode;

                        worksheet.Cell(currentRow, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 3).Value = item.JobTitle;

                        worksheet.Cell(currentRow, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 4).Value = item.RequestedDate;
                        worksheet.Cell(currentRow, 4).Style.DateFormat.Format = "dd-MMM-yyyy";
                        worksheet.Cell(currentRow, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 5).Value = item.NeedByDate;
                        worksheet.Cell(currentRow, 5).Style.DateFormat.Format = "dd-MMM-yyyy";
                        worksheet.Cell(currentRow, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 6).Value = item.NoOfRequirements;
                        worksheet.Cell(currentRow, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 7).Value = item.Location;
                        worksheet.Cell(currentRow, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 8).Value = item.Aging;
                        worksheet.Cell(currentRow, 9).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 9).Value = item.MappedResumes;
                     

                        //worksheet.Cell(currentRow, 14).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        //worksheet.Cell(currentRow, 14).Value = item.OFFER;

                        worksheet.Columns(1, 9).Style.Alignment.WrapText = true;
                        worksheet.Rows().Style.Alignment.WrapText = true;
                    }
                    #endregion
                    worksheet.Columns().AdjustToContents();
                    worksheet.Rows().AdjustToContents();
                    //worksheet.Protect("123");
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
                            "Walk In Interview List.xlsx"
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
