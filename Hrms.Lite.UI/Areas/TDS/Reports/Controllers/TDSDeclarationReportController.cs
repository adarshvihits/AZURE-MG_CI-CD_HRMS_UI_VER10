using ClosedXML.Excel;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.TDS.Reports;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TDS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Spire.Xls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Shared.Account;
using System.Data;

namespace Hrms.Lite.UI.Areas.TDS.Reports.Controllers
{
    [Area("TDS")]
    [SessionAuthorize]
    public class TDSDeclarationReportController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly ITDSDeclarationReportService _tdsReportService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        public static class Global
        {
            public static Filter GlobalFilter;
            public static bool IsRefreshed = true;
        }
        public TDSDeclarationReportController(ITDSDeclarationReportService _TDSReportService, ICommonService commonService, IDropdownService dropdownService, IHelpers Helpers)
        {
            _tdsReportService = _TDSReportService;
            _commonService = commonService;
            _dropdownService = dropdownService;
            _helpers = Helpers;
        }
        public async Task<IActionResult> Index()
        {
            TDSDeclarationReport model = new TDSDeclarationReport();
            var AssCode = await _commonService.GetCurrentAssessmentYear();
            int AssessmentYear = AssCode.Data;
            ViewBag.AssessmentYear = new SelectList(await _dropdownService.GetAssessmentYearDropdown(), "Code", "Name");
            model = await _tdsReportService.GetTDSDeclarationReport(AssessmentYear);
         
            return View("~/Areas/TDS/Reports/Views/TDSDeclarationReport/Index.cshtml", model);

        }
  
   
        public async Task<IActionResult> GetList(int AssessmentYear)
        {
            return PartialView("~/Areas/TDS/Reports/Views/TDSDeclarationReport/_List.cshtml", await _tdsReportService.GetTDSDeclarationReport(AssessmentYear));

        }
       
      
        public async Task<IActionResult> GetTdsEmployeeDetails(int CategoryCode,int AssessmentYear)
        {
            DataSet ds = await _tdsReportService.GetTDSDetailedReport(CategoryCode, AssessmentYear);
            return PartialView("~/Areas/TDS/Reports/Views/TDSDeclarationReport/_EmployeePopup.cshtml", ds);
        }
        public async Task<IActionResult> TDSReportExportToExcel( int AssessmentYear,string AssessmentYearName)
        {
            try
            {
                Login CurrentUser = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = CurrentUser.authUser.GroupCompanyName;
                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = CurrentUser.CompanyName;
         
                if (Global.GlobalFilter == null)
                    Global.GlobalFilter = new Filter();
               
                string FilterValues = await _helpers.GetFilterValues(Global.GlobalFilter);


                var dataset = await _tdsReportService.GetTDSReport_Export(Global.GlobalFilter, AssessmentYear);
               
                DataTable dt = dataset.Tables[0];
                dt.Columns.RemoveAt(0);
                dt.Columns.RemoveAt(2);
                for (int i = 0; i < dt.Columns.Count; i++)
                {

                    dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
                }


                if (FilterValues == "")
                {
                    FilterValues = "All";
                }
                string currDateTime = await _helpers.GetCurrentDate();
                

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("TDS Declaration Detailed Report");
                    int count = dt.Rows.Count + 5;


                    worksheet.Range("A1:BR" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:BR" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:BR" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:BR" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:BR" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 7)));
                    wsReportNameHeaderRange.Style.Font.Bold = true;
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Merge();
                    wsReportNameHeaderRange.Value = "TDS Declaration Detailed Report";

                    worksheet.Cell(2, 1).Style.Font.Bold = true;
                    worksheet.Cell(2, 1).Value = "Group Company Name";
                    worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();

                    worksheet.Cell(2, 3).Style.Font.Bold = true;
                    worksheet.Cell(2, 3).Value = "Assessment Year";

                    worksheet.Cell(2, 4).Value = AssessmentYearName;
                   
                    worksheet.Cell(2, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);


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

                    var filterapplied = worksheet.Range("B4:U4").Merge();

                    filterapplied.Value = FilterValues;

                    var currentRow = 5;
                    worksheet.Cell(currentRow, 1).InsertTable(dt);
                    worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;
                    worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                    worksheet.Range("A5:BR5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Range("A5:BR5").Style.Font.Bold = true;



                    worksheet.Columns().AdjustToContents();
                    worksheet.Rows().AdjustToContents();
                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);

                        var content = stream.ToArray();
                        return File(
                            content,
                            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                            "TDS Declaration Detailed Report.xlsx"
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
