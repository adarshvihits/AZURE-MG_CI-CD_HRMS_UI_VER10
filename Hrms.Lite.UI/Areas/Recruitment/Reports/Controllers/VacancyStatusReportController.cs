using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.Recruitment;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Recruitment.Reports;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Spire.Xls;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.Recruitment.Reports.Controllers
{
    [Area("Recruitment")]
    [SessionAuthorize]
    public class VacancyStatusReportController : Controller
    {
        private readonly IVacancyStatusReportService _service;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        public static class Global
        {
            public static Filter GlobalFilter;
            
        }
        public VacancyStatusReportController(IVacancyStatusReportService service, ICommonService commonService, IHelpers Helpers)
        {
            _service = service;
            _commonService = commonService;
            _helpers = Helpers;
        }
        public async Task<IActionResult> Index()
        {
            RecruitmentReports model = new RecruitmentReports();
            //model = await _service.GetVacancyStatusReport();
            return View("~/Areas/Recruitment/Reports/Views/VacancyStatusReport/Index.cshtml", model);
        }


        [HttpPost]
        public async Task<IActionResult> GetList(Filter filter1)
        {
            try
            {
                RecruitmentReports model = new RecruitmentReports();
                RecruitmentReports m = new RecruitmentReports();
                //model.EmployeeHeader = new EmployeeHeader();




                Filter filter = new Filter();
                Global.GlobalFilter = filter1;


                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }
                model = await _service.GetVacancyStatusReport(filter1);

                m.VacancyStatusReport = model.VacancyStatusReport;
                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.VacancyStatusReport == null || model.VacancyStatusReport.Count() == 0)
                {
                    //model.RequestVsAuthorityList = new List<RequestVsAuthorityList>();
                    //model.RequestVsAuthorityList.Add(new RequestVsAuthorityList());


                    return Json(new
                    {
                        data = "",
                        draw = filter1.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows
                    });
                }




                if (model.VacancyStatusReport.Count > 0)
                {
                    totalrows = m.VacancyStatusReport[0].active;

                    //m.OnBoardingList.ToList<Hrms.Lite.Shared.Master.Test>().Count; ;
                }

                //model.AllTabCount = m.AllTabCount;
                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = m.VacancyStatusReport[0].totalRecords;
                    return Json(new
                    {
                        data = model.VacancyStatusReport,
                        draw = filter1.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows//totalrowsafterfiltering
                    });
                }
                else
                {
                    // return Json(new { data = model.EmployeeProfileList, draw = filter1.draw, recordsTotal = totalrows, recordsFiltered = totalrows });
                    return Json(new
                    {
                        data = model.VacancyStatusReport,
                        draw = filter1.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows
                    });
                }


            }
            catch (Exception ex)
            {
                return View();
            }




        }



        public async Task<IActionResult> ExportToExcel(int totRecords)
        {
            try
            {
                RecruitmentReports model = new RecruitmentReports();
                if (Global.GlobalFilter == null)
                    Global.GlobalFilter = new Filter();
                else
                {
                    Global.GlobalFilter.length = totRecords;
                    Global.GlobalFilter.start = 0;
                }
                //string FilterValues = await _helpers.GetFilterValues(Global.GlobalFilter);

                //model = await _service.GetVacancyStatusReport(Global.GlobalFilter);

                var dataset = await _service.GetVacancyStatusReport_Excel(Global.GlobalFilter);
                DataTable dt = dataset.Tables[0];

                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (dt.Columns[i].ColumnName == "doj" || dt.Columns[i].ColumnName == "dob" || dt.Columns[i].ColumnName == "y/s")
                        dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.ToUpper();
                    else
                        dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
                }

                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Vacancy status report ","Excel");
                int slNo = 0;
                //if (FilterValues == "")
                //{
                //    FilterValues = "All";
                //}
                string currDateTime = await _helpers.GetCurrentDate();
                
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Vacancy Status Report");
                    //int count = model.JoiningReportList.Count + 5;
                    int count = dt.Rows.Count + 5;


                    worksheet.Range("A1:O" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:O" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:O" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:O" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:O" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 14)));
                    wsReportNameHeaderRange.Style.Font.Bold = true;
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Merge();
                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191); ;
                    wsReportNameHeaderRange.Value = "Vacancy Status Report";

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
                    worksheet.Cell(3, 6).Value = dt.Rows.Count;
                    //worksheet.Cell(4, 1).Style.Font.Bold = true;
                    //worksheet.Cell(4, 1).Value = "Filter Applied";
                    //worksheet.Range("B4:P4").Merge();
                    //worksheet.Cell("B4").Value = FilterValues;





                    var currentRow = 5;
                    worksheet.Cell(currentRow, 1).InsertTable(dt);
                    worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                    worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                    worksheet.Range("A5:Q5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Range("A5:Q5").Style.Font.Bold = true;


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
                            "Vacancy Status Report.xlsx"
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
    }
}
