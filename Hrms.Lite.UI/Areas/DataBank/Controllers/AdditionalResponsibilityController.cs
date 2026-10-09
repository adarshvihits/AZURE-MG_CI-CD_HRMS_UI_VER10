using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.DataBank;
using Hrms.Lite.Shared.General;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using Spire.Xls;
using Newtonsoft.Json;
using Hrms.Lite.Shared.Account;
using File = Hrms.Lite.Shared.File;

namespace Hrms.Lite.UI.Areas.DataBank.Controllers
{
    [Area("DataBank")]
    [SessionAuthorize]
    public class AdditionalResponsibilityController : Controller
    {
        private readonly IAdditionalResponsibilityService _AdditionalResponsibilityService;
        private readonly IHelpers _helpers;
        private readonly IDropdownService _dropdownService;
        private readonly ICommonService _commonService;

        public  AdditionalResponsibilityController(IAdditionalResponsibilityService  AdditionalResponsibilityService, IDropdownService dropdownService, IHelpers Helpers, ICommonService commonService)
        {
            _AdditionalResponsibilityService =  AdditionalResponsibilityService;
            _dropdownService = dropdownService;
            _helpers = Helpers;
            _commonService = commonService;
        }


        public async Task<IActionResult> Index()
        {
            try
            {
                AdditionalResponsibility model = new AdditionalResponsibility();

                model = await _AdditionalResponsibilityService.AdditionalResponsibilityList("General");

                ViewBag.ListName = "General";
                return View("Index", model);
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
        public async Task<IActionResult> GetList(string TabIndex)
        {
            try
            {
                ViewBag.ListName = TabIndex;
                AdditionalResponsibility model = new AdditionalResponsibility();
                model = await _AdditionalResponsibilityService.AdditionalResponsibilityList(TabIndex);
                // ViewBag.ListName = TabIndex;

                if (TabIndex == "General")
                {
                    return View("_GeneralList", model);
                }
                else if (TabIndex == "Closed")
                {
                    return View("_ClosedList", model);
                }
                else 
                {
                    return View("_OpenList", model);
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
        public async Task<IActionResult> GetAdditionalResponsibility(Guid EmployeeGI, string TabIndex)
        {
            try
            {
                AdditionalResponsibility model = new AdditionalResponsibility();

                ViewBag.SliderList = TabIndex;
                ViewBag.AdditionalResponsibility = new SelectList(await _dropdownService.GetAdditionalResponsibilityDropdown(), "Code", "Name");
                ViewBag.Status = new SelectList(await _dropdownService.GetStatusDropdown(), "Code", "Name");
                ViewBag.Branch = new SelectList(await _dropdownService.GetLocationDropdown(), "Code", "Name");
                model = await _AdditionalResponsibilityService.GetAdditionalResponsibility(EmployeeGI, TabIndex);
                model.EmployeeHeader = await _commonService.GetEmployeeHeader(EmployeeGI);
                if (TabIndex == "General")
                {
                    return View("AdditionalResponsibilityGeneral", model);
                }
                else if (TabIndex == "Closed")
                {
                    return View("AdditionalResponsibilityClosed", model);
                }
                else 
                {
                    return View("AdditionalResponsibilityOpen", model);
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
        public async Task<IActionResult> SaveAdditionalResponsibilityDetails(AdditionalResponsibility Model)
        {

            if (Model.Attachment != null)
            {
                if (Model.Attachment.FileData != null)
                {
                    Model.Attachment.fdata = await CompressFileAsync(Model.Attachment.FileData);
                    Model.Attachment.FileName = Model.Attachment.FileData.FileName;
                    Model.Attachment.ContentType = Model.Attachment.FileData.ContentType;
                    Model.Attachment.FileData = null;
                }
            }

            var response = await _AdditionalResponsibilityService.SaveAdditionalResponsibilityDetails(Model);
            return Json(response);
        }

        [HttpGet]
        public async Task<IActionResult> DownloadDoc(Guid RequestGI, Guid EmployeeGI)
        {
            var fileData = await _AdditionalResponsibilityService.DownloadDoc(RequestGI, EmployeeGI);
            if (fileData == null || fileData.fdata == null)
                return NotFound();

            var decompressedBytes = await DecompressFileAsync(fileData.fdata);
            //return File(decompressedBytes, "application/pdf", fileData.FileName);
            Response.Headers["Content-Disposition"] = $"inline; filename=\"{fileData.FileName}\"";
            return File(decompressedBytes, "application/pdf");
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

        private async Task<byte[]> DecompressFileAsync(byte[] compressedData)
        {
            using (var input = new MemoryStream(compressedData))
            using (var gzip = new GZipStream(input, CompressionMode.Decompress))
            using (var output = new MemoryStream())
            {
                await gzip.CopyToAsync(output);
                return output.ToArray();
            }
        }
    }
}
