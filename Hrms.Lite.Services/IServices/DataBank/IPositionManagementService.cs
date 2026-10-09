using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank
{
   public interface IPositionManagementService
    {
        public Task<PositionManagement> PositionManagementInfo();
        public Task<PositionManagement> GetPositionManagementDetails(string type,Filter filter);
        public Task<PositionManagement> GetPositionApprovalDetails();
        public Task<ResponseEntity> GetPositionProceedValidation(PositionManagement input);
        public Task<PositionManagement> GetPositionDetailsList(int LocationCode, int DepartmentCode, int Designation, int NoofPosition);
        public Task<ResponseEntity> Save(PositionManagement model);
        public Task<PositionManagement> GetPositionDetails(Guid PositionGI, int PositionCode, string Type);
        public Task<ResponseEntity> Delete(Guid GI);
        public Task<ResponseEntity> Approve(PositionManagement model);
        public Task<DataSet> GetPositionManagementDetails_Excel(string type, Filter filter);
    }
}

