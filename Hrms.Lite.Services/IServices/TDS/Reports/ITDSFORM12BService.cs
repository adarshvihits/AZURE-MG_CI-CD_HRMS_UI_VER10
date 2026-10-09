using Hrms.Lite.Services.Services.TDS.Reports;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TDS;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.TDS.Reports
{
  public  interface ITDSFORM12BService
    {
        public Task<Form12B> GetEmployeeList(int AssessmentYear, Filter filter);
    }
}
