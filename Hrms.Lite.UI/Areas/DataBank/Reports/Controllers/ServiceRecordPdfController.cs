using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.Master.Controllers
{
    [Area("DataBank")]
    [SessionAuthorize]
    public class ServiceRecordPdfController : Controller
    {
        private IDropdownService _dropdownService;
        public ServiceRecordPdfController(IDropdownService dropdownService)
        {
            _dropdownService = dropdownService;
        }
        public IActionResult Index()
        {
           
            return View();
        }
    }
}
