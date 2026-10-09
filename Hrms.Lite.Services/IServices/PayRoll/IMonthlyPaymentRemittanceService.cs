using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.PayRoll
{
    public interface IMonthlyPaymentRemittanceService
    {
        public Task<MonthlyPaymentRemittance> GetList(string TabIndex);

        public Task<ResponseEntity> Create(string TabIndex, MonthlyPaymentRemittance input);

        public Task<DataSet> MonthlyPay_Excel(string TabIndex, Filter filter);
    }
}
