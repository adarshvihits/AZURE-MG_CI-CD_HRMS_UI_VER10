using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.Master;
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
    public class CompanyDocumentsController : Controller
    {
        public readonly ICompanyDocumentsService _service;
        public readonly ICommonService _commonService;
        private readonly IDropdownService _dropdownService;
        public CompanyDocumentsController(ICompanyDocumentsService service, IDropdownService dropdownService, ICommonService commonService)
        {
            _service = service;
            _commonService = commonService;
            _dropdownService = dropdownService;
        }


        public async Task<IActionResult> Index()
        {           

            try
            {
                return View(await _service.GetList("ACTIVE"));
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
                CompanyDocuments model = new CompanyDocuments();
                model.Mode = "CREATE";
                model.Active = true;
                ViewBag.doc = new SelectList(await _dropdownService.GetCompanyDocumentTypeDropdown(), "Code", "Name");
                return PartialView("~/Areas/Master/Views/CompanyDocuments/_CreateOrEdit.cshtml", model);
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


        public async Task<IActionResult> Edit(Guid LogGI, Guid DocumentGI)
        {
            try
            {
                CompanyDocuments model = await _service.GetCompanyDocumentsDetails(DocumentGI, LogGI, "MODIFY");
                model.Mode = "MODIFY";
                ViewBag.doc = new SelectList(await _dropdownService.GetCompanyDocumentTypeDropdown(), "Code", "Name");

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

        [HttpPost]
        public async Task<ActionResult> Save(CompanyDocuments Input)
        {
            try
            {
                var response = await _service.Create(Input);

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