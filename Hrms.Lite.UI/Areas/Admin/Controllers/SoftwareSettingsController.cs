using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.Admin;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.Admin;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [SessionAuthorize]
    public class SoftwareSettingsController : Controller
    {
		public readonly IOrganizationTaskService _service;
		public readonly ICommonService _commonService;
		public readonly IDropdownService _dropdownService;
		public SoftwareSettingsController(IOrganizationTaskService service, ICommonService commonService, IDropdownService dropdownService)
		{
			_service = service;
			_commonService = commonService;
			_dropdownService = dropdownService;

		}

		public async Task<IActionResult> Index()
		{
			try
			{
				SoftwareSettings model = new SoftwareSettings();
				
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
		public async Task<ActionResult> Save(SoftwareSettings Input)
		{
			try
			{
				var response = await _service.QuerySave(Input);

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
