using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.Account;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.Account;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Controllers
{
    [Area("Master")]
    [SessionAuthorize]
    public class CustomizeSoftwareController : Controller
    {
        private readonly IAccountService _AccountService;
        private readonly ICommonService _commonService;
        private readonly IWebHostEnvironment Environment;
        private readonly IBlob _blob;
        public CustomizeSoftwareController(Services.IServices.Account.IAccountService AccountService, 
         ICommonService commonService, IWebHostEnvironment _environment, IBlob blob)
        {
            _AccountService = AccountService;

            _commonService = commonService;
          
            Environment = _environment;
            _blob = blob;
        }
        #region UploadSoftwareLogo
        public IActionResult UploadSoftwareLogo()
        {
            ViewBag.Message = null;
            ViewBag.Type = "SoftwareLogo";
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UploadSoftwareLogo(CustomSoftware software)
        {      
            

            string contentPath = this.Environment.ContentRootPath;


            string path = Path.Combine(this.Environment.WebRootPath, "CompanyLogo");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            if (software.SoftwareLogo != null)
            {
                string fileName = Path.GetFileName(software.SoftwareLogo.FileData.FileName);
                using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                {
                    software.SoftwareLogo.FileData.CopyTo(stream);

                }
                var serverpath = Path.Combine(Environment.WebRootPath, "CompanyLogo\\" + fileName);
                var contents = System.IO.File.ReadAllBytes(serverpath);
               
                software.SoftwareLogo.fdata = contents;
                software.SoftwareLogo.FileData = null;

               var  response = await _commonService.Save_SoftwareLogo("SOFTARE_LOGO", software);
                ViewBag.Message = response.Message;
                if (System.IO.File.Exists(serverpath))
                {
                    System.IO.File.Delete(serverpath);
                }
            }
          
            return RedirectToAction("UploadSoftwareLogo");
        }


        [HttpPost]
        public async Task<IActionResult> UploadJpgSoftwareLogo(CustomSoftware software)
        {
            var StorageType = await _commonService.GetFileStorageType();
            if (software.SoftwareLogo != null)
            {
                if (software.SoftwareLogo.FileData != null)
                {
                    if (StorageType.Data == "BLOB")
                    {

                        var fileobj = await _commonService.UploadIformFileToBLOB(software.SoftwareLogo, "ThumbNail");
                        if (fileobj.Success == false)
                        {
                            fileobj.Message = "FILE UPLOAD ERROR";
                            return Json(fileobj);
                        }
                        software.SoftwareLogo = fileobj.Data;

                    }
                    else if (StorageType.Data == "IIS")
                    {
                        var fileobj = await _commonService.UploadIformFileToIIS(software.SoftwareLogo, "ThumbNail");
                        if (fileobj.Success == false)
                        {
                            fileobj.Message = "FILE UPLOAD ERROR";
                            return Json(fileobj);
                        }
                        software.SoftwareLogo = fileobj.Data;

                    }
                    else
                    {
                        var fileobj = await _commonService.UploadIformFileToAWS(software.SoftwareLogo, "ThumbNail");
                        if (fileobj.Success == false)
                        {
                            fileobj.Message = "FILE UPLOAD ERROR";
                            return Json(fileobj);
                        }
                        software.SoftwareLogo = fileobj.Data;

                    }
                }
            }
            string contentPath = this.Environment.ContentRootPath;

            string path = Path.Combine(this.Environment.WebRootPath, "CompanyLogo");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            if (software.SoftwareLogo != null)
            {
                string fileName = Path.GetFileName(software.SoftwareLogo.FileData.FileName);
                using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                {
                    software.SoftwareLogo.FileData.CopyTo(stream);

                }
                var serverpath = Path.Combine(Environment.WebRootPath, "CompanyLogo\\" + fileName);
                var contents = System.IO.File.ReadAllBytes(serverpath);

                software.SoftwareLogo.fdata = contents;
                software.SoftwareLogo.FileData = null;

                var response = await _commonService.Save_JPG_SoftwareLogo("SOFTARE_LOGO", software);
                ViewBag.Message = response.Message;
                if (System.IO.File.Exists(serverpath))
                {
                    System.IO.File.Delete(serverpath);
                }
                return Json(response);
            }

            return RedirectToAction("UploadSoftwareLogo");
        }

        #endregion
        #region UploadSoftware NAme

        public async Task<IActionResult> UploadSoftwareName()
        {
            ViewBag.Message = null;
            ViewBag.Type = "SoftwareName";
            CustomSoftware model = new CustomSoftware();
            model = await _AccountService.GetSoftwareLogo();
            return View("UploadSoftwareLogo", model);
        }
        [HttpPost]
        public async Task<IActionResult> UploadSoftwareName(CustomSoftware software)
        {
            var serverpath_essp = "";
            var serverpath_hrms = "";


            string contentPath = this.Environment.ContentRootPath;

            string path = Path.Combine(this.Environment.WebRootPath, "CompanyLogo");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            if (software.SoftwareName != null)
            {
                string fileNameessp = Path.GetFileName(software.SoftwareName.FileData.FileName);
                using (FileStream stream = new FileStream(Path.Combine(path, fileNameessp), FileMode.Create))
                {
                    software.SoftwareName.FileData.CopyTo(stream);

                }
                serverpath_essp = Path.Combine(Environment.WebRootPath, "CompanyLogo\\" + fileNameessp);
                var contents_essp = System.IO.File.ReadAllBytes(serverpath_essp);
                software.SoftwareName.fdata = contents_essp;
                software.SoftwareName.FileName = software.SoftwareName.FileData.FileName;
                software.SoftwareName.FileData = null;
            }
            if (software.HRMS_SoftwareName != null)
            {
                string fileNamehrms = Path.GetFileName(software.HRMS_SoftwareName.FileData.FileName);


                using (FileStream stream = new FileStream(Path.Combine(path, fileNamehrms), FileMode.Create))
                {
                    software.HRMS_SoftwareName.FileData.CopyTo(stream);

                }


                serverpath_hrms = Path.Combine(Environment.WebRootPath, "CompanyLogo\\" + fileNamehrms);

                var contents_hrms = System.IO.File.ReadAllBytes(serverpath_hrms);

                software.HRMS_SoftwareName.fdata = contents_hrms;
                software.HRMS_SoftwareName.FileName = software.HRMS_SoftwareName.FileData.FileName;
                software.HRMS_SoftwareName.FileData = null;
            }
         
       
            var response = await _commonService.Save_SoftwareLogo("SOFTARE_NAME", software);

            if (System.IO.File.Exists(serverpath_essp))
            {
                System.IO.File.Delete(serverpath_essp);
            }
            if (System.IO.File.Exists(serverpath_hrms))
            {
                System.IO.File.Delete(serverpath_hrms);
            }
            ViewBag.Message = response.Message;
            return RedirectToAction("UploadSoftwareName");
        }
        #endregion

        //public async Task<IActionResult> Login()
        //{
        //    CustomSoftware model = await _AccountService.GetSoftwareLogo();
        //    if (model.SoftwareLogo != null)
        //    {
        //        var imreBase64Data = Convert.ToBase64String(model.SoftwareLogo.fdata);
        //        //string imgDataURL = string.Format("data:image/png;base64,{0}", imreBase64Data);
        //        var imgDataURL = String.Format("data:image/svg+xml;base64,{0}", imreBase64Data);

        //        ViewBag.SoftwareLogo = imgDataURL;
        //    }
        //    if (model.HRMS_SoftwareName != null)
        //    {
        //        var imreBase64Data = Convert.ToBase64String(model.HRMS_SoftwareName.fdata);
        //        //string imgDataURL = string.Format("data:image/png;base64,{0}", imreBase64Data);
        //        var imgDataURL = String.Format("data:image/svg+xml;base64,{0}", imreBase64Data);

        //        ViewBag.SoftwareName = imgDataURL;
        //    }

        //    return View("~/Views/Account/Login.cshtml");


        //}
    }
}
