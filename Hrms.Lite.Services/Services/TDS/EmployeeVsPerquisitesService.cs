using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.TDS;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TDS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
namespace Hrms.Lite.Services.Services.TDS
{

    public  class EmployeeVsPerquisitesService: IEmployeeVsPerquisitesService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public EmployeeVsPerquisitesService(IServiceBase httpService)
        {
            _baseURL = "EmployeeVsPerquisites";
            _httpService = httpService;
        }
        public async Task<List<EmployeeVsPerqList>> GetEmpVsPerquisitesList(int AssessmentYear, int Month, int PerquisiteNature, Filter filter)
        {
            return await _httpService.PostAsync<List<EmployeeVsPerqList>, Filter>($"{_baseURL}/GetEmpVsPerquisitesList?AssessmentYear={AssessmentYear}&Month={Month}&PerquisiteNature={PerquisiteNature}", filter);
        }
        public async Task<ResponseEntity> SaveEmployeeVsPerquisites(EmployeeVsPerqList model, int AssessmentYear, int Month, int PerquisiteNature)
        {
            return await _httpService.PostAsync<ResponseEntity, EmployeeVsPerqList>($"{_baseURL}/SaveEmployeeVsPerquisites?AssessmentYear={AssessmentYear}&Month={Month}&PerquisiteNature={PerquisiteNature}", model);
        }
        public async Task<DataSet> GetEmpVsPerquisitesList_Export(int AssessmentYear, int Month, int PerquisiteNature, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetEmpVsPerquisitesList_Export?AssessmentYear={AssessmentYear}&Month={Month}&PerquisiteNature={PerquisiteNature}", filter);
        }

        public async Task<ResponseEntity<List<EmployeeVsPerqList>>> ImportFromExcel(EmployeeVsPerquisites model)
        {
            return await _httpService.PostAsync<ResponseEntity<List<EmployeeVsPerqList>>, EmployeeVsPerquisites>($"{_baseURL}/ImportFromExcel", model);
        }
    }
}
