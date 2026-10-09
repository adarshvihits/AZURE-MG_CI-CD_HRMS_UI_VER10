using Hrms.Lite.Shared.PayRoll;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.PayRoll
{
    public interface ICertificationReimbursementService
    {
        public Task<CertificationReimbursement> GetCertificationReimbursementList(int Month, int TabIndex, Filter Filter);
        public Task<ResponseEntity> EmployeeWiseCertificationReimbursementSave(CertificationReimbursement model,int Month);
        public Task<DataSet> GetCertificationReimbursementList_Excel(int Month, int TabIndex, Filter Filter);
    }
}
