using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.TDS;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TDS;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hrms.Lite.Shared.PayRoll.Generic.Master;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hrms.Lite.UI.Areas.TDS.Controllers
{
    [Area("TDS")]
    [SessionAuthorize]
    public class InvestmentDeclarationController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly IInvestmentDeclarationService _InvestmentService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        
        public InvestmentDeclarationController(IInvestmentDeclarationService InvestmentDeclarationService, IDropdownService dropdownService, ICommonService commonService, IHelpers Helpers)
        {
            _InvestmentService = InvestmentDeclarationService;
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
        public async Task<IActionResult> GetTDSEmployeeList(Filter filter1, int AssessmentYear)
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
                    model = await _InvestmentService.GetList(AssessmentYear, filter1);

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
        
        public async Task<IActionResult> Create(Guid EmployeeGI,int AssessmentYear)
        {
            InvestmentDeclaration model = new InvestmentDeclaration();
            InvestmentDeclarationDropdown dropmodel = new InvestmentDeclarationDropdown();
            dropmodel = await _InvestmentService.GetTDSDropDown(AssessmentYear);

            ViewBag.AssessmentYear = new SelectList(await _dropdownService.GetAssessmentYearDropdown(), "Code", "Name");

           


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
            return View(model);
        }

        #region HOUSE RENTAL & LANDLORD
        [HttpPost]
        public async Task<IActionResult> HouseRentalSave(InvestmentDeclaration model)
        {
           
         
            model.TabIndex = "HouseRental";

            var response = await _InvestmentService.SaveInvestmentDeclaration(model);
            if (response.Success == true)
            {
                if (model.SliderName == "Modify")
                {
                    response.Message = "Modified Successfully";
                    return Json(response);
                }
                else
                {
                    response.Message = "Saved Successfully";
                    return Json(response);
                }
            }

            return Json(response);

        }
        public async Task<IActionResult> LandLordCreate( Guid EmployeeGI, int AssessmentYear, string Mode, int Rowid, string Name, string Address, string From, int FromCode, string To, int ToCode, string PAN)
        {

            InvestmentDeclaration model = new InvestmentDeclaration();
            InvestmentDeclarationDropdown dropmodel = new InvestmentDeclarationDropdown();
            dropmodel = await _InvestmentService.GetTDSDropDown(AssessmentYear);


            model.SliderName = "Save";
            model.EmployeeGuid = EmployeeGI;
            if (Mode == "Modify")
            {
                model.LandlordDetails = new LandlordDetails();
                model.LandlordDetails.From = new PayrollMonth();
                model.LandlordDetails.To = new PayrollMonth();
                //model.LandlordDetails = await _InvestmentService.GetLandLordDetails(TrnCode, EmployeeGI, AssessmentYear);
                model.LandlordDetails.Name = Name;
                model.LandlordDetails.Address = Address;
                model.LandlordDetails.From.Name = From;
                model.LandlordDetails.From.Code = FromCode;
                model.LandlordDetails.To.Name = To;
                model.LandlordDetails.To.Code = ToCode;
                model.LandlordDetails.Pan = PAN;
            
                model.SliderName = "Modify";
                model.LandlordDetails.Rowid = Rowid;
            }
            else
            {
                model.LandlordDetails = new LandlordDetails();
               
                model.LandlordDetails.Rowid = 0;

            }


            ViewBag.From = new SelectList(dropmodel.From, "Code", "Name");
            ViewBag.To = new SelectList(dropmodel.To, "Code", "Name");
          

            return PartialView("_LandlordCreate", model);


        }
      
        public async Task<IActionResult> GetRentalDetails(Guid EmployeeGI, int AssessmentYear)
        {
            try
            {
                InvestmentDeclaration model = new InvestmentDeclaration();
                model.LandlordDetails = new LandlordDetails();
                InvestmentDeclarationDropdown dropmodel = new InvestmentDeclarationDropdown();
                dropmodel = await _InvestmentService.GetTDSDropDown(AssessmentYear);

                model = await _InvestmentService.GetInvestmentDeclarationDetails(EmployeeGI, AssessmentYear);

                ViewBag.From = new SelectList(dropmodel.From, "Code", "Name");
                ViewBag.To = new SelectList(dropmodel.To, "Code", "Name");
                ViewBag.MetroCity = new SelectList(dropmodel.City, "Code", "Name");
                model.AssessmentYear = new MasterBase();
                model.AssessmentYear.Code = AssessmentYear;
                model.EmployeeGuid = EmployeeGI;
                return PartialView("_HouseRental", model);
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
        public async Task<IActionResult> DeleteLandLordDetails(Guid EmployeeGI, int AssessmentYear, int TrnCode, int TotalRent)
        {

            var response = await _InvestmentService.DeleteLandLordDetails(TrnCode, EmployeeGI, AssessmentYear, TotalRent);
            if (response.Success == true)
            {

                response.Message = "Deleted Successfully";
                return Json(response);

            }
            return Json(response);
        }
        [HttpGet]
        public async Task<JsonResult> GetEmployeePANNumber(Guid EmployeeGI)
        {
            InvestmentDeclaration model = new InvestmentDeclaration();
            model.PANDetails = new PANDetails();
            model.PANDetails = await _InvestmentService.GetEmployeePANNumber(EmployeeGI);
            return Json(model.PANDetails.PANNumber);
        }
        #endregion
        #region INVESTMENT UNDER 80C
        [HttpPost]
        public async Task<IActionResult> DeductionSave(InvestmentDeclaration model)
        {
          
            model.TabIndex = "80CSection";

            var response = await _InvestmentService.SaveInvestmentDeclaration(model);
            if (response.Success == true)
            {
                if (model.SliderName == "Modify")
                {
                    response.Message = "Modified Successfully";
                    return Json(response);
                }
                else
                {
                    response.Message = "Saved Successfully";
                    return Json(response);
                }
            }

            return Json(response);

        }
        #endregion
        #region LIC
        public async Task<IActionResult> LICCreate(Guid EmployeeGI, int AssessmentYear, int? TrnCode)
        {


            InvestmentDeclaration model = new InvestmentDeclaration();
            InvestmentDeclarationDropdown dropmodel = new InvestmentDeclarationDropdown();

            dropmodel = await _InvestmentService.GetTDSDropDown(AssessmentYear);
            model.SliderName = "Save";
            model.EmployeeGuid = EmployeeGI;
            if (TrnCode != null)
            {
                model.LICDetails = await _InvestmentService.GetLICDetails(TrnCode, EmployeeGI, AssessmentYear);
                model.SliderName = "Modify";
            }


            ViewBag.Relation = new SelectList(dropmodel.RelationShip, "Code", "Name");
          

            return PartialView("_LICCreate", model);

        }
        [HttpPost]
        
        public async Task<IActionResult> LICSave(int TrnCode,string Name,int RelationShip, DateTime PolicyTakenDate, string PolicyNo, decimal PremiumAmount, decimal SumAssured, string Remarks, string Tabindex,int AssessmentYear, Guid EmpGI)
        {
            InvestmentDeclaration model = new InvestmentDeclaration();

            model.EmployeeGuid = EmpGI;
            model.TabIndex = Tabindex;

            model.LICDetails = new LICDetails();
            model.LICDetails.RelationShip = new Dropdown();
           

            model.LICDetails.TrnCode = TrnCode;
            model.LICDetails.Name = Name;
            model.LICDetails.PolicyTakenDate = PolicyTakenDate;
            model.LICDetails.RelationShip.Code = RelationShip;
            model.LICDetails.PolicyNo = PolicyNo;
            model.LICDetails.PremiumAmount = PremiumAmount;
            model.LICDetails.SumAssured = SumAssured;
            model.LICDetails.Remarks = Remarks;
            model.AssessmentYear = new MasterBase();
            model.AssessmentYear.Code = AssessmentYear;
            var response = await _InvestmentService.SaveInvestmentDeclaration(model);
            if (response.Success == true)
            {
                if (TrnCode > 0)
                {
                    response.Message = "Modified Successfully";
                    return Json(response);
                }
                else
                {
                    response.Message = "Saved Successfully";
                    return Json(response);
                }
            }
            return Json(response);
        }
        public async Task<IActionResult> GetLICList(Guid EmployeeGI, int AssessmentYear)
        {
            try
            {
                InvestmentDeclaration model = new InvestmentDeclaration();
                model.LICDetails = new LICDetails();
                InvestmentDeclarationDropdown dropmodel = new InvestmentDeclarationDropdown();
                dropmodel = await _InvestmentService.GetTDSDropDown(AssessmentYear);
                Guid EmpGI = EmployeeGI;

                model = await _InvestmentService.GetInvestmentDeclarationDetails(EmpGI, AssessmentYear);

                ViewBag.Relation = new SelectList(dropmodel.RelationShip, "Code", "Name");
                model.AssessmentYear = new MasterBase();
                model.AssessmentYear.Code = AssessmentYear;
                model.EmployeeGuid = EmployeeGI;



                return PartialView("_80CDeductions", model);
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
        public async Task<IActionResult> DeleteLICDetails(Guid EmployeeGI, int AssessmentYear, int TrnCode)
        {

            var response = await _InvestmentService.DeleteLICDetails(TrnCode, EmployeeGI, AssessmentYear);
            if (response.Success == true)
            {

                response.Message = "Deleted Successfully";
                return Json(response);

            }
            return Json(response);
        }
        #endregion
        #region ChapterVIA
        [HttpPost]
        public async Task<IActionResult> ChapterVIASave(InvestmentDeclaration model)
        {
            
            model.TabIndex = "CHAPTERVIA";

            if(model.ChapterVIADetails.TrnCode>0)
            {
                model.SliderName = "Modify";

            }
            else
            {
                model.SliderName = "Save";
            }
            var response = await _InvestmentService.SaveInvestmentDeclaration(model);
            if (response.Success == true)
            {
                if (model.SliderName == "Modify")
                {
                    response.Message = "Modified Successfully";
                    return Json(response);
                }
                else
                {
                    response.Message = "Saved Successfully";
                    return Json(response);
                }
            }

            return Json(response);

        }

        #endregion
        #region 80G Donation
        [HttpPost]
        public async Task<IActionResult> DonationSave(InvestmentDeclaration model)
        {
            
            model.TabIndex = "80GSection";

            var response = await _InvestmentService.SaveInvestmentDeclaration(model);
            if (response.Success == true)
            {
                if (model.SliderName == "Modify")
                {
                    response.Message = "Modified Successfully";
                    return Json(response);
                }
                else
                {
                    response.Message = "Saved Successfully";
                    return Json(response);
                }
            }

            return Json(response);

        }
        #endregion

        #region HOUSE LOAN REPAYMENT
        public async Task<IActionResult> HouseLoanRepayCreate(Guid EmployeeGI, int AssessmentYear, int? TrnCode)
        {

            InvestmentDeclaration model = new InvestmentDeclaration();
            InvestmentDeclarationDropdown dropmodel = new InvestmentDeclarationDropdown();
            dropmodel = await _InvestmentService.GetTDSDropDown(AssessmentYear);

          
            model.SliderName = "Save";
            model.EmployeeGuid = EmployeeGI;
            if (TrnCode != null)
            {
                model.HLRepayDetails = await _InvestmentService.GetHLRepayDetails(TrnCode, EmployeeGI, AssessmentYear);
                model.SliderName = "Modify";
            }
  

            ViewBag.LocationStatus = new SelectList(dropmodel.LocationStatus, "Code", "Name");
            ViewBag.OwnerStatus = new SelectList(dropmodel.OwnerStatus, "Code", "Name");
            ViewBag.Purpose = new SelectList(dropmodel.Purpose, "Code", "Name");

            return PartialView("_HouseLoanRepayCreate", model);

        }
        [HttpPost]
        public async Task<IActionResult> HouseLoanRepaySave(int TrnCode,string HAddress,int LocStatus,int OwnerStatus,decimal Percentage,int Purpose,DateTime LDate, DateTime CDate,decimal PrincipleAmt, decimal Interest, decimal EMIInterest, decimal TotalInterest,string Bank, string PAN, string BankAddress, string Remarks, string Tabindex,int AssessmentYear, Guid EmpGI)
        {
            InvestmentDeclaration model = new InvestmentDeclaration();
     
            model.EmployeeGuid = EmpGI;
            model.TabIndex = Tabindex;

            model.HLRepayDetails = new HLRepayDetails();

            model.HLRepayDetails.LocationStatus = new Dropdown();
            model.HLRepayDetails.OwnerStatus = new Dropdown();
            model.HLRepayDetails.Purpose = new Dropdown();

            model.HLRepayDetails.TrnCode = TrnCode;
            model.HLRepayDetails.HouseAddress = HAddress;
            model.HLRepayDetails.LocationStatus.Code = LocStatus;
            model.HLRepayDetails.OwnerStatus.Code = OwnerStatus;
            model.HLRepayDetails.Percentage = Percentage;
            model.HLRepayDetails.Purpose.Code = Purpose;
            model.HLRepayDetails.DateofLoan= LDate;
            model.HLRepayDetails.DateofCompletion = CDate;
            model.HLRepayDetails.PrincipalAmt = PrincipleAmt;
            model.HLRepayDetails.Interest = Interest;
            model.HLRepayDetails.PreEMIInterest = EMIInterest;
            model.HLRepayDetails.TotalInterest = TotalInterest;
            model.HLRepayDetails.NameofBank = Bank;
            model.HLRepayDetails.PANofBank = PAN;
            model.HLRepayDetails.BankAddress = BankAddress;
            model.HLRepayDetails.Remarks = Remarks;
            model.AssessmentYear = new MasterBase();
            model.AssessmentYear.Code = AssessmentYear; 
            var response = await _InvestmentService.SaveInvestmentDeclaration(model);
            if (response.Success == true)
            {
                if (TrnCode > 0)
                {
                    response.Message = "Modified Successfully";
                    return Json(response);
                }
                else
                {
                    response.Message = "Saved Successfully";
                    return Json(response);
                }
            }
            return Json(response);
        }
        public async Task<IActionResult> GetHouseLoanRepayList(Guid EmployeeGI, int AssessmentYear)
        {
            try
            {
                InvestmentDeclaration model = new InvestmentDeclaration();
                model.HLRepayDetails = new HLRepayDetails();
                InvestmentDeclarationDropdown dropmodel = new InvestmentDeclarationDropdown();
                dropmodel = await _InvestmentService.GetTDSDropDown(AssessmentYear);
      
                model = await _InvestmentService.GetInvestmentDeclarationDetails(EmployeeGI, AssessmentYear);

                ViewBag.LocationStatus = new SelectList(dropmodel.LocationStatus, "Code", "Name");
                ViewBag.OwnerStatus = new SelectList(dropmodel.OwnerStatus, "Code", "Name");
                ViewBag.Purpose = new SelectList(dropmodel.Purpose, "Code", "Name");

                return PartialView("_HouseLoanRepayList", model);
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
        public async Task<IActionResult> DeleteHouseLoanRepayDetails(Guid EmployeeGI,int AssessmentYear, int TrnCode)
        {

            var response = await _InvestmentService.DeleteHouseLoanRepayDetails(TrnCode, EmployeeGI, AssessmentYear);
            if (response.Success == true)
            {

                response.Message = "Deleted Successfully";
                return Json(response);

            }
            return Json(response);
        }
        #endregion
        #region LET OUT HOUSE PROPERTY

        public async Task<IActionResult> LetOutHouseCreate(Guid EmployeeGI,int AssessmentYear, int? TrnCode)
        {

            InvestmentDeclaration model = new InvestmentDeclaration();
            InvestmentDeclarationDropdown dropmodel = new InvestmentDeclarationDropdown();
            dropmodel = await _InvestmentService.GetTDSDropDown(AssessmentYear);
            model.SliderName = "Save";
            model.EmployeeGuid = EmployeeGI;

            if (TrnCode != null)
            {
                model.LetOutDetails = await _InvestmentService.GetLetOutDetails(TrnCode, EmployeeGI, AssessmentYear);
                model.SliderName = "Modify";
            }


            ViewBag.OwnerStatus = new SelectList(dropmodel.OwnerStatus, "Code", "Name");
            ViewBag.Purpose = new SelectList(dropmodel.Purpose, "Code", "Name");



            return PartialView("_LetOutHousePropertyCreate", model);

        }
        [HttpPost]
        public async Task<IActionResult> LetOutHouseSave(int TrnCode, string HAddress,  int OwnerStatus, decimal Percentage, int Purpose, DateTime LDate, DateTime CDate, DateTime LetOutDate, decimal RentalIncome, decimal MunicipalTax, decimal PrincipleAmt, decimal Interest, decimal EMIInterest, decimal TotalInterest, string Bank, string PAN, string BankAddress, string Remarks, string Tabindex,int AssessmentYear, Guid EmpGI)
        {
            InvestmentDeclaration model = new InvestmentDeclaration();

            model.EmployeeGuid = EmpGI;
            model.TabIndex = Tabindex;
            model.LetOutDetails = new LetOutDetails();
  
            model.LetOutDetails.OwnerStatus = new Dropdown();
            model.LetOutDetails.Purpose = new Dropdown();

            model.LetOutDetails.TrnCode = TrnCode;
            model.LetOutDetails.HouseAddress = HAddress;
            model.LetOutDetails.OwnerStatus.Code = OwnerStatus;
            model.LetOutDetails.Percentage = Percentage;
            model.LetOutDetails.Purpose.Code = Purpose;
            model.LetOutDetails.DateofLoan = LDate;
            model.LetOutDetails.DateofCompletion = CDate;
            model.LetOutDetails.DateofLetOut = LetOutDate;
            model.LetOutDetails.RentalIncome = RentalIncome;
            model.LetOutDetails.MunicipalTax = MunicipalTax;
            model.LetOutDetails.PrincipalAmt = PrincipleAmt;
            model.LetOutDetails.Interest = Interest;
            model.LetOutDetails.PreEMIInterest = EMIInterest;
            model.LetOutDetails.TotalInterest = TotalInterest;
            model.LetOutDetails.NameofBank = Bank;
            model.LetOutDetails.PANofBank = PAN;
            model.LetOutDetails.BankAddress = BankAddress;
            model.LetOutDetails.Remarks = Remarks;
            model.AssessmentYear = new MasterBase();
            model.AssessmentYear.Code = AssessmentYear;
            var response = await _InvestmentService.SaveInvestmentDeclaration(model);
            if (response.Success == true)
            {
                if (TrnCode > 0)
                {
                    response.Message = "Modified Successfully";
                    return Json(response);
                }
                else
                {
                    response.Message = "Saved Successfully";
                    return Json(response);
                }
            }
            return Json(response);
        }
        public async Task<IActionResult> GetLetOutHouseList(Guid EmployeeGI, int AssessmentYear)
        {
            try
            {
                InvestmentDeclaration model = new InvestmentDeclaration();
                model.LetOutDetails = new LetOutDetails();
                InvestmentDeclarationDropdown dropmodel = new InvestmentDeclarationDropdown();
                dropmodel = await _InvestmentService.GetTDSDropDown(AssessmentYear);
                Guid EmpGI = EmployeeGI;
               
                model = await _InvestmentService.GetInvestmentDeclarationDetails(EmpGI, AssessmentYear);

          
                ViewBag.OwnerStatus = new SelectList(dropmodel.OwnerStatus, "Code", "Name");
                ViewBag.Purpose = new SelectList(dropmodel.Purpose, "Code", "Name");

                return PartialView("_LetOutHousePropertyList", model);
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
        public async Task<IActionResult> DeleteLetOutDetails(Guid EmployeeGI, int TrnCode,int AssessmentYear)
        {

            var response = await _InvestmentService.DeleteLetOutDetails(TrnCode, EmployeeGI, AssessmentYear);
            if (response.Success == true)
            {

                response.Message = "Deleted Successfully";
                return Json(response);

            }
            return Json(response);
        }

        #endregion
        #region OTHER INCOME
        public async Task<IActionResult> OtherIncomeCreate(Guid EmployeeGI, int AssessmentYear, int? TrnCode)
        {

            InvestmentDeclaration model = new InvestmentDeclaration();
            model.SliderName = "Save";
            model.EmployeeGuid = EmployeeGI;

            if (TrnCode != null)
            {
                model.OtherIncomeDetails = await _InvestmentService.GetOtherIncomeDetails(TrnCode, EmployeeGI, AssessmentYear);
                model.SliderName = "Modify";
            }


            return PartialView("_OtherIncomeCreate", model);

        }
        [HttpPost]
        public async Task<IActionResult> OtherIncomeSave(int TrnCode, string NatureofIncome, decimal Amount, decimal tdsortcs, string Tabindex,int AssessmentYear ,Guid EmpGI)
        {
            InvestmentDeclaration model = new InvestmentDeclaration();
            model.EmployeeGuid = EmpGI;
            model.TabIndex = Tabindex;
            model.OtherIncomeDetails = new OtherIncomeDetails();
            model.OtherIncomeDetails.TrnCode = TrnCode;
            model.OtherIncomeDetails.NatureofIncome = NatureofIncome;
            model.OtherIncomeDetails.Amount = Amount;
            model.OtherIncomeDetails.TdsorTcs = tdsortcs;
            model.AssessmentYear = new MasterBase();
            model.AssessmentYear.Code = AssessmentYear;
            var response = await _InvestmentService.SaveInvestmentDeclaration(model);
            if (response.Success == true)
            {
                if (TrnCode > 0)
                {
                    response.Message = "Modified Successfully";
                    return Json(response);
                }
                else
                {
                    response.Message = "Saved Successfully";
                    return Json(response);
                }
            }
            return Json(response);
        }
        public async Task<IActionResult> GetOtherIncomeList(Guid EmployeeGI, int AssessmentYear)
        {
            try
            {
                InvestmentDeclaration model = new InvestmentDeclaration();
                model.OtherIncomeDetails = new OtherIncomeDetails();
                
              
                model = await _InvestmentService.GetInvestmentDeclarationDetails(EmployeeGI, AssessmentYear);


                

                return PartialView("_OtherIncomeList", model);
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
        public async Task<IActionResult> DeleteOtherIncomeDetails(Guid EmployeeGI, int TrnCode,int AssessmentYear)
        {

            var response = await _InvestmentService.DeleteOtherIncomeDetails(TrnCode, EmployeeGI, AssessmentYear);
            if (response.Success == true)
            {

                response.Message = "Deleted Successfully";
                return Json(response);

            }
            return Json(response);
        }
        #endregion
    }
}
