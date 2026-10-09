using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.PayRoll;
using Hrms.Lite.Shared.Account;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll;
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

namespace Hrms.Lite.UI.Areas.PayRoll.Reports.Controllers
{
    [Area("PayRoll")]
    [SessionAuthorize]
    public class LeaveEncashmentProcessingReportController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly ILeaveEncashmentProcessingService _LeaveEncashmentProcessingService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;

       

        public LeaveEncashmentProcessingReportController(ILeaveEncashmentProcessingService LeaveEncashmentProcessingService, IDropdownService dropdownService, ICommonService commonService, IHelpers helpers)
        {
            _LeaveEncashmentProcessingService = LeaveEncashmentProcessingService;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = helpers;
        }
      
        public async Task<IActionResult> Index()
        {
            try
            {
                LeaveEncashmentProcessing model = new LeaveEncashmentProcessing();
                Filter filter = new Filter();

                ViewBag.Month = new SelectList(await _dropdownService.GetPayrollReportMonthDropdown(), "Code", "Name");
                ViewBag.Allowance = new SelectList(await _dropdownService.GetLeaveEncashmentSettingsAllowanceDropdown(), "Code", "Name");
                model.filter = filter;
                filter.length = 10;
                model = await _LeaveEncashmentProcessingService.GetListReport(0, 0, filter);
                model.filter = await _commonService.GetFilterDropdowns();

                return View("~/Areas/PayRoll/Reports/Views/LeaveEncashmentProcessingReport/Index.cshtml", model);
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
        public async Task<IActionResult> GetReportList(Filter filter1,int Month,int Allowance)
        {
            try
            {
                LeaveEncashmentProcessing model = new LeaveEncashmentProcessing();
                LeaveEncashmentProcessing m = new LeaveEncashmentProcessing();

                Filter filter = new Filter();
                model.filter = filter1;
                if (Month != 0 & Allowance != 0)
                {
                    model = await _LeaveEncashmentProcessingService.GetListReport(Month, Allowance, filter1);
                }
                 
                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }


                {
                    m.EncashmetProcessingReport = model.EncashmetProcessingReport;
                }
                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.EncashmetProcessingReport == null || model.EncashmetProcessingReport.Count() == 0)
                {

                    return Json(new
                    {
                        data = "",
                        draw = filter1.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows
                    });
                }




                if (model.EncashmetProcessingReport.Count > 0)
                {
                    m.EncashmetProcessingReport = model.EncashmetProcessingReport;
                    totalrows = m.EncashmetProcessingReport[0].Active;

                }


                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = m.EncashmetProcessingReport[0].totalRecords;
                    return Json(new
                    {
                        data = model.EncashmetProcessingReport,
                        draw = filter1.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows//totalrowsafterfiltering
                    });
                }
                else
                {
                    return Json(new
                    {
                        data = model.EncashmetProcessingReport,
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




        //public async Task<IActionResult> GetReportList(Filter filter, int Month,int Allowance)
        //{
        //    LeaveEncashmentProcessing model = new LeaveEncashmentProcessing();
        //    if (Month != 0 & Allowance != 0)
        //    {
        //        model = await _LeaveEncashmentProcessingService.GetListReport(Month,Allowance, filter);
        //    }

        //    model.Month = Month;
        //    return PartialView("~/Areas/PayRoll/Reports/Views/LeaveEncashmentProcessingReport/_ProcessedList.cshtml", model);
        //}




        public async Task<IActionResult> ExportToExcel(Filter filter, int Month, int Allowance, string MonthName, string AllowanceName,int TotalRecord)

        {
            try
            {
              
                string FilterValues = await _helpers.GetFilterValues(filter);

                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("LeaveEncashment ProcessingReport", "Excel");
                LeaveEncashmentProcessing model = new LeaveEncashmentProcessing();
                if (FilterValues == "")
                {
                    FilterValues = "All";
                }

                string currDateTime = await _helpers.GetCurrentDate();
                

                //Get Datset
                filter.length = TotalRecord;
                filter.start = 0;
                var dataset = await _LeaveEncashmentProcessingService.Encashment_excel_Report(Month, Allowance, filter);
                DataTable dt = dataset.Tables[0];


                //set EXCEL Column Heading 
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (dt.Columns[i].ColumnName == "doj" || dt.Columns[i].ColumnName == "dob" || dt.Columns[i].ColumnName == "y/s")
                        dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.ToUpper();
                    else
                        dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
                }

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Leave Encashment Processing");
                    int count = dt.Rows.Count + 5;


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
                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191); ;
                    //wsReportNameHeaderRange.Value = "Variable Provisions & Perk(" + AllowanceType + ")";
                    wsReportNameHeaderRange.Value = "Leave Encashment Processing";

                    worksheet.Cell(2, 1).Style.Font.Bold = true;
                    worksheet.Cell(2, 1).Value = "Group Company Name";
                    worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                    worksheet.Cell(2, 3).Style.Font.Bold = true;
                    worksheet.Cell(2, 3).Value = "Month";
                    worksheet.Cell(2, 4).Value = MonthName;
                    worksheet.Cell(2, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(2, 4).Style.DateFormat.Format = "dd-MMM-yyyy";
                    worksheet.Cell(2, 5).Style.Font.Bold = true;
                    worksheet.Cell(2, 5).Value = "Allowance";
                    worksheet.Cell(2, 6).Value = AllowanceName;
                    worksheet.Cell(2, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
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
                    worksheet.Range("B4:H4").Merge();
                    worksheet.Cell("B4").Value = FilterValues;



                    var currentRow = 5;

                    worksheet.Cell(currentRow, 1).InsertTable(dt);
                    worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                    worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                    worksheet.Range("A5:H5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Range("A5:H5").Style.Font.Bold = true;


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
                            book.Protect(EP.Password, false, false);
                            book.SaveToStream(stream);
                        }
                        var content = stream.ToArray();
                        return File(
                            content,
                            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                            "Leave Encashment Processing Report List.xlsx"
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
