using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared;
using Hrms.Lite.Shared.Account;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Spire.Xls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.DataBank.Controllers
{
    [Area("DataBank")]
    [SessionAuthorize]
    public class LifeEventsController : Controller
    {
        private readonly ILifeEventsService _lifeEventsService;
        private readonly IBlob _blob;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        public LifeEventsController(ILifeEventsService lifeEventsService, IBlob blob, ICommonService commonService, IHelpers helpers)
        {
            _lifeEventsService = lifeEventsService;
            _blob = blob;
            _commonService = commonService;
            _helpers = helpers;
        }
        public async Task<IActionResult> Index()
        {
            try { 
                LifeEvents model = new LifeEvents();
                LifeEvents m = new LifeEvents();
                model = await _lifeEventsService.GetCelebrities(0, "Today");
                m = await _lifeEventsService.GetEventList("Upcoming");

                var storage = await _commonService.GetFileStorageType();
                if (model != null && model.LifeEventsCelebrityList != null)
                {
                  
                   
                        //retreive Authority Profile pic From AZure Storage
                        for (int j = 0; j < model.LifeEventsCelebrityList.Count; j++)
                        {
                            Shared.File file = new Shared.File();
                            if (model.LifeEventsCelebrityList[j].FileName != "")
                            {
                                file.FileName = model.LifeEventsCelebrityList[j].FileName;
                            if (storage.Data == "BLOB")
                            {
                                file = await _blob.GetBlobFile(file);
                                if (file != null)
                                {
                                    file.fdata = file.stream.ToArray();
                                    var mimeType = GetMimeTypeFromFilename(model.LifeEventsCelebrityList[j].FileName);
                                    model.LifeEventsCelebrityList[j].Base64string = mimeType + Convert.ToBase64String(file.fdata, 0, file.fdata.Length);
                                }
                            }
                            else
                            {
                                model.LifeEventsCelebrityList[j].Base64string = model.LifeEventsCelebrityList[j].AbsoluteUri;
                            }
                            }

                        }
                    
                }
                //retreive Authority Profile pic From AZure Storage
                if (m != null && m.LifeEventsEventList != null)
                {
                    for (int j = 0; j < m.LifeEventsEventList.Count; j++)
                    {
                        Shared.File file = new Shared.File();
                        if (m.LifeEventsEventList[j].FileName != "")
                        {
                            file.FileName = m.LifeEventsEventList[j].FileName;
                            if (storage.Data == "BLOB")
                            {
                                file = await _blob.GetBlobFile(file);
                                if (file != null)
                                {
                                    file.fdata = file.stream.ToArray();
                                    var mimeType = GetMimeTypeFromFilename(m.LifeEventsEventList[j].FileName);
                                    m.LifeEventsEventList[j].Base64string = mimeType + Convert.ToBase64String(file.fdata, 0, file.fdata.Length);
                                }
                            }
                            else
                            {
                                m.LifeEventsEventList[j].Base64string = m.LifeEventsEventList[j].AbsoluteUri;

                            }
                        }
                    }
                }
                model.LifeEventsEventList = m.LifeEventsEventList;
                return View("~/Areas/DataBank/Views/LifeEvents/AddNew.cshtml",model);
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
        public static string GetMimeTypeFromFilename(string filename)
        {
            if (string.IsNullOrEmpty(filename))
            {
                return null;
            }

            var extension = Path.GetExtension(filename).TrimStart('.').ToLowerInvariant();
            var mimeTypeMap = new Dictionary<string, string>()
    {
        { "jpg", "data:image/jpeg;base64," },
        { "jpeg", "data:image/jpeg;base64," },
        { "png", "data:image/png;base64," },
        // Add more mappings for other supported extensions
    };

            return mimeTypeMap.ContainsKey(extension) ? mimeTypeMap[extension] : null;
        }
        public async Task<IActionResult> GetUpcomingCelebrationsList(int Celebration,string Classification)
        {
            try
            {
                LifeEvents model = new LifeEvents();
                model = await _lifeEventsService.GetCelebrities(Celebration, Classification);
                return PartialView("_UpcomingCelebrations", model);
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
        public IActionResult Create()
        {
            return View("~/Areas/DataBank/Views/LifeEvents/AddNew.cshtml");
       
        }
        public async Task<IActionResult> Save(LifeEvents model)
        {
            var StorageType = await _commonService.GetFileStorageType();
            try
            {
                if (model.Attachment != null)
                {
                    if (model.Attachment.FileData != null)
                    {
                        if (StorageType.Data == "BLOB")
                        {

                            var fileobj = await _commonService.UploadIformFileToBLOB(model.Attachment, "Documents");
                            if (fileobj.Success == false)
                            {
                                fileobj.Message = "FILE UPLOAD ERROR";
                                return Json(fileobj);
                            }
                            model.Attachment = fileobj.Data;

                        }
                        else if (StorageType.Data == "IIS")
                        {
                            var fileobj = await _commonService.UploadIformFileToIIS(model.Attachment, "Documents");
                            if (fileobj.Success == false)
                            {
                                fileobj.Message = "FILE UPLOAD ERROR";
                                return Json(fileobj);
                            }
                            model.Attachment = fileobj.Data;

                        }
                        else
                        {
                            var fileobj = await _commonService.UploadIformFileToAWS(model.Attachment, "Documents");
                            if (fileobj.Success == false)
                            {
                                fileobj.Message = "FILE UPLOAD ERROR";
                                return Json(fileobj);
                            }
                            model.Attachment = fileobj.Data;

                        }
                    }
                }
                
                var response = await _lifeEventsService.Save(model);

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

        
        
        public async Task<IActionResult> UpcomingEvents()
        {
            LifeEvents model = new LifeEvents();
            model = await _lifeEventsService.GetEventList("Upcoming");
            return View(model);        
        }
        public async Task<IActionResult> GetEventList(string type)
        {
            LifeEvents model = new LifeEvents();
            model = await _lifeEventsService.GetEventList(type);
            return PartialView("_EventsList",model);
        }
        public async Task<IActionResult> GetEventDetails(Guid EventGI)
        {
            LifeEvents model = new LifeEvents();
            model = await _lifeEventsService.LifeEventDetails(EventGI);
            return PartialView("_EditEvent", model);
        }
        public async Task<IActionResult> GetEventDetailsDelete(Guid EventGI)
        {
            LifeEvents model = new LifeEvents();
            model = await _lifeEventsService.LifeEventDetails(EventGI);
            return PartialView("_Delete", model);
        }
        public async Task<IActionResult> Delete(Guid EventGI)
        {
            var response = await _lifeEventsService.Delete(EventGI);
            return Json(response);
        }
        public async Task<IActionResult> ExportToExcel(string type)
        {
            try
            {
                LifeEvents model = new LifeEvents();
                Filter filter = new Filter();
                //string password = "123";
                //bool needPassword = true;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Life Events (" + type + ")", "Excel");
                //var abcd = TempData["FilterModel"];
               // filter = JsonConvert.DeserializeObject<Filter>(TempData["FilterModel"].ToString());
                //string FilterValues = await _helpers.GetFilterValues(filter);
                //if (FilterValues == "")
                //{
                //    FilterValues = "All";
                //}

                model = await _lifeEventsService.GetEventList(type);
                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                int slNo = 0;

                string currDateTime = await _helpers.GetCurrentDate();
                

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Life Events List");

                    int count = model.LifeEventsEventList.Count + 4;


                    worksheet.Range("A1:E" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:E" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:E" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:E" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:E" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;

                    var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 4)));
                    wsReportNameHeaderRange.Style.Font.Bold = true;
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Merge();
                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191); ;
                    wsReportNameHeaderRange.Value = "Life Events(" + type + ")";

                    worksheet.Cell(2, 1).Style.Font.Bold = true;
                    worksheet.Cell(2, 1).Value = "Group Company Name";
                    worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                    worksheet.Cell(2, 3).Style.Font.Bold = true;
                    worksheet.Cell(2, 3).Value = "Login User";
                    worksheet.Cell(2, 4).Value = LoginUser;
                    worksheet.Cell(3, 1).Style.Font.Bold = true;
                    worksheet.Cell(3, 1).Value = "Date & Time";
                    worksheet.Cell(3, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(3, 2).Style.DateFormat.Format = "dd-MMM-yyyy   h:mm AM/PM";
                    worksheet.Cell(3, 2).Value = currDateTime.ToString();
                    worksheet.Cell(3, 3).Style.Font.Bold = true;
                    worksheet.Cell(3, 3).Value = "Total Count";
                    worksheet.Cell(3, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(3, 4).Value = model.LifeEventsEventList.Count;
                    //worksheet.Cell(4, 1).Style.Font.Bold = true;
                    //worksheet.Cell(4, 1).Value = "Filter Applied";
                    //worksheet.Range("B4:E4").Merge();
                    //worksheet.Cell("B4").Value = FilterValues;

                    //worksheet.Row(3).AdjustToContents();
                    var currentRow = 4;
                    #region Header
                    worksheet.Columns("A", "E").AdjustToContents();
                    worksheet.Rows().AdjustToContents();

                    worksheet.Cell(currentRow, 1).Value = "Sl.No";
                    worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 2).Value = "Event Name";
                    worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 3).Value = "Description";
                    worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 4).Value = "Date";
                    worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 5).Value = "Time";                    



                    #endregion



                    #region Body
                    foreach (var item in model.LifeEventsEventList)
                    {
                        currentRow++;
                        slNo = slNo + 1;
                        worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Range("A4:E4").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                        worksheet.Cell(currentRow, 1).Value = slNo;
                        worksheet.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 2).Value = item.EventName;
                        worksheet.Cell(currentRow, 3).Value = item.Description;
                        worksheet.Cell(currentRow, 4).Value = item.EventDate;
                        worksheet.Cell(currentRow, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 4).Style.DateFormat.Format = "dd-MMM-yyyy";
                        worksheet.Cell(currentRow, 5).Value = item.EventTime;                        
                        


                        worksheet.Columns(1, 8).Style.Alignment.WrapText = true;


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
                            book.Protect(EP.Password,false,false);
                            book.SaveToStream(stream);
                        }
                        var content = stream.ToArray();
                        return File(
                                content,
                                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                                "Life Events List.xlsx"
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
