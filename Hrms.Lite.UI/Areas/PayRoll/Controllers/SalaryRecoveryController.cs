using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using ExcelDataReader;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.PayRoll;
using Hrms.Lite.Shared.Account;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using Hrms.Lite.Shared.PayRoll;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Spire.Xls;

namespace Hrms.Lite.UI.Areas.PayRoll.Controllers
{
    [Area("PayRoll")]
    [SessionAuthorize]
    public class SalaryRecoveryController : Controller
    {

        private readonly IDropdownService _dropdownService;
        private readonly ISalaryRecoveryService _SalaryRecoveryService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        private readonly IWebHostEnvironment _environment;
        public static class Global
        {
            public static Filter GlobalFilter;
            public static int monthCode;

        }
        public SalaryRecoveryController(ISalaryRecoveryService SalaryRecoveryService, IDropdownService dropdownService, ICommonService commonService, IHelpers Helpers, IWebHostEnvironment env)
        {
            _SalaryRecoveryService = SalaryRecoveryService;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = Helpers;
            _environment = env;

        }
        public async Task<IActionResult> Index()
        {
            try
            {
                SalaryRecovery model = new SalaryRecovery();
                ViewBag.Month = new SelectList(await _dropdownService.GetSalaryProcessing_MonthDropdown(0), "Code", "Name");
                ViewBag.AllowanceType = new SelectList(await _dropdownService.GetRecoveryAllowanceDropdown(), "Code", "Name");
                Filter filter = new Filter();
                filter.length = 10;
                model = await _SalaryRecoveryService.GetList(0, 0, filter);
                model.filter = await _commonService.GetFilterDropdowns();
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
        public async Task<IActionResult> ApplyFilter(Filter filter, int Month, int PayrollGroupCode)
        {
            try
            {
                Global.GlobalFilter = filter;
                SalaryRecovery model = new SalaryRecovery();
                model = await _SalaryRecoveryService.GetList(Month, PayrollGroupCode, filter);
                return PartialView("_SalaryRecoveryList", model);
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
        public async Task<IActionResult> GetSalaryRecoveryList(Filter filter1, int MonthCode, int AllowanceCode)
        {
            try
            {
                SalaryRecovery model = new SalaryRecovery();
                SalaryRecovery m = new SalaryRecovery();

                Filter filter = new Filter();
                Global.GlobalFilter = filter1;


                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }


                if (MonthCode != 0 && AllowanceCode != 0)
                {
                    model = await _SalaryRecoveryService.GetList(MonthCode, AllowanceCode, filter1);
                }
                if (model == null || model.SalaryRecoveryList == null || model.SalaryRecoveryList.Count() == 0)
                {
                    m.SalaryRecoveryList = new List<SalaryRecovery>();
                    model = new SalaryRecovery();
                    model.SalaryRecoveryList = new List<SalaryRecovery>();
                }
                else
                {
                    m.SalaryRecoveryList = model.SalaryRecoveryList;
                }
                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.SalaryRecoveryList == null || model.SalaryRecoveryList.Count() == 0)
                {

                    return Json(new
                    {
                        data = "",
                        draw = filter1.draw,
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows
                    });
                }




                if (model.SalaryRecoveryList.Count > 0)
                {
                    totalrows = m.SalaryRecoveryList[0].active;

                }


                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = m.SalaryRecoveryList[0].totalRecords;
                    return Json(new
                    {
                        data = model.SalaryRecoveryList,
                        draw = filter1.draw,
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows
                    });
                }
                else
                {
                    return Json(new
                    {
                        data = model.SalaryRecoveryList,
                        draw = filter1.draw,
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
        public async Task<IActionResult> Save(SalaryRecovery model, int AllowanceCode)
        {
            try
            {
                model.Allowance = new Allowance();
                model.Allowance.Code = AllowanceCode;
                var response = await _SalaryRecoveryService.Save(model);
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
        public async Task<IActionResult> ExportToExcel(Filter filter, int Month, int Allowance, string MonthName, string AllowanceType, int totRecords)

        {
            try
            {
                filter.length = totRecords;
                filter.start = 0;
                string FilterValues = await _helpers.GetFilterValues(filter);

                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Salary Recovery List (" + AllowanceType + ")", "Excel");

                if (FilterValues == "")
                {
                    FilterValues = "All";
                }


                string currDateTime = await _helpers.GetCurrentDate();
                

                //Get Datset
                var dataset = await _SalaryRecoveryService.SalaryRecoveryList_Excel(Month, Allowance, filter);
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


                    var worksheet = workbook.Worksheets.Add("Salary Recovery");
                    int rowcount = dt.Rows.Count + 6;

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
                    wsReportNameHeaderRange.Merge();
                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                    wsReportNameHeaderRange.Value = "Salary Recovery List (" + AllowanceType + ")";


                    worksheet.Cell(2, 1).Style.Font.Bold = true;
                    worksheet.Cell(2, 1).Value = "Group Company Name";
                    worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();



                    worksheet.Cell(2, 3).Style.Font.Bold = true;
                    worksheet.Cell(2, 3).Value = "Month";
                    worksheet.Cell(2, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(2, 4).Style.DateFormat.Format = "MMM-yyyy";
                    worksheet.Cell(2, 4).Value = MonthName;



                    worksheet.Cell(2, 5).Style.Font.Bold = true;
                    worksheet.Cell(2, 5).Value = "Allowance";
                    worksheet.Cell(2, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(2, 6).Value = AllowanceType;




                    worksheet.Cell(4, 1).Style.Font.Bold = true;
                    worksheet.Cell(4, 1).Value = "Login User";
                    worksheet.Cell(4, 2).Value = LoginUser;
                    worksheet.Cell(4, 3).Style.Font.Bold = true;
                    worksheet.Cell(4, 3).Value = "Date & Time";
                    worksheet.Cell(4, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(4, 4).Style.DateFormat.Format = "dd-MMM-yyyy   h:mm AM/PM";
                    worksheet.Cell(4, 4).Value = currDateTime.ToString();
                    worksheet.Cell(4, 5).Style.Font.Bold = true;
                    worksheet.Cell(4, 5).Value = "Total Count";
                    worksheet.Cell(4, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(4, 6).Value = dt.Rows.Count;
                    worksheet.Cell(5, 1).Style.Font.Bold = true;
                    worksheet.Cell(5, 1).Value = "Filter Applied";
                    worksheet.Range("B5", colName + 5).Merge();
                    worksheet.Cell("B5").Value = FilterValues;




                    var currentRow = 6;

                    worksheet.Cell(currentRow, 1).InsertTable(dt);
                    worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                    worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                    worksheet.Range("A6", colName + 6).Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Range("A6", colName + 6).Style.Font.Bold = true;


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
                          "Salary Recovery List (" + AllowanceType + ").xlsx"
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


        public async Task<IActionResult> ImportFromExcel(string Allowance, SalaryRecovery model)
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

                model.SalaryRecoveryList = new List<SalaryRecovery>();
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
                            List<SalaryRecovery> List = new List<SalaryRecovery>();
                            var readerrr = ExcelReaderFactory.CreateReader(stream);
                            while (reader.Read()) //Each row of the file
                            {
                                var item = new SalaryRecovery();
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
                                    if (reader.GetValue(2).GetType().Name != "String")
                                    {
                                        item.Amount = Convert.ToDecimal(reader.GetDouble(2));
                                    }
                                    else
                                    {
                                        item.AmountStr = reader.GetValue(2).ToString();
                                    }
                                }
                                //if (item.Amount.ToString().Contains("."))
                                //{
                                //    return Json(new ResponseEntity("Decimal values will not be supported.Please round off the decimal values and try again", false));
                                //}
                                //else
                                //{
                                List.Add(item);
                                //}
                            }
                            if (List[0].EmployeeID != "Employee ID" || List[0].EmployeeName != "Name" || List[0].AmountStr != "Amount")
                            {
                                reader.Close();
                                if (System.IO.File.Exists(path + "\\" + fileName))
                                {
                                    System.IO.File.Delete(path + "\\" + fileName);
                                }
                                return Json(new ResponseEntity("Excel File not in the prescribed format", false));
                            }
                            model.SalaryRecoveryList = List;

                            model.SalaryRecoveryList.RemoveAt(0);
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
                //model.Append = Append;
                var response = await _SalaryRecoveryService.ImportFromExcel(model);
                // Check if file exists with its full path    
                if (System.IO.File.Exists(path + "\\" + fileName))
                {
                    System.IO.File.Delete(path + "\\" + fileName);
                }
                if (response.Message != "UPDATED")
                {
                    model.SalaryRecoveryList = response.Data;
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
