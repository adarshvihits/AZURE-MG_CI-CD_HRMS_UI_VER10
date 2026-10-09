using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.TDS.Reports;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TDS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.StaticFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.TDS.Reports.Controllers
{
    [Area("TDS")]
    [SessionAuthorize]
    public class TDSDeclarationDetailsOldController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly ITDSDeclarationDetailsOldService _InvestmentService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        public TDSDeclarationDetailsOldController(ITDSDeclarationDetailsOldService InvestmentDeclarationAppService, IDropdownService dropdownService, ICommonService commonService, IHelpers Helpers)
        {
            _InvestmentService = InvestmentDeclarationAppService;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = Helpers;

        }
        public async Task<IActionResult> Index()
        {
            InvestmentDeclaration model = new InvestmentDeclaration();
            ViewBag.AssessmentYear = new SelectList(await _dropdownService.GetAssessmentYearForTDSOldDropdown(), "Code", "Name");
            model.filter = await _commonService.GetFilterDropdowns();
            return View("~/Areas/TDS/Reports/Views/TDSDeclarationDetailsOld/Index.cshtml", model);
        }

        [HttpPost]
        public async Task<IActionResult> GetTdsEmpList(Filter filter1, int AssessmentYear)
        {
            try
            {
                InvestmentDeclaration model = new InvestmentDeclaration();
                InvestmentDeclaration m = new InvestmentDeclaration();
                Filter filter = new Filter();
                model.filter = filter1;


                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }
               
                if (AssessmentYear != 0)
                {
                    model = await _InvestmentService.GetEmployeeList(AssessmentYear, filter1);

                }
                m.TDSEmployeeList = model.TDSEmployeeList;
                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.TDSEmployeeList == null || model.TDSEmployeeList.Count() == 0)
                {


                    return Json(new
                    {
                        data = "",
                        draw = filter1.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows
                    });
                }

                if (model.TDSEmployeeList.Count > 0)
                {
                    totalrows = m.TDSEmployeeList[0].active;
                }

                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = m.TDSEmployeeList[0].totalRecords;
                    return Json(new
                    {
                        data = model.TDSEmployeeList,
                        draw = filter1.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows//totalrowsafterfiltering
                    });
                }
                else
                {
                    return Json(new
                    {
                        data = model.TDSEmployeeList,
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
        public async Task<IActionResult> Create(Guid EmployeeGI, int AssessmentYear)
        {
            InvestmentDeclaration model = new InvestmentDeclaration();
            InvestmentDeclarationDropdown dropmodel = new InvestmentDeclarationDropdown();
            dropmodel = await _InvestmentService.GetTDSDropDown(AssessmentYear);

            ViewBag.AssessmentYear = new SelectList(await _dropdownService.GetAssessmentYearForTDSOldDropdown(), "Code", "Name");




            model = await _InvestmentService.GetInvestmentDeclarationDetails(EmployeeGI, AssessmentYear);
            model.EmployeeHeader = await _commonService.GetEmployeeHeader(EmployeeGI);
            model.AssessmentYear = new MasterBase();
            model.AssessmentYear.Code = AssessmentYear;
            model.EmployeeGuid = EmployeeGI;

            ViewBag.MetroCity = new SelectList(dropmodel.City, "Code", "Name");

            ViewBag.YesorNo = new SelectList(dropmodel.YesorNo, "Code", "Name");

            ViewBag.Percentage = new SelectList(dropmodel.PercentageofDisability, "Code", "Name");
            ViewBag.Relation = new SelectList(dropmodel.RelationShip, "Code", "Name");
            ViewBag.NatureofDisability = new SelectList(dropmodel.NatureofDisability, "Code", "Name");
            ViewBag.CriticalAliments = new SelectList(dropmodel.CriticalAilments, "Code", "Name");
            ViewBag.DepRelation = new SelectList(dropmodel.DependentRelationShip, "Code", "Name");
            ViewBag.MCRelation = new SelectList(dropmodel.MedicalCoverage, "Code", "Name");

            if (model.ChapterVIADetails.ECPurchaseDate == Convert.ToDateTime("01-Jan-0001"))
            {
                model.ChapterVIADetails.ECPurchaseDate = null;
            }
            if (model.ChapterVIADetails.HouseLoanDate == Convert.ToDateTime("01-Jan-0001"))
            {
                model.ChapterVIADetails.HouseLoanDate = null;
            }
            if (model.ChapterVIADetails.EEAHouseLoanDate == Convert.ToDateTime("01-Jan-0001"))
            {
                model.ChapterVIADetails.EEAHouseLoanDate = null;
            }
            if (model.ChapterVIADetails.EduLoanInterestDate == Convert.ToDateTime("01-Jan-0001"))
            {
                model.ChapterVIADetails.EduLoanInterestDate = null;
            }
            return View("~/Areas/TDS/Reports/Views/TDSDeclarationDetailsOld/Create.cshtml", model);
        }
        #region DOCUMENT UPLOAD
        public async Task<IActionResult> GetPopUpforDocUpload(string idType, string idRef, int assYear, Guid EmployeeGI)
        {
            try
            {

                InvestmentDeclaration model = new InvestmentDeclaration();
                model.AssessmentYear = new MasterBase();
                model.DocumentList = await _InvestmentService.GetDocumentList(EmployeeGI, assYear, idType, idRef);


                return View("~/Areas/TDS/Reports/Views/TDSDeclarationDetailsOld/_DocumentUploadPopUp.cshtml", model);
            }

            catch (Exception ex)
            {
                string ErrorMessage = ex.Message.ToString();
                if (ex.InnerException != null)
                {
                    if (ex.InnerException.Message != null)
                    {

                        ErrorMessage = ErrorMessage + ex.InnerException.Message.ToString();
                    }
                }
                ViewData["Error"] = ErrorMessage;
                return View("ShowError");
            }
        }
        public async Task<IActionResult> ViewDocument(int TrnCode)
        {
            DocumentList model = new DocumentList();
            model.Attachment = await _InvestmentService.GetDocumentForView(TrnCode);

            var fileBytes = model.Attachment.fdata; 

            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(model.Attachment.FileName, out string contentType))
            {
                contentType = "application/octet-stream";
            }

            Response.Headers["Content-Disposition"] =
                $"inline; filename=\"{model.Attachment.FileName}\"";

            return File(fileBytes, contentType);
        }
        //public async Task<IActionResult> ViewDocument(int TrnCode)
        //{
        //    DocumentList model = new DocumentList();
        //    model.Attachment = await _InvestmentService.GetDocumentForView(TrnCode);

        //    var decompressedBytes = await _helpers.DecompressFile(model.Attachment.fdata);
        //    var provider = new FileExtensionContentTypeProvider();
        //    if (!provider.TryGetContentType(model.Attachment.FileName, out string contentType))
        //    {
        //        // default if unknown
        //        contentType = "application/octet-stream";
        //    }
        //    Response.Headers["Content-Disposition"] = $"inline; filename=\"{model.Attachment.FileName}\"";
        //    return File(decompressedBytes, contentType);
        //}
        #endregion
    }
}
