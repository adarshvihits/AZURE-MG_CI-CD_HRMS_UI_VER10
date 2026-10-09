using Hrms.Lite.Infrastructure.Helpers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.MIS.Controllers
{
    [Area("MIS")]
    [SessionAuthorize]
    public class DashboardPMSDemoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
