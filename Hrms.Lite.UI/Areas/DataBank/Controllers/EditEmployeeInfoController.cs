using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.Account;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Spire.Xls;

namespace Hrms.Lite.UI.Areas.DataBank.Controllers
{
    [Area("DataBank")]
    [SessionAuthorize]
    public class EditEmployeeInfoController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly IEditEmployeeInfoService _editEmployeeInfoService;
        private readonly ICommonService _commonService;
        private readonly IEnrollmentService _enrollmentService;
        private readonly IHelpers _helpers;
        private readonly IBlob _blob;

        public EditEmployeeInfoController(IEditEmployeeInfoService EditEmployeeInfoService, IDropdownService dropdownService, ICommonService commonService,IEnrollmentService enrollmentService,
            IHelpers helpers,IBlob blob)
        {
            _enrollmentService = enrollmentService;
            _editEmployeeInfoService = EditEmployeeInfoService;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = helpers;
            _blob = blob;
        }
        public async Task<IActionResult> Index()
        {
            try { 
            EditEmployeeInfo model = new EditEmployeeInfo();
            Filter filter = new Filter();

            ViewBag.Empl = new SelectList(await _dropdownService.GetEditEmployee_EmployeeDropDown("ACTIVE"), "Code", "Name");
            ViewBag.Filter = new SelectList(await _dropdownService.GetEditEmployeeInfoListDropDown(), "Code", "Name");
            model.EmployeeHeader = new EmployeeHeader();
            model.EmployeeHeader.Department = new Department();
            model.EmployeeHeader.Designation = new Designation();
            model.EmployeeHeader.Location = new Location();
            model.EmployeeHeader.Category = new Category();
            model.FilterCombo = new FilterSlabComboFill();
            model.FilterCombo.Code = 3;
            model.Filter = await _commonService.GetFilterDropdowns();
            List<FilterSlabComboFill> Tabs = new List<FilterSlabComboFill>();
            FilterSlabComboFill Tab1 = new FilterSlabComboFill();
            FilterSlabComboFill Tab2 = new FilterSlabComboFill();
            Tab1.Code = 0;
            Tab1.Name = "Edit Summary";
            Tabs.Add(Tab1);
            Tab2.Code = 1;
            Tab2.Name = "Approval Pending";
            Tabs.Add(Tab2);

            ViewBag.Tabs = new SelectList(Tabs, "Code", "Name");
            //model.FirstTab = FirstTab;
            model.Type = "ACTIVE";

                //model = await _editEmployeeInfoService.GetEmployeelistDetails("ACTIVE",filter);

                EditEmployeeInfo m = new EditEmployeeInfo();
                filter.length = 10;
                m = await _editEmployeeInfoService.GetEditEmployeeInfoList(0, 0, filter);

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

        public async Task<JsonResult> GetEmployeeDropdown(string Type)
        {
            var Emp = await _dropdownService.GetEditEmployee_EmployeeDropDown(Type);
            return Json(Emp);
        }
        
        [HttpPost]
        public async Task<IActionResult> GetList(int tabIndex, int searchID, Filter filter1)
        {
            try
            {
                EditEmployeeInfo model = new EditEmployeeInfo();
                EditEmployeeInfo m = new EditEmployeeInfo();
               
                Filter filter = new Filter();
               model.Filter = filter1;
            ;

                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }
                model = await _editEmployeeInfoService.GetEditEmployeeInfoList(tabIndex, searchID, filter1);
              

                m.EditInfoList = model.EditInfoList;
                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.EditInfoList == null || model.EditInfoList.Count() == 0)
                {
                   

                    return Json(new
                    {
                        data = "",
                        draw = filter1.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        pendingcount = model.AppPendingCount,
                        activecount = model.EditSummaryCount,
                        recordsFiltered = totalrows
                    });
                }




                if (model.EditInfoList.Count > 0)
                {
                    totalrows = m.EditInfoList[0].active;

                    //m.OnBoardingList.ToList<Hrms.Lite.Shared.Master.Test>().Count; ;
                }

                //model.AllTabCount = m.AllTabCount;
                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = m.EditInfoList[0].totalRecords;
                    return Json(new
                    {
                        data = model.EditInfoList,
                        draw = filter1.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        recordsTotal = totalrows,
                        pendingcount=model.AppPendingCount,
                        activecount=model.EditSummaryCount,
                        recordsFiltered = totalrows//totalrowsafterfiltering
                    });
                }
                else
                {
                    // return Json(new { data = model.EmployeeProfileList, draw = filter1.draw, recordsTotal = totalrows, recordsFiltered = totalrows });
                    return Json(new
                    {
                        data = model.EditInfoList,
                        draw = filter1.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        pendingcount = model.AppPendingCount,
                        activecount = model.EditSummaryCount,
                        recordsFiltered = totalrows
                    });
                }


            }
            catch (Exception ex)
            {
                return View();
            }




        }
        public async Task<IActionResult> ApplyFilter(int TabIndex, int SearchId, Filter filter)
        {
            try { 
            EditEmployeeInfo model = new EditEmployeeInfo();
            model.Filter = filter;
            model = await _editEmployeeInfoService.GetEditEmployeeInfoList(TabIndex, SearchId, filter);
            if (TabIndex == 0)
                return PartialView("_EditSummaryList", model);
            else
                return PartialView("_ApproveList", model);
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

        
        public async Task<IActionResult> EditEmployee()
        {
           
            return PartialView("_EditEmployee");
        }
        public async Task<ActionResult> EmployeeCard(int EmpCode)
        {
            try { 
            EditEmployeeInfo model = new EditEmployeeInfo();

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
        public async Task<IActionResult> Create(Guid EmployeeGI)
        {
            try { 
            EditEmployeeInfo model = new EditEmployeeInfo();
            ViewBag.Empl = new SelectList(await _dropdownService.GetEmployeeDropDown(), "Code", "Name");
            ViewBag.Gender = new SelectList(await _dropdownService.GetGenderDropDown(), "Code", "Name");
            ViewBag.Salutation = new SelectList(await _dropdownService.GetSalutationDropDown(), "Code", "Name");
            ViewBag.PayMode = new SelectList(await _dropdownService.GetPaymentModeDropDown(), "Code", "Name");
            ViewBag.Bank = new SelectList(await _dropdownService.GetBankDropdown(), "Code", "Name");

            ViewBag.PFGroup = new SelectList(await _dropdownService.GetPFGroupDropdown(), "Code", "Name");
            ViewBag.ESIGroup = new SelectList(await _dropdownService.GetESIGroupDropDown(), "Code", "Name");
            ViewBag.PTGroup = new SelectList(await _dropdownService.GetPTGroupDropDown(), "Code", "Name");


            model = await _editEmployeeInfoService.GetEditEmployeeInfoDetails(EmployeeGI);
            model.EmployeeHeader = await _commonService.GetEmployeeHeader(EmployeeGI);
            model.StatutoryClassification.PFEnrollDate = model.StatutoryClassification.PFEnrollDate == DateTime.Parse("01/Jan/1900 12:00:00 AM") ? null : model.StatutoryClassification.PFEnrollDate;
            model.StatutoryClassification.ESIEnrollDate = model.StatutoryClassification.ESIEnrollDate == DateTime.Parse("01/Jan/1900 12:00:00 AM") ? null : model.StatutoryClassification.ESIEnrollDate;
            return View("~/Areas/DataBank/Views/EditEmployeeInfo/Create.cshtml",model);
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


        public async Task<IActionResult> CheckifAlreadySalaryProcessed(DateTime Date,Guid empGi)
        {
            try
            {
                var details = await _commonService.GetSalaryPeriodLockDatewiseValidation(Date.ToString("yyyy-MMM-dd"), "EditEmployeeInfo", empGi);
                return Json(details);
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
        public async Task<IActionResult>Approve(Guid EmployeeGI,Guid LogGI)
        {
            try { 
            EditEmployeeInfo model = new EditEmployeeInfo();
            model = await _editEmployeeInfoService.GetEditEmployeeInfoApproveDetails(EmployeeGI,LogGI);
            model.EmployeeHeader = await _commonService.GetEmployeeHeader(EmployeeGI);
            return View("~/Areas/DataBank/Views/EditEmployeeInfo/Approve.cshtml", model);
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
        public async Task<IActionResult> Create(EditEmployeeInfo model)
        {
            try {
                if (model.StatutoryClassification.PaymentMode.Code == 2)
                {
                    model.StatutoryClassification.Bank.Code=null;
                    model.StatutoryClassification.AccountNo=null;
                    model.StatutoryClassification.IFSC=null;
                }
                var response = await _editEmployeeInfoService.SaveEditEmployeeInfo(model);
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
        public async Task<IActionResult> Approve(EditEmployeeInfo model)
        {
            try { 
            model.ApproveMode = "APPROVE";
            var response = await _editEmployeeInfoService.ApproveEditEmployeeInfo(model);
           
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
        public async Task<IActionResult> Reject(EditEmployeeInfo model)
        {
            try { 
            model.ApproveMode = "REJECT";
            var response = await _editEmployeeInfoService.ApproveEditEmployeeInfo(model);
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
        public async Task<IActionResult> EditEmployeeLog(Guid EmployeeGI,Guid LogGI)
        {
            try { 
            EditEmployeeInfo model = new EditEmployeeInfo();
            model = await _editEmployeeInfoService.GetEditEmployeeInfoLogDetails(EmployeeGI, LogGI);
            model.EmployeeHeader = await _commonService.GetEmployeeHeader(EmployeeGI);
            return View("~/Areas/DataBank/Views/EditEmployeeInfo/EditEmployeeLog.cshtml", model);
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
        public async Task<JsonResult> AlreadyExistCheck(Guid EmployeeGI, string PFNumber, string ESINumber, string UANNumber, string EmployeeID,string AccountNo)
        {
            var result = await _enrollmentService.AlreadyExistCheck(EmployeeGI, "", "", "", PFNumber, ESINumber, UANNumber, EmployeeID, "", AccountNo);
            return Json(result);
        }
        

        public async Task<IActionResult> ExportToExcel(int TabIndex, int SearchId, int totCount,Filter filter)
        {
            EditEmployeeInfo model = new EditEmployeeInfo();
            var LoginUser = HttpContext.Session.GetString("LoginUser");
            var CompanyName = HttpContext.Session.GetString("CompanyName");
            var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
            var GroupCompanyName = User.authUser.GroupCompanyName;
            ExcelPassword EP = new ExcelPassword();
            string TabName;
            if (TabIndex == 0)
            {
                TabName= "Edit Summary";
            }
            else
            {
                TabName = "Approval Pending";

            }
            EP = await _commonService.GetExcelPassword("Edit Employee (" + TabName + ")", "Excel");
            int slNo = 0;
            
               filter.length = totCount;
                filter.start = 0;
            
          
            string FilterValues = await _helpers.GetFilterValues(filter);
            model = await _editEmployeeInfoService.GetEditEmployeeInfoList(TabIndex, SearchId, filter);

            if (FilterValues == "")
            {
                FilterValues = "All";
            }
            string currDateTime = await _helpers.GetCurrentDate();
            
            if (TabIndex ==0){

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Edit summary List");
                    int count = model.EditInfoList.Count + 5;


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
                    wsReportNameHeaderRange.Value = "EditEmployeeInfo";

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
                    worksheet.Cell(3, 6).Value = model.EditInfoList.Count;
                    worksheet.Cell(4, 1).Style.Font.Bold = true;
                    worksheet.Cell(4, 1).Value = "Filter Applied";
                    worksheet.Range("B4:H4").Merge();
                    worksheet.Cell("B4").Value = FilterValues;


                    var currentRow = 5;
                    #region Header
                    worksheet.Cell(currentRow, 1).Value = "Sl.No";
                    worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 2).Value = "ID";
                    worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 3).Value = "EmployeeName";
                    worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 4).Value = "Designation";
                    worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 5).Value = "Department";
                    worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 6).Value = "Location";
                    worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 7).Value = "DOJ";
                    worksheet.Cell(currentRow, 8).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 8).Value = "Edit Date";

                #endregion

                #region Body
                foreach (var item in model.EditInfoList)
                {
                    currentRow++;
                        slNo = slNo + 1;
                        worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Range("A5:H5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                        worksheet.Cell(currentRow, 1).Value = slNo;
                        worksheet.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 2).Value = item.EmployeeID;
                        worksheet.Cell(currentRow, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 3).Value = item.FullName;
                        worksheet.Cell(currentRow, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 4).Value = item.Designation;
                        worksheet.Cell(currentRow, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 5).Value = item.Department;
                        worksheet.Cell(currentRow, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 6).Value = item.Location;
                        worksheet.Cell(currentRow, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 7).Value = item.ExpectedDOJ.ToString();
                        worksheet.Cell(currentRow, 7).Style.DateFormat.Format = "dd-MMM-yyyy";
                        worksheet.Cell(currentRow, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 8).Value = item.EditDate.ToString();
                        worksheet.Cell(currentRow, 8).Style.DateFormat.Format = "dd-MMM-yyyy";

                        worksheet.Columns(1, 8).Style.Alignment.WrapText = true;
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
                        "Edit summary List.xlsx"
                        );
                }
            }
            }
            else
            {
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Pending List");
                    int count = model.EditInfoList.Count + 5;


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
                    wsReportNameHeaderRange.Value = "EditEmployeeInfo";

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
                    worksheet.Cell(3, 6).Value = model.EditInfoList.Count;
                    worksheet.Cell(4, 1).Style.Font.Bold = true;
                    worksheet.Cell(4, 1).Value = "Filter Applied";
                    worksheet.Range("B4:H4").Merge();
                    worksheet.Cell("B4").Value = FilterValues;


                    var currentRow = 5;
                    #region Header
                    worksheet.Cell(currentRow, 1).Value = "Sl.No";
                    worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 2).Value = "ID";
                    worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 3).Value = "EmployeeName";
                    worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 4).Value = "Designation";
                    worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 5).Value = "Department";
                    worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 6).Value = "Location";
                    worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 7).Value = "DOJ";
                    worksheet.Cell(currentRow, 8).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 8).Value = "Edit Date";

                    #endregion

                    #region Body
                    foreach (var item in model.EditInfoList)
                    {
                        currentRow++;
                        slNo = slNo + 1;
                        worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Range("A5:H5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                        worksheet.Cell(currentRow, 1).Value = slNo;
                        worksheet.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 2).Value = item.EmployeeID;
                        worksheet.Cell(currentRow, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 3).Value = item.FullName;
                        worksheet.Cell(currentRow, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 4).Value = item.Designation;
                        worksheet.Cell(currentRow, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 5).Value = item.Department;
                        worksheet.Cell(currentRow, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 6).Value = item.Location;
                        worksheet.Cell(currentRow, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 7).Value = item.ExpectedDOJ.ToString();
                        worksheet.Cell(currentRow, 7).Style.DateFormat.Format = "dd-MMM-yyyy";
                        worksheet.Cell(currentRow, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 8).Value = item.EditDate.ToString();
                        worksheet.Cell(currentRow, 8).Style.DateFormat.Format = "dd-MMM-yyyy";

                        worksheet.Columns(1, 8).Style.Alignment.WrapText = true;
                        worksheet.Rows().Style.Alignment.WrapText = true;
                    }
                    #endregion
                    worksheet.Columns().AdjustToContents();
                    worksheet.Rows().AdjustToContents();

                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        var content = stream.ToArray();
                        return File(
                            content,
                            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                            "Pending List.xlsx"
                            );
                    }
                }
            }

            //}
        }


        public async Task<IActionResult> TransactionHistory()
        {
            try
            {
                await fillfilterDropdown();
                EditEmployeeInfo model = new EditEmployeeInfo();
                ViewBag.Filter = new SelectList(await _dropdownService.GetEditEmployeeInfoListDropDown(), "Code", "Name");

                Filter filter = new Filter();
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


        [HttpPost]
        public async Task<IActionResult> GetEmployeelistDetails(string Type, Filter filter1)
        {
            try
            {
                EditEmployeeInfo model = new EditEmployeeInfo();
                Filter filter = new Filter();
                model.Filter = filter1;
                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }
                model = await _editEmployeeInfoService.GetEmployeelistDetails(Type, filter1);
                int totalrows = 0;
                int totalrowsafterfiltering = 0;
                if (model.EditInfoList == null || model.EditInfoList.Count() == 0)
                {
                    return Json(new
                    {
                        data = "",
                        draw = filter1.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows
                    });
                }
                if (model.EditInfoList.Count > 0)
                {
                    if (Type == "ACTIVE")
                    {
                        totalrows = model.EditInfoList[0].active;
                    }
                    else
                        totalrows = model.EditInfoList[0].resigned;

                }
                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = model.EditInfoList[0].totalRecords;
                    return Json(new
                    {
                        data = model.EditInfoList,
                        draw = filter1.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows//totalrowsafterfiltering
                    });
                }
                else
                {
                    return Json(new
                    {
                        data = model.EditInfoList,
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
        [HttpGet]
        public async Task<JsonResult> GetDigitsInAccountNumber(int BankCode)
        {
            var DigitsInAccountNumber = await _commonService.GetDigitsInAccountNumber(BankCode);
            return Json(DigitsInAccountNumber);
        }



        public async Task<IActionResult> ExportToExcelTrnList(int TotalRows, Filter filter,int TabIndex, int SearchId)
        {
            EditEmployeeInfo model = new EditEmployeeInfo();
            var LoginUser = HttpContext.Session.GetString("LoginUser");
            var CompanyName = HttpContext.Session.GetString("CompanyName");
            var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
            var GroupCompanyName = User.authUser.GroupCompanyName;
            ExcelPassword EP = new ExcelPassword();
            EP = await _commonService.GetExcelPassword("Attrition Report List", "Excel");


            filter.length = TotalRows;
            filter.start = 0;

            string FilterValues = await _helpers.GetFilterValues(filter);
            if (FilterValues == "")
            {
                FilterValues = "All";
            }
            model.filter = filter;



            string[] cu_Date = System.DateTime.Now.GetDateTimeFormats();
            string currDateTime = cu_Date[44];

            //Get Dataset
            var dataset = await _editEmployeeInfoService.editEmployeeInfoTrnList_Excel(filter, TabIndex, SearchId);
            DataTable dt = dataset.Tables[0];
            dataset.Tables[0].Rows.RemoveAt(0);
            if ((dataset.Tables[0]?.Rows?.Count ?? 0) > 0)
            {
                dt = dataset.Tables[0];
            }

            //set EXCEL Column Heading 

            for (int i = 0; i < dt.Columns.Count; i++)
            {
                if (dt.Columns[i].ColumnName == "id")
                    dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.ToUpper();
                if (dt.Columns[i].ColumnName == "doj" || dt.Columns[i].ColumnName == "lwd")
                    dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.ToUpper();
                else
                    dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
                if (dt.Columns[i].ColumnName == "Name")
                    dt.Columns[i].ColumnName = "Employee Name";

                //dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
            }

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Employee Transaction History");
                int count = dt.Rows.Count + 5;


                worksheet.Range("A1:J" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1:J" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1:J" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1:J" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1:J" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;



                var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 9)));
                wsReportNameHeaderRange.Style.Font.Bold = true;
                wsReportNameHeaderRange.Style.Font.FontSize = 15;
                wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                wsReportNameHeaderRange.Merge();
                wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                wsReportNameHeaderRange.Value = "Edit Employee Info Transaction History";

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
                worksheet.Cell(3, 4).Style.DateFormat.Format = "dd-MMM-yyyy";
                worksheet.Cell(3, 4).Value = currDateTime.ToString();
                worksheet.Cell(3, 5).Style.Font.Bold = true;
                worksheet.Cell(3, 5).Value = "Total Count";
                worksheet.Cell(3, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                worksheet.Cell(3, 6).Value = dt.Rows.Count;
                worksheet.Cell(4, 1).Style.Font.Bold = true;
                worksheet.Cell(4, 1).Value = "Filter Applied";
                var filterapplied = worksheet.Range("B4:J4").Merge();

                filterapplied.Value = FilterValues;


                var currentRow = 5;


                worksheet.Cell(currentRow, 1).InsertTable(dt);
                worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                worksheet.Range("A5:J5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                worksheet.Range("A5:J5").Style.Font.Bold = true;


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
                        "Employee Transaction History.xlsx"
                        );
                }
            }

            //}
        }



    }

}

