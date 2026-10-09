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
    public class AppraisalDueListController : Controller
    {
        private readonly IAppraisalDueListService _AppraisalDueListService;
        private readonly IHelpers _helpers;
        private readonly IDropdownService _dropdownService;
        private readonly ICommonService _commonService;


       


        public AppraisalDueListController(IAppraisalDueListService AppraisalDueListService, IHelpers Helpers, IDropdownService dropdownService, ICommonService commonService)
        {
            _AppraisalDueListService = AppraisalDueListService;
            _helpers = Helpers;
            _dropdownService = dropdownService;
            _commonService = commonService;
        }


        public async Task<IActionResult> Index()
        {
            AppraisalDueList model = new AppraisalDueList();
            Filter Filter = new Filter();
            model.Filter = await _commonService.GetFilterDropdowns();
            ViewBag.Type = new SelectList(await _dropdownService.GetJobTransitionDropDown(), "Code", "Name");

            return View("~/Areas/DataBank/Reports/Views/AppraisalDueList/Index.cshtml", model);
        }


        [HttpPost]
        public async Task<IActionResult> GetList(int Type, int duration, Filter filter)
        {
            try
            {
                AppraisalDueList model = new AppraisalDueList();
                AppraisalDueList m = new AppraisalDueList();
              
                if (filter.searchValue == "undefined" || filter.searchValue == null)
                {
                    filter.searchValue = "";
                }

                model = await _AppraisalDueListService.GetAppraisalDueList(Type,duration, filter);

                m.AppraisalDueListList = model.AppraisalDueListList;
                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.AppraisalDueListList == null || model.AppraisalDueListList.Count() == 0)
                {
                    return Json(new
                    {
                        data = "",
                        draw = filter.draw,
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows
                    });
                }


                if (model.AppraisalDueListList.Count > 0)
                {
                    totalrows = m.AppraisalDueListList[0].active;

                }

                if (filter.searchValue != "")
                {
                    totalrowsafterfiltering = m.AppraisalDueListList[0].totalRecords;
                    return Json(new
                    {
                        data = model.AppraisalDueListList,
                        draw = filter.draw,
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows
                    });
                }
                else
                {
                    return Json(new
                    {
                        data = model.AppraisalDueListList,
                        draw = filter.draw,
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




        public async Task<IActionResult> ExportToExcel(int Type, int duration, Filter filter, int TotalRows, string TypeName, string durationName)
        {
            AppraisalDueList model = new AppraisalDueList();
            var LoginUser = HttpContext.Session.GetString("LoginUser");
            var CompanyName = HttpContext.Session.GetString("CompanyName");
            var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
            var GroupCompanyName = User.authUser.GroupCompanyName;
            ExcelPassword EP = new ExcelPassword();
            EP = await _commonService.GetExcelPassword("Attrition Report List", "Excel");


            filter.length = TotalRows;
            filter.start = 0;

            string FilterValues = await _helpers.GetFilterValues(filter);
            if (FilterValues == "")
            {
                FilterValues = "All";
            }
            model.Filter = filter;



            string[] cu_Date = System.DateTime.Now.GetDateTimeFormats();
            string currDateTime = cu_Date[44];

            //Get Dataset
            var dataset = await _AppraisalDueListService.AppraisalDueList_Excel(Type, duration, filter);
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
                var worksheet = workbook.Worksheets.Add("Appraisal Due List");
                int count = dt.Rows.Count + 6;


                worksheet.Range("A1:I" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1:I" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1:I" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1:I" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1:I" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;



                var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 10)));
                wsReportNameHeaderRange.Style.Font.Bold = true;
                wsReportNameHeaderRange.Style.Font.FontSize = 15;
                wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                wsReportNameHeaderRange.Merge();
                wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                wsReportNameHeaderRange.Value = "Appraisal Due List";

                worksheet.Cell(2, 1).Style.Font.Bold = true;
                worksheet.Cell(2, 1).Value = "Group Company Name";
                worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                worksheet.Cell(2, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);

                worksheet.Cell(3, 1).Style.Font.Bold = true;
                worksheet.Cell(3, 1).Value = "Type";
                worksheet.Cell(3, 2).Value = TypeName;
                worksheet.Cell(3, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                //worksheet.Cell(3, 2).Style.DateFormat.Format = "dd-MMM-yyyy";
                worksheet.Cell(3, 3).Style.Font.Bold = true;
                worksheet.Cell(3, 3).Value = "Duration";
                worksheet.Cell(3, 4).Value = durationName;
                worksheet.Cell(3, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                //worksheet.Cell(3, 4).Style.DateFormat.Format = "dd-MMM-yyyy";


               
                worksheet.Cell(4, 1).Style.Font.Bold = true;
                worksheet.Cell(4, 1).Value = "Login User";
                worksheet.Cell(4, 2).Value = LoginUser;
                worksheet.Cell(4, 3).Style.Font.Bold = true;
                worksheet.Cell(4, 3).Value = "Date & Time";
                worksheet.Cell(4, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                worksheet.Cell(4, 4).Style.DateFormat.Format = "dd-MMM-yyyy h:mm AM/PM";
                worksheet.Cell(4, 4).Value = currDateTime.ToString();
                worksheet.Cell(4, 5).Style.Font.Bold = true;
                worksheet.Cell(4, 5).Value = "Total Count";
                worksheet.Cell(4, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                worksheet.Cell(4, 6).Value = dt.Rows.Count;
                worksheet.Cell(5, 1).Style.Font.Bold = true;
                worksheet.Cell(5, 1).Value = "Filter Applied";
                var filterapplied = worksheet.Range("B5:K5").Merge();

                filterapplied.Value = FilterValues;


                var currentRow = 6;


                worksheet.Cell(currentRow, 1).InsertTable(dt);
                worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                worksheet.Range("A6:I6").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                worksheet.Range("A6:I6").Style.Font.Bold = true;
                worksheet.Range("G6:G" + (dt.Rows.Count + 5)).Style.DateFormat.Format = "dd-MMM-yyyy";
                worksheet.Range("I6:I" + (dt.Rows.Count + 5)).Style.DateFormat.Format = "dd-MMM-yyyy";

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
                        "AppraisalDueList List.xlsx"
                        );
                }
            }

            //}
        }


    }
}
