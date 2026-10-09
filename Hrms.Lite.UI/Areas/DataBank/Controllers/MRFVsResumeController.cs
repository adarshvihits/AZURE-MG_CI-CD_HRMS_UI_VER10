using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.DataBank.Generic.PersonalDetails;
using Hrms.Lite.Shared.General;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Spire.Xls;

namespace Hrms.Lite.UI.Areas.DataBank.Controllers
{
    [Area("DataBank")]
    [SessionAuthorize]
    public class MRFVsResumeController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly IMRFVsResumeService _MRFVsResumeService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        private readonly IPersonalDetailsService _personalDetailsService;
        public static class Global
        {
            public static ResumeFilter GlobalFilter;

        }
        public MRFVsResumeController(IDropdownService dropdownService, ICommonService commonService, IHelpers helpers,IMRFVsResumeService MRFVsResumeService, IPersonalDetailsService personalDetailsService)
        {
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = helpers;
            _MRFVsResumeService = MRFVsResumeService;
            _personalDetailsService = personalDetailsService;
        }
        public async Task<IActionResult> Index()
        {
            MRFVsResume model = new MRFVsResume();
            //model.Filter = new ResumeFilter();
            ViewBag.resume = new SelectList(await _dropdownService.GetResumeMappingFilterTypeDropDown(), "Code", "Name");
            model = await _MRFVsResumeService.GetVacancyList(0);
            return View(model);
        }

        public async Task<IActionResult>GetList(int Type)
        {
            MRFVsResume model = new MRFVsResume();
            model = await _MRFVsResumeService.GetVacancyList(Type);
            //return PartialView("_ResumeMappingList", model);
            return PartialView("~/Areas/DataBank/Views/MRFVsResume/_ResumeMappingList.cshtml", model);
      


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
        //public async Task<IActionResult> ApplyFilter(ResumeFilter filter,Guid vacancyGI, int Type)
        //{
        //    try
        //    {
        //        MRFVsResume model = new MRFVsResume();
        //        Global.GlobalFilter = filter;
        //        model = await _MRFVsResumeService.GetMRFVsResumeList(Type, vacancyGI, filter);
        //        return PartialView("~/Areas/DataBank/Views/MRFVsResume/_MRFVsResumeList.cshtml", model);
        //    }
        //    catch (UnauthorizedAccessException e)
        //    {

        //        return RedirectToAction("Login", "Account", new { Area = "" });
        //    }
        //    catch (Exception e)
        //    {
        //        return RedirectToAction("Error", "Account", new { Area = "" });
        //    }
        //}




        /// list method

        ////public async Task<IActionResult> GetMRFVsResume(Guid VacancyGI)
        ////{
        ////    MRFVsResume model = new MRFVsResume();
        ////    MRFVsResume m = new MRFVsResume();
        ////    model.Filter = new ResumeFilter();
        ////    ViewBag.resume = new SelectList(await _dropdownService.GetResumeMappingFilterTypeDropDown(), "Code", "Name");
        ////    model = await _MRFVsResumeService.GetMRFVsResumeList(2, VacancyGI, model.Filter);
        ////    m = await _MRFVsResumeService.GeJobTitle(VacancyGI);
        ////    model.JobTitle = m.JobTitle;
        ////    model.Location = m.Location;
        ////    model.JobCode = m.JobCode;
        ////    model.RequestedDate = m.RequestedDate;
        ////    model.NeedByDate = m.NeedByDate;
        ////    model.DaysLeft = m.DaysLeft;
        ////    model.NoOfRequirements = m.NoOfRequirements;
        ////    model.MappedResumes = m.MappedResumes;
        ////    model.VacancyManagementGI = VacancyGI;
        ////    await fillfilterDropdown();
        ////    return View("~/Areas/DataBank/Views/MRFVsResume/MRFVsResume.cshtml", model);
        ////}

        public async Task<IActionResult> GetMRFVsResume(Guid VacancyGI)
        {
            MRFVsResume model = new MRFVsResume();
            MRFVsResume m = new MRFVsResume();
            model.Filter = new ResumeFilter();
            ViewBag.resume = new SelectList(await _dropdownService.GetResumeMappingFilterTypeDropDown(), "Code", "Name");
            // model = await _MRFVsResumeService.GetMRFVsResumeList(2, VacancyGI, model.Filter);
            m = await _MRFVsResumeService.GeJobTitle(VacancyGI);
            model.JobTitle = m.JobTitle;
            model.Location = m.Location;
            model.JobCode = m.JobCode;
            model.RequestedDate = m.RequestedDate;
            model.NeedByDate = m.NeedByDate;
            model.DaysLeft = m.DaysLeft;
            model.NoOfRequirements = m.NoOfRequirements;
            model.MappedResumes = m.MappedResumes;
            model.VacancyManagementGI = VacancyGI;
            await fillfilterDropdown();
            return View("~/Areas/DataBank/Views/MRFVsResume/MRFVsResume.cshtml", model);
        }



        //public async Task<IActionResult> GetMRFVsResumeList(int Type, Guid VacancyGI)
        //{
        //    MRFVsResume model = new MRFVsResume();
        //    model.Filter = new ResumeFilter();
        //    model = await _MRFVsResumeService.GetMRFVsResumeList(Type, VacancyGI,model.Filter);
        //    model.VacancyManagementGI = VacancyGI;
        //    return PartialView("_MRFVsResumeList",model);
        //}



        [HttpPost]
        public async Task<IActionResult> GetMRFVsResumeList(int Type, Guid VacancyGI,ResumeFilter filter1, MRFVsResume _model)
        {
            try
            {
                MRFVsResume model = new MRFVsResume();
                MRFVsResume m = new MRFVsResume();



                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }
                if (Type != 0 & VacancyGI != null)
                {

                    model = await _MRFVsResumeService.GetMRFVsResumeList(Type, VacancyGI, filter1);
               

                }
                if (model.ResumeList_List != null)
                {
                    if (_model.ResumeList_List != null && _model.ResumeList_List.Count != 0)
                    {
                        for (int i = 0; i < model.ResumeList_List.Count; i++)
                        {
                            for (int j = 0; j < _model.ResumeList_List.Count; j++)
                            {
                                if (model.ResumeList_List[i].ResumeDataBankGI == _model.ResumeList_List[j].ResumeDataBankGI)
                                {

                                    model.ResumeList_List[i].Applicable = _model.ResumeList_List[j].Applicable;

                                }
                            }
                        }
                    }
                }

                m.ResumeList_List = model.ResumeList_List;
                int totalrows = 0;
                int totalrowsafterfiltering = 0;

           
                
              

                if (model.ResumeList_List == null || model.ResumeList_List.Count() == 0)
                {

                    return Json(new
                    {
                        data = "",
                        draw = filter1.draw,
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows
                    });
                }


                if (model.ResumeList_List.Count > 0)
                {
                    totalrows = m.ResumeList_List[0].active;
                }
                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = m.ResumeList_List[0].totalRecords;
                    return Json(new
                    {
                        data = model.ResumeList_List,
                        draw = filter1.draw,
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows
                    });
                }
                else
                {
                    return Json(new
                    {
                        data = model.ResumeList_List,
                        draw = filter1.draw,
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




        [HttpPost]
        public async Task<IActionResult> Create(MRFVsResume model)
        {
            var response = await _MRFVsResumeService.Create(model);
            return Json(response);
        }
        public async Task<IActionResult> GetMappedResumePopUp(Guid VacancyGI)
        {
            try
            {
                //VacancyManagement model = new VacancyManagement();
                ResumeDataBank model = new ResumeDataBank();

                model = await _commonService.GetMappedResumeDetails(VacancyGI);

                return PartialView("_ResumeDetailsPopup", model);
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
        public async Task<IActionResult> ResumeView(Guid ResumeGI)
        {
            ResumeDataBank model = new ResumeDataBank();
            model = await _commonService.GetResumeDataBankDetails(ResumeGI);
            return View("~/Areas/DataBank/Views/Shared/_CommonResumeView.cshtml", model);
        }


        public async Task<IActionResult> ExportToExcel(int Type)
        {
            try
            {
                MRFVsResume model = new MRFVsResume();

                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Resume Mapping", "Excel");
                int slNo = 0;

                model = await _MRFVsResumeService.GetVacancyList(Type);
                string currDateTime = await _helpers.GetCurrentDate();
                
                using (var workbook = new XLWorkbook())
                {

                    var worksheet = workbook.Worksheets.Add("Resume Mapping");


                    int count = model.VacancyManagement_List.Count + 4;


                    worksheet.Range("A1:L" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:L" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:L" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:L" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:L" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;






                    var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 11)));
                    wsReportNameHeaderRange.Style.Font.Bold = true;
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Merge();
                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                    wsReportNameHeaderRange.Value = "Resume Mapping";

                    worksheet.Cell(2, 1).Style.Font.Bold = true;
                    worksheet.Cell(2, 1).Value = "Company Name";
                    worksheet.Cell(2, 2).Value = CompanyName.ToUpper();


                    worksheet.Cell(3, 1).Style.Font.Bold = true;
                    worksheet.Cell(3, 1).Value = "Login User";
                    worksheet.Cell(3, 2).Value = LoginUser;
                    worksheet.Cell(3, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(3, 3).Style.Font.Bold = true;
                    worksheet.Cell(3, 3).Value = "Date & Time";
                    worksheet.Cell(3, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(3, 4).Style.DateFormat.Format = "dd-MMM-yyyy   h:mm AM/PM";
                    worksheet.Cell(3, 4).Value = currDateTime.ToString();
                    worksheet.Cell(3, 5).Style.Font.Bold = true;
                    worksheet.Cell(3, 5).Value = "Total Count";
                    worksheet.Cell(3, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(3, 6).Value = model.VacancyManagement_List.Count();


                    var currentRow = 4;
                    #region Header
                    worksheet.Range("A4:L4").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Cell(currentRow, 1).Value = "Sl.No";
                    worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 2).Value = " MRF No";
                    worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 3).Value = "Job Title";
                    worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 4).Value = "Initiated On";
                    worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 5).Value = "Deadline";
                    worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 6).Value = "No.Req";
                    worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 7).Value = "Location";
                    worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 8).Value = " D. L.";
                    worksheet.Cell(currentRow, 8).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 9).Value = "Resumes";
                    worksheet.Cell(currentRow, 9).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 10).Value = "Shtlst";
                    worksheet.Cell(currentRow, 10).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 11).Value = "I & E";
                    worksheet.Cell(currentRow, 11).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 12).Value = " RAA";
                    worksheet.Cell(currentRow, 12).Style.Font.Bold = true;


                    #endregion

                    #region Body
                    foreach (var item in model.VacancyManagement_List)
                    {
                        currentRow++;
                        slNo = slNo + 1;
                        worksheet.Columns().AdjustToContents();
                        worksheet.Rows().AdjustToContents();
                        worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 1).Value = slNo;
                        worksheet.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 2).Value = item.JobCode;
                        worksheet.Cell(currentRow, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 3).Value = item.JobTitle;
                        worksheet.Cell(currentRow, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 4).Value = item.RequestedDate;
                        worksheet.Cell(currentRow, 4).Style.DateFormat.Format = "dd-MMM-yyyy";
                        worksheet.Cell(currentRow, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 5).Value = item.NeedByDate;
                        worksheet.Cell(currentRow, 5).Style.DateFormat.Format = "dd-MMM-yyyy";
                        worksheet.Cell(currentRow, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 6).Value = item.NoOfRequirements;
                        worksheet.Cell(currentRow, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 7).Value = item.Location;
                        worksheet.Cell(currentRow, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 8).Value = item.DaysLeft;
                        worksheet.Cell(currentRow, 9).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 9).Value = item.MappedResumes;
                        worksheet.Cell(currentRow, 10).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 10).Value = item.Shortlisted;
                        worksheet.Cell(currentRow, 11).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 11).Value = item.InterviewEvaluation;
                        worksheet.Cell(currentRow, 12).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 12).Value = item.FinalApprove;
                        worksheet.Columns(1, 12).Style.Alignment.WrapText = true;
                        worksheet.Rows().Style.Alignment.WrapText = true;
                    }
                    #endregion
                    worksheet.Columns().AdjustToContents();
                    worksheet.Rows().AdjustToContents();
                    //worksheet.Protect("123");
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
                            "Resume Mapping List.xlsx"
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
    }
}
