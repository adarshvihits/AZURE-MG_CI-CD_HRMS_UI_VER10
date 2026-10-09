using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.PayRoll;
using Hrms.Lite.Shared.PayRoll;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.PayRoll.Controllers
{
    [Area("PayRoll")]
    [SessionAuthorize]
    public class LOPSettingsController : Controller
    {
        public readonly ILOPSettingsService _service;
        public LOPSettingsController(ILOPSettingsService service)
        {
            _service = service;
         
        }
        public async Task<IActionResult> Index()
        {
            try { 
            LOPSettings model = new LOPSettings();
            model = await _service.GetLOPSettingsApplicableAllowanceList();
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

        [HttpPost]

        public async Task<ActionResult> SaveLOPSettings(LOPSettings input)
        {
            try { 
            var list = (from li in input.LOPSettingsList
                            where li.LopStatus == true
                            select li)
                            .Union(input.LOPSettingsList.Where(li => li.EsiLeaveStatus == true))
                            .Union(input.LOPSettingsList.Where(li => li.SuspensionStatus == true))
                            .Union(input.LOPSettingsList.Where(li => li.LateHourStatus == true))
                            .Union(input.LOPSettingsList.Where(li => li.LayOffStatus == true))
                            .Union(input.LOPSettingsList.Where(li => li.MaternityLeaveStatus == true));

                input.LOPSettingsList = list.ToList();


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


    }
}
