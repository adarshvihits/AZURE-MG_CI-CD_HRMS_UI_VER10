using ExcelDataReader;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.DataBank.Generic;
using Hrms.Lite.Shared.General;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.DataBank.Controllers
{
    [Area("DataBank")]
    [SessionAuthorize]
    public class SalaryIncrementToolController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly ISalaryIncrementToolService _SalaryIncrementToolService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        private readonly IWebHostEnvironment _environment;
        public SalaryIncrementToolController(IDropdownService dropdownService, ISalaryIncrementToolService service, ICommonService commonService, IHelpers helpers, IWebHostEnvironment env)
        {
            _dropdownService = dropdownService;
            _SalaryIncrementToolService = service;
            _commonService = commonService;
            _helpers = helpers;
            _environment = env;
        }
        public async Task<IActionResult> Index()
        {
            try
            {

                SalaryIncrementTool model = new SalaryIncrementTool();
                SalaryIncrementTool model1 = new SalaryIncrementTool();
                SalaryIncrementTool model2 = new SalaryIncrementTool();
                SalaryIncrementTool model3 = new SalaryIncrementTool();
                model1 = await _SalaryIncrementToolService.GetFixedList();
                model2 = await _SalaryIncrementToolService.GetClassificationList();
                model3 = await _SalaryIncrementToolService.GetAllowanceList();
                model = await _SalaryIncrementToolService.GetSettingsList();
                model.FixedList = model1.FixedList;
                model.ClassificationList = model2.ClassificationList;
                model.AllowanceList = model3.AllowanceList;
                ViewBag.Reason = new SelectList(await _dropdownService.GetIncrementToolReasonDropDown(), "Code", "Name");
                ViewBag.TransitionType = new SelectList(await _dropdownService.GetIncrementToolTransitionTypeDropdown(), "Code", "Name");

                TempData["SettingsList"] = JsonConvert.SerializeObject(model.SettingsList);
                return View(model);
                //return View(model);
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
        public async Task<IActionResult> onClassificationApplicabilityClick(SalaryIncrementTool model)
        {
            SalaryIncrementTool model1 = new SalaryIncrementTool();
            model1 = await _SalaryIncrementToolService.GetFixedSettingsList();
            var list = from li in model.ClassificationList
                       where li.Applicable == true
                       select li;
            var list2 = from li in model.AllowanceList
                        where li.Applicable == true
                        select li;
            //int order = 1;+
            var list3 = list.Concat(list2).ToList();
            list3 = list3.OrderBy(a => a.Order).ToList();
            model.SettingsList = model1.SettingsList;
            // model.SettingsList = model.SettingsList.Concat(list).ToList();
            model.SettingsList = model.SettingsList.Concat(list3).ToList();
            return PartialView("_SettingsList", model);
            //return Json(model);

        }
        [HttpPost]
        public async Task<ActionResult> Save(SalaryIncrementTool model)
        {
            try
            {
                var response = await _SalaryIncrementToolService.Create(model);
                TempData["SettingsList"] = JsonConvert.SerializeObject(model.SettingsList);
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
        public async Task<IActionResult> PrepareDoccument(SalaryIncrementTool model)
        {
            string path = Path.Combine(this._environment.WebRootPath, "Uploads");
            string fileName = model.Excel.FileData.FileName;
            try
            {
                string wwwPath = this._environment.WebRootPath;
                string contentPath = this._environment.ContentRootPath;
                DataTable DTROWS = new DataTable();

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
                            List<SalaryIncrementTool> List = new List<SalaryIncrementTool>();
                            var readerrr = ExcelReaderFactory.CreateReader(stream);

                            var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                            {
                                ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                                {
                                    UseHeaderRow = true
                                }
                            });
                            DTROWS = result.Tables[0];



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
                DataTable DT_HEAD = new DataTable();

                DT_HEAD.Columns.Add("HEAD");
                DT_HEAD.Columns.Add("INDX");
                DataRow Dr;


                for (int i = 0; i < DTROWS.Columns.Count; i++)
                {

                    Dr = DT_HEAD.NewRow();
                    Dr["HEAD"] = DTROWS.Columns[i].ColumnName.ToString();
                    Dr["INDX"] = (i + 1).ToString();
                    DT_HEAD.Rows.Add(Dr);

                }
                DataTable DT_ROWS = new DataTable();

                for (int i = 0; i < DTROWS.Columns.Count; i++)
                {

                    DT_ROWS.Columns.Add(DT_HEAD.Rows[i].ItemArray[0].ToString());

                }
                DataRow Dd;
                for (int i = 0; i < DTROWS.Rows.Count; i++)
                {

                    Dd = DT_ROWS.NewRow();
                    for (int j = 0; j < DTROWS.Columns.Count; j++)
                    {
                        Dd[DT_ROWS.Columns[j].ColumnName.ToString()] = DTROWS.Rows[i].ItemArray[j].ToString();

                    }
                    DT_ROWS.Rows.Add(Dd);
                }
                var a = DT_HEAD.Rows[0].ItemArray[0];
                model.Excel.FileData = null;
                model.ExcelTable = DTROWS;
                model.ExcelHead = DT_HEAD;
                model.ExcelRows = DT_ROWS;
                var response = await _SalaryIncrementToolService.PrepareDoccument(model);
                // Check if file exists with its full path    
                if (System.IO.File.Exists(path + "\\" + fileName))
                {
                    System.IO.File.Delete(path + "\\" + fileName);
                }
                if (response.Message == "UPDATED")
                {

                    return PartialView("_UnmatchedList", model);
                }
                else
                {

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
        public async Task<IActionResult> ImportFromExcel(SalaryIncrementTool model)
        {
            string path = Path.Combine(this._environment.WebRootPath, "Uploads");
            string fileName = model.Excel.FileData.FileName;
            try
            {
                string wwwPath = this._environment.WebRootPath;
                string contentPath = this._environment.ContentRootPath;
                DataTable DTROWS = new DataTable();
                DataTable DTROWSFOR_ALW = new DataTable();
                var Settings = JsonConvert.DeserializeObject<List<SettingsList>>(TempData.Peek("SettingsList").ToString());

                List<SettingsList> ListSettings = new List<SettingsList>();
                List<SettingsList> ListSettingsClassification = new List<SettingsList>();
                List<SettingsList> ListSettingsDetails = new List<SettingsList>();
                if (Settings != null || Settings.Count > 0)
                {
                    for (int i = 0; i < Settings.Count; i++)
                    {
                        if (Settings[i].Type == 0)
                        {
                            ListSettings.Add(Settings[i]);
                        }
                        if (Settings[i].Type == 2)
                        {
                            ListSettingsClassification.Add(Settings[i]);
                        }
                        if (Settings[i].Type == 1)
                        {
                            ListSettingsDetails.Add(Settings[i]);
                        }

                    }
                }
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
                            List<SalaryIncrementTool> List = new List<SalaryIncrementTool>();
                            var readerrr = ExcelReaderFactory.CreateReader(stream);

                            var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                            {
                                ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                                {
                                    UseHeaderRow = true
                                }
                            });
                            DTROWS = result.Tables[0];



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
                DataTable DT_HEAD = new DataTable();

                DT_HEAD.Columns.Add("HEAD");
                DT_HEAD.Columns.Add("INDX");
                DataRow Dr;


                for (int i = 0; i < DTROWS.Columns.Count; i++)
                {

                    Dr = DT_HEAD.NewRow();
                    Dr["HEAD"] = DTROWS.Columns[i].ColumnName.ToString();
                    Dr["INDX"] = (i + 1).ToString();
                    DT_HEAD.Rows.Add(Dr);

                }
                DataTable DT_ROWS = new DataTable();

                for (int i = 0; i < DTROWS.Columns.Count; i++)
                {

                    DT_ROWS.Columns.Add(DT_HEAD.Rows[i].ItemArray[0].ToString());

                }
                DataRow Dd;
                DataRow DdAlw;
                model.EmployeeDetailsList = new List<EmployeeDetails>();
                model.AllowanceResultList = new List<AllowanceResult>();
                model.ClassificationResultList = new List<SettingsTypeList>();


                for (int i = 0; i < DTROWS.Rows.Count; i++)
                {
                    model.EmployeeDetailsList.Add(new EmployeeDetails
                    {
                        EmployeeID = DTROWS.Rows[i].ItemArray[0].ToString(),
                        EmployeeName = DTROWS.Rows[i].ItemArray[1].ToString(),
                        EFFECTIVEDATE = Convert.ToDateTime(DTROWS.Rows[i].ItemArray[2]),
                        ORDERDATE = Convert.ToDateTime(DTROWS.Rows[i].ItemArray[3])
                    });

                    Dd = DT_ROWS.NewRow();
                    for (int j = 0; j < DTROWS.Columns.Count; j++)
                    {
                        Dd[DT_ROWS.Columns[j].ColumnName.ToString()] = DTROWS.Rows[i].ItemArray[j].ToString();
                        for (int k = 0; k < ListSettings.Count; k++)
                        {
                            if (ListSettings[k].Heading == DT_ROWS.Columns[j].ColumnName.ToString())
                            {

                                model.AllowanceResultList.Add(new AllowanceResult
                                {
                                    AllowanceCode = ListSettings[k].AllowanceCode,
                                    ID = DTROWS.Rows[i].ItemArray[0].ToString(),
                                    Amount = Convert.ToDecimal(DTROWS.Rows[i].ItemArray[j]),
                                    AllowanceName = ListSettings[k].Heading
                                });
                            }

                        }
                        //for (int k = 0; k < ListSettingsClassification.Count; k++)
                        //{
                        for (int m = 0; m < ListSettingsClassification.Count; m++)
                        {
                            if (ListSettingsClassification[m].Heading == DT_ROWS.Columns[j].ColumnName.ToString())
                            {

                                model.ClassificationResultList.Add(new SettingsTypeList
                                {
                                    EmployeeID = DTROWS.Rows[i].ItemArray[0].ToString(),
                                    ClassificationValue = DTROWS.Rows[i].ItemArray[j].ToString(),
                                    ClassificationType = ListSettingsClassification[m].Heading
                                });
                            }
                            //}
                        }




                    }



                    DT_ROWS.Rows.Add(Dd);
                }




                var a = DT_HEAD.Rows[0].ItemArray[0];
                model.Excel.FileData = null;
                model.ExcelTable = DTROWS;
                model.ExcelHead = DT_HEAD;
                model.ExcelRows = DT_ROWS;
                model.ExcelTable = null;
                model.ExcelRows = null;
                var jsondata = JsonConvert.SerializeObject(model);
                var response = await _SalaryIncrementToolService.ImportFromExcel(model);
                // Check if file exists with its full path    
                if (System.IO.File.Exists(path + "\\" + fileName))
                {
                    System.IO.File.Delete(path + "\\" + fileName);
                }
                if (response.Message == "UNMATCHED")
                {
                    model.SalaryIncrementToolList = response.Data;
                    model.Message = response.Message;
                    return PartialView("_UnmatchedList", model);
                }
                else
                {

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

        public async Task<IActionResult> ProcessExcelClick(SalaryIncrementTool model)
        {
            string path = Path.Combine(this._environment.WebRootPath, "Uploads");
            string fileName = model.Excel.FileData.FileName;
            try
            {
                string wwwPath = this._environment.WebRootPath;
                string contentPath = this._environment.ContentRootPath;
                DataTable DTROWS = new DataTable();

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
                            List<SalaryIncrementTool> List = new List<SalaryIncrementTool>();
                            var readerrr = ExcelReaderFactory.CreateReader(stream);

                            var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                            {
                                ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                                {
                                    UseHeaderRow = true
                                }
                            });
                            DTROWS = result.Tables[0];



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
                DataTable DT_HEAD = new DataTable();

                DT_HEAD.Columns.Add("HEAD");
                DT_HEAD.Columns.Add("INDX");
                DataRow Dr;


                for (int i = 0; i < DTROWS.Columns.Count; i++)
                {

                    Dr = DT_HEAD.NewRow();
                    Dr["HEAD"] = DTROWS.Columns[i].ColumnName.ToString();
                    Dr["INDX"] = (i + 1).ToString();
                    DT_HEAD.Rows.Add(Dr);

                }
                DataTable DT_ROWS = new DataTable();

                for (int i = 0; i < DTROWS.Columns.Count; i++)
                {

                    DT_ROWS.Columns.Add(DT_HEAD.Rows[i].ItemArray[0].ToString());

                }
                DataRow Dd;
                for (int i = 0; i < DTROWS.Rows.Count; i++)
                {

                    Dd = DT_ROWS.NewRow();
                    for (int j = 0; j < DTROWS.Columns.Count; j++)
                    {
                        Dd[DT_ROWS.Columns[j].ColumnName.ToString()] = DTROWS.Rows[i].ItemArray[j].ToString();

                    }
                    DT_ROWS.Rows.Add(Dd);
                }
                var a = DT_HEAD.Rows[0].ItemArray[0];
                model.Excel.FileData = null;
                model.ExcelTable = DTROWS;
                model.ExcelHead = DT_HEAD;
                model.ExcelRows = DT_ROWS;
                var response = await _SalaryIncrementToolService.ProcessExcelClick(model);
                // Check if file exists with its full path    
                if (System.IO.File.Exists(path + "\\" + fileName))
                {
                    System.IO.File.Delete(path + "\\" + fileName);
                }
                if (response.Message == "UPDATED")
                {
                    //model.VariableAllowanceList = response.Data;
                    //model.Message = response.Message;
                    return PartialView("_UnmatchedList", model);
                }
                else
                {

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
        //public DataTable ReadExcelFile(byte[] filePath)
        //{
        //    // Create an empty DataTable to hold the result
        //    DataTable dt = new DataTable();

        //    // Open the Excel file using ExcelDataReader
        //    using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
        //    {
        //        // Create an instance of ExcelDataReader
        //        using (var reader = ExcelReaderFactory.CreateReader(stream))
        //        {
        //            // Choose one of either 1 or 2:

        //            // 1. Use the first row as column headers (recommended)
        //            var result = reader.AsDataSet(new ExcelDataSetConfiguration()
        //            {
        //                ConfigureDataTable = _ => new ExcelDataTableConfiguration()
        //                {
        //                    UseHeaderRow = true
        //                }
        //            });

        //            // 2. Or use the default configuration (without column headers)
        //            // var result = reader.AsDataSet();

        //            // You can access each DataTable inside DataSet like this:
        //            dt = result.Tables[0]; // Assuming the first sheet is your target sheet
        //        }
        //    }

        //    return dt;
        //}

        public IActionResult DownloadFile()
        {
            // Example: reading a file into a byte array (replace with your logic)
            byte[] fileBytes = System.IO.File.ReadAllBytes("path/to/your/file.xlsx");
            string fileName = "example.xlsx";

            // Return the file as a byte array with the specified content type
            return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }
        public DataTable ReadExcelIntoDataTable(SalaryIncrementTool model)
        {
            DataTable dt = new DataTable();
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            string filePath = Path.Combine(this._environment.WebRootPath, "Uploads");
            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault();

                if (worksheet == null)
                {
                    throw new Exception("The workbook does not contain any worksheets.");
                }

                foreach (var firstRowCell in worksheet.Cells[1, 1, 1, worksheet.Dimension.End.Column])
                {
                    dt.Columns.Add(firstRowCell.Text);
                }

                for (int rowNumber = 2; rowNumber <= worksheet.Dimension.End.Row; rowNumber++)
                {
                    var row = worksheet.Cells[rowNumber, 1, rowNumber, worksheet.Dimension.End.Column];
                    DataRow dataRow = dt.Rows.Add();
                    foreach (var cell in row)
                    {
                        dataRow[cell.Start.Column - 1] = cell.Text;
                    }
                }
            }

            return dt;

        }
    }
}
