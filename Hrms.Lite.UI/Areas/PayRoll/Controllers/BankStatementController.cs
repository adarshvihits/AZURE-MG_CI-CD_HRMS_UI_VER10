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
using Newtonsoft.Json;
using Spire.Xls;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Renci.SshNet;
using Microsoft.Extensions.Options;

namespace Hrms.Lite.UI.Areas.PayRoll.Controllers
{
    [Area("PayRoll")]
    [SessionAuthorize]
    public class BankStatementController : Controller
    {
        private readonly IBankStatementService _BankStatementService;
        private readonly IDropdownService _dropdownService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        private readonly IWebHostEnvironment _Environment;
        private readonly Sftp _Config;
        public BankStatementController(IBankStatementService BankStatementService, IDropdownService dropdownService, ICommonService commonService, IHelpers helpers, IWebHostEnvironment environment, IOptions<AppSettings> settings)
        {
            _BankStatementService = BankStatementService;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = helpers;
            _Environment = environment;
            _Config = settings.Value.Sftp;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                await fillfilterDropdown();
                BankStatement model = new BankStatement();

                model.BasedOn = 0;
                if (model.ExecutionDate == Convert.ToDateTime("01-Jan-0001"))
                {
                    model.ExecutionDate = DateTime.Now;
                }
               
                return View("~/Areas/PayRoll/Views/BankStatement/Index.cshtml", model);
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
        public async Task<IActionResult> CheckDataCount(BankStatement model)
        {
            var dataset = await _BankStatementService.GetBankStatementDetails(model);
            var count = dataset.Tables[0].Rows.Count;
            return Json(count);
        }
        public async Task fillfilterDropdown()
        {
            ViewBag.Dsg = new SelectList(await _dropdownService.GetDesignationDropdown(), "Code", "Name");
            ViewBag.Dept = new SelectList(await _dropdownService.GetDepartmentDropdown(), "Code", "Name");
            ViewBag.Loc = new SelectList(await _dropdownService.GetLocationDropdown(), "Code", "Name");
            ViewBag.PayrollGrp = new SelectList(await _dropdownService.GetPayrollGroupWithGrpCmpyCheckDropdown(), "Code", "Name");
            ViewBag.division = new SelectList(await _dropdownService.GetDivisionDropdown(), "Code", "Name");
            ViewBag.section = new SelectList(await _dropdownService.GetSectionDropdown(), "Code", "Name");
            ViewBag.month = new SelectList(await _dropdownService.GetMonthDropdown(), "Code", "Name");
            ViewBag.bank = new SelectList(await _dropdownService.GetBankDropdown(), "Code", "Name");
          
        }
        public async Task<IActionResult> ExportToExcelBankStatement(BankStatement model)
        {

            try
            {

                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Bank Statement", "Excel");

                //Get Dataset
                var dataset = await _BankStatementService.GetBankStatementDetails(model);
                DataTable dt = dataset.Tables[0];

                DataTable header = dataset.Tables[1];
                //set EXCEL Column Heading 
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.ToUpper(); 
                }


                string currDateTime = await _helpers.GetCurrentDate();
                
                using (var workbook = new XLWorkbook())
                {

                    var worksheet = workbook.Worksheets.Add("Bank Statement");


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
                    wsReportNameHeaderRange.Value = "Bank Statement";

                    worksheet.Cell(2, 1).Style.Font.Bold = true;
                    worksheet.Cell(2, 1).Value = "Group Company Name";
                    worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                    worksheet.Cell(2, 3).Style.Font.Bold = true;
                    worksheet.Cell(2, 3).Value = "Period";
                    worksheet.Cell(2, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    //worksheet.Cell(2, 4).Style.DateFormat.Format = "MMM-yyyy";
                    //worksheet.Cell(2, 4).Value = header.Rows[0]["Period"];

                    var period = header.Rows[0]["Period"];

                    if (period != DBNull.Value && period != null)
                    {
                        worksheet.Cell(2, 4).Value = Convert.ToDateTime(period);
                        worksheet.Cell(2, 4).Style.DateFormat.Format = "MMM-yyyy";
                    }

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
                            "Bank Statement.xlsx"
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
        public async Task<IActionResult> PostToSFTP(BankStatement model)
        {

            try
            {
                DateTime now = DateTime.Now;
                string strfName = "0";

                //------BANK CODE FROM GROUPCOMPAY TABLE FOR FILE NAME GENERATION

                var BankCode = await _commonService.GetBankCode();

                //------COUNT +1 FROM REPOSTING HISTORY TABLE TO GENERATE FILENAME 
                var value = await _commonService.GetBankRepostCount(model.PayrollMonth.Code, "Normal",0);
         

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

                //Get DATA TO POST
                var ds = await _BankStatementService.GetBankStatementDetails(model);
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

                    // Upload via SFTP
                    //UploadToSftp(filePath, strfName);

                    //-----Save FTP filename to Table(HRMS.[trnBankStatmntFTPDetails] )

                    var response = await _BankStatementService.SaveSFTPFileName(model.PayrollMonth.Code,strfName);
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

                return RedirectToAction("Login", "Account", new { Area = "" });
            }
            catch (Exception e)
            {
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
        [HttpGet]
        public async Task<IActionResult> CheckIfAlreadyPostedtoSFTP( int MonthCode)
        {
            try
            {
                var response = await _BankStatementService.CheckIfAlreadyPostedtoSFTP(MonthCode);
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

    }
}
