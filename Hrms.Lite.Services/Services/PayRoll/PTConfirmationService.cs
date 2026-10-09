using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.PayRoll;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.PayRoll
{
    public class PTConfirmationService:IPTConfirmationService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public PTConfirmationService(IServiceBase httpService)
        {
            _baseURL = "PTConfirmation";
            _httpService = httpService;
        }
        public async Task<PTConfirmation> PTConfirmationList(int Month, int Deduction, Filter Filter)
        {
            return await _httpService.PostAsync<PTConfirmation, Filter>($"{_baseURL}/PTConfirmationList?Month={Month}&Deduction={Deduction}", Filter);
        }
        public async Task<ResponseEntity> EmployeeWisePTConfirmationSave(PTConfirmation model)
        {
            return await _httpService.PostAsync<ResponseEntity, PTConfirmation>($"{_baseURL}/EmployeeWisePTConfirmationSave", model);
        }
        public async Task<ResponseEntity> PTConfirmationSave(PTConfirmation model)
        {
            return await _httpService.PostAsync<ResponseEntity, PTConfirmation>($"{_baseURL}/PTConfirmationSave", model);
        }

        public async Task<DataSet> PTConfirmationList_Excel(int Month, int Deduction, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/PTConfirmationList_Excel?Month={Month}&Deduction={Deduction}", filter);
        }
        public async Task<ResponseEntity<List<PTConfirmation>>> ImportFromExcel(PTConfirmation model)
        {
            return await _httpService.PostAsync<ResponseEntity<List<PTConfirmation>>, PTConfirmation>($"{_baseURL}/ImportFromExcel", model);

        }
    }
}
