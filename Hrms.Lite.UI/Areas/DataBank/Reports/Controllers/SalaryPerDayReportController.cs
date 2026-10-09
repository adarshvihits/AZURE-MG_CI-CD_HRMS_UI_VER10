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
    public class SalaryPerDayReportController : Controller
    {

        private readonly ISalaryPerDayReportService _SalaryPerDayReportService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        private readonly IDropdownService _dropdownService;
        public SalaryPerDayReportController(ISalaryPerDayReportService SalaryPerDayReportService, ICommonService commonService, IHelpers Helpers, IDropdownService dropdownService)
        {
            _SalaryPerDayReportService = SalaryPerDayReportService;
            _commonService = commonService;
            _helpers = Helpers;
            _dropdownService = dropdownService;
        }

        public async Task<IActionResult> Index()
        {
            try { 
            SalaryPerDayReport model = new SalaryPerDayReport();
            DateTime now = DateTime.Now;
                //await fillfilterDropdown();
                Filter filter = new Filter();
                model.filter = filter;
                model.filter.length = 10;
                model = await _SalaryPerDayReportService.GetList(model);
                model.filter = await _commonService.GetFilterDropdowns();
            ViewBag.AllowanceType = new SelectList(await _dropdownService.GetSalaryPerDayAllowanceDropDown(), "Code", "Name");
            return View("~/Areas/DataBank/Reports/Views/SalaryPerDayReport/Index.cshtml", model);
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
        public async Task<IActionResult> GetList(Filter filter1,int dividingfactor,int salarydays,string allowance)
        {
            try
            {
                SalaryPerDayReport model = new SalaryPerDayReport();
                SalaryPerDayReport m = new SalaryPerDayReport();



                Filter filter = new Filter();
                model.filter = filter1;

                model.DividingFactor = dividingfactor;
                model.SalaryDays = salarydays;
                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }
                if (allowance != null)
                {
                    string[] values = allowance.Split(',');
                    for (int i = 0; i < values.Length; i++)
                    {
                        values[i] = values[i].Trim();
                    }

                    model.Allowance = Array.ConvertAll(values, int.Parse);
                }
                model.filter = filter1;
                model = await _SalaryPerDayReportService.GetList(model);

                m.EmployeeSalaryPerDayList = model.EmployeeSalaryPerDayList;
                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.EmployeeSalaryPerDayList == null || model.EmployeeSalaryPerDayList.Count() == 0)
                {


                    return Json(new
                    {
                        data = "",
                        draw = filter1.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows
                    });
                }




                if (model.EmployeeSalaryPerDayList.Count > 0)
                {
                    totalrows = model.active;


                }

                //model.AllTabCount = m.AllTabCount;
                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = m.EmployeeSalaryPerDayList[0].totalRecords;
                    return Json(new
                    {
                        data = model.EmployeeSalaryPerDayList,
                        draw = filter1.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows//totalrowsafterfiltering
                    });
                }
                else
                {
                    return Json(new
                    {
                        data = model.EmployeeSalaryPerDayList,
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




        public async Task<IActionResult> ExportToExcel(string allowance, string[] allowanceType,int SalaryDays,int DividingFactor,int totRecords, Filter filter)
        {
            try
            {
                SalaryPerDayReport _model = new SalaryPerDayReport();
                _model.SalaryDays = SalaryDays;
                _model.DividingFactor = DividingFactor;
                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                int slNo = 0;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Salary Per Day Report", "Excel");

                filter.length = totRecords;
                filter.start = 0;
                string FilterValues = await _helpers.GetFilterValues(filter);
                if (FilterValues == "")
                {
                    FilterValues = "All";
                }
                //model.filter = Global.GlobalFilter;
                //model.BloodGroup = group;

                _model.filter = filter;

                if (allowance != null)
                {
                    string[] values = allowance.Split(',');
                    for (int i = 0; i < values.Length; i++)
                    {
                        values[i] = values[i].Trim();
                    }

                    _model.Allowance = Array.ConvertAll(values, int.Parse);
                }


                string currDateTime = await _helpers.GetCurrentDate();
                


                //Get Dataset
   
                var dataset = await _SalaryPerDayReportService.GetList_Excel(_model);
                DataTable dt = dataset.Tables[0];

                //set EXCEL Column Heading 
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (dt.Columns[i].ColumnName == "id" || dt.Columns[i].ColumnName == "ctc")
                        dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.ToUpper();
                    else
                        dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
                }

                using (var workbook = new XLWorkbook())
                {

                    var worksheet = workbook.Worksheets.Add("Salary Per Day Report List");

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
                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                    wsReportNameHeaderRange.Value = "Salary Per Day Report";

                    worksheet.Cell(2, 1).Style.Font.Bold = true;
                    worksheet.Cell(2, 1).Value = "Group Company Name";
                    worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();

                    worksheet.Cell(3, 1).Style.Font.Bold = true;
                    worksheet.Cell(3, 1).Value = "Salary Days";
                    worksheet.Cell(3, 2).Value =SalaryDays;

                    worksheet.Cell(3, 3).Style.Font.Bold = true;
                    worksheet.Cell(3, 3).Value = "Dividing Factor";
                    worksheet.Cell(3, 4).Value = DividingFactor;

                    worksheet.Cell(3, 5).Style.Font.Bold = true;
                    worksheet.Cell(3, 5).Value = "Allowances";

                    int cnt = allowanceType.Count();

                    for (int i = 0; i < cnt; i++)
                    {
                        worksheet.Cell(3, 6).Value = allowanceType[i];

                    }



                    worksheet.Cell(4, 1).Style.Font.Bold = true;
                    worksheet.Cell(4, 1).Value = "Login User";
                    worksheet.Cell(4, 2).Value = LoginUser;
                    worksheet.Cell(4, 3).Style.Font.Bold = true;
                    worksheet.Cell(4, 3).Value = "Date & Time";
                    worksheet.Cell(4, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(4, 4).Style.DateFormat.Format = "dd-MMM-yyyy   h:mm AM/PM";
                    worksheet.Cell(4, 4).Value = currDateTime.ToString();
                    worksheet.Cell(4, 5).Style.Font.Bold = true;
                    worksheet.Cell(4, 5).Value = "Total Count";
                    worksheet.Cell(4, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(4, 6).Value = dt.Rows.Count;
                    worksheet.Cell(5, 1).Style.Font.Bold = true;
                    worksheet.Cell(5, 1).Value = "Filter Applied";
                    var filterapplied = worksheet.Range("B5:H5").Merge();

                    filterapplied.Value = FilterValues;


                    var currentRow = 6;

                    worksheet.Cell(currentRow, 1).InsertTable(dt);
                    worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                    worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                    worksheet.Range("A6:H6").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Range("A6:H6").Style.Font.Bold = true;


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
                            "Salary Per Day Report List.xlsx"
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


        public async Task<IActionResult> ApplyFilter(Filter filter)
        {
            try
            {
                SalaryPerDayReport model = new SalaryPerDayReport();
                model.filter = filter;


                model = await _SalaryPerDayReportService.GetList(model);

                return PartialView("~/Areas//DataBank/Reports/Views/SalaryPerDayReport/Index.cshtml", model);
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


        //[HttpPost]
        //public async Task<IActionResult> GetList(int dividingfactor, int salarydays,stri Filter filter)
        //{
        //    try
        //    {
        //       SalaryPerDayReport model = new SalaryPerDayReport();

        //        if (filter.searchValue == "undefined" || filter.searchValue == null)
        //        {
        //            filter.searchValue = "";
        //        }

        //        //if (Allowance != null)
        //        //{
        //        //    string[] values = Allowance.Split(',');
        //        //    for (int i = 0; i < values.Length; i++)
        //        //    {
        //        //        values[i] = values[i].Trim();
        //        //    }

        //            //model.Allowance = Array.ConvertAll(values, int.Parse);
        //      //  }
        //        model.filter = filter;
        //        model = await _SalaryPerDayReportService.GetList(model);
        //        int totalrows = 0;
        //        int totalrowsafterfiltering = 0;
        //        if (model.EmployeeSalaryPerDayList == null || model.EmployeeSalaryPerDayList.Count() == 0)
        //        {
        //            return Json(new
        //            {
        //                data = "",
        //                draw = filter.draw,//Request.Form["draw"], 
        //                recordsTotal = totalrows,
        //                recordsFiltered = totalrows
        //            });
        //        }

        //        if (model.EmployeeSalaryPerDayList.Count > 0)
        //        {
        //            totalrows = model.EmployeeSalaryPerDayList[0].active;
        //        }
        //        if (filter.searchValue != "")
        //        {
        //            totalrowsafterfiltering = model.EmployeeSalaryPerDayList[0].totalRecords;
        //            return Json(new
        //            {
        //                data = model.EmployeeSalaryPerDayList,
        //                draw = filter.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
        //                recordsTotal = totalrows,
        //                recordsFiltered = totalrows//totalrowsafterfiltering
        //            });
        //        }
        //        else
        //        {
        //            return Json(new
        //            {
        //                data = model.EmployeeSalaryPerDayList,
        //                draw = filter.draw,//Request.Form["draw"], 
        //                recordsTotal = totalrows,
        //                recordsFiltered = totalrows
        //            });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return View();
        //    }
        //}

    }
}
