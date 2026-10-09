using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.DashBoard;
using Hrms.Lite.Shared.DashBoard;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.DashBoard.Controllers
{
    [Area("DashBoard")]
    [SessionAuthorize]
    public class PayRollDashBoardNewController : Controller
    {
        private readonly IPayRollDashBoardService _service;
        public PayRollDashBoardNewController(IPayRollDashBoardService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            PayRollDashBoardNew model = new PayRollDashBoardNew();
            model = await _service.GetDashBoardDetailsNew();
            return View(model);
        }
    }
}
