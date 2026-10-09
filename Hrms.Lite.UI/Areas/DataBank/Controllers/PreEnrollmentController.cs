using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.Azure.Storage;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.DataBank.Controllers
{
    [Area("DataBank")]
    public class PreEnrollmentController : Controller
    {

        #region private
        private readonly IPreEnrollmentService _preenrollmentService;
        private readonly IDropdownService _dropdownService;
        private readonly ICommonService _commonService;
        private readonly IWebHostEnvironment _environment;
        private readonly IBlob _blob;
        #endregion

        #region Constructor
        public PreEnrollmentController(IPreEnrollmentService preenrollmentService, IDropdownService dropdownService, IWebHostEnvironment environment,IBlob blob, ICommonService commonService)
        {
            _preenrollmentService = preenrollmentService;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _environment = environment ?? throw new ArgumentException(nameof(environment));
            _blob = blob;
        }
        #endregion

        #region get or post requests
     
        public async Task<IActionResult> GetCompensationGrid(int GradeCode)
        {
            PreEnrollment model = new PreEnrollment();
            var ComponsationGrid = await _preenrollmentService.GetCompensationGrid(GradeCode);
            model.Compensation = ComponsationGrid;
            return PartialView("_Compensation", model);
        }


 
        [HttpGet]
        public async Task<JsonResult> GetState(int id)
        {
            var State = new SelectList(await _dropdownService.GetStateDropDown(id), "Code", "Name");
            return Json(State);
        }
        [HttpGet]
        public async Task<JsonResult> GetDistrict(int id)
        {
            var District = new SelectList(await _dropdownService.GetDistrictDropdown(id), "Code", "Name");
            return Json(District);
        }
        [HttpGet]
        public async Task<JsonResult> AlreadyExistCheck(string Mobile, string PAN, string Aadhar)
        {
            var result = await _preenrollmentService.AlreadyExistCheck(Mobile, PAN, Aadhar);
            return Json(result);
        }
        #endregion
    }
}
