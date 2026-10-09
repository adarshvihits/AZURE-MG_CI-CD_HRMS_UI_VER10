using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.TrainingModule;
using Hrms.Lite.Shared.Account;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TrainingModule;
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

namespace Hrms.Lite.UI.Areas.TrainingModule.Reports.Controllers
{
    [Area("TrainingModule")]
    [SessionAuthorize]
    public class Training_EmployeVsReportController : Controller
    {
        private readonly ITraining_EmployeVsReportService _Training_EmployeVsReportService;
        private readonly IDropdownService _dropdownService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;

        public Training_EmployeVsReportController(IDropdownService dropdownService, ICommonService commonService, ITraining_EmployeVsReportService Training_EmployeVsReportService, IHelpers helpers)
        {
            _Training_EmployeVsReportService = Training_EmployeVsReportService;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = helpers;
        }
       
        public async Task<IActionResult> Index()
        {
            Training_EmployeVsReport model = new Training_EmployeVsReport();
           
            ViewBag.Financialyr = new SelectList(await _dropdownService.GetFinancialYearDropdown(), "Code", "Name");
            ViewBag.Category = new SelectList(await _dropdownService.GetTrainingCategoryDropdown(), "Code", "Name");
            ViewBag.Training = new SelectList(await _dropdownService.GetTrainingEmployeeDropdown(0,0,0), "Code", "Name");
           
            var CurrFinCode = _commonService.GetCurrentLFinancialYear();
            model.FYCode= CurrFinCode.Result.Data;
            model.Filter = await _commonService.GetFilterDropdowns();
            return View("~/Areas/TrainingModule/Reports/Views/Training_EmployeVsReport/Index.cshtml", model);
        }
        [HttpPost]
        public async Task<ActionResult> GetList(int FYCode,int CategoryCode,int TrainingCode,Filter filter)
        {
            try
            {
                Training_EmployeVsReport model = new Training_EmployeVsReport();             

                    if (filter.searchValue == "undefined" || filter.searchValue == null)
                    {
                        filter.searchValue = "";
                    }
              
                if (FYCode != 0)
                  model = await _Training_EmployeVsReportService.GetTrainingEmployeeVsReportList(FYCode, CategoryCode, TrainingCode, filter);
               
                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.TrainingEmployeeReportList == null || model.TrainingEmployeeReportList.Count() == 0)
                {
                    return Json(new
                    {
                        data = "",
                        draw = filter.draw,
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows
                    });
                }
                if (model.TrainingEmployeeReportList.Count > 0)
                {
                    totalrows = model.TrainingEmployeeReportList[0].Active;
                }
                if (filter.searchValue != "")
                {
                    totalrowsafterfiltering = model.TrainingEmployeeReportList[0].TotalRecords;
                    return Json(new
                    {
                        data = model.TrainingEmployeeReportList,
                        draw = filter.draw,
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows //totalrowsafterFiltering
                    });

                }
                else
                {
                    return Json(new
                    {
                        data = model.TrainingEmployeeReportList,
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

        



        public async Task<ActionResult> GetEmployeeProfileReport(int EmpCode,int FYCode, int TrainingCode, int CategoryCode)
        {
            try
            {
                Training_EmployeVsReport model = new Training_EmployeVsReport();

                {
                    model = await _Training_EmployeVsReportService.GetEmployeeProfileReport(EmpCode, FYCode, TrainingCode, CategoryCode);
                }

                return PartialView("~/Areas/TrainingModule/Reports/Views/Training_EmployeVsReport/_EmployeeReport.cshtml", model);
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


        [HttpGet]

        public async Task<ActionResult> GetTrainingEmployeeComboData(int FYCode,int CategoryCode)
        {
            var data = await _dropdownService.GetTrainingReportDropdown(FYCode, CategoryCode);
            return Json(data);
        }

        public async Task<IActionResult> ExportToExcel(int FYCode,string FYName,int CategoryCode,string CategoryName,int TrainingCode,string TrainingName,Filter filter,int TotalRows)
        {           
             
                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Training Employee VsReport","Excel");
              

                filter.length = TotalRows;
                filter.start = 0;


                string FilterValues = await _helpers.GetFilterValues(filter);
                if (FilterValues == "")
                {
                    FilterValues = "All";
                }
             

                string currDateTime = await _helpers.GetCurrentDate();
                

                //Get Data Set
                var dataset = await _Training_EmployeVsReportService.TrainingEmployeeVsReport_Excel(FYCode, CategoryCode, TrainingCode, filter);
                DataTable dt = dataset.Tables[0];


                //set EXCEL Column heading
                for(int i = 0; i<dt.Columns.Count; i++)
                {
                if (dt.Columns[i].ColumnName == "doj")

                    dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.ToUpper();
                else
                    dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
                }

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Training EmployeeVs Report List");
                    int count = dt.Rows.Count+ 5;

                    worksheet.Range("A1:J" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:J" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:J" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:J" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:J" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;

                    var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:J{0}", 1, Char.ConvertFromUtf32(65 + 7)));
                    wsReportNameHeaderRange.Style.Font.Bold = true;
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Merge();
                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191); 
                    wsReportNameHeaderRange.Value = "Training Employee vs  Report List";

                    worksheet.Cell(2, 1).Style.Font.Bold = true;
                    worksheet.Cell(2, 1).Value = "Group Company Name";
                    worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                worksheet.Cell(2, 3).Style.Font.Bold = true;
                worksheet.Cell(2, 3).Value = "Financial Year";
                worksheet.Cell(2, 4).Value = FYName.ToUpper();

                worksheet.Cell(2, 5).Style.Font.Bold = true;
                worksheet.Cell(2, 5).Value = "Category";
                worksheet.Cell(2, 6).Value = CategoryName.ToUpper();


                worksheet.Cell(2, 7).Style.Font.Bold = true;
                worksheet.Cell(2, 7).Value = "Training";
                worksheet.Cell(2, 8).Value = TrainingName.ToUpper();




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
                    worksheet.Cell(3, 6).Value =dt.Rows.Count;
                worksheet.Cell(4, 1).Style.Font.Bold = true;
                worksheet.Cell(4, 1).Value = "Filter Applied";

                var filterapplied = worksheet.Range("B4:J4").Merge();

                filterapplied.Value = FilterValues;

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
                            Workbook book = new Spire.Xls.Workbook();
                            book.LoadFromStream(stream);
                            //Protect Workbook
                            book.Protect(EP.Password, false, false);
                            book.SaveToStream(stream);
                        }
                        var content = stream.ToArray();
                        return File(
                            content,
                            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                            "Training Report List.xlsx"
                            );
                    }
                }
            }
          
        public async Task<IActionResult> GetPopupDetails(int EmployeeCode, int FYCode,int CategoryCode,int TrainingCode)
        {
            try
            {

                Training_EmployeVsReport model = new Training_EmployeVsReport();

                model = await _Training_EmployeVsReportService.GetPopupDetails(EmployeeCode,FYCode,CategoryCode,TrainingCode);

                return PartialView("~/Areas/TrainingModule/Reports/Views/Training_EmployeVsReport/_TrainingDetailsPopup.cshtml", model);
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



        public async Task<IActionResult> PopupExportToExcel(int EmployeeCode, int FYCode, string FYName, int CategoryCode, int TrainingCode)
        {
            var LoginUser = HttpContext.Session.GetString("LoginUser");
            var CompanyName = HttpContext.Session.GetString("CompanyName");
            var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
            var GroupCompanyName = User.authUser.GroupCompanyName;
            ExcelPassword EP = new ExcelPassword();
            EP = await _commonService.GetExcelPassword("Training Details", "Excel");

            string currDateTime = await _helpers.GetCurrentDate();
            
            var dataset = await _Training_EmployeVsReportService.GetPopupExcel(EmployeeCode, FYCode, CategoryCode, TrainingCode);
           
            DataTable dt = dataset.Tables[0];
            DataTable dt1 = dataset.Tables[1];
            string empId = dt1.Rows[0][0].ToString();
            string empname = dt1.Rows[0][1].ToString();
            //set EXCEL Column Heading 
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                if (dt.Columns[i].ColumnName == "id" || dt.Columns[i].ColumnName == "fullName")
                    dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.ToUpper();
                else
                    dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
            }
            using (XLWorkbook wb = new XLWorkbook())
            {


                var worksheet = wb.Worksheets.Add("Training Details");
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
                wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);

                wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                wsReportNameHeaderRange.Merge();
                wsReportNameHeaderRange.Value = "Training Details - "+ empname +" ("+ empId +")";

                worksheet.Cell(2, 1).Style.Font.Bold = true;
                worksheet.Cell(2, 1).Value = "Group Company Name";
                worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                worksheet.Cell(2, 3).Style.Font.Bold = true;
                worksheet.Cell(2, 3).Value = "Financial Year";
                worksheet.Cell(2, 4).Value = FYName.ToUpper();



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
                //worksheet.Cell(4, 1).Style.Font.Bold = true;
                //worksheet.Cell(4, 1).Value = "Filter Applied";
                //worksheet.Range("B4:H4").Merge();
                //worksheet.Cell("B4").Value = FilterValues;

                var currentRow = 4;

                worksheet.Cell(currentRow, 1).InsertTable(dt);
                worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                worksheet.Range("A4:" + colName + "4").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                worksheet.Range("A4:" + colName + "4").Style.Font.Bold = true;

                worksheet.Columns().AdjustToContents();
                worksheet.Rows().AdjustToContents();
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
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Training List.xlsx");
                }

            }
        }
    }
    }

