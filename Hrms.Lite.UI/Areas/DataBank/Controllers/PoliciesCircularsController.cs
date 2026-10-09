using System;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.DataBank.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hrms.Lite.UI.Areas.DataBank.Controllers
{
    [Area("DataBank")]
    [SessionAuthorize]
    public class PoliciesCircularsController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly IPoliciesCircularService _service;
        private readonly ICommonService _commonService; 
        private readonly IBlob _blob;
        public PoliciesCircularsController(IPoliciesCircularService service, IDropdownService dropdownService, ICommonService commonService, IBlob blob)
        {
            _service = service;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _blob = blob;
        }
        public async Task<IActionResult> Index()
        {
            try { 
            PoliciesCirculars model = new PoliciesCirculars();
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
        public async Task<IActionResult> GetPolicyListList()
        {
            PoliciesCirculars model = new PoliciesCirculars();
            model = await _service.GetList();
            return PartialView("_List", model);
        }
        public async Task<IActionResult> Create()
        {
            PoliciesCirculars model = new PoliciesCirculars();
            ViewBag.MultipleCombination = new SelectList(await _dropdownService.GetHRPolicyCombinationDropDown(), "Code", "Name");
            ViewBag.Category = new SelectList(await _dropdownService.GetHRPolicyCategoryDropDown(), "Code", "Name");
            model.Mode = "Create";
            return View(model);
        }

        public ActionResult PdfViewer(string url)
        {

            ViewBag.PDF = url;
          
            return PartialView("New");
        }


        [HttpPost]
        public async Task<IActionResult> Create(PoliciesCirculars model)
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
                        using (var ms = new MemoryStream())
                        {
                            await model.Attachment.FileData.CopyToAsync(ms);
                            model.Attachment.fdata = ms.ToArray(); // raw bytes, no compression
                        }

                        model.Attachment.FileName = model.Attachment.FileData.FileName;
                        model.Attachment.ContentType = model.Attachment.FileData.ContentType;

                        model.Attachment.FileData = null;
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
                if (model.Mode != "Modify")
                    {
                        var response = await _service.PolicySave(model);
                        return Json(response);
                    }

                    else
                    {
                        var response = await _service.PolicyUpdate(model);
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
            PoliciesCirculars model = new PoliciesCirculars();
            ViewBag.MultipleCombination = new SelectList(await _dropdownService.GetHRPolicyCombinationDropDown(), "Code", "Name");
            ViewBag.Category = new SelectList(await _dropdownService.GetHRPolicyCategoryDropDown(), "Code", "Name");
            model = await _service.GetPolicyDetails(GI);
            model.Mode = "Modify";
            if (model.Combinations1[0].EmployeeID != "")
            {
                ViewBag.CombinationName = "Employee";
            }
            return View("Create",model);
        }
        public async Task<IActionResult> GetCombinationDetails(int id,int type,string combinationName)
        {
            try
            {
                ViewBag.CombinationName = combinationName;
                if (type == 0)
                {
                    PoliciesCirculars model = new PoliciesCirculars();
                    if (id != 0)
                    {
                        model = await _service.GetCombinationDetails(id);
                        model.Combinations1 = model.CombinationsList;
                    }
                    return PartialView("_CombinationList1", model);
                }
                else
                {
                    PoliciesCirculars model1 = new PoliciesCirculars();
                    if (id != 0)
                    {
                        model1 = await _service.GetCombinationDetails(id);
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
        public async Task<IActionResult> GetCategoryPopUp()
        {
            //var list = new List<PossibleResultsClassifications>();
            //list.Add(new PossibleResultsClassifications());
            PoliciesCirculars model = new PoliciesCirculars();
            model = await _service.GetCategoryDtls();
            if (model.Category.Count == 0)
            {
                model.Category = new List<PolicyCategory>();
                PolicyCategory list = new PolicyCategory()
                {
                    Code = 0,
                    Name = ""
                };
                model.Category.Add(list);
            }
            return PartialView("_categoryPopup", model);
        }
        [HttpPost]
        public async Task<IActionResult> SaveCategory(PoliciesCirculars input)
        {
            var response = await _service.CategoryResultSave(input.Category);
            return Json(response);

        }


        public async Task<IActionResult> DeleteCategory(Guid CategoryGI)
        {
            try
            {
                PolicyCategory model = new PolicyCategory();
                model.GI = CategoryGI;
                var response = await _service.DeleteCategory(model);
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


        [HttpPost]
        public async Task<ActionResult> Delete(Guid PolicyGI)
        {
            try
            {
                PoliciesCirculars model = new PoliciesCirculars();
                model.PolicyGI = PolicyGI;
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
        public async Task<IActionResult> ShowDoc(Guid PolicyGI)
        {
            var fileData = await _service.DownloadDoc(PolicyGI);

            if (fileData?.fdata == null)
                return NotFound();

            // Use raw bytes directly
            var fileBytes = fileData.fdata;
            var contentType = fileData.ContentType ?? "application/octet-stream";

            Response.Headers["Content-Disposition"] = $"inline; filename=\"{fileData.FileName}\"";

            return File(fileBytes, contentType);
        }

    }
}
