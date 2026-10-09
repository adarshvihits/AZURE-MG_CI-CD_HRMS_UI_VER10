using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.DataBank.Reports;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Spire.Xls;



namespace Hrms.Lite.UI.Areas.DataBank.Controllers
{

    [Area("DataBank")]
    [SessionAuthorize]
    public class KendoEmployeeProfileController : Controller
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
        public KendoEmployeeProfileController(IEmployeeProfileService employeeProfileService, ICommonService commonService, IBlob blob, IDropdownService dropdownService, IHelpers Helpers)
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
            try
            {
                EmployeeProfile model = new EmployeeProfile();
                EmployeeProfile m = new EmployeeProfile();
                model.EmployeeHeader = new EmployeeHeader();
                Filter filterdata = new Filter();
                //  model = await _employeeProfileService.GetEmployeeProfileList(0,filterdata);
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
            ViewBag.DateType = new SelectList(await _dropdownService.GetDateFilter(), "Code", "Name");
            ViewBag.Operators = new SelectList(await _dropdownService.GetOperators(), "Code", "Name");

        }

        public async Task<IActionResult> GetTabCount()
        {
            EmployeeProfile model = new EmployeeProfile();
            model.ActiveCount = Global.ActiveCount;
            model.ResignedCount = Global.ResignedCount;
            return Json(model);
        }


        DefaultContractResolver contractResolver = new DefaultContractResolver
        {
            NamingStrategy = new CamelCaseNamingStrategy
            {
                OverrideSpecifiedNames = true
            }
        };

        [HttpPost]
        public async Task<IActionResult> GetEmployeeList([DataSourceRequest] DataSourceRequest request, Filter filter1)
        {

            if (filter1.searchValue != null)
            {
                filter1.start = 0;
                filter1.length = request.PageSize;
            }
            else
            {

                filter1.start = (request.Page - 1) * request.PageSize;
                filter1.length = request.PageSize;
            }





            EmployeeProfile model = new EmployeeProfile();
            List<EmployeeProfile_List> modellist = new List<EmployeeProfile_List>();

            model = await _employeeProfileService.GetEmployeeProfileList(0, filter1);

            modellist = model.EmployeeProfileList;

            request.PageSize = 0;


            var dataResult1 = modellist.ToDataSourceResult(request);
            ;
            //   var dataResult = model.EmployeeProfileList.ToDataSourceResult(request);

            // dataResult.Total = model.ActiveCount;
            dataResult1.Total = model.ActiveCount;

        


            return Json(dataResult1, new JsonSerializerSettings { ContractResolver = new DefaultContractResolver() });

        }

        [HttpPost]
        public ActionResult Excel_Export_Save(string contentType, string base64, string fileName)
        {
            var fileContents = Convert.FromBase64String(base64);

            return File(fileContents, contentType, fileName);
        }

        public async Task<IActionResult> ApplyFilter(int TabIndex, Filter filter)
        {
            try
            {
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
            try
            {
                EmployeeProfile model = new EmployeeProfile();

                if (Global.GlobalFilter == null)
                    Global.GlobalFilter = new Filter();
                var GroupByLIst = await _dropdownService.GetEmployeeProfileSummaryGroupByDropDown();
                ViewBag.Gpy = new SelectList(GroupByLIst, "Code", "Name");
                int firstgroupById = GroupByLIst[0].Code;
                model = await _employeeProfileService.GetEmployeeSummary(firstgroupById, Global.GlobalFilter);
                ViewBag.TotalCount = model.EmployeeProfileSummary.Sum(x => x.Count);
                ViewBag.TotalCTC = model.EmployeeProfileSummary.Sum(x => x.CTC);
                model.groupbyname = GroupByLIst[0].Name;
                return PartialView("_Summary", model);
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
        public async Task<IActionResult> GroupByBasedSummary(int id, string groupbyname)
        {
            try
            {
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

        //[HttpPost]
        //public async Task<IActionResult> GetEmployeeProfileList(int TabIndex)
        //{
        //    EmployeeProfile model = new EmployeeProfile();
        //    Filter filterdata = new Filter();
        //    model = await _employeeProfileService.GetEmployeeProfileList(TabIndex,filterdata);
        //    if (TabIndex == 0)
        //    {
        //        return PartialView("_ActiveList", model);

        //    }
        //    else
        //    {
        //        return PartialView("_ResignedList", model);

        //    }


        //}
        public async Task<IActionResult> GetEmployeeProfileDetails(Guid EmployeeGI, int FinancialYear, string Breadcrumb, string BreadcrumbName)
        {
            try
            {
                EmployeeProfile model = new EmployeeProfile();
                EmployeeProfile m = new EmployeeProfile();

                m.EmployeeHeader = await _commonService.GetEmployeeHeader(EmployeeGI);

                var EmpName = m.EmployeeHeader.EmployeeName;
                var EmpNameWithoutId = EmpName.Split('(')[0].Trim();

                if (FinancialYear == 0)
                {
                    var CurrentFinancialYearCode = 0;

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

                ViewBag.FinancialYear = new SelectList(await _dropdownService.GetFinancialYearDropdown(), "Code", "Name");
                //model = await _employeeProfileService.GetEmployeeProfileDetails(EmployeeGI, FinancialYear);

                //model = await _employeeProfileService.GetEmployeeProfileDetails(EmployeeGI, model.FinancialYear.Code);

                model.EmployeeHeader = m.EmployeeHeader;
                model.EmployeeHeader.EmployeeGuid = EmployeeGI;
                model.EmployeeHeader.EmployeeName = EmpNameWithoutId;
                model.DocumentUpload.AadharIssueDate = model.DocumentUpload.AadharIssueDate == DateTime.Parse("0001-01-01") ? null : model.DocumentUpload.AadharIssueDate;
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
                for (int i = 0; i < model.TrainingLog.Count; i++)
                {
                    model.TrainingLog[i].TrainingRequest.Duration = decimal.Round(model.TrainingLog[i].TrainingRequest.Duration, 0, MidpointRounding.AwayFromZero);
                }
                for (int i = 0; i < model.AppraisalLog.Count; i++)
                {
                    model.AppraisalLog[i].AppraisalRequest.AppraiserScore = model.AppraisalLog[i].AppraisalRequest.AppraiserScore.Replace(".0", "");
                    model.AppraisalLog[i].AppraisalRequest.ReviewerScore = model.AppraisalLog[i].AppraisalRequest.ReviewerScore.Replace(".0", "");
                    model.AppraisalLog[i].AppraisalRequest.SelfScore = model.AppraisalLog[i].AppraisalRequest.SelfScore.Replace(".0", "");


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

        public async Task<IActionResult> GetEmployeeSalAttDetails(Guid EmployeeGI, int FinancialYear)
        {
            try
            {
                EmployeeProfile model = new EmployeeProfile();
                EmployeeProfile m = new EmployeeProfile();
                m.EmployeeHeader = await _commonService.GetEmployeeHeader(EmployeeGI);

                ViewBag.FinancialYear = new SelectList(await _dropdownService.GetFinancialYearDropdown(), "Code", "Name");
                model = await _employeeProfileService.GetEmployeeProfileDetails(EmployeeGI, FinancialYear);
                model.EmployeeHeader = m.EmployeeHeader;
                model.EmployeeHeader.EmployeeGuid = EmployeeGI;
                return PartialView("~/Areas/DataBank/Views/EmployeeProfile/_Att&Sal.cshtml", model);
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



        //[HttpPost]
        //public async Task<FileStreamResult> ExportServer(string col, string data)
        //{
        //    if (Global.GlobalFilter == null)
        //        Global.GlobalFilter = new Filter();
        //    else
        //    {
        //        Global.GlobalFilter.length = 10000;
        //        Global.GlobalFilter.start = 0;
        //    }

        //    EmployeeProfile model = new EmployeeProfile();
        //    model = await _employeeProfileService.GetEmployeeProfileList(0, Global.GlobalFilter);

        //    var columnsData = JsonConvert.DeserializeObject<IList<ExportColumnSettings>>(HttpUtility.UrlDecode(col));
        //    dynamic options = JsonConvert.DeserializeObject(HttpUtility.UrlDecode(data));
        //    SpreadDocumentFormat exportFormat = options.format.ToString() == "csv" ? exportFormat = SpreadDocumentFormat.Csv : exportFormat = SpreadDocumentFormat.Xlsx;
        //    Action<ExportCellStyle> cellStyle = new Action<ExportCellStyle>(ChangeCellStyle);
        //    Action<ExportRowStyle> rowStyle = new Action<ExportRowStyle>(ChangeRowStyle);
        //    Action<ExportColumnStyle> columnStyle = new Action<ExportColumnStyle>(ChangeColumnStyle);

        //    string fileName = string.Format("{0}.{1}", options.title, options.format);
        //    string mimeType = Kendo.Mvc.Export.Helpers.GetMimeType(exportFormat);



        //    string FilterValues = _helpers.GetFilterValues(Global.GlobalFilter);
        //    Stream exportStream = model.EmployeeProfileList.ToXlsxStream(columnsData, (string)options.title.ToString(), cellStyleAction: cellStyle, rowStyleAction: rowStyle, columnStyleAction: columnStyle);





        //    Stream exportStream = exportFormat == SpreadDocumentFormat.Xlsx ?
        //         productService.Read().ToXlsxStream(columnsData, (string)options.title.ToString(), cellStyleAction: cellStyle, rowStyleAction: rowStyle, columnStyleAction: columnStyle) :
        //         productService.Read().ToCsvStream(columnsData);





        //    var fileStreamResult = new FileStreamResult(exportStream, mimeType);
        //    fileStreamResult.FileDownloadName = fileName;
        //    fileStreamResult.FileStream.Seek(0, SeekOrigin.Begin);

        //    return fileStreamResult;
        //}

        //private void ChangeCellStyle(ExportCellStyle e)
        //{
        //    bool isHeader = e.Row == 0;
        //    SpreadCellFormat format = new SpreadCellFormat
        //    {
        //        ForeColor = isHeader ? SpreadThemableColor.FromRgb(50, 54, 58) : SpreadThemableColor.FromRgb(214, 214, 217),
        //        IsItalic = true,
        //        VerticalAlignment = SpreadVerticalAlignment.Center,
        //        WrapText = true,
        //        Fill = SpreadPatternFill.CreateSolidFill(isHeader ? new SpreadColor(93, 227, 0) : new SpreadColor(50, 54, 58))
        //    };
        //    e.Cell.SetFormat(format);
        //}

        //private void ChangeRowStyle(ExportRowStyle e)
        //{
        //    e.Row.SetHeightInPixels(e.Index == 0 ? 80 : 30);
        //}

        //private void ChangeColumnStyle(ExportColumnStyle e)
        //{
        //    double width = e.Name == "Product name" || e.Name == "Category Name" ? 250 : 100;
        //    e.Column.SetWidthInPixels(width);
        //}


        public async Task<IActionResult> ExportToExcel(int TabIndex, string TabName, int TotalRows)
        {
            try
            {
                EmployeeProfile model = new EmployeeProfile();
                if (Global.GlobalFilter == null)
                    Global.GlobalFilter = new Filter();
                else
                {
                    Global.GlobalFilter.length = TotalRows;
                    Global.GlobalFilter.start = 0;
                }
                string FilterValues = await _helpers.GetFilterValues(Global.GlobalFilter);
                model = await _employeeProfileService.GetEmployeeProfileList(TabIndex, Global.GlobalFilter);
                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Employee Profile (" + TabName + ")", "Excel");
                int slNo = 0;
                if (FilterValues == "")
                {
                    FilterValues = "All";
                }

                string currDateTime = await _helpers.GetCurrentDate();

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("EmployeeProfileList(" + TabName + ")");
                    int count = model.EmployeeProfileList.Count + 5;


                    worksheet.Range("A1:K" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:K" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:K" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:K" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:K" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, char.ConvertFromUtf32(65 + 10)));
                    wsReportNameHeaderRange.Style.Font.Bold = true;
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Merge();
                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191); ;
                    wsReportNameHeaderRange.Value = "Employee Profile (" + TabName + ")";

                    worksheet.Cell(2, 1).Style.Font.Bold = true;
                    worksheet.Cell(2, 1).Value = "Company Name";
                    worksheet.Cell(2, 2).Value = CompanyName.ToUpper();
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
                    worksheet.Cell(3, 6).Value = model.EmployeeProfileList.Count;
                    worksheet.Cell(4, 1).Style.Font.Bold = true;
                    worksheet.Cell(4, 1).Value = "Filter Applied";
                    worksheet.Range("B4:K4").Merge();
                    worksheet.Cell("B4").Value = FilterValues;



                    var currentRow = 5;
                    #region Header
                    worksheet.Range("A5:K5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Cell(currentRow, 1).Value = "Sl.No";
                    worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 2).Value = "ID";
                    worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 3).Value = "EmployeeName";
                    worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 4).Value = "Designation";
                    worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 5).Value = "Department";
                    worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 6).Value = "Location";
                    worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 7).Value = "Category";
                    worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 8).Value = "DOB";
                    worksheet.Cell(currentRow, 8).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 9).Value = "DOJ";
                    worksheet.Cell(currentRow, 9).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 10).Value = "Y/S";
                    worksheet.Cell(currentRow, 10).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 11).Value = "GrossPay";
                    worksheet.Cell(currentRow, 11).Style.Font.Bold = true;

                    #endregion

                    #region Body
                    foreach (var item in model.EmployeeProfileList)
                    {
                        currentRow++;
                        slNo = slNo + 1;
                        worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 1).Value = slNo;
                        worksheet.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 2).Value = item.Employeeid;
                        worksheet.Cell(currentRow, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 3).Value = item.EmployeeName;
                        worksheet.Cell(currentRow, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 4).Value = item.Designation;
                        worksheet.Cell(currentRow, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 5).Value = item.Department;
                        worksheet.Cell(currentRow, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 6).Value = item.Location;
                        worksheet.Cell(currentRow, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 7).Value = item.Category;
                        worksheet.Cell(currentRow, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 8).Style.DateFormat.Format = "dd-MMM-yyyy";
                        worksheet.Cell(currentRow, 8).Value = item.DOB;
                        worksheet.Cell(currentRow, 9).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 9).Style.DateFormat.Format = "dd-MMM-yyyy";
                        worksheet.Cell(currentRow, 9).Value = item.DOJ;
                        worksheet.Cell(currentRow, 10).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 10).Value = item.YOS;
                        worksheet.Cell(currentRow, 11).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 11).Value = item.GrossPay;


                        worksheet.Columns(1, 11).Style.Alignment.WrapText = true;
                        worksheet.Rows().Style.Alignment.WrapText = true;
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
                return RedirectToAction("Error", "Account", new { Area = "" });
            }

        }

        public async Task<IActionResult> SummaryExportToExcel(int groupbyid, string groupbyname)
        {
            try
            {
                EmployeeProfile model = new EmployeeProfile();

                model = await _employeeProfileService.GetEmployeeSummary(groupbyid, Global.GlobalFilter);
                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var TotalCount = model.EmployeeProfileSummary.Sum(x => x.Count);
                var TotalCTC = model.EmployeeProfileSummary.Sum(x => x.CTC);
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Employee Profile Summary (" + groupbyname + ")", "Excel");
                int slNo = 0;

                string currDateTime = await _helpers.GetCurrentDate();

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Employee Profile Summary List");
                    int count = model.EmployeeProfileSummary.Count + 4;


                    worksheet.Range("A1:F" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:F" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:F" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:F" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:F" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, char.ConvertFromUtf32(65 + 5)));
                    wsReportNameHeaderRange.Style.Font.Bold = true;
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Merge();
                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191); ;
                    wsReportNameHeaderRange.Value = "Employee Profile Summary";

                    worksheet.Cell(2, 1).Style.Font.Bold = true;
                    worksheet.Cell(2, 1).Value = "Company Name";
                    worksheet.Cell(2, 2).Value = CompanyName.ToUpper();
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
                    var wsReportNameFooterRange = worksheet.Range(string.Format("A{0}:{1}{0}", currentRow + 1, char.ConvertFromUtf32(65 + 5)));
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
                            book.Protect(EP.Password,false,false);
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

    }


}
