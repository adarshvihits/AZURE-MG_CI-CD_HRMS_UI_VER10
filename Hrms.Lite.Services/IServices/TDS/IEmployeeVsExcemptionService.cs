using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TDS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.TDS
{
   public interface IEmployeeVsExcemptionService
    {
        public Task<List<EmployeeVsExecempList>> GetEmpVsExcemptionList(int AssessmentYear, int Month, int Excemption, Filter filter);
        public Task<ResponseEntity> SaveEmployeeVsExcemption(EmployeeVsExecempList model, int AssessmentYear, int Month, int Excemption);
        public Task<DataSet> GetEmpVsExcemptionList_Excel(int AssessmentYear, int Month, int Excemption, Filter filter);
    }
}
