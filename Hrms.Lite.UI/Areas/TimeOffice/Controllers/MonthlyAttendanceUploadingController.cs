using ClosedXML.Excel;
using ExcelDataReader;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.TimeOffice;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TimeOffice;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
    public class MonthlyAttendanceUploadingController : Controller
    {
        private readonly IMonthlyAttendanceUploadingService _service;
        private readonly ICommonService _commonService;
        private readonly IDropdownService _dropdownService;
        private readonly IWebHostEnvironment _environment;
        private readonly IHelpers _helpers;

        public MonthlyAttendanceUploadingController(IMonthlyAttendanceUploadingService service, ICommonService commonService, IWebHostEnvironment environment,
            IDropdownService dropdownService, IHelpers helpers)
        {
            _service = service;
            _commonService = commonService;
            _dropdownService = dropdownService;
            _environment = environment;
            _helpers = helpers;

        }
      
        public async Task<IActionResult> Index()
        {
            try
            {
                MonthlyAttendanceUploading model = new MonthlyAttendanceUploading();
                model.Filter = new Filter();

                ViewBag.PayrollGroup = new SelectList(await _dropdownService.GetPayrollGroupDropdown(), "Code", "Name");
                ViewBag.Month = new SelectList(await _dropdownService.GetMonthly_AttendanceProcessing_MonthDropdown(0), "Code", "Name");
                Filter filter = new Filter();
                filter.length = 10;
                model = await _service.GetEmployeeList(0, 0, filter);
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
        [HttpGet]
        public async Task<IActionResult> GetMonth(int PayrollGrp)
        {
            try
            {
                var Months = await _dropdownService.GetMonthly_AttendanceProcessing_MonthDropdown(PayrollGrp);
                return Json(Months);
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
        public async Task<IActionResult> GetEmployeeList( Filter filter1, int Month, int PayrollGroup)
        {
            try
            {
                MonthlyAttendanceUploading model = new MonthlyAttendanceUploading();
               
                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }
                if (Month != 0 && PayrollGroup!=0)
                    model = await _service.GetEmployeeList(Month, PayrollGroup,filter1);

                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.MonthlyAttendanceUploading_Data == null || model.MonthlyAttendanceUploading_Data.Count() == 0)
                {

                    return Json(new
                    {
                        data = "",
                        draw = filter1.draw,
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows

                    });
                }

                if (model.MonthlyAttendanceUploading_Data.Count > 0)
                {
                    totalrows = model.MonthlyAttendanceUploading_Data[0].active;
                }


                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = model.MonthlyAttendanceUploading_Data[0].totalRecords;

                    return Json(new
                    {
                        data = model.MonthlyAttendanceUploading_Data,
                        draw = filter1.draw,
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows

                    });
                }
                else
                {

                    return Json(new
                    {
                        data = model.MonthlyAttendanceUploading_Data,
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


        public async Task<IActionResult> populateModel(string fileName, MonthlyAttendanceUploading model)
        {
            string path = Path.Combine(this._environment.WebRootPath, "Uploads");
            try
            {
                using (var stream = System.IO.File.Open(path + "\\" + fileName, FileMode.Open, FileAccess.Read))
                {

                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        List<MonthlyAttendanceUploading_Data> List = new List<MonthlyAttendanceUploading_Data>();

                        while (reader.Read()) //Each row of the file
                        {
                            var item = new MonthlyAttendanceUploading_Data();
                            if (reader.GetValue(0) != null)
                            {
                                item.EmployeeID = reader.GetValue(0).ToString();
                            }
                            if (reader.GetValue(1) != null)
                            {
                                item.FullName = reader.GetValue(1).ToString();
                            }
                            if (reader.GetValue(2) != null)
                            {
                                if (reader.GetValue(2).GetType().Name != "String")
                                {
                                    item.Lop = Convert.ToDecimal(reader.GetDouble(2));
                                }
                                else
                                {
                                    item.LopString = reader.GetValue(2).ToString();
                                }
                            }

                            if (reader.GetValue(3) != null)
                            {
                                if (reader.GetValue(2).GetType().Name != "String")
                                {
                                    item.OffdayPresent = Convert.ToDecimal(reader.GetDouble(3));
                                }
                                else
                                {
                                    item.OpString = reader.GetValue(3).ToString();
                                }
                            }

                            if (reader.GetValue(4) != null)
                            {
                                if (reader.GetValue(4).GetType().Name != "String")
                                {
                                    item.HolidayPresent = Convert.ToDecimal(reader.GetDouble(4));
                                }
                                else
                                {
                                    item.HpString = reader.GetValue(4).ToString();
                                }
                            }
                            if (reader.GetValue(5) != null)
                            {
                                if (reader.GetValue(5).GetType().Name != "String")
                                {
                                    item.LateHrs= Convert.ToDecimal(reader.GetDouble(5));
                                }
                                else
                                {
                                    item.LateHrsString = reader.GetValue(5).ToString();
                                }
                            }
                            if (reader.GetValue(6) != null)
                            {
                                if (reader.GetValue(6).GetType().Name != "String")
                                {
                                    item.OTHrs = Convert.ToDecimal(reader.GetDouble(6));
                                }
                                else
                                {
                                    item.OTHrsString = reader.GetValue(6).ToString();
                                }
                            }
                            List.Add(item);

                        }
                        //if (List[0].EmployeeID != "Employee ID" || List[0].FullName != "Employee Name" || List[0].LopString != "LOP")
                        //{
                        //    reader.Close();
                        //    if (System.IO.File.Exists(path + "\\" + fileName))
                        //    {
                        //        System.IO.File.Delete(path + "\\" + fileName);
                        //    }
                        //    return Json(new ResponseEntity("Excel File not in the prescribed format", false));
                        //}
                        model.MonthlyAttendanceUploading_Data = List;

                        model.MonthlyAttendanceUploading_Data.RemoveAt(0);
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
            var response = await _service.ImportFromExcel(model.MonthCode,model.PayrollGroup, model);
            // Check if file exists with its full path    
            if (System.IO.File.Exists(path + "\\" + fileName))
            {
                System.IO.File.Delete(path + "\\" + fileName);
            }
            if (response.Message != "UPDATED")
            {
                model.MonthlyAttendanceUploading_Data = response.Data.MonthlyAttendanceUploading_Data;
                //model.Message = response.Message;
                return PartialView("_UnmatchedList", model);
            }
            else
            {
                response.Message = "Uploaded Successfully";
                return Json(response);
            }
        }


        [HttpPost]
        public async Task<IActionResult> ImportFromExcel(MonthlyAttendanceUploading model)
        {
            string path = Path.Combine(this._environment.WebRootPath, "Uploads");
            string fileName = model.Excel.FileData.FileName;
            try
            {
                model.MonthlyAttendanceUploading_Data = new List<MonthlyAttendanceUploading_Data>();
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

             return  await populateModel(fileName,model);
               
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

        [HttpPost]
        public async Task<IActionResult> Update(Guid EmployeeGI, int Month, decimal LOP ,decimal HP, decimal OP, decimal LATE, decimal OT)
        {
            try
            {
                MonthlyAttendanceUploading_Data model = new MonthlyAttendanceUploading_Data 
            { EmployeeGI = EmployeeGI,Lop= LOP, LateHrs =LATE, HolidayPresent=HP, OffdayPresent =OP, OTHrs=OT};


            var response = await _service.Update(Month, model);
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

        public async Task<IActionResult> ExportToExcel(int month,string monthName,int Pg, string PgName,Filter Filter,int TotalRow)
        {
            try
            {
                Filter.length = TotalRow;
                string FilterValues = await _helpers.GetFilterValues(Filter);

                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Monthly Attendance Uploading", "Excel");

                if (FilterValues == "")
                {
                    FilterValues = "All";
                }

                string currDateTime = await _helpers.GetCurrentDate();
                

                //Get Datset
                var dataset = await _service.GetEmployeeList_Excel(month, Pg, Filter);
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
                    var worksheet = workbook.Worksheets.Add("Monthly Attendance Uploading");
                    int count = dt.Rows.Count + 5;


                    worksheet.Range("A1:Y" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:Y" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:Y" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:Y" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:Y" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 24)));
                    wsReportNameHeaderRange.Style.Font.Bold = true;
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Merge();
                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191); ;
                    wsReportNameHeaderRange.Value = "Monthly Attendance Uploading";

                    worksheet.Cell(2, 1).Style.Font.Bold = true;
                    worksheet.Cell(2, 1).Value = "Company Name";
                    worksheet.Cell(2, 2).Value = CompanyName.ToUpper();


                    worksheet.Cell(2, 3).Style.Font.Bold = true;
                    worksheet.Cell(2, 3).Value = "Month ";
                    worksheet.Cell(2, 4).Value = monthName;


                    worksheet.Cell(2, 5).Style.Font.Bold = true;
                    worksheet.Cell(2, 5).Value = "Payroll Group ";
                    worksheet.Cell(2, 6).Value = PgName;

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
                    worksheet.Range("B4:Y4").Merge();
                    worksheet.Cell("B4").Value = FilterValues;



                    var currentRow = 5;

                    worksheet.Cell(currentRow, 1).InsertTable(dt);
                    worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                    worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                    worksheet.Range("A5:Y5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Range("A5:Y5").Style.Font.Bold = true;


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
                            "Monthly Attendance Uploading List.xlsx"
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
