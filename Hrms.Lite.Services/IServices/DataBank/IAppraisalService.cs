using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank
{
    public interface IAppraisalService
    {
        public Task<ResponseEntity> AppraisalSave(Appraisal model);
        public Task<IList<AppraisalTabs>> GetAppraisalTabs();
        public Task<Appraisal> GetAppraisalList(int Tabindex, int? SearchId,int? AppraisalTypeCode, Filter filter);
        public Task<Appraisal> GetAppraisalAssesmentMode(int AppraisalTypeCode);
        public Task<IList<AppraisalLog>> GetAppraisalLogList(Guid EmployeeGI);
        public Task<AppraisalRequest> GetAppraisalLogDetails(Guid AppraisalTrnGI);
        public Task<AppraisalRequest> GetAppraisalDetails(Guid AppraisalTrnGI);
        public Task<ResponseEntity>AppraisalDelete(Guid AppraisalTrnGI);
        public Task<Appraisal> GetAppraisalEvaluationPeriod();

    }
}
