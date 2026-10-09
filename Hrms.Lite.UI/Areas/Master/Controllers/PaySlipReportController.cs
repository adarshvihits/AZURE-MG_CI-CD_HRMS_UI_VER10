
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.DataBank.Reports;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using Hrms.Lite.Services.Services.General;

namespace Hrms.Lite.UI.Areas.Master.Controllers
{
    [Area("Master")]
    public class PaySlipReportController : Controller
    {
        private IDropdownService _dropdownService;

 
        public PaySlipReportController( IDropdownService dropdownService)
        {
            _dropdownService = dropdownService;
        }

            //[HttpPost]
            public async Task<IActionResult> Index()
        {
            ViewBag.Loc = new SelectList(await _dropdownService.GetEmployeeDropDown(), "Code", "Name");//GetLocationDropdown(), "Code", "Name");
           // ViewBag.Loc = new SelectList(await _dropdownService.GetLocationDropdown(), "Code", "Name");
            return View();
        }

    }
}
