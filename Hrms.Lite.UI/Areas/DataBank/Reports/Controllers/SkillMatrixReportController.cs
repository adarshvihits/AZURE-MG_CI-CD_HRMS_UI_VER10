
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.DataBank.Reports;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.DataBank.Reports;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

using System.Threading.Tasks;

using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Data;

using Filter = Hrms.Lite.Shared.General.Filter;

using DataTable = System.Data.DataTable;


namespace Hrms.Lite.UI.Areas.DataBank.Reports.Controllers
{
    [Area("DataBank")]
    [SessionAuthorize]
    public class SkillMatrixReportController : Controller
    {
        private IDropdownService _dropdownService;
        private int i;
        private readonly ISkillMatrixService _employeeProfileService;
        private readonly IHelpers _helpers;

        private readonly ICommonService _commonService;
        private readonly IBlob _blob;
        public static class Global
        {
            public static Filter GlobalFilter;
            public static int ActiveCount;
            public static int ResignedCount;
        }

        public SkillMatrixReportController(ISkillMatrixService employeeProfileService, ICommonService commonService, IBlob blob, IDropdownService dropdownService, IHelpers Helpers)
        {
            _employeeProfileService = employeeProfileService;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _blob = blob;
            _helpers = Helpers;
        }
       

        //[HttpPost]
        public async Task<IActionResult> Index(int type=1)
        {
            try
            {
                Dictionary<string, string> tooltips = new Dictionary<string, string>();
                SkillMatrix model = new SkillMatrix();
                await fillfilterDropdown();
                var datas = await _employeeProfileService.GetSkillNames();


                if (datas != null && datas.Tables.Count > 0)
                {
                    DataTable dataTable = datas.Tables[0]; // Assuming the desired data is in the first table

                    foreach (DataRow row in dataTable.Rows)
                    {
                        string columnName = row["shortName"].ToString();
                        string tooltip = row["name"].ToString();

                        tooltips[columnName] = tooltip;
                    }
                }


                // Dictionary<string, string> tooltips = GetTooltips();

                // Pass the tooltips to the view
                ViewBag.Tooltips = tooltips;
                ViewBag.TooltipsJson = Newtonsoft.Json.JsonConvert.SerializeObject(ViewBag.Tooltips);
                Filter filterdata = new Filter();
                DataTable dt = new DataTable();
                DataTable dt1 = new DataTable();
                DataTable dt2 = new DataTable();

                var ds = await _employeeProfileService.GetSkillMatrixHeader(filterdata,type,"0");
                dt = ds.Tables[0];
                dt1 = ds.Tables[1];
                dt2 = ds.Tables[2];

                ModifyColumnHeaders(dt);
                ModifyColumnHeaderstoCaps(dt1);
                ModifyColumnHeaderstoCaps(dt2);
              

                var tupleModel = new Tuple<SkillMatrix, System.Data.DataTable,System.Data.DataTable, System.Data.DataTable>(model, dt,dt1,dt2);

                return View("~/Areas/DataBank/Reports/Views/SkillMatrixReport/Index.cshtml", tupleModel);
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

        public async Task<IActionResult> ReloadDiv(string skills,int type = 1)
        {
            try
            {
                Dictionary<string, string> tooltips = new Dictionary<string, string>();
                SkillMatrix model = new SkillMatrix();
                await fillfilterDropdown();
                var datas = await _employeeProfileService.GetSkillNames();


                if (datas != null && datas.Tables.Count > 0)
                {
                    DataTable dataTable = datas.Tables[0]; // Assuming the desired data is in the first table

                    foreach (DataRow row in dataTable.Rows)
                    {
                        string columnName = row["shortName"].ToString();
                        string tooltip = row["name"].ToString();

                        tooltips[columnName] = tooltip;
                    }
                }
                ViewBag.Tooltips = tooltips;
                ViewBag.TooltipsJson = Newtonsoft.Json.JsonConvert.SerializeObject(ViewBag.Tooltips);
                Filter filterdata = new Filter();
                DataTable dt = new DataTable();
                DataTable dt1 = new DataTable();
                DataTable dt2 = new DataTable();

                var ds = await _employeeProfileService.GetSkillMatrixHeader(filterdata, type,skills);
                dt = ds.Tables[0];
                dt1 = ds.Tables[1];
                dt2 = ds.Tables[2];

                ModifyColumnHeaders(dt);
                ModifyColumnHeaderstoCaps(dt1);
                ModifyColumnHeaderstoCaps(dt2);

                var tupleModel = new Tuple<SkillMatrix, System.Data.DataTable, System.Data.DataTable, System.Data.DataTable>(model, dt, dt1, dt2);



                return PartialView("~/Areas/DataBank/Reports/Views/SkillMatrixReport/_List.cshtml",tupleModel);
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

        void ModifyColumnHeaders(DataTable dataTable)
        {
            foreach (DataColumn column in dataTable.Columns)
            {
                string columnName = column.ColumnName;
                string modifiedColumnName = ModifyColumnName(columnName);
                column.ColumnName = modifiedColumnName;
            }
        }

        void ModifyColumnHeaderstoCaps(DataTable dataTable)
        {
            foreach (DataColumn column in dataTable.Columns)
            {
                string columnName = column.ColumnName;
                string modifiedColumnName = columnName.ToUpper();
                column.ColumnName = modifiedColumnName;
            }
        }

        void ModifyColumnHeadersData(DataTable dataTable)
        {
            foreach (DataColumn column in dataTable.Columns)
            {
                string columnName = column.ColumnName;
                string modifiedColumnName = "";
                if (column.ColumnName == "employeeID" || column.ColumnName == "fullName" || column.ColumnName == "yos" || column.ColumnName == "doj" || column.ColumnName == "designation" || column.ColumnName == "location" || column.ColumnName == "department" || column.ColumnName == "overallRating" || column.ColumnName == "skillLevel")
                {
                    modifiedColumnName = ModifyColumnName(columnName);
                  
                }
                else
                {
                    modifiedColumnName = columnName.ToUpper();
                }
                column.ColumnName = modifiedColumnName;
            }
        }

        string ModifyColumnName(string columnName)
        {
            string modifiedColumnName = columnName;

            if (!string.IsNullOrEmpty(columnName))
            {
                char firstLetter = columnName[0];
                char modifiedFirstLetter = char.ToUpper(firstLetter);
                modifiedColumnName = modifiedFirstLetter + columnName.Substring(1);
            }

            return modifiedColumnName;
        }



        DefaultContractResolver contractResolver = new DefaultContractResolver
        {
            NamingStrategy = new CamelCaseNamingStrategy
            {
                OverrideSpecifiedNames = true
            }
        };
        public async Task<IActionResult> GetList([DataSourceRequest] DataSourceRequest request, Filter filter1, int type,string skills)
        {

            var ds = await _employeeProfileService.GetSkillMatrixReport(filter1,type,skills);
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            dt = ds.Tables[0];

            //dt1 = ds.Tables[1];
            //dt2 = ds.Tables[2];

            ModifyColumnHeadersData(dt);
            //ModifyColumnHeaders(dt1);
            //ModifyColumnHeaders(dt2);



            return Json(ds.Tables[0].ToDataSourceResult(request), new JsonSerializerSettings { ContractResolver = new DefaultContractResolver() });



        }
        public ActionResult ChildGrid()
        {
            return PartialView("~/Areas/DataBank/Reports/Views/SkillMatrixReport/_Skills.cshtml");
        }

        public async Task<IActionResult> GetSkills([DataSourceRequest] DataSourceRequest request)
        {
            SkillMatrix model = new SkillMatrix();
            List<SkillsList> modellist = new List<SkillsList>();

            model = await _employeeProfileService.GetSkills();

            modellist = model.SkillsList;

          


            var dataResult1 = modellist.ToDataSourceResult(request);
            ;
            //   var dataResult = model.EmployeeProfileList.ToDataSourceResult(request);

            // dataResult.Total = model.ActiveCount;
           




            return Json(dataResult1, new JsonSerializerSettings { ContractResolver = new DefaultContractResolver() });
        }

            // ...



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

        [HttpPost]
        public ActionResult Excel_Export_Save(string contentType, string base64, string fileName)
        {
            var fileContents = Convert.FromBase64String(base64);

            return File(fileContents, contentType, fileName);
        }
    }
}
