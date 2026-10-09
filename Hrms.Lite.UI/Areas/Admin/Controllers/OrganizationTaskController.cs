using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.Admin;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.Admin;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.Admin.Controllers
{
	[Area("Admin")]
	[SessionAuthorize]
	public class OrganizationTaskController : Controller
	{
		public readonly IOrganizationTaskService _service;
		public readonly ICommonService _commonService;
		public readonly IDropdownService _dropdownService;
		public OrganizationTaskController(IOrganizationTaskService service, ICommonService commonService, IDropdownService dropdownService)
		{
			_service = service;
			_commonService = commonService;
			_dropdownService = dropdownService;

		}
		public async Task<IActionResult> Index()
		{
			try
			{
				OrganizationTask model = new OrganizationTask();
				model = await _service.GetOrganizationTaskList();
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
			try
			{
				OrganizationTask model = new OrganizationTask();
				ViewBag.Employee = new SelectList(await _dropdownService.GetEmployeeListDropDown(), "Code", "Name");
				model.Mode = "CREATE";
				
				return PartialView("~/Areas/Admin/Views/OrganizationTask/_CreateOrEdit.cshtml", model);
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

		public async Task<IActionResult> GetTaskPopup(int TaskCode,string Task)
		{
			OrganizationTask model = new OrganizationTask();
			model.TaskCode = TaskCode;
			model.Task = Task;
			model = await _service.GetOrganizationTaskDetails(TaskCode);
		
			return View("_TaskPopUp", model);
		}

		[HttpPost]
		public async Task<ActionResult> Save(OrganizationTask Input)
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
		public async Task<ActionResult> Delete(int TaskCode)
		{
			try
			{
				OrganizationTask input = await _service.GetOrganizationTaskDetails(TaskCode);
				input.Mode = "DELETE";
				return PartialView("_Delete", input);
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
		public async Task<ActionResult> TaskDelete(OrganizationTask Input)
		{
			try
			{
				var response = await _service.Delete(Input);

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

		[HttpGet]
		public async Task<ActionResult> TaskUpdate(int TaskCode,int StatusCode)
		{
			try
			{
				var response = await _service.TaskUpdate(TaskCode, StatusCode);

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
