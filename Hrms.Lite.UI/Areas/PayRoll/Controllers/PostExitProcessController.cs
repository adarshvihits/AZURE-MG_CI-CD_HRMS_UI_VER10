using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.PayRoll;
using Hrms.Lite.Shared.Account;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll;
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

namespace Hrms.Lite.UI.Areas.PayRoll.Controllers
{
    [Area("PayRoll")]
    [SessionAuthorize]
    public class PostExitProcessController : Controller
    {
        private readonly IPostExitProcessService _postExitProcessService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        private readonly IDropdownService _dropdownService;


        public PostExitProcessController(IPostExitProcessService postExitProcessService, ICommonService commonService, IHelpers helpers, IDropdownService dropdownService)
        {
            _postExitProcessService = postExitProcessService;
            _commonService = commonService;
            _helpers = helpers;
            _dropdownService = dropdownService;
        }
        public async Task<IActionResult> Index()
        {
            PostExitProcess model = new PostExitProcess();
         



            model.SettlementFrom = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            model.SettlementTo = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1);
            model.Filter = await _commonService.GetFilterDropdowns(); 
            return View("~/Areas/PayRoll/Views/PostExitProcess/Index.cshtml", model);



            //return View();
        }
        

        [HttpPost]

        public async Task<IActionResult> GetPostExitList(Filter filter1, DateTime fromdate, DateTime todate)
        {
            try
            {
                PostExitProcess model = new PostExitProcess();
                PostExitProcess m = new PostExitProcess();
                Filter filter = new Filter();


                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }

                model = await _postExitProcessService.GetList(fromdate.ToString("yyyy-MMM-dd"), todate.ToString("yyyy-MMM-dd"), filter1);
                //model = await _postExitProcessService.GetList(fromdate.ToString("yyyy-MMM-dd"), todate.ToString("yyyy-MMM-dd"), filter1);

                m.EmployeeList = model.EmployeeList;
                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.EmployeeList == null || model.EmployeeList.Count() == 0)
                {
                    return Json(new
                    {
                        data = "",
                        draw = filter1.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows
                    });
                }

                if (model.EmployeeList.Count > 0)
                {
                    totalrows = m.EmployeeList[0].Active;
                }
                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = m.EmployeeList[0].totalRecords;
                    return Json(new
                    {
                        data = model.EmployeeList,
                        draw = filter1.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows//totalrowsafterfiltering
                    });
                }
                else
                {
                    return Json(new
                    {
                        data = model.EmployeeList,
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


        public async Task<IActionResult> GetPostExitDetails(Guid EmpGI)
        {
            try
            {
                PostExitProcess model = new PostExitProcess();
                //model.Netpay = Netpay;
                model = await _postExitProcessService.GetPostExitDetails(EmpGI);
                model.EmployeeHeader = new Shared.General.EmployeeHeader();
                model.EmployeeHeader = await _commonService.GetEmployeeHeader(EmpGI);
                return PartialView("_Edit", model);
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
        public async Task<IActionResult> Save(PostExitProcess model)
        {
            try
            {

                //model.Filter = Global.GlobalFilter;
                //model.Month = month;
                if(model.Attachment1 != null && model.Attachment1.FileData != null)
                    model.Attachment1 =  FileUpload(model.Attachment1).Result.Data; 

                if(model.Attachment2 != null && model.Attachment2.FileData != null)
                    model.Attachment2 =  FileUpload(model.Attachment2).Result.Data;

                if(model.Attachment3 != null && model.Attachment3.FileData != null)
                    model.Attachment3 =  FileUpload(model.Attachment3).Result.Data;
                var response = await _postExitProcessService.Save(model);
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

        public async Task<ResponseEntity<Hrms.Lite.Shared.File>> FileUpload(Hrms.Lite.Shared.File file)
        {
            //try
            //{
                var StorageType = await _commonService.GetFileStorageType();

                if (StorageType.Data == "BLOB")
                {

                    var fileobj = await _commonService.UploadIformFileToBLOB(file, "PostExitProcessAttachments");
                    if (fileobj.Success == false)
                    {
                        fileobj.Message = "FILE UPLOAD ERROR";
                        //return Json(fileobj);
                    }
                    return fileobj;

                }
                else if (StorageType.Data == "IIS")
                {
                    var fileobj = await _commonService.UploadIformFileToIIS(file, "PostExitProcessAttachments");
                    if (fileobj.Success == false)
                    {
                        fileobj.Message = "FILE UPLOAD ERROR";
                        //return Json(fileobj);
                    }
                    return fileobj;

                }
                else
                {
                    var fileobj = await _commonService.UploadIformFileToAWS(file, "PostExitProcessAttachments");
                    if (fileobj.Success == false)
                    {
                        fileobj.Message = "FILE UPLOAD ERROR";
                        return fileobj;
                    }
                    return fileobj;

            }
                //return Json(null);
            //}
            //catch (UnauthorizedAccessException e)
            //{

            //    return RedirectToAction("Login", "Account", new { Area = "" });
            //}
            //catch (Exception e)
            //{
            //    return RedirectToAction("Error", "Account", new { Area = "" });
            //}
           
        }

        public async Task<IActionResult> ExportToExcel(Filter filter1, DateTime fromdate, DateTime todate,int TotalRecords)
        {
            try
            {
    
                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                //Filter filterdata = new Filter();
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Post Exit Process List", "Excel");
               
                filter1.searchValue = "";
                filter1.length = TotalRecords;

                string FilterValues = await _helpers.GetFilterValues(filter1);

                var dataset = await _postExitProcessService.GetPostExitProcessList_Excel(fromdate.ToString("yyyy-MMM-dd"), todate.ToString("yyyy-MMM-dd"), filter1);
                DataTable dt = dataset.Tables[0];
                dataset.Tables[0].Rows.RemoveAt(0);
                if ((dataset.Tables[0]?.Rows?.Count ?? 0) > 0)
                {
                    dt = dataset.Tables[0];
                }
                //set EXCEL Column Heading 
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (dt.Columns[i].ColumnName == "doj" )
                        dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.ToUpper();
                    else
                    dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
                }

                if (FilterValues == "")
                {
                    FilterValues = "All";
                }
                string currDateTime = await _helpers.GetCurrentDate();
                

                using (XLWorkbook wb = new XLWorkbook())
                {


                    var worksheet = wb.Worksheets.Add("Post Exit List");
                    int rowcount = dt.Rows.Count + 5;
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



                    var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, colName));
                    wsReportNameHeaderRange.Style.Font.Bold = true;
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);

                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Merge();
                    wsReportNameHeaderRange.Value = "Post Exit Process List";

                    worksheet.Cell(2, 1).Style.Font.Bold = true;
                    worksheet.Cell(2, 1).Value = "Group Company Name";
                    worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();



                    worksheet.Cell(2, 3).Style.Font.Bold = true;
                    worksheet.Cell(2, 3).Value = "Settlement From";
                    worksheet.Cell(2, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                 
                    worksheet.Cell(2, 4).Value = fromdate.ToString("dd-MMM-yyyy");

                    worksheet.Cell(2, 5).Style.Font.Bold = true;
                    worksheet.Cell(2, 5).Value = "Settlement To ";
                    worksheet.Cell(2, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);           
                    worksheet.Cell(2, 6).Value = todate.ToString("dd-MMM-yyyy"); ;

                    worksheet.Cell(3, 1).Style.Font.Bold = true;
                    worksheet.Cell(3, 1).Value = "Login User";
                    worksheet.Cell(3, 2).Value = LoginUser.ToString();
                    worksheet.Cell(3, 3).Style.Font.Bold = true;
                    worksheet.Cell(3, 3).Value = "Date & Time";
                    worksheet.Cell(3, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(3, 4).Style.DateFormat.Format = "dd-MMM-yyyy   h:mm AM/PM";
                    worksheet.Cell(3, 4).Value = currDateTime.ToString();
                    worksheet.Cell(3, 5).Style.Font.Bold = true;
                    worksheet.Cell(3, 5).Value = "Total Count";
                    worksheet.Cell(3, 6).Value = dt.Rows.Count;
                    worksheet.Cell(4, 1).Style.Font.Bold = true;
                    worksheet.Cell(4, 1).Value = "Filter Applied";
                    worksheet.Range("B4:" + colName + "4").Merge();
                    worksheet.Cell("B4").Value = FilterValues;

                    var currentRow = 5;

                    worksheet.Cell(currentRow, 1).InsertTable(dt);
                    worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                    worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                    worksheet.Range("A5:" + colName + "5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Range("A5:" + colName + "5").Style.Font.Bold = true;

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
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Post Exit Process List.xlsx");
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
