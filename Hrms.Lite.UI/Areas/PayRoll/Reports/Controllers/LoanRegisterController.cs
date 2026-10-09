
using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.PayRoll.Reports;
using Hrms.Lite.Shared.Account;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll;
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
    [Area("PayRoll")]
    [SessionAuthorize]
    public class LoanRegisterController : Controller
    {
        private readonly ILoanRegisterService _LoanRegisterService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        private readonly IDropdownService _dropdownService;

        public static class Global
        {
            public static Filter GlobalFilter;
            public static int balanceAmount;
            public static int principleAmount;
            public static int topUpAmount;
            public static int adjAmount;
            public static int alreadyPaidAmount;
            public static int EMIAmnt;

        }
        public LoanRegisterController(ILoanRegisterService LoanRegisterService, ICommonService commonService, IHelpers Helpers, IDropdownService dropdownService)
        {
            _LoanRegisterService = LoanRegisterService;
            _commonService = commonService;
            _helpers = Helpers;
            _dropdownService = dropdownService;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                LoanIssue model = new LoanIssue();
            Global.GlobalFilter = new Filter();

            ViewBag.LoanType = new SelectList(await _dropdownService.GetLoanTypeDropdown(), "Code", "Name");
            model = await _LoanRegisterService.GetLoanRegisterList(0, 0, Global.GlobalFilter);
            await fillfilterDropdown();
            return View("~/Areas/PayRoll/Reports/Views/LoanRegister/Index.cshtml", model);
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
        public async Task<IActionResult> GetList(int Tab, int type)
        {
            try
            {
                LoanIssue model = new LoanIssue();

                Global.GlobalFilter = new Filter();
                model = await _LoanRegisterService.GetLoanRegisterList(Tab, type,Global.GlobalFilter);
                return PartialView("~/Areas/PayRoll/Reports/Views/LoanRegister/_LoanRegisterList.cshtml", model);
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
        public async Task<ActionResult> GetLoanRegisterDetails(Guid EmployeeGI, int LoanIssueCode)
        {
            try
            {
                LoanIssue model = new LoanIssue();
               

                model = await _LoanRegisterService.GetLoanRegisterDetails(EmployeeGI, LoanIssueCode);
               
                
                return PartialView("~/Areas/PayRoll/Reports/Views/LoanRegister/_LoanRegisterPopUp.cshtml", model);
                
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
        public async Task<IActionResult> GetLoanRegisterList(Filter filter1,int type,int tab)
        {
            try
            {
                LoanIssue model = new LoanIssue();
                LoanIssue m = new LoanIssue();
                //model.EmployeeHeader = new EmployeeHeader();




                Filter filter = new Filter();
               

                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }
                if(tab != 0 || type != 0)
                {
                    model = await _LoanRegisterService.GetLoanRegisterList(tab, type, filter1);
                }
               if(type==0)
                    model = await _LoanRegisterService.GetLoanRegisterList(tab, type, filter1);
                m.LoanRegister = model.LoanRegister;
                int totalrows = 0;
                int totalrowsafterfiltering = 0;
                Global.principleAmount = model.PrincipleAmount;
                Global.topUpAmount = model.TopUpAmount;
                Global.adjAmount = model.AdjustmentAmount;
                Global.alreadyPaidAmount = model.AlreadyPaidAmount;
                Global.balanceAmount = model.TotalBalanceAmount;
                Global.EMIAmnt = model.TotalMonthlyEMI;
                if (model.LoanRegister == null || model.LoanRegister.Count() == 0)
                {
                  
                    return Json(new
                    {
     
                        data = "",
                        draw = filter1.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows,
                        principleAmount=model.PrincipleAmount,
                        topUpAmount=model.TopUpAmount,
                        adjAmount=model.AdjustmentAmount,
                        alreadyPaidAmount=model.AlreadyPaidAmount,
                        balanceAmount=model.TotalBalanceAmount,
                        monthlyEMI=model.TotalMonthlyEMI
                    });
                }




                if (model.LoanRegister.Count > 0)
                {
                    totalrows = m.LoanRegister[0].active;

                }

                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = m.LoanRegister[0].totalRecords;
                    return Json(new
                    {
                        data = model.LoanRegister,
                        draw = filter1.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        principleAmount = model.PrincipleAmount,
                        topUpAmount = model.TopUpAmount,
                        adjAmount = model.AdjustmentAmount,
                        alreadyPaidAmount = model.AlreadyPaidAmount,
                        balanceAmount = model.TotalBalanceAmount,
                        monthlyEMI = model.TotalMonthlyEMI,
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows//totalrowsafterfiltering
                    });
                }
                else
                {
                    // return Json(new { data = model.EmployeeProfileList, draw = filter1.draw, recordsTotal = totalrows, recordsFiltered = totalrows });
                    return Json(new
                    {
                        data = model.LoanRegister,
                        draw = filter1.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        principleAmount = model.PrincipleAmount,
                        topUpAmount = model.TopUpAmount,
                        adjAmount = model.AdjustmentAmount,
                        alreadyPaidAmount = model.AlreadyPaidAmount,
                        balanceAmount = model.TotalBalanceAmount,
                        monthlyEMI = model.TotalMonthlyEMI,
                        recordsFiltered = totalrows
                    });
                }


            }
            catch (Exception ex)
            {
                return View();
            }




        }



        public async Task<IActionResult> ExportToExcel(int Tab, int type,string typeName,int totRecords)
         {
         
            Filter filter = new Filter();
            ExcelPassword EP = new ExcelPassword();           
            EP = await _commonService.GetExcelPassword("Loan Register", "Excel");
          
            if (Global.GlobalFilter == null)
                Global.GlobalFilter = new Filter();
            else
            {
                Global.GlobalFilter.length = totRecords;
                Global.GlobalFilter.start = 0;
            }


         
            string FilterValues = await _helpers.GetFilterValues(Global.GlobalFilter);
            if (FilterValues == "")
            {
                FilterValues = "All";
            }
            string currDateTime = await _helpers.GetCurrentDate();
            

            //Get Dataset
            var datset = await _LoanRegisterService.GetLoanRegister_Excel(Tab, type, Global.GlobalFilter);
            DataTable dt = datset.Tables[0];


            //set EXCEL Column Heading 
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                
                    dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
                    
            }



            


            var LoginUser = HttpContext.Session.GetString("LoginUser");
            var CompanyName = HttpContext.Session.GetString("CompanyName");
            var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
            var GroupCompanyName = User.authUser.GroupCompanyName;

            using (var workbook = new XLWorkbook())
            {

                var worksheet = workbook.Worksheets.Add("Loan Register Report");
                int PendingCountnum = dt.Rows.Count + 6;

                worksheet.Range("A1:L" + PendingCountnum).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1:L" + PendingCountnum).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1:L" + PendingCountnum).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1:L" + PendingCountnum).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1:L" + PendingCountnum).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 11)));
                wsReportNameHeaderRange.Style.Font.Bold = true;
                wsReportNameHeaderRange.Style.Font.FontSize = 15;
                wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                wsReportNameHeaderRange.Merge();

                wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                wsReportNameHeaderRange.Style.Font.FontSize = 15;
                wsReportNameHeaderRange.Value = "Loan Register ";
                worksheet.Range("A5:L5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                worksheet.Cell(2, 1).Style.Font.Bold = true;
                worksheet.Cell(2, 1).Value = "Company Name";
                worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                worksheet.Cell(2, 3).Style.Font.Bold = true;
                worksheet.Cell(2, 3).Value = "Loan Type";
                worksheet.Cell(2, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                worksheet.Cell(2, 4).Value = typeName;


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
                var filterapplied = worksheet.Range("B4:L4").Merge();

                filterapplied.Value = FilterValues;

                var currentRow = 5;

                worksheet.Cell(currentRow, 1).InsertTable(dt);
                worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                worksheet.Range("A5:L5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                worksheet.Range("A5:L5").Style.Font.Bold = true;

                int rowCount = dt.Rows.Count + 6;
                worksheet.Range("A"+ rowCount, "L"+ rowCount).Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                worksheet.Range("A" + rowCount, "L" + rowCount).Style.Font.Bold = true;
                worksheet.Cell(rowCount, 1).Value = "Total";
                worksheet.Cell(rowCount, 5).Value = Global.principleAmount;
                worksheet.Cell(rowCount, 6).Value = Global.topUpAmount;
                worksheet.Cell(rowCount, 7).Value = Global.adjAmount;
                worksheet.Cell(rowCount, 8).Value = Global.EMIAmnt;
                worksheet.Cell(rowCount, 9).Value = Global.alreadyPaidAmount;
                worksheet.Cell(rowCount, 10).Value = Global.balanceAmount;

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
                        "Loan Register Report.xlsx"
                        );
                }
            }

        }
        public async Task<IActionResult> PopUpExportToExcel(Guid EmpGI,int LoanIssueCode)
        {
            LoanIssue model = new LoanIssue();
            Filter filter = new Filter();
            ExcelPassword EP = new ExcelPassword();
            EP = await _commonService.GetExcelPassword("Loan Register", "Excel");
            model = await _LoanRegisterService.GetLoanRegisterDetails(EmpGI, LoanIssueCode);

            int slno = 0;
           
            string currDateTime = await _helpers.GetCurrentDate();
            
            var LoginUser = HttpContext.Session.GetString("LoginUser");
            var CompanyName = HttpContext.Session.GetString("CompanyName");
            var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
            var GroupCompanyName = User.authUser.GroupCompanyName;

            using (var workbook = new XLWorkbook())
            {

                var worksheet = workbook.Worksheets.Add("Loan Register PopUp List");

                int PendingCountnum = model.LoanRegister_Details.info.Count + 6;


                worksheet.Range("A1:G" + PendingCountnum).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1:G" + PendingCountnum).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1:G" + PendingCountnum).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1:G" + PendingCountnum).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1:G" + PendingCountnum).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 6)));
                wsReportNameHeaderRange.Style.Font.Bold = true;
                wsReportNameHeaderRange.Style.Font.FontSize = 15;
                wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                wsReportNameHeaderRange.Merge();


                wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                wsReportNameHeaderRange.Style.Font.FontSize = 15;
                wsReportNameHeaderRange.Value = "Loan Register ";
                worksheet.Range("A6:G6").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                worksheet.Cell(2, 1).Style.Font.Bold = true;
                worksheet.Cell(2, 1).Value = "Group Company Name";
                worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                worksheet.Cell(2, 3).Style.Font.Bold = true;
                worksheet.Cell(2, 3).Value = "Login User";
                worksheet.Cell(2, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                worksheet.Cell(2, 4).Value = LoginUser;
                worksheet.Cell(2, 5).Value = "Date & Time";
                worksheet.Cell(2, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                worksheet.Cell(2, 6).Style.DateFormat.Format = "dd-MMM-yyyy   h:mm AM/PM";
                worksheet.Cell(2, 6).Value = currDateTime.ToString();



                    worksheet.Cell(3, 1).Style.Font.Bold = true;
                    worksheet.Cell(3, 1).Value = "Loan Type";
                    worksheet.Cell(3, 2).Value = model.LoanRegister_Details.LoanSettingsName;

                    worksheet.Cell(3, 3).Style.Font.Bold = true;
                    worksheet.Cell(3, 3).Value = " Status";
                    worksheet.Cell(3, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(3, 4).Value = model.LoanRegister_Details.LoanStatus;


                    worksheet.Cell(3, 5).Style.Font.Bold = true;
                    worksheet.Cell(3, 5).Value = " Principle Amount";
                    worksheet.Cell(3, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(3, 6).Value = model.LoanRegister_Details.PrincipleAmount;

                    worksheet.Cell(4, 1).Style.Font.Bold = true;
                    worksheet.Cell(4, 1).Value = "Topup Amount";
                    worksheet.Cell(4, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(4, 2).Value = model.LoanRegister_Details.TopUpAmount;


                    worksheet.Cell(4, 3).Style.Font.Bold = true;
                    worksheet.Cell(4, 3).Value = "Adjust. Amt.";
                    worksheet.Cell(4, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(4, 4).Value = model.LoanRegister_Details.AdjAmount;

                    worksheet.Cell(4, 5).Style.Font.Bold = true;
                    worksheet.Cell(4, 5).Value = "Monthly EMI";
                    worksheet.Cell(4, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(4, 6).Value = model.LoanRegister_Details.MonthlyEMI;

                    worksheet.Cell(5, 1).Style.Font.Bold = true;
                    worksheet.Cell(5, 1).Value = "Already Paid Amt.";
                    worksheet.Cell(5, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(5, 2).Value = model.LoanRegister_Details.AlreadyPaidAmount;

                    worksheet.Cell(5, 1).Style.Font.Bold = true;
                    worksheet.Cell(5, 1).Value = "Balance Amount";
                    worksheet.Cell(5, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(5, 2).Value = model.LoanRegister_Details.BalanceAmount;
                    
                    worksheet.Cell(5, 3).Style.Font.Bold = true;
                    worksheet.Cell(5, 3).Value = "Remaining Tenure";
                    worksheet.Cell(5, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(5, 4).Value = model.LoanRegister_Details.RemainingTenure;



                var currentRow = 6;
                #region Header
                worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 1).Value = "Sl.No";
                worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 2).Value = "Date";
                worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 3).Value = "Type";
                worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 4).Value = "Amount";
                worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 5).Value = "C/D";
                worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 6).Value = "Month";
                worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 7).Value = "Remarks";
                



                #endregion

                #region Body
                foreach (var item in model.LoanRegister_Details.info)
                {
                    currentRow++;
                    slno = slno + 1;
                    worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(currentRow, 1).Value = slno;
                    worksheet.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(currentRow, 2).Value = item.Date;
                    worksheet.Cell(currentRow, 2).Style.DateFormat.Format = "dd-MMM-yyyy";
                    worksheet.Cell(currentRow, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(currentRow, 3).Value = item.Type;
                    worksheet.Cell(currentRow, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(currentRow, 4).Value = item.Amount;
                    worksheet.Cell(currentRow, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(currentRow, 5).Value = item.CreditDebit;
                    worksheet.Cell(currentRow, 6).Style.DateFormat.Format = "MMM-yyyy";
                    worksheet.Cell(currentRow, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(currentRow, 6).Value = item.Month;
                    worksheet.Cell(currentRow, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(currentRow, 7).Value = item.Remarks;
                    worksheet.Cell(currentRow, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                   
                    worksheet.Columns(1, 8).Style.Alignment.WrapText = true;

                }
                #endregion
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
                        "Loan Register PopUp List.xlsx"
                        );
                }
            }

        }
    }
}
