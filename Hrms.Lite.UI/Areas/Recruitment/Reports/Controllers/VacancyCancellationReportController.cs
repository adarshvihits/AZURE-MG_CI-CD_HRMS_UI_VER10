using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.Recruitment;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Spire.Xls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.Recruitment.Reports.Controllers
{
    [Area("Recruitment")]
    [SessionAuthorize]
    public class VacancyCancellationReportController : Controller
    {
        private readonly IVacancyCancellationReportService _service;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        public VacancyCancellationReportController(IVacancyCancellationReportService service, ICommonService commonService, IHelpers helpers)
        {
            _service = service;
            _commonService = commonService;
            _helpers = helpers;
        }

        public async Task<IActionResult> Index()
        {
            VacancyCancellation model = new VacancyCancellation();
            model = await _service.GetList();
            return View("~/Areas/Recruitment/Reports/Views/VacancyCancellationReport/Index.cshtml",model);
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
        public async Task<IActionResult> ResumeView(Guid ResumeGI)
        {
            ResumeDataBank model = new ResumeDataBank();
            model = await _commonService.GetResumeDataBankDetails(ResumeGI);
            return View("~/Areas/DataBank/Views/Shared/_CommonResumeView.cshtml", model);
        }





        public async Task<IActionResult> ExportToExcel()
        {
            VacancyCancellation model = new VacancyCancellation();
            Filter filter = new Filter();
            int slNo = 0;
            //if (Global.GlobalFilter == null)
            //    Global.GlobalFilter = new Filter();
            model = await _service.GetList();
            string currDateTime = await _helpers.GetCurrentDate();
            

            var LoginUser = HttpContext.Session.GetString("LoginUser");
            var CompanyName = HttpContext.Session.GetString("CompanyName");
            ExcelPassword EP = new ExcelPassword();
            EP = await _commonService.GetExcelPassword("Vacancy Cancellation List", "Excel");


            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Vacancy Cancellation List");
                int count = model.VacancyStatusReport.Count + 4;


                worksheet.Range("A1:P" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1:P" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1:P" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1:P" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1:P" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;

                var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 15)));
                wsReportNameHeaderRange.Style.Font.Bold = true;
                wsReportNameHeaderRange.Style.Font.FontSize = 15;
                wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                wsReportNameHeaderRange.Merge();
                wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191); ;
                wsReportNameHeaderRange.Value = "Vacancy Cancellation List";
                worksheet.Range("A4:P4").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
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
                worksheet.Cell(3, 6).Value = model.VacancyStatusReport.Count;

                var currentRow = 4;
                #region Header
                worksheet.Cell(currentRow, 1).Value = "Sl:No";
                worksheet.Cell(currentRow, 1).Style.Font.Bold = true;

                worksheet.Cell(currentRow, 2).Value = "MRF Code";
                worksheet.Cell(currentRow, 2).Style.Font.Bold = true;

                worksheet.Cell(currentRow, 3).Value = "Job Title";
                worksheet.Cell(currentRow, 3).Style.Font.Bold = true;

                worksheet.Cell(currentRow, 4).Value = "Location";
                worksheet.Cell(currentRow, 4).Style.Font.Bold = true;

                worksheet.Cell(currentRow, 5).Value = "Initiated By";
                worksheet.Cell(currentRow, 5).Style.Font.Bold = true;

                worksheet.Cell(currentRow, 6).Value = "N/Req";
                worksheet.Cell(currentRow, 6).Style.Font.Bold = true;

                worksheet.Cell(currentRow, 7).Value = "Map";
                worksheet.Cell(currentRow, 7).Style.Font.Bold = true;

               

                worksheet.Cell(currentRow, 8).Value = "SL";
                worksheet.Cell(currentRow, 8).Style.Font.Bold = true;

                worksheet.Cell(currentRow, 9).Value = "I & E";
                worksheet.Cell(currentRow, 9).Style.Font.Bold = true;

                worksheet.Cell(currentRow, 10).Value = "RAA";
                worksheet.Cell(currentRow, 10).Style.Font.Bold = true;

                worksheet.Cell(currentRow, 11).Value = "Salary";
                worksheet.Cell(currentRow, 11).Style.Font.Bold = true;

                worksheet.Cell(currentRow, 12).Value = "CC";
                worksheet.Cell(currentRow, 12).Style.Font.Bold = true;

                worksheet.Cell(currentRow, 13).Value = "EC";
                worksheet.Cell(currentRow, 13).Style.Font.Bold = true;

                worksheet.Cell(currentRow, 14).Value = "Can.Date";
                worksheet.Cell(currentRow, 14).Style.Font.Bold = true;

                worksheet.Cell(currentRow, 15).Value = "Can.User";
                worksheet.Cell(currentRow, 15).Style.Font.Bold = true;

                worksheet.Cell(currentRow, 16).Value = "Reason";
                worksheet.Cell(currentRow, 16).Style.Font.Bold = true;



                #endregion

                #region Body
                foreach (var item in model.VacancyStatusReport)
                {
                    currentRow++;
                    slNo = slNo + 1;

                    worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(currentRow, 1).Value = slNo;

                    worksheet.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(currentRow, 2).Value = item.JobCode;

                    worksheet.Cell(currentRow, 3).Value = item.JobTitle;
                    worksheet.Cell(currentRow, 4).Value = item.LocationName;
                    worksheet.Cell(currentRow, 5).Value = item.RequestedBy;
                    worksheet.Cell(currentRow, 6).Value = item.NoOfRequirements;
                    worksheet.Cell(currentRow, 7).Value = item.MAP;
                    worksheet.Cell(currentRow, 8).Value = item.SL;
                   
                    worksheet.Cell(currentRow, 9).Value = item.IE;
                    worksheet.Cell(currentRow, 10).Value = item.RAA;
                    worksheet.Cell(currentRow, 11).Value = item.SALARY;
                    worksheet.Cell(currentRow, 12).Value = item.CC;
                    worksheet.Cell(currentRow, 13).Value = item.EC;
                    worksheet.Cell(currentRow, 14).Value = item.CancelledDate;
                    worksheet.Cell(currentRow, 14).Style.DateFormat.Format = "dd-MMM-yyyy";
                    worksheet.Cell(currentRow, 15).Value = item.CancelledBy;
                    worksheet.Cell(currentRow, 16).Value = item.Reason;


                    worksheet.Columns(1, 16).Style.Alignment.WrapText = true;

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
                        "vacancy cancellation report List.xlsx"
                        );
                }
            }







        }
    }
}
