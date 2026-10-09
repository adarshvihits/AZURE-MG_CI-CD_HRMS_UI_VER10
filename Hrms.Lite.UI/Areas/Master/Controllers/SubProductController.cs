using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.Master;
using Hrms.Lite.Shared.Account;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
    public class SubProductController : Controller
    {
        private readonly ISubProductService _service;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        public SubProductController(ISubProductService service, ICommonService commonService, IHelpers helpers)
        {
            _service = service;
            _commonService = commonService;
            _helpers = helpers;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                return View(await _service.GetList("ACTIVE"));
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

        public async Task<ActionResult> GetSubproductList(string TabIndex)
        {
            try
            {
                if (TabIndex != "APPROVAL_PENDING")
                    return PartialView("_SubProductList", await _service.GetList(TabIndex));
                else
                    return PartialView("_SubProductPendingList", await _service.GetList(TabIndex));
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


        public ActionResult Create()
        {
            try
            {
                SubProduct model = new SubProduct();
                model.Mode = "CREATE";
                model.Active = true;
                return PartialView("~/Areas/Master/Views/SubProduct/_CreateOrEdit.cshtml", model);
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


        public async Task<ActionResult> Save(SubProduct input)
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


        public async Task<ActionResult> Edit(Guid SubProductGI, Guid LogGI)
        {
            try
            {
                SubProduct model = await _service.GetSubProductDetails(SubProductGI, LogGI, "MODIFY");
                model.Mode = "MODIFY";
                return PartialView("_CreateOrEdit", model);
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


        public async Task<ActionResult>GetDeleteDetails(Guid SubProductGI, Guid LogGI)
        {
            try
            {
                SubProduct model = await _service.GetSubProductDetails(SubProductGI, LogGI, "DELETE");
                model.Mode = "DELETE";
                return PartialView("_Delete", model);
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


        public async Task<ActionResult> Deletee(SubProduct model)
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

        public async Task<ActionResult> GetApproveDetails(Guid SubProductGI, Guid LogGI)
        {
            try
            {
                SubProduct model = await _service.GetSubProductDetails(SubProductGI, LogGI, "APPROVAL");
                return PartialView("_Approve", model);
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
        public async Task<ActionResult> Approve(SubProduct model)
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

        public async Task<ActionResult> Reject(SubProduct model)
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

        public async Task<IActionResult> ExportToExcel(String TabIndex)
        {
            var LoginUser = HttpContext.Session.GetString("LoginUser");
            var CompanyName = HttpContext.Session.GetString("CompanyName");
            var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
            var GroupCompanyName = User.authUser.GroupCompanyName;
            ExcelPassword EP = new ExcelPassword();
            EP = await _commonService.GetExcelPassword("Sub Product", "Excel");

            string currDateTime = await _helpers.GetCurrentDate();

            

            Product model = new Product();
            var dataset = await _service.GetList_Excel(TabIndex);

            DataTable dt = dataset.Tables[0];

            //set EXCEL Column Heading 
            for (int i = 0; i < dt.Columns.Count; i++)
            {

                dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                var worksheet = wb.Worksheets.Add("Sub Product");
                int rowcount = dt.Rows.Count + 4;
                string columnName = "";
                int Columncount = dt.Columns.Count;

                while (Columncount > 0)
                {
                    int modulo = (Columncount - 1) % 26;
                    columnName = Convert.ToChar('A' + modulo) + columnName;
                    Columncount = (Columncount - modulo) / 26;
                }

                string colName = columnName;

                worksheet.Range("A1", colName + rowcount).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1", colName + rowcount).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1", colName + rowcount).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1", colName + rowcount).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1", colName + rowcount).Style.Border.LeftBorder = XLBorderStyleValues.Thin;

                var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 3)));
                wsReportNameHeaderRange.Style.Font.Bold = true;
                wsReportNameHeaderRange.Style.Font.FontSize = 15;
                wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);

                wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                wsReportNameHeaderRange.Merge();
                wsReportNameHeaderRange.Value = "Sub Product (" + TabIndex + ")";

                worksheet.Cell(2, 1).Style.Font.Bold = true;
                worksheet.Cell(2, 1).Value = "Group Company Name";
                worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();

                worksheet.Cell(2, 3).Style.Font.Bold = true;
                worksheet.Cell(2, 3).Value = "Login User";
                worksheet.Cell(2, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                worksheet.Cell(2, 4).Value = LoginUser.ToString();

                worksheet.Cell(3, 1).Style.Font.Bold = true;
                worksheet.Cell(3, 1).Value = "Date & Time";
                worksheet.Cell(3, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                worksheet.Cell(3, 2).Style.DateFormat.Format = "dd-MMM-yyyy   h:mm AM/PM";
                worksheet.Cell(3, 2).Value = currDateTime.ToString();
                worksheet.Cell(3, 3).Style.Font.Bold = true;
                worksheet.Cell(3, 3).Value = "Total Count";
                worksheet.Cell(3, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                worksheet.Cell(3, 4).Value = dt.Rows.Count;

                var currentRow = 4;

                worksheet.Cell(currentRow, 1).InsertTable(dt);
                worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                worksheet.Range("A4:" + colName + "4").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                worksheet.Range("A4:" + colName + "4").Style.Font.Bold = true;

                worksheet.Columns().AdjustToContents();
                worksheet.Rows().AdjustToContents();
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    if (EP.NeedPassword)
                    {
                        Workbook book = new Workbook();
                        book.LoadFromStream(stream);
                        //Protect Workbook
                        book.Protect(EP.Password, false, false);
                        book.SaveToStream(stream);
                    }
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Sub Product Details(" + TabIndex + ").xlsx");
                }
            }
        }

    }
}

    
