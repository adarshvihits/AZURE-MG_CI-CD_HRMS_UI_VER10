using ClosedXML.Excel;

using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.Account;
using Hrms.Lite.Shared.CandidateLink;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Spire.Xls;
using Filter = Hrms.Lite.Shared.General.Filter;

namespace Hrms.Lite.UI.Areas.DataBank.Controllers
{
    [Area("DataBank")]
    [SessionAuthorize]
    public class CandidatePreEnrolLinkController : Controller
    {
        private readonly ICandidatePreEnrolLinkService _CandidatePreEnrolLinkService;
        private readonly IDropdownService _dropdownService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
       
        public CandidatePreEnrolLinkController(ICandidatePreEnrolLinkService candidatePreEnrolLinkService, ICommonService commonService, IDropdownService dropdownService, IHelpers helpers)
        {
            _CandidatePreEnrolLinkService = candidatePreEnrolLinkService;
            _commonService = commonService;
            _dropdownService = dropdownService;
            _helpers = helpers;
        }
        public async Task<IActionResult> Index()
        {
            //PersonalDetails model = new PersonalDetails();
            //model.Filter = new Filter();
            //await fillfilterDropdown();
            //return View(model);
            try
            {
                //List<PreEnrollCandidateLink> ob = new List<PreEnrollCandidateLink>();
                //Filter filterdata = new Filter();
                //ob = (List<PreEnrollCandidateLink>)await _CandidatePreEnrolLinkService.GetPreEnrollLinkApprovalList(filterdata);
                //PersonalDetails model = new PersonalDetails();
                //model.PreEnrollCandidateLinkApprovalList = ob;
                PersonalDetails model = new PersonalDetails();
                Filter filterdata = new Filter();
                model = await _CandidatePreEnrolLinkService.GetCandidateList("CANDIDATELINK", filterdata, 0);
                model.Filter = await _commonService.GetFilterDropdowns();
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
        public async Task<IActionResult> GetList(string Tab, int radiomode, Filter filter)
        {
            try

            {
                if (Tab == "CANDIDATELINK")
                {
                    PersonalDetails model = new PersonalDetails();

                    model = await _CandidatePreEnrolLinkService.GetCandidateList(Tab, filter, radiomode);
                    return PartialView("_CandidatePreEnrollLink", model);
                }
                else
                {
                    List<PreEnrollCandidateLink> ob = new List<PreEnrollCandidateLink>();
                    Filter filterdata = new Filter();
                    ob = (List<PreEnrollCandidateLink>)await _CandidatePreEnrolLinkService.GetPreEnrollLinkApprovalList(filterdata);
                    PersonalDetails model = new PersonalDetails();
                    model.PreEnrollCandidateLinkApprovalList = ob;
                    model.Filter = await _commonService.GetFilterDropdowns();
                    return PartialView("_List", model);
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
        public async Task<IActionResult> Create(Guid PreEnrollGI)
         {
            try
            {
                PersonalDetails Model = new PersonalDetails();
                Model = await _CandidatePreEnrolLinkService.GetPreEnrollLinkApprovalData(PreEnrollGI);
                Model.enrollment.PreEnrollment.PreEnrollGI = PreEnrollGI;
                return View(Model);
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
        public async Task<IActionResult> SendCandidateMail(PersonalDetails model)
        {

            //model.File = new File();
            try
            {

                var response = await _CandidatePreEnrolLinkService.CandidateLinkSend(model);

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
        //public async Task<IActionResult> ApprovePreEnrollLinkDetails(PersonalDetails model, string Apmode)
        //{
        //    model.ApproveMode = Apmode;
        //    if (model.TabIndex == "Family Details")
        //    {
        //        var fd = from li in model.FamilyDetails
        //                 where li.flag == 1
        //                 select li;
        //        model.FamilyDetails = fd.ToList();
        //    }
        //    if (model.TabIndex == "Education")
        //    {
        //        var ed = from li in model.Education
        //                 where li.flag == 1
        //                 select li;
        //        model.Education = ed.ToList();
        //    }                   
        //    if (model.TabIndex == "Previous Employment")
        //    {
        //        var pe = from li in model.PreviousEmployment
        //                 where li.flag == 1
        //                 select li;
        //        model.PreviousEmployment = pe.ToList();
        //    }
        //    if (model.TabIndex == "Certifications")
        //    {
        //        var ce = from li in model.Certifications
        //                 where li.flag == 1
        //                 select li;
        //        model.Certifications = ce.ToList();
        //    }
        //    if (model.TabIndex == "Skills")
        //    {
        //        var sk = from li in model.Skills
        //                 where li.flag == 1
        //                 select li;
        //        model.Skills = sk.ToList();
        //    }
        //    var response = await _CandidatePreEnrolLinkService.ApprovePreEnrollLinkDetails(model);
        //    return Json(response);
        //}


        public async Task<IActionResult> ApprovePersonalProfile(PersonalDetails model, string Apmode,Guid PreEnrollGI)
        {
            model.enrollment = new Enrollment();
            model.enrollment.PreEnrollment = new PreEnrollment();
            model.enrollment.PreEnrollment.PreEnrollGI = PreEnrollGI;
            
            model.ApproveMode = Apmode;
            var response = await _CandidatePreEnrolLinkService.ApprovePreEnrollLinkDetails(model);
            return Json(response);
        }

        public async Task<IActionResult> ApprovePersonalProfileLists(PersonalDetails model, string Apmode,Guid PreEnrollGI)
        {
            model.ApproveMode = Apmode;
            model.enrollment = new Enrollment();
            model.enrollment.PreEnrollment = new PreEnrollment();
            model.enrollment.PreEnrollment.PreEnrollGI = PreEnrollGI;

            if (model.TabIndex == "Family Details")
            {
                var fd = from li in model.FamilyDetails
                         where li.flag == 1
                         select li;
                model.FamilyDetails = fd.ToList();
            }
            if (model.TabIndex == "Education")
            {
                var ed = from li in model.Education
                         where li.flag == 1
                         select li;
                model.Education = ed.ToList();
            }
            if (model.TabIndex == "Previous Employment")
            {
                var pe = from li in model.PreviousEmployment
                         where li.flag == 1
                         select li;
                model.PreviousEmployment = pe.ToList();
            }
            if (model.TabIndex == "Certifications")
            {
                var ce = from li in model.Certifications
                         where li.flag == 1
                         select li;
                model.Certifications = ce.ToList();
            }
            if (model.TabIndex == "Skills")
            {
                var sk = from li in model.Skills
                         where li.flag == 1
                         select li;
                model.Skills = sk.ToList();
            }
            if (model.TabIndex == "Documents")
            {
                var doc = from li in model.CandidateDocumentUpload
                         where li.flag == 1
                         select li;
                model.CandidateDocumentUpload = doc.ToList();
            }
            var response = await _CandidatePreEnrolLinkService.ApprovePreEnrollLinkDetails(model);
            return Json(response);
        }
        public async Task<IActionResult> ExportToExcel(Filter filter)
        {
            try
            {
                List<PreEnrollCandidateLink> model = new List<PreEnrollCandidateLink>();
                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                Filter filterdata = new Filter();
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Personal Profile Approval List", "Excel");
                int slNo = 0;
              
                string FilterValues = await _helpers.GetFilterValues(filter);
                model = (List<PreEnrollCandidateLink>)await _CandidatePreEnrolLinkService.GetPreEnrollLinkApprovalList(filterdata);

                if (FilterValues == "")
                {
                    FilterValues = "All";
                }
                string currDateTime = await _helpers.GetCurrentDate();
                

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Personal Profile Approval List");
                    int count = model.Count + 5;


                    worksheet.Range("A1:O" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:O" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:O" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:O" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:O" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;

                    var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 14)));
                    wsReportNameHeaderRange.Style.Font.Bold = true;
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Merge();
                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191); ;
                    wsReportNameHeaderRange.Value = "Personal Profile Approval List";

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
                    worksheet.Cell(3, 6).Value = model.Count;
                    worksheet.Cell(4, 1).Style.Font.Bold = true;
                    worksheet.Cell(4, 1).Value = "Filter Applied";
                    worksheet.Range("B4:O4").Merge();
                    worksheet.Cell("B4").Value = FilterValues;





                    var currentRow = 5;
                    #region Header
                    worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 1).Value = "SlNo";
                    worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 2).Value = "Employee Name";
                    worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 3).Value = "Designation";
                    worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 4).Value = "Department";
                    worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 5).Value = "Location";
                    worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 6).Value = "DOJ";
                    worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 7).Value = "Profile Photo";
                    worksheet.Cell(currentRow, 8).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 8).Value = "Address";
                    worksheet.Cell(currentRow, 9).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 9).Value = "Contact Details";
                    worksheet.Cell(currentRow, 10).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 10).Value = "Personal Info";
                    worksheet.Cell(currentRow, 11).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 11).Value = "Family Details";
                    worksheet.Cell(currentRow, 12).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 12).Value = "Educ";
                    worksheet.Cell(currentRow, 13).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 13).Value = "Skills";
                    worksheet.Cell(currentRow, 14).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 14).Value = "Certifications";
                    //worksheet.Cell(currentRow, 15).Style.Font.Bold = true;
                    //worksheet.Cell(currentRow, 15).Value = "Previous Employment";
                    #endregion

                    #region Body
                    foreach (var item in model)
                    {
                        currentRow++;
                        slNo = slNo + 1;
                        worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Range("A5:O5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                        worksheet.Cell(currentRow, 1).Value = slNo;
                        worksheet.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 2).Value = item.EmployeeName;
                        worksheet.Cell(currentRow, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 3).Value = item.Designation;
                        worksheet.Cell(currentRow, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 4).Value = item.Department;
                        worksheet.Cell(currentRow, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 5).Value = item.Location;
                        worksheet.Cell(currentRow, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 6).Value = item.DOJ;
                        worksheet.Cell(currentRow, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 7).Style.DateFormat.Format = "dd-MMM-yyyy";
                        worksheet.Cell(currentRow, 7).Value = item.Photo;
                        worksheet.Cell(currentRow, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 8).Value = (item.Addr == true) ? "" : "AP";
                        worksheet.Cell(currentRow, 9).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 9).Value = (item.Cont == true) ? "" : "AP";
                        worksheet.Cell(currentRow, 10).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 10).Value = (item.Pers == true) ? "" : "AP";
                        worksheet.Cell(currentRow, 11).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 11).Value = (item.Fmly == true) ? "" : "AP";
                        worksheet.Cell(currentRow, 12).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 12).Value = (item.Educ == true) ? "" : "AP";
                        worksheet.Cell(currentRow, 13).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 13).Value = (item.Skills == true) ? "" : "AP";
                        worksheet.Cell(currentRow, 14).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 14).Value = (item.Cert == true) ? "" : "AP";
                        //worksheet.Cell(currentRow, 15).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        //worksheet.Cell(currentRow, 15).Value = (item.Prev == true) ? "" : "AP";

                        worksheet.Columns(1, 14).Style.Alignment.WrapText = true;
                        worksheet.Rows().Style.Alignment.WrapText = true;
                    }
                    #endregion
                    worksheet.Columns().AdjustToContents();
                    worksheet.Rows().AdjustToContents();
                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        if (EP.NeedPassword)
                        {
                            Workbook book = new Spire.Xls.Workbook();
                            book.LoadFromStream(stream);
                            //Protect Workbook
                            book.Protect(EP.Password,false,false);
                            book.SaveToStream(stream);
                        }
                        var content = stream.ToArray();
                        return File(
                            content,
                            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                            "Preenrollment Approval List.xlsx"
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


        public async Task<IActionResult> ApplyFilter(Filter filter)
        {
            try
            {
                PersonalDetails model = new PersonalDetails();
                List<PreEnrollCandidateLink> ob = new List<PreEnrollCandidateLink>();
              
                ob = (List<PreEnrollCandidateLink>)await _CandidatePreEnrolLinkService.GetPreEnrollLinkApprovalList(filter);
                model.PreEnrollCandidateLinkApprovalList = ob;
                return PartialView("_List", model);
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
        public async Task<ActionResult> EditEmail(string UpdatedEmail, Guid PreEnrollGI)
        {
            try
            {
                PersonalDetails model = new PersonalDetails();
                model.PreEnrollGI = PreEnrollGI;
                model.Email = UpdatedEmail;

                var response = await _CandidatePreEnrolLinkService.EditCandidateEmail(model);
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
