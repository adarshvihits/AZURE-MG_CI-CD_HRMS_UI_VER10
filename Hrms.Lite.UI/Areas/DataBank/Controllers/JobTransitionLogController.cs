using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.DataBank.Generic.Master;
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
    public class JobTransitionLogController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly IJobTransitionService _jobTransitionService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        public static class Global
        {

            public static Filter GlobalFilter;

        }
        public JobTransitionLogController(IDropdownService dropdownService, ICommonService commonService, IJobTransitionService jobTransitionService, IHelpers helpers)
        {
            _jobTransitionService = jobTransitionService;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = helpers;

        }
        public async Task<IActionResult> Index()
        {
            try { 
            JobTransition model = new JobTransition();
            JobTransition _model = new JobTransition();
            Filter filter = new Filter();
            model.Filter = new FilterSlabComboFill();
            //int TabIndex = 0;
            ViewBag.Filter = new SelectList(await _commonService.GetDurationWiseFilterSlabDropDown(), "Code", "Name");
            model.Filter.Code = 3;
            //int SearchId = 3;
            model.JobTransition_Tabs = (List<JobTransition_Tabs>)await _jobTransitionService.GetJobTransitionTabs();
            //_model = await _jobTransitionService.GetJobTransitionLogList(TabIndex, SearchId, filter);
            ViewBag.TransitionType = new SelectList(await _dropdownService.GetFilteredJobTransitionTypeDropDown(), "Code", "Name");
            model.JobTransitionLogList = _model.JobTransitionLogList;
            model.AllTabCount = _model.AllTabCount;
            model.Tab1Count = _model.Tab1Count;
            //model.Tab2Count = _model.Tab2Count;
            model.Tab3Count = _model.Tab3Count;
            model.Tab4Count = _model.Tab4Count;
            model.MoreCount = _model.MoreCount;
            await fillfilterDropdown();
            ViewBag.Tabs = new SelectList(model.JobTransition_Tabs, "Code", "Name");
                model.Type = "ALL";
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



        [HttpPost]
        public async Task<IActionResult> GetActiveList(Filter filter1,string FromDate,string ToDate, int TransitionType,string Type)
        {
            try
            {
                JobTransition model = new JobTransition();
                JobTransition m = new JobTransition();
                model.EmployeeHeader = new EmployeeHeader();




                Filter filter = new Filter();
                Global.GlobalFilter = filter1;


                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }
        
                

                model = await _jobTransitionService.GetJobTransitionLogList(FromDate,ToDate,0, TransitionType,Type,filter1);
                //if (filter.Tabindex == 0 && model.JobTransitionLogList != null)
                //    model.JobTransitionLogList[0].active = model.AllTabCount;
                //else if (filter.Tabindex == 1000 && model.JobTransitionLogList != null)
                //    model.JobTransitionLogList[0].active = model.MoreCount;
                //else if (clickedTab == "Tab_1" && model.JobTransitionLogList != null)
                //    model.JobTransitionLogList[0].active = model.Tab1Count;
                //else if (clickedTab == "Tab_2" && model.JobTransitionLogList != null)
                //    model.JobTransitionLogList[0].active = model.Tab2Count;
                //else if (clickedTab == "Tab_3" && model.JobTransitionLogList != null)
                //    model.JobTransitionLogList[0].active = model.Tab3Count;
                //else if (clickedTab == "Tab_4" && model.JobTransitionLogList != null)
                //    model.JobTransitionLogList[0].active = model.Tab4Count;




                m.JobTransitionLogList = model.JobTransitionLogList;
                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.JobTransitionLogList == null || model.JobTransitionLogList.Count() == 0)
                {
                   
                    return Json(new
                    {
                        data = "",
                        draw = filter1.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows
                    });
                }

                if (model.JobTransitionLogList.Count > 0)
                {                    
                    
                    totalrows = m.JobTransitionLogList[0].active;

                    //m.OnBoardingList.ToList<Hrms.Lite.Shared.Master.Test>().Count; ;
                }

                //model.AllTabCount = m.AllTabCount;
                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = m.JobTransitionLogList[0].totalRecords;
                    return Json(new
                    {
                        data = model.JobTransitionLogList,
                        draw = filter1.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        recordsTotal = totalrows,
                        Count=model,
                        recordsFiltered = totalrows//totalrowsafterfiltering
                    });
                }
                else
                {
                    // return Json(new { data = model.EmployeeProfileList, draw = filter1.draw, recordsTotal = totalrows, recordsFiltered = totalrows });
                    return Json(new
                    {
                        data = model.JobTransitionLogList,
                        draw = filter1.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        Count = model,
                        recordsFiltered = totalrows
                    });
                }


            }
            catch (Exception ex)
            {
                return View();
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
        public async Task<IActionResult> ApplyFilter(Filter filter, string FromDate, string ToDate, int TransitionType,string Type)
        {
            try { 
            JobTransition model = new JobTransition();

            Global.GlobalFilter = filter;
            model = await _jobTransitionService.GetJobTransitionLogList(FromDate, ToDate, 0, TransitionType,Type, filter);
            return PartialView("_JobTransitionLogList", model);
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
        //public async Task<IActionResult> GetJobTransitionLogList(int TabIndex, int SearchId)
        //{
        //    JobTransition model = new JobTransition();
        //    model = await _jobTransitionService.GetJobTransitionLogList(TabIndex, SearchId);
        //    return PartialView("_JobTransitionLogList", model);
        //}
         
        public async Task<IActionResult> LOg(Guid EmployeeGI, Guid HistoryGI)
        {
            try { 
            JobTransition model = new JobTransition();
            model = await _jobTransitionService.GetJobTransitionDetails(EmployeeGI, HistoryGI, "APPROVE",null);
            model.EmployeeHeader = await _commonService.GetEmployeeHeader(EmployeeGI);
            return View("JobTransitionLog", model);
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


        public async Task<IActionResult> ExportToExcel(string FromDate, string ToDate, int TransitionType, string TabName,string Type, int totRecords)
        {
            try
            {

                if (Global.GlobalFilter == null)
                    Global.GlobalFilter = new Filter();
                else
                {
                    Global.GlobalFilter.length = totRecords;
                    Global.GlobalFilter.start = 0;
                }
                string FilterValues = await _helpers.GetFilterValues(Global.GlobalFilter);

                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Job Transition Log(" + TabName + ")", "Excel");

                if (FilterValues == "")
                {
                    FilterValues = "All";
                }

                string currDateTime = await _helpers.GetCurrentDate();
                

                //Get Datset
                var dataset = await _jobTransitionService.GetJobTransitionLogList_Excel(FromDate,ToDate,0, TransitionType, Type, Global.GlobalFilter);
                DataTable dt = dataset.Tables[0];


                //set EXCEL Column Heading 
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (dt.Columns[i].ColumnName == "id" || dt.Columns[i].ColumnName == "dob" || dt.Columns[i].ColumnName == "y/s")
                        dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.ToUpper();

                    else
                        dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
                }

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Job Transition Log");
                    int count = dt.Rows.Count + 5;
                    string columnName = "";
                    //int Columncount = model.MainList.Header.Count + 3;
                    int Columncount = dt.Columns.Count;
                    int rowcount = dt.Rows.Count + 5;

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
                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Merge();
                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191); ;
                    wsReportNameHeaderRange.Value = "Job Transition Log (" + TabName + ")";

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
                    worksheet.Range("B4",colName+4).Merge();
                    worksheet.Cell("B4").Value = FilterValues;



                    var currentRow = 5;

                    worksheet.Cell(currentRow, 1).InsertTable(dt);
                    worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                    worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                    worksheet.Range("A5",colName + 5).Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Range("A5", colName + 5).Style.Font.Bold = true;


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
                            "Job Transition Log( " + TabName + ") List.xlsx"
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




        //public async Task<IActionResult> ExportToExcel(int TabIndex, int SearchId,string TabName,int totRecords)
        //{
        //    try { 
        //    JobTransition model = new JobTransition();
        //    var LoginUser = HttpContext.Session.GetString("LoginUser");
        //    var CompanyName = HttpContext.Session.GetString("CompanyName");
        //    ExcelPassword EP = new ExcelPassword();
        //    EP = await _commonService.GetExcelPassword("Job Transition Log (" + TabName + ")", "Excel");
        //    int slNo = 0;
        //    if (Global.GlobalFilter == null)
        //        Global.GlobalFilter = new Filter();
        //        else
        //        {
        //            Global.GlobalFilter.length = totRecords;
        //            Global.GlobalFilter.start = 0;
        //        }
        //    string FilterValues =await _helpers.GetFilterValues(Global.GlobalFilter);
        //    model = await _jobTransitionService.GetJobTransitionLogList(TabIndex, SearchId, Global.GlobalFilter);
        //    if (FilterValues == "")
        //    {
        //        FilterValues = "All";
        //    }
        //    string currDateTime = await _helpers.GetCurrentDate();
        //    

        //    using (var workbook = new XLWorkbook())
        //    {
        //        var worksheet = workbook.Worksheets.Add("Job Transition Log List");
        //        int count = model.JobTransitionLogList.Count + 5;


        //        worksheet.Range("A1:I" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
        //        worksheet.Range("A1:I" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
        //        worksheet.Range("A1:I" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
        //        worksheet.Range("A1:I" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
        //        worksheet.Range("A1:I" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;

        //        var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 8)));
        //        wsReportNameHeaderRange.Style.Font.Bold = true;
        //        wsReportNameHeaderRange.Style.Font.FontSize = 15;
        //        wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        //        wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
        //        wsReportNameHeaderRange.Merge();
        //        wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191); ;
        //        wsReportNameHeaderRange.Value = "Job Transition Log (" + TabName + ")";

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
        //        worksheet.Cell(3, 6).Value = model.JobTransitionLogList.Count;
        //        worksheet.Cell(4, 1).Style.Font.Bold = true;
        //        worksheet.Cell(4, 1).Value = "Filter Applied";
        //        worksheet.Range("B4:I4").Merge();
        //        worksheet.Cell("B4").Value = FilterValues;

        //        var currentRow = 5;
        //        #region Header
        //        worksheet.Cell(currentRow, 1).Value = "Sl.No";
        //        worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
        //        worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
        //        worksheet.Cell(currentRow, 2).Value = "ID";
        //        worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
        //        worksheet.Cell(currentRow, 3).Value = "EmployeeName";
        //        worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
        //        worksheet.Cell(currentRow, 4).Value = "Designation";
        //        worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
        //        worksheet.Cell(currentRow, 5).Value = "Department";
        //        worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
        //        worksheet.Cell(currentRow, 6).Value = "Location";
        //        worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
        //        worksheet.Cell(currentRow, 7).Value = "Type";
        //        worksheet.Cell(currentRow, 8).Style.Font.Bold = true;
        //        worksheet.Cell(currentRow, 8).Value = "Order Date";
        //        worksheet.Cell(currentRow, 9).Style.Font.Bold = true;
        //        worksheet.Cell(currentRow, 9).Value = "Effective Date";



        //        #endregion

        //        #region Body
        //        foreach (var item in model.JobTransitionLogList)
        //        {
        //            currentRow++;
        //            slNo = slNo + 1;
        //            worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //            worksheet.Range("A5:I5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
        //            worksheet.Cell(currentRow, 1).Value = slNo;
        //            worksheet.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //            worksheet.Cell(currentRow, 2).Value = item.EmployeeID;
        //            worksheet.Cell(currentRow, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //            worksheet.Cell(currentRow, 3).Value = item.EmployeeName;
        //            worksheet.Cell(currentRow, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //            worksheet.Cell(currentRow, 4).Value = item.Designation;
        //            worksheet.Cell(currentRow, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //            worksheet.Cell(currentRow, 5).Value = item.Department;
        //            worksheet.Cell(currentRow, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //            worksheet.Cell(currentRow, 6).Value = item.Location;
        //            worksheet.Cell(currentRow, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //            worksheet.Cell(currentRow, 7).Value = item.TransitionType;
        //            worksheet.Cell(currentRow, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //            worksheet.Cell(currentRow, 8).Value = item.OrderDate;
        //                worksheet.Cell(currentRow, 8).Style.DateFormat.Format = "dd-MMM-yyyy";
        //            worksheet.Cell(currentRow, 9).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //            worksheet.Cell(currentRow, 9).Value = item.EffectiveDate;
        //                worksheet.Cell(currentRow, 9).Style.DateFormat.Format = "dd-MMM-yyyy";
        //            worksheet.Columns(1, 8).Style.Alignment.WrapText = true;
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
        //                "Job Transition Log List.xlsx"
        //                );
        //        }
        //    }

        //}
        //    catch (UnauthorizedAccessException e)
        //    {

        //        return RedirectToAction("Login", "Account", new { Area = "" });
        //    }
        //    catch (Exception e)
        //    {
        //        return RedirectToAction("Error", "Account", new { Area = "" });
        //    }

        //}
    }
}
