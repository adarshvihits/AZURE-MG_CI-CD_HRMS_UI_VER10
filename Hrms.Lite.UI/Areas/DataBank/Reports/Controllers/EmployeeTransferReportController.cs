using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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

namespace Hrms.Lite.UI.Areas.DataBank.Reports.Controllers
{
    [Area("DataBank")]
    [SessionAuthorize]
    public class EmployeeTransferReportController : Controller
    {
        private readonly IEmployeeTransferReportService _EmployeeTransferReportService;
        private readonly IHelpers _helpers;
        private readonly ICommonService _commonService;
        private readonly IDropdownService _dropdownService;

        public EmployeeTransferReportController(IEmployeeTransferReportService EmployeeTransferReportService, IHelpers helpers, IDropdownService dropdownService,ICommonService commonService)
        {
            _EmployeeTransferReportService = EmployeeTransferReportService;
            _helpers = helpers;
            _dropdownService = dropdownService;
            _commonService = commonService;
        }
        public class Global
        {
            public static Filter GlobalFilter;
            public static DateTime fromDate;
            public static DateTime toDate;
            public static int transferType;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                EmployeeTransferReport model = new EmployeeTransferReport();
                EmployeeTransferReport m = new EmployeeTransferReport();
                m.Filter = new Filter();
                m.Filter.length = 10;
                m = await _EmployeeTransferReportService.GetEmployeeTransferReport(m);
                Filter filter = new Filter();
                await fillfilterDropdown();
                ViewBag.Type = new SelectList(await _dropdownService.EmployeeTransferTypeDropDown(), "Code", "Name");
                //model = await _EmployeeTransferReportService.GetEmployeeTransferReport(filter);
                model.TransferFromDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                model.TransferToDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1);
                return View("~/Areas/DataBank/Reports/Views/EmployeeTransferReport/Index.cshtml", model);
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
        public async Task<IActionResult> GetList(Filter filter1,DateTime fromDate,DateTime toDate,int transferType)
        {
            try
            {
                EmployeeTransferReport model = new EmployeeTransferReport();
                EmployeeTransferReport m = new EmployeeTransferReport();
                Filter filter = new Filter();
                Global.GlobalFilter = filter1;
                Global.transferType= transferType;
                Global.fromDate= fromDate;
                Global.toDate= toDate;
                model.Filter = filter1;
                model.TransferFromDate = fromDate;
                model.TransferToDate = toDate;
                model.EmployeeTransferType = transferType;


                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }

                model = await _EmployeeTransferReportService.GetEmployeeTransferReport(model);
                m.EmployeePrimaryInfo = model.EmployeePrimaryInfo;
                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.EmployeePrimaryInfo == null || model.EmployeePrimaryInfo.Count() == 0)
                {


                    return Json(new
                    {
                        data = "",
                        draw = filter1.draw,
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows
                    });
                }




                if (model.EmployeePrimaryInfo.Count > 0)
                {
                    totalrows = m.EmployeePrimaryInfo[0].active;

                }


                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = m.EmployeePrimaryInfo[0].totalRecords;
                    return Json(new
                    {
                        data = model.EmployeePrimaryInfo,
                        draw = filter1.draw,
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows
                    });
                }
                else
                {
                    return Json(new
                    {
                        data = model.EmployeePrimaryInfo,
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


        public async Task<IActionResult> GetTransferList(DateTime fromDate, DateTime toDate, int transferType)
        {
            try
            {
                EmployeeTransferReport model = new EmployeeTransferReport();
               if (transferType==1)
                {
                    return PartialView("~/Areas/DataBank/Reports/Views/EmployeeTransferReport/_JoiningList.cshtml", model);
                }
                else
                {
                    return PartialView("~/Areas/DataBank/Reports/Views/EmployeeTransferReport/_List.cshtml", model);
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






        public async Task fillfilterDropdown()
        {

            ViewBag.Gen = new SelectList(await _dropdownService.GetGenderDropDown(), "Code", "Name");
            ViewBag.Cat = new SelectList(await _dropdownService.GetCategoryDropdown(), "Code", "Name");
            ViewBag.Grd = new SelectList(await _dropdownService.GetGradeDropdown(), "Code", "Name");
            ViewBag.Dsg = new SelectList(await _dropdownService.GetDesignationDropdown(), "Code", "Name");
            ViewBag.Dept = new SelectList(await _dropdownService.GetDepartmentDropdown(), "Code", "Name");
            ViewBag.Loc = new SelectList(await _dropdownService.GetLocationDropdown(), "Code", "Name");
            ViewBag.PayrollGrp = new SelectList(await _dropdownService.GetPayrollGroupDropdown(), "Code", "Name");

            ViewBag.division = new SelectList(await _dropdownService.GetDivisionDropdown(), "Code", "Name");
            ViewBag.section = new SelectList(await _dropdownService.GetSectionDropdown(), "Code", "Name");
            ViewBag.EmploymentType = new SelectList(await _dropdownService.GetEmploymentTypeDropdown(), "Code", "Name");
            ViewBag.EmploymentStatus = new SelectList(await _dropdownService.GetEmploymentStatusDropdown(), "Code", "Name");
            ViewBag.WorkLocation = new SelectList(await _dropdownService.GetWorkLocationDropdown(), "Code", "Name");
            ViewBag.WageGrade = new SelectList(await _dropdownService.GetWageGradeDropdown(), "Code", "Name");

        }        



        public async Task<IActionResult> ExportToExcel(int TotRecords)
        {
            try
            {
                EmployeeTransferReport model = new EmployeeTransferReport();

                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                Filter filter = new Filter();
                EP = await _commonService.GetExcelPassword("Employee Transfer Report", "Excel");

                if (Global.GlobalFilter == null)
                    Global.GlobalFilter = new Filter();
                else
                {
                    Global.GlobalFilter.length = TotRecords;
                    Global.GlobalFilter.start = 0;
                }
              
                string currDateTime = await _helpers.GetCurrentDate();
                
                model.Filter = Global.GlobalFilter;
                model.TransferFromDate = Global.fromDate;
                model.TransferToDate = Global.toDate;
                model.EmployeeTransferType=Global.transferType;

                var dataset = await _EmployeeTransferReportService.GetEmployeeTransferReport_Excel(model);
                DataTable dt = dataset.Tables[0];
                dataset.Tables[0].Rows.RemoveAt(0);
                if ((dataset.Tables[0]?.Rows?.Count ?? 0) > 0)
                {
                    dt = dataset.Tables[0];
                }

                //set EXCEL Column Heading 
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (dt.Columns[i].ColumnName == "doj" )
                        dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.ToUpper();
                    else
                        dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
                }


                using (var workbook = new XLWorkbook())
                {

                    var worksheet = workbook.Worksheets.Add("Employee Transfer Report");
                    int count =  dt.Rows.Count+ 4; //no of data + no of heading rows


                    worksheet.Range("A1:M" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin; //for table border
                    worksheet.Range("A1:M" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:M" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:M" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:M" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;






                    var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 12))); //heading
                    wsReportNameHeaderRange.Style.Font.Bold = true;
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Merge();
                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                    wsReportNameHeaderRange.Value = "Employee Transfer Report";

                    worksheet.Cell(2, 1).Style.Font.Bold = true;
                    worksheet.Cell(2, 1).Value = "Group Company Name";
                    worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();


                    if(Global.transferType==2)
                    {
                        worksheet.Cell(2, 3).Style.Font.Bold = true;
                        worksheet.Cell(2, 3).Value = "From Date";
                        worksheet.Cell(2, 4).Value = Global.fromDate;
                        worksheet.Cell(2, 4).Style.DateFormat.Format = "dd-MMM-yyyy";


                        worksheet.Cell(2, 5).Style.Font.Bold = true;
                        worksheet.Cell(2, 5).Value = "To Date";
                        worksheet.Cell(2, 6).Value = Global.toDate;
                        worksheet.Cell(2, 6).Style.DateFormat.Format = "dd-MMM-yyyy";
                    }


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



                    var currentRow = 4;

                    worksheet.Cell(currentRow, 1).InsertTable(dt);
                    worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                    worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                    worksheet.Range("A4:M4").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Range("A4:M4").Style.Font.Bold = true;
                    worksheet.Range("J4:J" + (dt.Rows.Count + 4)).Style.DateFormat.Format = "dd-MMM-yyyy";
                    worksheet.Range("L4:L" + (dt.Rows.Count + 4)).Style.DateFormat.Format = "dd-MMM-yyyy";


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
                            "Employee Transfer Report List.xlsx"
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
