using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TimeOffice;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.TimeOffice
{
   public interface IEmployeeVsMinPresentDaysService
    {
        Task<DataSet> GetEmployeeVsMinPresentDays_Excel(int TabIndex, Filter filter);
        Task<EmployeeVsMinPresentDays> GetEmployeeVsMinPresentDays(int TabIndex, Filter filter);
        Task<ResponseEntity> SaveEmployeeVsMinPresentDays(Guid EmployeeGI, int PresentDays);
    }
  
}
