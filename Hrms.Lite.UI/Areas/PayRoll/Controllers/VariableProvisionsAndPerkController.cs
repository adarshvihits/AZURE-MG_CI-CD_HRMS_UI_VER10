using ClosedXML.Excel;
using ExcelDataReader;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.PayRoll;
using Hrms.Lite.Shared.Account;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using Hrms.Lite.Shared.PayRoll;
using Hrms.Lite.Shared.PayRoll.Generic.Master;
using Microsoft.AspNetCore.Hosting;
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

namespace Hrms.Lite.UI.Areas.PayRoll.Controllers
{
    [Area("PayRoll")]
    [SessionAuthorize]
    public class VariableProvisionsAndPerkController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly IVariableProvisionsAndPerkService _variableProvisionsAndPerkService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        private readonly IWebHostEnvironment _environment;
        public static class Global
        {
            public static int Month;
            public static int Allowance;
            public static Filter GlobalFilter;

        }
        public VariableProvisionsAndPerkController(IDropdownService dropdownService, IVariableProvisionsAndPerkService VariableProvisionsAndPerkService, ICommonService commonService, IHelpers helpers, IWebHostEnvironment env)
        {
            _variableProvisionsAndPerkService = VariableProvisionsAndPerkService;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = helpers;
            _environment = env;

        }
        public async Task<IActionResult> Index()
        {
            try
            {
                VariableAllowance model = new VariableAllowance();
                ViewBag.AllowanceMonth = new SelectList(await _dropdownService.GetSalaryProcessing_MonthDropdown(0), "Code", "Name");
                ViewBag.ProvisionType = new SelectList(await _dropdownService.GetVariableProvisionAndPerk_AllowanceDropdown(), "Code", "Name");
                Filter filter = new Filter();
                await _variableProvisionsAndPerkService.GetVariableProvisionList(0, 0, filter);
                await fillfilterDropdown();
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

        [HttpPost]
        public async Task<IActionResult> GetVariableProvisionsAndPerkList(Filter filter1, int MonthCode, int AllowanceCode)
        {
            try
            {
                VariableAllowance model = new VariableAllowance();
                Filter filter = new Filter();
                


                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }


                if (MonthCode != 0 && AllowanceCode != 0)
                {
                    model = await _variableProvisionsAndPerkService.GetVariableProvisionList(MonthCode, AllowanceCode, filter1);
                }
               
                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.VariableAllowanceList == null || model.VariableAllowanceList.Count() == 0)
                {

                    return Json(new
                    {
                        data = "",
                        draw = filter1.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows
                    });
                }




                if (model.VariableAllowanceList.Count > 0)
                {
                    totalrows = model.VariableAllowanceList[0].active;

                }


                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = model.VariableAllowanceList[0].totalRecords;
                    return Json(new
                    {
                        data = model.VariableAllowanceList,
                        draw = filter1.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows//totalrowsafterfiltering
                    });
                }
                else
                {
                    return Json(new
                    {
                        data = model.VariableAllowanceList,
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

        [HttpPost]
        public async Task<ActionResult> EmployeeWiseUpdate(int TDSUpdatedAmount, int UpdatedAmount, int allowanceMonth, int allowanceType, Guid EmployeeGI, string Remark)
        {
            try
            {
                VariableAllowance model = new VariableAllowance();
                model.AllowanceMonth = new PayrollMonth();
                model.EmployeeGI = EmployeeGI;
                model.Allowance = new Allowance();
                model.Amount = UpdatedAmount;
                model.TDSAmount = TDSUpdatedAmount;
                model.AllowanceMonth.Code = allowanceMonth;
                model.Allowance.Code = allowanceType;
                model.Remark = Remark;
                var response = await _variableProvisionsAndPerkService.EmployeeWiseUpdate(model);
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

        public ActionResult GetBatchUpdateSlider(int MonthCode, int AllowanceCode, string MonthName, string AllowanceName)
        {
            try
            {
                VariableAllowance model = new VariableAllowance();
                model.AllowanceMonth = new PayrollMonth();
                model.Allowance = new Allowance();
                model.AllowanceMonth.Name = MonthName;
                model.Allowance.Code = AllowanceCode;
                model.Allowance.Name = AllowanceName;
                model.AllowanceMonth.Code = MonthCode;
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
        [HttpPost]
        public async Task<IActionResult> BatchUpdate(VariableAllowance model)
        {

            try
            {
                Guid EmployeeGI = model.EmployeeGI;
                model.Filter = Global.GlobalFilter;
                var response = await _variableProvisionsAndPerkService.VariableProvisionsBatchUpdate(model);
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

        public async Task<ActionResult> CountCheckAJAX(int Month, int Allowance, int totRecords, string Type)
        {
            try
            {
                DataSet dataset = new DataSet();
                object ResponseEntity = new object();
                if (Global.GlobalFilter == null)
                    Global.GlobalFilter = new Filter();
                else
                {
                    Global.GlobalFilter.length = totRecords;
                    Global.GlobalFilter.start = 0;
                }
                dataset = await _variableProvisionsAndPerkService.VariableAllowanceList_Excel(Month, Allowance, Global.GlobalFilter, Type);
                DataTable dt = dataset.Tables[0];

                if (dt.Columns.Count == 0)
                {
                    ResponseEntity = false;
                    return Json(ResponseEntity);

                }
                else
                {
                    //Global._dataset = ds;
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
        public async Task<IActionResult> ExportToExcel(int Month, int Allowance, string AllowanceMonth, string AllowanceType, int totRecords, string Type)

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
                EP = await _commonService.GetExcelPassword("Variable Provisions & Perk List (" + AllowanceType + ")", "Excel");

                if (FilterValues == "")
                {
                    FilterValues = "All";
                }

                string currDateTime = await _helpers.GetCurrentDate();
                

                //Get Datset
                var dataset = await _variableProvisionsAndPerkService.VariableAllowanceList_Excel(Month, Allowance, Global.GlobalFilter, Type);
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
                    var worksheet = workbook.Worksheets.Add("Variable Provisions & Perk");
                    int count = dt.Rows.Count + 6;


                    worksheet.Range("A1:H" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:H" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:H" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:H" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:H" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 7)));
                    wsReportNameHeaderRange.Style.Font.Bold = true;
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Merge();
                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191); ;
                    wsReportNameHeaderRange.Value = "Variable Provisions & Perk(" + AllowanceType + ")";

                    worksheet.Cell(2, 1).Style.Font.Bold = true;
                    worksheet.Cell(2, 1).Value = "Group Company Name";
                    worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                    worksheet.Cell(2, 3).Style.Font.Bold = true;
                    worksheet.Cell(2, 3).Value = "Month";
                    worksheet.Cell(2, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(2, 4).Style.DateFormat.Format = "MMM-yyyy";
                    worksheet.Cell(2, 4).Value = AllowanceMonth;
                    worksheet.Cell(2, 5).Style.Font.Bold = true;
                    worksheet.Cell(2, 5).Value = "Allowance";
                    worksheet.Cell(2, 6).Value = AllowanceType;
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
                    worksheet.Range("B4:H4").Merge();
                    worksheet.Cell("B4").Value = FilterValues;



                    var currentRow = 5;

                    worksheet.Cell(currentRow, 1).InsertTable(dt);
                    worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                    worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                    worksheet.Range("A5:H5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Range("A5:H5").Style.Font.Bold = true;


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
                            "Variable Provisions and Perk List (" + AllowanceType + ") List.xlsx"
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
        public async Task<IActionResult> ExportToBankStatement(int Month, int Allowance, string AllowanceMonth, string AllowanceType, int totRecords, string Type)

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
                EP = await _commonService.GetExcelPassword("Variable Provisions & Perk List (" + AllowanceType + ")", "Excel");

                if (FilterValues == "")
                {
                    FilterValues = "All";
                }

                string currDateTime = await _helpers.GetCurrentDate();
                

                //Get Datset
                var dataset = await _variableProvisionsAndPerkService.VariableAllowanceList_Excel(Month, Allowance, Global.GlobalFilter, Type);
                DataTable dt = dataset.Tables[0];
                DataTable dt1 = dataset.Tables[1];


                //set EXCEL Column Heading 
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (dt.Columns[i].ColumnName == "Sl.No" || dt.Columns[i].ColumnName == "Account" || dt.Columns[i].ColumnName == "Employee Name")
                        dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.ToUpper();
                    else
                        dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
                }





                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Bank Statement Incentive");
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
                    var amtmessageText = "Please find enclosed herewith Our cheque xxxx dt xxxx Rs." + dt1.Rows[0].ItemArray[0] + " /- drawn on ";
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
                    worksheet.Cell(hardcodedStartRow, 1).Value = "Kindly mention the transfer as Salary for the month of " + dt1.Rows[0].ItemArray[2] + ", ";
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
                            "Bank Statement Incentive.xlsx"
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
        public async Task<IActionResult> ImportFromExcel(string Allowance, bool Append, VariableAllowance model)
        {
            string path = Path.Combine(this._environment.WebRootPath, "Uploads");
            string fileName = model.Excel.FileData.FileName;
            string[] ext = model.Excel.FileData.FileName.Split('.');
            string AlwFilename = ext[0].ToString();
            string AllowanceName = Allowance;
            //string AllowanceName = Allowance + ".xlsx";

            try
            {
                if (AlwFilename != AllowanceName)
                {
                    return Json(new ResponseEntity("The file name should match the selected allowance.", false));
                }
                model.VariableAllowanceList = new List<VariableAllowance>();
                string wwwPath = this._environment.WebRootPath;
                string contentPath = this._environment.ContentRootPath;


                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                {
                    model.Excel.FileData.CopyTo(stream);

                }
                try
                {
                    using (var stream = System.IO.File.Open(path + "\\" + fileName, FileMode.Open, FileAccess.Read))
                    {

                        using (var reader = ExcelReaderFactory.CreateReader(stream))
                        {
                            List<VariableAllowance> List = new List<VariableAllowance>();
                            var readerrr = ExcelReaderFactory.CreateReader(stream);
                            while (reader.Read()) //Each row of the file
                            {
                                var item = new VariableAllowance();
                                if (reader.GetValue(0) != null)
                                {
                                    item.EmployeeID = reader.GetValue(0).ToString();
                                }
                                if (reader.GetValue(1) != null)
                                {
                                    item.EmployeeName = reader.GetValue(1).ToString();
                                }
                                if (reader.GetValue(2) != null)
                                {
                                    item.Remark = reader.GetValue(2).ToString();
                                }
                                if (reader.GetValue(3) != null)
                                {
                                    if (reader.GetValue(3).GetType().Name != "String")
                                    {
                                        item.Amount = Convert.ToDecimal(reader.GetDouble(3));
                                    }
                                    else
                                    {
                                        item.AmountStr = reader.GetValue(3).ToString();
                                    }
                                }
                                if (item.Amount.ToString().Contains("."))
                                {
                                    return Json(new ResponseEntity("Decimal values will not be supported.Please round off the decimal values and try again", false));
                                }

                                List.Add(item);

                            }
                            if (List[0].EmployeeID != "Employee ID" || List[0].EmployeeName != "Employee Name" || List[0].AmountStr != "Amount" || List[0].Remark != "Remark")
                            {
                                reader.Close();
                                if (System.IO.File.Exists(path + "\\" + fileName))
                                {
                                    System.IO.File.Delete(path + "\\" + fileName);
                                }
                                return Json(new ResponseEntity("Excel File not in the prescribed format", false));
                            }
                            model.VariableAllowanceList = List;
                            model.VariableAllowanceList.RemoveAt(0);
                        }
                    }
                }
                catch (Exception e)
                {
                    // Check if file exists with its full path    
                    if (System.IO.File.Exists(path + "\\" + fileName))
                    {
                        System.IO.File.Delete(path + "\\" + fileName);
                    }
                    return Json(new ResponseEntity("Excel File not in the prescribed format", false));
                }

                model.Excel.FileData = null;
                model.Append = Append;
                var response = await _variableProvisionsAndPerkService.ImportFromExcel(model);
                // Check if file exists with its full path    
                if (System.IO.File.Exists(path + "\\" + fileName))
                {
                    System.IO.File.Delete(path + "\\" + fileName);
                }
                if (response.Message != "UPDATED")
                {
                    model.VariableAllowanceList = response.Data;
                    model.Message = response.Message;
                    return PartialView("_UnmatchedList", model);
                }
                else
                {
                    response.Message = "Uploaded Successfully";
                    return Json(response);
                }
            }
            catch (UnauthorizedAccessException e)
            {

                return RedirectToAction("Login", "Account", new { Area = "" });
            }
            catch (Exception e)
            {
                // Check if file exists with its full path    
                if (System.IO.File.Exists(path + "\\" + fileName))
                {
                    System.IO.File.Delete(path + "\\" + fileName);
                }
                return RedirectToAction("Error", "Account", new { Area = "" });
            }


        }

    }
}
