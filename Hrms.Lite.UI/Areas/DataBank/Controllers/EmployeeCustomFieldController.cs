using ClosedXML.Excel;
using ExcelDataReader;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.Account;
using Hrms.Lite.Shared.DataBank.DTO;
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
    public class EmployeeCustomFieldController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly IEmployeeCustomFieldService _service;
        private readonly IHelpers _helpers;
        private readonly ICommonService _commonService;
        private readonly IWebHostEnvironment _environment;
        
        public EmployeeCustomFieldController(IDropdownService dropdownService, IEmployeeCustomFieldService service,IHelpers helpers,ICommonService commonService, IWebHostEnvironment environment)
        {
            _dropdownService = dropdownService;
            _service = service;
            _helpers = helpers;
            _commonService = commonService;
            _environment = environment;
        }
        public async Task<IActionResult> Index()
        {
            try
            {

                EmployeeVsCustomField model = new EmployeeVsCustomField();
                model.Filter = new Filter();
                ViewBag.CustomFieldType = new SelectList(await _dropdownService.GetCustomFieldDropDown(), "Code", "Name");
                model.CustomField = new CustomField();
                model.CustomField.CustomFieldCode = 0;
                model.Filter.length = 10;
                model.CustomField.EmployeeStatus = "y";
                model = await _service.GetList(0, "y", model.Filter);
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


        public async Task<IActionResult> GetList(Filter filter1, int Type,string employeestatus)
        {
            try
            {
                EmployeeVsCustomField model = new EmployeeVsCustomField();
                EmployeeVsCustomField m = new EmployeeVsCustomField();

                Filter filter = new Filter();
               model.Filter= filter1;

             
                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }
               if (Type != 0)
                {
                    model = await _service.GetList(Type, employeestatus, filter1);
                }
                m.EmployeeVsCustomFieldList = model.EmployeeVsCustomFieldList;
                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.EmployeeVsCustomFieldList == null)
                {
                   
                    return Json(new
                    {
                        data = "",
                        draw = filter1.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows,
                        headerName = model.HeaderName
                    });
                }

                if (model.EmployeeVsCustomFieldList.Count > 0)
                {
                    totalrows = m.EmployeeVsCustomFieldList[0].active;

                   
                }


                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = m.EmployeeVsCustomFieldList[0].totalRecords;
                    return Json(new
                    {
                        data = model.EmployeeVsCustomFieldList,
                        draw = filter1.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows,//totalrowsafterfiltering
                        headerName = model.HeaderName
                    });
                }
                else
                {

                    return Json(new
                    {
                        data = model.EmployeeVsCustomFieldList,
                        draw = filter1.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows,
                        headerName = model.HeaderName
                    });
                }


            }
            catch (Exception ex)
            {
                return View();
            }




        }
        public async Task<IActionResult> GetCustomFieldList()
        {
            try
            {
                CustomField model = new CustomField();
                model = await _service.GetCustomFieldList();
                if (model.CustomFieldList != null && model.CustomFieldList.Count == 0)
                {
                    model.CustomFieldList = new List<CustomField>();
                    CustomField m = new CustomField()
                    {
                        SlNo = 1,
                        CustomFieldName = ""
                    };
                    model.CustomFieldList.Add(m);
                }
                return PartialView("_PopUp", model);
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
        public async Task<IActionResult> CustomFieldSave(CustomField model)
        {
            try
            {
                var response = await _service.CustomFieldSave(model.CustomFieldList);
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
        public async Task<JsonResult> GetCustomFieldDropdown()
        {
            var CustomFieldsData = await _dropdownService.GetCustomFieldDropDown();
            var CustomField = new SelectList(CustomFieldsData, "Code", "Name",CustomFieldsData[0].Code);
            return Json(CustomField);
        }

        public async Task<IActionResult> CustomFieldEmployeeSave(string CustomFieldValue,int CustomFieldCode,Guid EmployeeGI)
        {
            try
            {
                EmployeeVsCustomField model = new EmployeeVsCustomField();
                model.CustomFieldValue = CustomFieldValue;
                model.CustomField = new CustomField();
                model.CustomField.CustomFieldCode = CustomFieldCode;
                model.EmployeeGI = EmployeeGI;
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
        
        public async Task<IActionResult> Delete(Guid CustomFieldGI)
        {
            try
            {
                CustomField model = new CustomField();
                model.CustomFieldGI = CustomFieldGI;
                var response = await _service.Delete(model);
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
        public async Task<IActionResult> ExportToExcel(int CustomFieldCode,string CustomFieldName, string employeestatus, int totRecords,Filter filter)
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
                EP = await _commonService.GetExcelPassword("Employee Custom Field List (" + CustomFieldName + ")", "Excel");

                if (FilterValues == "")
                {
                    FilterValues = "All";
                }

                string currDateTime = await _helpers.GetCurrentDate();
                

                //Get Datset
                var dataset = await _service.EmployeeVsCustomField_ExportToExcel(CustomFieldCode, employeestatus, filter);
                DataTable dt = dataset.Tables[0];
                dt.Columns[6].ColumnName = CustomFieldName;

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
                    var worksheet = workbook.Worksheets.Add("Employee Custom Field");
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
                    wsReportNameHeaderRange.Value = "Employee Custom Field List (" + CustomFieldName + ")";

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
                            book.Protect(EP.Password,false,false);
                            book.SaveToStream(stream);
                        }
                        var content = stream.ToArray();
                        return File(
                            content,
                            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                            "Employee Custom Field List (" + CustomFieldName + ") List.xlsx"
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
        public async Task<IActionResult> ImportFromExcel(EmployeeVsCustomField model,string CustomFieldName)
        {
            string path = Path.Combine(this._environment.WebRootPath, "Uploads");
            string fileName = model.Excel.FileData.FileName;
            try
            {
                model.EmployeeVsCustomFieldList = new List<EmployeeVsCustomFieldList>();
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
                            List<EmployeeVsCustomFieldList> List = new List<EmployeeVsCustomFieldList>();
                            var readerrr = ExcelReaderFactory.CreateReader(stream);
                            while (reader.Read()) //Each row of the file
                            {
                                var item = new EmployeeVsCustomFieldList();
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
                                    item.CustomFieldName = reader.GetValue(2).ToString();                               
                                }                                
                                
                                    List.Add(item);
                                
                            }
                            if (List[0].EmployeeID != "Employee ID" || List[0].Name != "Employee Name" || List[0].CustomFieldName != CustomFieldName)
                            {
                                // Check if file exists with its full path    
                                if (System.IO.File.Exists(path + "\\" + fileName))
                                {
                                    System.IO.File.Delete(path + "\\" + fileName);
                                }
                                return Json(new ResponseEntity("Excel File not in the prescribed format", false));
                            }
                            model.EmployeeVsCustomFieldList = List;

                            model.EmployeeVsCustomFieldList.RemoveAt(0);

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
                var response = await _service.ImportFromExcel(model);
                if (System.IO.File.Exists(path + "\\" + fileName))
                {
                    System.IO.File.Delete(path + "\\" + fileName);
                }

                if (response.Success != true)
                {
                    model.EmployeeVsCustomFieldList = response.Data;
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
    }
}
