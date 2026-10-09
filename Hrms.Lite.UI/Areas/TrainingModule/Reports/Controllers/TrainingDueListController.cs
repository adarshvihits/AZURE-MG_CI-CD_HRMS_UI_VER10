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
    public class TrainingDueListController : Controller
    {

        private readonly ITrainingDueListService _TrainingDueListService;
        private readonly IDropdownService _dropdownService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;

        public TrainingDueListController(IDropdownService dropdownService, ICommonService commonService, ITrainingDueListService TrainingDueListService, IHelpers helpers)
        {
            _TrainingDueListService = TrainingDueListService;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = helpers;
        }
        public static class Global
        {
            public static Filter GlobalFilter;
       
        }

        public async Task<IActionResult> Index()
        {
            TrainingDueList model = new TrainingDueList();
            ViewBag.Training = new SelectList(await _dropdownService.GetTrainingDropdown(), "Code", "Name");
            model.Filter = await _commonService.GetFilterDropdowns();
            return View("~/Areas/TrainingModule/Reports/Views/TrainingDueList/Index.cshtml", model);
        }

        [HttpPost]
        public async Task<ActionResult> GetList(int trainingcode, Filter filter)
        {
            try
            {
                TrainingDueList model = new TrainingDueList();
                TrainingDueList m = new TrainingDueList();
              

                if (filter.searchValue == "undefined" || filter.searchValue == null)
                {
                    filter.searchValue = "";
                }
                model = await _TrainingDueListService.GetList(trainingcode, filter);
                m.TrainingDueRptList = model.TrainingDueRptList;
                int totalrows = 0;
                int totalrowsafterfiltering = 0;


                if (model.TrainingDueRptList == null || model.TrainingDueRptList.Count() == 0)
                {


                    return Json(new
                    {
                        data = "",
                        draw = filter.draw,
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows
                    });
                }

                if (model.TrainingDueRptList.Count > 0)
                {
                    totalrows = m.TrainingDueRptList[0].active;

                }

                if (filter.searchValue != "")
                {
                    totalrowsafterfiltering = m.TrainingDueRptList[0].totalRecords;
                    return Json(new
                    {
                        data = model.TrainingDueRptList,
                        draw = filter.draw,
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows
                    });
                }
                else
                {
                    return Json(new
                    {
                        data = model.TrainingDueRptList,
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

      

        public async Task<IActionResult> ApplyFilter( int trainingcode, Filter filter)
        {
            try
            {
                TrainingDueList model = new TrainingDueList();
                Global.GlobalFilter = filter;
                model = await _TrainingDueListService.GetList(trainingcode, filter);
                return PartialView("~/Areas/TrainingModule/Reports/Views/TrainingDueList/_TrainingDueList.cshtml", model);


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




        public async Task<IActionResult> ExportToExcel(int trainingcode, Filter filter, int TotalRows)
        {
            try
            {
                TrainingDueList model = new TrainingDueList();
                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
               
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Training Report List", "Excel");
                int slNo = 0;

                filter.length = TotalRows;
                filter.start = 0;

                string FilterValues = await _helpers.GetFilterValues(filter);
                model = await _TrainingDueListService.GetList(trainingcode,filter);

                if (FilterValues == "")
                {
                    FilterValues = "All";
                }
                model.Filter = filter;
                string currDateTime = await _helpers.GetCurrentDate();
                

                //Get Dataset
                var dataset = await _TrainingDueListService.TrainingDueList_Excel(trainingcode,filter);
                DataTable dt = dataset.Tables[0];
                dataset.Tables[0].Rows.RemoveAt(0);
                if ((dataset.Tables[0]?.Rows?.Count ?? 0) > 0)
                {
                    dt = dataset.Tables[0];
                }


                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Training Due List");
                    int count = model.TrainingDueRptList.Count + 5;


                    worksheet.Range("A1:M" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:M" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:M" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:M" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:M" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;

                    var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 12)));
                    wsReportNameHeaderRange.Style.Font.Bold = true;
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Merge();
                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191); ;
                    wsReportNameHeaderRange.Value = "Training Due List";

                    worksheet.Cell(2, 1).Style.Font.Bold = true;
                    worksheet.Cell(2, 1).Value = "Group Company Name";
                    worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                    worksheet.Cell(2, 3).Style.Font.Bold = true;
                    
                   

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
                    worksheet.Cell(3, 6).Value = model.TrainingDueRptList.Count;
                    worksheet.Cell(4, 1).Style.Font.Bold = true;
                    worksheet.Cell(4, 1).Value = "Filter Applied";
                    worksheet.Range("B4:M4").Merge();
                    worksheet.Cell("B4").Value = FilterValues;





                    var currentRow = 5;
                    #region Header
                    worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 1).Value = "Sl no";
                    worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 2).Value = "EmployeeID";
                    worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 3).Value = "Name";
                    worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 4).Value = "Designation";
                    worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 5).Value = "Department";
                    worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 6).Value = "Location";
                    worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 7).Value = "DOJ";
                    worksheet.Cell(currentRow, 8).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 8).Value = "Training";
                    worksheet.Cell(currentRow, 9).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 9).Value = "Training Group";
                    worksheet.Cell(currentRow, 10).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 10).Value = "Duration";
                    worksheet.Cell(currentRow, 11).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 11).Value = "Last Attended Date";
                    worksheet.Cell(currentRow, 12).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 12).Value = "Due Date";
                    worksheet.Cell(currentRow, 13).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 13).Value = "Ageing";


                    #endregion

                    #region Body
                    foreach (var item in model.TrainingDueRptList)
                    {
                        currentRow++;
                        slNo = slNo + 1;
                        worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Range("A5:M5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
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
                        worksheet.Cell(currentRow, 7).Value = item.DOJ;
                        worksheet.Cell(currentRow, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 8).Value = item.Training;
                        worksheet.Cell(currentRow, 9).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 9).Value = item.TrainingGroup;
                        worksheet.Cell(currentRow, 10).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 10).Value = item.Duration;
                        worksheet.Cell(currentRow, 11).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 11).Style.DateFormat.Format = "dd-MMM-yyyy";
                        worksheet.Cell(currentRow, 11).Value = item.Lastattended;
                        worksheet.Cell(currentRow, 12).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 12).Style.DateFormat.Format = "dd-MMM-yyyy";
                        worksheet.Cell(currentRow, 12).Value = item.Duedate;
                        worksheet.Cell(currentRow, 13).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 13).Value = item.Ageing;



                        worksheet.Columns(1, 8).Style.Alignment.WrapText = true;
                        worksheet.Rows().Style.Alignment.WrapText = true;
                    }
                    #endregion
                    worksheet.Range("G5:G" + (dt.Rows.Count + 5)).Style.DateFormat.Format = "dd-MMM-yyyy";
                    worksheet.Range("K5:K" + (dt.Rows.Count + 5)).Style.DateFormat.Format = "dd-MMM-yyyy";
                    worksheet.Range("L5:L" + (dt.Rows.Count + 5)).Style.DateFormat.Format = "dd-MMM-yyyy";
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
                            "TrainingDueList.xlsx"
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


