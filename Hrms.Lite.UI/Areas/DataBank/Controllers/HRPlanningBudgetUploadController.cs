using ExcelDataReader;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
    public class HRPlanningBudgetUploadController : Controller
    {
        #region private
        private readonly IDropdownService _dropdownService;
        private readonly IWebHostEnvironment _environment;
        public readonly IHRPlanningBudgetService _Service;
        private readonly IHRPlannningService _hRPlannningService;

        #endregion
        #region constr
        public HRPlanningBudgetUploadController(IDropdownService dropdownService, IHRPlanningBudgetService Service, IHelpers helpers, IWebHostEnvironment env,
            IHRPlannningService hRPlannningService)
        {
            _Service = Service;
            _dropdownService = dropdownService;
            _environment = env;
            _hRPlannningService = hRPlannningService;
        }

        #endregion
        public async Task<IActionResult> Index()
        {
            try
            {
                HRPlanningBudget model = new HRPlanningBudget();
                HRPlanning HRPlanning = new HRPlanning();
                ViewBag.PlanningName = new SelectList(await _dropdownService.GetHRPlanningDropDown(), "Code", "Name");
                ViewBag.Designation = new SelectList(await _dropdownService.BudgetDesignationDropdown(), "Code", "Name");
                HRPlanning = await _hRPlannningService.GetCurrentMonthPlanningDetails();
                model.IsOverwrite = "NO";
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
        public async Task<IActionResult> GetUploadList(int HrPlanningCode, int DesignationCode)
        {
            try
            {
                HRPlanningBudget model = new HRPlanningBudget();
                if (DesignationCode != 0 && DesignationCode!=0)
                  model = await _Service.GetBudgetUploadList(HrPlanningCode, DesignationCode);
          
                return PartialView("_BudgetUploadList", model);
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
        public async Task<IActionResult> ImportFromExcel(HRPlanningBudget model)
        {
            string path = Path.Combine(this._environment.WebRootPath, "Uploads");
            string fileName = model.Excel.FileData.FileName;
            try
            {
                model.Details = new List<HRPlanningBudgetDetails>();
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
                            List<HRPlanningBudgetDetails> List = new List<HRPlanningBudgetDetails>();
                            var readerrr = ExcelReaderFactory.CreateReader(stream);
                            while (reader.Read()) //Each row of the file
                            {
                                var item = new HRPlanningBudgetDetails();
                                item.Region = new Shared.Master.Region();
                                if (reader.GetValue(0) != null)
                                {
                                    item.LocationName = reader.GetValue(0).ToString();
                                }
                                if (reader.GetValue(1) != null)
                                {
                                    item.Region.Name = reader.GetValue(1).ToString();
                                }
                                if (reader.GetValue(2) != null)
                                {
                                    item.BudgetedNo = reader.GetValue(2).ToString();
                                }
                                if (item.BudgetedNo.ToString().Contains("."))
                                {
                                    return Json(new ResponseEntity("Decimal values will not be supported.Please round off the decimal values and try again", false));
                                }
                                else
                                {
                                    List.Add(item);
                                }
                            }
                            if (List[0].LocationName != "Location" || List[0].Region.Name != "Region" || List[0].BudgetedNo != "Budgeted Number")
                            {
                                reader.Close();
                                // Check if file exists with its full path    
                                if (System.IO.File.Exists(path + "\\" + fileName))
                                {
                                    System.IO.File.Delete(path + "\\" + fileName);
                                }
                                return Json(new ResponseEntity("Excel File not in the prescribed format", false));
                            }
                            model.Details = List;

                            model.Details.RemoveAt(0);
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
                var response = await _Service.ImportFromExcel(model);
                // Check if file exists with its full path    
                if (System.IO.File.Exists(path + "\\" + fileName))
                {
                    System.IO.File.Delete(path + "\\" + fileName);
                }
                if (response.Success != true)
                {
                    if (response.Message == "UNMATCHED")
                    {
                        model.Details = response.Data;
                        model.Message = response.Message;
                        return PartialView("_UnmatchedList", model);
                    }
                    else
                    {
                        return Json(response);
                    }
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
    } 
}
