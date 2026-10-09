using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.Recruitment;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Spire.Xls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.Recruitment.Controllers
{
    [Area("Recruitment")]
    [SessionAuthorize]
    public class JoinConfirmEmpActivationController : Controller
    {
        #region private

        private readonly IJoinConfirmEmpActivationService _joinConfirmEmpActivationService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        private readonly IDropdownService _dropdownService;
        #endregion
        public static class Global
        {

            public static Filter GlobalFilter;

        }

        #region constr
        public JoinConfirmEmpActivationController(IDropdownService dropdownService, ICommonService commonService, IJoinConfirmEmpActivationService joinConfirmEmpActivationService, IHelpers helpers)
        {
            _joinConfirmEmpActivationService = joinConfirmEmpActivationService;          
            _commonService = commonService;
            _helpers = helpers;
            _dropdownService= dropdownService;



        }
        #endregion
        public async Task<IActionResult> PreEnrollJoiningConfirm()
        {
            try
            {
                OnBoarding model = new OnBoarding();
                Filter filter = new Filter();
                await fillDropdown();
                model = await _joinConfirmEmpActivationService.PreEnrollJoiningConfirmList(filter);                
                return View("PreEnrollJoiningConfirmList", model);
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
            ViewBag.Pay = new SelectList(await _dropdownService.GetPayrollGroupDropdown(), "Code", "Name");
            ViewBag.SlryStruct = new SelectList(await _dropdownService.GeSalaryStructureDropDown(), "Code", "Name");

        }
        public async Task<IActionResult> GetConfirmDetails(Guid PreEnrollGI)
        {
            try
            {
                PreEnrollment model = new PreEnrollment();
                model = await _joinConfirmEmpActivationService.GetPreEnrollJoiningConfirmDetails(PreEnrollGI);             

                return PartialView("_Confirm", model);
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


        public async Task<IActionResult> Approve(PreEnrollment model)
        {
            try
            {
                var response  = await _joinConfirmEmpActivationService.PreEnrollJoiningConfirm(model);
                if (response.Success == true)              
                    response.Message = "Confirmed Successfully";
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
        public async Task<IActionResult> EmployeeActivation()       
            {
            try
            {
                OnBoarding model = new OnBoarding();
                Filter filter = new Filter();
                model = await _joinConfirmEmpActivationService.GetPreEnrollEmployeeActivationList(filter);                
                return View("EmployeeActivationList", model);
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




        public async Task<IActionResult> ExportToExcel()
        {
            OnBoarding model = new OnBoarding();
            Filter filter = new Filter();
            int slNo = 0;
            if (Global.GlobalFilter == null)
                Global.GlobalFilter = new Filter();
            model = await _joinConfirmEmpActivationService.PreEnrollJoiningConfirmList(Global.GlobalFilter);
        
            string currDateTime = await _helpers.GetCurrentDate();
            

            var LoginUser = HttpContext.Session.GetString("LoginUser");
            var CompanyName = HttpContext.Session.GetString("CompanyName");
            ExcelPassword EP = new ExcelPassword();
            EP = await _commonService.GetExcelPassword("Joining Confirmation List", "Excel");


            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Joining Confirmation List");
                int count = model.OnBoardingList.Count + 4;


                worksheet.Range("A1:H" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1:H" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1:H" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1:H" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1:H" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;

                var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 7)));
                wsReportNameHeaderRange.Style.Font.Bold = true;
                wsReportNameHeaderRange.Style.Font.FontSize = 15;
                wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                wsReportNameHeaderRange.Merge();
                wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191); ;
                wsReportNameHeaderRange.Value = "Joining Confirmation List";
                worksheet.Range("A4:H4").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                worksheet.Cell(2, 1).Style.Font.Bold = true;
                worksheet.Cell(2, 1).Value = "Company Name";
                worksheet.Cell(2, 2).Value = CompanyName.ToUpper();
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
                worksheet.Cell(3, 6).Value = model.OnBoardingList.Count;
              
                var currentRow = 4;
                #region Header
                worksheet.Cell(currentRow, 1).Value = "Sl:No";
                worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 2).Value = "Appli. ID";
                worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 3).Value = "Candidate Name";
                worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 4).Value = "Designation";
                worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 5).Value = "Department";
                worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 6).Value = "Location";
                worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 7).Value = "Due days";
                worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 8).Value = "E:DOJ";
                worksheet.Cell(currentRow, 8).Style.Font.Bold = true;
               

                #endregion

                #region Body
                foreach (var item in model.OnBoardingList)
                {
                    currentRow++;
                    slNo = slNo + 1;
                  
                    worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(currentRow, 1).Value = slNo;
                    worksheet.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(currentRow, 2).Value = item.RefNo;
                    worksheet.Cell(currentRow, 3).Value = item.CandidateName;
                    worksheet.Cell(currentRow, 4).Value = item.Designation.Name;
                    worksheet.Cell(currentRow, 5).Value = item.Department.Name;
                    worksheet.Cell(currentRow, 6).Value = item.Location.Name;
                    worksheet.Cell(currentRow, 7).Value = item.DueDays;
                    worksheet.Cell(currentRow, 8).Value = item.ExpectedDOJ;
                    worksheet.Cell(currentRow, 8).Style.DateFormat.Format = "dd-MMM-yyyy";

                    worksheet.Columns(1, 13).Style.Alignment.WrapText = true;

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
                        "Joining Confirmation List.xlsx"
                        );
                }
            }







        }



    }
}
