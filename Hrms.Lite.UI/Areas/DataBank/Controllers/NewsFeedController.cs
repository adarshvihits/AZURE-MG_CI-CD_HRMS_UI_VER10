using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.DataBank.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Hrms.Lite.UI.Areas.DataBank.Controllers
{
    [Area("DataBank")]
    [SessionAuthorize]
    public class NewsFeedController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly INewsFeedService _service;
        private readonly ICommonService _commonService;
        private readonly IBlob _blob;

        public NewsFeedController(INewsFeedService service, IDropdownService dropdownService, ICommonService commonService, IBlob blob)
        {
            _service = service;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _blob = blob;
        }


        public async Task<IActionResult> Index()
        {
            try { 
            NewsFeed model = new NewsFeed();
            model = await _service.GetList();
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
        public async Task<IActionResult> Create()
        {
            NewsFeed model = new NewsFeed();
            ViewBag.MultipleCombination = new SelectList(await _dropdownService.GetAnnouncementCombinationDropDown(), "Code", "Name");
            model.Mode = "Create";
            return View(model);
        }

        public async Task<IActionResult> GetCombinationDetails(int id, int type, string combinationName)
        {
            try
            {
                ViewBag.CombinationName = combinationName;
                if (type == 0)
                {
                    NewsFeed model = new NewsFeed();
                    if (id != 0)
                    {
                        model = await _service.GetNewsFeedCombinationDetails(id);
                        model.Combinations1 = model.CombinationsList;
                    }
                    return PartialView("_CombinationList1", model);
                }
                else
                {
                    NewsFeed model1 = new NewsFeed();
                    if (id != 0)
                    {
                        model1 = await _service.GetNewsFeedCombinationDetails(id);
                        model1.Combinations2 = model1.CombinationsList;
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
        public async Task<IActionResult> Create(NewsFeed model)
        {
            Shared.File file1 = new Shared.File();
            file1 = model.Attachment;
            var StorageType = await _commonService.GetFileStorageType();

            try
            {
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
                if (model.Combinations1 != null)
                {
                    var cmb1 = from li in model.Combinations1
                               where li.Applicability == true
                               select li;
                    model.Combinations1 = cmb1.ToList();
                }
                if (model.Combinations2 != null)
                {
                    var cmb2 = from li in model.Combinations2
                               where li.Applicability == true
                               select li;
                    model.Combinations2 = cmb2.ToList();
                }
                if (model.Mode != "Modify")
                {
                    var response = await _service.NewsFeedSave(model);
                    return Json(response);
                }

                else
                {
                    var response = await _service.NewsFeedUpdate(model);
                    return Json(response);
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

        public async Task<IActionResult> Edit(Guid GI)
        {
            NewsFeed model = new NewsFeed();
            ViewBag.MultipleCombination = new SelectList(await _dropdownService.GetAnnouncementCombinationDropDown(), "Code", "Name");
            model = await _service.GetNewsFeedDetails(GI);
            model.Mode = "Modify";
            if (model.Combinations1[0].EmployeeID != "")
            {
                ViewBag.CombinationName = "Employee";
            }
           
            return View("Create", model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid NewsFeedGI)
        {
            try
            {
                NewsFeed model = new NewsFeed();
                model.NewsFeedGI = NewsFeedGI;
                var response = await _service.Delete(model);
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
