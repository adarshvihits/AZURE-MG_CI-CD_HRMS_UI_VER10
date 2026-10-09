using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.PayRoll.Reports;
using Hrms.Lite.Shared.Account;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll.Reports;
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
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.PayRoll.Reports.Controllers
{
    [Area("PayRoll")]
    [SessionAuthorize]
    public class PfEcrReportController : Controller
    {

        private readonly IPfEcrReportService _PfEcrReportService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        private readonly IDropdownService _dropdownService;

        public static class Global
        {
            public static Filter GlobalFilter;


        }
        public PfEcrReportController(IPfEcrReportService PfEcrReportService, ICommonService commonService, IHelpers Helpers, IDropdownService dropdownService)
        {
            _PfEcrReportService = PfEcrReportService;
            _commonService = commonService;
            _helpers = Helpers;
            _dropdownService = dropdownService;
        }
        public async Task <IActionResult> Index()
        {
            try
            {
                ViewBag.Month = new SelectList(await _dropdownService.GetMonthDropdown(), "Code", "Name");
                ViewBag.PFGrp = new SelectList(await _dropdownService.GetPFGroupDropdown(), "Code", "Name");
                Filter filter = new Filter();
                PfEcrReport model = new PfEcrReport();
                model.Filter = filter;
                filter.length = 10;
                Global.GlobalFilter = filter;
                model = await _PfEcrReportService.GetList(0, 0, 0, Global.GlobalFilter);
                await fillfilterDropdown();
                return View("~/Areas/PayRoll/Reports/Views/PfEcrReport/Index.cshtml",model);
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

        //public async Task<ActionResult> GetList(int Month, int PFGroup,int format)
        //{
        //    try
        //    {

        //        PfEcrReport model = new PfEcrReport();
        //        if (Global.GlobalFilter == null)
        //            Global.GlobalFilter = new Filter();

        //        if(Month!=0 && PFGroup!=0 && format !=0)
        //        model = await _PfEcrReportService.GetList(Month, PFGroup, format, Global.GlobalFilter);
               
        //        return PartialView("~/Areas/PayRoll/Reports/Views/PfEcrReport/_PFEcrReportList.cshtml", model);

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





        [HttpPost]
        public async Task<IActionResult> GetPfEcrList(Filter filter1, int Month, int PFGroup, int format)
        {
            try
            {
                PfEcrReport model = new PfEcrReport();
                PfEcrReport m = new PfEcrReport();


                Filter filter = new Filter();
                Global.GlobalFilter = filter1;
               

                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }
                if (Month != 0 && PFGroup != 0)
                {
                    model = await _PfEcrReportService.GetList(Month, PFGroup, format, Global.GlobalFilter);
                }
                //model = await _SeparationReportService.GetSeparationReport(filter1.F_From.ToString("yyyy-MMM-dd"), filter1.F_To.ToString("yyyy-MMM-dd"), filter1);

                m.PfEcrSummary = model.PfEcrSummary;
                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.PfEcrSummary == null || model.PfEcrSummary.Count() == 0)
                {


                    return Json(new
                    {
                        data = "",
                        draw = filter1.draw,
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows
                    });
                }

                if (model.PfEcrSummary.Count > 0)
                {
                    totalrows = m.PfEcrSummary[0].active;


                }


                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = m.PfEcrSummary[0].totalRecords;

                    return Json(new
                    {
                        data = model.PfEcrSummary,
                        draw = filter1.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows//totalrowsafterfiltering
                    });
                }
                else
                {

                    return Json(new
                    {
                        data = model.PfEcrSummary,
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






        public async Task<IActionResult> ApplyFilter(int Month,int PFGroup,int format,  Filter filter)
        {
            try
            {

                PfEcrReport model = new PfEcrReport();
                Global.GlobalFilter = new Filter();
        
                model = await _PfEcrReportService.GetList(Month, PFGroup, format, filter);

                return PartialView("~/Areas/PayRoll/Reports/Views/PfEcrReport/_PFEcrReportList.cshtml", model);
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








        public static string ConvertSubstringToUpperCase(string originalString, int startIndex, int length)
        {
            string upperCaseSubstring = originalString.Substring(startIndex, length).ToUpper();
            return originalString.Substring(0, startIndex) + upperCaseSubstring + originalString.Substring(startIndex + length);
        }

        public async Task<IActionResult> ExportToExcel(int Month, int PFGroup, string MonthName, string PFGroupName, int format,int TotalRows)
        {
            try
            {
              
                if (Global.GlobalFilter == null)
                    Global.GlobalFilter = new Filter();
                else
                {
                    Global.GlobalFilter.start = 0;
                    Global.GlobalFilter.length = TotalRows;
                }
                string FilterValues = await _helpers.GetFilterValues(Global.GlobalFilter);

               
                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("PF ECR Report", "Excel");

                if (FilterValues == "")
                {
                    FilterValues = "All";
                }
                string currDateTime = await _helpers.GetCurrentDate();
                

                //Get Dataset
                var dataset = await _PfEcrReportService.GetPfEcrReport_Excel(Month, PFGroup, Global.GlobalFilter, format);
                DataTable dt = dataset.Tables[0];
                int index = 0;

                //set EXCEL Column Heading 
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (dt.Columns[i].ColumnName.Contains("epf"))
                    {
                        index = dt.Columns[i].ColumnName.IndexOf("epf");
                        dt.Columns[i].ColumnName = ConvertSubstringToUpperCase(dt.Columns[i].ColumnName, index, 3);
                    }
                    if (dt.Columns[i].ColumnName.Contains("uan"))
                    {
                        index = dt.Columns[i].ColumnName.IndexOf("uan");
                        dt.Columns[i].ColumnName = ConvertSubstringToUpperCase(dt.Columns[i].ColumnName, index, 3);
                    }

                    if (dt.Columns[i].ColumnName.Contains("eps"))
                    {
                        index = dt.Columns[i].ColumnName.IndexOf("eps");
                        dt.Columns[i].ColumnName = ConvertSubstringToUpperCase(dt.Columns[i].ColumnName, index, 3);
                    }
                    if (dt.Columns[i].ColumnName.Contains("edli"))
                    {
                        index = dt.Columns[i].ColumnName.IndexOf("edli");
                        dt.Columns[i].ColumnName = ConvertSubstringToUpperCase(dt.Columns[i].ColumnName, index, 4);
                    }
                    if (dt.Columns[i].ColumnName.Contains("ncp"))
                    {
                        index = dt.Columns[i].ColumnName.IndexOf("ncp");
                        dt.Columns[i].ColumnName = ConvertSubstringToUpperCase(dt.Columns[i].ColumnName, index, 3);
                    }
                    else
                        dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
                }


                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("PF ECR Report");
                    int count = dt.Rows.Count + 5;


                    worksheet.Range("A1:M" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:M" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:M" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:M" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:M" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 +12)));
                    wsReportNameHeaderRange.Style.Font.Bold = true;
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Merge();
                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191); ;
                    wsReportNameHeaderRange.Value = "PF ECR Report";

                    worksheet.Cell(2, 1).Style.Font.Bold = true;
                    worksheet.Cell(2, 1).Value = "Group Company Name";
                    worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                    worksheet.Cell(2, 3).Style.Font.Bold = true;
                    worksheet.Cell(2, 3).Value = "Month";
                    worksheet.Cell(2, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(2, 4).Value = MonthName;
                    worksheet.Cell(2, 4).Style.DateFormat.Format = "MMM-yyyy";
                    worksheet.Cell(2, 5).Value = "PF Group";
                    worksheet.Cell(2, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(2, 6).Value = PFGroupName;

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
                    worksheet.Range("B4:M4").Merge();
                    worksheet.Cell("B4").Value = FilterValues;



                    var currentRow = 5;
                    worksheet.Cell(currentRow, 1).InsertTable(dt);
                    worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                    worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                    worksheet.Range("A5:M5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Range("A5:M5").Style.Font.Bold = true;


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
                            "PF ECR Report List.xlsx"
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


        //[HttpPost]
        public async Task<IActionResult> Export(int Month, int PFGroup, int format,int totCount)
        {


            PfEcrReport model = new PfEcrReport();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            if (Global.GlobalFilter == null)
                Global.GlobalFilter = new Filter();
            else
            {
                Global.GlobalFilter.start = 0;
                Global.GlobalFilter.length = totCount;
            }
            string FilterValues = await _helpers.GetFilterValues(Global.GlobalFilter);



            var length = Global.GlobalFilter.length;
            //Global.GlobalFilter.length = totCount;
            model = await _PfEcrReportService.GetList(Month, PFGroup, format, Global.GlobalFilter);
            Global.GlobalFilter.length = length;
            for (int i = 0; i < model.PfEcrSummary.Count; i++)
            {
                string UAN = model.PfEcrSummary[i].UAN;
                string Name = model.PfEcrSummary[i].MemberName;
                decimal Gross = model.PfEcrSummary[i].GrossWage;
                decimal EPF = model.PfEcrSummary[i].EpfWage;
                decimal EPS = model.PfEcrSummary[i].EpsWage;
                decimal EDLI = model.PfEcrSummary[i].EdliWage;
                decimal EPF_Contri = model.PfEcrSummary[i].EpfContriRemitted;
                decimal EPS_Contri = model.PfEcrSummary[i].EpsContriRemitted;
                decimal EPF_EPS = model.PfEcrSummary[i].EpfEpsDiffRemitted;
                decimal NCP = model.PfEcrSummary[i].Ncpdays;
                decimal Refund = model.PfEcrSummary[i].RefundAdvance;
                for (int j = 0; j < 1; j++)
                {
                    sb.Append(UAN + "#~#" + Name+ "#~#"+ Gross + "#~#"+ EPF+ "#~#"+ EPS+ "#~#"+ EDLI+ "#~#"+ EPF_Contri+ "#~#"+ EPS_Contri+"#~#"+ EPF_EPS+ "#~#"+ NCP+"#~#"+ Refund);
                    
                }

                sb.Append("\r\n");

            }
            return File(System.Text.Encoding.UTF8.GetBytes(sb.ToString()), "text/csv", "PFEcr.txt");




        }
    }

}

