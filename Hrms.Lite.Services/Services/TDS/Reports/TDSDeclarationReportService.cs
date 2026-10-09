using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.TDS.Reports;
using Hrms.Lite.Shared.TDS;
using System.Threading.Tasks;
using System.Data;
using Hrms.Lite.Shared.General;

namespace Hrms.Lite.Services.Services.TDS.Reports
{
  public  class TDSDeclarationReportService : ITDSDeclarationReportService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public TDSDeclarationReportService(IServiceBase httpService)
        {
            _baseURL = "TDSDeclarationReport";
            _httpService = httpService;
        }

        public async Task<TDSDeclarationReport> GetTDSDeclarationReport(int AssessmentYear)
        {
            return await _httpService.GetAsync<TDSDeclarationReport>($"{_baseURL}/GetTDSDeclarationReport?AssessmentYear={AssessmentYear}");
        }
        public async Task<DataSet> GetTDSDetailedReport(int CategoryCode,int AssessmentYear)
        {
            return await _httpService.GetAsync<DataSet> ($"{_baseURL}/GetTDSDetailedReport?CategoryCode={CategoryCode}&AssessmentYear={AssessmentYear}");
        }
        public async Task<DataSet> GetTDSReport_Export(Filter filter, int AssessmentYear)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetTDSReport_Export?AssessmentYear={AssessmentYear}", filter);
        }
    }
}
