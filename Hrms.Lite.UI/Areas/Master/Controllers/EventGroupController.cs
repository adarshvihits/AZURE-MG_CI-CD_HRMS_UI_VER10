using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.Master;
using Hrms.Lite.Shared.General;
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
    public class EventGroupController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly IEventGroupService _service;
        public EventGroupController(IEventGroupService service, IDropdownService dropdownService)
        {
            _service = service;
            _dropdownService = dropdownService;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                EventGroup model = new EventGroup();
            model = await _service.GetList(0);
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
        public async Task<IActionResult> GetList(int TabIndex)
        {
            EventGroup model = new EventGroup();
            model = await _service.GetList(TabIndex);
            return PartialView("_List", model);
        }
        public async Task<IActionResult> Create()
        {
            EventGroup model = new EventGroup();
            ViewBag.Event = new SelectList(await _dropdownService.GetEventDropDown(), "Code", "Name");
            ViewBag.MultipleCombination = new SelectList(await _dropdownService.GetEventGroupCombinationDropDown(), "Code", "Name");
            model.Mode = "Create";
            model.IsMultiCombination = 0;
            model.Active = true;
            return View(model);
        }
        public async Task<IActionResult> GetCombinationDetails(int id, int type, string combinationName)
        {
            try
            {
                ViewBag.CombinationName = combinationName;
                if (type == 0)
                {
                    EventGroup model = new EventGroup();
                    if (id != 0)
                    {
                        model = await _service.GetCombinationDetails(id);
                    }
                    return PartialView("_CombinationList1", model);
                }
                else
                {
                    EventGroup model1 = new EventGroup();
                    if (id != 0)
                    {
                        model1 = await _service.GetCombinationDetails(id);
                        model1.Combinations2 = model1.Combinations1;
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
        public async Task<IActionResult> GetEmpList()
        {
            EventGroup model = new EventGroup();
            return PartialView("_RecipientEmpPopup", model);    
        }

        [HttpPost]
        public async Task<IActionResult> GetEmpList([FromBody] EventGroup _model)
        {
            try
            {
                EventGroup model = new EventGroup();
                Filter filter1 = _model.filter;
                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }
                model = await _service.EmployeeList(_model.Mode, _model.EventGroupCode, filter1);
                
                    if (_model.EmpList != null && _model.EmpList.Count != 0)
                    {
                        for (int i = 0; i < model.EmpList.Count; i++)
                        {
                            for (int j = 0; j < _model.EmpList.Count; j++)
                            {
                                if (model.EmpList[i].EmployeeCode == _model.EmpList[j].EmployeeCode)
                                {
                                    model.EmpList[i].Applicable = _model.EmpList[j].Applicable;
                                }
                            }
                        }
                    }
                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.EmpList == null || model.EmpList.Count() == 0)
                {
                    return Json(new
                    {
                        data = "",
                        draw = filter1.draw,
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows
                    });
                }
                if (model.EmpList.Count > 0)
                {
                    totalrows = model.EmpList[0].active;
                }

                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = model.EmpList[0].totalRecords;
                    return Json(new
                    {
                        data = model.EmpList,
                        draw = filter1.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows,//totalrowsafterfiltering
                    });
                }
                else
                {
                    return Json(new
                    {
                        data = model.EmpList,
                        draw = filter1.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows,
                    });
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
        public async Task<IActionResult> Save([FromBody] EventGroup model)
        {
            try
            {
                var response = await _service.Save(model);
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
        public async Task<IActionResult> Edit(int Code)
        {
            EventGroup model = new EventGroup();
            ViewBag.Event = new SelectList(await _dropdownService.GetEventDropDown(), "Code", "Name");
            ViewBag.MultipleCombination = new SelectList(await _dropdownService.GetEventGroupCombinationDropDown(), "Code", "Name");
            model = await _service.GetEventGroupDetails(Code);
            model.Mode = "Modify";
            if (model.Combinations1[0].EmployeeID != "")
            {
                ViewBag.CombinationName = "Employee";
            }
            return View("Create", model);
        }
        public async Task<ActionResult> Delete(int Code)
        {
            try
            {
                EventGroup model = new EventGroup();
                var response = await _service.Delete(Code);
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