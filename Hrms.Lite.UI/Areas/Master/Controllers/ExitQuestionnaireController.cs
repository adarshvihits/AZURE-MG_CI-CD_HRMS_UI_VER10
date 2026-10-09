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
    public class ExitQuestionnaireController : Controller
    {
        private readonly IExitQuestionnaireService _service;
        private readonly ICommonService _commonService;
        private readonly IDropdownService _dropdownService;
        private readonly IHelpers _helpers;
        public ExitQuestionnaireController(IExitQuestionnaireService service, ICommonService commonService, IDropdownService dropdownService, IHelpers helpers)
        {
            _service = service;
            _commonService = commonService;
            _dropdownService = dropdownService;
            _helpers = helpers;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                ViewBag.Type = new SelectList(await _dropdownService.GetExitInterviewQuestionTypeDropDown(), "Code", "Name");
                ViewBag.PossibleResult = new SelectList(await _dropdownService.GetExitInterviewResultDropDown(), "Code", "Name");
                ViewBag.Group = new SelectList(await _dropdownService.GetExitInterViewGroupDropDown(), "Code", "Name");
                ExitInterviewQuestionnaire model = await _service.GetQuestionnaireList("ACTIVE");
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

        public async Task<IActionResult> Create()
        {
            try
            {
                ExitInterviewQuestionnaire model = new ExitInterviewQuestionnaire();

                ViewBag.Type = new SelectList(await _dropdownService.GetExitInterviewQuestionTypeDropDown(), "Code", "Name");
                ViewBag.PossibleResult = new SelectList(await _dropdownService.GetExitInterviewResultDropDown(), "Code", "Name");
                ViewBag.Group = new SelectList(await _dropdownService.GetExitInterViewGroupDropDown(), "Code", "Name");
                
                model.Mode = "CREATE";
                model.Active = true;
                return PartialView("~/Areas/Master/Views/ExitQuestionnaire/_AddNewOrEdit.cshtml", model);
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

        public async  Task<IActionResult> NewQuestionCreate()
        {
            ExitInterviewQuestionnaire model = new ExitInterviewQuestionnaire();
            ViewBag.Type = new SelectList(await _dropdownService.GetExitInterviewQuestionTypeDropDown(), "Code", "Name");
            ViewBag.PossibleResult = new SelectList(await _dropdownService.GetExitInterviewResultDropDown(), "Code", "Name");
            model.Mode = "CREATE";
            return PartialView("~/Areas/Master/Views/ExitQuestionnaire/_AddNewQuestions.cshtml",model);
        }
        public async Task<IActionResult> NewQuestioneEdit()
        {
            ExitInterviewQuestionnaire model = new ExitInterviewQuestionnaire();
            ViewBag.Type = new SelectList(await _dropdownService.GetExitInterviewQuestionTypeDropDown(), "Code", "Name");
            ViewBag.PossibleResult = new SelectList(await _dropdownService.GetExitInterviewResultDropDown(), "Code", "Name");
            model.Mode = "MODIFY";
            return PartialView("~/Areas/Master/Views/ExitQuestionnaire/_AddNewQuestions.cshtml",model);
        }




        public async Task<ActionResult> GetActiveInactiveValidation(string name, int code)
        {
            try
            {
                var response = await _service.GetMasterInactiveValidation(name, code);

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

        public async Task<IActionResult> GetList(string TabIndex)
        {
            try
            {
                if (TabIndex != "APPROVAL_PENDING")
                    return PartialView("_ExitQuestionnaireList", await _service.GetQuestionnaireList(TabIndex));
                else
                    return PartialView("_PendingList", await _service.GetQuestionnaireList(TabIndex));
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

       
        public async Task<ActionResult> Edit(Guid QuestionnaireGI, Guid LogGI)
        {
            try
            {
                ExitInterviewQuestionnaire model = await _service.GetDetails(QuestionnaireGI, LogGI, "MODIFY");
                ViewBag.Type = new SelectList(await _dropdownService.GetExitInterviewQuestionTypeDropDown(), "Code", "Name");
                ViewBag.PossibleResult = new SelectList(await _dropdownService.GetExitInterviewResultDropDown(), "Code", "Name");
                ViewBag.Group = new SelectList(await _dropdownService.GetExitInterViewGroupDropDown(), "Code", "Name");
                model.Mode = "MODIFY";
                return PartialView("_AddNewOrEdit", model);
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
       
         public async Task<IActionResult>Delete(Guid QuestionnaireGI, Guid LogGI)
        {
            ExitInterviewQuestionnaire model = await _service.GetDetails(QuestionnaireGI, LogGI, "DELETE");
            model.Mode = "DELETE";
            return PartialView("_Delete",model);
        }


        [HttpGet]
        public async Task<IActionResult> Approve(Guid QuestionnaireGI, Guid LogGI)
        {
            ExitInterviewQuestionnaire model = await _service.GetDetails(QuestionnaireGI, LogGI, "APPROVE");
            model.Mode = "APPROVE";
            return PartialView("_Approve", model);
        }

        public async Task<IActionResult> GetQuestionnairePopUp()
        {
           
          
            ExitInterviewQuestionnaire model = await _service.GetResultList();
            if (model.Results.Count == 0)
            { 
                model.Results = new List<PossibleResultsClassifications>();
                PossibleResultsClassifications lst = new PossibleResultsClassifications()
                {
                    SlNo = 1,
                    Name = ""
                };
                model.Results.Add(lst);
            }
            
            return PartialView("~/Areas/Master/Views/ExitQuestionnaire/_ObjectivePossibleResultsPopUp.cshtml", model);
        }
        [HttpPost]
        public async Task<IActionResult> Save(ExitInterviewQuestionnaire input)
        {
            var response = await _service.Create(input);
            return Json(response);
        }
        [HttpPost]
        public async Task<IActionResult> SavePossibleResult(ExitInterviewQuestionnaire input)
        {
            var response = await _service.ResultSave(input.Results);
            return Json(response);
           
        }
       [HttpPost]
        public async Task<IActionResult> Delete(Guid QuestionGI)
        {
            var response = await _service.Delete(QuestionGI);
            return Json(response);

        }
        [HttpPost]
        public async Task<ActionResult> Approve(ExitInterviewQuestionnaire model)
        {
            try
            {
                model.ApproveMode = "APPROVE";
                var response = await _service.ApproveOrReject(model);
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
        public async Task<ActionResult> Reject(ExitInterviewQuestionnaire model)
        {
            try
            {

                model.ApproveMode = "REJECT";
                var response = await _service.ApproveOrReject(model);
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


        public async Task<IActionResult> ExportToExcel(string TabIndex)
        {
            try
            {
                ExitInterviewQuestionnaire model = new ExitInterviewQuestionnaire();
                model = await _service.GetQuestionnaireList(TabIndex);
                int slno = 0;
                string currDateTime = await _helpers.GetCurrentDate();
                

                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Exit Interview Questionnaire (" + TabIndex + ")", "Excel");
                if (TabIndex == "APPROVAL_PENDING")
                {

                    //using (var workbook = new XLWorkbook())
                    //{
                    //    var worksheet = workbook.Worksheets.Add("Exit Interview Questionnaire PendingList");
                    //    int PendingCountnum = model.PendingCount + 6;

                    EP = await _commonService.GetExcelPassword("Exit Interview Questionnaire (" + TabIndex + ")", "Excel");
                    using (var workbook = new XLWorkbook())
                    {

                        var worksheet = workbook.Worksheets.Add("Exit Interview Pending List");
                        int PendingCountnum = model.PendingCount + 6;


                        worksheet.Range("A1:E" + PendingCountnum).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:E" + PendingCountnum).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:E" + PendingCountnum).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:E" + PendingCountnum).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:E" + PendingCountnum).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 4)));
                        wsReportNameHeaderRange.Style.Font.Bold = true;
                        wsReportNameHeaderRange.Style.Font.FontSize = 15;
                        wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                        wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                        wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        wsReportNameHeaderRange.Merge();
                        wsReportNameHeaderRange.Value = " Exit interview Questinnaire " + "  " + "(" + TabIndex + ")";
                        worksheet.Range("A6:E6").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                        worksheet.Cell(2, 1).Style.Font.Bold = true;
                        worksheet.Cell(2, 1).Value = "Company Name";
                        worksheet.Cell(2, 2).Value = CompanyName;
                        worksheet.Cell(3, 1).Style.Font.Bold = true;
                        worksheet.Cell(3, 1).Value = "Login User";
                        worksheet.Cell(3, 2).Value = LoginUser;
                        worksheet.Cell(4, 1).Style.Font.Bold = true;
                        worksheet.Cell(4, 1).Value = "Date & Time";
                        worksheet.Cell(4, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(4, 2).Style.DateFormat.Format = "dd-MMM-yyyy   h:mm AM/PM";
                        worksheet.Cell(4, 2).Value = currDateTime.ToString();
                        worksheet.Cell(5, 1).Style.Font.Bold = true;
                        worksheet.Cell(5, 1).Value = "Total Count";
                        worksheet.Cell(5, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(5, 2).Value = model.ExitInterviewQuestionnaireList.Count;
                        var currentRow = 6;




                        #region Header
                        worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 1).Value = "Sl.No";
                        worksheet.Cell(currentRow, 2).Value = "Question";
                        worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 3).Value = "Type";
                        worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 4).Value = "Group Name";
                        worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 5).Value = "Entry Type";
                        worksheet.Cell(currentRow, 5).Style.Font.Bold = true;


                        #endregion

                        #region Body
                        foreach (var item in model.ExitInterviewQuestionnaireList)
                        {
                            currentRow++;
                            slno = slno + 1;
                            worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 1).Value = slno;
                            worksheet.Cell(currentRow, 2).Value = item.Name;
                            worksheet.Cell(currentRow, 3).Value = item.TypeName;
                            worksheet.Cell(currentRow, 4).Value = item.GroupName;

                            worksheet.Cell(currentRow, 5).Value = item.Mode;


                            worksheet.Columns(1, 5).Style.Alignment.WrapText = true;


                        }
                        worksheet.Columns().AdjustToContents();

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
                                "Exit Questionnaire Pending List.xlsx"
                                );
                        }
                    }



                }

                else
                {
                    EP = await _commonService.GetExcelPassword("Exit Interview Questionnaire (" + TabIndex + ")", "Excel");
                    using (var workbook = new XLWorkbook())
                    {
                       
                        var worksheet = workbook.Worksheets.Add("Exit Interview Group List");
                        int count = model.ExitInterviewQuestionnaireList.Count + 6;


                        worksheet.Range("A1:D" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:D" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:D" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:D" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:D" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 3)));
                        wsReportNameHeaderRange.Style.Font.Bold = true;
                        wsReportNameHeaderRange.Style.Font.FontSize = 15;
                        wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                        wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                        wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        wsReportNameHeaderRange.Merge();
                        wsReportNameHeaderRange.Value = " Exit interview Questinnaire " + "  " + "(" + TabIndex + ")";
                        worksheet.Range("A6:D6").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                        worksheet.Cell(2, 1).Style.Font.Bold = true;
                        worksheet.Cell(2, 1).Value = "Company Name";
                        worksheet.Cell(2, 2).Value = CompanyName;
                        worksheet.Cell(3, 1).Style.Font.Bold = true;
                        worksheet.Cell(3, 1).Value = "Login User";
                        worksheet.Cell(3, 2).Value = LoginUser;
                        worksheet.Cell(4, 1).Style.Font.Bold = true;
                        worksheet.Cell(4, 1).Value = "Date & Time";
                        worksheet.Cell(4, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(4, 2).Style.DateFormat.Format = "dd-MMM-yyyy   h:mm AM/PM";
                        worksheet.Cell(4, 2).Value = currDateTime.ToString();
                        worksheet.Cell(5, 1).Style.Font.Bold = true;
                        worksheet.Cell(5, 1).Value = "Total Count";
                        worksheet.Cell(5, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(5, 2).Value = model.ExitInterviewQuestionnaireList.Count;
                        var currentRow = 6;





                        #region Header
                        worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 1).Value = "Sl.No";
                        worksheet.Cell(currentRow, 2).Value = "Question";
                        worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 3).Value = "Type";
                        worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 4).Value = "Group Name";
                        worksheet.Cell(currentRow, 4).Style.Font.Bold = true;


                        #endregion

                        #region Body
                        foreach (var item in model.ExitInterviewQuestionnaireList)
                        {
                            currentRow++;
                            slno = slno + 1;
                            worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 1).Value = slno;
                            worksheet.Cell(currentRow, 2).Value = item.Name;
                            worksheet.Cell(currentRow, 3).Value = item.TypeName;

                            worksheet.Cell(currentRow, 4).Value = item.GroupName;


                            worksheet.Columns(1, 5).Style.Alignment.WrapText = true;


                        }
                        worksheet.Columns().AdjustToContents();

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
                                "Exit Questionnaire List.xlsx"
                                );
                        }
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
