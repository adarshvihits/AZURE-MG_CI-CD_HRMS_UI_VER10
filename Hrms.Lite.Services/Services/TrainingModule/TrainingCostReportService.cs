using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.TrainingModule;
using Hrms.Lite.Shared.TrainingModule;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.TrainingModule
{
  public class TrainingCostReportService : ITrainingCostReportService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public TrainingCostReportService(IServiceBase httpService)
        {
            _baseURL = "TrainingCostReport";
            _httpService = httpService;
        }
      
        public async Task<TrainingCostReport> GetReportList(int FYCode, int Category)
        {
            return await _httpService.GetAsync<TrainingCostReport>($"{_baseURL}/GetReportList?FYCode={FYCode}&Category={Category}");
        }
    }
}
