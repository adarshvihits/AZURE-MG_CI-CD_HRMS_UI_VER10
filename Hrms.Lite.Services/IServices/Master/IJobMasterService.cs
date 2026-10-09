using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
    public interface IJobMasterService
    {
        public Task<JobMaster> GetList(int DepartmentCode, string Status);
        public Task<ResponseEntity> Create(JobMaster Input);
        public Task<JobMaster> GetJobMasterDetails(Guid JobGI, Guid LogGI, string Type);
        public Task<ResponseEntity> ApproveOrReject(JobMaster Input);
    }
}
