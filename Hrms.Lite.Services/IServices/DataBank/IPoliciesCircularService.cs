using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank
{
    public interface IPoliciesCircularService
    {
        public Task<PoliciesCirculars> GetList();
        public Task<PoliciesCirculars> GetPolicyDetails(Guid PolicyGI);
        Task<PoliciesCirculars> GetCategoryDtls();
        Task<PoliciesCirculars> GetCombinationDetails(int id);
        Task<ResponseEntity> CategoryResultSave(List<PolicyCategory> input);
        public Task<ResponseEntity> PolicySave(PoliciesCirculars model);
        public Task<ResponseEntity> PolicyUpdate(PoliciesCirculars model);
        public Task<ResponseEntity> Delete(PoliciesCirculars input);
        public Task<ResponseEntity> DeleteCategory(PolicyCategory input);
        Task<File> DownloadDoc(Guid PolicyGI);
    }
}
