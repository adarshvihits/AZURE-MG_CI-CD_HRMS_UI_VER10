using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Services.IServices.Recruitment;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.Account;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.Master;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Drawing.Imaging;
using Hrms.Lite.Shared.DataBank.Generic;
using Hrms.Lite.Shared.General;
using SkiaSharp;
using System.IO.Compression;

namespace Hrms.Lite.UI.Areas.DataBank.Controllers
{
    [Area("DataBank")]
    [SessionAuthorize]
    public class PreEnrollmentEditController : Controller
    {

        #region private
        private readonly IPreEnrollmentEditService _service;
        private readonly IOfferProcessingService _offer;
        private readonly IDropdownService _dropdownService;
        private readonly ICommonService _commonService;
        private readonly IWebHostEnvironment _env;
        #endregion


        #region Constructor
        public PreEnrollmentEditController( IDropdownService dropdownService, IWebHostEnvironment env,
            IPreEnrollmentEditService service, ICommonService commonService,IOfferProcessingService offer)
        {
            
            _dropdownService = dropdownService;
            _commonService = commonService;
            _service = service;
            _env = env;
            _offer = offer;
        }
        #endregion

        public async Task<IActionResult> Index()
        {

            try
            {
                OnBoarding model = new OnBoarding();               
                model = await _service.GetOnBoardingList(3, new Filter());               
                model.Filter = await _commonService.GetFilterDropdowns();
                model.Filter.Tabindex = 3;
                return View("Index", model);
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
        public async Task<IActionResult> Edit(Guid PreEnrollGI)
        {
            try
            {
                await fillDropdown();
                ViewBag.St = new SelectList(await _dropdownService.GetStateDropDown(0), "Code", "Name");
                ViewBag.Dist = new SelectList(await _dropdownService.GetDistrictDropdown(0), "Code", "Name");
                ViewBag.Pay = new SelectList(await _dropdownService.GetGCVsPayrollGroup(0), "Code", "Name");
                ViewBag.SlryStruct = new SelectList(await _dropdownService.GetWageGradeVsSalaryBand(0), "Code", "Name");

                var response = await _commonService.CheckEmptyDropDown("PreEnroll");

                if (response.Success is false)   //If Any Empty DropDown
                {
                    TempData["Message"] = response.Message;
                    return RedirectToAction("Index", "OnBoarding");
                }
                else
                {
                   
                    PreEnrollment model_ = await _commonService.GetMandatoryFiledValidation("PREENROLL");
                    Enrollment enroll = await _service.GetPreEnrollmentDetails(PreEnrollGI);
                    PreEnrollment model = new PreEnrollment();
                    model.FiledValidations = model_.FiledValidations;
                    model = enroll.PreEnrollment;

                    ViewBag.SlryStruct = new SelectList(await _dropdownService.GetWageGradeVsSalaryBand((int)model.JobDetails.WageGrade.Code), "Code", "Name");
                    model.JobDetails.NoticePeriod = await _commonService.GetNoticePeriod((int)model.JobDetails.Designation.Code, null);
                                    

                    if (model.BasicInfo.File.FileName != null && model.BasicInfo.File.FileName!="")
                    {
                        ViewBag.FileName = model.BasicInfo.File.FileName;
                        var ext = Path.GetExtension(ViewBag.FileName);

                        string dataUri = $"data:image/{ext.Substring(1)};base64,{Convert.ToBase64String(model.BasicInfo.File.fdata)}";
                        ViewBag.dataUri = dataUri;
                    }
                    return View("Edit", model);
                }
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
        #region Profile pic 
        public IActionResult SaveProfileToFolder(Enrollment model)
        {

            string path = Path.Combine(this._env.WebRootPath, "Personal_Profile");

            string fileName = model.PreEnrollment.BasicInfo.File.FileData.FileName;

            var Imgstream = model.PreEnrollment.BasicInfo.File.FileData.OpenReadStream();
            System.Drawing.Image originalImage = System.Drawing.Image.FromStream(Imgstream);
            if (model.PreEnrollment.BasicInfo.File.FileData.Length > (1024 * 1024) || (originalImage.Width > 1920 && originalImage.Height > 1080))
            {
                //----------------compress image --------------------------------------

                // Set the maximum width and height for the compressed image
                int maxWidth = 800;
                int maxHeight = 600;

                // Calculate the new size while maintaining the aspect ratio
                int newWidth, newHeight;
                if (originalImage.Width > originalImage.Height)
                {
                    newWidth = maxWidth;
                    newHeight = (int)((double)originalImage.Height / originalImage.Width * maxWidth);
                }
                else
                {
                    newHeight = maxHeight;
                    newWidth = (int)((double)originalImage.Width / originalImage.Height * maxHeight);
                }
                var stream2 = model.PreEnrollment.BasicInfo.File.FileData.OpenReadStream();
                System.Drawing.Image resized = resizeImage(newWidth, newHeight, stream2);
                using var imageStream = new MemoryStream();
                resized.Save(imageStream, ImageFormat.Jpeg);
                var imageBytes = imageStream.ToArray();
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                System.Drawing.Image finalImage = System.Drawing.Image.FromStream(imageStream);
                finalImage.Save(Path.Combine(path, fileName), ImageFormat.Jpeg);
                ViewBag.JsonData = $"data:image/png;base64,{Convert.ToBase64String(imageBytes)}";
               
            }
            else
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                {
                    model.PreEnrollment.BasicInfo.File.FileData.CopyTo(stream);
                }
            }
            ViewBag.fileName = fileName;
            return PartialView("~/Views/PreEnrollmentEdit/_Profile.cshtml");
        }
        
        public System.Drawing.Image resizeImage(int width, int height, Stream resourceImage)
        {
            try
            {
                var image = System.Drawing.Image.FromStream(resourceImage);
                var thumb = image.GetThumbnailImage(width, height, () => false, IntPtr.Zero);

                return thumb;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public IActionResult LoadProfilePic_In_KendoUI(string fileName)
        {
            var serverpath = Path.Combine(_env.WebRootPath, "Personal_Profile\\" + fileName);
            var contents = System.IO.File.ReadAllBytes(serverpath);
            return Json($"data:image/png;base64,{Convert.ToBase64String(contents)}");

        }

        [HttpPost]
        public ActionResult SaveProfilePicFromKendo(string contentType, string base64, string fileName)
        {
            var serverpath = Path.Combine(_env.WebRootPath, "Personal_Profile\\" + fileName);
            try
            {
                var fileContents = Convert.FromBase64String(base64);
                Enrollment model = new Enrollment();

                model.PreEnrollment = new PreEnrollment();
                model.PreEnrollment.BasicInfo = new EmployeeBasicInfo();
                model.PreEnrollment.BasicInfo.File = new Shared.File();
                model.PreEnrollment.BasicInfo.File.fdata = fileContents;
                model.PreEnrollment.BasicInfo.File.FileName = fileName;
                model.PreEnrollment.BasicInfo.File.ContentType = contentType;
                model.PreEnrollment.BasicInfo.File.FileExtension = Path.GetExtension(fileName);

                TempData["Message"] = "Uploaded Sucessfully";


                if (System.IO.File.Exists(serverpath))
                {
                    System.IO.File.Delete(serverpath);
                }

                return Json(model.PreEnrollment.BasicInfo.File);


            }
            catch (UnauthorizedAccessException e)
            {
                if (System.IO.File.Exists(serverpath))
                {
                    System.IO.File.Delete(serverpath);
                }
                return RedirectToAction("Login", "Account", new { Area = "" });
            }
            catch (Exception e)
            {
                if (System.IO.File.Exists(serverpath))
                {
                    System.IO.File.Delete(serverpath);
                }
                return RedirectToAction("Error", "Account", new { Area = "" });
            }
        }

        #endregion

        [HttpPost]
        public async Task<IActionResult> Edit(PreEnrollment model, DateTime Renewaldt)
        {

            model.BasicInfo.RenewalDate = Renewaldt;

            var response = await _service.Save(model);
            if (response.Success == true)
            {
                response.Message = "Updated Successfully";
                return Json(response);
            }
            else
            {
               
                return Json(response);
            }

        }
        [HttpPost]
        public async Task<IActionResult> Summary(PreEnrollment model)//(Guid PreEnrollGI)//(PreEnrollment model)
        {
            try
            {
                if (model.FormName == "OnBoarding")
                {
                    model.BasicInfo.File = null;

                }
                var formname = model.FormName;
               
                model = await _offer.PreviewOfferProcessing(model);
                model.FormName = formname;
                return PartialView("_PreviewAndApprove", model);
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

        public async Task fillDropdown()
        {
            ViewBag.Sal = new SelectList(await _dropdownService.GetSalutationDropDown(), "Code", "Name");
            ViewBag.Gen = new SelectList(await _dropdownService.GetGenderDropDown(), "Code", "Name");
            ViewBag.Cat = new SelectList(await _dropdownService.GetCategoryDropdown(), "Code", "Name");
            ViewBag.Dep = new SelectList(await _dropdownService.GetDepartmentDropdown(), "Code", "Name");
            ViewBag.Gra = new SelectList(await _dropdownService.GetGradeDropdown(), "Code", "Name");
            ViewBag.Loc = new SelectList(await _dropdownService.GetLocationDropdown(), "Code", "Name");
            ViewBag.Cou = new SelectList(await _dropdownService.GetCountryDropDown(), "Code", "Name");
            ViewBag.Des = new SelectList(await _dropdownService.GetDesignationDropdown(), "Code", "Name");
           
            ViewBag.VDAGroup = new SelectList(await _dropdownService.GetVDACombo(), "Code", "Name");
            ViewBag.Relation = new SelectList(await _dropdownService.GetPreenrollRelationDropDown(), "Code", "Name");
            ViewBag.SubDepartment = new SelectList(await _dropdownService.GetSubDepartmentDropDown(), "Code", "Name");
            ViewBag.Function = new SelectList(await _dropdownService.GetFunctionDropDown(), "Code", "Name");
            ViewBag.Company = new SelectList(await _dropdownService.GetGroupCompanyDropDown(), "Code", "Name");
            ViewBag.Division = new SelectList(await _dropdownService.GetDivisionDropdown(), "Code", "Name");
            ViewBag.Section = new SelectList(await _dropdownService.GetSectionDropdown(), "Code", "Name");
            ViewBag.WageGrade = new SelectList(await _dropdownService.GetWageGradeDropdown(), "Code", "Name");
            ViewBag.EmploymentType = new SelectList(await _dropdownService.GetEmploymentTypeDropdown(), "Code", "Name");
            ViewBag.Wlo = new SelectList(await _dropdownService.GetWorkLocationDropdown(), "Code", "Name");

        }
        public async Task<IActionResult> ApplyFilter(Filter filter)
        {
            try
            {

                OnBoarding model = new OnBoarding();
                model = await _service.GetOnBoardingList(filter.Tabindex, filter);

                model.TabIndex = filter.Tabindex;
                return View("_EditList", model);
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
