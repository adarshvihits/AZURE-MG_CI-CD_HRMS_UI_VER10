using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.DataBank.Controllers
{
    [Area("DataBank")]
    [SessionAuthorize]
    public class SocialWallController : Controller
    {
        private readonly IBlob _blob;
        private readonly ISocialWallService _service;
        private readonly ICommonService _commonService;
        private IHostingEnvironment Environment;
        private readonly IDropdownService _dropdownService;
      

        public SocialWallController(IHostingEnvironment _environment,IBlob blob, ISocialWallService service, ICommonService commonService, IDropdownService dropDownService)
        {
            Environment = _environment;
            _commonService = commonService;
            _blob = blob;
            _service = service;
            _dropdownService = dropDownService;
        }
        public async Task<IActionResult> Index(string TabIndex= "Active")
        {
            SocialWall model = new SocialWall();
            Filter filter = new Filter();
            model = await _service.SocialWallList(TabIndex, filter);
            model.Tab = TabIndex;
            model.Filter = await _commonService.GetFilterDropdowns();
            return View(model);
        }
       
       
        public async Task<IActionResult> Create()
        {
            try
            {
                SocialWall model = new SocialWall();
                ViewBag.GrpCompany = new SelectList(await _dropdownService.GetGroupCompanyDropDown(), "Code", "Name");
                return PartialView("~/Areas/DataBank/Views/SocialWall/_AddNew.cshtml", model);
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

        public async Task<IActionResult> GetDetails(Guid SocialWallGI)
        {
            try
            {
                SocialWall model = new SocialWall();
                model = await _service.SocialWallDetails(SocialWallGI);
                model.EmployeeHeader = await _commonService.GetEmployeeHeader(model.EmployeeGI);
                model.SocialWallGI = SocialWallGI;
                ViewBag.GrpCompany = new SelectList(await _dropdownService.GetGroupCompanyDropDown(), "Code", "Name");
                return PartialView("~/Areas/DataBank/Views/SocialWall/_Approve.cshtml", model);
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

        public async Task<IActionResult> GetList(string TabIndex,Filter filter)
        {
            try
            {
                SocialWall model = new SocialWall();
                model = await _service.SocialWallList(TabIndex, filter);
                model.Tab = TabIndex;
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

        [HttpPost]
        public async Task<ActionResult> Save(SocialWall input)
        {

            var StorageType = await _commonService.GetFileStorageType();

            try
            {
                if (input.file != null)
                {
                    if (input.file.FileData != null)
                    {
                        if (StorageType.Data == "BLOB")
                        {

                            var fileobj = await _commonService.UploadIformFileToBLOB(input.file, "Documents");
                            if (fileobj.Success == false)
                            {
                                fileobj.Message = "FILE UPLOAD ERROR";
                                return Json(fileobj);
                            }
                            input.file = fileobj.Data;

                        }
                        else if (StorageType.Data == "IIS")
                        {
                            var fileobj = await _commonService.UploadIformFileToIIS(input.file, "Documents");
                            if (fileobj.Success == false)
                            {
                                fileobj.Message = "FILE UPLOAD ERROR";
                                return Json(fileobj);
                            }
                            input.file = fileobj.Data;

                        }
                        else
                        {
                            var fileobj = await _commonService.UploadIformFileToAWS(input.file, "Documents");
                            if (fileobj.Success == false)
                            {
                                fileobj.Message = "FILE UPLOAD ERROR";
                                return Json(fileobj);
                            }
                            input.file = fileobj.Data;

                        }
                    }
                }
                //try
                //{
                //    if (input.file != null && input.file.FileData != null)
                //    {
                //        input.file = await _blob.UploadToAzureAsync(input.file);
                //        input.file.FileData = null;
                //    }
                var response = await _service.HRSave(input);

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
        public async Task<ActionResult> Delete(Guid SocialWallGI)
        {
            try
            {                
                var response = await _service.Delete(SocialWallGI);

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


        public async Task<IActionResult> ApproveOrReject(SocialWall model, string Mode)
        {
            try
            {
                //SocialWall model = new SocialWall();
                //model.SocialWallGI = SocialWallGI;
                model.ApproveMode = Mode;
                //model.GroupCompany = GrpCmp;
                ViewBag.GrpCompany = new SelectList(await _dropdownService.GetGroupCompanyDropDown(), "Code", "Name");
                var response = await _service.Approve(model);
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

        public IActionResult SaveProfileToFolder(SocialWall model, string SelectedTab)
        {
            //Global.model = model;
            //Global.SelecedTab = SelectedTab;
            string path = Path.Combine(this.Environment.WebRootPath, "Personal_Profile");

            string fileName = model.file.FileData.FileName;

            if (model.file.FileData.Length > (1024 * 1024))
            {
                //----------------compress image --------------------------------------
                var Imgstream = model.file.FileData.OpenReadStream();
                System.Drawing.Image originalImage = System.Drawing.Image.FromStream(Imgstream);

                // Set the maximum width and height for the compressed image
                int maxWidth = 800;
                int maxHeight = 600;

                // Calculate the new size while maintaining the aspect ratio
                int newWidth, newHeight;
                if (originalImage.Width > originalImage.Height)
                {
                    newWidth = maxWidth;
                    newHeight = (int)((double)originalImage.Height / originalImage.Width * maxWidth);
                }
                else
                {
                    newHeight = maxHeight;
                    newWidth = (int)((double)originalImage.Width / originalImage.Height * maxHeight);
                }
                var stream2 = model.file.FileData.OpenReadStream();
                System.Drawing.Image resized = resizeImage(newWidth, newHeight, stream2);
                using var imageStream = new MemoryStream();
                resized.Save(imageStream, ImageFormat.Jpeg);
                var imageBytes = imageStream.ToArray();
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                System.Drawing.Image finalImage = System.Drawing.Image.FromStream(imageStream);
                finalImage.Save(Path.Combine(path, fileName), ImageFormat.Jpeg);
                ViewBag.JsonData = $"data:image/png;base64,{Convert.ToBase64String(imageBytes)}";
                //Global.jsonD = $"data:image/png;base64,{Convert.ToBase64String(imageBytes)}";
                //----------------------------------------------------------------------
            }
            else
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                {
                    model.file.FileData.CopyTo(stream);
                }
            }
            //Global.SavedFileName = fileName;
            ViewBag.fileName = fileName;
            return PartialView("_Profile");
        }

        public System.Drawing.Image resizeImage(int width, int height, Stream resourceImage)
        {
            try
            {
                var image = System.Drawing.Image.FromStream(resourceImage);
                var thumb = image.GetThumbnailImage(width, height, () => false, IntPtr.Zero);

                return thumb;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public IActionResult LoadProfilePic_In_KendoUI(string fileName)
        {
            var serverpath = Path.Combine(Environment.WebRootPath, "Personal_Profile\\" + fileName);
            var contents = System.IO.File.ReadAllBytes(serverpath);
            return Json($"data:image/png;base64,{Convert.ToBase64String(contents)}");

        }

        [AcceptVerbs("Post")]
        public async Task<ActionResult> SaveProfilePicFromKendo(string contentType, string base64, string fileName)
        {
            var serverpath = Path.Combine(Environment.WebRootPath, "Personal_Profile\\" + fileName);
            try
            {
                var fileContents = Convert.FromBase64String(base64);
                SocialWall model = new SocialWall();
                SocialWall model1 = new SocialWall();
                model = new SocialWall();                
                model.file = new Shared.File();
                model.file.fdata = fileContents;
                model.file.FileName = fileName;
                model.file.ContentType = contentType;
                model.file.FileExtension = Path.GetExtension(fileName);
                //model.TabIndex = "ProfilePhoto";

                //model1.BasicInfo = new Shared.DataBank.Generic.EmployeeBasicInfo();
                //model1.BasicInfo.EmployeeThumbNail = new Shared.File();
                //model1.BasicInfo.EmployeeThumbNail.fdata = fileContents;
                //model1.BasicInfo.EmployeeThumbNail.FileName = fileName;
                //model1.BasicInfo.EmployeeThumbNail.ContentType = contentType;
                //model1.BasicInfo.EmployeeThumbNail.FileExtension = Path.GetExtension(fileName);

                // ...custom logic for saving the string to the database...

                string original = "";
                string thumb = "";



                model.file = await _blob.UploadToAzureAsyncWithByteArray(model.file);
                //model.BasicInfo.EmployeeThumbNail = await _blob.UploadToAzureThumbnailAsyncWithByteArray(model1.BasicInfo.EmployeeThumbNail);


                original = model.file.FileName;
                //thumb = model.BasicInfo.EmployeeThumbNail.FileName;


                //Global.model.file = model.file;
                //Global.model.BasicInfo.EmployeeThumbNail = model.BasicInfo.EmployeeThumbNail;

                TempData["Message"] = "Uploaded Sucessfully";

                //if (thumb != "" && original != "")
                //{
                //    await _blob.Delete(original);
                //    await _blob.Delete(thumb);
                //}


                if (System.IO.File.Exists(serverpath))
                {
                    System.IO.File.Delete(serverpath);
                }
                //Global.isRefreshed = false;

                //return Json(true);
                //return PartialView("_EnrollmentHeader", model);
                return RedirectToAction("Create");
            }
            catch (UnauthorizedAccessException e)
            {
                if (System.IO.File.Exists(serverpath))
                {
                    System.IO.File.Delete(serverpath);
                }
                return RedirectToAction("Login", "Account", new { Area = "" });
            }
            catch (Exception e)
            {
                if (System.IO.File.Exists(serverpath))
                {
                    System.IO.File.Delete(serverpath);
                }
                return RedirectToAction("Error", "Account", new { Area = "" });
            }
        }
    }
}
