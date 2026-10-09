using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TDS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.TDS
{
 public   interface IEmployeeVsPerquisitesService
    {
        public Task<List<EmployeeVsPerqList>> GetEmpVsPerquisitesList(int AssessmentYear,int Month,int PerquisiteNature, Filter filter);
        public Task<ResponseEntity> SaveEmployeeVsPerquisites(EmployeeVsPerqList model, int AssessmentYear, int Month, int PerquisiteNature);
        public Task<DataSet> GetEmpVsPerquisitesList_Export(int AssessmentYear, int Month, int PerquisiteNature, Filter filter);
        public Task<ResponseEntity<List<EmployeeVsPerqList>>> ImportFromExcel(EmployeeVsPerquisites model);
    }



}
