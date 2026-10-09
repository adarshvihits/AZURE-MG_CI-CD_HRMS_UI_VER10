using Hrms.Lite.Infrastructure.Helpers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.Master.Controllers
{
    [Area("Master")]
    [SessionAuthorize]
    public class OfferLetterGreaterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
