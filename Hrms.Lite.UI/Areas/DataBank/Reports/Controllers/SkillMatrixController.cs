using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Services.IServices.DataBank.Reports;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.DataBank.Reports;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hrms.Lite.UI.Areas.DataBank.Reports.Controllers
{
    [Area("DataBank")]
    [SessionAuthorize]
    public class SkillMatrixController : Controller
    {
        private IDropdownService _dropdownService;
        private int i;
        private readonly ISkillMatrixService _employeeProfileService;
        private readonly IHelpers _helpers;

        private readonly ICommonService _commonService;
        private readonly IBlob _blob;
      

        public SkillMatrixController(ISkillMatrixService employeeProfileService, ICommonService commonService, IBlob blob, IDropdownService dropdownService, IHelpers Helpers)
        {
            _employeeProfileService = employeeProfileService;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _blob = blob;
            _helpers = Helpers;
        }
        //[HttpPost]
        public async Task<IActionResult> Index()
        {
            try
            {
                SkillMatrix model = new SkillMatrix();
                model.Filter = await _commonService.GetFilterDropdowns();

                Filter filterdata = new Filter();
                model = await _employeeProfileService.GetSkillMatrix(filterdata);
                
                return View("~/Areas/DataBank/Reports/Views/SkillMatrix/Index.cshtml", model);
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
