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
using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll;
using Hrms.Lite.Shared.PayRoll.Generic.Master;
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
    public class PTConfirmationController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly IPTConfirmationService _PTConfirmationService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        private readonly IWebHostEnvironment _environment;

       
        public PTConfirmationController(IDropdownService dropdownService, IPTConfirmationService PTConfirmationService, ICommonService commonService, IHelpers Helpers, IWebHostEnvironment env)
        {
            _PTConfirmationService = PTConfirmationService;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = Helpers;
            _environment = env;
        }
        public static class Global
        {
            public static int Month;
            public static int Deduction;
            public static Filter GlobalFilter;

        }
        public async Task<IActionResult> Index()
        {
            try
            {
                ViewBag.Month = new SelectList(await _dropdownService.GetSalaryProcessing_MonthDropdown(0), "Code", "Name");
                ViewBag.Deduction = new SelectList(await _dropdownService.GetPTConfirmationDeductionDropdown(), "Code", "Name");
                PTConfirmation model = new PTConfirmation();
                Filter filter = new Filter();
                filter.length = 10;
                model = await _PTConfirmationService.PTConfirmationList(0, 0, filter);
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


        [HttpPost]
        public async Task<IActionResult> GetPTConfirmationList(Filter filter1, int PTMonthCode, int DeductionCode)
        {
            try
            {
                PTConfirmation model = new PTConfirmation();
                PTConfirmation m = new PTConfirmation();
                Global.Month = PTMonthCode;
                Global.Deduction = DeductionCode;


                Filter filter = new Filter();
                Global.GlobalFilter = filter1;


                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }

                if (PTMonthCode != 0 && DeductionCode != 0)
                {

                    model = await _PTConfirmationService.PTConfirmationList(PTMonthCode, DeductionCode, filter1);
                }
                if (model != null && model.PTConfirmationList != null)
                {
                    m.PTConfirmationList = model.PTConfirmationList;
                }
                else
                {
                    m.PTConfirmationList = new List<PTConfirmation>();
                }
                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model==null || model.PTConfirmationList == null || model.PTConfirmationList.Count() == 0)
                {

                    return Json(new
                    {
                        data = "",
                        draw = filter1.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows
                    });
                }




                if (model.PTConfirmationList.Count > 0)
                {
                    totalrows = m.PTConfirmationList[0].active;

                }


                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = m.PTConfirmationList[0].totalRecords;
                    return Json(new
                    {
                        data = model.PTConfirmationList,
                        draw = filter1.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows//totalrowsafterfiltering
                    });
                }
                else
                {
                    return Json(new
                    {
                        data = model.PTConfirmationList,
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




        public async Task<IActionResult> ImportFromExcel(PTConfirmation model)
        {
            string path = Path.Combine(this._environment.WebRootPath, "Uploads");
            string fileName = model.Excel.FileData.FileName;
            try
            {
                model.PTConfirmationList = new List<PTConfirmation>();
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
                            List<PTConfirmation> List = new List<PTConfirmation>();
                            var readerrr = ExcelReaderFactory.CreateReader(stream);
                            while (reader.Read()) //Each row of the file
                            {
                                var item = new PTConfirmation();
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

                                    if (item.Amount.ToString().Contains("."))
                                    {
                                        return Json(new ResponseEntity("Decimal values will not be supported.Please round off the decimal values and try again", false));
                                    }
                                   
                                }
                                        List.Add(item);
                            }
                            if (List[0].EmployeeID != "Employee ID" || List[0].EmployeeName != "Employee Name" || List[0].AmountStr != "Amount")
                            {
                                reader.Close();
                                // Check if file exists with its full path    
                                if (System.IO.File.Exists(path + "\\" + fileName))
                                {
                                    System.IO.File.Delete(path + "\\" + fileName);
                                }
                                return Json(new ResponseEntity("Excel File not in the prescribed format", false));
                            }
                            model.PTConfirmationList = List;

                            model.PTConfirmationList.RemoveAt(0);

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
                var response = await _PTConfirmationService.ImportFromExcel(model);
                if (System.IO.File.Exists(path + "\\" + fileName))
                {
                    System.IO.File.Delete(path + "\\" + fileName);
                }

                if (response.Success != true)
                {
                    model.PTConfirmationList = response.Data;
                    model.Message = response.Message;
                    return PartialView("_UnMatchedList", model);
                }
                else
                {
                    return Json(response);
                }
                // Check if file exists with its full path    

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




        public async Task<IActionResult> GetList(int MonthCode, int DeductionCode)
        {
            try {
                if (MonthCode != 0 && DeductionCode != 0)
                {
                    Global.Month = MonthCode;
                    Global.Deduction = DeductionCode;
                    Filter filter = new Filter();
                    PTConfirmation model = new PTConfirmation();
                    model = await _PTConfirmationService.PTConfirmationList(MonthCode, DeductionCode, filter);
                    return PartialView("_PTConfirmationList", model);
                }
                else
                {
                    PTConfirmation model = new PTConfirmation();
                    model.PTConfirmationList = new List<PTConfirmation>();
                    return PartialView("_PTConfirmationList", model);
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
        public async Task<IActionResult> ApplyFilter(Filter filter)
        {
            try { 
            PTConfirmation model = new PTConfirmation();
            Global.GlobalFilter = filter;
            model = await _PTConfirmationService.PTConfirmationList(Global.Month, Global.Deduction, filter);
            return PartialView("_PTConfirmationList", model);
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
        public async Task<IActionResult> EmployeeWisePTConfirmationSave(int UpdatedAmount,Guid EmployeeGI)
        {
            try { 
            PTConfirmation model = new PTConfirmation();
            model.PTMonth = new PayrollMonth();
            model.Deduction = new Deduction();
            model.EmployeeGI = EmployeeGI;
            model.PTMonth.Code = Global.Month;
            model.Deduction.Code = Global.Deduction;
            model.Amount = UpdatedAmount;
            var response = await _PTConfirmationService.EmployeeWisePTConfirmationSave(model);
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

        [HttpPost]
        public async Task<IActionResult> PTConfirmationSave(PTConfirmation model)
        {
            try { 
            model.PTMonth = new PayrollMonth();
            model.Deduction = new Deduction();            
            model.PTMonth.Code = Global.Month;
            model.Deduction.Code = Global.Deduction;
            var response = await _PTConfirmationService.PTConfirmationSave(model);
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


        public async Task<IActionResult> ExportToExcel(int MonthCode, int DeductionCode, string MonthName, string DeductionName, int totRecords)
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
                EP = await _commonService.GetExcelPassword("PT Confirmation (" + DeductionName + ")", "Excel");

                if (FilterValues == "")
                {
                    FilterValues = "All";
                }

                string currDateTime = await _helpers.GetCurrentDate();
                

                //Get Datset
                var dataset = await _PTConfirmationService.PTConfirmationList_Excel(MonthCode,DeductionCode, Global.GlobalFilter);
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
                    var worksheet = workbook.Worksheets.Add("PT Confirmation");
                    int count = dt.Rows.Count + 5;


                    worksheet.Range("A1:G" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:G" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:G" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:G" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:G" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 6)));
                    wsReportNameHeaderRange.Style.Font.Bold = true;
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Merge();
                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191); ;
                    wsReportNameHeaderRange.Value = "PT Confirmation(" + DeductionName + ")";

                    worksheet.Cell(2, 1).Style.Font.Bold = true;
                    worksheet.Cell(2, 1).Value = "Group Company Name";
                    worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                    worksheet.Cell(2, 3).Value = "Month";
                    worksheet.Cell(2, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(2, 4).Style.DateFormat.Format = "MMM-yyyy";
                    worksheet.Cell(2, 4).Value = MonthName;
                    worksheet.Cell(2, 5).Style.Font.Bold = true;
                    worksheet.Cell(2, 5).Value = "Deduction";
                    worksheet.Cell(2, 6).Value = DeductionName;
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
                    worksheet.Range("B4:G4").Merge();
                    worksheet.Cell("B4").Value = FilterValues;



                    var currentRow = 5;

                    worksheet.Cell(currentRow, 1).InsertTable(dt);
                    worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                    worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                    worksheet.Range("A5:G5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Range("A5:G5").Style.Font.Bold = true;


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
                            "PT Confirmation(" + DeductionName + ") List.xlsx"
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









        //public async Task<IActionResult> ExportToExcel(int MonthCode, int DeductionCode,string MonthName,string DeductionName,int totRecords)
        //{
        //    try { 
        //    PTConfirmation model = new PTConfirmation();

        //        if (Global.GlobalFilter == null)
        //            Global.GlobalFilter = new Filter();
        //        else
        //        { 
        //            Global.GlobalFilter.length = totRecords;
        //            Global.GlobalFilter.start = 0;
        //        }
        //    string FilterValues = await _helpers.GetFilterValues(Global.GlobalFilter);
        //    model = await _PTConfirmationService.PTConfirmationList(MonthCode, DeductionCode, Global.GlobalFilter);
        //    var LoginUser = HttpContext.Session.GetString("LoginUser");
        //    var CompanyName = HttpContext.Session.GetString("CompanyName");
        //        ExcelPassword EP = new ExcelPassword();
        //        EP = await _commonService.GetExcelPassword("PT Confirmation (" + DeductionName + ")", "Excel");
        //        int slNo = 0;
        //    if (FilterValues == "")
        //    {
        //        FilterValues = "All";
        //    }
        //    string currDateTime = await _helpers.GetCurrentDate();
        //    
        //    using (var workbook = new XLWorkbook())
        //    {
        //        var worksheet = workbook.Worksheets.Add("PT Confirmation");
        //        int count = model.PTConfirmationList.Count + 5;


        //        worksheet.Range("A1:G" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
        //        worksheet.Range("A1:G" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
        //        worksheet.Range("A1:G" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
        //        worksheet.Range("A1:G" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
        //        worksheet.Range("A1:G" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
        //        var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 6)));
        //        wsReportNameHeaderRange.Style.Font.Bold = true;
        //        wsReportNameHeaderRange.Style.Font.FontSize = 15;
        //        wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        //        wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
        //        wsReportNameHeaderRange.Merge();
        //        wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191); ;
        //        wsReportNameHeaderRange.Value = "PT Confirmation";

        //        worksheet.Cell(2, 1).Style.Font.Bold = true;
        //        worksheet.Cell(2, 1).Value = "Company Name";
        //        worksheet.Cell(2, 2).Value = CompanyName.ToUpper();
        //        worksheet.Cell(2, 3).Style.Font.Bold = true;
        //        worksheet.Cell(2, 3).Value = "Month";
        //        worksheet.Cell(2, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //        worksheet.Cell(2, 4).Style.DateFormat.Format = "MMM-yyyy";
        //        worksheet.Cell(2, 4).Value = MonthName;                               
        //        worksheet.Cell(2, 5).Style.Font.Bold = true;
        //        worksheet.Cell(2, 5).Value = "Deduction";
        //        worksheet.Cell(2, 6).Value = DeductionName;

        //        worksheet.Cell(3, 1).Style.Font.Bold = true;
        //        worksheet.Cell(3, 1).Value = "Login User";
        //        worksheet.Cell(3, 2).Value = LoginUser;
        //        worksheet.Cell(3, 3).Style.Font.Bold = true;
        //        worksheet.Cell(3, 3).Value = "Date & Time";
        //        worksheet.Cell(3, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //        worksheet.Cell(3, 4).Style.DateFormat.Format = "dd-MMM-yyyy   h:mm AM/PM";
        //        worksheet.Cell(3, 4).Value = currDateTime.ToString();
        //        worksheet.Cell(3, 5).Style.Font.Bold = true;
        //        worksheet.Cell(3, 5).Value = "Total Count";
        //        worksheet.Cell(3, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //        worksheet.Cell(3, 6).Value = model.PTConfirmationList.Count;
        //        worksheet.Cell(4, 1).Style.Font.Bold = true;
        //        worksheet.Cell(4, 1).Value = "Filter Applied";
        //        worksheet.Range("B4:G4").Merge();
        //        worksheet.Cell("B4").Value = FilterValues;



        //        var currentRow = 5;
        //        #region Header
        //        worksheet.Range("A5:G5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
        //        worksheet.Cell(currentRow, 1).Value = "Sl.No";
        //        worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
        //            worksheet.Cell(currentRow, 2).Value = "ID";
        //        worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
        //            worksheet.Cell(currentRow, 3).Value = "Name";


        //            worksheet.Cell(currentRow, 3).Style.Font.Bold = true;



        //            worksheet.Cell(currentRow, 4).Value = "Designation";
        //        worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
        //        worksheet.Cell(currentRow, 5).Value = "Department";
        //        worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
        //        worksheet.Cell(currentRow, 6).Value = "Location";
        //        worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
        //        worksheet.Cell(currentRow, 7).Value = "Amount";
        //        worksheet.Cell(currentRow, 7).Style.Font.Bold = true;



        //        #endregion

        //        #region Body
        //        foreach (var item in model.PTConfirmationList)
        //        {
        //            currentRow++;
        //            slNo = slNo + 1;
        //            worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //            worksheet.Cell(currentRow, 1).Value = slNo;
        //            worksheet.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //            worksheet.Cell(currentRow, 2).Value = item.EmployeeID;
        //            worksheet.Cell(currentRow, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //            worksheet.Cell(currentRow, 3).Value = item.EmployeeName;

        //            worksheet.Cell(currentRow, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //            worksheet.Cell(currentRow, 4).Value = item.Designation.Name;
        //            worksheet.Cell(currentRow, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //            worksheet.Cell(currentRow, 5).Value = item.Department.Name;
        //            worksheet.Cell(currentRow, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //            worksheet.Cell(currentRow, 6).Value = item.Location.Name;
        //            worksheet.Cell(currentRow, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //            worksheet.Cell(currentRow, 7).Value = item.Amount;




        //            //worksheet.Columns(1, 9).Style.Alignment.WrapText = true;
        //            worksheet.Rows().Style.Alignment.WrapText = true;
        //        }
        //        #endregion
        //        worksheet.Columns().AdjustToContents();
        //        worksheet.Rows().AdjustToContents();
        //        using (var stream = new MemoryStream())
        //        {
        //            workbook.SaveAs(stream);
        //                if (EP.NeedPassword)
        //                {
        //                    Workbook book = new Workbook();
        //                    book.LoadFromStream(stream);
        //                    //Protect Workbook
        //                    book.Protect(EP.Password,false,false);
        //                    book.SaveToStream(stream);
        //                }
        //                var content = stream.ToArray();
        //            return File(
        //                content,
        //                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
        //                "PT Confirmation List.xlsx"
        //                );
        //        }
        //    }
        //}
        //    catch (UnauthorizedAccessException e)
        //    {

        //        return RedirectToAction("Login", "Account", new { Area = "" });
        //    }
        //    catch (Exception e)
        //    {
        //        return RedirectToAction("Error", "Account", new { Area = "" });
        //    }

        //}
    }
}
