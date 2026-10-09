using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.DataBank.DTO;
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
    public class PreEnrollmentEditApprovalController : Controller
    {

        #region private
        private readonly IPreEnrollmentEditService _service;
        private readonly ICommonService _commonService;

        #endregion


        #region Constructor
        public PreEnrollmentEditApprovalController(IPreEnrollmentEditService service, ICommonService common)
        {
            _service = service;
            _commonService = common;
        }
            
        #endregion

        public async Task<IActionResult> Index()
        {

            try
            {
                OnBoarding model = new OnBoarding();
                model = await _service.GetPreEnrollEditApprovalList( new Filter());
                model.Filter = await _commonService.GetFilterDropdowns();
                model.Filter.Tabindex = 100;
                return View("Index", model);
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
        public async Task<IActionResult> Approve(int LogCode)
        {
            try
            {
                PreEnrollment model = new PreEnrollment();
                model = await _service.GetPreEnrollmentEditApprovalDetails(LogCode);
                return View("Approve", model);
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
            public async Task<ActionResult> ApproveReject(int LogCode, string mode)
            {
                try
                {
                    
                    PreEnrollment model = new PreEnrollment();
                    model.Mode = mode;
                    model.LogCode = LogCode;
                    var response = await _service.ApproveReject(model);
                    if (response.Message == "APPROVED")
                    {
                        response.Message = "Approved Successfully";
                    }
                    else if (response.Message == "REJECTED")
                    {
                        response.Message = "Rejected Successfully";
                    }
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
        public async Task<IActionResult> ApplyFilter(Filter filter)
        {
            try
            {

                OnBoarding model = new OnBoarding();               
                model = await _service.GetPreEnrollEditApprovalList( filter);    
               
                model.TabIndex = filter.Tabindex;                
                return View("_List", model);
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
