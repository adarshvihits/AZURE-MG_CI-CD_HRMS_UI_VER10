using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.Account;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.DataBank.Controllers
{
    [Area("DataBank")]
    [SessionAuthorize]
    public class CompanyLettersDetailsController : Controller
    {
        private readonly ICommonService _commonService;
        private readonly ICompanyLettersDetailsService _service;
        public CompanyLettersDetailsController(ICommonService CommonService, ICompanyLettersDetailsService service)
        {

            _commonService = CommonService;
            _service = service;

        }
        public static class Global
        {

            public static string url;



        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> GetFormatCode(int TypeCode)
        {
            var FormatCode = _commonService.GetLetterFormatCode(TypeCode);
            return Json(FormatCode.Result.Data);
        }


     

        public async Task<IActionResult> GetAppointmentLetterPDF(int PreEnrollCode, int LetterFormatCode, int EmpCode, int GcCode, bool Flag)
        {
            KendoReport model = new KendoReport();
            Login CurrentUser = JsonConvert.DeserializeObject<Hrms.Lite.Shared.Account.Login>(HttpContext.Session.GetString("User"));
            model.UserCode = CurrentUser.authUser.UserCode;
            model.EmpCode = EmpCode;
            model.PreEnrollCode = PreEnrollCode;
            model.Url = await _commonService.GetUrl();
            Global.url = model.Url.Urllink;

            model.Url = await _commonService.GetLettersFileName(LetterFormatCode);
            model.Url.Urllink = Global.url;
            model.TypeCode = LetterFormatCode;
            model.GcCode = GcCode;
            if (LetterFormatCode == 191)
            {
                model.flag = Flag;
            }

            if(LetterFormatCode==224 && GcCode==3)
            {
                model.Url.offerLetter = "GeojithAppointmentLetterGFSL.trdp";
            }

            if(LetterFormatCode==224 && GcCode==6)
            {
                model.Url.offerLetter = "GeojithAppointmentLetterGTPL.trdp";
            }

            if (LetterFormatCode == 224 && GcCode == 14)
            {
                model.Url.offerLetter = "GeojithAppointmentLetterFintech.trdp";
            }

            if (LetterFormatCode == 224 && GcCode == 2)
            {
                model.Url.offerLetter = "GeojithAppointmentLetterGeojitCredits.trdp";
            }

            if (LetterFormatCode == 224 && GcCode == 11)
            {
                model.Url.offerLetter = "GeojithAppointmentLetterGIFT.trdp";
            }

            if (LetterFormatCode == 224 && GcCode == 13)
            {
                model.Url.offerLetter = "GeojithAppointmentLetterGIL.trdp";
            }
            return View("AppointmentLetters", model);


        }


        public async Task<IActionResult> GetOfferLetterPDF(int PreEnrollCode, Guid EmpGI, int EmpCode, int LetterFormatCode)
        {
            KendoReport model = new KendoReport();

            model.PreEnrollCode = PreEnrollCode;
            model.EmployeeGI = EmpGI;
            model.EmpCode = EmpCode;
            model.Url = await _commonService.GetUrl();
            Global.url = model.Url.Urllink;


            model.Url = await _commonService.GetLettersFileName(LetterFormatCode);
            model.Url.Urllink = Global.url;
            model.DocumentTypeCode = LetterFormatCode;
            return View("OfferLetter", model);
        }

        public async Task<IActionResult> GetNoDueLetter(int EmpCode, int TypeCode, int LetterFormatCode, int GcCode)
        {
            KendoReport model = new KendoReport();
            model.Url = new Url();
            model.Url = await _commonService.GetUrl();
            Global.url = model.Url.Urllink;
            model.EmpCode = EmpCode;
            //model.TypeCode = TypeCode;


            model.Url = await _commonService.GetLettersFileName(LetterFormatCode);
            model.Url.Urllink = Global.url;
            model.TypeCode = TypeCode;
            model.FormatCode = LetterFormatCode;
            model.GcCode = GcCode;
            return View("NoDueLetter", model);


        }



        public async Task<IActionResult> GetExperienceLetter(int EmpCode, int TypeCode,int GcCode, int LetterFormatCode, string From, string To,int GroupCompanyCode)
        {
            KendoReport model = new KendoReport();
            var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
            model.Url = new Url();
            model.Url = await _commonService.GetUrl();
            Global.url = model.Url.Urllink;
            model.EmpCode = EmpCode;
            model.TypeCode = TypeCode;
            model.GcCode = GcCode;

           // model.Url = await _commonService.GetLettersFileName(LetterFormatCode);
            model.Url.Urllink = Global.url;
            model.TypeCode = TypeCode;
            model.FormatCode = LetterFormatCode;

            model.From = From;
            model.To = To;
            model.UserCode = User.authUser.UserCode;
            model.GroupcompanyCode = GroupCompanyCode;
            if (LetterFormatCode == 234 && model.GroupcompanyCode == 3)
            {
                model.Url.offerLetter = "ExperienceCumRelievingLetterHeadOffice.trdp";
            }
            if (LetterFormatCode == 233 && model.GroupcompanyCode == 3)
            {
                model.Url.offerLetter = "ExperienceCumRelievingLetterBranches.trdp";
            }
            if (LetterFormatCode == 235 && (model.GroupcompanyCode == 6 || model.GroupcompanyCode == 5 || model.GroupcompanyCode == 14))
            {
                model.Url.offerLetter = "ExperienceCumRelievingLetterHeadOfficeGTLWTCTELO.trdp";
            }

            return View("ExperienceLetters", model);
          

        }



        public async Task<IActionResult> GetIncrementLetter(int EmpCode, int TypeCode, int HistoryCode, int GcCode, int LetterFormatCode, string From, string To)
        {
            KendoReport model = new KendoReport();
            model.Url = new Url();
            model.Url = await _commonService.GetUrl();
            Global.url = model.Url.Urllink;
            model.EmpCode = EmpCode;
            model.TypeCode = TypeCode;
            model.GcCode = GcCode;

            model.Url = await _commonService.GetLettersFileName(LetterFormatCode);
            model.Url.Urllink = Global.url;
            model.TypeCode = TypeCode;
            model.FormatCode = LetterFormatCode;
            model.HistoryCode = HistoryCode;
            model.From = From;
            model.To = To;
            return View("IncrementLetters", model);


        }


        public async Task<IActionResult> GetPromotionLetter(int EmpCode, int TypeCode, int HistoryCode, int LetterFormatCode, string PromotionType)
        {
            KendoReport model = new KendoReport();
            model.Url = new Url();
            model.Url = await _commonService.GetUrl();
            Global.url = model.Url.Urllink;

            model.Url = await _commonService.GetLettersFileName(LetterFormatCode);
            model.Url.Urllink = Global.url;
            model.TypeCode = TypeCode;
            model.FormatCode = LetterFormatCode;
            model.EmpCode = EmpCode;
            model.HistoryCode = HistoryCode;
            model.PromotionType = PromotionType;
            return View("PromotionLetter", model);


        }



        public async Task<IActionResult> GetProbationExtensionLetter(int EmpCode, int HisCode, int LetterFormatCode,int GcCode)
        {
            KendoReport model = new KendoReport();
            model.Url = new Url();
            model.Url = await _commonService.GetUrl();
            Global.url = model.Url.Urllink;

            model.Url = await _commonService.GetLettersFileName(LetterFormatCode);
            model.Url.Urllink = Global.url;
            //model.TypeCode = TypeCode;
            model.FormatCode = LetterFormatCode;
            model.EmpCode = EmpCode;
            model.HistoryCode = HisCode;
            model.GcCode = GcCode;
            return View("ProbationExtensionLetter", model);


        }




        public async Task<IActionResult> GetRevisionLetter(int EmpCode, int TypeCode, int HistoryCode, int LetterFormatCode, string From, string To)
        {
            KendoReport model = new KendoReport();
            model.Url = new Url();
            model.Url = await _commonService.GetUrl();
            Global.url = model.Url.Urllink;

            model.Url = await _commonService.GetLettersFileName(LetterFormatCode);
            model.Url.Urllink = Global.url;
            model.TypeCode = TypeCode;
            model.FormatCode = LetterFormatCode;
            model.EmpCode = EmpCode;
            model.HistoryCode = HistoryCode;
            model.From = From;
            model.To = To;
            return View("RevisionLetter", model);


        }

        public async Task<IActionResult> GetRelievingLetter(int EmpCode, int TypeCode, int GcCode, int LetterFormatCode, string From, string To)
        {
            KendoReport model = new KendoReport();
            model.Url = new Url();
            model.Url = await _commonService.GetUrl();
            Global.url = model.Url.Urllink;
            model.EmpCode = EmpCode;
            //model.TypeCode = TypeCode;


            model.Url = await _commonService.GetLettersFileName(LetterFormatCode);
            model.Url.Urllink = Global.url;
            model.TypeCode = TypeCode;
            model.FormatCode = LetterFormatCode;
            model.GcCode = GcCode;
            return View("RelievingLetters", model);


        }

        public async Task<IActionResult> GetTerminationLetter(int EmpCode, int GcCode, int TypeCode, int LetterFormatCode, string offerletterNum, string Date, string offerletterdate)
        {
            KendoReport model = new KendoReport();
            model.Url = new Url();
            model.Url = await _commonService.GetUrl();
            Global.url = model.Url.Urllink;
            model.EmpCode = EmpCode;
            //model.TypeCode = TypeCode;


            model.Url = await _commonService.GetLettersFileName(LetterFormatCode);
            model.Url.Urllink = Global.url;
            model.TypeCode = TypeCode;
            model.FormatCode = LetterFormatCode;
            model.GcCode = GcCode;
            model.offerletterNum = offerletterNum;
            model.offerletterdate = offerletterdate;
            model.Date = Date;
            return View("TerminationLetter", model);


        }





        public async Task<IActionResult> GetWarningLetter(int EmpCode, int LetterFormatCode, int GcCode, int TypeCode, string ActionDate)
        {
            KendoReport model = new KendoReport();
            model.Url = new Url();
            model.Url = await _commonService.GetUrl();
            Global.url = model.Url.Urllink;
            model.EmpCode = EmpCode;
            //model.TypeCode = TypeCode;


            model.Url = await _commonService.GetLettersFileName(LetterFormatCode);
            model.Url.Urllink = Global.url;
            model.TypeCode = TypeCode;
            model.FormatCode = LetterFormatCode;
            model.GcCode = GcCode;
            model.ActionDate = ActionDate;
            return View("WarningLetters", model);


        }



        public async Task<IActionResult> GetShowCauseNotice(int EmpCode, int GcCode, int TypeCode, int LetterFormatCode, string Date, string ActionDate)
        {
            KendoReport model = new KendoReport();
            model.Url = new Url();
            model.Url = await _commonService.GetUrl();
            Global.url = model.Url.Urllink;
            model.EmpCode = EmpCode;
            //model.TypeCode = TypeCode;


            model.Url = await _commonService.GetLettersFileName(LetterFormatCode);
            model.Url.Urllink = Global.url;
            model.TypeCode = TypeCode;
            model.FormatCode = LetterFormatCode;
            model.GcCode = GcCode;
            model.Date = Date;
            model.ActionDate = ActionDate;
            return View("ShowCauseNotice", model);


        }






        public async Task<IActionResult> GetTransferLetter(int EmpCode, int TypeCode, int HistoryCode, int LetterFormatCode, int GcCode)
        {
            KendoReport model = new KendoReport();
            model.EmpCode = EmpCode;
            model.HistoryCode = HistoryCode;

            model.Url = await _commonService.GetUrl();
            Global.url = model.Url.Urllink;

            model.Url = await _commonService.GetLettersFileName(LetterFormatCode);
            model.Url.Urllink = Global.url;
            model.HistoryCode = HistoryCode;
            model.GcCode = GcCode;
            model.TypeCode = TypeCode;
            model.FormatCode = LetterFormatCode;
            return View("TransferLetters", model);
        }

        public async Task<IActionResult> GetSalaryLetterReport(int EmpCode, Guid GroupCompanyGI, string Reason, int LetterFormatCode, int DocTypeCode)

        {
            KendoReport model = new KendoReport();
            model.Url = new Url();
            model.Url = await _commonService.GetUrl();
            Global.url = model.Url.Urllink;
            model.EmpCode = EmpCode;



            model.Url = await _commonService.GetLettersFileName(LetterFormatCode);
            model.Url.Urllink = Global.url;
            model.TypeCode = DocTypeCode;
            model.FormatCode = LetterFormatCode;
            model.GroupCompanyGI = GroupCompanyGI;
            model.Reason = Reason;
            return View("SalaryCertificates", model);







        }

        public async Task<IActionResult> GetEmploymentCertificate(int EmpCode, int TypeCode, int GcCode, int LetterFormatCode, string From, string To)
        {
            KendoReport model = new KendoReport();
            model.Url = new Url();
            model.Url = await _commonService.GetUrl();
            Global.url = model.Url.Urllink;
            model.EmpCode = EmpCode;
            model.TypeCode = TypeCode;
            model.GcCode = GcCode;

            model.Url = await _commonService.GetLettersFileName(LetterFormatCode);
            model.Url.Urllink = Global.url;
            model.TypeCode = TypeCode;
            model.FormatCode = LetterFormatCode;

            model.From = From;
            model.To = To;
            return View("EmploymentCertificate", model);
        }




        public async Task<IActionResult> GetConfirmationLetter(int EmpCode, int TypeCode, int HistoryCode, int LetterFormatCode, int GcCode)
        {
            KendoReport model = new KendoReport();
            model.EmpCode = EmpCode;
            model.HistoryCode = HistoryCode;

            model.Url = await _commonService.GetUrl();
            Global.url = model.Url.Urllink;

            model.Url = await _commonService.GetLettersFileName(LetterFormatCode);
            model.Url.Urllink = Global.url;
            model.HistoryCode = HistoryCode;
            model.GcCode = GcCode;
            model.TypeCode = TypeCode;
            model.FormatCode = LetterFormatCode;
            return View("ConfirmationLetters", model);
        }



        public async Task<IActionResult> GetDeploymentLetter(int EmpCode, int TypeCode, int HistoryCode, int LetterFormatCode, int GcCode)
        {
            KendoReport model = new KendoReport();
            model.EmpCode = EmpCode;
            model.HistoryCode = HistoryCode;

            model.Url = await _commonService.GetUrl();
            Global.url = model.Url.Urllink;

            model.Url = await _commonService.GetLettersFileName(LetterFormatCode);
            model.Url.Urllink = Global.url;
            model.HistoryCode = HistoryCode;
            model.GcCode = GcCode;
            model.TypeCode = TypeCode;
            model.FormatCode = LetterFormatCode;
            return View("DeputationLetter", model);
        }

        public async Task<IActionResult> GetStillWorkingLetter(int EmpCode, int TypeCode, int GcCode, int LetterFormatCode, string Reason, string From, string To)
        {
            KendoReport model = new KendoReport();
            model.Url = new Url();
            model.Url = await _commonService.GetUrl();
            Global.url = model.Url.Urllink;
            model.EmpCode = EmpCode;
            model.TypeCode = TypeCode;
            model.GcCode = GcCode;

            model.Url = await _commonService.GetLettersFileName(LetterFormatCode);
            model.Url.Urllink = Global.url;
            model.TypeCode = TypeCode;
            model.FormatCode = LetterFormatCode;
            model.Reason = Reason;
            model.From = From;
            model.To = To;
            return View("StillWorkingLetter", model);
        }

        public async Task<IActionResult> GetAbscondingLetter(int EmpCode, int TypeCode, int GcCode, int LetterFormatCode, string Date, string Remarks)
        {
            KendoReport model = new KendoReport();
            model.Url = new Url();
            model.Url = await _commonService.GetUrl();
            Global.url = model.Url.Urllink;
            model.EmpCode = EmpCode;
            model.TypeCode = TypeCode;
            model.GcCode = GcCode;

            model.Url = await _commonService.GetLettersFileName(LetterFormatCode);
            model.Url.Urllink = Global.url;
            model.TypeCode = TypeCode;
            model.FormatCode = LetterFormatCode;
            model.Date = Date;
            model.Remarks = Remarks;

            return View("AbscondingLetter", model);
        }

        public async Task<IActionResult> SignPDF(int UserCode, int empCode, int LetterFormatCode, int GcCode, bool Flag)
        {
            var response = await _service.GeneratePDF(UserCode, empCode, LetterFormatCode, GcCode, Flag);
            return Json(response);
        }


    }
}
