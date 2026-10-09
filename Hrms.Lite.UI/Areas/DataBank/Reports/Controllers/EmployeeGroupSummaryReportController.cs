using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.DataBank.Reports;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.DataBank.Reports;
using Hrms.Lite.Shared.General;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Spire.Xls;
using Newtonsoft.Json;
using Hrms.Lite.Shared.Account;

namespace Hrms.Lite.UI.Areas.DataBank.Reports.Controllers
{
    [Area("DataBank")]
    [SessionAuthorize]
    public class EmployeeGroupSummaryReportController : Controller
    {
        private readonly IEmployeeGroupSummaryReportService _EmployeeGroupSummaryReportService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        private readonly IDropdownService _dropdownService;

    
        public EmployeeGroupSummaryReportController(IEmployeeGroupSummaryReportService EmployeeGroupSummaryReportService, ICommonService commonService, IHelpers Helpers, IDropdownService dropdownService)
        {
            _EmployeeGroupSummaryReportService = EmployeeGroupSummaryReportService;
            _commonService = commonService;
            _helpers = Helpers;
            _dropdownService = dropdownService;
        }


        public async Task <IActionResult> Index()
        {
            try { 
            EmployeeGroupSummaryReport model = new EmployeeGroupSummaryReport();
            ViewBag.Groupby = new SelectList(await _dropdownService.GetEmployeeGroupSummary(), "Code", "Name");
            Filter filter = new Filter();
            model = await _EmployeeGroupSummaryReportService.GetEmployeeGroupSummaryReportList(model);
           
            return View("~/Areas/DataBank/Reports/Views/EmployeeGroupSummaryReport/Index.cshtml",model);
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

        public async Task<ActionResult> GetList(int GroupbyX, int GroupbyY, string GroupbyXName)
        {
            try
            {
                EmployeeGroupSummaryReport model = new EmployeeGroupSummaryReport(); 
                model.filter = new Filter();
                model.GroupByX = GroupbyX;
                model.GroupByY = GroupbyY;
               model.filter= new Filter();
                ViewBag.GroupbyXName = GroupbyXName;

                model = await _EmployeeGroupSummaryReportService.GetEmployeeGroupSummaryReportList(model);

                return PartialView("~/Areas/DataBank/Reports/Views/EmployeeGroupSummaryReport/EmployeeGroupSummaryReportList.cshtml", model);
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

     
        public async Task<IActionResult> GetPopUp(int GroupbyY,string GroupbyYName)
        {
            try { 
            EmployeeGroupSummaryReport model = new EmployeeGroupSummaryReport();

            model = await _EmployeeGroupSummaryReportService.GetPopUp(GroupbyY);
            ViewBag.GroupbyYName = GroupbyYName;
           
            return PartialView("~/Areas/DataBank/Reports/Views/EmployeeGroupSummaryReport/_PopUp.cshtml", model);
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
        public async Task<ActionResult> FilterColumn(EmployeeGroupSummaryReport model,int GroupbyX, int GroupbyY,string GroupbyXName)
        {
            try { 
  
            model.GroupByX = GroupbyX;
            model.GroupByY = GroupbyY;
            ViewBag.GroupbyXName = GroupbyXName;
            model.filter = new Filter();

                var list = from li in model.EmployeeGroupSummarySelectList
                           where li.Applicability == true
                           select li;
                model.EmployeeGroupSummarySelectList = list.ToList();


                model = await _EmployeeGroupSummaryReportService.GetEmployeeGroupSummaryReportList(model);
            return PartialView("~/Areas/DataBank/Reports/Views/EmployeeGroupSummaryReport/EmployeeGroupSummaryReportList.cshtml", model);
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
            try { 
            EmployeeGroupSummaryReport model = new EmployeeGroupSummaryReport();
            model.filter = filter;
            model = await _EmployeeGroupSummaryReportService.GetEmployeeGroupSummaryReportList(model);

            return PartialView("~/Areas/DataBank/Reports/Views/EmployeeGroupSummaryReport/EmployeeGroupSummaryReportList.cshtml", model);
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

        


        public async Task<IActionResult> ExportToExcel(int GroupbyY,string GroupbyYName,int GroupbyX,string GroupbyXName,Filter filter)
        {
            try { 
            EmployeeGroupSummaryReport model = new EmployeeGroupSummaryReport();

            var LoginUser = HttpContext.Session.GetString("LoginUser");
            var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Employee Group Summary (" + GroupbyXName + " Vs " + GroupbyYName + ")", "Excel");
                int slNo = 0;
            string FilterValues = await _helpers.GetFilterValues(filter);
            if (FilterValues == "")
            {
                FilterValues = "All";
            }
            model.GroupByX = GroupbyX;
            model.GroupByY = GroupbyY;
            model.filter = filter;
            model = await _EmployeeGroupSummaryReportService.GetEmployeeGroupSummaryReportList(model);
            string currDateTime = await _helpers.GetCurrentDate();
            
            using (var workbook = new XLWorkbook())
            {

                var worksheet = workbook.Worksheets.Add("Employee Group Summary Report");


                int count = model.Rows.Count + 5;

                string columnName = "";
                int Columncount = model.Head.Count + 1;

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
                wsReportNameHeaderRange.Value = "Employee Group Summary Report";

                worksheet.Cell(2, 1).Style.Font.Bold = true;
                worksheet.Cell(2, 1).Value = "Group Company Name";
                worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                //worksheet.Cell(2, 3).Style.Font.Bold = true;
                //worksheet.Cell(2, 3).Value = "Group By X";
                //worksheet.Cell(2, 4).Value = GroupbyXName;
                //worksheet.Cell(2, 5).Style.Font.Bold = true;
                //worksheet.Cell(2, 5).Value = "Group By Y";
                //worksheet.Cell(2, 6).Value = GroupbyYName;
                //worksheet.Cell(2, 7).Style.Font.Bold = true;
                worksheet.Cell(2, 3).Style.Font.Bold = true;
                worksheet.Cell(2, 3).Value = "Group By ";
                var dropdownSelected = worksheet.Range("D2:G2").Merge();
                dropdownSelected.Value =GroupbyXName + " Vs " + GroupbyYName;





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
                worksheet.Cell(3, 6).Value = model.Rows.Count();
                worksheet.Cell(4, 1).Style.Font.Bold = true;
                worksheet.Cell(4, 1).Value = "Filter Applied";
                var filterapplied = worksheet.Range("B4", colName +4).Merge();

                filterapplied.Value = FilterValues;


                var currentRow = 5;
                #region Header
                worksheet.Range("A5", colName + 5).Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                worksheet.Cell(currentRow, 1).Value = "Sl.No";
                worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 2).Value = GroupbyXName;
                worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 3).Value = "Total";
                worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 4).Value = "%";
                worksheet.Cell(currentRow, 4).Style.Font.Bold = true;

                var a = 5;
                for (int i = 3; i < model.Head.Count; i++)
                {
                    worksheet.Cell(currentRow, a).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, a).Value = model.Head[i];
                    a++;
                }


                #endregion

                #region Body
                for (int i = 0; i < model.Rows.Count; i++)
                {
                    currentRow++;
                    slNo = slNo + 1;
                    worksheet.Columns().AdjustToContents();
                    worksheet.Rows().AdjustToContents();
                    worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(currentRow, 1).Value = slNo;
                    worksheet.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(currentRow, 2).Value = model.Rows[i].Columns[0];
                    worksheet.Cell(currentRow, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(currentRow, 3).Value = model.Rows[i].Columns[1];
                    worksheet.Cell(currentRow, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(currentRow, 4).Value = model.Rows[i].Columns[2];


                    var b = 5;
                    for (int j = 3; j < model.Rows[i].Columns.Count; j++)
                    {
                        worksheet.Cell(currentRow, b).Value =model.Rows[i].Columns[j];
                        b++;
                    }

                }
                #endregion
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
                            book.Protect(EP.Password,false,false);
                            book.SaveToStream(stream);
                        }
                        var content = stream.ToArray();
                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "Employee Group Summary Report List.xlsx"
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
