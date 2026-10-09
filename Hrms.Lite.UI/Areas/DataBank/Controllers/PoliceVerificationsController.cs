using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.Account;
using Hrms.Lite.Shared.DataBank;
using Hrms.Lite.Shared.General;
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

namespace Hrms.Lite.UI.Areas.DataBank.Controllers
{
    [Area("DataBank")]
    [SessionAuthorize]
    public class PoliceVerificationsController : Controller
    {
       
        private readonly IPoliceVerificationsService _PoliceVerificationService;
        private readonly ICommonService _commonService;
        private readonly IDropdownService _dropdownService;
        private readonly IHelpers _helpers;
        private readonly IBlob _blob;
        public PoliceVerificationsController(IPoliceVerificationsService PoliceVerificationService, ICommonService commonService, IDropdownService dropdownService, IHelpers Helpers, IBlob blob)
        {
            _PoliceVerificationService = PoliceVerificationService;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = Helpers;
            _blob = blob;
        }
       
        public async Task<IActionResult> Index()
        {
            try{
            PoliceVerification model = new PoliceVerification();
            model.Filter = await _commonService.GetFilterDropdowns();
            Filter filter = new Filter();
            filter.length = 10;
            model.TabIndex = "Not Done";
            model = await _PoliceVerificationService.GetList(model.TabIndex, 0, filter);
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
       
        public async Task<IActionResult> ApplyFilter(Filter filter ,string TabInd)
        {
            
            PoliceVerification model = new PoliceVerification();
            model.Filter = filter;
            model = await _PoliceVerificationService.GetList(TabInd, 0, filter);
            //return PartialView("~/Areas/DataBank/Views/AssetAllocation/_Filter.cshtml");
            if (TabInd == "Done")

                return PartialView("_VerificationDoneList", model);

            else
                return PartialView("_VerificationNotDoneList", model);

        }




        [HttpPost]
        public async Task<IActionResult> GetVerificationList(Filter filter1,string TabIndex)
        {
            try
            {
                PoliceVerification model = new PoliceVerification();
                PoliceVerification m = new PoliceVerification();
                model.EmployeeHeader = new EmployeeHeader();




                Filter filter = new Filter();
                model.Filter = filter1;


                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }
                model = await _PoliceVerificationService.GetList(TabIndex, 0, filter1);

               

                m.PoliceVerificationList = model.PoliceVerificationList;
                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.PoliceVerificationList == null)
                {
                    


                    return Json(new
                    {
                        data = "",
                        draw = filter1.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows
                    });
                }




                if (model.PoliceVerificationList.Count > 0)
                {
                    totalrows = m.PoliceVerificationList[0].active;

                    //m.OnBoardingList.ToList<Hrms.Lite.Shared.Master.Test>().Count; ;
                }

                //model.AllTabCount = m.AllTabCount;
                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = m.PoliceVerificationList[0].totalRecords;
                    return Json(new
                    {
                        data = model.PoliceVerificationList,
                        draw = filter1.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows//totalrowsafterfiltering
                    });
                }
                else
                {
                    return Json(new
                    {
                        data = model.PoliceVerificationList,
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


        public async Task<IActionResult>GetList(string TabIndex)
        {
            try

            {
                PoliceVerification model = new PoliceVerification();
                Filter filter = new Filter();
                //model = await _PoliceVerificationService.GetList(TabIndex, 0,filter);
            if (TabIndex=="Done")
            
                return PartialView("_VerificationDoneList");
            
            else
            return PartialView("_VerificationNotDoneList");
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


        public async Task<IActionResult> Create( Guid EmpGI, Guid VerificationGI)
        {
            try
            {
                
                PoliceVerification model = new PoliceVerification();
                model.EmployeeHeader = new Shared.General.EmployeeHeader();
                model.EmployeeHeader.Department = new Shared.Master.Department();
                model.EmployeeHeader.Designation = new Shared.Master.Designation();
                model.EmployeeHeader.Location = new Shared.Master.Location();
                model.EmployeeHeader.Category = new Shared.Master.Category();
                model.EmployeeHeader.EmployeeGuid = EmpGI;
                model.PoliceVerificationGI = VerificationGI;
                //model.CheckupType = new Shared.General.MasterBase();
                //model.CheckupType.Code = Type;
                model.EmployeeHeader = await _commonService.GetEmployeeHeader(EmpGI);
                model.EmployeeGI = EmpGI;
                ViewBag.Cntry = new SelectList(await _dropdownService.GetCountryDropDown(), "Code", "Name");
                ViewBag.St = new SelectList(await _dropdownService.GetStateDropDown(0), "Code", "Name");
                ViewBag.Dist = new SelectList(await _dropdownService.GetDistrictDropdown(0), "Code", "Name");

                return View("~/Areas/DataBank/Views/PoliceVerifications/AddVerificationDetials.cshtml", model);
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
        public async Task<ActionResult> Save(PoliceVerification input)
        {
            Shared.File file1 = new Shared.File();
            file1 = input.Attachment;

            var StorageType = await _commonService.GetFileStorageType();

            try
            {
                if (input.Attachment != null)
                {
                    if (input.Attachment.FileData != null)
                    {
                        if (StorageType.Data == "BLOB")
                        {

                            var fileobj = await _commonService.UploadIformFileToBLOB(input.Attachment, "Documents");
                            if (fileobj.Success == false)
                            {
                                fileobj.Message = "FILE UPLOAD ERROR";
                                return Json(fileobj);
                            }
                            input.Attachment = fileobj.Data;

                        }
                        else if (StorageType.Data == "IIS")
                        {
                            var fileobj = await _commonService.UploadIformFileToIIS(input.Attachment, "Documents");
                            if (fileobj.Success == false)
                            {
                                fileobj.Message = "FILE UPLOAD ERROR";
                                return Json(fileobj);
                            }
                            input.Attachment = fileobj.Data;

                        }
                        else
                        {
                            var fileobj = await _commonService.UploadIformFileToAWS(input.Attachment, "Documents");
                            if (fileobj.Success == false)
                            {
                                fileobj.Message = "FILE UPLOAD ERROR";
                                return Json(fileobj);
                            }
                            input.Attachment = fileobj.Data;

                        }
                    }
                }

                //try
                //{
                //    if (file1 != null) 
                //    {

                //        input.Attachment = await _blob.UploadToAzureAsync(input.Attachment);
                //        string filename = input.Attachment.FileName;
                //        input.Attachment.FileData = null;
                //    }
                var response = await _PoliceVerificationService.Save(input);
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


        public async Task<IActionResult> GetDeleteDetails( Guid EmpGI, Guid VerificationGI)
        {
            try
            {
                PoliceVerification model = new PoliceVerification();
                model.EmployeeHeader = new Shared.General.EmployeeHeader();
                model.EmployeeHeader.Department = new Shared.Master.Department();
                model.EmployeeHeader.Designation = new Shared.Master.Designation();
                model.EmployeeHeader.Location = new Shared.Master.Location();
                model.EmployeeHeader.Category = new Shared.Master.Category();
                model = await _PoliceVerificationService.GetVerificationDetails(VerificationGI, EmpGI);
                model.EmployeeHeader = await _commonService.GetEmployeeHeader(EmpGI);
                model.PoliceVerificationGI = VerificationGI;
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

        public async Task<ActionResult> Delete(Guid VerificationGI)
        {
            try
            {
                PoliceVerification model = new PoliceVerification();
                var response = await _PoliceVerificationService.Delete(VerificationGI);
                if (response.Success == true)
                {
                    TempData["Message"] = "Deleted Successfully";
                    return RedirectToAction("Index", "PoliceVerifications");
                }
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
        public async Task<IActionResult> ExportToExcel(string TabIndex, int totRecords, Filter filter)
        {
            try
            {

                filter.length = totRecords;
                filter.start = 0;
                string FilterValues = await _helpers.GetFilterValues(filter);

                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Police Verification (" + TabIndex + ")", "Excel");

                if (FilterValues == "")
                {
                    FilterValues = "All";
                }

                string currDateTime = await _helpers.GetCurrentDate();
                

                //Get Datset
                var dataset = await _PoliceVerificationService.GetPoliceVerificationList_Excel(TabIndex, 0, filter);
                DataTable dt = dataset.Tables[0];
                dataset.Tables[0].Rows.RemoveAt(0);
                if ((dataset.Tables[0]?.Rows?.Count ?? 0) > 0)
                {
                    dt = dataset.Tables[0];
                }
                


                //set EXCEL Column Heading 
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (dt.Columns[i].ColumnName == "doj" || dt.Columns[i].ColumnName == "dob" || dt.Columns[i].ColumnName == "y/s")
                        dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.ToUpper();
                    else
                        dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
                }

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Police Verification(" + TabIndex + ")");
                    int count = dt.Rows.Count + 5;
                    if (TabIndex == "Done")
                    {
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
                        wsReportNameHeaderRange.Value = "Police Verification(" + TabIndex + ")";

                    }
                    else
                    {
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
                        wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                        wsReportNameHeaderRange.Value = "Police Verification  (" + TabIndex + ")";

                    }


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
                    if (TabIndex == "Done")
                    {
                        worksheet.Range("B4:H4").Merge();
                    }
                    else
                    {
                        worksheet.Range("B4:F4").Merge();
                    }
                    worksheet.Cell("B4").Value = FilterValues;



                    var currentRow = 5;

                    worksheet.Cell(currentRow, 1).InsertTable(dt);
                    worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                    worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;

                    if (TabIndex == "Done")
                    {
                        worksheet.Range("A5:H5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                        worksheet.Range("A5:H5").Style.Font.Bold = true;
                        worksheet.Range("G5:G" + (dt.Rows.Count + 5)).Style.DateFormat.Format = "dd-MMM-yyyy";
                    }
                    else
                    {
                        worksheet.Range("A5:F5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                        worksheet.Range("A5:F5").Style.Font.Bold = true;
                    }



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
                            "Police verification " + TabIndex + " List.xlsx"
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


        //public async Task<IActionResult> ExportToExcel(string TabIndex, int totRecords)
        //{
        //    try
        //    {
        //        PoliceVerification model = new PoliceVerification();
        //        Filter filter = new Filter();
        //        if (Global.GlobalFilter == null)
        //            Global.GlobalFilter = new Filter();
        //        else
        //        {
        //            Global.GlobalFilter.length = totRecords;
        //            Global.GlobalFilter.start = 0;
        //        }
        //        string FilterValues = await _helpers.GetFilterValues(Global.GlobalFilter);

        //        model = await _PoliceVerificationService.GetList(TabIndex, 0, Global.GlobalFilter);
        //        int slno = 0;
        //        string currDateTime = await _helpers.GetCurrentDate();
        //        
        //        var LoginUser = HttpContext.Session.GetString("LoginUser");
        //        var CompanyName = HttpContext.Session.GetString("CompanyName");

        //        ExcelPassword EP = new ExcelPassword();
        //        EP = await _commonService.GetExcelPassword("Polioce Verification (" + TabIndex + ")", "Excel");
        //        if (FilterValues == "")
        //        {
        //            FilterValues = "All";
        //        }

        //        if (TabIndex == "Not Done")
        //        {

        //            using (var workbook = new XLWorkbook())
        //            {
        //                var worksheet = workbook.Worksheets.Add("Not Done List");
        //                int PendingCountnum = model.PendingCount + 5;


        //                worksheet.Range("A1:E" + PendingCountnum).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
        //                worksheet.Range("A1:E" + PendingCountnum).Style.Border.TopBorder = XLBorderStyleValues.Thin;
        //                worksheet.Range("A1:E" + PendingCountnum).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
        //                worksheet.Range("A1:E" + PendingCountnum).Style.Border.RightBorder = XLBorderStyleValues.Thin;
        //                worksheet.Range("A1:E" + PendingCountnum).Style.Border.LeftBorder = XLBorderStyleValues.Thin;



        //                var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 4)));
        //                wsReportNameHeaderRange.Style.Font.Bold = true;
        //                wsReportNameHeaderRange.Style.Font.FontSize = 15;
        //                wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        //                wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
        //                wsReportNameHeaderRange.Merge();


        //                wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
        //                wsReportNameHeaderRange.Style.Font.FontSize = 15;
        //                wsReportNameHeaderRange.Value = "Police Verification " + "(" + TabIndex + ")";
        //                worksheet.Range("A5:E5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
        //                worksheet.Cell(2, 1).Style.Font.Bold = true;
        //                worksheet.Cell(2, 1).Value = "Company Name";
        //                worksheet.Cell(2, 2).Value = CompanyName.ToUpper();
        //                worksheet.Cell(3, 1).Style.Font.Bold = true;
        //                worksheet.Cell(3, 1).Value = "Login User";
        //                worksheet.Cell(3, 2).Value = LoginUser;
        //                worksheet.Cell(3, 3).Style.Font.Bold = true;
        //                worksheet.Cell(3, 3).Value = "Date & Time";
        //                worksheet.Cell(3, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //                worksheet.Cell(3, 4).Style.DateFormat.Format = "dd-MMM-yyyy   h:mm AM/PM";
        //                worksheet.Cell(3, 4).Value = currDateTime.ToString();
        //                //worksheet.Cell(3, 5).Style.Font.Bold = true;
        //                //worksheet.Cell(3, 5).Value = "Total Count";
        //                //worksheet.Cell(3, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //                //worksheet.Cell(3, 6).Value = model.PendingCount;
        //                worksheet.Cell(4, 1).Style.Font.Bold = true;
        //                worksheet.Cell(4, 1).Value = "Filter Applied";
        //                worksheet.Range("B4:E4").Merge();
        //                worksheet.Cell("B4").Value = FilterValues;
        //                var currentRow = 5;






        //                #region Header
        //                worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
        //                worksheet.Cell(currentRow, 1).Value = "Sl.No";
        //                worksheet.Cell(currentRow, 2).Value = "Employee Name";
        //                worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
        //                worksheet.Cell(currentRow, 3).Value = "Designation";
        //                worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
        //                worksheet.Cell(currentRow, 4).Value = "Department";
        //                worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
        //                worksheet.Cell(currentRow, 5).Value = "Location";
        //                worksheet.Cell(currentRow, 5).Style.Font.Bold = true;





        //                #endregion

        //                #region Body
        //                foreach (var item in model.PoliceVerificationList)
        //                {
        //                    currentRow++;
        //                    slno = slno + 1;
        //                    worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //                    worksheet.Cell(currentRow, 1).Value = slno;
        //                    worksheet.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //                    worksheet.Cell(currentRow, 2).Value = item.EmployeeName;
        //                    worksheet.Cell(currentRow, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //                    worksheet.Cell(currentRow, 3).Value = item.Designation;
        //                    worksheet.Cell(currentRow, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //                    worksheet.Cell(currentRow, 4).Value = item.Department;
        //                    worksheet.Cell(currentRow, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //                    worksheet.Cell(currentRow, 5).Value = item.Location;
        //                    worksheet.Columns(1, 5).Style.Alignment.WrapText = true;
        //                }







        //                #endregion
        //                worksheet.Columns().AdjustToContents();
        //                using (var stream = new MemoryStream())
        //                {
        //                    workbook.SaveAs(stream);
        //                    if (EP.NeedPassword)
        //                    {
        //                        Workbook book = new Workbook();
        //                        book.LoadFromStream(stream);
        //                        //Protect Workbook
        //                        book.Protect(EP.Password,false,false);
        //                        book.SaveToStream(stream);
        //                    }
        //                    var content = stream.ToArray();
        //                    return File(
        //                        content,
        //                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
        //                        "Police Verification List.xlsx"
        //                        );
        //                }
        //            }



        //        }

        //        else
        //        {

        //            using (var workbook = new XLWorkbook())
        //            {
        //                var worksheet = workbook.Worksheets.Add("Police Verification Done List");
        //                int count = model.PoliceVerificationList.Count + 7;


        //                worksheet.Range("A1:H" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
        //                worksheet.Range("A1:H" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
        //                worksheet.Range("A1:H" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
        //                worksheet.Range("A1:H" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
        //                worksheet.Range("A1:H" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
        //                var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 7)));
        //                wsReportNameHeaderRange.Style.Font.Bold = true;
        //                wsReportNameHeaderRange.Style.Font.FontSize = 15;
        //                wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        //                wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
        //                wsReportNameHeaderRange.Merge();
        //                wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
        //                wsReportNameHeaderRange.Value = "Police Verification" + "(" + TabIndex + ")";
        //                worksheet.Range("A6:H6").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
        //                worksheet.Cell(2, 1).Style.Font.Bold = true;
        //                worksheet.Cell(2, 1).Value = "Company Name";
        //                worksheet.Cell(2, 2).Value = CompanyName.ToUpper();
        //                worksheet.Cell(3, 1).Style.Font.Bold = true;
        //                worksheet.Cell(3, 1).Value = "Login User";
        //                worksheet.Cell(3, 2).Value = LoginUser;
        //                worksheet.Cell(4, 1).Style.Font.Bold = true;
        //                worksheet.Cell(4, 1).Value = "Date & Time";
        //                worksheet.Cell(4, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //                worksheet.Cell(4, 2).Style.DateFormat.Format = "dd-MMM-yyyy   h:mm AM/PM";
        //                worksheet.Cell(4, 2).Value = currDateTime.ToString();
        //                worksheet.Cell(5, 1).Style.Font.Bold = true;
        //                worksheet.Cell(5, 1).Value = "Total Count";
        //                worksheet.Cell(5, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //                worksheet.Cell(5, 2).Value = model.PoliceVerificationList.Count;

        //                var currentRow = 6;
        //                #region Header
        //                worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
        //                worksheet.Cell(currentRow, 1).Value = "Sl.No";
        //                worksheet.Cell(currentRow, 2).Value = "ID";
        //                worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
        //                worksheet.Cell(currentRow, 3).Value = "Employee Name";
        //                worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
        //                worksheet.Cell(currentRow, 4).Value = "Designation";
        //                worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
        //                worksheet.Cell(currentRow, 5).Value = "Department";
        //                worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
        //                worksheet.Cell(currentRow, 6).Value = "Location";
        //                worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
        //                worksheet.Cell(currentRow, 7).Value = "Date Of Verification";
        //                worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
        //                worksheet.Cell(currentRow, 8).Value = "Police Station Details";
        //                worksheet.Cell(currentRow, 8).Style.Font.Bold = true;







        //                #endregion

        //                #region Body
        //                foreach (var item in model.PoliceVerificationList)
        //                {
        //                    currentRow++;
        //                    slno = slno + 1;
        //                    worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //                    worksheet.Cell(currentRow, 1).Value = slno;
        //                    worksheet.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //                    worksheet.Cell(currentRow, 2).Value = item.EmployeeID;

        //                    worksheet.Cell(currentRow, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //                    worksheet.Cell(currentRow, 3).Value = item.EmployeeName;

        //                    worksheet.Cell(currentRow, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //                    worksheet.Cell(currentRow, 4).Value = item.Designation;
        //                    worksheet.Cell(currentRow, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //                    worksheet.Cell(currentRow, 5).Value = item.Department;
        //                    worksheet.Cell(currentRow, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //                    worksheet.Cell(currentRow, 6).Value = item.Location;
        //                    worksheet.Cell(currentRow, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //                    worksheet.Cell(currentRow, 7).Style.DateFormat.Format = "dd-MMM-yyyy";
        //                    worksheet.Cell(currentRow, 7).Value = item.VerificationDate;
        //                    worksheet.Cell(currentRow, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //                    worksheet.Cell(currentRow, 8).Value = item.PoliceStationDetails;
        //                    worksheet.Columns(1, 8).Style.Alignment.WrapText = true;

        //                }
        //                #endregion
        //                worksheet.Columns().AdjustToContents();
        //                using (var stream = new MemoryStream())
        //                {
        //                    workbook.SaveAs(stream);
        //                    if (EP.NeedPassword)
        //                    {
        //                        Workbook book = new Workbook();
        //                        book.LoadFromStream(stream);
        //                        //Protect Workbook
        //                        book.Protect(EP.Password,false,false);
        //                        book.SaveToStream(stream);
        //                    }
        //                    var content = stream.ToArray();
        //                    return File(
        //                        content,
        //                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
        //                        "Police Verification Done List.xlsx"
        //                        );
        //                }
        //            }
        //        }
        //    }
        //    catch (UnauthorizedAccessException e)
        //    {

        //        return RedirectToAction("Login", "Account", new { Area = "" });
        //    }
        //    catch (Exception e)
        //    {
        //        return RedirectToAction("Error", "Account", new { Area = "" });
        //    }
        //}



    }
}
