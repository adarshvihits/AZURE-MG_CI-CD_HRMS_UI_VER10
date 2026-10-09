using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.PayRoll
{
    public interface IPTConfirmationService
    {
        public Task<PTConfirmation> PTConfirmationList(int Month, int Deduction, Filter Filter);
        public Task<ResponseEntity> EmployeeWisePTConfirmationSave(PTConfirmation model);
        public Task<ResponseEntity> PTConfirmationSave(PTConfirmation model);
        Task<DataSet> PTConfirmationList_Excel(int Month, int Deduction, Filter filter);
        public Task<ResponseEntity<List<PTConfirmation>>> ImportFromExcel(PTConfirmation model);
    }
}
