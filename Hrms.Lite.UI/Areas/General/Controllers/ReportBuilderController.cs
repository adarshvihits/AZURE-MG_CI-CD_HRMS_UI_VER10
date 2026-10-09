using ClosedXML.Excel;
using DocumentFormat.OpenXml.InkML;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.Account;
using Hrms.Lite.Shared.General;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Spire.Xls;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.General.Controllers
{
    [Area("General")]
    [SessionAuthorize]
   
    //DATABANK REPORT BUILDER
    public class ReportBuilderController : Controller
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
       
        public ReportBuilderController(IReportBuilderService service, ICommonService commonService, IDropdownService dropdownService, IHelpers helpers)
        {
            _service = service;          
            _commonService = commonService;
            _dropdownService = dropdownService;
            _helpers = helpers;
        }
        public async Task<IActionResult> Index()
        {
            ReportBuilder model = new ReportBuilder();
            model = await _service.GetDataBankReportBuilder_Fields();
            model.SliderFilter = await _commonService.GetFilterDropdowns();
            model.Mode = "Create";
            model.Tab = 1;
            return View("Create", model);
        }
        public async Task<IActionResult> Create()
        {
            ReportBuilder model = new ReportBuilder();
            model.SliderFilter = await _commonService.GetFilterDropdowns();
            return View("KendoCreate",model);
        }
        [HttpPost]
        public async Task<IActionResult> ApplyFilterfullForm(ReportBuilder model, string ReportName)
        {

            object response = new object();
            Global.Global_Builder = null;           
            model.Details = model.Combinations;
           
            model.Filter = model.Filter;            
            Global.Global_Builder = model;

            if (ReportName != "undefined")
            {
                model.ReportName = ReportName;
                response = await _service.DataBankReportBuilder_Save(model);
            }
            else
                response = true;
            return Json(response);
        }




        [HttpPost]
        public IActionResult ApplyFilter_SavedReport(ReportBuilder model,int Re_Code, string Re_name)
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
            List <ReportBuilderDetails> newlist= new List<ReportBuilderDetails>();
            if (model.Combinations != null)
            {
                int count = model.Combinations.Count;

                if (model.Combinations.Where(u => u.Code == E_code).Any())
                {

                    model.Combinations.RemoveAll(x => x.Code == E_code);
                    newlist = model.Combinations;
                    int order = 1;
                    for (int i = 0; i <newlist.Count; i++)
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
            GRID2 = await _service.GetDataBankReportBuilder_EditDetails(ReportCode);
            return PartialView("_CombinationGrid", GRID2);
        }
        public async Task<IActionResult> Edit(int ReportCode)
        {
            ReportBuilder model = new ReportBuilder();
            model = await _service.GetDataBankReportBuilder_EditDetails(ReportCode);
            model.SliderFilter = await _commonService.GetFilterDropdowns();
            model.ReportCode = ReportCode;
            model.Mode = "Edit";
            return View("Edit", model);

        }



        //[HttpPost]
        //public async Task<IActionResult> ApplyFilter(string CombinationDetails, MultiSelectFilter Filter, string ReportName)
        //{
        //    ReportBuilder model = new ReportBuilder();
        //    object response = new object();
        //    List<ReportBuilderDetails> data = JsonConvert.DeserializeObject<List<ReportBuilderDetails>>(CombinationDetails);
        //    model.Details = data;
        //    int order = 1;
        //    for (int i = 0; i < model.Details.Count; i++)
        //    {
        //        model.Details[i].Order = order;
        //        order++;
        //    }
        //    model.Filter = Filter;
        //    ;
        //    Global.Global_Builder = model;

        //    if (ReportName != "undefined")
        //    {
        //        model.ReportName = ReportName;
        //        response = await _service.DataBankReportBuilder_Save(model);
        //    }
        //    else
        //        response = true;
        //    return Json(response);
        //}
        public async Task<IActionResult> Data([DataSourceRequest] DataSourceRequest request)
        {
            ReportBuilder model = new ReportBuilder();           
                model = await _service.GetDataBankReportBuilder_Fields();          
            var dataResult1 = model.Details.ToDataSourceResult(request);
            
            //   var dataResult = model.EmployeeProfileList.ToDataSourceResult(request);

            // dataResult.Total = model.ActiveCount;
             return Json(dataResult1, new JsonSerializerSettings { ContractResolver = new DefaultContractResolver() });
        }

        public IActionResult DataEmpty([DataSourceRequest] DataSourceRequest request)
        {
            ReportBuilder model = new ReportBuilder();
            model.Details = new List<ReportBuilderDetails>();
            var dataResult1 = model.Details.ToDataSourceResult(request);

          
            return Json(dataResult1, new JsonSerializerSettings { ContractResolver = new DefaultContractResolver() });
        }

        public async Task fillfilterDropdown()
        {

            ViewBag.Gender = new SelectList(await _dropdownService.GetGenderDropDown(), "Code", "Name");
            ViewBag.Category = new SelectList(await _dropdownService.GetCategoryDropdown(), "Code", "Name");
            ViewBag.Grade = new SelectList(await _dropdownService.GetGradeDropdown(), "Code", "Name");
            ViewBag.Designation = new SelectList(await _dropdownService.GetDesignationDropdown(), "Code", "Name");
            ViewBag.Department = new SelectList(await _dropdownService.GetDepartmentDropdown(), "Code", "Name");
            ViewBag.Location = new SelectList(await _dropdownService.GetLocationDropdown(), "Code", "Name");
            ViewBag.PayrollGroup = new SelectList(await _dropdownService.GetPayrollGroupDropdown(), "Code", "Name");

            ViewBag.Division = new SelectList(await _dropdownService.GetDivisionDropdown(), "Code", "Name");
            ViewBag.Section = new SelectList(await _dropdownService.GetSectionDropdown(), "Code", "Name");
            ViewBag.EmploymentType = new SelectList(await _dropdownService.GetEmploymentTypeDropdown(), "Code", "Name");
            ViewBag.EmploymentStatus = new SelectList(await _dropdownService.GetEmploymentStatusDropdown(), "Code", "Name");
            ViewBag.WorkLocation = new SelectList(await _dropdownService.GetWorkLocationDropdown(), "Code", "Name");
            ViewBag.WageGrade = new SelectList(await _dropdownService.GetWageGradeDropdown(), "Code", "Name");
            ViewBag.DateType = new SelectList(await _dropdownService.GetDateFilter(), "Code", "Name");
            ViewBag.Operators = new SelectList(await _dropdownService.GetOperators(), "Code", "Name");

        }
        public async Task<IActionResult> ViewSavedReports()
        {

            ReportBuilder model = new ReportBuilder();
            model= await _service.GetDataBankReportBuilder_SavedReports();         
            return PartialView("_SavedReports", model);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int ReportCode)
        {           
            var response  = await _service.DataBankReportBuilder_Delete(ReportCode);
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



        public async Task<ActionResult> ReportBuilderAJAX()
        {
            try
            {
                DataSet dataset = new DataSet();
                object ResponseEntity = new object();

                if (Global.Global_Builder.Mode == "FromSavedReports")
                    dataset = await _service.GetDataBankReportBuilder_SavedReport(Global.Global_Builder.Saved_Report.Code, Global.Global_Builder.Filter);
                else

                    dataset = await _service.GetDataBankReportBuilder(Global.Global_Builder);
                DataTable dt = dataset.Tables[0];

                if (dt.Columns.Count == 0)
              
                {
                    Global.DataSet = dataset;
                    ResponseEntity = false;
                    return Json(ResponseEntity);

                }
                else
                {
                    Global.DataSet = dataset;
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







        public async Task<IActionResult> ReportBuilder()
        {
            try
            {

                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                DataSet dataset = new DataSet();
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Databank Report Builder", "Excel");


                string currDateTime = await _helpers.GetCurrentDate();
                


                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Databank Report Builder");

                    if (Global.Global_Builder.Mode == "FromSavedReports")
                        dataset = await _service.GetDataBankReportBuilder_SavedReport(Global.Global_Builder.Saved_Report.Code, Global.Global_Builder.Filter);
                    else

                        dataset = await _service.GetDataBankReportBuilder(Global.Global_Builder);
                    DataTable dt = dataset.Tables[0];


                    //set EXCEL Column Heading 
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        if (dt.Columns[i].ColumnName == "dob" || dt.Columns[i].ColumnName == "doj" || dt.Columns[i].ColumnName == "dow")
                            dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.ToUpper();
                        else
                            dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
                    }

                    int rowcount = dt.Rows.Count + 5;

                    string columnName = "";
                    int Columncount = dt.Columns.Count;

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
                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                    wsReportNameHeaderRange.Merge();

                    wsReportNameHeaderRange.Value = "Report Builder";

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
                    worksheet.Cell(4, 1).Style.Font.Bold = true;
                    worksheet.Cell(4, 1).Value = "Total Count";
                    worksheet.Cell(4, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(4, 2).Value = dt.Rows.Count;
                
                  

                    var currentRow = 5;
                    worksheet.Cell(currentRow, 1).InsertTable(dt);
                    worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                    worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                    worksheet.Range("A5:" + colName + "5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Range("A5:" + colName + "5").Style.Font.Bold = true;

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
                            "Databank Report Builder.xlsx"
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
