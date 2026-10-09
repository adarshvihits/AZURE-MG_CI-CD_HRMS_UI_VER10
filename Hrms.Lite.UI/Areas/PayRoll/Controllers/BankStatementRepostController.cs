using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.PayRoll;
using Hrms.Lite.Shared.PayRoll;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.PayRoll.Controllers
{
    [Area("PayRoll")]
    [SessionAuthorize]
    public class BankStatementRepostController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly IBankStatementRepostService _RepostService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        public BankStatementRepostController(IBankStatementRepostService BankStatementRepostService, IDropdownService dropdownService, ICommonService commonService, IHelpers Helpers)
        {
            _RepostService = BankStatementRepostService;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = Helpers;

        }
        public async Task<IActionResult> Index()
        {
            BankStatementRepost model = new BankStatementRepost();
            ViewBag.Period = new SelectList(await _dropdownService.GetBankPostedMonthDropdown(""), "Code", "Name");
    
            return View(model);
        }
        public async Task<IActionResult> GetList(int Month, int TabIndex)
        {
            try
            {
                BankStatementRepost model = new BankStatementRepost();
                model = await _RepostService.GetList(Month, TabIndex);
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
        public async Task<IActionResult> RepostingSave(int Month,String FileName,int PostedUser,Guid GroupCompanyGI)
        {
            try
            {
                BankStatementRepost model = new BankStatementRepost();
                model.PayrollMonth = new Shared.General.Dropdown();
                model.GroupCompany = new Shared.Master.GroupCompany();
                model.PayrollMonth.Code = Month;
                model.FileName = FileName;
                model.PostedUserCode = PostedUser;
                model.GroupCompany.GI = GroupCompanyGI;
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
        public async Task<IActionResult> RepostingApproval(int Month, Guid GroupCompanyGI)
        {
            try
            {
                BankStatementRepost model = new BankStatementRepost();
                model.PayrollMonth = new Shared.General.Dropdown();
                model.GroupCompany = new Shared.Master.GroupCompany();
                model.PayrollMonth.Code = Month;
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
