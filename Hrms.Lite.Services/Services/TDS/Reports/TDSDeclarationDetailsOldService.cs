using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.TDS.Reports;
using Hrms.Lite.Shared;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TDS;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.TDS.Reports
{
  public class TDSDeclarationDetailsOldService: ITDSDeclarationDetailsOldService
    {
        private readonly string _baseURL;
        private readonly string _baseURL2;
        private readonly IServiceBase _httpService;
        public TDSDeclarationDetailsOldService(IServiceBase httpService)
        {
            _baseURL = "InvestmentDeclaration";
            _baseURL2 = "DeclarationWithProof";
            _httpService = httpService;


        }
        public async Task<InvestmentDeclaration> GetEmployeeList(int AssessmentYear, Filter filter)
        {
            return await _httpService.PostAsync<InvestmentDeclaration, Filter>($"{_baseURL}/GetInvestDeclarationEmpListOld?AssessmentYear={AssessmentYear}", filter);
        }
        async Task<InvestmentDeclarationDropdown> ITDSDeclarationDetailsOldService.GetTDSDropDown(int AssessmentYear)
        {
            return await _httpService.GetAsync<InvestmentDeclarationDropdown>($"{_baseURL}/GetInvestmentDeclarationDropDown?AssessmentYear={AssessmentYear}");
        }
        async Task<InvestmentDeclaration> ITDSDeclarationDetailsOldService.GetInvestmentDeclarationDetails(Guid EmployeeGI, int AssessmentYear)
        {
            return await _httpService.GetAsync<InvestmentDeclaration>($"{_baseURL}/GetInvestmentDeclarationDetailsOld?EmployeeGI={EmployeeGI}&AssessmentYear={AssessmentYear}");
        }
        async Task<List<DocumentList>> ITDSDeclarationDetailsOldService.GetDocumentList(Guid EmployeeGI, int AssessmentYear, string idType, string idRef)
        {
            return await _httpService.GetAsync<List<DocumentList>>($"{_baseURL}/GetDocumentListOld?EmployeeGI={EmployeeGI}&AssessmentYear={AssessmentYear}&idType={idType}&idRef={idRef}");
        }
        async Task<File> ITDSDeclarationDetailsOldService.GetDocumentForView(int TrnCode)
        {
            return await _httpService.GetAsync<File>($"{_baseURL}/GetOldDocumentForView?TrnCode={TrnCode}");
        }
    }
}
