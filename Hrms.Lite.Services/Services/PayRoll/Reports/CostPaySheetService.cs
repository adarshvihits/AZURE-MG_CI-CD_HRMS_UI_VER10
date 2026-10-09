using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.PayRoll.Reports;
using Hrms.Lite.Shared.PayRoll.Reports;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.PayRoll.Reports
{
    public class CostPaySheetService:ICostPaySheetService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public CostPaySheetService(IServiceBase httpService)
        {
            _baseURL = "CostPaySheet";
            _httpService = httpService;
        }

        public Task<System.Data.DataSet> GetDetails(int SalaryPeriod,int? CostCenetrerCode,CostPaySheet model)
        {
            return _httpService.PostAsync<System.Data.DataSet, CostPaySheet>($"{_baseURL}/GetCostPaySheet?SalaryPeriodCode={SalaryPeriod}&CostCenterCode={CostCenetrerCode}", model);
        }

        public Task<System.Data.DataSet> GetPaySheetDraftDetails(int SalaryPeriod, CostPaySheet model)
        {
            return _httpService.PostAsync<System.Data.DataSet, CostPaySheet>($"{_baseURL}/GetPaySheetDraft?SalaryPeriodCode={SalaryPeriod}", model);
        }
    }
}
