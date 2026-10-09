using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.Master;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Spire.Xls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.Master.Controllers
{
    [Area("Master")]
    [SessionAuthorize]
    public class UserRoleController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly ICommonService _commonService;
        private readonly IUserRoleService _userRoleService;
        private readonly IHelpers _helpers;
        public UserRoleController(IDropdownService dropdownService, ICommonService commonService, IUserRoleService userRoleService, IHelpers helpers)
        {
            _dropdownService = dropdownService;
            _commonService = commonService;
            _userRoleService = userRoleService;
            _helpers = helpers;
        }
        public async Task<ActionResult> Index()
        {

            try {
                UserRole model = new UserRole();

                //model.AppraisalTabs = (List<AppraisalTabs>)await _appraisalService.GetAppraisalTabs();
                model = await _userRoleService.GetUserRoleList("ACTIVE");
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

        public async Task<IActionResult> GetUserVsTab(Guid UserRoleGI)
        {
            try
            {
                UserRole model = new UserRole();
                model = await _userRoleService.GetUserRoleTabDetails(UserRoleGI);
                return PartialView("_UserVsTabPopUp", model);
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
                var response = await _userRoleService.GetMasterInactiveValidation(name, code);

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





        public async Task<ActionResult> GetUserRoleList(string TabIndex)
        {
            try {
                if (TabIndex == "APPROVAL_PENDING")
                {
                    UserRole model = new UserRole();
                    model = await _userRoleService.GetUserRoleList(TabIndex);
                    return PartialView("_PendingList", model);
                }
                else if (TabIndex == "ACTIVE")
                    return PartialView("_UserRoleList", await _userRoleService.GetUserRoleList(TabIndex));
                else
                    return PartialView("_InactiveList", await _userRoleService.GetUserRoleList(TabIndex));
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
                UserRole model = new UserRole();
                string type = "Create";
                Guid LogGI = Guid.Parse("00000000-0000-0000-0000-000000000000");
                Guid UserRoleGI = Guid.Parse("00000000-0000-0000-0000-000000000000");
                model = await _userRoleService.GetUserRoleDetails(UserRoleGI, LogGI, type);
                model.UserVsSubModule = new List<FormPermissionSubModule>();
                model.UserVsMenu = new List<FormPermissionMenu>();
                model.Active = true;
                model.Editmode = "SAVE";
                ViewBag.Svi = new SelectList(await _dropdownService.GetSalaryVisibilityDropDown(), "Code", "Name");
                return View("~/Areas/Master/Views/UserRole/Create.cshtml", model);
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
        public async Task<IActionResult> Edit(Guid UserRoleGI, Guid LogGI)
        {
            try {
                UserRole model = new UserRole();
                string type = "MODIFY";
                LogGI = Guid.Parse("00000000-0000-0000-0000-000000000000");
                model.UserVsSubModule = new List<FormPermissionSubModule>();
                model.UserVsMenu = new List<FormPermissionMenu>();
                model = await _userRoleService.GetUserRoleDetails(UserRoleGI, LogGI, type);
                model.Editmode = type;
                ViewBag.Svi = new SelectList(await _dropdownService.GetSalaryVisibilityDropDown(), "Code", "Name");
                return View("~/Areas/Master/Views/UserRole/Create.cshtml", model);
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
        public async Task<IActionResult> Delete(Guid UserRoleGI, Guid LogGI)
        {
            try {
                UserRole model = new UserRole();
                model.UserVsSubModule = new List<FormPermissionSubModule>();
                model.UserVsMenu = new List<FormPermissionMenu>();
                string type = "DELETE";
                LogGI = Guid.Parse("00000000-0000-0000-0000-000000000000");
                model = await _userRoleService.GetUserRoleDetails(UserRoleGI, LogGI, type);
                ViewBag.Svi = new SelectList(await _dropdownService.GetSalaryVisibilityDropDown(), "Code", "Name");
                return View("~/Areas/Master/Views/UserRole/Delete.cshtml", model);
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
        public async Task<IActionResult> Approve(Guid UserRoleGI, Guid LogGI)
        {
            try {
                UserRole model = new UserRole();
                string type = "APPROVAL";
                //UserRoleGI = Guid.Parse("00000000-0000-0000-0000-000000000000");
                model.UserVsSubModule = new List<FormPermissionSubModule>();
                model.UserVsMenu = new List<FormPermissionMenu>();
                model = await _userRoleService.GetUserRoleDetails(UserRoleGI, LogGI, type);
                return View("~/Areas/Master/Views/UserRole/Approve.cshtml", model);
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
        public async Task<ActionResult> ApproveSubmit(UserRole model, string Mode)
        {
            try {
                model.ApproveMode = Mode;
                var response = await _userRoleService.UserRoleApprove(model);
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
        public async Task<IActionResult> UserRoleDelete(Guid UserRoleGI)
        {
            try {
                var response = await _userRoleService.UserRoleDelete(UserRoleGI);
                if (response.Success == true)
                {
                    response.Message = "Deleted Successfully";
                }
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
        public async Task<IActionResult> Create(UserRole model)
        {
            try {
                model.View = true;
                if (model.GI == Guid.Parse("00000000-0000-0000-0000-000000000000"))
                {
                    model.Mode = "Create";
                }
                else
                {
                    model.Mode = "MODIFY";
                }
                var response = await _userRoleService.UserRoleSave(model);
                return Json(response);
                //if (response.Success == true)
                //{
                //    TempData["Message"] = response.Message;
                //    return RedirectToAction("Index", "UserRole");
                //}
                //else
                //{
                //    TempData["Message"] = response.Message;
                //    ViewBag.Svi = new SelectList(await _dropdownService.GetSalaryVisibilityDropDown(), "Code", "Name");
                //    return View("~/Areas/Master/Views/UserRole/Create.cshtml", model);
                //}
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
        public async Task<IActionResult> UserVSTabSave(UserRole model)
        {
            try { 
                var response = await _userRoleService.TabApplicabilitySave(model);
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

            public async Task<IActionResult> ModuleChange(int[] Code,bool[] Appl,string[] Name,Guid GI,Guid LogGI,string Editmode)
        {
            try { 
            Head fpa = new Head();
            fpa.Module = new FormPermissionArray();
            fpa.SubModule = new FormPermissionArray();
            UserRole model = new UserRole();
            fpa.Module.Code = new int[Code.Length];
            fpa.Module.Name = new string[Name.Length];
            fpa.Module.Applicable = new bool[Appl.Length];
            fpa.Module.Code = Code;
            fpa.Module.Applicable = Appl;
            fpa.Module.Name = Name;
            string mode = "Module";
            fpa.Mode = mode;
            fpa.EDITMODE = Editmode;
            if (GI == Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {
                fpa.EDITMODE = "Create";
            }
            fpa.GI = GI;
            fpa.LogGI = LogGI;
            string data = JsonConvert.SerializeObject(fpa);
                //int[] ccode = new int[2] { 1,2 };
                //bool[] aappl = new bool[2] {true,true };
                //string[] nname = new string[2] { "niv","vv" };
                model = await _userRoleService.GetApplicabilityDetails(fpa, 0, Code.Length, mode);
                return PartialView("_SubModule", model);
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

        public async Task<IActionResult> SubModuleChange(int[] Code, Guid GI, Guid LogGI, string Editmode)
        {
            try { 
            Head fpa = new Head();
            fpa.SubModule = new FormPermissionArray();
            fpa.Module = new FormPermissionArray();
            UserRole model = new UserRole();
            fpa.SubModule.Code = new int[Code.Length];
            fpa.SubModule.Code = Code;
            string mode = "SubModule";
            fpa.Mode = mode;
            fpa.EDITMODE = Editmode;
            fpa.GI = GI;
            fpa.LogGI = LogGI;
            string data = JsonConvert.SerializeObject(fpa);
            //int[] ccode = new int[2] { 1,2 };
            //bool[] aappl = new bool[2] {true,true };
            //string[] nname = new string[2] { "niv","vv" };
            model = await _userRoleService.GetApplicabilityDetails(fpa, 0, Code.Length, mode);
            if (GI == Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {
                fpa.EDITMODE = "Create";
                model.Editmode = "SAVE";
            }
            else
            {
                model.Editmode = "MODIFY";
            }
            return PartialView("_Menu", model);
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
            UserRole model = new UserRole();
            model = await _userRoleService.GetUserRoleList(TabIndex);
            int slno = 0;
            string currDateTime = await _helpers.GetCurrentDate();
            
            var LoginUser = HttpContext.Session.GetString("LoginUser");
            var CompanyName = HttpContext.Session.GetString("CompanyName");
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("User Role (" + TabIndex + ")", "Excel");
                if (TabIndex == "APPROVAL_PENDING")
            {

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Pending List");

                    int PendingCountnum = model.PendingCount + 6;


                    worksheet.Range("A1:E" + PendingCountnum).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:E" + PendingCountnum).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:E" + PendingCountnum).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:E" + PendingCountnum).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:E" + PendingCountnum).Style.Border.LeftBorder = XLBorderStyleValues.Thin;



                    var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 5)));
                    wsReportNameHeaderRange.Style.Font.Bold = true;
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Merge();


                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Value = "User Role " + "(" + TabIndex + ")";
                    worksheet.Range("A6:E6").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
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
                    var currentRow = 6;

                    #region Header
                    worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 1).Value = "Sl.No";
                    worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 2).Value = "Name";
                    worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 3).Value = "ShortName";
                    worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 4).Value = "Salary Visibility";
                    worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 5).Value = "Permitted Action";
                    worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 6).Value = "Entry Type";
                    #endregion

                    #region Body
                    foreach (var item in model.UserRoleList)
                    {
                        currentRow++;
                        slno = slno + 1;
                        worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 1).Value = slno;
                        worksheet.Cell(currentRow, 2).Value = item.userrole;
                        worksheet.Cell(currentRow, 3).Value = item.ShortName;
                        worksheet.Cell(currentRow, 4).Value = item.SalaryVisibility.Name;
                        worksheet.Cell(currentRow, 5).Value = item.PermittedActions;
                        worksheet.Cell(currentRow, 6).Value = item.EntryType;
                        worksheet.Columns(1, 6).Style.Alignment.WrapText = true;
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
                            "Pending User Role List.xlsx"
                            );
                    }
                }

            }
            else
            {

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("User Role List");
                    int count = model.UserRoleList.Count + 6;


                    worksheet.Range("A1:E" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:E" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:E" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:E" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:E" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 4)));
                    wsReportNameHeaderRange.Style.Font.Bold = true;
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Merge();
                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                    wsReportNameHeaderRange.Value = "User Role" + "(" + TabIndex + ")";
                    worksheet.Range("A6:E6").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
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
                    worksheet.Cell(5, 2).Value = model.UserRoleList.Count;

                    var currentRow = 6;

                    #region Header
                    worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 1).Value = "Sl.No";
                    worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 2).Value = "Name";
                    worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 3).Value = "ShortName";
                    worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 4).Value = "Salary Visibility";
                    worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 5).Value = "Permitted Action";
                    #endregion

                    #region Body
                    foreach (var item in model.UserRoleList)
                    {
                        currentRow++;
                        slno = slno + 1;
                        worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 1).Value = slno;
                        worksheet.Cell(currentRow, 2).Value = item.userrole;
                        worksheet.Cell(currentRow, 3).Value = item.ShortName;
                        worksheet.Cell(currentRow, 4).Value = item.SalaryVisibility.Name;
                        worksheet.Cell(currentRow, 5).Value = item.PermittedActions;
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
                                book.Protect(EP.Password,false,false);
                                book.SaveToStream(stream);
                            }
                            var content = stream.ToArray();
                        return File(
                            content,
                            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                           TabIndex + "User Role List.xlsx"
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
