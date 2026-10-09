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
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.DataBank.Controllers
{
    [Area("DataBank")]
    [SessionAuthorize]
    public class SuccessionPlanningController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly ISuccessionPlanningService _SuccessionPlanningService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        
        public SuccessionPlanningController(ISuccessionPlanningService SuccessionPlanningService, IDropdownService dropdownService, ICommonService commonService, IHelpers Helpers)
        {
            _SuccessionPlanningService = SuccessionPlanningService;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = Helpers;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                SuccessionPlanning model = new SuccessionPlanning();
                Filter filter = new Filter();
                model.SuccessionPlanningList = new List<SuccessionPlanningList>();
                model.SuccessionPlanningList.Add(new SuccessionPlanningList());
                model.TabIndex = "ALL";         
                model = await _SuccessionPlanningService.GetList("ALL", filter);
                model.Filter = await _commonService.GetFilterDropdowns();
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



     

        public async Task<IActionResult> ApplyFilter(string TabIndex,Filter filter)
        {
            SuccessionPlanning model = new SuccessionPlanning();
            model.Filter = filter;
            model = await _SuccessionPlanningService.GetList(TabIndex, filter);
            //return PartialView("~/Areas/DataBank/Views/AssetAllocation/_Filter.cshtml");
            if (TabIndex == "PENDING")
                return PartialView("_PendingList",model);
            else
                return PartialView("_SuccessionPlanningList",model);

        }

        public async Task<ActionResult> SuccessionPlanningList()
        {
            try
            {
                SuccessionPlanning model = new SuccessionPlanning();
                Filter filter = new Filter();
                model.SuccessionPlanningList = new List<SuccessionPlanningList>();
                model.SuccessionPlanningList.Add(new SuccessionPlanningList());
                model.TabIndex = "ALL";
                model = await _SuccessionPlanningService.GetList("ALL", filter);
                model.Filter = await _commonService.GetFilterDropdowns();
                return View("Index",model);

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



        public async Task<ActionResult> GetSuccessionPlanningList(string TabIndex)
        {
            try
            {
                SuccessionPlanning model = new SuccessionPlanning();
                Filter filter = new Filter();
                //model.SuccessionPlanningList = new List<SuccessionPlanningList>();
                //model.SuccessionPlanningList.Add(new SuccessionPlanningList());
                model.TabIndex = TabIndex;
                if (TabIndex == "PENDING")
                    return PartialView("_PendingList", await _SuccessionPlanningService.GetList(TabIndex,filter));
                else
                    return PartialView("_SuccessionPlanningList", await _SuccessionPlanningService.GetList(TabIndex,filter));
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
        public async Task <IActionResult> Successionplanning(Guid KeyPositionGI, Guid EmployeeGI)
        {
            try
            {
                SuccessionPlanning model = new SuccessionPlanning();
                model = await _SuccessionPlanningService.GetSuccessionlanningDetails(KeyPositionGI, EmployeeGI);
                model.EmployeeHeader = new Shared.General.EmployeeHeader();
                model.EmployeeHeader = await _commonService.GetEmployeeHeader(EmployeeGI);
                return View("SuccessionPlanning",model);
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
        public IActionResult AddNewSuccessor(Guid KeyPositionGI,Guid EmployeeGI)
        {
            try
            {
                SuccessionPlanning model = new SuccessionPlanning();
                model.EmployeesType=1;
                model.KeyPositionGI = KeyPositionGI;
                model.EmployeeGI= EmployeeGI;
                return View("_AddNewSuccessor", model);
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


        //[HttpPost]
        //public async Task<IActionResult> GetList(Filter filter1, int EmployeesType, Guid KeyPositionGI, Guid EmployeeGI)
        //{
        //    try
        //    {
        //        SuccessionPlanning model = new SuccessionPlanning();
        //        SuccessionPlanning m = new SuccessionPlanning();
        //        //model.EmployeeHeader = new EmployeeHeader();




        //        Filter filter = new Filter();
        //        Global.GlobalFilter = filter1;
             

        //        if (filter1.searchValue == "undefined" || filter1.searchValue == null)
        //        {
        //            filter1.searchValue = "";
        //        }

        //        model = await _SuccessionPlanningService.GetEmployeeList(EmployeesType, KeyPositionGI, EmployeeGI,filter1);
        //        model.EmployeeGI = EmployeeGI;
        //        m.SuccessorEmployeeList = model.SuccessorEmployeeList;
        //        int totalrows = 0;
        //        int totalrowsafterfiltering = 0;

        //        if (model.SuccessorEmployeeList == null || model.SuccessorEmployeeList.Count() == 0)
        //        {
                

        //            return Json(new
        //            {
        //                data = "",
        //                draw = filter1.draw,//Request.Form["draw"], 
        //                recordsTotal = totalrows,
        //                recordsFiltered = totalrows
        //            });
        //        }




        //        if (model.SuccessorEmployeeList.Count > 0)
        //        {
        //            totalrows = m.SuccessorEmployeeList[0].active;

        //            //m.OnBoardingList.ToList<Hrms.Lite.Shared.Master.Test>().Count; ;
        //        }

        //        //model.AllTabCount = m.AllTabCount;
        //        if (filter1.searchValue != "")
        //        {
        //            totalrowsafterfiltering = m.SuccessorEmployeeList[0].totalRecords;
        //            return Json(new
        //            {
        //                data = model.SuccessorEmployeeList,
        //                draw = filter1.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
        //                recordsTotal = totalrows,
        //                recordsFiltered = totalrows//totalrowsafterfiltering
        //            });
        //        }
        //        else
        //        {
        //            // return Json(new { data = model.EmployeeProfileList, draw = filter1.draw, recordsTotal = totalrows, recordsFiltered = totalrows });
        //            return Json(new
        //            {
        //                data = model.SuccessorEmployeeList,
        //                draw = filter1.draw,//Request.Form["draw"], 
        //                recordsTotal = totalrows,
        //                recordsFiltered = totalrows
        //            });
        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        return View();
        //    }




        //}
        public async Task<IActionResult> GetEmployeeList(int EmployeesType, Guid KeyPositionGI, Guid EmployeeGI)
        {
            try
            {
                SuccessionPlanning model = new SuccessionPlanning();
                model.Filter = new Filter();
                
                model = await _SuccessionPlanningService.GetEmployeeList(EmployeesType, KeyPositionGI, EmployeeGI,model.Filter);
                model.EmployeeGI = EmployeeGI;
                return PartialView("_EmployeeList", model);
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
        public async Task<IActionResult> Save(SuccessionPlanning model)
        {
           

            var list = from li in model.SuccessorEmployeeList
                       where li.Applicable == true
                       select li;
           
            model.SuccessorEmployeeList = list.ToList();
           
            var response = await _SuccessionPlanningService.Save(model);
            return Json(response);
        }

        public async Task<ActionResult> Delete(Guid SuccessionPlanningGI)
        {
            try
            {
                SuccessionPlanning model = new SuccessionPlanning();
                var response = await _SuccessionPlanningService.Delete(SuccessionPlanningGI);
                if (response.Success == true)                
                    response.Message = "Deleted Successfully";                   
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

        //[HttpPost]
        //public async Task<ActionResult> Delete(SuccessionPlanning model)
        //{
        //    try
        //    {
        //        var response = await _SuccessionPlanningService.Delete(model);
        //        return Json(response);
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







        public async Task<IActionResult> ExportToExcel(string TabIndex,Filter filter)
        {
            try
            {
                SuccessionPlanning model = new SuccessionPlanning();
                
                string FilterValues = await _helpers.GetFilterValues(filter);
                model = await _SuccessionPlanningService.GetList(TabIndex, filter);
                int slno = 0;
                string currDateTime = await _helpers.GetCurrentDate();
                
                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Succession Planning (" + TabIndex + ")", "Excel");
                if (FilterValues == "")
                {
                    FilterValues = "All";
                }
                if (TabIndex == "ALL")
                {

                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("AllList");
                        int PendingCountnum = model.SuccessionPlanningList.Count + 5;


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
                        wsReportNameHeaderRange.Value = "Succession Planning" + "(" + TabIndex + ")";
                        worksheet.Range("A5:H5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
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
                        worksheet.Cell(4, 1).Style.Font.Bold = true;
                        worksheet.Cell(4, 1).Value = "Filter Applied";
                        worksheet.Range("B4:J4").Merge();
                        worksheet.Cell("B4").Value = FilterValues;

                        var currentRow = 5;
                        #region Header
                        worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 1).Value = "Sl.No";
                        worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 2).Value = "KP.Code";
                        worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 3).Value = "Key Position";
                        worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 4).Value = "Employee Name";
                        worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 5).Value = "Designation";
                        worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 6).Value = "Department";
                        worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 7).Value = "Location";
                        worksheet.Cell(currentRow, 8).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 8).Value = "Successors";


                        #endregion

                        #region Body
                        foreach (var item in model.SuccessionPlanningList)
                        {
                            currentRow++;
                            slno = slno + 1;
                            worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 1).Value = item.SlNo;
                            worksheet.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 2).Value = item.KeyPositionDesc;
                            worksheet.Cell(currentRow, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 3).Value = item.KeyPosition;
                            worksheet.Cell(currentRow, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 4).Value = item.EmployeeName;
                            worksheet.Cell(currentRow, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 5).Value = item.Designation;
                            worksheet.Cell(currentRow, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 6).Value = item.Department;
                            worksheet.Cell(currentRow, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 7).Value = item.Location;
                            worksheet.Cell(currentRow, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 8).Value = item.Successor;

                            worksheet.Columns(1, 8).Style.Alignment.WrapText = true;
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
                                "Succession Planning All List.xlsx"
                                );
                        }
                    }

                }

                else
                {

                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("PendingList");
                        int PendingCountnum = model.SuccessionPlanningList.Count + 5;


                        worksheet.Range("A1:G" + PendingCountnum).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:G" + PendingCountnum).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:G" + PendingCountnum).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:G" + PendingCountnum).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:G" + PendingCountnum).Style.Border.LeftBorder = XLBorderStyleValues.Thin;



                        var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 6)));
                        wsReportNameHeaderRange.Style.Font.Bold = true;
                        wsReportNameHeaderRange.Style.Font.FontSize = 15;
                        wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                        wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        wsReportNameHeaderRange.Merge();


                        wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                        wsReportNameHeaderRange.Style.Font.FontSize = 15;
                        wsReportNameHeaderRange.Value = "Succession Planning" + "(" + TabIndex + ")";
                        worksheet.Range("A5:G5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
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
                        worksheet.Cell(4, 1).Style.Font.Bold = true;
                        worksheet.Cell(4, 1).Value = "Filter Applied";
                        worksheet.Range("B4:J4").Merge();
                        worksheet.Cell("B4").Value = FilterValues;

                        var currentRow = 5;
                        #region Header
                        worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 1).Value = "Sl.No";
                        worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 2).Value = "KP.Code";
                        worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 3).Value = "Key Position";
                        worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 4).Value = "Employee Name";
                        worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 5).Value = "Designation";
                        worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 6).Value = "Department";
                        worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 7).Value = "Location";



                        #endregion

                        #region Body
                        foreach (var item in model.SuccessionPlanningList)
                        {
                            currentRow++;
                            slno = slno + 1;
                            worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 1).Value = item.SlNo;
                            worksheet.Cell(currentRow, 2).Value = item.KeyPositionDesc;
                            worksheet.Cell(currentRow, 3).Value = item.KeyPosition;
                            worksheet.Cell(currentRow, 4).Value = item.EmployeeName;
                            worksheet.Cell(currentRow, 5).Value = item.Designation;
                            worksheet.Cell(currentRow, 6).Value = item.Department;
                            worksheet.Cell(currentRow, 7).Value = item.Location;

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
                                "Succession planning Pending List.xlsx"
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
