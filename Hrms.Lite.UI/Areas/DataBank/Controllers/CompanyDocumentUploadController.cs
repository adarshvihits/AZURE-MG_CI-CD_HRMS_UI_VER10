using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.StaticFiles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;



namespace Hrms.Lite.UI.Areas.DataBank.Controllers
{
    [Area("DataBank")]
    [SessionAuthorize]
    public class CompanyDocumentUploadController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly ICompanyDocumentUploadService _service;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;

        public CompanyDocumentUploadController(IDropdownService dropdownService, ICompanyDocumentUploadService service, ICommonService commonService, IHelpers helpers)
        {
            _dropdownService = dropdownService;
            _service = service;
            _commonService = commonService;
            _helpers = helpers;
        }

        public async Task<IActionResult> Index()
        {
            CompanyDocumentUpload model = new CompanyDocumentUpload();
            model.Filter = await _commonService.GetFilterDropdowns();
            ViewBag.docType = new SelectList(await _dropdownService.GetCompanyDocumentTypeDropdown(), "Code", "Name"); 
            model.Type = "ACTIVE";
            return View(model);
           
        }


        [HttpPost]
        public async Task<IActionResult> GetList(string tabIndex, int doctypeCode, Filter filter1)
        {
            try
            {
                CompanyDocumentUpload model = new CompanyDocumentUpload();           

                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }

                model = await _service.GetList(tabIndex, doctypeCode,filter1);              
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
                    totalrows = model.EmployeeList[0].active;

                }


                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = model.EmployeeList[0].totalRecords;
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

        public async Task<IActionResult> GetDocumentDetails(Guid EmployeeGI, int doctypeCode)
        {
            try
            {
                CompanyDocumentUpload model = new CompanyDocumentUpload();                
                model = await _service.GetDocumentDetails(EmployeeGI, doctypeCode);
                model.EmployeeHeader = await _commonService.GetEmployeeHeader(EmployeeGI);
                
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

        public async Task<IActionResult> GetSliderDetails(Guid EmployeeGI, int doctypeCode) //slider
        {
            CompanyDocumentUpload model = new CompanyDocumentUpload();
            model.EmployeeHeader = await _commonService.GetEmployeeHeader(EmployeeGI);
            ViewBag.doc = new SelectList(await _dropdownService.GetCompanyDocumentDropdown(doctypeCode), "Code", "Name");
          
            model.EmployeeGI = EmployeeGI;
            return PartialView("_Slider", model);
        }

        [HttpGet]
        public async Task<IActionResult> CheckDocExists(int docCode ,Guid employeeGI)
        {
            CompanyDocumentUpload model = new CompanyDocumentUpload();
            model.EmployeeGI = employeeGI;
            model.CompanyDocument = new Dropdown();
            model.CompanyDocument.Code = docCode;
            var data = await _service.CheckDocExists( model);
            return Json(data);
        }

        [HttpPost]
        public async Task<IActionResult> DocSave(CompanyDocumentUpload model)
        {
           
            try
            {
                if (model.File != null)
                {
                    if (model.File.FileData != null)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await model.File.FileData.CopyToAsync(memoryStream);
                            model.File.fdata = memoryStream.ToArray();
                        }                       
                        model.File.FileName = model.File.FileData.FileName;
                        model.File.FileData = null;
                    }
                }             
                var response = await _service.DocumentSave(model);

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
        public async Task<IActionResult> ViewDocument(int index, Guid EmployeeGI, int TypeCode)
        {
            CompanyDocumentUpload model = await _service.GetDocumentDetails(EmployeeGI, TypeCode);
            Shared.File file = new Shared.File();
            file.fdata = model.DocumentList[index].DocData;
            file.FileName = model.DocumentList[index].FileName;
            if (file.fdata == null || file.fdata == null)
                return BadRequest(new { message = "No File Uploaded" });

            //var decompressedBytes = await _helpers.DecompressFile(file.fdata);
            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(file.FileName, out string contentType))
            {
                // default if unknown
                contentType = "application/octet-stream";
            }
            Response.Headers["Content-Disposition"] = $"inline; filename=\"{file.FileName}\"";
            return File(file.fdata, contentType);
        }
        [HttpPost]
        public async Task<ActionResult> DeleteDocument(int trncode, int EmpCode)
        {
            try
            {
                Popup model = new Popup();
                model.TrnCode = trncode;
                model.EmpCode = EmpCode;
                var response = await _service.DeleteDocument(model);
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
