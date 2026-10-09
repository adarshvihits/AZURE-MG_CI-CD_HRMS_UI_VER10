using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.Recruitment;
using Hrms.Lite.Shared.Account;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Recruitment.Reports;
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

namespace Hrms.Lite.UI.Areas.Recruitment.Reports.Controllers
{
    [Area("Recruitment")]
    [SessionAuthorize]
    public class RecruitmentTransactionHistoryController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly IRecruitmentTransactionHistoryService _service;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        public RecruitmentTransactionHistoryController(IDropdownService dropdownService, IRecruitmentTransactionHistoryService service,ICommonService commonService, IHelpers helpers)
        {
            _dropdownService = dropdownService;
            _service = service;
            _commonService = commonService;
            _helpers = helpers;
        }
        public async Task<IActionResult> Index()
        {
            RecruitmentReports model = new RecruitmentReports();
            model.BasedOn = 1;
            model.FromDate = DateTime.Now.AddDays(-15);
            model.ToDate = DateTime.Now;
            ViewBag.Usr = new SelectList(await _dropdownService.GetUserDropdown(), "Code", "Name");
            ViewBag.Menu = new SelectList(await _dropdownService.GetRecruitmentMenuDropdown(), "Code", "Name");
            return View("~/Areas/Recruitment/Reports/Views/RecruitmentTransactionHistory/Index.cshtml",model);
        }
        //public async Task<IActionResult> GetList(int Tab, string Fromdate, string Todate, int UserCode,int MenuCode)
        //{
        //    try
        //    {
        //        RecruitmentReports model = new RecruitmentReports();
        //        model = await _service.RecruitmentTransactionHistoryReport(Tab, Fromdate, Todate, UserCode,MenuCode);
        //        return View("~/Areas/Recruitment/Reports/Views/RecruitmentTransactionHistory/_List.cshtml", model);
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

        [HttpPost]
        public async Task<IActionResult> GetList(Filter filter1,int Tab, string Fromdate, string Todate, int UserCode, int MenuCode)
        {
            try
            {
                RecruitmentReports model = new RecruitmentReports();
                RecruitmentReports m = new RecruitmentReports();
                //model.EmployeeHeader = new EmployeeHeader();




        
                

                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }

                model = await _service.RecruitmentTransactionHistoryReport(Tab, Fromdate, Todate, UserCode, MenuCode,filter1);

                m.RecruitmentTrnHistory = model.RecruitmentTrnHistory;
                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.RecruitmentTrnHistory == null || model.RecruitmentTrnHistory.Count() == 0)
                {
                    //model.RequestVsAuthorityList = new List<RequestVsAuthorityList>();
                    //model.RequestVsAuthorityList.Add(new RequestVsAuthorityList());


                    return Json(new
                    {
                        data = "",
                        draw = filter1.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows
                    });
                }




                if (model.RecruitmentTrnHistory.Count > 0)
                {
                    totalrows = m.RecruitmentTrnHistory[0].active;

                    //m.OnBoardingList.ToList<Hrms.Lite.Shared.Master.Test>().Count; ;
                }

                //model.AllTabCount = m.AllTabCount;
                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = m.RecruitmentTrnHistory[0].totalRecords;
                    return Json(new
                    {
                        data = model.RecruitmentTrnHistory,
                        draw = filter1.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows//totalrowsafterfiltering
                    });
                }
                else
                {
                    // return Json(new { data = model.EmployeeProfileList, draw = filter1.draw, recordsTotal = totalrows, recordsFiltered = totalrows });
                    return Json(new
                    {
                        data = model.RecruitmentTrnHistory,
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




        public async Task<IActionResult> ExportToExcel(int Tab, string Fromdate, string Todate, int UserCode, int MenuCode,Filter filter)
        {
            try
            {

                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Recruitment Transaction History", "Excel");
               
                string currDateTime = await _helpers.GetCurrentDate();
                

                //Get Dataset
                var dataset = await _service.RecruitmentTransactionHistoryReport_Excel(Tab, Fromdate, Todate,UserCode, MenuCode,filter);
                DataTable dt = dataset.Tables[0];

                dataset.Tables[0].Rows.RemoveAt(0);

                if ((dataset.Tables[0]?.Rows?.Count ?? 0) > 0)
                {
                    dt = dataset.Tables[0];
                }
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (dt.Columns[i].ColumnName == "doj")
                        dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.ToUpper();
                    else
                        dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
                }

                using (var workbook = new XLWorkbook())
                {

                    var worksheet = workbook.Worksheets.Add("Recruitment Transaction History");
                    int count = dt.Rows.Count + 4;


                    if (Tab == 1)
                    {
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
                        wsReportNameHeaderRange.Value = "Recruitment Transaction History (Userwise:" + Fromdate + " - " + Todate + ")";
                    }
                    else
                    {
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
                        wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                        wsReportNameHeaderRange.Value = "Recruitment Transaction History (Menuwise:" + Fromdate + " - " + Todate + ")";
                    }

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
                    


                    var currentRow = 4;

                    worksheet.Cell(currentRow, 1).InsertTable(dt);
                    worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                    worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                    if (Tab == 1)
                    {
                        worksheet.Range("A4:H4").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                        worksheet.Range("A4:H4").Style.Font.Bold = true;
                    }
                    else
                    { 
                        worksheet.Range("A4:J4").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                        worksheet.Range("A4:J4").Style.Font.Bold = true;
                    }

                    worksheet.Range("E4:E" + (dt.Rows.Count + 5)).Style.DateFormat.Format = "dd-MMM-yyyy";
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
                            "Recruitment Transaction History List.xlsx"
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
