using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.PayRoll;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll;
using Hrms.Lite.Shared.PayRoll.Reports;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Spire.Xls;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Hrms.Lite.Services.IServices.PayRoll.Reports;
using File = Hrms.Lite.Shared.File;

using Hrms.Lite.Services.IServices.Recruitment;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.PayRoll.Generic.Master;
using Hrms.Lite.Shared.Master;
using Newtonsoft.Json;
using Hrms.Lite.Shared.Account;

namespace Hrms.Lite.UI.Areas.PayRoll.Controllers
{
    [Area("PayRoll")]
    [SessionAuthorize]
    public class GeneratePaySlipController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly IGeneratePaySlipService _service;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
     
        private readonly IPayslipService _payslipService;
        private readonly IBlob _blob;
        private readonly IHostingEnvironment _environment;
        public static class Global
        {
            public static int Month;
            public static int PGroup;
            
        }
        public GeneratePaySlipController(IGeneratePaySlipService service, IDropdownService dropdownService, ICommonService commonService, IHelpers helpers, IPayslipService payslipService, IBlob blob, IHostingEnvironment environment)
        {
            _service = service;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = helpers;
            _payslipService = payslipService;
            _blob = blob;
            _environment = environment ?? throw new ArgumentException(nameof(environment));
        }
        public async Task<IActionResult> Index()
        {
            PaySlipGeneration model = new PaySlipGeneration();
            ViewBag.currMonth = new SelectList(await _dropdownService.GetMonthDropdown(), "Code", "Name");

            ViewBag.Pgroup = new SelectList(await _dropdownService.GetPayrollGroupDropdown(), "Code", "Name");
            model.Type = "2";
           
            return View(model);
        }
        [HttpPost]
        public async Task <IActionResult> GeneratePaySlip(PaySlipGeneration model,string MonthName)
        {
            try
            {
              
                DataTable dt = await _service.GetPayslipGenerationMode();
                var FileUploadMode = "";
                model.Month.Name = MonthName;
                if ((dt.Rows?.Count ?? 0) > 0)
                {
                    FileUploadMode = dt.Rows[0]["PayslipGenerationMode"].ToString();
                }
              
                if (FileUploadMode == "BLOB")
                {
                    PaySlip PayslipModel = new PaySlip();
                    PaySlip UrlModel = new PaySlip();

                    PayslipModel.Url = new Url();
                    PayslipModel.Url = await _payslipService.GetPaySlip();
                    string paySlipName = PayslipModel.Url.Payslip;

                    UrlModel.Url = new Url();
                    UrlModel.Url = await _payslipService.GetUrl();
                    string url = UrlModel.Url.Urllink;

                    var newUrlLink = "";
                    if (url != "")
                    {
                        newUrlLink = url + "/api/ReportJson/GetPaySlipJSON?EmployeeCode=";
                    }
                    var reportProcessor = new Telerik.Reporting.Processing.ReportProcessor();
                    var deviceInfo = new System.Collections.Hashtable();
                    var reportSource = new Telerik.Reporting.UriReportSource();
                    var reportingFileName = System.IO.Path.Combine(_environment.ContentRootPath + "\\Reports\\", paySlipName);
                    reportSource.Uri = reportingFileName;
                    List<PaySlipGeneration> EmpModel = new List<PaySlipGeneration>();
                    EmpModel = (List<PaySlipGeneration>)await _service.GetPayslipAllEmployees(model);


                    foreach (var item in EmpModel)
                    {
                        object parameterValue = newUrlLink + item.EmployeeCode + "&SalaryPeriod=" + model.Month.Code.Value;
                        reportSource.Parameters.Add("Url", parameterValue);
                        reportSource.Parameters.Add("FileName", item.EmployeeName);
                        reportSource.Parameters.Add("ID", item.EmployeeID);
                        reportSource.Parameters.Add("Month", MonthName);
                        Telerik.Reporting.Processing.RenderingResult result = reportProcessor.RenderReport("PDF", reportSource, deviceInfo);
                        if (!result.HasErrors)
                        {
                            var b64 = Convert.ToBase64String(result.DocumentBytes);
                            File f = new File();
                            File final = new File();
                            f.FileName = result.DocumentName;
                            f.ActualFileName = "abc";
                            f.ContentType = "application/pdf";
                            f.FileExtension = ".pdf";
                            f.fdata = result.DocumentBytes;
                            var StorageType = await _commonService.GetFileStorageType();

                            if (StorageType.Data == "BLOB")
                            {

                                var fileobj = await _commonService.UploadByteArrayToBLOB(f, "Documents");
                                if (fileobj.Success == false)
                                {
                                    fileobj.Message = "FILE UPLOAD ERROR";
                                    return Json(fileobj);
                                }
                                final = fileobj.Data;

                            }
                            else if (StorageType.Data == "IIS")
                            {
                                var fileobj = await _commonService.UploadByteArrayToIIS(f, "Documents");
                                if (fileobj.Success == false)
                                {
                                    fileobj.Message = "FILE UPLOAD ERROR";
                                    return Json(fileobj);
                                }
                                final = fileobj.Data;

                            }
                            else
                            {
                                var fileobj = await _commonService.UploadByteArrayToAWS(f, "Documents");
                                if (fileobj.Success == false)
                                {
                                    fileobj.Message = "FILE UPLOAD ERROR";
                                    return Json(fileobj);
                                }
                                final = fileobj.Data;

                            }

                           

                            PaySlipGeneration modelitem = new PaySlipGeneration();
                            modelitem.EmployeeCode = item.EmployeeCode;
                            modelitem.CompanyCode = item.CompanyCode;
                            modelitem.AbsoluteUri = final.AbsoluteUri;
                            modelitem.FileName = final.FileName;
                            modelitem.Month = new PayrollMonth();
                            modelitem.PayGroup = new PayrollGroup();
                            modelitem.Month.Code = model.Month.Code.Value;
                            modelitem.PayGroup.Code = model.PayGroup.Code.Value;             
                           var response= await _service.SavePayslipUploaded(modelitem,FileUploadMode);
                           
                        }
                    }                
                }
                if(FileUploadMode == "DATABASE")
                {
                    _service.GeneratePaySlip("DATABASE", model);
                }
                          
                return Json(1);
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
        public async Task<IActionResult> GeneratePaySlipList(Filter filter1, int Month, int PayrollGroup, string Type)
        {
            try
            {
                PaySlipGeneration model = new PaySlipGeneration();
                   

                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }
                if (Month != 0 && PayrollGroup != 0)
                    model.PaySlipGenerationList = (List<PaySlipGeneration>)await _service.GetPaySlipGenerationList(Month, PayrollGroup, Type, filter1);

                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.PaySlipGenerationList == null || model.PaySlipGenerationList.Count() == 0)
                {


                    return Json(new
                    {
                        data = "",
                        draw = filter1.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows

                    });
                }

                if (model.PaySlipGenerationList.Count > 0)
                {
                    totalrows = model.PaySlipGenerationList[0].active;
                }


                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = model.PaySlipGenerationList[0].totalRecords;

                    return Json(new
                    {
                        data = model.PaySlipGenerationList,
                        draw = filter1.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows//totalrowsafterfiltering

                    });
                }
                else
                {

                    return Json(new
                    {
                        data = model.PaySlipGenerationList,
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


        [HttpGet]
        public async Task<IActionResult> PaySlipGeneratedStatus(int SalaryPeriod, int PayRollGroup)
        {
            var data = await _service.GetPaySlipGeneratedStatus(SalaryPeriod, PayRollGroup);
            return Json(data);
        }

        public async Task<IActionResult> ExportToExcel(int Month, int PayrollGroup,string Type, string PayRollName, string MName, int Total)
        {
            try
            {
                var _Type = "";
                if (Type == "0")
                    _Type = "Not Generated";
                else if (Type == "1")
                    _Type = "Generated";
                else
                    _Type = "All";
                Filter filter = new Filter();
                filter.length = Total;
                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Generate PaySlip", "Excel");


                string currDateTime = await _helpers.GetCurrentDate();
                

                var dataset = await _service.ExportToExcel(Month, PayrollGroup, Type,filter );

                DataTable dt = dataset.Tables[0];



                //set EXCEL Column Heading Format
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (dt.Columns[i].ColumnName == "doj" || dt.Columns[i].ColumnName == "dob")
                        dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.ToUpper();
                    else
                        dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
                }

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Generate PaySlip");
                    int count = dt.Rows.Count + 4;

                    worksheet.Range("A1:N" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:N" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:N" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:N" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:N" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 13)));
                    wsReportNameHeaderRange.Style.Font.Bold = true;
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Merge();
                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191); ;
                    wsReportNameHeaderRange.Value = "Generate PaySlip";

                    worksheet.Cell(2, 1).Style.Font.Bold = true;
                    worksheet.Cell(2, 1).Value = "Group Company Name";
                    worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                    worksheet.Cell(2, 3).Style.Font.Bold = true;
                    worksheet.Cell(2, 3).Value = "Month";
                    worksheet.Cell(2, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(2, 4).Value = MName;
        
                    worksheet.Cell(2, 5).Style.Font.Bold = true;
                    worksheet.Cell(2, 5).Value = "Payroll Group";
                    worksheet.Cell(2, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(2, 6).Value = PayRollName;
                  
                    worksheet.Cell(2, 7).Style.Font.Bold = true;
                    worksheet.Cell(2, 7).Value = "Type";
                    worksheet.Cell(2, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(2, 8).Value =_Type ;


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
                    worksheet.Cell(3, 6).Value = dt.Rows.Count;




                    var currentRow = 4;

                    worksheet.Cell(currentRow, 1).InsertTable(dt);


                    worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                    worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                    worksheet.Range("A4:N4").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Range("A4:N4").Style.Font.Bold = true;


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
                            "Generate PaySlip List.xlsx"
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
