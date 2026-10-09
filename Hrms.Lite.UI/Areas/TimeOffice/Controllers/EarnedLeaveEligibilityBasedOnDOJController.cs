using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.TimeOffice;
using Hrms.Lite.Shared.Account;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TimeOffice;
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
using Microsoft.AspNetCore.Hosting;
using ExcelDataReader;

namespace Hrms.Lite.UI.Areas.TimeOffice.Controllers
{
    [Area("TimeOffice")]
    [SessionAuthorize]
    public class EarnedLeaveEligibilityBasedOnDOJController : Controller
    {
        private readonly IEarnedLeaveEligibilityBasedOnDOJService _EarnedLeaveEligibilityService;
        private readonly ICommonService _commonService;
        private readonly IDropdownService _dropdownService;
        private readonly IWebHostEnvironment _environment;
        private readonly IHelpers _helpers;

        public EarnedLeaveEligibilityBasedOnDOJController(IDropdownService dropdownService, IEarnedLeaveEligibilityBasedOnDOJService EarnedLeaveEligibilityService, ICommonService commonService, IWebHostEnvironment environment, IHelpers helpers)
        {
            _EarnedLeaveEligibilityService = EarnedLeaveEligibilityService;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _environment = environment;
            _helpers = helpers;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                EarnedLeaveEligibility model = new EarnedLeaveEligibility();
                EarnedLeaveEligibility m1 = new EarnedLeaveEligibility();
                Filter filter = new Filter();
                m1 = await _EarnedLeaveEligibilityService.EarnedLeaveEligibilityList(0, 0, 0, filter);
                ViewBag.Leave = new SelectList(await _dropdownService.GetEarnedLeaveDropdown(0), "Code", "Name");
                ViewBag.Month = new SelectList(await _dropdownService.GetMonthlyAttendanceSummaryMonthDropdown(), "Code", "Name");
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
        public async Task<JsonResult> GetApplicableLeaves(int LeaveGroup)
        {
            //var Leaves = new SelectList(await _dropdownService.GetLeaveGroupApplicableLeavesDropdown(LeaveGroup), "Code", "Name");
            var Leaves = new SelectList(await _dropdownService.GetEarnedLeaveDropdown(LeaveGroup), "Code", "Name");

            return Json(Leaves);
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


        public async Task<IActionResult> GetEarnedLeaveEligibilityList(Filter filter, int Leave, int Month,int ConfirmationValue)
        {
            try
            {
                EarnedLeaveEligibility model = new EarnedLeaveEligibility();
                model = await _EarnedLeaveEligibilityService.EarnedLeaveEligibilityList(Leave, Month, ConfirmationValue, filter);

                return PartialView("_List", model);
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
        public async Task<IActionResult> EarnedLeaveEligibilitySave(Filter filter, int Leave, int Month)
        {

            try
            {
                var response = await _EarnedLeaveEligibilityService.SaveEarnedLeaveEligibility(Leave, Month, filter);
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
        public async Task<ActionResult> EmployeeWiseSave(int UpdatedOpening, int Month, int Leave, Guid EmployeeGI)
        {
            try
            {
                EarnedLeaveEligibility model = new EarnedLeaveEligibility();
                model.Month = new Shared.TimeOffice.Generic.Master.Month();
                model.Leave = new Shared.Master.Leave();

                model.ELOpening = UpdatedOpening;
                model.Month.Code = Month;
                model.Leave.Code = Leave;
                model.EmployeeGI = EmployeeGI;
                var response = await _EarnedLeaveEligibilityService.EmployeeWiseSave(model);
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


        public async Task<IActionResult> ExportToExcel(Filter filter, int totRecords, int Month, int Leave,int ConfirmationValue,string LeaveName,string MonthName)
        {
            try
            {



                filter.length = totRecords;
                filter.start = 0;

                string FilterValues = await _helpers.GetFilterValues(filter);
                //TempData["Global_Filter_DailyAttendanceEntry"] = JsonConvert.SerializeObject(filter);

                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Earned Leave Eligibility Based On DOJ", "Excel");

                if (FilterValues == "")
                {
                    FilterValues = "All";
                }

                string currDateTime = await _helpers.GetCurrentDate();
                

                //Get Datset
                var dataset = await _EarnedLeaveEligibilityService.EarnedLeaveEligibilityList_Excel(Leave, Month,ConfirmationValue ,filter);
                DataTable dt = dataset.Tables[0];


                //set EXCEL Column Heading 
                for (int i = 0; i < dt.Columns.Count; i++)
                {


                    dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
                }

                using (var workbook = new XLWorkbook())
                {
                   
                      
                    var worksheet = workbook.Worksheets.Add("EL Eligibility_DOJ");
                    int rowcount = dt.Rows.Count + 6;

                    string columnName = "";
                    //int Columncount = model.MainList.Header.Count + 3;
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
                    if (ConfirmationValue == 0) wsReportNameHeaderRange.Value = "Earned Leave Eligibility Based On DOJ";
                    else wsReportNameHeaderRange.Value = "Earned Leave Opening Based On DOJ";


                    worksheet.Cell(2, 1).Style.Font.Bold = true;
                    worksheet.Cell(2, 1).Value = "Group Company Name";
                    worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();



                    worksheet.Cell(2, 3).Style.Font.Bold = true;
                    worksheet.Cell(2, 3).Value = "Month";
                    worksheet.Cell(2, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(2, 4).Value = MonthName;


                    //worksheet.Cell(2, 5).Style.Font.Bold = true;
                    //worksheet.Cell(2, 5).Value = "Leave Period";
                    //worksheet.Cell(2, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    //worksheet.Cell(2, 6).Value = LeavePeriod;

                    //worksheet.Cell(3, 1).Style.Font.Bold = true;
                    //worksheet.Cell(3, 1).Value = "Leave Group";
                    //worksheet.Cell(3, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    //worksheet.Cell(3, 2).Value = LeaveGroup;

                    worksheet.Cell(2, 5).Style.Font.Bold = true;
                    worksheet.Cell(2, 5).Value = "Leave";
                    worksheet.Cell(2, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(2, 6).Value = LeaveName;




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
                        if (ConfirmationValue == 0)
                        {
                            return File(
                               content,
                               "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                               "Earned Leave Eligibility Based On DOJ.xlsx"
                               );
                        }
                        else
                        {
                            return File(
                            content,
                            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                            "Earned Leave Opening Based On DOJ.xlsx"
                            );
                        }
                           
                        
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
        [HttpPost]
        public async Task<IActionResult> ImportFromExcel(EarnedLeaveEligibility model)
        {
            string path = Path.Combine(this._environment.WebRootPath, "Uploads");
            string fileName = model.Excel.FileData.FileName;
            try
            {
                model.EarnedLeaveEligibilityList = new List<EarnedLeaveEligibility>();
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

                return await populateModel(fileName, model);

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
        public async Task<IActionResult> populateModel(string fileName, EarnedLeaveEligibility model)
        {
            string path = Path.Combine(this._environment.WebRootPath, "Uploads");
            try
            {
                using (var stream = System.IO.File.Open(path + "\\" + fileName, FileMode.Open, FileAccess.Read))
                {

                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        List<EarnedLeaveEligibility> List = new List<EarnedLeaveEligibility>();

                        while (reader.Read()) //Each row of the file
                        {
                            var item = new EarnedLeaveEligibility();
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
                                    item.ELOpening = Convert.ToInt32(reader.GetDouble(2));
                                }
                                else
                                {
                                    item.ELOpeningString = reader.GetValue(2).ToString();
                                }
                            }

                            List.Add(item);

                        }
                        model.EarnedLeaveEligibilityList = List;
                        model.EarnedLeaveEligibilityList.RemoveAt(0);
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
            var response = await _EarnedLeaveEligibilityService.ImportFromExcel(model.Month.Code, model.Leave.Code, model);
            // Check if file exists with its full path    
            if (System.IO.File.Exists(path + "\\" + fileName))
            {
                System.IO.File.Delete(path + "\\" + fileName);
            }
            if (response.Message != "UPDATED")
            {
                model.EarnedLeaveEligibilityList = response.Data.EarnedLeaveEligibilityList;
                //model.Message = response.Message;
                return PartialView("_UnmatchedList", model);
            }
            else
            {
                response.Message = "Uploaded Successfully";
                return Json(response);
            }
        }


    }
}