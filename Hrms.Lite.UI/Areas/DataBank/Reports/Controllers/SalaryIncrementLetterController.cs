using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.DataBank.Reports;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.DataBank.Reports.Controllers
{
    [Area("DataBank")]
    [SessionAuthorize]
    public class SalaryIncrementLetterController : Controller
    {
        private readonly ISalaryIncrementLetterService _Service;

        public SalaryIncrementLetterController(ISalaryIncrementLetterService Service)
        {

            _Service = Service;

        }
        public static class Global
        {

            public static string url;



        }
        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> GetLetterDetails(int EmpCode, int TypeCode,int HistoryCode)
        {
            KendoReport model = new KendoReport();
            model.Url = new Url();
            model.Url = await _Service.GetUrl();
            model.EmpCode = EmpCode;
          
            if (TypeCode == 6)
            {
                model.type = "Salary Increment Letter";
            }
            if(TypeCode==7)
            {
                model.type = "Increment Promotion Letter";
            }
            if (TypeCode == 8)
            {
                model.type = "IncrementSalaryDesignationLetter";
            }

          
            Global.url = model.Url.Urllink;
            model.Url.Urllink = Global.url;
            model.Url = await _Service.GetOfferLetter(model.type);
            model.Url.Urllink = Global.url;
            model.HistoryCode = HistoryCode;
            if (TypeCode == 7)
            {
                return PartialView("~/Areas/DataBank/Reports/Views/SalaryIncrementLetter/_IncrementPromotionLetter.cshtml", model);

            }
            else if(TypeCode==8)
            {
                return PartialView("~/Areas/DataBank/Reports/Views/SalaryIncrementLetter/_IncrementDesAndSalaryLetter.cshtml", model);
                
            }
            else
            return View("~/Areas/DataBank/Reports/Views/SalaryIncrementLetter/Index.cshtml", model);

        }

    }
}
