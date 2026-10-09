
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TDS;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.TDS
{
  public  interface IInvestmentDeclarationService
    {
        public Task<InvestmentDeclaration> GetList(int AssessmentYear, Filter filter);
        public Task<InvestmentDeclarationDropdown> GetTDSDropDown(int AssessmentYear);
        public Task<InvestmentDeclaration> GetInvestmentDeclarationDetails(Guid EmployeeGI,int AssessmentYear);
        public Task<ResponseEntity> SaveInvestmentDeclaration(InvestmentDeclaration InvestmentData);
        public Task<ResponseEntity> DeleteLandLordDetails(int TrnCode, Guid EmployeeGI, int AssessmentYear,int TotalRent);
        public Task<LICDetails> GetLICDetails(int? TrnCode, Guid EmployeeGI, int AssessmentYear);
        public Task<ResponseEntity> DeleteLICDetails(int TrnCode, Guid EmployeeGI, int AssessmentYear);
        public Task<DonationDetails> GetDonationDetails(int? TrnCode, Guid EmployeeGI, int AssessmentYear);
        public Task<HLRepayDetails> GetHLRepayDetails(int ? TrnCode, Guid EmployeeGI, int AssessmentYear);
        public Task<ResponseEntity> DeleteHouseLoanRepayDetails(int TrnCode, Guid EmployeeGI, int AssessmentYear);
        public Task<LetOutDetails> GetLetOutDetails(int? TrnCode, Guid EmployeeGI, int AssessmentYear);
        public Task<ResponseEntity> DeleteLetOutDetails(int TrnCode, Guid EmployeeGI, int AssessmentYear);
        public Task<OtherIncomeDetails> GetOtherIncomeDetails(int? TrnCode, Guid EmployeeGI, int AssessmentYear);
        public Task<ResponseEntity> DeleteOtherIncomeDetails(int TrnCode, Guid EmployeeGI, int AssessmentYear);
        public Task<PANDetails> GetEmployeePANNumber(Guid EmployeeGI);

    }
}
