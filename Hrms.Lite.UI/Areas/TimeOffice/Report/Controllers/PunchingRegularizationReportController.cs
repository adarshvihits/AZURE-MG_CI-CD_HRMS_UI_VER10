using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.TimeOffice.Report;
using Hrms.Lite.Shared.Account;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TimeOffice.Report;
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

namespace Hrms.Lite.UI.Areas.TimeOffice.Report.Controllers
{
    [Area("TimeOffice")]
    [SessionAuthorize]
    public class PunchingRegularizationReportController : Controller
    {
        private readonly IPunchingRegularizationReportService _PunchingRegularizationReportService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        private readonly IDropdownService _dropdownService;
        public PunchingRegularizationReportController(IDropdownService dropdownService, IPunchingRegularizationReportService PunchingRegularizationReportService, ICommonService commonService, IHelpers helpers)
        {
            _PunchingRegularizationReportService = PunchingRegularizationReportService;
            _commonService = commonService;
            _helpers = helpers;
            _dropdownService = dropdownService;
        }
        public async Task<IActionResult> Index()
        {
            PunchingRegularizationReport model = new PunchingRegularizationReport();
            Filter filter = new Filter();
            model.Filter = filter;
            var MonthList = await _dropdownService.GetMonthDropdown();
            ViewBag.Month = new SelectList(MonthList, "Code", "Name");
            ViewBag.LeavePeriod = new SelectList(await _dropdownService.GetLeavePeriodFilterDropdown(), "Code", "Name");
            ViewBag.Reason = new SelectList(await _dropdownService.GetPunchRegularizationReasonDropDown(), "Code", "Name");
            ViewBag.Month = new SelectList(await _dropdownService.GetMonthly_AttendanceProcessing_MonthDropdown(0), "Code", "Name");
            model.TabIndex = "ALL";
            await fillfilterDropdown();
            // model = await _PunchingRegularizationReportService.GetRegularizationList(model);
            return View("~/Areas/TimeOffice/Report/Views/PunchingRegularizationReport/Index.cshtml",model);
        }

        [HttpGet]
        public async Task<JsonResult> GetMonth(int LeavePeriod)
        {
            var HistoryMonth = await _dropdownService.GetLeaveEncashmentHistory_MonthDropdown(LeavePeriod);
     
            return Json(HistoryMonth);


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
        public async Task<IActionResult> GetList(Filter filter1,string TabIndex, int Month,int Reason,int LeavePeriod)
        {
            try
            {
                PunchingRegularizationReport model = new PunchingRegularizationReport();
                PunchingRegularizationReport m = new PunchingRegularizationReport();
                //model.EmployeeHeader = new EmployeeHeader();

                int totalrowsafterfiltering;
                int pending;
                int approved;
                int rejected;
                int all;


                Filter filter = new Filter();

              

                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }
                if (TabIndex!="" & LeavePeriod!=0 )
                {
                    model = await _PunchingRegularizationReportService.GetRegularizationList(TabIndex, Month, Reason,  LeavePeriod, filter1);
                }
                m.PunchingRegularizationList = model.PunchingRegularizationList;
                int totalrows = 0;
             


                if ( model.PunchingRegularizationList == null || model.PunchingRegularizationList.Count() == 0)
                { 
                    pending = model.PendingCount;
                   approved = model.ApproveCount;
                rejected = model.RejectedCount;
                all = model.TotalCount;
             
                    totalrowsafterfiltering = 0;
                  
                }
                else
                {
                    m.PunchingRegularizationList = model.PunchingRegularizationList;
                    m.PendingCount = model.PendingCount;
                    m.ApproveCount = model.ApproveCount;
                    m.TotalCount = model.TotalCount;
                    m.RejectedCount = model.RejectedCount;

                }



                if (model.PunchingRegularizationList == null || model.PunchingRegularizationList.Count() == 0)
                {

                    return Json(new
                    {
                        data = "",
                        pending = model.PendingCount,
                        approved = model.ApproveCount,
                        all = model.TotalCount,
                        rejected = model.RejectedCount,
                        draw = filter1.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows
                    });
                }




                if (model.PunchingRegularizationList.Count > 0)
                {
                    totalrows = m.PunchingRegularizationList[0].active;
                    pending = m.PendingCount;
                    approved = m.ApproveCount;
                    all = m.TotalCount;
                    rejected = m.RejectedCount;


                }


                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = m.PunchingRegularizationList[0].totalRecords;
                    return Json(new
                    {
                        data = model.PunchingRegularizationList,
                        pending = model.PendingCount,
                        approved = model.ApproveCount,
                        rejected = model.RejectedCount,
                        all = model.TotalCount,
                        draw = filter1.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows//totalrowsafterfiltering
                    });
                }
                else
                {
                    return Json(new
                    {
                        data = model.PunchingRegularizationList,
                        pending = model.PendingCount,
                        approved = model.ApproveCount,
                        rejected = model.RejectedCount,
                        all = model.TotalCount,
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

        public async Task<IActionResult> ExportToExcel(Filter filter1, string TabIndex, int Month, int Reason, int LeavePeriod,int TotalRecords,string LeaveName,string MName,string ReasonName)
        {
            try
            {


                if (filter1 == null)
                    filter1 = new Filter();

                string FilterValues = await _helpers.GetFilterValues(filter1);


                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Punching Regularization("+ TabIndex + ")List", "Excel");
               

                if (FilterValues == "")
                {
                    FilterValues = "All";
                }
                string currDateTime = await _helpers.GetCurrentDate();
                

                PunchingRegularizationReport model = new PunchingRegularizationReport();
                filter1.length = TotalRecords;




                var dataset = await _PunchingRegularizationReportService.GetRegularizationList_Excel(TabIndex, Month, Reason, LeavePeriod, filter1);
              

                DataTable dt = dataset.Tables[0];


                //set EXCEL Column Heading 
                //for (int i = 0; i < dt.Columns.Count; i++)
                //{
                //    if (dt.Columns[i].ColumnName == "order")
                //        dt.Columns[i].ColumnName = "Sl.No";
                //    dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
                //}


                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (dt.Columns[i].ColumnName == "order" || dt.Columns[i].ColumnName == "totalCount" || dt.Columns[i].ColumnName == "Active")
                    {
                        dt.Columns.RemoveAt(i);
                        if (dt.Columns.Count != i)
                        {
                            if (dt.Columns[i].ColumnName == "pf" || dt.Columns[i].ColumnName == "esi" || dt.Columns[i].ColumnName == "pfNo")
                                dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.ToUpper();
                            else
                                dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
                        }
                        i = i - 1;
                    }
                    else
                    {
                        if (dt.Columns[i].ColumnName == "pf" || dt.Columns[i].ColumnName == "esi" || dt.Columns[i].ColumnName == "pfNo")
                            dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.ToUpper();
                        else
                            dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
                    }
                }

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Punching Regularization Report");
                    int Rowcount = dt.Rows.Count + 5;

                    string columnName = "";
                    int Columncount = dt.Columns.Count;

                    while (Columncount > 0)
                    {
                        int modulo = (Columncount - 1) % 26;
                        columnName = Convert.ToChar('A' + modulo) + columnName;
                        Columncount = (Columncount - modulo) / 26;
                    }

                    string colName = columnName;
                    worksheet.Range("A1", colName + Rowcount).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1", colName + Rowcount).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1", colName + Rowcount).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1", colName + Rowcount).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1", colName + Rowcount).Style.Border.LeftBorder = XLBorderStyleValues.Thin;


                    var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, colName));
                    wsReportNameHeaderRange.Style.Font.Bold = true;
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Merge();
                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191); ;
                    wsReportNameHeaderRange.Value = "Punching Regularization List";

                    worksheet.Cell(2, 1).Style.Font.Bold = true;
                    worksheet.Cell(2, 1).Value = "Group Company Name";
                    worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                    worksheet.Cell(2, 3).Style.Font.Bold = true;
                    worksheet.Cell(2, 3).Value = "Leave Period";
                    worksheet.Cell(2, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(2, 4).Value = LeaveName;

                    worksheet.Cell(2, 5).Style.Font.Bold = true;
                    worksheet.Cell(2, 5).Value = "Month";
                    worksheet.Cell(2, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(2, 6).Value = MName ;
                    worksheet.Cell(2, 6).Style.DateFormat.Format = "MMM-yyyy";

                    worksheet.Cell(2, 7).Style.Font.Bold = true;
                    worksheet.Cell(2, 7).Value = "Reason";
                    worksheet.Cell(2, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(2, 8).Value = ReasonName;


                    worksheet.Cell(2, 4).Style.DateFormat.Format = "MMM-yyyy";
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
                    worksheet.Range("B4", colName + 4).Merge();
                    worksheet.Cell("B4").Value = FilterValues;


                    var currentRow = 5;
                    worksheet.Cell(currentRow, 1).InsertTable(dt);
                    worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                    worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                    worksheet.Range("A5:" + colName + "5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Range("A5:" + colName + "5").Style.Font.Bold = true;


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
                            "Punching Regularization Report List.xlsx"
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
