using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.PayRoll.Reports;
using Hrms.Lite.Shared.Account;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll.Reports;
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
    public class PaySheetDraftController : Controller
    {
        private readonly IPaySheetService _PaySheetdraftService;
        private readonly IDropdownService _dropdownService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        public static class Global
        {


            public static int Tab;
            public static DateTime? FromDate;
            public static DateTime? ToDate;
        }
        public PaySheetDraftController(IPaySheetService PaySheetdraftService, IDropdownService dropdownService, ICommonService commonService, IHelpers helpers)
        {
            _PaySheetdraftService = PaySheetdraftService;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = helpers;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                await fillfilterDropdown();
                PaySheet model = new PaySheet();
                model.BasedOn = 2;
                var ds = await _PaySheetdraftService.GetPaySheetDraftDetails(0, model);


                return View("~/Areas/PayRoll/Reports/Views/PaySheetDraft/Index.cshtml",model);
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
        public async Task fillfilterDropdown()
        {
            ViewBag.Dsg = new SelectList(await _dropdownService.GetDesignationDropdown(), "Code", "Name");
            ViewBag.Dept = new SelectList(await _dropdownService.GetDepartmentDropdown(), "Code", "Name");
            ViewBag.Loc = new SelectList(await _dropdownService.GetLocationDropdown(), "Code", "Name");
            ViewBag.Cat = new SelectList(await _dropdownService.GetCategoryDropdown(), "Code", "Name");
            ViewBag.Grd = new SelectList(await _dropdownService.GetGradeDropdown(), "Code", "Name");
            ViewBag.PayrollGrp = new SelectList(await _dropdownService.GetPayrollGroupDropdown(), "Code", "Name");
            ViewBag.division = new SelectList(await _dropdownService.GetDivisionDropdown(), "Code", "Name");
            ViewBag.section = new SelectList(await _dropdownService.GetSectionDropdown(), "Code", "Name");
            ViewBag.EmploymentType = new SelectList(await _dropdownService.GetEmploymentTypeDropdown(), "Code", "Name");
            ViewBag.WorkLocation = new SelectList(await _dropdownService.GetWorkLocationDropdown(), "Code", "Name");
            ViewBag.WageGrade = new SelectList(await _dropdownService.GetWageGradeDropdown(), "Code", "Name");
            ViewBag.PayModes = new SelectList(await _dropdownService.GetPaymentModeDropDown(), "Code", "Name");
            ViewBag.AttendanceStatus = new SelectList(await _dropdownService.GetAttendanceStatusDropdown(), "Code", "Name");
            ViewBag.Type = new SelectList(await _dropdownService.GetPaySheetTypeDropDown(), "Code", "Name");
            ViewBag.month = new SelectList(await _dropdownService.GetMonthDropdown(), "Code", "Name");
            ViewBag.Reg = new SelectList(await _dropdownService.GetRegionDropDown(), "Code", "Name");
            ViewBag.St = new SelectList(await _dropdownService.GetStateDropDown(), "Code", "Name");
        }
        [HttpPost]
   
        public async Task<IActionResult> CheckDataCount(PaySheet model, string PayrollMonth)
        {
            int count = await _PaySheetdraftService.GetPaysheetDataCount((int)model.PayrollMonth.Code, model, "Draft");
            return Json(count);
        }

        public async Task<IActionResult> ExportToexcel(PaySheet model)
        {
            try
            {         
                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("PaySheet Draft Report", "Excel");
        
             
                string currDateTime = await _helpers.GetCurrentDate();
                               
                var ds = await _PaySheetdraftService.GetPaySheetDraftDetails((int)model.PayrollMonth.Code, model);
                ds.Tables[0].Rows.RemoveAt(0);
                DataTable dt = ds.Tables[0];
     

                //set EXCEL Column Heading 
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.ToUpper();
                }



                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Paysheet Draft Report");         

                    int rowcount = dt.Rows.Count + 4;

                    string columnName = "";
                    int Columncount = dt.Columns.Count;

                    while (Columncount > 0)
                    {
                        int modulo = (Columncount - 1) % 26;
                        columnName = Convert.ToChar('A' + modulo) + columnName;
                        Columncount = (Columncount - modulo) / 26;
                    }

                    string colName = columnName;

                    worksheet.Range("A1", colName + rowcount).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1", colName + rowcount).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1", colName + rowcount).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1", colName + rowcount).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1", colName + rowcount).Style.Border.LeftBorder = XLBorderStyleValues.Thin;




                    var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, colName));
                    wsReportNameHeaderRange.Style.Font.Bold = true;
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                    wsReportNameHeaderRange.Merge();
                  
                    wsReportNameHeaderRange.Value = "PaySheet Draft Report";

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
                    if (dt.Rows.Count > 1)
                    {
                        worksheet.Cell(3, 6).Value = dt.Rows.Count - 1;
                    }
                    else
                    {
                        worksheet.Cell(3, 6).Value = 0;
                    }

                    worksheet.Cell(3, 7).Style.Font.Bold = true;
                    worksheet.Cell(3, 7).Value = "From Date";
                    worksheet.Cell(3, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(3, 8).Value = Global.FromDate;

                    worksheet.Cell(3, 9).Style.Font.Bold = true;
                    worksheet.Cell(3, 9).Value = "To Date";
                    worksheet.Cell(3, 10).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(3, 10).Value = Global.ToDate;



                    var currentrow = 4;


                    worksheet.Cell(currentrow, 1).InsertTable(dt);
                    worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                    worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                    worksheet.Range("A4:" + colName + "4").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Range("A4:" + colName + "4").Style.Font.Bold = true;

                    worksheet.Range("I4:I" + (dt.Rows.Count + 4)).Style.DateFormat.Format = "dd-MMM-yyyy";
                    worksheet.Range("J4:J" + (dt.Rows.Count + 4)).Style.DateFormat.Format = "dd-MMM-yyyy";
                    worksheet.Range("K4:K" + (dt.Rows.Count + 4)).Style.DateFormat.Format = "dd-MMM-yyyy";
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
                            "PaySheet Draft Report("+ model.PayrollMonth.Name + ").xlsx"
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
