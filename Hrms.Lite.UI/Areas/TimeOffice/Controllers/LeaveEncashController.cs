using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.TimeOffice;
using Hrms.Lite.Shared.Account;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TimeOffice;
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

namespace Hrms.Lite.UI.Areas.TimeOffice.Controllers
{
    [Area("TimeOffice")]
    [SessionAuthorize]
    public class LeaveEncashController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly ILeaveEncashService _LeaveEncashService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;

     
        public LeaveEncashController(IDropdownService dropdownService, ILeaveEncashService LeaveEncashService, ICommonService commonService,IHelpers helpers)
        {
            _LeaveEncashService = LeaveEncashService;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = helpers;
        }
        #region LeaveEncashment Entry 
        public async Task<IActionResult> Index()
        {
            try { 
            LeaveEncashment model = new LeaveEncashment();
                LeaveEncashment m1 = new LeaveEncashment();
                Filter filter = new Filter();
                filter.length = 10;
                m1 = await _LeaveEncashService.LeaveEncashmentHistoryList(0, 0, 0, filter);
                ViewBag.LeavePeriod = new SelectList(await _dropdownService.GetLeavePeriodFilterDropdown(), "Code", "Name");
            ViewBag.LeaveGroup = new SelectList(await _dropdownService.GetLeaveGroupFilterDropdown(), "Code", "Name");
            ViewBag.Leave = new SelectList(await _dropdownService.GetLeaveEncashment_LeaveDropdown(0), "Code", "Name");

                ViewBag.Month = new SelectList(await _dropdownService.GetLeaveEncashment_MonthDropdown(0,0), "Code", "Name");
                model.EntryType = 1;
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
        [HttpGet]
        public async Task<JsonResult> fillLeaveCombo(int LGroup)
        {
            var details = await _dropdownService.GetLeaveEncashment_LeaveDropdown(LGroup);
            return Json(details);
        }
        public IActionResult GetEncashHeading(int Type)
        {
            try
            {
                LeaveEncashment model = new LeaveEncashment();
                if (Type == 1)
                {
                    return PartialView("_LeaveEncashCreateList", model);

                }
                else

                {
                    return PartialView("_LeaveEncashEditList", model);

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


        [HttpPost]
        public async Task<IActionResult> GetLeaveEncashList(Filter filter1, int LeavePeriod, int LeaveGroup, int Leave, int Month, int Type,string Form)
        {
            try
            {
                LeaveEncashment model = new LeaveEncashment();
                Filter filter = new Filter();
              
               
                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }
                if (LeavePeriod != 0 && LeaveGroup != 0 && Leave != 0 && Month !=0)
                {
                    if (Form == "History")
                        model = await _LeaveEncashService.LeaveEncashmentHistoryList(LeavePeriod, LeaveGroup, Leave, filter1);
                    else

                    model = await _LeaveEncashService.LeaveEncashList(LeavePeriod, LeaveGroup, Leave, Month, Type,filter1);
                 
                }



                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.LeaveEncashmentList == null || model.LeaveEncashmentList.Count() == 0)
                {
                    

                    return Json(new
                    {
                        data = "",
                        draw = filter1.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows
                    });
                }




                if (model.LeaveEncashmentList.Count > 0)
                {
                    totalrows = model.LeaveEncashmentList[0].active;

                  
                }

                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = model.LeaveEncashmentList[0].totalRecords;
                    return Json(new
                    {
                        data = model.LeaveEncashmentList,
                        draw = filter1.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows//totalrowsafterfiltering
                    });
                }
                else
                {
                    return Json(new
                    {
                        data = model.LeaveEncashmentList,
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
      
        [HttpPost]
        public async Task<IActionResult> BatchUpdate(LeaveEncashment model, Filter Filter)
        {
            try { 
            Guid EmployeeGI = model.EmployeeGI;
            model.Filter = Filter;
            var response = await _LeaveEncashService.LeaveEncashBatchUpdate(model);
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
        public async Task<IActionResult> GetEditDetails(Guid EmployeeGI, int LeavePeriod, int LeaveGroup, int Leave,int Month)
        {
            try { 
            LeaveEncashment model = new LeaveEncashment();
            model = await _LeaveEncashService.GetEditDetails(EmployeeGI, LeavePeriod, LeaveGroup, Leave, Month);
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
        public async Task<IActionResult> Edit(LeaveEncashment model)
        {
            try { 
            Guid EmployeeGI = model.EmployeeGI;
            var response = await _LeaveEncashService.EmployeeWiseLeaveEncashModify(model);
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
     
        
        public async Task<IActionResult> ExportToExcel(Filter Filter,int LeavePeriod, int LeaveGroup, int Leave,int Month, string LPName, string LGName, string LName, string Mname, int totRecords)
        {
            try { 
            LeaveEncashment model = new LeaveEncashment();
          
            var LoginUser = HttpContext.Session.GetString("LoginUser");
            var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Leave Encash", "Excel");
                int slNo = 0;


                Filter.length = totRecords;
                Filter.start = 0;
                
                string FilterValues = await _helpers.GetFilterValues(Filter);
            if (FilterValues == "")
            {
                FilterValues = "All";
            }            
            

                var dataset = await _LeaveEncashService.LeaveEncashmentList_Excel(LeavePeriod, LeaveGroup,Leave, Month, Filter);
                DataTable dt = dataset.Tables[0];

                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (dt.Columns[i].ColumnName == "doj" || dt.Columns[i].ColumnName == "dob" || dt.Columns[i].ColumnName == "y/s")
                        dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.ToUpper();
                    else
                        dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
                }

                string currDateTime = await _helpers.GetCurrentDate();
            
            using (var workbook = new XLWorkbook())
            {
              
                var worksheet = workbook.Worksheets.Add("LeaveEncashList");
               

                int count = dt.Rows.Count + 6;


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
                wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                wsReportNameHeaderRange.Value = "Leave Encash";

                worksheet.Cell(2, 1).Style.Font.Bold = true;
                worksheet.Cell(2, 1).Value = "Group Company Name"; 
                worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                worksheet.Cell(2, 3).Style.Font.Bold = true;
                worksheet.Cell(2, 3).Value = "Leave Period";
                worksheet.Cell(2, 4).Value = LPName;
                worksheet.Cell(2, 5).Style.Font.Bold = true;
                worksheet.Cell(2, 5).Value = "Leave Group";
                worksheet.Cell(2, 6).Value = LGName;
                worksheet.Cell(2, 7).Style.Font.Bold = true;
                worksheet.Cell(2, 7).Value = "Leave";
                worksheet.Cell(2, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                worksheet.Cell(2, 8).Value = LName;
                worksheet.Cell(3, 1).Style.Font.Bold = true;
                worksheet.Cell(3, 1).Value = "Month";
                worksheet.Cell(3, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                worksheet.Cell(3, 2).Style.DateFormat.Format = "MMM-yyyy";
                worksheet.Cell(3, 2).Value = Mname;

                worksheet.Cell(4, 1).Style.Font.Bold = true;
                worksheet.Cell(4, 1).Value = "Login User"; 
                worksheet.Cell(4, 2).Value =  LoginUser;
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
                var filterapplied = worksheet.Range("B5:I5").Merge();

                filterapplied.Value = FilterValues;


                    var currentRow = 6;

                    worksheet.Cell(currentRow, 1).InsertTable(dt);
                    worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                    worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                    worksheet.Range("A6:I6").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Range("A6:I6").Style.Font.Bold = true;


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
                            "Leave Encash List.xlsx"
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
        #region EncashHistory
        public async Task<IActionResult> History()
        {
            try
            {
                LeaveEncashment model = new LeaveEncashment();
                ViewBag.LeavePeriod = new SelectList(await _dropdownService.GetLeavePeriodFilterDropdown(), "Code", "Name");
                ViewBag.LeaveGroup = new SelectList(await _dropdownService.GetLeaveGroupFilterDropdown(), "Code", "Name");
                ViewBag.Leave = new SelectList(await _dropdownService.GetLeaveEncashment_LeaveDropdown(0), "Code", "Name");
            
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

        public async Task<IActionResult> HistoryExportExcel(int LeavePeriod, int LeaveGroup, int Leave, int Month, string LPName, string LGName, string LName, string Mname, int totRecords)
        {
            try
            {
                LeaveEncashment model = new LeaveEncashment();

                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Leave Encash History", "Excel");
                int slNo = 0;

                
                  Filter Filter = new Filter();

                Filter.length = totRecords;
                Filter.start = 0;
               
                string FilterValues = await _helpers.GetFilterValues(Filter);
                if (FilterValues == "")
                {
                    FilterValues = "All";
                }
                model = await _LeaveEncashService.LeaveEncashmentHistoryList(LeavePeriod, LeaveGroup, Leave, Filter);
                string currDateTime = await _helpers.GetCurrentDate();
                
                using (var workbook = new XLWorkbook())
                {

                    var worksheet = workbook.Worksheets.Add("Leave Encashment History List");


                    int count = model.LeaveEncashmentList.Count + 6;


                    worksheet.Range("A1:L" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:L" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:L" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:L" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:L" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;






                    var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 11)));
                    wsReportNameHeaderRange.Style.Font.Bold = true;
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Merge();
                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                    wsReportNameHeaderRange.Value = "Leave Encashment History";

                    worksheet.Cell(2, 1).Style.Font.Bold = true;
                    worksheet.Cell(2, 1).Value = "Group Company Name";
                    worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                    worksheet.Cell(2, 3).Style.Font.Bold = true;
                    worksheet.Cell(2, 3).Value = "Leave Period";
                    worksheet.Cell(2, 4).Value = LPName;
                    worksheet.Cell(2, 5).Style.Font.Bold = true;
                    worksheet.Cell(2, 5).Value = "Leave Group";
                    worksheet.Cell(2, 6).Value = LGName;
                    worksheet.Cell(2, 7).Style.Font.Bold = true;
                    worksheet.Cell(2, 7).Value = "Leave";
                    worksheet.Cell(2, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(2, 8).Value = LName;
           

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
                    worksheet.Cell(4, 6).Value = model.LeaveEncashmentList.Count();
                    worksheet.Cell(5, 1).Style.Font.Bold = true;
                    worksheet.Cell(5, 1).Value = "Filter Applied";
                    var filterapplied = worksheet.Range("B5:L5").Merge();

                    filterapplied.Value = FilterValues;


                    var currentRow = 6;
                    #region Header
                    worksheet.Range("A6:L6").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Cell(currentRow, 1).Value = "Sl.No";
                    worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 2).Value = "ID";
                    worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 3).Value = "EmployeeName";
                    worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 4).Value = "Designation";
                    worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 5).Value = "Department";
                    worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 6).Value = "Location";
                    worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 7).Value = "Balance";
                    worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 8).Value = "Encash Max Limit";
                    worksheet.Cell(currentRow, 8).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 9).Value = "Encash";
                    worksheet.Cell(currentRow, 9).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 10).Value = "Entry User";
                    worksheet.Cell(currentRow, 10).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 11).Value = "Entry Date";
                    worksheet.Cell(currentRow, 11).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 12).Value = "Entry From";
                    worksheet.Cell(currentRow, 12).Style.Font.Bold = true;


                    #endregion

                    #region Body
                    foreach (var item in model.LeaveEncashmentList)
                    {
                        currentRow++;
                        slNo = slNo + 1;
                        worksheet.Columns().AdjustToContents();
                        worksheet.Rows().AdjustToContents();
                        worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 1).Value = slNo;
                        worksheet.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 2).Value = item.EmployeeID;
                        worksheet.Cell(currentRow, 3).Value = item.EmployeeName;
                        worksheet.Cell(currentRow, 4).Value = item.Designation.Name;
                        worksheet.Cell(currentRow, 5).Value = item.Department.Name;
                        worksheet.Cell(currentRow, 6).Value = item.Location.Name;
                        worksheet.Cell(currentRow, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 7).Value = item.Balance;
                        worksheet.Cell(currentRow, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 8).Value = item.MaxLimit;
                        worksheet.Cell(currentRow, 9).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 9).Value = item.Value;
                        worksheet.Cell(currentRow, 10).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 10).Value = item.UserName;
                        worksheet.Cell(currentRow, 11).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 11).Value = item.EntryDate;
                        worksheet.Cell(currentRow, 11).Style.DateFormat.Format = "dd-MMM-yyyy";
                        worksheet.Cell(currentRow, 12).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 12).Value = item.EntryFrom;
                        worksheet.Columns(1, 12).Style.Alignment.WrapText = true;
                        worksheet.Rows().Style.Alignment.WrapText = true;
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
                            "Leave Encashment History List.xlsx"
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
        [HttpGet]
        public async Task<JsonResult> GetMonth(int PayrollCode, int LeavePeriod)
        {
            var HistoryMonth = await _dropdownService.GetLeaveEncashment_MonthDropdown(PayrollCode, LeavePeriod);
            return Json(HistoryMonth);

        }
    }
}
