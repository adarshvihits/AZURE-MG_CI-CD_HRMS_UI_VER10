using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.PayRoll;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Spire.Xls;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.PayRoll.Controllers
{
    [Area("PayRoll")]
    [SessionAuthorize]

    public class PTProcessingController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly IPTProcessingService _PTProcessingService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        public PTProcessingController(IPTProcessingService PTProcessingService, IDropdownService dropdownService, ICommonService commonService, IHelpers Helpers)
        {
            _PTProcessingService = PTProcessingService;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = Helpers;

        }
        public static class Global
        {
            public static Filter GlobalFilter;
           
        }
        public async Task<IActionResult> Index()
        {
            PTProcessing model = new PTProcessing();
            await fillfilterDropdown();
            Filter filter = new Filter();
            ViewBag.PTGroup = new SelectList(await _dropdownService.GetPTGroupDropDown(), "Code", "Name");
            var currMonth = await _commonService.GetCurrentMonthDropdown();
            ViewBag.Month = new SelectList(await _dropdownService.GetSalaryProcessing_MonthDropdown(0), "Code", "Name", currMonth.Data);
    
            return View(model);
        }




        [HttpPost]
        public async Task<IActionResult> Save(PTProcessing model,int month,int PtGroup)
        {

            try
            {
                model.filter = Global.GlobalFilter;
                model.Month = new Shared.PayRoll.Generic.Master.PayrollMonth();
                model.PTGroup = new Shared.Master.PTGroup();
                model.Month.Code = month;
                model.PTGroup.Code = PtGroup;
                var response = await _PTProcessingService.PTProcessingSave(model);
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




        [HttpGet]
        public async Task<IActionResult> GetDetails(int Month,int PTGroup)
        {
            try
            {

                var PTDetails = await _PTProcessingService.GetPTGroupDetails(Month, PTGroup);
                return Json(PTDetails);
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
        public async Task<IActionResult> GetList(Filter filter1, int Month, int PTgroup)
        {
            try
            {
                PTProcessing model = new PTProcessing();
                PTProcessing m = new PTProcessing();
                //model.EmployeeHeader = new EmployeeHeader();




                Filter filter = new Filter();
                Global.GlobalFilter = filter1;


                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }

                model = await _PTProcessingService.GetPTProcessingList(Month, PTgroup, filter1);
                m.Details = model.Details;
                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.Details == null || model.Details.Count() == 0)
                {
                  

                    return Json(new
                    {
                        data = "",
                        draw = filter1.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows
                    });
                }




                if (model.Details.Count > 0)
                {
                    totalrows = m.Details[0].active;

                }

                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = m.Details[0].totalRecords;
                    return Json(new
                    {
                        data = model.Details,
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
                        data = model.Details,
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
        public async Task<IActionResult> ExportToExcel(int Month, int PTgroup, string MonthName, string ptGroupName, int totRecords)
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
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("PT Processing", "Excel");

                if (FilterValues == "")
                {
                    FilterValues = "All";
                }

                string currDateTime = await _helpers.GetCurrentDate();
                

                //Get Datset
                var dataset = await _PTProcessingService.GetPTProcessingDetails_Excel(Month, PTgroup, Global.GlobalFilter);
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
                    var worksheet = workbook.Worksheets.Add("PT Processing");
                    int count = dt.Rows.Count + 5;


                    worksheet.Range("A1:O" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:O" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:O" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:O" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:O" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 14)));
                    wsReportNameHeaderRange.Style.Font.Bold = true;
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Merge();
                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191); 
                    wsReportNameHeaderRange.Value = "PT Processing";

                    worksheet.Cell(2, 1).Style.Font.Bold = true;
                    worksheet.Cell(2, 1).Value = "Company Name";
                    worksheet.Cell(2, 2).Value = CompanyName.ToUpper();
                    worksheet.Cell(2, 3).Style.Font.Bold = true;
                    worksheet.Cell(2, 3).Value = "Month";
                    worksheet.Cell(2, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(2, 4).Value = MonthName;
                    worksheet.Cell(2, 4).Style.DateFormat.Format = "dd-MMM-yyyy";

                    worksheet.Cell(2, 5).Style.Font.Bold = true;
                    worksheet.Cell(2, 5).Value = "Pt Group";
                    worksheet.Cell(2, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(2, 6).Value = ptGroupName;

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
                    worksheet.Range("B4:O4").Merge();
                    worksheet.Cell("B4").Value = FilterValues;



                    var currentRow = 5;

                    worksheet.Cell(currentRow, 1).InsertTable(dt);
                    worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                    worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                    worksheet.Range("A5:O5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Range("A5:O5").Style.Font.Bold = true;


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
                            "PT Processing List.xlsx"
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


        //public async Task<IActionResult> ExportToExcel(int Month, int PTgroup,string MonthName,string ptGroupName,int totRecords)
        //{
        //    try
        //    {
        //        PTProcessing model = new PTProcessing();
        //        if (Global.GlobalFilter == null)
        //            Global.GlobalFilter = new Filter();
        //        else
        //        {
        //            Global.GlobalFilter.length = totRecords;
        //            Global.GlobalFilter.start = 0;
        //        }
        //        string FilterValues = await _helpers.GetFilterValues(Global.GlobalFilter);
        //        var GroupByLIst = await _dropdownService.GetEmployeeProfileSummaryGroupByDropDown();
        //        ViewBag.groupBy = new SelectList(GroupByLIst, "Code", "Name");
        //        int firstgroupById = GroupByLIst[0].Code;
        //        model = await _PTProcessingService.GetPTProcessingList(Month, PTgroup, Global.GlobalFilter);
        //        var LoginUser = HttpContext.Session.GetString("LoginUser");
        //        var CompanyName = HttpContext.Session.GetString("CompanyName");
        //        ExcelPassword EP = new ExcelPassword();
        //        EP = await _commonService.GetExcelPassword("PT Processing", "Excel");
        //        int slNo = 0;
        //        if (FilterValues == "")
        //        {
        //            FilterValues = "All";
        //        }
        //        string currDateTime = await _helpers.GetCurrentDate();
        //        
        //        using (var workbook = new XLWorkbook())
        //        {
        //            var worksheet = workbook.Worksheets.Add("Joining Report");
        //            //int count = model.JoiningReportList.Count + 5;
        //            int count = model.Details.Count + 5;


        //            worksheet.Range("A1:O" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
        //            worksheet.Range("A1:O" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
        //            worksheet.Range("A1:O" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
        //            worksheet.Range("A1:O" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
        //            worksheet.Range("A1:O" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
        //            var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 15)));
        //            wsReportNameHeaderRange.Style.Font.Bold = true;
        //            wsReportNameHeaderRange.Style.Font.FontSize = 15;
        //            wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        //            wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
        //            wsReportNameHeaderRange.Merge();
        //            wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191); ;
        //            wsReportNameHeaderRange.Value = "PT Processing";

        //            worksheet.Cell(2, 1).Style.Font.Bold = true;
        //            worksheet.Cell(2, 1).Value = "Company Name";
        //            worksheet.Cell(2, 2).Value = CompanyName.ToUpper();
        //            worksheet.Cell(2, 3).Style.Font.Bold = true;
        //            worksheet.Cell(2, 3).Value = "Month";
        //            worksheet.Cell(2, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //            worksheet.Cell(2, 4).Value = MonthName;
        //            worksheet.Cell(2, 4).Style.DateFormat.Format = "dd-MMM-yyyy";

        //            worksheet.Cell(2, 5).Style.Font.Bold = true;
        //            worksheet.Cell(2, 5).Value = "Pt Group";
        //            worksheet.Cell(2, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //            worksheet.Cell(2, 6).Value = ptGroupName;

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
        //            worksheet.Cell(3, 6).Value = model.Details.Count;
        //            worksheet.Cell(4, 1).Style.Font.Bold = true;
        //            worksheet.Cell(4, 1).Value = "Filter Applied";
        //            worksheet.Range("B4:O4").Merge();
        //            worksheet.Cell("B4").Value = FilterValues;



        //            var currentRow = 5;
        //            #region Header
        //            worksheet.Range("A5:O5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
        //            worksheet.Cell(currentRow, 1).Value = "Sl.No";
        //            worksheet.Cell(currentRow, 1).Style.Font.Bold = true;

        //            worksheet.Cell(currentRow, 2).Value = "ID";
        //            worksheet.Cell(currentRow, 2).Style.Font.Bold = true;

        //            worksheet.Cell(currentRow, 3).Value = "Employee Name";
        //            worksheet.Cell(currentRow, 3).Style.Font.Bold = true;

        //            worksheet.Cell(currentRow, 4).Value = "PT Appl Sal";
        //            worksheet.Cell(currentRow, 4).Style.Font.Bold = true;

        //            worksheet.Cell(currentRow, 5).Value = "Proc Mo";
        //            worksheet.Cell(currentRow, 5).Style.Font.Bold = true;

        //            worksheet.Cell(currentRow, 6).Value = "Proc Sal";
        //            worksheet.Cell(currentRow, 6).Style.Font.Bold = true;

        //            worksheet.Cell(currentRow, 7).Value = "Not Proc Mo";
        //            worksheet.Cell(currentRow, 7).Style.Font.Bold = true;

        //            worksheet.Cell(currentRow, 8).Value = "Not Proc Sal";
        //            worksheet.Cell(currentRow, 8).Style.Font.Bold = true;

        //            worksheet.Cell(currentRow, 9).Value = "PT Sal";
        //            worksheet.Cell(currentRow, 9).Style.Font.Bold = true;

        //            worksheet.Cell(currentRow, 10).Value = "Basic";
        //            worksheet.Cell(currentRow, 10).Style.Font.Bold = true;

        //            worksheet.Cell(currentRow, 11).Value = "Oth Alws";
        //            worksheet.Cell(currentRow, 11).Style.Font.Bold = true;

        //            worksheet.Cell(currentRow, 12).Value = "PT Amt";
        //            worksheet.Cell(currentRow, 12).Style.Font.Bold = true;

        //            worksheet.Cell(currentRow, 13).Value = "PT Paid";
        //            worksheet.Cell(currentRow, 13).Style.Font.Bold = true;

        //            worksheet.Cell(currentRow, 14).Value = "Curr Mo PT";
        //            worksheet.Cell(currentRow, 14).Style.Font.Bold = true;

        //            worksheet.Cell(currentRow, 15).Value = "Rmng Amt";
        //            worksheet.Cell(currentRow, 15).Style.Font.Bold = true;

        //            worksheet.Cell(currentRow, 16).Value = "Div Period";
        //            worksheet.Cell(currentRow, 16).Style.Font.Bold = true;


        //            #endregion

        //            #region Body
        //            foreach (var item in model.Details)
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
        //                worksheet.Cell(currentRow, 4).Value = item.ActualPTApplicabileSalary;

        //                worksheet.Cell(currentRow, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //                worksheet.Cell(currentRow, 5).Value = item.ProcessedMonth;

        //                worksheet.Cell(currentRow, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //                worksheet.Cell(currentRow, 6).Value = item.ProcessedSalary;

        //                worksheet.Cell(currentRow, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //                worksheet.Cell(currentRow, 7).Value = item.NotProcessedMonth;

        //                worksheet.Cell(currentRow, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //                worksheet.Cell(currentRow, 8).Value = item.NotProcessedSalary;

        //                worksheet.Cell(currentRow, 9).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //                worksheet.Cell(currentRow, 9).Value = item.PTSalary;

        //                worksheet.Cell(currentRow, 10).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //                worksheet.Cell(currentRow, 10).Value = item.Basic;

        //                worksheet.Cell(currentRow, 11).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //                worksheet.Cell(currentRow, 11).Value = item.OtherAllowance;

        //                worksheet.Cell(currentRow, 12).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //                worksheet.Cell(currentRow, 12).Value = item.PTAmount;

        //                worksheet.Cell(currentRow, 13).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //                worksheet.Cell(currentRow, 13).Value = item.PTAlreadyPaid;

        //                worksheet.Cell(currentRow, 14).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //                worksheet.Cell(currentRow, 14).Value = item.CurrentMonthPT;

        //                worksheet.Cell(currentRow, 15).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //                worksheet.Cell(currentRow, 15).Value = item.RemainingAmount;

        //                worksheet.Cell(currentRow, 16).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //                worksheet.Cell(currentRow, 16).Value = item.DividingPeriod;


        //                worksheet.Columns(1, 16).Style.Alignment.WrapText = true;
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
        //                    "PT Processing List.xlsx"
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



        [HttpGet]
        public async Task<IActionResult> GetPTprocessingList(int Month, int PTGroup)
        {
            try
            {

                PTProcessing model = new PTProcessing();
                Filter filter = new Filter();
                model = await _PTProcessingService.GetPTProcessingList(Month, PTGroup, filter);
                return PartialView("_PTProcessingList", model);
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
        public async Task<ActionResult> GetBatchUpdateDetails(int Month, int PTGroup,string Mode,Guid EmployeeGI)
        {
            try
            {
                PTProcessing model = new PTProcessing();
                model = await _PTProcessingService.GetBatchUpdateDetails(Month, PTGroup);
                if(Mode=="EDIT")
                {
                    model.EmployeeHeader = new EmployeeHeader();
                    model.EmployeeHeader = await _commonService.GetEmployeeHeader(EmployeeGI);
                    return PartialView("_Edit", model);
                }
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
        public async Task<IActionResult> ApplyFilter(int Month, int PTGroup, Filter filter)
        {
            try
            {

                PTProcessing model = new PTProcessing();
                Global.GlobalFilter = new Filter();
                model = await _PTProcessingService.GetPTProcessingList(Month, PTGroup, filter);
                return PartialView("~Areas/PayRoll/Views/PTProcessing/_PTProcessingList.cshtml", model);
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
