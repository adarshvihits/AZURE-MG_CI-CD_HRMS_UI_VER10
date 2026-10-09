using ClosedXML.Excel;
using ExcelDataReader;
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
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.PayRoll.Controllers
{
    [Area("PayRoll")]
    [SessionAuthorize]
    public class EmployeeVsInsuranceDetailsController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly IEmployeeVsInsuranceDetailsService _service;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        private readonly IWebHostEnvironment _environment;
        public static class Global
        {
            public static int Policy;            
            public static Filter GlobalFilter;

        }
        public EmployeeVsInsuranceDetailsController(IDropdownService dropdownService, IEmployeeVsInsuranceDetailsService service, ICommonService commonService, IHelpers helpers, IWebHostEnvironment env)
        {
            _dropdownService = dropdownService;
            _service = service;
            _commonService = commonService;
            _helpers = helpers;
            _environment = env;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                EmployeeVsInsuranceDetails model = new EmployeeVsInsuranceDetails();
                model = await _service.GetEmployeeInsuranceDetails(0);
                ViewBag.InsurancePolicy = new SelectList(await _dropdownService.GetInsurancePolicyDropDown(), "Code", "Name");
                await fillfilterDropdown();
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
        public async Task<IActionResult> GetEmployeeVsInsuranceDetails(Guid EmployeeGUID,int PolicyCode) //popup
        {
            EmployeeVsInsuranceDetails model = new EmployeeVsInsuranceDetails();
            model = await _service.GetEmployeeVsInsuranceDetails(EmployeeGUID, PolicyCode);
            model.EmployeeGI = EmployeeGUID;
            
            return PartialView("_Popup", model);
        }
        public async Task<IActionResult> GetEmployeeInsuranceDetails(int Policy,Guid EmployeeGI) //slider
        {
            EmployeeVsInsuranceDetails model = new EmployeeVsInsuranceDetails();
            model = await _service.GetEmployeeInsuranceDetails(Policy);
            model.EmployeeGI = EmployeeGI;
            return PartialView("_InsuranceDetails", model);
        }


        [HttpPost] 
        public async Task<IActionResult> ApplyFilter(Filter filter1, int Policy)
        {
            try
            {
                EmployeeVsInsuranceDetails model = new EmployeeVsInsuranceDetails();
                EmployeeVsInsuranceDetails m = new EmployeeVsInsuranceDetails();

                Filter filter = new Filter();
                Global.GlobalFilter = filter1;

                //m.GridSettings = new List<LeaveRegisterReport_GridSettings>();
                //m = await _leaveRegisterService.GetGridSettings();
                //model.GridSettings.Add(new LeaveRegisterReport_GridSettings());
                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }
                //model.filter = filter1;
                //model.GridSettings = m.GridSettings;
                //model = await _TDSConfirmationService.GetPFEmployeeWiseSettingsList(Deduction, Month, filter1);
                if (Policy != 0)
                {
                    model = await _service.GetEmployeeList(Policy, filter1);
                }
                m.InsuranceDetailsEmployeeList = model.InsuranceDetailsEmployeeList;
                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.InsuranceDetailsEmployeeList == null)
                {
                    //model.RequestVsAuthorityList = new List<RequestVsAuthorityList>();
                    //model.RequestVsAuthorityList.Add(new RequestVsAuthorityList());


                    return Json(new
                    {
                        data = "",
                        draw = filter1.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows
                    });
                }

                if (model.InsuranceDetailsEmployeeList.Count > 0)
                {
                    totalrows = m.InsuranceDetailsEmployeeList[0].active;

                    //m.OnBoardingList.ToList<Hrms.Lite.Shared.Master.Test>().Count; ;
                }


                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = m.InsuranceDetailsEmployeeList[0].totalRecords;
                    //return Json(new { data = model.EmployeeProfileList, draw = filter1.draw, recordsTotal = totalrows, recordsFiltered = totalrowsafterfiltering });
                    return Json(new
                    {
                        data = model.InsuranceDetailsEmployeeList,
                        draw = filter1.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows//totalrowsafterfiltering
                    });
                }
                else
                {

                    return Json(new
                    {
                        data = model.InsuranceDetailsEmployeeList,
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

        public async Task<IActionResult> Create(EmployeeVsInsuranceDetails model)
        {
            var response = await _service.Create(model);
            return Json(response);
        }
        public async Task<IActionResult> Update(EmployeeVsInsuranceDetails model)
        {
            var response = await _service.Update(model);
            return Json(response);
        }

        public async Task<IActionResult> ExportToExcel(int Policy, int totRecords)
        {
            try
            {

                if (Global.GlobalFilter == null)
                    Global.GlobalFilter = new Filter();
                else
                {
                    Global.GlobalFilter.length = totRecords;
                    Global.GlobalFilter.start = 0;
                }
                string FilterValues = await _helpers.GetFilterValues(Global.GlobalFilter);

                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Employee VS Insurance Details", "Excel");

                if (FilterValues == "")
                {
                    FilterValues = "All";
                }

                string currDateTime = await _helpers.GetCurrentDate();
                

                //Get Datset
                var dataset = await _service.GetEmployeeVsInsuranceDetails_Excel(Policy, Global.GlobalFilter);
                DataTable dt = dataset.Tables[0];


                //set EXCEL Column Heading 
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    //if (dt.Columns[i].ColumnName == "doj" || dt.Columns[i].ColumnName == "dob" || dt.Columns[i].ColumnName == "y/s")
                    //    dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.ToUpper();
                    //else
                        dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
                }

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Employee VS Insurance Details");
                    int count = dt.Rows.Count + 5;


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
                    wsReportNameHeaderRange.Value = "Employee VS Insurance Details";

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
                    worksheet.Cell(3, 6).Value = dt.Rows.Count;
                    worksheet.Cell(4, 1).Style.Font.Bold = true;
                    worksheet.Cell(4, 1).Value = "Filter Applied";
                    worksheet.Range("B4:G4").Merge();
                    worksheet.Cell("B4").Value = FilterValues;



                    var currentRow = 5;

                    worksheet.Cell(currentRow, 1).InsertTable(dt);
                    worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                    worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                    worksheet.Range("A5:G5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Range("A5:G5").Style.Font.Bold = true;


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
                            "Employee VS Insurance Details List.xlsx"
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

        public async Task<IActionResult> ImportFromExcel(EmployeeVsInsuranceDetails model)
        {
            string path = Path.Combine(this._environment.WebRootPath, "Uploads");
            string fileName = model.Excel.FileData.FileName;
            try
            {
                model.EmployeeVsInsuranceDetailsList = new List<EmployeeVsInsuranceDetails>();
                string wwwPath = this._environment.WebRootPath;
                string contentPath = this._environment.ContentRootPath;


                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                {
                    model.Excel.FileData.CopyTo(stream);

                }
                try
                {
                    using (var stream = System.IO.File.Open(path + "\\" + fileName, FileMode.Open, FileAccess.Read))
                    {

                        using (var reader = ExcelReaderFactory.CreateReader(stream))
                        {
                            List<EmployeeVsInsuranceDetails> List = new List<EmployeeVsInsuranceDetails>();
                            var readerrr = ExcelReaderFactory.CreateReader(stream);
                            while (reader.Read()) //Each row of the file
                            {
                                var item = new EmployeeVsInsuranceDetails();
                                if (reader.GetValue(0) != null)
                                {
                                    item.EmployeeID = reader.GetValue(0).ToString();
                                }
                                if (reader.GetValue(1) != null)
                                {
                                    item.EmployeeName = reader.GetValue(1).ToString();
                                }
                                if (reader.GetValue(2) != null)
                                {
                                    item.PolicyNumber = reader.GetValue(2).ToString();
                                }
                                if (reader.GetValue(3) != null)
                                {
                                    if (reader.GetValue(3).GetType().Name != "String")
                                    {
                                        item.StartDate = Convert.ToDateTime(reader.GetDateTime(3));
                                    }
                                    else
                                    {
                                        item.StartDateHead = reader.GetValue(3).ToString();
                                    }
                                }
                                if (reader.GetValue(4) != null)
                                {
                                    if (reader.GetValue(4).GetType().Name != "String")
                                    {
                                        item.MaturityDate = Convert.ToDateTime(reader.GetDateTime(4));
                                        //if(item.StartDate>item.MaturityDate)
                                        //return Json(new ResponseEntity("Maturity Date Must be greater than Start Date,Please Correct the Excel", false));
                                    }
                                    else
                                    {
                                        item.MaturityDateHead = reader.GetValue(4).ToString();
                                    }
                                }
                                if (reader.GetValue(5) != null)
                                {
                                    if (reader.GetValue(5).GetType().Name != "String")
                                    {
                                        item.PrincipalAmount = Convert.ToDecimal(reader.GetDouble(5));
                                    }
                                    else
                                    {
                                        item.PrinicipalAmt = reader.GetValue(5).ToString();
                                    }
                                }
                                if (reader.GetValue(6) != null)
                                {
                                    if (reader.GetValue(6).GetType().Name != "String")
                                    {
                                        item.PremiumAmount = Convert.ToDecimal(reader.GetDouble(6));
                                    }
                                    else
                                    {
                                        item.PremiumAmt = reader.GetValue(6).ToString();
                                    }
                                }
                                if (item.PrincipalAmount.ToString().Contains(".")|| item.PremiumAmount.ToString().Contains("."))
                                {
                                    return Json(new ResponseEntity("Decimal values will not be supported.Please round off the decimal values and try again", false));
                                }
                                else
                                {
                                    List.Add(item);
                                }
                            }
                            if (List[0].EmployeeID != "Employee ID" || List[0].EmployeeName != "Employee Name" || List[0].StartDateHead != "Start Date" || List[0].MaturityDateHead != "Maturity Date" || List[0].PrinicipalAmt != "Principal Amount" || List[0].PremiumAmt != "Premium Amount")
                            {
                                reader.Close();
                                if (System.IO.File.Exists(path + "\\" + fileName))
                                {
                                    System.IO.File.Delete(path + "\\" + fileName);
                                }
                                return Json(new ResponseEntity("Excel File not in the prescribed format", false));
                            }
                            model.EmployeeVsInsuranceDetailsList = List;

                            model.EmployeeVsInsuranceDetailsList.RemoveAt(0);
                        }
                    }
                }
                catch (Exception e)
                {
                    // Check if file exists with its full path    
                    if (System.IO.File.Exists(path + "\\" + fileName))
                    {
                        System.IO.File.Delete(path + "\\" + fileName);
                    }
                    return Json(new ResponseEntity("Excel File not in the prescribed format", false));
                }

                model.Excel.FileData = null;
                var response = await _service.ImportFromExcel(model);
                // Check if file exists with its full path    
                if (System.IO.File.Exists(path + "\\" + fileName))
                {
                    System.IO.File.Delete(path + "\\" + fileName);
                }
                if (response.Message != "UPDATED")
                {
                    model.EmployeeVsInsuranceDetailsList = response.Data;
                    model.Message = response.Message;
                    return PartialView("_UnmatchedList", model);
                }
                else
                {
                    response.Message = "Uploaded Successfully";
                    return Json(response);
                }
            }
            catch (UnauthorizedAccessException e)
            {

                return RedirectToAction("Login", "Account", new { Area = "" });
            }
            catch (Exception e)
            {
                // Check if file exists with its full path    
                if (System.IO.File.Exists(path + "\\" + fileName))
                {
                    System.IO.File.Delete(path + "\\" + fileName);
                }
                return RedirectToAction("Error", "Account", new { Area = "" });
            }


        }

    }
}
