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
    public class SendPayslipService: ISendPayslipService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public SendPayslipService(IServiceBase httpService)
        {
            _baseURL = "SendPayslip";
            _httpService = httpService;
        }

        public async Task<IList<SendPayslip>> GetPaySlipGenerationList(int Month, int PayRollGroup, string Type, Filter filter)
        {
            return await _httpService.PostAsync<IList<SendPayslip>, Filter>($"{_baseURL}/SendPayslipList?SalaryPeriod={Month}&PayRollGroup={PayRollGroup}&Type={Type}", filter);
        }
        //public Task<ResponseEntity> SendPayslip(SendPayslip model)
        //{
        //    return _httpService.PostAsync<ResponseEntity, SendPayslip>($"{_baseURL}/SendPayslip", model);
        //}
        public void SendPayslip(SendPayslip model)
        {
            _httpService.PostAsync<SendPayslip>($"{_baseURL}/SendPayslip", model);
        }
        public async Task<DataSet> ExportToExcel(int Month, int PayrollGroup, string Type, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/SendPayslipList_Excel?SalaryPeriod={Month}&PayRollGroup={PayrollGroup}&Type={Type}", filter);
        }
    }
}
