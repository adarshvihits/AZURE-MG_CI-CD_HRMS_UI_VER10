using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.DataBank.Reports;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.DataBank.Reports;
using Hrms.Lite.Shared.General;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hrms.Lite.UI.Areas.DataBank.Reports.Controllers
{
    [Area("DataBank")]
    [SessionAuthorize]

    public class EmployeeCompanyDocumentsController : Controller
    {
        private readonly IEmployeeCompanyDocumentsService _EmployeeCompanyDocumentsService;
        private readonly ICommonService _commonService;
        private readonly IDropdownService _dropdownService;
        private readonly IHelpers _helpers;
        public static class Global
        {
            public static Filter GlobalFilter;
            public static int DocTypeCode;
            public static string FromDate;
            public static string Todate;
            public static bool IsRefreshed = true;
         
        }
        public EmployeeCompanyDocumentsController(IEmployeeCompanyDocumentsService EmployeeCompanyDocumentsService, ICommonService commonService, IDropdownService dropdownService, IHelpers Helpers)
        {
            _EmployeeCompanyDocumentsService = EmployeeCompanyDocumentsService;
            _commonService = commonService;
            _dropdownService = dropdownService;
            _helpers = Helpers;
        }

        public async Task <IActionResult> Index()
        {
            try { 
            EmployeeCompanyDocuments model = new EmployeeCompanyDocuments();
            ViewBag.doc = new SelectList(await _dropdownService.GetDocumentTypeDropdown(), "Code", "Name");
            await fillfilterDropdown();
            Filter filter = new Filter();
        
            DateTime now = DateTime.Now;
            var startDate = new DateTime(now.Year, now.Month, 1);
            var endDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1);

            //model = await _EmployeeCompanyDocumentsService.GetEmployeeCompanyList(1, startDate.ToString("yyyy-MMM-dd"), endDate.ToString("yyyy-MMM-dd"), filter);
            Global.FromDate = startDate.ToString("yyyy-MMM-dd");
            Global.Todate = endDate.ToString("yyyy-MMM-dd");
            model.From = startDate;
            model.To = endDate;
            EmployeeCompanyDocuments m = new EmployeeCompanyDocuments();
            model.From = DateTime.Now.AddMonths(-3);
            model.To = DateTime.Now;
            filter.length = 10;
            m = await _EmployeeCompanyDocumentsService.GetEmployeeCompanyList(0, Global.FromDate, Global.Todate, filter);
            return View("~/Areas/DataBank/Reports/Views/EmployeeCompanyDocuments/Index.cshtml", model);
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

  

        public async Task<IActionResult> GetList(Filter filter1,int DocTypeCode)
        {
            try
            {
                EmployeeCompanyDocuments model = new EmployeeCompanyDocuments();
                EmployeeCompanyDocuments m = new EmployeeCompanyDocuments();
                //model.EmployeeHeader = new EmployeeHeader();

                Filter filter = new Filter();
                Global.GlobalFilter = filter1;
                Global.FromDate = filter1.F_From.ToString("yyyy-MMM-dd");
                Global.Todate = filter1.F_To.ToString("yyyy-MMM-dd");

                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }

                model = await _EmployeeCompanyDocumentsService.GetEmployeeCompanyList(DocTypeCode,Global.FromDate, Global.Todate, filter1);                

                m.EmployeeCompanyDocumentsList = model.EmployeeCompanyDocumentsList;
                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.EmployeeCompanyDocumentsList == null || model.EmployeeCompanyDocumentsList.Count() == 0)
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




                if (model.EmployeeCompanyDocumentsList.Count > 0)
                {
                    totalrows = m.EmployeeCompanyDocumentsList[0].active;

                    //m.OnBoardingList.ToList<Hrms.Lite.Shared.Master.Test>().Count; ;
                }

                //model.AllTabCount = m.AllTabCount;
                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = m.EmployeeCompanyDocumentsList[0].totalRecords;
                    return Json(new
                    {
                        data = model.EmployeeCompanyDocumentsList,
                        draw = filter1.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows//totalrowsafterfiltering
                    });
                }
                else
                {
                    // return Json(new { data = model.EmployeeProfileList, draw = filter1.draw, recordsTotal = totalrows, recordsFiltered = totalrows });
                    return Json(new
                    {
                        data = model.EmployeeCompanyDocumentsList,
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


        //[HttpGet]
        //public async Task<ActionResult> GetExperienceletterbracnhesDetails(int EmployeeCode, int LetterFormateCode)
        //{
        //    try
        //    {
        //        EmployeeCompanyDocuments model = new EmployeeCompanyDocuments();


        //        model = await _EmployeeCompanyDocumentsService.GetExperienceletterbracnhesDetails(EmployeeCode, LetterFormateCode);


        //        return PartialView("~/Areas/PayRoll/Reports/Views/LoanRegister/_ExperiencebranchPopUp.cshtml", model);

        //    }
        //    catch (UnauthorizedAccessException e)
        //    {

        //        return RedirectToAction("Login", "Account", new { Area = "" });
        //    }
        //    catch (Exception e)
        //    {
        //        return RedirectToAction("Error", "Account", new { Area = "" });
        //    }
        //}

        public async Task<IActionResult> GetAppointmentLetterList(Filter filter1, int DocTypeCode,int DocumentFormatCode, string Type)
        {
            try
            {
                AppointmentLetter model = new AppointmentLetter();
                AppointmentLetter m = new AppointmentLetter();
                //model.EmployeeHeader = new EmployeeHeader();




                Filter filter = new Filter();
                Global.GlobalFilter = filter1;
                Global.FromDate = filter1.F_From.ToString("yyyy-MMM-dd");
                Global.Todate = filter1.F_To.ToString("yyyy-MMM-dd");

                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }

                model = await _EmployeeCompanyDocumentsService.AppointmentList(DocTypeCode, DocumentFormatCode, Global.FromDate, Global.Todate,Type, filter1);


                m.GetAppointmentLetterList = model.GetAppointmentLetterList;
                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.GetAppointmentLetterList == null || model.GetAppointmentLetterList.Count() == 0)
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




                if (model.GetAppointmentLetterList.Count > 0)
                {
                    totalrows = m.GetAppointmentLetterList[0].active;

                    //m.OnBoardingList.ToList<Hrms.Lite.Shared.Master.Test>().Count; ;
                }

                //model.AllTabCount = m.AllTabCount;
                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = m.GetAppointmentLetterList[0].totalRecords;
                    return Json(new
                    {
                        data = model.GetAppointmentLetterList,
                        draw = filter1.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows//totalrowsafterfiltering
                    });
                }
                else
                {
                    // return Json(new { data = model.EmployeeProfileList, draw = filter1.draw, recordsTotal = totalrows, recordsFiltered = totalrows });
                    return Json(new
                    {
                        data = model.GetAppointmentLetterList,
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

        public async Task<IActionResult> GetProbationExtensionList(Filter filter1, int DocTypeCode,int HisCode, int DocumentFormatCode, string Type)
        {
            try
            {
                AppointmentLetter model = new AppointmentLetter();
                AppointmentLetter m = new AppointmentLetter();
                //model.EmployeeHeader = new EmployeeHeader();




                Filter filter = new Filter();
                Global.GlobalFilter = filter1;
                Global.FromDate = filter1.F_From.ToString("yyyy-MMM-dd");
                Global.Todate = filter1.F_To.ToString("yyyy-MMM-dd");

                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }

                model = await _EmployeeCompanyDocumentsService.GetProbationExtensionLetterList(Global.FromDate, Global.Todate, DocTypeCode,HisCode, DocumentFormatCode,  Type, filter1);


                m.GetProbationExtensionList = model.GetProbationExtensionList;
                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.GetProbationExtensionList == null || model.GetProbationExtensionList.Count() == 0)
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




                if (model.GetProbationExtensionList.Count > 0)
                {
                    totalrows = m.GetProbationExtensionList[0].active;

                    //m.OnBoardingList.ToList<Hrms.Lite.Shared.Master.Test>().Count; ;
                }

                //model.AllTabCount = m.AllTabCount;
                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = m.GetProbationExtensionList[0].totalRecords;
                    return Json(new
                    {
                        data = model.GetProbationExtensionList,
                        draw = filter1.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows//totalrowsafterfiltering
                    });
                }
                else
                {
                    // return Json(new { data = model.EmployeeProfileList, draw = filter1.draw, recordsTotal = totalrows, recordsFiltered = totalrows });
                    return Json(new
                    {
                        data = model.GetProbationExtensionList,
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





        public async Task<IActionResult> GetNoDueEmpList(Filter filter1, int DocTypeCode, int FormatCode)
        {
            try
            {
                EmployeeCompanyDocuments model = new EmployeeCompanyDocuments();
                EmployeeCompanyDocuments m = new EmployeeCompanyDocuments();
                //model.EmployeeHeader = new EmployeeHeader();

                Filter filter = new Filter();
                Global.GlobalFilter = filter1;
                Global.FromDate = filter1.F_From.ToString("yyyy-MMM-dd");
                Global.Todate = filter1.F_To.ToString("yyyy-MMM-dd");

                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }

                model = await _EmployeeCompanyDocumentsService.GetNoDueEmpList(DocTypeCode, FormatCode, Global.FromDate, Global.Todate, filter1);

                m.EmployeeCompanyDocumentsList = model.EmployeeCompanyDocumentsList;
                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.EmployeeCompanyDocumentsList == null || model.EmployeeCompanyDocumentsList.Count() == 0)
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




                if (model.EmployeeCompanyDocumentsList.Count > 0)
                {
                    totalrows = m.EmployeeCompanyDocumentsList[0].active;

                    //m.OnBoardingList.ToList<Hrms.Lite.Shared.Master.Test>().Count; ;
                }

                //model.AllTabCount = m.AllTabCount;
                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = m.EmployeeCompanyDocumentsList[0].totalRecords;
                    return Json(new
                    {
                        data = model.EmployeeCompanyDocumentsList,
                        draw = filter1.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows//totalrowsafterfiltering
                    });
                }
                else
                {
                    // return Json(new { data = model.EmployeeProfileList, draw = filter1.draw, recordsTotal = totalrows, recordsFiltered = totalrows });
                    return Json(new
                    {
                        data = model.EmployeeCompanyDocumentsList,
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

        public async Task<IActionResult> GetOfferLetterList(Filter filter1, int DocTypeCode, string Type)
        {
            try
            {
                OfferLetter model = new OfferLetter();
                OfferLetter m = new OfferLetter();
                //model.EmployeeHeader = new EmployeeHeader();

                


                Filter filter = new Filter();
                Global.GlobalFilter = filter1;
                Global.FromDate = filter1.F_From.ToString("yyyy-MMM-dd");
                Global.Todate = filter1.F_To.ToString("yyyy-MMM-dd");

                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }

            
                model = await _EmployeeCompanyDocumentsService.GetofferLetterList("APPROVAL PENDING", DocTypeCode,Type, filter1);
                m.OfferLetterList = model.OfferLetterList;
                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.OfferLetterList == null || model.OfferLetterList.Count() == 0)
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




                if (model.OfferLetterList.Count > 0)
                {
                    totalrows = m.OfferLetterList[0].active;

                    //m.OnBoardingList.ToList<Hrms.Lite.Shared.Master.Test>().Count; ;
                }

                //model.AllTabCount = m.AllTabCount;
                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = m.OfferLetterList[0].totalRecords;
                    return Json(new
                    {
                        data = model.OfferLetterList,
                        draw = filter1.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows//totalrowsafterfiltering
                    });
                }
                else
                {
                    // return Json(new { data = model.EmployeeProfileList, draw = filter1.draw, recordsTotal = totalrows, recordsFiltered = totalrows });
                    return Json(new
                    {
                        data = model.OfferLetterList,
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





        public async Task<IActionResult> GetSalaryLetterList(String FromDate, String Todate, Filter filter1, int DocTypeCode, int DocumentFormatCode, string Type)
        {
            try
            {
                EmployeeCompanyDocuments model = new EmployeeCompanyDocuments();
                EmployeeCompanyDocuments m = new EmployeeCompanyDocuments();
                //model.EmployeeHeader = new EmployeeHeader();




                Filter filter = new Filter();
                Global.GlobalFilter = filter1;
                Global.FromDate = filter1.F_From.ToString("yyyy-MMM-dd");
                Global.Todate = filter1.F_To.ToString("yyyy-MMM-dd");

                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }

                model = await _EmployeeCompanyDocumentsService.SalaryCertificateList(FromDate, Todate,DocTypeCode, DocumentFormatCode,Type,  filter1);

                m.EmployeeSalaryList = model.EmployeeSalaryList;
                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.EmployeeSalaryList == null || model.EmployeeSalaryList.Count() == 0)
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




                if (model.EmployeeSalaryList.Count > 0)
                {
                    totalrows = m.EmployeeSalaryList[0].active;

                    //m.OnBoardingList.ToList<Hrms.Lite.Shared.Master.Test>().Count; ;
                }

                //model.AllTabCount = m.AllTabCount;
                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = m.EmployeeSalaryList[0].totalRecords;
                    return Json(new
                    {
                        data = model.EmployeeSalaryList,
                        draw = filter1.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows//totalrowsafterfiltering
                    });
                }
                else
                {
                    // return Json(new { data = model.EmployeeProfileList, draw = filter1.draw, recordsTotal = totalrows, recordsFiltered = totalrows });
                    return Json(new
                    {
                        data = model.EmployeeSalaryList,
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



        [HttpPost]
        public async Task<IActionResult> GetExperienceLetterList(Filter filter1,int DocTypeCode, int DocumentFormatCode,string Type)
        {
            try
            {
                ExperienceLetter model = new ExperienceLetter();
                ExperienceLetter m = new ExperienceLetter();
                //model.EmployeeHeader = new EmployeeHeader();

                


                Filter filter = new Filter();
                Global.GlobalFilter = filter1;
                Global.FromDate = filter1.F_From.ToString("yyyy-MMM-dd");
                Global.Todate = filter1.F_To.ToString("yyyy-MMM-dd");
                
                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }

                model = await _EmployeeCompanyDocumentsService.GetExperienceLetter(Global.FromDate, Global.Todate, DocTypeCode, DocumentFormatCode, Type, filter1);
              
                m.GetExperienceLetterList = model.GetExperienceLetterList;
                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.GetExperienceLetterList == null || model.GetExperienceLetterList.Count() == 0)
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




                if (model.GetExperienceLetterList.Count > 0)
                {
                    totalrows = m.GetExperienceLetterList[0].active;

                    //m.OnBoardingList.ToList<Hrms.Lite.Shared.Master.Test>().Count; ;
                }

                //model.AllTabCount = m.AllTabCount;
                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = m.GetExperienceLetterList[0].totalRecords;
                    return Json(new
                    {
                        data = model.GetExperienceLetterList,
                        draw = filter1.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows//totalrowsafterfiltering
                    });
                }
                else
                {
                    // return Json(new { data = model.EmployeeProfileList, draw = filter1.draw, recordsTotal = totalrows, recordsFiltered = totalrows });
                    return Json(new
                    {
                        data = model.GetExperienceLetterList,
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


        [HttpPost]
        public async Task<IActionResult> GetRelievingLetterList(Filter filter1, int DocTypeCode, int DocumentFormatCode)
        {
            try
            {
                RelievingLetter model = new RelievingLetter();
                RelievingLetter m = new RelievingLetter();
                //model.EmployeeHeader = new EmployeeHeader();




                Filter filter = new Filter();
                Global.GlobalFilter = filter1;
                Global.FromDate = filter1.F_From.ToString("yyyy-MMM-dd");
                Global.Todate = filter1.F_To.ToString("yyyy-MMM-dd");

                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }

                model = await _EmployeeCompanyDocumentsService.GetRelievingLetterList(Global.FromDate, Global.Todate, DocTypeCode, DocumentFormatCode, filter1);

                m.GetRelievingLetterList = model.GetRelievingLetterList;
                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.GetRelievingLetterList == null || model.GetRelievingLetterList.Count() == 0)
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




                if (model.GetRelievingLetterList.Count > 0)
                {
                    totalrows = m.GetRelievingLetterList[0].active;

                    //m.OnBoardingList.ToList<Hrms.Lite.Shared.Master.Test>().Count; ;
                }

                //model.AllTabCount = m.AllTabCount;
                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = m.GetRelievingLetterList[0].totalRecords;
                    return Json(new
                    {
                        data = model.GetRelievingLetterList,
                        draw = filter1.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows//totalrowsafterfiltering
                    });
                }
                else
                {
                    // return Json(new { data = model.EmployeeProfileList, draw = filter1.draw, recordsTotal = totalrows, recordsFiltered = totalrows });
                    return Json(new
                    {
                        data = model.GetRelievingLetterList,
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


        [HttpPost]
        public async Task<IActionResult> GetTerminationLetterList(Filter filter1, int DocTypeCode, int DocumentFormatCode)
        {
            try
            {
                Termination model = new Termination();
                Termination m = new Termination();
                //model.EmployeeHeader = new EmployeeHeader();

                string FromDate = filter1.F_From.ToString("yyyy-MMM-dd");
                string Todate = filter1.F_To.ToString("yyyy-MMM-dd");

                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }

                model = await _EmployeeCompanyDocumentsService.GetTerminationList(FromDate,Todate, DocTypeCode, DocumentFormatCode, filter1);

                m.TerminationList = model.TerminationList;
                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.TerminationList == null || model.TerminationList.Count() == 0)
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




                if (model.TerminationList.Count > 0)
                {
                    totalrows = m.TerminationList[0].active;

                    //m.OnBoardingList.ToList<Hrms.Lite.Shared.Master.Test>().Count; ;
                }

                //model.AllTabCount = m.AllTabCount;
                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = m.TerminationList[0].totalRecords;
                    return Json(new
                    {
                        data = model.TerminationList,
                        draw = filter1.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows//totalrowsafterfiltering
                    });
                }
                else
                {
                    // return Json(new { data = model.EmployeeProfileList, draw = filter1.draw, recordsTotal = totalrows, recordsFiltered = totalrows });
                    return Json(new
                    {
                        data = model.TerminationList,
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




        [HttpPost]
        public async Task<IActionResult> GetEmploymentCertificateList(Filter filter1, int DocTypeCode, int DocumentFormatCode)
        {
            try
            {
                EmploymentCertificate model = new EmploymentCertificate();
                EmploymentCertificate m = new EmploymentCertificate();
                //model.EmployeeHeader = new EmployeeHeader();




                Filter filter = new Filter();
                Global.GlobalFilter = filter1;
                Global.FromDate = filter1.F_From.ToString("yyyy-MMM-dd");
                Global.Todate = filter1.F_To.ToString("yyyy-MMM-dd");

                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }

                model = await _EmployeeCompanyDocumentsService.GetEmploymentCertificate(Global.FromDate, Global.Todate, DocTypeCode, DocumentFormatCode, filter1);

                m.GetEmploymentCertificateList = model.GetEmploymentCertificateList;
                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.GetEmploymentCertificateList == null || model.GetEmploymentCertificateList.Count() == 0)
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




                if (model.GetEmploymentCertificateList.Count > 0)
                {
                    totalrows = m.GetEmploymentCertificateList[0].active;

                    //m.OnBoardingList.ToList<Hrms.Lite.Shared.Master.Test>().Count; ;
                }

                //model.AllTabCount = m.AllTabCount;
                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = m.GetEmploymentCertificateList[0].totalRecords;
                    return Json(new
                    {
                        data = model.GetEmploymentCertificateList,
                        draw = filter1.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows//totalrowsafterfiltering
                    });
                }
                else
                {
                    // return Json(new { data = model.EmployeeProfileList, draw = filter1.draw, recordsTotal = totalrows, recordsFiltered = totalrows });
                    return Json(new
                    {
                        data = model.GetEmploymentCertificateList,
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





        [HttpPost]
        public async Task<IActionResult> GetFTCRevisionList(Filter filter1, int DocTypeCode, int DocumentFormatCode,string Type)
        {
            try
            {
                FTCRevisionLetter model = new FTCRevisionLetter();
                FTCRevisionLetter m = new FTCRevisionLetter();
                //model.EmployeeHeader = new EmployeeHeader();




                Filter filter = new Filter();
                Global.GlobalFilter = filter1;
                Global.FromDate = filter1.F_From.ToString("yyyy-MMM-dd");
                Global.Todate = filter1.F_To.ToString("yyyy-MMM-dd");

                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }

                model = await _EmployeeCompanyDocumentsService.GetFTCRevisionList(Global.FromDate, Global.Todate, DocTypeCode, DocumentFormatCode,Type, filter1);

                m.GetFTCRevisionLetterList = model.GetFTCRevisionLetterList;
                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.GetFTCRevisionLetterList == null || model.GetFTCRevisionLetterList.Count() == 0)
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




                if (model.GetFTCRevisionLetterList.Count > 0)
                {
                    totalrows = m.GetFTCRevisionLetterList[0].active;

                    //m.OnBoardingList.ToList<Hrms.Lite.Shared.Master.Test>().Count; ;
                }

                //model.AllTabCount = m.AllTabCount;
                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = m.GetFTCRevisionLetterList[0].totalRecords;
                    return Json(new
                    {
                        data = model.GetFTCRevisionLetterList,
                        draw = filter1.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows//totalrowsafterfiltering
                    });
                }
                else
                {
                    // return Json(new { data = model.EmployeeProfileList, draw = filter1.draw, recordsTotal = totalrows, recordsFiltered = totalrows });
                    return Json(new
                    {
                        data = model.GetFTCRevisionLetterList,
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


        [HttpPost]
        public async Task<IActionResult> GetPromotionList(Filter filter1, int DocTypeCode, int DocumentFormatCode,string Type)
        {
            try
            {
                PromotionLetter model = new PromotionLetter();
                PromotionLetter m = new PromotionLetter();
                //model.EmployeeHeader = new EmployeeHeader();




                Filter filter = new Filter();
                Global.GlobalFilter = filter1;
                Global.FromDate = filter1.F_From.ToString("yyyy-MMM-dd");
                Global.Todate = filter1.F_To.ToString("yyyy-MMM-dd");

                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }

                model = await _EmployeeCompanyDocumentsService.GetPromotionList(Global.FromDate, Global.Todate, DocTypeCode, DocumentFormatCode,Type, filter1);

                m.GetPromotionLetterList = model.GetPromotionLetterList;
                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.GetPromotionLetterList == null || model.GetPromotionLetterList.Count() == 0)
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




                if (model.GetPromotionLetterList.Count > 0)
                {
                    totalrows = m.GetPromotionLetterList[0].active;

                    //m.OnBoardingList.ToList<Hrms.Lite.Shared.Master.Test>().Count; ;
                }

                //model.AllTabCount = m.AllTabCount;
                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = m.GetPromotionLetterList[0].totalRecords;
                    return Json(new
                    {
                        data = model.GetPromotionLetterList,
                        draw = filter1.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows//totalrowsafterfiltering
                    });
                }
                else
                {
                    // return Json(new { data = model.EmployeeProfileList, draw = filter1.draw, recordsTotal = totalrows, recordsFiltered = totalrows });
                    return Json(new
                    {
                        data = model.GetPromotionLetterList,
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



        [HttpPost]
        public async Task<IActionResult> GetSalaryEmpList(Filter filter1, int DocTypeCode)
        {
            try
            {
                EmployeeCompanyDocuments model = new EmployeeCompanyDocuments();
                EmployeeCompanyDocuments m = new EmployeeCompanyDocuments();
                //model.EmployeeHeader = new EmployeeHeader();




                Filter filter = new Filter();
                Global.GlobalFilter = filter1;
                Global.FromDate = filter1.F_From.ToString("yyyy-MMM-dd");
                Global.Todate = filter1.F_To.ToString("yyyy-MMM-dd");

                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }

                model = await _EmployeeCompanyDocumentsService.GetSalaryEmpList(filter1);

                m.EmployeeSalaryList = model.EmployeeSalaryList;
                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.EmployeeSalaryList == null || model.EmployeeSalaryList.Count() == 0)
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




                if (model.EmployeeSalaryList.Count > 0)
                {
                    totalrows = m.EmployeeSalaryList[0].active;

             
                }

   
                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = m.EmployeeSalaryList[0].totalRecords;
                    return Json(new
                    {
                        data = model.EmployeeSalaryList,
                        draw = filter1.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows//totalrowsafterfiltering
                    });
                }
                else
                {
                    // return Json(new { data = model.EmployeeProfileList, draw = filter1.draw, recordsTotal = totalrows, recordsFiltered = totalrows });
                    return Json(new
                    {
                        data = model.EmployeeSalaryList,
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


        [HttpPost]
        public async Task<IActionResult> GetIncrementLetterList(Filter filter1, int DocTypeCode, int DocumentFormatCode,string Type)
        {
            try
            {
                IncrementLetter model = new IncrementLetter();
                IncrementLetter m = new IncrementLetter();
                //model.EmployeeHeader = new EmployeeHeader();




                Filter filter = new Filter();
                Global.GlobalFilter = filter1;
                Global.FromDate = filter1.F_From.ToString("yyyy-MMM-dd");
                Global.Todate = filter1.F_To.ToString("yyyy-MMM-dd");

                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }

                model = await _EmployeeCompanyDocumentsService.GetIncrementList(Global.FromDate, Global.Todate, DocTypeCode, DocumentFormatCode,Type, filter1);

                m.GetIncrementLetterList = model.GetIncrementLetterList;
                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.GetIncrementLetterList == null || model.GetIncrementLetterList.Count() == 0)
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




                if (model.GetIncrementLetterList.Count > 0)
                {
                    totalrows = m.GetIncrementLetterList[0].active;

                    //m.OnBoardingList.ToList<Hrms.Lite.Shared.Master.Test>().Count; ;
                }

                //model.AllTabCount = m.AllTabCount;
                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = m.GetIncrementLetterList[0].totalRecords;
                    return Json(new
                    {
                        data = model.GetIncrementLetterList,
                        draw = filter1.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows//totalrowsafterfiltering
                    });
                }
                else
                {
                    // return Json(new { data = model.EmployeeProfileList, draw = filter1.draw, recordsTotal = totalrows, recordsFiltered = totalrows });
                    return Json(new
                    {
                        data = model.GetIncrementLetterList,
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






        [HttpPost]
        public async Task<IActionResult> GetTransferList(Filter filter1, int DocTypeCode, int DocumentFormatCode)
        {
            try
            {
                EmployeeCompanyDocuments model = new EmployeeCompanyDocuments();
                EmployeeCompanyDocuments m = new EmployeeCompanyDocuments();
                //model.EmployeeHeader = new EmployeeHeader();




                Filter filter = new Filter();
                Global.GlobalFilter = filter1;
                Global.FromDate = filter1.F_From.ToString("yyyy-MMM-dd");
                Global.Todate = filter1.F_To.ToString("yyyy-MMM-dd");

                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }

                model = await _EmployeeCompanyDocumentsService.GetTransferList(DocTypeCode, DocumentFormatCode,Global.FromDate, Global.Todate, filter1);

                m.EmployeeTransferList = model.EmployeeTransferList;
                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.EmployeeTransferList == null || model.EmployeeTransferList.Count() == 0)
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




                if (model.EmployeeTransferList.Count > 0)
                {
                    totalrows = m.EmployeeTransferList[0].active;

                    //m.OnBoardingList.ToList<Hrms.Lite.Shared.Master.Test>().Count; ;
                }

                //model.AllTabCount = m.AllTabCount;
                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = m.EmployeeTransferList[0].totalRecords;
                    return Json(new
                    {
                        data = model.EmployeeTransferList,
                        draw = filter1.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows//totalrowsafterfiltering
                    });
                }
                else
                {
                    // return Json(new { data = model.EmployeeProfileList, draw = filter1.draw, recordsTotal = totalrows, recordsFiltered = totalrows });
                    return Json(new
                    {
                        data = model.EmployeeTransferList,
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



        [HttpPost]
        public async Task<IActionResult> GetWarningLettertList(Filter filter1, int DocTypeCode, int DocumentFormatCode)
        {
            try
            {
                EmployeeCompanyDocuments model = new EmployeeCompanyDocuments();
                EmployeeCompanyDocuments m = new EmployeeCompanyDocuments();
                //model.EmployeeHeader = new EmployeeHeader();




                Filter filter = new Filter();
                Global.GlobalFilter = filter1;
                Global.FromDate = filter1.F_From.ToString("yyyy-MMM-dd");
                Global.Todate = filter1.F_To.ToString("yyyy-MMM-dd");

                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }

                model = await _EmployeeCompanyDocumentsService.GetShowCauseEmpList(DocTypeCode, DocumentFormatCode, Global.FromDate, Global.Todate, filter1);

                m.EmployeeCompanyDocumentsList = model.EmployeeCompanyDocumentsList;
                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.EmployeeCompanyDocumentsList == null || model.EmployeeCompanyDocumentsList.Count() == 0)
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




                if (model.EmployeeCompanyDocumentsList.Count > 0)
                {
                    totalrows = m.EmployeeCompanyDocumentsList[0].active;

                    //m.OnBoardingList.ToList<Hrms.Lite.Shared.Master.Test>().Count; ;
                }

                //model.AllTabCount = m.AllTabCount;
                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = m.EmployeeCompanyDocumentsList[0].totalRecords;
                    return Json(new
                    {
                        data = model.EmployeeCompanyDocumentsList,
                        draw = filter1.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows//totalrowsafterfiltering
                    });
                }
                else
                {
                    // return Json(new { data = model.EmployeeProfileList, draw = filter1.draw, recordsTotal = totalrows, recordsFiltered = totalrows });
                    return Json(new
                    {
                        data = model.EmployeeCompanyDocumentsList,
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


        [HttpPost]
        public async Task<IActionResult> GetShowCauseNoticeList(Filter filter1, int DocTypeCode, int DocumentFormatCode)
        {
            try
            {
                EmployeeCompanyDocuments model = new EmployeeCompanyDocuments();
                EmployeeCompanyDocuments m = new EmployeeCompanyDocuments();
                //model.EmployeeHeader = new EmployeeHeader();




                Filter filter = new Filter();
                Global.GlobalFilter = filter1;
                Global.FromDate = filter1.F_From.ToString("yyyy-MMM-dd");
                Global.Todate = filter1.F_To.ToString("yyyy-MMM-dd");

                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }

                model = await _EmployeeCompanyDocumentsService.GetShowCauseEmpList(DocTypeCode, DocumentFormatCode, Global.FromDate, Global.Todate, filter1);

                m.EmployeeCompanyDocumentsList = model.EmployeeCompanyDocumentsList;
                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.EmployeeCompanyDocumentsList == null || model.EmployeeCompanyDocumentsList.Count() == 0)
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




                if (model.EmployeeCompanyDocumentsList.Count > 0)
                {
                    totalrows = m.EmployeeCompanyDocumentsList[0].active;

                    //m.OnBoardingList.ToList<Hrms.Lite.Shared.Master.Test>().Count; ;
                }

                //model.AllTabCount = m.AllTabCount;
                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = m.EmployeeCompanyDocumentsList[0].totalRecords;
                    return Json(new
                    {
                        data = model.EmployeeCompanyDocumentsList,
                        draw = filter1.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows//totalrowsafterfiltering
                    });
                }
                else
                {
                    // return Json(new { data = model.EmployeeProfileList, draw = filter1.draw, recordsTotal = totalrows, recordsFiltered = totalrows });
                    return Json(new
                    {
                        data = model.EmployeeCompanyDocumentsList,
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





        //public async Task<IActionResult> GetList(int DocTypeCode,string FromDate, string Todate)
        //{
        //    try
        //    {
        //        EmployeeCompanyDocuments model = new EmployeeCompanyDocuments();
        //        Filter filter = new Filter();
        //        Global.FromDate = FromDate;
        //        Global.Todate = Todate;
        //        Global.DocTypeCode = DocTypeCode;
        //        if (DocTypeCode!=0&& FromDate!=null&& Todate!=null)
        //        {
        //            model = await _EmployeeCompanyDocumentsService.GetEmployeeCompanyList(DocTypeCode, FromDate, Todate, filter);

        //        }
        //        return PartialView("~/Areas/DataBank/Reports/Views/EmployeeCompanyDocuments/_List.cshtml", model);
        //    }
        //    catch (UnauthorizedAccessException e)
        //    {

        //        return RedirectToAction("Login", "Account", new { Area = "" });
        //    }
        //    catch (Exception e)
        //    {
        //        return RedirectToAction("Error", "Account", new { Area = "" });
        //    }
        //}

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


        public async Task<IActionResult> ApplyFilter(Filter filter)
        {
            try
            {
                EmployeeCompanyDocuments model = new EmployeeCompanyDocuments();
                Global.GlobalFilter = filter;
                model = await _EmployeeCompanyDocumentsService.GetEmployeeCompanyList(Global.DocTypeCode, Global.FromDate, Global.Todate, Global.GlobalFilter);
                //ViewBag.CalculationType = new SelectList(await _dropdownService.GetCalculationTypeDropdown(), "Code", "Name");
                return PartialView("~/Areas/DataBank/Reports/Views/EmployeeCompanyDocuments/_List.cshtml", model);
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
        public async Task<IActionResult> GetConfirmationList(Filter filter1, int DocTypeCode)
        {
            try
            {
                EmployeeCompanyDocuments model = new EmployeeCompanyDocuments();
                EmployeeCompanyDocuments m = new EmployeeCompanyDocuments();

                Filter filter = new Filter();
                Global.GlobalFilter = filter1;
                Global.FromDate = filter1.F_From.ToString("yyyy-MMM-dd");
                Global.Todate = filter1.F_To.ToString("yyyy-MMM-dd");

                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }

                model = await _EmployeeCompanyDocumentsService.GetConfirmationList(Global.FromDate, Global.Todate, filter1);

                m.EmployeeConfirmationList = model.EmployeeConfirmationList;
                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.EmployeeConfirmationList == null || model.EmployeeConfirmationList.Count() == 0)
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




                if (model.EmployeeConfirmationList.Count > 0)
                {
                    totalrows = m.EmployeeConfirmationList[0].active;

                    //m.OnBoardingList.ToList<Hrms.Lite.Shared.Master.Test>().Count; ;
                }

                //model.AllTabCount = m.AllTabCount;
                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = m.EmployeeConfirmationList[0].totalRecords;
                    return Json(new
                    {
                        data = model.EmployeeConfirmationList,
                        draw = filter1.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows//totalrowsafterfiltering
                    });
                }
                else
                {
                    // return Json(new { data = model.EmployeeProfileList, draw = filter1.draw, recordsTotal = totalrows, recordsFiltered = totalrows });
                    return Json(new
                    {
                        data = model.EmployeeConfirmationList,
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


        [HttpPost]
        public async Task<IActionResult> GetDeploymentList(Filter filter1, int DocTypeCode, int DocumentFormatCode)
        {
            try
            {
                EmployeeCompanyDocuments model = new EmployeeCompanyDocuments();
                EmployeeCompanyDocuments m = new EmployeeCompanyDocuments();
                //model.EmployeeHeader = new EmployeeHeader();




                Filter filter = new Filter();
                Global.GlobalFilter = filter1;
                Global.FromDate = filter1.F_From.ToString("yyyy-MMM-dd");
                Global.Todate = filter1.F_To.ToString("yyyy-MMM-dd");

                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }

                model = await _EmployeeCompanyDocumentsService.GetDeploymentList(DocTypeCode, DocumentFormatCode, Global.FromDate, Global.Todate, filter1);

                m.GetDeploymentList = model.GetDeploymentList;
                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.GetDeploymentList == null || model.GetDeploymentList.Count() == 0)
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




                if (model.GetDeploymentList.Count > 0)
                {
                    totalrows = m.GetDeploymentList[0].active;

                    //m.OnBoardingList.ToList<Hrms.Lite.Shared.Master.Test>().Count; ;
                }

                //model.AllTabCount = m.AllTabCount;
                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = m.GetDeploymentList[0].totalRecords;
                    return Json(new
                    {
                        data = model.GetDeploymentList,
                        draw = filter1.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows//totalrowsafterfiltering
                    });
                }
                else
                {
                    // return Json(new { data = model.EmployeeProfileList, draw = filter1.draw, recordsTotal = totalrows, recordsFiltered = totalrows });
                    return Json(new
                    {
                        data = model.GetDeploymentList,
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
        [HttpPost]
        public async Task<IActionResult> GetStillWorkingLetterList(Filter filter1, int DocTypeCode, int DocumentFormatCode, string Type)
        {
            try
            {
                StillWorkingLetter model = new StillWorkingLetter();
                StillWorkingLetter m = new StillWorkingLetter();
                //model.EmployeeHeader = new EmployeeHeader();




                Filter filter = new Filter();
                Global.GlobalFilter = filter1;
                Global.FromDate = filter1.F_From.ToString("yyyy-MMM-dd");
                Global.Todate = filter1.F_To.ToString("yyyy-MMM-dd");

                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }

                model = await _EmployeeCompanyDocumentsService.StillWorkingList(Global.FromDate, Global.Todate, DocTypeCode, DocumentFormatCode,Type, filter1);

                m.GetStillWorkingLetterList = model.GetStillWorkingLetterList;
                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.GetStillWorkingLetterList == null || model.GetStillWorkingLetterList.Count() == 0)
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




                if (model.GetStillWorkingLetterList.Count > 0)
                {
                    totalrows = m.GetStillWorkingLetterList[0].active;

                    //m.OnBoardingList.ToList<Hrms.Lite.Shared.Master.Test>().Count; ;
                }

                //model.AllTabCount = m.AllTabCount;
                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = m.GetStillWorkingLetterList[0].totalRecords;
                    return Json(new
                    {
                        data = model.GetStillWorkingLetterList,
                        draw = filter1.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows//totalrowsafterfiltering
                    });
                }
                else
                {
                    // return Json(new { data = model.EmployeeProfileList, draw = filter1.draw, recordsTotal = totalrows, recordsFiltered = totalrows });
                    return Json(new
                    {
                        data = model.GetStillWorkingLetterList,
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




        [HttpPost]
        public async Task<IActionResult> GetAbscondingLetterList(Filter filter1, int DocTypeCode, int DocumentFormatCode, string Type)
        {
            try
            {
                AbscondingLetter model = new AbscondingLetter();
                AbscondingLetter m = new AbscondingLetter();
                //model.EmployeeHeader = new EmployeeHeader();




                Filter filter = new Filter();
                Global.GlobalFilter = filter1;
                Global.FromDate = filter1.F_From.ToString("yyyy-MMM-dd");
                Global.Todate = filter1.F_To.ToString("yyyy-MMM-dd");

                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }

                model = await _EmployeeCompanyDocumentsService.AbscondingLetterList( DocTypeCode, DocumentFormatCode, Type, filter1);

                m.GetAbscondingLetterList = model.GetAbscondingLetterList;
                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.GetAbscondingLetterList == null || model.GetAbscondingLetterList.Count() == 0)
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

                if (model.GetAbscondingLetterList.Count > 0)
                {
                    totalrows = m.GetAbscondingLetterList[0].active;
                    //m.OnBoardingList.ToList<Hrms.Lite.Shared.Master.Test>().Count; ;
                }
                //model.AllTabCount = m.AllTabCount;
                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = m.GetAbscondingLetterList[0].totalRecords;
                    return Json(new
                    {
                        data = model.GetAbscondingLetterList,
                        draw = filter1.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows//totalrowsafterfiltering
                    });
                }
                else
                {
                    // return Json(new { data = model.EmployeeProfileList, draw = filter1.draw, recordsTotal = totalrows, recordsFiltered = totalrows });
                    return Json(new
                    {
                        data = model.GetAbscondingLetterList,
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






    }
}
