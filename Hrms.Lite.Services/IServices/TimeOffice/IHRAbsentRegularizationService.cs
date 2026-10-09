using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TimeOffice;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.TimeOffice
{
  public  interface IHRAbsentRegularizationService
    {
        Task<ResponseEntity> EmployeeWiseRegularize(HRAbsentRegularization model);
        Task<ResponseEntity> Regularize(HRAbsentRegularization model);
        Task<HRAbsentRegularization> HRAbsentRegularizationList(Filter Filter, int Pgroup, int Month, int Leave);
        Task<DataSet> List_Excel(Filter Filter,  int Pgroup, int Month, int Leave);
        Task<HRAbsentRegularization> EmployeeWiseABDetails( int EmpCode, int Pgroup, int Month, int Leave);
    }
}
