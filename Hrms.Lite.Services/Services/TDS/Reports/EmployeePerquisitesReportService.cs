
using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.TDS.Reports;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TDS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.TDS.Reports
{
    public class EmployeePerquisitesReportService: IEmployeePerquisitesReportService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public EmployeePerquisitesReportService(IServiceBase httpService)
        {
            _baseURL = "EmployeePerquisitesReport";
            _httpService = httpService;
        }
        public async Task<List<EmployeePerquisitesReport>> GetList(int AssessmentYear, int Month, int PerquisiteNature, Filter filter)
        {
            return await _httpService.PostAsync<List<EmployeePerquisitesReport>, Filter>($"{_baseURL}/GetList?AssessmentYear={AssessmentYear}&Month={Month}&PerquisiteNature={PerquisiteNature}", filter);
        }
        public async Task<DataSet> GetList_Excel(int AssessmentYear, int Month, int PerquisiteNature, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetList_Excel?AssessmentYear={AssessmentYear}&Month={Month}&PerquisiteNature={PerquisiteNature}", filter);
        }
    }
}
