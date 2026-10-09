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
    public class OndutyWfhSettingsController : Controller
    {
        private readonly IOndutyWfhSettingsService _ondutyWfhSettingService;
        private readonly ICommonService _commonService;
        private readonly IDropdownService _dropdownService;
        private readonly IHelpers _helpers;
        private readonly IWebHostEnvironment _environment;
        public static class Global
        {
            public static Filter GlobalFilter;
        }
        public OndutyWfhSettingsController(IOndutyWfhSettingsService ondutyWfhSettingService, ICommonService commonService, IDropdownService dropdownService, IHelpers Helpers, IWebHostEnvironment env)
        {
            _ondutyWfhSettingService = ondutyWfhSettingService;
            _commonService = commonService;
            _dropdownService = dropdownService;
            _helpers = Helpers;
            _environment = env;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                OndutyWfhSettings model = new OndutyWfhSettings();
                ViewBag.LeaveGrp = new SelectList(await _dropdownService.GetLeaveGroupFilterDropdown(), "Code", "Name");
                //model.Filter = new Filter();
                Filter filter = new Filter();
                filter.length = 10;
                model = await _ondutyWfhSettingService.GetOdWfhEmployeeList(filter);
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

        public async Task<IActionResult> ApplyFilter(Filter filter)
        {
            try
            {
                OndutyWfhSettings model = new OndutyWfhSettings();
                Global.GlobalFilter = filter;
                model = await _ondutyWfhSettingService.GetOdWfhEmployeeList(filter);
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

        //public async Task fillfilterDropdown()
        //{

        //    ViewBag.Gen = new SelectList(await _dropdownService.GetGenderDropDown(), "Code", "Name");
        //    ViewBag.Cat = new SelectList(await _dropdownService.GetCategoryDropdown(), "Code", "Name");
        //    ViewBag.Grd = new SelectList(await _dropdownService.GetGradeDropdown(), "Code", "Name");
        //    ViewBag.Dsg = new SelectList(await _dropdownService.GetDesignationDropdown(), "Code", "Name");
        //    ViewBag.Dept = new SelectList(await _dropdownService.GetDepartmentDropdown(), "Code", "Name");
        //    ViewBag.Loc = new SelectList(await _dropdownService.GetLocationDropdown(), "Code", "Name");
        //    ViewBag.PayrollGrp = new SelectList(await _dropdownService.GetPayrollGroupDropdown(), "Code", "Name");

        //    ViewBag.division = new SelectList(await _dropdownService.GetDivisionDropdown(), "Code", "Name");
        //    ViewBag.section = new SelectList(await _dropdownService.GetSectionDropdown(), "Code", "Name");
        //    ViewBag.EmploymentType = new SelectList(await _dropdownService.GetEmploymentTypeDropdown(), "Code", "Name");
        //    ViewBag.EmploymentStatus = new SelectList(await _dropdownService.GetEmploymentStatusDropdown(), "Code", "Name");
        //    ViewBag.WorkLocation = new SelectList(await _dropdownService.GetWorkLocationDropdown(), "Code", "Name");
        //    ViewBag.WageGrade = new SelectList(await _dropdownService.GetWageGradeDropdown(), "Code", "Name");

        //}

        public async Task<IActionResult> GetList(string TabIndex, Filter filter)
        {
            try

            {
                OndutyWfhSettings model = new OndutyWfhSettings();
                OndutyWfhSettings m = new OndutyWfhSettings();
                if (TabIndex == "MonthlyLimit")
                {


                    if (filter.searchValue == "undefined" || filter.searchValue == null)
                    {
                        filter.searchValue = "";
                    }

                    model = await _ondutyWfhSettingService.GetOdWfhEmployeeList(filter);
                    m.OndutyWfhSettingsList = model.OndutyWfhSettingsList;
                    int totalrows = 0;
                    int totalrowsafterfiltering = 0;

                    if (model.OndutyWfhSettingsList == null)
                    {

                        return Json(new
                        {
                            data = "",
                            draw = filter.draw,//Request.Form["draw"], 
                            recordsTotal = totalrows,
                            recordsFiltered = totalrows
                        });
                    }

                    if (model.OndutyWfhSettingsList.Count > 0)
                    {
                        totalrows = m.OndutyWfhSettingsList[0].activeRow;


                    }


                    if (filter.searchValue != "")
                    {
                        totalrowsafterfiltering = m.OndutyWfhSettingsList[0].totalRecords;

                        return Json(new
                        {
                            data = model.OndutyWfhSettingsList,
                            draw = filter.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                            recordsTotal = totalrows,
                            recordsFiltered = totalrows//totalrowsafterfiltering
                        });
                    }
                    else
                    {

                        return Json(new
                        {
                            data = model.OndutyWfhSettingsList,
                            draw = filter.draw,//Request.Form["draw"], 
                            recordsTotal = totalrows,
                            recordsFiltered = totalrows
                        });
                    }


                }

                else
                {

                    return PartialView("_OtherSettings");
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
        public async Task<ActionResult> EmployeeWiseSave(decimal UpdatedOdLimit, decimal UpdatedWfhLimit, Guid EmployeeGI)
        {
            try
            {
                OndutyWfhSettings model = new OndutyWfhSettings();
                model.EmployeeGI = EmployeeGI;
                model.OdLimit = UpdatedOdLimit;
                model.WfhLimit = UpdatedWfhLimit;
                var response = await _ondutyWfhSettingService.EmployeeWiseOdWfhLimitSave(model);
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

        public async Task<ActionResult> GetOtherSettingsDetails(int LeaveGroup)
        {
            try
            {
                OndutyWfhSettings model = new OndutyWfhSettings();
                model.LeaveGroup = new Shared.Master.LeaveGroup();
                model.LeaveGroup.Code = LeaveGroup;
                if (LeaveGroup != 0)
                {
                    model = await _ondutyWfhSettingService.GetOtherSettingsDetails(LeaveGroup);
                }

                return PartialView("_SettingsDetails", model);

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
        public async Task<ActionResult> Save(OndutyWfhSettings input, int LeaveGroup)
        {
            try
            {
                input.LeaveGroup = new Shared.Master.LeaveGroup();
                input.LeaveGroup.Code = LeaveGroup;
                var response = await _ondutyWfhSettingService.Save(input);

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
        public async Task<IActionResult> onExportToExcelMonrhlyLimit(int totalRecord, string searchValue, Filter filter)
        {
            try
            {
                //if (Global.GlobalFilter == null)
                //    Global.GlobalFilter = new Filter();
                //else
                //{
                //    Global.GlobalFilter.length = totalRecord;
                //    Global.GlobalFilter.start = 0;
                //}
                string FilterValues = await _helpers.GetFilterValues(filter);

                OndutyWfhSettings model = new OndutyWfhSettings();
                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                filter.length = totalRecord;
                filter.searchValue = searchValue;
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Onduty/WFH Monthly Limit List", "Excel");
                int slNo = 0;


                if (FilterValues == "")
                {
                    FilterValues = "All";
                }
                string currDateTime = await _helpers.GetCurrentDate();
                

                //Get Datset
                var dataset = await _ondutyWfhSettingService.OdWfhEmployeeList_Excel(filter);
                DataTable dt = dataset.Tables[0];


                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (dt.Columns[i].ColumnName == "doj" || dt.Columns[i].ColumnName == "dob" || dt.Columns[i].ColumnName == "y/s")
                        dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.ToUpper();
                    else
                        dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
                }

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Onduty WFH Monthly Limit List");
                    int count = dt.Rows.Count + 5;


                    worksheet.Range("A1:I" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:I" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:I" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:I" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:I" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 8)));
                    wsReportNameHeaderRange.Style.Font.Bold = true;
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Merge();
                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191); ;
                    wsReportNameHeaderRange.Value = "Onduty/WFH Monthly Limit List";

                    worksheet.Cell(2, 1).Style.Font.Bold = true;
                    worksheet.Cell(2, 1).Value = "Group Company Name";
                    worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                    worksheet.Cell(2, 3).Style.Font.Bold = true;

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
                    worksheet.Range("B4:I4").Merge();
                    worksheet.Cell("B4").Value = FilterValues;



                    var currentRow = 5;

                    worksheet.Cell(currentRow, 1).InsertTable(dt);
                    worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                    worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                    worksheet.Range("A5:I5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Range("A5:I5").Style.Font.Bold = true;


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
                            "Onduty-WFH Monthly Limit List.xlsx"
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
        public async Task<IActionResult> ExportToExcelOtherSettings(int LeaveGroup, string LGName)
        {
            try
            {
                OndutyWfhSettings model = new OndutyWfhSettings();
                model = await _ondutyWfhSettingService.GetOtherSettingsDetails(LeaveGroup);
                int slno = 0;
                string currDateTime = await _helpers.GetCurrentDate();
                
                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Od Wfh Settings", "Excel");


                using (var workbook = new XLWorkbook())
                {

                    var worksheet = workbook.Worksheets.Add("Od Wfh Settings");

                    int PendingCountnum = 18;


                    worksheet.Range("A1:D" + PendingCountnum).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:D" + PendingCountnum).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:D" + PendingCountnum).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:D" + PendingCountnum).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:D" + PendingCountnum).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 3)));
                    wsReportNameHeaderRange.Style.Font.Bold = true;
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Merge();


                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Value = "Od Wfh Settings";
                    worksheet.Range("A6:D6").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Cell(2, 1).Style.Font.Bold = true;
                    worksheet.Cell(2, 1).Value = "Company Name";
                    worksheet.Cell(2, 2).Value = CompanyName.ToUpper();
                    worksheet.Cell(3, 1).Style.Font.Bold = true;
                    worksheet.Cell(3, 1).Value = "Login User";
                    worksheet.Cell(3, 2).Value = LoginUser;
                    worksheet.Cell(4, 1).Style.Font.Bold = true;
                    worksheet.Cell(4, 1).Value = "Date & Time";
                    worksheet.Cell(4, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(4, 2).Style.DateFormat.Format = "dd-MMM-yyyy   h:mm AM/PM";
                    worksheet.Cell(4, 2).Value = currDateTime.ToString();
                    worksheet.Cell(5, 1).Style.Font.Bold = true;
                    worksheet.Cell(5, 1).Value = "Leave Group";
                    worksheet.Cell(5, 2).Value = LGName;

                    var currentRow = 6;
                    #region Header
                    worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 1).Value = "Sl.No";
                    worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 2).Value = "Category";
                    worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 3).Value = "Condition";





                    #endregion

                    #region Body
                    for (int i = 1; i <= 27; i++)
                    {
                        currentRow++;
                        slno = slno + 1;
                        worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 1).Value = slno;


                    }

                    worksheet.Range("B7:B12").Merge();
                    worksheet.Cell("B7").Value = "On duty";
                    worksheet.Cell("B7").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                    worksheet.Cell(7, 3).Value = "Pre dated application allowed";
                    worksheet.Cell(7, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(7, 4).Value = model.OdPreDatedApplAlwed;
                    worksheet.Cell(8, 3).Value = "No: of days prior";
                    worksheet.Cell(8, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(8, 4).Value = model.OdNoOfDaysPrior;
                    worksheet.Cell(9, 3).Value = "Advance Application required";
                    worksheet.Cell(9, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(9, 4).Value = model.OdAdvanceApplRequired;
                    worksheet.Cell(10, 3).Value = "No: of days in advance";
                    worksheet.Cell(10, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(10, 4).Value = model.OdNoOfDaysAdvance;
                    worksheet.Cell(11, 3).Value = "Remarks Mandatory or not";
                    worksheet.Cell(11, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(11, 4).Value = model.OdRemarksMandatory;

                    worksheet.Range("B13:B17").Merge();

                    worksheet.Cell("B13").Value = "Work From Home";
                    worksheet.Cell("B13").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                    worksheet.Cell(13, 3).Value = "Pre dated application allowed";
                    worksheet.Cell(13, 4).Value = model.WfhPreDatedApplAlwed;
                    worksheet.Cell(14, 3).Value = "No: of days prior";
                    worksheet.Cell(14, 4).Value = model.WfhNoOfDaysPrior;
                    worksheet.Cell(15, 3).Value = "Advance Application required";
                    worksheet.Cell(15, 4).Value = model.WfhAdvanceApplRequired;
                    worksheet.Cell(16, 3).Value = "No: of days in advance";
                    worksheet.Cell(16, 4).Value = model.WfhNoOfDaysAdvance;

                    worksheet.Cell(17, 3).Value = "Remarks Mandatory or not";
                    worksheet.Cell(17, 4).Value = model.WfhRemarksMandatory;



                    worksheet.Columns(1, 4).Style.Alignment.WrapText = true;
                    worksheet.Rows(1, 20).Style.Alignment.WrapText = true;
                    #endregion
                    worksheet.Columns().AdjustToContents();
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
                            "Od Wfh Settings.xlsx"
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
        public async Task<IActionResult> ImportFromExcel(OndutyWfhSettings model)
        {
            string path = Path.Combine(this._environment.WebRootPath, "Uploads");
            string fileName = model.Excel.FileData.FileName;
            try
            {
                model.OndutyWfhSettingsList = new List<OndutyWfhSettings>();
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
                            List<OndutyWfhSettings> List = new List<OndutyWfhSettings>();
                            var readerrr = ExcelReaderFactory.CreateReader(stream);
                            while (reader.Read()) //Each row of the file
                            {
                                var item = new OndutyWfhSettings();
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
                                        item.OdLimit = Convert.ToDecimal(reader.GetDouble(2));
                                    }
                                    else
                                    {
                                        item.OdLimitStr = reader.GetValue(2).ToString();
                                    }
                                }
                                if (reader.GetValue(3) != null)
                                {
                                    if (reader.GetValue(3).GetType().Name != "String")
                                    {
                                        item.WfhLimit = Convert.ToDecimal(reader.GetDouble(3));
                                    }
                                    else
                                    {
                                        item.WfhLimitStr = reader.GetValue(3).ToString();
                                    }
                                }

                                List.Add(item);

                            }
                            if (List[0].EmployeeID != "Employee ID" || List[0].EmployeeName != "Employee Name" || List[0].OdLimitStr != "OD" || List[0].WfhLimitStr != "WFH")
                            {
                                // Check if file exists with its full path    
                                if (System.IO.File.Exists(path + "\\" + fileName))
                                {
                                    System.IO.File.Delete(path + "\\" + fileName);
                                }
                                return Json(new ResponseEntity("Excel File not in the prescribed format", false));
                            }
                            model.OndutyWfhSettingsList = List;

                            model.OndutyWfhSettingsList.RemoveAt(0);

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
                var response = await _ondutyWfhSettingService.ImportFromExcel(model);
                if (System.IO.File.Exists(path + "\\" + fileName))
                {
                    System.IO.File.Delete(path + "\\" + fileName);
                }

                if (response.Success != true)
                {
                    model.OndutyWfhSettingsList = response.Data;
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

    }
}

