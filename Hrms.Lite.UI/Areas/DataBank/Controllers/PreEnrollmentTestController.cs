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
    public class PreEnrollmentTestController : Controller
    {

        #region private
        private readonly IPreEnrollmentService _preenrollmentService;
        private readonly IDropdownService _dropdownService;
        private readonly ICommonService _commonService;
        private readonly IBlob _blob;
        //private readonly IAWSBucket _aws;
        private readonly IOfferProcessingService _IOfferProcessingService;
        private IHostingEnvironment Environment;
        private IHelpers _help;
        #endregion

        public static class Global
        {
            public static string SavedFileName;
            public static string SelecedTab;
            public static PreEnrollment model;
            public static bool isRefreshed = true;
            public static bool isFromRecruitment = false;
        }


        #region Constructor
        public PreEnrollmentTestController(IOfferProcessingService OfferProcessingService,
            IPreEnrollmentService preenrollmentService, IDropdownService dropdownService, 
            IBlob blob/*, IAWSBucket aws*/, ICommonService commonService, IHostingEnvironment _environment, IHelpers help)
        {
            _preenrollmentService = preenrollmentService;
            _IOfferProcessingService = OfferProcessingService;
            _dropdownService = dropdownService;
            _commonService = commonService;
            Environment = _environment;
            _blob = blob;
            _help = help;
            //_aws = aws;
        }
        #endregion

        #region get or post requests
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
        public async Task<IActionResult> GetCompensationGrid(int GradeCode)
        {

            try
            {
                PreEnrollment model = new PreEnrollment();
                var ComponsationGrid = await _preenrollmentService.GetCompensationGrid(GradeCode);
                model.Compensation = ComponsationGrid;
                return PartialView("_Compensation", model);
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
        //public async Task<IActionResult> GetSalaryStructureAlwDedGrid(int SalaryStructureCode, decimal? GrossSalary, int VDAGroup)
         public async Task<IActionResult> GetSalaryStructureAlwDedGrid(int SalaryStructureCode, decimal? CTC, decimal? GrossSalary, int GroupCompany,int LocationCode,int DepartmentCode,int VDAGroup)
        {

            try
            {
                PreEnrollment model = new PreEnrollment();
                var ComponsationGrid = await _preenrollmentService.GetSalaryStructureAlwDedGrid(SalaryStructureCode,  CTC, GrossSalary,  GroupCompany,  LocationCode,  DepartmentCode, VDAGroup);
                var PFMaxLimit = await _commonService.GetPFMaxLimit();
                model.Compensation = ComponsationGrid;
                ViewBag.SlryStruct = new SelectList(await _dropdownService.GeSalaryStructureDropDown(), "Code", "Name");

                        model.Compensation.SalaryStructure = new SalaryStructure();
                        model.Compensation.SalaryStructure.Code = SalaryStructureCode;
                        model.Compensation.OfferedSalary = CTC;
                        model.Compensation.ApprovedGross = GrossSalary;
             
                return PartialView("_Compensation", model);
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
            //ViewBag.Pay = new SelectList(await _dropdownService.GetPayrollGroupDropdown(), "Code", "Name");
            //ViewBag.SlryStruct = new SelectList(await _dropdownService.GeSalaryStructureDropDown(), "Code", "Name");
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
        [HttpGet]
        public async Task<IActionResult> Create()
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
                    PreEnrollment model = new PreEnrollment();
                    model = await _commonService.GetMandatoryFiledValidation("PREENROLL");

                    model.JobDetails = new EmployeeClassification();

                    model.JobDetails.NoticePeriod = await _commonService.GetNoticePeriod(0, null);
                    //if (Global.isRefreshed)
                    //{
                        //Global.isRefreshed = false;
                        model.FormName = "OnBoarding";
                        model.Compensation = new Shared.DataBank.Generic.CompensationStructure();
                        model.selectedTab = "Personal-Info";
                    //}
                    //else
                    //{

                    //    Global.isRefreshed = true;

                    //    if (Global.model != null)
                    //        model = Global.model;
                    //    else
                    //    {
                    //        model.FormName = "OnBoarding";
                    //        model.Compensation = new Shared.DataBank.Generic.CompensationStructure();
                    //        model.selectedTab = "Personal-Info";
                    //    }
                    //    Global.model = null;
                    //}

                    if (model.BasicInfo != null)
                    {
                        ViewBag.FileName = model.BasicInfo.File.FileName;
                        var ext = Path.GetExtension(ViewBag.FileName);
                        string dataUri = $"data:image/{ext.Substring(1)};base64,{Convert.ToBase64String(model.BasicInfo.File.fdata)}";
                        ViewBag.dataUri = dataUri;
                    }
                    return View("Create", model);
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
        [HttpGet]
        public async Task<IActionResult> ApplicantDetails(Guid ApplicantGI)
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
                    PreEnrollment model = new PreEnrollment();
                    PreEnrollment model_ = new PreEnrollment();
                    model_ = await _commonService.GetMandatoryFiledValidation("PREENROLL");

                    model.JobDetails = new EmployeeClassification();
                    model.JobDetails.Designation = new Designation();
                    model.Compensation = new CompensationStructure();

                    model = await _preenrollmentService.GetApplicantDetails(ApplicantGI);
                    model.FiledValidations = model_.FiledValidations;


                    ViewBag.SlryStruct = new SelectList(await _dropdownService.GetWageGradeVsSalaryBand((int)model.JobDetails.WageGrade.Code), "Code", "Name");
                    model.JobDetails.NoticePeriod = await _commonService.GetNoticePeriod((int)model.JobDetails.Designation.Code, null);

                    Global.isFromRecruitment = true;
                    //if (Global.isRefreshed)
                    //{
                        Global.isRefreshed = false;
                        model.FormName = "OnBoarding";
                        //model.Compensation = new Shared.DataBank.Generic.CompensationStructure();
                        model.selectedTab = "Personal-Info";
                    //}
                    //else
                    //{

                    //    Global.isRefreshed = true;

                    //    if (Global.model != null)
                    //        model = Global.model;
                    //    else
                    //    {
                    //        model.FormName = "OnBoarding";
                    //        //model.Compensation = new Shared.DataBank.Generic.CompensationStructure();
                    //        model.selectedTab = "Personal-Info";
                    //    }
                    //    Global.model = null;
                    //}

                    if (model.BasicInfo != null)
                    {
                        ViewBag.FileName = model.BasicInfo.File.FileName;
                        var ext = Path.GetExtension(ViewBag.FileName);
                      
                        string dataUri = $"data:image/{ext.Substring(1)};base64,{Convert.ToBase64String(model.BasicInfo.File.fdata)}";
                        ViewBag.dataUri = dataUri;
                    }
                    return View("Create", model);
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
        


        public IActionResult SaveProfileToFolder(PreEnrollment model, string SelectedTab)
        {
            Global.model = model;
            Global.SelecedTab = SelectedTab;
            string path = Path.Combine(this.Environment.WebRootPath, "Personal_Profile");

            string fileName = model.BasicInfo.File.FileData.FileName;

            var Imgstream = model.BasicInfo.File.FileData.OpenReadStream();

            dynamic originalImage;
            try
            {
                System.Drawing.Image oImage = System.Drawing.Image.FromStream(Imgstream);
                originalImage = oImage;
            }
            catch (Exception e)
            {
                var wImage = SKBitmap.Decode(Imgstream);
                var oImage = wImage;
                originalImage = oImage;
            }
            if (model.BasicInfo.File.FileData.Length > (1024 * 1024) || (originalImage.Width > 1920 && originalImage.Height > 1080))
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
                var stream2 = model.BasicInfo.File.FileData.OpenReadStream();
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
                var ext = Path.GetExtension(fileName);
                ViewBag.JsonData = $"data:image/" + ext.Substring(1) + ";base64,{Convert.ToBase64String(imageBytes)}";
            }
            else
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                {
                    model.BasicInfo.File.FileData.CopyTo(stream);
                }
            }
            Global.SavedFileName = fileName;
            ViewBag.fileName = fileName;
         
            return PartialView("_Profile");
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
            var serverpath = Path.Combine(Environment.WebRootPath, "Personal_Profile\\" + fileName);
            var contents = System.IO.File.ReadAllBytes(serverpath);
            var ext = Path.GetExtension(fileName);
            return Json($"data:image/{ext.Substring(1)};base64,{Convert.ToBase64String(contents)}");

        }

        [AcceptVerbs("Post")]
        public async Task<ActionResult> SaveProfilePicFromKendo(string contentType, string base64, string fileName)
        {
            var serverpath = Path.Combine(Environment.WebRootPath, "Personal_Profile\\" + Global.SavedFileName);
            try
            {
                var fileContents = Convert.FromBase64String(base64);
                PreEnrollment model = new PreEnrollment();              
               
                model.BasicInfo = new Shared.DataBank.Generic.EmployeeBasicInfo();
                model.BasicInfo.File = new Shared.File();
                model.BasicInfo.File.fdata = fileContents;
                model.BasicInfo.File.FileName = $"{Guid.NewGuid().ToString()}{Path.GetExtension(fileName)}";
                model.BasicInfo.File.ContentType = contentType;
                model.BasicInfo.File.FileExtension = Path.GetExtension(fileName);

                Global.model.BasicInfo.File = model.BasicInfo.File;
             

                TempData["Message"] = "Uploaded Sucessfully";

                if (System.IO.File.Exists(serverpath))
                {
                    System.IO.File.Delete(serverpath);
                }
                Global.isRefreshed = false;

                return RedirectToAction("Create");
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

        [HttpPost]
        public async Task<IActionResult> SalarySummary(PreEnrollment model)//(Guid PreEnrollGI)//(PreEnrollment model)
        {
            try
            {
                if (model.FormName == "OnBoarding")
                {
                    model.BasicInfo.File = null;

                }
                var formname = model.FormName;
                //PreEnrollment model = new PreEnrollment();
                model = await _IOfferProcessingService.PreviewOfferProcessing(model);
                model.FormName = formname;
                return PartialView("_SalaryDetails", model);
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
        public async Task<IActionResult> Summary(PreEnrollment model)//(Guid PreEnrollGI)//(PreEnrollment model)
        {
            try
            {
                if (model.FormName == "OnBoarding")
                {
                    model.BasicInfo.File = null;

                }
                var formname = model.FormName;
                //PreEnrollment model = new PreEnrollment();
                model = await _IOfferProcessingService.PreviewOfferProcessing(model);
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


        [HttpPost]
        public async Task<IActionResult> Create(PreEnrollment model, DateTime Renewaldt)
        {

            model.BasicInfo.RenewalDate = Renewaldt;

            var response = await _preenrollmentService.SavePreEnrollment(model);
            if (response.Success == true)
            {
                response.Message = "Saved Successfully";
                return Json(response);
            }
            else
            {
                //bool val = await _blob.Delete(Global.SavedFileName);
                return Json(response);
            }

        }

        public async Task<IActionResult> GetAadharDuplication(string AadharNo)
        {
            try
            {
                EmployeeHeader model = new EmployeeHeader();
                model.TypeCode = 1;
                model = await _commonService.GetAadharDuplication(AadharNo, model.TypeCode);
                model.TypeCode = 1;
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

        public async Task<IActionResult> GetPANDuplication(string PANno)
        {
            try
            {
                EmployeeHeader model = new EmployeeHeader();
                model.TypeCode = 2;
                model = await _commonService.GetAadharDuplication(PANno, model.TypeCode);
                model.TypeCode = 2;
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
        public async Task<IActionResult> GetMobDuplication(string Mobile)
        {
            try
            {
                EmployeeHeader model = new EmployeeHeader();
                model.TypeCode = 3;
                model = await _commonService.GetAadharDuplication(Mobile, model.TypeCode);
                model.TypeCode = 3;
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


        [HttpGet]
        public async Task<JsonResult> GetNoticePeriod(int DesignationCode, Guid? EmployeeGI)
        {
            var data = await _commonService.GetNoticePeriod(DesignationCode, EmployeeGI);
            return Json(data);
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
        [HttpGet]
        public async Task<JsonResult> GetRenewalDate(int CategoryCode, string DOB, string DOJ)
        {
            var result = await _preenrollmentService.GetRenewalDate(CategoryCode, DOJ, DOB);
            return Json(result.BasicInfo.RenewalDate.Value.ToString("dd-MMM-yyyy"));
        }
        [HttpGet]
        public async Task<JsonResult> GetRetirementType(int CategoryCode, string DOB, string DOJ)
        {
            var result = await _preenrollmentService.GetRenewalDate(CategoryCode, DOJ, DOB);
            return Json(result.BasicInfo.RetirementType);
        }


        [HttpGet]
        public async Task<JsonResult> GetSalaryStructure(int id)
        {
            var SalaryBand = new SelectList(await _dropdownService.GetWageGradeVsSalaryBand(id), "Code", "Name");
            return Json(SalaryBand);
        }

        [HttpGet]
        public async Task<JsonResult> GetPayrollGroup(int id)
        {
            var PayrollGroup = new SelectList(await _dropdownService.GetGCVsPayrollGroup(id), "Code", "Name");
            return Json(PayrollGroup);
        }
        [HttpGet]
        public async Task<JsonResult> GetMinCTC(int WageGradeCode,int LocationCode)
        {
            var data = await _preenrollmentService.GetMinCTC(WageGradeCode, LocationCode);
            return Json(data);
        }
        #endregion
    }
}
