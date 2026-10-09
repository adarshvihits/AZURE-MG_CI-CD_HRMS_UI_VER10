using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.TrainingModule;
using Hrms.Lite.Shared.Account;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TrainingModule;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Spire.Xls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.TrainingModule.Controllers
{
    [Area("TrainingModule")]
    [SessionAuthorize]
    public class TrnParticipantSelectionController : Controller
    {

        private readonly IDropdownService _dropdownService;
        private readonly ICommonService _commonService;
        private readonly ITrnParticipantService _trnParticipantService;
        private readonly IHelpers _helpers;

        public TrnParticipantSelectionController(ITrnParticipantService trnParticipantService, IDropdownService dropdownService, ICommonService commonService, IHelpers helpers)
        {
            _trnParticipantService = trnParticipantService;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = helpers;

        }
        public async Task<IActionResult> Index()
        {

            TrnParticipantSelection model = new TrnParticipantSelection();
            await fillfilterDropdown();
            ViewBag.Schedule = new SelectList(await _dropdownService.GetTrnScheduleDropdown(), "Code", "Name");
           

            //model = await _trnParticipantService.GetList(Month, filter);
            //model.Filter = filter;
            //return View("~/Areas/PayRoll/Reports/Views/SalaryReleaseReport/Index.cshtml", model);
            model.Filter = new Filter();
            return View(model);
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
        public async Task<ActionResult> Save(int ScheduleCode, TrnParticipantSelection model)
        {
            try
            {
                //TrnParticipantSelection model = new TrnParticipantSelection();

                var response = await _trnParticipantService.Save(ScheduleCode, model);

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

        public async Task<ActionResult> GetList(int ScheduleCode,Filter filter)
        {
            try
            {
                TrnParticipantSelection model = new TrnParticipantSelection();
            


                if (filter.searchValue == "undefined" || filter.searchValue == null)
                {
                    filter.searchValue = "";
                }
                if (ScheduleCode != 0)
                {
                    model = await _trnParticipantService.GetScheduleList(ScheduleCode, filter);
                }
                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.TrnParticipantList == null || model.TrnParticipantList.Count() == 0)
                {
                    return Json(new
                    {
                        data = "",
                        draw = filter.draw,
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows
                    });
                }

                if (model.TrnParticipantList.Count > 0)
                {
                    totalrows = model.TrnParticipantList[0].active;

                }
                if (filter.searchValue != "")
                {
                    totalrowsafterfiltering = model.TrnParticipantList[0].totalRecords;
                    return Json(new
                    {
                        data = model.TrnParticipantList,
                        draw = filter.draw,
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows
                    });
                }
                else
                {
                    return Json(new
                    {
                        data = model.TrnParticipantList,
                        draw = filter.draw,
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows
                    });
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

        public async Task<ActionResult> GetFilteredList(int ScheduleCode, Filter filter)
        {
            try
            {
                TrnParticipantSelection model = new TrnParticipantSelection();

                model = await _trnParticipantService.GetFilteredList(ScheduleCode, filter);

                return PartialView("~/Areas/TrainingModule/Views/TrnParticipantSelection/_filteredList.cshtml", model);
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

       public async Task<ActionResult> DeleteAll(int ScheduleCode)
        {
            try
            {
                var response = await _trnParticipantService.DeleteAll(ScheduleCode);

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
        public async Task<ActionResult> DeleteParticipant(Guid EmployeeGI, int ScheduleCode)
        {
            try
            {
                var response = await _trnParticipantService.DeleteParticipant(EmployeeGI, ScheduleCode);

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
        public async Task<ActionResult> schedulefilter()
        {
            try
            {
                //TrnParticipantSelection model = new TrnParticipantSelection();
                Filter filter = new Filter();
                await fillfilterDropdown();
                return PartialView("~/Areas/TrainingModule/Views/TrnParticipantSelection/_Filter.cshtml", filter);
                
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

        public async Task<IActionResult> ExportToExcel(int ScheduleCode,  Filter filter,int Length)
        {
            try
            {
                TrnParticipantSelection model = new TrnParticipantSelection();
                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                Filter filterdata = new Filter();
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Participant Selection List", "Excel");
                int slNo = 0;
                filter.length = Length;

                string FilterValues = await _helpers.GetFilterValues(filter);
                model = await _trnParticipantService.GetScheduleList(ScheduleCode, filter);

                if (FilterValues == "")
                {
                    FilterValues = "All";
                }
                string currDateTime = await _helpers.GetCurrentDate();
                

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Participant Selection List");
                    int count = model.TrnParticipantList.Count + 5;


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
                    wsReportNameHeaderRange.Value = "Participant Selection List";

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
                    worksheet.Cell(3, 6).Value = model.TrnParticipantList.Count;
                    worksheet.Cell(4, 1).Style.Font.Bold = true;
                    worksheet.Cell(4, 1).Value = "Filter Applied";
                    worksheet.Range("B4:H4").Merge();
                    worksheet.Cell("B4").Value = FilterValues;





                    var currentRow = 5;
                    #region Header
                    worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 1).Value = "Sl no";
                    worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 2).Value = "ID";
                    worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 3).Value = "Candidate Name";
                    worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 4).Value = "Designation";
                    worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 5).Value = "Department";
                    worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 6).Value = "Location";
                    worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 7).Value = "E:DOJ";
                    worksheet.Cell(currentRow, 8).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 8).Value = "Status";
                    //worksheet.Cell(currentRow, 8).Style.Font.Bold = true;
                    //worksheet.Cell(currentRow, 8).Value = "Mobile";
                    //worksheet.Cell(currentRow, 9).Style.Font.Bold = true;
                    //worksheet.Cell(currentRow, 9).Value = "Contact Details";
                    //worksheet.Cell(currentRow, 10).Style.Font.Bold = true;
                    //worksheet.Cell(currentRow, 10).Value = "Personal Info";
                    //worksheet.Cell(currentRow, 11).Style.Font.Bold = true;
                    //worksheet.Cell(currentRow, 11).Value = "Family Details";
                    //worksheet.Cell(currentRow, 12).Style.Font.Bold = true;
                    //worksheet.Cell(currentRow, 12).Value = "Educ";
                    //worksheet.Cell(currentRow, 13).Style.Font.Bold = true;
                    //worksheet.Cell(currentRow, 13).Value = "Skills";
                    //worksheet.Cell(currentRow, 14).Style.Font.Bold = true;
                    //worksheet.Cell(currentRow, 14).Value = "Certifications";
                    //worksheet.Cell(currentRow, 15).Style.Font.Bold = true;
                    //worksheet.Cell(currentRow, 15).Value = "Previous Employment";
                    #endregion

                    #region Body
                    foreach (var item in model.TrnParticipantList)
                    {
                        currentRow++;
                        slNo = slNo + 1;
                        worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Range("A5:H5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                        worksheet.Cell(currentRow, 1).Value = slNo;
                        worksheet.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 2).Value = item.EmployeeID;
                        worksheet.Cell(currentRow, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 3).Value = item.EmployeeName;
                        worksheet.Cell(currentRow, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 4).Value = item.Designation;
                        worksheet.Cell(currentRow, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 5).Value = item.Department;
                        worksheet.Cell(currentRow, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 6).Value = item.Location;
                        worksheet.Cell(currentRow, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 7).Style.DateFormat.Format = "dd-MMM-yyyy";
                        worksheet.Cell(currentRow, 7).Value = item.DOJ;
                        worksheet.Cell(currentRow, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 8).Style.DateFormat.Format = "dd-MMM-yyyy";
                        worksheet.Cell(currentRow, 8).Value = item.TrnStatus;
                        //worksheet.Cell(currentRow, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        //worksheet.Cell(currentRow, 8).Value = item.Mobile;
                        //worksheet.Cell(currentRow, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        //worksheet.Cell(currentRow, 8).Value = (item.Addr == true) ? "" : "AP";
                        //worksheet.Cell(currentRow, 9).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        //worksheet.Cell(currentRow, 9).Value = (item.Cont == true) ? "" : "AP";
                        //worksheet.Cell(currentRow, 10).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        //worksheet.Cell(currentRow, 10).Value = (item.Pers == true) ? "" : "AP";
                        //worksheet.Cell(currentRow, 11).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        //worksheet.Cell(currentRow, 11).Value = (item.Fmly == true) ? "" : "AP";
                        //worksheet.Cell(currentRow, 12).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        //worksheet.Cell(currentRow, 12).Value = (item.Educ == true) ? "" : "AP";
                        //worksheet.Cell(currentRow, 13).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        //worksheet.Cell(currentRow, 13).Value = (item.Skills == true) ? "" : "AP";
                        //worksheet.Cell(currentRow, 14).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        //worksheet.Cell(currentRow, 14).Value = (item.Cert == true) ? "" : "AP";
                        //worksheet.Cell(currentRow, 15).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        //worksheet.Cell(currentRow, 15).Value = (item.Prev == true) ? "" : "AP";

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
                            Workbook book = new Spire.Xls.Workbook();
                            book.LoadFromStream(stream);
                            //Protect Workbook
                            book.Protect(EP.Password, false, false);
                            book.SaveToStream(stream);
                        }
                        var content = stream.ToArray();
                        return File(
                            content,
                            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                            "Participant Selection List.xlsx"
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
