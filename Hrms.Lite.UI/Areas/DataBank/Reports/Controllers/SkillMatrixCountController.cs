
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
    public class SkillMatrixCountController : Controller
    {
        private IDropdownService _dropdownService;
        private int i;
        private readonly ISkillMatrixService _employeeProfileService;
        private readonly IHelpers _helpers;

        private readonly ICommonService _commonService;
        private readonly IBlob _blob;
       

        public SkillMatrixCountController(ISkillMatrixService employeeProfileService, ICommonService commonService, IBlob blob, IDropdownService dropdownService, IHelpers Helpers)
        {
            _employeeProfileService = employeeProfileService;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _blob = blob;
            _helpers = Helpers;
        }

        public async Task<IActionResult> Index(int type = 1)
        {
            try
            {
                
                SkillMatrix model = new SkillMatrix();
                model.Filter = await _commonService.GetFilterDropdowns();



                Filter filterdata = new Filter();
                DataTable dt = new DataTable();
               

                var ds = await _employeeProfileService.GetSkillMatrixEmployeeCount(filterdata, type, "Header");
                dt = ds.Tables[0];
              

                ModifyColumnHeaders(dt);
              


                var tupleModel = new Tuple<SkillMatrix, System.Data.DataTable>(model, dt);

                return View("~/Areas/DataBank/Reports/Views/SkillMatrixCount/Index.cshtml", tupleModel);
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


        DefaultContractResolver contractResolver = new DefaultContractResolver
        {
            NamingStrategy = new CamelCaseNamingStrategy
            {
                OverrideSpecifiedNames = true
            }
        };
        public async Task<IActionResult> GetList([DataSourceRequest] DataSourceRequest request, Filter filter1, int type)
        {

            var ds = await _employeeProfileService.GetSkillMatrixEmployeeCount(filter1, type, "DATA");
            DataTable dt = new DataTable();
           
            dt = ds.Tables[0];

            ModifyColumnHeadersData(dt);

            return Json(dt.ToDataSourceResult(request), new JsonSerializerSettings { ContractResolver = new DefaultContractResolver() });



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
                //if (column.ColumnName == "employeeID" || column.ColumnName == "fullName" || column.ColumnName == "yos" || column.ColumnName == "doj" || column.ColumnName == "designation" || column.ColumnName == "location" || column.ColumnName == "department" || column.ColumnName == "overallRating" || column.ColumnName == "skillLevel")
                //{
                    modifiedColumnName = ModifyColumnName(columnName);

                //}
                //else
                //{
                //    modifiedColumnName = columnName.ToUpper();
                //}
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
       

    }
}
