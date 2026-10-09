using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
    public interface IJobDescriptionService
    {
        public Task<JobDescription> GetList(string TabIndex);
        public Task<JobDescription> GetJobDescriptionDetails(Guid JobDescriptionGI, Guid LogGI, string Type);
        public Task<ResponseEntity> JobDescriptionSave(JobDescription model);
        public Task<ResponseEntity> JobDescriptionDelete(JobDescription model);
        public Task<ResponseEntity> ApproveOrReject(JobDescription model);
        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code);
        public Task<ResponseEntity> GetJDVSDesignation(string DesignationCode, int? JobDescriptionCode);
        public Task<JobDescription> GetMasterJobDetails(int PositionPlanCode);
    }
}
