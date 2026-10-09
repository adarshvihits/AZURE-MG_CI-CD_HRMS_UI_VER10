using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.Admin;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.Admin;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [SessionAuthorize]
    public class PhotoGalleryController : Controller
    {
        private readonly IBlob _blob;
        private readonly IPhotoGalleryService _service;
        private readonly ICommonService _commonService;
        private IHostingEnvironment Environment;
        public PhotoGalleryController(IHostingEnvironment _environment, IBlob blob, IPhotoGalleryService service, ICommonService commonService)
        {
            Environment = _environment;
            _commonService = commonService;
            _blob = blob;
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            PhotoGallery model = new PhotoGallery();
            model = await _service.PhotoGalleryList();
            return View(model);
        }
        public async Task<IActionResult> Createold()
        {
            try
            {
                PhotoGallery model = new PhotoGallery();
                return View("Create", model);
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

        public async Task<IActionResult> Create()
        {
            try
            {
                PhotoGallery model = new PhotoGallery();
                return PartialView("~/Areas/Admin/Views/PhotoGallery/_CreateSlider.cshtml", model);
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

        public async Task<IActionResult> GetDetailsOld(Guid PhotoGalleryGI)
        {
            try
            {
                PhotoGallery model = new PhotoGallery();
                model = await _service.PhotoGalleryDetails(PhotoGalleryGI);
                model.PhotoGalleryGI = PhotoGalleryGI;
                return PartialView("~/Areas/Admin/Views/PhotoGallery/DetailsView.cshtml", model);
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
        public async Task<IActionResult> GetDetails(Guid PhotoGalleryGI)
        {
            try
            {
                PhotoGallery model = new PhotoGallery();
                model = await _service.PhotoGalleryDetails(PhotoGalleryGI);
                model.PhotoGalleryGI = PhotoGalleryGI;
                return PartialView("~/Areas/Admin/Views/PhotoGallery/_ViewSlider.cshtml", model);
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
        public async Task<IActionResult> GetList()
        {
            try
            {
                PhotoGallery model = new PhotoGallery();
                model = await _service.PhotoGalleryList();
                return PartialView("_List", model);
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

        private byte[] CreateThumbnail(byte[] imageData)
        {
            using (var ms = new MemoryStream(imageData))
            using (var image = Image.FromStream(ms))
            {
                int width = 150; // Set thumbnail width
                int height = (int)(image.Height / (double)image.Width * width);

                using (var thumbnail = new Bitmap(width, height))
                using (var g = Graphics.FromImage(thumbnail))
                {
                    g.CompositingQuality = CompositingQuality.HighQuality;
                    g.SmoothingMode = SmoothingMode.HighQuality;
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                    g.DrawImage(image, 0, 0, width, height);

                    using (var thumbStream = new MemoryStream())
                    {
                        thumbnail.Save(thumbStream, ImageFormat.Jpeg);
                        return thumbStream.ToArray();
                    }
                }
            }
        }
        [HttpPost]
        public async Task<IActionResult> Upload(PhotoGallery model)
        {
            var StorageType = await _commonService.GetFileStorageType();
            var i = 0;
            List<PhotoGallery> list = new List<PhotoGallery>();
            model.PhotoGalleryList = new List<PhotoGalleryList>();
            if (model.Files != null && model.Files.Count > 0)
            {


                foreach (var file in model.Files)
                {
                    if (file != null)
                    {
                        if (StorageType.Data == "BLOB")
                        {

                            var fileobj = await _commonService.UploadMultipleFileToBLOB(file, "Documents");

                            if (fileobj.Success == false)
                            {
                                fileobj.Message = "FILE UPLOAD ERROR";
                                return Json(fileobj);
                            }
                            model.PhotoGalleryList.Add(new PhotoGalleryList());
                            model.PhotoGalleryList[i].file = fileobj.Data;
                            model.PhotoGalleryList[i].Description = model.Descriptions[i];


                            byte[] fileDataList = new byte[0];
                            using (var memoryStream = new MemoryStream())
                            {
                                await file.CopyToAsync(memoryStream);
                                byte[] fileData = memoryStream.ToArray();
                                fileDataList = fileData;
                            }

                            model.PhotoGalleryList[i].ThumbnailUri = fileobj.Data;

                            //resize image For thumbnail
                            // byte[] resizedImage = _blob.ResizeByteImage(100, 100, fileDataList);
                            //model.PhotoGalleryList[i].ThumbnailUri.fdata = resizedImage;
                            //var fileobj1 = await _commonService.UploadByteArrayToBLOB(model.PhotoGalleryList[i].ThumbnailUri, "ThumbNail");
                            //model.PhotoGalleryList[i].ThumbnailUri = fileobj1.Data;

                            var thumbnailData = CreateThumbnail(fileDataList);
                            model.PhotoGalleryList[i].ThumbnailUri.fdata = thumbnailData;
                            var fileobj2 = await _commonService.UploadByteArrayToBLOB(model.PhotoGalleryList[i].ThumbnailUri, "ThumbNail");
                            model.PhotoGalleryList[i].ThumbnailUri = fileobj2.Data;
                        }
                        else if (StorageType.Data == "IIS")
                        {
                            var fileobj = await _commonService.UploadMultipleFileToIIS(file, "Documents");
                            if (fileobj.Success == false)
                            {
                                fileobj.Message = "FILE UPLOAD ERROR";
                                return Json(fileobj);
                            }
                            model.PhotoGalleryList.Add(new PhotoGalleryList());
                            model.PhotoGalleryList[i].file = fileobj.Data;
                            model.PhotoGalleryList[i].Description = model.Descriptions[i];

                            byte[] fileDataList = new byte[0];
                            using (var memoryStream = new MemoryStream())
                            {
                                await file.CopyToAsync(memoryStream);
                                byte[] fileData = memoryStream.ToArray();
                                fileDataList = fileData;
                            }

                            model.PhotoGalleryList[i].ThumbnailUri = fileobj.Data;

                            //resize image For thumbnail

                            var thumbnailData = CreateThumbnail(fileDataList);
                            model.PhotoGalleryList[i].ThumbnailUri.fdata = thumbnailData;
                            var fileobj2 = await _commonService.UploadByteArrayToIIS(model.PhotoGalleryList[i].ThumbnailUri, "ThumbNail");
                            model.PhotoGalleryList[i].ThumbnailUri = fileobj2.Data;

                        }
                        else
                        {
                            var fileobj = await _commonService.UploadMultipleFileToAWS(file, "Documents");
                            if (fileobj.Success == false)
                            {
                                fileobj.Message = "FILE UPLOAD ERROR";
                                return Json(fileobj);
                            }
                            model.PhotoGalleryList.Add(new PhotoGalleryList());
                            model.PhotoGalleryList[i].file = fileobj.Data;
                            model.PhotoGalleryList[i].Description = model.Descriptions[i];

                            byte[] fileDataList = new byte[0];
                            using (var memoryStream = new MemoryStream())
                            {
                                await file.CopyToAsync(memoryStream);
                                byte[] fileData = memoryStream.ToArray();
                                fileDataList = fileData;
                            }

                            model.PhotoGalleryList[i].ThumbnailUri = fileobj.Data;

                            //resize image For thumbnail

                            var thumbnailData = CreateThumbnail(fileDataList);
                            model.PhotoGalleryList[i].ThumbnailUri.fdata = thumbnailData;
                            var fileobj2 = await _commonService.UploadByteArrayToAWS(model.PhotoGalleryList[i].ThumbnailUri, "ThumbNail");
                            model.PhotoGalleryList[i].ThumbnailUri = fileobj2.Data;

                        }

                    }
                    i = i + 1;
                }

            }
            var response = await _service.Save(model);

            return Json(response);
        }
        public async Task<ActionResult> Delete(Guid PhotoGalleryGI)
        {
            try
            {
                var response = await _service.Delete(PhotoGalleryGI);

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
        public async Task<IActionResult> Edit(Guid PhotoGalleryGI)
        {
            try
            {
                PhotoGallery model = new PhotoGallery();
                model = await _service.PhotoGalleryDetails(PhotoGalleryGI);
                model.PhotoGalleryGI = PhotoGalleryGI;
                return View("~/Areas/Admin/Views/PhotoGallery/Edit.cshtml", model);
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
        public async Task<IActionResult> Edit(PhotoGallery model)
        {
            var StorageType = await _commonService.GetFileStorageType();
            var count = model.PhotoGalleryList.Count();
            var i = count;
            var k = 0;
            var j = 0;
            foreach (var item in model.PhotoGalleryList)
            {
                model.PhotoGalleryList[j].file.AbsoluteUri = model.PhotoGalleryList[j].file.RelativeUri;

                model.PhotoGalleryList[j].ThumbnailUri.AbsoluteUri = model.PhotoGalleryList[j].ThumbnailUri.RelativeUri;
                j = j + 1;
            }

                if (model.Files != null && model.Files.Count > 0)
            {


                foreach (var file in model.Files)
                {
                    if (file != null)
                    {
                        if (StorageType.Data == "BLOB")
                        {

                            var fileobj = await _commonService.UploadMultipleFileToBLOB(file, "Documents");
                            if (fileobj.Success == false)
                            {
                                fileobj.Message = "FILE UPLOAD ERROR";
                                return Json(fileobj);
                            }
                            model.PhotoGalleryList.Add(new PhotoGalleryList());
                            model.PhotoGalleryList[i].file = new Shared.File();
                            model.PhotoGalleryList[i].file = fileobj.Data;
                            model.PhotoGalleryList[i].Description = model.Descriptions[k];


                            byte[] fileDataList = new byte[0];
                            using (var memoryStream = new MemoryStream())
                            {
                                await file.CopyToAsync(memoryStream);
                                byte[] fileData = memoryStream.ToArray();
                                fileDataList = fileData;
                            }

                            model.PhotoGalleryList[i].ThumbnailUri = fileobj.Data;

                            var thumbnailData = CreateThumbnail(fileDataList);
                            model.PhotoGalleryList[i].ThumbnailUri.fdata = thumbnailData;
                            var fileobj2 = await _commonService.UploadByteArrayToBLOB(model.PhotoGalleryList[i].ThumbnailUri, "ThumbNail");
                            model.PhotoGalleryList[i].ThumbnailUri = fileobj2.Data;
                        }
                        else if (StorageType.Data == "IIS")
                        {
                            var fileobj = await _commonService.UploadMultipleFileToIIS(file, "Documents");
                            if (fileobj.Success == false)
                            {
                                fileobj.Message = "FILE UPLOAD ERROR";
                                return Json(fileobj);
                            }
                            model.PhotoGalleryList.Add(new PhotoGalleryList());
                            model.PhotoGalleryList[i].file = new Shared.File();
                            model.PhotoGalleryList[i].file = fileobj.Data;
                            model.PhotoGalleryList[i].Description = model.Descriptions[k];


                            byte[] fileDataList = new byte[0];
                            using (var memoryStream = new MemoryStream())
                            {
                                await file.CopyToAsync(memoryStream);
                                byte[] fileData = memoryStream.ToArray();
                                fileDataList = fileData;
                            }

                            model.PhotoGalleryList[i].ThumbnailUri = fileobj.Data;
                            //resize image For thumbnail
                            var thumbnailData = CreateThumbnail(fileDataList);
                            model.PhotoGalleryList[i].ThumbnailUri.fdata = thumbnailData;
                            var fileobj2 = await _commonService.UploadByteArrayToIIS(model.PhotoGalleryList[i].ThumbnailUri, "ThumbNail");
                            model.PhotoGalleryList[i].ThumbnailUri = fileobj2.Data;

                           
                        }
                        else
                        {
                            var fileobj = await _commonService.UploadMultipleFileToAWS(file, "Documents");
                            if (fileobj.Success == false)
                            {
                                fileobj.Message = "FILE UPLOAD ERROR";
                                return Json(fileobj);
                            }
                            model.PhotoGalleryList.Add(new PhotoGalleryList());
                            model.PhotoGalleryList[i].file = new Shared.File();
                            model.PhotoGalleryList[i].file = fileobj.Data;
                            model.PhotoGalleryList[i].Description = model.Descriptions[k];

                            byte[] fileDataList = new byte[0];
                            using (var memoryStream = new MemoryStream())
                            {
                                await file.CopyToAsync(memoryStream);
                                byte[] fileData = memoryStream.ToArray();
                                fileDataList = fileData;
                            }

                            model.PhotoGalleryList[i].ThumbnailUri = fileobj.Data;
                            //resize image For thumbnail
                            var thumbnailData = CreateThumbnail(fileDataList);
                            model.PhotoGalleryList[i].ThumbnailUri.fdata = thumbnailData;
                            var fileobj2 = await _commonService.UploadByteArrayToAWS(model.PhotoGalleryList[i].ThumbnailUri, "ThumbNail");
                            model.PhotoGalleryList[i].ThumbnailUri = fileobj2.Data;

                          
                        }

                    }
                    i = i + 1;
                    k = k + 1;
                }

            }
            var response = await _service.Modify(model);

            return Json(response);
        }
    }
}
