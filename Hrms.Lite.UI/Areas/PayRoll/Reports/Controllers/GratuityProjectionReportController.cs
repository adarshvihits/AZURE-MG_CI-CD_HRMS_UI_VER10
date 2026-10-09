using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.PayRoll.Reports;
using Hrms.Lite.Shared.Account;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Spire.Xls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.PayRoll.Reports.Controllers
{
    [Area("PayRoll")]
    [SessionAuthorize]
    public class GratuityProjectionReportController : Controller
    {
        private readonly IGratuityProjectionReportService _GratuityProjectionReportService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        private readonly IDropdownService _dropdownService;
        public static class Global
        {
            public static Filter GlobalFilter;
            public static int type;


        }
        public GratuityProjectionReportController(IGratuityProjectionReportService GratuityProjectionReportService, ICommonService commonService, IHelpers Helpers, IDropdownService dropdownService)
        {
            _GratuityProjectionReportService = GratuityProjectionReportService;
            _commonService = commonService;
            _helpers = Helpers;
            _dropdownService = dropdownService;
        }


        public async Task<IActionResult> Index()
         {
            try 
            { 
            GratuitySettings model = new GratuitySettings();
            model.EmployeeStatus = 0;
            Filter filter = new Filter();
            filter.length = 10;
            model = await _GratuityProjectionReportService.GratuityProjectionList(0, filter);
            await fillfilterDropdown();
            if (Global.GlobalFilter == null)
                Global.GlobalFilter = new Filter();
            //model = await _GratuityProjectionReportService.GratuityProjectionList(0,Global.GlobalFilter);
            return View("~/Areas/PayRoll/Reports/Views/GratuityProjectionReport/Index.cshtml",model);
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
        public async Task<IActionResult> GetList(Filter filter1, int Type)
        {
            try
            {
                GratuitySettings model = new GratuitySettings();
                GratuitySettings m = new GratuitySettings();
                Global.type = Type;
                Filter filter = new Filter();
                Global.GlobalFilter = filter1;

                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }
                model = await _GratuityProjectionReportService.GratuityProjectionList(Type, filter1);


                m.GratuityProjection = model.GratuityProjection;
                int totalrows = 0;
                int totalrowsafterfiltering = 0;



                if (model == null || model.GratuityProjection == null || model.GratuityProjection.Count() == 0)
                {
                    m.GratuityProjection = new List<GratuityProjection>();
                    model.GratuityProjection = new List<GratuityProjection>();
                }
                else
                {
                    m.GratuityProjection = model.GratuityProjection;
                }



                if (model.GratuityProjection == null || model.GratuityProjection.Count() == 0)
                {

                    return Json(new
                    {
                        data = "",
                        draw = filter1.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        tab = Type,
                        recordsFiltered = totalrows
                    });
                }




                if (model.GratuityProjection.Count > 0)
                {
                    totalrows = m.GratuityProjection[0].active;

                }


                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = m.GratuityProjection[0].totalRecords;
                    return Json(new
                    {
                        data = model.GratuityProjection,
                        draw = filter1.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        recordsTotal = totalrows,
                        tab= Type,
                        recordsFiltered = totalrows//totalrowsafterfiltering
                    });
                }
                else
                {
                    return Json(new
                    {
                        data = model.GratuityProjection,
                        draw = filter1.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        tab = Type,
                        recordsFiltered = totalrows
                    });
                }


            }
            catch (Exception ex)
            {
                return View();
            }


        }



        public async Task<IActionResult> GetGratuityProjectionReportList(int Type)
        {
            try
            {
                GratuitySettings model = new GratuitySettings();
                Global.GlobalFilter = new Filter();
                Global.type = Type;
                if(Type==0)
                {
                    return PartialView("~/Areas/PayRoll/Reports/Views/GratuityProjectionReport/_ActiveList.cshtml", model);

                }
                else
                {
                    return PartialView("~/Areas/PayRoll/Reports/Views/GratuityProjectionReport/_ResignedList.cshtml", model);

                }
                //model = await _GratuityProjectionReportService.GratuityProjectionList(Type, Global.GlobalFilter);
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





        public async Task<IActionResult> ExportToExcel(int TotalRec)
        {
            try
            {
               

                 var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;

                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Gratuity Projection Report", "Excel");
            if (Global.GlobalFilter == null)
                    Global.GlobalFilter = new Filter();
                else
                {
                    Global.GlobalFilter.length = TotalRec;
                    Global.GlobalFilter.start = 0;
                }
                string FilterValues = await _helpers.GetFilterValues(Global.GlobalFilter);
                if (FilterValues == "")
                {
                    FilterValues = "All";
                }             
               
              
                string currDateTime = await _helpers.GetCurrentDate();
                

                //Get Dataset
                var dataset = await _GratuityProjectionReportService.GetGratuityProjection_Excel(Global.type, Global.GlobalFilter);
                DataTable dt = dataset.Tables[0];

                dataset.Tables[0].Rows.RemoveAt(0);
                if ((dataset.Tables[0]?.Rows?.Count ?? 0) > 0)
                {
                    dt = dataset.Tables[0];
                }

                //set EXCEL Column Heading 
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (dt.Columns[i].ColumnName == "doj" || dt.Columns[i].ColumnName == "dob" || dt.Columns[i].ColumnName == "y/s"|| dt.Columns[i].ColumnName == "lwd" || dt.Columns[i].ColumnName == "doc" || dt.Columns[i].ColumnName == "lds")
                        dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.ToUpper();
                    else
                        dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
                }
                if (Global.type == 0)
                {
                    using (var workbook = new XLWorkbook())
                    {

                        var worksheet = workbook.Worksheets.Add("Gratuity Projection Report List");
                        int count = dt.Rows.Count + 5;
                        string columnName = "";
                        int Columncount = dt.Columns.Count;

                        while (Columncount > 0)
                        {
                            int modulo = (Columncount - 1) % 26;
                            columnName = Convert.ToChar('A' + modulo) + columnName;
                            Columncount = (Columncount - modulo) / 26;
                        }

                        string colName = columnName;
                        worksheet.Range("A1" , colName + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
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
                        wsReportNameHeaderRange.Value = "Gratuity Projection Report";

                        worksheet.Cell(2, 1).Style.Font.Bold = true;
                        worksheet.Cell(2, 1).Value = "Group Company Name";
                        worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                        worksheet.Cell(2, 3).Style.Font.Bold = true;
                        worksheet.Cell(2, 3).Value = "Employee Type";
                      
                            worksheet.Cell(2, 4).Value = "Active Employees";
                      


                        worksheet.Cell(3, 1).Style.Font.Bold = true;
                        worksheet.Cell(3, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
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
                        var filterapplied = worksheet.Range("B4",colName+4).Merge();

                        filterapplied.Value = FilterValues;


                        var currentRow = 5;
                        worksheet.Cell(currentRow, 1).InsertTable(dt);
                        worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                        worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                        worksheet.Range("A5",colName+5).Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                        worksheet.Range("A5",colName+5).Style.Font.Bold = true;


                        worksheet.Columns().AdjustToContents();
                        worksheet.Rows().AdjustToContents();
                        //worksheet.Protect("123");
                        using (var stream = new System.IO.MemoryStream())
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
                                "Gratuity Projection Report List.xlsx"
                                );
                        }
                    }
                }
                else
                {
                    using (var workbook = new XLWorkbook())
                    {

                        var worksheet = workbook.Worksheets.Add("Gratuity Projection Report List");
                        int count = dt.Rows.Count + 5;
                        string columnName = "";
                        int Columncount = dt.Columns.Count;

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
                        wsReportNameHeaderRange.Value = "Gratuity Projection Report";

                        worksheet.Cell(2, 1).Style.Font.Bold = true;
                        worksheet.Cell(2, 1).Value = "Group Company Name";
                        worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                        worksheet.Cell(2, 3).Style.Font.Bold = true;
                        worksheet.Cell(2, 3).Value = "Employee Type";
                       
                            worksheet.Cell(2, 4).Value = "Resigned Employees";
                        


                        worksheet.Cell(3, 1).Style.Font.Bold = true;
                        worksheet.Cell(3, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
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
                        var filterapplied = worksheet.Range("B4", colName+4).Merge();

                        filterapplied.Value = FilterValues;


                        var currentRow = 5;
                        worksheet.Cell(currentRow, 1).InsertTable(dt);
                        worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                        worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                        worksheet.Range("A5", colName+5).Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                        worksheet.Range("A5", colName+5).Style.Font.Bold = true;


                        worksheet.Columns().AdjustToContents();
                        worksheet.Rows().AdjustToContents();
                        //worksheet.Protect("123");
                        using (var stream = new System.IO.MemoryStream())
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
                                "Gratuity Projection Report List.xlsx"
                                );
                        }
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

        public async Task<IActionResult> ApplyFilter(Filter filter)
        {
            try
            {

                GratuitySettings model = new GratuitySettings();
                Global.GlobalFilter = new Filter();
                model = await _GratuityProjectionReportService.GratuityProjectionList(Global.type,filter);
                return PartialView("~/Areas/PayRoll/Reports/Views/GratuityProjectionReport/_GratuityProjectionReportList.cshtml", model);
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
