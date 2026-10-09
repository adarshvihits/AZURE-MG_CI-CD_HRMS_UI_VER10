using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Hrms.Lite.Shared.TDS;
using Hrms.Lite.Shared.General;

namespace Hrms.Lite.Services.IServices.TDS
{
 public   interface IPreviousEmployerHRMSService
    {

        public Task<PreviousEmployer> GetList(int AssessmentYear, Filter filter);
        public Task<PreviousEmployer> GetPreviousEmployerDetails(Guid EmployeeGI);
        public Task<ResponseEntity> SavePreviousEmployer(PreviousEmployer model);
        public Task<ResponseEntity> DeletePreviousEmployer(Guid EmployeeGI);
    }
}
