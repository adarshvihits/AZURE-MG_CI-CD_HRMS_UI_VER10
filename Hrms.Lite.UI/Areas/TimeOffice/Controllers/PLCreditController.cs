using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.TimeOffice;
using Hrms.Lite.Services.Services.TimeOffice;
using Hrms.Lite.Shared.Account;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TimeOffice;
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

namespace Hrms.Lite.UI.Areas.TimeOffice.Controllers
{
    [Area("TimeOffice")]
    [SessionAuthorize]
    public class PLCreditController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly IPLCreditService _PLCreditService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;




        public PLCreditController(IDropdownService dropdownService, IPLCreditService PLCreditService, ICommonService commonService, IHelpers helpers)
        {
            _PLCreditService = PLCreditService;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = helpers;

        }

        public async Task<IActionResult> Index()
        {

            try
            {
                PLCredit model = new PLCredit();
               
                PLCredit m1 = new PLCredit();
                Filter filter = new Filter();
                filter.length = 10;
                m1 = await _PLCreditService.GetPLCreditList(0, 0, filter);
                model.Filter = new Filter();
                model.Filter = await _commonService.GetFilterDropdowns();
                ViewBag.LeavePeriod = new SelectList(await _dropdownService.GetLeavePeriodFilterDropdown(), "Code", "Name");
                ViewBag.Leave = new SelectList(await _dropdownService.GetPLCreditLeaveDropdown(), "Code", "Name");                    
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


      

        [HttpPost]
        public async Task<ActionResult> PLCreditBatchUpdate(Filter filter, int LeavePeriodCode, int LeaveCode)
        {
            try
            {
                PLCredit model = new PLCredit();
                model.LeaveCode = LeaveCode;
                model.LeavePeriodCode = LeavePeriodCode;
                model.Filter = filter;
                var response = await _PLCreditService.PLCreditBatchUpdate(model);
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


        [HttpPost]
        public async Task<ActionResult> PLCreditSave(int LeavePeriodCode, int LeaveCode, decimal Opening, Guid EmployeeGI)
        {
            try
            {
                PLCredit model = new PLCredit();
                model.EmployeeGI = EmployeeGI;
                model.LeaveCode = LeaveCode;
                model.Opening = Opening;
                model.LeavePeriodCode = LeavePeriodCode;               
                var response = await _PLCreditService.PLCreditSave(model);
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



        [HttpPost]
        public async Task<IActionResult> GetPLCreditList(int LeavePeriodCode,int LeaveCode, Filter filter1)
        {
            try
            {
                PLCredit model = new PLCredit();
                PLCredit m = new PLCredit();



                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }
              
                model = await _PLCreditService.GetPLCreditList(LeavePeriodCode,LeaveCode,filter1);
           

                m.PLCreditList = model.PLCreditList;
                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.PLCreditList == null || model.PLCreditList.Count() == 0)
                {

                    return Json(new
                    {
                        data = "",
                        draw = filter1.draw,
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows
                    });
                }


                if (model.PLCreditList.Count > 0)
                {
                    totalrows = m.PLCreditList[0].active;
                }
                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = m.PLCreditList[0].totalRecords;
                    return Json(new
                    {
                        data = model.PLCreditList,
                        draw = filter1.draw,
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows
                    });
                }
                else
                {
                    return Json(new
                    {
                        data = model.PLCreditList,
                        draw = filter1.draw,
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


        public async Task<IActionResult> ExportToExcel(Filter filter, int LeavePeriodCode,int LeaveCode,string LeavePeriodName,string LeaveName,int TotalRecords)
        {
            try
            {


                filter.length = TotalRecords;
                filter.start = 0;
                string FilterValues = await _helpers.GetFilterValues(filter);

                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("PL Credit", "Excel");

                if (FilterValues == "")
                {
                    FilterValues = "All";
                }

                string currDateTime = await _helpers.GetCurrentDate();
                

                //Get Datset
                var dataset = await _PLCreditService.GetPLCredit_Excel(LeavePeriodCode, LeaveCode, filter);
                DataTable dt = dataset.Tables[0];


                //set EXCEL Column Heading 
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (dt.Columns[i].ColumnName == "lop/ab" || dt.Columns[i].ColumnName == "doj")
                        dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.ToUpper();
                    else
                        dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
                }

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("PL Cedit");
                    int count = dt.Rows.Count + 5;


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
                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191); ;
                    wsReportNameHeaderRange.Value = "PL Credit";
                    worksheet.Cell(2, 1).Style.Font.Bold = true;
                    worksheet.Cell(2, 1).Value = "Group Company Name";
                    worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();

                    worksheet.Cell(2, 3).Style.Font.Bold = true;
                    worksheet.Cell(2, 3).Value = "Leave Period";
                    worksheet.Cell(2, 4).Value = LeavePeriodName;


                    worksheet.Cell(2, 5).Style.Font.Bold = true;
                    worksheet.Cell(2, 5).Value = "Leave";
                    worksheet.Cell(2, 6).Value = LeaveName;


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
                    worksheet.Range("B4:J4").Merge();
                    worksheet.Cell("B4").Value = FilterValues;


                    var currentRow = 5;

                    worksheet.Cell(currentRow, 1).InsertTable(dt);
                    worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                    worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                    worksheet.Range("A5:J5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Range("A5:J5").Style.Font.Bold = true;


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
                            "PL Credit List.xlsx"
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
