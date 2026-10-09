using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.TimeOffice;
using Hrms.Lite.Shared.Account;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TimeOffice;
using Hrms.Lite.Shared.TimeOffice.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Spire.Xls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.TimeOffice.Controllers
{
    [Area("TimeOffice")]
    [SessionAuthorize]
    public class MaternityLeaveApplicationController : Controller
    {

        private readonly IMaternityLeaveApplicationService _service;
        private readonly ILeaveApplicationService _LeaveApplicationService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        private readonly IDropdownService _drop;
        private readonly IBlob _blob;
        public MaternityLeaveApplicationController(IMaternityLeaveApplicationService service, ICommonService commonService,
            IHelpers helpers, IDropdownService drop, IBlob blob, ILeaveApplicationService leave)
        {
            _service = service;
            _commonService = commonService;
            _helpers = helpers;
            _drop = drop;
            _blob = blob;
            _LeaveApplicationService = leave;

        }
        #region  EmployeeList
        public async Task<IActionResult> Index()
        {
            try
            {
                MaternityLeaveApplication model = new MaternityLeaveApplication();
            model.LeaveApplication = new LeaveApplication();
            model.LeaveApplication.filter = new Filter();
            MaternityLeaveApplication m1 = new MaternityLeaveApplication();
            Filter filter = new Filter();
            filter.length = 10;
            m1 = await _service.GetMaternityLeaveApplicationEmployeeList(filter);
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
        public async Task<IActionResult> GetEmployeeList(Filter filter)
        {
            try
            {
                MaternityLeaveApplication model = new MaternityLeaveApplication();

                if (filter.searchValue == "undefined" || filter.searchValue == null)
                {
                    filter.searchValue = "";
                }

                model = await _service.GetMaternityLeaveApplicationEmployeeList(filter);

                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.LeaveApplication.LeaveApplicationEmployeeList == null || model.LeaveApplication.LeaveApplicationEmployeeList.Count() == 0)
                {


                    return Json(new
                    {
                        data = "",
                        draw = filter.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows
                    });
                }

                if (model.LeaveApplication.LeaveApplicationEmployeeList.Count > 0)
                {
                    totalrows = model.LeaveApplication.LeaveApplicationEmployeeList[0].active;
                }
                if (filter.searchValue != "")
                {
                    totalrowsafterfiltering = model.LeaveApplication.LeaveApplicationEmployeeList[0].totalRecords;

                    return Json(new
                    {
                        data = model.LeaveApplication.LeaveApplicationEmployeeList,
                        draw = filter.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows//totalrowsafterfiltering
                    });
                }
                else
                {
                    return Json(new
                    {
                        data = model.LeaveApplication.LeaveApplicationEmployeeList,
                        draw = filter.draw,//Request.Form["draw"], 
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

            ViewBag.Gen = new SelectList(await _drop.GetGenderDropDown(), "Code", "Name");
            ViewBag.Cat = new SelectList(await _drop.GetCategoryDropdown(), "Code", "Name");
            ViewBag.Grd = new SelectList(await _drop.GetGradeDropdown(), "Code", "Name");
            ViewBag.Dsg = new SelectList(await _drop.GetDesignationDropdown(), "Code", "Name");
            ViewBag.Dept = new SelectList(await _drop.GetDepartmentDropdown(), "Code", "Name");
            ViewBag.Loc = new SelectList(await _drop.GetLocationDropdown(), "Code", "Name");
            ViewBag.PayrollGrp = new SelectList(await _drop.GetPayrollGroupDropdown(), "Code", "Name");

            ViewBag.division = new SelectList(await _drop.GetDivisionDropdown(), "Code", "Name");
            ViewBag.section = new SelectList(await _drop.GetSectionDropdown(), "Code", "Name");
            ViewBag.EmploymentType = new SelectList(await _drop.GetEmploymentTypeDropdown(), "Code", "Name");
            ViewBag.EmploymentStatus = new SelectList(await _drop.GetEmploymentStatusDropdown(), "Code", "Name");
            ViewBag.WorkLocation = new SelectList(await _drop.GetWorkLocationDropdown(), "Code", "Name");
            ViewBag.WageGrade = new SelectList(await _drop.GetWageGradeDropdown(), "Code", "Name");

        }
        public async Task<IActionResult> ExportToExcelEmployeeList(Filter filter, int TotalRows)
        {
           
            var LoginUser = HttpContext.Session.GetString("LoginUser");
            var CompanyName = HttpContext.Session.GetString("CompanyName");
            var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
            var GroupCompanyName = User.authUser.GroupCompanyName;
            ExcelPassword EP = new ExcelPassword();
            EP = await _commonService.GetExcelPassword("Maternity/ESI Leave Report", "Excel");


            filter.length = TotalRows;
            filter.start = 0;

            string FilterValues = await _helpers.GetFilterValues(filter);
            if (FilterValues == "")
            {
                FilterValues = "All";
            }




            string[] cu_Date = System.DateTime.Now.GetDateTimeFormats();
            string currDateTime = cu_Date[44];

            //Get Dataset
            var dataset = await _service.GetMaternityLeaveApplicationEmployeeList_Excel( filter);
            DataTable dt = dataset.Tables[0];


            //set EXCEL Column Heading 
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                if (dt.Columns[i].ColumnName == "doj")
                    dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.ToUpper();
                else
                    dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
            }


            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Maternity Employee  List");
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
                wsReportNameHeaderRange.Value = "Maternity Leave Application Employee  List";

                worksheet.Cell(2, 1).Style.Font.Bold = true;
                worksheet.Cell(2, 1).Value = "Group Company Name";
                worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                worksheet.Cell(2, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                

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
                var filterapplied = worksheet.Range("B4:G4").Merge();

                filterapplied.Value = FilterValues;


                var currentRow = 5;


                worksheet.Cell(currentRow, 1).InsertTable(dt);
                worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;

                worksheet.Range("A5:G5" ).Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                worksheet.Range("A5:G5" ).Style.Font.Bold = true;

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
                        book.Protect(EP.Password);
                        book.SaveToStream(stream);
                    }
                    var content = stream.ToArray();
                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "Maternity Leave Application Employee List.xlsx"
                        );
                }
            }

            //}
        }

        #endregion

        #region Create
        public async Task<IActionResult> Create(Guid EmployeeGI, int employeeCode, string EmployeeId, string EmployeeName)
        {
            try
            {
                LeaveApplication model = new LeaveApplication();
                model.EmployeeGI = EmployeeGI;
                ViewBag.Session = new SelectList(await _drop.GetDayStatusDropDown(), "Code", "Name");
                ViewBag.SessionTo = new SelectList(await _drop.GetDayStatusWithoutSecondHalfDropDown(), "Code", "Name");
                ViewBag.LeaveType = new SelectList(await _drop.GetLeaveDropDown("ML_ESI_Leave", EmployeeGI), "Code", "Name");
                ViewBag.Reason = new SelectList(await _drop.GetLeaveVsLeaveReasonsDropDown(0), "Code", "Name");
                ViewBag.Date = DateTime.Now.Date.ToString("dd-MMM-yyyy", CultureInfo.InvariantCulture);
                model.DateOfApplication = DateTime.Now.Date;
                model.FromType = new MasterBase();
                model.ToType = new MasterBase();

                model.FromType.Code = 1;
                model.ToType.Code = 1;
                ViewBag.employeeId = EmployeeId;
                ViewBag.employeeName = EmployeeName;
                EmployeeAuthority modelempauth = new EmployeeAuthority();
                modelempauth.EmployeeAuthorityList = (List<EmployeeAuthority>)await _commonService.GetEmployeeAuthorityDetails(EmployeeGI);

                model.EmployeeAuthority = modelempauth.EmployeeAuthorityList;

                //Get Storage Type
                var StorageType = await _commonService.GetFileStorageType();

                if (StorageType.Data == "BLOB")
                {
                    //retreive Authority Profile pic From AZure Storage
                    for (int j = 0; j < model.EmployeeAuthority.Count; j++)
                    {
                        Shared.File file = new Shared.File();
                        file.FileName = model.EmployeeAuthority[j].FileName;
                        file = await _blob.GetBlobFile(file);
                        if (file != null)
                        {
                            file.fdata = file.stream.ToArray();
                            var mimeType = GetMimeTypeFromFilename(model.EmployeeAuthority[j].FileName);
                            model.EmployeeAuthority[j].Base64string = mimeType + Convert.ToBase64String(file.fdata, 0, file.fdata.Length);
                        }

                    }
                }

                //Leave balance
                var response = await _commonService.GetCurrentLeavePeriod();
                int leavePeriodCode = Convert.ToInt32(response.Data);

                model.LeaveBalanceList = (List<Shared.TimeOffice.Generic.LeaveBalance>)await _LeaveApplicationService.GetLeaveBalance(leavePeriodCode, employeeCode);
                //var leaveType = _commonService.GetAttendanceStatusCode("LOP");
            
                model.EmployeeHeader = new Shared.General.EmployeeHeader();
                model.EmployeeHeader = await _commonService.GetEmployeeHeader(EmployeeGI);

                //Get Maternity leave days

                ViewBag.Storage = StorageType.Data;
                return View("Create", model);
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


        public static string GetMimeTypeFromFilename(string filename)
        {
            if (string.IsNullOrEmpty(filename))
            {
                return null;
            }

            var extension = Path.GetExtension(filename).TrimStart('.').ToLowerInvariant();
            var mimeTypeMap = new Dictionary<string, string>()
          {
        { "jpg", "data:image/jpeg;base64," },
        { "jpeg", "data:image/jpeg;base64," },
        { "png", "data:image/png;base64," },
        // Add more mappings for other supported extensions
          };

            return mimeTypeMap.ContainsKey(extension) ? mimeTypeMap[extension] : null;
        }




        [HttpGet]
        public async Task<IActionResult> GetMaternityDaysAndToDate(DateTime FromDate)
        {
            try
            {
        
                var days = await _commonService.GetMaternityLeaveDays();
                DateTime TODATE = FromDate.AddDays((days-1));
                var result = new { Date = TODATE, Days = days };
                return Json(result);
               
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
        public async Task<IActionResult> GetLeaveVsReaons(int LeaveCode)
        {
            try
            {
                var Reasons = await _drop.GetLeaveVsLeaveReasonsDropDown(LeaveCode);
                return Json(Reasons);
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
        public async Task<IActionResult> CheckIfAlreadyLeaveApplied(DateTime DateOfApplication, int LeaveType, DateTime fromDate, int SessionFrom, DateTime ToDate, int SessionTo, Guid empGI)
        {
            try
            {
                LeaveApplication model = new LeaveApplication();

                model.LeaveType = new MasterBase();
                model.FromType = new MasterBase();
                model.ToType = new MasterBase();

                model.DateOfApplication = DateOfApplication;
                model.LeaveType.Code = LeaveType;
                model.FromType.Code = SessionFrom;
                model.ToType.Code = SessionTo;
                model.From = fromDate;
                model.To = ToDate;
                model.EmployeeGI = empGI;
                var response = await _commonService.GetLeaveValidation(model, empGI);
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
        public async Task<IActionResult> GetLeaveDayWiseDetails(DateTime DateOfApplication, int LeaveType, DateTime fromDate, int SessionFrom, DateTime ToDate, int SessionTo, Guid empGI)
        {
            try
            {
                LeaveApplication model = new LeaveApplication();

                model.LeaveType = new MasterBase();
                model.FromType = new MasterBase();
                model.ToType = new MasterBase();

                model.DateOfApplication = DateOfApplication;
                model.LeaveType.Code = LeaveType;
                model.FromType.Code = SessionFrom;
                model.ToType.Code = SessionTo;
                model.From = fromDate;
                model.To = ToDate;

                model.LeaveDetails = await _LeaveApplicationService.GetLeaveDayWiseDetails(model, empGI);
                return PartialView("_DayWiseList", model);
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

        #endregion
        [HttpPost]
        public async Task<IActionResult> SaveLeave(LeaveApplication model)
        {
            var StorageType = await _commonService.GetFileStorageType();

            try
            {
                if (model.Attachment != null)
                {
                    if (model.Attachment.FileData != null)
                    {
                        model.Attachment.fdata = await _helpers.CompressFile(model.Attachment.FileData);
                        model.Attachment.FileName = model.Attachment.FileData.FileName;
                        model.Attachment.ContentType = model.Attachment.FileData.ContentType;
                        model.Attachment.FileExtension = Path.GetExtension(model.Attachment.FileData.FileName).ToLowerInvariant();
                        model.Attachment.FileData = null;

                    }
                }

              
                var response = await _LeaveApplicationService.SaveLeave(model);
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




        #region Apllication History

        public async Task<IActionResult> ApplicationHistory(Guid EmployeeGI)
        {
            try
            {

                MaternityLeaveApplication model = new MaternityLeaveApplication();
                ViewBag.LeavePeriod = new SelectList(await _drop.GetLeavePeriodFilterDropdown(), "Code", "Name");
                ViewBag.LeaveType = new SelectList(await _drop.GetLeaveDropDown("ML_ESI_Leave", EmployeeGI), "Code", "Name");
               
                var leavePeriodCode = 0;
                var response = await _commonService.GetCurrentLeavePeriod();
                leavePeriodCode = Convert.ToInt32(response.Data);
                model = await _service.GetMaternityLeaveApplicationList(leavePeriodCode, 0, "PENDING", EmployeeGI);

               
                model.LeaveApplication.LeavePeriod = new MasterBase();
                model.LeaveApplication.LeavePeriod.Code = leavePeriodCode;
                model.LeaveApplication.EmployeeGI = EmployeeGI;
                model.LeaveApplication.EmployeeHeader = new Shared.General.EmployeeHeader();
                model.LeaveApplication.EmployeeHeader = await _commonService.GetEmployeeHeader(EmployeeGI);
                return View("ApplicationHistory", model);
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




        public async Task<IActionResult> GetLeaveApplicationList(int leavePeriodCode, int leaveType, string TabIndex, Guid EmployeeGI)
        {
            try
            {
                MaternityLeaveApplication model = new MaternityLeaveApplication();

                model = await _service.GetMaternityLeaveApplicationList(leavePeriodCode, leaveType, TabIndex, EmployeeGI);
                
                return PartialView("_ApplicationHistoryList", model);
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

        public async Task<IActionResult> ExportToExcel(int leavePeriodCode, string periodName, int leaveType, string leaveTypeName, string TabIndex, Guid EmployeeGI)
        {
            try
            { 
                MaternityLeaveApplication model = new MaternityLeaveApplication();
            var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Leave Application (" + TabIndex + ")", "Excel");
                string currDateTime = await _helpers.GetCurrentDate();
                
                int slNo = 0;
                model = await _service.GetMaternityLeaveApplicationList(leavePeriodCode, leaveType, TabIndex, EmployeeGI);


                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Leave Application( " + TabIndex + " )");

                    int count = model.LeaveApplication.LeaveApplicationList.Count + 4;


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
                    wsReportNameHeaderRange.Value = "Maternity Leave Application List( " + TabIndex + " )";

                    worksheet.Cell(2, 1).Style.Font.Bold = true;
                    worksheet.Cell(2, 1).Value = "Group Company Name";
                    worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                    worksheet.Cell(2, 3).Style.Font.Bold = true;
                    worksheet.Cell(2, 3).Value = "Leave Period";
                    worksheet.Cell(2, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(2, 4).Value = periodName;
                    worksheet.Cell(2, 5).Style.Font.Bold = true;
                    worksheet.Cell(2, 5).Value = "Leave Type";
                    worksheet.Cell(2, 6).Value = leaveTypeName;
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
                    worksheet.Cell(3, 6).Value = model.LeaveApplication.LeaveApplicationList.Count;

                    var currentRow = 4;
                    #region Header
                    worksheet.Range("A4:H4").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Cell(currentRow, 1).Value = "Sl.NO";
                    worksheet.Cell(currentRow, 1).Style.Font.Bold = true;

                    worksheet.Cell(currentRow, 2).Value = "From";
                    worksheet.Cell(currentRow, 2).Style.Font.Bold = true;

                    worksheet.Cell(currentRow, 3).Value = "To";
                    worksheet.Cell(currentRow, 3).Style.Font.Bold = true;

                    worksheet.Cell(currentRow, 4).Value = "No.of Days";
                    worksheet.Cell(currentRow, 4).Style.Font.Bold = true;

                    worksheet.Cell(currentRow, 5).Value = "Type";
                    worksheet.Cell(currentRow, 5).Style.Font.Bold = true;

                    worksheet.Cell(currentRow, 6).Value = "Reason";
                    worksheet.Cell(currentRow, 6).Style.Font.Bold = true;

                    worksheet.Cell(currentRow, 7).Value = "Date of Application";
                    worksheet.Cell(currentRow, 7).Style.Font.Bold = true;

                    worksheet.Cell(currentRow, 8).Value = "Status";
                    worksheet.Cell(currentRow, 8).Style.Font.Bold = true;

                    foreach (var item in model.LeaveApplication.LeaveApplicationList)
                    {
                        currentRow++;
                        slNo = slNo + 1;

                        worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 1).Value = slNo;
                        worksheet.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 2).Style.DateFormat.Format = "dd-MMM-yyyy";
                        worksheet.Cell(currentRow, 2).Value = item.From;
                        worksheet.Cell(currentRow, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 3).Style.DateFormat.Format = "dd-MMM-yyyy";
                        worksheet.Cell(currentRow, 3).Value = item.To;
                        worksheet.Cell(currentRow, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 4).Value = item.TotalDays;
                        worksheet.Cell(currentRow, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 5).Value = item.LeaveType.Name;
                        worksheet.Cell(currentRow, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 6).Value = item.LeaveReason.Name;
                        worksheet.Cell(currentRow, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 7).Style.DateFormat.Format = "dd-MMM-yyyy";
                        worksheet.Cell(currentRow, 7).Value = item.DateOfApplication;
                        worksheet.Cell(currentRow, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 8).Value = item.Status;




                        worksheet.Columns(1, 8).Style.Alignment.WrapText = true;
                    }
                    #endregion
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
                          "Maternity Leave Application List.xlsx"
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
        public async Task<IActionResult> GetTrackerDetails(Guid LeaveApplicationGI)
        {
            try
            {

                MaternityLeaveApplication model = new MaternityLeaveApplication();

                model = await _service.GetTrackerDetails(LeaveApplicationGI);
                DateTime FromDate = Convert.ToDateTime(model.LeaveApplication.From.ToString());
                DateTime ToDate = Convert.ToDateTime(model.LeaveApplication.To.ToString());

                model.LeaveApplication.From = Convert.ToDateTime(FromDate.ToString("dd-MMM-yyyy", CultureInfo.InvariantCulture));
                model.LeaveApplication.To = Convert.ToDateTime(ToDate.ToString("dd-MMM-yyyy", CultureInfo.InvariantCulture));


                model.LeaveApplication.DateOfApplication = Convert.ToDateTime(model.LeaveApplication.DateOfApplication.ToString("dd-MMM-yyyy", CultureInfo.InvariantCulture));


                

                return PartialView("_ApplicationTracker", model);
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
        #endregion

    }

}


