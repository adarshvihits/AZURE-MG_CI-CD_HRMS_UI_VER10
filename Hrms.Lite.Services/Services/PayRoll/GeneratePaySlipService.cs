using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.PayRoll;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.PayRoll
{
    public class GeneratePaySlipService: IGeneratePaySlipService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public GeneratePaySlipService(IServiceBase httpService)
        {
            _baseURL = "PaySlipGeneration";
            _httpService = httpService;
        }
        public async Task<IList<PaySlipGeneration>> GetPaySlipGenerationList(int Month, int PayRollGroup,string Type, Filter filter)
        {
            return await _httpService.PostAsync<IList<PaySlipGeneration>, Filter>($"{_baseURL}/GetPaySlipGenerationList?SalaryPeriod={Month}&PayRollGroup={PayRollGroup}&Type={Type}", filter);

        }

        public async Task<IList<PaySlipGeneration>> GetPayslipAllEmployees(PaySlipGeneration model)
        {
            return await _httpService.PostAsync<IList<PaySlipGeneration>, PaySlipGeneration>($"{_baseURL}/GetPayslipAllEmployees", model);

        }

        public Task<ResponseEntity> SavePayslipUploaded(PaySlipGeneration model,string FileUploadMode)
        {
            return _httpService.PostAsync<ResponseEntity, PaySlipGeneration>($"{_baseURL}/SavePayslipUploaded?FileUploadMode={FileUploadMode}", model);
        }
        public async Task<HttpResponseMessage> GeneratePaySlip(string FileUploadMode,PaySlipGeneration model)
        {
              return await _httpService.PostAsync<PaySlipGeneration>($"{_baseURL}/GeneratePaySlip?FileUploadMode={FileUploadMode}", model);

        }
      
        public async Task<DataTable> GetPayslipGenerationMode()
        {
            return await _httpService.GetAsync<DataTable>($"{_baseURL}/GetPayslipGenerationMode");
        }

        public async Task<DataSet> ExportToExcel(int Month, int PayrollGroup, string Type,Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetPaySlipGenerationList_Excel?SalaryPeriod={Month}&PayRollGroup={PayrollGroup}&Type={Type}", filter);
        }

        public async Task<PaySlipGeneratedStatus> GetPaySlipGeneratedStatus(int SalaryPeriod, int PayRollGroup)
        {
            return await _httpService.GetAsync<PaySlipGeneratedStatus>($"{_baseURL}/GetPaySlipGeneratedStatus?SalaryPeriod={SalaryPeriod}&PayRollGroup={PayRollGroup}");
        }

    }
}

