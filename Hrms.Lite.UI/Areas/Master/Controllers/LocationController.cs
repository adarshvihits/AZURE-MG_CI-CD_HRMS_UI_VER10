
using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.Master;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Spire.Xls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.Master.Controllers
{
    [Area("Master")]
    [SessionAuthorize]
    public class LocationController : Controller
    {
        public readonly ILocationService _service;
        private readonly IDropdownService _dropdownService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        public LocationController(ILocationService service, IDropdownService dropdownService,ICommonService commonService, IHelpers helpers)
        {
            _service = service;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = helpers;
        }
        public async Task<IActionResult> Index()
        {
            try { 
            ViewBag.CountryDropdown = new SelectList(await _dropdownService.GetCountryDropDown(), "Code", "Name");
            ViewBag.StateDropDown = new SelectList(await _dropdownService.GetStateDropDown(0), "Code", "Name");
            ViewBag.StateDropDown = new SelectList(await _dropdownService.GetDistrictDropdown(0), "Code", "Name");
            ViewBag.RegionDropDown = new SelectList(await _dropdownService.GetAllRegionsDropDown(), "Code", "Name");
            ViewBag.LocationTypeDropDown = new SelectList(await _dropdownService.GetLocationTypeDropDown(), "Code", "Name");
            ViewBag.PTGroupDropDown = new SelectList(await _dropdownService.GetPTGroupDropDown(), "Code", "Name");
            ViewBag.ESIGroupDropDown = new SelectList(await _dropdownService.GetESIGroupDropDown(), "Code", "Name");

                return View(await _service.GetList("ACTIVE"));
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
        public async Task<IActionResult> GetLocationList(string TabIndex)
        {
            try { 
            if(TabIndex=="APPROVAL_PENDING")
            return PartialView("_PendingList", await _service.GetList(TabIndex));
            else
            return PartialView("_LocationList", await _service.GetList(TabIndex));
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
        public async Task<IActionResult> Create()
        {
            try { 
            Location model = new Location();
            model.Mode = "CREATE";
            model.Active = true;
            ViewBag.CountryDropdown = new SelectList(await _dropdownService.GetCountryDropDown(), "Code", "Name");
            ViewBag.StateDropDown = new SelectList(await _dropdownService.GetStateDropDown(0), "Code", "Name");
            ViewBag.DistrictDropDown = new SelectList(await _dropdownService.GetDistrictDropdown(0), "Code", "Name");
            ViewBag.RegionDropDown = new SelectList(await _dropdownService.GetAllRegionsDropDown(), "Code", "Name");
            ViewBag.LocationTypeDropDown = new SelectList(await _dropdownService.GetLocationTypeDropDown(), "Code", "Name");
            ViewBag.PTGroupDropDown = new SelectList(await _dropdownService.GetPTGroupDropDown(), "Code", "Name");
            ViewBag.ESIGroupDropDown = new SelectList(await _dropdownService.GetESIGroupDropDown(), "Code", "Name");
            ViewBag.Area = new SelectList(await _dropdownService.GetAreaDropDown(), "Code", "Name");
            ViewBag.FunctionalState = new SelectList(await _dropdownService.GetFunctionalStateDropDown(), "Code", "Name");
            ViewBag.CategoryDropDown = new SelectList(await _dropdownService.GetLocationCategoryDropDown(), "Code", "Name");
           
            ViewBag.BusinessTypeDropDown = new SelectList(await _dropdownService.GetBusinessTypeDropDown(), "Code", "Name");
            ViewBag.ZoneDropDown = new SelectList(await _dropdownService.GetZoneDropDown(), "Code", "Name");
            ViewBag.ClusterDropDown = new SelectList(await _dropdownService.GetclusterDropDown(), "Code", "Name");
            ViewBag.Metrodropdown = new SelectList(await _dropdownService.GetMetroDropDown(), "Code", "Name");

                return PartialView("~/Areas/Master/Views/Location/_CreateOrEdit.cshtml",model);
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
        public async Task<JsonResult> GetState(int id)
        {
            var State = new SelectList(await _dropdownService.GetStateDropDown(id), "Code", "Name");
            return Json(State);
        }
        [HttpGet]
        public async Task<JsonResult> GetDistrict(int id)
        {
            var District = new SelectList(await _dropdownService.GetDistrictDropdown(id), "Code", "Name");
            return Json(District);
        }

        [HttpPost]
        public async Task<ActionResult> Save(Location Input)
        {
            try { 
            var response = await _service.Create(Input);

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
        public async Task<IActionResult> Edit(Guid LocationGI,Guid LogGI)
        {
            try { 
            Location model = await _service.GetLocationDetails(LocationGI,LogGI,"MODIFY");
            model.Mode = "MODIFY";

            ViewBag.CountryDropdown = new SelectList(await _dropdownService.GetCountryDropDown(), "Code", "Name");
            ViewBag.StateDropDown = new SelectList(await _dropdownService.GetStateDropDown(model.Country.Code), "Code", "Name");
            ViewBag.DistrictDropDown = new SelectList(await _dropdownService.GetDistrictDropdown(model.State.Code), "Code", "Name");
            ViewBag.RegionDropDown = new SelectList(await _dropdownService.GetRegionDropDown(), "Code", "Name");
            ViewBag.LocationTypeDropDown = new SelectList(await _dropdownService.GetLocationTypeDropDown(), "Code", "Name");
            ViewBag.PTGroupDropDown = new SelectList(await _dropdownService.GetPTGroupDropDown(), "Code", "Name");
            ViewBag.ESIGroupDropDown = new SelectList(await _dropdownService.GetESIGroupDropDown(), "Code", "Name");
            ViewBag.Area = new SelectList(await _dropdownService.GetAreaDropDown(), "Code", "Name");
            ViewBag.FunctionalState = new SelectList(await _dropdownService.GetFunctionalStateDropDown(), "Code", "Name");
            ViewBag.CategoryDropDown = new SelectList(await _dropdownService.GetLocationCategoryDropDown(), "Code", "Name");
           
            ViewBag.BusinessTypeDropDown = new SelectList(await _dropdownService.GetBusinessTypeDropDown(), "Code", "Name");
            ViewBag.ZoneDropDown = new SelectList(await _dropdownService.GetZoneDropDown(), "Code", "Name");
            ViewBag.ClusterDropDown = new SelectList(await _dropdownService.GetclusterDropDown(), "Code", "Name");
            ViewBag.Metrodropdown = new SelectList(await _dropdownService.GetMetroDropDown(), "Code", "Name");


                return PartialView("_CreateOrEdit", model);
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
        public async Task<IActionResult> Delete(Guid LocationGI,Guid LogGI)
        {
            try { 
            Location model = await _service.GetLocationDetails(LocationGI,LogGI,"DELETE");
            model.Mode = "DELETE";
            return PartialView("_Delete", model);
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
        public async Task<ActionResult> Delete(Location model)
        {
            try { 
            var response = await _service.Delete(model);
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
        public async Task<IActionResult> Approve(Guid LocationGI,Guid LogGI)
        {
            try { 
            Location model = await _service.GetLocationDetails(LocationGI, LogGI, "APPROVAL");
            //model.Mode = "Approval";
            return PartialView("_Approve", model);
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
        public async Task<ActionResult> Approve1(Location model)
        {
            try { 
            model.ApproveMode = "APPROVE";
            var response = await _service.ApproveOrReject(model);
    
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



        public async Task<ActionResult> GetActiveInactiveValidation(string name, int code)
        {
            try
            {
                var response = await _service.GetMasterInactiveValidation(name, code);

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
        public async Task<ActionResult> Reject(Location model)
        {
            try { 
            model.ApproveMode = "REJECT";
            var response = await _service.ApproveOrReject(model);
          
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


        public async Task<IActionResult> ExportToExcel(string TabIndex)
        {
            try { 
            Location model = new Location();
            model = await _service.GetList(TabIndex);

            int slno = 0;

            string currDateTime = await _helpers.GetCurrentDate();
            
            var LoginUser = HttpContext.Session.GetString("LoginUser");
            var CompanyName = HttpContext.Session.GetString("CompanyName");
            ExcelPassword EP = new ExcelPassword();
            
            if (TabIndex == "APPROVAL_PENDING")
            {
                EP = await _commonService.GetExcelPassword("Location Pending List", "Excel");
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("PendingList");
                    int Count = model.PendingCount + 6;
                        string columnName = "";

                        int Columncount = 30;

                        while (Columncount > 0)
                        {
                            int modulo = (Columncount - 1) % 26;
                            columnName = Convert.ToChar('A' + modulo) + columnName;
                            Columncount = (Columncount - modulo) / 26;
                        }

                        string colName = columnName;

                        worksheet.Range("A1" , colName+Count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1", colName + Count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1", colName + Count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1", colName + Count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1", colName + Count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;



                    var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1,colName));
                    wsReportNameHeaderRange.Style.Font.Bold = true;
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Merge();


                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Value = "Location " + "(" + TabIndex + ")";
                    worksheet.Range("A6",colName+6).Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
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
                    worksheet.Cell(5, 1).Value = "Total Count";
                    worksheet.Cell(5, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(5, 2).Value = model.PendingCount;

                    #region Header
                    var currentRow = 6;
                    worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 1).Value = "Sl.No";
                    worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 2).Value = "Location";
                    worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 3).Value = "ShortName";

                    worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 4).Value = "DoorNo & Building";

                        worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 5).Value = "Street";

                        worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 6).Value = "Area";

                        worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 7).Value = "Location type";

                        worksheet.Cell(currentRow, 8).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 8).Value = "Region";

                        worksheet.Cell(currentRow, 9).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 9).Value = "Country";

                        worksheet.Cell(currentRow, 10).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 10).Value = "State";

                        worksheet.Cell(currentRow, 11).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 11).Value = "District";

                        worksheet.Cell(currentRow, 12).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 12).Value = "Land Line";

                        worksheet.Cell(currentRow, 13).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 13).Value = "Email";

                        worksheet.Cell(currentRow, 14).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 14).Value = "Web Address";

                        worksheet.Cell(currentRow, 15).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 15).Value = "Lattitude";

                        worksheet.Cell(currentRow, 16).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 16).Value = "Longitude";

                        worksheet.Cell(currentRow, 17).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 17).Value = "Geo Address";

                        worksheet.Cell(currentRow, 18).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 18).Value = "PT Group";

                        worksheet.Cell(currentRow, 19).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 19).Value = "ESI Group";
                        worksheet.Cell(currentRow, 20).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 20).Value = "Entry Type";

                        worksheet.Cell(currentRow, 21).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 21).Value = "ERP Code";

                        worksheet.Cell(currentRow, 22).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 22).Value = "Functional State";

                        worksheet.Cell(currentRow, 23).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 23).Value = "Statutory Registration";


                        worksheet.Cell(currentRow, 24).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 24).Value = "Tax Employer";

                        worksheet.Cell(currentRow, 25).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 25).Value = "Tax Employee";

                        worksheet.Cell(currentRow, 26).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 26).Value = "Welfare Fund";

                        worksheet.Cell(currentRow, 27).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 27).Value = "License Number";

                        worksheet.Cell(currentRow, 28).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 28).Value = "LIN Number";

                        worksheet.Cell(currentRow, 29).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 29).Value = "SLN";


                        worksheet.Cell(currentRow, 30).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 30).Value = "Area Name";


                        #endregion

                        #region Body
                        foreach (var item in model.LocationList)
                    {
                        currentRow++;
                        slno = slno + 1;
                        worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 1).Value = slno;
                        worksheet.Cell(currentRow, 2).Value = item.Name;
                        worksheet.Cell(currentRow, 3).Value = item.ShortName;
                        worksheet.Cell(currentRow, 4).Value = item.DoorNumAndBuilding;
                        worksheet.Cell(currentRow, 5).Value = item.Street;
                        worksheet.Cell(currentRow, 6).Value = item.AreaName;
                        worksheet.Cell(currentRow, 7).Value = item.LocationType.Name;
                        worksheet.Cell(currentRow, 8).Value = item.Region.Name;
                        worksheet.Cell(currentRow, 9).Value = item.Country.Name;
                        worksheet.Cell(currentRow, 10).Value = item.State.Name;
                        worksheet.Cell(currentRow, 11).Value = item.District.Name;
                        worksheet.Cell(currentRow, 12).Value = item.LandLineNum;
                        worksheet.Cell(currentRow, 13).Value = item.EmailId;
                        worksheet.Cell(currentRow, 14).Value = item.WebAdderss;
                        worksheet.Cell(currentRow, 15).Value = item.Latitude;
                        worksheet.Cell(currentRow, 16).Value = item.Longitude;
                        worksheet.Cell(currentRow, 17).Value = item.GeoAdderss;
                        worksheet.Cell(currentRow, 18).Value = item.PTgroup.Name;
                        worksheet.Cell(currentRow, 19).Value = item.ESIgroup.Name;
                           
                            worksheet.Cell(currentRow, 20).Value = item.Mode;
                            worksheet.Cell(currentRow, 21).Value = item.ERPCode;
                            worksheet.Cell(currentRow, 22).Value = item.FunctionalState.Name;
                            worksheet.Cell(currentRow, 23).Value = item.StatutoryRegistration;
                            worksheet.Cell(currentRow, 24).Value = item.TaxEmployer;
                            worksheet.Cell(currentRow, 25).Value = item.TaxEmployee;
                            worksheet.Cell(currentRow, 26).Value = item.WelfareFund;
                            worksheet.Cell(currentRow, 27).Value = item.LicenseNumber;
                            worksheet.Cell(currentRow, 28).Value = item.LINNumber;
                            worksheet.Cell(currentRow, 29).Value = item.SLN;
                            worksheet.Cell(currentRow, 30).Value = item.AreaName;

                            worksheet.Columns(1, 30).Style.Alignment.WrapText = true;

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
                            "Location Pending List.xlsx"
                            );
                    }
                }

            }

            else
            {
                EP = await _commonService.GetExcelPassword("Location List ("+TabIndex+")", "Excel");
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add(TabIndex + " List");
                    int count = model.LocationList.Count + 6;


                       

                        string columnName = "";

                        int Columncount = 30;

                        while (Columncount > 0)
                        {
                            int modulo = (Columncount - 1) % 26;
                            columnName = Convert.ToChar('A' + modulo) + columnName;
                            Columncount = (Columncount - modulo) / 26;
                        }

                        string colName = columnName;




                        worksheet.Range("A1", colName+ count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
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
                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                    wsReportNameHeaderRange.Value = "Location" + "(" + TabIndex + ")";
                    worksheet.Range("A6",colName+6).Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
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
                    worksheet.Cell(5, 1).Value = "Total Count";
                    worksheet.Cell(5, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(5, 2).Value = model.LocationList.Count;




                    var currentRow = 6;
                    #region Header

                    worksheet.Cell(currentRow, 1).Value = "Sl.No";
                    worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 2).Value = "Name";
                    worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 3).Value = "Short Name";
                    worksheet.Cell(currentRow, 3).Style.Font.Bold = true;

                        worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 4).Value = "DoorNo & Building";

                        worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 5).Value = "Street";

                        worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 6).Value = "Area";

                        worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 7).Value = "Location type";

                        worksheet.Cell(currentRow, 8).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 8).Value = "Region";

                        worksheet.Cell(currentRow, 9).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 9).Value = "Country";

                        worksheet.Cell(currentRow, 10).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 10).Value = "State";

                        worksheet.Cell(currentRow, 11).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 11).Value = "District";

                        worksheet.Cell(currentRow, 12).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 12).Value = "Land Line";

                        worksheet.Cell(currentRow, 13).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 13).Value = "Email";

                        worksheet.Cell(currentRow, 14).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 14).Value = "Web Address";

                        worksheet.Cell(currentRow, 15).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 15).Value = "Lattitude";

                        worksheet.Cell(currentRow, 16).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 16).Value = "Longitude";

                        worksheet.Cell(currentRow, 17).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 17).Value = "Geo Address";

                        worksheet.Cell(currentRow, 18).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 18).Value = "PT Group";

                        worksheet.Cell(currentRow, 19).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 19).Value = "ESI Group";

                        //worksheet.Cell(currentRow, 20).Style.Font.Bold = true;
                        //worksheet.Cell(currentRow, 20).Value = "Mode";

                        worksheet.Cell(currentRow, 20).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 20).Value = "ERP Code";

                        worksheet.Cell(currentRow, 21).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 21).Value = "Functional State";

                        worksheet.Cell(currentRow, 22).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 22).Value = "Statutory Registration";


                        worksheet.Cell(currentRow, 23).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 23).Value = "Tax Employer";

                        worksheet.Cell(currentRow, 24).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 24).Value = "Tax Employee";

                        worksheet.Cell(currentRow, 25).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 25).Value = "Welfare Fund";

                        worksheet.Cell(currentRow, 26).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 26).Value = "License Number";

                        worksheet.Cell(currentRow, 27).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 27).Value = "LIN Number";

                        worksheet.Cell(currentRow, 28).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 28).Value = "SLN";
                        worksheet.Cell(currentRow, 29).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 29).Value = "Area Name";


                        #endregion
                        #region Body
                        foreach (var item in model.LocationList)
                    {
                        currentRow++;
                        slno = slno + 1;
                        worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 1).Value = slno;
                        worksheet.Cell(currentRow, 2).Value = item.Name;
                        worksheet.Cell(currentRow, 3).Value = item.ShortName;
                            worksheet.Cell(currentRow, 4).Value = item.DoorNumAndBuilding;
                            worksheet.Cell(currentRow, 5).Value = item.Street;
                            worksheet.Cell(currentRow, 6).Value = item.AreaName;
                            worksheet.Cell(currentRow, 7).Value = item.LocationType.Name;
                            worksheet.Cell(currentRow, 8).Value = item.Region.Name;
                            worksheet.Cell(currentRow, 9).Value = item.Country.Name;
                            worksheet.Cell(currentRow, 10).Value = item.State.Name;
                            worksheet.Cell(currentRow, 11).Value = item.District.Name;
                            worksheet.Cell(currentRow, 12).Value = item.LandLineNum;
                            worksheet.Cell(currentRow, 13).Value = item.EmailId;
                            worksheet.Cell(currentRow, 14).Value = item.WebAdderss;
                            worksheet.Cell(currentRow, 15).Value = item.Latitude;
                            worksheet.Cell(currentRow, 16).Value = item.Longitude;
                            worksheet.Cell(currentRow, 17).Value = item.GeoAdderss;
                            worksheet.Cell(currentRow, 18).Value = item.PTgroup.Name;
                            worksheet.Cell(currentRow, 19).Value = item.ESIgroup.Name;

                            //worksheet.Cell(currentRow, 20).Value = item.Mode;
                            worksheet.Cell(currentRow, 20).Value = item.ERPCode;
                            worksheet.Cell(currentRow, 21).Value = item.FunctionalState.Name;
                            worksheet.Cell(currentRow, 22).Value = item.StatutoryRegistration;
                            worksheet.Cell(currentRow, 23).Value = item.TaxEmployer;
                            worksheet.Cell(currentRow, 24).Value = item.TaxEmployee;
                            worksheet.Cell(currentRow, 25).Value = item.WelfareFund;
                            worksheet.Cell(currentRow, 26).Value = item.LicenseNumber;
                            worksheet.Cell(currentRow, 27).Value = item.LINNumber;
                            worksheet.Cell(currentRow, 28).Value = item.SLN;
                            worksheet.Cell(currentRow, 29).Value = item.AreaName;



                            worksheet.Columns(1, 29).Style.Alignment.WrapText = true;

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
                             "Location List.xlsx"
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


    }
}
