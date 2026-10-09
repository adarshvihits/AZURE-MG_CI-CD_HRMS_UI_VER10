using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.TrainingModule;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.TrainingModule;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.TrainingModule.Controllers
{
    [Area("TrainingModule")]
    [SessionAuthorize]
    public class TrainingReplyGroupController : Controller
    {
        public readonly ITrainingReplyGroupServices _service;
        public readonly ICommonService _commonService;
        private readonly IDropdownService _dropdownService;
        public TrainingReplyGroupController(IDropdownService dropdownService, ITrainingReplyGroupServices service, ICommonService commonService)
        {
            _service = service;
            _commonService = commonService;
            _dropdownService = dropdownService;
        }
        public async Task<ActionResult> Index()
        {
            try
            {
                TrainingReplyGroup model = new TrainingReplyGroup();
                model = await _service.GetList("ACTIVE");
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
        public async Task<ActionResult> GetReplaygroupList(string TabIndex)
        {
            try
            {
                if (TabIndex == "APPROVAL_PENDING")
                    return PartialView("_PendingList", await _service.GetList(TabIndex));
                else
                    return PartialView("_ActiveList", await _service.GetList(TabIndex));
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
        public IActionResult Create()
        {
            try
            {
                TrainingReplyGroup model = new TrainingReplyGroup();
                model.Mode = "CREATE";
                model.Active = true;
                model.Type = 1;
                string master = "Replaygroup";

                return PartialView("~/Areas/TrainingModule/Views/TrainingReplyGroup/_ReplyGroupSave.cshtml", model);
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
        public async Task<ActionResult> Save(TrainingReplyGroup input)
        {
            try
            {
                var response = await _service.Create(input);

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
        public async Task<ActionResult> Edit(Guid ReplyGroupGI, Guid LogGI)
        {
            try
            {
                TrainingReplyGroup model = new TrainingReplyGroup();
                model.Type = 1;
                model = await _service.GetReplygroupDetails(ReplyGroupGI, LogGI, "MODIFY");
                model.Mode = "MODIFY";
                return PartialView("~/Areas/TrainingModule/Views/TrainingReplyGroup/_ReplyGroupSave.cshtml", model);
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
        public async Task<ActionResult> Delete(Guid ReplyGroupGI, Guid LogGI)
        {
            try
            {
                TrainingReplyGroup model = await _service.GetReplygroupDetails(ReplyGroupGI, LogGI, "DELETE");
                //model.Mode = "Delete";
                return PartialView("~/Areas/TrainingModule/Views/TrainingReplyGroup/_Delete.cshtml", model);
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
        public async Task<ActionResult> Delete(TrainingReplyGroup model)
        {
            try
            {
                model.Type = 1;
                var response = await _service.Delete(model);
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
        public async Task<ActionResult> Approve(Guid ReplyGroupGI, Guid LogGI)
        {
            try
            {
                TrainingReplyGroup model = await _service.GetReplygroupDetails(ReplyGroupGI, LogGI, "APPROVAL");
                //model.Mode = "Approval";
                return PartialView("~/Areas/TrainingModule/Views/TrainingReplyGroup/_Approve.cshtml", model);
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
        public async Task<ActionResult> Approve(TrainingReplyGroup model)
        {
            try
            {
                model.ApproveMode = "Approve";
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
        [HttpPost]
        public async Task<ActionResult> Reject(TrainingReplyGroup model)
        {
            try
            {
                model.ApproveMode = "Reject";
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
