using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
   public interface IFinancialYearService
    {
        public Task<FinancialYear> GetList(string TabIndex);
        public Task<FinancialYear> FinancialYeardetailsList(string fromDate,string ToDate);

        public Task<ResponseEntity> GetFinancialYRProceedValidation(FinancialYear input);
        public Task<ResponseEntity> Save(FinancialYear model);
        public Task<FinancialYear> GetFinancialYearDetails(Guid FinancialYearGI, Guid LogGI, string type);
        public Task<ResponseEntity> ApproveOrReject(FinancialYear model);
        public Task<ResponseEntity> Delete(FinancialYear Input);

        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code);

    }
}
