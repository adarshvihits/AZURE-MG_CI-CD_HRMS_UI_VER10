using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.DataBank;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.DataBank.Controllers
{
    [Area("DataBank")]
    [SessionAuthorize]

    public class CertificatePolicyController : Controller
    {

        public readonly ICertificatePolicyService _service;
        private readonly IDropdownService _dropdownService;
        private readonly ICommonService _commonService;
        public CertificatePolicyController(ICertificatePolicyService service, IDropdownService dropdownService, ICommonService commonService)
        {
            _service = service;
            _dropdownService = dropdownService;
            _commonService = commonService;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                CertificatePolicy model = new CertificatePolicy();

                return View(await _service.GetList());
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

        public async Task<ActionResult> Create()
        {
            try
            {
                CertificatePolicy model = new CertificatePolicy();
                ViewBag.CertificateCategory = new SelectList(await _dropdownService.GetCertificateCategoryDropDown(), "Code", "Name");
                model.Mode = "CREATE";

                return PartialView("~/Areas/DataBank/Views/CertificatePolicy/_CreateOrEdit.cshtml", model);
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
        public async Task<ActionResult> Save(CertificatePolicy Model, string Validity,int CertificateCode)
        {
            try
            {
                Model.Validity = Validity;
                Model.CertificateCode = CertificateCode;
                var response = await _service.Create(Model);
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


        public async Task<ActionResult> Edit(int CertificateCode)
        {
            try
            {
                CertificatePolicy model = await _service.GetDetails(CertificateCode, "MODIFY");
                ViewBag.CertificateCategory = new SelectList(await _dropdownService.GetCertificateCategoryDropDown(), "Code", "Name");
                model.Mode = "MODIFY";

            
                return PartialView("_CreateOrEdit", model);
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


        public async Task<ActionResult> Delete(int CertifcateCode)
        {
            try
            {
                CertificatePolicy model = await _service.GetDetails(CertifcateCode,  "DELETE");
                model.Mode = "DELETE";
                return PartialView("_Delete", model);
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
        public async Task<ActionResult> ConfirmDelete(int CertificateCode)
        {
            try
            {
                var response = await _service.Delete(CertificateCode);
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




        public async Task<ActionResult> CertificationPolicyPopUp()
        {
            try
            {
                CertificatePolicy model = await _service.GetPopUpDetails();


                return PartialView("~/Areas/DataBank/Views/CertificatePolicy/_PopUp.cshtml", model);
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
        public async Task<IActionResult> SaveCertificate(CertificatePolicy Model)
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

            var response = await _service.CreateCertificate(Model);
            return Json(response);
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

        [HttpGet]
        public async Task<IActionResult> DownloadDoc()
        {
            var fileData = await _service.DownloadDoc();
            if (fileData == null || fileData.fdata == null)
                return NotFound();

            var decompressedBytes = await DecompressFileAsync(fileData.fdata);
            //return File(decompressedBytes, "application/pdf", fileData.FileName);
            Response.Headers["Content-Disposition"] = $"inline; filename=\"{fileData.FileName}\"";
            return File(decompressedBytes, "application/pdf");
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
