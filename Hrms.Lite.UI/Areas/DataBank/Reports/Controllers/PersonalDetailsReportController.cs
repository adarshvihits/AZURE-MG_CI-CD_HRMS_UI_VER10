using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Services.IServices.DataBank.Reports;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.Account;
using Hrms.Lite.Shared.DataBank.Generic.PersonalDetails;
using Hrms.Lite.Shared.DataBank.Reports;
using Hrms.Lite.Shared.General;
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

namespace Hrms.Lite.UI.Areas.DataBank.Reports.Controllers
{
    [Area("DataBank")]
    [SessionAuthorize]
    public class PersonalDetailsReportController : Controller
    {
        private readonly IEmployeeInformationReportService _employeeInformationReportService;
        IPersonalDetailsService _personalDetailsService;
        private readonly IDropdownService _dropdownService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        public static class Global
        {   
            public static Filter GlobalFilter;
            public static EmployeeInformationReport _exportdata;
        }
        public PersonalDetailsReportController(IEmployeeInformationReportService employeeInformationReportService, IDropdownService dropdownService, ICommonService commonService, IHelpers helpers, IPersonalDetailsService personalDetailsService)
        {
            _employeeInformationReportService = employeeInformationReportService;
            _personalDetailsService = personalDetailsService;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = helpers;
        }
     
        public async Task<IActionResult> Index()
        {
            try { 
            EmployeeInformationReport model = new EmployeeInformationReport();
            model.filter = new Filter();
            model.filter.length = 10;
            model = await _employeeInformationReportService.GetPersonalDetailsDetailedReport(model);
            model.filter = new Filter();
            Global.GlobalFilter = new Filter();
            await fillfilterDropdown();
            var reporttype = await _dropdownService.GetPersonalDetailReportTypeDropdown();
            ViewBag.ReportType = new SelectList(reporttype, "Code", "Name");
            //Skill is selected by default
            var typelist = await _dropdownService.GetSkillDropdown();
            ViewBag.PersonalDetailsType = new SelectList(await _dropdownService.GetSkillDropdown(), "Code", "Name");
            model.ReportType = new MasterBase();
            model.ReportType.Code = 1;
            // int?[] array= new int?[typelist.Count];
            //int j = 0;
            // for(int i=0;i<typelist.Count;i++)
            // {
            //     array[j] = typelist[i].Code;
            //     j++;
            // }
            //  model.PersonalDetailsTypes = array;   

            ViewBag.Reportname = "Skill";
            await fillfilterDropdown();
            return View("~/Areas/DataBank/Reports/Views/PersonalDetailsReport/Index.cshtml", model);
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

        [HttpGet]
        public async Task<IActionResult> GetPersonalDetailsTypeDropdown(int Type)
        {
            PersonalDetailsDropDown dropmodel = new PersonalDetailsDropDown();
            dropmodel = await _personalDetailsService.GetPersonalDetailsDropDown();
            dynamic data=null;
            if(Type==1)//Skill
                data =await _dropdownService.GetSkillDropdown();
            if(Type == 2) //Qualification
                 data = await _dropdownService.GetAllQualificationDropDown();
            if (Type == 3)//Family
                data = await _dropdownService.GetFamilyRelationDropdown();
            if (Type == 5)
                data = dropmodel.Languages;
            if (Type == 6)
                data = dropmodel.CertificateType;
            if (Type == 7)
                data = await _dropdownService.GetCheckUpTypeDropDown();
            return Json(data);
        }


        public IActionResult GetReportHeading(int TypeCode, string ReportType_Name)
        {
            ViewBag.Reportname = ReportType_Name;
            if (TypeCode == 3)
            {
                
                return PartialView("~/Areas/DataBank/Reports/Views/PersonalDetailsReport/_Family.cshtml");
            }
            else if (TypeCode == 5)
            {
                return PartialView("~/Areas/DataBank/Reports/Views/PersonalDetailsReport/_Language.cshtml");

            }
            else if (TypeCode == 2)
            {
                return PartialView("~/Areas/DataBank/Reports/Views/PersonalDetailsReport/_Qualification.cshtml");

            }
            else if (TypeCode == 6)
            {
                return PartialView("~/Areas/DataBank/Reports/Views/PersonalDetailsReport/_Certification.cshtml");

            }
            else if (TypeCode == 7)
            {
                return PartialView("~/Areas/DataBank/Reports/Views/PersonalDetailsReport/_AnnualHealth.cshtml");

            }
            else if (TypeCode == 8)
            {
                return PartialView("~/Areas/DataBank/Reports/Views/PersonalDetailsReport/_PreviousEmployer.cshtml");

            }
            else
            {
                return PartialView("~/Areas/DataBank/Reports/Views/PersonalDetailsReport/_Report.cshtml");
            }
        }



        [HttpPost]
        public async Task<IActionResult> GetReportList(Filter filter1,int type,string PersonalDetailsTypes)
       {
            try
            {
                EmployeeInformationReport model = new EmployeeInformationReport();
                EmployeeInformationReport m = new EmployeeInformationReport();
                Filter filter = new Filter();

                model.ReportType = new MasterBase();
                model.ReportType.Code = type;
              

                model.filter = filter1;

                if (PersonalDetailsTypes == null)
                {
                    PersonalDetailsDropDown dropmodel = new PersonalDetailsDropDown();
                    dropmodel = await _personalDetailsService.GetPersonalDetailsDropDown();
                    dynamic data = null;
                    if (type == 1)//Skill
                        data = await _dropdownService.GetSkillDropdown();
                    if (type == 2) //Qualification
                        data = await _dropdownService.GetAllQualificationDropDown();
                    if (type == 3)//Family
                        data = await _dropdownService.GetFamilyRelationDropdown();
                    if (type == 5) //languages
                        data = dropmodel.Languages;
                    if (type == 6)
                        data = dropmodel.CertificateType;
                    if (type == 7)
                        data = dropmodel.MedicalCheckup;

                    if (type != 4|| type != 8)
                    {
                        int?[] array = new int?[data.Count];
                        int j = 0;
                        for (int i = 0; i < data.Count; i++)
                        {
                            array[j] = data[i].Code;
                            j++;
                        }
                        model.PersonalDetailsTypes = array;
                    }
                }
                if (PersonalDetailsTypes != null)
                {
                    string[] values = PersonalDetailsTypes.Split(',');
                    for (int i = 0; i < values.Length; i++)
                    {
                        values[i] = values[i].Trim();
                    }

                    int[] intarray = Array.ConvertAll(values, int.Parse);


                    int?[] array = new int?[intarray.Length];
                    int j = 0;
                    for (int i = 0; i < intarray.Length; i++)
                    {
                        array[j] = intarray[i];
                        j++;
                    }
                    model.PersonalDetailsTypes = array;
                }



                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }
  
                //setting globals
                Global._exportdata = model;
                Global.GlobalFilter = filter1;


                model = await _employeeInformationReportService.GetPersonalDetailsDetailedReport(model);
                m.PersonalDetailsDetailedReport = model.PersonalDetailsDetailedReport;
          
                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.PersonalDetailsDetailedReport == null || model.PersonalDetailsDetailedReport.Count() == 0)
                {


                    return Json(new
                    {
                        data = "",
                        draw = filter1.draw,
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows
                    });
                }




                if (model.PersonalDetailsDetailedReport.Count > 0)
                {
                    totalrows = m.PersonalDetailsDetailedReport[0].active;

                }


                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = m.PersonalDetailsDetailedReport[0].totalRecords;
                    return Json(new
                    {
                        data = model.PersonalDetailsDetailedReport,
                        draw = filter1.draw,
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows
                    });
                }
                else
                {
                    return Json(new
                    {
                        data = model.PersonalDetailsDetailedReport,
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
        public async Task<IActionResult> ExportToExcel(int tableLength, string ReportType)
        {
            try
            {
                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Personal Details Report", "Excel");

                if (Global.GlobalFilter == null)
                    Global.GlobalFilter = new Filter();
                else
                {
                    Global.GlobalFilter.length = tableLength;
                    Global.GlobalFilter.start = 0;
                }
                string FilterValues = await _helpers.GetFilterValues(Global.GlobalFilter);
                if (FilterValues == "")
                {
                    FilterValues = "All";
                }
                //Get Dataset
                var dataset = await _employeeInformationReportService.GetPersonalDetailsDetailedReport_Excel(Global._exportdata);
                DataTable dt = dataset.Tables[0];
                var colcount = dt.Columns.Count;
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (dt.Columns[i].ColumnName == "doj")
                        dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.ToUpper();
                    else
                        dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
                }

                //set EXCEL Column Heading 
                for (int i = 0; i < dt.Columns.Count; i++)
                {

                    if (dt.Columns[i].ColumnName == "ReportType")
                        dt.Columns[i].ColumnName = ReportType;
                    dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
                }

              
                string currDateTime = await _helpers.GetCurrentDate();
                
               
                using (var workbook = new XLWorkbook())
                {

                    var worksheet = workbook.Worksheets.Add("Personal Details List ");
                    int count = dt.Rows.Count + 5;

                    IXLRange wsReportNameHeaderRange;
                    if (ReportType == "Skill" || ReportType == "Certification" || ReportType == "Previous Employement")
                    {
                        worksheet.Range("A1:I" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:I" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:I" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:I" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:I" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    }
                    else if (ReportType == "Family")
                    {
                        worksheet.Range("A1:N" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:N" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:N" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:N" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:N" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    }
                    else if (ReportType == "Qualification")
                    {
                        worksheet.Range("A1:K" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:K" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:K" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:K" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:K" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    }
                    else
                    {
                        worksheet.Range("A1:L" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:L" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:L" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:L" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:L" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    }
                   if (ReportType == "Skill" || ReportType == "Certification" || ReportType == "Previous Employement")
                        wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 8)));
                   else  if(ReportType == "Family")
                        wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 13)));

                    else if (ReportType == "Qualification")
                        wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 10)));
                        else
                        wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 11)));                    

                    wsReportNameHeaderRange.Style.Font.Bold = true;
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Merge();
                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                    wsReportNameHeaderRange.Value = "Personal Details Report(" + ReportType + ")";

                    worksheet.Cell(2, 1).Style.Font.Bold = true;
                    worksheet.Cell(2, 1).Value = "Group Company Name";
                    worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();

                    worksheet.Cell(2, 3).Style.Font.Bold = true;
                    worksheet.Cell(2, 3).Value = "Report Type";
                    worksheet.Cell(2, 4).Value = ReportType;



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
                    var filterapplied = worksheet.Range("B4:I4").Merge();

                    filterapplied.Value = FilterValues;
                    var currentRow = 5;

                    worksheet.Cell(currentRow, 1).InsertTable(dt);
                    worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                    worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                    if (ReportType == "Skill" || ReportType == "Certification" || ReportType == "Previous Employement")
                    {
                        worksheet.Range("A5:I5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                        worksheet.Range("A5:I5").Style.Font.Bold = true;
                    }

                    else if (ReportType == "Family")
                    {
                        worksheet.Range("A5:N5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                        worksheet.Range("A5:N5").Style.Font.Bold = true;
                    }
                    else if (ReportType == "Qualification")
                    {
                        worksheet.Range("A5:K5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                        worksheet.Range("A5:K5").Style.Font.Bold = true;
                    }
                    else
                    {
                        worksheet.Range("A5:L5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                        worksheet.Range("A5:L5").Style.Font.Bold = true;
                    }
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
                            "Personal Details Report List.xlsx"
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

