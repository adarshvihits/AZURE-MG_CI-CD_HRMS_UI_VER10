using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Infrastructure.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Spire.Xls;
using Hrms.Lite.Shared.Account;

namespace Hrms.Lite.UI.Areas.DataBank.Controllers
{
    [Area("DataBank")]
    [SessionAuthorize]
    public class AssetAllocationController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly IAssetAllocationService _assetAllocationService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        
        public AssetAllocationController(IAssetAllocationService assetAllocationService,IDropdownService dropdownService,ICommonService commonService,IHelpers helpers)
        {
            _assetAllocationService = assetAllocationService;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = helpers;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
            AssetAllocation model = new AssetAllocation();
            AssetAllocation m = new AssetAllocation();
                Filter filter = new Filter();
            model.AssetList = new List<Asset>();
            model.AssetRequest = new AssetRequest();
            model.AssetRequest.Asset = new Asset();
            model.AssetRequest.Asset.Condition = new Condition();
            model.AssetRequest.Asset.IssuingDepartment = new Shared.Master.Department();
            model.AssetRequest.Asset.AssetCategory = new Shared.Master.Category();
            model.EmployeeHeader = new Shared.General.EmployeeHeader();
            model.EmployeeHeader.Department = new Shared.Master.Department();
            model.EmployeeHeader.Designation = new Shared.Master.Designation();
            model.EmployeeHeader.Location = new Shared.Master.Location();
            model.EmployeeHeader.Category = new Shared.Master.Category();

            model.AssetAllocationTabs = (List<AssetAllocationTabs>)await _assetAllocationService.GetAssetAllocationTabs();
            model.Tabindex = 0;
            model.SearchId = 1;
            model.AssetAllocationFilter = new AssetAllocationFilterCombo();
            model.AssetAllocationFilter.Code = 1;               
           
            m = await _assetAllocationService.GetAssetAllocationList(model.Tabindex, model.SearchId, 0,filter);
            model.AssetAllocationList = m.AssetAllocationList;
            model.AllTabCount = m.AllTabCount;
            model.Tab1Count = m.Tab1Count;
            model.Tab2Count = m.Tab2Count;
            model.Tab3Count = m.Tab3Count;
            model.MoreTabCount = m.MoreTabCount;
            model.PendingTabCount = m.PendingTabCount;
            model.EmpReqTabCount = m.EmpReqTabCount;
            ViewBag.AssetAllocationFilterCom = new SelectList(await _dropdownService.GetAssetAllocationFilterDropDown(), "Code", "Name");
            ViewBag.ReturnCondition = new SelectList(await _dropdownService.GetAssetReturnConditionDropDown(), "Code", "Name");
            ViewBag.Emp = new SelectList(await _dropdownService.GetEmployeeDropDown(), "Code", "Name");            
            ViewBag.Tabs = new SelectList(model.AssetAllocationTabs,"Code","Name");
                ViewBag.AssetCat = new SelectList(await _dropdownService.GetAssetCategoryDropDown(), "Code", "Name");
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

       

        public async Task<ActionResult> EmployeeCard(int EmpCode)
        {
            try
            {
            AssetAllocation model = new AssetAllocation();

            //model = await _assetAllocationService.GetEmployeeCard(EmpCode);
            model.EmployeeHeader = await _commonService.GetEmployeeCard(EmpCode);

            return PartialView("_EmployeeCard", model.EmployeeHeader);
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

       
        public async Task<IActionResult> ApplyFilter(int TabIndex,int SearchId,Filter filter,int AssetCategoryCode)
        {
            AssetAllocation model = new AssetAllocation();
           model.Filter = filter;
            model = await _assetAllocationService.GetAssetAllocationList(TabIndex, SearchId, AssetCategoryCode, filter);
            model.Tabindex = TabIndex;
            //return PartialView("~/Areas/DataBank/Views/AssetAllocation/_Filter.cshtml");
            if (TabIndex == 1)
            {
                return View("_PendingRequests", model);
            }
            else if (SearchId == 2)
            {
                return View("_AssetInHand", model);
            }
            else if (SearchId == 3)
            {
                return View("_AssetDueList", model);
            }
            else if (SearchId == 4)
            {
                return View("_AssetIssueHistory", model);
            }
            else if (SearchId == 5)
            {
                return View("_AssetDamaged", model);
            }
            else if(TabIndex == 0)
            {
                return View("_AssetAllocationList", model);
            }
            else
            {
                return View("_EmployeeAssetReqList", model);
            }
            
        }
        
        public IActionResult NewRequest()
        {
            return PartialView("_NewRequest.cshtml");
        }
        public async Task<IActionResult> Create(Guid EmployeeGI)
        {
            try
            {
            AssetAllocation model = new AssetAllocation();

            model.EmployeeHeader = await _commonService.GetEmployeeHeader(EmployeeGI);
            model.EmployeeGI = EmployeeGI;
            model.AssetRequest = new AssetRequest();
            model.AssetRequest.AssetLog = new List<AssetLog>();
            //List<AssetLog> AL = new List<AssetLog>();           
            //AL = (List<AssetLog>)await _assetAllocationService.GetAssetLogList(EmployeeGI);
            model.AssetRequest.AssetLog = (List<AssetLog>)await _assetAllocationService.GetAssetLogList(EmployeeGI);
            ViewBag.AssetCat = new SelectList(await _dropdownService.GetAssetCategoryDropDown(), "Code", "Name");
            ViewBag.Asset = new SelectList(await _dropdownService.GetAssetDropDown(0), "Code", "Name");
            model.AssetRequest.Asset = new Asset();
            model.AssetRequest.Asset.Condition = new Condition();
            model.AssetRequest.Asset.IssuingDepartment = new Shared.Master.Department();
            model.AssetRequest.Asset.AssetCategory = new Shared.Master.Category();
                //model.AssetRequest.Asset.DateOfPurchase = model.AssetRequest.Asset.DateOfPurchase == DateTime.Parse("1/1/0001 12:00:00 AM") ? DateTime.Parse("") : model.AssetRequest.Asset.DateOfPurchase;
            return View("~/Areas/DataBank/Views/AssetAllocation/Create.cshtml", model);
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
        public async Task<IActionResult> AssetLog(string AssetAllocationGI, Guid EmployeeGI)
        {
            try
            {
            AssetAllocation model = new AssetAllocation();
            model = await _assetAllocationService.GetAssetLogDetails(Guid.Parse(AssetAllocationGI));            
            return PartialView("_AssetLog", model);
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
        public async Task<ActionResult> GetAssetDetails(int AssetCode)
        {
            try
            {
            AssetAllocation model = new AssetAllocation();
            model = await _assetAllocationService.GetAssetDetails(AssetCode);
            return View("_AssetDetails", model);
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
        public async Task<ActionResult> AssetAllocationDetails(Guid AssetAllocationGI,Guid EmployeeGI)
        {
            try
            {
            AssetAllocation model = new AssetAllocation();
            model = await _assetAllocationService.GetAssetAllocationDetails(AssetAllocationGI);
            model.EmployeeHeader = await _commonService.GetEmployeeHeader(EmployeeGI);            
            ViewBag.ReturnCondition = new SelectList(await _dropdownService.GetAssetReturnConditionDropDown(), "Code", "Name");
            //model.AssetRequest.AssetReturnDate = model.AssetRequest.AssetReturnDate == DateTime.Parse("01/Jan/0001 12:00:00 AM") ? null : model.AssetRequest.AssetReturnDate;
            model.AssetRequest.AssetReturnDate = DateTime.Now;
                model.AssetRequest.Deduction = model.AssetRequest.Deduction == 0 ? null : model.AssetRequest.Deduction;
            return PartialView("_AssetAllocationDetails", model);
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

        public async Task<ActionResult> EmployeeAssetReqDetails(Guid EmployeeGI,int AssetCategoryCode,int TrnCode)
        {
            try
            {
                AssetAllocation model = new AssetAllocation();
                model = await _assetAllocationService.GetEmpAssetReqDetails(EmployeeGI, TrnCode);
                model.EmployeeHeader = await _commonService.GetEmployeeHeader(EmployeeGI);
                ViewBag.Asset = new SelectList(await _dropdownService.GetAssetDropDown(AssetCategoryCode), "Code", "Name");
                return PartialView("_EmpAssetReqDetails", model);

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


        public async Task<ActionResult> AssetAllocationDelete(Guid AssetAllocationGI)
        {
            try
            { 
                AssetAllocation model = new AssetAllocation();
            model.AssetAllocationGI = AssetAllocationGI;
            var response = await _assetAllocationService.AssetAllocationDelete(AssetAllocationGI);
            if (response.Success == true)
            {
                TempData["Message"] = "Deleted Successfully";
                return RedirectToAction("Index", "AssetAllocation");
            }
            return PartialView("_AssetAllocationDetails", model);
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
        public async Task<ActionResult> Pending(int TabIndex)
        {
            try
            {
                Filter filter = new Filter();
                AssetAllocation model = new AssetAllocation();
            model.Tabindex = TabIndex;
            model.SearchId = 1;
            model = await _assetAllocationService.GetAssetAllocationList(model.Tabindex, model.SearchId, 0,filter);
            return PartialView("_PendingRequests", model);
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
        public async Task<ActionResult> AssetIssueHistoryDetails(Guid AssetAllocationGI, Guid EmployeeGI)
        {
            try
            { 
            AssetAllocation model = new AssetAllocation();
            model = await _assetAllocationService.GetAssetAllocationDetails(AssetAllocationGI);
            model.EmployeeHeader = await _commonService.GetEmployeeHeader(EmployeeGI);
            return PartialView("_AssetIssueHistoryDetails", model);
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
       
        public async Task<ActionResult> Issue(Guid AssetAllocationGI, Guid EmployeeGI)
        {
            try
            {

            AssetAllocation model = new AssetAllocation();
            model = await _assetAllocationService.GetIssueDetails(AssetAllocationGI);
            model.EmployeeHeader = await _commonService.GetEmployeeHeader(EmployeeGI);
            return View("_AssetAllocationIssue", model);
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
               
        public async Task<IActionResult> ExportToExcel(int TabIndex, int SearchId,string TabName, string SearchName,int AssetCategoryCode)
        {
            AssetAllocation model = new AssetAllocation();
            Filter filter = new Filter();
            int slNo = 0;
            
            string FilterValues = await _helpers.GetFilterValues(filter);
            model = await _assetAllocationService.GetAssetAllocationList(TabIndex, SearchId, AssetCategoryCode,filter);
            if (FilterValues == "")
            {
                FilterValues = "All";
            }
            string currDateTime = await _helpers.GetCurrentDate();
            
           
            var LoginUser = HttpContext.Session.GetString("LoginUser");
            var CompanyName = HttpContext.Session.GetString("CompanyName");
            var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
            var GroupCompanyName = User.authUser.GroupCompanyName;
            ExcelPassword EP = new ExcelPassword();
            EP = await _commonService.GetExcelPassword("Asset Register (" + TabName + ")", "Excel");
            if (TabIndex == -2) //Pending
            {
                
                    using (var workbook = new XLWorkbook())
                    {
                    var worksheet = workbook.Worksheets.Add("Pending List");
                    int count = model.AssetAllocationList.Count + 5;


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
                    wsReportNameHeaderRange.Value = "Pending Requests";

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
                    worksheet.Cell(3, 6).Value = model.AssetAllocationList.Count;
                    worksheet.Cell(4, 1).Style.Font.Bold = true;
                    worksheet.Cell(4, 1).Value = "Filter Applied";
                    worksheet.Range("B4:H4").Merge();
                    worksheet.Cell("B4").Value = FilterValues;
                    var currentRow = 5;
                        #region Header
                        worksheet.Cell(currentRow, 1).Value = "Slno.";
                        worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 2).Value = "ID";
                        worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 3).Value = "EmployeeName";
                        worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 4).Value = "Fun.Designation";
                        worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 5).Value = "Department";
                        worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 6).Value = "Location";
                        worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 7).Value = "Category";
                        worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 8).Value = "Request Date";
                        worksheet.Cell(currentRow, 8).Style.Font.Bold = true;

                        #endregion
                      
                        #region Body
                        foreach (var item in model.AssetAllocationList)
                        {
                            currentRow++;
                            slNo = slNo + 1;
                            DateTime RequestDte = (DateTime)item.IssuedDate;
                             worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                             worksheet.Range("A5:H5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                             worksheet.Cell(currentRow, 1).Value = slNo;
                             worksheet.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 2).Value = item.EmpId;                            
                            worksheet.Cell(currentRow, 3).Value = item.EmployeeName;                            
                            worksheet.Cell(currentRow, 4).Value = item.Designation.Name;
                            worksheet.Cell(currentRow, 5).Value = item.Department.Name;
                            worksheet.Cell(currentRow, 6).Value = item.Location.Name;
                            worksheet.Cell(currentRow, 7).Value = item.Category.Name;
                            worksheet.Cell(currentRow, 8).Value = RequestDte;
                            worksheet.Cell(currentRow, 8).Style.DateFormat.Format = "dd-MMM-yyyy";
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
                                book.Protect(EP.Password,false,false);
                                book.SaveToStream(stream);
                            }
                        var content = stream.ToArray();
                            return File(
                                content,
                                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                                "Pending requests List.xlsx"
                                );
                        }
                    }
                
            }

            else if (TabIndex == 2)
            {

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Employee RequestPending List");
                    int count = model.AssetAllocationList.Count + 5;


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
                    wsReportNameHeaderRange.Value = "Employee Pending Requests";

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
                    worksheet.Cell(3, 6).Value = model.AssetAllocationList.Count;
                    worksheet.Cell(4, 1).Style.Font.Bold = true;
                    worksheet.Cell(4, 1).Value = "Filter Applied";
                    worksheet.Range("B4:H4").Merge();
                    worksheet.Cell("B4").Value = FilterValues;
                    var currentRow = 5;
                    #region Header
                    worksheet.Cell(currentRow, 1).Value = "Slno.";
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
                    worksheet.Cell(currentRow, 8).Value = "Required Date";
                    worksheet.Cell(currentRow, 8).Style.Font.Bold = true;

                    #endregion

                    #region Body
                    foreach (var item in model.AssetAllocationList)
                    {
                        currentRow++;
                        slNo = slNo + 1;
                        
                        worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Range("A5:H5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                        worksheet.Cell(currentRow, 1).Value = slNo;
                        worksheet.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 2).Value = item.EmpId;
                        worksheet.Cell(currentRow, 3).Value = item.EmployeeName;
                        worksheet.Cell(currentRow, 4).Value = item.Designation.Name;
                        worksheet.Cell(currentRow, 5).Value = item.Department.Name;
                        worksheet.Cell(currentRow, 6).Value = item.Location.Name;
                        worksheet.Cell(currentRow, 7).Value = item.Category.Name;
                        worksheet.Cell(currentRow, 8).Value = item.RequiredDate;
                        worksheet.Cell(currentRow, 8).Style.DateFormat.Format = "dd-MMM-yyyy";
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
                            "Employee Pending requests List.xlsx"
                            );
                    }
                }

            }


            else if (SearchId == 2 || SearchId == 5) //Stock and Damaged
            {
                
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add(SearchName);
                    int count = model.AssetList.Count + 5;


                    worksheet.Range("A1:G" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:G" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:G" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:G" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:G" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;

                    var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 7)));
                    wsReportNameHeaderRange.Style.Font.Bold = true;
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Merge();
                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191); ;
                    wsReportNameHeaderRange.Value = SearchName + "(" + TabName + ")";
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
                    worksheet.Cell(3, 6).Value = model.AssetList.Count;
                    worksheet.Cell(4, 1).Style.Font.Bold = true;
                    worksheet.Cell(4, 1).Value = "Filter Applied";
                    worksheet.Range("B4:G4").Merge();
                    worksheet.Cell("B4").Value = FilterValues;



                   


                    var currentRow = 5;
                    #region Header
                    worksheet.Cell(currentRow, 1).Value = "Slno.";
                    worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 2).Value = "Asset Code";
                    worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 3).Value = "Asset Category";
                    worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 4).Value = "Asset Name";
                    worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 5).Value = "Issuing Department";
                    worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 6).Value = "Condition";
                    worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 7).Value = "Specifications";
                    worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                    #endregion
                    int i=1;
                    #region Body
                    foreach (var item in model.AssetList)
                    {
                        currentRow++;
                        worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Range("A5:G5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                     
                        worksheet.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 1).Value = i;
                        worksheet.Cell(currentRow, 2).Value = item.AssetCode;
                        worksheet.Cell(currentRow, 3).Value = item.AssetCategory.Name;
                        worksheet.Cell(currentRow, 4).Value = item.AssetName;
                        worksheet.Cell(currentRow, 5).Value = item.IssuingDepartment.Name;
                        worksheet.Cell(currentRow, 6).Value = item.Condition.Name;
                        worksheet.Cell(currentRow, 7).Value = item.Specification;
                        i++;
                        worksheet.Columns(1, 7).Style.Alignment.WrapText = true;

                    }
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
                            book.Protect(EP.Password,false,false);
                            book.SaveToStream(stream);
                        }
                        var content = stream.ToArray();
                        return File(
                            content,
                            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                            SearchName +".xlsx"
                            );
                    }
                }
                    
                }
            else if (SearchId == 3) //Due
            {
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Return Due List");
                    int count = model.AssetAllocationList.Count + 5;


                    worksheet.Range("A1:K" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:K" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:K" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:K" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:K" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;

                    var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 10)));
                    wsReportNameHeaderRange.Style.Font.Bold = true;
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Merge();
                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191); ;
                    wsReportNameHeaderRange.Value = "Return Due List (" + TabName + ")";

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
                    worksheet.Cell(3, 6).Value = model.AssetAllocationList.Count;
                    worksheet.Cell(4, 1).Style.Font.Bold = true;
                    worksheet.Cell(4, 1).Value = "Filter Applied";
                    worksheet.Range("B4:K4").Merge();
                    worksheet.Cell("B4").Value = FilterValues;

                  

                    var currentRow = 5;
                    #region Header
                    worksheet.Cell(currentRow, 1).Value = "Slno.";
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
                    worksheet.Cell(currentRow, 8).Value = "Asset Name";
                    worksheet.Cell(currentRow, 8).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 9).Value = "Issued Date";
                    worksheet.Cell(currentRow, 9).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 10).Value = "Due Date";
                    worksheet.Cell(currentRow, 10).Style.Font.Bold = true;                    
                    worksheet.Cell(currentRow, 11).Value = "Ageing";
                    worksheet.Cell(currentRow, 11).Style.Font.Bold = true;
                    #endregion

                    int i = 1;
                    #region Body
                    foreach (var item in model.AssetAllocationList)
                    {
                        currentRow++;
                        worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Range("A5:K5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                     
                        worksheet.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 1).Value = i;
                        worksheet.Cell(currentRow, 2).Value = item.EmpId;
                        worksheet.Cell(currentRow, 3).Value = item.EmployeeName;
                        worksheet.Cell(currentRow, 4).Value = item.Designation.Name;
                        worksheet.Cell(currentRow, 5).Value = item.Department.Name;
                        worksheet.Cell(currentRow, 6).Value = item.Location.Name;
                        worksheet.Cell(currentRow, 7).Value = item.Category.Name;
                        worksheet.Cell(currentRow, 8).Value = item.AssetName;
                        worksheet.Cell(currentRow, 9).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 9).Value = item.IssuedDate;
                        worksheet.Cell(currentRow, 9).Style.DateFormat.Format = "dd-MMM-yyyy";
                        worksheet.Cell(currentRow, 10).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 10).Value = item.DueDate;
                        worksheet.Cell(currentRow, 10).Style.DateFormat.Format = "dd-MMM-yyyy";
                        worksheet.Cell(currentRow, 11).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 11).Value = item.Ageing;
                        i++;
                        worksheet.Columns(1, 11).Style.Alignment.WrapText = true;
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
                            "Return Due List.xlsx"
                            );
                    }
                }
            }
            else if (SearchId == 4) //History
            {
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Asset Issue History List");
                    int count = model.AssetAllocationList.Count + 5;


                    worksheet.Range("A1:K" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:K" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:K" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:K" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:K" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;

                    var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 10)));
                    wsReportNameHeaderRange.Style.Font.Bold = true;
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Merge();
                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191); ;
                    wsReportNameHeaderRange.Value = "Asset Issue History List (" + TabName + ")";
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
                    worksheet.Cell(3, 6).Value = model.AssetAllocationList.Count;
                    worksheet.Cell(4, 1).Style.Font.Bold = true;
                    worksheet.Cell(4, 1).Value = "Filter Applied";
                    worksheet.Range("B4:K4").Merge();
                    worksheet.Cell("B4").Value = FilterValues;
                    wsReportNameHeaderRange.Value = "Asset Issue History List (" + TabName + ")";

                    

                    var currentRow = 5;
                    #region Header
                    worksheet.Cell(currentRow, 1).Value = "Slno.";
                    worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 2).Value = "ID";
                    worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 3).Value = "EmployeeName";
                    worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 4).Value = "Designation";
                    worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 5).Value = "Department";
                    worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 6).Value = "Category";
                    worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 7).Value = "Asset Name";
                    worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 8).Value = "Issued Date";
                    worksheet.Cell(currentRow, 8).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 9).Value = "Due Date";
                    worksheet.Cell(currentRow, 9).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 10).Value = "Return Date";
                    worksheet.Cell(currentRow, 10).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 11).Value = "Status";
                    worksheet.Cell(currentRow, 11).Style.Font.Bold = true;
                    #endregion
                    int i = 1;
                    #region Body
                    foreach (var item in model.AssetAllocationList)
                    {
                        currentRow++;
                        worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Range("A5:K5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    
                        worksheet.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 1).Value = i;
                        worksheet.Cell(currentRow, 2).Value = item.EmpId;
                        worksheet.Cell(currentRow, 3).Value = item.EmployeeName;
                        worksheet.Cell(currentRow, 4).Value = item.Designation.Name;
                        worksheet.Cell(currentRow, 5).Value = item.Department.Name;
                        worksheet.Cell(currentRow, 6).Value = item.Category.Name;
                        worksheet.Cell(currentRow, 7).Value = item.AssetName;
                        worksheet.Cell(currentRow, 8).Value = item.IssuedDate;
                        worksheet.Cell(currentRow,8).Style.DateFormat.Format = "dd-MMM-yyyy";
                        worksheet.Cell(currentRow, 9).Value = item.DueDate;
                        worksheet.Cell(currentRow, 9).Style.DateFormat.Format = "dd-MMM-yyyy";
                        worksheet.Cell(currentRow, 10).Value = item.ReturnDate == DateTime.Parse("01/Jan/0001 12:00:00 AM") ? null : item.ReturnDate;
                        worksheet.Cell(currentRow, 10).Style.DateFormat.Format = "dd-MMM-yyyy";
                        worksheet.Cell(currentRow, 11).Value = item.Status;
                        i++;
                        worksheet.Columns(1, 11).Style.Alignment.WrapText = true;
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
                            "Asset Issue History List.xlsx"
                            );
                    }
                }
            }
            else //Allocated
            {
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("AssetAllocationList");
                    int count = model.AssetAllocationList.Count + 14;


                    worksheet.Range("A1:R" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:R" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:R" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:R" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:R" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;

                    var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 17)));
                    wsReportNameHeaderRange.Style.Font.Bold = true;
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Merge();
                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191); ;
                    wsReportNameHeaderRange.Value = "Asset Allocation List (" + TabName + ")";

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
                    worksheet.Cell(3, 6).Value = model.AssetAllocationList.Count;
                    worksheet.Cell(4, 1).Style.Font.Bold = true;
                    worksheet.Cell(4, 1).Value = "Filter Applied";
                    worksheet.Range("B4:R4").Merge();
                    worksheet.Cell("B4").Value = FilterValues;

                   

                    var currentRow =5;
                    #region Header
                    worksheet.Cell(currentRow, 1).Value = "Slno.";
                    worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 2).Value = "ID";
                    worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 3).Value = "Employee Name";
                    worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 4).Value = "Designation";
                    worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 5).Value = "Department";
                    worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 6).Value = "Location";
                    worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 7).Value = "Issued Date";
                    worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 8).Value = "Aging";
                    worksheet.Cell(currentRow, 8).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 9).Value = "Asset Code";
                    worksheet.Cell(currentRow, 9).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 10).Value = "Asset Category";
                    worksheet.Cell(currentRow, 10).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 11).Value = "Asset Name";
                    worksheet.Cell(currentRow, 11).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 12).Value = "Issuing Department";
                    worksheet.Cell(currentRow, 12).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 13).Value = "Date Of Purchase";
                    worksheet.Cell(currentRow, 13).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 14).Value = "Asset Cost";
                    worksheet.Cell(currentRow, 14).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 15).Value = "Model No";
                    worksheet.Cell(currentRow, 15).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 16).Value = "Brand";
                    worksheet.Cell(currentRow, 16).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 17).Value = "Specification";
                    worksheet.Cell(currentRow, 17).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 18).Value = "Due Date";
                    worksheet.Cell(currentRow, 18).Style.Font.Bold = true;
                    #endregion
                    int i = 1;
                    #region Body
                    foreach (var item in model.AssetAllocationList)
                    {
                        currentRow++;
                        worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Range("A5:R5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                        worksheet.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 1).Value = i;
                        worksheet.Cell(currentRow, 2).Value = item.EmpId;
                        worksheet.Cell(currentRow, 3).Value = item.EmployeeName;
                        worksheet.Cell(currentRow, 4).Value = item.Designation.Name;
                        worksheet.Cell(currentRow, 5).Value = item.Department.Name;
                        worksheet.Cell(currentRow, 6).Value = item.Location.Name;
                        worksheet.Cell(currentRow, 7).Value = item.IssuedDate;
                        worksheet.Cell(currentRow, 7).Style.DateFormat.Format = "dd-MMM-yyyy";
                        worksheet.Cell(currentRow, 8).Value = item.Ageing;
                        worksheet.Cell(currentRow, 9).Value = item.AssetCode;
                        worksheet.Cell(currentRow, 10).Value = item.Category.Name;
                        worksheet.Cell(currentRow, 11).Value = item.AssetName;
                        worksheet.Cell(currentRow, 12).Value = item.IsssueDept;
                        worksheet.Cell(currentRow, 13).Value = item.PurchaseDate;
                        worksheet.Cell(currentRow, 13).Style.DateFormat.Format = "dd-MMM-yyyy";
                        worksheet.Cell(currentRow, 14).Value = item.AssetCost;
                        worksheet.Cell(currentRow, 15).Value = item.ModelName;
                        worksheet.Cell(currentRow, 16).Value = item.Brand;
                        worksheet.Cell(currentRow, 17).Value = item.Specification;
                        worksheet.Cell(currentRow, 18).Value = item.DueDate;
                        worksheet.Cell(currentRow, 18).Style.DateFormat.Format = "dd-MMM-yyyy";

                        i++;
                        worksheet.Columns(1, 9).Style.Alignment.WrapText = true;

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
                            "AssetAllocationList.xlsx"
                            );
                    }
                }
            }

           
        }

        #region POST
        [HttpPost]
        public async Task<IActionResult> Create(AssetAllocation model)
        {
            try
            {

                var response = await _assetAllocationService.AssetAllocationSave(model);
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
        public async Task<ActionResult> AssetAllocationDetailsSave(AssetAllocation model)
        {
            try
            {
                var response = await _assetAllocationService.AssetAllocationDetailsSave(model);
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
        public async Task<ActionResult> IssueSave(AssetAllocation model)
        {
            try
            {
                var response = await _assetAllocationService.AssetIssue(model);
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
        public async Task<IActionResult> Approve(AssetAllocation model)
        {
            try
            {
                var response = await _assetAllocationService.Approve(model);
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
        public async Task<IActionResult> Reject(AssetAllocation model)
        {
            try
            {
                var response = await _assetAllocationService.Reject(model);
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


        #endregion
        #region GET
        [HttpGet]
        public async Task<ActionResult> GetAsset(int AssetCategoryCode)
        {
            try
            {
            var Asset = new SelectList(await _dropdownService.GetAssetDropDown(AssetCategoryCode), "Code", "Name");
            return Json(Asset);
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
        public async Task<ActionResult> GetAssetDescription(int AssetCode)
        {
            try
            {
            var AssetDetails = await _assetAllocationService.GetAssetDetails(AssetCode);
            return Json(AssetDetails);
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
