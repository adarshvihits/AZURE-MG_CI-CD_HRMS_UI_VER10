using ClosedXML.Excel;
using ExcelDataReader;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.PayRoll;
using Hrms.Lite.Shared.Account;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll;
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

namespace Hrms.Lite.UI.Areas.PayRoll.Controllers
{
    [Area("PayRoll")]
    [SessionAuthorize]
    public class VariableAllowanceUploadController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly IVariableAllowanceService _VariableAllowanceService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        private readonly IWebHostEnvironment _environment;
        public VariableAllowanceUploadController(IDropdownService dropdownService, IVariableAllowanceService VariableAllowanceService, ICommonService commonService, IHelpers helpers, IWebHostEnvironment env)
        {
            _VariableAllowanceService = VariableAllowanceService;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = helpers;
            _environment = env;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                VariableAllowance model = new VariableAllowance(); 
                model = await _VariableAllowanceService.GetVariableAllowanceUploadList(0);
                ViewBag.AllowanceMonth = new SelectList(await _dropdownService.GetSalaryProcessing_MonthDropdown(0), "Code", "Name");
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
        public async Task<IActionResult> GetVariableAllowanceUploadList(int MonthCode)
        {
            try
            {
                VariableAllowance model = new VariableAllowance();
                if (MonthCode != 0 )
                model = await _VariableAllowanceService.GetVariableAllowanceUploadList(MonthCode);

                return PartialView("_VariableAllowanceSummaryList", model);
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
        public async Task<IActionResult> ImportFromExcel(VariableAllowance model)
        {
            string path = Path.Combine(this._environment.WebRootPath, "Uploads");
            string fileName = model.Excel.FileData.FileName;
            try
            {
                model.VariableAllowanceList = new List<VariableAllowance>();
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
                            List<VariableAllowance> List = new List<VariableAllowance>();
                            var readerrr = ExcelReaderFactory.CreateReader(stream);
                            while (reader.Read()) //Each row of the file
                            {
                                var item = new VariableAllowance();
                                item.Allowance = new Shared.Master.Allowance();
                                if (reader.GetValue(0) != null)
                                {
                                    item.EmployeeID = reader.GetValue(0).ToString();
                                }
                                if (reader.GetValue(1) != null)
                                {
                                    item.Allowance.ShortName = reader.GetValue(1).ToString();
                                }
                               
                                if (reader.GetValue(2) != null)
                                {
                                    if (reader.GetValue(2).GetType().Name != "String")
                                    {
                                        item.Amount = Convert.ToDecimal(reader.GetDouble(2));
                                    }
                                    else
                                    {
                                        item.AmountStr = reader.GetValue(2).ToString();
                                    }
                                }
                         
                                List.Add(item);
                         
                            }
                            if (List[0].EmployeeID != "Employee ID" || List[0].Allowance.ShortName != "Allowance Short Name" || List[0].AmountStr != "Amount" )
                            {
                                reader.Close();
                                if (System.IO.File.Exists(path + "\\" + fileName))
                                {
                                    System.IO.File.Delete(path + "\\" + fileName);
                                }
                                return Json(new ResponseEntity("Excel File not in the prescribed format", false));
                            }
                            model.VariableAllowanceList = List;

                            model.VariableAllowanceList.RemoveAt(0);
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
              
                var response = await _VariableAllowanceService.ImportFromExcelMultiple(model);
                // Check if file exists with its full path    
                if (System.IO.File.Exists(path + "\\" + fileName))
                {
                    System.IO.File.Delete(path + "\\" + fileName);
                }
                if (response.Message != "UPDATED")
                {
                    model.VariableAllowanceList = response.Data;
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



    }
}
