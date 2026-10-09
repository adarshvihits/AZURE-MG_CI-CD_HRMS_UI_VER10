using DocumentFormat.OpenXml.Office.CustomUI;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.Master;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.Master.Controllers
{
    [Area("Master")]
    [SessionAuthorize]
    public class FireBaseImageSettingsController : Controller
    {
        public readonly IFireBaseImageSettingsService _service;
        public readonly ICommonService _commonService;
        private readonly IDropdownService _dropdownService;
        private readonly IHelpers _helpers;
        private readonly IBlob _blob;
        public FireBaseImageSettingsController(IFireBaseImageSettingsService service, IDropdownService dropdownService, ICommonService commonService, IHelpers helpers, IBlob blob)
        {
            _service = service;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = helpers;
            _blob = blob;
            
        }


        public async Task<IActionResult> Index()
        {
            try
            {

                string TabIndex = "MobileSettings";
            FireBaseImageSettings model = await _service.GetMobileGeneralSettingsDetails(TabIndex);
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

   
        public async Task<ActionResult> Edit(int Code)
        {

            try
            {
                FireBaseImageSettings model = await _service.GetDetails(Code);
                return PartialView("_CreateorEdit", model);
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
        public async Task<IActionResult> Save(FireBaseImageSettings model, int Code)
        {
            try
            {
                var StorageType = await _commonService.GetFileStorageType();

                //model.Mode = "CREATE";
                if (model.Attachment != null)

                {
                    if (model.Attachment.FileData != null)
                    {
                        if (StorageType.Data == "BLOB")
                        {

                            var fileobj = await _commonService.UploadIformFileToBLOB(model.Attachment, "Documents");
                            if (fileobj.Success == false)
                            {
                                fileobj.Message = "FILE UPLOAD ERROR";
                                return Json(fileobj);
                            }
                            model.Attachment = fileobj.Data;

                        }
                        else if (StorageType.Data == "IIS")
                        {
                            var fileobj = await _commonService.UploadIformFileToIIS(model.Attachment, "Documents");
                            if (fileobj.Success == false)
                            {
                                fileobj.Message = "FILE UPLOAD ERROR";
                                return Json(fileobj);
                            }
                            model.Attachment = fileobj.Data;

                        }
                        else
                        {
                            var fileobj = await _commonService.UploadIformFileToAWS(model.Attachment, "Documents");
                            if (fileobj.Success == false)
                            {
                                fileobj.Message = "FILE UPLOAD ERROR";
                                return Json(fileobj);
                            }
                            model.Attachment = fileobj.Data;

                        }
                    }
                }
                model.Code = Code;
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


        public static string GetMimeTypeFromFilename(string filename)
        {
            if (string.IsNullOrEmpty(filename))
            {
                return null;
            }

            var extension = Path.GetExtension(filename).TrimStart('.').ToLowerInvariant();
            var mimeTypeMap = new Dictionary<string, string>()
          {
        { "jpg", "data:image/jpeg;base64," },
        { "jpeg", "data:image/jpeg;base64," },
        { "png", "data:image/png;base64," },
        // Add more mappings for other supported extensions
          };

            return mimeTypeMap.ContainsKey(extension) ? mimeTypeMap[extension] : null;
        }


        [HttpPost]
        public async Task<IActionResult> MobileGeneralSettingsSave(FireBaseImageSettings model)
        {
            try
            {

                var response = await _service.MobileGeneralSettingsSave(model);
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
        public async Task<ActionResult> GetMobileGeneralSettingsDetails(string TabIndex)
        {
            try
            {
                if (TabIndex == "MobileSettings")
                {
                    FireBaseImageSettings model = await _service.GetMobileGeneralSettingsDetails(TabIndex);

                    return PartialView("_MobileGeneralSettings", model);
                }
                else
                {
                    FireBaseImageSettings model = new FireBaseImageSettings();
                    model = await _service.GetList();

                    Filter filter = new Filter();

                    //retreive  pic From AZure Storage
                    var storage = await _commonService.GetFileStorageType();
                    if (storage.Data == "BLOB")
                    {
                        for (int j = 0; j < model.FireBaseImageSettingsList.Count; j++)
                        {

                            Shared.File file = new Shared.File();
                            file.FileName = model.FireBaseImageSettingsList[j].FileName;
                            file = await _blob.GetBlobFile(file);
                            if (file != null)
                            {
                                file.fdata = file.stream.ToArray();
                                var mimeType = GetMimeTypeFromFilename(model.FireBaseImageSettingsList[j].FileName);
                                model.FireBaseImageSettingsList[j].Base64string = mimeType + Convert.ToBase64String(file.fdata, 0, file.fdata.Length);
                            }

                        }
                    }
                    else
                    {
                        for (int j = 0; j < model.FireBaseImageSettingsList.Count; j++)
                        {

                            model.FireBaseImageSettingsList[j].Base64string = model.FireBaseImageSettingsList[j].AbsoluteUri;
                        }
                    }
                    return PartialView("_FireBaseImageSettingsList", model);
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





    }
}
