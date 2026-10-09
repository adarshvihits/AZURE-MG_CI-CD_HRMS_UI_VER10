using ClosedXML.Excel;
using ExcelDataReader;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.Account;
using Hrms.Lite.Shared.DataBank;
using Hrms.Lite.Shared.General;
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
    public class BulkLeaveApplicablityController : Controller
    {

        private readonly IDropdownService _dropdownService;
        private readonly IBulkLeaveApplicablityService _service;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        private readonly IWebHostEnvironment _environment;

        public BulkLeaveApplicablityController(IDropdownService dropdownService, IBulkLeaveApplicablityService service, ICommonService commonService, IHelpers helpers, IWebHostEnvironment environment)
        {
            _dropdownService = dropdownService;
            _service = service;
            _commonService = commonService;
            _helpers = helpers;
            _environment = environment;
        }
        public async Task<IActionResult> Index()
        {
            BulkLeaveApplicablity model = new BulkLeaveApplicablity();
            ViewBag.LeaveGroup = new SelectList(await _dropdownService.GetLeaveGroupFilterDropdown(), "Code", "Name");
            ViewBag.LeaveType = new SelectList(await _dropdownService.GetLeaveGroupApplicableLeavesDropdown(0), "Code", "Name");
            model.Filter = await _commonService.GetFilterDropdowns();
            return View(model);
        }

        [HttpGet]
        public async Task<JsonResult> GetLeaveGroupApplicableLeavesDropdown(int TypeCode)
        {
            SelectList data = new SelectList("", "Code", "Name");
            data = new SelectList(await _dropdownService.GetLeaveGroupApplicableLeavesDropdown(TypeCode), "Code", "Name");
            return Json(data);
        }


        public async Task<IActionResult> GetApplicableList(int LeaveTypeCode, int LeaveGroupCode, int radioSelection, Filter filter)
        {
            try
            {
                BulkLeaveApplicablity model = new BulkLeaveApplicablity();
                model = await _service.GetApplicableList(LeaveTypeCode, LeaveGroupCode, radioSelection, filter);
                model.Filter = await _commonService.GetFilterDropdowns();
                return PartialView("~/Areas/DataBank/Views/BulkLeaveApplicablity/_ApplicabilityList.cshtml", model);
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



        public async Task<IActionResult> ExportToExcel(int LeaveTypeCode, int LeaveGroupCode, string Name, int radioSelection, int totRecords, Filter filter)
        {
            try
            {
                BulkLeaveApplicablity model = new BulkLeaveApplicablity();
                filter.length = totRecords;
                filter.start = 0;
                if (radioSelection == 1)
                    model.TabName = "Assigned";
                else if (radioSelection == 2)
                    model.TabName = "Not Assigned";
                else
                    model.TabName = "All";

                string FilterValues = await _helpers.GetFilterValues(filter);

                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Bulk Leave Applicability  (" + Name + ") - " + model.TabName, "Excel");

                if (FilterValues == "")
                {
                    FilterValues = "All";
                }

                string currDateTime = await _helpers.GetCurrentDate();
                
                DataSet dataset;
                dataset = await _service.GetApplicableList_Excel(LeaveTypeCode, LeaveGroupCode, radioSelection, filter);
                DataTable dt = dataset.Tables[0];

                dt.Columns[6].ColumnName = Name;
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
                    var worksheet = workbook.Worksheets.Add("Bulk Leave Applicability");
                    int count = dt.Rows.Count + 5;


                    worksheet.Range("A1:G" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:G" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:G" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:G" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:G" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 6)));
                    wsReportNameHeaderRange.Style.Font.Bold = true;
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Merge();
                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191); ;
                    wsReportNameHeaderRange.Value = "Bulk Leave Applicability List (" + Name + ") - " + model.TabName;

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
                    worksheet.Range("A5:G5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Range("A5:G5").Style.Font.Bold = true;


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
                            "Bulk Leave Applicability (" + Name + ") - " + model.TabName + " List.xlsx"
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


        public async Task<IActionResult> ImportFromExcel(BulkLeaveApplicablity model, string LeaveTypeName, int LeaveTypeCode, int LeaveGroupCode)
        {

            string path = Path.Combine(this._environment.WebRootPath, "Uploads");
            string fileName = model.Excel.FileData.FileName;
            try
            {
                model.BulkLeaveApplicablityList = new List<BulkLeaveApplicablityList>();
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
                            List<BulkLeaveApplicablityList> List = new List<BulkLeaveApplicablityList>();
                            var readerrr = ExcelReaderFactory.CreateReader(stream);
                            while (reader.Read()) //Each row of the file
                            {
                                var item = new BulkLeaveApplicablityList();
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
                                    item.LeaveTypeName = reader.GetValue(2).ToString();
                                }

                                List.Add(item);

                            }
                            if (List[0].EmployeeID != "Employee ID" || List[0].Name != "Employee Name" || List[0].LeaveTypeName != LeaveTypeName)
                            {
                                // Check if file exists with its full path    
                                if (System.IO.File.Exists(path + "\\" + fileName))
                                {
                                    System.IO.File.Delete(path + "\\" + fileName);
                                }
                                return Json(new ResponseEntity("Excel File not in the prescribed format", false));
                            }
                            model.BulkLeaveApplicablityList = List;

                            model.BulkLeaveApplicablityList.RemoveAt(0);

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
                model.LeaveTypeCode = LeaveTypeCode;
                model.LeaveGroupCode = LeaveGroupCode;

                var response = await _service.ImportFromExcel(model);
                if (System.IO.File.Exists(path + "\\" + fileName))
                {
                    System.IO.File.Delete(path + "\\" + fileName);
                }

                if (response.Success != true)
                {
                    model.BulkLeaveApplicablityList = response.Data;
                    model.Message = response.Message;
                    return PartialView("_UnMatchedList", model);
                }
                else
                {
                    return Json(response);
                }
                // Check if file exists with its full path    

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


        [HttpPost]
        public async Task<IActionResult> ApplicabilitySave(BulkLeaveApplicablity model)
        {
            try
            {
                var response = await _service.ApplicabilitySave(model);
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




    }
}
