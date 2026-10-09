using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared;
using Hrms.Lite.Shared.Master;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hrms.Lite.Shared.General;
using File = Hrms.Lite.Shared.File;
using ClosedXML.Excel;
using Spire.Xls;
using Hrms.Lite.Shared.Account;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.IO.Compression;
using System.Data;

namespace Hrms.Lite.UI.Areas.DataBank.Controllers
{

    [Area("DataBank")]
    [SessionAuthorize]
    public class OfferController : Controller
    {
       

        private readonly ICommonService _commonService;
        private readonly IOfferService _offerService;
        private readonly IDropdownService _dropdownService;
        private readonly IHelpers _helpers;
        private readonly IBlob _blob;

        private readonly IHostingEnvironment _environment;
    
        public static class Global
        {
            public static Filter GlobalFilter;
            public static string url;
        }
        public OfferController(ICommonService commonService, IOfferService offerService,IDropdownService dropdownService, IHelpers helpers, IBlob blob, IHostingEnvironment environment)
        {
           
            _commonService = commonService;
            _offerService= offerService;
              _dropdownService = dropdownService;
            _helpers = helpers;
            _blob = blob;
            _environment = environment ?? throw new ArgumentException(nameof(environment));
        }
        public async Task<IActionResult> Index()
        {
            try
            {

                OfferLetter model = new OfferLetter();
                model.Filter = new Filter();
                model.Filter.length = 10;
                await fillfilterDropdown();
                model = await _offerService.GetofferLetterList("APPROVAL PENDING", 1, model.Filter);
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

        public async Task fillfilterDropdown()
        {

            ViewBag.Gen = new SelectList(await _dropdownService.GetGenderDropDown(), "Code", "Name");
            ViewBag.Cat = new SelectList(await _dropdownService.GetCategoryDropdown(), "Code", "Name");
            ViewBag.Grd = new SelectList(await _dropdownService.GetGradeDropdown(), "Code", "Name");
            ViewBag.Dsg = new SelectList(await _dropdownService.GetDesignationDropdown(), "Code", "Name");
            ViewBag.Dept = new SelectList(await _dropdownService.GetDepartmentDropdown(), "Code", "Name");
            ViewBag.Loc = new SelectList(await _dropdownService.GetLocationDropdown(), "Code", "Name");
            ViewBag.PayrollGrp = new SelectList(await _dropdownService.GetPayrollGroupDropdown(), "Code", "Name");

            ViewBag.division = new SelectList(await _dropdownService.GetDivisionDropdown(), "Code", "Name");
            ViewBag.section = new SelectList(await _dropdownService.GetSectionDropdown(), "Code", "Name");
            ViewBag.EmploymentType = new SelectList(await _dropdownService.GetEmploymentTypeDropdown(), "Code", "Name");
            ViewBag.EmploymentStatus = new SelectList(await _dropdownService.GetEmploymentStatusDropdown(), "Code", "Name");
            ViewBag.WorkLocation = new SelectList(await _dropdownService.GetWorkLocationDropdown(), "Code", "Name");
            ViewBag.WageGrade = new SelectList(await _dropdownService.GetWageGradeDropdown(), "Code", "Name");

        }

        public async Task<IActionResult> GetList(string TabIndex, int radiomode, Filter filter)
        {
            try
            {
                OfferLetter model = new OfferLetter();

                if (TabIndex == "APPROVAL PENDING")
                {
                    return PartialView("_OfferVerificationList");
                }
                else if (TabIndex == "CANDIDATELINK")
                {
                    model = await _offerService.GetCandidateList(TabIndex, filter, radiomode);
                    return PartialView("_CandidatePreEnrollLink", model);
                }
                // ADD THIS: Handle the new tab click
                else if (TabIndex == "APPROVEDFINAL")
                {
                    return PartialView("_OfferVerifiedList", model);
                }
                else
                {
                    return PartialView("_OfferSendList");
                }
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", "Account", new { Area = "" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetOfferLetterList(Filter filter1, int DocType,string Tabindex)
        {
            try
            {
               OfferLetter model = new OfferLetter();
                OfferLetter m = new OfferLetter();





                Filter filter = new Filter();
                Global.GlobalFilter = filter1;


                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }
                model = await _offerService.GetofferLetterList(Tabindex,DocType, filter1);



                m.OfferLetterList = model.OfferLetterList;
                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.OfferLetterList == null)
                {



                    return Json(new
                    {
                        data = "",
                        draw = filter1.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows
                    });
                }




                if (model.OfferLetterList.Count > 0)
                {
                    totalrows = m.OfferLetterList[0].active;

                    //m.OnBoardingList.ToList<Hrms.Lite.Shared.Master.Test>().Count; ;
                }

                //model.AllTabCount = m.AllTabCount;
                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = m.OfferLetterList[0].totalRecords;
                    return Json(new
                    {
                        data = model.OfferLetterList,
                        draw = filter1.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows//totalrowsafterfiltering
                    });
                }
                else
                {
                    return Json(new
                    {
                        data = model.OfferLetterList,
                        draw = filter1.draw,//Request.Form["draw"], 
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
        public async Task<IActionResult> GenerateReportPDF(string ReportName, int PreEnrollCode, Guid EmpGI, int EmpCode,string mode)
        {
            try
            {

                OfferLetter model = new OfferLetter();
                OfferLetter model1 = new OfferLetter();


                var reportProcessor = new Telerik.Reporting.Processing.ReportProcessor();


                var deviceInfo = new System.Collections.Hashtable();

                var reportSource = new Telerik.Reporting.UriReportSource();

                var reportingFileName = System.IO.Path.Combine(_environment.ContentRootPath + "/Reports/", ReportName);


                reportSource.Uri = reportingFileName;

                model.Url = await _commonService.GetUrl();
                var url = model.Url.Urllink + "/api/OfferLetterReport/GetOfferLetterReportPrint?PreEnrollCode=" + PreEnrollCode;

                object parameterValue = PreEnrollCode;
                reportSource.Parameters.Add("url", url);

                Telerik.Reporting.Processing.RenderingResult result = reportProcessor.RenderReport("PDF", reportSource, deviceInfo);
                var b64 = Convert.ToBase64String(result.DocumentBytes);
                //return new ContentResult() { Content = b64 };

                File f = new File();
                File fl = new File();
                File f2 = new File();
                f.FileName = result.DocumentName;
                f.ContentType = "application/pdf";
                f.FileExtension = ".pdf";
                f.fdata = result.DocumentBytes;
                //fl = await _blob.UploadToAzureAsyncWithByteArray(f);
                
                f2.fdata = await CompressFileAsync(f.fdata);
                var response = await _offerService.SaveOfferLetter(f2, PreEnrollCode);

                string AbsoluteUri = fl.AbsoluteUri;
                string FileName = fl.FileName;
                return RedirectToAction("OfferApprove", "Offer", new { PreEnrollCode= PreEnrollCode, FileName = FileName, EmpGI= EmpGI, Mode = mode, AbsoluteUri= AbsoluteUri });
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
        private async Task<byte[]> CompressFileAsync(byte[] data)
        {
            if (data == null || data.Length == 0)
                return data;

            using (var output = new MemoryStream())
            {
                using (var gzip = new GZipStream(output, CompressionMode.Compress))
                {
                    await gzip.WriteAsync(data, 0, data.Length);
                }
                return output.ToArray();
            }
        }

        public async Task<IActionResult> OfferApprove(int PreEnrollCode, string FileName, Guid EmpGI,string Mode,string AbsoluteUri)
        {
            OfferLetter model = new OfferLetter();
            model.File = new File();
            try
            {
                model.PreEnrollCode = PreEnrollCode;
                model.Mode = Mode;
                model.EmployeeGI = EmpGI;
                model.Mode = Mode;
                model.File.FileName = FileName;
                model.File.AbsoluteUri = AbsoluteUri;
                var response = await _offerService.OfferApprove(model);
                //if (response.Success == true && model.Mode == "APPROVE")
                //{
                //    var reportresponse = await _offerProcessingService.RepotService(model.PreEnrollCode);
                //}
                if (response.Message == "APPROVED")
                {
                    response.Message = "Approved Successfully";
                    return Json(response);
                }
                if (response.Message == "REJECTED")
                {
                    response.Message = "Rejected Successfully";
                    return Json(response);
                }
                else
                {
                    return Json(response);
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

        public async Task<IActionResult> SendMail(int PreEnrollCode, Guid PreEnrollGI, int EmpCode)
        {
            OfferLetter model = new OfferLetter();
            model.File = new File();
            try
            {
                model.PreEnrollCode = PreEnrollCode;

                model.PreEnrollGI = PreEnrollGI;
                model.EmployeeCode = EmpCode;


                var response = await _offerService.OfferSend(model);
              
                
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



        public async Task<IActionResult> GetPDF(int PreEnrollCode,Guid EmpGI,int EmpCode,int LetterFormatCode)
        {
            OfferLetter model = new OfferLetter();

            model.PreEnrollCode = PreEnrollCode;
            model.EmployeeGI = EmpGI;
            model.EmployeeCode = EmpCode;
            model.Url = await _commonService.GetUrl();
            Global.url = model.Url.Urllink;
            model.Type = "OfferLetter";
            if(LetterFormatCode==32)
            {

                model.Url = await _commonService.GetLettersFileName(LetterFormatCode);
                model.Url.Urllink = Global.url;
            }
            else
            {
                model.Url = await _commonService.GetOfferLetter(model.Type);
                model.Url.Urllink = Global.url;

            }

            return View("OfferLetterView", model);
        }




        public async Task<IActionResult> GetPDFVerified(int PreEnrollCode, Guid EmpGI, int EmpCode, int LetterFormatCode)
        {
            OfferLetter model = new OfferLetter();

            model.PreEnrollCode = PreEnrollCode;
            model.EmployeeGI = EmpGI;
            model.EmployeeCode = EmpCode;
            model.Url = await _commonService.GetUrl();
            Global.url = model.Url.Urllink;
            model.Type = "OfferLetter";
            if (LetterFormatCode == 32)
            {

                model.Url = await _commonService.GetLettersFileName(LetterFormatCode);
                model.Url.Urllink = Global.url;
            }
            else
            {
                model.Url = await _commonService.GetOfferLetter(model.Type);
                model.Url.Urllink = Global.url;

            }

            return View("OfferLetterViewVerfied", model);
        }





        public async Task<IActionResult> SendCandidateMail(OfferLetter model)
        {

            //model.File = new File();
            try
            {

                var response = await _offerService.CandidateLinkSend(model);

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


        public async Task<IActionResult> ExportToExcel(string TabIndex, int radiomode, Filter filter)
        {
            try
            {
                OfferLetter model = new OfferLetter();
                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                Filter filterdata = new Filter();
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Preenrollment Approval List", "Excel");
                int slNo = 0;
                if (Global.GlobalFilter == null)
                    Global.GlobalFilter = new Filter();
                string FilterValues = await _helpers.GetFilterValues(Global.GlobalFilter);
                model = await _offerService.GetCandidateList(TabIndex, filter, radiomode);

                if (FilterValues == "")
                {
                    FilterValues = "All";
                }
                string currDateTime = await _helpers.GetCurrentDate();
                

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Preenrollment Approval List");
                    int count = model.OfferLetterList.Count + 5;


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
                    wsReportNameHeaderRange.Value = "Preenrollment Approval List";

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
                    worksheet.Cell(3, 6).Value = model.OfferLetterList.Count;
                    worksheet.Cell(4, 1).Style.Font.Bold = true;
                    worksheet.Cell(4, 1).Value = "Filter Applied";
                    worksheet.Range("B4:H4").Merge();
                    worksheet.Cell("B4").Value = FilterValues;





                    var currentRow = 5;
                    #region Header
                    worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 1).Value = "Sl no";
                    worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 2).Value = "Candidate Name";
                    worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 3).Value = "Fun.Designation";
                    worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 4).Value = "Department";
                    worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 5).Value = "Location";
                    worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 6).Value = "E:DOJ";
                    worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 7).Value = "Email";
                    worksheet.Cell(currentRow, 8).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 8).Value = "Mobile";
                    //worksheet.Cell(currentRow, 9).Style.Font.Bold = true;
                    //worksheet.Cell(currentRow, 9).Value = "Contact Details";
                    //worksheet.Cell(currentRow, 10).Style.Font.Bold = true;
                    //worksheet.Cell(currentRow, 10).Value = "Personal Info";
                    //worksheet.Cell(currentRow, 11).Style.Font.Bold = true;
                    //worksheet.Cell(currentRow, 11).Value = "Family Details";
                    //worksheet.Cell(currentRow, 12).Style.Font.Bold = true;
                    //worksheet.Cell(currentRow, 12).Value = "Educ";
                    //worksheet.Cell(currentRow, 13).Style.Font.Bold = true;
                    //worksheet.Cell(currentRow, 13).Value = "Skills";
                    //worksheet.Cell(currentRow, 14).Style.Font.Bold = true;
                    //worksheet.Cell(currentRow, 14).Value = "Certifications";
                    //worksheet.Cell(currentRow, 15).Style.Font.Bold = true;
                    //worksheet.Cell(currentRow, 15).Value = "Previous Employment";
                    #endregion

                    #region Body
                    foreach (var item in model.OfferLetterList)
                    {
                        currentRow++;
                        slNo = slNo + 1;
                        worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Range("A5:H5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
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
                        worksheet.Cell(currentRow, 7).Value = item.Email;
                        worksheet.Cell(currentRow, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 8).Value = item.Mobile;
                        //worksheet.Cell(currentRow, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        //worksheet.Cell(currentRow, 8).Value = (item.Addr == true) ? "" : "AP";
                        //worksheet.Cell(currentRow, 9).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        //worksheet.Cell(currentRow, 9).Value = (item.Cont == true) ? "" : "AP";
                        //worksheet.Cell(currentRow, 10).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        //worksheet.Cell(currentRow, 10).Value = (item.Pers == true) ? "" : "AP";
                        //worksheet.Cell(currentRow, 11).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        //worksheet.Cell(currentRow, 11).Value = (item.Fmly == true) ? "" : "AP";
                        //worksheet.Cell(currentRow, 12).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        //worksheet.Cell(currentRow, 12).Value = (item.Educ == true) ? "" : "AP";
                        //worksheet.Cell(currentRow, 13).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        //worksheet.Cell(currentRow, 13).Value = (item.Skills == true) ? "" : "AP";
                        //worksheet.Cell(currentRow, 14).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        //worksheet.Cell(currentRow, 14).Value = (item.Cert == true) ? "" : "AP";
                        //worksheet.Cell(currentRow, 15).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        //worksheet.Cell(currentRow, 15).Value = (item.Prev == true) ? "" : "AP";

                        worksheet.Columns(1, 8).Style.Alignment.WrapText = true;
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
        public async Task<IActionResult> ShowDoc(Guid PreEnrollGI)
        {
            var fileData = await _offerService.DownloadDoc(PreEnrollGI);
            if (fileData == null || fileData.fdata == null)
                return NotFound();

            var decompressedBytes = await DecompressFileAsync(fileData.fdata);
            
            Response.Headers["Content-Disposition"] = $"inline; filename=\"{fileData.FileName}\"";

            return File(decompressedBytes, "application/pdf", fileData.FileName);
        }
        private async Task<byte[]> DecompressFileAsync(byte[] compressedData)
        {
            using (var input = new MemoryStream(compressedData))
            using (var gzip = new GZipStream(input, CompressionMode.Decompress))
            using (var output = new MemoryStream())
            {
                await gzip.CopyToAsync(output);
                return output.ToArray();
            }
        }
//        public async Task<IActionResult> OfferApproveAndSave(IFormFile FileData, string ReportName, int PreEnrollCode, Guid EmpGI, int EmpCode, string mode)
//        {
//            try
//            {
//                File f = new File();
//                File fl = new File();
//                File f2 = new File();
//                f.FileName = result.DocumentName;
//                f.ContentType = "application/pdf";
//                f.FileExtension = ".pdf";
//                f.fdata = FileData;
//                //fl = await _blob.UploadToAzureAsyncWithByteArray(f);

//                f2.fdata = await CompressFileAsync(f.fdata);

//                var response = await _offerService.SaveOfferLetter(f2, PreEnrollCode);
//                return RedirectToAction("OfferApprove", "Offer", new { PreEnrollCode= PreEnrollCode, FileName = FileName, EmpGI= EmpGI, Mode = mode, AbsoluteUri= AbsoluteUri
//    });
//            }
//            catch (UnauthorizedAccessException e)
//{

//    return RedirectToAction("Login", "Account", new { Area = "" });
//}
//catch (Exception e)
//{
//    return RedirectToAction("Error", "Account", new { Area = "" });
//}



//        }

        [HttpPost]
        public async Task<IActionResult> OfferApproveAndSave(IFormFile FileData, string ReportName, int PreEnrollCode, Guid EmpGI, int EmpCode, string mode)
        {
            try
            {
                if (FileData == null || FileData.Length == 0)
                {
                    return Json(new { success = false, message = "File not received" });
                }

                // ✅ Get filename safely
                var fileName = Path.GetFileName(FileData.FileName);

                // ✅ Convert IFormFile → byte[]
                byte[] fileBytes;
                using (var ms = new MemoryStream())
                {
                    await FileData.CopyToAsync(ms);
                    fileBytes = ms.ToArray();
                }

                // ✅ Build your File object
                File f = new File
                {
                    FileName = fileName,
                    ContentType = FileData.ContentType ?? "application/pdf",
                    FileExtension = Path.GetExtension(fileName),
                    fdata = fileBytes
                };

                // ✅ Compress (expects byte[])
                File f2 = new File
                {
                    FileName = f.FileName,
                    ContentType = f.ContentType,
                    FileExtension = f.FileExtension,
                    fdata = await CompressFileAsync(f.fdata)
                };

                // ✅ Save
                var response = await _offerService.SaveOfferLetter(f2, PreEnrollCode);

                return RedirectToAction("OfferApprove","Offer",  new { PreEnrollCode = PreEnrollCode, FileName = fileName, EmpGI = EmpGI, Mode = mode });
            }
            catch (UnauthorizedAccessException)
            {
                return RedirectToAction("Login", "Account", new { Area = "" });
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Account", new { Area = "" });
            }
        }
        public async Task<IActionResult> OfferExportToExcel(Filter filter1, int DocType, string Tabindex,string Tabname, int TotalRows)
        {
            OfferLetter model = new OfferLetter();
            var LoginUser = HttpContext.Session.GetString("LoginUser");
            var CompanyName = HttpContext.Session.GetString("CompanyName");
            var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
            var GroupCompanyName = User.authUser.GroupCompanyName;
            ExcelPassword EP = new ExcelPassword();
            EP = await _commonService.GetExcelPassword("Offer", "Excel");


            filter1.length = TotalRows;
            filter1.start = 0;

            string FilterValues = await _helpers.GetFilterValues(filter1);
            if (FilterValues == "")
            {
                FilterValues = "All";
            }
            model.Filter = filter1;

            string[] cu_Date = System.DateTime.Now.GetDateTimeFormats();
            string currDateTime = cu_Date[44];

            //Get Dataset
            var dataset = await _offerService.GetofferLetterList_Excel(Tabindex, DocType, filter1);
            DataTable dt = dataset.Tables[0];
            //set EXCEL Column Heading 
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                if (dt.Columns[i].ColumnName == "id" || dt.Columns[i].ColumnName == "doj" )
                    dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.ToUpper();
                else
                    dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
            }

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Offer");
                int count = dt.Rows.Count + 5;

                string columnName = "";
                int Columncount = dt.Columns.Count;

                while (Columncount > 0)
                {
                    int modulo = (Columncount - 1) % 26;
                    columnName = Convert.ToChar('A' + modulo) + columnName;
                    Columncount = (Columncount - modulo) / 26;
                }
                string colName = columnName;
                worksheet.Range("A1", colName + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1", colName + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1", colName + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1", colName + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1", colName + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;



                var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, colName));
                wsReportNameHeaderRange.Style.Font.Bold = true;
                wsReportNameHeaderRange.Style.Font.FontSize = 15;
                wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                wsReportNameHeaderRange.Merge();
                wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                wsReportNameHeaderRange.Value = "Offer ("+ Tabname +")";

                worksheet.Cell(2, 1).Style.Font.Bold = true;
                worksheet.Cell(2, 1).Value = "Group Company Name";
                worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                worksheet.Cell(2, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                worksheet.Cell(2, 3).Style.Font.Bold = true;
                worksheet.Cell(2, 3).Value = "Login User";
                worksheet.Cell(2, 4).Value = LoginUser;

                worksheet.Cell(3, 1).Style.Font.Bold = true;
                worksheet.Cell(3, 1).Value = "Date & Time";
                worksheet.Cell(3, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                worksheet.Cell(3, 2).Style.DateFormat.Format = "dd-MMM-yyyy";
                worksheet.Cell(3, 2).Value = currDateTime.ToString();
                worksheet.Cell(3, 3).Style.Font.Bold = true;
                worksheet.Cell(3, 3).Value = "Total Count";
                worksheet.Cell(3, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                worksheet.Cell(3, 4).Value = dt.Rows.Count;
                worksheet.Cell(4, 1).Style.Font.Bold = true;
                worksheet.Cell(4, 1).Value = "Filter Applied";
                var filterapplied = worksheet.Range("B4", colName + 4).Merge();

                filterapplied.Value = FilterValues;


                var currentRow = 5;


                worksheet.Cell(currentRow, 1).InsertTable(dt);
                worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                worksheet.Range("A5", colName + 5).Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                worksheet.Range("A5", colName + 5).Style.Font.Bold = true;


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
                        book.Protect(EP.Password);
                        book.SaveToStream(stream);
                    }
                    var content = stream.ToArray();
                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "Offer List.xlsx"
                        );
                }
            }
        }
    }
}
