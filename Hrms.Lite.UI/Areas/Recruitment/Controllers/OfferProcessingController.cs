using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.Recruitment;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.DataBank.Generic;
using Hrms.Lite.Shared.General;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Spire.Xls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using File = Hrms.Lite.Shared.File;

namespace Hrms.Lite.UI.Areas.Recruitment.Controllers
{
    [Area("Recruitment")]
    [SessionAuthorize]
    public class OfferProcessingController : Controller
    {
        #region private
           private readonly IDropdownService _dropdownService;
            private readonly IOfferProcessingService _offerProcessingService;
            private readonly ICommonService _commonService;
            private readonly IHelpers _helpers;
            private readonly IBlob _blob;

        private readonly IHostingEnvironment _environment;
        #endregion
        public static class Global
        {
           
            public static Filter GlobalFilter;
            public static string url;


        }

        #region constr
        public OfferProcessingController(IDropdownService dropdownService, ICommonService commonService, IOfferProcessingService offerProcessingService, IHelpers helpers, IBlob blob, IHostingEnvironment environment)
            {
                _offerProcessingService = offerProcessingService;
                _dropdownService = dropdownService;
                _commonService = commonService;
                _helpers = helpers;
               _blob = blob;
            _environment = environment ?? throw new ArgumentException(nameof(environment));

        }
        #endregion
        #region OfferVerification
        public async Task<IActionResult> OfferVerification()  //PREENROLL PENDING
            {
            try
            {
                OnBoarding model = new OnBoarding();
                Filter filter = new Filter();
                model = await _offerProcessingService.GetOfferProcessingList(1, filter,"Pending");
                model.TabName = "Pending";
                return View("OfferVerification", model);
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
        public async Task<IActionResult> GetSalarySplitUpList(string TabName)  
        {
            try
            {
                OnBoarding model = new OnBoarding();
                Filter filter = new Filter();
                model = await _offerProcessingService.GetOfferProcessingList(1, filter, TabName);
                model.TabName = TabName;
                return PartialView("_OfferVerificationList", model);
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
        public async Task<IActionResult> GetEmployementCheckList(string TabName)  
        {
            try
            {
                OnBoarding model = new OnBoarding();
                Filter filter = new Filter();
                model = await _offerProcessingService.GetOfferProcessingList(3, filter, TabName);
                model.TabName = TabName;
                return PartialView("_EmployementCheckList", model);
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
        public async Task<IActionResult> GetOfferVerificationDetails(Guid ResumeDataBankGI, int ApplicantTrackingCode)
        {
            try
            {
                PreEnrollment model = new PreEnrollment();
                PreEnrollment m = new PreEnrollment(); 
                model = await _offerProcessingService.GetOfferProcessingDetails(ResumeDataBankGI, ApplicantTrackingCode);
                await fillDropdown();
                ViewBag.St = new SelectList(await _dropdownService.GetStateDropDown(model.PermanentAddress.Country.Code), "Code", "Name");
                ViewBag.Dist = new SelectList(await _dropdownService.GetDistrictDropdown(model.PermanentAddress.State.Code), "Code", "Name");
                model.FormName = "OfferProcessing";
                //model.Compensation = new Shared.DataBank.Generic.CompensationStructure();
                m = await _commonService.GetMandatoryFiledValidation("PREENROLL");
                model.FiledValidations = m.FiledValidations;      

                model.JobDetails.NoticePeriod = await _commonService.GetNoticePeriod(model.JobDetails.Designation.Code.Value,null);
                ViewBag.AbsoluteURL = model.BasicInfo.File.AbsoluteUri;
                ViewBag.FileName = model.BasicInfo.File.FileName;
                ViewBag.Relation = new SelectList(await _dropdownService.GetPreenrollRelationDropDown(), "Code", "Name");

                return View("~/Areas/DataBank/Views/PreEnrollmentTest/Create.cshtml", model);
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
            ViewBag.Sal = new SelectList(await _dropdownService.GetSalutationDropDown(), "Code", "Name");
            ViewBag.Gen = new SelectList(await _dropdownService.GetGenderDropDown(), "Code", "Name");
            ViewBag.Cat = new SelectList(await _dropdownService.GetCategoryDropdown(), "Code", "Name");
            ViewBag.Dep = new SelectList(await _dropdownService.GetDepartmentDropdown(), "Code", "Name");
            ViewBag.Gra = new SelectList(await _dropdownService.GetGradeDropdown(), "Code", "Name");
            ViewBag.Loc = new SelectList(await _dropdownService.GetLocationDropdown(), "Code", "Name");
            ViewBag.Cou = new SelectList(await _dropdownService.GetCountryDropDown(), "Code", "Name");
            ViewBag.Des = new SelectList(await _dropdownService.GetDesignationDropdown(), "Code", "Name");
            ViewBag.Pay = new SelectList(await _dropdownService.GetPayrollGroupDropdown(), "Code", "Name");
            ViewBag.SlryStruct = new SelectList(await _dropdownService.GeSalaryStructureDropDown(), "Code", "Name");
            ViewBag.VDAGroup = new SelectList(await _dropdownService.GetVDACombo(), "Code", "Name");
        }
        [HttpPost]
        public async Task<IActionResult> SaveOfferProcessing(PreEnrollment model, DateTime Renewaldt)
        {
            var StorageType = await _commonService.GetFileStorageType();
            try
            {
                model.BasicInfo.RenewalDate = Renewaldt;
                string filename = "";
                if (model.BasicInfo.File != null)
                {
                    if (model.BasicInfo.File.FileData != null)
                    {
                        if (StorageType.Data == "BLOB")
                        {

                            var fileobj = await _commonService.UploadIformFileToBLOB(model.BasicInfo.File, "Documents");
                            if (fileobj.Success == false)
                            {
                                fileobj.Message = "FILE UPLOAD ERROR";
                                return Json(fileobj);
                            }
                            model.BasicInfo.File = fileobj.Data;

                        }
                        else if (StorageType.Data == "IIS")
                        {
                            var fileobj = await _commonService.UploadIformFileToIIS(model.BasicInfo.File, "Documents");
                            if (fileobj.Success == false)
                            {
                                fileobj.Message = "FILE UPLOAD ERROR";
                                return Json(fileobj);
                            }
                            model.BasicInfo.File = fileobj.Data;

                        }
                        else
                        {
                            var fileobj = await _commonService.UploadIformFileToAWS(model.BasicInfo.File, "Documents");
                            if (fileobj.Success == false)
                            {
                                fileobj.Message = "FILE UPLOAD ERROR";
                                return Json(fileobj);
                            }
                            model.BasicInfo.File = fileobj.Data;

                        }
                        //model.BasicInfo.File = await _blob.UploadToAzureAsync(model.BasicInfo.File);
                        //filename = model.BasicInfo.File.FileName;
                        //model.BasicInfo.File.FileData = null;
                    }
                }
                var response = await _offerProcessingService.SaveOfferProcessing(model);
                if (response.Success == true)
                {
                    response.Message = "Saved Successfully";
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

        #endregion

        #region OfferInitiation
        public async Task<IActionResult> OfferInitiation()  //PREENROLL  APPROVAL PENDING
        {
            try
            {
                OnBoarding model = new OnBoarding();
            Filter filter = new Filter();
            model = await _offerProcessingService.GetInitiationList("All", filter);
                model.TabName = "ALL";
            return View("OfferInitiation", model);
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
        public async Task<IActionResult> GetList(string TabIndex)
        {
            try
            {
                OnBoarding model = new OnBoarding();
                Filter filter = new Filter();
                model = await _offerProcessingService.GetInitiationList(TabIndex, filter);
                model.TabName = TabIndex;
                return View("_OfferInitiationList", model);
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
        public async Task<IActionResult> OfferLetter(Guid GI, string Tab)
        {
            OnBoarding model = new OnBoarding();
            Filter filter = new Filter();
            if (Tab == "Offer_Acceptance_Pending")
                model = await _offerProcessingService.GetInitiationList("Offer_Acceptance_Pending", filter);
            else
                model = await _offerProcessingService.GetInitiationList("Offer_Accepted", filter);
            //var list = from li in model.OnBoardingList
            //           where li.PreEnrollGI == GI
            //           select li;
            var list = model.OnBoardingList.ToList().Where(u => u.PreEnrollGI == GI).ToList();

            byte[] contents = list[0].OfferLetterData;
            //return new FileContentResult(contents, "application/pdf")
            //{
            //    FileDownloadName = "offerLetter.pdf";
            //};
            return File(contents, "application/pdf");
        }

        [HttpPost]
        public async Task<IActionResult> OfferAcceptance(Guid PreEnrollGI,string mode,string Remarks)
        {
            PreEnrollment model = new PreEnrollment();
            model.PreEnrollGI = PreEnrollGI;
            model.Mode = mode;
            model.Description = Remarks;
            model.OfferEntryFrom = "HRMS";
            var response = await _offerProcessingService.OfferAcceptance(model);
            return Json(response);
        }

        //public IActionResult OfferLetter(int PreEnrollCode, string Tab,int Gross)
        //{
        //    PreEnrollment model = new PreEnrollment();
        //    model.PreEnrollCode = PreEnrollCode;
        //    model.Tab = Tab;
        //    if (Gross > 40000)

        //        return View("OfferLetterGreater", model);

        //    else
        //        return View("OfferLetter", model);
        //}


        public async Task<IActionResult> Summary(Guid PreEnrollGI)
        {
            try
            {
                PreEnrollment model = new PreEnrollment();
                model = await _offerProcessingService.GetOfferLetterDetails(PreEnrollGI);
                model.EmployeeHeader = await _commonService.GetEmployeeHeader(PreEnrollGI);
                return PartialView("_PreviewAndApprove", model);
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


        public  async Task<IActionResult> GetOfferLetter(int PreEnrollCode,Guid PreEnrollGI,int Gross,string Tab)
        {
            try
            {
                PreEnrollment model = new PreEnrollment();
                
              
                model.PreEnrollCode = PreEnrollCode;
                model.PreEnrollGI = PreEnrollGI;
                model.Url = await _commonService.GetUrl();
                Global.url = model.Url.Urllink;
                model.Tab = Tab;

                model.Url = await _commonService.GetOfferLetter("OfferLetter");
                model.Url.Urllink = Global.url;

                //if (Gross>40000)
              
                //    return View("OfferLetterGreater", model);
                
                //else
                return View("OfferLetter", model);
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



        public async Task<IActionResult> GetOfferInitiationDetails(Guid PreEnrollGI,string Tab)  //PREENROLL  APPROVAL PENDING
         {
            try
            {
                PreEnrollment model = new PreEnrollment();
            model = await _offerProcessingService.GetOfferInitiationDetails(PreEnrollGI);
            model.EmployeeHeader = await _commonService.GetEmployeeHeader(PreEnrollGI);
                model.Tab = Tab;
            return View("PreEnrollmentApproval", model);
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
        public async Task<IActionResult> GenerateReportPDF(string ReportName, int PreEnrollCode, Guid PreEnrollGI, string mode)
        {
            try
            {

                PreEnrollment model = new PreEnrollment();
                var reportProcessor = new Telerik.Reporting.Processing.ReportProcessor();


            var deviceInfo = new System.Collections.Hashtable();

            var reportSource = new Telerik.Reporting.UriReportSource();

            var reportingFileName = System.IO.Path.Combine(_environment.ContentRootPath + "/Reports/", ReportName);


            reportSource.Uri = reportingFileName;

              model.Url = await _commonService.GetUrl();
                var url = model.Url.Urllink + "/api/OfferLetterReport/GetOfferLetterReportPrint?PreEnrollCode=" + PreEnrollCode;

                object parameterValue = PreEnrollCode;
            reportSource.Parameters.Add("url", url);

            Telerik.Reporting.Processing.RenderingResult result = reportProcessor.RenderReport("PDF", reportSource, deviceInfo);
            var b64 = Convert.ToBase64String(result.DocumentBytes);
            //return new ContentResult() { Content = b64 };

            File f = new File();
                File f1 = new File();
                f.FileName = result.DocumentName;
            f.ContentType = "application/pdf";
            f.FileExtension = ".pdf";
            f.fdata = result.DocumentBytes;
                //fl = await _blob.UploadToAzureAsyncWithByteArray(f);


                var StorageType = await _commonService.GetFileStorageType();
                
                        if (StorageType.Data == "BLOB")
                        {

                            var fileobj = await _commonService.UploadByteArrayToBLOB(f, "Documents");
                            if (fileobj.Success == false)
                            {
                                fileobj.Message = "FILE UPLOAD ERROR";
                                return Json(fileobj);
                            }
                          f1 = fileobj.Data;

                        }
                        else if (StorageType.Data == "IIS")
                        {
                            var fileobj = await _commonService.UploadByteArrayToIIS(f, "Documents");
                            if (fileobj.Success == false)
                            {
                                fileobj.Message = "FILE UPLOAD ERROR";
                                return Json(fileobj);
                            }
                        f1 = fileobj.Data;

                      }
                        else
                        {
                            var fileobj = await _commonService.UploadByteArrayToAWS(f, "Documents");
                            if (fileobj.Success == false)
                            {
                                fileobj.Message = "FILE UPLOAD ERROR";
                                return Json(fileobj);
                            }
                    f1 = fileobj.Data;

                }
              
                string AbsoluteUri = f1.AbsoluteUri;
                string FileName = f1.FileName;

            return RedirectToAction("OfferInitiationApproveorReject", "OfferProcessing", new { PreEnrollGI = PreEnrollGI, FileName = FileName, Mode = mode, AbsoluteUri=AbsoluteUri });
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


        
        public async Task<IActionResult> OfferInitiationApproveorReject(Guid PreEnrollGI,string FileName, string Mode, string AbsoluteUri)
        {
            PreEnrollment model = new PreEnrollment();
            try
            {
                model.PreEnrollGI = PreEnrollGI;
                model.Mode = Mode;
                model.BasicInfo = new Shared.DataBank.Generic.EmployeeBasicInfo();
                model.BasicInfo.OfferLetter = new File();
                 model.BasicInfo.OfferLetter.AbsoluteUri = AbsoluteUri;
                model.BasicInfo.OfferLetter.FileName = FileName;
                var response = await _offerProcessingService.OfferInitiation(model);
             
                if (response.Message == "APPROVED")
            {
                response.Message = "Approved Successfully";
                return Json(response);
            }
            if (response.Message == "REJECTED")
            {
                response.Message = "Rejected Successfully";
                return Json(response);
            }
            else
            {               
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

      














        public async Task<IActionResult> ExportToExcelOfferInitiation(string TabIndex)
        {
            try
            {
                OnBoarding model = new OnBoarding();

                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Offer Initiation", "Excel");
                int slNo = 0;
                if (Global.GlobalFilter == null)
                    Global.GlobalFilter = new Filter();
                model = await _offerProcessingService.GetInitiationList(TabIndex, Global.GlobalFilter);
                string currDateTime = await _helpers.GetCurrentDate();
                
                using (var workbook = new XLWorkbook())
                {

                    var worksheet = workbook.Worksheets.Add("Offer Initiation");


                    int count = model.OnBoardingList.Count + 4;

                    if (TabIndex == "All")
                    {

                        worksheet.Range("A1:J" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:J" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:J" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:J" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:J" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 9)));
                        wsReportNameHeaderRange.Style.Font.Bold = true;
                        wsReportNameHeaderRange.Style.Font.FontSize = 15;
                        wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                        wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        wsReportNameHeaderRange.Merge();
                        wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                        wsReportNameHeaderRange.Value = "OfferInitiation(" + TabIndex + ")";
                        worksheet.Range("A4:J4").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    }
                    else
                    {

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
                        wsReportNameHeaderRange.Value = "OfferInitiation("+ TabIndex +")";
                    }


                   

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
                    worksheet.Cell(3, 6).Value = model.OnBoardingList.Count();


                    var currentRow = 4;
                    #region Header
                    worksheet.Range("A4:I4").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Cell(currentRow, 1).Value = "Sl.No";
                    worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 2).Value = "Appli. ID";
                    worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 3).Value = "Candidate Name";
                    worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 4).Value = "Designation";
                    worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 5).Value = "Department";
                    worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 6).Value = "Location";
                    worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 7).Value = "DL";
                    worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 8).Value = "E:DOJ";
                    worksheet.Cell(currentRow, 8).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 9).Value = "Doc Verified";
                    worksheet.Cell(currentRow, 9).Style.Font.Bold = true;
                    if(TabIndex== "All")
                    {
                        worksheet.Cell(currentRow, 10).Value = "Stage";
                        worksheet.Cell(currentRow, 10).Style.Font.Bold = true;
                    }
                   
                  
                

                    #endregion

                    #region Body
                    foreach (var item in model.OnBoardingList)
                    {
                        currentRow++;
                        slNo = slNo + 1;
                        worksheet.Columns().AdjustToContents();
                        worksheet.Rows().AdjustToContents();
                        worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 1).Value = slNo;
                        worksheet.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 2).Value = item.RefNo;
                        worksheet.Cell(currentRow, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 3).Value = item.CandidateName;
                        worksheet.Cell(currentRow, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 4).Value = item.Designation.Name;
                        worksheet.Cell(currentRow, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 5).Value = item.Department.Name;
                        worksheet.Cell(currentRow, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 6).Value = item.Location.Name;
                        worksheet.Cell(currentRow, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 7).Value = item.DueDays;
                        worksheet.Cell(currentRow, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 8).Value = item.ExpectedDOJ;
                        worksheet.Cell(currentRow, 8).Style.DateFormat.Format = "dd-MMM-yyyy";
                        if(item.DocVerified == "Y")
                        {
                            worksheet.Cell(currentRow, 9).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 9).Value = "Yes";
                        }
                        else
                        {
                            worksheet.Cell(currentRow, 9).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 9).Value = "No";
                        }

                        if(TabIndex== "All")
                        {
                            worksheet.Cell(currentRow, 10).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 10).Value = item.Stage;
                        }
                      
                       
                       
                        worksheet.Columns(1, 12).Style.Alignment.WrapText = true;
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
                            "Offer Initiation List.xlsx"
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




        #endregion
        #region EmployeementChecking and CreditChecking
        public async Task<IActionResult> EmployeementChecking()
        {
            try
            {
                OnBoarding model = new OnBoarding();
                Filter filter = new Filter();
                model = await _offerProcessingService.GetOfferProcessingList(3, filter,"Pending");
                model.TabName = "Pending";
                return View("EmployeementChecking", model);

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
        public async Task<IActionResult> CreditChecking()
        {
            try
            {
                OnBoarding model = new OnBoarding();
                Filter filter = new Filter();
                model = await _offerProcessingService.GetOfferProcessingList(2, filter, "Pending");
                model.TabName = "Pending";
                return View("CreditChecking", model);
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
        public async Task<IActionResult> GetCreditCheckList(string TabName)
        {
            try
            {
                OnBoarding model = new OnBoarding();
                Filter filter = new Filter();
                model = await _offerProcessingService.GetOfferProcessingList(2, filter, TabName);
                model.TabName = TabName;
                return PartialView("_CreditCheckingList", model);
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
        public async Task<IActionResult> GetCheckingDetails(Guid PreEnrollGI, string formName)
        {
            try
            {
                PreEnrollment model = new PreEnrollment();
            model = await _offerProcessingService.GetCandidateDetails(PreEnrollGI);
            model.FormName = formName;
            model.CreditChecking.SelectionStatus = 1;
            return PartialView("_Checking", model);
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
        public async Task<IActionResult> SaveCreditChecking(PreEnrollment model)
        {
            var StorageType = await _commonService.GetFileStorageType();
            try
            {
                string filename = "";

                if (model.CreditChecking.Attachment != null)
                {
                    if (model.CreditChecking.Attachment.FileData != null)
                    {
                        if (StorageType.Data == "BLOB")
                        {

                            var fileobj = await _commonService.UploadIformFileToBLOB(model.CreditChecking.Attachment, "Documents");
                            if (fileobj.Success == false)
                            {
                                fileobj.Message = "FILE UPLOAD ERROR";
                                return Json(fileobj);
                            }
                            model.CreditChecking.Attachment = fileobj.Data;

                        }
                        else if (StorageType.Data == "IIS")
                        {
                            var fileobj = await _commonService.UploadIformFileToIIS(model.CreditChecking.Attachment, "Documents");
                            if (fileobj.Success == false)
                            {
                                fileobj.Message = "FILE UPLOAD ERROR";
                                return Json(fileobj);
                            }
                            model.CreditChecking.Attachment = fileobj.Data;

                        }
                        else
                        {
                            var fileobj = await _commonService.UploadIformFileToAWS(model.CreditChecking.Attachment, "Documents");
                            if (fileobj.Success == false)
                            {
                                fileobj.Message = "FILE UPLOAD ERROR";
                                return Json(fileobj);
                            }
                            model.CreditChecking.Attachment = fileobj.Data;

                        }
                        //        model.CreditChecking.Attachment = await _blob.UploadToAzureAsync(model.CreditChecking.Attachment);
                        //filename = model.CreditChecking.Attachment.FileName;
                        //model.CreditChecking.Attachment.FileData= null;
                    }
                }
                var response= await _offerProcessingService.SaveCreditCheck(model.CreditChecking);
            if (response.Success == true)
            {
                response.Message = "Saved Successfully";
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
        [HttpPost]
        public async Task<IActionResult> SaveEmployeementChecking(PreEnrollment model)
        {
            var StorageType = await _commonService.GetFileStorageType();
            try
            {
                string filename = "";

                if (model.CreditChecking.Attachment != null)
                {
                    if (model.CreditChecking.Attachment.FileData != null)
                    {
                        if (StorageType.Data == "BLOB")
                        {

                            var fileobj = await _commonService.UploadIformFileToBLOB(model.CreditChecking.Attachment, "Documents");
                            if (fileobj.Success == false)
                            {
                                fileobj.Message = "FILE UPLOAD ERROR";
                                return Json(fileobj);
                            }
                            model.CreditChecking.Attachment = fileobj.Data;

                        }
                        else if (StorageType.Data == "IIS")
                        {
                            var fileobj = await _commonService.UploadIformFileToIIS(model.CreditChecking.Attachment, "Documents");
                            if (fileobj.Success == false)
                            {
                                fileobj.Message = "FILE UPLOAD ERROR";
                                return Json(fileobj);
                            }
                            model.CreditChecking.Attachment = fileobj.Data;

                        }
                        else
                        {
                            var fileobj = await _commonService.UploadIformFileToAWS(model.CreditChecking.Attachment, "Documents");
                            if (fileobj.Success == false)
                            {
                                fileobj.Message = "FILE UPLOAD ERROR";
                                return Json(fileobj);
                            }
                            model.CreditChecking.Attachment = fileobj.Data;

                        }



                        //        model.CreditChecking.Attachment = await _blob.UploadToAzureAsync(model.CreditChecking.Attachment);
                        //filename = model.CreditChecking.Attachment.FileName;
                        //model.CreditChecking.Attachment.FileData = null;
                    }
                }
                   
            var response = await _offerProcessingService.SaveEmployementCheck(model.CreditChecking);
            if (response.Success == true)
            {
                response.Message = "Saved Successfully";
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

        public async Task<IActionResult> ExportToExcel(string TabName)
        {
            try
            {
                OnBoarding model = new OnBoarding();

                string LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                int slNo = 0;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Credit Checking", "Excel");
                //Filter filter = new Filter();
                //filter = JsonConvert.DeserializeObject<Filter>(TempData["FilterModel"].ToString());
                Filter filter = new Filter();
                model = await _offerProcessingService.GetOfferProcessingList(2, filter, TabName);
                int no = model.OnBoardingList.Count();
                string currDateTime = await _helpers.GetCurrentDate();
                
                using (var workbook = new XLWorkbook())
                {

                    var worksheet = workbook.Worksheets.Add("Credit Checking List");


                    int count = model.OnBoardingList.Count + 4;


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
                    wsReportNameHeaderRange.Value = "Credit Checking List";

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
                    worksheet.Cell(3, 4).Value = currDateTime.ToString();
                    worksheet.Cell(3, 5).Style.Font.Bold = true;
                    worksheet.Cell(3, 5).Value = "Total Count";
                    worksheet.Cell(3, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(3, 6).Value = model.OnBoardingList.Count();



                    var currentRow = 4;
                    #region Header
                    worksheet.Range("A4:I4").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Cell(currentRow, 1).Value = "Sl.No";
                    worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 2).Value = "Appli. ID";
                    worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 3).Value = "Candidate Name";
                    worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 4).Value = "MRF No.";
                    worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 5).Value = "Job Title";
                    worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 6).Value = "Designation";
                    worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 7).Value = "Location";
                    worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 8).Value = "Deadline";
                    worksheet.Cell(currentRow, 8).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 9).Value = "Doc Verified";
                    worksheet.Cell(currentRow, 9).Style.Font.Bold = true;








                    //worksheet.Cell(currentRow, 14).Value = "Offer";
                    //worksheet.Cell(currentRow, 14).Style.Font.Bold = true;

                    #endregion

                    #region Body

                    foreach (var item in model.OnBoardingList)
                    {
                        currentRow++;
                        slNo = slNo + 1;
                        worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
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
                        worksheet.Cell(currentRow, 6).Value = item.Designation.Name;
                        worksheet.Cell(currentRow, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 7).Value = item.Location.Name;
                        worksheet.Cell(currentRow, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 8).Value = item.NeedByDate;
                        worksheet.Cell(currentRow, 8).Style.DateFormat.Format = "dd-MMM-yyyy";
                        worksheet.Cell(currentRow, 9).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 9).Value = item.DocVerified;



                        //worksheet.Cell(currentRow, 14).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        //worksheet.Cell(currentRow, 14).Value = item.OFFER;

                        worksheet.Columns(1, 8).Style.Alignment.WrapText = true;
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
                            "Credit Checking List.xlsx"
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

        public async Task<IActionResult> EmploymentCheckExportToExcel(string TabName)
        {
            try
            {
                OnBoarding model = new OnBoarding();

                string LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                int slNo = 0;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Employment Checking List", "Excel");
                //Filter filter = new Filter();
                //filter = JsonConvert.DeserializeObject<Filter>(TempData["FilterModel"].ToString());
                Filter filter = new Filter();
                model = await _offerProcessingService.GetOfferProcessingList(3, filter,TabName);
                int no = model.OnBoardingList.Count();
                string currDateTime = await _helpers.GetCurrentDate();
                
                using (var workbook = new XLWorkbook())
                {

                    var worksheet = workbook.Worksheets.Add("Employment Checking List");


                    int count = model.OnBoardingList.Count + 4;


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
                    wsReportNameHeaderRange.Value = "Employment Checking List";

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
                    worksheet.Cell(3, 4).Value = currDateTime.ToString();
                    worksheet.Cell(3, 5).Style.Font.Bold = true;
                    worksheet.Cell(3, 5).Value = "Total Count";
                    worksheet.Cell(3, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(3, 6).Value = model.OnBoardingList.Count();



                    var currentRow = 4;
                    #region Header
                    worksheet.Range("A4:I4").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Cell(currentRow, 1).Value = "Sl.No";
                    worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 2).Value = "Appli. ID";
                    worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 3).Value = "Candidate Name";
                    worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 4).Value = "MRF No.";
                    worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 5).Value = "Job Title";
                    worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 6).Value = "Designation";
                    worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 7).Value = "Location";
                    worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 8).Value = "Deadline";
                    worksheet.Cell(currentRow, 8).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 9).Value = "Doc Verified";
                    worksheet.Cell(currentRow, 9).Style.Font.Bold = true;



                    //worksheet.Cell(currentRow, 14).Value = "Offer";
                    //worksheet.Cell(currentRow, 14).Style.Font.Bold = true;

                    #endregion

                    #region Body

                    foreach (var item in model.OnBoardingList)
                    {
                        currentRow++;
                        slNo = slNo + 1;
                        worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
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
                        worksheet.Cell(currentRow, 6).Value = item.Designation.Name;
                        worksheet.Cell(currentRow, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 7).Value = item.Location.Name;
                        worksheet.Cell(currentRow, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 8).Value = item.NeedByDate;
                        worksheet.Cell(currentRow, 8).Style.DateFormat.Format = "dd-MMM-yyyy";
                        worksheet.Cell(currentRow, 9).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 9).Value = item.DocVerified;



                        //worksheet.Cell(currentRow, 14).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        //worksheet.Cell(currentRow, 14).Value = item.OFFER;

                        worksheet.Columns(1, 8).Style.Alignment.WrapText = true;
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
                            "Employment Checking List.xlsx"
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

        public async Task<IActionResult> SalarySplitUpexportToExcel(string TabName)
        {
            try
            {
                OnBoarding model = new OnBoarding();

                string LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                int slNo = 0;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Salary Split Up", "Excel");
                //Filter filter = new Filter();
                //filter = JsonConvert.DeserializeObject<Filter>(TempData["FilterModel"].ToString());
                Filter filter = new Filter();

                model = await _offerProcessingService.GetOfferProcessingList(1, filter,TabName);

                int no = model.OnBoardingList.Count();
                string currDateTime = await _helpers.GetCurrentDate();
                
                using (var workbook = new XLWorkbook())
                {

                    var worksheet = workbook.Worksheets.Add("Salary Split Up List");


                    int count = model.OnBoardingList.Count + 4;


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
                    wsReportNameHeaderRange.Value = "Salary Split Up List";

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
                    worksheet.Cell(3, 6).Value = model.OnBoardingList.Count();



                    var currentRow = 4;
                    #region Header
                    worksheet.Range("A4:I4").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Cell(currentRow, 1).Value = "Sl.No";
                    worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 2).Value = "Appli. ID";
                    worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 3).Value = "Candidate Name";
                    worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 4).Value = "MRF No ";
                    worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 5).Value = "Job Title";
                    worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 6).Value = "Designation";
                    worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 7).Value = "Location";
                    worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 8).Value = "Deadline";
                
                    worksheet.Cell(currentRow, 8).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 9).Value = "Doc Verified";
                    worksheet.Cell(currentRow, 9).Style.Font.Bold = true;



                    //worksheet.Cell(currentRow, 14).Value = "Offer";
                    //worksheet.Cell(currentRow, 14).Style.Font.Bold = true;

                    #endregion

                    #region Body

                    foreach (var item in model.OnBoardingList)
                    {
                        currentRow++;
                        slNo = slNo + 1;
                        worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
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
                        worksheet.Cell(currentRow, 6).Value = item.Designation.Name;
                        worksheet.Cell(currentRow, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 7).Value = item.Location.Name;
                        worksheet.Cell(currentRow, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 8).Value = item.NeedByDate;
                        worksheet.Cell(currentRow, 8).Style.DateFormat.Format = "dd-MMM-yyyy";
                        worksheet.Cell(currentRow, 9).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 9).Value = item.DocVerified;

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
                            "Salary Plit Up List.xlsx"
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




        #endregion
    }
}

