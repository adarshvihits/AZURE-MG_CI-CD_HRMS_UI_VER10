using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.PayRoll;
using Hrms.Lite.Shared.PayRoll;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Org.BouncyCastle.Tls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.PayRoll.Controllers
{
    [Area("PayRoll")]
    [SessionAuthorize]
    public class SalaryReleaseRepostController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly ISalaryReleaseRepostService _RepostService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        public SalaryReleaseRepostController(ISalaryReleaseRepostService SalaryReleaseRepostService, IDropdownService dropdownService, ICommonService commonService, IHelpers Helpers)
        {
            _RepostService = SalaryReleaseRepostService;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = Helpers;

        }
        public async Task<IActionResult> Index()
        {
            try
            {
                SalaryReleaseRepost model = new SalaryReleaseRepost();
                model = await _RepostService.GetList(0, 0, 0);
                ViewBag.Period = new SelectList(await _dropdownService.GetBankPostedMonthDropdown("FF"), "Code", "Name");
                ViewBag.ReportFormat = new SelectList(await _dropdownService.GetBnkStatemntRptFormatDropdown(), "Code", "Name");
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
        public async Task<IActionResult> GetList(int Month, int TabIndex,int ReportFormat)
        {
            try
            {
                SalaryReleaseRepost model = new SalaryReleaseRepost();
                model = await _RepostService.GetList(Month, TabIndex, ReportFormat);
                if (TabIndex == 0)
                {
                    return PartialView("_RepostingList", model);
                }
                else //if (TabIndex == 1)
                {
                    return PartialView("_ApprovalList", model);
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
        public async Task<IActionResult> RepostingSave(int Month, String FileName,int ReportFormat, int PostedUser, Guid GroupCompanyGI,int HdCode)
        {
            try
            {
                SalaryReleaseRepost model = new SalaryReleaseRepost();
                model.PayrollMonth = new Shared.General.Dropdown();
                model.GroupCompany = new Shared.Master.GroupCompany();
                model.PayrollMonth.Code = Month;
                model.FileName = FileName;
                model.ReportFormat = ReportFormat;
                model.PostedUserCode = PostedUser;
                model.GroupCompany.GI = GroupCompanyGI;
                model.HdCode = HdCode;
                var response = await _RepostService.Save(model);
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
        public async Task<IActionResult> RepostingApproval(int Month, int ReportFormat, Guid GroupCompanyGI,int HdCode)
        {
            try
            {
                SalaryReleaseRepost model = new SalaryReleaseRepost();
                model.PayrollMonth = new Shared.General.Dropdown();
                model.GroupCompany = new Shared.Master.GroupCompany();
                model.PayrollMonth.Code = Month;
                model.ReportFormat = ReportFormat;
                model.HdCode = HdCode;
                model.GroupCompany.GI = GroupCompanyGI;
                var response = await _RepostService.RepostingApproval(model);
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
