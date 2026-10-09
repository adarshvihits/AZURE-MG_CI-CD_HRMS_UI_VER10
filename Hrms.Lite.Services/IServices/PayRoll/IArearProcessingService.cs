using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.PayRoll
{
  public  interface IArearProcessingService
    {
        Task<ArearProcessing> ArearProcessing(int Month, int PayrollGroup, Filter filter);
        Task<DataSet> ArearProcessing_Excel(int Month, int PayrollGroup,Filter filter);
        Task<ArearProcessing> ArearProcessingDetails(int Month, int EmployeeCode);
    }
}
