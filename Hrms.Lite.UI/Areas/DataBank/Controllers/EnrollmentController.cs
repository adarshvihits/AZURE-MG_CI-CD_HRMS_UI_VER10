using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.DataBank.Generic;
using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.DataBank.Controllers
{
    [Area("DataBank")]
    public class EnrollmentController : Controller
    {
        private readonly IEnrollmentService _enrollmentService;
        private readonly IDropdownService _dropdownService;
        private readonly ICommonService _commonService;
        private readonly IWebHostEnvironment _environment;
        private readonly IBlob _blob;
        public EnrollmentController(IEnrollmentService enrollmentService, IDropdownService dropdownService, ICommonService commonService, IWebHostEnvironment environment, IBlob blob)
        {
            _enrollmentService = enrollmentService;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _environment = environment ?? throw new ArgumentException(nameof(environment));
            _blob = blob;
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
        public async Task<JsonResult> AlreadyExistCheck(Guid EmployeeGI, string Mobile, string PAN, string Aadhar)
        {
            var result = await _enrollmentService.AlreadyExistCheck(EmployeeGI, Mobile, PAN, Aadhar,"","","","","","");
            return Json(result);
        }

        public async Task<IActionResult> GetCompensationGrid(int GradeCode)
        {
            Enrollment model = new Enrollment();
            var CompensationGrid = await _enrollmentService.GetComponsationGrid(GradeCode);
            model.PreEnrollment = new PreEnrollment();
            model.PreEnrollment.Compensation = CompensationGrid;
            return PartialView("_CompensationAndBenefits",model);
        }        
    }
}
