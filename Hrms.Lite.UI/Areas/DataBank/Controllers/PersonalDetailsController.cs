using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.DataBank.Generic.PersonalDetails;
using Hrms.Lite.Shared.General;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Spire.Xls;
using Microsoft.AspNetCore.Hosting;
using System.Drawing.Imaging;
using System.Data;
using Newtonsoft.Json;
using Hrms.Lite.Shared.Account;
using System.IO.Compression;
using Microsoft.AspNetCore.StaticFiles;

namespace Hrms.Lite.UI.Areas.DataBank.Controllers
{
    [Area("DataBank")]
    [SessionAuthorize]
    public class PersonalDetailsController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly IPersonalDetailsService _personalDetailsService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        private readonly IBlob _blob;
        private readonly IWebHostEnvironment _environment;
        private readonly ICipherService _cipherService;


        public static class Global
        {
            public static Filter GlobalFilter;
        }
        public PersonalDetailsController(IPersonalDetailsService personalDetailsService, IDropdownService dropdownService, IHelpers helpers, ICommonService commonService,
            IBlob blob, IWebHostEnvironment env, ICipherService cipherService)
        {
            _personalDetailsService = personalDetailsService;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = helpers;
            _blob = blob;
            _environment = env;
            _cipherService = cipherService;
        }
        public async Task <ActionResult> Index()
        {
            try
            {
                PersonalDetailsList pd = new PersonalDetailsList();
                Filter filter = new Filter();
                filter.length = 10;
                ViewBag.Filter = new SelectList(await _dropdownService.GetPersoalDetailsFilterDropdown(), "Code", "Name");
                pd = await _personalDetailsService.GetPersonalDetailsList(0, filter);
                await fillfilterDropdown();
                return View(pd);
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
        public async Task<IActionResult> GetList(Filter filter1, int filterCount)
        {
            try
            {
                PersonalDetailsList model = new PersonalDetailsList();
                Filter filter = new Filter();
                Global.GlobalFilter = filter1;


                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }

                model = await _personalDetailsService.GetPersonalDetailsList(filterCount, filter1);

                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.PersonalDetailsLists.Count > 0)
                {
                    totalrows = model.PersonalDetailsLists[0].active;

 
                }

                
                if (filter1.searchValue != "")
                {
                    if (model.PersonalDetailsLists.Count > 0)
                    {
                        totalrowsafterfiltering = model.PersonalDetailsLists[0].totalRecords;
                    }
                    else
                    {
                        totalrowsafterfiltering = 0;
                    }
                 
                    return Json(new
                    {
                        data = model.PersonalDetailsLists,
                        draw = filter1.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows//totalrowsafterfiltering
                    });
                }
                else
                {
                   
                    return Json(new
                    {
                        data = model.PersonalDetailsLists,
                        draw = filter1.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows
                    });
                }


            }
            catch (Exception ex)
            {
                return View();
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

     
            public async Task <IActionResult> Create(Guid EmployeeGI)
            {
            try
            {
                //await fillDropdown();

                if (TempData["Message"] != null)
                {
                    ViewBag.Message = TempData["Message"].ToString();
                    TempData["Message"] = null;
                }
                GeneralData generalData = new GeneralData();
                generalData.EmpGI = EmployeeGI;
                PersonalDetailsDropDown dropmodel = new PersonalDetailsDropDown();
                dropmodel = await _personalDetailsService.GetPersonalDetailsDropDown();
                PersonalDetails Model = new PersonalDetails();
                //_sharedData.EmpGI= EmployeeGI;
                HttpContext.Session.SetString("CommonData", JsonConvert.SerializeObject(generalData));
               
                ViewBag.Fam = new SelectList(dropmodel.FamilyRelation, "Code", "Name");
                ViewBag.Blo = new SelectList(dropmodel.bloodGroup, "Code", "Name");
                ViewBag.Mar = new SelectList(dropmodel.MaritalStatus, "Code", "Name");
                ViewBag.Rel = new SelectList(dropmodel.Religion, "Code", "Name");
                ViewBag.Cat = new SelectList(dropmodel.ReservationCategory, "Code", "Name");
                ViewBag.Con = new SelectList(dropmodel.Relation, "Code", "Name");
                ViewBag.NomCat = new SelectList(dropmodel.NominationCategory, "Code", "Name");
                ViewBag.Mar = new SelectList(dropmodel.MaritalStatus, "Code", "Name");
                ViewBag.Sta = new SelectList(dropmodel.Status, "Code", "Name");
                ViewBag.Qty = new SelectList(dropmodel.QualificationType, "Code", "Name");
                ViewBag.Yoc = new SelectList(dropmodel.YearOfCompletion, "Code", "Name");
                ViewBag.Hob = new SelectList(dropmodel.Hobbies, "Code", "Name");
                ViewBag.Lan = new SelectList(dropmodel.Languages, "Code", "Name");
                //ViewBag.Ski = new SelectList(dropmodel.Skill, "Code", "Name");
                ViewBag.Sty = new SelectList(await _dropdownService.GetSkillType_Dropdown(), "Code", "Name");
                ViewBag.Skp = new SelectList(dropmodel.SkillProficiency, "Code", "Name");
                ViewBag.Cty = new SelectList(dropmodel.CertificateType, "Code", "Name");
                ViewBag.Cmo = new SelectList(dropmodel.CertificateMode, "Code", "Name");
                ViewBag.Cva = new SelectList(dropmodel.CertificateValidity, "Code", "Name");
                ViewBag.CertificatePolicy = new SelectList(await _dropdownService.GetCertificatePolicyDropdown("H"), "Code", "Name");
                Model = await _personalDetailsService.GetPersonalDetails(EmployeeGI);
                if (Model.FamilyDetails[0].DateOfBirth == Convert.ToDateTime("01-Jan-0001"))
                {
                    Model.FamilyDetails[0].DateOfBirth = null;
                }
               
                ViewBag.Cou = new SelectList(await _dropdownService.GetCountryDropDown(), "Code", "Name");
                ViewBag.St = new SelectList(await _dropdownService.GetStateDropDown(Model.Address.PermanentAddress.Country.Code), "Code", "Name");
                ViewBag.Dist = new SelectList(await _dropdownService.GetDistrictDropdown(Model.Address.PermanentAddress.State.Code), "Code", "Name");
                ViewBag.PrSt = new SelectList(await _dropdownService.GetStateDropDown(Model.Address.PresentAddress.Country.Code), "Code", "Name");
                ViewBag.PrDist = new SelectList(await _dropdownService.GetDistrictDropdown(Model.Address.PresentAddress.State.Code), "Code", "Name");
                ViewBag.Ski = new SelectList(await _dropdownService.GetSkillBasedOnSkillTypeDropdown(0), "Code", "Name");
                ViewBag.Qua = new SelectList(await _dropdownService.GetQualificationDropDown(0), "Code", "Name");
                ViewBag.Spc = new SelectList(await _dropdownService.GetSpecialisationDropDown(0), "Code", "Name");
                Model.EmployeeGI = EmployeeGI;           
                return View("~/Areas/DataBank/Views/PersonalDetails/Create.cshtml", Model);
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
        public async Task<IActionResult> GetFamilyList(Guid EmployeeGI)
        {
            try
            {
                PersonalDetails model = new PersonalDetails();
                model.GetFamilyDetails = new FamilyDetails();
                PersonalDetailsDropDown dropmodel = new PersonalDetailsDropDown();
                dropmodel = await _personalDetailsService.GetPersonalDetailsDropDown();
                model = await _personalDetailsService.GetPersonalDetails(EmployeeGI);
                ViewBag.Fam = new SelectList(dropmodel.FamilyRelation, "Code", "Name");
                ViewBag.Sta = new SelectList(dropmodel.Status, "Code", "Name");
                return PartialView("_FamilyDetails", model);
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

        public async Task<IActionResult> GetCertificateList(Guid EmployeeGI)
        {
            try
            {
                PersonalDetails model = new PersonalDetails();
                PersonalDetailsDropDown dropmodel = new PersonalDetailsDropDown();
                dropmodel = await _personalDetailsService.GetPersonalDetailsDropDown();
                model = await _personalDetailsService.GetPersonalDetails(EmployeeGI);
                ViewBag.Cty = new SelectList(dropmodel.CertificateType, "Code", "Name");
                ViewBag.Cmo = new SelectList(dropmodel.CertificateMode, "Code", "Name");
                ViewBag.Cva = new SelectList(dropmodel.CertificateValidity, "Code", "Name");
                ViewBag.CertificatePolicy = new SelectList(await _dropdownService.GetCertificatePolicyDropdown("H"), "Code", "Name");
              
                return PartialView("_Certifications", model);
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
        public async Task<IActionResult> GetEducationList(Guid EmployeeGI)
        {
            try
            {
                PersonalDetails model = new PersonalDetails();
                PersonalDetailsDropDown dropmodel = new PersonalDetailsDropDown();
                dropmodel = await _personalDetailsService.GetPersonalDetailsDropDown();
                model = await _personalDetailsService.GetPersonalDetails(EmployeeGI);
                ViewBag.Qty = new SelectList(dropmodel.QualificationType, "Code", "Name");
                ViewBag.Yoc = new SelectList(dropmodel.YearOfCompletion, "Code", "Name");
                ViewBag.Qua = new SelectList(await _dropdownService.GetQualificationDropDown(0), "Code", "Name");
                ViewBag.Spc = new SelectList(await _dropdownService.GetSpecialisationDropDown(0), "Code", "Name");
                return PartialView("_Education", model);
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
        public async Task<IActionResult> GetSkillsList(Guid EmployeeGI)
        {
            try
            {
                PersonalDetails model = new PersonalDetails();
                PersonalDetailsDropDown dropmodel = new PersonalDetailsDropDown();
                dropmodel = await _personalDetailsService.GetPersonalDetailsDropDown();
                model = await _personalDetailsService.GetPersonalDetails(EmployeeGI);
                ViewBag.Sty = new SelectList(await _dropdownService.GetSkillType_Dropdown(), "Code", "Name");
                ViewBag.Skp = new SelectList(dropmodel.SkillProficiency, "Code", "Name");
                ViewBag.Ski = new SelectList(await _dropdownService.GetSkillBasedOnSkillTypeDropdown(0), "Code", "Name");
                return PartialView("_Skills", model);
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
        public async Task<IActionResult> GetEmpVsNomineeList(Guid EmployeeGI)
        {
            try
            {
                PersonalDetails model = new PersonalDetails();
                PersonalDetailsDropDown dropmodel = new PersonalDetailsDropDown();
                dropmodel = await _personalDetailsService.GetPersonalDetailsDropDown();
                model = await _personalDetailsService.GetPersonalDetails(EmployeeGI);
                ViewBag.NomCat = new SelectList(dropmodel.NominationCategory, "Code", "Name");
                ViewBag.Fam = new SelectList(dropmodel.FamilyRelation, "Code", "Name");
                return PartialView("_EmployeeVsNominee", model);
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
        public async Task<IActionResult> GetPreviousEmploymentList(Guid EmployeeGI)
        {
            try
            {
                PersonalDetails model = new PersonalDetails();
                PersonalDetailsDropDown dropmodel = new PersonalDetailsDropDown();
                dropmodel = await _personalDetailsService.GetPersonalDetailsDropDown();
                model = await _personalDetailsService.GetPersonalDetails(EmployeeGI);
                return PartialView("_PreviousEmployment", model);
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
     
       
        public IActionResult SaveProfileToFolder(PersonalDetails model)
        {
            GeneralData general = JsonConvert.DeserializeObject<GeneralData>(HttpContext.Session.GetString("CommonData"));
            string path = Path.Combine(this._environment.WebRootPath, "Personal_Profile");

            string fileName = model.enrollment.PreEnrollment.BasicInfo.File.FileData.FileName;

            if (model.enrollment.PreEnrollment.BasicInfo.File.FileData.Length > (1024 * 1024))
            {
                //----------------compress image --------------------------------------
                var Imgstream = model.enrollment.PreEnrollment.BasicInfo.File.FileData.OpenReadStream();
                System.Drawing.Image originalImage = System.Drawing.Image.FromStream(Imgstream);

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
                var stream2 = model.enrollment.PreEnrollment.BasicInfo.File.FileData.OpenReadStream();
                System.Drawing.Image resized = resizeImage(newWidth, newHeight, stream2);
                using var imageStream = new MemoryStream();
                resized.Save(imageStream, ImageFormat.Jpeg);
                var imageBytes = imageStream.ToArray();
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                System.Drawing.Image finalImage = System.Drawing.Image.FromStream(imageStream);
                finalImage.Save(Path.Combine(path, fileName),ImageFormat.Jpeg);
                var ext = Path.GetExtension(fileName);
                ViewBag.JsonData = $"data:image/"+ext.Substring(1)+";base64,{Convert.ToBase64String(imageBytes)}";
                //Global.jsonD = $"data:image/png;base64,{Convert.ToBase64String(imageBytes)}";
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
                    model.enrollment.PreEnrollment.BasicInfo.File.FileData.CopyTo(stream);
                }
            }
            general.SavedFileName = fileName;
            HttpContext.Session.SetString("CommonData", JsonConvert.SerializeObject(general));
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
            var serverpath = Path.Combine(_environment.WebRootPath, "Personal_Profile\\" + fileName);
            var contents = System.IO.File.ReadAllBytes(serverpath);
            var ext = Path.GetExtension(fileName);
            return Json($"data:image/{ext.Substring(1)};base64,{Convert.ToBase64String(contents)}");


        }
        [AcceptVerbs("Post")]
        public async Task<ActionResult> SaveProfilePicFromKendo(string contentType, string base64,string fileName)
        {
            GeneralData general = JsonConvert.DeserializeObject<GeneralData>(HttpContext.Session.GetString("CommonData"));
            var serverpath = Path.Combine(_environment.WebRootPath, "Personal_Profile\\" + general.SavedFileName);
            try
            {
            var fileContents = Convert.FromBase64String(base64);
            PersonalDetails model = new PersonalDetails();
            PersonalDetails model1 = new PersonalDetails();
            model.enrollment = new Enrollment();
            model.enrollment.PreEnrollment = new PreEnrollment();
            model.enrollment.PreEnrollment.BasicInfo = new Shared.DataBank.Generic.EmployeeBasicInfo();
            model.enrollment.PreEnrollment.BasicInfo.File = new Shared.File();
            model.enrollment.PreEnrollment.BasicInfo.File.fdata = fileContents; 
            model.enrollment.PreEnrollment.BasicInfo.File.FileName = $"{Guid.NewGuid().ToString()}{Path.GetExtension(fileName)}"; 
            model.enrollment.PreEnrollment.BasicInfo.File.ContentType = contentType;
            model.enrollment.PreEnrollment.BasicInfo.File.FileExtension = Path.GetExtension(fileName);
            model.EmployeeGI = general.EmpGI;
                model.TabIndex = "ProfilePhoto";
              
                    

             
                var response = await _personalDetailsService.SavePersonalDetails(model);

                if (response.Success == true)
            {
                    TempData["Message"] = "Saved Sucessfully";
            }
            else
            {
              TempData["Message"] = response.Message;
            

            }
            if (System.IO.File.Exists(serverpath))
            {
                System.IO.File.Delete(serverpath);
            }
            return RedirectToAction("Create", new { EmployeeGI = general.EmpGI });
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
        public async Task<IActionResult> Create(PersonalDetails model)
        {
            try
            {
                if (model.TabIndex == "Certifications")
                {
                    if (model.GetCertifications.File != null)
                    {
                        if (model.GetCertifications.File.FileData != null)
                        {
                            model.GetCertifications.File.fdata = await _helpers.CompressFile(model.GetCertifications.File.FileData);
                            model.GetCertifications.File.FileName = model.GetCertifications.File.FileData.FileName;
                            model.GetCertifications.File.FileExtension = Path.GetExtension(model.GetCertifications.File.FileName);
                            model.GetCertifications.File.FileData = null;

                        }
                    }
                }
                if (model.TabIndex == "Education")
                {
                    if (model.GetEducation.File != null)
                    {
                        if (model.GetEducation.File.FileData != null)
                        {
                            model.GetEducation.File.fdata = await _helpers.CompressFile(model.GetEducation.File.FileData);
                            model.GetEducation.File.FileName = model.GetEducation.File.FileData.FileName;
                            model.GetEducation.File.FileExtension = Path.GetExtension(model.GetEducation.File.FileName);
                            model.GetEducation.File.FileData = null;

                        }
                    }
                }

                if (model.TabIndex == "Previous Employment")
                {
                    if (model.GetPreviousEmployment.File != null)
                    {
                        if (model.GetPreviousEmployment.File.FileData != null)
                        {
                            model.GetPreviousEmployment.File.fdata = await _helpers.CompressFile(model.GetPreviousEmployment.File.FileData);
                            model.GetPreviousEmployment.File.FileName = model.GetPreviousEmployment.File.FileData.FileName;
                            model.GetPreviousEmployment.File.FileExtension = Path.GetExtension(model.GetPreviousEmployment.File.FileName);
                            model.GetPreviousEmployment.File.FileData = null;
                        }
                    }
                }
                var response = await _personalDetailsService.SavePersonalDetails(model);
                if (response.Success == true)
                {
                    if (model.SliderName == "Modify")
                    {
                        response.Message = "Modified Successfully";
                        return Json(response);
                    }
                    else
                    {
                        response.Message = "Saved Successfully";
                        return Json(response);
                    }
                }

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


        [HttpPost]
        public async Task<IActionResult> familyCreate(string Name,int relation,DateTime? dob,string occupation,int status,string Tabindex,Guid EmpGI,Guid fmlyGI)
        {
            PersonalDetails model = new PersonalDetails();
            model.EmployeeGI = EmpGI;
            model.TabIndex = Tabindex;
            model.GetFamilyDetails = new FamilyDetails();
            model.GetFamilyDetails.Relation = new Shared.DataBank.Generic.Master.FamilyRelation();
            model.GetFamilyDetails.Status = new Shared.DataBank.Generic.Master.LivingStatus();
            model.GetFamilyDetails.Name = Name;
            model.GetFamilyDetails.Relation.Code = relation;
           
                model.GetFamilyDetails.DateOfBirth = dob;
           

            model.GetFamilyDetails.Occupation = occupation;
            model.GetFamilyDetails.Status.Code = status;
            model.GetFamilyDetails.FamilyLogGI = fmlyGI;
            var response = await _personalDetailsService.SavePersonalDetails(model);
            if (response.Success == true)
            {
                if (Guid.Parse("00000000-0000-0000-0000-000000000000") != fmlyGI)
                {
                    response.Message = "Modified Successfully";
                    return Json(response);
                }
                else
                {
                    response.Message = "Saved Successfully";
                    return Json(response);
                }
            }
            return Json(response);
        }
        
        

           


        [HttpGet]
        public async Task<IActionResult> ViewDocument(int Code, string DocType)
        {
            try
            {
                var fileData = await _personalDetailsService.ViewDocument(Code, DocType);

                if (fileData == null || fileData.fdata == null || fileData.fdata.Length == 0)
                {
                    // This returns a plain text message to the browser
                    return Content("NO DATA FOUND");
                }

                var fileBytes = await _helpers.DecompressIfNeeded(fileData.fdata);


                var provider = new FileExtensionContentTypeProvider();
                if (!provider.TryGetContentType(fileData.FileName, out string contentType))
                {
                    // default if unknown
                    contentType = "application/octet-stream";
                }
                Response.Headers["Content-Disposition"] = $"inline; filename=\"{fileData.FileName}\"";

                return File(fileBytes, contentType);
            }
            catch (Exception ex)
            {
                // Log exception here if needed
                return StatusCode(500, "Error while downloading file.");
            }
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
        public async Task<JsonResult> GetSkill(int id)
        {
            var Skill = new SelectList(await _dropdownService.GetSkillBasedOnSkillTypeDropdown(id), "Code", "Name");
            return Json(Skill);
        }

        [HttpGet]
        public async Task<JsonResult> GetQualification(int id)
        {
            var Qualification = new SelectList(await _dropdownService.GetQualificationDropDown(id), "Code", "Name");
            return Json(Qualification);
        }
        [HttpGet]
        public async Task<JsonResult> GetSpecialisation(int id)
        {
            var Specialisation = new SelectList(await _dropdownService.GetSpecialisationDropDown(id), "Code", "Name");
            return Json(Specialisation);
        }
       

       public async Task<IActionResult> EducationSave(Guid LogGI,Guid EmployeeGI)
        {
           
                PersonalDetails model = new PersonalDetails();
                PersonalDetailsDropDown dropmodel = new PersonalDetailsDropDown();
                dropmodel = await _personalDetailsService.GetPersonalDetailsDropDown();
            ViewBag.Qty = new SelectList(dropmodel.QualificationType, "Code", "Name");
            ViewBag.Yoc = new SelectList(dropmodel.YearOfCompletion, "Code", "Name");
            if (LogGI != Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {
                model = await _personalDetailsService.GetPersonalDetailsEducation(LogGI);
        
              model.SliderName = "Modify";
                ViewBag.Qua = new SelectList(await _dropdownService.GetQualificationDropDown(model.GetEducation.QualificationType.Code), "Code", "Name");
                ViewBag.Spc = new SelectList(await _dropdownService.GetSpecialisationDropDown(model.GetEducation.Qualification.Code), "Code", "Name");

            }

            else
            {

                ViewBag.Qua = new SelectList(await _dropdownService.GetQualificationDropDown(0), "Code", "Name");
                ViewBag.Spc = new SelectList(await _dropdownService.GetSpecialisationDropDown(0), "Code", "Name");
                model.SliderName = "Save";
            }
          
            model.EmployeeGI = EmployeeGI;
            return PartialView("_EducationSave", model);
        }
        public async Task<IActionResult> certificatesave(int TrnCode, Guid EmployeeGI)
        {

            PersonalDetails model = new PersonalDetails();
          
            PersonalDetailsDropDown dropmodel = new PersonalDetailsDropDown();
            model.GetCertifications = new Certifications();
            dropmodel = await _personalDetailsService.GetPersonalDetailsDropDown();
            ViewBag.CertificatePolicy = new SelectList(await _dropdownService.GetCertificatePolicyDropdown("H"), "Code", "Name");
            model = await _personalDetailsService.GetPersonalDetailsCertificate(TrnCode);
           
            model.SliderName = "Save";
            if (TrnCode >0)
            {
                model.SliderName = "Modify";
            }
            model.EmployeeGI = EmployeeGI;
            ViewBag.Cty = new SelectList(dropmodel.CertificateType, "Code", "Name");
            ViewBag.Cmo = new SelectList(dropmodel.CertificateMode, "Code", "Name");
            ViewBag.Cva = new SelectList(dropmodel.CertificateValidity, "Code", "Name");
            model.GetCertifications.File.ActualFileName = model.GetCertifications.File.FileName;
            model.GetCertifications.File.FileName = model.GetCertifications.File.FileName != null ? _cipherService.EncryptWithCustomKey(model.GetCertifications.File.FileName) : null;
            return PartialView("_CertificationSave", model);
        }
        public async Task<IActionResult> EmpVsNomineeSave(int NomCatCode,int RelationCode, Guid EmployeeGI)
        {

            PersonalDetails model = new PersonalDetails();
            PersonalDetailsDropDown dropmodel = new PersonalDetailsDropDown();
            dropmodel = await _personalDetailsService.GetPersonalDetailsDropDown();
            ViewBag.NomCat = new SelectList(dropmodel.NominationCategory, "Code", "Name");
            ViewBag.Fam = new SelectList(dropmodel.FamilyRelation, "Code", "Name");
            model = await _personalDetailsService.GetPersonalDetailsEmpVsNominee(NomCatCode,  RelationCode,  EmployeeGI);
            model.SliderName = "Save";
            if (NomCatCode > 0)
            {
                model.SliderName = "Modify";
            }
            model.EmployeeGI = EmployeeGI;
            return PartialView("_EmployeeVsNomineeSave", model);
        }
        public async Task<IActionResult> GetSkills(Guid LogGI, Guid EmployeeGI)
        {
            try
            {
                PersonalDetails model = new PersonalDetails();
                PersonalDetailsDropDown dropmodel = new PersonalDetailsDropDown();
                dropmodel = await _personalDetailsService.GetPersonalDetailsDropDown();
                if (LogGI != Guid.Parse("00000000-0000-0000-0000-000000000000"))
                {
                    model = await _personalDetailsService.GetPersonalDetailsSkill(LogGI);
                    ViewBag.Ski = new SelectList(await _dropdownService.GetSkillBasedOnSkillTypeDropdown(model.GetSkills.SkillType.Code.Value), "Code", "Name");
                    model.SliderName = "Modify";
                }
                else
                {
                    model.SliderName = "Save";
                    ViewBag.Ski = new SelectList(await _dropdownService.GetSkillBasedOnSkillTypeDropdown(0), "Code", "Name");

                }
                model.EmployeeGI = EmployeeGI;
                ViewBag.Sty = new SelectList(await _dropdownService.GetSkillType_Dropdown(), "Code", "Name");
                ViewBag.Skp = new SelectList(dropmodel.SkillProficiency, "Code", "Name");

                return PartialView("_SkillSave", model);
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

        public async Task<IActionResult> FamilySave(Guid LogGI, Guid EmployeeGI)
        {
           
                PersonalDetails model = new PersonalDetails();
                PersonalDetailsDropDown dropmodel = new PersonalDetailsDropDown();
                dropmodel = await _personalDetailsService.GetPersonalDetailsDropDown();
                model = await _personalDetailsService.GetPersonalDetailsFamily(LogGI);
                model.SliderName = "Save";
                if (LogGI != Guid.Parse("00000000-0000-0000-0000-000000000000"))
                {
                    model.SliderName = "Modify";
                }
                model.EmployeeGI = EmployeeGI;
                ViewBag.Fam = new SelectList(dropmodel.FamilyRelation, "Code", "Name");
                ViewBag.Sta = new SelectList(dropmodel.Status, "Code", "Name");

                return PartialView("_FamilyDetailsSave", model);
           
        }
        public async Task<IActionResult> PrevEmploymentSave(Guid LogGI, Guid EmployeeGI)
        {
            
            PersonalDetails model = new PersonalDetails();
            model = await _personalDetailsService.GetPersonalDetailsPrevEmployment(LogGI);
            model.SliderName = "Save";
            if (LogGI != Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {
                model.SliderName = "Modify";
            }
            model.EmployeeGI = EmployeeGI;
            return PartialView("_PreviousEmploymentSave", model);
           
        }
        
        [HttpPost]
        public async Task<IActionResult> EducationDelete(Guid LogGI)
        {
            
                var response = await _personalDetailsService.DeletePersonalDetailsEducation(LogGI);
            if (response.Success == true)
            {
                
                    response.Message = "Deleted Successfully";
                    return Json(response);
                
            }
            return Json(response);
        }


        [HttpPost]
        public async Task<IActionResult> SkillDelete(Guid LogGI)
        {

            var response = await _personalDetailsService.DeletePersonalDetailsSkill(LogGI);
            if (response.Success == true)
            {

                response.Message = "Deleted Successfully";
                return Json(response);

            }
            return Json(response);
        }
        [HttpPost]
        public async Task<IActionResult> CertificateDelete(int TrnCode)
        {

            var response = await _personalDetailsService.DeletePersonalDetailsCertificate(TrnCode);
            if (response.Success == true)
            {

                response.Message = "Deleted Successfully";
                return Json(response);

            }
            return Json(response);
        }
        [HttpPost]
        public async Task<IActionResult> EmpVsNomineeDelete(int NomcatCode,int RelationCode,Guid EmployeeGI)
        {

            var response = await _personalDetailsService.DeletePersonalDetailsEmpVsNominee( NomcatCode,  RelationCode,  EmployeeGI);
            if (response.Success == true)
            {

                response.Message = "Deleted Successfully";
                return Json(response);

            }
            return Json(response);
        }
        [HttpPost]
        public async Task<IActionResult> FamilyDelete(Guid LogGI)
        {
            
            var response = await _personalDetailsService.DeletePersonalDetailsFamilyDetails(LogGI);
            if (response.Success == true)
            {

                response.Message = "Deleted Successfully";
                return Json(response);

            }
            return Json(response);
        }
        public async Task<IActionResult> PrevEmploymentDelete(Guid LogGI)
        {
           
            var response = await _personalDetailsService.DeletePersonalDetailsPrevEmployment(LogGI);
            if (response.Success == true)
            {

                response.Message = "Deleted Successfully";
                return Json(response);

            }
            return Json(response);
        }
        public async Task<IActionResult> ExportToExcel(int SearchId, int TotRecords)
        {
            try
            {

                if (Global.GlobalFilter == null)
                    Global.GlobalFilter = new Filter();
                else
                {
                    Global.GlobalFilter.length = TotRecords;
                    Global.GlobalFilter.start = 0;
                }
                string FilterValues = await _helpers.GetFilterValues(Global.GlobalFilter);

                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Personal Details List", "Excel");

                if (FilterValues == "")
                {
                    FilterValues = "All";
                }

                string currDateTime = await _helpers.GetCurrentDate();
                

                //Get Datset
                var dataset = await _personalDetailsService.GetPersonalDetailsList_Excel(Global.GlobalFilter, SearchId);
                DataTable dt = dataset.Tables[0];


                //set EXCEL Column Heading 
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (dt.Columns[i].ColumnName == "doj" || dt.Columns[i].ColumnName == "dob" || dt.Columns[i].ColumnName == "y/s")
                        dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.ToUpper();
                    else
                        dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
                }

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Personal Details List");
                    int count = dt.Rows.Count + 5;


                    worksheet.Range("A1:Q" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:Q" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:Q" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:Q" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:Q" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 16)));
                    wsReportNameHeaderRange.Style.Font.Bold = true;
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Merge();
                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191); ;
                    wsReportNameHeaderRange.Value = "Personal Details List";

                    worksheet.Cell(2, 1).Style.Font.Bold = true;
                    worksheet.Cell(2, 1).Value = "Group Company Name";
                    worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                    worksheet.Cell(3, 1).Style.Font.Bold = true;
                    worksheet.Cell(3, 1).Value = "Login User";
                    worksheet.Cell(3, 2).Value = LoginUser;
                    worksheet.Cell(3, 3).Style.Font.Bold = true;
                    worksheet.Cell(3, 3).Value = "Date & Time";
                    worksheet.Cell(3, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(3, 4).Style.DateFormat.Format = "dd-MMM-yyyy   h:mm AM/PM";
                    worksheet.Cell(3, 4).Value = currDateTime.ToString();
                    worksheet.Cell(3, 5).Style.Font.Bold = true;
                    worksheet.Cell(3, 5).Value = "Total Count";
                    worksheet.Cell(3, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(3, 6).Value = dt.Rows.Count;
                    worksheet.Cell(4, 1).Style.Font.Bold = true;
                    worksheet.Cell(4, 1).Value = "Filter Applied";
                    worksheet.Range("B4:Q4").Merge();
                    worksheet.Cell("B4").Value = FilterValues;



                    var currentRow = 5;

                    worksheet.Cell(currentRow, 1).InsertTable(dt);
                    worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                    worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                    worksheet.Range("A5:Q5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Range("A5:Q5").Style.Font.Bold = true;


                    worksheet.Columns().AdjustToContents();
                    worksheet.Rows().AdjustToContents();
                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        if (EP.NeedPassword)
                        {
                            Workbook book = new Workbook();
                            book.LoadFromStream(stream);
                            //Protect Workbook
                            book.Protect(EP.Password,false,false);
                            book.SaveToStream(stream);
                        }
                        var content = stream.ToArray();
                        return File(
                            content,
                            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                            "Personal Details List.xlsx"
                            );
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
        public async Task<IActionResult> GetPopUpforCertiDocView(int LogCode, Guid EmployeeGI)
        {
            try
            {

                PersonalDetails model = new PersonalDetails();
                model.CertificationDocList = await _personalDetailsService.GetCertificationDocumentList( LogCode);
                return View("_CertificateUploadPopUp", model);
            }

            catch (Exception ex)
            {
                string ErrorMessage = ex.Message.ToString();
                if (ex.InnerException != null)
                {
                    if (ex.InnerException.Message != null)
                    {

                        ErrorMessage = ErrorMessage + ex.InnerException.Message.ToString();
                    }
                }
                ViewData["Error"] = ErrorMessage;
                return View("ShowError");
            }
        }

    }
}
