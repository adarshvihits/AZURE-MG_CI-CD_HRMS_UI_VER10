using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.PayRoll;
using Hrms.Lite.Shared.Account;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Org.BouncyCastle.Tls;
using Renci.SshNet;
using Spire.Xls;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.PayRoll.Controllers
{
    [Area("PayRoll")]
    [SessionAuthorize]
    public class SalaryReleaseBankStatementController : Controller
    {
        private readonly IDropdownService _dropdownService;
        public readonly ISalaryReleaseBankStatementService _service;
        private readonly IHelpers _helpers;
        public readonly ICommonService _commonService;
        private readonly Sftp _Config;
        private readonly IWebHostEnvironment _Environment;
        public SalaryReleaseBankStatementController(ISalaryReleaseBankStatementService service,  ICommonService commonService, IDropdownService dropdownService, IWebHostEnvironment environment, IOptions<AppSettings> settings, IHelpers helpers)
        {
            _service = service;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _Environment = environment;
            _Config = settings.Value.Sftp;
            _helpers = helpers;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                SalaryReleaseBankStatement model = new SalaryReleaseBankStatement();
                model.PayrollMonth = new MasterBase();
                ViewBag.month = new SelectList(await _dropdownService.GetSalaryReleaseMonthDropdown(0), "Code", "Name");
                ViewBag.ReportFormat = new SelectList(await _dropdownService.GetBnkStatemntRptFormatDropdown(), "Code", "Name");
                Filter filter = new Filter();
                filter.length = 10;
                model = await _service.GetList(0, 0, filter);
                if (model.ExecutionDate == Convert.ToDateTime("01-Jan-0001"))
                {
                    model.ExecutionDate = DateTime.Now;
                }
                model.ReportFormat = 0;
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
        public async Task<IActionResult> GetList(int Month,int ReportFormat, Filter filter)
        {
            try
            {
                SalaryReleaseBankStatement model = new SalaryReleaseBankStatement();
                model = await _service.GetList(Month,ReportFormat, filter);
                return PartialView("_SalaryReleaseBnkStmtList", model);
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

        public async Task<IActionResult> ExportToExcelReleaseStatement(int Month, string MonthName, int ReportFormat, string ReportFormatNme, Filter filter, SalaryReleaseBankStatement model)
        {

            try
            {
               
                model.PayrollMonth = new MasterBase();
                model.Filter = filter;
                model.ReportFormat = ReportFormat;
                model.PayrollMonth.Code = Month;
                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Salary Release Bank Statement", "Excel");

                string filename = "BANK STATEMENT-" + Month;
                //Get Dataset
                model.FileName = filename;
                var dataset = await _service.GetReleaseStatementDetails(model);
                DataTable dt = dataset.Tables[0];


                //set EXCEL Column Heading 
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.ToUpper();
                }


                string currDateTime = await _helpers.GetCurrentDate();
                
                using (var workbook = new XLWorkbook())
                {

                    var worksheet = workbook.Worksheets.Add("Salary Release Bank Statement");


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
                    wsReportNameHeaderRange.Value = "Salary Release Bank Statement";

                    worksheet.Cell(2, 1).Style.Font.Bold = true;
                    worksheet.Cell(2, 1).Value = "Group Company Name";
                    worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                    worksheet.Cell(2, 3).Style.Font.Bold = true;
                    worksheet.Cell(2, 3).Value = "Period";
                    worksheet.Cell(2, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(2, 4).Style.DateFormat.Format = "MMM-yyyy";
                    worksheet.Cell(2, 4).Value = MonthName;
                    worksheet.Cell(2, 5).Style.Font.Bold = true;
                    worksheet.Cell(2, 5).Value = "Report Format";
                    worksheet.Cell(2, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(2, 6).Style.DateFormat.Format = "MMM-yyyy";
                    worksheet.Cell(2, 6).Value = ReportFormatNme;


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
                            book.Protect(EP.Password, false, false);
                            book.SaveToStream(stream);
                        }
                        var content = stream.ToArray();
                        return File(
                            content,
                            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                            "Salary Release Bank Statement.xlsx"
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

        [HttpPost]
        public async Task<IActionResult> PostToSFTP(int Month, int ReportFormat, SalaryReleaseBankStatement model)
        {

            try
            {
                model.PayrollMonth = new MasterBase();
                model.PayrollMonth.Code = Month;
                model.ReportFormat = ReportFormat;
                model.Filter = new Filter();
                
                var BankCode = await _commonService.GetBankCode();
               

                DateTime now = DateTime.Now;
                string strfName = "0";

                //------COUNT +1 FROM REPOSTING HISTORY TABLE TO GENERATE FILENAME 
                var value = await _commonService.GetBankRepostCount(model.PayrollMonth.Code, "SalaryRelease", ReportFormat);

                if (value != 0)
                {
                    strfName = "GEOJIT_" + BankCode.Data + "_" + BankCode.Data + now.Day.ToString("00") + DateTime.Now.ToString("MM") + ".00" + value;
                }
                else
                {     
                    strfName = "GEOJIT_" + BankCode.Data + "_" + BankCode.Data + now.Day.ToString("00") + DateTime.Now.ToString("MM") + ".001";
                }


                //FILE PATH GEOJIT_HRMCLOUD_UI\Hrms.Lite.UI\wwwroot\TextFile
                string filePath = Path.Combine(this._Environment.WebRootPath, "TextFile", strfName);
                model.FileName = strfName;


                //Get DATA TO POST
                var ds = await _service.GetReleaseStatementDetails(model);
                DataTable dt = ds.Tables[0];

                //BINDING  DATA TO FILE

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    using (var fil = new StreamWriter(filePath))
                    {
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            string referenceNumber = row["Cust Reference Number"].ToString().Trim();
                            string beneficiaryName = row["Beneficiary Name"].ToString().Trim();
                            string beneAcct = row["Bene Acct"].ToString().Trim();
                            string ifsc = row["IFSC"].ToString().Trim();
                            string beneAcctType = row["Bene Acct type"].ToString().Trim();
                            string amount = row["Amount"].ToString().Trim();
                            string strDate = model.ExecutionDate.ToString("yyyyMMdd");

                            string content = string.Join(",", new[]
                            {
                                    referenceNumber, beneficiaryName, beneAcct, ifsc,
                                    beneAcctType, amount, strDate
                                });

                            fil.WriteLine(content);
                        }
                    }


                    //Upload via SFTP

                    //UploadToSftp(filePath, strfName);

                    //-----Save FTP filename AND Employees to trnSalaryStatementEmpHD and trnSalaryStatementEmpDT
                    var response = await _service.Save(model);

                    return Json(response);
                }
                else
                {
                    TempData["Message"] = "No data present.";
                    return RedirectToAction("Index");
                }
            }

            catch (UnauthorizedAccessException e)
            {
                await _service.LogErrorToDB(e.Message);
                return RedirectToAction("Login", "Account", new { Area = "" });
            }
            catch (Exception e)
            {
                await _service.LogErrorToDB(e.Message);
                return RedirectToAction("Error", "Account", new { Area = "" });
            }

        }
     

        private void UploadToSftp(string localFilePath, string remoteFileName)
        {
            using var client = new SftpClient(
                _Config.Host,
                _Config.Port,
                _Config.User,
                _Config.Password);

            client.Connect();

            client.ChangeDirectory(_Config.Path);

            using var fileStream = new FileStream(
                localFilePath,
                FileMode.Open,
                FileAccess.Read,
                FileShare.Read);

            client.UploadFile(fileStream, remoteFileName, true);

            client.Disconnect();
        }


     
        public async Task<IActionResult> TransactionHistory(int Month,string MonthName)
        {
            try
            {
                SalaryReleaseBankStatement model = new SalaryReleaseBankStatement();

                model.PayrollMonth = new MasterBase();
                model.PayrollMonth.Name = MonthName;
                model.PayrollMonth.Code = Month;
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
        public async Task<IActionResult> GetPreviousList(Filter filter, int MonthCode)
        {
            try
            {

                SalaryReleaseBankStatement model = new SalaryReleaseBankStatement();
                model = await _service.GetPreviousList(MonthCode, filter);
                if (model == null)
                {
                    model = new SalaryReleaseBankStatement();
                    model.SalaryReleaseBankStatementList = new List<SalaryReleaseBankStatement>();
                }
                return PartialView("_HistoryList", model);
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
