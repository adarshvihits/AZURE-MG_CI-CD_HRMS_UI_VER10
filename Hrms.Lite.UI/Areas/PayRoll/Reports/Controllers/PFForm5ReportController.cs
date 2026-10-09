using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.Account;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll.Reports;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.PayRoll.Reports.Controllers
{
    [Area("PayRoll")]
    [SessionAuthorize]
    public class PFForm5ReportController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;

        public PFForm5ReportController(IDropdownService dropdownService, ICommonService commonService, IHelpers helpers)
        {
       
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = helpers;
        }
        public async Task<IActionResult> Index()
        {
           
            PFForm5Report model = new PFForm5Report();
            model.Url = new Url();
            ViewBag.PayRollMonth = new SelectList(await _dropdownService.GetMonthDropdown(), "Code", "Name");
            model.Url = await _commonService.GetUrl();
            
            var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));           
            var UserCode = User.authUser.UserCode;
            var CompanyCode = User.authUser.CompanyID;
            var GroupCompanyCode = User.GroupCompanyCode;
            model.UserCode = UserCode;
            model.CompanyCode = CompanyCode;
            model.GroupCompanyCode = GroupCompanyCode;
            model.filter = new Filter();
            model.filter = await _commonService.GetFilterDropdowns();

            return View("~/Areas/PayRoll/Reports/Views/PFForm5Report/Index.cshtml",model);
        }

   

    }
}
