using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.TrainingModule;
using Hrms.Lite.Shared.TrainingModule;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.TrainingModule
{
   public class TrainingCostAllotedReportServices: ITrainingCostAllotedReportServices
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public TrainingCostAllotedReportServices(IServiceBase httpService)
        {
            _baseURL = "TrainingCostAllotedReport";
            _baseURL1 = "Common";
            _httpService = httpService;
        }
        public async Task<TraineeCost> GetList()
        {
            return await _httpService.GetAsync<TraineeCost>($"{_baseURL}/List");
        }
    }
}
