using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using Microsoft.AspNetCore.Mvc;

namespace Hrms.Lite.UI.Areas.DataBank.Controllers
{
    [Area("DataBank")]
    [SessionAuthorize]
    public class PositionManagerController : Controller
    {
        private readonly ICommonService _commonService;
        private readonly IPositionManagerService _positionManagerService;

        public PositionManagerController(IPositionManagerService positionManagerService, ICommonService commonService)
        {
            _positionManagerService = positionManagerService;
            _commonService = commonService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                PositionManager model = new PositionManager();
                Filter filter = new Filter();
                filter.length = 10;
                model = await _positionManagerService.GetPositionManagerSummary(filter) ?? new PositionManager();
                model.Filter = await _commonService.GetFilterDropdowns();
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
        public async Task<IActionResult> GetPositionManagerSummary()
        {
            try
            {
                Filter filter = new Filter();
                filter.length = 10;
                PositionManager model = await _positionManagerService.GetPositionManagerSummary(filter) ?? new PositionManager();
                return PartialView("_Summary", model);
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
        public async Task<IActionResult> GetPositionManagerList(PositionManager input)
        {
            try
            {
                Filter filter1 = input.Filter ?? new Filter();
                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }
                filter1.length = 10;
                List<PositionManager> list = await _positionManagerService.GetPositionManagerList(filter1);

                if (list == null || list.Count() == 0)
                {
                    return Json(new
                    {
                        data = "",
                        draw = filter1.draw,
                        recordsTotal = 0,
                        recordsFiltered = 0
                    });
                }

                return Json(new
                {
                    data = list,
                    draw = filter1.draw,
                    recordsTotal = list[0].totalRecords,
                    recordsFiltered = list[0].active
                });
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

        public async Task<IActionResult> PositionDetails(Guid PositionGI)
        {
            try
            {
                PositionManager model = await _positionManagerService.GetPositionManagerDetails(PositionGI) ?? new PositionManager();
                return PartialView("_PositionDetails", model);
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
