using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.PayRoll
{
    public interface IIncomeTaxRatesService
    {
        Task<IncomeTaxRates> IncomeTaxRatesList(int AssessmentYear, int ITCategory, int BasedOn);
        Task<ResponseEntity> IncomeTaxRates_Save(IncomeTaxRates input);
    }
}
