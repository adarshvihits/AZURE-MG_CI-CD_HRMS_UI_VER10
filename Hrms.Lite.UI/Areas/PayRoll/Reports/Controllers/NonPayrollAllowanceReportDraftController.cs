using ClosedXML.Excel;
using DocumentFormat.OpenXml.EMMA;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.PayRoll.Reports;
using Hrms.Lite.Services.Services.PayRoll.Reports;
using Hrms.Lite.Shared.Account;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using Hrms.Lite.Shared.PayRoll.Generic.Master;
using Hrms.Lite.Shared.PayRoll.Reports;
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

namespace Hrms.Lite.UI.Areas.PayRoll.Reports.Controllers
{
    [Area("PayRoll")]
    [SessionAuthorize]
    public class NonPayrollAllowanceDraftReportController : Controller
    {
        private readonly INonPayrollAllowanceReportService _NonPayrollAllowanceReportService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        private readonly IDropdownService _dropdownService;

       
        public NonPayrollAllowanceDraftReportController(INonPayrollAllowanceReportService NonPayrollAllowanceReportService, ICommonService commonService, IHelpers Helpers, IDropdownService dropdownService)
        {
            _NonPayrollAllowanceReportService = NonPayrollAllowanceReportService;
            _commonService = commonService;
            _helpers = Helpers;
            _dropdownService = dropdownService;
        }

        public async Task <IActionResult> Index()
        {
            SalaryReports model = new SalaryReports();
            ViewBag.month = new SelectList(await _dropdownService.GetMonthDropdown(), "Code", "Name");
            DataTable dt = new DataTable();
            Filter filter = new Filter();
            model.Filter = new Filter();
            model.PayrollMonth = new PayrollMonth();
            
            var currMonth = await _commonService.GetCurrentMonthDropdown();
            int currentmonth = Convert.ToInt32(currMonth.Data);
            model.PayrollMonth.Code = currentmonth;
            //  model = await _NonPayrollAllowanceReportService.GetList(model);
           
          
            model.Filter = filter;
            var ds = await _NonPayrollAllowanceReportService.GetNonPayrollDetailedReportHeader(currMonth.Data,model);
            dt = ds.Tables[0];
            ModifyColumnHeaders(dt);

            var tupleModel = new Tuple<SalaryReports, System.Data.DataTable>(model, dt);


            model.PayrollMonth = new PayrollMonth();
            model.PayrollMonth.Code = currentmonth;
            model.Filter = await _commonService.GetFilterDropdowns();

            return View("~/Areas/PayRoll/Reports/Views/NonPayrollAllowanceDraftReport/Index.cshtml", tupleModel);
        }


        public async Task<IActionResult> GetNonPayrollDetailedDraftReportData([DataSourceRequest] DataSourceRequest request, Filter filter1, int SalaryPeriod)
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
         

            var ds = await _NonPayrollAllowanceReportService.GetNonPayrollDetailedReportDraftData(SalaryPeriod, model);
            DataTable dt = new DataTable();


            dt = ds.Tables[0];

            ModifyColumnHeadersData(dt);
            request.PageSize = 0;

            var dataResult1 = dt.ToDataSourceResult(request);

            dataResult1.Total = Convert.ToInt32(ds.Tables[1].Rows[0]["ACTIVE"].ToString());

            return Json(dataResult1, new JsonSerializerSettings { ContractResolver = new DefaultContractResolver() });



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
        DefaultContractResolver contractResolver = new DefaultContractResolver
        {
            NamingStrategy = new CamelCaseNamingStrategy
            {
                OverrideSpecifiedNames = true
            }
        };

        public ActionResult ChildGrid()
        {

            return PartialView("~/Areas/PayRoll/Reports/Views/NonPayrollAllowanceDraftReport/_Settings.cshtml");
        }
        public async Task<IActionResult> GetAlwSettings([DataSourceRequest] DataSourceRequest request)
        {
            SalaryReports model = new SalaryReports();
            List<AlwSettings> modellist = new List<AlwSettings>();

            model = await _NonPayrollAllowanceReportService.GetPopUp();

            modellist = model.AlwSettings;


            var dataResult1 = modellist.ToDataSourceResult(request);


            return Json(dataResult1, new JsonSerializerSettings { ContractResolver = new DefaultContractResolver() });
        }

        public async Task<IActionResult> ReloadDiv(int SalaryPeriod)
        {
            try
            {


                SalaryReports model = new SalaryReports();
                Filter filter = new Filter();
              
                model.Filter = filter;
                if (model.Filter.length == 0)
                {
                    model.Filter.length = 10;
                }
                DataTable dt = new DataTable();
                var ds = await _NonPayrollAllowanceReportService.GetNonPayrollDetailedReportHeader(SalaryPeriod,model);
                dt = ds.Tables[0];
                ModifyColumnHeaders(dt);
                var tupleModel = new Tuple<SalaryReports, System.Data.DataTable>(model, dt);
                model.Filter = await _commonService.GetFilterDropdowns();
                return PartialView("~/Areas/PayRoll/Reports/Views/NonPayrollAllowanceDraftReport/_List.cshtml", tupleModel);
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

        public async Task<ActionResult> GetList(int monthCode)
        {
            try
            {

                SalaryReports model = new SalaryReports();
                model.PayrollMonth = new PayrollMonth();
                model.PayrollMonth.Code = monthCode;

                model = await _NonPayrollAllowanceReportService.GetDraftList(model);

                return PartialView("~/Areas/PayRoll/Reports/Views/NonPayrollAllowanceDraftReport/_NonPayrollAllowanceList.cshtml", model);

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

                model = await _NonPayrollAllowanceReportService.GetPopUp();


                return PartialView("~/Areas/PayRoll/Reports/Views/NonPayrollAllowanceDraftReport/_GridSettings.cshtml", model);
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
        public async Task<ActionResult> FilterColumn(SalaryReports model, int monthCode)
        {
            try
            {


              
                model.PayrollMonth = new PayrollMonth();
                model.PayrollMonth.Code = monthCode;


                var list = from li in model.Allowances
                            where li.Applicable == true
                            select li;
             

                model.Allowances = list.ToList();

                model = await _NonPayrollAllowanceReportService.GetList(model);
                return PartialView("~/Areas/PayRoll/Reports/Views/NonPayrollAllowanceDraftReport/_NonPayrollAllowanceList.cshtml", model);
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


       
        public async Task<IActionResult> ApplyFilter(int monthCode,Filter filter)
        {
            try
            {

                SalaryReports model = new SalaryReports();               
           
                model.PayrollMonth = new PayrollMonth();
                model.PayrollMonth.Code = monthCode;
                model.Filter = filter;
                model = await _NonPayrollAllowanceReportService.GetList(model);

                return PartialView("~/Areas/PayRoll/Reports/Views/NonPayrollAllowanceDraftReport/_NonPayrollAllowanceList.cshtml", model); 
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


        public async Task<IActionResult> ExportToExcel(int Month, string MonthName,int TotRec,Filter filter)
        {
            try
            {
                SalaryReports _model = new SalaryReports();
                filter.length = TotRec;
                filter.start = 0;

                string FilterValues = await _helpers.GetFilterValues(filter);
                _model.PayrollMonth = new PayrollMonth();
                _model.PayrollMonth.Code = Month;
                _model.Filter = filter;
               

                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("NonPayroll Allowance Report Draft", "Excel");

                if (FilterValues == "")
                {
                    FilterValues = "All";
                }
                string currDateTime = await _helpers.GetCurrentDate();
                



                var dataset = await _NonPayrollAllowanceReportService.GetNonPayrollDetailedReportDraftData(Month,_model);//await _NonPayrollAllowanceReportService.GetMonthlyGratuityDetailedReport_Excel(_model);
                DataTable dt =  new DataTable();
                 dt=   dataset.Tables[0];


                List<string> columnsToRemove = new List<string>
{
    "Ppempcode", // Replace with the actual column names you want to remove
    "Ppeempcode",
     "Order",
     "ACTIVE",
     "TotalCount",
    // Add more column names if needed
};

                // Loop through the list of column names and remove them from the DataTable
                foreach (string columnName in columnsToRemove)
                {
                    if (dt.Columns.Contains(columnName))
                    {
                        dt.Columns.Remove(columnName);
                    }
                }


                //set EXCEL Column Heading 
                for (int i = 0; i < dt.Columns.Count; i++)
                {                    
                 dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
                }


                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("NonPayrollAllowance ReportDraft");
                    int Rowcount = dt.Rows.Count+ 5;
                    
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
                    wsReportNameHeaderRange.Value = "NonPayroll Allowance Report Draft";

                    worksheet.Cell(2, 1).Style.Font.Bold = true;
                    worksheet.Cell(2, 1).Value = "Group Company Name";
                    worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                    worksheet.Cell(2, 3).Style.Font.Bold = true;
                    worksheet.Cell(2, 3).Value = "Month";
                    worksheet.Cell(2, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(2, 4).Value = MonthName;
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
                    worksheet.Cell(3, 6).Value = TotRec;
                    worksheet.Cell(4, 1).Style.Font.Bold = true;
                    worksheet.Cell(4, 1).Value = "Filter Applied";
                    worksheet.Range("B4",colName+4).Merge();
                    worksheet.Cell("B4").Value = FilterValues;



                    var currentRow = 5;

                    worksheet.Cell(currentRow, 1).InsertTable(dt);
                    worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                    worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                    worksheet.Range("A5:"+ colName+"5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
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
                            " Non Payroll Allowance Report Draft.xlsx"
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
