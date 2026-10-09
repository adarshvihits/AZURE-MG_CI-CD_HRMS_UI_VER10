using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Shared.DataBank;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DataBank
{
  public  class EmployeeFeedbackService: IEmployeeFeedbackService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public EmployeeFeedbackService(IServiceBase httpService)
        {
            _baseURL = "EmployeeFeedback";
            _baseURL1 = "Common";
            _httpService = httpService;
        }
        public async Task<EmployeeFeedback> GetList(int radiomode)
        {
            return await _httpService.GetAsync<EmployeeFeedback>($"{_baseURL}/List?radiomode={radiomode}");
        }

        public async Task<EmployeeFeedback> GetFeedbackDetails(Guid EmployeeGI,int FeedbackCode)
        {
            return await _httpService.GetAsync<EmployeeFeedback>($"{_baseURL}/GetFeedbackDetails?EmployeeGI={EmployeeGI}&FeedbackCode={FeedbackCode}");

        }
        public async Task<ResponseEntity> GetMasterInactiveValidation(string name, int code)
        {
            return await _httpService.GetAsync<ResponseEntity>($"{_baseURL1}/GetMasterInactiveValidation?Master={name}&Code={code}");
        }

        public Task<ResponseEntity> Create(EmployeeFeedback input)
        {
            return _httpService.PostAsync<ResponseEntity, EmployeeFeedback>($"{_baseURL}/Create", input);
        }
    }
}
