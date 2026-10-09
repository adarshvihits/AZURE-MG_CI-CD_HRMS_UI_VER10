using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.DataBank;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.IO.Compression;
using Microsoft.AspNetCore.StaticFiles;

namespace Hrms.Lite.UI.Areas.DataBank.Controllers
{
    [Area("DataBank")]
    [SessionAuthorize]
    public class EconsoleController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly IEconsoleService _service;
        private readonly ICommonService _commonService;
        private readonly IBlob _blob;
        private readonly IHelpers _helpers;
        public EconsoleController(IEconsoleService service, IDropdownService dropdownService, ICommonService commonService, IBlob blob, IHelpers helpers)
        {
            _service = service;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _blob = blob; 
            _helpers = helpers;
        }
        public async Task<IActionResult> Index()
        {
            Econsole model = new Econsole();
            model = await _service.GetList(0);
            return View(model);
        }
        public async Task<IActionResult> GetList(int TabIndex)
        {
            Econsole model = new Econsole();
            model = await _service.GetList(TabIndex);
            return PartialView("_List", model);
        }
        public async Task<IActionResult> Create()
         {
            Econsole model = new Econsole();
            ViewBag.ConsoleType = new SelectList(await _dropdownService.GetConsoleTypeDropDown(), "Code", "Name");
            ViewBag.Category = new SelectList(await _dropdownService.GetEconsoleCategoryDropDown(), "Code", "Name");
            ViewBag.MultipleCombination = new SelectList(await _dropdownService.GetHRPolicyCombinationDropDown(), "Code", "Name");
            model.Mode = "Create";
            model.IsMultiCombination = 0;
            model.Active = true;
            return View(model);
        }
        public async Task<IActionResult> GetCombinationDetails(int id, int type, string combinationName,int DojAppl, int BeforeAfter, DateTime? Date)
        {
            try
            {
                ViewBag.CombinationName = combinationName;
                if (type == 0)
                {
                    Econsole model = new Econsole();
                    if (id != 0)
                    {
                        model = await _service.GetCombinationDetails(id, DojAppl, BeforeAfter, Date.ToString());
                    }
                    return PartialView("_CombinationList1", model);
                }
                else
                {
                    Econsole model1 = new Econsole();
                    if (id != 0)
                    {
                        model1 = await _service.GetCombinationDetails(id, DojAppl, BeforeAfter, Date.ToString());
                        model1.Combinations2 = model1.Combinations1;
                    }
                    return PartialView("_CombinationList2", model1);
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
        public async Task<IActionResult> Save(Econsole model)
        {
            Shared.File file1 = new Shared.File();
            file1 = model.File;
            var StorageType = await _commonService.GetFileStorageType();
            try
            {
                if (model.File != null)
                {
                    if (model.File.FileData != null)
                    {
                        model.File.fdata = await CompressFileAsync(model.File.FileData);
                        model.File.FileName = model.File.FileData.FileName;
                        model.File.ContentType = model.File.FileData.ContentType;
                        model.File.FileData = null;
                    }
                }
                if (model.Combinations1 != null)
                {
                    var cmb1 = from li in model.Combinations1
                               where li.Applicable == true
                               select li;
                    model.Combinations1 = cmb1.ToList();
                }
                if (model.Combinations2 != null)
                {
                    var cmb2 = from li in model.Combinations2
                               where li.Applicable == true
                               select li;
                    model.Combinations2 = cmb2.ToList();
                }
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
        public async Task<IActionResult> Edit(int Code)
        {
            Econsole model = new Econsole();
            ViewBag.ConsoleType = new SelectList(await _dropdownService.GetConsoleTypeDropDown(), "Code", "Name");
            ViewBag.Category = new SelectList(await _dropdownService.GetEconsoleCategoryDropDown(), "Code", "Name");
            ViewBag.MultipleCombination = new SelectList(await _dropdownService.GetHRPolicyCombinationDropDown(), "Code", "Name");
            model = await _service.GetEconsoleDetails(Code);
            model.Mode = "Modify";
            if (model.Combinations1[0].EmployeeID != "")
            {
                ViewBag.CombinationName = "Employee";
            }
            model.consoleCode = Code;
            return View("Create", model);
        }
        public async Task<IActionResult> ViewDocument(int Code)
        {
            try
            {
                Econsole model = new Econsole();
                model = await _service.GetEconsoleDetails(Code);
                Shared.File file = new Shared.File();
                file = model.File;
                if (file.fdata == null || file.fdata == null)
                    return BadRequest(new { message = "No File Uploaded" });

                var decompressedBytes = await _helpers.DecompressFile(file.fdata);
                var provider = new FileExtensionContentTypeProvider();
                if (!provider.TryGetContentType(file.FileName, out string contentType))
                {
                    // default if unknown
                    contentType = "application/octet-stream";
                }
                Response.Headers["Content-Disposition"] = $"inline; filename=\"{file.FileName}\"";
                return File(decompressedBytes, contentType);
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", "Account", new { Area = "" });
            }
        }
        public async Task<ActionResult> Delete(int Code)
        {
            try
            {
                Econsole model = new Econsole();
                var response = await _service.Delete(Code);
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
        private async Task<byte[]> CompressFileAsync(IFormFile file)
        {
            using (var ms = new MemoryStream())
            {
                await file.CopyToAsync(ms);
                using (var compressedStream = new MemoryStream())
                {
                    using (var gzip = new GZipStream(compressedStream, CompressionMode.Compress))
                    {
                        gzip.Write(ms.ToArray(), 0, ms.ToArray().Length);
                    }
                    return compressedStream.ToArray();
                }
            }
        }
    }
}
