using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.Master;
using Hrms.Lite.Shared.Account;
using Hrms.Lite.Shared.DataBank;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Spire.Xls;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.DataBank.Controllers
{
    [Area("DataBank")]
    [SessionAuthorize]


    public class DepartmentWiseERPCostCenterController : Controller
    {
        private readonly ICommonService _commonService;
        private readonly IDropdownService _dropdownService;
        private readonly IDepartmentWiseERPCostCenterService _DepartmentWiseERPCostCenterService;
        private readonly IHelpers _helpers;

        public DepartmentWiseERPCostCenterController(ICommonService commonService, IDropdownService dropdownService, IDepartmentWiseERPCostCenterService DepartmentWiseERPCostCenterService, IHelpers helpers)
        {
            _commonService = commonService;
            _dropdownService = dropdownService;
            _DepartmentWiseERPCostCenterService = DepartmentWiseERPCostCenterService;

            _helpers = helpers;
        }

        public async Task<IActionResult> Index()
        {
            DepartmentWiseERPCostCenter model = new DepartmentWiseERPCostCenter();
            ViewBag.CostCenterDropdown = new SelectList(await _dropdownService.GetERPCostCenterDropDown(), "Code", "Name");
            ViewBag.GroupCompanyDropdown = new SelectList(await _dropdownService.GetGroupCompanyDropDown(), "Code", "Name");

            return View(model);
        }




        public async Task<ActionResult> GetList(int groupCompanyCode)
        {

            try
            {
                ViewBag.CostCenterDropdown = new SelectList(await _dropdownService.GetERPCostCenterDropDown(), "Code", "Name");
                return PartialView("DepartmentWiseERPCostCenterList", await _DepartmentWiseERPCostCenterService.GetList(groupCompanyCode));
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


        public async Task<ActionResult> Save(int UpdatedValue, int DepartmentCode, int groupCompanyCode)
        {
            try
            {
                DepartmentWiseERPCostCenter model = new DepartmentWiseERPCostCenter();
                model.CostCenterCode = UpdatedValue;
                model.DepartmentCode = DepartmentCode;
                model.GroupCompanyCode = groupCompanyCode;
                var response = await _DepartmentWiseERPCostCenterService.Save(model);
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


        public async Task<IActionResult> ExportToExcel(int totRecords, int groupCompanyCode)
        {
            try
            {
                DepartmentWiseERPCostCenter _model = new DepartmentWiseERPCostCenter();

                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                int slNo = 0;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Department Wise Cost Center", "Excel");




                string currDateTime = await _helpers.GetCurrentDate();
                


                //Get Dataset
                var dataset = await _DepartmentWiseERPCostCenterService.GetDepartmentWiseERPCostCenter_Excel(groupCompanyCode);
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

                    var worksheet = workbook.Worksheets.Add("Department Wise Cost Center");

                    int count = dt.Rows.Count + 5;

                    worksheet.Range("A1:C" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:C" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:C" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:C" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:C" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;



                    var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 2)));
                    wsReportNameHeaderRange.Style.Font.Bold = true;
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Merge();
                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                    wsReportNameHeaderRange.Value = "Department Wise Cost Center";

                    worksheet.Cell(2, 1).Style.Font.Bold = true;
                    worksheet.Cell(2, 1).Value = "Group Company Name";
                    worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                    worksheet.Cell(4, 1).Style.Font.Bold = true;
                    worksheet.Cell(4, 1).Value = "Login User";
                    worksheet.Cell(4, 2).Value = LoginUser;

                    worksheet.Cell(3, 1).Style.Font.Bold = true;
                    worksheet.Cell(3, 1).Value = "Date & Time";
                    worksheet.Cell(3, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(3, 2).Style.DateFormat.Format = "dd-MMM-yyyy   h:mm AM/PM";
                    worksheet.Cell(3, 2).Value = currDateTime.ToString();
                    worksheet.Cell(5, 1).Style.Font.Bold = true;
                    worksheet.Cell(5, 1).Value = "Total Count";
                    worksheet.Cell(5, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(5, 2).Value = dt.Rows.Count;


                    var currentRow = 6;

                    worksheet.Cell(currentRow, 1).InsertTable(dt);
                    worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                    worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                    worksheet.Range("A6:C6").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Range("A6:C6").Style.Font.Bold = true;


                    worksheet.Columns().AdjustToContents();
                    worksheet.Rows().AdjustToContents();
                    //worksheet.Protect("123");
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
                            "Department Wise Cost Center List.xlsx"
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

