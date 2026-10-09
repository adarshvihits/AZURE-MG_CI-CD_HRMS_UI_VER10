using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.DataBank.DTO;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.DataBank.Controllers
{
    [Area("DataBank")]
    [SessionAuthorize]
    public class SkillRequisitionController : Controller
    {
        private IDropdownService _dropdownService;
        private readonly ISkillRequisitionService _service;
        private readonly IHelpers _helpers;
        private readonly ICommonService _commonService;


        public SkillRequisitionController(ISkillRequisitionService service, ICommonService commonService, IDropdownService dropdownService, IHelpers Helpers)
        {
            _service = service;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = Helpers;
        }
        public async Task<IActionResult> Index()
        {
            try { 
            SkillRequisition model = new SkillRequisition();
            ViewBag.SkillType = new SelectList(await _dropdownService.GetSkillType_Dropdown(), "Code", "Name");
            ViewBag.SkillProficiency = new SelectList(await _dropdownService.GetSkillProficiencyDropdown(), "Code", "Name");
            model = await _service.GetSkillRequisitionList(0, 0);
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
        public async Task<IActionResult> GetList([DataSourceRequest] DataSourceRequest request, int SkillTypeCode, int SkillProficiencyCode)
        {

            SkillRequisition model = new SkillRequisition();
            List<SkillRequisition> modellist = new List<SkillRequisition>();

            model = await _service.GetSkillRequisitionList(SkillTypeCode, SkillProficiencyCode);
            modellist = model.SkillRequisitionList;

            var dataResult = modellist.ToDataSourceResult(request);

            return Json(dataResult, new JsonSerializerSettings { ContractResolver = new DefaultContractResolver() });



        }
        public async Task<IActionResult> GetRequisitionList(int SkillTypeCode, int SkillProficiencyCode)
        {
            try
            {


                SkillRequisition model = new SkillRequisition();
                model = await _service.GetSkillRequisitionList(SkillTypeCode, SkillProficiencyCode);
                return View("RequisitionList", model);
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
        public async Task<IActionResult> Create(SkillRequisition model)
        {
            try
            {
                var response = await _service.SkillRequisitionSave(model);
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
