using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Spire.Xls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.DataBank.Controllers
{
    [Area("DataBank")]
    [SessionAuthorize]
    public class ApplicantTrackingController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly ICommonService _commonService;
        private readonly IApplicantTrackingService _applicantTrackingService;
        private readonly IBlob _blob;
        private readonly IHelpers _helpers;
        public ApplicantTrackingController(IDropdownService dropdownService, ICommonService commonService, IApplicantTrackingService applicantTrackingService,IBlob blob, IHelpers helpers)
        {
            _dropdownService = dropdownService;
            _commonService = commonService;
            _applicantTrackingService = applicantTrackingService;
            _blob = blob;
            _helpers = helpers;
        }
        public static class Global
        {
            
            public static string TabName;
        }
            public async Task<IActionResult> Index()
        {
            Filter filter = new Filter();
            ApplicantTracking model = new ApplicantTracking();
            model = await _applicantTrackingService.GetList(1);
            return View(model);
        }
        public async Task<IActionResult> InterviewOrEvaluation()
        {
            Filter filter = new Filter();
            ApplicantTracking model = new ApplicantTracking();
            model = await _applicantTrackingService.GetList(2);
            return View("InterviewEvaluation", model);
        }
        public async Task<IActionResult> FinalApprovalList()
        {
            Filter filter = new Filter();
            ApplicantTracking model = new ApplicantTracking();

            model = await _applicantTrackingService.GetList(3);
            model.ApplicantTrackingListTabs = (List<ApplicantTrackingListTabs>)await _applicantTrackingService.GetRAApprovalListTabs();
            return View("FinalApproval", model);
        }
        public async Task<IActionResult> GetList(int TabIndex,string TabName)
        {
            ApplicantTracking model = new ApplicantTracking();
            Global.TabName= TabName;
            model = await _applicantTrackingService.GetList(TabIndex);
            if (TabIndex == 1)
                return PartialView("_ScreeningOrShortlistedList", model);
            else if (TabIndex == 2)
                return PartialView("_CustomInterviewAndEvaluationList",model);
            else   if(TabIndex == 3)
                return PartialView("_RAApprovalPendingList", model);
            else if(TabIndex == 6)
                return PartialView("_PostRAApprovalList", model);
            else //TabIndex = 7
                return PartialView("_OfferRejectedList", model);

        }

        

    public async Task<IActionResult> GetShortListedRARejectDetails(string Mode,Guid ResumeDataBankGI,int ApplicantTrackingCode)
        {
            ApplicantTracking model = new ApplicantTracking();
            model = await _applicantTrackingService.GetShortListedDetails(ResumeDataBankGI);
            model.ApplicantTrackingCode = ApplicantTrackingCode;
            ViewBag.RejectReason = new SelectList(await _dropdownService.CandidateRejectReasonDropdown(), "Code", "Name");
            if (Mode == "RAReject")
                return PartialView("_RAReject", model);
            else
            {
                ViewBag.selection = new SelectList(await _dropdownService.GetInterviewSelectionDropdown(), "Code", "Name");
                ViewBag.level = new SelectList(await _dropdownService.GetInterviewLevelDropdown(), "Code", "Name");
                return PartialView("_CustomScreeningOrShortlisted", model);
            }
        }
       
        public async Task<IActionResult> GetEvaluationDetails(Guid ResumeDataBankGI,int ApplicantTrackingCode)
        {
            ApplicantTracking model = new ApplicantTracking();
            model.ApplicantTrackingDT = await _applicantTrackingService.GetEvaluationDetails(ResumeDataBankGI);
            ViewBag.interviewTyp = new SelectList(await _dropdownService.GetInterviewTypeDropdown(), "Code", "Name");
            ViewBag.interviewMod = new SelectList(await _dropdownService.GetInterviewModeDropdown(), "Code", "Name");
            ViewBag.interviewer = new SelectList(await _dropdownService.GetEmployeeDropDown(), "Code", "Name");
            ViewBag.selection = new SelectList(await _dropdownService.GetInterviewSelectionDropdown(), "Code", "Name");
            ViewBag.RejectReason = new SelectList(await _dropdownService.CandidateRejectReasonDropdown(), "Code", "Name");
            model.ApplicantTrackingDTList = new List<ApplicantTracking_Details>();
            model.ApplicantTrackingDTList.Add(new ApplicantTracking_Details());
            model.ResumeDataBankGI = ResumeDataBankGI;
            model.ApplicantTrackingCode = ApplicantTrackingCode;
            return PartialView("_CustomEvaluationDetails", model);

         
        }
        public async Task<IActionResult> GetApplicantTracker(Guid ResumeDataBankGI)
        {
            ApplicantTracking model = new ApplicantTracking();
            model = await _applicantTrackingService.GetApplicantTracker(ResumeDataBankGI);                        
            return PartialView("_ApplicantTracking", model);          
        }

        public async Task<IActionResult> GetInterviewEvaluationView(Guid ResumeDataBankGI,string InterviewStatusType)
        {
            ApplicantTracking model = new ApplicantTracking();
            model = await _applicantTrackingService.GetInterviewEvaluationView(ResumeDataBankGI, InterviewStatusType);
            
            return PartialView("_ViewSchedule",model);
        }

        public async Task<IActionResult> GetInterviewDetails(int ApplicantTrackingCode)
        {
            try { 
                ApplicantTracking model = new ApplicantTracking();
                model = await _applicantTrackingService.GetInterviewDetails(ApplicantTrackingCode);
                return PartialView("_ViewInterviewDetails", model);
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
        public async Task<IActionResult> GetSalaryCommunication(Guid ResumeDataBankGI,int ApplicantTrackingCode)
        {
            ApplicantTracking model = new ApplicantTracking();
            model = await _applicantTrackingService.GetShortListedDetails(ResumeDataBankGI);
            model.ApplicantTrackingCode = ApplicantTrackingCode;
            ViewBag.selection = new SelectList(await _dropdownService.GetInterviewSelectionDropdown(), "Code", "Name");
            ViewBag.RejectReason = new SelectList(await _dropdownService.CandidateRejectReasonDropdown(), "Code", "Name");
            return PartialView("_SalaryCommunication", model);
        }
        public async Task<IActionResult> ProceesToInterview(ApplicantTracking model)
        {
            var response = await _applicantTrackingService.ScreeningSave(model);
            return Json(response);
        }
        public async Task<IActionResult> Schedule(ApplicantTracking model)
        {
            var response = await _applicantTrackingService.InterviewEvaluationSave(model);
            return Json(response);
        }
        [HttpPost]
        public async Task<IActionResult> RAReject(ApplicantTracking model)
        {
            model.HoldOrReject = "Reject";
            var response = await _applicantTrackingService.HoldReject(model);
            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> OfferRevise(Guid PreEnrollGI, string Mode, string Remarks)
        {
            try
            {
                var response = await _applicantTrackingService.OfferRevise(PreEnrollGI, Mode, Remarks);
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
        public async Task<IActionResult> SaveEvaluation(ApplicantTracking model)
        {
            var StorageType = await _commonService.GetFileStorageType();  
            try
            {
                for (int i = 0; i < model.ApplicantTrackingDTList.Count; i++)
                {                
                    if (model.ApplicantTrackingDTList[i].File != null)
                    {
                        if (model.ApplicantTrackingDTList[i].File.FileData != null)
                        {
                            if (StorageType.Data == "BLOB")
                            {
                                var fileobj = await _commonService.UploadIformFileToBLOB(model.ApplicantTrackingDTList[i].File, "Documents");
                                if (fileobj.Success == false)
                                {
                                    fileobj.Message = "FILE UPLOAD ERROR";
                                    return Json(fileobj);
                                }
                                model.ApplicantTrackingDTList[i].File = fileobj.Data;
                            }
                            else if (StorageType.Data == "IIS")
                            {
                                var fileobj = await _commonService.UploadIformFileToIIS(model.ApplicantTrackingDTList[i].File, "Documents");
                                if (fileobj.Success == false)
                                {
                                    fileobj.Message = "FILE UPLOAD ERROR";
                                    return Json(fileobj);
                                }
                                model.ApplicantTrackingDTList[i].File = fileobj.Data;
                            }
                            else
                            {
                                var fileobj = await _commonService.UploadIformFileToAWS(model.ApplicantTrackingDTList[i].File, "Documents");
                                if (fileobj.Success == false)
                                {
                                    fileobj.Message = "FILE UPLOAD ERROR";
                                    return Json(fileobj);
                                }
                                model.ApplicantTrackingDTList[i].File = fileobj.Data;
                            }
                        }
                    }
                    //foreach (var item in model.ApplicantTrackingDTList)
                    //{
                    //    if (model.ApplicantTrackingDTList[i].File!=null)
                    //    {

                    //        model.ApplicantTrackingDTList[i].File = await _blob.UploadToAzureAsync(item.File);
                    //        model.ApplicantTrackingDTList[i].File.FileData = null;
                    //    }
                    //    i++;
                    //}            
                }
                    var response = await _applicantTrackingService.InterviewEvaluationSave(model);
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
        public async Task<IActionResult> FinalApproval(ApplicantTracking model)
        {
            var response = await _applicantTrackingService.FinalApproval(model);
            return Json(response);
        }
        public async Task<IActionResult> HoldReject(int ResumeDataBankCode,string status)
        {
            ApplicantTracking model = new ApplicantTracking();
            model.ResumeDataBankCode = ResumeDataBankCode;
            model.HoldOrReject = status;
            var response = await _applicantTrackingService.HoldReject(model);
            return Json(response);
        }
        public async Task<IActionResult> ResumeView(Guid ResumeGI)
        {
            ResumeDataBank model = new ResumeDataBank();
            model = await _applicantTrackingService.GetResumeDetails(ResumeGI);
            return View(model);
        }


        //public async Task<IActionResult> ExportToExcel()
        //{
        //    try
        //    {
        //        ApplicantTracking model = new ApplicantTracking();

        //        string LoginUser = HttpContext.Session.GetString("LoginUser");
        //        var CompanyName = HttpContext.Session.GetString("CompanyName");
        //        int slNo = 0;
        //        ExcelPassword EP = new ExcelPassword();
        //        EP = await _commonService.GetExcelPassword("Screening/Shortlisted", "Excel");
        //        //Filter filter = new Filter();
        //        //filter = JsonConvert.DeserializeObject<Filter>(TempData["FilterModel"].ToString());


        //        model = await _applicantTrackingService.GetList(1);

        //        int no = model.ApplicantTrackingList.Count();
        //        string currDateTime = await _helpers.GetCurrentDate();
        //        
        //        using (var workbook = new XLWorkbook())
        //        {

        //            var worksheet = workbook.Worksheets.Add("Screening/ShortlistedList");


        //            int count = model.ApplicantTrackingList.Count + 4;


        //            worksheet.Range("A1:J" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
        //            worksheet.Range("A1:J" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
        //            worksheet.Range("A1:J" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
        //            worksheet.Range("A1:J" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
        //            worksheet.Range("A1:J" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;






        //            var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 9)));
        //            wsReportNameHeaderRange.Style.Font.Bold = true;
        //            wsReportNameHeaderRange.Style.Font.FontSize = 15;
        //            wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        //            wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
        //            wsReportNameHeaderRange.Merge();
        //            wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
        //            wsReportNameHeaderRange.Value = "Screening/Shortlisted List";

        //            worksheet.Cell(2, 1).Style.Font.Bold = true;
        //            worksheet.Cell(2, 1).Value = "Company Name";
        //            worksheet.Cell(2, 2).Value = CompanyName.ToUpper();


        //            worksheet.Cell(3, 1).Style.Font.Bold = true;
        //            worksheet.Cell(3, 1).Value = "Login User";
        //            worksheet.Cell(3, 2).Value = LoginUser;
        //            worksheet.Cell(3, 3).Style.Font.Bold = true;

        //            worksheet.Cell(3, 3).Value = "Date & Time";
        //            worksheet.Cell(3, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //            worksheet.Cell(3, 3).Style.DateFormat.Format = "dd-MMM-yyyy   h:mm AM/PM";
        //            worksheet.Cell(3, 4).Value = currDateTime.ToString();
        //            worksheet.Cell(3, 5).Style.Font.Bold = true;
        //            worksheet.Cell(3, 5).Value = "Total Count";
        //            worksheet.Cell(3, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //            worksheet.Cell(3, 6).Value = model.ApplicantTrackingList.Count();



        //            var currentRow = 4;
        //            #region Header
        //            worksheet.Range("A4:J4").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
        //            worksheet.Cell(currentRow, 1).Value = "Sl.No";
        //            worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
        //            worksheet.Cell(currentRow, 2).Value = "Appli. ID";
        //            worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
        //            worksheet.Cell(currentRow, 3).Value = " Applicant Name";
        //            worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
        //            worksheet.Cell(currentRow, 4).Value = "MRF No.";
        //            worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
        //            worksheet.Cell(currentRow, 5).Value = "Job Title";
        //            worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
        //            worksheet.Cell(currentRow, 6).Value = "Need By Date";
        //            worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
        //            worksheet.Cell(currentRow, 7).Value = " D.L.";
        //            worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
        //            worksheet.Cell(currentRow, 8).Value = "Location";
        //            worksheet.Cell(currentRow, 8).Style.Font.Bold = true;
        //            worksheet.Cell(currentRow, 9).Value = " Age.";
        //            worksheet.Cell(currentRow, 9).Style.Font.Bold = true;
        //            worksheet.Cell(currentRow, 10).Value = "Y/E";
        //            worksheet.Cell(currentRow, 10).Style.Font.Bold = true;


        //            //worksheet.Cell(currentRow, 14).Value = "Offer";
        //            //worksheet.Cell(currentRow, 14).Style.Font.Bold = true;

        //            #endregion

        //            #region Body

        //            foreach (var item in model.ApplicantTrackingList)
        //            {
        //                currentRow++;
        //                slNo = slNo + 1;
        //                worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //                worksheet.Cell(currentRow, 1).Value = slNo;
        //                worksheet.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //                worksheet.Cell(currentRow, 2).Value = item.RefNo;
        //                worksheet.Cell(currentRow, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //                worksheet.Cell(currentRow, 3).Value = item.ApplicantName;

        //                worksheet.Cell(currentRow, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //                worksheet.Cell(currentRow, 4).Value = item.JobCode;

        //                worksheet.Cell(currentRow, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //                worksheet.Cell(currentRow, 5).Value = item.JobTitle;

        //                worksheet.Cell(currentRow, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //                worksheet.Cell(currentRow, 6).Value = item.NeededByDate;
        //                worksheet.Cell(currentRow, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //                worksheet.Cell(currentRow, 7).Value = item.DaysLeft;
        //                worksheet.Cell(currentRow, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //                worksheet.Cell(currentRow, 8).Value = item.Location;
        //                worksheet.Cell(currentRow, 9).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //                worksheet.Cell(currentRow, 9).Value = item.Age;
        //                worksheet.Cell(currentRow, 10).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //                worksheet.Cell(currentRow, 10).Value = item.YOS;


        //                //worksheet.Cell(currentRow, 14).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //                //worksheet.Cell(currentRow, 14).Value = item.OFFER;

        //                worksheet.Columns(1, 8).Style.Alignment.WrapText = true;
        //                worksheet.Rows().Style.Alignment.WrapText = true;
        //            }
        //            #endregion
        //            worksheet.Columns().AdjustToContents();
        //            worksheet.Rows().AdjustToContents();
        //            //worksheet.Protect("123");
        //            using (var stream = new MemoryStream())
        //            {
        //                workbook.SaveAs(stream);
        //                if (EP.NeedPassword)
        //                {
        //                    Workbook book = new Workbook();
        //                    book.LoadFromStream(stream);
        //                    //Protect Workbook
        //                    book.Protect(EP.Password,false,false);
        //                    book.SaveToStream(stream);
        //                }
        //                var content = stream.ToArray();
        //                return File(
        //                    content,
        //                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
        //                    "Screening/ShortListed List.xlsx"
        //                    );
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


        public async Task<IActionResult> ScreeningShortlistExporttoexcel()
        {
            try
            {
                ApplicantTracking model = new ApplicantTracking();

                string LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                int slNo = 0;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Screenig/Shortlisting", "Excel");
                //Filter filter = new Filter();
                //filter = JsonConvert.DeserializeObject<Filter>(TempData["FilterModel"].ToString());


                model = await _applicantTrackingService.GetList(1);

                int no = model.ApplicantTrackingList.Count();
                string currDateTime = await _helpers.GetCurrentDate();
                
                using (var workbook = new XLWorkbook())
                {

                    var worksheet = workbook.Worksheets.Add("Screening OR ShortList");


                    int count = model.ApplicantTrackingList.Count + 4;


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
                    wsReportNameHeaderRange.Value = "Screening And ShortListed";

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
                    worksheet.Cell(3, 6).Value = model.ApplicantTrackingList.Count();



                    var currentRow = 4;
                    #region Header
                    worksheet.Range("A4:J4").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Cell(currentRow, 1).Value = "Sl.No";
                    worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 2).Value = "Appli. ID";
                    worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 3).Value = " Applicant Name";
                    worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 4).Value = "MRF No.";
                    worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 5).Value = "Job Title";
                    worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 6).Value = "Deadline";
                    worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 7).Value = " D.L.";
                    worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 8).Value = "Location";
                    worksheet.Cell(currentRow, 8).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 9).Value = " Age.";
                    worksheet.Cell(currentRow, 9).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 10).Value = "Y/E";
                    worksheet.Cell(currentRow, 10).Style.Font.Bold = true;


                    //worksheet.Cell(currentRow, 14).Value = "Offer";
                    //worksheet.Cell(currentRow, 14).Style.Font.Bold = true;

                    #endregion

                    #region Body


                    foreach (var item in model.ApplicantTrackingList)
                    {
                        currentRow++;
                        slNo = slNo + 1;
                        worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 1).Value = slNo;
                        worksheet.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 2).Value = item.RefNo;
                        worksheet.Cell(currentRow, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 3).Value = item.ApplicantName;

                        worksheet.Cell(currentRow, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 4).Value = item.JobCode;

                        worksheet.Cell(currentRow, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 5).Value = item.JobTitle;

                        worksheet.Cell(currentRow, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 6).Style.DateFormat.Format = "dd-MMM-yyyy";
                        worksheet.Cell(currentRow, 6).Value = item.NeededByDate;
                        worksheet.Cell(currentRow, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 7).Value = item.DaysLeft;
                        worksheet.Cell(currentRow, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 8).Value = item.Location;
                        worksheet.Cell(currentRow, 9).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 9).Value = item.Age;
                        worksheet.Cell(currentRow, 10).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 10).Value = item.YOS;
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
                            "Screening/ShortListed List.xlsx"
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






        public async Task<IActionResult> InterviewAndEvaluationExportToExcel()
        {
            try
            {
                ApplicantTracking model = new ApplicantTracking();

                string LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                int slNo = 0;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("MRF Approval", "Excel");
                //Filter filter = new Filter();
                //filter = JsonConvert.DeserializeObject<Filter>(TempData["FilterModel"].ToString());

                model = await _applicantTrackingService.GetList(2);

                int no = model.ApplicantTrackingList.Count();
                string currDateTime = await _helpers.GetCurrentDate();
                
                using (var workbook = new XLWorkbook())
                {

                    var worksheet = workbook.Worksheets.Add("Interview And Evaluation List");


                    int count = model.ApplicantTrackingList.Count + 4;


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
                    wsReportNameHeaderRange.Value = "Interview And Evalution List";

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
                    worksheet.Cell(3, 6).Value = model.ApplicantTrackingList.Count();



                    var currentRow = 4;
                    #region Header
                    worksheet.Range("A4:J4").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Cell(currentRow, 1).Value = "Sl.No";
                    worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 2).Value = "Appli. ID";
                    worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 3).Value = " Applicant Name";
                    worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 4).Value = "MRF No.";
                    worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 5).Value = "Job Title";
                    worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 6).Value = "Deadline";
                    worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 7).Value = " D.L.";
                    worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 8).Value = "Location";
                    worksheet.Cell(currentRow, 8).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 9).Value = " Age.";
                    worksheet.Cell(currentRow, 9).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 10).Value = "Y/E";
                    worksheet.Cell(currentRow, 10).Style.Font.Bold = true;


                    //worksheet.Cell(currentRow, 14).Value = "Offer";
                    //worksheet.Cell(currentRow, 14).Style.Font.Bold = true;

                    #endregion

                    #region Body

                    foreach (var item in model.ApplicantTrackingList)
                    {
                        currentRow++;
                        slNo = slNo + 1;
                        worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 1).Value = slNo;
                        worksheet.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 2).Value = item.RefNo;
                        worksheet.Cell(currentRow, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 3).Value = item.ApplicantName;

                        worksheet.Cell(currentRow, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 4).Value = item.JobCode;

                        worksheet.Cell(currentRow, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 5).Value = item.JobTitle;

                        worksheet.Cell(currentRow, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 6).Style.DateFormat.Format = "dd-MMM-yyyy";
                        worksheet.Cell(currentRow, 6).Value = item.NeededByDate;
                        worksheet.Cell(currentRow, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 7).Value = item.DaysLeft;
                        worksheet.Cell(currentRow, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 8).Value = item.Location;
                        worksheet.Cell(currentRow, 9).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 9).Value = item.Age;
                        worksheet.Cell(currentRow, 10).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 10).Value = item.YOS;


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
                            "Interview And Evaluation List.xlsx"
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



        public async Task<IActionResult> RAapprovalExportToExcel(int Tab)
        {
            try
            {
                ApplicantTracking model = new ApplicantTracking();

                string LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                int slNo = 0;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("RA Approval List", "Excel");
                //Filter filter = new Filter();
                //filter = JsonConvert.DeserializeObject<Filter>(TempData["FilterModel"].ToString());
                model = await _applicantTrackingService.GetList(Tab);

                //model = await _applicantTrackingService.GetList(3);

                int no = model.ApplicantTrackingList.Count();
                string currDateTime = await _helpers.GetCurrentDate();
                
                using (var workbook = new XLWorkbook())
                {

                    if(Tab==6)
                    {

                        var worksheet = workbook.Worksheets.Add("RA Approval List");


                        int count = model.ApplicantTrackingList.Count + 4;


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
                        wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                        wsReportNameHeaderRange.Value = "RA Approval List ("+ Global.TabName + ")";

                        worksheet.Cell(2, 1).Style.Font.Bold = true;
                        worksheet.Cell(2, 1).Value = "Company Name";
                        worksheet.Cell(2, 2).Value = CompanyName.ToUpper();


                        worksheet.Cell(3, 1).Style.Font.Bold = true;
                        worksheet.Cell(3, 1).Value = "Login User";
                        worksheet.Cell(3, 2).Value = LoginUser;
                        worksheet.Cell(3, 3).Style.Font.Bold = true;

                        worksheet.Cell(3, 3).Value = "Date & Time";
                        worksheet.Cell(3, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(3, 4).Style.DateFormat.Format = "dd-MMM-yyyy   h:mm AM/PM";
                        worksheet.Cell(3, 4).Value = currDateTime.ToString();
                        worksheet.Cell(3, 5).Style.Font.Bold = true;
                        worksheet.Cell(3, 5).Value = "Total Count";
                        worksheet.Cell(3, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(3, 6).Value = model.ApplicantTrackingList.Count();



                        var currentRow = 4;
                        #region Header
                        worksheet.Range("A4:K4").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                        worksheet.Cell(currentRow, 1).Value = "Sl.No";
                        worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 2).Value = "Appli. ID";
                        worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 3).Value = " Applicant Name";
                        worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 4).Value = "MRF No.";
                        worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 5).Value = "Job Title";
                        worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 6).Value = "Designation";
                        worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 7).Value = " Location.";
                        worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 8).Value = "Deadline";
                        worksheet.Cell(currentRow, 8).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 9).Value = " Doc Verified";
                        worksheet.Cell(currentRow, 9).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 10).Value = "Aging";
                        worksheet.Cell(currentRow, 10).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 11).Value = "Stage";
                        worksheet.Cell(currentRow, 11).Style.Font.Bold = true;


                        //worksheet.Cell(currentRow, 14).Value = "Offer";
                        //worksheet.Cell(currentRow, 14).Style.Font.Bold = true;

                        #endregion

                        #region Body

                        foreach (var item in model.ApplicantTrackingList)
                        {
                            currentRow++;
                            slNo = slNo + 1;
                            worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 1).Value = slNo;
                            worksheet.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 2).Value = item.RefNo;
                            worksheet.Cell(currentRow, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 3).Value = item.ApplicantName;

                            worksheet.Cell(currentRow, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 4).Value = item.JobCode;

                            worksheet.Cell(currentRow, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 5).Value = item.JobTitle;

                            worksheet.Cell(currentRow, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            //worksheet.Cell(currentRow, 6).Style.DateFormat.Format = "dd-MMM-yyyy";
                            worksheet.Cell(currentRow, 6).Value = item.Designation;
                            worksheet.Cell(currentRow, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 7).Value = item.Location;
                            worksheet.Cell(currentRow, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 8).Value = item.NeededByDate;
                            worksheet.Cell(currentRow, 8).Style.DateFormat.Format = "dd-MMM-yyyy";
                            worksheet.Cell(currentRow, 9).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            if (item.DocVerified == "Y")
                            {
                                worksheet.Cell(currentRow, 9).Value = "Yes";
                            }
                            else if(item.DocVerified == "N")
                            {
                                worksheet.Cell(currentRow, 9).Value = "No";
                            }
                            worksheet.Cell(currentRow, 10).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 10).Value = item.Aging;


                            worksheet.Cell(currentRow, 11).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 11).Value = item.Stage;

                            worksheet.Columns(1, 11).Style.Alignment.WrapText = true;
                            worksheet.Rows().Style.Alignment.WrapText = true;
                        }
                        #endregion
                        worksheet.Columns().AdjustToContents();
                        worksheet.Rows().AdjustToContents();

                    }
                    else if (Tab == 7)
                    {

                        var worksheet = workbook.Worksheets.Add("Offer Rejected List");


                        int count = model.ApplicantTrackingList.Count + 4;


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
                        wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                        wsReportNameHeaderRange.Value = "Offer Rejected List";

                        worksheet.Cell(2, 1).Style.Font.Bold = true;
                        worksheet.Cell(2, 1).Value = "Company Name";
                        worksheet.Cell(2, 2).Value = CompanyName.ToUpper();


                        worksheet.Cell(3, 1).Style.Font.Bold = true;
                        worksheet.Cell(3, 1).Value = "Login User";
                        worksheet.Cell(3, 2).Value = LoginUser;
                        worksheet.Cell(3, 3).Style.Font.Bold = true;

                        worksheet.Cell(3, 3).Value = "Date & Time";
                        worksheet.Cell(3, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(3, 4).Style.DateFormat.Format = "dd-MMM-yyyy   h:mm AM/PM";
                        worksheet.Cell(3, 4).Value = currDateTime.ToString();
                        worksheet.Cell(3, 5).Style.Font.Bold = true;
                        worksheet.Cell(3, 5).Value = "Total Count";
                        worksheet.Cell(3, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(3, 6).Value = model.ApplicantTrackingList.Count();



                        var currentRow = 4;
                        #region Header
                        worksheet.Range("A4:F4").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                        worksheet.Cell(currentRow, 1).Value = "Sl.No";
                        worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 2).Value = "Applicant ID";
                        worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 3).Value = " Applicant Name";
                        worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 4).Value = "MRF No.";
                        worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 5).Value = "Job Title";
                        worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 6).Value = " Location";
                        worksheet.Cell(currentRow, 6).Style.Font.Bold = true;


                        //worksheet.Cell(currentRow, 14).Value = "Offer";
                        //worksheet.Cell(currentRow, 14).Style.Font.Bold = true;

                        #endregion

                        #region Body

                        foreach (var item in model.ApplicantTrackingList)
                        {
                            currentRow++;
                            slNo = slNo + 1;
                            worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 1).Value = slNo;
                            worksheet.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 2).Value = item.RefNo;
                            worksheet.Cell(currentRow, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 3).Value = item.ApplicantName;

                            worksheet.Cell(currentRow, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 4).Value = item.JobCode;

                            worksheet.Cell(currentRow, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 5).Value = item.JobTitle;


                            worksheet.Cell(currentRow, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 6).Value = item.Location;
                            


                            worksheet.Columns(1, 6).Style.Alignment.WrapText = true;
                            worksheet.Rows().Style.Alignment.WrapText = true;
                        }
                        #endregion
                        worksheet.Columns().AdjustToContents();
                        worksheet.Rows().AdjustToContents();

                    }

                    else
                    {
                        var worksheet = workbook.Worksheets.Add("RA Approval List");


                        int count = model.ApplicantTrackingList.Count + 4;


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
                        wsReportNameHeaderRange.Value = "RA Approval List";

                        worksheet.Cell(2, 1).Style.Font.Bold = true;
                        worksheet.Cell(2, 1).Value = "Company Name";
                        worksheet.Cell(2, 2).Value = CompanyName.ToUpper();


                        worksheet.Cell(3, 1).Style.Font.Bold = true;
                        worksheet.Cell(3, 1).Value = "Login User";
                        worksheet.Cell(3, 2).Value = LoginUser;
                        worksheet.Cell(3, 3).Style.Font.Bold = true;

                        worksheet.Cell(3, 3).Value = "Date & Time";
                        worksheet.Cell(3, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(3, 4).Style.DateFormat.Format = "dd-MMM-yyyy   h:mm AM/PM";
                        worksheet.Cell(3, 4).Value = currDateTime.ToString();
                        worksheet.Cell(3, 5).Style.Font.Bold = true;
                        worksheet.Cell(3, 5).Value = "Total Count";
                        worksheet.Cell(3, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(3, 6).Value = model.ApplicantTrackingList.Count();



                        var currentRow = 4;
                        #region Header
                        worksheet.Range("A4:J4").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                        worksheet.Cell(currentRow, 1).Value = "Sl.No";
                        worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 2).Value = "Appli. ID";
                        worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 3).Value = " Applicant Name";
                        worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 4).Value = "MRF No.";
                        worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 5).Value = "Job Title";
                        worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 6).Value = "Deadline";
                        worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 7).Value = " D.L.";
                        worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 8).Value = "Location";
                        worksheet.Cell(currentRow, 8).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 9).Value = " Age.";
                        worksheet.Cell(currentRow, 9).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 10).Value = "Y/E";
                        worksheet.Cell(currentRow, 10).Style.Font.Bold = true;


                        //worksheet.Cell(currentRow, 14).Value = "Offer";
                        //worksheet.Cell(currentRow, 14).Style.Font.Bold = true;

                        #endregion

                        #region Body

                        foreach (var item in model.ApplicantTrackingList)
                        {
                            currentRow++;
                            slNo = slNo + 1;
                            worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 1).Value = slNo;
                            worksheet.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 2).Value = item.RefNo;
                            worksheet.Cell(currentRow, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 3).Value = item.ApplicantName;

                            worksheet.Cell(currentRow, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 4).Value = item.JobCode;

                            worksheet.Cell(currentRow, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 5).Value = item.JobTitle;

                            worksheet.Cell(currentRow, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 6).Style.DateFormat.Format = "dd-MMM-yyyy";
                            worksheet.Cell(currentRow, 6).Value = item.NeededByDate;
                            worksheet.Cell(currentRow, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 7).Value = item.DaysLeft;
                            worksheet.Cell(currentRow, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 8).Value = item.Location;
                            worksheet.Cell(currentRow, 9).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 9).Value = item.Age;
                            worksheet.Cell(currentRow, 10).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 10).Value = item.YOS;


                            //worksheet.Cell(currentRow, 14).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            //worksheet.Cell(currentRow, 14).Value = item.OFFER;

                            worksheet.Columns(1, 8).Style.Alignment.WrapText = true;
                            worksheet.Rows().Style.Alignment.WrapText = true;
                        }
                        #endregion
                        worksheet.Columns().AdjustToContents();
                        worksheet.Rows().AdjustToContents();
                    }



                    
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
                            "RA Approval List.xlsx"
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
