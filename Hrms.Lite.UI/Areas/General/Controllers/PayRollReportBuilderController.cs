using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.General;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Spire.Xls;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.General.Controllers
{
    [Area("General")]
    [SessionAuthorize]
    public class PayRollReportBuilderController : Controller
    {
        private readonly IReportBuilderService _service;

        private readonly ICommonService _commonService;
        private readonly IDropdownService _dropdownService;
        private readonly IHelpers _helpers;
        public static class Global
        {
            public static ReportBuilder Global_Builder;
            public static DataSet DataSet;
        }

        public PayRollReportBuilderController(IReportBuilderService service, ICommonService commonService, IDropdownService dropdownService, IHelpers helpers)
        {
            _service = service;
            _commonService = commonService;
            _dropdownService = dropdownService;
            _helpers = helpers;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                ReportBuilder model = new ReportBuilder();

            await fill_Head_DropDowns();
            model = await _service.GetPayRollReportBuilder_Fields();
            model.Mode = "Create";
            model.PaySelectionBasis = 1;
                model = await _service.GetPayrollReportBuilder_EditDetails(0);
                model.SliderFilter = await _commonService.GetFilterDropdowns();
            model.SliderFilter.EmployeeWise_Filter = new EmployeeWise_Filter();
            model.SliderFilter.EmployeeWise_Filter.DropdownData = new SelectList(await _dropdownService.GetEmployeeDropDown(), "Code", "Name");

            return View("Create", model);
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

        #region FILL HEAD DROPDOWNS

        public async Task fill_Head_DropDowns()
        {           
          ViewBag.FinancialYear = new SelectList(await _dropdownService.GetFinancialYearDropdown(), "Code", "Name");
            List<SelectListItem> ReportType = new List<SelectListItem>()
                    {
                    new SelectListItem{ Text="Monthly", Value = "1" },
                    new SelectListItem{ Text="Quarterly", Value = "2" },
                    new SelectListItem{ Text="Half Year", Value = "3" },
                    new SelectListItem{ Text="Yearly", Value = "4" },
                    new SelectListItem{ Text="Periodic", Value = "5" }          
                    };
         ViewBag.ReportType = ReportType.ToList();
            List<SelectListItem> BasedOn = new List<SelectListItem>()
                    {
                    new SelectListItem{ Text="Summary", Value = "1" },
                    new SelectListItem{ Text="Detailed", Value = "2" }                   
                    };
            ViewBag.BasedOn = BasedOn.ToList(); ;

            List<SelectListItem> Month = new List<SelectListItem>();
            ViewBag.Month = Month.ToList() ;

            ViewBag.FromMonth = new SelectList(await _dropdownService.GetPayrollReportBuilderMonthDropdown(0,0), "Code", "Name");
            ViewBag.ToMonth = Month.ToList();
        }


        #endregion

        #region Fill Month DP when ReportType Chnage

        public async Task<JsonResult> fillMonthDropdown(int ReportType,int FinancialYrCode)
        {
            object _object= new object();
            if (ReportType == 1 || ReportType==5)  // Monthly
            {
                _object = await _dropdownService.GetPayrollReportBuilderMonthDropdown(FinancialYrCode, ReportType);
            }
            else  // Quearterly, half year
                _object = fillmonth(ReportType);         

            return Json(_object);         
        }
        public async Task<JsonResult> fillToMonthDropdown(int fromMonth)
        {
            var drop = await _dropdownService.GetNextMonthDropDown(fromMonth);

            return Json(drop);
        }
        public  object fillmonth(int ReportType)
        {
            List<SelectListItem> data = new List<SelectListItem>();
            if (ReportType==2)
            {
                data.Add(new SelectListItem { Text = "Q1", Value = "1" });
                data.Add(new SelectListItem { Text = "Q2", Value = "2" });
                data.Add(new SelectListItem { Text = "Q3", Value = "3" });
                data.Add(new SelectListItem { Text = "Q4", Value = "4" });
            }
            else
            {
                data.Add(new SelectListItem { Text = "H1", Value = "1" });
                data.Add(new SelectListItem { Text = "H2", Value = "2" });               
            }
            return data;
        }
        #endregion 
        [HttpPost]
        public async Task<IActionResult> ApplyFilterfullForm(ReportBuilder model, string ReportName, string SaveFlag)
        {

            object response = new object();
            Global.Global_Builder = null;
            model.Details = model.Combinations;

            model.Filter = model.Filter;
            Global.Global_Builder = model;

            if (SaveFlag == "PleaseSave")
            {
                model.ReportName = ReportName;
                response = await _service.PayRollReportBuilder_Save(model);
            }
            else
                response = true;
            return Json(response);
        }

        [HttpPost]
        public IActionResult ApplyFilter_SavedReport(ReportBuilder model, int Re_Code, string Re_name)
        {

            object response = new object();
            Global.Global_Builder = null;
            model.Saved_Report = new Saved_Report();

            model.Filter = model.Filter;
            model.Saved_Report.Code = Re_Code;
            model.Saved_Report.Name = Re_name;
            Global.Global_Builder = model;
            response = true;
            return Json(response);
        }


        public JsonResult fillEditModeCombinGrid_OnApplClick(ReportBuilder model, int E_code, string E_field, string E_fgroup)
        {
            ReportBuilder GRID2 = new ReportBuilder();
            List<ReportBuilderDetails> newlist = new List<ReportBuilderDetails>();
            if (model.Combinations != null)
            {
                int count = model.Combinations.Count;

                if (model.Combinations.Where(u => u.Code == E_code).Any())
                {

                    model.Combinations.RemoveAll(x => x.Code == E_code);
                    newlist = model.Combinations;
                    int order = 1;
                    for (int i = 0; i < newlist.Count; i++)
                    {
                        newlist[i].Order = order;
                        order++;
                    }

                    model.Combinations = newlist;
                }
                else
                {
                    List<ReportBuilderDetails> abc = new List<ReportBuilderDetails>();
                    abc.Add(new ReportBuilderDetails() { Code = E_code, Field = E_field, FieldGroup = E_fgroup, Order = count + 1 });
                    model.Combinations.AddRange(abc);
                }
            }
            else
            {
                model.Combinations = new List<ReportBuilderDetails>();
                List<ReportBuilderDetails> abc = new List<ReportBuilderDetails>();
                abc.Add(new ReportBuilderDetails() { Code = E_code, Field = E_field, FieldGroup = E_fgroup, Order = 1 });
                model.Combinations.AddRange(abc);
            }
            model.Combinations = model.Combinations.OrderBy(x => x.Order).ToList();
            //return PartialView("_CombinationGrid", model);
            return Json(model);
        }
        public async Task<IActionResult> fillEditMOdeCombinGrid(int ReportCode)
        {
            ReportBuilder GRID2 = new ReportBuilder();
            GRID2 = await _service.GetPayrollReportBuilder_EditDetails(ReportCode);
            return PartialView("_CombinationGrid", GRID2);
        }
        public async Task<IActionResult> Edit(int ReportCode)
        {
            ReportBuilder model = new ReportBuilder();
            

            await fill_Head_DropDowns();
            model = await _service.GetPayrollReportBuilder_EditDetails(ReportCode);
            model.ReportCode = ReportCode;
            model.Mode = "Edit";
            model.PaySelectionBasis = 1;
            model.SliderFilter = await _commonService.GetFilterDropdowns();
            model.SliderFilter.EmployeeWise_Filter = new EmployeeWise_Filter();
            model.SliderFilter.EmployeeWise_Filter.DropdownData = new SelectList(await _dropdownService.GetEmployeeDropDown(), "Code", "Name");
            return View("Edit", model);

        }

    
    
        
        public async Task<IActionResult> ViewSavedReports()
        {

            ReportBuilder model = new ReportBuilder();
            model = await _service.GetPayrollReportBuilder_SavedReports();
            return PartialView("_SavedReports", model);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int ReportCode)
        {
            var response = await _service.PayRollReportBuilder_Delete(ReportCode);
            return Json(response);
        }


        [HttpPost]
        public IActionResult fillCombinationGrid(ReportBuilder model)
        {

            var list = from li in model.Details
                       where li.Applicability == true
                       select li;
            model.Combinations = list.ToList();
            int order = 1;
            for (int i = 0; i < model.Combinations.Count; i++)
            {
                model.Combinations[i].Order = order;
                order++;
            }

            return PartialView("_CombinationGrid", model);
            //return Json(model);
        }
        public static string ConvertSubstringToUpperCase(string originalString, int startIndex, int length)
        {
            string upperCaseSubstring = originalString.Substring(startIndex, length).ToUpper();
            return originalString.Substring(0, startIndex) + upperCaseSubstring + originalString.Substring(startIndex + length);
        }

        public async Task<IActionResult> ReportBuilder(string ReportTypeName,string FinancialYearName,string Month,string BasedOn,string PaySelectionBasis,string From,string To)
        {
            try
            {

                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
               
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Payroll Report Builder", "Excel");


                string currDateTime = await _helpers.GetCurrentDate();
                

                int index = 0;
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Payroll Report Builder");
                    var dataset = await _service.GetPayRollReportBuilder(Global.Global_Builder);
                    DataTable dt = dataset.Tables[0];



                        //set EXCEL Column Heading 
                        for (int i = 0; i < dt.Columns.Count; i++)
                        {
                            if (dt.Columns[i].ColumnName == "dob" || dt.Columns[i].ColumnName == "doj" || dt.Columns[i].ColumnName == "dow"|| dt.Columns[i].ColumnName == "ctc"||
                            dt.Columns[i].ColumnName == "pf" || dt.Columns[i].ColumnName == "esi" || dt.Columns[i].ColumnName == "lwf" || dt.Columns[i].ColumnName == "tds"  )
                                 
                           dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.ToUpper();
                        if (dt.Columns[i].ColumnName.Contains("lop"))
                        {
                            index = dt.Columns[i].ColumnName.IndexOf("lop");
                            dt.Columns[i].ColumnName = ConvertSubstringToUpperCase(dt.Columns[i].ColumnName, index, 3);
                        }
                        else
                                dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
                        }

                        int rowcount = dt.Rows.Count + 7;
                        if (ReportTypeName == "Yearly")
                            rowcount = 6;

                        string columnName = "";
                        int Columncount = dt.Columns.Count;

                        while (Columncount > 0)
                        {
                            int modulo = (Columncount - 1) % 26;
                            columnName = Convert.ToChar('A' + modulo) + columnName;
                            Columncount = (Columncount - modulo) / 26;
                        }

                        string HeadingColName;
                        if (columnName == "C")
                            HeadingColName = "D";
                        else
                            HeadingColName = columnName;

                        string colName = columnName;
                        var currentRow = 0;
                        worksheet.Range("A1", HeadingColName + rowcount).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1", HeadingColName + rowcount).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1", HeadingColName + rowcount).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1", HeadingColName + rowcount).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1", HeadingColName + rowcount).Style.Border.LeftBorder = XLBorderStyleValues.Thin;




                        var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, HeadingColName));
                        wsReportNameHeaderRange.Style.Font.Bold = true;
                        wsReportNameHeaderRange.Style.Font.FontSize = 15;
                        wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                        wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                        wsReportNameHeaderRange.Merge();

                        wsReportNameHeaderRange.Value = "Payroll Report Builder";

                        worksheet.Cell(2, 1).Style.Font.Bold = true;
                        worksheet.Cell(2, 1).Value = "Company Name";
                        worksheet.Cell(2, 2).Value = CompanyName.ToUpper();
                        worksheet.Cell(2, 3).Style.Font.Bold = true;
                        worksheet.Cell(2, 3).Value = "Login User";
                        worksheet.Cell(2, 4).Value = LoginUser;




                        worksheet.Cell(3, 1).Style.Font.Bold = true;
                        worksheet.Cell(3, 1).Value = "Date & Time";
                        worksheet.Cell(3, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(3, 2).Style.DateFormat.Format = "dd-MMM-yyyy   h:mm AM/PM";
                        worksheet.Cell(3, 2).Value = currDateTime.ToString();
                        worksheet.Cell(3, 3).Style.Font.Bold = true;
                        worksheet.Cell(3, 3).Value = "Total Count";
                        worksheet.Cell(3, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(3, 4).Value = dt.Rows.Count;

                        worksheet.Cell(4, 1).Style.Font.Bold = true;
                        worksheet.Cell(4, 1).Value = "Report Type";
                        worksheet.Cell(4, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(4, 2).Value = ReportTypeName;
                        if (ReportTypeName != "Periodic")
                        { 
                            worksheet.Cell(4, 3).Style.Font.Bold = true;
                            worksheet.Cell(4, 3).Value = "Financial Year";
                            worksheet.Cell(4, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(4, 4).Value = FinancialYearName;
                        }

                    if (ReportTypeName != "Yearly" && ReportTypeName != "Periodic")
                    {
                        worksheet.Cell(5, 1).Style.Font.Bold = true;
                        worksheet.Cell(5, 1).Value = ReportTypeName;
                        worksheet.Cell(5, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);                        
                        worksheet.Cell(5, 2).Style.DateFormat.Format = "MMM-yyyy";
                        worksheet.Cell(5, 2).Value = Month;
                        worksheet.Cell(5, 3).Style.Font.Bold = true;
                        worksheet.Cell(5, 3).Value = "Based On";
                        worksheet.Cell(5, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(5, 4).Value = BasedOn;

                        worksheet.Cell(6, 1).Style.Font.Bold = true;
                        worksheet.Cell(6, 1).Value = "Pay Selection Basis";
                        worksheet.Cell(6, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(6, 2).Value = PaySelectionBasis;
                        

                        currentRow = 7;
                    }
                    else if (ReportTypeName == "Periodic")
                    {
                        worksheet.Cell(5, 1).Style.Font.Bold = true;
                        worksheet.Cell(5, 1).Value = "Based On";
                        worksheet.Cell(5, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(5, 2).Value = BasedOn;

                        worksheet.Cell(5, 3).Style.Font.Bold = true;
                        worksheet.Cell(5, 3).Value = "From";
                        worksheet.Cell(5, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(5, 4).Style.DateFormat.Format = "MMM-yyyy";
                        worksheet.Cell(5, 4).Value = From;


                        worksheet.Cell(6, 1).Style.Font.Bold = true;
                        worksheet.Cell(6, 1).Value = "To";
                        worksheet.Cell(6, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(6, 2).Style.DateFormat.Format = "MMM-yyyy";
                        worksheet.Cell(6, 2).Value = To;


                        worksheet.Cell(6, 3).Style.Font.Bold = true;
                        worksheet.Cell(6, 3).Value = "Pay Selection Basis";
                        worksheet.Cell(6, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(6, 4).Value = PaySelectionBasis;

                        currentRow = 7;
                    }
                    else
                    {
                        worksheet.Cell(5, 1).Style.Font.Bold = true;
                        worksheet.Cell(5, 1).Value = "Based On";
                        worksheet.Cell(5, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(5, 2).Value = BasedOn;

                        worksheet.Cell(5, 3).Style.Font.Bold = true;
                        worksheet.Cell(5, 3).Value = "Pay Selection Basis";
                        worksheet.Cell(5, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(5, 4).Value = PaySelectionBasis;
                        currentRow = 6;
                    }

                    
                        worksheet.Cell(currentRow, 1).InsertTable(dt);
                        worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                        worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                        worksheet.Range("A"+ currentRow + ":" + colName + currentRow).Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                        worksheet.Range("A"+ currentRow + ":" + colName + currentRow).Style.Font.Bold = true;

                        //worksheet.Cell(5, 1).Style.Font.Bold = true;
                        //worksheet.Cell(5, 1).Value = "Filter Applied";
                        //worksheet.Cell(5, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        //worksheet.Cell(5, 2).Value = "";

                        //var currentRow = 6;
                        //worksheet.Cell(currentRow, 1).InsertTable(dt);
                        //worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                        //worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                        //worksheet.Range("A6:" + colName + "6").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                        //worksheet.Range("A6:" + colName + "6").Style.Font.Bold = true;





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
                                "Payroll Report Builder.xlsx"
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


        public async Task<ActionResult> ReportBuilderAJAX()
        {
            try
            {
                DataSet dataset = new DataSet();
                object ResponseEntity = new object();

                if (Global.Global_Builder.Mode == "FromSavedReports")
                        dataset = await _service.GetPayRollReportBuilder_SavedReport(Global.Global_Builder.Saved_Report.Code, Global.Global_Builder.Filter);
                    else

                        dataset = await _service.GetPayRollReportBuilder(Global.Global_Builder);
                    DataTable dt = dataset.Tables[0];

                    if (dt.Columns.Count ==0 )
                    {
                        //Global.DataSet = dataset;
                        ResponseEntity = false;
                        return Json(ResponseEntity);

                    }
                    else
                    {
                        //Global.DataSet = dataset;
                        ResponseEntity = true;
                        return Json(ResponseEntity);

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
