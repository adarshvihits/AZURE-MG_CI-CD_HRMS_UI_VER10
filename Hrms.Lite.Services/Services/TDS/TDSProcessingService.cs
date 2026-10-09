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
  public  class TDSProcessingService: ITDSProcessingService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public TDSProcessingService(IServiceBase httpService)
        {
            _baseURL = "TDSProcessing";
            _httpService = httpService;
        }
        
        public async Task<ResponseEntity> TDSProcessing(TDSProcessing Model)
        {
            return await _httpService.PostAsync<ResponseEntity, TDSProcessing>($"{_baseURL}/TDSProcessing", Model);
        }
        public async Task<TDSProcessing> TDSConfirmDetails(int AssessmentYear,int SalaryPeriodCode,int PayrollGroupCode)
        {
            return await _httpService.GetAsync<TDSProcessing>($"{_baseURL}/TDSConfirmDetails?AssessmentYear={AssessmentYear}&SalaryPeriodCode={SalaryPeriodCode}&PayrollGroupCode={PayrollGroupCode}");
        }
        public async Task<ResponseEntity> TDSConfirmation(TDSProcessing model)
        {
            return await _httpService.PostAsync<ResponseEntity, TDSProcessing>($"{_baseURL}/TDSConfirmation", model);
        }
        public async Task<DataSet> TDSConfirmDetails_Export(int AssessmentYear, int SalaryPeriodCode, int PayrollGroupCode)
        {
            return await _httpService.GetAsync<DataSet>($"{_baseURL}/TDSConfirmDetails_Export?AssessmentYear={AssessmentYear}&SalaryPeriodCode={SalaryPeriodCode}&PayrollGroupCode={PayrollGroupCode}");
        }
        public async Task<System.Data.DataSet> TDSDetailRptExportToExcel(int AssessmentYearCode, int SalaryPeriodCode,int PayrollGroupCode)
        {
            return await _httpService.GetAsync<System.Data.DataSet>($"{_baseURL}/TDSDetailRptExportToExcel?AssessmentYear={AssessmentYearCode}&SalaryPeriodCode={SalaryPeriodCode}&PayrollGroupCode={PayrollGroupCode}");
        }
       
        public async Task<TDSEmployeeDetails> GetTDSEmployeeDetails(Guid EmployeeGI,int SalaryPeriodCode)
        {
            return await _httpService.GetAsync<TDSEmployeeDetails>($"{_baseURL}/GetTDSEmployeeDetails?EmployeeGI={EmployeeGI}&SalaryPeriodCode={SalaryPeriodCode}");
        }
    }
}
