using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.PayRoll;
using Hrms.Lite.Shared.PayRoll;
using Hrms.Lite.Shared.PayRoll.Generic.Master;
using Hrms.Lite.Shared.TimeOffice;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hrms.Lite.Shared.DataBank.Generic.Master;
using Microsoft.AspNetCore.Http;
using ClosedXML.Excel;
using System.IO;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Infrastructure.Helpers;
using Spire.Xls;
using System.Data;
using ExcelDataReader;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using Hrms.Lite.Shared.Account;

namespace Hrms.Lite.UI.Areas.PayRoll.Controllers
{
    [Area("PayRoll")]
    [SessionAuthorize]
    public class VariableDeductionController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly IVariableDeductionService _VariableDeductionService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        private readonly IWebHostEnvironment _environment;

        public static class Global
        {
            public static int Month;
            public static int Deduction;
            public static Filter GlobalFilter;

        }
        public VariableDeductionController(IDropdownService dropdownService, IVariableDeductionService VariableDeductionService, ICommonService commonService,IHelpers helpers, IWebHostEnvironment env)
        {
            _VariableDeductionService = VariableDeductionService;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = helpers;
            _environment = env;
        }
        public async Task<IActionResult> Index()
        {
            try { 
            VariableDeduction model = new VariableDeduction(); 
                Filter filter = new Filter();
                filter.length = 10;
                model = await _VariableDeductionService.VariableDeductionList(0, 0, filter);
                ViewBag.DeductionMonth = new SelectList(await _dropdownService.GetSalaryProcessing_MonthDropdown(0), "Code", "Name");
            ViewBag.Deduction = new SelectList(await _dropdownService.GetVariableDeduction_DeductionDropdown(), "Code", "Name");
            await fillfilterDropdown();
            Global.GlobalFilter = new Filter();
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

        public async Task<IActionResult> ApplyFilter(Filter filter)
        {
            try { 
            VariableDeduction model = new VariableDeduction();
            Global.GlobalFilter = filter;
            model = await _VariableDeductionService.VariableDeductionList(Global.Month, Global.Deduction, filter);
            return PartialView("_VariableDeductionList", model);
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
        public async Task<IActionResult> GetDeductionList(Filter filter1, int Month, int Deduction)
        {
            try
            {
                VariableDeduction model = new VariableDeduction();
                VariableDeduction m = new VariableDeduction();

                Filter filter = new Filter();
                Global.GlobalFilter = filter1;

                //m.GridSettings = new List<LeaveRegisterReport_GridSettings>();
                //m = await _leaveRegisterService.GetGridSettings();
                //model.GridSettings.Add(new LeaveRegisterReport_GridSettings());
                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }
                //model.filter = filter1;
                //model.GridSettings = m.GridSettings;
                //model = await _TDSConfirmationService.GetPFEmployeeWiseSettingsList(Deduction, Month, filter1);
                if (Month != 0 && Deduction != 0)
                {
                    model = await _VariableDeductionService.VariableDeductionList(Month, Deduction, filter1);
                }
                m.VariableDeductionList = model.VariableDeductionList;
                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.VariableDeductionList == null)
                {
                    //model.RequestVsAuthorityList = new List<RequestVsAuthorityList>();
                    //model.RequestVsAuthorityList.Add(new RequestVsAuthorityList());


                    return Json(new
                    {
                        data = "",
                        draw = filter1.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows
                    });
                }

                if (model.VariableDeductionList.Count > 0)
                {
                    totalrows = m.VariableDeductionList[0].activeRow;

                    //m.OnBoardingList.ToList<Hrms.Lite.Shared.Master.Test>().Count; ;
                }


                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = m.VariableDeductionList[0].totalRecords;
                    //return Json(new { data = model.EmployeeProfileList, draw = filter1.draw, recordsTotal = totalrows, recordsFiltered = totalrowsafterfiltering });
                    return Json(new
                    {
                        data = model.VariableDeductionList,
                        draw = filter1.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows//totalrowsafterfiltering
                    });
                }
                else
                {

                    return Json(new
                    {
                        data = model.VariableDeductionList,
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


        public async Task<IActionResult> GetList(int Month, int Deduction)
        {
            try { 
            VariableDeduction model = new VariableDeduction();
            Global.Month = Month;
            Global.Deduction = Deduction;
            Filter filter = new Filter();
            model = await _VariableDeductionService.VariableDeductionList(Month, Deduction, filter);
            if(Month==0 && Deduction==0)
            {
                model.VariableDeductionList = new List<VariableDeduction>();
            }
            return PartialView("_VariableDeductionList", model);
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

        public IActionResult GetBatchUpdateDetails(int MonthCode, string Month,int DedCode, string Deduction)
        {
            try { 
            VariableDeduction model = new VariableDeduction();
            model.DeductionMonth = new PayrollMonth();
            model.Deduction = new Deduction();
            model.DeductionMonth.Code = MonthCode;
            model.DeductionMonth.Name = Month;
            model.Deduction.Code = DedCode;
            model.Deduction.Name = Deduction;
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
        public async Task<IActionResult> BatchUpdate(VariableDeduction model)
        {
            try { 
            Guid EmployeeGI = model.EmployeeGI;
            model.Filter = Global.GlobalFilter;
            var response = await _VariableDeductionService.VariableDeductionBatchUpdate(model);
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
        public async Task<ActionResult> EmployeeWiseSave(decimal UpdatedAmount, int DeductionMonth, int DeductionType, Guid EmployeeGI,string Remark)
        {
            try
            {
                VariableDeduction model = new VariableDeduction();
                model.DeductionMonth = new PayrollMonth();
                model.EmployeeGI = EmployeeGI;
                model.Deduction = new Deduction();
                model.Amount = UpdatedAmount;
                model.DeductionMonth.Code = DeductionMonth;
                model.Deduction.Code = DeductionType;
                model.Remark = Remark;
                var response = await _VariableDeductionService.EmployeeWiseVariableDeductionSave(model);
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

        public async Task<ActionResult> CountCheckAJAX(int Month, int Deduction,int totalRecord)
        {
            try
            {
                DataSet dataset = new DataSet();
                object ResponseEntity = new object();

                if (Global.GlobalFilter == null)
                    Global.GlobalFilter = new Filter();
                else
                {
                    Global.GlobalFilter.length = totalRecord;
                    Global.GlobalFilter.start = 0;
                }
                dataset = await _VariableDeductionService.VariableDeductionList_Excel(Month, Deduction, Global.GlobalFilter);
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
        public async Task<IActionResult> ExportToExcel(int Month, int Deduction, string MName, string DName, int totalRecord)
        {
            try
            {

                if (Global.GlobalFilter == null)
                    Global.GlobalFilter = new Filter();
                else
                {
                    Global.GlobalFilter.length = totalRecord;
                    Global.GlobalFilter.start = 0;
                }
                string FilterValues = await _helpers.GetFilterValues(Global.GlobalFilter);

                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Variable Deduction List (" + DName + ")", "Excel");

                if (FilterValues == "")
                {
                    FilterValues = "All";
                }

                string currDateTime = await _helpers.GetCurrentDate();
                

                //Get Datset
                var dataset = await _VariableDeductionService.VariableDeductionList_Excel(Month, Deduction,Global.GlobalFilter);
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
                    var worksheet = workbook.Worksheets.Add("Variable Deduction List");
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
                    wsReportNameHeaderRange.Value = "Variable Deduction List (" + DName + ")";

                    worksheet.Cell(2, 1).Style.Font.Bold = true;
                    worksheet.Cell(2, 1).Value = "Group Company Name";
                    worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                    worksheet.Cell(2, 3).Style.Font.Bold = true;
                    worksheet.Cell(2, 3).Value = "Month";
                    worksheet.Cell(2, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(2, 4).Style.DateFormat.Format = "MMM-yyyy";
                    worksheet.Cell(2, 4).Value = MName;
                    worksheet.Cell(2, 5).Style.Font.Bold = true;
                    worksheet.Cell(2, 5).Value = "Deduction";
                    worksheet.Cell(2, 6).Value = DName;


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
                            book.Protect(EP.Password,false,false);
                            book.SaveToStream(stream);
                        }
                        var content = stream.ToArray();
                        return File(
                            content,
                            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                            "Variable Deduction List (" + DName + ")List.xlsx"
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


        public async Task<IActionResult> ImportFromExcel(string Deduction,bool Append, VariableDeduction model)
        {
            string path = Path.Combine(this._environment.WebRootPath, "Uploads");
            string fileName = model.Excel.FileData.FileName;
            string[] ext = model.Excel.FileData.FileName.Split('.');
            string dedFilename = ext[0].ToString();
            string DeductionName = Deduction;
            try
            {
                if (dedFilename != DeductionName)
                {
                    return Json(new ResponseEntity("The file name should match the selected deduction.", false));
                }
 
                model.VariableDeductionList = new List<VariableDeduction>();
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
                            List<VariableDeduction> List = new List<VariableDeduction>();
                            var readerrr = ExcelReaderFactory.CreateReader(stream);
                            while (reader.Read()) //Each row of the file
                            {
                                var item = new VariableDeduction();
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
                                //if (item.Amount.ToString().Contains("."))
                                //{
                                //    return Json(new ResponseEntity("Decimal values will not be supported.Please round off the decimal values and try again", false));
                                //}
                                //else
                                //{
                                    List.Add(item);
                                //}
                            }
                                if (List[0].EmployeeID != "Employee ID" || List[0].EmployeeName != "Employee Name" || List[0].AmountStr != "Amount" || List[0].Remark != "Remark")
                                {
                                    // Check if file exists with its full path    
                                    if (System.IO.File.Exists(path + "\\" + fileName))
                                    {
                                        System.IO.File.Delete(path + "\\" + fileName);
                                    }
                                    return Json(new ResponseEntity("Excel File not in the prescribed format", false));
                                }
                                model.VariableDeductionList = List;

                                model.VariableDeductionList.RemoveAt(0);
                            
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
                var response = await _VariableDeductionService.ImportFromExcel(model);
                if (System.IO.File.Exists(path + "\\" + fileName))
                {
                    System.IO.File.Delete(path + "\\" + fileName);
                }

                if (response.Success != true)
                {
                    model.VariableDeductionList = response.Data;
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




        //public async Task<IActionResult> ExportToExcel(int Month, int Deduction, string MName, string DName,int totalRecords)
        //{
        //    try { 
        //    VariableDeduction model = new VariableDeduction();
        //    if (Global.GlobalFilter == null)
        //        Global.GlobalFilter = new Filter();
        //        else
        //        {
        //            Global.GlobalFilter.length = totalRecords;
        //            Global.GlobalFilter.start = 0;
        //        }

        //        string FilterValues = await _helpers.GetFilterValues(Global.GlobalFilter);
        //    model = await _VariableDeductionService.VariableDeductionList(Month, Deduction, Global.GlobalFilter);
        //    var LoginUser = HttpContext.Session.GetString("LoginUser");
        //    var CompanyName = HttpContext.Session.GetString("CompanyName");
        //        ExcelPassword EP = new ExcelPassword();
        //        EP = await _commonService.GetExcelPassword("Variable Deduction List (" + DName + ")", "Excel");
        //        int slNo = 0;
        //    if (FilterValues == "")
        //    {
        //        FilterValues = "All";
        //    }
        //    string currDateTime = await _helpers.GetCurrentDate();   
        //    

        //    using (var workbook = new XLWorkbook())
        //    {
        //        var worksheet = workbook.Worksheets.Add("Variable Deduction List");
        //        int count = model.VariableDeductionList.Count + 5;


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
        //        wsReportNameHeaderRange.Value = "Variable Deduction";

        //        worksheet.Cell(2, 1).Style.Font.Bold = true;
        //        worksheet.Cell(2, 1).Value = "Company Name";
        //        worksheet.Cell(2, 2).Value = CompanyName.ToUpper();
        //        worksheet.Cell(2, 3).Style.Font.Bold = true;
        //        worksheet.Cell(2, 3).Value = "Month";
        //        worksheet.Cell(2, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //        worksheet.Cell(2, 4).Style.DateFormat.Format = "MMM-yyyy";
        //        worksheet.Cell(2, 4).Value = MName;
        //        worksheet.Cell(2, 5).Style.Font.Bold = true;
        //        worksheet.Cell(2, 5).Value = "Deduction";
        //        worksheet.Cell(2, 6).Value = DName;
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
        //        worksheet.Cell(3, 6).Value = model.VariableDeductionList.Count;
        //        worksheet.Cell(4, 1).Style.Font.Bold = true;
        //        worksheet.Cell(4, 1).Value = "Filter Applied";
        //        worksheet.Range("B4:G4").Merge();
        //        worksheet.Cell("B4").Value = FilterValues;



        //        var currentRow = 5;
        //        #region Header
        //        worksheet.Range("A5:G5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
        //        worksheet.Cell(currentRow, 1).Value = "Sl.No";
        //        worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
        //        worksheet.Cell(currentRow, 2).Value = "ID";
        //        worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
        //        worksheet.Cell(currentRow, 3).Value = "EmployeeName";
        //        worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
        //        worksheet.Cell(currentRow, 4).Value = "Designation";
        //        worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
        //        worksheet.Cell(currentRow, 5).Value = "Department";
        //        worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
        //        worksheet.Cell(currentRow, 6).Value = "Location";
        //        worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
        //        worksheet.Cell(currentRow, 7).Value = "Amount";
        //        worksheet.Cell(currentRow, 7).Style.Font.Bold = true;



        //        #endregion

        //        #region Body
        //        foreach (var item in model.VariableDeductionList)
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
        //            worksheet.Cell(currentRow, 7).Value = item.Amount;

        //            worksheet.Columns(1, 8).Style.Alignment.WrapText = true;
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
        //                "Variable Deduction List.xlsx"
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
