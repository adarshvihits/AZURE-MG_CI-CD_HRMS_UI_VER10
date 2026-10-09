using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.DataBank.Reports;
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
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.DataBank.Reports.Controllers
{
    [Area("DataBank")]
    [SessionAuthorize]

    public class GrowthHistoryController : Controller
    {
        private readonly IGrowthHistoryService _growthHistoryService;
        private readonly IHelpers _helpers;
        private readonly IDropdownService _dropdownService;
        private readonly ICommonService _commonService;
        
        public GrowthHistoryController(IGrowthHistoryService growthHistoryService, IHelpers Helpers, IDropdownService dropdownService, ICommonService commonService)
        {
            _growthHistoryService = growthHistoryService;
            _helpers = Helpers;
            _dropdownService = dropdownService;
            _commonService = commonService;
        }
        public async Task<IActionResult> Index()
        {
            try{
            EmployeeProfile model = new EmployeeProfile();
            //Filter filterdata = new Filter();
            //model = await _growthHistoryService.GetEmployeeList(0, filterdata);
            model.Filter = await _commonService.GetFilterDropdowns();

            return View("~/Areas/DataBank/Reports/Views/GrowthHistory/Index.cshtml",model);
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
        public async Task<IActionResult> Index2()
        {
            EmployeeProfile model = new EmployeeProfile();
            model = await _growthHistoryService.GetGrowthHistory(Guid.Parse("66661051-1e4e-46ab-b2c6-c53c1f6c255f"));
            model.EmployeeHeader = await _commonService.GetEmployeeHeader(Guid.Parse("66661051-1e4e-46ab-b2c6-c53c1f6c255f"));
            ViewBag.chartData = JsonConvert.SerializeObject(model.GrowthHistory.Select(m => new { m.EffectiveDate_, m.GrossPay, m.TransitionType, m.Department }));
            return View("~/Areas/DataBank/Reports/Views/GrowthHistory/Index2.cshtml", model);
        }

        [HttpPost]
        public async Task<IActionResult> GetActiveList(Filter filter1, string test)
        {
            try
            {
                EmployeeProfile model = new EmployeeProfile();
                EmployeeProfile m = new EmployeeProfile();
                model.EmployeeHeader = new EmployeeHeader();




                Filter filter = new Filter();
                model.Filter= filter1;


                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }

                model = await _growthHistoryService.GetEmployeeList(filter1.Tabindex, filter1);

                m.EmployeeProfileList = model.EmployeeProfileList;
                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.EmployeeProfileList.Count > 0)
                {
                    if (filter1.Tabindex == 0)
                        totalrows = m.EmployeeProfileList[0].active;
                    else
                        totalrows = m.EmployeeProfileList[0].resigned;


                    //m.OnBoardingList.ToList<Hrms.Lite.Shared.Master.Test>().Count; ;
                }

                //model.ActiveCount = m.ActiveCount;
                if (filter1.searchValue != "")
                {
                    if (model.EmployeeProfileList.Count > 0)
                    {
                        totalrowsafterfiltering = m.EmployeeProfileList[0].totalRecords;
                    }
                    else
                    {
                        totalrowsafterfiltering = 0;
                    }
                    //return Json(new { data = model.EmployeeProfileList, draw = filter1.draw, recordsTotal = totalrows, recordsFiltered = totalrowsafterfiltering });
                    return Json(new
                    {
                        data = model.EmployeeProfileList,
                        draw = filter1.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows,//totalrowsafterfiltering
                        activeCount = model.ActiveCount,
                        resignedCount = model.ResignedCount
                    });
                }
                else
                {
                    // return Json(new { data = model.EmployeeProfileList, draw = filter1.draw, recordsTotal = totalrows, recordsFiltered = totalrows });
                    return Json(new
                    {
                        data = model.EmployeeProfileList,
                        draw = filter1.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                       recordsFiltered = totalrows,
                        activeCount = model.ActiveCount,
                        resignedCount = model.ResignedCount
                    });
                }


            }
            catch (Exception ex)
            {
                return View();
            }




        }




      
      
        public async Task<IActionResult> Create(Guid EmployeeGI)
        {
            EmployeeProfile model = new EmployeeProfile();
            model = await _growthHistoryService.GetGrowthHistory(EmployeeGI);
            model.EmployeeHeader = await _commonService.GetEmployeeHeader(EmployeeGI);           
            ViewBag.chartData = JsonConvert.SerializeObject(model.GrowthHistory.Select(m => new { m.EffectiveDate_,m.GrossPay,m.TransitionType,m.Department }));
            return View("~/Areas/DataBank/Reports/Views/GrowthHistory/Create.cshtml",model);
        }

        public async Task<IActionResult> GetGrowthHistoryDetails(Guid EmployeeGI, Guid HistoryGI)
        {
            JobTransition model = new JobTransition();
            model = await _growthHistoryService.GetGrowthHistoryDetails(EmployeeGI, HistoryGI);
            return PartialView("~/Areas/DataBank/Reports/Views/GrowthHistory/_InnerList.cshtml",model);
        }
        public async Task<IActionResult> ExportToExcel(int TabIndex, string TabName,int TotalRow,Filter filter)
        {
            
            try
            {
               
                    filter.length = TotalRow;
                    filter.start = 0;
                
                string FilterValues = await _helpers.GetFilterValues(filter);
               
                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Growth History (" + TabName + ")", "Excel");

                if (FilterValues == "")
                {
                    FilterValues = "All";
                }
                string currDateTime = await _helpers.GetCurrentDate();
                
                //Get Dataset
                var dataset = await _growthHistoryService.GetEmployeeList_Excel(TabIndex,filter);
                DataTable dt = dataset.Tables[0];

                dataset.Tables[0].Rows.RemoveAt(0);
                if ((dataset.Tables[0]?.Rows?.Count ?? 0) > 0)
                {
                    dt = dataset.Tables[0];
                }
                //set EXCEL Column Heading 
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (dt.Columns[i].ColumnName == "doj" || dt.Columns[i].ColumnName == "y/s" )
                        dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.ToUpper();
                    else
                        dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
                }

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Growth History(" + TabName + ")");
                    int count = dt.Rows.Count+5;


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
                    wsReportNameHeaderRange.Value = "Growth History (" + TabName + ")";

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
                    worksheet.Cell(3, 5).Style.Font.Bold = true;
                    worksheet.Cell(3, 5).Value = "Total Count";
                    worksheet.Cell(3, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(3, 6).Value = dt.Rows.Count;
                    worksheet.Cell(4, 1).Style.Font.Bold = true;
                    worksheet.Cell(4, 1).Value = "Filter Applied";
                    worksheet.Range("B4:I4").Merge();
                    worksheet.Cell("B4").Value = FilterValues;



                    var currentRow = 5;



                    worksheet.Cell(currentRow, 1).InsertTable(dt);
                    worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                    worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                    worksheet.Range("A5:I5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Range("A5:I5").Style.Font.Bold = true;
                    
                    worksheet.Range("H5:H" + (dt.Rows.Count + 5)).Style.DateFormat.Format = "dd-MMM-yyyy";
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
                            "Growth History " + TabName + " List.xlsx"
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
