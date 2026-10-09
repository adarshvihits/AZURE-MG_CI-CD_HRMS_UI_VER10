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
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.Master.Controllers
{
    [Area("Master")]
    [SessionAuthorize]
    public class AllowanceDeductionVsAccountHeadController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly IAllowanceDeductionVsAccountHeadService _service;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        public AllowanceDeductionVsAccountHeadController(IAllowanceDeductionVsAccountHeadService Service, IDropdownService dropdownService, ICommonService commonService, IHelpers helpers)
        {
            _service = Service;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = helpers;
        }
        public async Task<IActionResult> Index()
        {

            try
            {
                AccountsHead model = new AccountsHead();
                model = await _service.GetList("ALLOWANCE",1);
                ViewBag.GroupCompany = new SelectList(await _dropdownService.GetGroupCompanyDropDown(), "Code", "Name");
                ViewBag.Groupby = new SelectList(await _dropdownService.GetERPClassificationDropDown(), "Code", "Name");

                ViewBag.AccountHead = new SelectList(await _dropdownService.GetAccountHeadCombo("ALLOWANCE"), "Code", "Name");
                model.GroupCompanyCode = 1;
                model.TabName = "ALLOWANCE";
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
        public async Task<ActionResult> GetList(string TabIndex,int GroupCode)
        {
            try
            {
                AccountsHead model = new AccountsHead();
                if (TabIndex == "ERP_CODE")
                {
                    model = await _service.ClassificationVsERPcodeList(GroupCode);                  
                    model.TabName = TabIndex;
                    return PartialView("_ClassifnVsErpCodeList", model);
                }
                else
                {
                    model = await _service.GetList(TabIndex, GroupCode);
                    ViewBag.AccountHead = new SelectList(await _dropdownService.GetAccountHeadCombo(TabIndex), "Code", "Name");
                    model.TabName = TabIndex;
                    return PartialView("_List", model);
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
        public async Task<ActionResult> Save(AccountsHead input, string TabName,int GroupCode)
        {
            try
            {
                object response = new object();
                input.TabName = TabName;
                input.GroupCompanyCode = GroupCode;
                if (TabName == "ERP_CODE")
                {
                    var Dsg = from li in input.ClassificationVsERPCodeList
                              where li.ERPCode != null
                              select li;
                   input.ClassificationVsERPCodeList= Dsg.ToList();
                    response = await _service.ClassificationVsERPcodeSave(input);
                }
                else

                    response = await _service.Create(input);

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
        public async Task<IActionResult> ExportToExcel(string TabIndex,int ClassificationCode, int GroupCode)
        {
            try
            {
                
                
                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Allow/Deduction Vs Accounts Heads (" + TabIndex + ")", "Excel");

             
                string currDateTime = await _helpers.GetCurrentDate();
                

                //Get Datset
                if (TabIndex != "ERP_CODE")
                {
                    var dataset = await _service.GetExcel(TabIndex, GroupCode);
                    DataTable dt = dataset.Tables[0];


                    //set EXCEL Column Heading 
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        if (dt.Columns[i].ColumnName == "doj" || dt.Columns[i].ColumnName == "dob" || dt.Columns[i].ColumnName == "y/s")
                            dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.ToUpper();
                        else
                            dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
                    }

                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("Allow&DeductionVsAccHeads");
                        int count = dt.Rows.Count + 5;


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
                        wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                        wsReportNameHeaderRange.Style.Alignment.SetWrapText();
                        wsReportNameHeaderRange.Value = "Allowance Or Deduction Vs Accounts Heads (" + TabIndex + ")";

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
                        worksheet.Cell(4, 1).Style.Font.Bold = true;
                        worksheet.Cell(4, 1).Value = "Total Count";
                        worksheet.Cell(4, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(4, 2).Value = dt.Rows.Count;




                        var currentRow = 5;

                        worksheet.Cell(currentRow, 1).InsertTable(dt);
                        worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                        worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                        worksheet.Range("A5:E5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                        worksheet.Range("A5:E5").Style.Font.Bold = true;


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
                                "Allowance Or Deduction Vs Accounts Heads.xlsx"
                                );
                        }
                    }

                }
                else
                {
                    var dataset = await _service.ClassificationVsERPList_Excel(ClassificationCode);
                    DataTable dt = dataset.Tables[0];
                    DataTable dt1 = dataset.Tables[1];



                    //set EXCEL Column Heading 
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {

                        if (dt.Columns[i].ColumnName =="classificationName")
                            dt.Columns[i].ColumnName = dt1.Rows[0].ItemArray[0].ToString();

                        if (dt.Columns[i].ColumnName == "doj" || dt.Columns[i].ColumnName == "dob" || dt.Columns[i].ColumnName == "y/s")
                            dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.ToUpper();
                        else
                            dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);

                    }

                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("Allow&DeductionVsAccHeads");
                        int count = dt.Rows.Count + 6;


                        worksheet.Range("A1:C" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:C" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:C" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:C" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:C" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 2)));
                        wsReportNameHeaderRange.Style.Font.Bold = true;
                        wsReportNameHeaderRange.Style.Font.FontSize = 13;
                        wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                        wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        wsReportNameHeaderRange.Merge();
                        wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                        wsReportNameHeaderRange.Style.Alignment.SetWrapText();
                        wsReportNameHeaderRange.Value = "Allowance Or Deduction Vs Accounts Heads (" + TabIndex + ")";

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
                        worksheet.Cell(5, 2).Value = dt.Rows.Count;




                        var currentRow = 6;

                        worksheet.Cell(currentRow, 1).InsertTable(dt);
                        worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                        worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                        worksheet.Range("A6:C6").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                        worksheet.Range("A6:C6").Style.Font.Bold = true;


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
                                "Allowance Or Deduction Vs Accounts Heads.xlsx"
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
