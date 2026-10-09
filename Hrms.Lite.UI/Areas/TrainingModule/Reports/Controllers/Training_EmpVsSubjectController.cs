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
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.TrainingModule.Reports.Controllers
{

    [Area("TrainingModule")]
    [SessionAuthorize]
    public class Training_EmpVsSubjectController : Controller
    {
        private readonly ITraining_EmpVsSubjectService _Training_EmpVsSubjectService;
        private readonly IDropdownService _dropdownService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;

        public Training_EmpVsSubjectController(IDropdownService dropdownService, ICommonService commonService, ITraining_EmpVsSubjectService Training_EmpVsSubjectService, IHelpers helpers)
        {
            _Training_EmpVsSubjectService = Training_EmpVsSubjectService;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = helpers;
        }

        public async Task<IActionResult> Index()
        {
            Training_EmpVsSubject model = new Training_EmpVsSubject();

            ViewBag.Financialyr = new SelectList(await _dropdownService.GetFinancialYearDropdown(), "Code", "Name");
            ViewBag.Subject = new SelectList(await _dropdownService.GetTrainingSubjectDropdown(), "Code", "Name");
           
            var CurrFinCode = _commonService.GetCurrentLFinancialYear();
            model.FYCode = CurrFinCode.Result.Data;
            model.Filter = await _commonService.GetFilterDropdowns();

            return View("~/Areas/TrainingModule/Reports/Views/Training_EmpVsSubject/Index.cshtml", model);
        }
        [HttpPost]
        public async Task<ActionResult> GetList(int FYCode, int SubjectCode, Filter filter)
        {
            try
            {
                Training_EmpVsSubject model = new Training_EmpVsSubject();

                if (filter.searchValue == "undefined" || filter.searchValue == null)
                {
                    filter.searchValue = "";
                }

                if (FYCode != 0)
                    model = await _Training_EmpVsSubjectService.GetTrainingEmpVsSubjectList(FYCode, SubjectCode, filter);

                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.TrainingEmployeeReportList == null || model.TrainingEmployeeReportList.Count() == 0)
                {
                    return Json(new
                    {
                        data = "",
                        draw = filter.draw,
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows
                    });
                }
                if (model.TrainingEmployeeReportList.Count > 0)
                {
                    totalrows = model.TrainingEmployeeReportList[0].Active;
                }
                if (filter.searchValue != "")
                {
                    totalrowsafterfiltering = model.TrainingEmployeeReportList[0].TotalRecords;
                    return Json(new
                    {
                        data = model.TrainingEmployeeReportList,
                        draw = filter.draw,
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows //totalrowsafterFiltering
                    });

                }
                else
                {
                    return Json(new
                    {
                        data = model.TrainingEmployeeReportList,
                        draw = filter.draw,
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

       

        public async Task<ActionResult> GetEmployeeProfileReport(int EmpCode, int FYCode, int SubjectCode)
        {
            try
            {
                Training_EmpVsSubject model = new Training_EmpVsSubject();

                {
                    model = await _Training_EmpVsSubjectService.GetEmployeeProfileReport(EmpCode, FYCode, SubjectCode);
                }

                return PartialView("~/Areas/TrainingModule/Reports/Views/Training_EmpVsSubject/_EmployeeReport.cshtml", model);
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

        public async Task<ActionResult> GetTrainingEmployeeComboData(int FYCode, int SubjectCode)
        {
            var data = await _dropdownService.GetTrainingReportDropdown(FYCode, SubjectCode);
            return Json(data);
        }

        public async Task<IActionResult> ExportToExcel(int FYCode, string FYName, int SubjectCode, string SubjectName, int TrainingCode, string TrainingName, Filter filter, int TotalRows)
        {

            var LoginUser = HttpContext.Session.GetString("LoginUser");
            var CompanyName = HttpContext.Session.GetString("CompanyName");
            var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
            var GroupCompanyName = User.authUser.GroupCompanyName;
            ExcelPassword EP = new ExcelPassword();
            EP = await _commonService.GetExcelPassword("Training Employee VsReport", "Excel");


            filter.length = TotalRows;
            filter.start = 0;


            string FilterValues = await _helpers.GetFilterValues(filter);
            if (FilterValues == "")
            {
                FilterValues = "All";
            }


            string currDateTime = await _helpers.GetCurrentDate();
            

            //Get Data Set
            var dataset = await _Training_EmpVsSubjectService.TrainingEmpVsSubject_Excel(FYCode, SubjectCode, filter);
            DataTable dt = dataset.Tables[0];

            dataset.Tables[0].Rows.RemoveAt(0);
            if ((dataset.Tables[0]?.Rows?.Count ?? 0) > 0)
            {
                dt = dataset.Tables[0];
            }

            //set EXCEL Column heading
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                if (dt.Columns[i].ColumnName == "doj"|| dt.Columns[i].ColumnName == "id")
                    dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.ToUpper();
                else
                dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
            }

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Subject Wise Training Report");
                int count = dt.Rows.Count + 5;

                worksheet.Range("A1:J" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1:J" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1:J" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1:J" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1:J" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;

                var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:J{0}", 1, Char.ConvertFromUtf32(65 + 7)));
                wsReportNameHeaderRange.Style.Font.Bold = true;
                wsReportNameHeaderRange.Style.Font.FontSize = 15;
                wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                wsReportNameHeaderRange.Merge();
                wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                wsReportNameHeaderRange.Value = "Subject Wise Training Report";

                worksheet.Cell(2, 1).Style.Font.Bold = true;
                worksheet.Cell(2, 1).Value = "Group Company Name";
                worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                worksheet.Cell(2, 3).Style.Font.Bold = true;
                worksheet.Cell(2, 3).Value = "Financial Year";
                worksheet.Cell(2, 4).Value = FYName.ToUpper();

                worksheet.Cell(2, 5).Style.Font.Bold = true;
                worksheet.Cell(2, 5).Value = "Subject";
                worksheet.Cell(2, 6).Value = SubjectName.ToUpper();


                //worksheet.Cell(2, 7).Style.Font.Bold = true;
                //worksheet.Cell(2, 7).Value = "Training";
                //worksheet.Cell(2, 8).Value = TrainingName.ToUpper();




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

                var filterapplied = worksheet.Range("B4:J4").Merge();

                filterapplied.Value = FilterValues;

                var currentRow = 5;
                worksheet.Cell(currentRow, 1).InsertTable(dt);
                worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;
                worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                worksheet.Range("A5:J5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                worksheet.Range("A5:J5").Style.Font.Bold = true;
                worksheet.Range("H5:H" + (dt.Rows.Count + 5)).Style.DateFormat.Format = "dd-MMM-yyyy";



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
                        "Subject Wise Training Report.xlsx"
                        );
                }
            }
        }


    }
}

