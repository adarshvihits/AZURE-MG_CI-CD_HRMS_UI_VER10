using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared;
using Hrms.Lite.Shared.DataBank;
using Hrms.Lite.Shared.General;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.DataBank.Controllers
{
    [Area("DataBank")]
    [SessionAuthorize]
    public class RequestToHRController : Controller
    {
        private readonly IRequestToHRService _service;
        private readonly ICommonService _commonService;
        private readonly IDropdownService _dropdownService;
        private readonly IHelpers _helpers;
        public RequestToHRController(IRequestToHRService service, ICommonService commonService, IDropdownService dropdownService, IHelpers Helpers)
        {
            _service = service;
            _commonService = commonService;
            _dropdownService = dropdownService;
            _helpers = Helpers;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                RequestToHR model = new RequestToHR();
                Filter filter = new Filter();
                model = await _service.GetList(0);
                model.SliderFilter = await _commonService.GetFilterDropdowns();
                return View(model);
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

        public async Task<IActionResult> GetList(int radiomode)
        {

            try
            {
                RequestToHR model = new RequestToHR();

                model = await _service.GetList(radiomode);

                return PartialView("~/Areas/DataBank/Views/RequestToHR/_RequestToHRList.cshtml", model);
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



        public async Task<IActionResult> GetDetails(Guid EmployeeGI, int RequestCode,int radiomode)
        {
            try
            {
                RequestToHR model = new RequestToHR();
                //model.RequestToHRDocList = new List<File>();
                //File list = new File()
                //{
                //    ActualFileName = "",
                //    FileExtension = "",
                //    AbsoluteUri = "",
                //    FileName = null

                //};
                //model.RequestToHRDocList.Add(list);

                model = await _service.GetRequestToHRDetails(EmployeeGI, RequestCode);
                model.EmployeeHeader = await _commonService.GetEmployeeHeader(EmployeeGI);
                model.radiomode = radiomode;
                return PartialView("_RequestToHRSlider", model);
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
        public async Task<ActionResult> Save(RequestToHR Model)
        {
            //Shared.File file1 = new Shared.File();
            //file1 = input.Attachment;

            var StorageType = await _commonService.GetFileStorageType();


            try
            {             

                for (int i = 0; i < Model.RequestToHRDocList.Count; i++)
                {
                    var attachment = Model.RequestToHRDocList[i];
                    //if (Model.FactorComments[i].Attachment.FileData != null)
                    if (attachment != null && attachment.FileData != null)
                    {
                        if (StorageType.Data == "BLOB")
                        {

                            var fileobj = await _commonService.UploadIformFileToBLOB(Model.RequestToHRDocList[i], "Documents");
                            if (fileobj.Success == false)
                            {
                                fileobj.Message = "FILE UPLOAD ERROR";
                                return Json(fileobj);
                            }
                            Model.RequestToHRDocList[i] = fileobj.Data;

                        }
                        else if (StorageType.Data == "IIS")
                        {
                            var fileobj = await _commonService.UploadIformFileToIIS(Model.RequestToHRDocList[i], "Documents");
                            if (fileobj.Success == false)
                            {
                                fileobj.Message = "FILE UPLOAD ERROR";
                                return Json(fileobj);
                            }
                            Model.RequestToHRDocList[i] = fileobj.Data;

                        }
                        else
                        {
                            var fileobj = await _commonService.UploadIformFileToAWS(Model.RequestToHRDocList[i], "Documents");
                            if (fileobj.Success == false)
                            {
                                fileobj.Message = "FILE UPLOAD ERROR";
                                return Json(fileobj);
                            }
                            Model.RequestToHRDocList[i] = fileobj.Data;

                        }




                    }
                   
                }

                var response = await _service.Create(Model);

                return Json(response);
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
