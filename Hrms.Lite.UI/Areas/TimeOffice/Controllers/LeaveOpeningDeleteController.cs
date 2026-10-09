using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.TimeOffice;
using Hrms.Lite.Shared.Account;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TimeOffice;
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

namespace Hrms.Lite.UI.Areas.TimeOffice
{
    [Area("TimeOffice")]
    [SessionAuthorize]
    public class LeaveOpeningDeleteController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly ILeaveOpeningService _leaveOpeningService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;


        public static class Global
        {
            public static int LeavePeriod;
            public static int LeaveGroup;
            public static int Leave;
            public static Filter GlobalFilter;
        }

        public LeaveOpeningDeleteController(IDropdownService dropdownService, ILeaveOpeningService leaveOpeningService, ICommonService commonService, IHelpers helpers)
        {
            _leaveOpeningService = leaveOpeningService;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = helpers;
        }

        public async Task<IActionResult> Index()
        {
            LeaveOpening model = new LeaveOpening();
            ViewBag.LeavePeriod = new SelectList(await _dropdownService.GetLeavePeriodFilterDropdown(), "Code", "Name");
            ViewBag.LeaveGroup = new SelectList(await _dropdownService.GetLeaveGroupFilterDropdown(), "Code", "Name");
            ViewBag.Leave = new SelectList(await _dropdownService.GetLeaveFilterDropdown(), "Code", "Name");
  
            await fillfilterDropdown();
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


        public async Task<IActionResult> ApplyFilter(Filter filter)
        {
            try
            {
                LeaveOpening model = new LeaveOpening();
                Global.GlobalFilter = filter;
               
                model = await _leaveOpeningService.LeaveOpeningDeleteList(Global.LeavePeriod, Global.LeaveGroup, Global.Leave, filter);

                return PartialView("_DeleteList", model);

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
        public async Task<IActionResult> GetLeaveOpeningDeleteList(Filter filter1, int LeavePeriod, int LeaveGroup, int Leave)
        {
            try
            {
                LeaveOpening model = new LeaveOpening();
                LeaveOpening m = new LeaveOpening();
                //model.EmployeeHeader = new EmployeeHeader();




                Filter filter = new Filter();
                Global.GlobalFilter = filter1;
                Global.LeavePeriod = LeavePeriod;
                Global.LeaveGroup = LeaveGroup;
                Global.Leave = Leave;

                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }
                if (LeavePeriod != 0 && Leave != 0 && LeaveGroup != 0)
                {
                    model = await _leaveOpeningService.LeaveOpeningDeleteList(LeavePeriod, LeaveGroup, Leave,filter1);
                }
                m.LeaveOpeningList = model.LeaveOpeningList;
                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.LeaveOpeningList == null || model.LeaveOpeningList.Count() == 0)
                {

                    return Json(new
                    {
                        data = "",
                        draw = filter1.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows
                    });
                }




                if (model.LeaveOpeningList.Count > 0)
                {
                    totalrows = m.LeaveOpeningList[0].active;

                }

                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = m.LeaveOpeningList[0].totalRecords;
                    return Json(new
                    {
                        data = model.LeaveOpeningList,
                        draw = filter1.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows//totalrowsafterfiltering
                    });
                }
                else
                {
                    return Json(new
                    {
                        data = model.LeaveOpeningList,
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


        public async Task<IActionResult> Delete(LeaveOpening model,Guid employeeGI)
        {
            try
            {
                model.LeavePeriod = new Shared.Master.LeavePeriod();
                model.LeaveGroup = new Shared.Master.LeaveGroup();
                model.Leave = new Shared.Master.Leave();
             
             
                model.LeavePeriod.Code = Global.LeavePeriod;
                model.LeaveGroup.Code = Global.LeaveGroup;
                model.Leave.Code = Global.Leave;
                model.EmployeeGI = employeeGI;

                var response = await _leaveOpeningService.Delete(model);

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

        public async Task<IActionResult> DeleteAll(LeaveOpening model)
        {
            try
            {
                model.LeaveOpeningBatchUpDate = new LeaveOpeningBatchUpDate();
                model.LeaveOpeningBatchUpDate.LeavePeriod = new Shared.Master.LeavePeriod();
                model.LeaveOpeningBatchUpDate.LeaveGroup = new Shared.Master.LeaveGroup();
                model.LeaveOpeningBatchUpDate.Leave = new Shared.Master.Leave();
                model.LeaveOpeningBatchUpDate.LeavePeriod.Code = Global.LeavePeriod;
                model.LeaveOpeningBatchUpDate.LeaveGroup.Code = Global.LeaveGroup;
                model.LeaveOpeningBatchUpDate.Leave.Code = Global.Leave;
                model.Filter = Global.GlobalFilter;
                var response = await _leaveOpeningService.DeleteAll(model);
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

        //public async Task<IActionResult> GetList(int LeavePeriod, int LeaveGroup, int Leave)
        //{
        //    try
        //    {

        //        LeaveOpening model = new LeaveOpening();
        //        Global.GlobalFilter = new Filter();
        //        Global.LeavePeriod = LeavePeriod;
        //        Global.LeaveGroup = LeaveGroup;
        //        Global.Leave = Leave;
        //        model = await _leaveOpeningService.LeaveOpeningDeleteList(LeavePeriod,LeaveGroup,Leave, Global.GlobalFilter);

        //            return PartialView("_DeleteList", model);

        //    }
        //    catch (UnauthorizedAccessException e)
        //    {

        //        return RedirectToAction("Login", "Account", new { Area = "" });
        //    }
        //    catch (Exception e)
        //    {
        //        return RedirectToAction("Error", "Account", new { Area = "" });
        //    }
        //}

        //[HttpPost]
        //public async Task<IActionResult> Delete(LeaveOpening model)
        //{

        //    model.LeavePeriod = new Shared.Master.LeavePeriod();
        //    model.LeaveGroup = new Shared.Master.LeaveGroup();
        //    model.Leave = new Shared.Master.Leave();
        //    var list = from li in model.LeaveOpeningList
        //               where li.Applicability == true
        //               select li;

        //    model.LeaveOpeningList = list.ToList();
        //    model.LeavePeriod.Code = Global.LeavePeriod;
        //    model.LeaveGroup.Code = Global.LeaveGroup;
        //    model.Leave.Code = Global.Leave;
        //    var response = await _leaveOpeningService.Delete(model);
        //    return Json(response);
        //}








        public async Task<IActionResult> ExportToExcel(string LeavPrdName,string LeavGrpName,string LeavName,int TotalRec)
        {
            try
            {
                LeaveOpening model = new LeaveOpening();
                if (Global.GlobalFilter == null)
                    Global.GlobalFilter = new Filter();
                else
                {
                    Global.GlobalFilter.length = TotalRec;
                    Global.GlobalFilter.start = 0;
                }
                string FilterValues = await _helpers.GetFilterValues(Global.GlobalFilter);
                model = await _leaveOpeningService.LeaveOpeningDeleteList(Global.LeavePeriod, Global.LeaveGroup, Global.Leave, Global.GlobalFilter);
                int slno = 0;
                string currDateTime = await _helpers.GetCurrentDate();
                
                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Leave Opening Delete", "Excel");
                if (FilterValues == "")
                {
                    FilterValues = "All";
                }
           
                {

                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("Leave opening Delete List");
                        int PendingCountnum = model.LeaveOpeningList.Count + 5;


                        worksheet.Range("A1:H" + PendingCountnum).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:H" + PendingCountnum).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:H" + PendingCountnum).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:H" + PendingCountnum).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:H" + PendingCountnum).Style.Border.LeftBorder = XLBorderStyleValues.Thin;



                        var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 7)));
                        wsReportNameHeaderRange.Style.Font.Bold = true;
                        wsReportNameHeaderRange.Style.Font.FontSize = 15;
                        wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                        wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        wsReportNameHeaderRange.Merge();


                        wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                        wsReportNameHeaderRange.Style.Font.FontSize = 15;
                        wsReportNameHeaderRange.Value = "Leave Opening Delete";
                        worksheet.Range("A5:H5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                        worksheet.Cell(2, 1).Style.Font.Bold = true;
                        worksheet.Cell(2, 1).Value = "Group Company Name";
                        worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                     
                        worksheet.Cell(2, 3).Style.Font.Bold = true;
                        worksheet.Cell(2, 3).Value = "Leave Period";
                        worksheet.Cell(2, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(2, 4).Value = LeavPrdName;
                        worksheet.Cell(2, 5).Style.Font.Bold = true;
                        worksheet.Cell(2, 5).Value = "Leave Group";
                        worksheet.Cell(2, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(2, 6).Value = LeavGrpName;
                        worksheet.Cell(2, 7).Style.Font.Bold = true;
                        worksheet.Cell(2, 7).Value = "Leave";
                        worksheet.Cell(2, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(2, 8 ).Value = LeavName;


                        worksheet.Cell(3, 1).Style.Font.Bold = true;
                        worksheet.Cell(3, 1).Value = "Login User";
                        worksheet.Cell(3, 2).Value = LoginUser;
                        worksheet.Cell(3, 3).Style.Font.Bold = true;
                        worksheet.Cell(3, 3).Value = "Date & Time";
                        worksheet.Cell(3, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(3, 4).Style.DateFormat.Format = "dd-MMM-yyyy   h:mm AM/PM";
                        worksheet.Cell(3, 4).Value = currDateTime.ToString();
                        worksheet.Cell(4, 1).Style.Font.Bold = true;
                        worksheet.Cell(4, 1).Value = "Filter Applied";
                        worksheet.Range("B4:H4").Merge();
                        worksheet.Cell("B4").Value = FilterValues;

                        var currentRow = 5;
                        #region Header
                        worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 1).Value = "Sl.No";
                        worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 2).Value = "ID";
                        worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 3).Value = "Employee Name";
                        worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 4).Value = "Fun.Designation";
                        worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 5).Value = "Department";
                        worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 6).Value = "Location";
                        worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 7).Value = "DOJ";
                        worksheet.Cell(currentRow, 8).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 8).Value = "Opening";



                        #endregion

                        #region Body
                        foreach (var item in model.LeaveOpeningList)
                        {
                            currentRow++;
                            slno = slno + 1;
                            worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 1).Value = slno;
                            worksheet.Cell(currentRow, 2).Value = item.EmployeeID;
                            worksheet.Cell(currentRow, 3).Value = item.EmployeeName;
                            worksheet.Cell(currentRow, 4).Value = item.Designation.Name;
                            worksheet.Cell(currentRow, 5).Value = item.Department.Name;
                            worksheet.Cell(currentRow, 6).Value = item.Location.Name;
                            worksheet.Cell(currentRow, 7).Value = item.DOJ;
                            worksheet.Cell(currentRow, 7).Style.DateFormat.Format = "dd-MMM-yyyy";
                            worksheet.Cell(currentRow, 8).Value = item.Opening;

                            worksheet.Columns(1, 7).Style.Alignment.WrapText = true;
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
                                "Leave opening delete List.xlsx"
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
