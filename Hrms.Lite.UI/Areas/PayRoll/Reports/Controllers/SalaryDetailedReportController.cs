using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.PayRoll.Reports;
using Hrms.Lite.Shared.DataBank.Reports;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll.Generic.Master;
using Hrms.Lite.Shared.PayRoll.Reports;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Spire.Xls;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Hrms.Lite.Shared.Account;

namespace Hrms.Lite.UI.Areas.PayRoll.Reports.Controllers
{
    [Area("PayRoll")]
    [SessionAuthorize]
    public class SalaryDetailedReportController : Controller
    {
        private readonly ISalaryDetailedReportService _SalaryDetailedReportService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        private readonly IDropdownService _dropdownService;

        public static class Global
        {
            public static Filter GlobalFilter;
            public static int currmonth;
            public static SalaryReports model1;

        }

        public SalaryDetailedReportController(ISalaryDetailedReportService SalaryDetailedReportService, ICommonService commonService, IHelpers Helpers, IDropdownService dropdownService)
        {
            _SalaryDetailedReportService = SalaryDetailedReportService;
            _commonService = commonService;
            _helpers = Helpers;
            _dropdownService = dropdownService;
        }
        public async Task <IActionResult> Index()
        {
            try
            { 
            ViewBag.SAP = new SelectList(await _dropdownService.GetMonthDropdown(), "Code", "Name");
            SalaryReports model = new SalaryReports();
            Filter filter = new Filter();
            DataTable dt = new DataTable();

            var currMonth = await _commonService.GetCurrentMonthDropdown();
            Global.currmonth= currMonth.Data;

            model.PayrollMonth = new MasterBase();
            model.PayrollMonth.Code = currMonth.Data;
            //Pass Grid settings 
            SalaryReports GridSettings = new SalaryReports();
            GridSettings = await _SalaryDetailedReportService.GetPopUp();

            model.AlwSettings = GridSettings.AlwSettings;
            model.DedSettings = GridSettings.DedSettings;
            string allowanceIds = string.Join(",", model.AlwSettings.Where(a => a.Applicability == true).Select(a => a.AllowanceCode));
            string deductionIds = string.Join(",", model.DedSettings.Where(a => a.Applicability == true).Select(a => a.DeductionCode));

            model.allowanceIds = allowanceIds;
            model.deductionIds=deductionIds;

            model.Filter = filter;

            // model = await _SalaryDetailedReportService.GetList(model);

            var ds = await _SalaryDetailedReportService.GetSalaryDetailedHeaders( model,currMonth.Data ,allowanceIds,deductionIds);
            dt = ds.Tables[0];
            ModifyColumnHeaders(dt);

            var tupleModel = new Tuple<SalaryReports, System.Data.DataTable>(model, dt);

            model.PayrollMonth = new MasterBase();
            model.PayrollMonth.Code = currMonth.Data;

             await fillfilterDropdown();

            
            return View("~/Areas/PayRoll/Reports/Views/SalaryDetailedReport/Index.cshtml", tupleModel);
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

        void ModifyColumnHeaders(DataTable dataTable)
        {
            foreach (DataColumn column in dataTable.Columns)
            {
                string columnName = column.ColumnName;
                string modifiedColumnName = ModifyColumnName(columnName);
                column.ColumnName = modifiedColumnName;
            }
        }
        string ModifyColumnName(string columnName)
        {
            string modifiedColumnName = columnName;

            if (!string.IsNullOrEmpty(columnName))
            {
                char firstLetter = columnName[0];
                char modifiedFirstLetter = char.ToUpper(firstLetter);
                modifiedColumnName = modifiedFirstLetter + columnName.Substring(1);
            }

            return modifiedColumnName;
        }

        DefaultContractResolver contractResolver = new DefaultContractResolver
        {
            NamingStrategy = new CamelCaseNamingStrategy
            {
                OverrideSpecifiedNames = true
            }
        };
        public async Task<IActionResult> GetSalaryDetailedReport([DataSourceRequest] DataSourceRequest request, Filter filter1, int SalaryPeriod, string allowance, string deduction)
        {
            if (filter1.searchValue != null)
            {
                filter1.start = 0;
                filter1.length = request.PageSize;
            }
            else
            {

                filter1.start = (request.Page - 1) * request.PageSize;
                filter1.length = request.PageSize;
            }

            SalaryReports model = new SalaryReports();
            model.Filter = filter1;
            Global.GlobalFilter = filter1;

            var ds = await _SalaryDetailedReportService.GetSalaryDetailedReport(SalaryPeriod, model,allowance,deduction);
            DataTable dt = new DataTable();
           

            dt = ds.Tables[0];
           
            ModifyColumnHeadersData(dt);
            request.PageSize = 0;

            var dataResult1 = dt.ToDataSourceResult(request);

            dataResult1.Total = Convert.ToInt32(ds.Tables[1].Rows[0]["ACTIVE"].ToString());

            return Json(dataResult1, new JsonSerializerSettings { ContractResolver = new DefaultContractResolver() });



        }

        void ModifyColumnHeadersData(DataTable dataTable)
        {
            foreach (DataColumn column in dataTable.Columns)
            {
                string columnName = column.ColumnName;
                string modifiedColumnName = "";

                modifiedColumnName = ModifyColumnName(columnName);


                column.ColumnName = modifiedColumnName;
            }
        }

        public ActionResult ChildGrid()
        {
       
            return PartialView("~/Areas/PayRoll/Reports/Views/SalaryDetailedReport/_Settings.cshtml");
        }


        public async Task<IActionResult> ReloadDiv(int SalaryPeriod, string allowance, string deduction)
        {
            try
            {
               
               
                SalaryReports model = new SalaryReports();
                Filter filter = new Filter();
                await fillfilterDropdown();
                model.Filter = filter;
                if(model.Filter.length==0)
                {
                    model.Filter.length = 10;
                }
                DataTable dt = new DataTable();
                var ds = await _SalaryDetailedReportService.GetSalaryDetailedHeaders(model,SalaryPeriod,allowance, deduction);
                dt = ds.Tables[0];
                ModifyColumnHeaders(dt);
                var tupleModel = new Tuple<SalaryReports, System.Data.DataTable>(model, dt);
                return PartialView("~/Areas/PayRoll/Reports/Views/SalaryDetailedReport/_SalaryReportsList.cshtml", tupleModel);
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


        public async Task<IActionResult> ApplyFilter(Filter filter,int SalPeriod)
        {
            try
            {

                SalaryReports model = new SalaryReports();
                model.PayrollMonth = new MasterBase();
                model.PayrollMonth.Code = SalPeriod;
                model.Filter = filter;
                Global.GlobalFilter = new Filter();

                model = await _SalaryDetailedReportService.GetList(model);

                return PartialView("~/Areas/PayRoll/Reports/Views/SalaryDetailedReport/_SalaryReportsList.cshtml", model);
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

        public async Task<ActionResult> GetList(int salCode)
        {
            try
            {

                SalaryReports model = new SalaryReports();
                if (Global.GlobalFilter == null)
                    Global.GlobalFilter = new Filter();
                model.PayrollMonth = new PayrollMonth();
                model.PayrollMonth.Code = salCode;


                Global.model1=model;

                model = await _SalaryDetailedReportService.GetList(model);
                
                    return PartialView("~/Areas/PayRoll/Reports/Views/SalaryDetailedReport/_SalaryReportsList.cshtml", model);
               
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
        public async Task<IActionResult> GetPopUp()
        {
            try
            {
                SalaryReports model = new SalaryReports();

                model = await _SalaryDetailedReportService.GetPopUp();
               

                return PartialView("~/Areas/PayRoll/Reports/Views/SalaryDetailedReport/_GridSettingsPopUp.cshtml", model);
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

        public async Task<IActionResult> GetAlwSettings([DataSourceRequest] DataSourceRequest request)
        {
            SalaryReports model = new SalaryReports();
            List<AlwSettings> modellist = new List<AlwSettings>();

            model = await _SalaryDetailedReportService.GetPopUp();

            modellist = model.AlwSettings;


            var dataResult1 = modellist.ToDataSourceResult(request);
          

            return Json(dataResult1, new JsonSerializerSettings { ContractResolver = new DefaultContractResolver() });
        }
        public async Task<IActionResult> GetDedSettings([DataSourceRequest] DataSourceRequest request)
        {
            SalaryReports model = new SalaryReports();
            List<DedSettings> modellist = new List<DedSettings>();

            model = await _SalaryDetailedReportService.GetPopUp();

            modellist = model.DedSettings;


            var dataResult1 = modellist.ToDataSourceResult(request);


            return Json(dataResult1, new JsonSerializerSettings { ContractResolver = new DefaultContractResolver() });
        }

        [HttpPost]
        public async Task<ActionResult> FilterColumn(SalaryReports model,int salCode)
        {
            try
            {

               
                Global.GlobalFilter = new Filter();
                model.PayrollMonth = new PayrollMonth();
                model.PayrollMonth.Code = salCode;
               

                var list1 = from li in model.Allowances
                           where li.Applicable == true
                           select li;
                var lis2 = from li in model.Deductions
                           where li.Applicable == true
                           select li;

               model.Allowances= list1.ToList();
               model.Deductions = lis2.ToList();
                Global.model1= model;
                model = await _SalaryDetailedReportService.GetList(model);
                return PartialView("~/Areas/PayRoll/Reports/Views/SalaryDetailedReport/_SalaryReportsList.cshtml", model);
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

        public async Task<IActionResult> ExportToExcel(int SalPeriod, string SalPeriodName, string allowance, string deduction, int Total)
        {
            try
            {

              
                if (Global.GlobalFilter == null)
                    Global.GlobalFilter = new Filter();
              
                string FilterValues = await _helpers.GetFilterValues(Global.GlobalFilter);
            

                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Salary Detailed Report(" + SalPeriodName + ")", "Excel");

                if (FilterValues == "")
                {
                    FilterValues = "All";
                }
                string currDateTime = await _helpers.GetCurrentDate();
                


                SalaryReports model = new SalaryReports();
                model.PayrollMonth = new PayrollMonth();
                model.PayrollMonth.Code = SalPeriod;
                model.Filter = Global.GlobalFilter;
              
                var length = Global.GlobalFilter.length;
                Global.GlobalFilter.length = Total;
                var dataset = await _SalaryDetailedReportService.GetSalaryDetailedReport(SalPeriod, model, allowance, deduction);
                Global.GlobalFilter.length = length;
                
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
                    if (dt.Columns[i].ColumnName == "order" || dt.Columns[i].ColumnName == "totalCount" || dt.Columns[i].ColumnName == "Active" )
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
                    var worksheet = workbook.Worksheets.Add("Salary Detailed Report");
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
                    wsReportNameHeaderRange.Value = "Salary Detailed Report";

                    worksheet.Cell(2, 1).Style.Font.Bold = true;
                    worksheet.Cell(2, 1).Value = "Group Company Name";
                    worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                    worksheet.Cell(2, 3).Style.Font.Bold = true;
                    worksheet.Cell(2, 3).Value = "Salary Period";
                    worksheet.Cell(2, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(2, 4).Value = SalPeriodName;
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
                            book.Protect(EP.Password,false,false);
                            book.SaveToStream(stream);
                        }
                        var content = stream.ToArray();
                        return File(
                            content,
                            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                            "Salary Detailed Report List.xlsx"
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
        public async Task<IActionResult> GetSalaryDetailedRptPrint(int SalaryPeriod, string Allowance, string Deduction)
        {

            if (Global.GlobalFilter == null)
                Global.GlobalFilter = new Filter();

            string FilterValues = await _helpers.GetFilterValues(Global.GlobalFilter);

            if (FilterValues == "")
            {
                FilterValues = "All";
            }

            SalaryReports model = new SalaryReports();
            model.Filter = Global.GlobalFilter;

            model.PayrollMonth = new MasterBase();
            model.PayrollMonth.Code = SalaryPeriod;
            model.Url = await _commonService.GetUrl();
            model.allowanceIds = Allowance;
            model.deductionIds = Deduction;
      

            Hrms.Lite.Shared.Account.Login CurrentUser = JsonConvert.DeserializeObject<Hrms.Lite.Shared.Account.Login>(HttpContext.Session.GetString("User"));
            model.UserId = CurrentUser.authUser.UserCode;
            model.CompanyId = CurrentUser.authUser.CompanyID;
            model.GroupCompanyId = CurrentUser.GroupCompanyCode;


            return View("~/Areas/PayRoll/Reports/Views/SalaryDetailedReport/Print.cshtml", model);
        }
    }
}
