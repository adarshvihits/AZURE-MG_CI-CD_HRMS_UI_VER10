using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.TrainingModule;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TrainingModule;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Spire.Xls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.TrainingModule.Controllers
{
    [Area("TrainingModule")]
    [SessionAuthorize]
    public class TrainingScheduleController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly ICommonService _commonService;
        private readonly ITrainingScheduleService _service;
        private readonly IHelpers _helpers;
        public TrainingScheduleController(ITrainingScheduleService service, IDropdownService dropdownService, ICommonService commonService, IHelpers helpers)
        {
            _service = service;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = helpers;
        }

        public async Task<ActionResult> Index()
        {
            try
            {

                TrainingSchedule model = new TrainingSchedule();

                ViewBag.FinancialYear = new SelectList(await _dropdownService.GetFinancialYearDropdown(), "Code", "Name");
                var CurrenYear = await _commonService.GetCurrentLFinancialYear();
               
                model = await _service.GetList(0, CurrenYear.Data);
                model.FinancialYear = new Shared.Master.FinancialYear();
                model.FinancialYear.Code = CurrenYear.Data;
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

        public async Task<IActionResult> GetList(int StatusCode, int FinancialYearCode)
        {
            try
            {
                TrainingSchedule model = new TrainingSchedule();
                model = await _service.GetList(StatusCode, FinancialYearCode);
                return View("_TrainingSchedule", model);
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
                TrainingSchedule model = new TrainingSchedule();
                model.PostassessmentRequired = 2;
                model.StartDate = DateTime.Now;
                model.EndDate= DateTime.Now; 
                model.RequestUptoDate= DateTime.Now;
                model.Mode = "CREATE"; 
                ViewBag.TrainingSub = new SelectList(await _dropdownService.GetTrainingDropdown(), "Code", "Name");
                ViewBag.Month = new SelectList(await _dropdownService.GetMonthDropdown(), "Code", "Name");
                ViewBag.RequestType = new SelectList(await _dropdownService.GetRequestTypeTrainingDropDown(), "Code", "Name");
                return PartialView("_AddTrainingSchedule", model);


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

        public async Task<IActionResult> GetFRomToDates(int Month)
        {
            try
            {
                TrainingSchedule model = new TrainingSchedule();

                var FromDate = await _service.GetFRomToDates(Month);
                return Json(FromDate.Data);
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
        public async Task<ActionResult> Save(TrainingSchedule input)
        {
            try
            {
                var response = await _service.Create(input);

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

        public async Task<ActionResult> Edit(int ScheduleCode)
        {
            try
            {
                TrainingSchedule model = await _service.GetTrainingScheduleDetails(ScheduleCode,"MODIFY");
                model.Mode = "MODIFY";
                ViewBag.TrainingSub = new SelectList(await _dropdownService.GetTrainingDropdown(), "Code", "Name");
                ViewBag.Month = new SelectList(await _dropdownService.GetMonthDropdown(), "Code", "Name");
                ViewBag.RequestType = new SelectList(await _dropdownService.GetRequestTypeTrainingDropDown(), "Code", "Name");
               
                
                return PartialView("_AddTrainingSchedule", model);
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

        public async Task<ActionResult> Delete(int ScheduleCode)
        {
            try
            {
                TrainingSchedule model = await _service.GetTrainingScheduleDetails(ScheduleCode,"Delete");
              
               
                return PartialView("_DeleteSchedule", model);
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
        public async Task<ActionResult> Delete(TrainingSchedule model)
        {
            try
            {
                var response = await _service.Delete(model);
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

        public async Task<IActionResult> AddTopic(int ScheduleCode,int TrainingCode)
        {
            try
            {
                TrainingSchedule model = new TrainingSchedule();
                model.ScheduleCode = ScheduleCode;
                model.TrainingCode = TrainingCode;
                model = await _service.GetTopicList(ScheduleCode);

                ViewBag.Subject = new SelectList(await _dropdownService.GetTrainingSubjectTopicDropDown(TrainingCode), "Code", "Name");
                ViewBag.TrainerType = new SelectList(await _dropdownService.GetSheduleTrainingTypeDropDown(), "Code", "Name");
                ViewBag.Trainer = new SelectList(await _dropdownService.GetSheduleTrainerDropdown(0), "Code", "Name");
                ViewBag.VenueType = new SelectList(await _dropdownService.GetScheduleVenueTypeDropDown(), "Code", "Name");
                ViewBag.Venue = new SelectList(await _dropdownService.GetSheduleVenueDropdown(0), "Code", "Name");
               
                return PartialView("_addTopic", model);


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
        public async Task<JsonResult> GetTrainer(int id)
        {
            var Data = new SelectList(await _dropdownService.GetSheduleTrainerDropdown(id), "Code", "Name");
            return Json(Data);
        }

        [HttpGet]
        public async Task<JsonResult> GetVenue(int id)
        {
            var Data = new SelectList(await _dropdownService.GetSheduleVenueDropdown(id), "Code", "Name");
            return Json(Data);
        }


        [HttpPost]
        public async Task<ActionResult> SaveTopic(TrainingSchedule input)
        {
            try
            {
                var response = await _service.CreateTopic(input);

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

        public async Task<IActionResult> GetTopicList(int ScheduleCode)
        {
            try
            {
                TrainingSchedule model = new TrainingSchedule();
                model = await _service.GetTopicList(ScheduleCode);
                return View("_TopicList", model);
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
        public async Task<ActionResult> DeleteTopic(int TransactionCode)
        {
            try
            {
                var response = await _service.DeleteTopic(TransactionCode);
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




        public async Task<IActionResult> ExportToExcel(int StatusCode,int FinancialYearCode)
        {
            try
            {
                TrainingSchedule model = new TrainingSchedule();
                model = await _service.GetList(StatusCode, FinancialYearCode);
                int slno = 0;
                string currDateTime = await _helpers.GetCurrentDate();
                
                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword(" Training Schedule" , "Excel");
               

                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("Training Schedule List");
                        int PendingCountnum = model.TrainingScheduleList.Count + 6;


                        worksheet.Range("A1:I" + PendingCountnum).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:I" + PendingCountnum).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:I" + PendingCountnum).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:I" + PendingCountnum).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:I" + PendingCountnum).Style.Border.LeftBorder = XLBorderStyleValues.Thin;


                        var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 8)));
                        wsReportNameHeaderRange.Style.Font.Bold = true;
                        wsReportNameHeaderRange.Style.Font.FontSize = 15;
                        wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                        wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        wsReportNameHeaderRange.Merge();
                        wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                        wsReportNameHeaderRange.Style.Font.FontSize = 15;
                        wsReportNameHeaderRange.Value = "Training Schedule ";
                        worksheet.Range("A6:I6").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
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
                        worksheet.Cell(5, 1).Value = "Total Count";
                        worksheet.Cell(5, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(5, 2).Value = model.TrainingScheduleList.Count;
                        var currentRow = 6;

                        #region Header
                        worksheet.Cell(currentRow, 1).Value = "Sl.No";
                        worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 2).Value = "TrainingSubject";
                        worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 3).Value = "Start Date";
                        worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 4).Value = "End Date";
                        worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 5).Value = "HeadCount";
                    worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 6).Value = "RequestFromName";
                    worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 7).Value = "RequestUptoDate";
                    worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 8).Value = "Nextevaldate";
                    worksheet.Cell(currentRow, 8).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 9).Value = "TopicCount";
                    worksheet.Cell(currentRow, 9).Style.Font.Bold = true;



                    #endregion

                    #region Body
                    foreach (var item in model.TrainingScheduleList)
                        {
                            currentRow++;
                            slno = slno + 1;
                            worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 1).Value = slno;
                            worksheet.Cell(currentRow, 2).Value = item.TrainingSubject;
                            worksheet.Cell(currentRow, 3).Value = item.StartDate;
                            worksheet.Cell(currentRow, 4).Value = item.EndDate;
                            worksheet.Cell(currentRow, 5).Value = item.HeadCount;
                            worksheet.Cell(currentRow, 6).Value = item.RequestFromName;
                            worksheet.Cell(currentRow, 7).Value = item.RequestUptoDate;
                            worksheet.Cell(currentRow, 8).Value = item.Nextevaldate;
                            worksheet.Cell(currentRow, 9).Value = item.TopicCount;
                            worksheet.Columns(1, 5).Style.Alignment.WrapText = true;
                        }
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
                                book.Protect(EP.Password, false, false);
                                book.SaveToStream(stream);
                            }
                            var content = stream.ToArray();
                            return File(
                                content,
                                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                                "Training Schedule List.xlsx"
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
