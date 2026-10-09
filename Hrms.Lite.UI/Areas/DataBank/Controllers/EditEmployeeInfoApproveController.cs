using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.Account;
using Hrms.Lite.Shared.DataBank.DTO;
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
    public class EditEmployeeInfoApproveController : Controller
    {
        private readonly IEditEmployeeInfoApproveService _service;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        private readonly IDropdownService _drop;

        public EditEmployeeInfoApproveController(IEditEmployeeInfoApproveService service, ICommonService commonService, IHelpers helpers, IDropdownService drop)
        {
            _service = service;
            _commonService = commonService;
            _helpers = helpers;
            _drop = drop;
        }

        public async Task<IActionResult> Index()
        {
            try { 
            EditEmployeeInfoApprove model = new EditEmployeeInfoApprove();
            ViewBag.Filter = new SelectList(await _drop.GetEditEmployeeInfoListDropDown(), "Code", "Name");
            await fillfilterDropdown();
            //model = await _service.GetEditEmployeeInfoApproveDetails(searchID, filter);
            EditEmployeeInfoApprove m= new EditEmployeeInfoApprove();
            Filter filter = new Filter();
            filter.length = 10;
            m = await _service.GetEditEmployeeInfoApproveDetails(0, filter);

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
        public async Task<IActionResult> GetEditEmployeeInfoApprove(int searchID, Filter filter)
        {
            try
            {
                EditEmployeeInfoApprove model = new EditEmployeeInfoApprove();
                if (filter.searchValue == "undefined" || filter.searchValue == null)
                {
                    filter.searchValue = "";
                }
                model = await _service.GetEditEmployeeInfoApproveDetails(searchID, filter);
                int totalrows = 0;
                int totalrowsafterfiltering = 0;
                if (model.EditInfoList == null || model.EditInfoList.Count() == 0)
                {
                    return Json(new
                    {
                        data = "",
                        draw = filter.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows
                    });
                }
                if (model.EditInfoList.Count > 0)
                {
                    totalrows = model.EditInfoList[0].active;
                }
                if (filter.searchValue != "")
                {
                    totalrowsafterfiltering = model.EditInfoList[0].totalRecords;
                    return Json(new
                    {
                        data = model.EditInfoList,
                        draw = filter.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows//totalrowsafterfiltering
                    });
                }
                else
                {
                    return Json(new
                    {
                        data = model.EditInfoList,
                        draw = filter.draw,//Request.Form["draw"], 
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

        public async Task<IActionResult> Approve(Guid EmployeeGI, Guid LogGI)
        {
            try
            {
                EditEmployeeInfoApprove model = new EditEmployeeInfoApprove();
                model = await _service.GetEditEmployeeApproveDetails(EmployeeGI, LogGI);
                model.EmployeeHeader = await _commonService.GetEmployeeHeader(EmployeeGI);
                return View("~/Areas/DataBank/Views/EditEmployeeInfoApprove/Approve.cshtml", model);
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
        public async Task<IActionResult> Approve(EditEmployeeInfoApprove model)
        {
            try
            {
                model.ApproveMode = "APPROVE";
                var response = await _service.ApproveEditApproveEmployeeInfo(model);
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
        public async Task<IActionResult> Reject(EditEmployeeInfoApprove model)
        {
            try
            {
                model.ApproveMode = "REJECT";
                var response = await _service.ApproveEditApproveEmployeeInfo(model);
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


        public async Task<IActionResult> ExportToExcel( Filter filter, int TotalRows)
        {
            EditEmployeeInfoApprove model = new EditEmployeeInfoApprove();
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
            var dataset = await _service.EditEmployeeInfoApprove_Excel( filter);
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
                var worksheet = workbook.Worksheets.Add("Edit Employee Info Approve list");
                int count = dt.Rows.Count + 3;


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
                wsReportNameHeaderRange.Value = "Edit Employee Info Approve list ";

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
                var filterapplied = worksheet.Range("B4:H4").Merge();

                filterapplied.Value = FilterValues;


                var currentRow = 5;


                worksheet.Cell(currentRow, 1).InsertTable(dt);
                worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                worksheet.Range("A5:H5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                worksheet.Range("A5:H5").Style.Font.Bold = true;


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
                        "EditEmployeeInfoApprovelist.xlsx"
                        );
                }
            }
            
        }


        public async Task fillfilterDropdown()
        {
            ViewBag.Gen = new SelectList(await _drop.GetGenderDropDown(), "Code", "Name");
            ViewBag.Cat = new SelectList(await _drop.GetCategoryDropdown(), "Code", "Name");
            ViewBag.Grd = new SelectList(await _drop.GetGradeDropdown(), "Code", "Name");
            ViewBag.Dsg = new SelectList(await _drop.GetDesignationDropdown(), "Code", "Name");
            ViewBag.Dept = new SelectList(await _drop.GetDepartmentDropdown(), "Code", "Name");
            ViewBag.Loc = new SelectList(await _drop.GetLocationDropdown(), "Code", "Name");
            ViewBag.PayrollGrp = new SelectList(await _drop.GetPayrollGroupDropdown(), "Code", "Name");
            ViewBag.division = new SelectList(await _drop.GetDivisionDropdown(), "Code", "Name");
            ViewBag.section = new SelectList(await _drop.GetSectionDropdown(), "Code", "Name");
            ViewBag.EmploymentType = new SelectList(await _drop.GetEmploymentTypeDropdown(), "Code", "Name");
            ViewBag.EmploymentStatus = new SelectList(await _drop.GetEmploymentStatusDropdown(), "Code", "Name");
            ViewBag.WorkLocation = new SelectList(await _drop.GetWorkLocationDropdown(), "Code", "Name");
            ViewBag.WageGrade = new SelectList(await _drop.GetWageGradeDropdown(), "Code", "Name");
        }
    }
}
