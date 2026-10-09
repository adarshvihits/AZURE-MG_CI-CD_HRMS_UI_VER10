using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.Master;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Spire.Xls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.Master.Controllers
{
    [Area("Master")]
    [SessionAuthorize]
    public class CoffRuleController : Controller
    {
        private readonly ICoffRuleService _service;
        private readonly ICommonService _commonService;
        private readonly IDropdownService _dropdownService;
        private readonly IHelpers _helpers;
        public CoffRuleController(ICoffRuleService service, IDropdownService dropdownService, ICommonService commonService, IHelpers helpers)
        {
            _service = service;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = helpers;
        }
        public async Task <IActionResult> Index()
        {
            try
            {
                CoffLeaveRule model = new CoffLeaveRule();
                model.LeaveGroup = new LeaveGroup();
                ViewBag.Leavegroup = new SelectList(await _dropdownService.GetLeaveGroupFilterDropdown(), "Code", "Name");
                ViewBag.ClubLeaves = new SelectList(await _dropdownService.GetClubbingLeaveDropDown(0), "Code", "Name");
                if (ViewBag.Leavegroup != null && ViewBag.Leavegroup.Items.Count > 0)
                    model.LeaveGroup.Code = ViewBag.Leavegroup.Items[0].Code;
                model = await _service.GetCoffRuleDetails(Convert.ToInt32(model.LeaveGroup.Code));
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


        public async Task<ActionResult> GetCoffRuleDetails(int Leavegroup)
        {
            try
            {
                CoffLeaveRule model = await _service.GetCoffRuleDetails(Leavegroup);
                ViewBag.ClubLeaves = new SelectList(await _dropdownService.GetClubbingLeaveDropDown(0), "Code", "Name");
                return PartialView("_CoffRuleDetails", model);
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
        public async Task<JsonResult> GetClubbingDropdown(int LeaveCode)
        {
            var ClubLeaves = await _dropdownService.GetClubbingLeaveDropDown(LeaveCode);
            return Json(ClubLeaves);
        }



        [HttpPost]
        public async Task<IActionResult> Save(CoffLeaveRule model,int LeaveGroup)
        {
            try
            {
                model.LeaveGroup = new LeaveGroup();
                model.LeaveGroup.Code = LeaveGroup;                
                var response = await _service.Create(model);

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

        public async Task<IActionResult> ExportToExcel(int LeaveGroup, string LGName)
        {
            try
            {
                CoffLeaveRule model = new CoffLeaveRule();

                model = await _service.GetCoffRuleDetails(LeaveGroup);
                int slno = 0;
                string currDateTime = await _helpers.GetCurrentDate();
                
                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("COff Rule", "Excel");


                using (var workbook = new XLWorkbook())
                {

                    var worksheet = workbook.Worksheets.Add("Compensatory Off policy");

                    int PendingCountnum = 13;


                    worksheet.Range("A1:C" + PendingCountnum).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:C" + PendingCountnum).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:C" + PendingCountnum).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:C" + PendingCountnum).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:C" + PendingCountnum).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 2)));
                    wsReportNameHeaderRange.Style.Font.Bold = true;
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Merge();


                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Value = "Compensatory Off Policy";
                    worksheet.Range("A6:C6").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Cell(2, 1).Style.Font.Bold = true;
                    worksheet.Cell(2, 1).Value = "Company Name";
                    worksheet.Cell(2, 2).Value = CompanyName.ToUpper();
                    worksheet.Cell(3, 1).Style.Font.Bold = true;
                    worksheet.Cell(3, 1).Value = "Login User";
                    worksheet.Cell(3, 2).Value = LoginUser;
                    worksheet.Cell(4, 1).Style.Font.Bold = true;
                    worksheet.Cell(4, 1).Value = "Date & Time     ";
                    worksheet.Cell(4, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(4, 2).Style.DateFormat.Format = "dd-MMM-yyyy   h:mm AM/PM";
                    worksheet.Cell(4, 2).Value = currDateTime.ToString();
                    worksheet.Cell(5, 1).Style.Font.Bold = true;
                    worksheet.Cell(5, 1).Value = "Leave Group";
                    worksheet.Cell(5, 2).Value = LGName;
                

                    var currentRow = 6;
                    #region Header
                    //worksheet.Range("B2:C2").Merge();
                    //worksheet.Range("B3:C3").Merge();
                    //worksheet.Range("B4:C4").Merge();
                    //worksheet.Range("B5:C5").Merge();
                    worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 1).Value = "Sl.No";
                    //worksheet.Range("B6:C6").Merge();
                    worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 2).Value = "Condition";
                    worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 3).Value = " ";
                  




                    #endregion

                    #region Body
                    for (int i = 1; i <= 7; i++)
                    {
                        currentRow++;
                        slno = slno + 1;
                        worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 1).Value = slno;


                    }
                    currentRow = 7;
                    worksheet.Cell(currentRow, 2).Value = "Hours to work for coff eligibility";
                    worksheet.Cell(currentRow, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(currentRow, 3).Value = model.HoursToWork;

                    currentRow = 8;
                    worksheet.Cell(currentRow, 2).Value = "Applicable for offday";
                    worksheet.Cell(currentRow, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    if(model.OffdayApplicable==true)
                    worksheet.Cell(currentRow, 3).Value = "Yes";
                   else if (model.OffdayApplicable == false)
                        worksheet.Cell(currentRow, 3).Value = "No";
                    //worksheet.Range("B9:C9").Merge();
                    currentRow = 9;
                    worksheet.Cell(currentRow, 2).Value = "Applicable for Holiday";
                    worksheet.Cell(currentRow, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    if (model.HolidayApplicable == true)
                        worksheet.Cell(currentRow, 3).Value = "Yes";
                    else if (model.HolidayApplicable == false)
                        worksheet.Cell(currentRow, 3).Value = "No";

                    //worksheet.Range("B10:C10").Merge();
                    currentRow = 10;
                    worksheet.Cell(currentRow, 2).Value = "Expiry period";
                    worksheet.Cell(currentRow, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(currentRow, 3).Value = model.ExpiryPeriod;

                    //worksheet.Range("B11:C11").Merge();
                    currentRow = 11;
                    worksheet.Cell(currentRow, 2).Value = "Coff credit Approval required";
                    worksheet.Cell(currentRow, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    if (model.CoffCreditApprovalRequired == true)
                        worksheet.Cell(currentRow, 3).Value = "Yes";
                    else if (model.CoffCreditApprovalRequired == false)
                        worksheet.Cell(currentRow, 3).Value = "No";

                    currentRow = 12;
                    //worksheet.Range("B12:C12").Merge();
                    worksheet.Cell(currentRow, 2).Value = "HRMS";
                    worksheet.Cell(currentRow, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    //worksheet.Cell(currentRow, 7).Value = model.HRMSApprovalRequired;
                    if (model.HRMSApprovalRequired == true)
                        worksheet.Cell(currentRow, 3).Value = "Yes";
                    else if (model.HRMSApprovalRequired == false)
                        worksheet.Cell(currentRow, 3).Value = "No";
                    //worksheet.Range("B13:C13").Merge();

                    currentRow = 13;
                    worksheet.Cell(currentRow, 2).Value = "Essp(Reporting manager)";
                    worksheet.Cell(currentRow, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);

                    if (model.ESSPApprovalRequired == true)
                        worksheet.Cell(currentRow, 3).Value = "Yes";
                    else if (model.ESSPApprovalRequired == false)
                        worksheet.Cell(currentRow, 3).Value = "No";

                    currentRow = 14;
                    worksheet.Cell(currentRow, 2).Value = "Leave Clubbing";
                    worksheet.Cell(currentRow, 3).Value = "Club with other leaves";
                    worksheet.Cell(currentRow, 4).Value = model.ClubWithOtherLeaves;
                    worksheet.Cell(currentRow, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);


                    worksheet.Columns(1, 13).Style.Alignment.WrapText = true;
                    worksheet.Rows(1, 13).Style.Alignment.WrapText = true;
                    #endregion
                    worksheet.Columns().AdjustToContents();
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
                            "Compensatory Off Policy.xlsx"
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
