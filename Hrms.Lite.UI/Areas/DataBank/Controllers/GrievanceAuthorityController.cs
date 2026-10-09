using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.Account;
using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.General;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
    public class GrievanceAuthorityController : Controller
    {
        private readonly ICommonService _commonService;
        private readonly IDropdownService _dropdownService;
        private readonly IGrievanceAuthorityService _GrievanceAuthorityService;
        private readonly IHelpers _helpers;
        
        public GrievanceAuthorityController(ICommonService commonService, IDropdownService dropdownService, IGrievanceAuthorityService GrievanceAuthorityService, IHelpers helpers)
        {
            _commonService = commonService;
            _dropdownService = dropdownService;
            _GrievanceAuthorityService = GrievanceAuthorityService;
      
            _helpers = helpers;
        }
        public async Task<IActionResult> Index()
        {
            GrievanceAuthority model = new GrievanceAuthority();
            ViewBag.Authority = new SelectList(await _dropdownService.GetEmployeeDropDown(), "Code", "Name");
            model = await _GrievanceAuthorityService.GetList();        
            return View(model);
        }

        public async Task<IActionResult> GetDetails(int GrievanceCatID)
        {
            try { 
            GrievanceAuthority model1 = new GrievanceAuthority();
                GrievanceAuthority model = new GrievanceAuthority();
             
                ViewBag.Authority = new SelectList(await _dropdownService.GetEmployeeDropDown(), "Code", "Name");
                model = await _GrievanceAuthorityService.GetDetails(GrievanceCatID);
                model1 = await _GrievanceAuthorityService.GetRefNumber();
                model.RefNo = model1.RefNo;
                model.RequestedDate = model1.RequestedDate;
                return PartialView("_EditGrievanceAuthority",model);
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


        public async Task<IActionResult> GetList()
        {
            try
            {
                GrievanceAuthority model = new GrievanceAuthority();

                model = await _GrievanceAuthorityService.GetList();
                return PartialView("_GrievanceAuthorityList", model);


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
        public async Task<ActionResult> Save(GrievanceAuthority input)
        {
            try
            {
                var response = await _GrievanceAuthorityService.Save(input);

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
        public async Task<IActionResult> ExportToExcel()
        {
            try
            {
                GrievanceAuthority model = new GrievanceAuthority();

                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                int slNo = 0;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword(" Bank Statement Report", "Excel");
                //Filter filter = new Filter();
                //filter = JsonConvert.DeserializeObject<Filter>(TempData["FilterModel"].ToString());
               

                model = await _GrievanceAuthorityService.GetList();

                int no = model.GrievanceAuthorityList.Count();
                string currDateTime = await _helpers.GetCurrentDate();
                
                using (var workbook = new XLWorkbook())
                {

                    var worksheet = workbook.Worksheets.Add("Grievance Authority List");


                    int count = model.GrievanceAuthorityList.Count + 6;


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
                    wsReportNameHeaderRange.Value = "Grievance Authority List";

                    worksheet.Cell(2, 1).Style.Font.Bold = true;
                    worksheet.Cell(2, 1).Value = "Group Company Name";
                    worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                   
                   

                    worksheet.Cell(3, 1).Style.Font.Bold = true;
                    worksheet.Cell(3, 1).Value = "Login User";
                    worksheet.Cell(3, 2).Value = LoginUser;
                    worksheet.Cell(3, 3).Style.Font.Bold = true;

                    worksheet.Cell(4, 1).Value = "Date & Time";
                    worksheet.Cell(4, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(4, 2).Style.DateFormat.Format = "dd-MMM-yyyy   h:mm AM/PM";
                    worksheet.Cell(4, 2).Value = currDateTime.ToString();
                    worksheet.Cell(5, 1).Style.Font.Bold = true;
                    worksheet.Cell(5, 1).Value = "Total Count";
                    worksheet.Cell(5, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(5, 2).Value = model.GrievanceAuthorityList.Count();
                

            


                    var currentRow = 6;
                    #region Header
                    worksheet.Range("A6:C6").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Cell(currentRow, 1).Value = "Sl.No";
                    worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 2).Value = "Grievance Category";
                    worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 3).Value = "Authority Name";
                    worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                   
                   



                    #endregion

                    #region Body

                    foreach (var item in model.GrievanceAuthorityList)
                    {
                        currentRow++;
                        slNo = slNo + 1;

                        worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 1).Value = item.SlNo;
                        worksheet.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 2).Value = item.CategoryName;
                        worksheet.Cell(currentRow, 3).Value = item.EmployeeInfo.FullName;
                     



                        worksheet.Columns(1, 7).Style.Alignment.WrapText = true;
                        worksheet.Rows().Style.Alignment.WrapText = true;
                    }
                    #endregion
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
                            book.Protect(EP.Password,false,false);
                            book.SaveToStream(stream);
                        }
                        var content = stream.ToArray();
                        return File(
                            content,
                            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                            "Grievance Authority List.xlsx"
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
