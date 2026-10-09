using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.Master;
using Hrms.Lite.Shared.Master;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.Master.Controllers
{
    [Area("Master")]
    [SessionAuthorize]
    public class OfferLetterReportController : Controller
    {
        private readonly IOfferLetterReportService _Service;

        public OfferLetterReportController(IOfferLetterReportService Service)
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
        public async Task<IActionResult> GetOfferReportPDF(int PreEnrollCode,int Type,int EmpCode)
        {
            KendoReport model = new KendoReport();
            model.EmpCode = EmpCode;
            model.PreEnrollCode = PreEnrollCode;
            model.Url = await _Service.GetUrl();
            Global.url = model.Url.Urllink;
            if(Type==1|| Type == 2|| Type == 3|| Type == 4)
            {
                model.type = "AppointmentLetter";
            }
           else 
            {
                model.type = "Service Invitation Letter";
            }
           
     
            model.Url= await _Service.GetOfferLetter(model.type);
            model.Url.Urllink = Global.url;
            if (Type == 3 )
            {
                return View("AppointmentLetter", model);
            }
            else if (Type == 5)
            {
                return View("ServiceInvitationLetter", model);
            }
            else
            return View("Index",model);
        }
       





        //[HttpPost]
        //public IActionResult GenerateReportPDF(string reportName)
        //{
        //    var reportProcessor = new Telerik.Reporting.Processing.ReportProcessor();


        //    var deviceInfo = new System.Collections.Hashtable();

        //    var reportSource = new Telerik.Reporting.UriReportSource();

        //    var reportingFileName = System.IO.Path.Combine(_environment.ContentRootPath + "/Reports/", reportName);

        //    reportSource.Uri = reportingFileName;

        //    object parameterValue = "72";
        //    reportSource.Parameters.Add("PreEnrollCode", parameterValue);

        //    Telerik.Reporting.Processing.RenderingResult result = reportProcessor.RenderReport("PDF", reportSource, deviceInfo);

        //    if (!result.HasErrors)
        //    {
        //        string fileName = result.DocumentName + "." + result.Extension;
        //        string path = System.IO.Path.GetTempPath();

        //        string filePath = System.IO.Path.Combine(_environment.ContentRootPath + "/PDF/", fileName);

        //        using (System.IO.FileStream fs = new System.IO.FileStream(filePath, System.IO.FileMode.Create))
        //        {
        //            fs.Write(result.DocumentBytes, 0, result.DocumentBytes.Length);
        //        }
        //    }
        //    return View();
        //}


    }
}
