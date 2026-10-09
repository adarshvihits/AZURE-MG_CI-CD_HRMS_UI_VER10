using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.TimeOffice.Report;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TimeOffice.Report;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Spire.Xls;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Kendo.Mvc.Extensions;
using Hrms.Lite.Shared.PayRoll.Reports;
using Hrms.Lite.Shared.Account;

namespace Hrms.Lite.UI.Areas.TimeOffice.Report.Controllers
{
    [Area("TimeOffice")]
    [SessionAuthorize]
    public class MusterRollReportController : Controller
    {
       
        private readonly IDropdownService _dropdownService;
        private readonly IMusteRollReportService _musteRollReportService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        public MusterRollReportController(IMusteRollReportService service, IDropdownService dropdownService, ICommonService commonService, IHelpers Helpers)
        {
            _musteRollReportService = service;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = Helpers;

        }
        public static class Global
        {

            public static Filter GlobalFilter;
        }
        public async Task<IActionResult> Index()
        {
            try { 
            MusterRollReport model = new MusterRollReport();
            Filter filter = new Filter();
                DataTable dt = new DataTable();
                await fillfilterDropdown();
            var CurrentMonth = await _commonService.GetCurrentMonthDropdown();
            ViewBag.Month = new SelectList(await _dropdownService.GetMonthDropdown(), "Code", "Name",CurrentMonth.Data);
            model.Month = CurrentMonth.Data;
            model.filter = filter;
                var ds = await _musteRollReportService.GetMusterRollReportHeaders(model);
                dt = ds.Tables[0];
                ModifyColumnHeaders(dt);
                var tupleModel = new Tuple<MusterRollReport, System.Data.DataTable>(model, dt);

                model.Month = CurrentMonth.Data;
                model.filter = new Filter();
                // model = await _musteRollReportService.GetMusterRollReport(model);
                model.Month = CurrentMonth.Data;
                model.filter = new Filter();
            return View("~/Areas/TimeOffice/Report/Views/MusterRollReport/Index.cshtml", tupleModel);


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
    
        DefaultContractResolver contractResolver = new DefaultContractResolver
        {
            NamingStrategy = new CamelCaseNamingStrategy
            {
                OverrideSpecifiedNames = true
            }
        };
        public async Task<IActionResult> GetReport([DataSourceRequest] DataSourceRequest request, Filter filter1, int month)
        {
            if (filter1.searchValue != null)
            {
                filter1.start = 0;
                filter1.length = request.PageSize;
            }
            else
            {

                filter1.start = (request.Page - 1) * request.PageSize;
                filter1.length = request.PageSize;
            }

            MusterRollReport model = new MusterRollReport();

            model.Month = month;
            model.filter = filter1;
            Global.GlobalFilter = filter1;
            request.PageSize = 0;

            var ds = await _musteRollReportService.GetMusterRollReportDynamic(model);
            DataTable dt = new DataTable();


            dt = ds.Tables[0];
            ModifyColumnHeadersData(dt);
            var dataResult1 =dt.ToDataSourceResult(request);

            dataResult1.Total = Convert.ToInt32(ds.Tables[1].Rows[0]["ACTIVE"].ToString());


            return Json(dataResult1, new JsonSerializerSettings { ContractResolver = new DefaultContractResolver() });



        }

        public async Task<IActionResult> ReloadDiv(int month)
        {
            try
            {


                MusterRollReport model = new MusterRollReport();
                Filter filter = new Filter();
                await fillfilterDropdown();
                model.filter = filter;
                if (model.filter.length == 0)
                {
                    model.filter.length = 10;
                }
                model.Month = month;
                DataTable dt = new DataTable();
                var ds = await _musteRollReportService.GetMusterRollReportHeaders(model);
                dt = ds.Tables[0];
                ModifyColumnHeaders(dt);
                var tupleModel = new Tuple<MusterRollReport, System.Data.DataTable>(model, dt);
            
                return PartialView("~/Areas/TimeOffice/Report/Views/MusterRollReport/_MusterRollReportList.cshtml", tupleModel);
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
        void ModifyColumnHeaders(DataTable dataTable)
        {
            foreach (DataColumn column in dataTable.Columns)
            {
                string columnName = column.ColumnName;
                string modifiedColumnName = ModifyColumnName(columnName);
                column.ColumnName = modifiedColumnName;
            }
        }
        string ModifyColumnName(string columnName)
        {
            string modifiedColumnName = columnName;

            if (!string.IsNullOrEmpty(columnName))
            {
                char firstLetter = columnName[0];
                char modifiedFirstLetter = char.ToUpper(firstLetter);
                modifiedColumnName = modifiedFirstLetter + columnName.Substring(1);
            }

            return modifiedColumnName;
        }
        void ModifyColumnHeadersData(DataTable dataTable)
        {
            foreach (DataColumn column in dataTable.Columns)
            {
                string columnName = column.ColumnName;
                string modifiedColumnName = "";

                modifiedColumnName = ModifyColumnName(columnName);


                column.ColumnName = modifiedColumnName;
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
        public async Task<IActionResult>GetReportList(int Month)
        {
            try { 
            MusterRollReport model = new MusterRollReport();
            Filter filter = new Filter();
            model.Month = Month;
            model.filter = filter;
            model = await _musteRollReportService.GetMusterRollReport(model);
            return PartialView("~/Areas/TimeOffice/Report/Views/MusterRollReport/_MusterRollReportList.cshtml",model);
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

        public async Task<IActionResult> ApplyFilter(int Month, Filter filter)
        {
            try
            {


                MusterRollReport model = new MusterRollReport();
                Global.GlobalFilter = new Filter();
                model.Month = Month;
                model.filter = filter;
                model = await _musteRollReportService.GetMusterRollReport(model);

                return PartialView("~/Areas/TimeOffice/Report/Views/MusterRollReport/_MusterRollReportList.cshtml", model);
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
        public async Task<IActionResult> ExportToExcel(int month, string MonthName,int TotalRows)
        {
            try
            {

                if (Global.GlobalFilter == null)
                    Global.GlobalFilter = new Filter();
                else
                {
                    Global.GlobalFilter.length = TotalRows;
                    Global.GlobalFilter.start = 0;
                }
                string FilterValues = await _helpers.GetFilterValues(Global.GlobalFilter);

                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("MusterRoll Report", "Excel");

                if (FilterValues == "")
                {
                    FilterValues = "All";
                }

                string currDateTime = await _helpers.GetCurrentDate();
                

                MusterRollReport model = new MusterRollReport();
                Filter filter = new Filter(); 
                model.Month = month;
                 model.filter = Global.GlobalFilter;

                //Get Datset
                //var dataset = await _musteRollReportService.GetMusterRollReport_Excel(model);
                var dataset = await _musteRollReportService.GetMusterRollReportDynamic(model);
                DataTable dt = dataset.Tables[0];

                dataset.Tables[0].Rows.RemoveAt(0);
                if ((dataset.Tables[0]?.Rows?.Count ?? 0) > 0)
                {
                    dt = dataset.Tables[0];
                }

                //set EXCEL Column Heading 
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (dt.Columns[i].ColumnName == "code")
                    {
                        dt.Columns.RemoveAt(i);
                        
                        i = i - 1;
                    }
                    else
                    {                        
                        dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
                    }
                }

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("MusterRoll Report");
                    int count = dt.Rows.Count + 5;



                    string columnName = "";
                    int Columncount = dt.Columns.Count ;

                    while (Columncount > 0)
                    {
                        int modulo = (Columncount - 1) % 26;
                        columnName = Convert.ToChar('A' + modulo) + columnName;
                        Columncount = (Columncount - modulo) / 26;
                    }

                    string colName = columnName;

                    worksheet.Range("A1", colName + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1", colName + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1", colName + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1", colName + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1", colName + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;


                    var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, colName));
                    wsReportNameHeaderRange.Style.Font.Bold = true;
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Merge();
                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191); 
                    wsReportNameHeaderRange.Value = "MusterRoll Report";

                    worksheet.Cell(2, 1).Style.Font.Bold = true;
                    worksheet.Cell(2, 1).Value = "Group Company Name";
                    worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                    worksheet.Cell(2, 3).Style.Font.Bold = true;
                    worksheet.Cell(2, 3).Value = "Month";
                    worksheet.Cell(2, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(2, 4).Style.DateFormat.Format = "MMM-yyyy";
                    worksheet.Cell(2, 4).Value = MonthName;
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
                    worksheet.Range("B4", colName+4).Merge();
                    worksheet.Cell("B4").Value = FilterValues;



                    var currentRow = 5;

                    worksheet.Cell(currentRow, 1).InsertTable(dt);
                    worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                    worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                    worksheet.Range("A5", colName+5).Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Range("A5", colName+5).Style.Font.Bold = true;


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
                            "MusterRoll Report List.xlsx"
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


        //public async Task<IActionResult> ExportToExcel(int month, string MonthName)
        //{
        //    MusterRollReport model = new MusterRollReport();
        //    Filter filter = new Filter(); 
        //    model.Month = month;
        //    model.filter = Global.GlobalFilter;
        //    model = await _musteRollReportService.GetMusterRollReport(model);
        //    ExcelPassword EP = new ExcelPassword();
        //    EP = await _commonService.GetExcelPassword("MusterRollReport ", "Excel");
        //    int slNo = 0;
        //    if (Global.GlobalFilter == null)
        //        Global.GlobalFilter = new Filter();
        //    string FilterValues = await _helpers.GetFilterValues(Global.GlobalFilter);
        //    if (FilterValues == "")
        //    {
        //        FilterValues = "All";
        //    }
        //    string currDateTime = await _helpers.GetCurrentDate();
        //    
        //    var LoginUser = HttpContext.Session.GetString("LoginUser");
        //    var CompanyName = HttpContext.Session.GetString("CompanyName");


        //    using (var workbook = new XLWorkbook())
        //    {

        //        var worksheet = workbook.Worksheets.Add("MusterRollReport List");

        //        int count = model.Rows.Count + 5;

        //        string columnName = "";
        //        int Columncount = model.Head.Count + 1;

        //        while (Columncount > 0)
        //        {
        //            int modulo = (Columncount - 1) % 26;
        //            columnName = Convert.ToChar('A' + modulo) + columnName;
        //            Columncount = (Columncount - modulo) / 26;
        //        }

        //        string colName = columnName;

        //        worksheet.Range("A1", colName + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
        //        worksheet.Range("A1", colName + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
        //        worksheet.Range("A1", colName + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
        //        worksheet.Range("A1", colName + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
        //        worksheet.Range("A1", colName + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;



        //        var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, colName));
        //        wsReportNameHeaderRange.Style.Font.Bold = true;
        //        wsReportNameHeaderRange.Style.Font.FontSize = 15;
        //        wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        //        wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
        //        wsReportNameHeaderRange.Merge();


        //        wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
        //        wsReportNameHeaderRange.Style.Font.FontSize = 15;
        //        wsReportNameHeaderRange.Value = "MusterRollReport List";
        //        //worksheet.Range("A5:K5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
        //        worksheet.Cell(2, 1).Style.Font.Bold = true;
        //        worksheet.Cell(2, 1).Value = "Company Name";
        //        worksheet.Cell(2, 2).Value = CompanyName.ToUpper();
        //        worksheet.Cell(2, 3).Style.Font.Bold = true;
        //        worksheet.Cell(2, 3).Value = "Month";
        //        worksheet.Cell(2, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //        worksheet.Cell(2, 4).Style.DateFormat.Format = "MMM-yyyy";
        //        worksheet.Cell(2, 4).Value = MonthName;


        //        worksheet.Cell(3, 1).Style.Font.Bold = true;
        //        worksheet.Cell(3, 1).Value = "Login User";
        //        worksheet.Cell(3, 2).Value = LoginUser;
        //        worksheet.Cell(3, 3).Style.Font.Bold = true;
        //        worksheet.Cell(3, 3).Value = "Date & Time";
        //        worksheet.Cell(3, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //        worksheet.Cell(3, 4).Style.DateFormat.Format = "dd-MMM-yyyy   h:mm AM/PM";
        //        worksheet.Cell(3, 4).Value = currDateTime.ToString();
        //        worksheet.Cell(3, 5).Style.Font.Bold = true;
        //        worksheet.Cell(3, 5).Value = "Total Count";
        //        worksheet.Cell(3, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //        worksheet.Cell(3, 6).Value = model.Rows.Count;
        //        worksheet.Cell(4, 1).Style.Font.Bold = true;
        //        worksheet.Cell(4, 1).Value = "Filter Applied";
        //        var filterapplied = worksheet.Range("B4", colName + 4).Merge();

        //        filterapplied.Value = FilterValues;

        //        var currentRow = 5;
        //        #region Header
        //        worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
        //        worksheet.Cell(currentRow, 1).Value = "Sl.No";
        //        worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
        //        worksheet.Cell(currentRow, 2).Value = "ID";
        //        worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
        //        worksheet.Cell(currentRow, 3).Value = "Employee Name";
        //        worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
        //        worksheet.Cell(currentRow, 4).Value = "Father/Husband Name";
        //        worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
        //        worksheet.Cell(currentRow, 5).Value = "Gender";
        //        worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
        //        worksheet.Cell(currentRow, 6).Value = "Nature Of Works";
        //        worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
        //        worksheet.Cell(currentRow, 7).Value = "Date Of Entry In To Service";
        //        worksheet.Cell(currentRow, 8).Style.Font.Bold = true;
        //        worksheet.Cell(currentRow, 8).Value = "Man Days";
        //        var a = 9;
        //        for (int i = 7; i < model.Head.Count; i++)
        //        {
        //            worksheet.Cell(currentRow, a).Style.Font.Bold = true;
        //            worksheet.Cell(currentRow, a).Value = model.Head[i];
        //            a++;
        //        }



        //        #endregion

        //        #region Body
        //        for (int i = 0; i < model.Rows.Count; i++)
        //        {
        //            currentRow++;
        //            slNo = slNo + 1;
        //            worksheet.Columns().AdjustToContents();
        //            worksheet.Rows().AdjustToContents();
        //            worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //            worksheet.Cell(currentRow, 1).Value = slNo;
        //            worksheet.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //            worksheet.Cell(currentRow, 2).Value = model.Rows[i].Columns[0];
        //            worksheet.Cell(currentRow, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //            worksheet.Cell(currentRow, 3).Value = model.Rows[i].Columns[1];
        //            worksheet.Cell(currentRow, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //            worksheet.Cell(currentRow, 4).Value = model.Rows[i].Columns[2];
        //            worksheet.Cell(currentRow, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //            worksheet.Cell(currentRow, 5).Value = model.Rows[i].Columns[3];
        //            worksheet.Cell(currentRow, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //            worksheet.Cell(currentRow, 6).Value = model.Rows[i].Columns[4];
        //            worksheet.Cell(currentRow, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //            worksheet.Cell(currentRow, 7).Style.DateFormat.Format = "dd-MMM-yyyy";
        //            worksheet.Cell(currentRow, 7).Value = model.Rows[i].Columns[5];
        //            worksheet.Cell(currentRow, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //            worksheet.Cell(currentRow, 8).Value = model.Rows[i].Columns[6];

        //            var b = 9;
        //            for (int j = 7; j < model.Rows[i].Columns.Count; j++)
        //            {
        //                worksheet.Cell(currentRow, b).Value = model.Rows[i].Columns[j];
        //                b++;
        //            }

        //        }
        //        #endregion
        //        worksheet.Columns().AdjustToContents();
        //        using (var stream = new MemoryStream())
        //        {
        //            workbook.SaveAs(stream);
        //            var content = stream.ToArray();
        //            return File(
        //                content,
        //                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
        //                " MusterRollReport List.xlsx"
        //                );
        //        }
        //    }

        //}




    }
}
