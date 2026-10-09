using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.DataBank.Reports;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.Account;
using Hrms.Lite.Shared.DataBank.Generic.Master;
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
    public class JobTransitionReportController : Controller
    {
        private readonly IJobTransitionReportService _JobTransitionReportService;
        private readonly ICommonService _commonService;
        private readonly IDropdownService _dropdownService;
        private readonly IHelpers _helpers;
        public static class Global
        {
            public static Filter GlobalFilter;
            public static DateTime fromDate;
            public static DateTime toDate;
            public static int TranType;
            public static bool IsRefreshed = true;

        }
        public JobTransitionReportController(IJobTransitionReportService JobTransitionReportService, ICommonService commonService, IDropdownService dropdownService, IHelpers Helpers)
        {
            _JobTransitionReportService = JobTransitionReportService;
            _commonService = commonService;
            _dropdownService = dropdownService;
            _helpers = Helpers;
        }

        public async Task<IActionResult> Index()
        {
            try { 
            JobTransitionReport model = new JobTransitionReport();
               
                
                await fillfilterDropdown();
                ViewBag.JobTranType = new SelectList(await _dropdownService.GetJobTransitionTypeDropdown(), "Code", "Name");
                model.GridSettings = new List<JobTransitionReport_GridSettings>();
                model = await _JobTransitionReportService.GetGridSettingDetails();
                model.filter = new Filter();
                string firstdayofmonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("yyyy-MMM-dd");
                string lastdayofmonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1).ToString("yyyy-MMM-dd");
                var val = await _dropdownService.GetJobTransitionTypeDropdown();
                int firstVal = val[0].Code;
                //model = await _JobTransitionReportService.GetJobTransitionReport(firstdayofmonth, lastdayofmonth, firstVal, model);
                model.From = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                model.To = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1);
        
               
                return View("~/Areas/DataBank/Reports/Views/JobTransitionReport/Index.cshtml", model);
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
        public async Task<IActionResult> GetJobTransitionReportList(Filter filter1,int TransitionType,string F_From,string F_To)
        {
            try
            {
                JobTransitionReport model = new JobTransitionReport();
                JobTransitionReport m = new JobTransitionReport();
                //model.EmployeeHeader = new EmployeeHeader();

                Filter filter = new Filter();
                Global.GlobalFilter = filter1;
                model.filter = filter1;
                //model.GridSettings = new List<JobTransitionReport_GridSettings>();
                //model = await _JobTransitionReportService.GetGridSettingDetails();
                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }
                if(F_From != null && F_To != null)
                {
                    model = await _JobTransitionReportService.GetJobTransitionReport(filter1.F_From.ToString("yyyy-MMM-dd"), filter1.F_To.ToString("yyyy-MMM-dd"), TransitionType, model);

                }

                m.JobTransitionReportDetails = model.JobTransitionReportDetails;
                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.JobTransitionReportDetails == null)
                {
                    //model.RequestVsAuthorityList = new List<RequestVsAuthorityList>();
                    //model.RequestVsAuthorityList.Add(new RequestVsAuthorityList());


                    return Json(new
                    {
                        data = "",
                        draw = filter1.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows
                    });
                }




                if (model.JobTransitionReportDetails.Count > 0)
                {
                    totalrows = m.JobTransitionReportDetails[0].active;

                    //m.OnBoardingList.ToList<Hrms.Lite.Shared.Master.Test>().Count; ;
                }

                //model.AllTabCount = m.AllTabCount;
                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = m.JobTransitionReportDetails[0].totalRecords;
                    return Json(new
                    {
                        data = model.JobTransitionReportDetails,
                        draw = filter1.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows//totalrowsafterfiltering
                    });
                }
                else
                {
                    // return Json(new { data = model.EmployeeProfileList, draw = filter1.draw, recordsTotal = totalrows, recordsFiltered = totalrows });
                    return Json(new
                    {
                        data = model.JobTransitionReportDetails,
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


        public async Task<IActionResult> GetJobTransitionReport(DateTime fromdate, DateTime todate, int TranType)
        {
            try { 
            JobTransitionReport model = new JobTransitionReport();
               
                model.GridSettings = new List<JobTransitionReport_GridSettings>();
                model = await _JobTransitionReportService.GetGridSettingDetails();
                Global.fromDate = fromdate;
                Global.toDate = todate;
                Global.TranType= TranType;
                model.filter = new Filter();
                model = await _JobTransitionReportService.GetJobTransitionReport(fromdate.ToString("yyyy-MMM-dd"), todate.ToString("yyyy-MMM-dd"), TranType, model);
            return View("~/Areas/DataBank/Reports/Views/JobTransitionReport/_JobTransitionReportList.cshtml", model);
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



        public async Task<IActionResult> GetGridSetting(DateTime fromDate, DateTime toDate)
        {
            //try
            //{
            try { 
            JobTransitionReport model = new JobTransitionReport();
            model = await _JobTransitionReportService.GetGridSettingDetails();

            return PartialView("~/Areas/DataBank/Reports/Views/JobTransitionReport/_GridSettingsPopUp.cshtml", model);
                ////}
                //catch (UnauthorizedAccessException e)
                //{

                //    return RedirectToAction("Login", "Account", new { Area = "" });
                //}
                //catch (Exception e)
                //{
                //    return RedirectToAction("Error", "Account", new { Area = "" });
                //}
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
        public async Task<ActionResult> FilterColumn(DateTime fromdate, DateTime todate, int TranType, JobTransitionReport model)
        {


            try {
                model.filter = new Filter();

                var list = from li in model.GridSettings
                           where li.Applicability == true
                           select li;
                model.GridSettings = list.ToList();
                model = await _JobTransitionReportService.GetJobTransitionReport(fromdate.ToString("yyyy-MMM-dd"), todate.ToString("yyyy-MMM-dd"), TranType, model);
            return PartialView("~/Areas/DataBank/Reports/Views/JobTransitionReport/_JobTransitionReportList.cshtml", model);
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
            try
            {
                JobTransitionReport model = new JobTransitionReport();
                model.GridSettings = new List<JobTransitionReport_GridSettings>();
                model = await _JobTransitionReportService.GetGridSettingDetails();
                Global.GlobalFilter = filter;
                model.filter = filter;
                model = await _JobTransitionReportService.GetJobTransitionReport(Global.fromDate.ToString("yyyy-MMM-dd"), Global.toDate.ToString("yyyy-MMM-dd"), Global.TranType, model);
                return View("~/Areas/DataBank/Reports/Views/JobTransitionReport/_JobTransitionReportList.cshtml", model);
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
        public async Task<IActionResult> ExportToExcel(DateTime fromdate, DateTime todate, int JobTranType, string Type,int TotalRows)
        {
            try { 
            JobTransitionReport _model = new JobTransitionReport();
                _model.filter = new Filter();
                _model.GridSettings = new List<JobTransitionReport_GridSettings>();
                if (Global.GlobalFilter == null)
                    Global.GlobalFilter = new Filter();
                
                {
                    Global.GlobalFilter.length = TotalRows;
                    Global.GlobalFilter.start = 0;

                }
                _model.filter = Global.GlobalFilter;
               
           
                string FilterValues = await _helpers.GetFilterValues(Global.GlobalFilter);
            if (FilterValues == "")
            {
                FilterValues = "All";
            }
            string currDateTime = await _helpers.GetCurrentDate();
            
            var LoginUser = HttpContext.Session.GetString("LoginUser");
            var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
             EP = await _commonService.GetExcelPassword("Job Transition Report (" + Type + ")", "Excel");



                var dataset = await _JobTransitionReportService.GetJobTransitionReportDetails_Excel(fromdate.ToString("yyyy-MMM-dd"), todate.ToString("yyyy-MMM-dd"), JobTranType, _model);
                DataTable dt = dataset.Tables[0];


                //set EXCEL Column Heading 
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (dt.Columns[i].ColumnName == "pfmc" || dt.Columns[i].ColumnName == "ctc" || dt.Columns[i].ColumnName == "esimc")
                        dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.ToUpper();
                    else
                        dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
                }


                using (var workbook = new XLWorkbook())
            {

                var worksheet = workbook.Worksheets.Add("JobTransition Report List");

                  
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
                wsReportNameHeaderRange.Style.Font.FontSize = 15;
                wsReportNameHeaderRange.Value = "JobTransition Report List ";
                //worksheet.Range("A5:J5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
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
                worksheet.Cell(2, 7).Value = "Type";
                    worksheet.Cell(2, 7).Style.Font.Bold = true;
                    worksheet.Cell(2, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                worksheet.Cell(2, 8).Value = Type;



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
                var filterapplied = worksheet.Range("B4", colName + 4).Merge();

                filterapplied.Value = FilterValues;

                var currentRow = 5;

                    worksheet.Cell(currentRow, 1).InsertTable(dt);
                    worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                    worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                    worksheet.Range("A5:"+colName+"5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Range("A5:"+ colName+"5").Style.Font.Bold = true;


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
                        "JobTransition Report List.xlsx"
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
