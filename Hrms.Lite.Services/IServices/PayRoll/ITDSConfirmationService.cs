using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.PayRoll
{
    public interface ITDSConfirmationService
    {
        public Task<TDSConfirmation> GetTDSConfirmationList(int Month, int Deduction, Filter filter);
        public Task<ResponseEntity> EmployeeWiseTDSConfirmationSave(TDSConfirmation model);
        Task<DataSet> TDSConfirmationList_Excel(int Month, int Deduction, Filter filter);
        public Task<ResponseEntity<List<TDSConfirmation>>> ImportFromExcel(TDSConfirmation model);


    }
}
