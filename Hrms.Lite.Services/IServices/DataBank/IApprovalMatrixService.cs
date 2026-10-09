using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank
{
    public interface IApprovalMatrixService
    {
        Task<ApprovalMatrix> ApprovalMatrixList( int WorkFlow, Filter model);
        Task<ResponseEntity> ApprovalMatrixSave(ApprovalMatrix model);
        Task<ResponseEntity> ApprovalMatrixUpdate(ApprovalMatrix model);
        Task<ResponseEntity> ApprovalMatrixDelete(Guid ApprovalMatrixGI);
        Task<ResponseEntity> ApprovalMatrixLevelDelete(Guid ApprovalMatrixGI, int Level);
        Task<ApprovalMatrix> GetApprovalMatrixDetails(Guid ApprovalMatrixGI);
        Task<ApprovalMatrix> GetApprovalMatrixEditDetails(Guid ApprovalMatrixGI);
        Task<DataSet> GetList_Excel(int WorkFlow,  Filter model);
        Task<ApprovalMatrix> GetRoleList(ApprovalMatrix model);
        Task<ApprovalMatrix> GetEditRoleList(int? NoOfLevels, Guid ApprovalMatrixGI);
          Task<List<Dropdown>> GetAuthorityEntityDropdown(ApprovalMatrix model);
    }
}
