using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.General;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Spire.Xls;
using Newtonsoft.Json;
using Hrms.Lite.Shared.Account;

namespace Hrms.Lite.UI.Areas.DataBank.Controllers
{
    [Area("DataBank")]
    public class JobTransitionDueListController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly IJobTransitionService _jobTransitionService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        public static class Global
        {

            public static Filter GlobalFilter;

        }
        public JobTransitionDueListController(IDropdownService dropdownService, ICommonService commonService, IJobTransitionService jobTransitionService, IHelpers ihelper)
        {
            _jobTransitionService = jobTransitionService;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = ihelper;

        }
        public async Task<IActionResult> Index(int TabIndex = 0)
        {
            try { 
            JobTransition model = new JobTransition();
            JobTransition _model = new JobTransition();
            model.Filter = new FilterSlabComboFill();
            Filter filter = new Filter();
            //int TabIndex = 0;
            //ViewBag.Filter = new SelectList(await _commonService.GetDurationWiseFilterSlabDropDown(), "Code", "Name");
            model.Filter.Code = 3;
            int SearchId = 1;
            model.JobTransition_Tabs = (List<JobTransition_Tabs>)await _jobTransitionService.GetJobTransitionDueListTabs();
            _model = await _jobTransitionService.GetJobTransitionDueList(TabIndex, SearchId,filter);

            model.JobTransitionDueList = _model.JobTransitionDueList;
            model.AllTabCount = _model.AllTabCount;
            model.Tab1Count = _model.Tab1Count;
            model.Tab2Count = _model.Tab2Count;
            model.Tab3Count = _model.Tab3Count;
            model.Tab4Count = _model.Tab4Count;
            model.Tabindex = TabIndex;
            await fillfilterDropdown();
            //model.MoreCount = _model.MoreCount;
            ViewBag.Tabs = new SelectList(model.JobTransition_Tabs, "Code", "Name");
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

        }

        public async Task<IActionResult> ApplyFilter(int TabIndex, int SearchId, Filter filter)
        {
            try { 
            JobTransition model = new JobTransition();
            Global.GlobalFilter = filter;
            model = await _jobTransitionService.GetJobTransitionDueList(TabIndex, SearchId, filter);
            return PartialView("_JobTransitionDueList", model);
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

        //public async Task<IActionResult> GetJobTransitionDueList(int TabIndex, int SearchId)
        //{
        //    JobTransition model = new JobTransition();
        //    model = await _jobTransitionService.GetJobTransitionDueList(TabIndex, SearchId);
        //    return PartialView("_JobTransitionDueList", model);
        //}



        public async Task<IActionResult> ExportToExcel(int TabIndex, int SearchId,string TabName)
        {
            try { 
            JobTransition model = new JobTransition();
            var LoginUser = HttpContext.Session.GetString("LoginUser");
            var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Job Transition Due List(" + TabName + ")", "Excel");
                int slNo = 0;
            if (Global.GlobalFilter == null)
                Global.GlobalFilter = new Filter();
            string FilterValues = await _helpers.GetFilterValues(Global.GlobalFilter);
            model = await _jobTransitionService.GetJobTransitionDueList(TabIndex, SearchId, Global.GlobalFilter);
            if (FilterValues == "")
            {
                FilterValues = "All";
            }
            string currDateTime = await _helpers.GetCurrentDate();
            

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Job Transition Due List");
                int count = model.JobTransitionDueList.Count + 5;


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
                wsReportNameHeaderRange.Value = "Job Transition Due List(" + TabName + ")";

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
                worksheet.Cell(3, 6).Value = model.JobTransitionDueList.Count;
                worksheet.Cell(4, 1).Style.Font.Bold = true;
                worksheet.Cell(4, 1).Value = "Filter Applied";
                worksheet.Range("B4:I4").Merge();
                worksheet.Cell("B4").Value = FilterValues;


                var currentRow = 5;
                #region Header
                worksheet.Cell(currentRow, 1).Value = "Sl.No";
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
                worksheet.Cell(currentRow, 7).Value = "Renewel Date";
                worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 8).Value = "Aging";
                worksheet.Cell(currentRow, 8).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 9).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 9).Value = "Type";



                #endregion

                #region Body
                foreach (var item in model.JobTransitionDueList)
                {
                    currentRow++;
                    slNo = slNo + 1;
                    worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Range("A5:I5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
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
                    worksheet.Cell(currentRow, 7).Value = item.RenewalDate;
                    worksheet.Cell(currentRow, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(currentRow, 8).Value = item.Ageing;
                    worksheet.Cell(currentRow, 9).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(currentRow, 9).Value = item.TransitionType;
                    worksheet.Columns(1, 9).Style.Alignment.WrapText = true;
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
                        "Job Transition Due List.xlsx"
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
