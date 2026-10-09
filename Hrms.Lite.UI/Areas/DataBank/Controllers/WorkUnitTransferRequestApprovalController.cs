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
    public class WorkUnitTransferRequestApprovalController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly ICommonService _commonService;
        private readonly IWorkUnitTransferRequestApprovalService _service;
        public WorkUnitTransferRequestApprovalController(IDropdownService dropdownService, ICommonService commonService, IWorkUnitTransferRequestApprovalService service)
        {
            _dropdownService = dropdownService;
            _commonService = commonService;
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                WorkUnitTransferRequest model = new WorkUnitTransferRequest();
                model = await _service.GetWorkUnitTransferRequestApprovalList();
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
        public async Task<IActionResult> GetEmployeeWorkUnitDetails(Guid EmployeeGI)
        {
            try
            {
                WorkUnitTransferRequest model = new WorkUnitTransferRequest();
                model = await _service.GetEmployeeWorkUnitDetails(EmployeeGI);
                model.EmployeeHeader = await _commonService.GetEmployeeHeader(EmployeeGI);
                ViewBag.Aut = new SelectList(await _dropdownService.GetRequestAuthorityDropDown(), "Code", "Name");
                return PartialView("_PopUp", model);
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
       

        public async Task<IActionResult> ApproveOrReject(WorkUnitTransferRequest model,string Mode)
        {
            try
            {
                model.Mode = Mode;
                var response = await _service.ApproveOrReject(model);
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
