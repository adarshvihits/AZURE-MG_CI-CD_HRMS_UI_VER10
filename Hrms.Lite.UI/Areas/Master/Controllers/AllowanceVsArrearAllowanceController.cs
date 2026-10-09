using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.Master;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Spire.Xls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.Master.Controllers
{
    [Area("Master")]
    [SessionAuthorize]
    public class AllowanceVsArrearAllowanceController : Controller
    {
        public readonly IAllowanceVsArrearAllowanceService _service;
        public readonly ICommonService _commonService;
        private readonly IDropdownService _dropdownService;
        public AllowanceVsArrearAllowanceController(IAllowanceVsArrearAllowanceService service, ICommonService commonService, IDropdownService dropdownService)
        {
            _service = service;
            _commonService = commonService;
            _dropdownService = dropdownService;
        }
        public async Task<ActionResult> Index()
        {
            try
            {
                AllowanceVsArrearAllowance model = new AllowanceVsArrearAllowance();
                ViewBag.ArrearAllowance = new SelectList(await _dropdownService.GetArrearVsAllowanceDropdown(), "Code", "Name");
                model = await _service.GetAllowanceVsArrearAllowanceList();
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
        public async Task<ActionResult> Save(AllowanceVsArrearAllowance model)
        {
            try
            {
                var response = await _service.SaveAllowanceVsArrearAllowance(model);
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
