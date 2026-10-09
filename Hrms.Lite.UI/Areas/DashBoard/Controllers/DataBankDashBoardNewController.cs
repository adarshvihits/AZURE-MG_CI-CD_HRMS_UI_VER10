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
    public class DataBankDashBoardNewController : Controller
    {
        private readonly IDataBankDashBoardService _service;
        public DataBankDashBoardNewController(IDataBankDashBoardService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {

            DataBankDashBoardNew model = new DataBankDashBoardNew();
            model = await _service.GetNewDashBoardDetails();
            return View(model);
        }
    }
}
