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
    public class LeaveRuleController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly ILeaveRuleService _LeaveRuleService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;

        public LeaveRuleController(IDropdownService dropdownService, ILeaveRuleService LeaveRuleService, ICommonService commonService, IHelpers helpers)
        {
            _LeaveRuleService = LeaveRuleService;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = helpers;
        }
        public async Task <IActionResult> Index()
        {
            try
            {
                LeaveRule model = new LeaveRule();
                ViewBag.LeaveGrp = new SelectList(await _dropdownService.GetLeaveGroupFilterDropdown(), "Code", "Name");
                ViewBag.Leave = new SelectList(await _dropdownService.GetLeaveFilterDropdown(), "Code", "Name");
                ViewBag.ClubLeaves = new SelectList(await _dropdownService.GetClubbingLeaveDropDown(0), "Code", "Name");
                model = await _LeaveRuleService.GetLeaveRuleDetails(0, 0);
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

      

        public async Task<ActionResult> GetLeaveRuleDetails(int LeaveGroup, int Leave,string LeaveName)
        {
            try
            {
                LeaveRule model = new LeaveRule();
                model.LeaveGroup = new LeaveGroup();
                model.Leave = new Leave();
                ViewBag.LName = LeaveName;
                model.Leave.Code = Leave;
                model.LeaveGroup.Code = LeaveGroup;
                if (LeaveGroup != 0 && Leave != 0)
                { 
                model = await _LeaveRuleService.GetLeaveRuleDetails(LeaveGroup, Leave);
                }
                else
                {
                    ViewBag.LName = null;
                }
                ViewBag.ClubLeaves = new SelectList(await _dropdownService.GetClubbingLeaveDropDown(Leave), "Code", "Name");
                return PartialView("_LeaveRuleDetails",model);
               
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
        public async Task<JsonResult> GetClubbingDropdown(int LeaveCode)
        {
            var ClubLeaves = await _dropdownService.GetClubbingLeaveDropDown(LeaveCode);
            return Json(ClubLeaves);
        }

        [HttpPost]
        public async Task<ActionResult> Save(LeaveRule input,int LeaveGroup, int Leave)
        {
            try
            {
                input.LeaveGroup = new LeaveGroup();
                input.Leave = new Leave();
                input.Leave.Code = Leave;
                input.LeaveGroup.Code = LeaveGroup;
                var response = await _LeaveRuleService.Save(input);

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

        public async Task<IActionResult> ExportToExcel(int LeaveGroup,string LGName,int Leave,string LeaveName)
        {
            try
            {
                LeaveRule model = new LeaveRule();
                model = await _LeaveRuleService.GetLeaveRuleDetails(LeaveGroup, Leave);
                int slno = 0;
                string currDateTime = await _helpers.GetCurrentDate();
                
                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Leave Rule", "Excel");
               

                    using (var workbook = new XLWorkbook())
                    {

                        var worksheet = workbook.Worksheets.Add("Leave Rule");

                        int PendingCountnum = 33;


                        worksheet.Range("A1:D" + PendingCountnum).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:D" + PendingCountnum).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:D" + PendingCountnum).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:D" + PendingCountnum).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:D" + PendingCountnum).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 3)));
                        wsReportNameHeaderRange.Style.Font.Bold = true;
                        wsReportNameHeaderRange.Style.Font.FontSize = 15;
                        wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                        wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        wsReportNameHeaderRange.Merge();


                        wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                        wsReportNameHeaderRange.Style.Font.FontSize = 15;
                        wsReportNameHeaderRange.Value = "Leave Rule";
                        worksheet.Range("A6:D6").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                        worksheet.Cell(2, 1).Style.Font.Bold = true;
                        worksheet.Cell(2, 1).Value = "Company Name";
                        worksheet.Cell(2, 2).Value = CompanyName.ToUpper();
                        worksheet.Cell(3, 1).Style.Font.Bold = true;
                        worksheet.Cell(3, 1).Value = "Login User";
                        worksheet.Cell(3, 2).Value = LoginUser;
                        worksheet.Cell(4, 1).Style.Font.Bold = true;
                        worksheet.Cell(4, 1).Value = "Date & Time";
                        worksheet.Cell(4, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(4, 2).Style.DateFormat.Format = "dd-MMM-yyyy   h:mm AM/PM";
                        worksheet.Cell(4, 2).Value = currDateTime.ToString();
                        worksheet.Cell(5, 1).Style.Font.Bold = true;
                        worksheet.Cell(5, 1).Value = "Leave Group";
                        worksheet.Cell(5, 2).Value = LGName;
                        worksheet.Cell(5, 3).Style.Font.Bold = true;
                        worksheet.Cell(5, 3).Value = "Leave";
                        worksheet.Cell(5, 4).Value = LeaveName;

                    var currentRow = 6;
                        #region Header
                        worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 1).Value = "Sl.No";
                        worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 2).Value = "Category";
                        worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 3).Value = "Condition";
                        worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 4).Value = LeaveName;




                    #endregion

                    #region Body
                     for(int i=1;i<=27;i++)
                    {
                        currentRow++;
                        slno = slno + 1;
                        worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 1).Value = slno;
                        

                    }
                    worksheet.Range("B7:B12").Merge();
                    worksheet.Cell("B7").Value = "General Settings";
                    worksheet.Cell(7, 3).Value = "Applicability";
                    worksheet.Cell(7, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(7, 4).Value = model.Leave_Appl;
                    worksheet.Cell(8, 3).Value = "Opening based on";
                    worksheet.Cell(8, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(8, 4).Value = model.OpeningBased;
                    worksheet.Cell(9, 3).Value = "Maximum Opening";
                    worksheet.Cell(9, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(9, 4).Value = model.MaxOpening;
                    worksheet.Cell(10, 3).Value = "Leave Credit Frequency";
                    worksheet.Cell(10, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(10, 4).Value = model.LeaveCreditFreq;
                    worksheet.Cell(11, 3).Value = "No: of leaves to credit";
                    worksheet.Cell(11, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(11, 4).Value = model.LeaveCreditCount;
                    worksheet.Cell(12, 3).Value = "Maximum Leave employee can hold";
                    worksheet.Cell(12, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(12, 4).Value = model.MaxLeaveHold;

                    worksheet.Range("B13:B14").Merge();

                    worksheet.Cell("B13").Value = "Leave Encash";

                    //worksheet.Cell(13, 2).Value = "Leave Encash";
                    worksheet.Cell(13, 3).Value = "Encash applicable";
                    worksheet.Cell(13, 4).Value = model.Encashment_Appl;
                    worksheet.Cell(14, 3).Value = "Maximum Encashable";
                    worksheet.Cell(14, 4).Value = model.EncashmentMaxLimit;

                    worksheet.Range("B15:B17").Merge();
                    worksheet.Cell("B15").Value = "Leave Carryforward";

                    //worksheet.Cell(15, 2).Value = "Leave Carryforward";
                    worksheet.Cell(15, 3).Value = "Carryforward applicable";
                    worksheet.Cell(15, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(15, 4).Value = model.CarryForward_Appl;
                    worksheet.Cell(16, 3).Value = "Maximum carryforwadable";
                    worksheet.Cell(16, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(16, 4).Value = model.CarryForwardMaxLimit;
                    worksheet.Cell(17, 3).Value = "Half Day Allowed";
                    worksheet.Cell(17, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(17, 4).Value = model.HalfDay_Appl;
                    worksheet.Cell(18, 3).Value = "No: of Leaves in a stretch";
                    worksheet.Cell(18, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(18, 4).Value = model.NoOfLeavesAtaTime;
                    worksheet.Cell(19, 3).Value = "No: of Leave application per year";
                    worksheet.Cell(19, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(19, 4).Value = model.NoOfLeaveApplPerYr;
                    worksheet.Cell(20, 3).Value = "Pre dated application allowed";
                    worksheet.Cell(20, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(20, 4).Value = model.PreDatedApplAlwed;
                    //worksheet.Cell(21, 2).Value = "Leave Application";

                    worksheet.Range("B18:B26").Merge();
                    worksheet.Cell("B18").Value = "Leave Application";

                    worksheet.Cell(21, 3).Value = "No: of days prior";
                    worksheet.Cell(21, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(21, 4).Value = model.NoOfDaysPrior;
                    worksheet.Cell(22, 3).Value = "Advance Application required";
                    worksheet.Cell(22, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(22, 4).Value = model.AdvanceApplRequired;
                    worksheet.Cell(23, 3).Value = "No: of days in advance";
                    worksheet.Cell(23, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(23, 4).Value = model.NoOfDaysAdvance;
                    worksheet.Cell(24, 3).Value = "Apply pro data basis";
                    worksheet.Cell(24, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(24, 4).Value = model.ApplyProDataBasis;
                    worksheet.Cell(25, 3).Value = "Remarks Mandatory or not";
                    worksheet.Cell(25, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(25, 4).Value = model.RemarksMandatory;
                    worksheet.Cell(26, 3).Value = "Mandatory";
                    //worksheet.Cell(26, 4).Value = model.Mandatory;
                    //worksheet.Cell(27, 2).Value = "Document Attachment";



                    worksheet.Range("B27:B28").Merge();
                    worksheet.Cell("B27").Value = "Document Attachment";
                    worksheet.Cell(27, 3).Value = "Mandatory";
                    worksheet.Cell(27, 4).Value = model.Mandatory;
                    worksheet.Cell(27, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(28, 3).Value = "No: of leaves for Mandatory Doc";
                    worksheet.Cell(28, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(28, 4).Value = model.NoOfLeavesForMandatoryDoc;

                    worksheet.Cell("B29").Value = "Leave Clubbing";
                    worksheet.Cell(29, 3).Value = "Club with other leaves";
                    worksheet.Cell(29, 4).Value = model.ClubWithOtherLeaves;
                    worksheet.Cell(29, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);

                    worksheet.Range("B30:B31").Merge();
                    worksheet.Cell("B30").Value = "Holiday Sandwich";
                    worksheet.Cell(30, 3).Value = "Holiday Sandwich applicable";
                    worksheet.Cell(30, 4).Value = model.HolidaySandwichAppl;
                    worksheet.Cell(30, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(31, 3).Value = "Sandwitch no: of leaves";
                    worksheet.Cell(31, 4).Value = model.HolidaySandwitchNoOfLeaves;
                    worksheet.Cell(31, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                   
                    worksheet.Range("B32:B33").Merge();
                    worksheet.Cell("B32").Value = "Offday Sandwich";
                    worksheet.Cell(32, 3).Value = "Offday Sandwich applicable";
                    worksheet.Cell(32, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(32, 4).Value = model.OffdaySandwichAppl;
                    worksheet.Cell(33, 3).Value = "Sandwitch no: of leaves";
                    worksheet.Cell(33, 4).Value = model.OffdaySandwitchNoOfLeaves;
                    worksheet.Cell(33, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Columns(1, 4).Style.Alignment.WrapText = true;
                    worksheet.Rows(1, 35).Style.Alignment.WrapText = true;
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
                                "Leave Rule.xlsx"
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



