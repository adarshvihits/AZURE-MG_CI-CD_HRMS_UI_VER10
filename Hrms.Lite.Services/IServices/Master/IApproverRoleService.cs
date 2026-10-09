using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
    public interface IApproverRoleService
    {
        Task<ApproverRole> GetList(string TabIndex, int ClassificationType, int country);
        Task<ApproverRole> GetApproverRoleDetails(int AuthorityEntityCode, string Type);
        Task<ResponseEntity> Create(ApproverRole input);
        Task<ResponseEntity> Delete(ApproverRole model);
        Task<ResponseEntity> GetMasterInactiveValidation(string name, int code);
        Task<ApproverRole> GetApplicabilityDropdown(int? RoleCode, int? Country);
        Task<List<Dropdown>> GetEditApplicabilityDropdown(int? RoleCode, int? Country);
    }
}
