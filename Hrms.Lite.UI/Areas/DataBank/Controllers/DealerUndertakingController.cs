using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.DataBank;
using Hrms.Lite.Shared.General;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using Spire.Xls;
using Newtonsoft.Json;
using Hrms.Lite.Shared.Account;
using File = Hrms.Lite.Shared.File;

namespace Hrms.Lite.UI.Areas.DataBank.Controllers
{
    [Area("DataBank")]
    [SessionAuthorize]
    public class DealerUndertakingController : Controller
    {
        private readonly IDealerUndertakingService _DealerUndertakingService;
        private readonly IHelpers _helpers;
        private readonly IDropdownService _dropdownService;
        private readonly ICommonService _commonService;

        public DealerUndertakingController(IDealerUndertakingService DealerUndertakingService, IDropdownService dropdownService, IHelpers Helpers, ICommonService commonService)
        {
            _DealerUndertakingService = DealerUndertakingService;
            _dropdownService = dropdownService;
            _helpers = Helpers;
            _commonService = commonService;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                DealerUndertaking model = new DealerUndertaking();

                model = await _DealerUndertakingService.DealerUndertakingList("General");

                ViewBag.ListName = "General";
                return View("Index", model);
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
        public async Task<IActionResult> GetList(string TabIndex)
        {
            try
            {
                ViewBag.ListName = TabIndex;
                DealerUndertaking model = new DealerUndertaking();
                model = await _DealerUndertakingService.DealerUndertakingList(TabIndex);
                // ViewBag.ListName = TabIndex;

                if (TabIndex == "General")
                {
                    return View("_GeneralList", model);
                }
                else if (TabIndex == "Closed")
                {
                    return View("_ClosedList", model);
                }
                else
                {
                    return View("_OpenList", model);
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
        public async Task<IActionResult> GetDealerUndertaking(Guid EmployeeGI, string TabIndex)
        {
            try
            {
                DealerUndertaking model = new DealerUndertaking();
                //model.EffectiveDate = DateTime.Now;
                ViewBag.SliderList = TabIndex;
                model = await _DealerUndertakingService.GetDealerUndertaking(EmployeeGI, TabIndex);
                model.EmployeeHeader = await _commonService.GetEmployeeHeader(EmployeeGI);
                if (model.EffectiveDate == DateTime.MinValue)
                {
                    model.EffectiveDate = DateTime.Now;
                }
                if (TabIndex == "General")
                {
                    return View("DealerUndertakingGeneral", model);
                }
                else if (TabIndex == "Closed")
                {
                    return View("DealerUndertakingClosed", model);
                }
                else
                {
                    return View("DealerUndertakingOpen", model);
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
        public async Task<IActionResult> SaveDealerUndertakingDetails(DealerUndertaking Model)
        {
            var response = await _DealerUndertakingService.SaveDealerUndertakingDetails(Model);
            return Json(response);
        }
        public async Task<ActionResult> GetDealerUndertakingDetails(Guid EmployeeGI,int RequestCode, String TabIndex)
        {
            try
            {
                DealerUndertaking model = new DealerUndertaking();
                model = await _DealerUndertakingService.GetDealerUndertakingDetails(EmployeeGI, RequestCode, TabIndex);
                if (TabIndex == "Open")
                {
                    return PartialView("_Open", model);
                }
                else {
                    return PartialView("_Close", model);
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
