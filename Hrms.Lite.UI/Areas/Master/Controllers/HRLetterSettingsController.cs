using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.Master;
using Hrms.Lite.Shared;
using Hrms.Lite.Shared.Master;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.Master.Controllers
{
    [Area("Master")]
    [SessionAuthorize]
    public class HRLetterSettingsController : Controller
    {
        public readonly IHRLetterSettingsService _service;
        public readonly ICommonService _commonService;
        private readonly IDropdownService _dropdownService;
        private readonly IHelpers _helpers;
        private readonly IBlob _blob;

        public HRLetterSettingsController(IHRLetterSettingsService service, IDropdownService dropdownService, ICommonService commonService, IHelpers Helpers, IBlob blob)
        {
            _service = service;
            _commonService = commonService;
            _dropdownService = dropdownService;
            _helpers = Helpers;
            _blob = blob;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                HRLetterSettings model = new HRLetterSettings();
                ViewBag.doc = new SelectList(await _dropdownService.GetDocumentTypeDropdown(), "Code", "Name");
                model = await _service.GetHRLetterSettingsDetails(0);
                model.Attachment1 = new File();
                model.Attachment2 = new File();
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


    



        [HttpPost]
        public async Task<IActionResult> Save(HRLetterSettings model,int LetterType)
        {
            var StorageType = await _commonService.GetFileStorageType();
            try
            {
                if (model.Attachment1 != null)
                {
                    if (model.Attachment1.FileData != null)
                    {
                        if (StorageType.Data == "BLOB")
                        {

                            var fileobj = await _commonService.UploadIformFileToBLOB(model.Attachment1, "Signature");
                            if (fileobj.Success == false)
                            {
                                fileobj.Message = "FILE UPLOAD ERROR";
                                return Json(fileobj);
                            }
                            model.Attachment1 = fileobj.Data;

                        }
                        else if (StorageType.Data == "IIS")
                        {
                            var fileobj = await _commonService.UploadIformFileToIIS(model.Attachment1, "Signature");
                            if (fileobj.Success == false)
                            {
                                fileobj.Message = "FILE UPLOAD ERROR";
                                return Json(fileobj);
                            }
                            model.Attachment1 = fileobj.Data;

                        }
                        else
                        {
                            var fileobj = await _commonService.UploadIformFileToAWS(model.Attachment1, "Signature");
                            if (fileobj.Success == false)
                            {
                                fileobj.Message = "FILE UPLOAD ERROR";
                                return Json(fileobj);
                            }
                            model.Attachment1 = fileobj.Data;

                        }
                    }
                }
                if (model.Attachment2 != null)
                {
                    if (model.Attachment2.FileData != null)
                    {
                        if (StorageType.Data == "BLOB")
                        {

                            var fileobj = await _commonService.UploadIformFileToBLOB(model.Attachment2, "Signature");
                            if (fileobj.Success == false)
                            {
                                fileobj.Message = "FILE UPLOAD ERROR";
                                return Json(fileobj);
                            }
                            model.Attachment2 = fileobj.Data;

                        }
                        else if (StorageType.Data == "IIS")
                        {
                            var fileobj = await _commonService.UploadIformFileToIIS(model.Attachment2, "Signature");
                            if (fileobj.Success == false)
                            {
                                fileobj.Message = "FILE UPLOAD ERROR";
                                return Json(fileobj);
                            }
                            model.Attachment2 = fileobj.Data;

                        }
                        else
                        {
                            var fileobj = await _commonService.UploadIformFileToAWS(model.Attachment2, "Signature");
                            if (fileobj.Success == false)
                            {
                                fileobj.Message = "FILE UPLOAD ERROR";
                                return Json(fileobj);
                            }
                            model.Attachment2 = fileobj.Data;

                        }
                    }
                }
                var response = await _service.HRLetterSettingsSave(model,LetterType);
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





        public async Task<ActionResult> GetHRLetterSettingsDetails(int LetterType)
        {
            try
            {
                HRLetterSettings model = await _service.GetHRLetterSettingsDetails(LetterType);

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










    }
}
