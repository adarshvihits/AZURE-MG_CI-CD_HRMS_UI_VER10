using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.PayRoll;
using Hrms.Lite.Shared.Account;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll;
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

namespace Hrms.Lite.UI.Areas.PayRoll.Controllers
{
    [Area("PayRoll")]
    [SessionAuthorize]
    public class GratuityProcessingController : Controller
    {


        private readonly IDropdownService _dropdownService;
        private readonly IGratuityProcessingService _GratuityProcessingService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;

        public static class Global
        {
            public static int Allowance;
            public static Filter GlobalFilter;

        }

        public GratuityProcessingController(IGratuityProcessingService gratuityProcessingService, IDropdownService dropdownService, ICommonService commonService, IHelpers helpers)
        {
            _GratuityProcessingService = gratuityProcessingService;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = helpers;
        }


        public async Task<IActionResult> Index()
        {
            GratuityProcessing model = new GratuityProcessing();
            Filter filter = new Filter();
            model.Filter = filter;
            await fillfilterDropdown();
            // model = await _GratuityProcessingService.GetGratuityList(filter);
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
        public async Task<IActionResult> GetBreakingServiceDetails(Guid EmployeeGI,int EmpCode)
        {
            GratuityProcessing model = new GratuityProcessing();
            GratuityProcessing model1 = new GratuityProcessing();
            model.EmployeeData = new GratuityProcessing_EmployeeList();
            model.EmployeeGaratuity = new EmployeeVsGratuity();
            model = await _GratuityProcessingService.GetProcessingStage(EmployeeGI);
            string processinStage = model.ProcessingStage;
            if (model.ProcessingStage == "2")
            {
                model1 = await _GratuityProcessingService.GetEmployeeVsGratuityDetails(EmployeeGI);
            
            }
            if (model.ProcessingStage == "3")
            {
                model1 = await _GratuityProcessingService.GetEmployeeVsGratuityDetails(EmployeeGI);

            }
            model = await _GratuityProcessingService.GetGratuityDetails(EmployeeGI);
            model.EmployeeGI = EmployeeGI;
            model.EmployeeHeader = await _commonService.GetEmployeeHeader(model.EmployeeGI);
            model.BreakingService = new List<EmployeeVsBreakingService>();
            EmployeeVsBreakingService list = new EmployeeVsBreakingService()

            {

                BreakingServiceCode=0,
                BreakingServiceGI= EmployeeGI,
                EmpCode=0,
                StartDate=null,
                EndDate=null,
                Remarks=null,
                TotalDays=0


            };
            model.BreakingService.Add(list);
            model.EmployeeCode = EmpCode;
           
            model.EmployeeData = await _GratuityProcessingService.GetHeaderDetails(EmployeeGI);
            model.ProcessingStage = processinStage;
            model.EmployeeGaratuity = model1.EmployeeGaratuity;




            return View("GratuityBreakingServiceDetails", model);
        }



        [HttpPost]
        public async Task<IActionResult> GetgratuityEmployeeList(Filter filter1)
        {
            try
            {
                GratuityProcessing model = new GratuityProcessing();
                GratuityProcessing m = new GratuityProcessing();

                Filter filter = new Filter();
                Global.GlobalFilter = filter1;
                model = await _GratuityProcessingService.GetGratuityList(filter1);

                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }


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
                        draw = filter1.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows
                    });
                }




                if (model.EmployeeList.Count > 0)
                {
                    m.EmployeeList = model.EmployeeList;
                    totalrows = m.EmployeeList[0].Active;

                }


                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = m.EmployeeList[0].totalRecords;
                    return Json(new
                    {
                        data = model.EmployeeList,
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
        public async Task<ActionResult> SaveBreakingService(GratuityProcessing model)
        {
            try
            {
                var response = new object();
                if (model.BreakingService.Count >= 1)
                {
                    if (model.BreakingService[0].StartDate != null)
                    {
                         response = await _GratuityProcessingService.SaveBreakingService(model);                      

                    }

                }
                else                
                     response = "GoToNext";              

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

        public async Task<IActionResult> GetGratuityDetails(Guid EmployeeGI)
        {
            GratuityProcessing model = new GratuityProcessing();
            model.EmployeeGaratuity = new EmployeeVsGratuity();
            model = await _GratuityProcessingService.GetEmployeeVsGratuityDetails(EmployeeGI);
            model.EmployeeGI = EmployeeGI;
            return PartialView("_GratuitydetailsApprove",model);
        }


        [HttpPost]
        public async Task<ActionResult> ApproveGratuityDetails(GratuityProcessing model)
        {
            try
            {
                var response = await _GratuityProcessingService.SaveGratuityDetails(model);
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

        public async Task<IActionResult> GetPaymentDetails(Guid EmployeeGI)
        {
            GratuityProcessing model = new GratuityProcessing();
            //model.EmployeeGaratuity = new EmployeeVsGratuity();
            //model = await _GratuityProcessingService.GetEmployeeVsGratuityDetails(EmployeeGI);
            model = await _GratuityProcessingService.GetEmployeeVsGratuityDetails(EmployeeGI);
          
            return PartialView("_PaymentDetails",model);
        }
        [HttpPost]
        public async Task<ActionResult> SavePaymentDetails(GratuityProcessing model)
        {
            try
            {
                var response = await _GratuityProcessingService.SavePaymentDetails(model);
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



        public async Task<IActionResult> ExportToExcel(Filter filter, int totRecords)

        {
            try
            {

                if (Global.GlobalFilter == null)
                    Global.GlobalFilter = new Filter();
                else
                {
                    Global.GlobalFilter.length = totRecords;
                    Global.GlobalFilter.start = 0;
                }
                string FilterValues = await _helpers.GetFilterValues(Global.GlobalFilter);

                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Gratuity Processing", "Excel");
                GratuityProcessing model = new GratuityProcessing();
                if (FilterValues == "")
                {
                    FilterValues = "All";
                }

                string currDateTime = await _helpers.GetCurrentDate();
                

                //Get Datset

                var dataset = await _GratuityProcessingService.GratuityListExcel(filter);
                DataTable dt = dataset.Tables[0];


                //set EXCEL Column Heading 
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (dt.Columns[i].ColumnName == "doj" || dt.Columns[i].ColumnName == "doc" || dt.Columns[i].ColumnName == "lwd")
                        dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.ToUpper();
                    else
                        dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
                }

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Gratuity Processing List");
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
                    //wsReportNameHeaderRange.Value = "Variable Provisions & Perk(" + AllowanceType + ")";
                    wsReportNameHeaderRange.Value = "Gratuity Processing List";

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
                            "Gratuity processing List.xlsx"
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
