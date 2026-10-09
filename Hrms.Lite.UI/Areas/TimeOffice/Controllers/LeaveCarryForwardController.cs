using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.TimeOffice;
using Hrms.Lite.Shared.Account;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using Hrms.Lite.Shared.TimeOffice;
using Hrms.Lite.Shared.TimeOffice.Generic.Master;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Spire.Xls;

namespace Hrms.Lite.UI.Areas.TimeOffice.Controllers
{
    [Area("TimeOffice")]
    [SessionAuthorize]
    public class LeaveCarryForwardController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly ILeaveCarryForwardService _leaveCarryForwardService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;

        public static class Global
        {
            public static int LeavePeriodFrom;
            public static int LeavePeriodTo;
            public static int LeaveGroup;
            public static int Leave;
            public static int Month;


        }

        public LeaveCarryForwardController(IDropdownService dropdownService, ICommonService commonService, ILeaveCarryForwardService leaveCarryForwardService,IHelpers helpers)
        {
            _leaveCarryForwardService = leaveCarryForwardService;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = helpers;
        }

        #region  LEAVE CARRYFORWARD ENTRY
        public async Task<IActionResult> Index()
        {
            try { 
            LeaveCarryForward model = new LeaveCarryForward();
                LeaveCarryForward m1 = new LeaveCarryForward();
                Filter filter = new Filter();
                filter.length = 10;
                m1 = await _leaveCarryForwardService.LeaveCarryForwardHistoryList(0, 0, 0, 0, filter);
                model.LeaveGroup = new LeaveGroup();
            model.Leave = new LeaveCF();
            model.LeavePeriodFrom = new LeavePeriod();
            model.LeavePeriodTo = new LeavePeriod();
            model.Month = new Month();            

            ViewBag.LeaveGroup = new SelectList(await _dropdownService.GetLeaveGroupFilterDropdown(), "Code", "Name");
            ViewBag.Leave = new SelectList(await _dropdownService.GetLeaveCF_LeaveDropdown(0), "Code", "Name");
             ViewBag.Month = new SelectList(await _dropdownService.GetMonthly_AttendanceProcessing_MonthDropdown(0), "Code", "Name");
            ViewBag.LeavePeriodFrom = new SelectList(await _dropdownService.GetLeavePeriodFilterDropdown(), "Code", "Name");
            ViewBag.EntryType = new SelectList(await _dropdownService.GetLeaveCFEntryTypeDropDown(), "Code", "Name");
                model.EntryType = 1;
            await fillfilterDropdown();
            await fillCFLeavePeriodToDropdown(0);

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
        [HttpGet]
        public async Task<JsonResult> fillLeaveCombo(int LGroup)
        {
            var details = await _dropdownService.GetLeaveCF_LeaveDropdown(LGroup);
            return Json(details);
        }
        [HttpGet]
        public async Task<JsonResult> fillLeavePeriodToDropdown(int? LeavePeriodFrom)
        {
            var details = await _dropdownService.GetLeavePeriodToFilterDropdown(LeavePeriodFrom.Value);
            return Json(details);
        }
        public async Task fillCFLeavePeriodToDropdown(int? LeavePeriodFrom)
        {
            ViewBag.LeavePeriodTo = new SelectList(await _dropdownService.GetLeavePeriodToFilterDropdown(LeavePeriodFrom.Value), "Code", "Name");
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



        public IActionResult GetCarryForwardHeading(int Type)
        {
            try
            {
                LeaveCarryForward model = new LeaveCarryForward();
                if (Type == 1)
                {
                    return PartialView("_LeaveCfCreateList", model);

                }
                else

                {
                    return PartialView("_LeaveCfEditList", model);

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
        public async Task<IActionResult> GetLeaveCFList(Filter filter1, int LeavePeriodFrom, int LeavePeriodTo, int LeaveGroup, int Leave,int Month,int  EntryType,string Form)
        {
            try
            {
                LeaveCarryForward model = new LeaveCarryForward();
                LeaveCarryForward m = new LeaveCarryForward();
             
                Filter filter = new Filter();
               

                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }
                if (Form == "History")
                {
                    if (LeavePeriodFrom != 0 && LeavePeriodTo != 0 && LeaveGroup != 0 && Leave != 0 && Month != 0)
                        model = await _leaveCarryForwardService.LeaveCarryForwardHistoryList(LeavePeriodFrom, LeavePeriodTo, LeaveGroup, Leave, filter1);
                }
                else
                { 
                     if (LeavePeriodFrom != 0 && LeavePeriodTo != 0 && LeaveGroup != 0 && Leave != 0 && Month != 0 && EntryType != 0)
                    model = await _leaveCarryForwardService.LeaveCarryForwardList(LeavePeriodFrom, LeavePeriodTo, LeaveGroup, Leave, Month, EntryType, filter1);

                }
               
                m.LeaveCarryForwardList = model.LeaveCarryForwardList;
                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.LeaveCarryForwardList == null || model.LeaveCarryForwardList.Count() == 0)
                {

                    return Json(new
                    {
                        data = "",
                        draw = filter1.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows
                    });
                }

                if (model.LeaveCarryForwardList.Count > 0)
                {
                    totalrows = m.LeaveCarryForwardList[0].active;

                   
                }

                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = m.LeaveCarryForwardList[0].totalRecords;

                    return Json(new
                    {
                        data = model.LeaveCarryForwardList,
                        draw = filter1.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows//totalrowsafterfiltering
                    });
                }
                else
                {
                    return Json(new
                    {
                        data = model.LeaveCarryForwardList,
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




        public async Task<IActionResult> GetBatchUpdateDetails(int LeavePeriodFrom,int LeavePeriodTo, int LeaveGroup, int Leave,int Month)
        {
            try { 
            LeaveCarryForward model = new LeaveCarryForward();
            model = await _leaveCarryForwardService.GetBatchUpDateDetails(LeavePeriodFrom, LeavePeriodTo, LeaveGroup, Leave, Month);
            return PartialView("_BatchUpdate", model);
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
        public async Task<IActionResult> GetEditDetails(Guid EmployeeGI,int LeavePeriodFrom, int LeavePeriodTo, int LeaveGroup, int Leave, int Month)
        {
            try { 
            LeaveCarryForward model = new LeaveCarryForward();
            model = await _leaveCarryForwardService.GetEditDetails(EmployeeGI, LeavePeriodFrom, LeavePeriodTo,LeaveGroup, Leave, Month);
            model.EmployeeHeader = await _commonService.GetEmployeeHeader(EmployeeGI);
            model.EmployeeGI = EmployeeGI;
            return PartialView("_Edit", model);
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
        public async Task<IActionResult> LeaveCarryForwardSave(Filter filter, int LeaveGroup ,int Leave, int  LeavePeriodFrom, int LeavePeriodTo,int  Month)
        {
            try {
                LeaveCarryForward model = new LeaveCarryForward();
                model.LeaveCFDetails = new LeaveCFDetails();
                model.LeaveCFDetails.Month = new Month();
                model.LeaveCFDetails.Leave = new Leave();
                model.LeaveCFDetails.LeaveGroup = new LeaveGroup();
                model.LeaveCFDetails.LeavePeriodFrom = new LeavePeriod();
                model.LeaveCFDetails.LeavePeriodTo = new LeavePeriod();


                model.LeaveCFDetails.Month.Code = Month;
                model.LeaveCFDetails.Leave.Code = Leave;
                model.LeaveCFDetails.LeaveGroup.Code = LeaveGroup;
                model.LeaveCFDetails.LeavePeriodFrom.Code =LeavePeriodFrom;
                model.LeaveCFDetails.LeavePeriodTo.Code = LeavePeriodTo;

                model.Filter = filter;
                //var response = await _leaveCarryForwardService.LeaveCFSave(model);
                var response = await _leaveCarryForwardService.LeaveCFBatchUpdate(model);
                return Json(response);
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
        public async Task<IActionResult> Edit(LeaveCarryForward model)
        {
            try { 
            Guid EmployeeGI = model.EmployeeGI;
            var response = await _leaveCarryForwardService.EmployeeWiseCFSave(model);
            return Json(response);
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
        public async Task<IActionResult> BatchUpdate(LeaveCarryForward model)
        {
            try { 
            Guid EmployeeGI = model.EmployeeGI;
 
            var response = await _leaveCarryForwardService.LeaveCFBatchUpdate(model);
            return Json(response);
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
        public async Task<IActionResult> ExportToExcel(Filter Filter,int LeaveGroup, int Leave, int LeavePeriodFrom, int LeavePeriodTo, int Month, int EntryType,string LPFrom,string LPTo,string LName,string LGName,string MName,int TotalRow)
        {
            try
            {

                
                    Filter.length = TotalRow;
                    Filter.start = 0;
                
                string FilterValues = await _helpers.GetFilterValues(Filter);

                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Leave Carry Forward", "Excel");

                if (FilterValues == "")
                {
                    FilterValues = "All";
                }

                string currDateTime = await _helpers.GetCurrentDate();
                

                //Get Datset
                var dataset = await _leaveCarryForwardService.LeaveCarryForwardList_Excel(LeavePeriodFrom, LeavePeriodTo, LeaveGroup, Leave, Month, Filter, EntryType);
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
                    var worksheet = workbook.Worksheets.Add("Leave Carry Forward");
                    int count = dt.Rows.Count + 5;


                    worksheet.Range("A1:I" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:I" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:I" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:I" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:I" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 8)));
                    wsReportNameHeaderRange.Style.Font.Bold = true;
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Merge();
                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191); ;
                    wsReportNameHeaderRange.Value = "Leave Carry Forward";

                    worksheet.Cell(2, 1).Style.Font.Bold = true;
                    worksheet.Cell(2, 1).Value = "Group Company Name";
                    worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                    worksheet.Cell(2, 3).Style.Font.Bold = true;
                    worksheet.Cell(2, 3).Value = "Leave Group";
                    worksheet.Cell(2, 4).Value = LGName;

                    worksheet.Cell(2, 5).Style.Font.Bold = true;
                    worksheet.Cell(2, 5).Value = "Leave";
                    worksheet.Cell(2, 6).Value = LName;

                    worksheet.Cell(3, 1).Style.Font.Bold = true;
                    worksheet.Cell(3, 1).Value = "Leave Period From";
                    worksheet.Cell(3, 2).Value = LPFrom;
                    worksheet.Cell(3, 3).Style.Font.Bold = true;
                    worksheet.Cell(3, 3).Value = "Leave Period To";
                    worksheet.Cell(3, 4).Value = LPTo;
                    worksheet.Cell(3, 5).Style.Font.Bold = true;
                    worksheet.Cell(3, 5).Value = "Month";
                    worksheet.Cell(3, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(3, 6).Style.DateFormat.Format = "MMM-yyyy";
                    worksheet.Cell(3, 6).Value = MName;

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
                    worksheet.Range("B5:I5").Merge();
                    worksheet.Cell("B5").Value = FilterValues;



                    var currentRow = 6;

                    worksheet.Cell(currentRow, 1).InsertTable(dt);
                    worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                    worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                    worksheet.Range("A5:I6").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Range("A5:I6").Style.Font.Bold = true;


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
                            "Leave Carry Forward List.xlsx"
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


        #endregion
        #region  LEAVE CARRYFORWARD HISTORY
        public async Task<IActionResult> History()
        {
            try
            {
                LeaveCarryForward model = new LeaveCarryForward();               

                ViewBag.LeaveGroup = new SelectList(await _dropdownService.GetLeaveGroupFilterDropdown(), "Code", "Name");
                ViewBag.Leave = new SelectList(await _dropdownService.GetLeaveCF_LeaveDropdown(0), "Code", "Name");              
                ViewBag.LeavePeriodFrom = new SelectList(await _dropdownService.GetLeavePeriodFilterDropdown(), "Code", "Name");
                await fillCFLeavePeriodToDropdown(0);

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
        public async Task<IActionResult> HistoryExportToExcel(int LeaveGroup, int Leave, int LeavePeriodFrom, int LeavePeriodTo, string LPFrom, string LPTo, string LName,string LGName,  int TotalRow)
        {
            try
            {
                Filter filter= new Filter();

                filter.length = TotalRow;
                filter.start = 0;
               
                string FilterValues = await _helpers.GetFilterValues(filter);

                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Leave Carry Forward History", "Excel");

                if (FilterValues == "")
                {
                    FilterValues = "All";
                }

                string currDateTime = await _helpers.GetCurrentDate();
                

                //Get Datset
                var dataset = await _leaveCarryForwardService.LeaveCarryForwardHistoryList_Excel(LeavePeriodFrom, LeavePeriodTo, LeaveGroup, Leave,filter);
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
                    var worksheet = workbook.Worksheets.Add("Leave Carry Forward History");
                    int count = dt.Rows.Count + 5;


                    worksheet.Range("A1:I" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:I" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:I" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:I" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:I" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 11)));
                    wsReportNameHeaderRange.Style.Font.Bold = true;
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Merge();
                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191); ;
                    wsReportNameHeaderRange.Value = "Leave Carry Forward History";

                    worksheet.Cell(2, 1).Style.Font.Bold = true;
                    worksheet.Cell(2, 1).Value = "Group Company Name";
                    worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();


                    worksheet.Cell(2, 3).Style.Font.Bold = true;
                    worksheet.Cell(2, 3).Value = "Leave Group";
                    worksheet.Cell(2, 4).Value = LGName;


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
                    worksheet.Range("B4:I4").Merge();
                    worksheet.Cell("B4").Value = FilterValues;



                    var currentRow = 5;

                    worksheet.Cell(currentRow, 1).InsertTable(dt);
                    worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                    worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                    worksheet.Range("A5:I5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Range("A5:I5").Style.Font.Bold = true;


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
                            "Leave Carry Forward History List.xlsx"
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


        //public async Task<IActionResult> HistoryExportToExcel(int LeaveGroup, int Leave, int LeavePeriodFrom, int LeavePeriodTo,string LPFrom, string LPTo, string LName, 
        //    string LGName,  int TotalRow)
        //{
        //    try
        //    {
        //        LeaveCarryForward model = new LeaveCarryForward();

        //        var LoginUser = HttpContext.Session.GetString("LoginUser");
        //        var CompanyName = HttpContext.Session.GetString("CompanyName");
        //        ExcelPassword EP = new ExcelPassword();
        //        EP = await _commonService.GetExcelPassword("Leave Carry Forward History", "Excel");
        //        int slNo = 0;
        //        //Filter filter = new Filter();
        //        if (Global.GlobalFilter == null)
        //            Global.GlobalFilter = new Filter();
        //        else
        //        {
        //            Global.GlobalFilter.length = TotalRow;
        //            Global.GlobalFilter.start = 0;
        //        }
        //        string FilterValues = await _helpers.GetFilterValues(Global.GlobalFilter);
        //        if (FilterValues == "")
        //        {
        //            FilterValues = "All";
        //        }
        //        model = await _leaveCarryForwardService.LeaveCarryForwardHistoryList(LeavePeriodFrom, LeavePeriodTo, LeaveGroup, Leave, Global.GlobalFilter);

        //        string currDateTime = await _helpers.GetCurrentDate();
        //        
        //        using (var workbook = new XLWorkbook())
        //        {
        //            var worksheet = workbook.Worksheets.Add("Leave CF History List");
        //            int count = model.LeaveCarryForwardList.Count + 6;


        //            worksheet.Range("A1:L" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
        //            worksheet.Range("A1:L" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
        //            worksheet.Range("A1:L" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
        //            worksheet.Range("A1:L" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
        //            worksheet.Range("A1:L" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;


        //            var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 11)));
        //            wsReportNameHeaderRange.Style.Font.Bold = true;
        //            wsReportNameHeaderRange.Style.Font.FontSize = 15;
        //            wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        //            wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
        //            wsReportNameHeaderRange.Merge();
        //            wsReportNameHeaderRange.Value = "Leave Carry Forward History";

        //            worksheet.Cell(2, 1).Style.Font.Bold = true;
        //            worksheet.Cell(2, 1).Value = "Company Name";
        //            worksheet.Cell(2, 2).Value = CompanyName.ToUpper();
        //            worksheet.Cell(2, 3).Style.Font.Bold = true;
        //            worksheet.Cell(2, 3).Value = "Leave Group";
        //            worksheet.Cell(2, 4).Value = LGName;

        //            worksheet.Cell(2, 5).Style.Font.Bold = true;
        //            worksheet.Cell(2, 5).Value = "Leave";
        //            worksheet.Cell(2, 6).Value = LName;

        //            worksheet.Cell(3, 1).Style.Font.Bold = true;
        //            worksheet.Cell(3, 1).Value = "Leave Period From";
        //            worksheet.Cell(3, 2).Value = LPFrom;
        //            worksheet.Cell(3, 3).Style.Font.Bold = true;
        //            worksheet.Cell(3, 3).Value = "Leave Period To";
        //            worksheet.Cell(3, 4).Value = LPTo;


        //            worksheet.Cell(4, 1).Style.Font.Bold = true;
        //            worksheet.Cell(4, 1).Value = "Login User";
        //            worksheet.Cell(4, 2).Value = LoginUser;
        //            worksheet.Cell(4, 3).Style.Font.Bold = true;
        //            worksheet.Cell(4, 3).Value = "Date & Time";
        //            worksheet.Cell(4, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //            worksheet.Cell(4, 4).Style.DateFormat.Format = "dd-MMM-yyyy   h:mm AM/PM";
        //            worksheet.Cell(4, 4).Value = currDateTime.ToString();
        //            worksheet.Cell(4, 5).Style.Font.Bold = true;
        //            worksheet.Cell(4, 5).Value = "Total Count";
        //            worksheet.Cell(4, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //            worksheet.Cell(4, 6).Value = model.LeaveCarryForwardList.Count();
        //            worksheet.Cell(5, 1).Style.Font.Bold = true;
        //            worksheet.Cell(5, 1).Value = "Filter Applied";
        //            var filterapplied = worksheet.Range("B5:L5").Merge();

        //            filterapplied.Value = FilterValues;


        //            var currentRow = 6;

        //            #region Header
        //            worksheet.Range("A6:I6").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
        //            worksheet.Cell(currentRow, 1).Value = "Sl.No";
        //            worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
        //            worksheet.Cell(currentRow, 2).Value = "ID";
        //            worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
        //            worksheet.Cell(currentRow, 3).Value = "EmployeeName";
        //            worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
        //            worksheet.Cell(currentRow, 4).Value = "Designation";
        //            worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
        //            worksheet.Cell(currentRow, 5).Value = "Department";
        //            worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
        //            worksheet.Cell(currentRow, 6).Value = "Location";
        //            worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
        //            worksheet.Cell(currentRow, 7).Value = "Balance";
        //            worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
        //            worksheet.Cell(currentRow, 8).Value = "CF Max Limit";
        //            worksheet.Cell(currentRow, 8).Style.Font.Bold = true;
        //            worksheet.Cell(currentRow, 9).Value = "CF";
        //            worksheet.Cell(currentRow, 9).Style.Font.Bold = true;
        //            worksheet.Cell(currentRow, 10).Value = "Entry User";
        //            worksheet.Cell(currentRow, 10).Style.Font.Bold = true;
        //            worksheet.Cell(currentRow, 11).Value = "Entry Date";
        //            worksheet.Cell(currentRow, 11).Style.Font.Bold = true;
        //            worksheet.Cell(currentRow, 12).Value = "Entry From";
        //            worksheet.Cell(currentRow, 12).Style.Font.Bold = true;

        //            #endregion

        //            #region Body
        //            foreach (var item in model.LeaveCarryForwardList)
        //            {
        //                currentRow++;
        //                slNo = slNo + 1;
        //                worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //                worksheet.Cell(currentRow, 1).Value = slNo;
        //                worksheet.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //                worksheet.Cell(currentRow, 2).Value = item.EmployeeID;
        //                worksheet.Cell(currentRow, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //                worksheet.Cell(currentRow, 3).Value = item.EmployeeName;
        //                worksheet.Cell(currentRow, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //                worksheet.Cell(currentRow, 4).Value = item.Designation.Name;
        //                worksheet.Cell(currentRow, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //                worksheet.Cell(currentRow, 5).Value = item.Department.Name;
        //                worksheet.Cell(currentRow, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //                worksheet.Cell(currentRow, 6).Value = item.Location.Name;
        //                worksheet.Cell(currentRow, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //                worksheet.Cell(currentRow, 7).Value = item.Balance;
        //                worksheet.Cell(currentRow, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //                worksheet.Cell(currentRow, 8).Value = item.CFMaxLimit;
        //                worksheet.Cell(currentRow, 9).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //                worksheet.Cell(currentRow, 9).Value = item.CF;

        //                worksheet.Cell(currentRow, 10).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //                worksheet.Cell(currentRow, 10).Value = item.UserName;
        //                worksheet.Cell(currentRow, 11).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //                worksheet.Cell(currentRow, 11).Value = item.EntryDate;
        //                worksheet.Cell(currentRow, 11).Style.DateFormat.Format = "dd-MMM-yyyy";
        //                worksheet.Cell(currentRow, 12).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //                worksheet.Cell(currentRow, 12).Value = item.EntryFrom;

        //                worksheet.Columns(1, 12).Style.Alignment.WrapText = true;
        //                worksheet.Rows().Style.Alignment.WrapText = true;
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
        //                    content,
        //                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
        //                    "Leave Carry Forward History.xlsx"
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

        #endregion






    }

}
