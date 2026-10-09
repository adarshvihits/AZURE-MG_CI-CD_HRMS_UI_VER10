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
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Spire.Xls;
using Hrms.Lite.Shared.Account;
using Newtonsoft.Json;

namespace Hrms.Lite.UI.Areas.DataBank.Controllers
{
    [Area("DataBank")]
    [SessionAuthorize]
    public class PersonalDetailsApproveController : Controller
    {
        private readonly IPersonalDetailsApproveService _personalDetailsApproveService;
        private readonly IDropdownService _dropdownService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
       
        public PersonalDetailsApproveController(IPersonalDetailsApproveService personalDetailsApproveService,IDropdownService dropdownService, IHelpers helpers,ICommonService commonService)
        {
            _personalDetailsApproveService = personalDetailsApproveService;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = helpers;
        }
        public async Task<IActionResult>Index()
        {
            try
            {
                List<PersonalDetailsList> ob = new List<PersonalDetailsList>();
                Filter filter = new Filter();
                ob = (List<PersonalDetailsList>)await _personalDetailsApproveService.GetPersonalDetailsApprovalList(0, filter);
                PersonalDetails model = new PersonalDetails();
                model.PersonalDetailsApprovalList = ob;
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
        public async Task<IActionResult> ApplyFilter(Filter filter)
        {
            try
            {
                PersonalDetails model = new PersonalDetails();
                List<PersonalDetailsList> ob = new List<PersonalDetailsList>();
                model.Filter = filter;
                ob = (List<PersonalDetailsList>)await _personalDetailsApproveService.GetPersonalDetailsApprovalList(0, filter);
                model.PersonalDetailsApprovalList = ob;
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
        public async Task<IActionResult> Create(Guid EmployeeGI)
        {
            try
            {
                PersonalDetails Model = new PersonalDetails();
                Model = await _personalDetailsApproveService.GetPersonalDetailsApprovalData(EmployeeGI);
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
        [HttpPost]
        public async Task<IActionResult> ApprovePersonalProfile(PersonalDetails model,string Apmode)
        {
            model.ApproveMode = Apmode;
            var response = await _personalDetailsApproveService.ApprovePersonalDetails(model);
            return Json(response);
        }
        [HttpPost]
        public async Task<IActionResult> ListWiseApproveRejectPersonalProfile(Guid EmpGI,Guid GI,string Apmode,string TabIndex)
        {
            PersonalDetails model = new PersonalDetails();
            if(TabIndex== "Family Details")
            {
                model.FamilyDetails = new List<FamilyDetails>();
                List<FamilyDetails> fam = new List<FamilyDetails>();
                FamilyDetails list = new FamilyDetails()
                {
                    FamilyLogGI = GI
                };
                fam.Add(list);
                model.FamilyDetails = fam;
            }
           else if (TabIndex == "Education")
            {
                model.Education = new List<Education>();
                List<Education> edu = new List<Education>();
                Education list = new Education()
                {
                    EducationLogGI = GI
                };
                edu.Add(list);
                model.Education = edu;
            }
            model.EmployeeGI = EmpGI;
            model.ApproveMode = Apmode;
            model.TabIndex = TabIndex;
            var response = await _personalDetailsApproveService.ApprovePersonalDetails(model);
            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> ApprovePersonalProfileLists(PersonalDetails model, string Apmode)
        {
            model.ApproveMode = Apmode;
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
            if (model.TabIndex == "EmployeeVsNominee")
            {
                var en = from li in model.EmployeeVsNomineeList
                         where li.flag == 1
                         select li;
                model.EmployeeVsNomineeList = en.ToList();
            }
            var response = await _personalDetailsApproveService.ApprovePersonalDetails(model);
            return Json(response);
        }

        public async Task<IActionResult> ExportToExcel(Filter filter)
        {
            try
            {
                List<PersonalDetailsList> model = new List<PersonalDetailsList>();
                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Personal Profile Approval List", "Excel");
                int slNo = 0;
               
                string FilterValues = await _helpers.GetFilterValues(filter);
                model = (List<PersonalDetailsList>)await _personalDetailsApproveService.GetPersonalDetailsApprovalList(0, filter);

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
                    worksheet.Cell(currentRow, 1).Value = "Sl.No";
                    worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 2).Value = "ID";
                    worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 3).Value = "EmployeeName";
                    worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 4).Value = "Designation";
                    worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 5).Value = "Department";
                    worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 6).Value = "Location";
                    worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 7).Value = "DOJ";
                    worksheet.Cell(currentRow, 8).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 8).Value = "Address";
                    worksheet.Cell(currentRow, 9).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 9).Value = "Contact Details";
                    worksheet.Cell(currentRow, 10).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 10).Value = "Personal Info";
                    worksheet.Cell(currentRow, 11).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 11).Value = "Family Details";
                    worksheet.Cell(currentRow, 12).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 12).Value = "Education";
                    worksheet.Cell(currentRow, 13).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 13).Value = "Skills";
                    worksheet.Cell(currentRow, 14).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 14).Value = "Certifications";
                    worksheet.Cell(currentRow, 15).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 15).Value = "Previous Employment";
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
                        worksheet.Cell(currentRow, 2).Value = item.Employeeid;
                        worksheet.Cell(currentRow, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 3).Value = item.EmployeeName;
                        worksheet.Cell(currentRow, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 4).Value = item.Designation;
                        worksheet.Cell(currentRow, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 5).Value = item.Department;
                        worksheet.Cell(currentRow, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 6).Value = item.Location;
                        worksheet.Cell(currentRow, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 7).Style.DateFormat.Format = "dd-MMM-yyyy";
                        worksheet.Cell(currentRow, 7).Value = item.DOJ;
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
                        worksheet.Cell(currentRow, 15).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 15).Value = (item.Prev == true) ? "" : "AP";

                        worksheet.Columns(1, 15).Style.Alignment.WrapText = true;
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
                            "Personal Profile Approval List.xlsx"
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
        [HttpGet]
        public async Task<IActionResult> DocumentView(Guid EmployeeGI, string DocType)
        {

            try
            {
                var fileData = await _personalDetailsApproveService.DocumentView(EmployeeGI, DocType);

                if (fileData == null || fileData.fdata == null || fileData.fdata.Length == 0)
                {
                    // This returns a plain text message to the browser
                    return Content("NO DATA FOUND");
                }
                //table containing both compressed(new version) and not compressed data(old)
                var fileBytes = await _helpers.DecompressIfNeeded(fileData.fdata);

                    Response.Headers["Content-Disposition"] =
                    $"inline; filename=\"{fileData.FileName}\"";

                return File(fileBytes, "application/pdf");
            }
            catch (Exception ex)
            {
                // Log exception here if needed
                return StatusCode(500, "Error while downloading file.");
            }
        }


        
      
      
    }
}
