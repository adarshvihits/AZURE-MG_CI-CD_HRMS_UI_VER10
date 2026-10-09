using Hrms.Lite.Infrastructure.Helpers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.DashBoard.Controllers
{
    [Area("DashBoard")]
    [SessionAuthorize]
    public class PMSDashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
