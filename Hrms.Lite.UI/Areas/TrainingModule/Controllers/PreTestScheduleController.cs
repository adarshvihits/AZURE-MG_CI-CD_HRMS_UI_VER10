using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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

namespace Hrms.Lite.UI.Areas.TrainingModule.Controllers
{
    [Area("TrainingModule")]
    [SessionAuthorize]
    public class PreTestScheduleController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly ICommonService _commonService;
        private readonly ITrainingScheduleService _service;
        private readonly IHelpers _helpers;
        public PreTestScheduleController(ITrainingScheduleService service, IDropdownService dropdownService, ICommonService commonService, IHelpers helpers)
        {
            _service = service;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = helpers;
        }
        public async Task<ActionResult> Index()
        {
            try
            {

                PreTrainingSchedule model = new PreTrainingSchedule();
                await fillfilterDropdown();
                model = await _service.GetPreScheduleList(0);
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


        public async Task<IActionResult> GetPretestSchedulePopup()
        {
            PreTrainingSchedule model = new PreTrainingSchedule();
     
            ViewBag.Training = new SelectList(await _dropdownService.GetTrainingDropdown(), "Code", "Name");
            model.ScheduleDate = DateTime.Now;
            return PartialView("_AddPopup", model);
        }

        public async Task<ActionResult> PreScheduleTestSave(PreTrainingSchedule model)
        {
            try
            {
                

                var response = await _service.PreScheduleTestSave(model);

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



        public async Task<ActionResult> schedulefilter(int PreTestScheduleCode)
        {
            try
            {
                //TrnParticipantSelection model = new TrnParticipantSelection();
                Filter filter = new Filter();
                filter.Tabindex = PreTestScheduleCode;
                await fillfilterDropdown();
                return PartialView("~/Areas/TrainingModule/Views/PreTestSchedule/_Filter.cshtml", filter);

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
        //[HttpPost]
        public async Task<ActionResult> AddEmployee(int ScheduleCode, Filter filter)
        {
            PreTrainingSchedule model = new PreTrainingSchedule();
            await fillfilterDropdown();
            model.Filter = new Filter();
            model.Filter = filter;
            model.PreTestScheduleCode = ScheduleCode;
            model.Status = 0;
            return View("AddEmployee", model);
        }


        public async Task<ActionResult> GetAddEmployeeList(int ScheduleCode,int BasedOn, Filter filter)
        {
            try
            {
                PreTrainingSchedule model = new PreTrainingSchedule();



                if (filter.searchValue == "undefined" || filter.searchValue == null)
                {
                    filter.searchValue = "";
                }
                if (ScheduleCode != 0)
                {
                    model = await _service.GetNotScheduleList(ScheduleCode, BasedOn, filter);
                }
                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.PreTrainingScheduleList == null || model.PreTrainingScheduleList.Count() == 0)
                {
                    return Json(new
                    {
                        data = "",
                        draw = filter.draw,
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows
                    });
                }

                if (model.PreTrainingScheduleList.Count > 0)
                {
                    totalrows = model.PreTrainingScheduleList[0].active;

                }
                if (filter.searchValue != "")
                {
                    totalrowsafterfiltering = model.PreTrainingScheduleList[0].totalRecords;
                    return Json(new
                    {
                        data = model.PreTrainingScheduleList,
                        draw = filter.draw,
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows
                    });
                }
                else
                {
                    return Json(new
                    {
                        data = model.PreTrainingScheduleList,
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

        public async Task<ActionResult> PreTestPeoceed(Guid employeeGI,int PreTestScheduleCode)
        {
            try
            {


                var response = await _service.PreTestPeoceed(employeeGI, PreTestScheduleCode);

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
        public async Task<ActionResult> PreTestDelete(int PreTestScheduleCode)
        {
            try
            {


                var response = await _service.PreTestDelete( PreTestScheduleCode);

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
        public async Task<ActionResult> PreTestDeleteEmployeeWise(Guid employeeGI, int PreTestScheduleCode)
        {
            try
            {


                var response = await _service.PreTestDeleteEmployeeWise(employeeGI, PreTestScheduleCode);

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

        public async Task<IActionResult> ExportToExcel(int pendingTableLength)
        {
            try
            {
                PreTrainingSchedule model = new PreTrainingSchedule();
                model = await _service.GetPreScheduleList(0);
                int slno = 0;
                string currDateTime = await _helpers.GetCurrentDate();
                
                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Training Pre Test Schedule", "Excel");
               

                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("Pending List");
                        int PendingCountnum = model.PreTrainingScheduleList.Count + 6;


                        worksheet.Range("A1:C" + PendingCountnum).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:C" + PendingCountnum).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:C" + PendingCountnum).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:C" + PendingCountnum).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:C" + PendingCountnum).Style.Border.LeftBorder = XLBorderStyleValues.Thin;



                        var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 2)));
                        wsReportNameHeaderRange.Style.Font.Bold = true;
                        wsReportNameHeaderRange.Style.Font.FontSize = 15;
                        wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                        wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        wsReportNameHeaderRange.Merge();


                        wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                        wsReportNameHeaderRange.Style.Font.FontSize = 15;
                        wsReportNameHeaderRange.Value = "Training Pre Test Schedule List";
                        worksheet.Range("A6:C6").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
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
                        worksheet.Cell(5, 2).Value = model.PreTrainingScheduleList.Count;
                        var currentRow = 6;

                        #region Header
                        worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 1).Value = "Training";
                        worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 2).Value = "Schedule Date";
                        worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 3).Value = "Schedule Emp Count";
                       


                        #endregion

                        #region Body
                        foreach (var item in model.PreTrainingScheduleList)
                        {
                            currentRow++;
                            slno = slno + 1;
                            worksheet.Cell(currentRow, 1).Value = item.Training;
                            worksheet.Cell(currentRow, 2).Value = item.ScheduleDate;
                            worksheet.Cell(currentRow, 3).Value = item.HeadCount;
                            worksheet.Columns(1, 5).Style.Alignment.WrapText = true;

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
                                book.Protect(EP.Password, false, false);
                                book.SaveToStream(stream);
                            }
                            var content = stream.ToArray();
                            return File(
                                content,
                                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                                "Training Pre Test Schedule List.xlsx"
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




        public async Task<IActionResult> ExportToExcel_EmployeeWise(int ScheduleCode, int BasedOn,int totRecords, Filter filter)
        {
            try
            {
                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Add Employee", "Excel");

                if (filter == null)
                    filter = new Filter();
                else
                {
                    filter.length = totRecords;
                    filter.start = 0;
                }
                string FilterValues = await _helpers.GetFilterValues(filter);
                if (FilterValues == "")
                {
                    FilterValues = "All";
                }

                string currDateTime = await _helpers.GetCurrentDate();
                


                //Get Dataset
                var dataset = await _service.GetNotScheduleList_Excel(ScheduleCode, BasedOn, filter);
                DataTable dt = dataset.Tables[0];

                int index = 0;
                //set EXCEL Column Heading 
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                
                        dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
                }



                using (var workbook = new XLWorkbook())
                {

                    var worksheet = workbook.Worksheets.Add();


                    int count = dt.Rows.Count + 1;

                    worksheet.Range("A1:E" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:E" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:E" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:E" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:E" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;


                    var currentRow = 1;


                    worksheet.Cell(currentRow, 1).InsertTable(dt);
                    worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;


                    worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                    worksheet.Range("A1:E1").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    //worksheet.Cell("A2:G2").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Range("A1:E1").Style.Font.Bold = true;



                    worksheet.Columns().AdjustToContents();
                    worksheet.Rows().AdjustToContents();
                    //worksheet.Protect("123");
                    using (var stream = new System.IO.MemoryStream())
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
                            "Pre TestSchedule Add Employee List.xlsx"
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
