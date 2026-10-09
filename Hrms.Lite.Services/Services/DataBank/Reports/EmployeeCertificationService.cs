using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DataBank.Reports;
using Hrms.Lite.Shared.DataBank.Reports;
using Hrms.Lite.Shared.General;
using System;
using System.Data;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DataBank.Reports
{
    public class EmployeeCertificationService : IEmployeeCertificationService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public EmployeeCertificationService(IServiceBase httpService)
        {
            _baseURL = "EmployeeCertification";
            _baseURL1 = "Common";
            _httpService = httpService;

        }
      

        async Task<EmployeeCertification> IEmployeeCertificationService.GetList(int CertificateTypecode,int TabIndex, Filter filterdata)
        {
            return await _httpService.PostAsync<EmployeeCertification, Filter>($"{_baseURL}/GetList?CertificateTypecode={CertificateTypecode}&&TabIndex={TabIndex}", filterdata);
        }


        public async Task<DataSet> GetList_Excel(int CertificateTypecode,int TabIndex, Filter Filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetList_Excel?CertificateTypecode={CertificateTypecode}&&TabIndex={TabIndex}", Filter);
        }
        public async Task<DataSet> GetList_Summary(int CertificateTypecode, Filter Filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetList_Summary?CertificateTypecode={CertificateTypecode}", Filter);
        }
    }
}
