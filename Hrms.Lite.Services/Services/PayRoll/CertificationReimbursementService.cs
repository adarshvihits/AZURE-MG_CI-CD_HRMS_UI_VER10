using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.PayRoll;
using Hrms.Lite.Shared.PayRoll;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.PayRoll
{
    public class CertificationReimbursementService: ICertificationReimbursementService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public CertificationReimbursementService(IServiceBase httpService)
        {
            _baseURL = "CertificationReimbursement";
            _httpService = httpService;
        }
        public async Task<CertificationReimbursement> GetCertificationReimbursementList(int Month, int TabIndex, Filter Filter)
        {
            return await _httpService.PostAsync<CertificationReimbursement, Filter>($"{_baseURL}/CertificationReimbursementList?Month={Month}&TabIndex={TabIndex}", Filter);
        }
        public async Task<ResponseEntity> EmployeeWiseCertificationReimbursementSave(CertificationReimbursement model,int Month)
        {
            return await _httpService.PostAsync<ResponseEntity, CertificationReimbursement>($"{_baseURL}/EmployeeWiseCertificationReimbursementSave?Month={Month}", model);
        }
        public async Task<DataSet> GetCertificationReimbursementList_Excel(int Month, int TabIndex, Filter Filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetCertificationReimbursementList_Excel?Month={Month}&TabIndex={TabIndex}", Filter);
        }
    }
}
