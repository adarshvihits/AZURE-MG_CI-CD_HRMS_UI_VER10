using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Hrms.Lite.Services.Services.DataBank
{
   public class InterviewSchedulingService: IInterviewSchedulingService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public InterviewSchedulingService(IServiceBase httpService)
        {
            _baseURL = "InterviewScheduling";
            _httpService = httpService;
        }

        public async Task<ApplicantTracking> GetInterviewSchedulingList(int Tabindex)
        {
            return await _httpService.GetAsync<ApplicantTracking>($"{_baseURL}/GetInterviewSchedulingList?Tabindex={Tabindex}");
        }
        public async Task<DataSet> GetInterviewSchedulingList_Excel( int Tabindex)
        {
            return await _httpService.GetAsync<DataSet>($"{_baseURL}/GetInterviewSchedulingList_Excel?Tabindex={Tabindex}");
        }
     
        public async Task<ResponseEntity> EmployeeWiseInterviewScheduling(ApplicantTracking model)
        {
            return await _httpService.PostAsync<ResponseEntity, ApplicantTracking>($"{_baseURL}/EmployeeWiseInterviewScheduling", model);
        }
    }
}
