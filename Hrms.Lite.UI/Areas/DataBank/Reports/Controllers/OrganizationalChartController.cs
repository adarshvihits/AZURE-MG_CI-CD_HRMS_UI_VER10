using ClosedXML.Excel;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using DocumentFormat.OpenXml.EMMA;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.DataBank.Reports;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.Services.DataBank.Reports;
using Hrms.Lite.Services.Services.General;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.DataBank.Reports;
using Hrms.Lite.Shared.General;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Spire.Xls;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using System.Web;

namespace Hrms.Lite.UI.Areas.DataBank.Reports.Controllers
{
    [Area("DataBank")]
    [SessionAuthorize]
    public class OrganizationalChartController : Controller
    {
        private readonly IOrganizationalChartService _organizationalChartService;
        private readonly IDropdownService _dropdownService;

        public OrganizationalChartController(IOrganizationalChartService organizationalChartService, IDropdownService dropdownService)
        {
            _organizationalChartService = organizationalChartService;
            _dropdownService = dropdownService;

        }

        public async Task<IActionResult> Index()
        {
            try { 
            OrganizationalChart model = new OrganizationalChart();
            ViewBag.Empl = new SelectList(await _dropdownService.GetEmployeeList(), "Code", "Name");
            model = await _organizationalChartService.GetOrgChart(0);
            return View("~/Areas/DataBank/Reports/Views/OrganizationChart/Index.cshtml", model);
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
        public async Task<IActionResult> GetOrgChart([DataSourceRequest] DataSourceRequest request,int empCode)
        {
            OrganizationalChart model = new OrganizationalChart();

            List<OrganizationalChart_List> modellist = new List<OrganizationalChart_List>();

            model = await _organizationalChartService.GetOrgChart(empCode);



            modellist = model.OrganizationalChartList;

            var dataResult = modellist.ToDataSourceResult(request);


            return Json(new { Data = dataResult.Data }, new JsonSerializerSettings { ContractResolver = new DefaultContractResolver() });
        }

        [HttpPost]
        public IActionResult Export_Save(string contentType, string base64, string fileName)
        {
            var fileContents = Convert.FromBase64String(base64);

            return File(fileContents, contentType, fileName);
        }

    }
}
