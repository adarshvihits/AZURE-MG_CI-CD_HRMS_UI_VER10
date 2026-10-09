using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using File = Hrms.Lite.Shared.File;

namespace Hrms.Lite.UI.Controllers
{
    //[SessionAuthorize]
    public class AccessFileController : Controller
    {
        private readonly IBlob _blob;
        private readonly ICommonService _commonService;
        public AccessFileController(IBlob blob, ICommonService commonservice)
        {
            _blob = blob;
            _commonService = commonservice;
        }

        [HttpGet]
        public async Task<string> GetFileStorageType()
        {
            var StorageType = await _commonService.GetFileStorageType();
            return StorageType.Data;
        }
        public async Task<ActionResult> ViewDocument(string filename)
        {

            Shared.File model = new File();
            model.FileName = filename;
            model = await _blob.GetBlobFile(model);
            try
            {
                if (model.stream == null)
                {
                    return NotFound();
                }
                Response.Headers.Add("X-Content-Type-Options", "nosniff");
                return File(model.stream, model.ContentType);
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
        public async Task<IActionResult> GetEmpThumbnail(Guid EmployeeGI)
        {

            File file = await _commonService.GetEmpThumbnail(EmployeeGI);
            if (file == null || file.fdata == null || file.fdata.Length == 0)
            {
                // If the 500 happens BEFORE this check, implement the check now
                return NotFound(); // This handles cases where data is missing safely
            }
            var mimeType = GetMimeTypeFromFilename(file.FileName);
            // Return the byte array as a file stream with the correct MIME type
            if (string.IsNullOrEmpty(mimeType))
            {
                // If a MIME type cannot be determined, the File() call might fail
                mimeType = "application/octet-stream"; // Safe fallback
            }
            return File(file.fdata, mimeType);
        }
        public async Task<IActionResult> GetEmpThumbnail_Approve(Guid EmployeeGI)//Pdersonal profile approval_pending
        {

            File file = await _commonService.GetEmpThumbnail_Approve(EmployeeGI);
            if (file == null || file.fdata == null || file.fdata.Length == 0)
            {
                // If the 500 happens BEFORE this check, implement the check now
                return NotFound(); // This handles cases where data is missing safely
            }
            var mimeType = GetMimeTypeFromFilename(file.FileName);
            // Return the byte array as a file stream with the correct MIME type
            if (string.IsNullOrEmpty(mimeType))
            {
                // If a MIME type cannot be determined, the File() call might fail
                mimeType = "application/octet-stream"; // Safe fallback
            }
            return File(file.fdata, mimeType);
        }
        public async Task<IActionResult> GetEmpThumbnail_OnBoarding(Guid GI)//Pdersonal profile approval_pending
        {

            File file = await _commonService.GetEmpThumbnail_OnBoarding(GI);
            if (file == null || file.fdata == null || file.fdata.Length == 0)
            {
                // If the 500 happens BEFORE this check, implement the check now
                return NotFound(); // This handles cases where data is missing safely
            }
            var mimeType = GetMimeTypeFromFilename(file.FileName);
            // Return the byte array as a file stream with the correct MIME type
            if (string.IsNullOrEmpty(mimeType))
            {
                // If a MIME type cannot be determined, the File() call might fail
                mimeType = "application/octet-stream"; // Safe fallback
            }
            return File(file.fdata, mimeType);
        }
        public static string GetMimeTypeFromFilename(string filename)
        {
            if (string.IsNullOrEmpty(filename))
            {
                return null;
            }

            var extension = System.IO.Path.GetExtension(filename).TrimStart('.').ToLowerInvariant();
            var mimeTypeMap = new Dictionary<string, string>()
                {
                    { "jpg", "image/jpg" },
                    { "jpeg", "image/jpeg" },
                    { "png", "image/png" },
                    // Add more mappings for other supported extensions
                };

            return mimeTypeMap.ContainsKey(extension) ? mimeTypeMap[extension] : null;
        }
        public async Task<string> ViewAsBackGroundImage(string filename)
        {
            try
            {
                File model = new File();
                model.FileName = filename;
                model = await _blob.GetBlobFile(model);
                if (model != null)
                {
                    model.fdata = model.stream.ToArray();
                    string base64String = Convert.ToBase64String(model.fdata, 0, model.fdata.Length);                    
                    return base64String;
                }
                else
                    return null;
            }
            catch (UnauthorizedAccessException e)
            {

                return e.Message.ToString();
            }
            catch (Exception e)
            {

                return e.Message.ToString();
            }
        }
        public async Task<ActionResult> DownloadDocument(string filename)
        {
            if (string.IsNullOrEmpty(filename))
            {
                return BadRequest("Filename is required.");
            }

            File model = new File();
            model.FileName = filename;

            try
            {
                // Fetch the file from the blob or storage system
                model = await _blob.GetBlobFile(model);

                // Check if file stream is null
                if (model.stream == null)
                {
                    return NotFound();
                }

                // Set headers to force download
                Response.Headers.Add("X-Content-Type-Options", "nosniff");
                Response.Headers.Add("Content-Disposition", $"attachment; filename={filename}");

                // Return the file with the correct content type and filename
                return File(model.stream, model.ContentType, filename);
            }
            catch (UnauthorizedAccessException)
            {
                return RedirectToAction("Login", "Account", new { Area = "" });
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Account", new { Area = "" });
            }
        }
        #region COMPRESS FILE AND DECOMPRESS FILE

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
        #endregion

    }
}
