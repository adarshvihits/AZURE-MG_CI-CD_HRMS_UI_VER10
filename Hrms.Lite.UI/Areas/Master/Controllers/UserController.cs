using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.Account;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.Master;
using Hrms.Lite.Shared.Account;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
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

namespace Hrms.Lite.UI.Areas.Master.Controllers
{
    [Area("Master")]
    [SessionAuthorize]
    public class UserController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly IUserService _userService;
        private readonly ICommonService _commonService;
        private readonly IPasswordSettingsService _IPasswordSettingsService;
        private readonly IHelpers _helpers;
        public static class Global
        {
            public static Filter GlobalFilter;
        }
        public UserController(IUserService UserService, IDropdownService dropdownService, ICommonService commonService, IPasswordSettingsService IPasswordSettingsService, IHelpers helpers)
        {
            _userService = UserService;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _IPasswordSettingsService = IPasswordSettingsService;
            _helpers = helpers;
        }
        public async Task<IActionResult> Index()
        {
            try { 
            User model = new User();
                model = await _userService.GetUserDetails(Guid.Parse("00000000-0000-0000-0000-000000000000"), Guid.Parse("00000000-0000-0000-0000-000000000000"), "CREATE");
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
        public async Task<IActionResult> Create()
        {
            try { 
            User model = new User();
            AddOnClassification AOC = new AddOnClassification();
            AOC = await _commonService.GetAddOnClassificationApplicability();
             model = await _userService.GetUserDetails(Guid.Parse("00000000-0000-0000-0000-000000000000"), Guid.Parse("00000000-0000-0000-0000-000000000000"), "CREATE");
            model.AddOnClassification = AOC;
            ViewBag.UserR = new SelectList(await _dropdownService.GetUserRoleDropDown(), "Code", "Name");
            model.Active = true;
            model.ActiveFrom = model.ActiveFrom == Convert.ToDateTime("01-Jan-0001") ? null : model.ActiveFrom;
            model.ActiveTo = model.ActiveTo == Convert.ToDateTime("01-Jan-0001") ? null : model.ActiveTo;
            model.NonEmployeePassword = "Password@1234";
            return View("~/Areas/Master/Views/User/Create.cshtml",model);
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
        public async Task<IActionResult> GetApplicableMenus(int UserRoleCode)
        {
            try { 
            User model = new User();
            model = await _userService.GetApplicableMenus(UserRoleCode);
            return PartialView("_ApplicableMenus", model);
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
        public async Task<IActionResult> Approve(Guid UserMasterGI,Guid LogGI, string UserType,string Mode, string IsAdmin)
        {
            try
            {
                User model = new User();
                if (Mode == "CREATE")
                    model = await _userService.GetUserDetails(Guid.Parse("00000000-0000-0000-0000-000000000000"), LogGI, "APPROVAL");
                else
                    model = await _userService.GetUserDetails(UserMasterGI, LogGI, "APPROVAL");
                model.UserType = UserType;
                model.Mode = Mode;
                if (UserType == "Essp")
                    return PartialView("_Approve", model);

                if (UserType == "HR" && IsAdmin == "Yes")
                    return PartialView("_Approve", model);


                else
                    return View("~/Areas/Master/Views/User/ApproveNonEmployee.cshtml", model);
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
        public async Task<IActionResult> EditUserEssp(Guid UserMasterGI)
        {
            try { 
            User model = new User();
            var LogGI = Guid.Parse("00000000-0000-0000-0000-000000000000");
            model = await _userService.GetUserDetails(UserMasterGI, LogGI, "MODIFY");
            model.UserType = "ESSP";
            model.EsspUser = true;
            ViewBag.UserR = new SelectList(await _dropdownService.GetUserRoleDropDown(), "Code", "Name");
            return View("~/Areas/Master/Views/User/EditEsspUser.cshtml",model);
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
        public async Task<IActionResult> EditUserHr(Guid UserMasterGI)
        {
            try { 
            User model = new User();           
            var LogGI = Guid.Parse("00000000-0000-0000-0000-000000000000");
            model = await _userService.GetUserDetails(UserMasterGI, LogGI, "MODIFY");          
            ViewBag.UserR = new SelectList(await _dropdownService.GetUserRoleDropDown(), "Code", "Name");
            model.UserType = "HR";
            model.EsspUser = true;
            model.HrmsUser = true;
            return View("~/Areas/Master/Views/User/EditHrUser.cshtml", model);
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
                var response = await _userService.GetMasterInactiveValidation(name, code);

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






        public async Task<IActionResult> EditUserNonEmployee(Guid UserMasterGI)
        {
            try { 
            User model = new User();
            model = await _userService.GetUserDetails(UserMasterGI, Guid.Parse("00000000-0000-0000-0000-000000000000"), "MODIFY");
            ViewBag.UserR = new SelectList(await _dropdownService.GetUserRoleDropDown(), "Code", "Name");
            model.UserType = "NonEmp";
            return View("~/Areas/Master/Views/User/EditNonEmployeeUser.cshtml", model);
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
        public async Task<IActionResult> DeleteUserNonEmployee(Guid UserMasterGI)
        {
            try { 
            User model = new User();
            model = await _userService.GetUserDetails(UserMasterGI, Guid.Parse("00000000-0000-0000-0000-000000000000"), "DELETE");            
            return View("~/Areas/Master/Views/User/DeleteNonEmployeeUser.cshtml",model);
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
        public async Task<IActionResult> DeleteUserInActive(Guid UserMasterGI)
        {
            try { 
            User model = new User();
            model = await _userService.GetUserDetails(UserMasterGI, Guid.Parse("00000000-0000-0000-0000-000000000000"), "DELETE");
            return View("~/Areas/Master/Views/User/DeleteNonEmployeeUser.cshtml", model);
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
        public async Task<IActionResult> DeleteUserHr(Guid UserMasterGI)
        {
            try { 
            User model = new User();
            model = await _userService.GetUserDetails(UserMasterGI, Guid.Parse("00000000-0000-0000-0000-000000000000"), "DELETE");
            return View("~/Areas/Master/Views/User/DeleteHrUser.cshtml", model);
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
        public async Task<IActionResult> DeleteUserEssp(Guid UserMasterGI)
        {
            try { 
            User model = new User();
            model = await _userService.GetUserDetails(UserMasterGI, Guid.Parse("00000000-0000-0000-0000-000000000000"), "DELETE");
            return PartialView("_DeleteEsspUser", model);
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
        public async Task<IActionResult> ResetUserEssp(int EmployeeCode,string Password, int UserCode)
        {
            try
            {
                SchedulePasswordGenertion model = new SchedulePasswordGenertion();
                Login CurrentUser = JsonConvert.DeserializeObject<Hrms.Lite.Shared.Account.Login>(HttpContext.Session.GetString("User"));                
               
                model.Password = Password;
                model.EmployeeCode = EmployeeCode;
                model.UserCode = UserCode;
                var response = await _IPasswordSettingsService.ResetPassword(model);
                if(response.Success==true)
                response.Message = "Password Reset Successfully.Your New Password is:" + model.Password;
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
        public async Task<IActionResult> DeleteUser(Guid UserMasterGI)
        {
            try { 
            var response = await _userService.UserDelete(UserMasterGI);
            if (response.Success == true)
            {
                TempData["Message"] = "Deleted Successfully";
                return RedirectToAction("Index", "User");
            }
            return View("~/Areas/Master/Views/User/Create.cshtml");
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
        public async Task<IActionResult> GetUserList(string TabIndex)
        {
            try {
                User model = new User();
               
                if (TabIndex == "Essp")
                    return PartialView("_ESSPList", model);
                else
                {
                    model = await _userService.GetUserList(TabIndex, Global.GlobalFilter);
                    if (TabIndex == "HR")
                        return PartialView("_HRList", model);
                    else if (TabIndex == "Non_Employee")
                        return PartialView("_NonEmployeeUsersList", model);
                    else if (TabIndex == "INACTIVE")
                        return PartialView("_InActiveList", model);
                    else
                        return PartialView("_ApprovalPendingList", model);
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




        [HttpPost]
        public async Task<IActionResult> GetEsspList(Filter filter1,string TabIndex)
         {
            try
            {
                User model = new User();
                User m = new User();
                Filter filter = new Filter();
                Global.GlobalFilter = filter1;


                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }
                model = await _userService.GetUserList(TabIndex, filter1);
                m.UserList = model.UserList;
                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.UserList == null || model.UserList.Count() == 0)
                {



                    return Json(new
                    {
                        data = "",
                        draw = filter1.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        Count = model,
                        recordsFiltered = totalrows
                    });
                }




                if (model.UserList.Count > 0)
                {
                    totalrows = m.UserList[0].active;


                }


                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = m.UserList[0].totalRecords;
                    return Json(new
                    {
                        data = model.UserList,
                        Count = model,
                        draw = filter1.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows//totalrowsafterfiltering
                    });
                }
                else
                {
                    return Json(new
                    {
                        data = model.UserList,
                        Count=model,
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








        [HttpPost]
        public async Task<IActionResult> Create(User model)
        {
            try { 
            if (model.UserVsEmployeeApplicability.Designation != null)
            {
                var Dsg = from li in model.UserVsEmployeeApplicability.Designation
                          where li.Applicable == true
                          select li;
                model.UserVsEmployeeApplicability.Designation = Dsg.ToList();
            }
            if (model.UserVsEmployeeApplicability.Department != null)
            {
                var Dept = from li in model.UserVsEmployeeApplicability.Department
                           where li.Applicable == true
                           select li;
                model.UserVsEmployeeApplicability.Department = Dept.ToList();
            }
            if (model.UserVsEmployeeApplicability.Location != null)
            {
                var Loc = from li in model.UserVsEmployeeApplicability.Location
                          where li.Applicable == true
                          select li;
                model.UserVsEmployeeApplicability.Location = Loc.ToList();
            }
            if (model.UserVsEmployeeApplicability.Category != null)
            {
                var Cat = from li in model.UserVsEmployeeApplicability.Category
                          where li.Applicable == true
                          select li;
                model.UserVsEmployeeApplicability.Category = Cat.ToList();
            }
            if (model.UserVsEmployeeApplicability.Grade != null)
            {
                var Grd = from li in model.UserVsEmployeeApplicability.Grade
                          where li.Applicable == true
                          select li;
                model.UserVsEmployeeApplicability.Grade = Grd.ToList();
            }
            if (model.UserVsEmployeeApplicability.PayrollGroup != null)
            {
                var Pay = from li in model.UserVsEmployeeApplicability.PayrollGroup
                          where li.Applicable == true
                          select li;
                model.UserVsEmployeeApplicability.PayrollGroup = Pay.ToList();
            }
            if (model.UserVsEmployeeApplicability.Division != null)
            {
                var Div = from li in model.UserVsEmployeeApplicability.Division
                          where li.Applicable == true
                          select li;
                model.UserVsEmployeeApplicability.Division = Div.ToList();
            }
            if (model.UserVsEmployeeApplicability.Section != null)
            {
                var Sec = from li in model.UserVsEmployeeApplicability.Section
                          where li.Applicable == true
                          select li;
                model.UserVsEmployeeApplicability.Section = Sec.ToList();
            }
            if (model.UserVsEmployeeApplicability.EmploymentType != null)
            {
                var EmpType = from li in model.UserVsEmployeeApplicability.EmploymentType
                              where li.Applicable == true
                              select li;
                model.UserVsEmployeeApplicability.EmploymentType = EmpType.ToList();
            }
            if (model.UserVsEmployeeApplicability.GroupCompany != null)
            {
                var GrpCmp = from li in model.UserVsEmployeeApplicability.GroupCompany
                              where li.Applicable == true
                              select li;
                model.UserVsEmployeeApplicability.GroupCompany = GrpCmp.ToList();
            }

            model.Mode = "CREATE";
            var response = await _userService.UserSave(model);
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
        public async Task<IActionResult> ApproveRequest(User model)
        {
            try { 
            model.ApproveMode = "APPROVE";
            
            var response = await _userService.ApproveOrReject(model);
            return Json(response);
            //if (response.Success == true)
            //{
            //    TempData["Message"] = "Approved Successfully";
            //    return RedirectToAction("Index", "User");
            //}
            //return View("~/Areas/Master/Views/User/ApproveNonEmployee.cshtml", model);
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
        public async Task<ActionResult> Reject(User model)
        {
            try { 
            model.ApproveMode = "REJECT";
            var response = await _userService.ApproveOrReject(model);
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
        public async Task<IActionResult> Modify(User model)
        {
            try { 
            if(model.UserVsEmployeeApplicability.Designation != null)
            {
                var Dsg = from li in model.UserVsEmployeeApplicability.Designation
                              where li.Applicable == true
                              select li;
                model.UserVsEmployeeApplicability.Designation = Dsg.ToList();
            }
            if (model.UserVsEmployeeApplicability.Department != null)
            {
                var Dept = from li in model.UserVsEmployeeApplicability.Department
                                 where li.Applicable == true
                                 select li;
                model.UserVsEmployeeApplicability.Department = Dept.ToList();
            }
            if (model.UserVsEmployeeApplicability.Location != null)
            {
                var Loc = from li in model.UserVsEmployeeApplicability.Location
                               where li.Applicable == true
                               select li;
                model.UserVsEmployeeApplicability.Location = Loc.ToList();
            }
            if (model.UserVsEmployeeApplicability.Category != null)
            {
                var Cat = from li in model.UserVsEmployeeApplicability.Category
                               where li.Applicable == true
                               select li;
                model.UserVsEmployeeApplicability.Category = Cat.ToList();
            }
            if (model.UserVsEmployeeApplicability.Grade != null)
            {
                var Grd = from li in model.UserVsEmployeeApplicability.Grade
                            where li.Applicable == true
                            select li;
                model.UserVsEmployeeApplicability.Grade = Grd.ToList();
            }
            if (model.UserVsEmployeeApplicability.PayrollGroup != null)
            {
                var Pay = from li in model.UserVsEmployeeApplicability.PayrollGroup
                                   where li.Applicable == true
                                   select li;
                model.UserVsEmployeeApplicability.PayrollGroup = Pay.ToList();
            }
            if (model.UserVsEmployeeApplicability.Division != null)
            {
                var Div = from li in model.UserVsEmployeeApplicability.Division
                               where li.Applicable == true
                               select li;
                model.UserVsEmployeeApplicability.Division = Div.ToList();
            }
            if (model.UserVsEmployeeApplicability.Section != null)
            {
                var Sec = from li in model.UserVsEmployeeApplicability.Section
                              where li.Applicable == true
                              select li;
                model.UserVsEmployeeApplicability.Section = Sec.ToList();
            }
            if (model.UserVsEmployeeApplicability.EmploymentType != null)
            {
                var EmpType = from li in model.UserVsEmployeeApplicability.EmploymentType
                                     where li.Applicable == true
                                     select li;
                model.UserVsEmployeeApplicability.EmploymentType = EmpType.ToList();
            }
            if (model.UserVsEmployeeApplicability.GroupCompany != null)
            {
                var GrpCmp = from li in model.UserVsEmployeeApplicability.GroupCompany
                                     where li.Applicable == true
                                     select li;
                model.UserVsEmployeeApplicability.GroupCompany = GrpCmp.ToList();
            }

            model.Mode = "MODIFY";


            var response = await _userService.UserSave(model);
            return Json(response);
            //if (response.Success == true)
            //{
            //    TempData["Message"] = "Saved Successfully";
            //    return RedirectToAction("Index", "User");
            //}
            //if(model.UserType =="ESSP")
            //{
            //    return View("~/Areas/Master/Views/User/EditEsspUser.cshtml", model);
            //}
            //else if(model.UserType =="HR")
            //{
            //    return View("~/Areas/Master/Views/User/EditHrUser.cshtml", model);
            //}
            //else
            //{
            //    return View("~/Areas/Master/Views/User/Create.cshtml", model);
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

        public async Task<IActionResult> ExportToExcel(string TabIndex, int totalRecords)
        {
            try
            {

                if (Global.GlobalFilter == null)
                    Global.GlobalFilter = new Filter();
                else
                {
                    Global.GlobalFilter.length = totalRecords;
                    Global.GlobalFilter.start = 0;
                }

                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("User (" + TabIndex + ")", "Excel");

              

                string currDateTime = await _helpers.GetCurrentDate();
                

                //Get Datset
                var dataset = await _userService.GetUserList_Excel(TabIndex, Global.GlobalFilter);
                DataTable dt = dataset.Tables[1];


                //set EXCEL Column Heading 
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (dt.Columns[i].ColumnName == "doj" || dt.Columns[i].ColumnName == "dob" || dt.Columns[i].ColumnName == "y/s")
                        dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.ToUpper();
                    else
                        dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
                }
                if (TabIndex == "ESSP")
                {
                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("User (" + TabIndex + ")");
                        int count = dt.Rows.Count + 6;


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
                        wsReportNameHeaderRange.Value = "User (" + TabIndex + ")";

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
                        worksheet.Cell(5, 2).Value = dt.Rows.Count;



                        var currentRow = 6;

                        worksheet.Cell(currentRow, 1).InsertTable(dt);
                        worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                        worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                        worksheet.Range("A6:I6").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                        worksheet.Range("A6:I6").Style.Font.Bold = true;


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
                                "User (" + TabIndex + ") List.xlsx"
                                );
                        }
                    }
                }



                else if (TabIndex == "HR")
                {
                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("User (" + TabIndex + ")");
                        int count = dt.Rows.Count + 6;


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
                        wsReportNameHeaderRange.Value = "User (" + TabIndex + ")";

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
                        worksheet.Cell(5, 2).Value = dt.Rows.Count;



                        var currentRow = 6;

                        worksheet.Cell(currentRow, 1).InsertTable(dt);
                        worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                        worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                        worksheet.Range("A6:H6").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                        worksheet.Range("A6:H6").Style.Font.Bold = true;


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
                                "User (" + TabIndex + ") List.xlsx"
                                );
                        }
                    }
                }

                else if (TabIndex == "Non_Employee")
                {
                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("User (" + TabIndex + ")");
                        int count = dt.Rows.Count + 6;


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
                        wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191); ;
                        wsReportNameHeaderRange.Value = "User (" + TabIndex + ")";

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
                        worksheet.Cell(5, 2).Value = dt.Rows.Count;



                        var currentRow = 6;

                        worksheet.Cell(currentRow, 1).InsertTable(dt);
                        worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                        worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                        worksheet.Range("A6:G6").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                        worksheet.Range("A6:G6").Style.Font.Bold = true;


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
                                "User (" + TabIndex + ") List.xlsx"
                                );
                        }
                    }

                }

                else if (TabIndex == "INACTIVE")
                {
                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("User (" + TabIndex + ")");
                        int count = dt.Rows.Count + 6;


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
                        wsReportNameHeaderRange.Value = "User (" + TabIndex + ")";

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
                        worksheet.Cell(5, 2).Value = dt.Rows.Count;



                        var currentRow = 6;

                        worksheet.Cell(currentRow, 1).InsertTable(dt);
                        worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                        worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                        worksheet.Range("A6:I6").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                        worksheet.Range("A6:I6").Style.Font.Bold = true;


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
                                "User (" + TabIndex + ") List.xlsx"
                                );
                        }
                    }

                }

                else
                {
                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("User (" + TabIndex + ")");
                        int count = dt.Rows.Count + 6;


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
                        wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191); ;
                        wsReportNameHeaderRange.Value = "User (" + TabIndex + ")";

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
                        worksheet.Cell(5, 2).Value = dt.Rows.Count;



                        var currentRow = 6;

                        worksheet.Cell(currentRow, 1).InsertTable(dt);
                        worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                        worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                        worksheet.Range("A6:J6").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                        worksheet.Range("A6:J6").Style.Font.Bold = true;


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
                                "User (" + TabIndex + ") List.xlsx"
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
