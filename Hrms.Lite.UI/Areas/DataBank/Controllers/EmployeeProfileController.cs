using System;
using System.Data;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.Account;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.DataBank.Reports;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.StaticFiles;
using Newtonsoft.Json;
using Spire.Xls;

namespace Hrms.Lite.UI.Areas.DataBank.Controllers
{
    [Area("DataBank")]
    [SessionAuthorize]
    public class EmployeeProfileController : Controller
    {
        private IDropdownService _dropdownService;
        private int i;
        private readonly IEmployeeProfileService _employeeProfileService;
        private readonly IHelpers _helpers;

        private readonly ICommonService _commonService;
        private readonly IBlob _blob;
        public static class Global
        {
            public static Filter GlobalFilter;
            public static int ActiveCount;
            public static int ResignedCount;
        }
        public EmployeeProfileController(IEmployeeProfileService employeeProfileService, ICommonService commonService, IBlob blob, IDropdownService dropdownService, IHelpers Helpers)
        {
            _employeeProfileService = employeeProfileService;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _blob = blob;
            _helpers = Helpers;
        }
        //[HttpPost]
        public async Task<IActionResult> Index()
        {
            try {
                EmployeeProfile model = new EmployeeProfile();
                EmployeeProfile m = new EmployeeProfile();
                Filter filter = new Filter();
                filter.length = 10;
                m = await _employeeProfileService.GetEmployeeProfileList(0, filter);
                model.EmployeeHeader = new EmployeeHeader();
                model.Filter = new Filter();
                //  model = await _employeeProfileService.GetEmployeeProfileList(0,filterdata);
                model.Filter = await _commonService.GetFilterDropdowns();
                var StorageType = await _commonService.GetFileStorageType();
                ViewBag.Storage = StorageType.Data;
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
       

        public IActionResult GetTabCount()
        {
            EmployeeProfile model = new EmployeeProfile();
            model.ActiveCount = Global.ActiveCount;
            model.ResignedCount = Global.ResignedCount;
            return Json(model);
        }
        [HttpPost]
        public async Task<IActionResult> GetActiveList(Filter filter1, string test)
        {
            try
            {
                EmployeeProfile model = new EmployeeProfile();
              
                model.EmployeeHeader = new EmployeeHeader();

                if(filter1.Advance_Filter.AgeAboveOrBelow== "Less Than")
                {
                    filter1.Advance_Filter.AgeAboveOrBelow = "Below";
                }
                if (filter1.Advance_Filter.AgeAboveOrBelow == "Greater Than")
                {
                    filter1.Advance_Filter.AgeAboveOrBelow = "Above";
                }
                if (filter1.Advance_Filter.YSAboveOrBelow == "Less Than")
                {
                    filter1.Advance_Filter.YSAboveOrBelow = "Below";
                }
                if (filter1.Advance_Filter.YSAboveOrBelow == "Greater Than")
                {
                    filter1.Advance_Filter.YSAboveOrBelow = "Above";
                }
               
                    Filter filter = new Filter();
                Global.GlobalFilter = filter1;


                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }     

                model = await _employeeProfileService.GetEmployeeProfileList(0, filter1);
                Global.ActiveCount = model.ActiveCount;
                Global.ResignedCount = model.ResignedCount;


              
                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.EmployeeProfileList.Count > 0)
                {

                    if (filter1.Tabindex == 0)
                    {
                        totalrows = model.EmployeeProfileList[0].active;
                    }
                    else
                    {
                        totalrows = model.EmployeeProfileList[0].resigned;
                    }

                    //m.OnBoardingList.ToList<Hrms.Lite.Shared.Master.Test>().Count; ;
                }

               

                if (model.EmployeeProfileList == null || model.EmployeeProfileList.Count==0)
                {

                    return Json(new
                    {

                       
                        data = model.ActiveCount,
                        
                        draw = filter1.draw,
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows
                    });
                }
                if (filter1.searchValue != "")
                { 
                    totalrowsafterfiltering = model.EmployeeProfileList[0].totalRecords;
                    
                    return Json(new
                    {
                        data = model.EmployeeProfileList,
                        draw = filter1.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows//totalrowsafterfiltering
                    }) ;
                }
                else
                {
                    return Json(new
                    {
                        data = model.EmployeeProfileList,
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

        public async Task<IActionResult> ApplyFilter(int TabIndex, Filter filter)
        {
            try { 
            EmployeeProfile model = new EmployeeProfile();
            Global.GlobalFilter = filter;
           // model = await _employeeProfileService.GetEmployeeProfileList(TabIndex, filter);
            if (TabIndex == 0)
            {
                return PartialView("_ActiveList", model);

            }
            else
            {
                    model = await _employeeProfileService.GetEmployeeProfileList(TabIndex, filter);
                    return PartialView("_ResignedList", model);

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

        public async Task<IActionResult> Summary()
        {
            try { 
            EmployeeProfile model = new EmployeeProfile();

            if (Global.GlobalFilter == null)
                Global.GlobalFilter = new Filter();
            var GroupByLIst = await _dropdownService.GetEmployeeProfileSummaryGroupByDropDown();
            ViewBag.Gpy = new SelectList(GroupByLIst, "Code", "Name");
            int firstgroupById= GroupByLIst[0].Code;
            model = await _employeeProfileService.GetEmployeeSummary(firstgroupById, Global.GlobalFilter);
            ViewBag.TotalCount = model.EmployeeProfileSummary.Sum(x => x.Count);
            ViewBag.TotalCTC = model.EmployeeProfileSummary.Sum(x => x.CTC);
            model.groupbyname = GroupByLIst[0].Name;
            return PartialView("_Summary",model);
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
        public async Task<IActionResult> GroupByBasedSummary(int id,string groupbyname)
        {
            try { 
            EmployeeProfile model = new EmployeeProfile();

            if (Global.GlobalFilter == null)
                Global.GlobalFilter = new Filter();
            //ViewBag.Gpy = new SelectList(await _dropdownService.GetEmployeeProfileSummaryGroupByDropDown(), "Code", "Name");
            model = await _employeeProfileService.GetEmployeeSummary(id, Global.GlobalFilter);
          
            model.groupbyname = groupbyname;
            ViewBag.TotalCount = model.EmployeeProfileSummary.Sum(x => x.Count);         
            ViewBag.TotalCTC = model.EmployeeProfileSummary.Sum(x => x.CTC);

            return PartialView("_SummaryList", model);
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

     
        public async Task<IActionResult> GetEmployeeProfileDetails(Guid EmployeeGI, int FinancialYear, string Breadcrumb,string BreadcrumbName)
        {
            try { 
            EmployeeProfile model = new EmployeeProfile();

                int CurrentFinancialYearCode = 0;

                if (FinancialYear == 0)
            {
                

                if (CurrentFinancialYearCode == 0)
                {
                    var response = await _commonService.GetCurrentLFinancialYear();

                    CurrentFinancialYearCode = Convert.ToInt32(response.Data);
                }
                model.FinancialYear = new FinancialYear();
                model.FinancialYear.Code = CurrentFinancialYearCode;
                FinancialYear = CurrentFinancialYearCode;
                model = await _employeeProfileService.GetEmployeeProfileDetails(EmployeeGI, FinancialYear);
                }
            else
            {
                model.FinancialYear.Code = FinancialYear;
                model = await _employeeProfileService.GetEmployeeProfileDetails(EmployeeGI, FinancialYear);
                }
                var EmpNameWithoutId = model.EmployeeHeader.EmployeeName.Split('(')[0].Trim();
                ViewBag.FinancialYear = new SelectList(await _dropdownService.GetFinancialYearDropdown(), "Code", "Name");

            model.EmployeeHeader.EmployeeGuid = EmployeeGI;
            model.EmployeeHeader.EmployeeName = EmpNameWithoutId;
            model.DocumentUpload.AadharIssueDate =model.DocumentUpload.AadharIssueDate == DateTime.Parse("0001-01-01") ? null : model.DocumentUpload.AadharIssueDate;
            model.DocumentUpload.DrivingLicenseValidFrom = model.DocumentUpload.DrivingLicenseValidFrom == DateTime.Parse("0001-01-01") ? null : model.DocumentUpload.DrivingLicenseValidFrom;
            model.DocumentUpload.DrivingLicenseValidTo = model.DocumentUpload.DrivingLicenseValidTo == DateTime.Parse("0001-01-01") ? null : model.DocumentUpload.DrivingLicenseValidTo;
            model.DocumentUpload.PANIssueDate = model.DocumentUpload.PANIssueDate == DateTime.Parse("0001-01-01") ? null : model.DocumentUpload.PANIssueDate;
            model.DocumentUpload.PassportValidFrom = model.DocumentUpload.PassportValidFrom == DateTime.Parse("0001-01-01") ? null : model.DocumentUpload.PassportValidFrom;
            model.DocumentUpload.PassportValidTo = model.DocumentUpload.PassportValidTo == DateTime.Parse("0001-01-01") ? null : model.DocumentUpload.PassportValidTo;
            model.DocumentUpload.PANIssueDate = model.DocumentUpload.PANIssueDate == DateTime.Parse("0001-01-01") ? null : model.DocumentUpload.PANIssueDate;
            model.Statutory_Payment_Details.ESIEnrollDate = model.Statutory_Payment_Details.ESIEnrollDate == DateTime.Parse("0001-01-01") ? null : model.Statutory_Payment_Details.ESIEnrollDate;
            model.Statutory_Payment_Details.PFEnrollDate = model.Statutory_Payment_Details.PFEnrollDate == DateTime.Parse("0001-01-01") ? null : model.Statutory_Payment_Details.PFEnrollDate;
            //  model.Job_Details.RenewalDate = model.Job_Details.RenewalDate == DateTime.Parse("0001-01-01") ? null : model.Job_Details.RenewalDate;

            for (int i = 0; i < model.AssetRequest.Count; i++)
            {
                model.AssetRequest[i].AssetReturnDate = model.AssetRequest[i].AssetReturnDate == DateTime.Parse("0001-01-01") ? null : model.AssetRequest[i].AssetReturnDate;
                model.AssetRequest[i].AssetIssueDate = model.AssetRequest[i].AssetIssueDate == DateTime.Parse("0001-01-01") ? null : model.AssetRequest[i].AssetIssueDate;
                model.AssetRequest[i].AssetDueDate = model.AssetRequest[i].AssetDueDate == DateTime.Parse("0001-01-01") ? null : model.AssetRequest[i].AssetDueDate;
                model.AssetRequest[i].Asset.DateOfPurchase = model.AssetRequest[i].Asset.DateOfPurchase == DateTime.Parse("0001-01-01") ? null : model.AssetRequest[i].Asset.DateOfPurchase;


            }
            for (int i=0;i<model.TrainingLog.Count; i++)
            {
                model.TrainingLog[i].TrainingRequest.Duration = decimal.Round(model.TrainingLog[i].TrainingRequest.Duration,0, MidpointRounding.AwayFromZero);
            }
            for (int i=0;i<model.AppraisalLog.Count;i++)
            {
                model.AppraisalLog[i].AppraisalRequest.AppraiserScore = (model.AppraisalLog[i].AppraisalRequest.AppraiserScore).Replace(".0", "");
                model.AppraisalLog[i].AppraisalRequest.ReviewerScore =(model.AppraisalLog[i].AppraisalRequest.ReviewerScore).Replace(".0", "");
                model.AppraisalLog[i].AppraisalRequest.SelfScore =(model.AppraisalLog[i].AppraisalRequest.SelfScore).Replace(".0", "");


            }
            for (int i = 0; i < model.FamilyDetails.Count; i++)
            {
                model.FamilyDetails[i].DateOfBirth = model.FamilyDetails[i].DateOfBirth == DateTime.Parse("0001-01-01") ? null : model.FamilyDetails[i].DateOfBirth;

            }
            model.Compensation.EffectFrom = model.Compensation.EffectFrom == DateTime.Parse("0001-01-01") ? null : model.Compensation.EffectFrom;

            model.Breadcrumb = Breadcrumb;
            model.BreadcrumbName = BreadcrumbName;
            model.CompensationStructureReport = new CompensationStructureReport();
                

            model.CompensationStructureReport = await _employeeProfileService.GetCompensationDetails(EmployeeGI);


                model.FinancialYear = new FinancialYear();
                model.FinancialYear.Code = CurrentFinancialYearCode;

            return View("~/Areas/DataBank/Views/EmployeeProfile/EmployeeProfile.cshtml", model);
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
        public async Task<IActionResult> GetCertificationsList(Guid EmployeeGI, int TabIndex)
        {
            try
            {
                EmployeeProfile model = new EmployeeProfile();
                model.Certifications = await _employeeProfileService.GetCertificationsList(EmployeeGI,TabIndex);
                return PartialView("_CertificationsList", model);
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
        public async Task<IActionResult> GetEmployeeSalAttDetails(Guid EmployeeGI, int FinancialYear,string Tab)
        {
            try { 
            EmployeeProfile model = new EmployeeProfile();
            EmployeeProfile m = new EmployeeProfile();
            m.EmployeeHeader = await _commonService.GetEmployeeHeader(EmployeeGI);

            ViewBag.FinancialYear = new SelectList(await _dropdownService.GetFinancialYearDropdown(), "Code", "Name");
            model = await _employeeProfileService.GetEmployeeProfileDetails(EmployeeGI, FinancialYear);
            model.EmployeeHeader = m.EmployeeHeader;
            model.EmployeeHeader.EmployeeGuid = EmployeeGI;
                if(Tab== "Attendance")
                    return PartialView("~/Areas/DataBank/Views/EmployeeProfile/_Attendance.cshtml", model);
                else
                    return PartialView("~/Areas/DataBank/Views/EmployeeProfile/_Salary.cshtml", model);
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
        public async Task<IActionResult> ExportToExcel(int TabIndex, string TabName,int TotalRows)
        {
            try { 
        
            if (Global.GlobalFilter == null)
                Global.GlobalFilter = new Filter();
                else
                {
                    Global.GlobalFilter.length = TotalRows;
                    Global.GlobalFilter.start = 0;
                }
                string FilterValues = await _helpers.GetFilterValues(Global.GlobalFilter);

               var LoginUser = HttpContext.Session.GetString("LoginUser");
               var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Employee Profile (" + TabName + ")", "Excel");
              
            if (FilterValues == "")
            {
                FilterValues = "All";
            }

            string currDateTime = await _helpers.GetCurrentDate();
            

                //Get Datset
                var dataset = await _employeeProfileService.GetEmployeeProfileList_Excel(TabIndex,Global.GlobalFilter);
                DataTable dt = dataset.Tables[0];
                dataset.Tables[0].Rows.RemoveAt(0);
                if ((dataset.Tables[0]?.Rows?.Count ?? 0) > 0)
                {
                    dt = dataset.Tables[0];
                }


                //set EXCEL Column Heading 
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (dt.Columns[i].ColumnName == "doj" || dt.Columns[i].ColumnName == "dob" || dt.Columns[i].ColumnName == "y/s" || dt.Columns[i].ColumnName == "ctc")
                        dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.ToUpper();
                    else
                        dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
                }

                using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("EmployeeProfileList("+TabName+")");
                int count =dt.Rows.Count+ 5;

                    string columnName = "";
                    int Columncount = dt.Columns.Count;

                    while (Columncount > 0)
                    {
                        int modulo = (Columncount - 1) % 26;
                        columnName = Convert.ToChar('A' + modulo) + columnName;
                        Columncount = (Columncount - modulo) / 26;
                    }

                    string colName = columnName;





                    worksheet.Range("A1" ,colName+ count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1", colName + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1", colName + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1", colName + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1", colName + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, colName));
                wsReportNameHeaderRange.Style.Font.Bold = true;
                wsReportNameHeaderRange.Style.Font.FontSize = 15;
                wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                wsReportNameHeaderRange.Merge();
                wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191); ;
                wsReportNameHeaderRange.Value = "Employee Profile (" + TabName + ")";

                worksheet.Cell(2, 1).Style.Font.Bold = true;
                worksheet.Cell(2, 1).Value = "Group Company Name";
                worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
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
                worksheet.Range("B4",colName + 4).Merge();
                worksheet.Cell("B4").Value = FilterValues;



                var currentRow = 5;

                    worksheet.Cell(currentRow, 1).InsertTable(dt);            
                    worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                    worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                    worksheet.Range("A5", colName + 5).Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Range("A5", colName + 5).Style.Font.Bold = true;
                    worksheet.Range("I5:I" + (dt.Rows.Count + 5)).Style.DateFormat.Format = "dd-MMM-yyyy";
                    worksheet.Range("K5:K" + (dt.Rows.Count + 5)).Style.DateFormat.Format = "dd-MMM-yyyy";
                    worksheet.Range("L5:L" + (dt.Rows.Count + 5)).Style.DateFormat.Format = "dd-MMM-yyyy";
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
                        "Employee profile " + TabName + " List.xlsx"
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

                System.Diagnostics.Debug.WriteLine(e.ToString());
                throw;
                //return RedirectToAction("Error", "Account", new { Area = "" });
            }

        }

        public async Task<IActionResult> SummaryExportToExcel(int groupbyid,string groupbyname)
        {
            try
            {
                EmployeeProfile model = new EmployeeProfile();

                model = await _employeeProfileService.GetEmployeeSummary(groupbyid, Global.GlobalFilter);
                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                var TotalCount = model.EmployeeProfileSummary.Sum(x => x.Count);
                var TotalCTC = model.EmployeeProfileSummary.Sum(x => x.CTC);
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Employee Profile Summary (" + groupbyname + ")", "Excel");
                int slNo = 0;

                string currDateTime = await _helpers.GetCurrentDate();
                
                if (User.authUser.SalaryVisibility != 0)
                {


                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("Employee Profile Summary List");
                        int count = model.EmployeeProfileSummary.Count + 4;


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
                        wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191); ;
                        wsReportNameHeaderRange.Value = "Employee Profile Summary";

                        worksheet.Cell(2, 1).Style.Font.Bold = true;
                        worksheet.Cell(2, 1).Value = "Group Company Name";
                        worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                        worksheet.Cell(2, 3).Style.Font.Bold = true;
                        worksheet.Cell(2, 3).Value = "Group By";
                        worksheet.Cell(2, 4).Value = groupbyname;



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
                        worksheet.Cell(3, 6).Value = model.EmployeeProfileSummary.Count;




                        var currentRow = 4;
                        #region Header
                        worksheet.Range("A4:F4").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                        worksheet.Cell(currentRow, 1).Value = "SlNo";
                        worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 2).Value = groupbyname;
                        worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 3).Value = "Count";
                        worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 4).Value = "Count %";
                        worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 5).Value = "CTC";
                        worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 6).Value = "CTC %";
                        worksheet.Cell(currentRow, 6).Style.Font.Bold = true;

                        #endregion

                        #region Body
                        foreach (var item in model.EmployeeProfileSummary)
                        {
                            currentRow++;
                            slNo = slNo + 1;
                            worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 1).Value = slNo;
                            worksheet.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 2).Value = item.Name;
                            worksheet.Cell(currentRow, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 3).Value = item.Count;
                            worksheet.Cell(currentRow, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 4).Value = item.Count_Perc;
                            worksheet.Cell(currentRow, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 5).Value = item.CTC;
                            worksheet.Cell(currentRow, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 6).Value = item.CTC_Perc;


                            worksheet.Columns(1, 6).Style.Alignment.WrapText = true;
                            worksheet.Rows().Style.Alignment.WrapText = true;
                        }
                        #endregion
                        var wsReportNameFooterRange = worksheet.Range(string.Format("A{0}:{1}{0}", currentRow + 1, Char.ConvertFromUtf32(65 + 5)));
                        wsReportNameFooterRange.Style.Font.Bold = true;
                        wsReportNameFooterRange.Style.Font.FontSize = 15;
                        wsReportNameFooterRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                        wsReportNameFooterRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        wsReportNameFooterRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191); ;
                        worksheet.Cell(currentRow + 1, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow + 1, 3).Value = TotalCount;
                        worksheet.Cell(currentRow + 1, 5).Value = TotalCTC;
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
                                "Employee profile Summary List.xlsx"
                                );
                        }
                    }
                }
                else
                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("Employee Profile Summary List");
                        int count = model.EmployeeProfileSummary.Count + 4;


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
                        wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191); ;
                        wsReportNameHeaderRange.Value = "Employee Profile Summary";

                        worksheet.Cell(2, 1).Style.Font.Bold = true;
                        worksheet.Cell(2, 1).Value = "Group Company Name";
                        worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                        worksheet.Cell(2, 3).Style.Font.Bold = true;
                        worksheet.Cell(2, 3).Value = "Group By";
                        worksheet.Cell(2, 4).Value = groupbyname;



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
                        worksheet.Cell(3, 6).Value = model.EmployeeProfileSummary.Count;




                        var currentRow = 4;
                        #region Header
                        worksheet.Range("A4:F4").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                        worksheet.Cell(currentRow, 1).Value = "SlNo";
                        worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 2).Value = groupbyname;
                        worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 3).Value = "Count";
                        worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 4).Value = "Count %";
                        worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                       

                        #endregion

                        #region Body
                        foreach (var item in model.EmployeeProfileSummary)
                        {
                            currentRow++;
                            slNo = slNo + 1;
                            worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 1).Value = slNo;
                            worksheet.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 2).Value = item.Name;
                            worksheet.Cell(currentRow, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 3).Value = item.Count;
                            worksheet.Cell(currentRow, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 4).Value = item.Count_Perc;
    


                            worksheet.Columns(1, 6).Style.Alignment.WrapText = true;
                            worksheet.Rows().Style.Alignment.WrapText = true;
                        }
                        #endregion
                        var wsReportNameFooterRange = worksheet.Range(string.Format("A{0}:{1}{0}", currentRow + 1, Char.ConvertFromUtf32(65 + 5)));
                        wsReportNameFooterRange.Style.Font.Bold = true;
                        wsReportNameFooterRange.Style.Font.FontSize = 15;
                        wsReportNameFooterRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                        wsReportNameFooterRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        wsReportNameFooterRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191); ;
                        worksheet.Cell(currentRow + 1, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow + 1, 3).Value = TotalCount;
  
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
                                "Employee profile Summary List.xlsx"
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
        public async Task<IActionResult> GetEmpProfilePrint(int EmployeeCode)
        {
            EmployeeProfile model = new EmployeeProfile();

            var FinancialYear = 0;


            var response = await _commonService.GetCurrentLFinancialYear();

            FinancialYear = Convert.ToInt32(response.Data);
            model.EmployeeHeader = new EmployeeHeader();
            model.EmployeeHeader.EmployeeCode = EmployeeCode;

            model.Url = await _commonService.GetUrl();
            model.FinancialYear = new FinancialYear();
            model.FinancialYear.Code = FinancialYear;

            return View("~/Areas/DataBank/Views/EmployeeProfile/EmployeeProfilePrint.cshtml", model);
        }
        public async Task<IActionResult> GetPopUpforCertiDocView(int LogCode)
        {
            try
            {

                EmployeeProfile model = new EmployeeProfile();
                model.DocumentList = await _employeeProfileService.GetCertificationDocumentList( LogCode);
                return View("_CertificateUploadPopUp", model);
            }

            catch (Exception ex)
            {
                string ErrorMessage = ex.Message.ToString();
                if (ex.InnerException != null)
                {
                    if (ex.InnerException.Message != null)
                    {

                        ErrorMessage = ErrorMessage + ex.InnerException.Message.ToString();
                    }
                }
                ViewData["Error"] = ErrorMessage;
                return View("ShowError");
            }
        }
        [HttpGet]
        public async Task<IActionResult> EmpProfileDocView(int Code,string DocType)
        {
            try
            {
                var fileData = await _employeeProfileService.EmpProfileDocView(Code, DocType);
                if (fileData == null || fileData.fdata == null || fileData.fdata.Length == 0)
                    return BadRequest(new { message = "No File Uploaded" });
                var fileBytes = await _helpers.DecompressIfNeeded(fileData.fdata);
                var provider = new FileExtensionContentTypeProvider();
                if (!provider.TryGetContentType(fileData.FileName, out string contentType))
                {
                    // default if unknown
                    contentType = "application/octet-stream";
                }
                Response.Headers["Content-Disposition"] = $"inline; filename=\"{fileData.FileName}\"";
                return File(fileBytes, contentType);
            }
            catch (Exception ex)
            {
                // Log exception here if needed
                return StatusCode(500, "Error while downloading file.");
            }
        }


    }
}
