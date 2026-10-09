using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.DataBank.Reports;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.Services.DataBank.Reports;
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

    public class SalaryProjectionReportController : Controller
    {
        private readonly ISalaryProjectionReportService _SalaryProjectionReportService;
        private readonly ICommonService _commonService;
        private readonly IDropdownService _dropdownService;
        private readonly IHelpers _helpers;


        public SalaryProjectionReportController(ISalaryProjectionReportService SalaryProjectionReportService, ICommonService commonService, IDropdownService dropdownService, IHelpers Helpers)
        {
            _SalaryProjectionReportService = SalaryProjectionReportService;
            _commonService = commonService;
            _dropdownService = dropdownService;
            _helpers = Helpers;
        }


        public async Task<IActionResult> Index()
        {
            SalaryProjectionReport model = new SalaryProjectionReport();
            //model.ds= await _SalaryProjectionReportService.GetList(null, null, "", "",model);
            return View("~/Areas/DataBank/Reports/Views/SalaryProjectionReport/Index.cshtml", model);
        }


        [HttpGet]
        public async Task<IActionResult> GetPopUp()
        {
            try
            {
                SalaryProjectionReport model = new SalaryProjectionReport();
                ViewBag.doc = new SelectList(await _dropdownService.GetAllowanceDropdown(), "Code", "Name");
                ViewBag.Dept = new SelectList(await _dropdownService.GetDepartmentDropdown(), "Code", "Name");
                ViewBag.Loc = new SelectList(await _dropdownService.GetLocationDropdown(), "Code", "Name");
                ViewBag.Dsg = new SelectList(await _dropdownService.GetDesignationDropdown(), "Code", "Name");

                return PartialView("~/Areas/DataBank/Reports/Views/SalaryProjectionReport/_popup.cshtml", model);
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


        public async Task<IActionResult> GetList(int AmountType, int Type, string Amount, string Allowance,SalaryProjectionReport model)
        {
            //SalaryProjectionReport model = new SalaryProjectionReport();

            if (model.Designation != null)
            {
                int Deslength = model.Designation.Length;

                string[] values=new string[Deslength];
                for (int i = 0; i < Deslength; i++)
                {
                  values[i] =  model.Designation[i].ToString();
                }

                model.Designation = Array.ConvertAll(values, int.Parse);
            }
            if (model.Department != null)
            {
                int Deplength = model.Department.Length;

                string[] values = new string[Deplength];
                for (int i = 0; i < Deplength; i++)
                {
                    values[i] = model.Department[i].ToString();
                }

                model.Department = Array.ConvertAll(values, int.Parse);
            }
            if (model.Location != null)
            {
                int Loclength = model.Location.Length;

                string[] values = new string[Loclength];
                for (int i = 0; i < Loclength; i++)
                {
                    values[i] = model.Location[i].ToString();
                }

                model.Location = Array.ConvertAll(values, int.Parse);
            }
            DataSet ds = await _SalaryProjectionReportService.GetList(AmountType, Type, Amount, Allowance, model);

            return PartialView("~/Areas/DataBank/Reports/Views/SalaryProjectionReport/_List.cshtml", ds);
        }



        //public async Task<IActionResult> ExportToExcel( int TotalRows)
        //{
        //    SalaryProjectionReport model = new SalaryProjectionReport();
        //    var LoginUser = HttpContext.Session.GetString("LoginUser");
        //    var CompanyName = HttpContext.Session.GetString("CompanyName");
        //    var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
        //    var GroupCompanyName = User.authUser.GroupCompanyName;
        //    ExcelPassword EP = new ExcelPassword();
        //    EP = await _commonService.GetExcelPassword("Attrition Report List", "Excel");

        //    //filter.length = TotalRows;
        //    //filter.start = 0;

        //    //string FilterValues = await _helpers.GetFilterValues(filter);
        //    //if (FilterValues == "")
        //    //{
        //    //    FilterValues = "All";
        //    //}
        //    //model.filter = filter;

        //    string[] cu_Date = System.DateTime.Now.GetDateTimeFormats();
        //    string currDateTime = cu_Date[44];

        //    //Get Dataset
        //    //var ds = await _SalaryProjectionReportService.SalaryProj_Excel();
        //    var ds = await _SalaryProjectionReportService.GetList(null, null, "", "", model);
        //    DataTable dt = ds.Tables[0];
        //    ds.Tables[0].Rows.RemoveAt(0);
        //    if ((ds.Tables[0]?.Rows?.Count ?? 0) > 0)
        //    {
        //        dt = ds.Tables[0];
        //    }

        //    //set EXCEL Column Heading 
        //    for (int i = 0; i < dt.Columns.Count; i++)
        //    {
        //        if (dt.Columns[i].ColumnName == "id")
        //            dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.ToUpper();
        //        if (dt.Columns[i].ColumnName == "doj" || dt.Columns[i].ColumnName == "lwd")
        //            dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.ToUpper();
        //        else
        //            dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
        //        if (dt.Columns[i].ColumnName == "Name")
        //            dt.Columns[i].ColumnName = "Employee Name";
        //        //dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
        //    }

        //    using (var workbook = new XLWorkbook())
        //    {
        //        var worksheet = workbook.Worksheets.Add("Salary Projection Report List");
        //        int count = dt.Rows.Count + 6;
        //        worksheet.Range("A1:L" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
        //        worksheet.Range("A1:L" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
        //        worksheet.Range("A1:L" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
        //        worksheet.Range("A1:L" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
        //        worksheet.Range("A1:L" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;


        //        //string colName = columnName;

        //        //worksheet.Range("A1", colName + rowcount).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
        //        //worksheet.Range("A1", colName + rowcount).Style.Border.TopBorder = XLBorderStyleValues.Thin;
        //        //worksheet.Range("A1", colName + rowcount).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
        //        //worksheet.Range("A1", colName + rowcount).Style.Border.RightBorder = XLBorderStyleValues.Thin;
        //        //worksheet.Range("A1", colName + rowcount).Style.Border.LeftBorder = XLBorderStyleValues.Thin;

        //        var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 11)));
        //        wsReportNameHeaderRange.Style.Font.Bold = true;
        //        wsReportNameHeaderRange.Style.Font.FontSize = 15;
        //        wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        //        wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
        //        wsReportNameHeaderRange.Merge();
        //        wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
        //        wsReportNameHeaderRange.Value = "Salary Projection Report List";

        //        worksheet.Cell(2, 1).Style.Font.Bold = true;
        //        worksheet.Cell(2, 1).Value = "Group Company Name";
        //        worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
        //        worksheet.Cell(2, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);

        //        //worksheet.Cell(3, 1).Style.Font.Bold = true;
        //        //worksheet.Cell(3, 1).Value = "FromDate";
        //        //worksheet.Cell(3, 2).Value = FromDate;
        //        //worksheet.Cell(3, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //        //worksheet.Cell(3, 2).Style.DateFormat.Format = "dd-MMM-yyyy";
        //        //worksheet.Cell(3, 3).Style.Font.Bold = true;
        //        //worksheet.Cell(3, 3).Value = "ToDate";
        //        //worksheet.Cell(3, 4).Value = ToDate;
        //        //worksheet.Cell(3, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //        //worksheet.Cell(3, 4).Style.DateFormat.Format = "dd-MMM-yyyy";

        //        //worksheet.Cell(2, 3).Style.Font.Bold = true;
        //        //worksheet.Cell(2, 3).Value = "MoodName";
        //        //worksheet.Cell(2, 4).Value = MoodName;
        //        //worksheet.Cell(2, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //        //worksheet.Cell(2, 4).Style.DateFormat.Format = "dd-MMM-yyyy";

        //        worksheet.Cell(4, 1).Style.Font.Bold = true;
        //        worksheet.Cell(4, 1).Value = "Login User";
        //        worksheet.Cell(4, 2).Value = LoginUser;
        //        worksheet.Cell(4, 3).Style.Font.Bold = true;
        //        worksheet.Cell(4, 3).Value = "Date & Time";
        //        worksheet.Cell(4, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //        worksheet.Cell(4, 4).Style.DateFormat.Format = "dd-MMM-yyyy";
        //        worksheet.Cell(4, 4).Value = currDateTime.ToString();
        //        worksheet.Cell(4, 5).Style.Font.Bold = true;
        //        worksheet.Cell(4, 5).Value = "Total Count";
        //        worksheet.Cell(4, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //        worksheet.Cell(4, 6).Value = dt.Rows.Count;
        //        worksheet.Cell(5, 1).Style.Font.Bold = true;
        //        //worksheet.Cell(5, 1).Value = "Filter Applied";
        //        //var filterapplied = worksheet.Range("B5:L5").Merge();

        //        //filterapplied.Value = FilterValues;


        //        var currentRow = 6;


        //        worksheet.Cell(currentRow, 1).InsertTable(dt);
        //        worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

        //        worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
        //        worksheet.Range("A6:L6").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
        //        worksheet.Range("A6:L6").Style.Font.Bold = true;

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
        //                book.Protect(EP.Password);
        //                book.SaveToStream(stream);
        //            }
        //            var content = stream.ToArray();
        //            return File(
        //                content,
        //                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
        //                "SalaryProjectionReportList.xlsx"
        //                );
        //        }
        //    }
        //}



        public async Task<IActionResult> ExportToExcel(int TotalRows)
        {
            SalaryProjectionReport model = new SalaryProjectionReport();
            var LoginUser = HttpContext.Session.GetString("LoginUser");
            var CompanyName = HttpContext.Session.GetString("CompanyName");

            ExcelPassword EP = new ExcelPassword();
            EP = await _commonService.GetExcelPassword("Yearly Report", "Excel");


            string currDateTime = await _helpers.GetCurrentDate();
            

            var ds = await _SalaryProjectionReportService.GetList(null, null, "", "", model);
            ds.Tables[0].Columns.RemoveAt(0);
            DataTable dt = ds.Tables[0];
            var index = 0;

            //set EXCEL Column Heading 
            for (int i = 0; i < dt.Columns.Count; i++)
            {

                if (dt.Columns[i].ColumnName == "eucode")

                    dt.Columns[i].ColumnName = "Employee ID";
                else if (dt.Columns[i].ColumnName == "ename")
                    dt.Columns[i].ColumnName = "Employee Name";
                else
                    dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
            }


            using (XLWorkbook wb = new XLWorkbook())
            {


                var worksheet = wb.Worksheets.Add("Salary Projection Report List");
                int rowcount = dt.Rows.Count + 5;
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
                wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);

                wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                wsReportNameHeaderRange.Merge();
                wsReportNameHeaderRange.Value = "Salary Projection Report List";

                worksheet.Cell(2, 1).Style.Font.Bold = true;
                worksheet.Cell(2, 1).Value = "Company Name";
                worksheet.Cell(2, 2).Value = CompanyName.ToUpper();
                worksheet.Cell(2, 5).Style.Font.Bold = true;
                worksheet.Cell(3, 1).Style.Font.Bold = true;
                worksheet.Cell(3, 1).Value = "Login User";
                worksheet.Cell(3, 2).Value = LoginUser.ToString();
                worksheet.Cell(3, 3).Style.Font.Bold = true;
                worksheet.Cell(3, 3).Value = "Date & Time";
                worksheet.Cell(3, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                worksheet.Cell(3, 4).Style.DateFormat.Format = "dd-MMM-yyyy   h:mm AM/PM";
                worksheet.Cell(3, 4).Value = currDateTime.ToString();
                worksheet.Cell(3, 5).Style.Font.Bold = true;
                worksheet.Cell(3, 5).Value = "Total Count";
                worksheet.Cell(3, 6).Value = dt.Rows.Count;


                var currentRow = 4;

                worksheet.Cell(currentRow, 1).InsertTable(dt);
                worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                worksheet.Range("A4:" + colName + "4").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                worksheet.Range("A4:" + colName + "4").Style.Font.Bold = true;


                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    if (EP.NeedPassword)
                    {
                        Workbook book = new Workbook();
                        book.LoadFromStream(stream);
                        //Protect Workbook
                        book.Protect(EP.Password, false, false);
                        book.SaveToStream(stream);
                    }
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "SalaryProjectionReport List.xlsx");
                }
            }
        }



    }
}
