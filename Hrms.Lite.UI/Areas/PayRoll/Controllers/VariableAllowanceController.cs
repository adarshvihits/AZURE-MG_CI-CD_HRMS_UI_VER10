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
    public class VariableAllowanceController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly IVariableAllowanceService _VariableAllowanceService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        private readonly IWebHostEnvironment _environment;
        public static class Global
        {
            public static int Month;
            public static int Allowance;            
            public static Filter GlobalFilter;

        }
        public VariableAllowanceController(IDropdownService dropdownService, IVariableAllowanceService VariableAllowanceService, ICommonService commonService,IHelpers helpers,IWebHostEnvironment env)
        {
            _VariableAllowanceService = VariableAllowanceService;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = helpers;
            _environment = env;

        }
        public async  Task<IActionResult> Index()
        {
            try { 
            VariableAllowance model = new VariableAllowance();
            ViewBag.AllowanceMonth = new SelectList(await _dropdownService.GetSalaryProcessing_MonthDropdown(0), "Code", "Name");
            ViewBag.AllowanceType = new SelectList(await _dropdownService.GetVariableAllowance_AllowanceDropdown(), "Code", "Name");
                Filter filter = new Filter();
                filter.length = 10;
                model = await _VariableAllowanceService.GetVariableAllowanceList(0, 0, filter);
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
            VariableAllowance model = new VariableAllowance();
            Global.GlobalFilter = filter;
            model = await _VariableAllowanceService.GetVariableAllowanceList(Global.Month, Global.Allowance, filter);
            return PartialView("_VariableAllowanceList", model);
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
        public async Task<IActionResult> GetVariableAllowanceList(Filter filter1, int MonthCode, int AllowanceCode)
        {
            try
            {
                VariableAllowance model = new VariableAllowance();
                VariableAllowance m = new VariableAllowance();
                //model.EmployeeHeader = new EmployeeHeader();




                Filter filter = new Filter();
                Global.GlobalFilter = filter1;


                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }


                if (MonthCode != 0 && AllowanceCode != 0)
                {
                    model = await _VariableAllowanceService.GetVariableAllowanceList(MonthCode, AllowanceCode, filter1);
                }
                if (model == null || model.VariableAllowanceList == null || model.VariableAllowanceList.Count() == 0)
                {
                    m.VariableAllowanceList = new List<VariableAllowance>();
                    model = new VariableAllowance();
                    model.VariableAllowanceList = new List<VariableAllowance>();
                }
                else
                { 
                    m.VariableAllowanceList = model.VariableAllowanceList;
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
                    totalrows = m.VariableAllowanceList[0].active;

                }


                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = m.VariableAllowanceList[0].totalRecords;
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







        public async Task<IActionResult> GetList(int Month, int Allowance)
        {
            try { 
            VariableAllowance model = new VariableAllowance();
            Global.Month = Month;
            Global.Allowance = Allowance;
            Filter filter = new Filter();
            model = await _VariableAllowanceService.GetVariableAllowanceList(Month, Allowance, filter);  
            if(Month==0&& Allowance==0)
            {
                model.VariableAllowanceList = new List<VariableAllowance>();
            }
            return PartialView("_VariableAllowanceList", model);
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

            try { 
            Guid EmployeeGI = model.EmployeeGI;
            model.Filter = Global.GlobalFilter;
            var response = await _VariableAllowanceService.VariableAllowanceBatchUpdate(model);
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
        public ActionResult GetBatchUpdateSlider(int MonthCode, int AllowanceCode,string MonthName, string AllowanceName )
        {
            try { 
            VariableAllowance model = new VariableAllowance();
            model.AllowanceMonth = new PayrollMonth();
            model.Allowance = new Allowance();
            model.AllowanceMonth.Name = MonthName;
            model.Allowance.Code = AllowanceCode ;
            model.Allowance.Name = AllowanceName ;
            model.AllowanceMonth.Code = MonthCode;
            return PartialView("_BatchUpdate",model);
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
        public async Task<ActionResult> EmployeeWiseSave(decimal UpdatedAmount, int allowanceMonth, int allowanceType,Guid EmployeeGI,string Remarks)
        {
            try
            {
                VariableAllowance model = new VariableAllowance();
                model.AllowanceMonth = new PayrollMonth();
                model.EmployeeGI = EmployeeGI;
                model.Allowance = new Allowance();
                model.Amount = UpdatedAmount;
                model.AllowanceMonth.Code = allowanceMonth;
                model.Allowance.Code = allowanceType;
                model.Remark = Remarks;
                var response = await _VariableAllowanceService.EmployeeWiseVariableAllowanceSave(model);
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


        public async Task<ActionResult> CountCheckAJAX(int Month, int Allowance,int totRecords)
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
                dataset = await _VariableAllowanceService.VariableAllowanceList_Excel(Month, Allowance, Global.GlobalFilter);
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
        public async Task<IActionResult> ExportToExcel(Filter filter, int Month, int Allowance, string AllowanceMonth, string AllowanceType, int totRecords)
        
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
                EP = await _commonService.GetExcelPassword("Variable Allowance List (" + AllowanceType + ")", "Excel");

                if (FilterValues == "")
                {
                    FilterValues = "All";
                }


                string currDateTime = await _helpers.GetCurrentDate();
                

                //Get Datset
                var dataset = await _VariableAllowanceService.VariableAllowanceList_Excel(Month, Allowance, filter);
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


                    var worksheet = workbook.Worksheets.Add("Variable Allowance");
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
                   wsReportNameHeaderRange.Value = "Variable Allowance List (" + AllowanceType + ")";


                    worksheet.Cell(2, 1).Style.Font.Bold = true;
                    worksheet.Cell(2, 1).Value = "Group Company Name";
                    worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();



                    worksheet.Cell(2, 3).Style.Font.Bold = true;
                    worksheet.Cell(2, 3).Value = "Month";
                    worksheet.Cell(2, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(2, 4).Value = AllowanceMonth;


                  
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
                              "Variable Allowance List (" + AllowanceType + ").xlsx"
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


        public async Task<IActionResult> ImportFromExcel(string Allowance,bool Append, VariableAllowance model)
        {
            string path = Path.Combine(this._environment.WebRootPath, "Uploads");
            string fileName = model.Excel.FileData.FileName;
            string[] ext = model.Excel.FileData.FileName.Split('.');
            string AlwFilename = ext[0].ToString();
            string AllowanceName = Allowance;
            //string AllowanceName = Allowance + ".xlsx";
            try
            {

                if(AlwFilename != AllowanceName)
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
                                //if (item.Amount.ToString().Contains("."))
                                //{
                                //    return Json(new ResponseEntity("Decimal values will not be supported.Please round off the decimal values and try again", false));
                                //}
                                //else
                                //{
                                    List.Add(item);
                                //}
                            }
                            if (List[0].EmployeeID != "Employee ID" || List[0].EmployeeName != "Employee Name" || List[0].AmountStr!="Amount" || List[0].Remark != "Remark")
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
                model.Append =Append;
                var response = await _VariableAllowanceService.ImportFromExcel(model);
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
