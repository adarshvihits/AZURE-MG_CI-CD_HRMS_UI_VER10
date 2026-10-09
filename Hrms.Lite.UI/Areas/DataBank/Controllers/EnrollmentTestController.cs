using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.DataBank.Generic;
using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.General;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Hrms.Lite.Services.IServices.Recruitment;
using Newtonsoft.Json;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.IO.Compression;
using Microsoft.AspNetCore.StaticFiles;

namespace Hrms.Lite.UI.Areas.DataBank.Controllers
{
    [Area("DataBank")]
    [SessionAuthorize]
    public class EnrollmentTestController : Controller
    {
        private readonly IEnrollmentService _enrollmentService;
        private readonly IPreEnrollmentService _preEnrollmentService;
        private readonly IDropdownService _dropdownService;
        private readonly ICommonService _commonService;
        private IHostingEnvironment Environment;
        private readonly IOfferProcessingService _IOfferProcessingService;
        private readonly IHelpers _helpers;
        private readonly IBlob _blob;

        public static class Global
        {
            public static string SavedFileName;
            public static Guid EnrollGI;
            public static string Breadcrumb;
            public static string BreadcrumbName;
            public static string SelecedTab;
            public static Enrollment model;
            public static bool isRefreshed = true;
        }
        
        public EnrollmentTestController(IEnrollmentService enrollmentService, IDropdownService dropdownService, ICommonService commonService, IBlob blob, IPreEnrollmentService preEnrollmentService, IHostingEnvironment _environment, IOfferProcessingService IOfferProcessingService, IHelpers helpers)
        {
            _enrollmentService = enrollmentService;
            _preEnrollmentService = preEnrollmentService;
            _dropdownService = dropdownService;
            _commonService = commonService;
            Environment = _environment;
            _blob = blob;
            _IOfferProcessingService = IOfferProcessingService;
            _helpers = helpers;
        }
        public async Task<IActionResult> Index(Filter filter, int IsFilter)
        {
            try
            {
                OnBoarding model = new OnBoarding();
                //Filter filter = new Filter();
                await fillfilterDropdown();
                model = await _enrollmentService.GetEnrollEditList(filter);
                if (IsFilter != 1)
                    return View(model);
                else
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


        public async Task fillfilterDropdown()
        {

            ViewBag.Gen = new SelectList(await _dropdownService.GetGenderDropDown(), "Code", "Name");
            ViewBag.Cat = new SelectList(await _dropdownService.GetCategoryDropdown(), "Code", "Name");
            ViewBag.Grd = new SelectList(await _dropdownService.GetGradeDropdown(), "Code", "Name");
            ViewBag.Dsg = new SelectList(await _dropdownService.GetDesignationDropdown(), "Code", "Name");
            ViewBag.Dept = new SelectList(await _dropdownService.GetDepartmentDropdown(), "Code", "Name");
            ViewBag.Loc = new SelectList(await _dropdownService.GetLocationDropdown(), "Code", "Name");
            ViewBag.PayrollGrp = new SelectList(await _dropdownService.GetPayrollGroupDropdown(), "Code", "Name");

            ViewBag.division = new SelectList(await _dropdownService.GetDivisionDropdown(), "Code", "Name");
            ViewBag.section = new SelectList(await _dropdownService.GetSectionDropdown(), "Code", "Name");
            ViewBag.EmploymentType = new SelectList(await _dropdownService.GetEmploymentTypeDropdown(), "Code", "Name");
            ViewBag.EmploymentStatus = new SelectList(await _dropdownService.GetEmploymentStatusDropdown(), "Code", "Name");
            ViewBag.WorkLocation = new SelectList(await _dropdownService.GetWorkLocationDropdown(), "Code", "Name");
            ViewBag.WageGrade = new SelectList(await _dropdownService.GetWageGradeDropdown(), "Code", "Name");

       
        }
        [HttpGet]
        public async Task<IActionResult> Create(Guid EnrollGI, string Breadcrumb, string BreadcrumbName, bool isNewUpload)
        {
            try
            {
                if (Global.isRefreshed)
                {
                    isNewUpload = false;
                    Global.isRefreshed = false;
                }
                if (!isNewUpload)
                {
                    Global.EnrollGI = EnrollGI;
                    Global.Breadcrumb = Breadcrumb;
                    Global.BreadcrumbName = BreadcrumbName;


                    await fillDropdown();
                    Enrollment model = new Enrollment();
                    Enrollment model2 = new Enrollment();
                    var response = await _commonService.CheckEmptyDropDown("Enroll");
                    if (response.Success is false)   //If Any Empty DropDown
                    {
                        TempData["Message"] = response.Message;
                        return RedirectToAction("Index", "OnBoarding");
                    }
                    else
                    {
                        model2.PreEnrollment = await _commonService.GetMandatoryFiledValidation("ENROLL");
                        List<FiledValidations> list = model2.PreEnrollment.FiledValidations;
                        model = await _enrollmentService.GetPreEnrollmentDetails(EnrollGI);
                        model.PreEnrollment.FiledValidations = list;
                        model.AddOnClassification = await _commonService.GetAddOnClassificationApplicability();
                        model.EnrollGUID = EnrollGI;
                        model.AttendanceSettings = new AttendanceSettings();
                        ViewBag.St = new SelectList(await _dropdownService.GetStateDropDown(model.PreEnrollment.PermanentAddress.Country.Code), "Code", "Name");
                        ViewBag.Dist = new SelectList(await _dropdownService.GetDistrictDropdown(model.PreEnrollment.PermanentAddress.State.Code), "Code", "Name");
                        ViewBag.Pay = new SelectList(await _dropdownService.GetGCVsPayrollGroup((int)model.PreEnrollment.JobDetails.Company.Code), "Code", "Name");
                        ViewBag.SlryStruct = new SelectList(await _dropdownService.GetWageGradeVsSalaryBand((int)model.PreEnrollment.JobDetails.WageGrade.Code), "Code", "Name");
                        ViewBag.Position = new SelectList(await _dropdownService.GetNewPositionDropdown((int)model.PreEnrollment.JobDetails.Designation.Code, (int)model.PreEnrollment.JobDetails.Location.Code, (int)model.PreEnrollment.JobDetails.Department.Code), "Code", "Name");

                        model.Breadcrumb = Breadcrumb;
                        model.BreadcrumbName = BreadcrumbName;
                        //model.SelectedTab = "Attendance-Settings";
                        model.SelectedTab = "Personal-Info";
                        
                        if (model.PreEnrollment.BasicInfo.File.fdata != null)
                        {
                            ViewBag.FileName = model.PreEnrollment.BasicInfo.File.FileName;
                            var ext = Path.GetExtension(ViewBag.FileName);
         
                            string dataUri = $"data:image/{ext.Substring(1)};base64,{Convert.ToBase64String(model.PreEnrollment.BasicInfo.File.fdata)}";
                            ViewBag.dataUri = dataUri;
                        }
                        return View("Create", model);
                    }
                }
                else
                {
                    Global.isRefreshed = true;
                    await fillDropdown();
                    Enrollment model = new Enrollment();
                    if (Global.model != null)
                        model = Global.model;
                    var response = await _commonService.CheckEmptyDropDown("Enroll");
                    if (response.Success is false)   //If Any Empty DropDown
                    {
                        TempData["Message"] = response.Message;
                        return RedirectToAction("Index", "OnBoarding");
                    }
                    else
                    {
                        //model = await _enrollmentService.GetPreEnrollmentDetails(EnrollGI);
                        if (Global.model == null && model.PreEnrollment == null)
                        {
                            model = await _enrollmentService.GetPreEnrollmentDetails(EnrollGI);
                            model.AttendanceSettings = new AttendanceSettings();
                        }
                        model.AddOnClassification = await _commonService.GetAddOnClassificationApplicability();
                        model.EnrollGUID = EnrollGI;
                        //model.AttendanceSettings = new AttendanceSettings();
                        ViewBag.St = new SelectList(await _dropdownService.GetStateDropDown(model.PreEnrollment.PermanentAddress.Country.Code), "Code", "Name");
                        ViewBag.Dist = new SelectList(await _dropdownService.GetDistrictDropdown(model.PreEnrollment.PermanentAddress.State.Code), "Code", "Name");
                        ViewBag.Pay = new SelectList(await _dropdownService.GetGCVsPayrollGroup((int)model.PreEnrollment.JobDetails.Company.Code), "Code", "Name");
                        ViewBag.SlryStruct = new SelectList(await _dropdownService.GetWageGradeVsSalaryBand((int)model.PreEnrollment.JobDetails.WageGrade.Code), "Code", "Name");
                        ViewBag.Position = new SelectList(await _dropdownService.GetNewPositionDropdown((int)model.PreEnrollment.JobDetails.Designation.Code, (int)model.PreEnrollment.JobDetails.Location.Code, (int)model.PreEnrollment.JobDetails.Department.Code), "Code", "Name");

                        model.Breadcrumb = Breadcrumb;
                        model.BreadcrumbName = BreadcrumbName;
                        model.SelectedTab = Global.SelecedTab;
                        Global.model = null;
                        if (model.PreEnrollment.BasicInfo.File.fdata != null)
                        {
                            ViewBag.FileName = model.PreEnrollment.BasicInfo.File.FileName;
                            var ext = Path.GetExtension(ViewBag.FileName);
                     
                            string dataUri = $"data:image/{ext.Substring(1)};base64,{Convert.ToBase64String(model.PreEnrollment.BasicInfo.File.fdata)}";
                            ViewBag.dataUri = dataUri;
                        }
                        return View("Create", model);
                    }
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

    

        public async Task fillDropdown()
        {
            ViewBag.Sal = new SelectList(await _dropdownService.GetSalutationDropDown(), "Code", "Name");
            ViewBag.Gen = new SelectList(await _dropdownService.GetGenderDropDown(), "Code", "Name");
            ViewBag.Cat = new SelectList(await _dropdownService.GetCategoryDropdown(), "Code", "Name");
            ViewBag.Grd = new SelectList(await _dropdownService.GetGradeDropdown(), "Code", "Name");
            ViewBag.Dsg = new SelectList(await _dropdownService.GetDesignationDropdown(), "Code", "Name");
            ViewBag.Dept = new SelectList(await _dropdownService.GetDepartmentDropdown(), "Code", "Name");
            ViewBag.Loc = new SelectList(await _dropdownService.GetLocationDropdown(), "Code", "Name");
            ViewBag.PayrollGrp = new SelectList(await _dropdownService.GetPayrollGroupDropdown(), "Code", "Name");
            ViewBag.Cntry = new SelectList(await _dropdownService.GetCountryDropDown(), "Code", "Name");

            ViewBag.SftType = new SelectList(await _dropdownService.GetShiftTypeDropDown(), "Code", "Name");
            ViewBag.SftDetails = new SelectList(await _dropdownService.GetShiftDetailsDropdown(), "Code", "Name");
            ViewBag.Punching = new SelectList(await _dropdownService.GetPunchingMethodDropdown(), "Code", "Name");
            ViewBag.LeavGroup = new SelectList(await _dropdownService.GetLeaveGroupFilterDropdown(), "Code", "Name");
            ViewBag.HolidayGrp = new SelectList(await _dropdownService.GetHolidayGroupDropdown(), "Code", "Name");
            ViewBag.YearlyOffDaySettings = new SelectList(await _dropdownService.GetOffDaySettings_Dropdown(), "Code", "Name");
            ViewBag.ShiftCircle = new SelectList(await _dropdownService.GetDutyRosterCircleDropdown(), "Code", "Name");



            ViewBag.Payment = new SelectList(await _dropdownService.GetPaymentModeDropDown(), "Code", "Name");
            ViewBag.BankName = new SelectList(await _dropdownService.GetBankDropdown(), "Code", "Name");
            ViewBag.PFGroup = new SelectList(await _dropdownService.GetPFGroupDropdown(), "Code", "Name");
            ViewBag.ESIGroup = new SelectList(await _dropdownService.GetESIGroupDropDown(), "Code", "Name");
            ViewBag.PTGroup = new SelectList(await _dropdownService.GetPTGroupDropDown(), "Code", "Name");



            ViewBag.ReportingMgr = new SelectList(await _dropdownService.GetReportingManagerDropdown(), "Code", "Name");
            ViewBag.Div = new SelectList(await _dropdownService.GetDivisionDropdown(), "Code", "Name");
            ViewBag.Sec = new SelectList(await _dropdownService.GetSectionDropdown(), "Code", "Name");
            ViewBag.Emt = new SelectList(await _dropdownService.GetEmploymentTypeDropdown(), "Code", "Name");
            ViewBag.Wlo = new SelectList(await _dropdownService.GetWorkLocationDropdown(), "Code", "Name");
            ViewBag.Wgr = new SelectList(await _dropdownService.GetWageGradeDropdown(), "Code", "Name");

            ViewBag.SlryStruct = new SelectList(await _dropdownService.GeSalaryStructureDropDown(), "Code", "Name");
            ViewBag.VDAGroup = new SelectList(await _dropdownService.GetVDACombo(), "Code", "Name");
            ViewBag.Relation = new SelectList(await _dropdownService.GetPreenrollRelationDropDown(), "Code", "Name");

            ViewBag.SubDepartment = new SelectList(await _dropdownService.GetSubDepartmentDropDown(), "Code", "Name");
            ViewBag.Function = new SelectList(await _dropdownService.GetFunctionDropDown(), "Code", "Name");
            ViewBag.Company = new SelectList(await _dropdownService.GetGroupCompanyDropDown(), "Code", "Name");
            ViewBag.Position = new SelectList(await _dropdownService.GetNewPositionDropdown(0, 0, 0), "Code", "Name");

            ViewBag.IncApplicable = new SelectList(new[] { new { Code = "1", Name = "Yes" }, new { Code = "0", Name = "No" } }, "Code", "Name");
            ViewBag.IncentiveCostCenter = new SelectList(await _dropdownService.GetIncentiveCostCenterDropdown(1), "Code", "Name");



        }
        public async Task<IActionResult> GetSalaryStructureAlwDedGrid(int PreEnrollCode,int SalaryStructureCode, decimal? CTC, decimal? GrossSalary, int GroupCompany, int LocationCode, int DepartmentCode, int VDAGroup)
        {

            try
            {
                Enrollment model = new Enrollment();
                var ComponsationGrid = await _enrollmentService.GetSalaryStructureAlwDedGrid(PreEnrollCode,SalaryStructureCode, CTC, GrossSalary, GroupCompany, LocationCode, DepartmentCode, VDAGroup);
                model.PreEnrollment = new PreEnrollment();
                model.PreEnrollment.Compensation = ComponsationGrid;
                ViewBag.SlryStruct = new SelectList(await _dropdownService.GeSalaryStructureDropDown(), "Code", "Name");
                model.PreEnrollment.Compensation.SalaryStructure = new SalaryStructure();
                model.PreEnrollment.Compensation.SalaryStructure.Code = SalaryStructureCode;
                model.PreEnrollment.Compensation.ApprovedGross = GrossSalary;
                model.PreEnrollment.Compensation.MinCTC = ComponsationGrid.MinCTC;
                return PartialView("_EnrollCompensation", model);
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

        //public async Task<IActionResult> Create(Enrollment input, DateTime Renewaldt)
        //{
        //    var json = JsonConvert.SerializeObject(input);
        //    Enrollment model = JsonConvert.DeserializeObject<Enrollment>(json);

        //    //if (model.AttendanceSettings.ShiftDetailsId != null)
        //    //{
        //    //    model.AttendanceSettings.ShiftDetailsIds = new int[1];
        //    //    model.AttendanceSettings.ShiftDetailsIds[0] = (int)model.AttendanceSettings.ShiftDetailsId;
        //    //}
        //    //var response = await _enrollmentService.SaveEnrollment(model);


        //    //string path = Path.Combine(this.Environment.WebRootPath, "ETEST");

        //    //string relativeFilePath = Path.Combine(path, model.PreEnrollment.BasicInfo.File.FileName);

        //    //string localFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativeFilePath);

        //    using (var httpClient = new HttpClient())
        //    {
        //        using (var response = await httpClient.GetAsync(input.PreEnrollment.BasicInfo.File.AbsoluteUri))
        //        {

        //            if (response.IsSuccessStatusCode)
        //            {

        //                input.PreEnrollment.BasicInfo.File.fdata = await response.Content.ReadAsByteArrayAsync();
        //                input.PreEnrollment.BasicInfo.File.FileExtension = Path.GetExtension(input.PreEnrollment.BasicInfo.File.FileName);
        //                input.PreEnrollment.BasicInfo.EmployeeThumbNail  = new Shared.File();
        //                input.PreEnrollment.BasicInfo.EmployeeThumbNail  = await _blob.UploadToAzureThumbnailAsyncWithByteArray(input.PreEnrollment.BasicInfo.File);


        //                //var content = await response.Content.ReadAsByteArrayAsync();                        
        //                //Directory.CreateDirectory(Path.GetDirectoryName(localFilePath));

        //                //using (var fileStream = new FileStream(localFilePath, FileMode.Create, FileAccess.Write))
        //                //{
        //                //    await fileStream.WriteAsync(content, 0, content.Length);                           
        //                //}

        //            }
        //        }
        //    }

        //    model.PreEnrollment.BasicInfo.EmployeeThumbNail = new Shared.File();
        //    model.PreEnrollment.BasicInfo.EmployeeThumbNail = input.PreEnrollment.BasicInfo.EmployeeThumbNail;


        //    return Json(1);


        //}

        public IActionResult SaveProfileToFolder(Enrollment model, string SelectedTab, string Breadcrumb)
        {
            Global.model = model;
            Global.SelecedTab = SelectedTab;
            Shared.File File = model.PreEnrollment.BasicInfo.File;
            model.PreEnrollment.BasicInfo.File = null;
            model.PreEnrollment.BasicInfo.EmployeeThumbNail = null;
            model.PreEnrollment.BasicInfo.OfferLetter = null;

            HttpContext.Session.SetString("Data", JsonConvert.SerializeObject(model));
            model.PreEnrollment.BasicInfo.File = File;
            string path = Path.Combine(this.Environment.WebRootPath, "Personal_Profile");

            string fileName = model.PreEnrollment.BasicInfo.File.FileData.FileName;

            var Imgstream = model.PreEnrollment.BasicInfo.File.FileData.OpenReadStream();
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
                var ext = Path.GetExtension(fileName);
                ViewBag.JsonData = $"data:image/" + ext.Substring(1) + ";base64,{Convert.ToBase64String(imageBytes)}";
                //----------------------------------------------------------------------
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
            Global.SavedFileName = fileName;
            ViewBag.fileName = fileName;
         
            if (Breadcrumb != "Employee Rejoin")
                return PartialView("_Profile");
            else
                return PartialView("~/Areas/DataBank/Views/EmployeeRejoin/_Profile.cshtml", model);

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
                Enrollment model = new Enrollment();               
                model.PreEnrollment = new PreEnrollment();
                model.PreEnrollment.BasicInfo = new Shared.DataBank.Generic.EmployeeBasicInfo();
                model.PreEnrollment.BasicInfo.File = new Shared.File();
                model.PreEnrollment.BasicInfo.File.fdata = fileContents;
                model.PreEnrollment.BasicInfo.File.FileName = $"{Guid.NewGuid().ToString()}{Path.GetExtension(fileName)}";
                model.PreEnrollment.BasicInfo.File.ContentType = contentType;
                model.PreEnrollment.BasicInfo.File.FileExtension = Path.GetExtension(fileName);
                // ...custom logic for saving the string to the database...

                Global.model.PreEnrollment.BasicInfo.File = model.PreEnrollment.BasicInfo.File;
          

                TempData["Message"] = "Uploaded Sucessfully";



                if (System.IO.File.Exists(serverpath))
                {
                    System.IO.File.Delete(serverpath);
                }
                if (Global.model != null)
                    Global.isRefreshed = false;
                else
                    Global.isRefreshed = true;

                return RedirectToAction("Create", new { EnrollGI = Global.EnrollGI, Breadcrumb = Global.Breadcrumb, BreadcrumbName = Global.BreadcrumbName, isNewUpload = true });
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
        [AcceptVerbs("Post")]
        public async Task<ActionResult> SaveProfilePicRejoinFromKendo(string contentType, string base64, string fileName, Guid EnrollGI)
        {
            var serverpath = Path.Combine(Environment.WebRootPath, "Personal_Profile\\" + Global.SavedFileName);
            try
            {
                var fileContents = Convert.FromBase64String(base64);
                Enrollment model = new Enrollment();
                Enrollment model1 = new Enrollment();
                model.PreEnrollment = new PreEnrollment();
                model.PreEnrollment.BasicInfo = new Shared.DataBank.Generic.EmployeeBasicInfo();
                model.PreEnrollment.BasicInfo.File = new Shared.File();
                model.PreEnrollment.BasicInfo.File.fdata = fileContents;
                model.PreEnrollment.BasicInfo.File.FileName = $"{Guid.NewGuid().ToString()}{Path.GetExtension(fileName)}";
                model.PreEnrollment.BasicInfo.File.ContentType = contentType;
                model.PreEnrollment.BasicInfo.File.FileExtension = Path.GetExtension(fileName);       


              

                Global.model.PreEnrollment.BasicInfo.File = model.PreEnrollment.BasicInfo.File;


                TempData["Message"] = "Uploaded Sucessfully";




                if (System.IO.File.Exists(serverpath))
                {
                    System.IO.File.Delete(serverpath);
                }
                if (Global.model != null)
                    Global.isRefreshed = false;
                else
                    Global.isRefreshed = true;

                string jsonData = HttpContext.Session.GetString("Data");
                Enrollment data = JsonConvert.DeserializeObject<Enrollment>(jsonData);
          
                return RedirectToAction("Create", "EmployeeRejoin", new { EnrollGI = EnrollGI, OriginalFileName = fileName });
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
        public async Task<IActionResult> GetAuthorityDetails()
        {
            try
            {
                Enrollment model = new Enrollment();
                model.EmployeeSettings = new EmployeeSettings();
                model.EmployeeSettings.RequestVsAuthoritySettings = new List<RequestVsAuthoritySettings>();
                RequestVsAuthoritySettings list = new RequestVsAuthoritySettings
                {
                    LevelCode = 1,
                    LevelName = "Level 1"
                };
                model.EmployeeSettings.RequestVsAuthoritySettings.Add(list);
                ViewBag.Aut = new SelectList(await _dropdownService.GetRequestAuthorityDropDown(), "Code", "Name");
                ViewBag.ReqType = new SelectList(await _dropdownService.GetRequestTypeDropDown(), "Code", "Name", 1);
                return PartialView("_RequestVsAuthorityPopUp", model.EmployeeSettings);
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
        public async Task<JsonResult> GetDigitsInAccountNumber(int BankCode)
        {
            var DigitsInAccountNumber = await _commonService.GetDigitsInAccountNumber(BankCode);
            return Json(DigitsInAccountNumber);
        }
        [HttpPost]
        public async Task<IActionResult> AddToList(Enrollment model, EmployeeSettings popupData, int ReqCode)
        {
            List<RequestVsAuthoritySettings> m = new List<RequestVsAuthoritySettings>();

         
            if (model.EmployeeSettingsReqVsAuth != null)
            {
                for (int j = 0; j < model.EmployeeSettingsReqVsAuth.RequestVsAuthoritySettings.Count; j++)
                {
                    if (model.EmployeeSettingsReqVsAuth.RequestVsAuthoritySettings[j].RequestTypeCode != ReqCode)
                    {

                        m.Add(model.EmployeeSettingsReqVsAuth.RequestVsAuthoritySettings[j]);
                    }
                }
            }


            for (int i = 0; i < popupData.RequestVsAuthoritySettings.Count; i++)
            {
                if (popupData.RequestVsAuthoritySettings[i].AuthorityCode != 0)
                {
                    m.Add(popupData.RequestVsAuthoritySettings[i]);
                }
            }
            model.EmployeeSettingsReqVsAuth = new EmployeeSettings();
            model.EmployeeSettingsReqVsAuth.RequestVsAuthoritySettings = m;

            return PartialView("_RequestVsAuthorityList", model);
        }
        [HttpPost]
        public async Task<IActionResult> Create(Enrollment model, DateTime Renewaldt)
        {
            if (model.StatutoryClassification.BankFile != null)
            {
                if (model.StatutoryClassification.BankFile.FileData != null)
                {
                    model.StatutoryClassification.BankFile.fdata = await CompressFileAsync(model.StatutoryClassification.BankFile.FileData);
                    model.StatutoryClassification.BankFile.FileName = model.StatutoryClassification.BankFile.FileData.FileName;
                    model.StatutoryClassification.BankFile.ContentType = model.StatutoryClassification.BankFile.FileData.ContentType;
                    model.StatutoryClassification.BankFile.FileData = null;
                }
            }
            model.PreEnrollment.BasicInfo.RenewalDate = Renewaldt;
            if (model.AttendanceSettings.ShiftDetailsId != null)
            {
                model.AttendanceSettings.ShiftDetailsIds = new int[1];
                model.AttendanceSettings.ShiftDetailsIds[0] = (int)model.AttendanceSettings.ShiftDetailsId;
            }
            var response = await _enrollmentService.SaveEnrollment(model);
            return Json(response);

        }

        public async Task<IActionResult> RejectEnroll(Guid PreEnrollGI, string Reason)
        {
            PreEnrollment model = new PreEnrollment();
            model.PreEnrollGI = PreEnrollGI;
            model.Reason = Reason;
            var response = await _enrollmentService.RejectEnroll(model);

            return Json(response);
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
        public async Task<JsonResult> AlreadyExistCheck(string AccountNo, Guid EmployeeGI, string Mobile, string PAN, string Aadhar, string PFNumber, string ESINumber, string UANNumber, string EmployeeID, string PunchingID)
        {
            var result = await _enrollmentService.AlreadyExistCheck(EmployeeGI, Mobile, PAN, Aadhar, PFNumber, ESINumber, UANNumber, EmployeeID, PunchingID, AccountNo);
            return Json(result);
        }

        public async Task<IActionResult> GetCompensationGrid(int GradeCode)
        {
            try
            {
                Enrollment model = new Enrollment();
                var CompensationGrid = await _enrollmentService.GetComponsationGrid(GradeCode);
                model.PreEnrollment = new PreEnrollment();
                model.PreEnrollment.Compensation = CompensationGrid;
                return PartialView("_EnrollCompensation", model);
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
        public async Task<JsonResult> GetRenewalDate(int CategoryCode, string DOB, string DOJ)
        {
            var result = await _preEnrollmentService.GetRenewalDate(CategoryCode, DOJ, DOB);
            return Json(result.BasicInfo.RenewalDate.Value.ToString("dd-MMM-yyyy"));
        }


        [HttpGet]
        public async Task<ActionResult> GetEnrollLeaveOpeningDetails(DateTime? DOJ, int LeaveGroup)
        {
            Enrollment model = new Enrollment();
            model = await _enrollmentService.GetEnrollLeaveOpeningDetails(DOJ.Value.ToString("yyyy-MMM-dd"), LeaveGroup);

            return PartialView("_EnrollLeaveGrid", model);
        }
        //Enroll Edit
        public async Task<IActionResult> Edit(Guid EnrollGI)
        {
            try
            {


                await fillDropdown();
                JoiningConfirmation model = new JoiningConfirmation();
                Enrollment model2 = new Enrollment();
                var response = await _commonService.CheckEmptyDropDown("Enroll");
                if (response.Success is false)   //If Any Empty DropDown
                {
                    TempData["Message"] = response.Message;
                    return RedirectToAction("Index", "OnBoarding");
                }
                else
                {
                    model2.PreEnrollment = await _commonService.GetMandatoryFiledValidation("ENROLL");
                    List<FiledValidations> list = model2.PreEnrollment.FiledValidations;
                    model = await _enrollmentService.GetEnrollEditDetails(EnrollGI);
                    model.Enrollment.PreEnrollment.FiledValidations = list;
                    model.Enrollment.AddOnClassification = await _commonService.GetAddOnClassificationApplicability();
                    model.Enrollment.EnrollGUID = EnrollGI;
                    if (model.Enrollment.AttendanceSettings.ShiftDetailsIds != null && model.Enrollment.AttendanceSettings.ShiftDetailsIds.Count() > 0)
                        model.Enrollment.AttendanceSettings.ShiftDetailsId = model.Enrollment.AttendanceSettings.ShiftDetailsIds[0];
                    //model.Enrollment.AttendanceSettings = new AttendanceSettings();
                    ViewBag.St = new SelectList(await _dropdownService.GetStateDropDown(model.Enrollment.PreEnrollment.PermanentAddress.Country.Code), "Code", "Name");
                    ViewBag.Dist = new SelectList(await _dropdownService.GetDistrictDropdown(model.Enrollment.PreEnrollment.PermanentAddress.State.Code), "Code", "Name");
                    model.Enrollment.Breadcrumb = "Employee Activation Edit";
                    //model.BreadcrumbName = BreadcrumbName;
                    model.Enrollment.SelectedTab = "Personal-Info";

                    if (model.Enrollment.PreEnrollment.BasicInfo != null)
                    {
                        ViewBag.FileName = model.Enrollment.PreEnrollment.BasicInfo.File.FileName;
                        var ext = Path.GetExtension(ViewBag.FileName);

                        string dataUri = $"data:image/{ext.Substring(1)};base64,{Convert.ToBase64String(model.Enrollment.PreEnrollment.BasicInfo.File.fdata)}";
                        ViewBag.dataUri = dataUri;
                    }
                    return View("Edit", model.Enrollment);
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
        public async Task<JsonResult> GetNewPosition(int Designation, int Location, int Departmnet)
        {
            var Position = new SelectList(await _dropdownService.GetNewPositionDropdown(Designation, Location, Departmnet), "Code", "Name");
            return Json(Position);
        }
        [HttpGet]
        public async Task<JsonResult> GetReplacementEmployee(int Designation, int Location, int Departmnet)
        {
            var Position = new SelectList(await _dropdownService.GetReplacementEmployeeDropdown(Designation, Location, Departmnet, "Yes"), "Code", "Name");
            return Json(Position);
        }
        [HttpGet]
        public async Task<JsonResult> fillCostCenter(int IncApplicable)
        {
            var CostCenter = new SelectList(await _dropdownService.GetIncentiveCostCenterDropdown(IncApplicable), "Code", "Name");
            return Json(CostCenter);
        }
        [HttpPost]
        public async Task<IActionResult> SalarySummary(Enrollment model)//(Guid PreEnrollGI)//(PreEnrollment model)
        {
            try
            {
                 model = await _IOfferProcessingService.SalaryDetails_enroll(model);
               
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
        public async Task<JsonResult> GetWageGradeVsApplSaleryBand(int WageGradeCode)
        {
            var SlryStruct = new SelectList(await _dropdownService.GetWageGradeVsSalaryBand((int)WageGradeCode), "Code", "Name");

            return Json(SlryStruct);
        }
        public async Task<IActionResult> ViewDocument(Guid EnrollGI)
        {
            try
            {
                Enrollment model = new Enrollment();
                model = await _enrollmentService.GetPreEnrollmentDetails(EnrollGI);
                Shared.File file = new Shared.File();
                file = model.StatutoryClassification.BankFile;

                if (file.fdata == null || file.fdata == null)
                    return BadRequest(new { message = "No File Uploaded" });

                var decompressedBytes = await _helpers.DecompressFile(file.fdata);
                var provider = new FileExtensionContentTypeProvider();
                if (!provider.TryGetContentType(file.FileName, out string contentType))
                {
                    // default if unknown
                    contentType = "application/octet-stream";
                }
                Response.Headers["Content-Disposition"] = $"inline; filename=\"{file.FileName}\"";
                return File(decompressedBytes, contentType);
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", "Account", new { Area = "" });
            }
        }
        private async Task<byte[]> CompressFileAsync(IFormFile file)
        {
            using (var ms = new MemoryStream())
            {
                await file.CopyToAsync(ms);
                using (var compressedStream = new MemoryStream())
                {
                    using (var gzip = new GZipStream(compressedStream, CompressionMode.Compress))
                    {
                        gzip.Write(ms.ToArray(), 0, ms.ToArray().Length);
                    }
                    return compressedStream.ToArray();
                }
            }
        }
    }
}
