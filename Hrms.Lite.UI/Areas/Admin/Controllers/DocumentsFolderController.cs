using Microsoft.AspNetCore.Mvc;
using Hrms.Lite.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hrms.Lite.Services.IServices.Admin;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.Admin;
using Microsoft.AspNetCore.Mvc.Rendering;
using Hrms.Lite.Shared;

namespace Hrms.Lite.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [SessionAuthorize]

    public class DocumentsFolderController : Controller
    {
        
            public readonly IDocumentsFolderService _service;
            public readonly ICommonService _commonService;
            private readonly IDropdownService _dropdownService;
            public DocumentsFolderController(IDocumentsFolderService service, IDropdownService dropdownService, ICommonService commonService)
            {
                _service = service;
                _commonService = commonService;
                _dropdownService = dropdownService;
            }
            public async Task<IActionResult> Index()
            {
                try
                {
                    DocumentsFolder model = new DocumentsFolder();
                    ViewBag.DocumentCategory = new SelectList(await _dropdownService.DocumentCategoryDropDown(), "Code", "Name");
                    model.File= new File();
                    model = await _service.GetList(0);
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
        public async Task<ActionResult> GetList(int Code)
        {
            try
            {
                return PartialView("_DocumentsList", await _service.GetList(Code));
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
        public async Task<IActionResult> CreateOrEdit(string Mode,int DocCategoryCode, Guid DocGI)
        {
            try
            {
                DocumentsFolder model = new DocumentsFolder();
                ViewBag.DocumentCategory = new SelectList(await _dropdownService.DocumentCategoryDropDown(), "Code", "Name");
                model = await _service.GetDocumentDetails(DocCategoryCode,DocGI);
                model.Mode = Mode;
                model.CategoryDropdown = new Shared.General.Dropdown();
                model.CategoryDropdown.Code=model.DocumentCategoryCode;
                return PartialView("~/Areas/Admin/Views/DocumentsFolder/_Create.cshtml", model);
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

        public async Task<IActionResult> GetDocCategoryDetails(int DocCategoryCode, Guid DocGI)
        {
            try
            {
                DocumentsFolder model = new DocumentsFolder();
                model = await _service.GetDocumentDetails(DocCategoryCode, DocGI);
                return PartialView("~/Areas/Admin/Views/DocumentsFolder/_ParametersList.cshtml", model);
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
        public async Task<IActionResult> DocumentSave(DocumentsFolder model)
        {
            var StorageType = await _commonService.GetFileStorageType();
            try
            {
                if (model.File != null)
                {
                    if (model.File.FileData != null)
                    {
                        if (StorageType.Data == "BLOB")
                        {

                            var fileobj = await _commonService.UploadIformFileToBLOB(model.File, "Documents");
                            if (fileobj.Success == false)
                            {
                                fileobj.Message = "FILE UPLOAD ERROR";
                                return Json(fileobj);
                            }
                            model.File = fileobj.Data;

                        }
                        else if (StorageType.Data == "IIS")
                        {
                            var fileobj = await _commonService.UploadIformFileToIIS(model.File, "Documents");
                            if (fileobj.Success == false)
                            {
                                fileobj.Message = "FILE UPLOAD ERROR";
                                return Json(fileobj);
                            }
                            model.File = fileobj.Data;

                        }
                        else
                        {
                            var fileobj = await _commonService.UploadIformFileToAWS(model.File, "Documents");
                            if (fileobj.Success == false)
                            {
                                fileobj.Message = "FILE UPLOAD ERROR";
                                return Json(fileobj);
                            }
                            model.File = fileobj.Data;

                        }
                    }
                }

                if (model.Mode != "MODIFY")
                {
                    model.Mode = "CREATE";
                    model.DocumentCategoryCode = model.CategoryDropdown.Code;
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
        public async Task<ActionResult> Delete(int DocumentCategoryCode,Guid DocGI)
        {
            try
            {
                var response = await _service.Delete(DocumentCategoryCode, DocGI);
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

