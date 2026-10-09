using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.TimeOffice;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TimeOffice;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.TimeOffice
{
   public class ManualCOFFCreditService : IManualCOFFCreditService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public ManualCOFFCreditService(IServiceBase httpService)
        {
            _baseURL = "ManualCOFFCredit";
            _httpService = httpService;
        }
        public async Task<ManualCOFFCredit> ManualCOFFCreditList(Filter filter, int LeaveGroup)
        {
            return await _httpService.PostAsync<ManualCOFFCredit, Filter>($"{_baseURL}/ManualCOFFCreditList?LeaveGroup={LeaveGroup}", filter);
        }
        public async Task<DataSet> ManualCOFFCreditList_Excel(Filter filter, int LeaveGroup)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/ManualCOFFCreditList_Excel?LeaveGroup={LeaveGroup}", filter);
        }
        public async Task<ManualCOFFCredit> GetEditDetails(Guid EmployeeGI)
        {
            return await _httpService.GetAsync<ManualCOFFCredit>($"{_baseURL}/GetEditDetails?EmployeeGI={EmployeeGI}");
        }
        public async Task<ResponseEntity> EmployeeWiseCreditSave(ManualCOFFCredit model)
        {
            return await _httpService.PostAsync<ResponseEntity, ManualCOFFCredit>($"{_baseURL}/EmployeeWiseCreditSave",model);
        }
    }
}
