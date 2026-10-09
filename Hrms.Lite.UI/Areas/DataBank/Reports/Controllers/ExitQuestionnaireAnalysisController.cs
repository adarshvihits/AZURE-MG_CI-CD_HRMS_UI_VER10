using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.DataBank.Reports;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.Account;
using Hrms.Lite.Shared.DataBank.Reports;
using Hrms.Lite.Shared.General;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Spire.Xls;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.DataBank.Reports.Controllers
{
    [Area("DataBank")]
    [SessionAuthorize]
    public class ExitQuestionnaireAnalysisController : Controller
    {
        private readonly IExitQuestionnaireAnalysisService _service;
        private readonly IDropdownService _dropdownService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        
        public ExitQuestionnaireAnalysisController(IExitQuestionnaireAnalysisService service, IDropdownService dropdownService,ICommonService commonService,IHelpers helpers)
        {
            _service = service;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = helpers;
        }
        public async Task<IActionResult> Index()
        {
            try{
            ExitQuestionnaireAnalysis model = new ExitQuestionnaireAnalysis();
            model = await _service.ExitQuestionnaireAnalysisReport();
            return View("~/Areas/DataBank/Reports/Views/ExitQuestionnaireAnalysis/Index.cshtml",model);
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
        public async Task<IActionResult> GetDetails(int QuestionCode, int AnswerCode)
        {
            ExitQuestionnaireAnalysis model = new ExitQuestionnaireAnalysis();
            Filter filter = new Filter();
            model = await _service.GetQuestionnaireDetails(QuestionCode, AnswerCode,filter);
            model.AnswerCode = AnswerCode;
            model.QuestionCode = QuestionCode;
            model.SliderFilter = await _commonService.GetFilterDropdowns();
            return PartialView("~/Areas/DataBank/Reports/Views/ExitQuestionnaireAnalysis/_Popup.cshtml",model);
        }
        public async Task<IActionResult> ApplyFilter(int QuestionCode, int AnswerCode, Filter filter)
        {
            try
            {
                ExitQuestionnaireAnalysis model = new ExitQuestionnaireAnalysis();
                model.SliderFilter= filter;
                model = await _service.GetQuestionnaireDetails(QuestionCode, AnswerCode, filter);
                return PartialView("~/Areas/DataBank/Reports/Views/ExitQuestionnaireAnalysis/_EmployeeList.cshtml", model);
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

        public async Task<ActionResult> EExportToExcel()
        {
            try
            {
                ExitQuestionnaireAnalysis model = new ExitQuestionnaireAnalysis();
                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Exit Questionnaire Analysis", "Excel");
                int slNo = 0;
                model = await _service.ExitQuestionnaireAnalysisReport();
                 string currDateTime = await _helpers.GetCurrentDate();
                

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Exit Questionnaire Analysis");

                    int count = model.ExitQuestionnaireAnalysisList.Count + 5;


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
                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191); ;
                    wsReportNameHeaderRange.Value = "Exit Questionnaire Analysis";

                    worksheet.Cell(2, 1).Style.Font.Bold = true;
                    worksheet.Cell(2, 1).Value = "Group Company Name";
                    worksheet.Cell(2, 3).Style.Font.Bold = true;
                    worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                    worksheet.Cell(3, 1).Style.Font.Bold = true;
                    worksheet.Cell(3, 1).Value = "Login User";
                    worksheet.Cell(3, 2).Value = LoginUser;
                    worksheet.Cell(3, 3).Style.Font.Bold = true;
                    worksheet.Cell(3, 3).Value = "Date & Time";
                    worksheet.Cell(3, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(3, 4).Style.DateFormat.Format = "dd-MMM-yyyy   h:mm AM/PM";
                    worksheet.Cell(3, 4).Value = currDateTime.ToString();
                    worksheet.Cell(4, 1).Style.Font.Bold = true;
                    worksheet.Cell(4, 1).Value = "No. of questions";
                    var currentRow = 5;
                    #region Header
                    worksheet.Range("A5:F5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Cell(currentRow, 1).Value = "Sl.No";
                    worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 2).Value = "Questions";
                    worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 3).Value = "Total Attempts";
                    worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 4).Value = "Answers";
                    worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 5).Value = "Count";
                    worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 6).Value = "Percentage";
                    worksheet.Cell(currentRow, 6).Style.Font.Bold = true;


                    var groups = model.ExitQuestionnaireAnalysisList.GroupBy(x => x.QuestionCode);
                    int Next_EmployeeCode = 0;
                    int a = currentRow + 1;
                    var EmpWiseLeaveRowCount = 1;
                    var nxtEmpWiseLeaveRowCount = 6;
                    var firstIteration = 0;
                    var secondIteration = 0;
                    var EmpCount = 0;
                    foreach (var group in groups)
                    {
                        int rowspanCount = group.Count();

                        foreach (var item in group)
                        {
                            var EmployeeCode = @item.QuestionCode;
                            if (EmployeeCode != Next_EmployeeCode)
                            {
                                Next_EmployeeCode = EmployeeCode;
                                currentRow++;
                                slNo++;
                                //a = rowspanCount + currentRow - 1;
                                EmpCount++;
                                worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                                worksheet.Cell(currentRow, 1).Value = slNo;


                                //worksheet.Range(worksheet.Cell(currentRow, 7).Address, worksheet.Cell(currentRow+1, 7).Address).Style.Border.InsideBorder = XLBorderStyleValues.Dotted;


                                worksheet.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                                worksheet.Cell(currentRow, 2).Value = item.Question;
                                worksheet.Columns(currentRow, 2).AdjustToContents();
                                worksheet.Cell(currentRow, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                                worksheet.Cell(currentRow, 3).Value = item.TotalAttempts;

                                //worksheet.Cell(currentRow, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                                //worksheet.Cell(currentRow, 4).Value = item.Designation;
                                //worksheet.Cell(currentRow, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                                //worksheet.Cell(currentRow, 5).Value = item.Department;

                                //worksheet.Cell(currentRow, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                                //worksheet.Cell(currentRow, 6).Value = item.Location;
                                worksheet.Cell(currentRow, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);



                                if (firstIteration == 1)
                                {
                                    if (secondIteration == 1)
                                    {
                                        nxtEmpWiseLeaveRowCount = nxtEmpWiseLeaveRowCount + EmpWiseLeaveRowCount;
                                        worksheet.Range(worksheet.Cell("A" + nxtEmpWiseLeaveRowCount).Address, worksheet.Cell("A" + ((nxtEmpWiseLeaveRowCount - 1) + EmpWiseLeaveRowCount)).Address).Merge();
                                        worksheet.Range(worksheet.Cell("A" + nxtEmpWiseLeaveRowCount).Address, worksheet.Cell("A" + ((nxtEmpWiseLeaveRowCount - 1) + EmpWiseLeaveRowCount)).Address).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                        worksheet.Range(worksheet.Cell("A" + nxtEmpWiseLeaveRowCount).Address, worksheet.Cell("A" + ((nxtEmpWiseLeaveRowCount - 1) + EmpWiseLeaveRowCount)).Address).Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                                        worksheet.Range(worksheet.Cell("B" + nxtEmpWiseLeaveRowCount).Address, worksheet.Cell("B" + ((nxtEmpWiseLeaveRowCount - 1) + EmpWiseLeaveRowCount)).Address).Merge();
                                        worksheet.Range(worksheet.Cell("B" + nxtEmpWiseLeaveRowCount).Address, worksheet.Cell("B" + ((nxtEmpWiseLeaveRowCount - 1) + EmpWiseLeaveRowCount)).Address).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                        worksheet.Range(worksheet.Cell("B" + nxtEmpWiseLeaveRowCount).Address, worksheet.Cell("B" + ((nxtEmpWiseLeaveRowCount - 1) + EmpWiseLeaveRowCount)).Address).Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                                        worksheet.Range(worksheet.Cell("C" + nxtEmpWiseLeaveRowCount).Address, worksheet.Cell("C" + ((nxtEmpWiseLeaveRowCount - 1) + EmpWiseLeaveRowCount)).Address).Merge();
                                        worksheet.Range(worksheet.Cell("C" + nxtEmpWiseLeaveRowCount).Address, worksheet.Cell("C" + ((nxtEmpWiseLeaveRowCount - 1) + EmpWiseLeaveRowCount)).Address).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                        worksheet.Range(worksheet.Cell("C" + nxtEmpWiseLeaveRowCount).Address, worksheet.Cell("C" + ((nxtEmpWiseLeaveRowCount - 1) + EmpWiseLeaveRowCount)).Address).Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                                       
                                    }
                                    else
                                    {
                                        worksheet.Range(worksheet.Cell("A6").Address, worksheet.Cell("A" + (EmpWiseLeaveRowCount + 5)).Address).Merge();
                                        worksheet.Range(worksheet.Cell("A6").Address, worksheet.Cell("A" + (EmpWiseLeaveRowCount + 5)).Address).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                        worksheet.Range(worksheet.Cell("A6").Address, worksheet.Cell("A" + (EmpWiseLeaveRowCount + 5)).Address).Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                                        worksheet.Range(worksheet.Cell("B6").Address, worksheet.Cell("B" + (EmpWiseLeaveRowCount + 5)).Address).Merge();
                                        worksheet.Range(worksheet.Cell("B6").Address, worksheet.Cell("B" + (EmpWiseLeaveRowCount + 5)).Address).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                        worksheet.Range(worksheet.Cell("B6").Address, worksheet.Cell("B" + (EmpWiseLeaveRowCount + 5)).Address).Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                                        worksheet.Range(worksheet.Cell("C6").Address, worksheet.Cell("C" + (EmpWiseLeaveRowCount + 5)).Address).Merge();
                                        worksheet.Range(worksheet.Cell("C6").Address, worksheet.Cell("C" + (EmpWiseLeaveRowCount + 5)).Address).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                        worksheet.Range(worksheet.Cell("C6").Address, worksheet.Cell("C" + (EmpWiseLeaveRowCount + 5)).Address).Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                                        
                                        secondIteration = 1;
                                    }
                                }
                                EmpWiseLeaveRowCount = 1;
                                firstIteration = 1;
                            }
                            else
                            {
                                currentRow++;
                                EmpWiseLeaveRowCount++;

                            }

                            worksheet.Cell(currentRow, 4).Value = item.Answer;

                            worksheet.Cell(currentRow, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 5).Value = item.Count;
                            worksheet.Cell(currentRow, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 6).Value = item.Percentage;
                            
                            worksheet.Columns(1, 6).Style.Alignment.WrapText = true;

                        }
                    }

                    var LastMerge = nxtEmpWiseLeaveRowCount + EmpWiseLeaveRowCount;
                    worksheet.Range(worksheet.Cell("A" + LastMerge).Address, worksheet.Cell("A" + ((LastMerge - 1) + EmpWiseLeaveRowCount)).Address).Merge();
                    worksheet.Range(worksheet.Cell("A" + LastMerge).Address, worksheet.Cell("A" + ((LastMerge - 1) + EmpWiseLeaveRowCount)).Address).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    worksheet.Range(worksheet.Cell("A" + LastMerge).Address, worksheet.Cell("A" + ((LastMerge - 1) + EmpWiseLeaveRowCount)).Address).Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                    worksheet.Range(worksheet.Cell("B" + LastMerge).Address, worksheet.Cell("B" + ((LastMerge - 1) + EmpWiseLeaveRowCount)).Address).Merge();
                    worksheet.Range(worksheet.Cell("B" + LastMerge).Address, worksheet.Cell("B" + ((LastMerge - 1) + EmpWiseLeaveRowCount)).Address).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    worksheet.Range(worksheet.Cell("B" + LastMerge).Address, worksheet.Cell("B" + ((LastMerge - 1) + EmpWiseLeaveRowCount)).Address).Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                    worksheet.Range(worksheet.Cell("C" + LastMerge).Address, worksheet.Cell("C" + ((LastMerge - 1) + EmpWiseLeaveRowCount)).Address).Merge();
                    worksheet.Range(worksheet.Cell("C" + LastMerge).Address, worksheet.Cell("C" + ((LastMerge - 1) + EmpWiseLeaveRowCount)).Address).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    worksheet.Range(worksheet.Cell("C" + LastMerge).Address, worksheet.Cell("C" + ((LastMerge - 1) + EmpWiseLeaveRowCount)).Address).Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);


                    //worksheet.Cell(3, 5).Style.Font.Bold = true;
                    //worksheet.Cell(3, 5).Value = "Total Count";
                    //worksheet.Cell(3, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    //worksheet.Cell(3, 6).Value = EmpCount;
                    worksheet.Cell("B4").Value = slNo;
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
                          "Exit Questionnaire Analysis List.xlsx"
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
        public async Task<IActionResult> ExportToExcel()
        {

            //try
            //{
            //    ExitQuestionnaireAnalysis model = new ExitQuestionnaireAnalysis();

            //    var LoginUser = HttpContext.Session.GetString("LoginUser");
            //    var CompanyName = HttpContext.Session.GetString("CompanyName");
            //    var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
            //    var GroupCompanyName = User.authUser.GroupCompanyName;
            //    ExcelPassword EP = new ExcelPassword();
            //    EP = await _commonService.GetExcelPassword("Exit Questionnaire Analysis", "Excel");
            //    int slNo = 0;

            //    model = await _service.ExitQuestionnaireAnalysisReport();
            //    string currDateTime = await _helpers.GetCurrentDate();
            //    
            //    using (var workbook = new XLWorkbook())
            //    {

            //        var worksheet = workbook.Worksheets.Add("Exit Questionnaire Analysis");


            //        int count = model.ExitQuestionnaireAnalysisList.Count + 4;


            //        worksheet.Range("A1:F" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            //        worksheet.Range("A1:F" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
            //        worksheet.Range("A1:F" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            //        worksheet.Range("A1:F" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
            //        worksheet.Range("A1:F" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;






            //        var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 5)));
            //        wsReportNameHeaderRange.Style.Font.Bold = true;
            //        wsReportNameHeaderRange.Style.Font.FontSize = 15;
            //        wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            //        wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            //        wsReportNameHeaderRange.Merge();
            //        wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
            //        wsReportNameHeaderRange.Value = "Exit Questionnaire Analysis";

            //        worksheet.Cell(2, 1).Style.Font.Bold = true;
            //        worksheet.Cell(2, 1).Value = "Group Company Name";
            //        worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();


            //        worksheet.Cell(3, 1).Style.Font.Bold = true;
            //        worksheet.Cell(3, 1).Value = "Login User";
            //        worksheet.Cell(3, 2).Value = LoginUser;
            //        worksheet.Cell(3, 3).Style.Font.Bold = true;
            //        worksheet.Cell(3, 3).Value = "Date & Time";
            //        worksheet.Cell(3, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
            //        worksheet.Cell(3, 4).Style.DateFormat.Format = "dd-MMM-yyyy   h:mm AM/PM";
            //        worksheet.Cell(3, 4).Value = currDateTime.ToString();
            //        worksheet.Cell(3, 5).Style.Font.Bold = true;
            //        worksheet.Cell(3, 5).Value = "No. of questions";
            //        worksheet.Cell(3, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);



            //        var currentRow = 4;
            //        #region Header
            //        worksheet.Range("A4:F4").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
            //        worksheet.Cell(currentRow, 1).Value = "Sl.No";
            //        worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
            //        worksheet.Cell(currentRow, 2).Value = "Questions";
            //        worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
            //        worksheet.Cell(currentRow, 3).Value = "Total Attempts";
            //        worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
            //        worksheet.Cell(currentRow, 4).Value = "Answers";
            //        worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
            //        worksheet.Cell(currentRow, 5).Value = "Count";
            //        worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
            //        worksheet.Cell(currentRow, 6).Value = "Percentage";
            //        worksheet.Cell(currentRow, 6).Style.Font.Bold = true;



            //        #endregion

            //        #region Body
            //        var groups = model.ExitQuestionnaireAnalysisList.GroupBy(x => x.QuestionCode);
            //        int Next_EmployeeCode = 0;
            //        int a = currentRow + 1;
            //        var EmpWiseLeaveRowCount = 1;
            //        var nxtEmpWiseLeaveRowCount = 5;
            //        var firstIteration = 0;
            //        var secondIteration = 0;
            //        var EmpCount = 0;
            //        foreach (var group in groups)
            //        {
            //            int rowspanCount = group.Count();

            //            foreach (var item in group)
            //            {
            //                var EmployeeCode = @item.QuestionCode;
            //                if (EmployeeCode != Next_EmployeeCode)
            //                {
            //                    Next_EmployeeCode = EmployeeCode;
            //                    currentRow++;
            //                    slNo++;
            //                    //a = rowspanCount + currentRow - 1;
            //                    EmpCount++;
            //                    worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            //                    worksheet.Cell(currentRow, 1).Value = slNo;


            //                    //worksheet.Range(worksheet.Cell(currentRow, 7).Address, worksheet.Cell(currentRow+1, 7).Address).Style.Border.InsideBorder = XLBorderStyleValues.Dotted;


            //                    worksheet.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            //                    worksheet.Cell(currentRow, 2).Value = item.Question;
            //                    worksheet.Cell(currentRow, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            //                    worksheet.Cell(currentRow, 3).Value = item.TotalAttempts;




            //                    if (firstIteration == 1)
            //                    {
            //                        if (secondIteration == 1)
            //                        {
            //                            nxtEmpWiseLeaveRowCount = nxtEmpWiseLeaveRowCount + EmpWiseLeaveRowCount;
            //                            worksheet.Range(worksheet.Cell("A" + nxtEmpWiseLeaveRowCount).Address, worksheet.Cell("A" + ((nxtEmpWiseLeaveRowCount - 1) + EmpWiseLeaveRowCount)).Address).Merge();
            //                            worksheet.Range(worksheet.Cell("A" + nxtEmpWiseLeaveRowCount).Address, worksheet.Cell("A" + ((nxtEmpWiseLeaveRowCount - 1) + EmpWiseLeaveRowCount)).Address).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            //                            worksheet.Range(worksheet.Cell("A" + nxtEmpWiseLeaveRowCount).Address, worksheet.Cell("A" + ((nxtEmpWiseLeaveRowCount - 1) + EmpWiseLeaveRowCount)).Address).Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
            //                            worksheet.Range(worksheet.Cell("B" + nxtEmpWiseLeaveRowCount).Address, worksheet.Cell("B" + ((nxtEmpWiseLeaveRowCount - 1) + EmpWiseLeaveRowCount)).Address).Merge();
            //                            worksheet.Range(worksheet.Cell("B" + nxtEmpWiseLeaveRowCount).Address, worksheet.Cell("B" + ((nxtEmpWiseLeaveRowCount - 1) + EmpWiseLeaveRowCount)).Address).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            //                            worksheet.Range(worksheet.Cell("B" + nxtEmpWiseLeaveRowCount).Address, worksheet.Cell("B" + ((nxtEmpWiseLeaveRowCount - 1) + EmpWiseLeaveRowCount)).Address).Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
            //                            worksheet.Range(worksheet.Cell("C" + nxtEmpWiseLeaveRowCount).Address, worksheet.Cell("C" + ((nxtEmpWiseLeaveRowCount - 1) + EmpWiseLeaveRowCount)).Address).Merge();
            //                            worksheet.Range(worksheet.Cell("C" + nxtEmpWiseLeaveRowCount).Address, worksheet.Cell("C" + ((nxtEmpWiseLeaveRowCount - 1) + EmpWiseLeaveRowCount)).Address).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            //                            worksheet.Range(worksheet.Cell("C" + nxtEmpWiseLeaveRowCount).Address, worksheet.Cell("C" + ((nxtEmpWiseLeaveRowCount - 1) + EmpWiseLeaveRowCount)).Address).Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);

            //                        }
            //                        else
            //                        {
            //                            worksheet.Range(worksheet.Cell("A5").Address, worksheet.Cell("A" + (EmpWiseLeaveRowCount + 4)).Address).Merge();
            //                            worksheet.Range(worksheet.Cell("A5").Address, worksheet.Cell("A" + (EmpWiseLeaveRowCount + 4)).Address).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            //                            worksheet.Range(worksheet.Cell("A5").Address, worksheet.Cell("A" + (EmpWiseLeaveRowCount + 4)).Address).Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
            //                            worksheet.Range(worksheet.Cell("B5").Address, worksheet.Cell("B" + (EmpWiseLeaveRowCount + 4)).Address).Merge();
            //                            worksheet.Range(worksheet.Cell("B5").Address, worksheet.Cell("B" + (EmpWiseLeaveRowCount + 4)).Address).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            //                            worksheet.Range(worksheet.Cell("B5").Address, worksheet.Cell("B" + (EmpWiseLeaveRowCount + 4)).Address).Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
            //                            worksheet.Range(worksheet.Cell("C5").Address, worksheet.Cell("C" + (EmpWiseLeaveRowCount + 4)).Address).Merge();
            //                            worksheet.Range(worksheet.Cell("C5").Address, worksheet.Cell("C" + (EmpWiseLeaveRowCount + 4)).Address).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            //                            worksheet.Range(worksheet.Cell("C5").Address, worksheet.Cell("C" + (EmpWiseLeaveRowCount + 4)).Address).Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
            //                            secondIteration = 1;
            //                        }
            //                    }
            //                    EmpWiseLeaveRowCount = 1;
            //                    firstIteration = 1;
            //                }
            //                else
            //                {
            //                    currentRow++;
            //                    EmpWiseLeaveRowCount++;

            //                }

            //                worksheet.Cell(currentRow, 4).Value = item.Answer;

            //                worksheet.Cell(currentRow, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            //                worksheet.Cell(currentRow, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
            //                worksheet.Cell(currentRow, 5).Value = item.Count;
            //                worksheet.Cell(currentRow, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
            //                worksheet.Cell(currentRow, 6).Value = item.Percentage;
            //                worksheet.Columns(1, 6).Style.Alignment.WrapText = true;

            //            }
            //        }

            //        var LastMerge = nxtEmpWiseLeaveRowCount + EmpWiseLeaveRowCount;
            //        worksheet.Range(worksheet.Cell("A" + LastMerge).Address, worksheet.Cell("A" + ((LastMerge - 1) + EmpWiseLeaveRowCount)).Address).Merge();
            //        worksheet.Range(worksheet.Cell("A" + LastMerge).Address, worksheet.Cell("A" + ((LastMerge - 1) + EmpWiseLeaveRowCount)).Address).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            //        worksheet.Range(worksheet.Cell("A" + LastMerge).Address, worksheet.Cell("A" + ((LastMerge - 1) + EmpWiseLeaveRowCount)).Address).Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
            //        worksheet.Range(worksheet.Cell("B" + LastMerge).Address, worksheet.Cell("B" + ((LastMerge - 1) + EmpWiseLeaveRowCount)).Address).Merge();
            //        worksheet.Range(worksheet.Cell("B" + LastMerge).Address, worksheet.Cell("B" + ((LastMerge - 1) + EmpWiseLeaveRowCount)).Address).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            //        worksheet.Range(worksheet.Cell("B" + LastMerge).Address, worksheet.Cell("B" + ((LastMerge - 1) + EmpWiseLeaveRowCount)).Address).Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
            //        worksheet.Range(worksheet.Cell("C" + LastMerge).Address, worksheet.Cell("C" + ((LastMerge - 1) + EmpWiseLeaveRowCount)).Address).Merge();
            //        worksheet.Range(worksheet.Cell("C" + LastMerge).Address, worksheet.Cell("C" + ((LastMerge - 1) + EmpWiseLeaveRowCount)).Address).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            //        worksheet.Range(worksheet.Cell("C" + LastMerge).Address, worksheet.Cell("C" + ((LastMerge - 1) + EmpWiseLeaveRowCount)).Address).Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
            //        #endregion
            //        worksheet.Cell(3, 6).Value = slNo;
            //        worksheet.Columns().AdjustToContents();
            //        worksheet.Rows().AdjustToContents();
            //        using (var stream = new MemoryStream())
            //        {

            //            workbook.SaveAs(stream);
            //            if (EP.NeedPassword)
            //            {
            //                Workbook book = new Workbook();

            //                book.LoadFromStream(stream);
            //                //Protect Workbook
            //                Worksheet sheet = book.Worksheets[0];                            
            //                sheet.SetColumnWidth(2, 80);
            //                book.Protect(EP.Password,false,false);
            //                book.SaveToStream(stream);
            //            }
            //            var content = stream.ToArray();
            //            return File(
            //                content,
            //                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            //                "Exit Questionnaire Analysis List.xlsx"
            //                );
            //        }
            //    }
            //}
            //catch (UnauthorizedAccessException e)
            //{

            //    return RedirectToAction("Login", "Account", new { Area = "" });
            //}
            //catch (Exception e)
            //{
            //    return RedirectToAction("Error", "Account", new { Area = "" });
            //}

            try
            {
                ExitQuestionnaireAnalysis model = new ExitQuestionnaireAnalysis();

                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = await _commonService.GetExcelPassword("Exit Questionnaire Analysis", "Excel");

                model = await _service.ExitQuestionnaireAnalysisReport();
                string currDateTime = DateTime.Now.ToString("dd-MMM-yyyy h:mm tt");

                using (var workbook = new XLWorkbook())
                {




                   

                    var worksheet = workbook.Worksheets.Add("Exit Questionnaire Analysis");


                    int count = model.ExitQuestionnaireAnalysisList.Count + 5;


                    worksheet.Range("A1:F" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:F" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:F" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:F" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:F" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;

                 
                    worksheet.Range("A1:F" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                    // General formatting and header setup
                    worksheet.Range("A1:F5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:F1").Merge().Style.Font.SetBold();
                    worksheet.Range("A1:F1").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                   
                    worksheet.Range("A1:F1").Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                    worksheet.Cell("A1").Value = "Exit Questionnaire Analysis";

                    worksheet.Cell("A2").Value = "Group Company Name";
                    worksheet.Cell("B2").Value = GroupCompanyName.ToUpper();
                    worksheet.Cell("A3").Value = "Login User";
                    worksheet.Cell("B3").Value = LoginUser;
                    worksheet.Cell("C3").Value = "Date & Time";
                    worksheet.Cell("D3").Value = currDateTime;
                    worksheet.Cell("E3").Value = "No. of questions";
                    worksheet.Cell("F3").Value = model.ExitQuestionnaireAnalysisList.Select(x => x.QuestionCode).Distinct().Count();

                    // Header row
                    int currentRow = 5;
                    worksheet.Cell(currentRow, 1).Value = "Sl.No";
                    worksheet.Cell(currentRow, 2).Value = "Questions";
                    worksheet.Cell(currentRow, 3).Value = "Total Attempts";
                    worksheet.Cell(currentRow, 4).Value = "Answers";
                    worksheet.Cell(currentRow, 5).Value = "Count";
                    worksheet.Cell(currentRow, 6).Value = "Percentage";
                    worksheet.Range("A5:F5").Style.Font.Bold = true;
                    worksheet.Range("A5:F5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);

                    // Populate data with merged rows for each question
                    var groups = model.ExitQuestionnaireAnalysisList.GroupBy(x => x.QuestionCode);
                    int slNo = 1;

                    foreach (var group in groups)
                    {
                        int rowspan = group.Count();
                        var question = group.FirstOrDefault();

                        // Merge cells for Sl.No, Questions, and Total Attempts
                        worksheet.Range(currentRow + 1, 1, currentRow + rowspan, 1).Merge().SetValue(slNo).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                        worksheet.Range(currentRow + 1, 2, currentRow + rowspan, 2).Merge().SetValue(question.Question).Style.Alignment.WrapText = true;
                        worksheet.Range(currentRow + 1, 3, currentRow + rowspan, 3).Merge().SetValue(question.TotalAttempts).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

                        // Populate answers, count, and percentage
                        foreach (var item in group)
                        {
                            worksheet.Cell(currentRow + 1, 4).SetValue(item.Answer);
                            worksheet.Cell(currentRow + 1, 5).SetValue(item.Count);
                            worksheet.Cell(currentRow + 1, 6).SetValue(item.Percentage);
                            currentRow++;
                        }

                        slNo++;
                    }
                    // Apply borders to the entire data range
                    //worksheet.Range("A5:F1" + count + 5).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    //worksheet.Range("A5:F1" + count + 5).Style.Border.InsideBorder = XLBorderStyleValues.Thin;

                    worksheet.Columns().AdjustToContents();

                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);

                        // Apply password if required
                        if (EP.NeedPassword)
                        {
                            Workbook spireBook = new Workbook();
                            spireBook.LoadFromStream(stream);
                            Worksheet sheet = spireBook.Worksheets[0];
                            spireBook.Protect(EP.Password, false, false);
                            spireBook.SaveToStream(stream);
                        }

                        var content = stream.ToArray();
                        return File(
                            content,
                            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                            "Exit Questionnaire Analysis List.xlsx"
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
        public async Task<IActionResult> ExportToExcelPopUp(int QuestionCode,int AnswerCode,string Question,string Answer,int totRecords,Filter filter)
        {
            try
            {              

                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Exit Questionnaire Analysis", "Excel");
                filter.length = totRecords;
                filter.start = 0;
                string FilterValues = await _helpers.GetFilterValues(filter);                
                if (FilterValues == "")
                {
                    FilterValues = "All";
                }         
                 string currDateTime = await _helpers.GetCurrentDate();
                

                //Get Dataset
                var dataset = await _service.GetQuestionnaireEmployee_Excel(QuestionCode, AnswerCode, filter);
                DataTable dt = dataset.Tables[0];

                dataset.Tables[0].Rows.RemoveAt(0);
                if ((dataset.Tables[0]?.Rows?.Count ?? 0) > 0)
                {
                    dt = dataset.Tables[0];
                }

                //set EXCEL Column Heading 
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (dt.Columns[i].ColumnName == "lwd")
                        dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.ToUpper();
                    else
                        dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
                }

                using (var workbook = new XLWorkbook())
                {

                    var worksheet = workbook.Worksheets.Add("Exit Questionnaire Analysis");

                    int count =dt.Rows.Count + 7;

                    worksheet.Range("A1:H" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:H" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:H" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:H" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:H" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;



                    var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 7)));
                    wsReportNameHeaderRange.Style.Font.Bold = true;
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Merge();
                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                    wsReportNameHeaderRange.Value = "Exit Questionnaire Analysis";

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
                    worksheet.Cell(3, 6).Value =dt.Rows.Count;
                    worksheet.Cell(4, 1).Style.Font.Bold = true;
                    worksheet.Cell(4, 1).Value = "Filter Applied";
                    worksheet.Range("B4:H4").Merge();
                    worksheet.Cell("B4").Value = FilterValues;
                    worksheet.Cell(5, 1).Value = "Question";
                    worksheet.Cell(5, 1).Style.Font.Bold = true;
                    worksheet.Range("B5:H5").Merge();
                    worksheet.Cell("B5").Value = Question;
                    worksheet.Cell(6, 1).Value = "Answer";
                    worksheet.Cell(6, 1).Style.Font.Bold = true;
                    worksheet.Cell(6, 2).Value = Answer;

                    var currentRow = 7;
                    worksheet.Cell(currentRow, 1).InsertTable(dt);
                    worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                    worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                    worksheet.Range("A7:H7").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Range("A7:H7").Style.Font.Bold = true;
                    worksheet.Range("G7:G" + (dt.Rows.Count + 7)).Style.DateFormat.Format = "dd-MMM-yyyy";


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
                            "Exit Questionnaire Analysis List.xlsx"
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
