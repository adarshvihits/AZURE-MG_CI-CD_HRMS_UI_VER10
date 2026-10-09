using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.Recruitment;
using Hrms.Lite.Shared.Account;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Recruitment;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Spire.Xls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.Recruitment.Controllers
{
    [Area("Recruitment")]
    [SessionAuthorize]
    public class BackgroundVerificationController : Controller
    {
        private readonly IBackgroundVerificationService _service;
        private readonly IOfferProcessingService _offerProcessingService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        public BackgroundVerificationController(IDropdownService dropdownService, ICommonService commonService, IBackgroundVerificationService service ,IOfferProcessingService offerProcessingService, IHelpers helpers)
        {
            _service = service;    
            _commonService = commonService;
            _offerProcessingService = offerProcessingService;
            _helpers = helpers;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                BackgroundVerification model = new BackgroundVerification();
                Filter filter = new Filter();
                OnBoarding m = new OnBoarding();
                m = await _offerProcessingService.GetOfferProcessingList(3, filter, "Pending");
                model.BackgroundVerificationList = new List<Shared.DataBank.DTO.OnBoarding>();
                model.BackgroundVerificationList = m.OnBoardingList;
                model.ALLOfferCount = m.ALLOfferCount;
                model.EmploymentCheckingCount = m.EmploymentCheckingCount;
                model.TabName = "Pending";
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


        public async Task<IActionResult> GetBackGroundVerificationList(string TabName)
        {
            try
            {
                BackgroundVerification model = new BackgroundVerification();
                Filter filter = new Filter();
                OnBoarding m = new OnBoarding();
                m = await _offerProcessingService.GetOfferProcessingList(3, filter, TabName);
                model.BackgroundVerificationList = new List<Shared.DataBank.DTO.OnBoarding>();
                model.BackgroundVerificationList = m.OnBoardingList;
                model.ALLOfferCount = m.ALLOfferCount;
                model.EmploymentCheckingCount = m.EmploymentCheckingCount;

                model.TabName = TabName;
                return PartialView("_BackgroundVerificationList", model);
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
                BackgroundVerification model = new BackgroundVerification();
                model = await _service.GetCandidateDetails(PreEnrollGI);
                model.SelectionStatus = 1;
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


        public async Task<IActionResult> EmpCreditCheckDetailsClick(Guid PreEnrollGI, string formName)
        {
            try
            {
                BackgroundVerification model = new BackgroundVerification();
                model = await _service.GetCandidateDetails(PreEnrollGI);
                model.EmployeeHeader = await _commonService.GetEmployeeHeader(PreEnrollGI);
                model.SelectionStatus = 1;
                
                return View("CreditCheckDetails", model);
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
        public async Task<IActionResult> SaveBackgroundVerification(BackgroundVerification model)
        {
            try
            {
                string filename = "";


                var response = await _service.SaveBackgroundVerification(model);
                if (response.Success == true)
                {
                    response.Message = "Saved Successfully";
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



        public async Task<IActionResult> ExportToExcel(string TabName)
        {
            try
            {
                BackgroundVerification model = new BackgroundVerification();

                var Userlogin = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                int slNo = 0;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Background Verification List", "Excel");
                //Filter filter = new Filter();
                //filter = JsonConvert.DeserializeObject<Filter>(TempData["FilterModel"].ToString());
                Filter filter = new Filter();              
                model.BackgroundVerificationList =  _offerProcessingService.GetOfferProcessingList(3, filter, TabName).Result.OnBoardingList;
                int no = model.BackgroundVerificationList.Count();
                string currDateTime = await _helpers.GetCurrentDate();
                
                using (var workbook = new XLWorkbook())
                {

                    var worksheet = workbook.Worksheets.Add("BackgroundVerification List");


                    int count = model.BackgroundVerificationList.Count + 4;


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
                    wsReportNameHeaderRange.Value = "Background Verification List";

                    worksheet.Cell(2, 1).Style.Font.Bold = true;
                    worksheet.Cell(2, 1).Value = "Total Count";
                    worksheet.Cell(2, 2).Value = model.BackgroundVerificationList.Count();


                    worksheet.Cell(3, 1).Style.Font.Bold = true;
                    worksheet.Cell(3, 1).Value = "Login User";
                    worksheet.Cell(3, 2).Value = Userlogin.authUser.EmployeeName;
                    worksheet.Cell(3, 3).Style.Font.Bold = true;

                    worksheet.Cell(3, 3).Value = "Date & Time";
                    worksheet.Cell(3, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(3, 3).Style.DateFormat.Format = "dd-MMM-yyyy   h:mm AM/PM";
                    worksheet.Cell(3, 4).Value = currDateTime.ToString();
                    worksheet.Cell(3, 5).Style.Font.Bold = true;




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

                    foreach (var item in model.BackgroundVerificationList)
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
                            book.Protect(EP.Password, false, false);
                            book.SaveToStream(stream);
                        }
                        var content = stream.ToArray();
                        return File(
                            content,
                            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                            "Background Verification List.xlsx"
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



        [HttpPost]
        public async Task<IActionResult> BackGroundVerificationDetailsSave(BackgroundVerification model,string Action)
        {
            try
            {
                var response = await _service.BackgroundVerificationDetailsSave(model);
                if (response.Success == true)
                {
                    response.Message = "Saved Successfully";
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
    }
}
