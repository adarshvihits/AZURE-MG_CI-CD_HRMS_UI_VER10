using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.PayRoll
{
    public interface ISendPayslipService
    {
        public Task<IList<SendPayslip>> GetPaySlipGenerationList(int Month, int PayRollGroup, string Type, Filter filter);
        public Task<DataSet> ExportToExcel(int Month, int PayrollGroup, string Type, Filter filter);
        //Task<ResponseEntity> SendPayslip(SendPayslip model);
      public  void SendPayslip(SendPayslip model);
    }
}
