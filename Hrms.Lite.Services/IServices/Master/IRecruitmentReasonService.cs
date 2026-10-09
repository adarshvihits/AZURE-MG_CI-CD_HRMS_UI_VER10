using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
    public interface IRecruitmentReasonService
    {
        public Task<RecruitmentReason> GetList(string TabIndex);
        public Task<ResponseEntity> Create(RecruitmentReason input);
        public Task<RecruitmentReason> GetRecruitmentReasonDetails(Guid RecruitmentReasonGI, Guid LogGI, string Type);
        public Task<ResponseEntity> Delete(RecruitmentReason model);
        public Task<ResponseEntity> ApproveOrReject(RecruitmentReason model);

        public Task<DataSet> GetList_Excel(string TabIndex);

    }
}
