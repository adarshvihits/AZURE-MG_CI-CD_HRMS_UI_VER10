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
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.TimeOffice.Controllers
{
    [Area("TimeOffice")]
    [SessionAuthorize]
    public class HRAbsentRegularizationController : Controller
    {

        private readonly IHRAbsentRegularizationService _service;
        private readonly IDropdownService _dropdownService;
        private readonly IHelpers _helpers;
        private readonly ICommonService _commonService;
        public HRAbsentRegularizationController(IHRAbsentRegularizationService service, 
            IDropdownService dropdownService, ICommonService commonService, IHelpers helpers)
        {
            _service = service;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = helpers;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                HRAbsentRegularization model = new HRAbsentRegularization();
                HRAbsentRegularization m1 = new HRAbsentRegularization();
                Filter filter = new Filter();
                filter.length = 10;
                m1 = await _service.HRAbsentRegularizationList(filter, 0, 0, 0);
                ViewBag.PGroup = new SelectList(await _dropdownService.GetPayrollGroupDropdown(), "Code", "Name");
                ViewBag.Month = new SelectList(await _dropdownService.GetMonthly_AttendanceProcessing_MonthDropdown(0), "Code", "Name");
                ViewBag.Leave = new SelectList(await _dropdownService.GetLeaveFilterDropdown(), "Code", "Name");
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
        [HttpPost]
        public async Task<IActionResult> GetList(Filter filter1, int Pgroup, int Month, int Leave, HRAbsentRegularization _model)
        {
            try
            {
                HRAbsentRegularization model = new HRAbsentRegularization();
                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }
                if(Pgroup!= 0 && Month!=0&& Leave!=0)
                model = await _service.HRAbsentRegularizationList(filter1, Pgroup, Month, Leave);
               
                    if (_model.HRAbsentRegularizationList != null && _model.HRAbsentRegularizationList.Count != 0)
                    {
                        for (int i = 0; i < model.HRAbsentRegularizationList.Count; i++)
                        {
                            for (int j = 0; j < _model.HRAbsentRegularizationList.Count; j++)
                            {
                                if (model.HRAbsentRegularizationList[i].EmpId == _model.HRAbsentRegularizationList[j].EmpId)
                                {
                                    model.HRAbsentRegularizationList[i].Applicable = _model.HRAbsentRegularizationList[j].Applicable;

                                }
                            }
                        }
                    }
               
                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.HRAbsentRegularizationList == null || model.HRAbsentRegularizationList.Count() == 0)
                {


                    return Json(new
                    {
                        data = "",
                        draw = filter1.draw,
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows
                    });
                }
                if (model.HRAbsentRegularizationList.Count > 0)
                {
                    totalrows = model.HRAbsentRegularizationList[0].active;

                }

                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = model.HRAbsentRegularizationList[0].totalRecords;
                    return Json(new
                    {
                        data = model.HRAbsentRegularizationList,
                        draw = filter1.draw,
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows,
                    });
                }
                else
                {
                    return Json(new
                    {
                        data = model.HRAbsentRegularizationList,
                        draw = filter1.draw,
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows,                      

                    });
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
        public async Task<IActionResult> Regularize(HRAbsentRegularization model)
        {
            try
            {             
                   var response = await _service.Regularize(model);
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
        public async Task<IActionResult> EmployeeWiseRegularize(HRAbsentRegularization model)
        {
            try
            {
                var response = await _service.EmployeeWiseRegularize(model);
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
        public async Task<IActionResult> EmployeeWiseABDetails(int EmpCode,Guid EmpGI, int Pgroup, int Month, int Leave, decimal Balance)
        {
            try
            {
                HRAbsentRegularization model = new HRAbsentRegularization();
                model = await _service.EmployeeWiseABDetails(EmpCode, Pgroup, Month, Leave);
                model.EmployeeHeader = await _commonService.GetEmployeeHeader(EmpGI);
                model.LeaveBalance = Balance;                
                return PartialView("_AbscentDetails", model);
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

        public async Task<IActionResult> ExportToExcel(Filter filter, int LCode, int Pcode,int MCode ,int  TotalRows,
            string LName, string MName, string PName )
        {

            var LoginUser = HttpContext.Session.GetString("LoginUser");
            var CompanyName = HttpContext.Session.GetString("CompanyName");
            var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
            var GroupCompanyName = User.authUser.GroupCompanyName;
            ExcelPassword EP = new ExcelPassword();
            EP = await _commonService.GetExcelPassword("HR Absent Regularization List", "Excel");

            string FilterValues = await _helpers.GetFilterValues(filter);
            string currDateTime = await _helpers.GetCurrentDate();
            

            filter.length = TotalRows;
            filter.start = 0;

            var dataset = await _service.List_Excel(filter, Pcode, MCode, LCode);
            DataTable dt = dataset.Tables[0];

            //set EXCEL Column Heading 
            for (int i = 0; i < dt.Columns.Count; i++)
            {
              
                    dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
            }


            using (XLWorkbook wb = new XLWorkbook())
            {


                var worksheet = wb.Worksheets.Add("HR Absent Regularization");
                int rowcount = dt.Rows.Count + 5;
                string columnName = "";
                int Columncount = dt.Columns.Count;

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



                var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, colName));
                wsReportNameHeaderRange.Style.Font.Bold = true;
                wsReportNameHeaderRange.Style.Font.FontSize = 15;
                wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);

                wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                wsReportNameHeaderRange.Merge();
                wsReportNameHeaderRange.Value = "HR Absent Regularization List";

                worksheet.Cell(2, 1).Style.Font.Bold = true;
                worksheet.Cell(2, 1).Value = "Group Company Name";
                worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();



                worksheet.Cell(2, 3).Style.Font.Bold = true;
                worksheet.Cell(2, 3).Value = "PayRoll Group";
                worksheet.Cell(2, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                worksheet.Cell(2, 4).Value = PName;
                worksheet.Cell(2, 5).Style.Font.Bold = true;
                worksheet.Cell(2, 5).Value = "Month";
                worksheet.Cell(2, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                worksheet.Cell(2, 6).Value = MName;

                worksheet.Cell(2, 7).Style.Font.Bold = true;
                worksheet.Cell(2, 7).Value = "Leave";
                worksheet.Cell(2, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                worksheet.Cell(2, 8).Value = LName;

                worksheet.Cell(3, 1).Style.Font.Bold = true;
                worksheet.Cell(3, 1).Value = "Login User";
                worksheet.Cell(3, 2).Value = LoginUser.ToString();
                worksheet.Cell(3, 3).Style.Font.Bold = true;
                worksheet.Cell(3, 3).Value = "Date & Time";
                worksheet.Cell(3, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                worksheet.Cell(3, 4).Style.DateFormat.Format = "dd-MMM-yyyy   h:mm AM/PM";
                worksheet.Cell(3, 4).Value = currDateTime.ToString();
                worksheet.Cell(3, 5).Style.Font.Bold = true;
                worksheet.Cell(3, 5).Value = "Total Count";
                worksheet.Cell(3, 6).Value = dt.Rows.Count;
                worksheet.Cell(4, 1).Style.Font.Bold = true;
                worksheet.Cell(4, 1).Value = "Filter Applied";
                worksheet.Range("B4:" + colName + "4").Merge();
                worksheet.Cell("B4").Value = FilterValues;

                var currentRow = 5;

                worksheet.Cell(currentRow, 1).InsertTable(dt);
                worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                worksheet.Range("A5:" + colName + "5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                worksheet.Range("A5:" + colName + "5").Style.Font.Bold = true;

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
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "HR Absent Regularization List("+MName+").xlsx");
                }
            }
        }
    }
}
