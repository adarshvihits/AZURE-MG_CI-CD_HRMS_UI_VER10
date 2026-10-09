using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TDS;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.TDS
{
    public interface ITaxCalculatorService
    {
        public Task<TaxCalculator> GetTaxComparisonDetails(Guid EmployeeGI, int AssessmentYear,int Parameter);
        public Task<TaxCalculator> GetList(int AssessmentYear, Filter filter);
    }
}
