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
    public class MedicalCheckupController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly IMedicalCheckupService _MedicalCheckupService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        private readonly IBlob _blob;
        public MedicalCheckupController(IMedicalCheckupService MedicalCheckupService, IDropdownService dropdownService, ICommonService commonService, IHelpers Helpers,IBlob blob)
        {
            _MedicalCheckupService = MedicalCheckupService;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = Helpers;
            _blob = blob;
        }
        public static class Global
        {

            public static Filter GlobalFilter;
            public static int Type;


        }
        public async Task <IActionResult> Index()
        {
            try { 
            MedicalCheckup m1 = new MedicalCheckup();
            await fillfilterDropdown();
            Filter filter = new Filter();
            filter.length = 10;
            m1 = await _MedicalCheckupService.GetMedicalCheckupList(5, "Not_Done", filter);
            MedicalCheckup model = new MedicalCheckup();
            ViewBag.MedCheckup = new SelectList(await _dropdownService.GetCheckUpTypeDropDown(), "Code", "Name");
            model.EmployeeHeader = new Shared.General.EmployeeHeader();
            model.EmployeeHeader.Department = new Shared.Master.Department();
            model.EmployeeHeader.Designation = new Shared.Master.Designation();
            model.EmployeeHeader.Location = new Shared.Master.Location();
            model.EmployeeHeader.Category = new Shared.Master.Category();
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

        public async Task<IActionResult> ApplyFilter(Filter filter, string TabInd)
        {

            MedicalCheckup model = new MedicalCheckup();
            Global.GlobalFilter = filter;
            model = await _MedicalCheckupService.GetMedicalCheckupList(Global.Type, TabInd,Global.GlobalFilter);
            //return PartialView("~/Areas/DataBank/Views/AssetAllocation/_Filter.cshtml");
            if (TabInd == "Done")
                return PartialView("_CheckupDoneList", model);
            else
                return PartialView("_CheckUpNotDoneList", model);

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


        [HttpPost]
        public async Task<IActionResult> GetList(Filter filter1,int Type,string TabIndex)
        {
            try
            {
                MedicalCheckup model = new MedicalCheckup();
                MedicalCheckup m = new MedicalCheckup();
                model.EmployeeHeader = new EmployeeHeader();


                Filter filter = new Filter();
                Global.GlobalFilter = filter1;


                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }
                model = await _MedicalCheckupService.GetMedicalCheckupList(Type, TabIndex, filter1);
                
                m.MedicalCheckupList = model.MedicalCheckupList;
                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.MedicalCheckupList == null)
                {



                    return Json(new
                    {
                        data = "",
                        draw = filter1.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows
                    });
                }


                if (model.MedicalCheckupList.Count > 0)
                {
                    totalrows = m.MedicalCheckupList[0].active;

                  
                }

               
                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = m.MedicalCheckupList[0].totalRecords;
              
                    return Json(new
                    {
                        data = model.MedicalCheckupList,
                        draw = filter1.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows//totalrowsafterfiltering
                    });
                }
                else
                {
                    
                    return Json(new
                    {
                        data = model.MedicalCheckupList,
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


        public async Task<IActionResult> GetMedicalCheckupList(int Type, string TabIndex)
        {
            try
            {
                MedicalCheckup model = new MedicalCheckup();
                Global.Type = Type;
                Filter filter = new Filter();
                if (TabIndex == "Done")
                    return PartialView("_CheckupDoneList");
                else
                    return PartialView("_CheckUpNotDoneList");
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


        public async Task<IActionResult> Create(int Type,string TypeName, Guid EmpGI,Guid MedicalGI)
        {
            try
            {
                MedicalCheckup model = new MedicalCheckup();
                model.EmployeeHeader = new Shared.General.EmployeeHeader();
                model.EmployeeHeader.Department = new Shared.Master.Department();
                model.EmployeeHeader.Designation = new Shared.Master.Designation();
                model.EmployeeHeader.Location = new Shared.Master.Location();
                model.EmployeeHeader.Category = new Shared.Master.Category();
                model.EmployeeGI = EmpGI;
                model.MedicalCheckupGI = MedicalGI;
                model.CheckupType = new Shared.General.MasterBase();
                model.CheckupType.Code = Type;
                model.CheckupType.Name = TypeName;
                model.EmployeeHeader = await _commonService.GetEmployeeHeader(EmpGI);
                return PartialView("_Create", model);
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
        public async Task<ActionResult> Save(MedicalCheckup input)
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
                //    if (file1 != null) /*if (model.File.FileData != null)*/
                //    {
                //        //var image = Image.FromStream(model.File.FileData.OpenReadStream());
                //        //var resized = new Bitmap(image, new Size(256, 256));
                //        //using var imageStream = new MemoryStream();
                //        //resized.Save(imageStream, ImageFormat.Jpeg);
                //        //var imageBytes = imageStream.ToArray();

                //        //var FileData = model.File.FileData;
                //        input.Attachment = await _blob.UploadToAzureAsync(input.Attachment);
                //        string filename = input.Attachment.FileName;
                //        input.Attachment.FileData = null;
                //    }
                var response = await _MedicalCheckupService.Save(input);
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
       
        public async Task <IActionResult> Delete(int Type,Guid EmpGI,Guid MedicalGI)
        {
            try
            {
                MedicalCheckup model = new MedicalCheckup();
                model.EmployeeHeader = new Shared.General.EmployeeHeader();
                model.EmployeeHeader.Department = new Shared.Master.Department();
                model.EmployeeHeader.Designation = new Shared.Master.Designation();
                model.EmployeeHeader.Location = new Shared.Master.Location();
                model.EmployeeHeader.Category = new Shared.Master.Category();
                model = await _MedicalCheckupService.GetMedicalCheckUpDetails(EmpGI, MedicalGI);
                model.EmployeeHeader = await _commonService.GetEmployeeHeader(EmpGI);
                model.MedicalCheckupGI = MedicalGI;
                return PartialView("_DeleteCheckUp", model);
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

        public async Task<ActionResult> DeleteMedicalCheckUp(Guid MedicalGI)
        {
            try
            {
                MedicalCheckup model = new MedicalCheckup();
                var response = await _MedicalCheckupService.Delete(MedicalGI);
                if (response.Success == true)
                {
                    TempData["Message"] = "Deleted Successfully";
                    return RedirectToAction("Index", "MedicalCheckup");
                }
                return View("Delete", model);
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


        public async Task<IActionResult> ExportToExcel(string TabIndex, int Type, string MedType, int totRecords)
        {
            try
            {

                if (Global.GlobalFilter == null)
                    Global.GlobalFilter = new Filter();
                else
                {
                    Global.GlobalFilter.length = totRecords;
                    Global.GlobalFilter.start = 0;
                }
                string FilterValues = await _helpers.GetFilterValues(Global.GlobalFilter);

                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Medical CheckUp (" + TabIndex + ")", "Excel");

                if (FilterValues == "")
                {
                    FilterValues = "All";
                }

                string currDateTime = await _helpers.GetCurrentDate();
                

                //Get Datset
                var dataset = await _MedicalCheckupService.GetMedicalCheckupList_Excel(TabIndex,Type, Global.GlobalFilter);
                DataTable dt = dataset.Tables[0];


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
                    var worksheet = workbook.Worksheets.Add("Medical CheckUp(" + TabIndex + ")");
                    int count = dt.Rows.Count + 5;
                    if(TabIndex=="Done")
                    {
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
                        wsReportNameHeaderRange.Value = "Medical CheckUp (" + TabIndex + ")";

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
                        wsReportNameHeaderRange.Value = "Medical CheckUp (" + TabIndex + ")";

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
                   if(TabIndex=="Done")
                    {
                        worksheet.Range("B4:J4").Merge();
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

                    if(TabIndex=="Done")
                    {
                        worksheet.Range("A5:J5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                        worksheet.Range("A5:J5").Style.Font.Bold = true;
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
                            "Medical checkup " + TabIndex + " List.xlsx"
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
