using Hrms.Lite.Services.Services.TDS.Reports;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Hrms.Lite.Shared.TDS;

namespace Hrms.Lite.Services.IServices.TDS.Reports
{
  public  interface IFORM12BBService
    {
        public Task<Form12B> GetEmployeeList(int AssessmentYear, Filter filter);
    }
}
