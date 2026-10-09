using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
   public interface IAssessmentYearService
    {

        public Task<AssessmentYear> GetList(string TabIndex);

        public Task<AssessmentYear> AssessmentYearDetails(Guid AssessmentYearGI, Guid LogGI, string Type);

        public Task<ResponseEntity> Create(AssessmentYear input);

        public Task<ResponseEntity> Delete(AssessmentYear model);
        public Task<ResponseEntity> ApproveOrReject(AssessmentYear model);


        //public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code);


    }
}
