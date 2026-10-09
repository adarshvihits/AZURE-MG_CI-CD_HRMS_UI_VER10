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
    public class YearOfServiceReportController : Controller
    {
        private readonly IYearOfServiceReportService _YearOfServiceReportService;
        private readonly IHelpers _helpers;
        private readonly IDropdownService _dropdownService;
        private readonly ICommonService _commonService;


        

        public YearOfServiceReportController(IYearOfServiceReportService YearOfServiceReportService, IHelpers Helpers, IDropdownService dropdownService, ICommonService commonService)
        {
            _YearOfServiceReportService = YearOfServiceReportService;
            _helpers = Helpers;
            _dropdownService = dropdownService;
            _commonService = commonService;
        }


        public async Task<IActionResult> Index()
        {
            YearOfServiceReport model = new YearOfServiceReport();

            Filter filter = new Filter();
          
            List<SelectListItem> items = new List<SelectListItem>
        {
            new SelectListItem { Text = "Department", Value = "1" },
            new SelectListItem { Text = "Designation", Value = "2" },
            new SelectListItem { Text = "Grade", Value = "3" },
            new SelectListItem { Text = "Location", Value = "4"},
            new SelectListItem { Text = "Section", Value = "5"}
        };

            ViewBag.DropDownItems = items;
            //model.Type.Code = 1;
            model.Type = new Dropdown();
            model.Type.Code = 1;

            model = await _YearOfServiceReportService.GetYearOfServiceReport(1,"Current", filter);
            model.filter = await _commonService.GetFilterDropdowns();
            model.option = "Current";
            return View("~/Areas/DataBank/Reports/Views/YearOfServiceReport/Index.cshtml", model);
        }



        //public async Task<IActionResult> GetYearOfServiceReport( string Type, Filter filterdata)
        //{
        //    try
        //    {
        //        YearOfServiceReport model = new YearOfServiceReport();

        //        if (filterdata.searchValue == "undefined" || filterdata.searchValue == null)
        //        {
        //            filterdata.searchValue = "";
        //        }

        //        model = await _YearOfServiceReportService.GetYearOfServiceReport(Type, filterdata);

        //        int totalrows = 0;
        //        int totalrowsafterfiltering = 0;

        //        if (model.YearOfServiceReportList == null || model.YearOfServiceReportList.Count() == 0)
        //        {


        //            return Json(new
        //            {
        //                data = "",
        //                draw = filterdata.draw,//Request.Form["draw"], 
        //                recordsTotal = totalrows,
        //                recordsFiltered = totalrows
        //            });
        //        }

        //        if (model.YearOfServiceReportList.Count > 0)
        //        {
        //            totalrows = model.YearOfServiceReportList[0].active;
        //        }
        //        if (filterdata.searchValue != "")
        //        {
        //            totalrowsafterfiltering = model.YearOfServiceReportList[0].totalRecords;

        //            return Json(new
        //            {
        //                data = model.YearOfServiceReportList,
        //                draw = filterdata.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
        //                recordsTotal = totalrows,
        //                recordsFiltered = totalrows//totalrowsafterfiltering
        //            });
        //        }
        //        else
        //        {
        //            return Json(new
        //            {
        //                data = model.YearOfServiceReportList,
        //                draw = filterdata.draw,//Request.Form["draw"], 
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



        public async Task<IActionResult> GetYearOfServiceReport(int Type,string listCategory, Filter filterdata)
        {
            YearOfServiceReport model = new YearOfServiceReport();
            Filter filter = new Filter();
            model = await _YearOfServiceReportService.GetYearOfServiceReport(Type, listCategory, filterdata);
            model.filter = await _commonService.GetFilterDropdowns();
            //model.Type = Type;

            return PartialView("~/Areas/DataBank/Reports/Views/YearOfServiceReport/_List.cshtml", model);
        }






        public async Task<IActionResult> ExportToExcel(int Type,string TypeName, string listCategory, Filter filterdata, int TotalRows)
        {
            YearOfServiceReport model = new YearOfServiceReport();
            var LoginUser = HttpContext.Session.GetString("LoginUser");
            var CompanyName = HttpContext.Session.GetString("CompanyName");
            var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
            var GroupCompanyName = User.authUser.GroupCompanyName;
            ExcelPassword EP = new ExcelPassword();
            EP = await _commonService.GetExcelPassword("Classification vs Service", "Excel");


            filterdata.length = TotalRows;
            filterdata.start = 0;

            string FilterValues = await _helpers.GetFilterValues(filterdata);
            if (FilterValues == "")
            {
                FilterValues = "All";
            }
            model.filter = filterdata;



            string[] cu_Date = System.DateTime.Now.GetDateTimeFormats();
            string currDateTime = cu_Date[44];

            //Get Dataset
            var dataset = await _YearOfServiceReportService.YearOfServiceReport_Excel(Type, listCategory, filterdata);
            DataTable dt = dataset.Tables[0];
            dataset.Tables[0].Rows.RemoveAt(0);
            if ((dataset.Tables[0]?.Rows?.Count ?? 0) > 0)
            {
                dt = dataset.Tables[0];
            }


            //set EXCEL Column Heading 
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                if (dt.Columns[i].ColumnName == "doj")
                    dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.ToUpper();
                else
                    dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
            }

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Classification vs Service");
                int count = dt.Rows.Count + 6;


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
                wsReportNameHeaderRange.Value = "Classification vs Service- " + listCategory + " List";

                worksheet.Cell(2, 1).Style.Font.Bold = true;
                worksheet.Cell(2, 1).Value = "Group Company Name";
                worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                worksheet.Cell(2, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);

                worksheet.Cell(3, 1).Style.Font.Bold = true;
                worksheet.Cell(3, 1).Value = "Division";
                worksheet.Cell(3, 2).Value = TypeName;
                worksheet.Cell(3, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                //worksheet.Cell(3, 3).Style.Font.Bold = true;
                //worksheet.Cell(3, 3).Value = "ToDate";
                //worksheet.Cell(3, 4).Value = ToDate;
                //worksheet.Cell(3, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);



                //worksheet.Cell(2, 3).Style.Font.Bold = true;
                //worksheet.Cell(2, 3).Value = "MoodName";
                //worksheet.Cell(2, 4).Value = MoodName;
                //worksheet.Cell(2, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                //worksheet.Cell(2, 4).Style.DateFormat.Format = "dd-MMM-yyyy";



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
                worksheet.Range("E6:E" + (dt.Rows.Count + 5)).Style.DateFormat.Format = "dd-MMM-yyyy";
                worksheet.Range("F6:F" + (dt.Rows.Count + 5)).Style.DateFormat.Format = "dd-MMM-yyyy";


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
                        book.Protect(EP.Password);
                        book.SaveToStream(stream);
                    }
                    var content = stream.ToArray();
                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "ClassificationVsService Report.xlsx"
                        );
                }
            }

            //}
        }










    }

}
