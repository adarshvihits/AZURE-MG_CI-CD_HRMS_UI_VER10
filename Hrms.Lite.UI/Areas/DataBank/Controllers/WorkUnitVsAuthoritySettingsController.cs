using Microsoft.AspNetCore.Mvc;
using Hrms.Lite.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Shared.DataBank;
using Microsoft.AspNetCore.Mvc.Rendering;
using Hrms.Lite.Services.IServices.General;

namespace Hrms.Lite.UI.Areas.DataBank.Controllers
{
    [Area("DataBank")]
    [SessionAuthorize]
    public class WorkUnitVsAuthoritySettingsController : Controller
    {
        public readonly IWorkUnitVsAuthoritySettingsService _service;
        private readonly IDropdownService _dropdownService;

        public WorkUnitVsAuthoritySettingsController(IWorkUnitVsAuthoritySettingsService service, IDropdownService dropdownService)
        {
            _service = service;
            _dropdownService = dropdownService;

        }
        public async Task<IActionResult> Index()
        {
            try
            {
                WorkUnitVsAuthoritySettings model = new WorkUnitVsAuthoritySettings();
                model = await _service.GetList();
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
        public async Task<IActionResult> WorkUnit(int WorkunitCode,int RequestTypeCode)
        {
            try
            {
                WorkUnitVsAuthoritySettings model = new WorkUnitVsAuthoritySettings();
                model = await _service.GetUnitVsAuthDetails(WorkunitCode, RequestTypeCode);
                model.flag = 0;
                return PartialView("~/Areas/DataBank/Views/WorkUnitVsAuthoritySettings/WorkUnitAuthority.cshtml", model);
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
        public async Task<IActionResult> GetAuthorityDetails(int WorkunitCode,int RequestTypeCode, WorkUnitVsAuthoritySettings savedData, int flag)
        {
            try
            {
                WorkUnitVsAuthoritySettings model = new WorkUnitVsAuthoritySettings();
                ViewBag.Auth = new SelectList(await _dropdownService.GetRequestAuthorityDropDown(), "Code", "Name");
                ViewBag.ReqType = new SelectList(await _dropdownService.GetReqDropDown(), "Code", "Name");
                if (flag == 1)
                { 
                    List<WorkUnitVsAuthority> m = new List<WorkUnitVsAuthority>();
                    if (savedData.WorkUnitVsAuthority != null)
                    {
                        for (int j = 0; j < savedData.WorkUnitVsAuthority.Count; j++)
                        {
                            if (savedData.WorkUnitVsAuthority[j].RequestTypeCode == RequestTypeCode)
                            {
                                m.Add(savedData.WorkUnitVsAuthority[j]);
                            }
                        }
                    }

                    model.WorkUnitAuth = m;
                }
                else {
                    
                    model = await _service.GetUnitVsAuthDetails(WorkunitCode, RequestTypeCode);
                    if (model.WorkUnitVsAuthority == null || model.WorkUnitVsAuthority.Count == 0)
                    {
                        model.WorkUnitVsAuthority = new List<WorkUnitVsAuthority>();
                        model.WorkUnitAuth = new List<WorkUnitVsAuthority>();
                        WorkUnitVsAuthority list = new WorkUnitVsAuthority
                        {
                            LevelCode = 1,
                            LevelName = "Level 1"
                        };
                        model.WorkUnitVsAuthority.Add(list);
                        model.WorkUnitAuth.Add(list);
                    }
                    
                }
                model.RequestType = new Shared.General.Dropdown();
                model.RequestType.Code = RequestTypeCode;

                return PartialView("_RequestVsAuthorityPopUp", model);
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
        public async Task<IActionResult> AddToList(WorkUnitVsAuthoritySettings popupData, WorkUnitVsAuthoritySettings savedData, int ReqCode)
        {
            WorkUnitVsAuthoritySettings model = new WorkUnitVsAuthoritySettings();
            List<WorkUnitVsAuthority> m = new List<WorkUnitVsAuthority>();
            if (savedData.WorkUnitVsAuthority != null)
            {
                for (int j = 0; j < savedData.WorkUnitVsAuthority.Count; j++)
                {
                    if (savedData.WorkUnitVsAuthority[j].RequestTypeCode != ReqCode)
                    {
                        m.Add(savedData.WorkUnitVsAuthority[j]);
                    }
                }
            }

            for (int i = 0; i < popupData.WorkUnitAuth.Count; i++)
            {
                if (popupData.WorkUnitAuth[i].AuthorityCode != 0 )
                { 
                    m.Add(popupData.WorkUnitAuth[i]);
                }
            }
            model.WorkUnitVsAuthority = m;
            model.flag = 1;
            return PartialView("_RequestVsAuthorityList", model);
        }
        public async Task<IActionResult> GetPopupList(int WorkunitCode, int RequestTypeCode, WorkUnitVsAuthoritySettings savedData, int flag)
        {
            try
            {
                WorkUnitVsAuthoritySettings model = new WorkUnitVsAuthoritySettings();
                ViewBag.Auth = new SelectList(await _dropdownService.GetRequestAuthorityDropDown(), "Code", "Name");
                if (flag == 1)
                {
                    List<WorkUnitVsAuthority> m = new List<WorkUnitVsAuthority>();
                    if (savedData.WorkUnitVsAuthority != null)
                    {
                        for (int j = 0; j < savedData.WorkUnitVsAuthority.Count; j++)
                        {
                            if (savedData.WorkUnitVsAuthority[j].RequestTypeCode == RequestTypeCode)
                            {
                                m.Add(savedData.WorkUnitVsAuthority[j]);
                            }
                        }
                    }

                    model.WorkUnitAuth = m;
                }
                else
                {
                    model = await _service.GetUnitVsAuthDetails(WorkunitCode, RequestTypeCode);

                    if (model.WorkUnitVsAuthority == null || model.WorkUnitVsAuthority.Count == 0)
                    {
                        model.WorkUnitVsAuthority = new List<WorkUnitVsAuthority>();
                        model.WorkUnitAuth = new List<WorkUnitVsAuthority>();
                        WorkUnitVsAuthority list = new WorkUnitVsAuthority
                        {
                            LevelCode = 1,
                            LevelName = "Level 1",
                            RequestTypeCode = RequestTypeCode
                        };
                        model.WorkUnitVsAuthority.Add(list);
                        model.WorkUnitAuth.Add(list);
                    }

                }

                return PartialView("~/Areas/DataBank/Views/WorkUnitVsAuthoritySettings/_PopupList.cshtml", model);
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
        public async Task<ActionResult> Save(WorkUnitVsAuthoritySettings input)
        {
            try
            {
                var response = await _service.Save(input);
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
        public async Task<IActionResult> GetApplicableShiftPopup(int WorkUnitCode)
        {
            try
            {
                WorkUnitVsAuthoritySettings model = new WorkUnitVsAuthoritySettings();
                model = await _service.GetApplicableShift(WorkUnitCode);
                return PartialView("~/Areas/DataBank/Views/WorkUnitVsAuthoritySettings/_ApplicableShiftPopup.cshtml", model);
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
        public async Task<ActionResult> ApplicableShiftSave(WorkUnitVsAuthoritySettings input)
        {
            try
            {
                var response = await _service.ApplicableShiftSave(input);
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
