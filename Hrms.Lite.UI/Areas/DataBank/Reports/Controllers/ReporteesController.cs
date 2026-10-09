using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.DataBank.Reports;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.Account;
using Hrms.Lite.Shared.DataBank.Reports;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
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
    public class ReporteesController : Controller
    {
        private readonly IReporteesService _ReporteesService;
        private readonly ICommonService _commonService;
        private readonly IDropdownService _dropdownService;
        private readonly IHelpers _helpers;

       



        public ReporteesController(IReporteesService _reporteesService, ICommonService commonService, IDropdownService dropdownService, IHelpers Helpers)
        {
            _ReporteesService = _reporteesService;
            _commonService = commonService;
            _dropdownService = dropdownService;
            _helpers = Helpers;
        }
        public async Task<IActionResult> Index()
        {
            try { 
            Reportees model = new Reportees();
            Filter filter = new Filter();
            filter.length = 10;
            model = await _ReporteesService.GetReporteesList(0, 0, 0, filter);
            model.Filter = await _commonService.GetFilterDropdowns();
            ViewBag.Authority = new SelectList(await _dropdownService.GetAuthorityDropDown(0,0), "Code", "Name");
            ViewBag.Type = new SelectList(await _dropdownService.GetRequestTypeDropDown(), "Code", "Name");
            ViewBag.Level = new SelectList(await _dropdownService.GetRequestLevelDropDown(0), "Code", "Name");
            return View("~/Areas/DataBank/Reports/Views/Reportees/Index.cshtml", model);
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
        public async Task<JsonResult> GetLevelDropDown(int TypeCode)
        {
            SelectList data = new SelectList("", "Code", "Name");
            data = new SelectList(await _dropdownService.GetRequestLevelDropDown(TypeCode), "Code", "Name");
            return Json(data);
        }
        [HttpGet]
        public async Task<JsonResult> GetAuthorityDropDown(int TypeCode,int LevelCode)
        {
            SelectList data = new SelectList("", "Code", "Name");
            data = new SelectList(await _dropdownService.GetAuthorityDropDown(TypeCode,LevelCode), "Code", "Name");
            return Json(data);
        }
        
        [HttpPost]
        public async Task<IActionResult> GetList(Filter filter1, int Authority,int Type,int Level)
        {
            try
            {
                Reportees model = new Reportees();
                Reportees m = new Reportees();





                Filter filter = new Filter();
                model.Filter = filter1;


                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }
              

                    model = await _ReporteesService.GetReporteesList(Type,Level,Authority, filter1);

              
                m.ReporteesList = model.ReporteesList;
                int totalrows = 0;

                    int totalrowsafterfiltering = 0;

                    if (model.ReporteesList == null || model.ReporteesList.Count() == 0)
                    {


                        return Json(new
                        {
                            data = "",
                            draw = filter1.draw,//Request.Form["draw"], 
                            recordsTotal = totalrows,


                            recordsFiltered = totalrows
                        });
                    }



                    if (model.ReporteesList.Count > 0)
                    {
                        totalrows = m.ReporteesList[0].active;


                    }


                    if (filter1.searchValue != "")
                    {
                        totalrowsafterfiltering = m.ReporteesList[0].totalRecords;
                        return Json(new
                        {
                            data = model.ReporteesList,
                            draw = filter1.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                            recordsTotal = totalrows,

                            recordsFiltered = totalrows//totalrowsafterfiltering
                        });
                    }
                    else
                    {
                        return Json(new
                        {
                            data = model.ReporteesList,
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
        //public async Task<IActionResult> ExportToExcel(int Type, int Level,int Authority, string AuthorityName, int totRecords)
        //{
        //    try
        //    {
        //        int slNo = 0;
        //        Reportees model = new Reportees();
        //        if (Global.GlobalFilter == null)
        //            Global.GlobalFilter = new Filter();
        //        else
        //        {
        //            Global.GlobalFilter.length = totRecords;
        //            Global.GlobalFilter.start = 0;
        //        }
        //        string FilterValues = await _helpers.GetFilterValues(Global.GlobalFilter);



        //        var LoginUser = HttpContext.Session.GetString("LoginUser");
        //        var CompanyName = HttpContext.Session.GetString("CompanyName");
        //        var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
        //        var GroupCompanyName = User.authUser.GroupCompanyName;
        //        ExcelPassword EP = new ExcelPassword();
        //        EP = await _commonService.GetExcelPassword("Reportees Register", "Excel");



        //        if (FilterValues == "")
        //        {
        //            FilterValues = "All";
        //        }
        //        string currDateTime = await _helpers.GetCurrentDate();
        //        

        //        model = await _ReporteesService.GetReporteesList(Type,Level,Authority, Global.GlobalFilter);
        //        int count = 10;
        //        int colcount = 0;
        //        string columnName = "";
        //        int Columncount = 0;

        //        //set EXCEL Column Heading Format

        //        using (var workbook = new XLWorkbook())
        //        {
        //            var worksheet = workbook.Worksheets.Add("Reportees Register");


        //            colcount = model.ReporteesList.Count + 4;
        //            Columncount = count;
        //            while (Columncount > 0)
        //            {
        //                int modulo = (Columncount - 1) % 26;
        //                columnName = Convert.ToChar('A' + modulo) + columnName;
        //                Columncount = (Columncount - modulo) / 26;
        //            }

        //            string colName = columnName;


        //            worksheet.Range("A1", colName + colcount).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
        //            worksheet.Range("A1", colName + colcount).Style.Border.TopBorder = XLBorderStyleValues.Thin;
        //            worksheet.Range("A1", colName + colcount).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
        //            worksheet.Range("A1", colName + colcount).Style.Border.RightBorder = XLBorderStyleValues.Thin;
        //            worksheet.Range("A1", colName + colcount).Style.Border.LeftBorder = XLBorderStyleValues.Thin;

        //            var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, colName));
        //            wsReportNameHeaderRange.Style.Font.Bold = true;
        //            wsReportNameHeaderRange.Style.Font.FontSize = 15;
        //            wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        //            wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
        //            wsReportNameHeaderRange.Merge();
        //            wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191); ;
        //            wsReportNameHeaderRange.Value = "Reportees Register ";

        //            worksheet.Cell(2, 1).Style.Font.Bold = true;
        //            worksheet.Cell(2, 1).Value = "Group Company Name";
        //            worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();


        //            worksheet.Cell(2, 3).Style.Font.Bold = true;
        //            worksheet.Cell(2, 3).Value = "Authority";
        //            worksheet.Cell(2, 4).Value = AuthorityName;


        //            worksheet.Cell(3, 1).Style.Font.Bold = true;
        //            worksheet.Cell(3, 1).Value = "Login User";
        //            worksheet.Cell(3, 2).Value = LoginUser;
        //            worksheet.Cell(3, 3).Style.Font.Bold = true;
        //            worksheet.Cell(3, 3).Value = "Date & Time";
        //            worksheet.Cell(3, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //            worksheet.Cell(3, 4).Style.DateFormat.Format = "dd-MMM-yyyy   h:mm AM/PM";
        //            worksheet.Cell(3, 4).Value = currDateTime.ToString();
        //            worksheet.Cell(3, 5).Style.Font.Bold = true;
        //            worksheet.Cell(3, 5).Value = "Total Count";
        //            worksheet.Cell(3, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //            worksheet.Cell(3, 6).Value = model.ReporteesList.Count;

        //            var currentRow = 4;
        //            #region Header
        //            worksheet.Range("A4", colName + 4).Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
        //            worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
        //            worksheet.Cell(currentRow, 1).Value = "Sl.No";
        //            worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
        //            worksheet.Cell(currentRow, 2).Value = "ID";
        //            worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
        //            worksheet.Cell(currentRow, 3).Value = "Employee Name";
        //            worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
        //            worksheet.Cell(currentRow, 4).Value = "Designation";
        //            worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
        //            worksheet.Cell(currentRow, 5).Value = "Department";
        //            worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
        //            worksheet.Cell(currentRow, 6).Value = "Location";
        //            worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
        //            worksheet.Cell(currentRow, 7).Value = "DOJ";
        //            worksheet.Cell(currentRow, 8).Style.Font.Bold = true;
        //            worksheet.Cell(currentRow, 8).Value = "Type";
        //            worksheet.Cell(currentRow, 9).Style.Font.Bold = true;
        //            worksheet.Cell(currentRow, 9).Value = "Level";
        //            worksheet.Cell(currentRow, 10).Style.Font.Bold = true;
        //            worksheet.Cell(currentRow, 10).Value = "Authority";



        //            foreach (var item in model.ReporteesList)
        //            {
        //                currentRow++;
        //                slNo = slNo + 1;


        //                worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //                worksheet.Cell(currentRow, 1).Value = slNo;
        //                worksheet.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //                worksheet.Cell(currentRow, 2).Value = item.EmployeeHeader.EmployeeId;
        //                worksheet.Cell(currentRow, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //                worksheet.Cell(currentRow, 3).Value = item.EmployeeHeader.EmployeeName;
        //                worksheet.Cell(currentRow, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //                worksheet.Cell(currentRow, 4).Value = item.Designation;
        //                worksheet.Cell(currentRow, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //                worksheet.Cell(currentRow, 5).Value = item.Department;
        //                worksheet.Cell(currentRow, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //                worksheet.Cell(currentRow, 6).Value = item.Location;
        //                worksheet.Cell(currentRow, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //                worksheet.Cell(currentRow, 7).Value = item.DOJ;
        //                worksheet.Cell(currentRow, 7).Style.DateFormat.Format = "dd-MMM-yyyy";
        //                worksheet.Cell(currentRow, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //                worksheet.Cell(currentRow, 8).Value = item.Type;
        //                worksheet.Cell(currentRow, 9).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //                worksheet.Cell(currentRow, 9).Value = item.Level;
        //                worksheet.Cell(currentRow, 10).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //                worksheet.Cell(currentRow, 10).Value = item.AuthorityName;



        //                worksheet.Columns(1, 20).Style.Alignment.WrapText = true;
        //            }
        //            #endregion
        //            worksheet.Columns().AdjustToContents();
        //            worksheet.Rows().AdjustToContents();
        //            using (var stream = new MemoryStream())
        //            {
        //                workbook.SaveAs(stream);
        //                if (EP.NeedPassword)
        //                {
        //                    Workbook book = new Workbook();
        //                    book.LoadFromStream(stream);
        //                    //Protect Workbook
        //                    book.Protect(EP.Password,false,false);
        //                    book.SaveToStream(stream);
        //                }
        //                var content = stream.ToArray();
        //                return File(
        //                     content,
        //                  "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
        //                   "Reportees List.xlsx"
        //                    );
        //            }
        //        }

        //    }
        //    catch (UnauthorizedAccessException e)
        //    {

        //        return RedirectToAction("Login", "Account", new { Area = "" });
        //    }
        //    catch (Exception e)
        //    {
        //        return RedirectToAction("Error", "Account", new { Area = "" });
        //    }

        //}



        public async Task<IActionResult> ExportToExcel(Filter filter,int Type, int Level, int Authority, string AuthorityName,string LevelName,string TypeName, int totRecords)
        {
            try
            {

                //if (Global.GlobalFilter == null)
                //    Global.GlobalFilter = new Filter();
                //else
                //{
                //    Global.GlobalFilter.length = totRecords;
                //    Global.GlobalFilter.start = 0;
                //}
                filter.length = totRecords;
                string FilterValues = await _helpers.GetFilterValues(filter);

                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Reportees Register", "Excel");

                if (FilterValues == "")
                {
                    FilterValues = "All";
                }

                string currDateTime = await _helpers.GetCurrentDate();
                

                //Get Datset
                //   model = await _ReporteesService.GetReporteesList(Type,Level,Authority, Global.GlobalFilter);
                var dataset = await _ReporteesService.GetExcelList(Type, Level, Authority,filter);
                DataTable dt = dataset.Tables[0];


                //set EXCEL Column Heading 
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (dt.Columns[i].ColumnName == "doj" || dt.Columns[i].ColumnName == "dob" || dt.Columns[i].ColumnName == "y/s")
                        dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.ToUpper();
                    else
                        dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
                }

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Reportees Register ");
                    int count = dt.Rows.Count + 5;




                    worksheet.Range("A1:j" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:j" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:j" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:j" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:j" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 9)));
                    wsReportNameHeaderRange.Style.Font.Bold = true;
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Merge();
                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191); ;
                    wsReportNameHeaderRange.Value = "Reportees Register List";



                    worksheet.Cell(2, 1).Style.Font.Bold = true;
                    worksheet.Cell(2, 1).Value = "Group Company Name";
                    worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();


                    worksheet.Cell(2, 3).Style.Font.Bold = true;
                    worksheet.Cell(2, 3).Value = "Authority";
                    worksheet.Cell(2, 4).Value = AuthorityName;

                    worksheet.Cell(2, 5).Style.Font.Bold = true;
                    worksheet.Cell(2, 5).Value = "Level";
                    worksheet.Cell(2, 6).Value = LevelName;

                    worksheet.Cell(2, 7).Style.Font.Bold = true;
                    worksheet.Cell(2, 7).Value = "Request Type";
                    worksheet.Cell(2, 8).Value = TypeName;

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
                    worksheet.Range("B4:j4").Merge();
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
                            "Reportees Register List.xlsx"
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
