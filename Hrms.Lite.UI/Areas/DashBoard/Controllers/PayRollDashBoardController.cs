using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.DashBoard;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.DashBoard;
using Hrms.Lite.Shared.General;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Spire.Xls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.DashBoard.Controllers
{
    [Area("DashBoard")]
    [SessionAuthorize]
    public class PayRollDashBoardController : Controller
    {
        private readonly IPayRollDashBoardService _service;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        public PayRollDashBoardController(IPayRollDashBoardService service, ICommonService commonService, IHelpers helpers)
        {
            _service = service;
            _commonService = commonService;
            _helpers = helpers;
        }
        public static class Global
            {
            public static string type;
            }
        public async Task<IActionResult> Index()
        {
            PayRollDashBoardNew model = new PayRollDashBoardNew();
            PayRollDashBoardNew _model = new PayRollDashBoardNew();
            model = await _service.GetDashBoardDetailsNew();
            _model = await _service.GetPayrollNotifications();
            model.DashBoardNotificationList = _model.DashBoardNotificationList;
            for (int i = 0; i < model.DashBoardNotificationList.Count; i++)
            {
                if (model.DashBoardNotificationList[i].Type == "Loan approval pending")
                {
                    model.DashBoardNotificationList[i].Action = "/PayRoll/LoanIssue/Index";
                }
                if (model.DashBoardNotificationList[i].Type == "Variable Allowance not updated")
                {
                    model.DashBoardNotificationList[i].Action = "/PayRoll/VariableAllowance/Index";
                }
                if (model.DashBoardNotificationList[i].Type == "Variable deductions not updated")
                {
                    model.DashBoardNotificationList[i].Action = "/PayRoll/VariableDeduction/Index";
                }
                if (model.DashBoardNotificationList[i].Type == "Offday wage applicable but settings missing")
                {
                    model.DashBoardNotificationList[i].Action = "/PayRoll/OffdayWageSettings/Index";
                }
                if (model.DashBoardNotificationList[i].Type == "Holiday wage applicable but settings missing")
                {
                    model.DashBoardNotificationList[i].Action = "/PayRoll/HolidayWageSettings/Index";
                }
                if (model.DashBoardNotificationList[i].Type == "Over time wage applicable but settings missing")
                {
                    model.DashBoardNotificationList[i].Action = "/PayRoll/OverTimeSettings/Index";
                }
                if (model.DashBoardNotificationList[i].Type == "PF pension fund expired employees")
                {
                    model.DashBoardNotificationList[i].Action = "/PayRoll/PFSettings/Index";
                }
                if (model.DashBoardNotificationList[i].Type == "PF general settings variance")
                {
                    model.DashBoardNotificationList[i].Action = "/PayRoll/PFSettings/Index";
                }
                if (model.DashBoardNotificationList[i].Type == "PF UAN missing")
                {
                    model.DashBoardNotificationList[i].Action = "/PayRoll/PFSettings/Index";
                }
                if (model.DashBoardNotificationList[i].Type == "PF group missing")
                {
                    model.DashBoardNotificationList[i].Action = "/PayRoll/PFSettings/Index";
                }
                if (model.DashBoardNotificationList[i].Type == "ESI expiry update")
                {
                    model.DashBoardNotificationList[i].Action = "/PayRoll/ESISettings/Index";
                }
                if (model.DashBoardNotificationList[i].Type == "ESI general settings variance")
                {
                    model.DashBoardNotificationList[i].Action = "/PayRoll/ESISettings/Index";
                }
                if (model.DashBoardNotificationList[i].Type == "ESI group missing")
                {
                    model.DashBoardNotificationList[i].Action = "/PayRoll/ESISettings/Index";
                }
                if (model.DashBoardNotificationList[i].Type == "ESI No missing")
                {
                    model.DashBoardNotificationList[i].Action = "/PayRoll/ESISettings/Index";
                }
            }
            return View(model);
        }
        public async Task<IActionResult> IndexV3()
        {
            PayRollDashBoardNew model = new PayRollDashBoardNew();
            PayRollDashBoardNew _model = new PayRollDashBoardNew();
            model = await _service.GetDashBoardDetailsNew();
            _model = await _service.GetPayrollNotifications();
            model.DashBoardNotificationList = _model.DashBoardNotificationList;
            for (int i = 0; i < model.DashBoardNotificationList.Count; i++)
            {
                if (model.DashBoardNotificationList[i].Type == "Loan approval pending")
                {
                    model.DashBoardNotificationList[i].Action = "/PayRoll/LoanIssue/Index";
                }
                if (model.DashBoardNotificationList[i].Type == "Variable Allowance not updated")
                {
                    model.DashBoardNotificationList[i].Action = "/PayRoll/VariableAllowance/Index";
                }
                if (model.DashBoardNotificationList[i].Type == "Variable deductions not updated")
                {
                    model.DashBoardNotificationList[i].Action = "/PayRoll/VariableDeduction/Index";
                }
                if (model.DashBoardNotificationList[i].Type == "Offday wage applicable but settings missing")
                {
                    model.DashBoardNotificationList[i].Action = "/PayRoll/OffdayWageSettings/Index";
                }
                if (model.DashBoardNotificationList[i].Type == "Holiday wage applicable but settings missing")
                {
                    model.DashBoardNotificationList[i].Action = "/PayRoll/HolidayWageSettings/Index";
                }
                if (model.DashBoardNotificationList[i].Type == "Over time wage applicable but settings missing")
                {
                    model.DashBoardNotificationList[i].Action = "/PayRoll/OverTimeSettings/Index";
                }
                if (model.DashBoardNotificationList[i].Type == "PF pension fund expired employees")
                {
                    model.DashBoardNotificationList[i].Action = "/PayRoll/PFSettings/Index";
                }
                if (model.DashBoardNotificationList[i].Type == "PF general settings variance")
                {
                    model.DashBoardNotificationList[i].Action = "/PayRoll/PFSettings/Index";
                }
                if (model.DashBoardNotificationList[i].Type == "PF UAN missing")
                {
                    model.DashBoardNotificationList[i].Action = "/PayRoll/PFSettings/Index";
                }
                if (model.DashBoardNotificationList[i].Type == "PF group missing")
                {
                    model.DashBoardNotificationList[i].Action = "/PayRoll/PFSettings/Index";
                }
                if (model.DashBoardNotificationList[i].Type == "ESI expiry update")
                {
                    model.DashBoardNotificationList[i].Action = "/PayRoll/ESISettings/Index";
                }
                if (model.DashBoardNotificationList[i].Type == "ESI general settings variance")
                {
                    model.DashBoardNotificationList[i].Action = "/PayRoll/ESISettings/Index";
                }
                if (model.DashBoardNotificationList[i].Type == "ESI group missing")
                {
                    model.DashBoardNotificationList[i].Action = "/PayRoll/ESISettings/Index";
                }
                if (model.DashBoardNotificationList[i].Type == "ESI No missing")
                {
                    model.DashBoardNotificationList[i].Action = "/PayRoll/ESISettings/Index";
                }
            }
            return View(model);
        }

        public async Task<IActionResult> IndexV1()
        {

            try
            {
                PayrollDashBoard model = new PayrollDashBoard();
            model = await _service.GetDashBoardDetails();
            var Manpower = await _service.GetDashBoardDiagrams("Manpower");
            model.PayrollDashBoardDiagram = Manpower.PayrollDashBoardDiagram;
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
        public async Task<IActionResult> GetApplicabilityCount()
        {
            PayRollDashBoardNew model = new PayRollDashBoardNew();
            model = await _service.GetDashBoardApplicableCount();
            return PartialView("_ApplicabilityCount", model);
        }
        public async Task<IActionResult> GetDashBoardList(string Type)
        {

            try
            {
                PayRollDashBoardNew model = new PayRollDashBoardNew();
                 model = await _service.GetDashBoardNewList(Type);
                 //model = await _service.GetDashBoardList(Type);
                if (Type == "PayrollConfirmations")
                    return PartialView("_PayrollConfirmations", model);
                if (Type== "FandFPending")         
                return PartialView("_FandFPending", model);
                if (Type == "OutstandingLoanBalance")
                    return PartialView("_OutstandingLoanBalance", model);
                if (Type == "SalaryVariance")
                    return PartialView("_SalaryVariance", model);
                if (Type == "AcNoVariance")
                    return PartialView("_AccNoVariance", model);
                if (Type == "TdsOldRegime" || Type == "TdsNewRegime" || Type == "ActivePayrollEmployees")
                    return PartialView("_TdsPayrollEmployees", model);
                else   
                    return PartialView("_OtherVarience", model);
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


        public async Task<IActionResult> GetPFAndESIDetails(string Type,string SubType)
        {

            try
            {
                PayrollDashBoard model = new PayrollDashBoard();
                model = await _service.GetPFAndESIDetails(Type, SubType);              
                    if (SubType== "YES" || SubType == "NO")
                    return PartialView("_PFandESI", model);
                else
                    return PartialView("_PFandESIVariance", model);
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

        public async Task<IActionResult> GetApplicableCountDetails(string Type, string SubType)
        {

            try
            {
                PayRollDashBoardNew model = new PayRollDashBoardNew();
                model = await _service.GetApplicableCountDetails(Type, SubType);
                model.ApplicableType = Type;
                model.ApplicableSubType = SubType;
                if (SubType == "YES" || SubType == "NO")
                    return PartialView("_ApplicabilityPopUp", model);
                else
                    return PartialView("_ApplicabilityVariancePopUp", model);
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


        public async Task<IActionResult> GetGraph(string Classification)
        {
            try
            {
                PayRollDashBoardNew model = new PayRollDashBoardNew();
                model = await _service.GetPayrollGraphDetails(Classification);
                ViewBag.Classsification = Classification;
                return PartialView("_SalaryChart", model.GraphList);
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


        public async Task<IActionResult> ExportToExcel(string Type, string SubType,string PfOrEsi)
        {
            try
            {
                PayrollDashBoard model = new PayrollDashBoard();
                //if (Global.GlobalFilter == null)
                //    Global.GlobalFilter = new Filter();
                //string FilterValues = await _helpers.GetFilterValues(Global.GlobalFilter);
                model = await _service.GetPFAndESIDetails(Type, SubType);
                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword(PfOrEsi + " Applicability", "Excel");
                int slNo = 0;
                //if (FilterValues == "")
                //{
                //    FilterValues = "All";
                //}
                string currDateTime = await _helpers.GetCurrentDate();
                

                if (SubType == "YES" || SubType == "NO")
                {
                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add(PfOrEsi + " Applicability");
                        int count = model.ESI_PFApplicable.Count + 4;


                        worksheet.Range("A1:G" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:G" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:G" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:G" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:G" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 6)));
                        wsReportNameHeaderRange.Style.Font.Bold = true;
                        wsReportNameHeaderRange.Style.Font.FontSize = 15;
                        wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                        wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        wsReportNameHeaderRange.Merge();
                        wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191); ;

                        wsReportNameHeaderRange.Value = PfOrEsi + " Applicability";
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
                        worksheet.Cell(3, 5).Style.Font.Bold = true;
                        worksheet.Cell(3, 5).Value = "Total Count";
                        worksheet.Cell(3, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(3, 6).Value = model.ESI_PFApplicable.Count;
                        //worksheet.Cell(4, 1).Style.Font.Bold = true;
                        //worksheet.Cell(4, 1).Value = "Filter Applied";
                        //worksheet.Range("B3:H3").Merge();
                        //worksheet.Cell("B4").Value = FilterValues;

                        var currentRow = 4;
                        #region Header
                        worksheet.Range("A4:G4").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                        worksheet.Cell(currentRow, 1).Value = "Sl.No";
                        worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 2).Value = "ID";
                        worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 3).Value = "Employee Name";
                        worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 4).Value = "Designation";
                        worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 5).Value = "Department";
                        worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 6).Value = "Location";
                        worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 7).Value = PfOrEsi+" Group";
                        worksheet.Cell(currentRow, 7).Style.Font.Bold = true;                        


                        #endregion

                        #region Body
                        foreach (var item in model.ESI_PFApplicable)
                        {
                            currentRow++;
                            slNo = slNo + 1;
                            worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 1).Value = slNo;
                            worksheet.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 2).Value = item.EmployeeID;
                            worksheet.Cell(currentRow, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 3).Value = item.FullName;
                            worksheet.Cell(currentRow, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 4).Value = item.Designation;
                            worksheet.Cell(currentRow, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 5).Value = item.Department;
                            worksheet.Cell(currentRow, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 6).Value = item.Location;
                            worksheet.Cell(currentRow, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 7).Value = item.PfEsiGroup;
                            

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
                                PfOrEsi + " Applicability.xlsx"
                                );
                        }
                    }
                }
                //else if (TabIndex == 1 || TabIndex == 2 || TabIndex == 3)
                //{
                //    using (var workbook = new XLWorkbook())
                //    {
                //        var worksheet = workbook.Worksheets.Add(TabName + " List");
                //        var currentRow = 1;
                //        #region Header
                //        worksheet.Cell(currentRow, 1).Value = "ID";
                //        worksheet.Cell(currentRow, 2).Value = "EmployeeName";
                //        worksheet.Cell(currentRow, 3).Value = "Designation";
                //        worksheet.Cell(currentRow, 4).Value = "Department";
                //        worksheet.Cell(currentRow, 5).Value = "Location";
                //        worksheet.Cell(currentRow, 6).Value = "DOJ";
                //        worksheet.Cell(currentRow, 7).Value = "Y/S";
                //        worksheet.Cell(currentRow, 8).Value = "LWD";
                //        worksheet.Cell(currentRow, 9).Value = "Reason";


                //        #endregion

                //        #region Body
                //        foreach (var item in model.ResignationList)
                //        {
                //            currentRow++;
                //            worksheet.Cell(currentRow, 1).Value = item.EmployeeID;
                //            worksheet.Cell(currentRow, 2).Value = item.EmployeeName;
                //            worksheet.Cell(currentRow, 3).Value = item.Designation.Name;
                //            worksheet.Cell(currentRow, 4).Value = item.Department.Name;
                //            worksheet.Cell(currentRow, 5).Value = item.Location.Name;
                //            worksheet.Cell(currentRow, 6).Value = item.DOJ;
                //            worksheet.Cell(currentRow, 7).Value = item.YOS;
                //            worksheet.Cell(currentRow, 8).Value = item.LWD;
                //            worksheet.Cell(currentRow, 9).Value = item.Reason;

                //        }
                //        #endregion

                //        using (var stream = new MemoryStream())
                //        {
                //            workbook.SaveAs(stream);
                //            var content = stream.ToArray();
                //            return File(
                //                content,
                //                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                //                TabName + " List.xlsx"
                //                );
                //        }
                //    }
                //}
                else
                {
                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add(PfOrEsi + " Applicability");
                        int count = model.ESI_PF_Variance_Applicable.Count + 4;


                        worksheet.Range("A1:J" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:J" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:J" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:J" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:J" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 9)));
                        wsReportNameHeaderRange.Style.Font.Bold = true;
                        wsReportNameHeaderRange.Style.Font.FontSize = 15;
                        wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                        wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        wsReportNameHeaderRange.Merge();
                        wsReportNameHeaderRange.Value = PfOrEsi + " Applicability";
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
                        worksheet.Cell(3, 5).Style.Font.Bold = true;
                        worksheet.Cell(3, 5).Value = "Total Count";
                        worksheet.Cell(3, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(3, 6).Value = model.ESI_PF_Variance_Applicable.Count;
                        //worksheet.Cell(4, 1).Style.Font.Bold = true;
                        //worksheet.Cell(4, 1).Value = "Filter Applied";
                        //worksheet.Range("B3:H3").Merge();
                        //worksheet.Cell("B4").Value = FilterValues;

                        var currentRow = 4;
                        #region Header
                        worksheet.Range("A4:J4").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                        worksheet.Cell(currentRow, 1).Value = "Sl.No";
                        worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 2).Value = "ID";
                        worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 3).Value = "Employee Name";
                        worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 4).Value = "Designation";
                        worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 5).Value = "Department";
                        worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 6).Value = "Location";
                        worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 7).Value = "Old"+PfOrEsi+"Grp";
                        worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 8).Value = "New" + PfOrEsi + "Grp";
                        worksheet.Cell(currentRow, 8).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 9).Value = "Old Appli.";
                        worksheet.Cell(currentRow, 9).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 10).Value = "New Appli.";
                        worksheet.Cell(currentRow, 10).Style.Font.Bold = true;

                        #endregion

                        #region Body
                        foreach (var item in model.ESI_PF_Variance_Applicable)
                        {
                            currentRow++;
                            slNo = slNo + 1;
                            worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 1).Value = slNo;
                            worksheet.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 2).Value = item.EmployeeID;
                            worksheet.Cell(currentRow, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 3).Value = item.FullName;
                            worksheet.Cell(currentRow, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 4).Value = item.Designation;
                            worksheet.Cell(currentRow, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 5).Value = item.Department;
                            worksheet.Cell(currentRow, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 6).Value = item.Location;
                            worksheet.Cell(currentRow, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 7).Value = item.OldPfEsiGroup;
                            worksheet.Cell(currentRow, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 8).Value = item.NewPfEsiGroup;
                            worksheet.Cell(currentRow, 9).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 9).Value = item.OldPfEsiApplicable;
                            worksheet.Cell(currentRow, 10).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 10).Value = item.NewPfEsiApplicable;

                            
                            worksheet.Columns(1, 11).Style.Alignment.WrapText = true;
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
                                PfOrEsi + " Applicability.xlsx"
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




        public async Task<ActionResult> GetEmployeeDetailsPopUp(string Type)
        {
            try
            {
                PayRollDashBoardNew model = new PayRollDashBoardNew();

                Global.type = Type;
                ViewBag.type = Type;
                model = await _service.GetDashBoardPopUpList(Type);
                return PartialView("_NotificationPopUp", model);



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

        public async Task<IActionResult> ExportToExcel_PopUp(int TableLength)
        {
            try
            {
                PayRollDashBoardNew model = new PayRollDashBoardNew();


               

                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("PayrollNotification", "Excel");
                int slNo = 0;

                Filter filter = new Filter();
                string currDateTime = await _helpers.GetCurrentDate();
                

                model = await _service.GetDashBoardPopUpList(Global.type); ;
                int Totcount = model.PayrollDashBoardList.Count+4;
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Payroll Notification");

                    int count = model.PayrollDashBoardList.Count + 4;

                    string columnName = "";
                    int Columncount;
                    if (Global.type == "Employment PF variance" || Global.type == "Employer Contribution to EPS variance" || Global.type == "Employee Maximum Limit variance" || Global.type == "Employer Maximum Limit variance")
                    {
                        Columncount = 8;
                    }
                    if (Global.type == "Employer contribution to EPF variance")
                    {
                        Columncount = 9;

                    }
                    else
                    {
                         Columncount = 6;
                    }
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
                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191); ;
                    wsReportNameHeaderRange.Value ="PayRoll Notification("+ Global.type+")";



                 
                            worksheet.Cell(2, 1).Style.Font.Bold = true;
                            worksheet.Cell(2, 1).Value = "Company Name";
                            worksheet.Cell(2, 2).Value = CompanyName.ToUpper();


                            worksheet.Cell(2, 3).Style.Font.Bold = true;
                            worksheet.Cell(2, 3).Value = "Type";
                            worksheet.Cell(2, 4).Value = Global.type;
                         


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
                            worksheet.Cell(3, 6).Value = model.PayrollDashBoardList.Count;
                     
                    var currentRow = 4;
                


                    #region Header
                    worksheet.Range("A4", colName+4).Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);

                    

                    worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 1).Value = "Sl.No";
                    worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 2).Value = "ID";
                    worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 3).Value = "Employee Name";
                    worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 4).Value = "Designation";
                    worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 5).Value = "Department";
                    worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 6).Value = "Location";
                    if(Global.type == "Employment PF variance" || Global.type == "Employer Contribution to EPS variance" || Global.type == "Employer contribution to EPF variance" || Global.type == "Employee Maximum Limit variance" || Global.type == "Employer Maximum Limit variance")
        {
                        worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 7).Value = "Based on Settings";
                        worksheet.Cell(currentRow, 8).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 8).Value = "Current";

                        if (Global.type == "Employer contribution to EPF variance")
                    {
                            worksheet.Cell(currentRow, 9).Style.Font.Bold = true;
                            worksheet.Cell(currentRow, 9).Value = "Age";
                        }
                    }




                        foreach (var item in model.PayrollDashBoardList)
                    {
                        currentRow++;
                        slNo = slNo + 1;

                        

                        worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 1).Value = slNo;
                        worksheet.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 2).Value = item.EmployeeID;
                        worksheet.Cell(currentRow, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 3).Value = item.FullName;
                        worksheet.Cell(currentRow, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 4).Value = item.Designation;
                        worksheet.Cell(currentRow, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 5).Value = item.Department;
                        worksheet.Cell(currentRow, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 6).Value = item.Location;
                        if (Global.type == "Employment PF variance" || Global.type == "Employer Contribution to EPS variance" || Global.type == "Employer contribution to EPF variance" || Global.type == "Employee Maximum Limit variance" || Global.type == "Employer Maximum Limit variance")
                        {
                            worksheet.Cell(currentRow, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 7).Value = item.OldValue;
                            worksheet.Cell(currentRow, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 8).Value = item.CurrentValue;
                            if (Global.type == "Employer contribution to EPF variance")
                            {
                                worksheet.Cell(currentRow, 9).Value = item.Age;
                            }
                        }



                            worksheet.Columns(1, 20).Style.Alignment.WrapText = true;
                    }
                    #endregion
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
                          "Payroll dashboard Notification " + " List.xlsx"
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


        public async Task<IActionResult> ExportToExcelApplicabilityCount(string Type, string SubType)
        {
            try
            {
                PayRollDashBoardNew model = new PayRollDashBoardNew();
                //if (Global.GlobalFilter == null)
                //    Global.GlobalFilter = new Filter();
                //string FilterValues = await _helpers.GetFilterValues(Global.GlobalFilter);
                model = await _service.GetApplicableCountDetails(Type, SubType);
                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword(Type + " Applicability", "Excel");
                int slNo = 0;
                //if (FilterValues == "")
                //{
                //    FilterValues = "All";
                //}
                string currDateTime = await _helpers.GetCurrentDate();
                

                if (SubType == "YES" || SubType == "NO")
                {
                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add(Type + " Applicability");
                        int count = model.ApplicableList.Count + 4;


                        worksheet.Range("A1:G" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:G" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:G" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:G" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:G" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 6)));
                        wsReportNameHeaderRange.Style.Font.Bold = true;
                        wsReportNameHeaderRange.Style.Font.FontSize = 15;
                        wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                        wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        wsReportNameHeaderRange.Merge();
                        wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191); ;

                        wsReportNameHeaderRange.Value = Type + " Applicability";
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
                        worksheet.Cell(3, 5).Style.Font.Bold = true;
                        worksheet.Cell(3, 5).Value = "Total Count";
                        worksheet.Cell(3, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(3, 6).Value = model.ApplicableList.Count;
                        //worksheet.Cell(4, 1).Style.Font.Bold = true;
                        //worksheet.Cell(4, 1).Value = "Filter Applied";
                        //worksheet.Range("B3:H3").Merge();
                        //worksheet.Cell("B4").Value = FilterValues;

                        var currentRow = 4;
                        #region Header
                        worksheet.Range("A4:G4").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                        worksheet.Cell(currentRow, 1).Value = "Sl.No";
                        worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 2).Value = "ID";
                        worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 3).Value = "Employee Name";
                        worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 4).Value = "Designation";
                        worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 5).Value = "Department";
                        worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 6).Value = "Location";
                        worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 7).Value = Type + " Group";
                        worksheet.Cell(currentRow, 7).Style.Font.Bold = true;


                        #endregion

                        #region Body
                        foreach (var item in model.ApplicableList)
                        {
                            currentRow++;
                            slNo = slNo + 1;
                            worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 1).Value = slNo;
                            worksheet.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 2).Value = item.EmployeeID;
                            worksheet.Cell(currentRow, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 3).Value = item.FullName;
                            worksheet.Cell(currentRow, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 4).Value = item.Designation;
                            worksheet.Cell(currentRow, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 5).Value = item.Department;
                            worksheet.Cell(currentRow, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 6).Value = item.Location;
                            worksheet.Cell(currentRow, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 7).Value = item.GroupName;


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
                                Type + " Applicability.xlsx"
                                );
                        }
                    }
                }
                
                else
                {
                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add(Type + " Applicability");
                        int count = model.ESI_PF_PT_Variance_Applicable.Count + 4;


                        worksheet.Range("A1:J" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:J" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:J" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:J" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:J" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 9)));
                        wsReportNameHeaderRange.Style.Font.Bold = true;
                        wsReportNameHeaderRange.Style.Font.FontSize = 15;
                        wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                        wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        wsReportNameHeaderRange.Merge();
                        wsReportNameHeaderRange.Value = Type + " Applicability";
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
                        worksheet.Cell(3, 5).Style.Font.Bold = true;
                        worksheet.Cell(3, 5).Value = "Total Count";
                        worksheet.Cell(3, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(3, 6).Value = model.ESI_PF_PT_Variance_Applicable.Count;
                        //worksheet.Cell(4, 1).Style.Font.Bold = true;
                        //worksheet.Cell(4, 1).Value = "Filter Applied";
                        //worksheet.Range("B3:H3").Merge();
                        //worksheet.Cell("B4").Value = FilterValues;

                        var currentRow = 4;
                        #region Header
                        worksheet.Range("A4:J4").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                        worksheet.Cell(currentRow, 1).Value = "Sl.No";
                        worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 2).Value = "ID";
                        worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 3).Value = "Employee Name";
                        worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 4).Value = "Designation";
                        worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 5).Value = "Department";
                        worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 6).Value = "Location";
                        worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 7).Value = "Old" + Type + "Grp";
                        worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 8).Value = "New" + Type + "Grp";
                        worksheet.Cell(currentRow, 8).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 9).Value = "Old Appli.";
                        worksheet.Cell(currentRow, 9).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 10).Value = "New Appli.";
                        worksheet.Cell(currentRow, 10).Style.Font.Bold = true;

                        #endregion

                        #region Body
                        foreach (var item in model.ESI_PF_PT_Variance_Applicable)
                        {
                            currentRow++;
                            slNo = slNo + 1;
                            worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 1).Value = slNo;
                            worksheet.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 2).Value = item.EmployeeID;
                            worksheet.Cell(currentRow, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 3).Value = item.FullName;
                            worksheet.Cell(currentRow, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 4).Value = item.Designation;
                            worksheet.Cell(currentRow, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 5).Value = item.Department;
                            worksheet.Cell(currentRow, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 6).Value = item.Location;
                            worksheet.Cell(currentRow, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 7).Value = item.OldGroupName;
                            worksheet.Cell(currentRow, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 8).Value = item.NewGroupName;
                            worksheet.Cell(currentRow, 9).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 9).Value = item.OldApplicable;
                            worksheet.Cell(currentRow, 10).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 10).Value = item.NewApplicable;


                            worksheet.Columns(1, 11).Style.Alignment.WrapText = true;
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
                                Type + " Applicability.xlsx"
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
