using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.DataBank;
using Hrms.Lite.Shared.General;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.DataBank.Controllers
{
    [Area("DataBank")]
    [SessionAuthorize]
    public class InstantMessagingController : Controller
    {
        private readonly IInstantMessagingService _service;
        private readonly ICommonService _commonService;

        public InstantMessagingController(IInstantMessagingService instantMessagingService, ICommonService commonService)
        {
            _service = instantMessagingService;
            _commonService = commonService;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                InstantMessaging model = new InstantMessaging();
                model = await _service.GetMessageList();
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
        public async Task<IActionResult> CreateOrEdit(string Mode, int MessageCode)
        {
            try
            {
                InstantMessaging model = new InstantMessaging();
                model = await _service.GetMessageDetails(MessageCode);
                model.Mode = Mode;
                return PartialView("~/Areas/DataBank/Views/InstantMessaging/_CreatePopup.cshtml", model);
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
        public async Task<ActionResult> Save(string Mode, InstantMessaging model)
        {
            try
            {
                var response = await _service.Message_Save(model);
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
        public async Task<IActionResult> AddNewEmployee(string Mode, int MessageCode,Filter filter)
        {
            try
            {
                InstantMessaging model = new InstantMessaging();
                filter.length = 10;
                model = await _service.GetEmployeesList(filter,MessageCode);
                model.Mode = Mode;
                model.filter = await _commonService.GetFilterDropdowns();
                return View("~/Areas/DataBank/Views/InstantMessaging/AddEmployees.cshtml", model);
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
        public async Task<ActionResult> EmployeeSave(InstantMessaging model)
        {
            try
            {
                var response = await _service.Employee_Save(model);
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
        public async Task<IActionResult> ApplyFilter(Filter filter, int MessageCode)
        {
            try
            {
                InstantMessaging model = new InstantMessaging();
                model = await _service.GetEmployeesList( filter,MessageCode);
                return PartialView("~/Areas/DataBank/Views/InstantMessaging/_EmployeeList.cshtml", model);
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
        public async Task<ActionResult> Send(int MessageCode)
        {
            try
            {
                var response = await _service.Message_Send(MessageCode);
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
