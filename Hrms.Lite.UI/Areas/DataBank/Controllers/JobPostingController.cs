using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Spire.Xls;

namespace Hrms.Lite.UI.Areas.DataBank.Controllers
{
    [Area("DataBank")]
    [SessionAuthorize]
    public class JobPostingController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly ICommonService _commonService;
        private readonly IJobPostingService _service;
        private readonly IHelpers _helpers;
        public JobPostingController(IDropdownService dropdownService, ICommonService commonService, IJobPostingService service, IHelpers helpers)
        {
            _dropdownService = dropdownService;
            _commonService = commonService;
            _service = service;
            _helpers = helpers;
        }
        public async Task<IActionResult> Index()
        {
            JobPosting model = new JobPosting();         
             model = await _service.GetJobPostingList(1);
            ViewBag.RedirectigFRom = "JobPosting";
            return View(model);
        }

        public async Task<IActionResult> FromRecruitmentDashBoard()
        {
            JobPosting model = new JobPosting();
            model = await _service.GetJobPostingList(2);
            ViewBag.RedirectigFRom = "FromRecruitDashBoard";
            return View("Index",model);
        }
        public async Task<IActionResult> Getlist(int TabIndex)
        {
            try
            {
                JobPosting model = new JobPosting();
                model = await _service.GetJobPostingList(TabIndex);
                if (TabIndex == 1)
                {
                    return PartialView("_PendingPostList", model);
                }
                else
                {
                    return PartialView("_PostedList", model);
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
        public async Task<IActionResult> Create(Guid GI,string mode)
        {
            try
            {
                JobPosting model = new JobPosting();
                model = await _service.GetJobPostingDetails(GI,mode);
                model.MRFGI = GI;
                return PartialView("_JobPostingSlider", model);
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
        public async Task<IActionResult> JPCreate(JobPosting model)
        {
            try
            {

                var response = await _service.JobPostingSave(model);
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
   
        public async Task<IActionResult> JPDelete(Guid VacancyManagementGI)
        {
            try
            {

                var response = await _service.JobPostingDelete(VacancyManagementGI);
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



        public async Task<IActionResult> ExportToExcel(int TabIndex)
        {
            try
            {
                JobPosting model = new JobPosting();
                model = await _service.GetJobPostingList(TabIndex);
                int slno = 0;
                string currDateTime = await _helpers.GetCurrentDate();
                
                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Job Posting", "Excel");
                using (var workbook = new XLWorkbook())
                {

                    if (TabIndex == 1)
                    {
                        var worksheet = workbook.Worksheets.Add("JobPosting Pending List");
                        
                        int PendingCountnum = model.JobPostingList.Count + 4;


                        worksheet.Range("A1:I" + PendingCountnum).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:I" + PendingCountnum).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:I" + PendingCountnum).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:I" + PendingCountnum).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:I" + PendingCountnum).Style.Border.LeftBorder = XLBorderStyleValues.Thin;



                        var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 8)));
                        wsReportNameHeaderRange.Style.Font.Bold = true;
                        wsReportNameHeaderRange.Style.Font.FontSize = 15;
                        wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                        wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        wsReportNameHeaderRange.Merge();


                        wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                        wsReportNameHeaderRange.Style.Font.FontSize = 15;
                        wsReportNameHeaderRange.Value = "Job Posting List";
                        worksheet.Range("A4:I4").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                        //worksheet.Range("A6:I6").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
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
                        worksheet.Cell(3, 5).Value = "Total Count";
                        worksheet.Cell(3, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(3, 6).Value = model.JobPostingList.Count;
                        var currentRow = 4;
                        #region Header
                        worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 1).Value = "Sl.No";
                        worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 2).Value = "MRF No.";
                        worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 3).Value = "Job Title";
                        worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 4).Value = "Applied Designation";
                        worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 5).Value = "Applied Location";
                        worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 6).Value = "Initiated On";
                        worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 7).Value = "Deadline";
                        worksheet.Cell(currentRow, 8).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 8).Value = "N / Req";
                        worksheet.Cell(currentRow, 9).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 9).Value = "D.L.";




                        #endregion

                        #region Body
                        foreach (var item in model.JobPostingList)
                        {
                            currentRow++;
                            slno = slno + 1;
                            worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 1).Value = slno;
                            worksheet.Cell(currentRow, 2).Value = item.JobCode;
                            worksheet.Cell(currentRow, 3).Value = item.JobTitle;
                            worksheet.Cell(currentRow, 4).Value = item.Designation;
                            worksheet.Cell(currentRow, 5).Value = item.Location;
                            worksheet.Cell(currentRow, 6).Value = item.RequestedDate;
                            worksheet.Cell(currentRow, 6).Style.DateFormat.Format = "dd-MMM-yyyy";
                            worksheet.Cell(currentRow, 7).Style.DateFormat.Format = "dd-MMM-yyyy";
                            worksheet.Cell(currentRow, 7).Value = item.NeedByDate;
                            worksheet.Cell(currentRow, 8).Value = item.NoOfRequirements;
                            worksheet.Cell(currentRow, 9).Value = item.Aging;
                            worksheet.Columns(1, 9).Style.Alignment.WrapText = true;

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
                                book.Protect(EP.Password,false,false);
                                book.SaveToStream(stream);
                            }
                            var content = stream.ToArray();
                            return File(
                                content,
                                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                                "Job Posting Pending List.xlsx"
                                );
                        }
                    }
                    else
                    {
                       
                            var worksheet = workbook.Worksheets.Add("JobPosting List");

                            int PendingCountnum = model.JobPostingList.Count + 4;


                            worksheet.Range("A1:H" + PendingCountnum).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                            worksheet.Range("A1:H" + PendingCountnum).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                            worksheet.Range("A1:H" + PendingCountnum).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                            worksheet.Range("A1:H" + PendingCountnum).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                            worksheet.Range("A1:H" + PendingCountnum).Style.Border.LeftBorder = XLBorderStyleValues.Thin;



                            var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 7)));
                            wsReportNameHeaderRange.Style.Font.Bold = true;
                            wsReportNameHeaderRange.Style.Font.FontSize = 15;
                            wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                            wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                            wsReportNameHeaderRange.Merge();


                            wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                            wsReportNameHeaderRange.Style.Font.FontSize = 15;
                            wsReportNameHeaderRange.Value = "Job Posting List";
                            worksheet.Range("A4:H4").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
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
                        worksheet.Cell(3, 5).Value = "Total Count";
                        worksheet.Cell(3, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(3, 6).Value = model.JobPostingList.Count;
                        var currentRow = 4;
                            #region Header
                            worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                            worksheet.Cell(currentRow, 1).Value = "Sl.No";
                            worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                            worksheet.Cell(currentRow, 2).Value = "MRF No.";
                            worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                            worksheet.Cell(currentRow, 3).Value = "Job Title";
                            worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                            worksheet.Cell(currentRow, 4).Value = "Applied Designation";
                            worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                            worksheet.Cell(currentRow, 5).Value = "Initiated On";
                            worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
                            worksheet.Cell(currentRow, 6).Value = "N / Req";
                            worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                            worksheet.Cell(currentRow, 7).Value = "D.L.";
                            worksheet.Cell(currentRow, 8).Style.Font.Bold = true;
                            worksheet.Cell(currentRow, 8).Value = "Job Posted In";
                           




                            #endregion

                            #region Body
                            foreach (var item in model.JobPostingList)
                            {
                                currentRow++;
                                slno = slno + 1;
                                worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                                worksheet.Cell(currentRow, 1).Value = slno;
                                worksheet.Cell(currentRow, 2).Value = item.JobCode;
                                worksheet.Cell(currentRow, 3).Value = item.JobTitle;
                                worksheet.Cell(currentRow, 4).Value = item.Designation;
                                worksheet.Cell(currentRow, 5).Value = item.RequestedDate;
                                worksheet.Cell(currentRow, 5).Style.DateFormat.Format = "dd-MMM-yyyy";
                            worksheet.Cell(currentRow, 6).Value = item.NoOfRequirements;
                            worksheet.Cell(currentRow, 8).Value = item.JobpostedIn;
                            worksheet.Cell(currentRow, 8).Style.DateFormat.Format = "dd-MMM-yyyy";
                            worksheet.Cell(currentRow, 7).Value = item.Aging;
                                worksheet.Columns(1, 8).Style.Alignment.WrapText = true;

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
                                    book.Protect(EP.Password,false,false);
                                    book.SaveToStream(stream);
                                }
                                var content = stream.ToArray();
                                return File(
                                    content,
                                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                                    "Job Posting List.xlsx"
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
