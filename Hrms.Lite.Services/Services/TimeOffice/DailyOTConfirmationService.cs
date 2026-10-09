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
    public class DailyOTConfirmationService : IDailyOTConfirmationService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public DailyOTConfirmationService(IServiceBase httpService)
        {
            _baseURL = "DailyOTConfirmation";
            _httpService = httpService;
        }

        public async Task<OverTimeApproval> GetOverTimeApprovalList(string Date,Filter filter)
        {
            return await _httpService.PostAsync<OverTimeApproval,Filter>($"{_baseURL}/GetOverTimeApprovalList?Date={Date}",filter);
        }        
        public async Task<ResponseEntity> OverTimeApproval(OverTimeApproval model)
        {
            return await _httpService.PostAsync<ResponseEntity, OverTimeApproval>($"{_baseURL}/OverTimeApproval", model);
        }
        public async Task<OverTimeApproval> GetOverTimeApprovedDetails(string Date,Filter filter)
        {
            return await _httpService.PostAsync<OverTimeApproval,Filter>($"{_baseURL}/GetOverTimeApprovedDetails?Date={Date}",filter);
        }
         public async Task<DataSet> GetOverTimeApprovalList_Excel(string Date,Filter filter)
        {
            return await _httpService.PostAsync<DataSet,Filter>($"{_baseURL}/GetOverTimeApprovalList_Excel?Date={Date}",filter);
        }
        public async Task<DataSet> GetOverTimeApprovedDetails_Excel(string Date, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetOverTimeApprovedDetails_Excel?Date={Date}", filter);
        }
    }
}
