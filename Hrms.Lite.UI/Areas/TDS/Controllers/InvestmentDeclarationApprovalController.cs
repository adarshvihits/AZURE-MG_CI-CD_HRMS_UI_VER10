using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.TDS;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TDS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.StaticFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.TDS.Controllers
{
    [Area("TDS")]
    [SessionAuthorize]
    public class InvestmentDeclarationApprovalController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly IInvestmentDeclarationApprovalService _InvestmentService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        public static class Global
        {
            public static Filter GlobalFilter;
            public static int monthCode;

        }
        public InvestmentDeclarationApprovalController(IInvestmentDeclarationApprovalService InvestmentDeclarationAppService, IDropdownService dropdownService, ICommonService commonService, IHelpers Helpers)
        {
            _InvestmentService = InvestmentDeclarationAppService;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = Helpers;

        }
        public async Task<IActionResult> Index()
        {
            InvestmentDeclaration model = new InvestmentDeclaration();
            ViewBag.AssessmentYear = new SelectList(await _dropdownService.GetAssessmentYearDropdown(), "Code", "Name");
            model.filter = await _commonService.GetFilterDropdowns();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> GetInvestDeclarationApprovalEmpList(Filter filter1, int AssessmentYear, int TabIndex)
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
                filter1.Tabindex = TabIndex;
                if (AssessmentYear != 0)
                {
                    model = await _InvestmentService.GetApprovalList(AssessmentYear, filter1);

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
        public async Task<IActionResult> Approve(Guid EmployeeGI, int AssessmentYear)
        {
            InvestmentDeclaration model = new InvestmentDeclaration();
            InvestmentDeclarationDropdown dropmodel = new InvestmentDeclarationDropdown();
            dropmodel = await _InvestmentService.GetTDSDropDown(AssessmentYear);

            ViewBag.AssessmentYear = new SelectList(await _dropdownService.GetAssessmentYearDropdown(), "Code", "Name");
            ViewBag.MetroCity = new SelectList(dropmodel.City, "Code", "Name");
            ViewBag.YesorNo = new SelectList(dropmodel.YesorNo, "Code", "Name");
            ViewBag.Percentage = new SelectList(dropmodel.PercentageofDisability, "Code", "Name");
            ViewBag.Relation = new SelectList(dropmodel.RelationShip, "Code", "Name");
            ViewBag.NatureofDisability = new SelectList(dropmodel.NatureofDisability, "Code", "Name");
            ViewBag.CriticalAliments = new SelectList(dropmodel.CriticalAilments, "Code", "Name");
            ViewBag.DepRelation = new SelectList(dropmodel.DependentRelationShip, "Code", "Name");
            ViewBag.MCRelation = new SelectList(dropmodel.MedicalCoverage, "Code", "Name");
            ViewBag.OwnerStatus = new SelectList(dropmodel.OwnerStatus, "Code", "Name");
            ViewBag.Purpose = new SelectList(dropmodel.Purpose, "Code", "Name");
            ViewBag.LocationStatus = new SelectList(dropmodel.LocationStatus, "Code", "Name");

            model = await _InvestmentService.GetInvDeclarationApprovalDetails(EmployeeGI, AssessmentYear);
            model.EmployeeHeader = await _commonService.GetEmployeeHeader(EmployeeGI);
            model.AssessmentYear = new MasterBase();
            model.AssessmentYear.Code = AssessmentYear;
            model.EmployeeGuid = EmployeeGI;

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
            return View(model);
        }
        #region HOUSE RENTAL & LANDLORD APPROVAL
        [HttpPost]
        public async Task<IActionResult> HouseRentalandLandlordApproval(InvestmentDeclaration model, string Status, string Remarks)
        {


            model.TabIndex = "HouseRental";
            model.ApproveReject = Status;
            model.ApproverComments = Remarks;
            var response = await _InvestmentService.SaveInvestmentDeclarationApproval(model);
            if (response.Success == true)
            {
                return Json(response);
            }
            return Json(response);

        }
        public async Task<IActionResult> GetRentalDetails(Guid EmployeeGI, int AssessmentYear)
        {
            try
            {
                InvestmentDeclaration model = new InvestmentDeclaration();
                model.LandlordDetails = new LandlordDetails();
                InvestmentDeclarationDropdown dropmodel = new InvestmentDeclarationDropdown();
                dropmodel = await _InvestmentService.GetTDSDropDown(AssessmentYear);

                model = await _InvestmentService.GetInvDeclarationApprovalDetails(EmployeeGI, AssessmentYear);

                ViewBag.From = new SelectList(dropmodel.From, "Code", "Name");
                ViewBag.To = new SelectList(dropmodel.To, "Code", "Name");
                ViewBag.MetroCity = new SelectList(dropmodel.City, "Code", "Name");
                model.AssessmentYear = new MasterBase();
                model.AssessmentYear.Code = AssessmentYear;
                model.EmployeeGuid = EmployeeGI;
                return PartialView("_HouseRentalApproval", model);
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
        #endregion
        #region ROW WISE APPROVE OR REJECT
        [HttpPost]
        public async Task<IActionResult> RowWiseApproveOrReject(InvestmentDeclaration model, string TabIndex, string Status, string Remarks)
        {
            model.TabIndex = TabIndex;
            if (model.TabIndex == "80CSection")
            {
                var EightyC = from li in model.EightyCDeductionList
                              where li.flag == 1
                              select li;
                model.EightyCDeductionList = EightyC.ToList();
            }
            if (model.TabIndex == "lic")
            {
                var LIC = from li in model.LICList
                          where li.flag == 1
                          select li;
                model.LICList = LIC.ToList();
            }
            if (model.TabIndex == "80GSection")
            {
                var EightyG = from li in model.DonationList
                              where li.flag == 1
                              select li;
                model.DonationList = EightyG.ToList();
            }
            if (model.TabIndex == "HLRepay")
            {
                var repay = from li in model.HLRepayList
                            where li.flag == 1
                            select li;
                model.HLRepayList = repay.ToList();
            }
            if (model.TabIndex == "LetOut")
            {
                var letout = from li in model.LetOutList
                             where li.flag == 1
                             select li;
                model.LetOutList = letout.ToList();
            }
            if (model.TabIndex == "OtherIncome")
            {
                var OtherIn = from li in model.OtherIncomeList
                              where li.flag == 1
                              select li;
                model.OtherIncomeList = OtherIn.ToList();
            }
            model.ApproveReject = Status;
            model.ApproverComments = Remarks;
            var response = await _InvestmentService.SaveInvestmentDeclarationApproval(model);
            if (response.Success == true)
            {
                return Json(response);
            }

            return Json(response);

        }
        #endregion
        #region CHAPTERVIA
        [HttpPost]
        public async Task<IActionResult> ChapterVIAApproveOrReject(InvestmentDeclaration model, string SectionIndex, string Status, string Remarks)
        {




            model.ChapterVIADetails.VIASectionTab = SectionIndex;
            model.TabIndex = "ChapterVIA";
            model.ApproveReject = Status;
            model.ApproverComments = Remarks;

            var response = await _InvestmentService.SaveInvestmentDeclarationApproval(model);
            if (response.Success == true)
            {
                return Json(response);
            }

            return Json(response);

        }
        #endregion
        #region 80C DEDUCTION & LIC
        public async Task<IActionResult> RefeshTabDetails(Guid EmployeeGI, int AssessmentYear, string TabIndex)
        {
            try
            {
                InvestmentDeclaration model = new InvestmentDeclaration();
                model.LICDetails = new LICDetails();
                InvestmentDeclarationDropdown dropmodel = new InvestmentDeclarationDropdown();
                dropmodel = await _InvestmentService.GetTDSDropDown(AssessmentYear);


                model = await _InvestmentService.GetInvDeclarationApprovalDetails(EmployeeGI, AssessmentYear);
                ViewBag.YesorNo = new SelectList(dropmodel.YesorNo, "Code", "Name");
                ViewBag.Percentage = new SelectList(dropmodel.PercentageofDisability, "Code", "Name");
                ViewBag.Relation = new SelectList(dropmodel.RelationShip, "Code", "Name");
                ViewBag.NatureofDisability = new SelectList(dropmodel.NatureofDisability, "Code", "Name");
                ViewBag.CriticalAliments = new SelectList(dropmodel.CriticalAilments, "Code", "Name");
                ViewBag.DepRelation = new SelectList(dropmodel.DependentRelationShip, "Code", "Name");
                ViewBag.MCRelation = new SelectList(dropmodel.MedicalCoverage, "Code", "Name");
                ViewBag.OwnerStatus = new SelectList(dropmodel.OwnerStatus, "Code", "Name");
                ViewBag.Purpose = new SelectList(dropmodel.Purpose, "Code", "Name");
                ViewBag.LocationStatus = new SelectList(dropmodel.LocationStatus, "Code", "Name");
                ViewBag.Relation = new SelectList(dropmodel.RelationShip, "Code", "Name");
                model.AssessmentYear = new MasterBase();
                model.AssessmentYear.Code = AssessmentYear;
                model.EmployeeGuid = EmployeeGI;
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
                if (TabIndex == "80CSection")
                {
                    return PartialView("_80CDeductionsApproval", model);
                }
                if (TabIndex == "lic")
                {
                    return PartialView("_80CDeductionsApproval", model);
                }
                if (TabIndex == "80GSection")
                {
                    return PartialView("_80GDonationApproval", model);
                }
                if (TabIndex == "OtherIncome")
                {
                    return PartialView("_OtherIncomeApproval", model);
                }
                if (TabIndex == "LetOut")
                {
                    return PartialView("_LetOutHousePropertyApproval", model);
                }
                if (TabIndex == "HLRepay")
                {
                    return PartialView("_HouseLoanRepayApproval", model);
                }
                if (TabIndex == "ChapterVIA")
                {
                    return PartialView("_ChapterVIAApproval", model);
                }
                return RedirectToAction("Approve", new { EmployeeGI = EmployeeGI, AssessmentYear = AssessmentYear });


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
        #endregion
        #region DOCUMENT UPLOAD
        public async Task<IActionResult> GetPopUpforDocUpload(string idType, string idRef, int assYear, Guid EmployeeGI)
        {
            try
            {

                InvestmentDeclaration model = new InvestmentDeclaration();
                model.AssessmentYear = new MasterBase();
                model.DocumentList = await _InvestmentService.GetDocumentList(EmployeeGI, assYear, idType, idRef);


                return View("_DocumentUploadPopUp", model);
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

            var decompressedBytes = await _helpers.DecompressFile(model.Attachment.fdata);
            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(model.Attachment.FileName, out string contentType))
            {
                // default if unknown
                contentType = "application/octet-stream";
            }
            Response.Headers["Content-Disposition"] = $"inline; filename=\"{model.Attachment.FileName}\"";
            return File(decompressedBytes, contentType);
        }
        #endregion
    }
}
