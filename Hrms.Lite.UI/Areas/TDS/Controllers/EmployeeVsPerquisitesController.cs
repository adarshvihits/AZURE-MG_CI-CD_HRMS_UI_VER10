using ClosedXML.Excel;
using ExcelDataReader;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.TDS;
using Hrms.Lite.Shared.Account;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TDS;
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

namespace Hrms.Lite.UI.Areas.TDS.Controllers
{
    [Area("TDS")]
    [SessionAuthorize]
    public class EmployeeVsPerquisitesController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly IEmployeeVsPerquisitesService _EmployeeVsPerquisitesService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        private readonly IWebHostEnvironment _environment;

        public EmployeeVsPerquisitesController(IEmployeeVsPerquisitesService EmployeeVsPerquisitesService, IDropdownService dropdownService, ICommonService commonService,
            IHelpers helpers, IWebHostEnvironment env)
        {
            _EmployeeVsPerquisitesService = EmployeeVsPerquisitesService;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = helpers;
            _environment = env;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                EmployeeVsPerquisites model = new EmployeeVsPerquisites();            
                model.PayrollMonth = new Dropdown();
                model.AssessmentYear = new Dropdown();
                model.Perquisite = new Dropdown();
                ViewBag.AssessmentYear = new SelectList(await _dropdownService.GetAssessmentYearDropdown(), "Code", "Name");
                ViewBag.month = new SelectList(await _dropdownService.GetTDSConfirmationMonthDropdown(0), "Code", "Name");
                ViewBag.Perquisite = new SelectList(await _dropdownService.GetPerquisiteDropdown(), "Code", "Name");
                
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
        public async Task<JsonResult> GetMonthDropDown(int AssessmentYearCode)
        {
            SelectList data = new SelectList("", "Code", "Name");
            data = new SelectList(await _dropdownService.GetTDSConfirmationMonthDropdown(AssessmentYearCode), "Code", "Name");
            return Json(data);
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
        public async Task<IActionResult> GetEmpVsPerquisitesList(Filter filter1, int AssessmentYear,int Month,int PerquisiteNature)
        {
            try
            {
                EmployeeVsPerquisites model = new EmployeeVsPerquisites(); 
                Filter filter = new Filter();
               
                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }
                if (AssessmentYear != 0)
                {

                    model.EmployeeVsPerquisitesList = await _EmployeeVsPerquisitesService.GetEmpVsPerquisitesList(AssessmentYear,Month, PerquisiteNature, filter1);
                }
               
                int totalrows = 0;
                int totalrowsafterfiltering = 0;



                if (model == null || model.EmployeeVsPerquisitesList == null || model.EmployeeVsPerquisitesList.Count() == 0)
                {
                    
                    model.EmployeeVsPerquisitesList = new List<EmployeeVsPerqList>();
                }
               



                if (model.EmployeeVsPerquisitesList == null || model.EmployeeVsPerquisitesList.Count() == 0)
                {

                    return Json(new
                    {
                        data = "",
                        draw = filter1.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows
                    });
                }

                if (model.EmployeeVsPerquisitesList.Count > 0)
                {
                    totalrows = model.EmployeeVsPerquisitesList[0].active;

                }


                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = model.EmployeeVsPerquisitesList[0].totalRecords;
                    return Json(new
                    {
                        data = model.EmployeeVsPerquisitesList,
                        draw = filter1.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows//totalrowsafterfiltering
                    });
                }
                else
                {
                    return Json(new
                    {
                        data = model.EmployeeVsPerquisitesList,
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
        public async Task<ActionResult> SaveEmployeeVsPerquisites(decimal PerqAmount,decimal tdsAmount, Guid EmployeeGI, int AssessmentYear,int Month,int PerquisiteNature)
        {
            try
            {
                EmployeeVsPerqList model = new EmployeeVsPerqList();
                model.PerqAmount = PerqAmount;
                model.TDSAmount = tdsAmount;
                model.Employee = new EmployeeHeader();
                model.Employee.EmployeeGuid = EmployeeGI;



                var response = await _EmployeeVsPerquisitesService.SaveEmployeeVsPerquisites(model, AssessmentYear, Month, PerquisiteNature);
                if (response.Success == true)
                    response.Message = "Updated Successfully";
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
    
        public async Task<IActionResult> ExportToExcel(Filter filter,int AssessmentYear, string AssessmentYearName,int Month ,string MName,int Perquisite, string PerquisiteName, int Totalrow)
        {
            try
            {
                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;


                filter.length = Totalrow;
               
                string FilterValues = await _helpers.GetFilterValues(filter);

                var dataset = await _EmployeeVsPerquisitesService.GetEmpVsPerquisitesList_Export(AssessmentYear, Month, Perquisite, filter);
                DataTable dt = dataset.Tables[0];
                for (int i = 0; i < dt.Columns.Count; i++)
                {

                    dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
                }

                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Employee Vs Perquisite (" + AssessmentYearName + ")", "Excel");
              
                if (FilterValues == "")
                {
                    FilterValues = "All";
                }
                string currDateTime = await _helpers.GetCurrentDate();
                
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Employee Vs Perquisite List");
                    int count = dt.Rows.Count + 5;


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
                    wsReportNameHeaderRange.Value = "Employee Vs Perquisite";

                    worksheet.Cell(2, 1).Style.Font.Bold = true;
                    worksheet.Cell(2, 1).Value = "Group Company Name";
                    worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                    worksheet.Cell(2, 3).Style.Font.Bold = true;
                    worksheet.Cell(2, 3).Value = "Assessment Year";
                    worksheet.Cell(2, 4).Value = AssessmentYearName;
                    worksheet.Cell(2, 5).Style.Font.Bold = true;
                    worksheet.Cell(2, 5).Value = "Month";

                    worksheet.Cell(2, 6).Value = MName;
                    worksheet.Cell(2, 6).Style.DateFormat.Format = "MMM-yyyy";
                    worksheet.Cell(2, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(2, 7).Style.Font.Bold = true;
                    worksheet.Cell(2, 7).Value = "Perquisite";
                    worksheet.Cell(2, 8).Value = PerquisiteName;

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

                    var filterapplied = worksheet.Range("B4:H4").Merge();

                    filterapplied.Value = FilterValues;

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
                            "Employee Vs Perquisites List.xlsx"
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
        public async Task<IActionResult> ImportFromExcel(EmployeeVsPerquisites model)
        {
            string path = Path.Combine(this._environment.WebRootPath, "Uploads");
            string fname = model.Excel.FileData.FileName;

            try
            {               

               
                string wwwPath = this._environment.WebRootPath;
                string contentPath = this._environment.ContentRootPath;


                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                using (FileStream stream = new FileStream(Path.Combine(path, model.Excel.FileData.FileName), FileMode.Create))
                {
                    model.Excel.FileData.CopyTo(stream);

                }
                try
                {
                    using (var stream = System.IO.File.Open(path + "\\" + model.Excel.FileData.FileName, FileMode.Open, FileAccess.Read))
                    {

                        using (var reader = ExcelReaderFactory.CreateReader(stream))
                        {
                           model.EmployeeVsPerquisitesList= new List<EmployeeVsPerqList>();
                            var readerrr = ExcelReaderFactory.CreateReader(stream);
                            while (reader.Read()) //Each row of the file
                            {
                                var item = new EmployeeVsPerqList();
                                item.Employee = new EmployeeHeader();
                                if (reader.GetValue(0) != null)
                                {
                                    item.Employee.EmployeeId = reader.GetValue(0).ToString();
                                }
                                if (reader.GetValue(1) != null)
                                {
                                    item.Employee.EmployeeName = reader.GetValue(1).ToString();
                                }
                                if (reader.GetValue(2) != null)
                                {
                                    if (reader.GetValue(2).GetType().Name != "String")
                                    {
                                        item.PerqAmount = Convert.ToDecimal(reader.GetDouble(2));
                                    }else

                                        item.PerqAmountheading = reader.GetValue(2).ToString();
                                }
                                if (reader.GetValue(3) != null)
                                {
                                    if (reader.GetValue(3).GetType().Name != "String")
                                    {
                                        item.TDSAmount = Convert.ToDecimal(reader.GetDouble(3));
                                    }                                  
                                    else
                                        item.TDSAmountheading = reader.GetValue(3).ToString();
                                }

                                model.EmployeeVsPerquisitesList.Add(item);
                                
                            }
                            if (model.EmployeeVsPerquisitesList[0].Employee.EmployeeId != "Employee ID" || model.EmployeeVsPerquisitesList[0].Employee.EmployeeName != "Employee Name" || model.EmployeeVsPerquisitesList[0].TDSAmountheading != "TDS Amount" || model.EmployeeVsPerquisitesList[0].PerqAmountheading != "Perquisite Amount")
                            {
                                reader.Close();
                                if (System.IO.File.Exists(path + "\\" + fname))
                                {
                                    System.IO.File.Delete(path + "\\" + fname);
                                }
                                return Json(new ResponseEntity("Excel File not in the prescribed format", false));
                            }
                           

                            model.EmployeeVsPerquisitesList.RemoveAt(0);
                        }
                    }
                }
                catch (Exception e)
                {
                    
                    if (System.IO.File.Exists(path + "\\" + fname))
                    {
                        System.IO.File.Delete(path + "\\" + fname);
                    }
                    return Json(new ResponseEntity("Excel File not in the prescribed format", false));
                }

                model.Excel.FileData = null;
               
                var response = await _EmployeeVsPerquisitesService.ImportFromExcel(model);
                // Check if file exists with its full path    
                if (System.IO.File.Exists(path + "\\" + fname))
                {
                    System.IO.File.Delete(path + "\\" + fname);
                }
                if (response.Message != "UPDATED")
                {
                    model.EmployeeVsPerquisitesList = response.Data;
                   
                    return PartialView("_UnmatchedList", model.EmployeeVsPerquisitesList);
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
                if (System.IO.File.Exists(path + "\\" + fname))
                {
                    System.IO.File.Delete(path + "\\" + fname);
                }
                return RedirectToAction("Error", "Account", new { Area = "" });
            }


        }
    }
}
