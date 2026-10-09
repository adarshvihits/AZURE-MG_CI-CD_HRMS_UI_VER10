using ClosedXML.Excel;
using ExcelDataReader;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.PayRoll;
using Hrms.Lite.Shared.Account;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll;
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
    public class PreviousMonthAdjustmentsController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly ICommonService _commonService;
        private readonly IPreviousMonthAdjustmentsService _Service;
        private readonly IHelpers _helpers;
        private readonly IWebHostEnvironment _environment;
        public PreviousMonthAdjustmentsController(IDropdownService dropdownService, ICommonService commonService, IPreviousMonthAdjustmentsService PreviousMonthAdjustmentsService, IHelpers Helpers, IWebHostEnvironment environment)
        {
            _dropdownService = dropdownService;
            _commonService = commonService;
            _Service = PreviousMonthAdjustmentsService;
            _helpers = Helpers;
            _environment = environment;
        }
        public async Task <IActionResult> Index()
        {
            try
            {
                PreviousMonthAdjustment model = new PreviousMonthAdjustment();
                ViewBag.processingMonth = new SelectList(await _dropdownService.GetSalaryProcessing_MonthDropdown(0), "Code", "Name");
                ViewBag.EffectiveMonth = new SelectList(await _dropdownService.GetPrevMonthAdj_EffectiveMonthDropdown(0), "Code", "Name");
                ViewBag.PayGroup = new SelectList(await _dropdownService.GetPayrollGroupDropdown(), "Code", "Name");
                model.AdjustmentType = "LOP";
                model.Filter = await _commonService.GetFilterDropdowns();
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
        public async Task<JsonResult> GetEffectiveMonth(int id)
        {
            var month = new SelectList(await _dropdownService.GetPrevMonthAdj_EffectiveMonthDropdown(id), "Code", "Name");
            return Json(month);
        }

        [HttpGet]
        public async Task<JsonResult> GetProcessingMonth(int id)
        {

            var month = new SelectList(await _dropdownService.GetSalaryProcessing_MonthDropdown(id), "Code", "Name");
            return Json(month);

        }

        [HttpPost]
        public async Task<IActionResult> GetList(Filter filter1, int PayrollGroup, int ProcessingMonth, int EffectiveMonth, string AdjustmentType)
        {
            try
            {
                PreviousMonthAdjustment model = new PreviousMonthAdjustment();
                PreviousMonthAdjustment m = new PreviousMonthAdjustment();
                model.Filter = filter1;
                Filter filter = new Filter();
              
                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }
           
                if (PayrollGroup != 0 && ProcessingMonth != 0 && EffectiveMonth != 0)
                {
                    model = await _Service.PrevMonthAdjList(PayrollGroup, ProcessingMonth, EffectiveMonth,AdjustmentType, filter1);
                }
                m.PrevMonthAdjList = model.PrevMonthAdjList;
                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.PrevMonthAdjList == null)
                {
                  
                    return Json(new
                    {
                        data = "",
                        draw = filter1.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows
                    });
                }

                if (model.PrevMonthAdjList.Count > 0)
                {
                    totalrows = m.PrevMonthAdjList[0].active;

                }

                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = m.PrevMonthAdjList[0].totalRecords;
                    return Json(new
                    {
                        data = model.PrevMonthAdjList,
                        draw = filter1.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows//totalrowsafterfiltering
                    });
                }
                else
                {

                    return Json(new
                    {
                        data = model.PrevMonthAdjList,
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
        public async Task<ActionResult> EmployeeWiseSave(decimal days,string Remarks, int PayrollGroup, int ProcessingMonth,int EffectiveMonth, string AdjustmentType, Guid EmployeeGI)
        {
            try
            {
                PreviousMonthAdjustment model = new PreviousMonthAdjustment();
                model.PayrollGroup = new Shared.Master.PayrollGroup();
                model.ProcessingMonth = new Shared.PayRoll.Generic.Master.PayrollMonth();
                model.EffectiveMonth = new Shared.PayRoll.Generic.Master.PayrollMonth();
                model.PayrollGroup.Code = PayrollGroup;
                model.EmployeeGI = EmployeeGI;             
                model.Days = days;
                model.EffectiveMonth.Code = EffectiveMonth;
                model.ProcessingMonth.Code = ProcessingMonth;
                model.Remarks = Remarks;
                model.AdjustmentType = AdjustmentType;
                var response = await _Service.EmployeeWisePrevMonthAdjSave(model);
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
        public async Task<IActionResult> ImportFromExcel(PreviousMonthAdjustment model)
        {
            string path = Path.Combine(this._environment.WebRootPath, "Uploads");
            string fileName = model.Excel.FileData.FileName;
            try
            {
                model.UnMatchedList = new List<PreviousMonthAdjustment>();
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
                            List<PreviousMonthAdjustment> List = new List<PreviousMonthAdjustment>();
                            var readerrr = ExcelReaderFactory.CreateReader(stream);
                            while (reader.Read()) //Each row of the file
                            {
                                var item = new PreviousMonthAdjustment();
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
                                    item.NoOfDays = reader.GetValue(2).ToString();
                                }
                                if (reader.GetValue(3) != null)
                                {
                                    item.Remarks = reader.GetValue(3).ToString();
                                }

                                List.Add(item);
                            }
                            if (List[0].EmployeeID != "Employee ID" || List[0].EmployeeName != "Employee Name" || List[0].NoOfDays != "Days" || List[0].Remarks != "Remarks")
                            {
                                // Check if file exists with its full path    
                                if (System.IO.File.Exists(path + "\\" + fileName))
                                {
                                    System.IO.File.Delete(path + "\\" + fileName);
                                }
                                return Json(new ResponseEntity("Excel File not in the prescribed format", false));
                            }
                            model.UnMatchedList = List;

                            model.UnMatchedList.RemoveAt(0);

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
                var response = await _Service.ImportFromExcel(model);
                if (System.IO.File.Exists(path + "\\" + fileName))
                {
                    System.IO.File.Delete(path + "\\" + fileName);
                }

                if (response.Success != true && response.Data != null)
                {
                    model.UnMatchedList = response.Data;
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
        public static string ConvertSubstringToUpperCase(string originalString, int startIndex, int length)
        {
            string upperCaseSubstring = originalString.Substring(startIndex, length).ToUpper();
            return originalString.Substring(0, startIndex) + upperCaseSubstring + originalString.Substring(startIndex + length);
        }

        public async Task<IActionResult> ExportToExcel(int PayrollGroup, int ProcessingMonth,int EffectiveMonth,string PayrollGroupName, string ProcessingMonthName,
        string EffectiveMonthName,string AdjustmentType, int totalRecord, Filter Filter)
        {
            try
            {
                Filter.length = totalRecord;
                Filter.start = 0;

                string FilterValues = await _helpers.GetFilterValues(Filter);

                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Arrear Allowance / Deduction", "Excel");

                if (FilterValues == "")
                {
                    FilterValues = "All";
                }

                string currDateTime = await _helpers.GetCurrentDate();
                

                //Get Datset
                var dataset = await _Service.GetPrevMonthAdj_Excel(PayrollGroup, ProcessingMonth, EffectiveMonth, AdjustmentType, Filter);
                DataTable dt = dataset.Tables[0];

                int index = 0;
                //set EXCEL Column Heading 
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (dt.Columns[i].ColumnName == "doj" || dt.Columns[i].ColumnName == "dob" || dt.Columns[i].ColumnName == "y/s")
                        dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.ToUpper();
                    if (dt.Columns[i].ColumnName.Contains("lop"))
                    {
                        index = dt.Columns[i].ColumnName.IndexOf("lop");
                        dt.Columns[i].ColumnName = ConvertSubstringToUpperCase(dt.Columns[i].ColumnName, index, 3);
                    }
                    else
                        dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
                }

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Arrear Allowance or Deduction");
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
                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191); 
                    if(AdjustmentType=="LOP")
                    wsReportNameHeaderRange.Value = "Arrear Deduction";
                    else
                        wsReportNameHeaderRange.Value = "Arrear Allowance";

                    worksheet.Cell(2, 1).Style.Font.Bold = true;
                    worksheet.Cell(2, 1).Value = "Group Company Name";
                    worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                    worksheet.Cell(2, 3).Style.Font.Bold = true;
                    worksheet.Cell(2, 3).Value = "PayRoll Group";
                    worksheet.Cell(2, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(2, 4).Value = PayrollGroupName;
                    worksheet.Cell(2, 5).Style.Font.Bold = true;
                    worksheet.Cell(2, 5).Value = "Processing Month";
                    worksheet.Cell(2, 6).Value = ProcessingMonthName;
                    worksheet.Cell(2, 6).Style.DateFormat.Format = "dd-MMM-yyyy";
                    worksheet.Cell(3, 1).Style.Font.Bold = true;
                    worksheet.Cell(3, 1).Value = "Effective From";
                    worksheet.Cell(3, 2).Value = EffectiveMonthName;
                    worksheet.Cell(3, 2).Style.DateFormat.Format = "dd-MMM-yyyy";
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
                    worksheet.Range("B5:G5").Merge();
                    worksheet.Cell("B5").Value = FilterValues;

                    var currentRow = 6;

                    worksheet.Cell(currentRow, 1).InsertTable(dt);
                    worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                    worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                    worksheet.Range("A6:H6").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Range("A6:H6").Style.Font.Bold = true;


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
                            "Arrear Allowance / Deduction List.xlsx"
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
