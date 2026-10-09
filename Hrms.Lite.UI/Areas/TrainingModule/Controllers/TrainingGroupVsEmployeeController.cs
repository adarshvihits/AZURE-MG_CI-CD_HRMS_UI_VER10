using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.TrainingModule;
using Hrms.Lite.Shared.Account;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TrainingModule;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Spire.Xls;

namespace Hrms.Lite.UI.Areas.TrainingModule.Controllers
{
    [Area("TrainingModule")]
    [SessionAuthorize]
    public class TrainingGroupVsEmployeeController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly ITrainingGroupVsEmployeeService _service;
        private readonly IHelpers _helpers;
        private readonly ICommonService _commonService;
        public TrainingGroupVsEmployeeController(IDropdownService dropdownService, ITrainingGroupVsEmployeeService service, IHelpers helpers, ICommonService commonService)
        {
            _dropdownService = dropdownService;
            _service = service;
            _helpers = helpers;
            _commonService = commonService;
        }
        public async Task<IActionResult> Index()
        {
           
            TrainingGroupVsEmployee model = new TrainingGroupVsEmployee();
            model.Filter = await _commonService.GetFilterDropdowns();

            return View(model);
        }
        
        [HttpPost]
        public async Task<IActionResult> GetList(Filter filter1)
        {
            try
            {
                TrainingGroupVsEmployee model = new TrainingGroupVsEmployee();
                TrainingGroupVsEmployee m = new TrainingGroupVsEmployee();



                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }



                model = await _service.GetEmployeeList( filter1);
                List<Dropdown> TrainingGroupVsEmployeeDropdown = new List<Dropdown>();
                TrainingGroupVsEmployeeDropdown = await _dropdownService.GetTrainingGroupVsEmployeeDropdown();
                if (model == null || model.EmployeeList == null || model.EmployeeList.Count() == 0)
                {
                    m.EmployeeList = new List<TrainingGroupVsEmployeeDetails>();
                    model = new TrainingGroupVsEmployee();
                    model.EmployeeList = new List<TrainingGroupVsEmployeeDetails>();
                }
                else
                {
                    m.EmployeeList = model.EmployeeList;
                }
                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.EmployeeList == null || model.EmployeeList.Count() == 0)
                {

                    return Json(new
                    {
                        data = "",
                        TrainingGroupVsEmployeeDropdown = TrainingGroupVsEmployeeDropdown,
                        draw = filter1.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows
                    });
                }




                if (model.EmployeeList.Count > 0)
                {
                    totalrows = m.EmployeeList[0].active;

                }


                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = m.EmployeeList[0].totalRecords;
                    return Json(new
                    {
                        data = model.EmployeeList,
                        TrainingGroupVsEmployeeDropdown = TrainingGroupVsEmployeeDropdown,
                        draw = filter1.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows//totalrowsafterfiltering
                    });
                }
                else
                {
                    return Json(new
                    {
                        data = model.EmployeeList,
                        TrainingGroupVsEmployeeDropdown = TrainingGroupVsEmployeeDropdown,
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
        public async Task<IActionResult> Save(Guid employeeGI, int TrainingGroupCode)
        {
            try
            {
                TrainingGroupVsEmployee model = new TrainingGroupVsEmployee();
                model.TrainingGroupVsEmployeeDetails = new TrainingGroupVsEmployeeDetails();
                model.TrainingGroupVsEmployeeDetails.EmployeeGI = employeeGI;
                model.TrainingGroupVsEmployeeDetails.TrainingGroupCode = TrainingGroupCode;
                var response = await _service.Save(model);
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
        public async Task<IActionResult> BatchUpdate()
        {
            try
            {
                ViewBag.TrainingGroup = new SelectList(await _dropdownService.GetTrainingGroupVsEmployeeDropdown(), "Code", "Name");
                return PartialView("_BatchUpdate");
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
        public async Task<IActionResult> BatchUpdateSave( int TrainingGroupCode, Filter filter)
        {
            try
            {
                var response = await _service.TrainingGroupVsEmployeeBatchUpdate(filter, TrainingGroupCode);
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


        public async Task<IActionResult> ExportToExcel( int totRecords, Filter filter)

        {
            try
            {


                string FilterValues = await _helpers.GetFilterValues(filter);
                filter.length = totRecords;
                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Training Group Vs Employee", "Excel");

                if (FilterValues == "")
                {
                    FilterValues = "All";
                }

                string currDateTime = await _helpers.GetCurrentDate();
                

                //Get Datset
                var dataset = await _service.GetEmployeeList_Excel(filter);
                DataTable dt = dataset.Tables[0];
                dataset.Tables[0].Rows.RemoveAt(0);
                if ((dataset.Tables[0]?.Rows?.Count ?? 0) > 0)
                {
                    dt = dataset.Tables[0];
                }

                //set EXCEL Column Heading 
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (dt.Columns[i].ColumnName == "doj")

                        dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.ToUpper();
                    else
                        dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
                }

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Training Group Vs Employee");
                    int count = dt.Rows.Count + 5;


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
                    wsReportNameHeaderRange.Value = "Training Group Vs Employee";

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
                    worksheet.Range("G5:G" + (dt.Rows.Count + 5)).Style.DateFormat.Format = "dd-MMM-yyyy";


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
                            "Training Group Vs Employee List.xlsx"
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
