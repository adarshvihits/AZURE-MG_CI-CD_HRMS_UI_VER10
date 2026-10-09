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
    public class UserApplicabilityAdvancedUserController : Controller
    {
        public readonly IUserApplicabilityAdvancedUserService _UserApplicabilityAdvancedUserService;
        private readonly IHelpers _helpers;
        public readonly ICommonService _commonService;
        public readonly IDropdownService _dropdownService;
        public UserApplicabilityAdvancedUserController(IUserApplicabilityAdvancedUserService UserApplicabilityAdvancedUserService, IHelpers Helpers, IDropdownService dropdownService, ICommonService commonService)
        {
            _UserApplicabilityAdvancedUserService = UserApplicabilityAdvancedUserService;
            _helpers = Helpers;
            _dropdownService = dropdownService;
            _commonService = commonService;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                UserApplicabilityAdvancedUser model = new UserApplicabilityAdvancedUser();


                ViewBag.ClassificationType = new SelectList(await _dropdownService.GetClassificationTypeDropdown(), "Code", "Name");
                ViewBag.Classification = new SelectList(await _dropdownService.GetClassification_Dropdown(0), "Code", "Name");
                model = await _UserApplicabilityAdvancedUserService.GetList(0, 0);
                return View("~/Areas/Master/Views/UserApplicabilityAdvancedUser/Index.cshtml", model);
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
        public async Task<JsonResult> GetClassificationType(int id)
        {

            var Classification = new SelectList(await _dropdownService.GetClassification_Dropdown(id), "Code", "Name");
            return Json(Classification);

        }
        public async Task<IActionResult> GetList(int GroupingCode, int ClassificationCode)
        {
            UserApplicabilityAdvancedUser model = new UserApplicabilityAdvancedUser();
            model = await _UserApplicabilityAdvancedUserService.GetList(GroupingCode, ClassificationCode);
            return PartialView("_List", model);
        }
        [HttpPost]
        public async Task<ActionResult> Save(int GroupingCode, int ClassificationCode, int UserCode, string Mode)
        {
            try
            {
                UserApplicabilityAdvancedUser model = new UserApplicabilityAdvancedUser();
                model.GroupingCode = GroupingCode;
                model.ClassificationCode = ClassificationCode;
                model.UserCode = UserCode;
                model.Mode = Mode;
                var response = await _UserApplicabilityAdvancedUserService.Save(model);
                return Json(response);
            }
            catch (UnauthorizedAccessException e)
            {

                return RedirectToAction("Error", "Account", new { Area = "" });
            }

        }

        [HttpPost]
        public async Task<ActionResult> BatchSave(UserApplicabilityAdvancedUser Input)
        {
            try
            {
                var response = await _UserApplicabilityAdvancedUserService.BatchSave(Input);

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
