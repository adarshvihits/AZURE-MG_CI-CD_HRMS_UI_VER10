using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.DataBank.DTO;
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
using System.Data;
using Newtonsoft.Json;
using Hrms.Lite.Shared.Account;

namespace Hrms.Lite.UI.Areas.DataBank.Controllers
{
    [Area("DataBank")]
    [SessionAuthorize]
    public class IndustryBenchmarkSalaryController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly IIndustryBenchmarkSalaryService _industryBenchmarkSalaryService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        public static class Global
        {


            public static Filter GlobalFilter;
            public static int allCount;
            public static int LessCount;
            public static int GreaterCount;
            public static int EqualCount;

        }
        public IndustryBenchmarkSalaryController(IIndustryBenchmarkSalaryService IndustryBenchmarkSalaryService, IDropdownService dropdownService, ICommonService commonService,IHelpers helpers)
        {
            _industryBenchmarkSalaryService = IndustryBenchmarkSalaryService;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = helpers;
        }
        public async Task<IActionResult> Index()
        {
            try { 
            IndustryBenchmarkSalary m1 = new IndustryBenchmarkSalary();
            Filter filter = new Filter();
            filter.length = 10;
            m1 = await _industryBenchmarkSalaryService.GetIndustryBenchmarkSalaryList("All", filter);
            IndustryBenchmarkSalary model = new IndustryBenchmarkSalary();
            model.Filter = new Filter();
            await fillfilterDropdown();
            return View(model);
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
            //ViewBag.EmploymentStatus = new SelectList(await _dropdownService.GetEmploymentStatusDropdown(), "Code", "Name");
            ViewBag.WorkLocation = new SelectList(await _dropdownService.GetWorkLocationDropdown(), "Code", "Name");
            ViewBag.WageGrade = new SelectList(await _dropdownService.GetWageGradeDropdown(), "Code", "Name");

        }

        //public async Task<IActionResult> GetIndustryBenchmarkSalaryList(string TabIndex)
        //{

        //    IndustryBenchmarkSalary model = new IndustryBenchmarkSalary();
        //    model = await _industryBenchmarkSalaryService.GetIndustryBenchmarkSalaryList(TabIndex);
        //    return PartialView("_IndustryBenchmarkSalaryList", model);
        //}
        public async Task<JsonResult> GetTabCount()
        {
            IndustryBenchmarkSalary model = new IndustryBenchmarkSalary();
            model.AllCount = Global.allCount;
            model.GreaterBenchmarkSalaryCount = Global.GreaterCount;
            model.LessBenchmarkSalaryCount = Global.LessCount;
            model.SameBenchmarkSalaryCount = Global.EqualCount;

            return Json(model);
        }

        [HttpPost]
        public async Task<IActionResult> GetList(Filter filter1, string TabIndex)
        {
            try
            {
                IndustryBenchmarkSalary model = new IndustryBenchmarkSalary();
                IndustryBenchmarkSalary m = new IndustryBenchmarkSalary();
                
                Filter filter = new Filter();
                Global.GlobalFilter = filter1;


                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }
                model = await _industryBenchmarkSalaryService.GetIndustryBenchmarkSalaryList(TabIndex, filter1);
                Global.allCount = model.AllCount;
                Global.GreaterCount = model.GreaterBenchmarkSalaryCount;
                 Global.LessCount = model.LessBenchmarkSalaryCount;
                 Global.EqualCount = model.SameBenchmarkSalaryCount;

                m.IndustryBenchmarkSalaryList = model.IndustryBenchmarkSalaryList;
                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.IndustryBenchmarkSalaryList == null)
                {

                    return Json(new
                    {
                        data = "",
                        draw = filter1.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        allCount = model.AllCount,
                        greaterCount = model.GreaterBenchmarkSalaryCount,
                        lessCount = model.LessBenchmarkSalaryCount,
                        SameCount = model.SameBenchmarkSalaryCount,
                        recordsFiltered = totalrows
                    });
                }

                if (model.IndustryBenchmarkSalaryList.Count > 0)
                {
                    totalrows = m.IndustryBenchmarkSalaryList[0].active;

                    //m.OnBoardingList.ToList<Hrms.Lite.Shared.Master.Test>().Count; ;
                }

                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = m.IndustryBenchmarkSalaryList[0].totalRecords;
                    
                    return Json(new
                    {
                        data = model.IndustryBenchmarkSalaryList,
                        draw = filter1.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        recordsTotal = totalrows,
                        allCount=model.AllCount,
                        greaterCount=model.GreaterBenchmarkSalaryCount,
                        lessCount=model.LessBenchmarkSalaryCount,
                        SameCount=model.SameBenchmarkSalaryCount,
                        recordsFiltered = totalrows//totalrowsafterfiltering
                    });
                }
                else
                {
                    return Json(new
                    {
                        data = model.IndustryBenchmarkSalaryList,
                        draw = filter1.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        allCount = model.AllCount,
                        greaterCount = model.GreaterBenchmarkSalaryCount,
                        lessCount = model.LessBenchmarkSalaryCount,
                        SameCount = model.SameBenchmarkSalaryCount,
                        recordsFiltered = totalrows
                    });
                }


            }
            catch (Exception ex)
            {
                return View();
            }



        }

        [HttpPost]
        public async Task<IActionResult> GetBenchmarkList(string TabIndex)
        {
            IndustryBenchmarkSalary model = new IndustryBenchmarkSalary();
            //Global.GlobalFilter = filter;
            //model = await _industryBenchmarkSalaryService.GetIndustryBenchmarkSalaryList(TabIndex, filter);
            if(TabIndex== "All")
            {
                return PartialView("_IndustryBenchmarkSalaryList", model);
            }
            else if(TabIndex == "GREATER")
            {
                return PartialView("_GreaterBenchMarkSalaryList", model);
            }
            else if(TabIndex == "LESS")
            {
                return PartialView("_LessBenchMarkSalaryList", model);
            }
            else
            {
                return PartialView("_SameAsBenchMarkSalaryList", model);
            }
        }

        public async Task<IActionResult> AddBenchmarkSalary()
        {
            IndustryBenchmarkSalary model = await _industryBenchmarkSalaryService.GetBenchmarkSalaryDetails();
            return PartialView("_Create",model);
        }
        [HttpPost]
        public async Task<IActionResult> Create(IndustryBenchmarkSalary model)
        {
            var response = await _industryBenchmarkSalaryService.BenchmarkSalarySave(model);
            //if (response.Success == true)
            //{
            //    TempData["Message"] = "Saved Successfully";
            //    return RedirectToAction("Index", "IndustryBenchmarkSalary");
            //}
            //return PartialView("_Create", model);

         

            return Json(response);
        }

        public async Task<IActionResult> ExportToExcel(string TabIndex, string TabName, int TotalRows)
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
                EP = await _commonService.GetExcelPassword("Industry Benchmark Salary (" + TabName + ")", "Excel");

                if (FilterValues == "")
                {
                    FilterValues = "All";
                }

                string currDateTime = await _helpers.GetCurrentDate();
                

                //Get Datset
                var dataset = await _industryBenchmarkSalaryService.GetIndustryBenchmarkSalaryList_Excel(TabIndex, Global.GlobalFilter);
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
                    var worksheet = workbook.Worksheets.Add("Industry Benchmark Salary");
                    int count = dt.Rows.Count + 5;


                    worksheet.Range("A1:J" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:J" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:J" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:J" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:J" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 9)));
                    wsReportNameHeaderRange.Style.Font.Bold = true;
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Merge();
                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191); ;
                    wsReportNameHeaderRange.Value = "Industry Benchmark Salary (" + TabName + ")";

                    worksheet.Cell(2, 1).Style.Font.Bold = true;
                    worksheet.Cell(2, 1).Value = "Group Company Name";
                    worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
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
                    worksheet.Range("B4:J4").Merge();
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
                            book.Protect(EP.Password,false,false);
                            book.SaveToStream(stream);
                        }
                        var content = stream.ToArray();
                        return File(
                            content,
                            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                            "Industry Benchmark Salary " + TabName + " List.xlsx"
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












        //public async Task<IActionResult> ExportToExcel(string TabIndex, string TabName, int TotalRows)
        //{
        //    IndustryBenchmarkSalary model = new IndustryBenchmarkSalary();
        //    //Filter filter = new Filter();
        //    var LoginUser = HttpContext.Session.GetString("LoginUser");
        //    var CompanyName = HttpContext.Session.GetString("CompanyName");
        //    ExcelPassword EP = new ExcelPassword();
        //    EP = await _commonService.GetExcelPassword("Industry Benchmark Salary (" + TabName + ")", "Excel");
        //    int slNo = 0;
        //    if (Global.GlobalFilter == null)
        //        Global.GlobalFilter = new Filter();
        //    else
        //    {
        //        Global.GlobalFilter.length = TotalRows;
        //        Global.GlobalFilter.start = 0;
        //    }
        //    string FilterValues = await _helpers.GetFilterValues(Global.GlobalFilter);
        //    model = await _industryBenchmarkSalaryService.GetIndustryBenchmarkSalaryList(TabIndex, Global.GlobalFilter);
        //    if (FilterValues == "")
        //    {
        //        FilterValues = "All";
        //    }
        //    string currDateTime = await _helpers.GetCurrentDate();
        //    



        //    using (var workbook = new XLWorkbook())
        //    {
        //        var worksheet = workbook.Worksheets.Add("Industry Benchmark Salary");
        //        int count = model.IndustryBenchmarkSalaryList.Count + 5;


        //        worksheet.Range("A1:J" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
        //        worksheet.Range("A1:J" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
        //        worksheet.Range("A1:J" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
        //        worksheet.Range("A1:J" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
        //        worksheet.Range("A1:J" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;

        //        var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 9)));
        //        wsReportNameHeaderRange.Style.Font.Bold = true;
        //        wsReportNameHeaderRange.Style.Font.FontSize = 15;
        //        wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        //        wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
        //        wsReportNameHeaderRange.Merge();
        //        wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191); ;
        //        wsReportNameHeaderRange.Value = "Industry Benchmark Salary(" + TabName + ")";

        //        worksheet.Cell(2, 1).Style.Font.Bold = true;
        //        worksheet.Cell(2, 1).Value = "Company Name";
        //        worksheet.Cell(2, 2).Value = CompanyName.ToUpper();
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
        //        worksheet.Cell(3, 6).Value = model.IndustryBenchmarkSalaryList.Count;
        //        worksheet.Cell(4, 1).Style.Font.Bold = true;
        //        worksheet.Cell(4, 1).Value = "Filter Applied";
        //        worksheet.Range("B4:J4").Merge();
        //        worksheet.Cell("B4").Value = FilterValues;
        //        var currentRow = 5;

        //        #region Header
        //        worksheet.Cell(currentRow, 1).Value = "Sl.No";
        //        worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
        //        worksheet.Cell(currentRow, 2).Value = "ID";
        //        worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
        //        worksheet.Cell(currentRow, 3).Value = "EmployeeName";
        //        worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
        //        worksheet.Cell(currentRow, 4).Value = "Designation";
        //        worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
        //        worksheet.Cell(currentRow, 5).Value = "Department";
        //        worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
        //        worksheet.Cell(currentRow, 6).Value = "Location";
        //        worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
        //        worksheet.Cell(currentRow, 7).Value = "Wage Grade";
        //        worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
        //        worksheet.Cell(currentRow, 8).Value = "Current GP";
        //        worksheet.Cell(currentRow, 8).Style.Font.Bold = true;
        //        worksheet.Cell(currentRow, 9).Value = "Benchmark GP";
        //        worksheet.Cell(currentRow, 9).Style.Font.Bold = true;
        //        worksheet.Cell(currentRow, 10).Value = "Variation";
        //        worksheet.Cell(currentRow, 10).Style.Font.Bold = true;

        //        #endregion

        //        #region Body
        //        foreach (var item in model.IndustryBenchmarkSalaryList)
        //        {
        //            currentRow++;
        //            slNo = slNo + 1;
        //            worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //            worksheet.Range("A5:J5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
        //            worksheet.Cell(currentRow, 1).Value = slNo;
        //            worksheet.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //            worksheet.Cell(currentRow, 2).Value = item.EmployeeHeader.EmployeeId;
        //            worksheet.Cell(currentRow, 3).Value = item.EmployeeHeader.EmployeeName;
        //            worksheet.Cell(currentRow, 4).Value = item.Designation.Name;
        //            worksheet.Cell(currentRow, 5).Value = item.Department.Name;
        //            worksheet.Cell(currentRow, 6).Value = item.Location.Name;
        //            worksheet.Cell(currentRow, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //            worksheet.Cell(currentRow, 7).Value = item.WageGrade;
        //            worksheet.Cell(currentRow, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //            worksheet.Cell(currentRow, 8).Value = item.CurrentGP;
        //            worksheet.Cell(currentRow, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //            worksheet.Cell(currentRow, 9).Value = item.BenchmarkGP;
        //            worksheet.Cell(currentRow, 9).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //            worksheet.Cell(currentRow, 10).Value = item.Variation;
        //            worksheet.Cell(currentRow, 10).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //            worksheet.Columns(1, 10).Style.Alignment.WrapText = true;
        //            worksheet.Rows().Style.Alignment.WrapText = true;
        //        }
        //        #endregion
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
        //                book.Protect(EP.Password,false,false);
        //                book.SaveToStream(stream);
        //            }
        //            var content = stream.ToArray();
        //            return File(
        //                content,
        //                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
        //                "Industry Benchmark Salary.xlsx"
        //                );
        //        }
        //    }

        //    //}
        //}
    }
}
