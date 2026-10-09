using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TDS;

namespace Hrms.Lite.Services.IServices.TDS.Reports
{
  public  interface IForm16Service
    {
        public Task<Form16> GetEmployeeList(int AssessmentYear, Filter filter);
    }
}
