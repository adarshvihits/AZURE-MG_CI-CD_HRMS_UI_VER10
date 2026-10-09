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
  public  class EmployeeVsExcemptionService: IEmployeeVsExcemptionService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public EmployeeVsExcemptionService(IServiceBase httpService)
        {
            _baseURL = "EmployeeVsExcemption";
            _httpService = httpService;
        }
        public async Task<List<EmployeeVsExecempList>> GetEmpVsExcemptionList(int AssessmentYear, int Month, int Excemption, Filter filter)
        {
            return await _httpService.PostAsync<List<EmployeeVsExecempList>, Filter>($"{_baseURL}/GetEmpVsExcemptionList?AssessmentYear={AssessmentYear}&Month={Month}&Excemption={Excemption}", filter);
        }
        public async Task<ResponseEntity> SaveEmployeeVsExcemption(EmployeeVsExecempList model, int AssessmentYear, int Month, int Excemption)
        {
            return await _httpService.PostAsync<ResponseEntity, EmployeeVsExecempList>($"{_baseURL}/SaveEmployeeVsExcemption?AssessmentYear={AssessmentYear}&Month={Month}&Excemption={Excemption}", model);
        }
        public async Task<DataSet> GetEmpVsExcemptionList_Excel(int AssessmentYear, int Month, int Excemption, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetEmpVsExcemptionList_Excel?AssessmentYear={AssessmentYear}&Month={Month}&Excemption={Excemption}", filter);
        }
    }
}
