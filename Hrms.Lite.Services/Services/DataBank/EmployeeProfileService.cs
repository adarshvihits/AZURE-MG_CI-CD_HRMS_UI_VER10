using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Shared;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.DataBank.Generic;
using Hrms.Lite.Shared.DataBank.Generic.PersonalDetails;
using Hrms.Lite.Shared.DataBank.Reports;
using Hrms.Lite.Shared.General;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DataBank
{
    public class EmployeeProfileService : IEmployeeProfileService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public EmployeeProfileService(IServiceBase httpService)
        {
            _baseURL = "EmployeeProfile";
            _baseURL1 = "CompensationStructure";
            _httpService = httpService;
        }
        async Task<EmployeeProfile> IEmployeeProfileService.GetEmployeeProfileList(int Tabindex,Filter filter)
        {
            return await _httpService.PostAsync<EmployeeProfile,Filter>($"{_baseURL}/GetEmployeeProfileList?Tabindex={Tabindex}",filter);
        }

      public  async Task<DataSet> GetEmployeeProfileList_Excel(int Tabindex, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetEmployeeProfileList_Excel?Tabindex={Tabindex}", filter);
        }
        public async Task<EmployeeProfile> GetEmployeeProfileDetails(Guid EmployeeGI, int FinancialYear)
        {
            return await _httpService.GetAsync<EmployeeProfile>($"{_baseURL}/GetEmployeeProfileDetails?EmployeeGI={EmployeeGI}&FinancialYear={FinancialYear}");
        }
        async Task<List<Certifications>> IEmployeeProfileService.GetCertificationsList(Guid EmployeeGI, int Tabindex)
        {
            return await _httpService.GetAsync<List<Certifications>>($"{_baseURL}/GetCertificationsList?EmployeeGI={EmployeeGI}&Tabindex={Tabindex}");
        }
        public async Task<EmployeeProfile> GetEmployeeSummary(int GroupBy, Filter filter)
        {
            return await _httpService.PostAsync<EmployeeProfile,Filter>($"{_baseURL}/GetEmployeeProfileSummary?GroupBy={GroupBy}",filter);
        }

        public async Task<CompensationStructureReport> GetCompensationDetails(Guid EmpGI)
        {
            return await _httpService.GetAsync<CompensationStructureReport>($"{_baseURL1}/GetCompensationStructuredeDetails?EmployeeGI={EmpGI}");
        }

        public async Task<File> EmpProfileDocView(int Code, string DocType)
        {
            return await _httpService.GetAsync<File>($"{_baseURL}/EmpProfileDocView?Code={Code}&DocType={DocType}");
        }
        public async Task<List<DocumentList>> GetCertificationDocumentList( int LogCode)
        {
            return await _httpService.GetAsync<List<DocumentList>>($"{_baseURL}/GetCertificationDocumentList?LogCode={LogCode}");
        }
    }
}
