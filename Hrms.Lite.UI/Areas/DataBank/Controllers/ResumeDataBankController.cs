using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.Account;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.DataBank.Generic.PersonalDetails;
using Hrms.Lite.Shared.General;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using SkiaSharp;
using Spire.Xls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.DataBank.Controllers
{
    [Area("DataBank")]
    [SessionAuthorize]
    public class ResumeDataBankController : Controller
    {
        private readonly IResumeDataBankService _resumeDataBankService;
        private readonly IPersonalDetailsService _personalDetailsService;
        private readonly IApplicantTrackingService _applicantTrackingService;
        private readonly IDropdownService _dropdownService;
        private readonly ICommonService _commonService;
        private readonly IBlob _blob;
        private IHostingEnvironment Environment;
        private readonly IHelpers _helpers;
        public static class Global
        {
            public static ResumeFilter GlobalFilter;
            public static string SavedFileName;
            public static ResumeDataBank model;
            public static bool isRefreshed = true;
            public static string SelectedTab;
        }
        public ResumeDataBankController(IResumeDataBankService resumeDataBankService, IDropdownService dropdownService, ICommonService commonService, IBlob blob, IPersonalDetailsService personalDetailsService, IApplicantTrackingService applicantTrackingService, IHostingEnvironment _environment, IHelpers Helpers)
        {
            _resumeDataBankService = resumeDataBankService;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _personalDetailsService = personalDetailsService;
            _applicantTrackingService = applicantTrackingService;
            _blob = blob;
            Environment = _environment;
            _helpers = Helpers;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                ResumeDataBank model = new ResumeDataBank();
                model.Filter = new ResumeFilter();
                // model = await _resumeDataBankService.GetResumeDataBankListt(1,null,null, model.Filter);
                model.ResumeDataBankListTabs = (List<ResumeDataBankListTabs>)await _resumeDataBankService.GetResumeDataBankTabs();
                DateTime now = DateTime.Now;
                model.From = new DateTime(now.Year, now.Month, 1);
                model.To = model.From.Value.AddMonths(1).AddDays(-1);

                await fillfilterDropdown();
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

        public async Task<IActionResult> GetResumeList(int TabIndex, DateTime? FROMDATE, DateTime? TODATE)
        {
            try
            {
                ResumeDataBank model = new ResumeDataBank();
                if (TabIndex != 5)
                    model = await _resumeDataBankService.GetResumeDataBankList(TabIndex, null, null);
                else
                    model = await _resumeDataBankService.GetResumeDataBankList(TabIndex, FROMDATE, TODATE);
                model.TabIndex = TabIndex;
                if (TabIndex == 1)
                    return PartialView("_ResumeList", model);
                else if (TabIndex == 2)  //2=in progress, and 4=reject
                    return PartialView("_RecruitmentList", model);
                else if (TabIndex == 4)  //2=in progress, and 4=reject
                    return PartialView("_RejectList", model);
                else if (TabIndex == 5)
                    return PartialView("_JoinedList", model);
                else
                    return PartialView("_HoldList", model);

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
        public async Task<IActionResult> GetResumeRepoList(ResumeFilter filter, int TabIndex, DateTime? FROMDATE, DateTime? TODATE)
        {
            try
            {
                ResumeDataBank model = new ResumeDataBank();
                ResumeDataBank m = new ResumeDataBank();
                int all;
                int CountTab1;
                int CountTab2;
                int CountTab3;
                int CountTab4;
                int CountTab5;
                //  Filter filter = new Filter();
                if (TabIndex != 5)
                    model = await _resumeDataBankService.GetResumeDataBankListt(TabIndex, null, null, filter);
                else
                    model = await _resumeDataBankService.GetResumeDataBankListt(TabIndex, FROMDATE, TODATE, filter);

                if (filter.searchValue == "undefined" || filter.searchValue == null)
                {
                    filter.searchValue = "";
                }


                {
                    m.ResumeDataBank_List = model.ResumeDataBank_List;
                }
                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.ResumeDataBank_List == null || model.ResumeDataBank_List.Count() == 0)
                {

                    return Json(new
                    {
                        data = "",
                        draw = filter.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows


                    });
                }


                if (model.ResumeDataBank_List.Count > 0)
                {
                    m.ResumeDataBank_List = model.ResumeDataBank_List;
                    totalrows = m.ResumeDataBank_List[0].active;
                    m.AllTabCount = model.AllTabCount;
                    all = m.AllTabCount;
                    CountTab1 = model.Tab1Count;
                    CountTab2 = model.Tab2Count;
                    CountTab3 = model.Tab3Count;
                    CountTab4 = model.Tab4Count;
                    CountTab5 = model.Tab5Count;


                }


                if (filter.searchValue != "")
                {
                    totalrowsafterfiltering = m.ResumeDataBank_List[0].totalRecords;
                    return Json(new
                    {
                        data = model.ResumeDataBank_List,
                        draw = filter.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows,//totalrowsafterfiltering
                        all = m.AllTabCount,
                        CountTab1 = model.Tab1Count,
                        CountTab2 = model.Tab2Count,
                        CountTab3 = model.Tab3Count,
                        CountTab4 = model.Tab4Count,
                        CountTab5 = model.Tab5Count



                    });
                }
                else
                {
                    return Json(new
                    {
                        data = model.ResumeDataBank_List,
                        draw = filter.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows,
                        all = model.AllTabCount,
                        CountTab1 = model.Tab1Count,
                        CountTab2 = model.Tab2Count,
                        CountTab3 = model.Tab3Count,
                        CountTab4 = model.Tab4Count,
                        CountTab5 = model.Tab5Count


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
            PersonalDetailsDropDown dropmodel = new PersonalDetailsDropDown();
            dropmodel = await _personalDetailsService.GetPersonalDetailsDropDown();
            ViewBag.CertType = new SelectList(dropmodel.CertificateType, "Code", "Name");
            ViewBag.Dsg = new SelectList(await _dropdownService.GetDesignationDropdown(), "Code", "Name");
            ViewBag.Loc = new SelectList(await _dropdownService.GetLocationDropdown(), "Code", "Name");
            ViewBag.Skills = new SelectList(await _dropdownService.GetSkillDropdown(), "Code", "Name");
            ViewBag.Qualifications = new SelectList(await _dropdownService.GetAllQualificationDropDown(), "Code", "Name");
            ViewBag.Districts = new SelectList(await _dropdownService.GetDistrictDropdown(1039), "Code", "Name");
            ViewBag.MaritalStatus = new SelectList(await _dropdownService.GetMaritalStatusDropdown(), "Code", "Name");
            ViewBag.Gender = new SelectList(await _dropdownService.GetGenderDropDown(), "Code", "Name");
            ViewBag.ResumeSource = new SelectList(await _dropdownService.GetResumeSourceDropdown(), "Code", "Name");
            ViewBag.Operators = new SelectList(await _dropdownService.GetOperators(), "Code", "Name");
        }
        public async Task<IActionResult> ApplyFilter(ResumeFilter filter, int TabIndex, DateTime? FROMDATE, DateTime? TODATE)
        {
            try
            {
                ResumeDataBank model = new ResumeDataBank();
                Global.GlobalFilter = filter;
                //if (TabIndex != 5)
                //    model = await _resumeDataBankService.GetResumeDataBankListt(TabIndex, null, null,filter);
                //else
                //    model = await _resumeDataBankService.GetResumeDataBankListt(TabIndex, FROMDATE, TODATE, filter);
                model.TabIndex = TabIndex;
                if (TabIndex == 1)
                    return PartialView("_ResumeList", model);
                else if (TabIndex == 2)  //2=in progress, and 4=reject
                    return PartialView("_RecruitmentList", model);
                else if (TabIndex == 4)  //2=in progress, and 4=reject
                    return PartialView("_RejectList", model);
                else if (TabIndex == 5)
                    return PartialView("_JoinedList", model);
                else if (TabIndex == 6)
                    return PartialView("_ClosedList", model);
                else
                    return PartialView("_HoldList", model);
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


        public IActionResult SaveProfileToFolder(ResumeDataBank model, string SelectedTab)
        {
            Global.model = model;
            Global.SelectedTab = SelectedTab;
            string path = Path.Combine(this.Environment.WebRootPath, "Personal_Profile");

            string fileName = model.Details.CandidatesPhoto.FileData.FileName;

            var Imgstream = model.Details.CandidatesPhoto.FileData.OpenReadStream();
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
            if (model.Details.CandidatesPhoto.FileData.Length > (1024 * 1024) || (originalImage.Width > 1920 && originalImage.Height > 1080))
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
                var stream2 = model.Details.CandidatesPhoto.FileData.OpenReadStream();
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
                    model.Details.CandidatesPhoto.FileData.CopyTo(stream);
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
            return Json($"data:image/png;base64,{Convert.ToBase64String(contents)}");

        }

        [AcceptVerbs("Post")]
        public async Task<ActionResult> SaveProfilePicFromKendo(string contentType, string base64, string fileName)
        {
            var serverpath = Path.Combine(Environment.WebRootPath, "Personal_Profile\\" + Global.SavedFileName);
            try
            {
                var fileContents = Convert.FromBase64String(base64);
                ResumeDataBank model = new ResumeDataBank();
                model.Details = new ResumeDataBank_Details();
                model.Details.CandidatesPhoto = new Shared.File();
                model.Details.CandidatesPhoto.fdata = fileContents;
                model.Details.CandidatesPhoto.FileName = fileName;
                model.Details.CandidatesPhoto.ContentType = contentType;
                model.Details.CandidatesPhoto.FileExtension = Path.GetExtension(fileName);



                var StorageType = await _commonService.GetFileStorageType();
                if (model.Details.CandidatesPhoto != null)
                {
                    if (model.Details.CandidatesPhoto.fdata != null)
                    {
                        if (StorageType.Data == "BLOB")
                        {

                            var fileobj = await _commonService.UploadByteArrayToBLOB(model.Details.CandidatesPhoto, "ProfilePhoto");
                            if (fileobj.Success == false)
                            {
                                fileobj.Message = "FILE UPLOAD ERROR";
                                return Json(fileobj);
                            }
                            model.Details.CandidatesPhoto = fileobj.Data;

                        }
                        else if (StorageType.Data == "IIS")
                        {
                            var fileobj = await _commonService.UploadByteArrayToIIS(model.Details.CandidatesPhoto, "ProfilePhoto");
                            if (fileobj.Success == false)
                            {
                                fileobj.Message = "FILE UPLOAD ERROR";
                                return Json(fileobj);
                            }
                            model.Details.CandidatesPhoto = fileobj.Data;

                        }
                        else
                        {
                            var fileobj = await _commonService.UploadByteArrayToAWS(model.Details.CandidatesPhoto, "ProfilePhoto");
                            if (fileobj.Success == false)
                            {
                                fileobj.Message = "FILE UPLOAD ERROR";
                                return Json(fileobj);
                            }
                            model.Details.CandidatesPhoto = fileobj.Data;

                        }
                    }
                }



                Global.model.Details.CandidatesPhoto = model.Details.CandidatesPhoto;

                TempData["Message"] = "Uploaded Sucessfully";


                if (System.IO.File.Exists(serverpath))
                {
                    System.IO.File.Delete(serverpath);
                }
                if (Global.model != null)
                    Global.isRefreshed = false;
                else
                    Global.isRefreshed = true;

                return RedirectToAction("Create", new { isNewUpload = true });
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

        public async Task<IActionResult> Create(bool isNewUpload)
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
                    ResumeDataBank model = new ResumeDataBank();
                    model.FresherOrExperienced = 1;
                    model.Details = new ResumeDataBank_Details();
                    model.Certifications = new List<ResumeDataBank_Certifications>();
                    model.Skill = new List<ResumeDataBank_Skill>();
                    model.Experience = new List<ResumeDataBank_Experience>();
                    model.Education = new List<ResumeDataBank_Education>();
                    model.Certifications.Add(new ResumeDataBank_Certifications());
                    model.Experience.Add(new ResumeDataBank_Experience());
                    model.Skill.Add(new ResumeDataBank_Skill());
                    model.Education.Add(new ResumeDataBank_Education());
                    await fillDropdown();
                    await fillDistrict(0);
                    await fillState(0);


                    var RefNo = HttpContext.Session.GetString("RefNumber");
                    if (RefNo == "" || RefNo == null)
                    {
                        var response = await _commonService.GetReferenceNumber("Resume Databank");
                        RefNo = response.Data;
                        HttpContext.Session.SetString("RefNumber", RefNo);

                    }
                    model.Details.RefNo = RefNo;
                    foreach (var item in model.Education)
                    {
                        item.QualifnDropdown = await _dropdownService.GetQualificationDropDown(0);
                    }
                    foreach (var item in model.Education)
                    {
                        item.speznDropdown = await _dropdownService.GetSpecialisationDropDown(0);
                    }
                    foreach (var item in model.Skill)
                    {
                        item.SkillDropdown = await _dropdownService.GetSkillBasedOnSkillTypeDropdown(0);
                    }
                    model.Mode = "CREATE";
                    model.SelectedTab = "Rd_Basic";
                    model.Details.CandidatesPhoto = new Shared.File();

                    return View("Create", model);
                }
                else
                {
                    Global.isRefreshed = true;
                    ResumeDataBank model = new ResumeDataBank();
                    await fillDropdown();
                    if (Global.model != null)
                    {
                        model = Global.model;
                        await fillDistrict(Global.model.Details.State.Code);
                        await fillState(Global.model.Details.Country.Code);
                    }
                    foreach (var item in model.Education)
                    {
                        if (item.QualifnDropdown == null)
                        {
                            item.QualifnDropdown = await _dropdownService.GetQualificationDropDown(0);
                        }
                    }
                    foreach (var item in model.Education)
                    {
                        if (item.speznDropdown == null)
                        {
                            item.speznDropdown = await _dropdownService.GetSpecialisationDropDown(0);
                        }
                    }

                    foreach (var item in model.Skill)
                    {
                        if (item.SkillDropdown == null)
                        {
                            item.SkillDropdown = await _dropdownService.GetSkillBasedOnSkillTypeDropdown(0);
                        }
                    }


                    model.Mode = "CREATE";
                    model.SelectedTab = Global.SelectedTab;
                    Global.model = null;
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
        public async Task<IActionResult> Edit(Guid ResumeDataBankGI)
        {
            ResumeDataBank model = new ResumeDataBank();
            model = await _resumeDataBankService.GetResumeDataBankDetails(ResumeDataBankGI);
            await fillDropdown();
            await fillDistrict(model.Details.State.Code);
            await fillState(model.Details.Country.Code);



            if (model.Certifications.Count == 0)
            {
                model.Certifications = new List<ResumeDataBank_Certifications>();
                model.Certifications.Add(new ResumeDataBank_Certifications());
            }
            if (model.Skill.Count == 0)
            {
                model.Skill = new List<ResumeDataBank_Skill>();
                model.Skill.Add(new ResumeDataBank_Skill());
            }

            if (model.Experience.Count == 0)
            {

                model.Experience = new List<ResumeDataBank_Experience>();
                model.Experience.Add(new ResumeDataBank_Experience());
            }
            if (model.Education.Count == 0)
            {

                model.Education = new List<ResumeDataBank_Education>();

                model.Education.Add(new ResumeDataBank_Education());
            }
            foreach (var item in model.Education)
            {
                if (item.QualificationType == null)
                {

                    item.QualificationType = new Shared.DataBank.Generic.Master.QualificationType();
                    item.QualificationType.Code = 0;
                }

                item.QualifnDropdown = await _dropdownService.GetQualificationDropDown(item.QualificationType.Code);
            }
            foreach (var item in model.Education)
            {
                if (item.Qualification == null)
                {
                    item.Qualification = new Shared.DataBank.Generic.Master.Qualification();
                    item.Qualification.Code = 0;
                }
                item.speznDropdown = await _dropdownService.GetSpecialisationDropDown(item.Qualification.Code);
            }
            foreach (var item in model.Skill)
            {
                if (item.SkillType == null)
                {
                    item.SkillType = new Shared.DataBank.Generic.Master.SkillType();
                    item.SkillType.Code = 0;
                }
                item.SkillDropdown = await _dropdownService.GetSkillBasedOnSkillTypeDropdown(item.SkillType.Code.Value);
            }
            model.Mode = "MODIFY";

            return View("Create", model);
        }

        [HttpPost]
        public async Task<IActionResult> Save(ResumeDataBank model)
        {
            string filename1 = "";
            string filename2 = "";
            var StorageType = await _commonService.GetFileStorageType();

            try
            {
                if (model.Details.Resume.FileData != null)
                {
                    if (model.Details.Resume.FileData != null)
                    {
                        if (StorageType.Data == "BLOB")
                        {

                            var fileobj = await _commonService.UploadIformFileToBLOB(model.Details.Resume, "Documents");
                            if (fileobj.Success == false)
                            {
                                fileobj.Message = "FILE UPLOAD ERROR";
                                return Json(fileobj);
                            }
                            model.Details.Resume = fileobj.Data;

                        }
                        else if (StorageType.Data == "IIS")
                        {
                            var fileobj = await _commonService.UploadIformFileToIIS(model.Details.Resume, "Documents");
                            if (fileobj.Success == false)
                            {
                                fileobj.Message = "FILE UPLOAD ERROR";
                                return Json(fileobj);
                            }
                            model.Details.Resume = fileobj.Data;

                        }
                        else
                        {
                            var fileobj = await _commonService.UploadIformFileToAWS(model.Details.Resume, "Documents");
                            if (fileobj.Success == false)
                            {
                                fileobj.Message = "FILE UPLOAD ERROR";
                                return Json(fileobj);
                            }
                            model.Details.Resume = fileobj.Data;

                        }
                    }
                }



                var response = await _resumeDataBankService.ResumeDataBankSave(model);
                if (response.Message == "CREATED")
                {
                    response.Message = "Saved successfully";
                    HttpContext.Session.SetString("RefNumber", "");
                }
                if (response.Message == "UPDATED")
                {
                    response.Message = "Modified successfully";
                    HttpContext.Session.SetString("RefNumber", "");
                }
                if (response.Success == false)
                {
                    bool val = await _blob.Delete(filename1);
                    bool val1 = await _blob.Delete(filename2);
                    return Json(response);
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
        public async Task<IActionResult> ReleaseHold(Guid ResumeDataBankGI)
        {
            var response = await _resumeDataBankService.ReleaseHoldResume(ResumeDataBankGI);
            //if (response.Message == "CREATED")
            //    response.Message = "Saved successfully";
            //if (response.Message == "UPDATED")
            //    response.Message = "Modified successfully";
            return Json(response);
        }
        public async Task fillDropdown()
        {
            ViewBag.Gender = new SelectList(await _dropdownService.GetGenderDropDown(), "Code", "Name");
            ViewBag.Cntry = new SelectList(await _dropdownService.GetCountryDropDown(), "Code", "Name");

            ViewBag.Designation = new SelectList(await _dropdownService.GetDesignationDropdown(), "Code", "Name");
            ViewBag.Location = new SelectList(await _dropdownService.GetLocationDropdown(), "Code", "Name");
            ViewBag.skillType = new SelectList(await _dropdownService.GetSkillType_Dropdown(), "Code", "Name");
            ViewBag.ResumeSource = new SelectList(await _dropdownService.GetResumeSourceDropdown(), "Code", "Name");

            PersonalDetailsDropDown dropmodel = new PersonalDetailsDropDown();
            dropmodel = await _personalDetailsService.GetPersonalDetailsDropDown();
            ViewBag.QualifnType = new SelectList(dropmodel.QualificationType, "Code", "Name");
            ViewBag.marital = new SelectList(dropmodel.MaritalStatus, "Code", "Name");

            ViewBag.SkillProficiency = new SelectList(dropmodel.SkillProficiency, "Code", "Name");
            ViewBag.CertificateType = new SelectList(dropmodel.CertificateType, "Code", "Name");
            ViewBag.CertificateMode = new SelectList(dropmodel.CertificateMode, "Code", "Name");
            ViewBag.CertificateValidity = new SelectList(dropmodel.CertificateValidity, "Code", "Name");
            ViewBag.Year = new SelectList(dropmodel.YearOfCompletion, "Code", "Name");
        }

        public async Task<IActionResult> ExportToExcel(ResumeFilter filter, int TabIndex, string TabName, string FROMDATE, string TODATE, int TotalRecords)
        {
            try
            {


                filter.length = TotalRecords;
                filter.start = 0;
                //string FilterValues = await _helpers.GetFilterValues(filter);

                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Resume Repository", "Excel");

                //if (FilterValues == "")
                //{
                //    FilterValues = "All";
                //}

                string currDateTime = await _helpers.GetCurrentDate();
                

                //Get Datset


                var dataset = await _resumeDataBankService.GetResumeDataBankListt_Excel(TabIndex, FROMDATE, TODATE, filter);
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
                    var worksheet = workbook.Worksheets.Add(TabName);
                    int count = dt.Rows.Count + 5;


                    worksheet.Range("A1:I" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:I" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:I" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:I" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:I" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 8)));
                    wsReportNameHeaderRange.Style.Font.Bold = true;
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Merge();
                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191); ;
                    wsReportNameHeaderRange.Value = "Leave Laps";
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
                    //worksheet.Range("B4:I4").Merge();
                    //worksheet.Cell("B4").Value = FilterValues;


                    var currentRow = 5;

                    worksheet.Cell(currentRow, 1).InsertTable(dt);
                    worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                    worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                    worksheet.Range("A5:I5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Range("A5:I5").Style.Font.Bold = true;


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
                            book.Protect(EP.Password, false, false);
                            book.SaveToStream(stream);
                        }
                        var content = stream.ToArray();
                        return File(
                            content,
                            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                            TabName + ".xlsx"
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

        public async Task<ActionResult> onExportToExcelClick(int TabIndex, string TabName, DateTime FROMDATE, DateTime TODATE)
        {
            try
            {
                ResumeDataBank model = new ResumeDataBank();
                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("ResumeDataBase (" + TabName + ")", "Excel");
                int slNo = 0;
                //if (TabIndex != 5)
                //    model = await _resumeDataBankService.GetResumeDataBankList(TabIndex, null, null);
                //else
                //    model = await _resumeDataBankService.GetResumeDataBankList(TabIndex, FROMDATE, TODATE);
                if (Global.GlobalFilter == null)
                    Global.GlobalFilter = new ResumeFilter();
                if (TabIndex != 5)
                    model = await _resumeDataBankService.GetResumeDataBankListt(TabIndex, null, null, Global.GlobalFilter);
                else
                    model = await _resumeDataBankService.GetResumeDataBankListt(TabIndex, FROMDATE, TODATE, Global.GlobalFilter);

                string currDateTime = await _helpers.GetCurrentDate();
                //var a = Date.Length;

                
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("ResumeDataBase List");

                    int count = model.ResumeDataBank_List.Count + 4;

                    if (TabIndex == 1 || TabIndex == 4)
                    {
                        worksheet.Range("A1:G" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:G" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:G" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:G" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:G" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;

                        var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 6)));
                        wsReportNameHeaderRange.Style.Font.Bold = true;
                        wsReportNameHeaderRange.Style.Font.FontSize = 15;
                        wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                        wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        wsReportNameHeaderRange.Merge();
                        wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191); ;
                        wsReportNameHeaderRange.Value = "Resume DataBase (" + TabName + ")";
                        worksheet.Range("A4:G4").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    }
                    if (TabIndex == 2 || TabIndex == 3)
                    {
                        worksheet.Range("A1:F" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:F" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:F" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:F" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:F" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;

                        var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 5)));
                        wsReportNameHeaderRange.Style.Font.Bold = true;
                        wsReportNameHeaderRange.Style.Font.FontSize = 15;
                        wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                        wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        wsReportNameHeaderRange.Merge();
                        wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191); ;
                        wsReportNameHeaderRange.Value = "Resume DataBase (" + TabName + ")";
                        worksheet.Range("A4:F4").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);

                    }
                    if (TabIndex == 6)
                    {
                        worksheet.Range("A1:F" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:F" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:F" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:F" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:F" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;

                        var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 5)));
                        wsReportNameHeaderRange.Style.Font.Bold = true;
                        wsReportNameHeaderRange.Style.Font.FontSize = 15;
                        wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                        wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        wsReportNameHeaderRange.Merge();
                        wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191); ;
                        wsReportNameHeaderRange.Value = "Resume DataBase (" + TabName + ")";
                        worksheet.Range("A4:F4").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);

                    }

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
                    worksheet.Cell(3, 4).Value = DateTime.Now;
                    worksheet.Cell(3, 5).Style.Font.Bold = true;
                    worksheet.Cell(3, 5).Value = "Total Count";
                    worksheet.Cell(3, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(3, 6).Value = model.ResumeDataBank_List.Count;
                    //worksheet.Cell(4, 1).Style.Font.Bold = true;
                    //worksheet.Cell(4, 1).Value = "Filter Applied";
                    //worksheet.Range("B4:I4").Merge();
                    //worksheet.Cell("B4").Value = FilterValues;
                    var currentRow = 4;
                    #region Header


                    worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 1).Value = "Sl.No";
                    worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 2).Value = "Applicant ID";
                    worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 3).Value = "Candidate Name";

                    if (TabIndex == 1)
                    {
                        worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 4).Value = "Designation";
                        worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 5).Value = "Location";
                        worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 6).Value = "Y/E";
                        worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 7).Value = "Age";
                    }
                    if (TabIndex == 2)
                    {
                        worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 4).Value = "MRF No";
                        worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 5).Value = "Job Title";
                        worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 6).Value = "Stage";
                    }

                    if (TabIndex == 3)
                    {
                        worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 4).Value = "MRF No";
                        worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 5).Value = "Job Title";
                        worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 6).Value = "Held At";
                    }
                    else if (TabIndex == 4)
                    {
                        worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 4).Value = "MRF No";
                        worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 5).Value = "Job Title";
                        worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 6).Value = "Rejected At";
                        worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 7).Value = "Reason for Reject";

                    }
                    else if (TabIndex == 6)
                    {
                        worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 4).Value = "MRF No";
                        worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 5).Value = "Job Title";
                        worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 6).Value = "Stage Of Rejection";

                    }


                    foreach (var item in model.ResumeDataBank_List)
                    {
                        currentRow++;
                        slNo = slNo + 1;
                        worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 1).Value = slNo;
                        worksheet.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 2).Value = item.RefNo;
                        worksheet.Cell(currentRow, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 3).Value = item.CandidateName;

                        if (TabIndex == 1)
                        {
                            worksheet.Cell(currentRow, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 4).Value = item.SuitableDesignation;
                            worksheet.Cell(currentRow, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 5).Value = item.SuitedLocation;
                            worksheet.Cell(currentRow, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 6).Value = item.YOS;
                            worksheet.Cell(currentRow, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 7).Value = item.Age;
                        }
                        if (TabIndex == 2)
                        {
                            worksheet.Cell(currentRow, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 4).Value = item.MRFNo;
                            worksheet.Cell(currentRow, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 5).Value = item.JobTitle;
                            worksheet.Cell(currentRow, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 6).Value = item.Stage;

                        }
                        if (TabIndex == 3)
                        {
                            worksheet.Cell(currentRow, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 4).Value = item.MRFNo;
                            worksheet.Cell(currentRow, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 5).Value = item.JobTitle;
                            worksheet.Cell(currentRow, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 6).Value = item.HeldAt;

                        }
                        else if (TabIndex == 4)
                        {
                            worksheet.Cell(currentRow, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 4).Value = item.MRFNo;
                            worksheet.Cell(currentRow, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 5).Value = item.JobTitle;
                            worksheet.Cell(currentRow, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 6).Value = item.RejectedAt;
                            worksheet.Cell(currentRow, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 7).Value = item.RejectReason;
                        }
                        else if (TabIndex == 6)
                        {
                            worksheet.Cell(currentRow, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 4).Value = item.MRFNo;
                            worksheet.Cell(currentRow, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 5).Value = item.JobTitle;
                            worksheet.Cell(currentRow, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 6).Value = item.Stage;
                        }




                        worksheet.Columns(1, 9).Style.Alignment.WrapText = true;
                    }
                    #endregion
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
                            book.Protect(EP.Password, false, false);
                            book.SaveToStream(stream);
                        }
                        var content = stream.ToArray();
                        return File(
                            content,
                            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                          "Resume DataBase(" + TabName + ") List.xlsx"
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

        public async Task fillDistrict(int? id)
        {
            ViewBag.District = new SelectList(await _dropdownService.GetDistrictDropdown(id), "Code", "Name");
        }
        public async Task fillState(int? id)
        {
            ViewBag.State = new SelectList(await _dropdownService.GetStateDropDown(id), "Code", "Name");
        }


        [HttpGet]
        public async Task<JsonResult> GetState(int? id)
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
        public async Task<JsonResult> GetQualification(int qualTypeCode)
        {
            var Qualification = new SelectList(await _dropdownService.GetQualificationDropDown(qualTypeCode), "Code", "Name");
            return Json(Qualification);
        }
        [HttpGet]
        public async Task<JsonResult> GetSpecialisation(int QualCode)
        {
            var Specialisation = new SelectList(await _dropdownService.GetSpecialisationDropDown(QualCode), "Code", "Name");
            return Json(Specialisation);
        }
        public async Task<IActionResult> ResumeView(Guid ResumeGI)
        {
            ResumeDataBank model = new ResumeDataBank();
            model = await _resumeDataBankService.GetResumeDataBankDetails(ResumeGI);
            return View(model);
        }
    }
}

