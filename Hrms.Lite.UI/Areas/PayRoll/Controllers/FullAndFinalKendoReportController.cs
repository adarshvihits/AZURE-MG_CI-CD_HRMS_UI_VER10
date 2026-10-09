using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.DataBank.Reports;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.PayRoll;
using Hrms.Lite.Shared.Account;
using Hrms.Lite.Shared.PayRoll;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.Master.Controllers
{
    [Area("PayRoll")]
    [SessionAuthorize]
    public class FullAndFinalKendoReportController : Controller
    {
        private readonly IFullAndFinalKendoReportService _Service;
       
        public FullAndFinalKendoReportController(IFullAndFinalKendoReportService Service)
        {
            _Service  = Service;
        }
        public static class Global
        {
            
            public static string urlLink;
        

        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetFullAndFinalReportPdf(int EmployeeCode,int UserCode,int CompanyCode,string EmployeeName,string ID)
         
        {
            FullandFinalSettelment model = new FullandFinalSettelment();
            Login CurrentUser = JsonConvert.DeserializeObject<Hrms.Lite.Shared.Account.Login>(HttpContext.Session.GetString("User"));
          
          
            model.Details = new FullandFinalSettelment_Details();
            model.Details.EmployeeCode = EmployeeCode;
            model.Details.UserCode = UserCode;
            model.Details.CompanyCode = CompanyCode;
            model.Url = await _Service.GetUrl();
            Global.urlLink = model.Url.Urllink;
            model.Url.Urllink = Global.urlLink;
            model.Url.EmployeeName = EmployeeName;
            model.Url.ID = ID;
            if (CurrentUser.CompanyCode.ToUpper() == "CAFS")
            {
                model.Url.Payslip = "FullAndFinalSettlementCAFS.trdp";
            }
            else
            {
                model.Url.Payslip = "FullAndFinalSettlement.trdp";
            }
            return View("Index",model);
        }

    }
}
