using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.DataBank.Reports;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.DataBank.Reports;
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
using Telerik.SvgIcons;
using Filter = Hrms.Lite.Shared.General.Filter;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.Reflection.Metadata.Ecma335;
using DocumentFormat.OpenXml.EMMA;
using Hrms.Lite.Shared.Account;

namespace Hrms.Lite.UI.Areas.DataBank.Reports.Controllers
{
    [Area("DataBank")]
    [SessionAuthorize]
    public class SalaryStructureReportController : Controller
    {
        private readonly ISalaryStructureReportService _SalaryStructureReportService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        private readonly IDropdownService _dropdownService;

        

        public SalaryStructureReportController(ISalaryStructureReportService SalaryStructureReportService, ICommonService commonService,IHelpers Helpers, IDropdownService dropdownService)
        {
            _SalaryStructureReportService = SalaryStructureReportService;
            _commonService = commonService;
            _helpers = Helpers;
            _dropdownService = dropdownService;
        }

        public async Task<IActionResult> Index()
        {
            try { 
            SalaryStructureReport model = new SalaryStructureReport();
            Filter filter = new Filter();
            DataTable dt = new DataTable();
                model.SliderFilter = await _commonService.GetFilterDropdowns();
                //model = await _SalaryStructureReportService.GetList(0, filter);
                var ds = await _SalaryStructureReportService.GetSalaryStructureReportHeaders(0,filter);
                dt = ds.Tables[0];
                ModifyColumnHeaders(dt);
                model.ActiveCount = Convert.ToInt32(dt.Rows[0]["ACTIVE"].ToString());
                model.ResignedCount = Convert.ToInt32(dt.Rows[0]["RESIGNED"].ToString());

                var tupleModel = new Tuple<SalaryStructureReport, System.Data.DataTable>(model, dt);

                return View("~/Areas/DataBank/Reports/Views/SalaryStructureReport/Index.cshtml", tupleModel);
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

        [HttpPost]
        public async Task<IActionResult> GetSalaryStructureReportList(Filter filter1,int TabIndex)
        {
            try
            {
                SalaryStructureReport model = new SalaryStructureReport();
                SalaryStructureReport m = new SalaryStructureReport();
                //model.EmployeeHeader = new EmployeeHeader();


                Filter filter = new Filter();
                model.SliderFilter = filter1;


                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }

                model = await _SalaryStructureReportService.GetSalaryStructureList(TabIndex, filter1);

                m.SalaryStructureReportDetails = model.SalaryStructureReportDetails;
                int totalrows = 0;
                int totalrowsafterfiltering = 0;
                //Global.ActiveCount = model.ActiveCount;
                //Global.ResignedCount = model.ResignedCount;

                if (model.SalaryStructureReportDetails == null || model.SalaryStructureReportDetails.Count() == 0)
                {
                    //model.RequestVsAuthorityList = new List<RequestVsAuthorityList>();
                    //model.RequestVsAuthorityList.Add(new RequestVsAuthorityList());


                    return Json(new
                    {
                        data = "",
                        draw = filter1.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        Count = model,
                        recordsFiltered = totalrows
                    });
                }



                if (model.SalaryStructureReportDetails.Count > 0)
                {

                    if (filter1.Tabindex == 0)
                    {
                        totalrows = m.SalaryStructureReportDetails[0].active;
                    }
                    else
                    {
                        totalrows = model.ResignedCount;
                    }


                }





                //if (model.SalaryStructureReportDetails.Count > 0)
                //{
                //    totalrows = m.SalaryStructureReportDetails[0].active;


                //}

                //model.AllTabCount = m.AllTabCount;
                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = m.SalaryStructureReportDetails[0].totalRecords;
                    return Json(new
                    {
                        data = model.SalaryStructureReportDetails,
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
                        data = model.SalaryStructureReportDetails,
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

        DefaultContractResolver contractResolver = new DefaultContractResolver
        {
            NamingStrategy = new CamelCaseNamingStrategy
            {
                OverrideSpecifiedNames = true
            }
        };
        public async Task<IActionResult> GetReportData([DataSourceRequest] DataSourceRequest request, Filter filter1, int TabIndex,string page)
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

         

            filter1.Tabindex = TabIndex;

            //Global.GlobalFilter = filter1;
            var ds = await _SalaryStructureReportService.GetSalaryStructureReportData(TabIndex, filter1);
            DataTable dt = new DataTable();
            DataTable dtCount = new DataTable();

            dt = ds.Tables[0];
            dtCount = ds.Tables[1];
            ModifyColumnHeadersData(dt);
            request.PageSize = 0;

            var dataResult1 = dt.ToDataSourceResult(request);

               if(filter1.Tabindex==0)
            dataResult1.Total = Convert.ToInt32(dt.Rows[0]["ACTIVE"].ToString());
               else
                dataResult1.Total = Convert.ToInt32(dt.Rows[0]["RESIGNED"].ToString());


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
        public async Task<ActionResult> GetList(int TabIndex,Filter filter)
        {
            try { 

            SalaryStructureReport model = new SalaryStructureReport();
            
            //model = await _SalaryStructureReportService.GetSalaryStructureList(TabIndex, filter);
            if(TabIndex==0)
            return PartialView("~/Areas/DataBank/Reports/Views/SalaryStructureReport/_ActiveList.cshtml");
            else
                return PartialView("~/Areas/DataBank/Reports/Views/SalaryStructureReport/_ResignedList.cshtml");
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

        public async Task<IActionResult> ApplyFilter(Filter filter)
        {
            try { 
            SalaryStructureReport model = new SalaryStructureReport();
            model.SliderFilter = filter;
         
            model = await _SalaryStructureReportService.GetList(model.TabIndex, filter);

            if (model.TabIndex == 0)
                return PartialView("~/Areas/DataBank/Reports/Views/SalaryStructureReport/_ActiveList.cshtml", model);
            else
                return PartialView("~/Areas/DataBank/Reports/Views/SalaryStructureReport/_ResignedList.cshtml", model);
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

       

        //public async Task<IActionResult> ExportToExcel(int TabIndex, string TabName, int totRecords)
        //{
        //    try
        //    {
        //        SalaryStructureReport model = new SalaryStructureReport();

        //        var LoginUser = HttpContext.Session.GetString("LoginUser");
        //        var CompanyName = HttpContext.Session.GetString("CompanyName");
        //        ExcelPassword EP = new ExcelPassword();
        //        EP = await _commonService.GetExcelPassword("Salary Structure (" + TabName + ")", "Excel");
        //        int slNo = 0;

        //        if (Global.GlobalFilter == null)
        //            Global.GlobalFilter = new Filter();
        //        else
        //        {
        //            Global.GlobalFilter.length = totRecords;
        //            Global.GlobalFilter.start = 0;
        //        }
        //        string FilterValues = await _helpers.GetFilterValues(Global.GlobalFilter);
        //        if (FilterValues == "")
        //        {
        //            FilterValues = "All";
        //        }


        //        model = await _SalaryStructureReportService.GetList(TabIndex, Global.GlobalFilter);
        //        string currDateTime = await _helpers.GetCurrentDate();
        //        

        //        using (var workbook = new XLWorkbook())
        //        {

        //            var worksheet = workbook.Worksheets.Add("Salary Structure Report");


        //            int count = model.Data.Count + 5;

        //            string columnName = "";

        //            int Columncount = model.MainList.Header.Count + 1;

        //            while (Columncount > 0)
        //            {
        //                int modulo = (Columncount - 1) % 26;
        //                columnName = Convert.ToChar('A' + modulo) + columnName;
        //                Columncount = (Columncount - modulo) / 26;
        //            }

        //            string colName = columnName;

        //            worksheet.Range("A1", colName + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
        //            worksheet.Range("A1", colName + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
        //            worksheet.Range("A1", colName + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
        //            worksheet.Range("A1", colName + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
        //            worksheet.Range("A1", colName + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;






        //            var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, colName));
        //            wsReportNameHeaderRange.Style.Font.Bold = true;
        //            wsReportNameHeaderRange.Style.Font.FontSize = 15;
        //            wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        //            wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
        //            wsReportNameHeaderRange.Merge();
        //            wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
        //            wsReportNameHeaderRange.Value = "SalaryStructure (" + TabName + ")";

        //            worksheet.Cell(2, 1).Style.Font.Bold = true;
        //            worksheet.Cell(2, 1).Value = "Company Name";
        //            worksheet.Cell(2, 2).Value = CompanyName.ToUpper();
        //            worksheet.Cell(2, 3).Style.Font.Bold = true;
        //            worksheet.Cell(2, 3).Value = "Tab Name";
        //            worksheet.Cell(2, 4).Value = TabName;



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
        //            worksheet.Cell(3, 6).Value = model.Data.Count();
        //            worksheet.Cell(4, 1).Style.Font.Bold = true;
        //            worksheet.Cell(4, 1).Value = "Filter Applied";
        //            var filterapplied = worksheet.Range("B4", colName + 4).Merge();

        //            filterapplied.Value = FilterValues;


        //            var currentRow = 5;
        //            #region Header
        //            worksheet.Range("A5", colName + 5).Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
        //            worksheet.Cell(currentRow, 1).Value = "Sl.No";
        //            worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
        //            worksheet.Cell(currentRow, 2).Value = "ID";
        //            worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
        //            worksheet.Cell(currentRow, 3).Value = "Employee Name";
        //            worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
        //            worksheet.Cell(currentRow, 4).Value = "Salary Structure";
        //            worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
        //            worksheet.Cell(currentRow, 5).Value = "Designation";
        //            worksheet.Cell(currentRow, 5).Style.Font.Bold = true;

        //            var a = 6;
        //            for (int i = 4; i < model.MainList.Header.Count; i++)
        //            {
        //                worksheet.Cell(currentRow, a).Style.Font.Bold = true;
        //                worksheet.Cell(currentRow, a).Value = model.MainList.Header[i].HeaderName;
        //                a++;
        //            }


        //            #endregion

        //            #region Body
        //            for (int i = 0; i < model.Data.Count; i++)
        //            {
        //                currentRow++;
        //                slNo = slNo + 1;

        //                worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //                worksheet.Cell(currentRow, 1).Value = slNo;
        //                worksheet.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //                worksheet.Cell(currentRow, 2).Value = Convert.ToString(model.Data[i]["Employee"].employeeID);
        //                worksheet.Cell(currentRow, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //                worksheet.Cell(currentRow, 3).Value = Convert.ToString(model.Data[i]["Employee"].fullName);
        //                worksheet.Cell(currentRow, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //                worksheet.Cell(currentRow, 4).Value = Convert.ToString(model.Data[i]["Employee"].salaryStructure);
        //                worksheet.Cell(currentRow, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //                worksheet.Cell(currentRow, 5).Value = Convert.ToString(model.Data[i]["Employee"].designation);
        //                worksheet.Cell(currentRow, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //                worksheet.Cell(currentRow, 6).Value = Convert.ToString(model.Data[i]["Employee"].department);
        //                worksheet.Cell(currentRow, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //                worksheet.Cell(currentRow, 7).Value = Convert.ToString(model.Data[i]["Employee"].location);

        //                worksheet.Cell(currentRow, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //                worksheet.Cell(currentRow, 8).Value = Convert.ToString(model.Data[i]["Employee"].category);
        //                worksheet.Cell(currentRow, 9).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //                worksheet.Cell(currentRow, 9).Value = Convert.ToString(model.Data[i]["Employee"].doj);
        //                worksheet.Cell(currentRow, 9).Style.DateFormat.Format = "dd-MMM-yyyy";
        //                worksheet.Cell(currentRow, 10).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //                worksheet.Cell(currentRow, 10).Value = Convert.ToString(model.Data[i]["Employee"].lid);
        //                worksheet.Cell(currentRow, 10).Style.DateFormat.Format = "dd-MMM-yyyy";
        //                worksheet.Cell(currentRow, 11).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //                worksheet.Cell(currentRow, 11).Value = Convert.ToString(model.Data[i]["Employee"].yos);

        //                if (TabIndex == 1)
        //                {
        //                    worksheet.Cell(currentRow, 12).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //                    worksheet.Cell(currentRow, 12).Value = Convert.ToString(model.Data[i]["Employee"].lwd);
        //                    worksheet.Cell(currentRow, 12).Style.DateFormat.Format = "dd-MMM-yyyy";

        //                    var c = 13;
        //                    for (int k = 11; k < model.MainList.Header.Count; k++)
        //                    {
        //                        worksheet.Cell(currentRow, c).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //                        worksheet.Cell(currentRow, c).Value = Convert.ToString(model.Data[i][model.MainList.Header[k].FieldName]);
        //                        c++;
        //                    }

        //                }







        //                var b = 12;
        //                for (int j = 10; j < model.MainList.Header.Count; j++)
        //                {


        //                    worksheet.Cell(currentRow, b).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //                    worksheet.Cell(currentRow, b).Value = Convert.ToString(model.Data[i][model.MainList.Header[j].FieldName]);
        //                    b++;
        //                }

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
        //                    "Salary Structure Report " + TabName + " List.xlsx"
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


        public async Task<IActionResult> ExportToExcel(int TabIndex, string TabName, int totRecords, string searchValue, Filter filter)
        {
            try
            {
                SalaryStructureReport _model = new SalaryStructureReport();
                //_model.filter = new Filter();
                //_model.GridSettings = new List<JobTransitionReport_GridSettings>();
                filter.length = totRecords;
                filter.searchValue = searchValue;
                filter.start = 0;
                //_model.filter = Global.GlobalFilter;


                string FilterValues = await _helpers.GetFilterValues(filter);
                if (FilterValues == "")
                {
                    FilterValues = "All";
                }
                string currDateTime = await _helpers.GetCurrentDate();
                
                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Shared.Account.Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Salary Band Report (" + TabName + ")", "Excel");


                var dataset = await _SalaryStructureReportService.GetSalaryStructureReportData(TabIndex, filter);
                DataTable dt = dataset.Tables[0];
               
                if ((dataset.Tables[0]?.Rows?.Count ?? 0) > 0)
                {
                    dt = dataset.Tables[0];
                }



                //set EXCEL Column Heading 
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    string colNameLower = dt.Columns[i].ColumnName.ToLower();

                    // 1. Check for columns to be removed
                    if (colNameLower == "employeecode" || colNameLower == "employeegi" || colNameLower == "order" ||
                        colNameLower == "totalcount" || colNameLower == "active" || colNameLower == "resigned")
                    {
                        dt.Columns.RemoveAt(i);
                        i--; // Adjust index after removal
                        continue;
                    }

                    // 2. Handle specific naming logic
                    if (colNameLower == "designation")
                    {
                        dt.Columns[i].ColumnName = "Fun.Designation";
                    }
                    else if (colNameLower == "doj" || colNameLower == "lid" || colNameLower == "lwd" || colNameLower == "yos")
                    {
                        dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.ToUpper();
                    }
                    else
                    {
                        
                        dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
                    }
                }
                //set EXCEL Column Heading 
                //for (int i = 0; i < dt.Columns.Count; i++)
                //{
                //    if (dt.Columns[i].ColumnName == "employeeCode" || dt.Columns[i].ColumnName == "EmployeeGI" || dt.Columns[i].ColumnName == "order" || dt.Columns[i].ColumnName == "totalCount" || dt.Columns[i].ColumnName == "Active" || dt.Columns[i].ColumnName == "Resigned")
                //    {
                //        dt.Columns.RemoveAt(i);
                //        if (dt.Columns.Count != i)
                //        {
                //            if (dt.Columns[i].ColumnName == "doj" || dt.Columns[i].ColumnName == "lid" || dt.Columns[i].ColumnName == "lwd" || dt.Columns[i].ColumnName == "yos")
                //                dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.ToUpper();
                //            else
                //                dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
                //        }
                //        i = i - 1;
                //    }

                //    else
                //    {
                //        if (dt.Columns[i].ColumnName == "doj" || dt.Columns[i].ColumnName == "lid" || dt.Columns[i].ColumnName == "lwd" || dt.Columns[i].ColumnName == "yos")
                //            dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.ToUpper();
                //        else
                //            dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
                //    }
                //}


                using (var workbook = new XLWorkbook())
                {

                    var worksheet = workbook.Worksheets.Add("Salary Band Report");


                    int count = dt.Rows.Count + 5;

                    string columnName = "";

                    int Columncount = dt.Columns.Count;

                    while (Columncount > 0)
                    {
                        int modulo = (Columncount - 1) % 26;
                        columnName = Convert.ToChar('A' + modulo) + columnName;
                        Columncount = (Columncount - modulo) / 26;
                    }

                    string colName = columnName;

                    worksheet.Range("A1", colName + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1", colName + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1", colName + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1", colName + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1", colName + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;






                    var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, colName));
                    wsReportNameHeaderRange.Style.Font.Bold = true;
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Merge();


                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Value = "Salary Band Report (" + TabName + ")";
                    //worksheet.Range("A5:J5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Cell(2, 1).Style.Font.Bold = true;
                    worksheet.Cell(2, 1).Value = "Group Company Name";
                    worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                    worksheet.Cell(2, 3).Style.Font.Bold = true;
                    worksheet.Cell(2, 3).Value = "Tab Name";
                    worksheet.Cell(2, 3).Style.Font.Bold = true;                    
                    worksheet.Cell(2, 4).Value = TabName;
                    //worksheet.Cell(2, 6).Value = "Type";
                    //worksheet.Cell(2, 6).Style.Font.Bold = true;
                    //worksheet.Cell(2, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    //worksheet.Cell(2, 7).Value = Type;



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
                    var filterapplied = worksheet.Range("B4", colName + 4).Merge();

                    filterapplied.Value = FilterValues;

                    var currentRow = 5;

                    worksheet.Cell(currentRow, 1).InsertTable(dt);
                    worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                    worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                    worksheet.Range("A5:" + colName + "5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Range("A5:" + colName + "5").Style.Font.Bold = true;

                    worksheet.Range("H5:H" + (dt.Rows.Count + 5)).Style.DateFormat.Format = "dd-MMM-yyyy";
                    worksheet.Range("I5:I" + (dt.Rows.Count + 5)).Style.DateFormat.Format = "dd-MMM-yyyy";
                    worksheet.Columns().AdjustToContents();
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
                            "Salary Band Report " + TabName + " List.xlsx"
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
