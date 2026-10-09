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
    public class CelebrityListController : Controller
    {

        private readonly ICelebrityListService _CelebrityListService;
        private readonly IHelpers _helpers;
        private readonly IDropdownService _dropdownService;
        private readonly ICommonService _commonService;
        public static class Global
        {
            public static Filter GlobalFilter;
            public static DateTime FromDate;
            public static DateTime ToDate;
            public static int allCount;
            public static int bdayCount;
            public static int weddingCount;
            public static int joiningCount;

        }
        public CelebrityListController(ICelebrityListService CelebrityListService, IHelpers Helpers, IDropdownService dropdownService,ICommonService commonService)
        {
            _CelebrityListService = CelebrityListService;
            _helpers = Helpers;
            _dropdownService = dropdownService;
            _commonService = commonService;
        }
        public async Task<IActionResult> Index(DateTime? FromDate, DateTime? ToDate,string FirstTab="All")
        {
            try
            {
                CelebrityList model = new CelebrityList();
                await fillfilterDropdown();
                Filter filter = new Filter();
                model.ToDate = DateTime.Now;
                model.FromDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                string TabIndex = "All";
                if (FromDate != null)
                    model.FromDate = Convert.ToDateTime(FromDate);
                else
                    model.FromDate = DateTime.Now;
                if (ToDate != null)
                    model.ToDate = Convert.ToDateTime(ToDate);
                else
                    model.ToDate = DateTime.Now.AddDays(7);                
                model.FirstTab = FirstTab;
                var StorageType = await _commonService.GetFileStorageType();
                ViewBag.Storage = StorageType.Data;
                return View("~/Areas/DataBank/Reports/Views/CelebrityList/Index.cshtml", model);
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
        public async Task<IActionResult> Celebrations()
        {
            try
            {
                CelebrityList model = new CelebrityList();
                await fillfilterDropdown();
                Filter filter = new Filter();
                model.ToDate = DateTime.Now;
                model.FromDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                string TabIndex = "All";
                //model = await _CelebrityListService.GetCelebrityList(model.FromDate.ToString("yyyy-MMM-dd"), model.ToDate.ToString("yyyy-MMM-dd"), TabIndex, filter);
                model.ToDate = DateTime.Now;
                model.FromDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                model.Breadscrumb = "Life Events";
                return View("~/Areas/DataBank/Reports/Views/CelebrityList/Index.cshtml", model);
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




        public  IActionResult GetCelebrityList(string TabIndex, DateTime fromdate, DateTime todate)
        {
            try
            {

                CelebrityList model = new CelebrityList();
                Filter filter = new Filter();           
             
           

                if (TabIndex == "All")
                {
                    return PartialView("~/Areas/DataBank/Reports/Views/CelebrityList/_AllCelebrityList.cshtml", model);
                }
                else if(TabIndex=="Birthday")
                {
                    return PartialView("~/Areas/DataBank/Reports/Views/CelebrityList/_BirthdayList.cshtml", model);

                }
                else if (TabIndex == "Wedding")
                {
                    return PartialView("~/Areas/DataBank/Reports/Views/CelebrityList/_weddingList.cshtml", model);

                }
                else
                {
                    return PartialView("~/Areas/DataBank/Reports/Views/CelebrityList/_JoiningList.cshtml", model);
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




        [HttpPost]
        public async Task<IActionResult> GetList(Filter filter1,string TabIndex, DateTime FROM, DateTime TO)
        {
            try
            {
                CelebrityList model = new CelebrityList();
                CelebrityList m = new CelebrityList();
                model.List = new List<CelebrityList>();

                model.List.Add(new CelebrityList());
               

                Filter filter = new Filter();
                Global.GlobalFilter = filter1;


                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }

              model = await _CelebrityListService.GetCelebrityList(filter1.F_From.ToString("yyyy-MMM-dd"), filter1.F_To.ToString("yyyy-MMM-dd"), TabIndex, filter1);
               
                Global.allCount = model.AllCount;
               Global.bdayCount = model.BirthdayCount;
               Global.weddingCount = model.WeddingCount;
               Global.joiningCount = model.JoiningCount;
                m.List = model.List;
                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.List == null)
                {

                    return Json(new
                    {
                        data = "",
                        draw = filter1.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        allCount = model.AllCount,
                        joiningcount = model.JoiningCount,
                        weddingcount = model.WeddingCount,
                        BdayCount = model.BirthdayCount,
                        recordsFiltered = totalrows
                    });
                }

                if (model.List.Count > 0)
                {
                    totalrows = m.List[0].active;
                   
                    //m.OnBoardingList.ToList<Hrms.Lite.Shared.Master.Test>().Count; ;
                }


                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = m.List[0].totalRecords;
                    //return Json(new { data = model.EmployeeProfileList, draw = filter1.draw, recordsTotal = totalrows, recordsFiltered = totalrowsafterfiltering });
                    return Json(new
                    {
                        data = model.List,
                        draw = filter1.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        recordsTotal = totalrows,
                        allCount=model.AllCount,
                        joiningcount=model.JoiningCount,
                        weddingcount=model.WeddingCount,
                        BdayCount=model.BirthdayCount,
                        recordsFiltered = totalrows//totalrowsafterfiltering
                    });
                }
                else
                {
                    // return Json(new { data = model.EmployeeProfileList, draw = filter1.draw, recordsTotal = totalrows, recordsFiltered = totalrows });
                    return Json(new
                    {
                        data = model.List,
                        draw = filter1.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        allCount = model.AllCount,
                        joiningcount = model.JoiningCount,
                        weddingcount = model.WeddingCount,
                        BdayCount = model.BirthdayCount,
                        recordsFiltered = totalrows
                    });
                }


            }
            catch (Exception ex)
            {
                return View();
            }




        }


      
        public async Task<JsonResult> GetTabCount()
        {
            CelebrityList model = new CelebrityList();
            model.AllCount = Global.allCount;
            model.BirthdayCount = Global.bdayCount;
            model.WeddingCount = Global.weddingCount;
            model.JoiningCount = Global.joiningCount;

            return Json(model);
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

        public async Task<IActionResult> ApplyFilter(string Tab, DateTime fromdate, DateTime todate, Filter filter)
        {
            try
            {

                CelebrityList model = new CelebrityList();
                Global.GlobalFilter = new Filter();
                model = await _CelebrityListService.GetCelebrityList(fromdate.ToString("yyyy-MMM-dd"), todate.ToString("yyyy-MMM-dd"), Tab, filter);
                if (Tab == "All")
                {
                    return PartialView("~/Areas/DataBank/Reports/Views/CelebrityList/_AllCelebrityList.cshtml", model);
                }
                else
                {
                    return PartialView("~/Areas/DataBank/Reports/Views/CelebrityList/_CelebrityList.cshtml", model);

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
        public async Task<IActionResult> ExportToExcel(DateTime fromdate, DateTime todate, string TabIndex,int totalrow)
        {
            try { 
           
            Filter filter = new Filter();
                if (Global.GlobalFilter == null)
                    Global.GlobalFilter = new Filter();
                else
                {
                    Global.GlobalFilter.length = totalrow;
                    Global.GlobalFilter.start = 0;
                }

              
          
          
                string FilterValues = await _helpers.GetFilterValues(Global.GlobalFilter);
            if (FilterValues == "")
            {
                FilterValues = "All";
            }
            string currDateTime = await _helpers.GetCurrentDate();
            



                //Get Dataset
                var dataset = await _CelebrityListService.GetCelebrityList_Excel(fromdate.ToString("yyyy-MMM-dd"), todate.ToString("yyyy-MMM-dd"), TabIndex, Global.GlobalFilter);
                DataTable dt = dataset.Tables[0];


                //set EXCEL Column Heading 
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                   
                        dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
                }

                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Celebrity List (" + TabIndex + ")", "Excel");
                if (TabIndex == "All")
            {
                using (var workbook = new XLWorkbook())
                {

                    var worksheet = workbook.Worksheets.Add("Celebrity List");

                    int PendingCountnum = dt.Rows.Count + 5;


                    worksheet.Range("A1:L" + PendingCountnum).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:L" + PendingCountnum).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:L" + PendingCountnum).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:L" + PendingCountnum).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:L" + PendingCountnum).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 11)));
                    wsReportNameHeaderRange.Style.Font.Bold = true;
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Merge();


                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Value = "Celebrity " + "(" + TabIndex + ") List ";
                    worksheet.Range("A5:L5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Cell(2, 1).Style.Font.Bold = true;
                    worksheet.Cell(2, 1).Value = "Group Company Name";
                    worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                    worksheet.Cell(2, 3).Style.Font.Bold = true;
                    worksheet.Cell(2, 3).Value = "From";
                    worksheet.Cell(2, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(2, 4).Value = fromdate;
                    worksheet.Cell(2, 4).Style.DateFormat.Format = "dd-MMM-yyyy";
                    worksheet.Cell(2, 5).Style.Font.Bold = true;
                    worksheet.Cell(2, 5).Value = "To";
                    worksheet.Cell(2, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(2, 6).Value = todate;
                    worksheet.Cell(2, 6).Style.DateFormat.Format = "dd-MMM-yyyy";

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
                    var filterapplied = worksheet.Range("B4:L4").Merge();

                    filterapplied.Value = FilterValues;

                    var currentRow = 5;
                        worksheet.Cell(currentRow, 1).InsertTable(dt);
                        worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                        worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                        worksheet.Range("A5:L5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                        worksheet.Range("A5:L5").Style.Font.Bold = true;


                        worksheet.Columns().AdjustToContents();
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
                            "Celebrity List " + TabIndex + " List.xlsx"
                            );
                    }
                }
            }
            else
            {

                using (var workbook = new XLWorkbook())
                {

                    var worksheet = workbook.Worksheets.Add("Celebrity List");

                    int PendingCountnum = dt.Rows.Count+ 5;


                    worksheet.Range("A1:K" + PendingCountnum).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:K" + PendingCountnum).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:K" + PendingCountnum).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:K" + PendingCountnum).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:K" + PendingCountnum).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 10)));
                    wsReportNameHeaderRange.Style.Font.Bold = true;
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Merge();


                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Value = "Celebrity List " + "(" + TabIndex + ")";
                    worksheet.Range("A5:K5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Cell(2, 1).Style.Font.Bold = true;
                    worksheet.Cell(2, 1).Value = "Group Company Name";
                    worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                    worksheet.Cell(2, 3).Style.Font.Bold = true;
                    worksheet.Cell(2, 3).Value = "From";
                    worksheet.Cell(2, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(2, 4).Value = fromdate ;
                    worksheet.Cell(2, 5).Style.Font.Bold = true;
                    worksheet.Cell(2, 5).Value = "To";
                    worksheet.Cell(2, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(2, 6).Value = todate;



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
                    var filterapplied = worksheet.Range("B4:K4").Merge();

                    filterapplied.Value = FilterValues;

                    var currentRow = 5;
                        worksheet.Cell(currentRow, 1).InsertTable(dt);
                        worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                        worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                        worksheet.Range("A5:K5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                        worksheet.Range("A5:K5").Style.Font.Bold = true;

                        worksheet.Columns().AdjustToContents();
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
                            "Celebrity List " + TabIndex + " List.xlsx"
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

    }
}

