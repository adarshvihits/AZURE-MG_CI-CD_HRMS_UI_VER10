using ClosedXML.Excel;
using ExcelDataReader;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.TimeOffice;
using Hrms.Lite.Shared.Account;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TimeOffice;
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

namespace Hrms.Lite.UI.Areas.TimeOffice.Controllers
{
    [Area("TimeOffice")]
    [SessionAuthorize]
    public class ExceptionEmployeesController : Controller
    {
        private readonly IExceptionEmployeesService _service;
        private readonly ICommonService _commonService;
        private readonly IDropdownService _dropdownService;
        private readonly IWebHostEnvironment _environment;
        private readonly IHelpers _helpers;
        public ExceptionEmployeesController(IExceptionEmployeesService service, ICommonService commonService, IDropdownService dropdownService, IWebHostEnvironment env, IHelpers helpers)
        {
            _service = service;
            _commonService = commonService;
            _dropdownService = dropdownService;
            _environment = env;
            _helpers = helpers;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                ExceptionEmployees model = new ExceptionEmployees();
                Filter filter = new Filter();
                filter.length = 10;
                model = await _service.GetEmployeeList(0, 0, filter);
                await fillfilterDropdown();
            ViewBag.Month = new SelectList(await _dropdownService.GetMonthly_AttendanceProcessing_MonthDropdown(0), "Code", "Name");
            //ViewBag.Month1 = new SelectList(await _dropdownService.GetMonthDropdown(), "Code", "Name");
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

        public async Task<IActionResult> GetList(int MonthCode,int BasedOn, Filter Filter)
        {
            try
            {
                ExceptionEmployees model = new ExceptionEmployees();


                if (Filter.searchValue == "undefined" || Filter.searchValue == null)
                {
                    Filter.searchValue = "";
                }
                if (MonthCode != 0)
                {
                    model = await _service.GetEmployeeList(MonthCode, BasedOn, Filter);
                }
                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.ExceptionEmployeesList == null || model.ExceptionEmployeesList.Count() == 0)
                {
                    return Json(new
                    {
                        data = "",
                        draw = Filter.draw,
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows
                    });
                }

                if (model.ExceptionEmployeesList.Count > 0)
                {
                    totalrows = model.ExceptionEmployeesList[0].active;

                }
                if (Filter.searchValue != "")
                {
                    totalrowsafterfiltering = model.ExceptionEmployeesList[0].TotalRecords;
                    return Json(new
                    {
                        data = model.ExceptionEmployeesList,
                        draw = Filter.draw,
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows
                    });
                }
                else
                {
                    return Json(new
                    {
                        data = model.ExceptionEmployeesList,
                        draw = Filter.draw,
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows
                    });
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

            //GeneralAuthoritySettings model = new GeneralAuthoritySettings();
            //model = await _service.GetEmployeeList(Type, LocationCode);

            //return PartialView("_AuthorityList", model);

        }

        public async Task<ActionResult> AddEmployee(Guid EmployeeGI, string Type, int MonthCode,string? remarks)
        {
            try
            {
                var response = await _service.AddEmployee(EmployeeGI, Type, MonthCode, remarks);

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

        public async Task<ActionResult> BatchUpdate(int MonthCode,int BasedOn,string TypeValue, Filter Filter)
        {
            try
            {

                var response = await _service.BatchUpdate(MonthCode, BasedOn, TypeValue, Filter);

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
        public async Task<IActionResult> ExportToExcel(int MonthCode, Filter Filter, int TableLength,int BasedOn, string Type, string searchValue)
        {
            try
            {

                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Employee (" + Type + ")  List", "Excel");

                string currDateTime = await _helpers.GetCurrentDate();
                
                Filter.length = TableLength;
                Filter.searchValue = searchValue;
                var dataset = await _service.EmployeeList_Excel(MonthCode, BasedOn, Filter);
                DataTable dt = dataset.Tables[0];


                //set EXCEL Column Heading 
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
                }

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Employee("+Type+")List");
                    int count = dt.Rows.Count + 2;


                    worksheet.Range("A1:F" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:F" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:F" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:F" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:F" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 5)));
                    wsReportNameHeaderRange.Style.Font.Bold = true;
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Merge();
                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                    wsReportNameHeaderRange.Value = "Employee (" + Type + ")  List";

                    //worksheet.Cell(2, 1).Style.Font.Bold = true;
                    //worksheet.Cell(2, 1).Value = "Group Company Name";
                    //worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                    //worksheet.Cell(2, 1).Style.Font.Bold = true;
                    //worksheet.Cell(2, 1).Value = "Login User";
                    //worksheet.Cell(2, 2).Value = LoginUser;
                    //worksheet.Cell(2, 1).Style.Font.Bold = true;
                    //worksheet.Cell(2, 1).Value = "Date & Time";
                    //worksheet.Cell(2, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    //worksheet.Cell(2, 1).Style.DateFormat.Format = "dd-MMM-yyyy   h:mm AM/PM";
                    //worksheet.Cell(2, 1).Value = currDateTime.ToString();
                    //worksheet.Cell(2, 2).Style.Font.Bold = true;
                    //worksheet.Cell(2, 1).Value = "Total Count";
                    //worksheet.Cell(2, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    //worksheet.Cell(2, 2).Value = dt.Rows.Count;



                    var currentRow = 2;
                    worksheet.Cell(currentRow, 1).InsertTable(dt);
                    worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                    worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                    worksheet.Range("A2:F2").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Range("A2:F2").Style.Font.Bold = true;


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
                            "Employee (" + Type + ")   List.xlsx"
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


        public async Task<IActionResult> ImportFromExcel( int BasedOn, ExceptionEmployees model)
        {
            string path = Path.Combine(this._environment.WebRootPath, "Uploads");
            string fileName = model.Excel.FileData.FileName;
            try
            {
                model.ExceptionEmployeesList = new List<ExceptionEmployees>();
                string wwwPath = this._environment.WebRootPath;
                string contentPath = this._environment.ContentRootPath;
                model.BasedOn = BasedOn;

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
                            List<ExceptionEmployees> List = new List<ExceptionEmployees>();
                            var readerrr = ExcelReaderFactory.CreateReader(stream);
                            while (reader.Read()) //Each row of the file
                            {
                                var item = new ExceptionEmployees();
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

                                    item.Remarks = reader.GetValue(2).ToString();

                                }
                                if (reader.GetValue(3) != null)
                                {

                                    item.Status = reader.GetValue(3).ToString();

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
                            if (List[0].EmployeeID != "Employee ID" || List[0].EmployeeName != "Employee Name" || List[0].Remarks != "Remark" || List[0].Status != "Status")
                            {
                                reader.Close();
                                if (System.IO.File.Exists(path + "\\" + fileName))
                                {
                                    System.IO.File.Delete(path + "\\" + fileName);
                                }
                                return Json(new ResponseEntity("Excel File not in the prescribed format", false));
                            }
                            model.ExceptionEmployeesList = List;

                            model.ExceptionEmployeesList.RemoveAt(0);
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
                var response = await _service.ImportFromExcel(model);
                // Check if file exists with its full path    
                if (System.IO.File.Exists(path + "\\" + fileName))
                {
                    System.IO.File.Delete(path + "\\" + fileName);
                }
                if (response.Message != "UPDATED")
                {
                    model.ExceptionEmployeesList = response.Data;
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


        public async Task<IActionResult> History()
        {
            try
            {
                ExceptionEmployees model = new ExceptionEmployees();
                ViewBag.Month1 = new SelectList(await _dropdownService.GetMonthDropdown(), "Code", "Name");
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


        public async Task<IActionResult> Getdatalist(int MonthCode, Filter Filter)
        {
            try
            {
                ExceptionEmployees model = new ExceptionEmployees();


                if (Filter.searchValue == "undefined" || Filter.searchValue == null)
                {
                    Filter.searchValue = "";
                }
                if (MonthCode != 0)
                {
                    model = await _service.TransactionHistoryList(MonthCode, Filter);
                }
                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.TransactionHistoryList == null || model.TransactionHistoryList.Count() == 0)
                {
                    return Json(new
                    {
                        data = "",
                        draw = Filter.draw,
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows
                    });
                }

                if (model.TransactionHistoryList.Count > 0)
                {
                    totalrows = model.TransactionHistoryList[0].active;

                }
                if (Filter.searchValue != "")
                {
                    totalrowsafterfiltering = model.TransactionHistoryList[0].TotalRecords;
                    return Json(new
                    {
                        data = model.TransactionHistoryList,
                        draw = Filter.draw,
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows
                    });
                }
                else
                {
                    return Json(new
                    {
                        data = model.TransactionHistoryList,
                        draw = Filter.draw,
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows
                    });
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


        public async Task<IActionResult> HistoryExportToExcel(int MonthCode, Filter Filter, int TableLength)
        {
            try
            {

                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Employee Exception List", "Excel");

                string currDateTime = await _helpers.GetCurrentDate();
                
                Filter.length = TableLength;
                var dataset = await _service.HistoryEmployeeList_Excel(MonthCode,  Filter);
                DataTable dt = dataset.Tables[0];


                //set EXCEL Column Heading 
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
                }

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Employee Exception List");
                    int count = dt.Rows.Count + 2;


                    worksheet.Range("A1:F" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:F" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:F" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:F" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:F" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 5)));
                    wsReportNameHeaderRange.Style.Font.Bold = true;
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Merge();
                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                    wsReportNameHeaderRange.Value = "Employee Exception  List";

                    //worksheet.Cell(2, 1).Style.Font.Bold = true;
                    //worksheet.Cell(2, 1).Value = "Group Company Name";
                    //worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                    //worksheet.Cell(2, 1).Style.Font.Bold = true;
                    //worksheet.Cell(2, 1).Value = "Login User";
                    //worksheet.Cell(2, 2).Value = LoginUser;
                    //worksheet.Cell(2, 1).Style.Font.Bold = true;
                    //worksheet.Cell(2, 1).Value = "Date & Time";
                    //worksheet.Cell(2, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    //worksheet.Cell(2, 1).Style.DateFormat.Format = "dd-MMM-yyyy   h:mm AM/PM";
                    //worksheet.Cell(2, 1).Value = currDateTime.ToString();
                    //worksheet.Cell(2, 2).Style.Font.Bold = true;
                    //worksheet.Cell(2, 1).Value = "Total Count";
                    //worksheet.Cell(2, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    //worksheet.Cell(2, 2).Value = dt.Rows.Count;



                    var currentRow = 2;
                    worksheet.Cell(currentRow, 1).InsertTable(dt);
                    worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                    worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                    worksheet.Range("A2:F2").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Range("A2:F2").Style.Font.Bold = true;


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
                            "Employee Exception List.xlsx"
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
