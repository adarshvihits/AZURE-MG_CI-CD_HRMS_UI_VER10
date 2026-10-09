using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.Account;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.DataBank.Controllers
{
    [Area("DataBank")]
    [SessionAuthorize]
    public class JoiningConfirmationController : Controller
    {
        private readonly ICommonService _commonService;
        private readonly IJoiningConfirmationService  _joiningConfirmationService;
        public JoiningConfirmationController(IJoiningConfirmationService  joiningConfirmationService,ICommonService commonService)
        {
            _joiningConfirmationService = joiningConfirmationService;
            _commonService = commonService;
        }
        public IActionResult Index()
        {            
            try
            {
                return View();
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

        //public IActionResult Create(Guid EnrollGI)
        //{

        //    return View("~/Areas/DataBank/Views/JoiningConfirmation/Create.cshtml");
        //}
        public async Task<IActionResult> Create(Guid EnrollGI)
        {            
            try
            {
                JoiningConfirmation model = new JoiningConfirmation();

                model = await _joiningConfirmationService.GetJoiningCofirmDetails(EnrollGI);
                model.Enrollment.PreEnrollment.EmployeeHeader = await _commonService.GetEmployeeHeader(EnrollGI);
                return View("~/Areas/DataBank/Views/JoiningConfirmation/Create.cshtml", model);
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
        [HttpGet]
        public async Task<JsonResult> confirmJoinExistValidation(Guid EnrollGI)
        {


            Login CurrentUser = JsonConvert.DeserializeObject<Hrms.Lite.Shared.Account.Login>(HttpContext.Session.GetString("User"));
            JoiningConfirmation model = new JoiningConfirmation();
            model.Enrollment = new Enrollment();
            //model.Enrollment.EnrollGUID = EnrollGI;
            //model.Enrollment.PreEnrollment.PermanentAddress.MobileNo = Mobile;
            //model.Enrollment.PreEnrollment.BasicInfo.AdharNo = Aadhar;
            //model.Enrollment.PreEnrollment.BasicInfo.PanNo = Pan;
            model.Enrollment.EnrollGUID = EnrollGI;

            var result = await _joiningConfirmationService.confirmJoinExistValidation(EnrollGI);

            return Json(result);
        }

        public async Task<IActionResult> GetDuplicationList(string DocumentNum,int Type)
        {
            try
            {
                EmployeeHeader model = new EmployeeHeader();
                model.TypeCode = Type;
                model = await _joiningConfirmationService.GetDuplicationDetails(DocumentNum, model.TypeCode);
                model.TypeCode = Type;
                return PartialView("_DuplicationList", model);
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



        [HttpPost]
        public async Task<ActionResult> ConfirmJoin(JoiningConfirmation model)
        {
            

            Login CurrentUser = JsonConvert.DeserializeObject<Hrms.Lite.Shared.Account.Login>(HttpContext.Session.GetString("User"));
          
            if (CurrentUser.CompanyCode.ToUpper() == "XLA")
            {
                model.company = "XLA";
            }
                var response = await _joiningConfirmationService.ConfirmJoin(model);
           
            return Json(response);
            }


        [HttpPost]
        public async Task<ActionResult> RejectJoin(JoiningConfirmation model)
        {

            Login CurrentUser = JsonConvert.DeserializeObject<Hrms.Lite.Shared.Account.Login>(HttpContext.Session.GetString("User"));
          
            if (CurrentUser.CompanyCode.ToUpper() == "XLA")
            {
                model.company = "XLA";
            }
            var response = await _joiningConfirmationService.RejectJoin(model);
            return Json(response);
        }







    }
}
