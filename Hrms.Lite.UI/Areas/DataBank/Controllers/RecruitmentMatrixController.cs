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
    public class RecruitmentMatrixController : Controller
    {
        private readonly IRecruitmentMatrixService _RecruitmentMatrixService;
        private readonly ICommonService _commonService;
        private readonly IDropdownService _dropdownService;
        private readonly IHelpers _helpers;
        public static class Global
        {
            public static Filter GlobalFilter;
        }
        public RecruitmentMatrixController(IRecruitmentMatrixService RecruitmentMatrixService, ICommonService commonService, IDropdownService dropdownService, IHelpers Helpers)
        {
            _RecruitmentMatrixService = RecruitmentMatrixService;
            _commonService = commonService;
            _dropdownService = dropdownService;
            _helpers = Helpers;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                RecruitmentMatrix model = new RecruitmentMatrix();
                ViewBag.Template = new SelectList(await _dropdownService.GetTemplateDropdown(), "Code", "Name");
                await fillfilterDropdown();
                //Filter filter = new Filter();
                Filter filter = new Filter();
               // model = await _RecruitmentMatrixService.RecruitmentMatrixList(0, filter);
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
        public async Task<IActionResult> GetAuthorityEntity(RecruitmentMatrix model)
        {
            try
            {
                RecruitmentMatrix model1 = new RecruitmentMatrix();
                model1 = await _RecruitmentMatrixService.GetRoleList(model);
                var dropdownData = await _dropdownService.GetApproverRolesDropdown();
                ViewBag.Role = new SelectList(await _dropdownService.GetApproverRolesDropdown(), "Code", "Name");
                ViewBag.RecruitmentRole = new SelectList(await _dropdownService.GetRecruitmentRoleDropdown(), "Code", "Name");
                ViewBag.ResponsiblePerson = new SelectList(await _dropdownService.GetResponsiblePersonDropdown(0), "Code", "Name");
                ViewBag.RoleOrEmp = new SelectList(await _dropdownService.GetRoleOrEmpDropdown(), "Code", "Name");
                //ViewBag.Role = dropdownData.Select(x => new SelectListItem
                //{
                //    Value = x.Code.ToString(),
                //    Text = x.Name
                //}).ToList();

                return PartialView("_RoleList", model1);
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
        public async Task<IActionResult> GetListData(Filter filter1, int WorkFlow)
        {
            try
            {
                if (string.IsNullOrEmpty(filter1.searchValue) || filter1.searchValue == "undefined")
                    filter1.searchValue = "";

                var model = await _RecruitmentMatrixService.RecruitmentMatrixList(WorkFlow, filter1);

                var list = model?.RecruitmentMatrixList ?? new List<RecruitmentMatrix>();

                int total = list.Count > 0 ? list[0].active : 0;
                int filtered = filter1.searchValue != "" && list.Count > 0
                    ? list[0].totalRecords
                    : total;

                return Json(new
                {
                    data = list,
                    draw = filter1.draw,
                    recordsTotal = total,
                    recordsFiltered = filtered
                });
            }
            catch (Exception)
            {
                return Json(new
                {
                    data = new List<RecruitmentMatrix>(),
                    draw = filter1.draw,
                    recordsTotal = 0,
                    recordsFiltered = 0
                });
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
            ViewBag.VDAGroup = new SelectList(await _dropdownService.GetVDACombo(), "Code", "Name");
        }
        public async Task<IActionResult> GetList(int TemplateList)
        {
            try
            {
                RecruitmentMatrix model = new RecruitmentMatrix();
                Filter filter = new Filter();
                model = await _RecruitmentMatrixService.RecruitmentMatrixList(TemplateList, filter);
                return PartialView("_List", model);
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
        public async Task<IActionResult> GetEditRoleList(RecruitmentMatrix model)
        {
            try
            {
                RecruitmentMatrix model1 = new RecruitmentMatrix();
                model1 = await _RecruitmentMatrixService.GetEditRoleList(model.NoOfLevels, model.RecruitmentMatrixGI);
                //ViewBag.Role = new SelectList(await _RecruitmentMatrixService.GetAuthorityEntityDropdown(model), "Code", "Name", "ShortName");
                ViewBag.Role = new SelectList(await _dropdownService.GetApproverRolesDropdown(), "Code", "Name");
                return PartialView("_LevelList", model1);
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

        public async Task<IActionResult> GetDetails(Guid RecruitmentMatrixGI)
        {
            try
            {
                RecruitmentMatrix model = new RecruitmentMatrix();
                model = await _RecruitmentMatrixService.GetRecruitmentMatrixDetails(RecruitmentMatrixGI);
                return PartialView("_View", model);
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

        public async Task<IActionResult> Edit(int TemplateList)
        {
            try
            {
                RecruitmentMatrix model = new RecruitmentMatrix();
                model = await _RecruitmentMatrixService.GetRecruitmentMatrixEditDetails(TemplateList);
                var dropdownData = await _dropdownService.GetApproverRolesDropdown();
                ViewBag.Role = new SelectList(await _dropdownService.GetApproverRolesDropdown(), "Code", "Name");
                ViewBag.RecruitmentRole = new SelectList(await _dropdownService.GetRecruitmentRoleDropdown(), "Code", "Name");
                ViewBag.ResponsiblePerson = new SelectList(await _dropdownService.GetResponsiblePersonDropdown(0), "Code", "Name");
                ViewBag.RoleOrEmp = new SelectList(await _dropdownService.GetRoleOrEmpDropdown(), "Code", "Name");
                return PartialView("_EditSlider", model);
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
            try
            {
                RecruitmentMatrix model = new RecruitmentMatrix();
                ViewBag.Template = new SelectList(await _dropdownService.GetTemplateDropdown(), "Code", "Name");
                //ViewBag.Role = new SelectList(Enumerable.Empty<Dropdown>(), "Code", "Name", "ShortName");
                ViewBag.Role = new SelectList(await _dropdownService.GetApproverRolesDropdown(), "Code", "Name");
                ViewBag.Level = new SelectList(await _dropdownService.GetRequestLevelDropDown(0), "Code", "Name");
                return PartialView("_CreateSlider", model);
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
        public async Task<IActionResult> RecruitmentMatrixSave(RecruitmentMatrix model)
        {
            try
            {


                var response = await _RecruitmentMatrixService.RecruitmentMatrixSave(model);
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
        public async Task<IActionResult> RecruitmentMatrixUpdate(RecruitmentMatrix model, int Template)
        {
            try
            {
                var response = await _RecruitmentMatrixService.RecruitmentMatrixUpdate(model, Template);
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
        [HttpGet]
        public async Task<IActionResult> RecruitmentMatrixDelete(Guid RecruitmentMatrixGI)
        {
            try
            {
                var response = await _RecruitmentMatrixService.RecruitmentMatrixDelete(RecruitmentMatrixGI);
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

        [HttpGet]
        public async Task<IActionResult> RecruitmentMatrixLevelDelete(Guid RecruitmentMatrixGI, int Level)
        {
            try
            {
                var response = await _RecruitmentMatrixService.RecruitmentMatrixLevelDelete(RecruitmentMatrixGI, Level);
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
        public async Task<IActionResult> ExportToexcel(string WorkFlowName, int WorkFlow, int TotalRows)
        {
            try
            {
                RecruitmentMatrix model = new RecruitmentMatrix();

                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Approval Matrix", "Excel");
                int slNo = 0;
                //Filter filter = new Filter();
                //filter = JsonConvert.DeserializeObject<Filter>(TempData["FilterModel"].ToString());
                if (Global.GlobalFilter == null)
                    Global.GlobalFilter = new Filter();
                else
                {
                    Global.GlobalFilter.length = TotalRows;
                    Global.GlobalFilter.start = 0;
                }
                string FilterValues = await _helpers.GetFilterValues(Global.GlobalFilter);
                if (FilterValues == "")
                {
                    FilterValues = "All";
                }
                var dataset = await _RecruitmentMatrixService.GetList_Excel(WorkFlow, Global.GlobalFilter);
                DataTable dt = dataset.Tables[0];
                string currDateTime = await _helpers.GetCurrentDate();
                

                //set EXCEL Column Heading 
                for (int i = 0; i < dt.Columns.Count; i++)
                {

                    dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
                }

                using (var workbook = new XLWorkbook())
                {

                    var worksheet = workbook.Worksheets.Add("Approval Matrix");


                    int count = dt.Rows.Count + 5;


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
                    wsReportNameHeaderRange.Value = "Approval Matrix";

                    worksheet.Cell(2, 1).Style.Font.Bold = true;
                    worksheet.Cell(2, 1).Value = "Group Company Name";
                    worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                    worksheet.Cell(2, 3).Style.Font.Bold = true;
                    worksheet.Cell(2, 3).Value = "Workflow";
                    worksheet.Cell(2, 4).Value = WorkFlowName;

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



                    var currentRow = 5;

                    worksheet.Cell(currentRow, 1).InsertTable(dt);
                    worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                    worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                    worksheet.Range("A5:h5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Range("A5:h5").Style.Font.Bold = true;


                    worksheet.Columns().AdjustToContents();
                    worksheet.Rows().AdjustToContents();
                    //worksheet.Protect("123");
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
                            "Approval Matrix.xlsx"
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
