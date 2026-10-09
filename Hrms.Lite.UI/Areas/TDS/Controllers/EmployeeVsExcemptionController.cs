using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.TDS;
using Hrms.Lite.Shared.Account;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TDS;
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
namespace Hrms.Lite.UI.Areas.TDS.Controllers
{
    [Area("TDS")]
    [SessionAuthorize]
    public class EmployeeVsExcemptionController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly IEmployeeVsExcemptionService _EmployeeVsExcemptionService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        public static class Global
        {

            public static Filter GlobalFilter;

        }
        public EmployeeVsExcemptionController(IEmployeeVsExcemptionService EmployeeVsExcemptionService, IDropdownService dropdownService, ICommonService commonService, IHelpers helpers)
        {
            _EmployeeVsExcemptionService = EmployeeVsExcemptionService;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = helpers;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                EmployeeVsExcemption model = new EmployeeVsExcemption();
                EmployeeVsExcemption m = new EmployeeVsExcemption();
                
                model.PayrollMonth = new MasterBase();
                model.AssessmentYear = new MasterBase();
                model.Excemption = new Dropdown();
                ViewBag.AssessmentYear = new SelectList(await _dropdownService.GetAssessmentYearDropdown(), "Code", "Name");
                ViewBag.month = new SelectList(await _dropdownService.GetTDSConfirmationMonthDropdown(0), "Code", "Name");
                ViewBag.Excemption = new SelectList(await _dropdownService.GetExcemptionDropdown(), "Code", "Name");

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
        [HttpGet]
        public async Task<JsonResult> GetMonthDropDown(int AssessmentYearCode)
        {
            SelectList data = new SelectList("", "Code", "Name");
            data = new SelectList(await _dropdownService.GetTDSConfirmationMonthDropdown(AssessmentYearCode), "Code", "Name");
            return Json(data);
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
        public async Task<IActionResult> GetEmpVsExcemptionList(Filter filter1, int AssessmentYear, int Month, int Excemption)
        {
            try
            {
                EmployeeVsExcemption model = new EmployeeVsExcemption();
                EmployeeVsExcemption m = new EmployeeVsExcemption();


                Filter filter = new Filter();
                Global.GlobalFilter = filter1;


                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }
                if (AssessmentYear != 0)
                {

                    model.EmployeeVsExcemptionList = (List<EmployeeVsExecempList>)await _EmployeeVsExcemptionService.GetEmpVsExcemptionList(AssessmentYear, Month, Excemption, filter1);
                }
                m.EmployeeVsExcemptionList = model.EmployeeVsExcemptionList;
                int totalrows = 0;
                int totalrowsafterfiltering = 0;



                if (model == null || model.EmployeeVsExcemptionList == null || model.EmployeeVsExcemptionList.Count() == 0)
                {
                    m.EmployeeVsExcemptionList = new List<EmployeeVsExecempList>();
                    model = new EmployeeVsExcemption();
                    model.EmployeeVsExcemptionList = new List<EmployeeVsExecempList>();
                }
                else
                {
                    m.EmployeeVsExcemptionList = model.EmployeeVsExcemptionList;
                }



                if (model.EmployeeVsExcemptionList == null || model.EmployeeVsExcemptionList.Count() == 0)
                {

                    return Json(new
                    {
                        data = "",
                        draw = filter1.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows
                    });
                }




                if (model.EmployeeVsExcemptionList.Count > 0)
                {
                    totalrows = m.EmployeeVsExcemptionList[0].active;

                }


                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = m.EmployeeVsExcemptionList[0].totalRecords;
                    return Json(new
                    {
                        data = model.EmployeeVsExcemptionList,
                        draw = filter1.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows//totalrowsafterfiltering
                    });
                }
                else
                {
                    return Json(new
                    {
                        data = model.EmployeeVsExcemptionList,
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
        public async Task<ActionResult> SaveEmployeeVsExcemption(decimal ExcemptionAmount,Guid EmployeeGI, int AssessmentYear, int Month, int Excemption)
        {
            try
            {
                EmployeeVsExecempList model = new EmployeeVsExecempList();
                model.ExcemptionAmount = ExcemptionAmount;
               
                model.Employee = new EmployeeHeader();
                model.Employee.EmployeeGuid = EmployeeGI;



                var response = await _EmployeeVsExcemptionService.SaveEmployeeVsExcemption(model, AssessmentYear, Month, Excemption);
                if (response.Success == true)
                    response.Message = "Updated Successfully";
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

        public async Task<IActionResult> ExportToExcel(int AssessmentYear, string AssessmentYearName, int Month, string MName, int Excemption, string ExcemptionName, int Totalrow)
        {
            try
            {
                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;

              
                if (Global.GlobalFilter == null)
                    Global.GlobalFilter = new Filter();
                else
                {
                    Global.GlobalFilter.length = Totalrow;
                    Global.GlobalFilter.start = 0;
                }
                string FilterValues = await _helpers.GetFilterValues(Global.GlobalFilter);

                var dataset = await _EmployeeVsExcemptionService.GetEmpVsExcemptionList_Excel(AssessmentYear, Month, Excemption, Global.GlobalFilter);
                DataTable dt = dataset.Tables[0];

                for (int i = 0; i < dt.Columns.Count; i++)
                {

                    dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
                }

                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Employee Vs Excemption (" + AssessmentYearName + ")", "Excel");
               
                if (FilterValues == "")
                {
                    FilterValues = "All";  
                
                }
                string currDateTime = await _helpers.GetCurrentDate();
                
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Employee Vs Excemption List");
                    int count = dt.Rows.Count + 5;


                    worksheet.Range("A1:G" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:G" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:G" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:G" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:G" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 6)));
                    wsReportNameHeaderRange.Style.Font.Bold = true;
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Merge();
                    wsReportNameHeaderRange.Value = "Employee Vs Excemption";

                    worksheet.Cell(2, 1).Style.Font.Bold = true;
                    worksheet.Cell(2, 1).Value = "Group Company Name";
                    worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                    worksheet.Cell(2, 3).Style.Font.Bold = true;
                    worksheet.Cell(2, 3).Value = "Assessment Year";
                    worksheet.Cell(2, 4).Value = AssessmentYearName;
                    worksheet.Cell(2, 5).Style.Font.Bold = true;
                    worksheet.Cell(2, 5).Value = "Month";

                    worksheet.Cell(2, 6).Value = MName;
                    worksheet.Cell(2, 6).Style.DateFormat.Format = "MMM-yyyy";
                    worksheet.Cell(2, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(2, 7).Style.Font.Bold = true;
                    worksheet.Cell(2, 7).Value = "Excemption:" + ExcemptionName;


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

                    var filterapplied = worksheet.Range("B4:G4").Merge();

                    filterapplied.Value = FilterValues;

                    var currentRow = 5;
                    worksheet.Cell(currentRow, 1).InsertTable(dt);
                    worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;
                    worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                    worksheet.Range("A5:G5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Range("A5:G5").Style.Font.Bold = true;



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
                            "Employee Vs Excemption Report.xlsx"
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
