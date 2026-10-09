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
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.PayRoll.Reports.Controllers
{
    [SessionAuthorize]
    [Area("PayRoll")]
    public class BankStatementReportController : Controller
    {
        private readonly IBankStatementReportService _BankstatementReportService;
        private readonly IDropdownService _dropdownService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        public static class Global
        {
            public static FilterMultiSelect GlobalFilter;
            public static int[] GlobalBanks;
        }

        public BankStatementReportController(IBankStatementReportService BankstatementReportService, IDropdownService dropdownService, ICommonService commonService, IHelpers helpers)
        {
            _BankstatementReportService = BankstatementReportService;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = helpers;
        }
        public async Task<IActionResult> Index()
        {
            
          
            FilterMultiSelect filter = new FilterMultiSelect();
            BankStatement model = new BankStatement();
            var currMonth = await _commonService.GetCurrentMonthDropdown();
            int currentmonth = Convert.ToInt32(currMonth.Data);
            model.PayrollMonth = new MasterBase();
            model.BankStatementFormat = new MasterBase();
            // model.Month = new Shared.TimeOffice.Generic.Master.Month();
            //model.PayrollMonth.Code = currentmonth;
            model.Banks = Global.GlobalBanks;
            ViewBag.month = new SelectList(await _dropdownService.GetPayrollReportMonthDropdown(), "Code", "Name");
            ViewBag.bank = new SelectList(await _dropdownService.GetBankDropdown(), "Code", "Name");
            ViewBag.format = new SelectList(await _dropdownService.GetBankStatementFormatDropDown(), "Code", "Name");
            model.Filter = await _commonService.GetMultiSelectFilterDropdowns();          
            model.BankStatementFormat.Code = 1;

            ViewBag.allowance = new SelectList(await _dropdownService.GetIncentiveAllowanceDropdown(), "Code", "Name");
            return View("~/Areas/PayRoll/Reports/Views/BankStatementReport/Index.cshtml",model);
        }
        public async Task <IActionResult> getListBankStatement(int month, int bank, int format)
        {
            try
            {

                BankStatement model = new BankStatement();
                Filter filter = new Filter();
                model.BankStatementFormat = new MasterBase();
                model.PayrollMonth = new MasterBase();
                model.Bank = new Shared.Master.Bank();
                model.PayrollMonth.Code = month;
                model.Bank.Code = bank;
                model.BankStatementFormat.Code= format;

                model.Filter = new FilterMultiSelect();
                var response = await _BankstatementReportService.AccountNumberDuplication(model);
                model = response.Data;
                if (response.Success == false)
                {
                    return PartialView("~/Areas/PayRoll/Reports/Views/BankStatementReport/_PopUpEmployee.cshtml", model);


                    
                }
                else {
                    return Json(response);
                }

                //else
                //{
                //    return View("~/Areas/PayRoll/Reports/Views/BankStatementReport/_BankStatementReportList.cshtml");

                //}




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

        public async Task<IActionResult> BankStmtList()
        {
            return View("~/Areas/PayRoll/Reports/Views/BankStatementReport/_BankStatementReportList.cshtml");
        }


        [HttpPost]
        public async Task<IActionResult> GetBankStatementReportList(FilterMultiSelect filter1, int month,string bank,int format, int AllowanceCode)
        {
            try
            {
                BankStatement model = new BankStatement();
                BankStatement m = new BankStatement();
                Global.GlobalFilter = filter1;

                model.BankStatementFormat = new MasterBase();
                model.PayrollMonth = new MasterBase();

                if (bank != null)
                {
                    string[] values = bank.Split(',');
                    for (int i = 0; i < values.Length; i++)
                    {
                        values[i] = values[i].Trim();
                    }

                    model.Banks = Array.ConvertAll(values, int.Parse);
                }


                model.Filter = filter1;
                model.PayrollMonth.Code = month;
                model.BankStatementFormat.Code = format;
                model.Allowance = new Dropdown();
                model.Allowance.Code = AllowanceCode;



                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }
               if(month!=0)

                    model = await _BankstatementReportService.GetList(model);
                
                m.Details = model.Details;


               
                decimal TotalNetPay = 0;

                TotalNetPay = model.Total_NetAmount;
               
                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.Details == null || model.Details.Count() == 0)
                {


                    return Json(new
                    {
                        data = "",
                        draw = filter1.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,

                       
                        totnetpay = TotalNetPay,
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
                       
                        totnetpay = TotalNetPay,
                        recordsFiltered = totalrows//totalrowsafterfiltering
                    });
                }
                else
                {
                    return Json(new
                    {
                        data = model.Details,
                        draw = filter1.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        
                        totnetpay = TotalNetPay,
                        recordsFiltered = totalrows
                    });
                }


            }
            catch (Exception ex)
            {
                return View();
            }




        }

        public async Task<IActionResult> GetList(int MonthCode, string BankCode,int ExcelFormat)
        {
            try
            {
                FilterMultiSelect filter = new FilterMultiSelect();
                BankStatement model = new BankStatement();
                //model.Month = new Shared.TimeOffice.Generic.Master.Month();
                model.BankStatementFormat = new MasterBase();
                model.PayrollMonth = new MasterBase();

                if (BankCode != null)
                {
                    string[] values = BankCode.Split(',');
                    for (int i = 0; i < values.Length; i++)
                    {
                        values[i] = values[i].Trim();
                    }

                    model.Banks = Array.ConvertAll(values, int.Parse);
                }


                model.Filter = filter;
                model.PayrollMonth.Code = MonthCode;
                model.BankStatementFormat.Code = ExcelFormat;
               model = await _BankstatementReportService.GetList(model);
             
                return PartialView("~/Areas/PayRoll/Reports/Views/BankStatementReport/_BankStatementReportList.cshtml", model);
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






    

        public async Task<IActionResult> ApplyFilter(int Month, string BankCode,int ExcelFormat, Filter filter)
        {
            try
            {

                BankStatement model = new BankStatement();
                Global.GlobalFilter = new FilterMultiSelect();
                model.BankStatementFormat = new MasterBase();
                model.PayrollMonth = new MasterBase();
                if (BankCode != null)
                {
                    string[] values = BankCode.Split(',');
                    for (int i = 0; i < values.Length; i++)
                    {
                        values[i] = values[i].Trim();
                    }

                    model.Banks = Array.ConvertAll(values, int.Parse);
                }
                model.PayrollMonth.Code = Month;
                model.BankStatementFormat.Code = ExcelFormat;
                model.Filter = Global.GlobalFilter;
                model = await _BankstatementReportService.GetList(model);
                return PartialView("~/Areas/PayRoll/Reports/Views/BankStatementReport/_BankStatementReportList.cshtml", model);
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

        public async Task<IActionResult> ExportToExcel(int Month, string BankCode, string monthName, string bankName,int Format,int totRec)
        {
            try
            {

                if (Global.GlobalFilter == null)
                    Global.GlobalFilter = new FilterMultiSelect();
                else
                {
                    Global.GlobalFilter.length = totRec;
                    Global.GlobalFilter.start = 0;
                }

                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword(" Bank Statement Report", "Excel");
                BankStatement model = new BankStatement();
                model.BankStatementFormat = new MasterBase();
                model.PayrollMonth = new MasterBase();
                model.PayrollMonth.Code = Month;
                if (BankCode != null)
                {
                    string[] values = BankCode.Split(',');
                    for (int i = 0; i < values.Length; i++)
                    {
                        values[i] = values[i].Trim();
                    }

                    model.Banks = Array.ConvertAll(values, int.Parse);
                }



                model.BankStatementFormat.Code = Format;

                if (Global.GlobalFilter == null)
                    Global.GlobalFilter = new FilterMultiSelect();
                string FilterValues = "";// await _helpers.GetFilterValuesMultiSelect(Global.GlobalFilter);
                if (FilterValues == "")
                {
                    FilterValues = "All";
                }


                string currDateTime = await _helpers.GetCurrentDate();
                
                model.Filter = Global.GlobalFilter;
                //Get Dataset
                var dataset = await _BankstatementReportService.GetBankStatement_Excel(model);
                DataTable dt = dataset.Tables[0];
                DataTable dt1 = dataset.Tables[1];

                //var totalNetPay = dataset.Tables[1].Rows[0].ItemArray[0];
                int index = 0;

                //set EXCEL Column Heading 
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (dt.Columns[i].ColumnName.Contains("ifsc"))
                    {
                        index = dt.Columns[i].ColumnName.IndexOf("ifsc");
                        dt.Columns[i].ColumnName = ConvertSubstringToUpperCase(dt.Columns[i].ColumnName, index, 4);
                    }

                    else
                        dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
                }


                using (var workbook = new XLWorkbook())
                {

                    var worksheet = workbook.Worksheets.Add("Bank Statement Report List");
                    int count = dt.Rows.Count + 6;


                    worksheet.Range("A1:G" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:G" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:G" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:G" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:G" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("E6:E"+count).Style.NumberFormat.NumberFormatId = 1;


                    var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 6)));
                    wsReportNameHeaderRange.Style.Font.Bold = true;
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Merge();
                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                    wsReportNameHeaderRange.Value = "Bank Statement Report";

                    worksheet.Cell(2, 1).Style.Font.Bold = true;
                    worksheet.Cell(2, 1).Value = "Group Company Name";
                    worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                    worksheet.Cell(2, 3).Style.Font.Bold = true;
                    worksheet.Cell(2, 3).Value = "Month";

                    worksheet.Cell(2, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(2, 4).Style.DateFormat.Format = "dd-MMM-yyyy";
                    worksheet.Cell(2, 4).Value = monthName;
                    worksheet.Cell(2, 5).Style.Font.Bold = true;
                    //worksheet.Cell(2, 5).Value = "Bank";
                    //worksheet.Cell(2, 6).Value = bankName;

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
                    var filterapplied = worksheet.Range("B4:G4").Merge();

                    filterapplied.Value = FilterValues;


                    var currentRow = 5;


                    worksheet.Cell(currentRow, 1).InsertTable(dt);
                    worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                    worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                    worksheet.Range("A5:G5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Range("A5:G5").Style.Font.Bold = true;
                    var netpayrow = dt.Rows.Count + 5;
                    var netpaycolumn = dt.Columns.Count;

                    worksheet.Cell(netpayrow + 1, netpaycolumn).Value ="Total : "+ dt1.Rows[0].ItemArray[0];






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
                            "Bank Statement Report List.xlsx"
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





        public async Task<IActionResult> ExportToExcelFormat2(int Month, string BankCode, string monthName, string bankName,int Format,int AllowanceCode,int totRec)
        {
            try
            {

                if (Global.GlobalFilter == null)
                    Global.GlobalFilter = new FilterMultiSelect();
                else
                {
                    Global.GlobalFilter.length = totRec;
                    Global.GlobalFilter.start = 0;
                }
                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Bank Statement Report", "Excel");
                BankStatement model = new BankStatement();
                model.BankStatementFormat = new MasterBase();
                model.PayrollMonth = new MasterBase();
           

                if (BankCode != null)
                {
                    string[] values = BankCode.Split(',');
                    for (int i = 0; i < values.Length; i++)
                    {
                        values[i] = values[i].Trim();
                    }

                    model.Banks = Array.ConvertAll(values, int.Parse);
                }



                model.Filter = Global.GlobalFilter;
                model.PayrollMonth.Code = Month;
                model.BankStatementFormat.Code = Format;
                model.Allowance = new Dropdown();
                model.Allowance.Code = AllowanceCode;
                // model.BankStatementFormat.Code = 2;
                var dataset = await _BankstatementReportService.GetBankStatement_Excel(model);
                DataTable dt = dataset.Tables[0];
                                DataTable dt1 = dataset.Tables[1];

                int index = 0;


                
                //set EXCEL Column Heading 
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (dt.Columns[i].ColumnName.Contains("ifsc"))
                    {
                        index = dt.Columns[i].ColumnName.IndexOf("ifsc");
                        dt.Columns[i].ColumnName = ConvertSubstringToUpperCase(dt.Columns[i].ColumnName, index, 4);
                    }

                    else
                        dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);                        

                   
                }



                if (Format == 10)
                {
                    
                        for (int i = 0; i < dt.Columns.Count; i++)
                        {
                            if (dt.Columns[i].ColumnName == "Transaction Type(N:NEFT/I:SAME BANK)" || dt.Columns[i].ColumnName == "Ac NUMBER" || dt.Columns[i].ColumnName == "Amount" || dt.Columns[i].ColumnName == "Employee NAME" ||
                                dt.Columns[i].ColumnName == "Description" || dt.Columns[i].ColumnName == "Date OF PAYMENT" || dt.Columns[i].ColumnName == "Bank Name" ||
                                dt.Columns[i].ColumnName == "Email ID")
                                dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.ToUpper();
                            else
                                dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
                        }

                        using (var workbook = new XLWorkbook())
                    {

                        var worksheet = workbook.Worksheets.Add("Bank Statement Report List");
                        int count = dt.Rows.Count + 1;


                        worksheet.Range("A1:AA" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:AA" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:AA" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:AA" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:AA" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("C2:C" + count).Style.NumberFormat.NumberFormatId = 1;


                        var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65)));
                        wsReportNameHeaderRange.Style.Font.Bold = true;
                        wsReportNameHeaderRange.Style.Font.FontSize = 11;
                        wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                        wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        // wsReportNameHeaderRange.Merge();
                        wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                        // wsReportNameHeaderRange.Value = "Bank Statement Report";


                        var currentRow = 1;


                        worksheet.Cell(currentRow, 1).InsertTable(dt);
                        worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;


                        worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                        worksheet.Range("A1:AA1").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                        worksheet.Range("A1:AA1").Style.Font.Bold = true;


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
                                book.Protect(EP.Password, false, false);
                                book.SaveToStream(stream);
                            }
                            var content = stream.ToArray();
                            return File(
                                content,
                                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                                "Bank Statement Report List.xlsx"
                                );
                        }
                    }
                }




                if (Format==2)
                {


                    using (var workbook = new XLWorkbook())
                    {

                        var worksheet = workbook.Worksheets.Add("Bank Statement Report List");
                        int count = dt.Rows.Count + 1;


                        worksheet.Range("A1:AB" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:AB" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:AB" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:AB" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:AB" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("C2:C" + count).Style.NumberFormat.NumberFormatId = 1;


                        var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65)));
                        wsReportNameHeaderRange.Style.Font.Bold = true;
                        wsReportNameHeaderRange.Style.Font.FontSize = 15;
                        wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                        wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        // wsReportNameHeaderRange.Merge();
                        wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                        // wsReportNameHeaderRange.Value = "Bank Statement Report";




                        var currentRow = 1;



                        if (dt.Columns[0].ColumnName == "Ref" && dt.Columns[27].ColumnName == "Email ID")
                        {
                            worksheet.Range("A2", "A" + count).Style.Font.FontColor = XLColor.Red;
                            worksheet.Range("AB2", "AB" + count).Style.Font.FontColor = XLColor.Red;
                        }

                        worksheet.Cell(currentRow, 1).InsertTable(dt);
                        worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;








                        worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                        worksheet.Range("A1:AB1").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                        worksheet.Range("A1:AB1").Style.Font.Bold = true;



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
                                "Bank Statement Report List.xlsx"
                                );
                        }
                    }



                }


                if (Format == 4)
                {


                    using (var workbook = new XLWorkbook())
                    {

                        var worksheet = workbook.Worksheets.Add("Bank Statement Report List");
                        int count = dt.Rows.Count + 1;


                        worksheet.Range("A1:G" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:G" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:G" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:G" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:G" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("C2:C" + count).Style.NumberFormat.NumberFormatId = 1;


                        var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65)));
                        wsReportNameHeaderRange.Style.Font.Bold = true;
                        wsReportNameHeaderRange.Style.Font.FontSize = 13;
                        wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                        wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        // wsReportNameHeaderRange.Merge();
                        wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                        // wsReportNameHeaderRange.Value = "Bank Statement Report";




                        var currentRow = 1;



                        //if (dt.Columns[0].ColumnName == "Ref" && dt.Columns[27].ColumnName == "Email ID")
                        //{
                        //    worksheet.Range("A2", "A" + count).Style.Font.FontColor = XLColor.Red;
                        //    worksheet.Range("AB2", "AB" + count).Style.Font.FontColor = XLColor.Red;
                        //}

                        worksheet.Cell(currentRow, 1).InsertTable(dt);
                        worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;








                        worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                        worksheet.Range("A1:G1").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                        worksheet.Range("A1:G1").Style.Font.Bold = true;



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
                                "Bank Statement Report List.xlsx"
                                );
                        }
                    }









                }


                if (Format == 6)
                {

                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        if (dt.Columns[i].ColumnName == "Name" || dt.Columns[i].ColumnName == "Account NO" || dt.Columns[i].ColumnName == "Amount" || dt.Columns[i].ColumnName == "Remark")
                            dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.ToUpper();
                        else
                            dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
                    }

                    using (var workbook = new XLWorkbook())
                    {

                        var worksheet = workbook.Worksheets.Add("Bank Statement Report List");
                        int count = dt.Rows.Count + 2;


                        worksheet.Range("A1:F" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:F" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:F" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:F" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:F" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("C2:C" + count).Style.NumberFormat.NumberFormatId = 1;


                        var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65+5)));
                        wsReportNameHeaderRange.Style.Font.Bold = true;
                        wsReportNameHeaderRange.Style.Font.FontSize = 13;
                        wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                        wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        wsReportNameHeaderRange.Merge();
                        //wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                        wsReportNameHeaderRange.Value = CompanyName+" - "+monthName;




                        var currentRow = 2;



                        

                        worksheet.Cell(currentRow, 1).InsertTable(dt);
                        worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;








                        worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                        //worksheet.Range("A1:F1").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                        worksheet.Range("A2:G2").Style.Font.Bold = true;


                        var amountrow = dt.Rows.Count + 2;
                        var amountcolumn = dt.Columns.Count;

                        worksheet.Cell(amountrow + 1, 4).Value = "Total : " + dt1.Rows[0].ItemArray[0];


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
                                book.Protect(EP.Password, false, false);
                                book.SaveToStream(stream);
                            }
                            var content = stream.ToArray();
                            return File(
                                content,
                                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                                "Bank Statement Report List.xlsx"
                                );
                        }
                    }










                }


                if (Format == 7)
                {
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        if (dt.Columns[i].ColumnName == "Amount" || dt.Columns[i].ColumnName == "IFSCcode" || dt.Columns[i].ColumnName == "Account NO" ||
                            dt.Columns[i].ColumnName == "Client NAME" || dt.Columns[i].ColumnName == "Remark" || dt.Columns[i].ColumnName == "Mobile" ||
                            dt.Columns[i].ColumnName == "Email")
                            dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.ToUpper();
                        else
                            dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
                    }

                    using (var workbook = new XLWorkbook())
                    {

                        var worksheet = workbook.Worksheets.Add("Bank Statement Report List");
                        int count = dt.Rows.Count + 2;


                        worksheet.Range("A1:H" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:H" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:H" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:H" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:H" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("C2:C" + count).Style.NumberFormat.NumberFormatId = 1;


                        var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65+7)));
                        wsReportNameHeaderRange.Style.Font.Bold = true;
                        wsReportNameHeaderRange.Style.Font.FontSize = 13;
                        wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                        wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        wsReportNameHeaderRange.Merge();
                        //wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                        wsReportNameHeaderRange.Value = CompanyName + " - " + monthName;




                        var currentRow = 2;





                        worksheet.Cell(currentRow, 1).InsertTable(dt);
                        worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;








                        worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                        //worksheet.Range("A1:F1").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                        worksheet.Range("A2:H2").Style.Font.Bold = true;

                        var amountrow = dt.Rows.Count + 2;
                        var amountcolumn = dt.Columns.Count;

                        worksheet.Cell(amountrow + 1, 2).Value = "Total : " + dt1.Rows[0].ItemArray[0];






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
                                book.Protect(EP.Password, false, false);
                                book.SaveToStream(stream);
                            }
                            var content = stream.ToArray();
                            return File(
                                content,
                                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                                "Bank Statement Report List.xlsx"
                                );
                        }
                    }










                }

                if (Format == 9)
                {
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        if (dt.Columns[i].ColumnName == "Amount" || dt.Columns[i].ColumnName == "IFSCcode" || dt.Columns[i].ColumnName == "Account NO" ||
                            dt.Columns[i].ColumnName == "Client NAME" || dt.Columns[i].ColumnName == "Remark" || dt.Columns[i].ColumnName == "Mobile" ||
                            dt.Columns[i].ColumnName == "Email")
                            dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.ToUpper();
                        else
                            dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
                    }

                    using (var workbook = new XLWorkbook())
                    {

                        var worksheet = workbook.Worksheets.Add("Bank Statement Report List");
                        int count = dt.Rows.Count + 1;


                        worksheet.Range("A1:L" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:L" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:L" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:L" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:L" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("C2:C" + count).Style.NumberFormat.NumberFormatId = 1;


                        var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65)));
                        wsReportNameHeaderRange.Style.Font.Bold = true;
                        wsReportNameHeaderRange.Style.Font.FontSize = 11;
                        wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                        wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        // wsReportNameHeaderRange.Merge();
                        wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                        // wsReportNameHeaderRange.Value = "Bank Statement Report";




                        var currentRow = 1;




                        worksheet.Cell(currentRow, 1).InsertTable(dt);
                        worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;



                        worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                        worksheet.Range("A1:L1").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                        worksheet.Range("A1:L1").Style.Font.Bold = true;



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
                                book.Protect(EP.Password, false, false);
                                book.SaveToStream(stream);
                            }
                            var content = stream.ToArray();
                            return File(
                                content,
                                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                                "Bank Statement Report List.xlsx"
                                );
                        }
                    }

                }

                if (Format == 11)
                {
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        if (dt.Columns[i].ColumnName == "PuiF020524" || dt.Columns[i].ColumnName == "Tb" || dt.Columns[i].ColumnName == "UtiB0003714" ||
                             dt.Columns[i].ColumnName == "Staff NAME" || dt.Columns[i].ColumnName == "IFSC" ||
                            
                            dt.Columns[i].ColumnName == "Account NUMBER" || dt.Columns[i].ColumnName == "Chq")
                            dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.ToUpper();
                        else
                            dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
                        //if (dt.Columns[i].ColumnName == "PuiF0205241" || dt.Columns[i].ColumnName == "PuiF0205242" || dt.Columns[i].ColumnName == "PuiF0205243")
                        //    dt.Columns[i].ColumnName = "PuiF020524";
                    }

                    using (var workbook = new XLWorkbook())
                    {


                        var worksheet = workbook.Worksheets.Add("Bank Statement Report List");
                        int count = dt.Rows.Count + 1;


                        worksheet.Range("A1:AJ" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:AJ" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:AJ" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:AJ" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:AJ" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("C2:C" + count).Style.NumberFormat.NumberFormatId = 1;


                        var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65)));

                        wsReportNameHeaderRange.Style.Font.FontSize = 13;
                        wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                        wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        wsReportNameHeaderRange.Merge();
                        //wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                    



                        var currentRow = 1;





                        worksheet.Cell(currentRow, 1).InsertTable(dt);
                        worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;








                        worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                        worksheet.Cell("C1").Style.Font.FontColor = XLColor.Red;
                        worksheet.Cell("V1").Style.Font.FontColor = XLColor.Red;
                        worksheet.Range("A1:AJ1").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                        // worksheet.Range("A2:H2").Style.Font.Bold = true;


                        //var amountrow = dt.Rows.Count + 2;
                        //var amountcolumn = dt.Columns.Count;

                        //worksheet.Cell(amountrow + 1, 2).Value = "Total : " + dt1.Rows[0].ItemArray[0];
                        //var netpayrow = dt.Rows.Count + 5;
                        //var netpaycolumn = dt.Columns.Count;

                        //worksheet.Cell(netpayrow + 1, netpaycolumn).Value = "Total : " + dt1.Rows[0].ItemArray[0];
                        string netPayValue = dt1.Rows[0].ItemArray[0]?.ToString() ?? "NetPay";
                        int targetColumnIndex = 22; // 22nd column
                        worksheet.Cell(1, targetColumnIndex).Value = netPayValue;





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
                                book.Protect(EP.Password, false, false);
                                book.SaveToStream(stream);
                            }
                            var content = stream.ToArray();
                            return File(
                                content,
                                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                                "Bank Statement Report List.xlsx"
                                );
                        }
                    }










                }


                if (Format == 13)
                {


                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("Bank Statement Report List");
                        worksheet.ShowGridLines = false; 
                        // Adjust row heights to avoid extra spacing
                        worksheet.Rows().Height = 15; 
                        worksheet.Rows().AdjustToContents();
                        // Remove unnecessary borders, keeping only table structure clean
                        // worksheet.Range($"A{dataStartRow}:D{amountRow}").Style.Border.OutsideBorder = XLBorderStyleValues.Thin; worksheet.Range($"A{dataStartRow}:D{amountRow}").Style.Border.InsideBorder = XLBorderStyleValues.Thin; worksheet.Range($"A{dataStartRow}:D{amountRow}").Style.Border.LeftBorder = XLBorderStyleValues.Thin; worksheet.Range($"A{dataStartRow}:D{amountRow}").Style.Border.RightBorder = XLBorderStyleValues.Thin;  
                        var toCell = worksheet.Range("A1:D1");
                        toCell.Merge(); toCell.Value = "To";
                      
                        toCell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                        worksheet.Row(1).Height = 20; 
                        // **A2:D2 - "The Bank Manager"**
                         var bankManager = worksheet.Range("A2:D2");
                        bankManager.Merge(); 
                        bankManager.Value = "The Bank Manager"; 
                      
                        bankManager.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                        worksheet.Row(2).Height = 20; 
                        // **A3:D3 & A4:D4 - EMPTY MERGED CELLS**
                         worksheet.Range("A3:D3").Merge(); 
                        worksheet.Range("A4:D4").Merge(); 
                        // **A5:D5 - "Sir,"**
                        var sirCell = worksheet.Range("A5:D5"); 
                        sirCell.Merge(); sirCell.Value = "Sir,"; 
                  
                        sirCell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                        worksheet.Row(5).Height = 20;
                        // **A6:D6- The Message Text**
                        var amtmessageText = "Please find enclosed herewith Our cheque xxxx dt xxxx Rs."+ dt1.Rows[0].ItemArray[0] +" /- drawn on " ;
                        var messageCell = worksheet.Range("A6:D6");
                        messageCell.Merge();
                        messageCell.Value = amtmessageText;
                        messageCell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left; 
                        messageCell.Style.Alignment.Vertical = XLAlignmentVerticalValues.Top; 
                        messageCell.Style.Alignment.WrapText = true;
                        worksheet.Row(6).Height = 30; 
                     
                        // **A7:D8- The Message Text**
                        var messageText = "your favour of our following employees. Kindly transfer the amount to their corresponding accounts as per details given under";
                        var messagetextCell = worksheet.Range("A7:D8");
                        messagetextCell.Merge();
                        messagetextCell.Value = messageText;
                        messagetextCell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                        messagetextCell.Style.Alignment.Vertical = XLAlignmentVerticalValues.Top;
                        messagetextCell.Style.Alignment.WrapText = true;
                        worksheet.Row(7).Height = 30;
                        worksheet.Row(8).Height = 30;
                        // **AUTO ADJUST COLUMNS**
                        worksheet.Columns().AdjustToContents();  

                        // Insert the table data below the text
                        int dataStartRow = 10; // Assuming the data starts after the hardcoded content
                        worksheet.Cell(dataStartRow, 1).InsertTable(dt);
                        worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;
                        worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;

                        // Set background color and formatting for the table header
                        worksheet.Range($"A{dataStartRow}:D{dataStartRow}").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                        worksheet.Range($"A{dataStartRow}:D{dataStartRow}").Style.Font.Bold = true;

                        // Add Grand Total below the table
                        var amountRow = dataStartRow + dt.Rows.Count;
                        worksheet.Cell(amountRow + 1, 1).Value = "Grand Total";
                        worksheet.Cell(amountRow + 1, 4).Value = dt1.Rows[0].ItemArray[0].ToString();
                        worksheet.Range(amountRow + 1, 1, amountRow + 1, 4).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                        // Optionally, set alignment for better appearance
                        worksheet.Range(amountRow + 1, 1, amountRow + 1, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        worksheet.Range(amountRow + 1, 1, amountRow + 1, 4).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                        // Add borders for the table
                        worksheet.Range($"A{dataStartRow}:D{amountRow}").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Range($"A{dataStartRow}:D{amountRow}").Style.Border.InsideBorder = XLBorderStyleValues.Thin;

                        // Add hardcoded content after the table
                        var hardcodedStartRow = amountRow + 3; // Leave some space after the Grand Total
                        worksheet.Cell(hardcodedStartRow, 1).Value = "Kindly mention the transfer as Salary for the month of "+dt1.Rows[0].ItemArray[2]+", ";
                        worksheet.Cell(hardcodedStartRow + 1, 1).Value = "" + dt1.Rows[0].ItemArray[3] + " in our employees accounts";
                        worksheet.Cell(hardcodedStartRow + 3, 1).Value = "Thanking you.";
                        worksheet.Cell(hardcodedStartRow + 5, 1).Value = dt1.Rows[0].ItemArray[3].ToString();
                        DateTime dateValue = Convert.ToDateTime(dt1.Rows[0].ItemArray[1]);
                        string formattedDate = dateValue.ToString("dd-MMM-yy"); 
                        worksheet.Cell(hardcodedStartRow + 7, 2).Value = formattedDate;
                        worksheet.Cell(hardcodedStartRow + 8, 1).Value = "Manager HR & Admin";

                        // Merge cells for proper alignment of hardcoded content
                        worksheet.Range($"A{hardcodedStartRow}:D{hardcodedStartRow}").Merge();
                        worksheet.Range($"A{hardcodedStartRow + 1}:D{hardcodedStartRow + 1}").Merge();
                        worksheet.Range($"A{hardcodedStartRow + 2}:D{hardcodedStartRow + 2}").Merge();
                        worksheet.Range($"A{hardcodedStartRow + 3}:D{hardcodedStartRow + 3}").Merge();
                        worksheet.Range($"A{hardcodedStartRow + 4}:D{hardcodedStartRow + 4}").Merge();
                        worksheet.Range($"A{hardcodedStartRow + 5}:D{hardcodedStartRow + 5}").Merge();
                        worksheet.Range($"A{hardcodedStartRow + 6}:D{hardcodedStartRow + 6}").Merge();
                        worksheet.Range($"B{hardcodedStartRow + 7}:D{hardcodedStartRow + 7}").Merge();
                        worksheet.Range($"A{hardcodedStartRow + 8}:D{hardcodedStartRow + 8}").Merge();

                        // Adjust column widths and row heights
                        worksheet.Columns().AdjustToContents();
                        worksheet.Rows().AdjustToContents();

                        // Save the workbook
                        using (var stream = new MemoryStream())
                        {
                            workbook.SaveAs(stream);
                            if (EP.NeedPassword)
                            {
                                Workbook book = new Workbook();
                                book.LoadFromStream(stream);
                                // Protect Workbook
                                book.Protect(EP.Password, false, false);
                                book.SaveToStream(stream);
                            }
                            var content = stream.ToArray();
                            return File(
                                content,
                                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                                "Bank Statement Report List.xlsx"
                            );
                        }
                    }














                }



                //if (Format == 8)
                //{
                //    for (int i = 0; i < dt.Columns.Count; i++)
                //    {
                //        if (dt.Columns[i].ColumnName == "Debit ACCOUNT NO" || dt.Columns[i].ColumnName == "Date" || dt.Columns[i].ColumnName == "Amount" || dt.Columns[i].ColumnName == "Beneficiary IFSC CODE" ||
                //            dt.Columns[i].ColumnName == "Beneficiary ACCOUNT NO" || dt.Columns[i].ColumnName == "Beneficiary CUSTOMER NAME" ||
                //            dt.Columns[i].ColumnName == "Remark" || dt.Columns[i].ColumnName == "Mobile NO." || dt.Columns[i].ColumnName == "Email")
                //            dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.ToUpper();
                //        else
                //            dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
                //    }

                //    using (var workbook = new XLWorkbook())
                //    {

                //        var worksheet = workbook.Worksheets.Add("Bank Statement Report List");
                //        int count = dt.Rows.Count + 2;


                //        worksheet.Range("A1:H" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                //        worksheet.Range("A1:H" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                //        worksheet.Range("A1:H" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                //        worksheet.Range("A1:H" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                //        worksheet.Range("A1:H" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                //        worksheet.Range("C2:C" + count).Style.NumberFormat.NumberFormatId = 1;


                //        var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 7)));
                //        wsReportNameHeaderRange.Style.Font.Bold = true;
                //        wsReportNameHeaderRange.Style.Font.FontSize = 13;
                //        wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                //        wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                //        wsReportNameHeaderRange.Merge();
                //        //wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                //        wsReportNameHeaderRange.Value = "BCMCH - RTGS BANK DETAILS ";





                //        var currentRow = 2;





                //        worksheet.Cell(currentRow, 1).InsertTable(dt);
                //        worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;








                //        worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                //        //worksheet.Range("A1:F1").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                //        worksheet.Range("A2:H2").Style.Font.Bold = true;



                //        worksheet.Columns().AdjustToContents();
                //        worksheet.Rows().AdjustToContents();
                //        //worksheet.Protect("123");
                //        using (var stream = new MemoryStream())
                //        {
                //            workbook.SaveAs(stream);
                //            if (EP.NeedPassword)
                //            {
                //                Workbook book = new Workbook();
                //                book.LoadFromStream(stream);
                //                //Protect Workbook
                //                book.Protect(EP.Password, false, false);
                //                book.SaveToStream(stream);
                //            }
                //            var content = stream.ToArray();
                //            return File(
                //                content,
                //                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                //                "Bank Statement Report List.xlsx"
                //                );
                //        }
                //    }

                //}

                else
                {


                    using (var workbook = new XLWorkbook())
                    {

                        var worksheet = workbook.Worksheets.Add("Bank Statement Report List");
                        int count = dt.Rows.Count + 1;


                        worksheet.Range("A1:E" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:E" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:E" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:E" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:E" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("C2:C" + count).Style.NumberFormat.NumberFormatId = 1;


                        var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65)));
                        wsReportNameHeaderRange.Style.Font.Bold = true;
                        wsReportNameHeaderRange.Style.Font.FontSize = 11;
                        wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                        wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        // wsReportNameHeaderRange.Merge();
                        wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                        // wsReportNameHeaderRange.Value = "Bank Statement Report";




                        var currentRow = 1;




                        worksheet.Cell(currentRow, 1).InsertTable(dt);
                        worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;



                        worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                        worksheet.Range("A1:E1").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                        worksheet.Range("A1:E1").Style.Font.Bold = true;



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
                                "Bank Statement Report List.xlsx"
                                );
                        }
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

        public async Task<IActionResult> ExportToExcelFormat8(int Month, string BankCode, string monthName, string bankName, int Format, int totRec)


        {
            try
            {

                if (Global.GlobalFilter == null)
                    Global.GlobalFilter = new FilterMultiSelect();
                else
                {
                    Global.GlobalFilter.length = totRec;
                    Global.GlobalFilter.start = 0;
                }
                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Bank Statement Report", "Excel");
                BankStatement model = new BankStatement();
                model.BankStatementFormat = new MasterBase();
                model.PayrollMonth = new MasterBase();


                if (BankCode != null)
                {
                    string[] values = BankCode.Split(',');
                    for (int i = 0; i < values.Length; i++)
                    {
                        values[i] = values[i].Trim();
                    }

                    model.Banks = Array.ConvertAll(values, int.Parse);
                }



                model.Filter = Global.GlobalFilter;
                model.PayrollMonth.Code = Month;
                model.BankStatementFormat.Code = Format;
                // model.BankStatementFormat.Code = 2;
                var dataset = await _BankstatementReportService.GetBankStatement_Excel(model);
                DataTable dt = dataset.Tables[0];
                DataTable dt1 = dataset.Tables[1];

                int index = 0;

                //set EXCEL Column Heading 
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (dt.Columns[i].ColumnName.Contains("ifsc"))
                    {
                        index = dt.Columns[i].ColumnName.IndexOf("ifsc");
                        dt.Columns[i].ColumnName = ConvertSubstringToUpperCase(dt.Columns[i].ColumnName, index, 4);
                    }

                    else
                        dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);


                }

                for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        if (dt.Columns[i].ColumnName == "Debit ACCOUNT NO" || dt.Columns[i].ColumnName == "Date" || dt.Columns[i].ColumnName == "Amount" || dt.Columns[i].ColumnName == "Beneficiary IFSC CODE" ||
                            dt.Columns[i].ColumnName == "Beneficiary ACCOUNT NO" || dt.Columns[i].ColumnName == "Beneficiary CUSTOMER NAME" ||
                            dt.Columns[i].ColumnName == "Remark" || dt.Columns[i].ColumnName == "Mobile NO." || dt.Columns[i].ColumnName == "Email")
                            dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.ToUpper();
                        else
                            dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
                    }

                    using (var workbook = new XLWorkbook())
                    {

                        var worksheet = workbook.Worksheets.Add("Bank Statement Report List");
                        int count = dt.Rows.Count + 2;


                        worksheet.Range("A1:E" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:E" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:E" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:E" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:E" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("C2:C" + count).Style.NumberFormat.NumberFormatId = 1;


                        var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 4)));
                        wsReportNameHeaderRange.Style.Font.Bold = true;
                        wsReportNameHeaderRange.Style.Font.FontSize = 13;
                        wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                        wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        wsReportNameHeaderRange.Merge();
                        //wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                        wsReportNameHeaderRange.Value = "BCMCH - RTGS BANK DETAILS";

                    //var wsReportNameHeaderRange1 = worksheet.Range(string.Format("A{0}:{2}{0}", 2, Char.ConvertFromUtf32(65 + 4)));
                    //wsReportNameHeaderRange1.Style.Font.Bold = true;
                    //wsReportNameHeaderRange1.Style.Font.FontSize = 13;
                    //wsReportNameHeaderRange1.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    //wsReportNameHeaderRange1.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    //wsReportNameHeaderRange1.Merge();
                    ////wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                    //wsReportNameHeaderRange1.Value = " RTGS BANK DETAILS ";



                    var currentRow = 2;


                        worksheet.Cell(currentRow, 1).InsertTable(dt);
                        worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;


                        worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                        //worksheet.Range("A1:F1").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                        worksheet.Range("A2:E2").Style.Font.Bold = true;


                    var amountrow = dt.Rows.Count + 2;
                    var amountcolumn = dt.Columns.Count;

                    worksheet.Cell(amountrow + 1, 1).Value = "Total : " + dt1.Rows[0].ItemArray[0];



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
                                book.Protect(EP.Password, false, false);
                                book.SaveToStream(stream);
                            }
                            var content = stream.ToArray();
                            return File(
                                content,
                                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                                "Bank Statement Report List.xlsx"
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
        public async Task<IActionResult> ExportToExcelFormat12(int Month, string BankCode, string monthName, string bankName, int Format, int totRec)


        {
            try
            {


                if (Global.GlobalFilter == null)
                    Global.GlobalFilter = new FilterMultiSelect();
                else
                {
                    Global.GlobalFilter.length = totRec;
                    Global.GlobalFilter.start = 0;
                }
                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Bank Statement Report", "Excel");
                BankStatement model = new BankStatement();
                model.BankStatementFormat = new MasterBase();
                model.PayrollMonth = new MasterBase();
                model.TYPE = "EXCEL";

                if (BankCode != null)
                {
                    string[] values = BankCode.Split(',');
                    for (int i = 0; i < values.Length; i++)
                    {
                        values[i] = values[i].Trim();
                    }

                    model.Banks = Array.ConvertAll(values, int.Parse);
                }



                model.Filter = Global.GlobalFilter;
                model.PayrollMonth.Code = Month;
                model.BankStatementFormat.Code = Format;
                // model.BankStatementFormat.Code = 2;
                var dataset = await _BankstatementReportService.GetBankStatement_Excel(model);
                DataTable dt = dataset.Tables[0];


          



                //set EXCEL Column Heading 
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (dt.Columns[i].ColumnName == "doj" || dt.Columns[i].ColumnName == "dob" || dt.Columns[i].ColumnName == "y/s")
                        dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.ToUpper();
                    else
                        dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
                }
                foreach (DataRow row in dt.Rows)
                {
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        if (row[i] != DBNull.Value) // Check for non-null values
                        {
                            row[i] = row[i].ToString().TrimEnd(); // Trim trailing spaces
                        }
                    }
                }
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Bank Statement Report List");
                    //int count = dt.Rows.Count + 5;


                    // Style the header
                    worksheet.Range("A1:D1").Style.Font.Bold = true;
                    worksheet.Range("A1:D1").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Range("A1:D1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

                    // Insert data rows below the header
                    int currentRow = 1;
                    //foreach (DataRow row in dt.Rows)
                    //{
                    //    worksheet.Cell(currentRow, 1).Value = row["FirstColumnName"].ToString().PadLeft(19, '0'); // First column (19 digits)
                    //    worksheet.Cell(currentRow, 2).Value = row["SecondColumnName"].ToString().PadLeft(17, '0'); // Second column (17 digits)
                    //    worksheet.Cell(currentRow, 3).Value = row["SecondColumnName"].ToString().PadLeft(15, '0'); ; // Third column (1 digit)
                    //    worksheet.Cell(currentRow, 4).Value = row["LastColumnName"].ToString().PadRight(72, ' '); // Last column (72 characters)
                    //    currentRow++;
                    //}

                   // var currentRow = 2;

                    worksheet.Cell(currentRow, 1).InsertTable(dt);
                    worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                    worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                    worksheet.Range("A1:H1").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Range("A1:H1").Style.Font.Bold = true;


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
                            "Bank Statement" + ".xlsx"
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


        public async Task<IActionResult> ExportToNotePad(int Month, string BankCode, string monthName, string bankName, int Format, int AllowanceCode)
        {


       
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
     
            BankStatement model = new BankStatement();
            model.BankStatementFormat = new MasterBase();
            model.PayrollMonth = new MasterBase();
            model.PayrollMonth.Code = Month;

            model.Allowance = new Dropdown();
            model.Allowance.Code = AllowanceCode;
         
            if (Format==12)
            {
               
                model.BankStatementFormat.Code = Format;
                model.Filter = Global.GlobalFilter;
                model.TYPE = "NOTEPAD";
                if (BankCode != null)
                {
                    string[] values = BankCode.Split(',');
                    for (int i = 0; i < values.Length; i++)
                    {
                        values[i] = values[i].Trim();
                    }

                    model.Banks = Array.ConvertAll(values, int.Parse);
                }
                var dataset = await _BankstatementReportService.GetBankStatement_Excel(model);
                DataTable dt = dataset.Tables[0];

                if (dt.Rows.Count != 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        for (int i = 0; i < dt.Columns.Count; i++)
                        {
                            // Append the data from each column and ensure proper formatting
                            sb.Append(dr[i].ToString().Trim());

                            // Add a separator (e.g., comma for CSV format) between columns, but not after the last column
                            //if (i < dt.Columns.Count - 1)
                            //{
                            //    sb.Append();
                            //}
                        }

                        // Add a new line after each row
                        sb.AppendLine();
                    }
                }

                return File(System.Text.Encoding.UTF8.GetBytes(sb.ToString()), "text/csv", "BankStatement.txt");
            }
            else
            {
                if (BankCode != null)
                {
                    string[] values = BankCode.Split(',');
                    for (int i = 0; i < values.Length; i++)
                    {
                        values[i] = values[i].Trim();
                    }

                    model.Banks = Array.ConvertAll(values, int.Parse);
                }
                model.BankStatementFormat.Code = Format;
                model.Filter = Global.GlobalFilter;
                var dataset = await _BankstatementReportService.GetBankStatement_Excel(model);
                DataTable dt = dataset.Tables[0];

                if (dt.Rows.Count != 0)
                {
                    foreach (DataRow dr in dataset.Tables[0].Rows)
                    {

                        for (int i = 0; i < 28; i++)
                        {
                            sb.AppendFormat(dr[i].ToString().Replace("\\s+", "").Trim());
                            // sb.AppendFormat(dr[i].ToString().Trim());
                            //sb.AppendLine();
                        }

                        sb.Append("\r\n");
                    }

                }


                return File(System.Text.Encoding.UTF8.GetBytes(sb.ToString()), "text/csv", "BankStatement.txt");

            }








        }
    }
}
