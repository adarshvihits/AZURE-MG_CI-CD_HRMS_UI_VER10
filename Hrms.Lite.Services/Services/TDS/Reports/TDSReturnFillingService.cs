using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.TDS.Reports;
using Hrms.Lite.Shared.TDS;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.TDS.Reports
{
  public  class TDSReturnFillingService: ITDSReturnFillingService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public TDSReturnFillingService(IServiceBase httpService)
        {
            _baseURL = "TDSReturnFilling";
            _httpService = httpService;
        }
        public Task<System.Data.DataSet> GetDetails(TDSReturnFilling model)
        {
            return _httpService.PostAsync<System.Data.DataSet, TDSReturnFilling>($"{_baseURL}/GetTDSReturnFillingData", model);
        }
    }
}
