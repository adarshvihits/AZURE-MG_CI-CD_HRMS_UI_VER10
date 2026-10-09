using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.TDS;
using Hrms.Lite.Shared;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TDS;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.TDS
{
   public class InvestmentDeclarationApprovalService: IInvestmentDeclarationApprovalService
    {
        private readonly string _baseURL;
        private readonly string _baseURL2;
        private readonly IServiceBase _httpService;
        public InvestmentDeclarationApprovalService(IServiceBase httpService)
        {
            _baseURL = "InvestmentDeclaration";
            _baseURL2 = "DeclarationWithProof";
            _httpService = httpService;
        
       
        }

        public async Task<InvestmentDeclaration> GetApprovalList(int AssessmentYear, Filter filter)
        {
            return await _httpService.PostAsync<InvestmentDeclaration, Filter>($"{_baseURL}/GetInvestDeclarationApprovalEmpList?AssessmentYear={AssessmentYear}", filter);
        }
        async Task<InvestmentDeclarationDropdown> IInvestmentDeclarationApprovalService.GetTDSDropDown(int AssessmentYear)
        {
            return await _httpService.GetAsync<InvestmentDeclarationDropdown>($"{_baseURL}/GetInvestmentDeclarationDropDown?AssessmentYear={AssessmentYear}");
        }
        async Task<InvestmentDeclaration> IInvestmentDeclarationApprovalService.GetInvDeclarationApprovalDetails(Guid EmployeeGI, int AssessmentYear)
        {
            return await _httpService.GetAsync<InvestmentDeclaration>($"{_baseURL}/GetInvDeclarationDetailsforApproval?EmployeeGI={EmployeeGI}&AssessmentYear={AssessmentYear}");
        }
        async Task<ResponseEntity> IInvestmentDeclarationApprovalService.SaveInvestmentDeclarationApproval(InvestmentDeclaration InvestmentData)
        {
            return await _httpService.PostAsync<ResponseEntity, InvestmentDeclaration>($"{_baseURL}/SaveInvestmentDeclarationApproval", InvestmentData);
        }
        async Task<List<DocumentList>> IInvestmentDeclarationApprovalService.GetDocumentList(Guid EmployeeGI, int AssessmentYear, string idType, string idRef)
        {
            return await _httpService.GetAsync<List<DocumentList>>($"{_baseURL2}/GetDocumentList?EmployeeGI={EmployeeGI}&AssessmentYear={AssessmentYear}&idType={idType}&idRef={idRef}");
        }
        async Task<File> IInvestmentDeclarationApprovalService.GetDocumentForView(int TrnCode)
        {
            return await _httpService.GetAsync<File>($"{_baseURL2}/GetDocumentForView?TrnCode={TrnCode}");
        }
    }
}
