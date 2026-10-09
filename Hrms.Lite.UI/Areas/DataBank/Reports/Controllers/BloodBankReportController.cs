using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.DataBank.Reports;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.Account;
using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.DataBank.Reports;
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
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.DataBank.Reports.Controllers
{
    [Area("DataBank")]
    [SessionAuthorize]
    public class BloodBankReportController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly IBloodBankReportService _BloodbankReportService;
        private readonly IHelpers _helpers;
        private readonly ICommonService _commonService;

      

      
        public BloodBankReportController(IBloodBankReportService BloodBankReportService, IDropdownService dropdownService, IHelpers Helpers,ICommonService commonService)
        {
            _BloodbankReportService = BloodBankReportService;
            _dropdownService = dropdownService;
            _helpers = Helpers;
            _commonService = commonService;
        }
        public async Task<IActionResult> Index()
        {
            try {
                BloodBankReport model = new BloodBankReport();
                BloodBankReport m = new BloodBankReport();
                Filter filter = new Filter();
                filter.length = 10;
                m = await _BloodbankReportService.GetBloodBankReport(model);
                model.filter = await _commonService.GetFilterDropdowns();
                ViewBag.BloodgroupType = new SelectList(await _dropdownService.GetBloodGroupDropdown(), "Code", "Name");
                var StorageType = await _commonService.GetFileStorageType();
                ViewBag.Storage = StorageType.Data;
                return View("~/Areas/DataBank/Reports/Views/BloodBankReport/Index.cshtml", model);
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
        public async Task<IActionResult> GetBloodBankList(Filter filter1,string bloodgroup)
        {
            try
            {
                BloodBankReport model = new BloodBankReport();
                BloodBankReport m = new BloodBankReport();



                Filter filter = new Filter();
                model.filter = filter1;


                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }
                if (bloodgroup != null)
                { 
                    string[] values = bloodgroup.Split(',');
                    for (int i = 0; i < values.Length; i++)
                    {
                        values[i] = values[i].Trim();
                    }

                model.BloodGroup = Array.ConvertAll(values, int.Parse);
                }
                model.filter = filter1;
                model = await _BloodbankReportService.GetBloodBankReport(model);

                m.BloodBankReportList = model.BloodBankReportList;
                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.BloodBankReportList == null || model.BloodBankReportList.Count() == 0)
                {
                   

                    return Json(new
                    {
                        data = "",
                        draw = filter1.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows
                    });
                }




                if (model.BloodBankReportList.Count > 0)
                {
                    totalrows = m.BloodBankReportList[0].active;

                    
                }

                //model.AllTabCount = m.AllTabCount;
                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = m.BloodBankReportList[0].totalRecords;
                    return Json(new
                    {
                        data = model.BloodBankReportList,
                        draw = filter1.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows//totalrowsafterfiltering
                    });
                }
                else
                {
                    return Json(new
                    {
                        data = model.BloodBankReportList,
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

        public async Task<IActionResult> BloodBankReportList() //For employee profile breadcrumb
        {
            try { 
            
            return RedirectToAction("Index");
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

        public async Task<IActionResult> GetBloodBankReportList(int[] Boodgroups)
        {
            try { 
            BloodBankReport model = new BloodBankReport();          
            Filter filter = new Filter();
            model.filter = filter;
           
            model.BloodGroup = Boodgroups;
            model = await _BloodbankReportService.GetBloodBankReport(model);
          
            return PartialView("~/Areas/DataBank/Reports/Views/BloodBankReport/_BloodBankReportList.cshtml", model);
                //return PartialView("_BloodBankReportList", model);
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

       
        public async Task<IActionResult> ApplyFilter( Filter filter)
        {
            try { 
            BloodBankReport model = new BloodBankReport();
           model.filter = filter;
           
          
            model = await _BloodbankReportService.GetBloodBankReport(model);
           
            return PartialView("~/Areas/DataBank/Reports/Views/BloodBankReport/_BloodBankReportList.cshtml",model);
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

       

        public async Task<IActionResult> ExportToExcel(string bloodgroup,string[] groups, int totRecords,Filter filter
            )
        {
            try { 
            BloodBankReport _model = new BloodBankReport();

            var LoginUser = HttpContext.Session.GetString("LoginUser");
            var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                int slNo = 0;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Blood Bank Report", "Excel");

                filter.length = totRecords;
                filter.start = 0;
                string FilterValues = await _helpers.GetFilterValues(filter);
            if (FilterValues == "")
            {
                FilterValues = "All";
            }
            //model.filter = Global.GlobalFilter;
            //model.BloodGroup = group;
           
            _model.filter = filter;

                if (bloodgroup != null)
                {
                    string[] values = bloodgroup.Split(',');
                    for (int i = 0; i < values.Length; i++)
                    {
                        values[i] = values[i].Trim();
                    }

                    _model.BloodGroup = Array.ConvertAll(values, int.Parse);
                }             

        
            string currDateTime = await _helpers.GetCurrentDate();
            


                //Get Dataset
                var dataset = await _BloodbankReportService.GetBloodBankReport_Excel(_model);
                DataTable dt = dataset.Tables[0];


                //set EXCEL Column Heading 
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (dt.Columns[i].ColumnName == "doj" || dt.Columns[i].ColumnName == "dob" || dt.Columns[i].ColumnName == "y/s")
                        dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.ToUpper();
                    else
                        dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
                }

                using (var workbook = new XLWorkbook())
            {

                var worksheet = workbook.Worksheets.Add("BloodBank Report List");

                int count = dt.Rows.Count+ 5;

                worksheet.Range("A1:L" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1:L" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1:L" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1:L" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1:L" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;



                var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 11)));
                wsReportNameHeaderRange.Style.Font.Bold = true;
                wsReportNameHeaderRange.Style.Font.FontSize = 15;
                wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                wsReportNameHeaderRange.Merge();
                wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                wsReportNameHeaderRange.Value = "Blood Bank";

                worksheet.Cell(2, 1).Style.Font.Bold = true;
                worksheet.Cell(2, 1).Value = "Group Company Name";
                worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                worksheet.Cell(2, 3).Style.Font.Bold = true;
                worksheet.Cell(2, 3).Value = "Blood Group";

                    int cnt= groups.Count();

                    for (int i=0;i<cnt;i++)
                    {
                        worksheet.Cell(2, 4).Value = groups[i];

                    }
               
               

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
                var filterapplied = worksheet.Range("B4:L4").Merge();

                filterapplied.Value = FilterValues;


                var currentRow = 5;
                    
                    worksheet.Cell(currentRow, 1).InsertTable(dt);
                    worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                    worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                    worksheet.Range("A5:L5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Range("A5:L5").Style.Font.Bold = true;


                    worksheet.Columns().AdjustToContents();
                worksheet.Rows().AdjustToContents();
                //worksheet.Protect("123");
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
                        "Blood Group Report List.xlsx"
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
