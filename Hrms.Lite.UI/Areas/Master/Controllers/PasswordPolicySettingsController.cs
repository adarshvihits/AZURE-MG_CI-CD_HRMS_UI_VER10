using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hrms.Lite.Services.IServices.Account;
using Hrms.Lite.Shared.Account;
using Microsoft.AspNetCore.Mvc;

namespace Hrms.Lite.UI.Areas.Master.Controllers
{
    [Area("Master")]
    public class PasswordPolicySettingsController : Controller
    {
        private readonly IPasswordSettingsService _PasswordSettingsService;
        public PasswordPolicySettingsController(IPasswordSettingsService PasswordSettingsService)
        {
            _PasswordSettingsService = PasswordSettingsService;
        }
        public async Task<IActionResult> Index()
        {
            PasswordPolicySettings model = new PasswordPolicySettings();
            model =await _PasswordSettingsService.GetPasswordPolicySettings()
;            return View("~/Areas/Master/Views/PasswordPolicySettings/Index.cshtml",model);
        }
        public async Task<IActionResult> Save(PasswordPolicySettings model)
        {
            var response = await _PasswordSettingsService.PasswordPolicySettingsSave(model);
            return Json(response);
        }
    }
}
