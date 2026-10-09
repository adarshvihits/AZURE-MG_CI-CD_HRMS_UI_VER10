
using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.TDS;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TDS;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.TDS
{
  public  class InvestmentDeclarationService: IInvestmentDeclarationService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public InvestmentDeclarationService(IServiceBase httpService)
        {
            _baseURL = "InvestmentDeclaration";
            _httpService = httpService;
        }
        public async Task<InvestmentDeclaration> GetList(int AssessmentYear, Filter filter)
        {
            return await _httpService.PostAsync<InvestmentDeclaration, Filter>($"{_baseURL}/GetInvestDeclarationEmpList?AssessmentYear={AssessmentYear}", filter);
        }
        async Task<InvestmentDeclarationDropdown> IInvestmentDeclarationService.GetTDSDropDown(int AssessmentYear)
        {
            return await _httpService.GetAsync<InvestmentDeclarationDropdown>($"{_baseURL}/GetInvestmentDeclarationDropDown?AssessmentYear={AssessmentYear}");
        }
        async Task<InvestmentDeclaration> IInvestmentDeclarationService.GetInvestmentDeclarationDetails(Guid EmployeeGI, int AssessmentYear)
        {
            return await _httpService.GetAsync<InvestmentDeclaration>($"{_baseURL}/GetInvestmentDeclarationDetails?EmployeeGI={EmployeeGI}&AssessmentYear={AssessmentYear}");
        }
        async Task<ResponseEntity> IInvestmentDeclarationService.SaveInvestmentDeclaration(InvestmentDeclaration InvestmentData)
        {
            return await _httpService.PostAsync<ResponseEntity, InvestmentDeclaration>($"{_baseURL}/SaveInvestmentDeclaration", InvestmentData);
        }
        async Task<ResponseEntity> IInvestmentDeclarationService.DeleteLandLordDetails(int TrnCode, Guid EmployeeGI, int AssessmentYear,int TotalRent)
        {
            return await _httpService.DeleteAsync<ResponseEntity>($"{_baseURL}/DeleteLandLordDetails?TrnCode={TrnCode}&EmployeeGI={EmployeeGI}&AssessmentYear={AssessmentYear}&TotalRent={TotalRent}");
        }
        async Task<LICDetails> IInvestmentDeclarationService.GetLICDetails(int? TrnCode, Guid EmployeeGI, int AssessmentYear)
        {
            return await _httpService.GetAsync<LICDetails>($"{_baseURL}/GetLICDetails?TrnCode={TrnCode}&EmployeeGI={EmployeeGI}&AssessmentYear={AssessmentYear}");
        }
        async Task<ResponseEntity> IInvestmentDeclarationService.DeleteLICDetails(int TrnCode, Guid EmployeeGI, int AssessmentYear)
        {
            return await _httpService.DeleteAsync<ResponseEntity>($"{_baseURL}/DeleteLICDetails?TrnCode={TrnCode}&EmployeeGI={EmployeeGI}&AssessmentYear={AssessmentYear}");
        }
      
        async Task<DonationDetails> IInvestmentDeclarationService.GetDonationDetails(int? TrnCode, Guid EmployeeGI, int AssessmentYear)
        {
            return await _httpService.GetAsync<DonationDetails>($"{_baseURL}/GetDonationDetails?TrnCode={TrnCode}&EmployeeGI={EmployeeGI}&AssessmentYear={AssessmentYear}");
        }
        async Task<HLRepayDetails> IInvestmentDeclarationService.GetHLRepayDetails( int ?TrnCode, Guid EmployeeGI, int AssessmentYear)
        {
            return await _httpService.GetAsync<HLRepayDetails>($"{_baseURL}/GetHLRepayDetails?TrnCode={TrnCode}&EmployeeGI={EmployeeGI}&AssessmentYear={AssessmentYear}");
        }
        async Task<ResponseEntity> IInvestmentDeclarationService.DeleteHouseLoanRepayDetails(int TrnCode, Guid EmployeeGI, int AssessmentYear)
        {
            return await _httpService.DeleteAsync<ResponseEntity>($"{_baseURL}/DeleteHouseLoanRepayDetails?TrnCode={TrnCode}&EmployeeGI={EmployeeGI}&AssessmentYear={AssessmentYear}");
        }
        async Task<LetOutDetails> IInvestmentDeclarationService.GetLetOutDetails(int? TrnCode, Guid EmployeeGI, int AssessmentYear)
        {
            return await _httpService.GetAsync<LetOutDetails>($"{_baseURL}/GetLetOutDetails?TrnCode={TrnCode}&EmployeeGI={EmployeeGI}&AssessmentYear={AssessmentYear}");
        }
        async Task<ResponseEntity> IInvestmentDeclarationService.DeleteLetOutDetails(int TrnCode, Guid EmployeeGI, int AssessmentYear)
        {
            return await _httpService.DeleteAsync<ResponseEntity>($"{_baseURL}/DeleteLetOutDetails?TrnCode={TrnCode}&EmployeeGI={EmployeeGI}&AssessmentYear={AssessmentYear}");
        }
        async Task<OtherIncomeDetails> IInvestmentDeclarationService.GetOtherIncomeDetails(int? TrnCode, Guid EmployeeGI, int AssessmentYear)
        {
            return await _httpService.GetAsync<OtherIncomeDetails>($"{_baseURL}/GetOtherIncomeDetails?TrnCode={TrnCode}&EmployeeGI={EmployeeGI}&AssessmentYear={AssessmentYear}");
        }
        async Task<ResponseEntity> IInvestmentDeclarationService.DeleteOtherIncomeDetails(int TrnCode, Guid EmployeeGI, int AssessmentYear)
        {
            return await _httpService.DeleteAsync<ResponseEntity>($"{_baseURL}/DeleteOtherIncomeDetails?TrnCode={TrnCode}&EmployeeGI={EmployeeGI}&AssessmentYear={AssessmentYear}");
        }

        async Task<PANDetails> IInvestmentDeclarationService.GetEmployeePANNumber(Guid EmployeeGI)
        {
            return await _httpService.GetAsync<PANDetails>($"{_baseURL}/GetEmployeePANNumber?EmployeeGI={EmployeeGI}");
        }
    }
}
