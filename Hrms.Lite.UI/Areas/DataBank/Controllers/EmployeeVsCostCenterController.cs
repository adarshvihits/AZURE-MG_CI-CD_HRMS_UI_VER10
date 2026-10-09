using ClosedXML.Excel;
using ExcelDataReader;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.Account;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using Microsoft.AspNetCore.Hosting;
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

namespace Hrms.Lite.UI.Areas.DataBank.Controllers
{
    [Area("DataBank")]
    [SessionAuthorize]
    public class EmployeeVsCostCenterController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly IEmployeeVsCostCenterService _service;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        private readonly IWebHostEnvironment _environment;
        
        public EmployeeVsCostCenterController(IDropdownService dropdownService, IEmployeeVsCostCenterService Service, ICommonService commonService, IHelpers helpers, IWebHostEnvironment env)
        {
            _service = Service;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = helpers;
            _environment = env;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                
            CostCenter model = new CostCenter();
            Filter filter = new Filter();
            filter.length = 10;
            model = await _service.GetList("Assigned", filter);
            CostCenter m1 = new CostCenter();
            m1.Filter = new Filter();
            m1.AssignOrUnassign = "Assigned";
            m1.Filter = await _commonService.GetFilterDropdowns();
            return View(m1);
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

        public async Task<IActionResult> AddCostCenter()
        {
            try
            {
                CostCenter model = new CostCenter();
                ViewBag.CostCenter = new SelectList(await _dropdownService.GetCostCenterCombo(), "Code", "Name");
                return PartialView("_MappingSlider", model);
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
        public async Task<JsonResult> GetCostCenterCombo()
        {
            var CostCenterCombo = new SelectList(await _dropdownService.GetCostCenterCombo(), "Code", "Name");
            return Json(CostCenterCombo);
        }
        [HttpPost]
        public async Task<IActionResult> Create(int CostCode,Guid EmployeeGI)
        {
            try
            {
                CostCenter model = new CostCenter();
                model.CostCenterCode = CostCode;
                model.EmployeeGI = EmployeeGI;
               if (EmployeeGI == Guid.Parse("00000000-0000-0000-0000-000000000000")){
                    var response = await _service.BatchUpdate(model);
                    response.Message = "Updated Successfully";
                    return Json(response);
                }
                else
                {
                    var response = await _service.Create(model);
                    response.Message = "Updated Successfully";
                    return Json(response);
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
        public async Task<IActionResult> GetList(Filter filter1, string TabIndex)
        {
            try
            {
                CostCenter model = new CostCenter();
                CostCenter m = new CostCenter();

                Filter filter = new Filter();
                model.Filter = filter1;
                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }
                    model = await _service.GetList(TabIndex, filter1);
                m.CostCenterTablist = model.CostCenterTablist;
                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.CostCenterTablist == null || model.CostCenterTablist.Count() == 0)
                {

                    return Json(new
                    {
                        data = "",
                        draw = filter1.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows
                    });
                }




                if (model.CostCenterTablist.Count > 0)
                {
                    totalrows = m.CostCenterTablist[0].active;


                }


                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = m.CostCenterTablist[0].totalRecords;
                    return Json(new
                    {
                        data = model.CostCenterTablist,
                        draw = filter1.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows//totalrowsafterfiltering
                    });
                }
                else
                {
                    return Json(new
                    {
                        data = model.CostCenterTablist,
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
        public async Task<IActionResult> ImportFromExcel(CostCenter model,string TabIndex)
        {
            string path = Path.Combine(this._environment.WebRootPath, "Uploads");
            string fileName = model.Excel.FileData.FileName;
            try
            {
                model.CostCenterList = new List<CostCenter>();
                string wwwPath = this._environment.WebRootPath;
                string contentPath = this._environment.ContentRootPath;


                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                {
                    model.Excel.FileData.CopyTo(stream);

                }
                try
                {
                    using (var stream = System.IO.File.Open(path + "\\" + fileName, FileMode.Open, FileAccess.Read))
                    {

                        using (var reader = ExcelReaderFactory.CreateReader(stream))
                        {
                            List<CostCenter> List = new List<CostCenter>();
                            var readerrr = ExcelReaderFactory.CreateReader(stream);
                            while (reader.Read()) //Each row of the file
                            {
                                var item = new CostCenter();
                                if (reader.GetValue(0) != null)
                                {
                                    item.EmployeeID = reader.GetValue(0).ToString();
                                }
                                if (reader.GetValue(1) != null)
                                {
                                    item.Name = reader.GetValue(1).ToString();
                                }

                                if (reader.GetValue(2) != null)
                                {
                                    item.CostCenterName = reader.GetValue(2).ToString();
                                }



                                List.Add(item);
                                
                            }
                            if (List[0].EmployeeID != "Employee ID" || List[0].Name != "Employee Name"|| List[0].CostCenterName != "Cost Center")
                            {
                                reader.Close();
                                if (System.IO.File.Exists(path + "\\" + fileName))
                                {
                                    System.IO.File.Delete(path + "\\" + fileName);
                                }
                                return Json(new ResponseEntity("Excel File not in the prescribed format", false));
                            }
                            model.CostCenterList = List;

                            model.CostCenterList.RemoveAt(0);
                        }
                    }
                }
                catch (Exception e)
                {
                    // Check if file exists with its full path    
                    if (System.IO.File.Exists(path + "\\" + fileName))
                    {
                        System.IO.File.Delete(path + "\\" + fileName);
                    }
                    return Json(new ResponseEntity("Excel File not in the prescribed format", false));
                }

                model.Excel.FileData = null;
                var response = await _service.ImportFromExcel(TabIndex, model);
                // Check if file exists with its full path    
                if (System.IO.File.Exists(path + "\\" + fileName))
                {
                    System.IO.File.Delete(path + "\\" + fileName);
                }
                if (response.Message != "UPDATED")
                {
                    model.CostCenterList = response.Data;
                    model.Message = response.Message;
                    return PartialView("_UnmatchedList", model);
                }
                else
                {
                    response.Message = "Uploaded Successfully";
                    return Json(response);
                }
            }
            catch (UnauthorizedAccessException e)
            {

                return RedirectToAction("Login", "Account", new { Area = "" });
            }
            catch (Exception e)
            {
                // Check if file exists with its full path    
                if (System.IO.File.Exists(path + "\\" + fileName))
                {
                    System.IO.File.Delete(path + "\\" + fileName);
                }
                return RedirectToAction("Error", "Account", new { Area = "" });
            }
        }

        public async Task<IActionResult> ExportToExcel(string TabName,int totRecords,Filter filter)
        {
            try
            {
                filter.length = totRecords;
                filter.start = 0;
                string FilterValues = await _helpers.GetFilterValues(filter);
                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Employee Vs CostCenter (" + TabName + ")", "Excel");
                if (FilterValues == "")
                {
                    FilterValues = "All";
                }
                string currDateTime = await _helpers.GetCurrentDate();
                
                //Get Datset
                var dataset = await _service.EmployeeCostCenter_Excel(TabName,filter);
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
                    var worksheet = workbook.Worksheets.Add("EmployeeVsCostCenter");
                    int count = dt.Rows.Count + 5;
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
                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191); ;
                    wsReportNameHeaderRange.Value = "Employee Vs CostCenter (" + TabName + ")";
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
                    worksheet.Range("B4:G4").Merge();
                    worksheet.Cell("B4").Value = FilterValues;
                    var currentRow = 5;
                    worksheet.Cell(currentRow, 1).InsertTable(dt);
                    worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;
                    worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                    worksheet.Range("A5:H5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Range("A5:H5").Style.Font.Bold = true;
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
                        return File(content,
                            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                            "Employee Vs CostCenter List(" + TabName + ").xlsx");
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
