using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
    public interface IInsurancePolicyService
    {
        public Task<InsurancePolicy> GetList(string TabIndex);
        public Task<InsuranceCompany> GetInsuranceCompanyList();
        public Task<ResponseEntity> Create(InsurancePolicy input);
        public Task<ResponseEntity> InsuranceCompanySave(InsuranceCompany input);
        public Task<InsurancePolicy> GetInsurancePolicyDetails(Guid LogGI, Guid InsurancePolicyGI, string Type);
        public Task<ResponseEntity> ApproveOrReject(InsurancePolicy model);
        public Task<ResponseEntity> Delete(InsurancePolicy model);
    }
}
