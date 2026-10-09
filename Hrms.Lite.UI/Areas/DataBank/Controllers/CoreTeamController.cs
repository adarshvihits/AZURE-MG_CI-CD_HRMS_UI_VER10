using Microsoft.AspNetCore.Mvc;
using Hrms.Lite.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.DataBank;
using Hrms.Lite.Services.IServices.DataBank;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Hrms.Lite.Shared.Account;
using System.Data;
using ClosedXML.Excel;
using System.IO;
using Spire.Xls;

namespace Hrms.Lite.UI.Areas.DataBank.Controllers
{
    [Area("DataBank")]
    [SessionAuthorize]
    public class CoreTeamController : Controller
    {
        private readonly ICommonService _commonService;
        private readonly ICoreTeamService _coreTeamService;
        private readonly IHelpers _helpers;
        public CoreTeamController(ICommonService commonService, ICoreTeamService coreTeamService, IHelpers helpers)
        {
            _commonService = commonService;
            _coreTeamService = coreTeamService;
            _helpers = helpers;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                CoreTeam model = new CoreTeam();
                model.filter = new Filter();
                model = await _coreTeamService.GetCoreTeamList(0,model.filter);
                model.filter = await _commonService.GetFilterDropdowns();
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
        //public async Task<IActionResult> GetTabCount()
        //{
        //    CoreTeam model = new CoreTeam();
        //    //model.AllCount = AllCount;
        //    //model.CoreTeamCount = ResignedCount;
        //    return Json(model);
        //}


        [HttpPost]
        public async Task<IActionResult> GetList(int TabIndex, Filter filter)
 
        {
            try
            {
                CoreTeam model = new CoreTeam();
                if (filter.Advance_Filter.AgeAboveOrBelow == "Less Than")
                {
                    filter.Advance_Filter.AgeAboveOrBelow = "Below";
                }
                if (filter.Advance_Filter.AgeAboveOrBelow == "Greater Than")
                {
                    filter.Advance_Filter.AgeAboveOrBelow = "Above";
                }
                if (filter.Advance_Filter.YSAboveOrBelow == "Less Than")
                {
                    filter.Advance_Filter.YSAboveOrBelow = "Below";
                }
                if (filter.Advance_Filter.YSAboveOrBelow == "Greater Than")
                {
                    filter.Advance_Filter.YSAboveOrBelow = "Above";
                }

                if (filter.searchValue == "undefined" || filter.searchValue == null)
                {
                    filter.searchValue = "";
                }
                model = await _coreTeamService.GetCoreTeamList(TabIndex, filter);
                model.Tabindex = TabIndex;
                if (TabIndex == 0)
                    return PartialView("_AllList", model);
                else
                    return PartialView("_CoreTeamList", model);
                
                //int totalrows = 0;
                //int totalrowsafterfiltering = 0;

                //if (model.TeamList == null || model.TeamList.Count() == 0)
                //{


                //    return Json(new
                //    {
                //        data = "",
                //        draw = filter1.draw,
                //        recordsTotal = totalrows,
                //        recordsFiltered = totalrows
                //    });
                //}
                //if (model.TeamList.Count > 0)
                //{
                //    totalrows = model.TeamList[0].active;

                //}

                //if (filter1.searchValue != "")
                //{
                //    totalrowsafterfiltering = model.TeamList[0].totalRecords;
                //    return Json(new
                //    {
                //        data = model.TeamList,
                //        draw = filter1.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                //        recordsTotal = totalrows,
                //        recordsFiltered = totalrows,//totalrowsafterfiltering


                //        AllCount = model.AllCount,
                //       CoreTeamCount = model.CoreTeamCount,



                //    });
                //}
                //else
                //{
                //    return Json(new
                //    {
                //        data = model.TeamList,
                //        draw = filter1.draw,//Request.Form["draw"], 
                //        recordsTotal = totalrows,
                //        recordsFiltered = totalrows,
                //        AllCount = model.AllCount,
                //        CoreTeamCount = model.CoreTeamCount,

                //    });
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
        public async Task<IActionResult> CoreTeamSave(CoreTeam model)
            {
            try
            {
                var response = new object();

                //var list = from li in model.TeamList
                //           where li.Applicable == true
                //           select li;
                //model.TeamList = list.ToList();
                response = await _coreTeamService.CoreTeamSave(model);
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
        public async Task<IActionResult> ExportToExcel(int TabIndex, Filter filter)
        {
            var LoginUser = HttpContext.Session.GetString("LoginUser");
            var CompanyName = HttpContext.Session.GetString("CompanyName");
            var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
            var GroupCompanyName = User.authUser.GroupCompanyName;
            ExcelPassword EP = new ExcelPassword();
            EP = await _commonService.GetExcelPassword("Core Team", "Excel");

            string currDateTime = await _helpers.GetCurrentDate();
            

            CoreTeam model = new CoreTeam();
            filter.length = model.CoreTeamCount;
            var dataset = await _coreTeamService.GetCoreTeam_Excel(TabIndex, filter);

            DataTable dt = dataset.Tables[0];
            DataTable dt1 = dataset.Tables[1];

            //set EXCEL Column Heading 
            for (int i = 0; i < dt1.Columns.Count; i++)
            {

                dt1.Columns[i].ColumnName = char.ToUpper(dt1.Columns[i].ColumnName[0]) + dt1.Columns[i].ColumnName.Substring(1);
            }


            using (XLWorkbook wb = new XLWorkbook())
            {


                var worksheet = wb.Worksheets.Add("Core Team");
                int rowcount = dt1.Rows.Count + 3;
                string columnName = "";
                int Columncount = dt1.Columns.Count;

                while (Columncount > 0)
                {
                    int modulo = (Columncount - 1) % 26;
                    columnName = Convert.ToChar('A' + modulo) + columnName;
                    Columncount = (Columncount - modulo) / 26;
                }

                string colName = columnName;

                worksheet.Range("A1", colName + rowcount).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1", colName + rowcount).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1", colName + rowcount).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1", colName + rowcount).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1", colName + rowcount).Style.Border.LeftBorder = XLBorderStyleValues.Thin;



                var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 8)));
                wsReportNameHeaderRange.Style.Font.Bold = true;
                wsReportNameHeaderRange.Style.Font.FontSize = 15;
                wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);

                wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                wsReportNameHeaderRange.Merge();
                wsReportNameHeaderRange.Value = "Core Team";

                worksheet.Cell(2, 1).Style.Font.Bold = true;
                worksheet.Cell(2, 1).Value = "Group Company Name";
                worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();

                worksheet.Cell(2, 3).Style.Font.Bold = true;
                worksheet.Cell(2, 3).Value = "Login User";
                worksheet.Cell(2, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                worksheet.Cell(2, 4).Value = LoginUser.ToString();
               
                //worksheet.Cell(2, 5).Style.Font.Bold = true;
                //worksheet.Cell(2, 5).Value = "To";
                //worksheet.Cell(2, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                ////worksheet.Cell(2, 6).Value = toDate;
                //worksheet.Cell(2, 6).Style.DateFormat.Format = "dd-MMM-yyyy";


                //worksheet.Cell(3, 1).Style.Font.Bold = true;
                //worksheet.Cell(3, 1).Value = "Login User";
                //worksheet.Cell(3, 2).Value = LoginUser.ToString();
                worksheet.Cell(2, 5).Style.Font.Bold = true;
                worksheet.Cell(2, 5).Value = "Date & Time";
                worksheet.Cell(2, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                worksheet.Cell(2, 6).Style.DateFormat.Format = "dd-MMM-yyyy   h:mm AM/PM";
                worksheet.Cell(2, 6).Value = currDateTime.ToString();
                worksheet.Cell(2, 7).Style.Font.Bold = true;
                worksheet.Cell(2, 7).Value = "Total Count";
                worksheet.Cell(2, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                worksheet.Cell(2, 8).Value = dt1.Rows.Count;


                var currentRow = 3;

                worksheet.Cell(currentRow, 1).InsertTable(dt1);
                worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                worksheet.Range("A3:" + colName + "3").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                worksheet.Range("A3:" + colName + "3").Style.Font.Bold = true;

                worksheet.Columns().AdjustToContents();
                worksheet.Rows().AdjustToContents();
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    if (EP.NeedPassword)
                    {
                        Workbook book = new Workbook();
                        book.LoadFromStream(stream);
                        //Protect Workbook
                        book.Protect(EP.Password, false, false);
                        book.SaveToStream(stream);
                    }
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Core Team.xlsx");
                }
            }
        }


    }
}
