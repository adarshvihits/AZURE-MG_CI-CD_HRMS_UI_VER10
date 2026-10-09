using Hrms.Lite.Shared.DataBank.Reports;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank.Reports
{
   public interface IBloodBankReportService
    {
         
        public Task<BloodBankReport> GetBloodBankReport(BloodBankReport model);
        public Task<DataSet> GetBloodBankReport_Excel(BloodBankReport BloodBankReport);
    }
}
