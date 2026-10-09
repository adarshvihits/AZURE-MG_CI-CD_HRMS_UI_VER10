
using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Shared.DataBank;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DataBank
{
    public class BulkLeaveApplicablityService : IBulkLeaveApplicablityService
    {

        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public BulkLeaveApplicablityService(IServiceBase httpService)
        {
            _baseURL = "BulkLeaveApplicablity";
            _httpService = httpService;
        }

        async Task<BulkLeaveApplicablity> IBulkLeaveApplicablityService.GetApplicableList(int LeaveTypeCode, int LeaveGroupCode, int radioSelection, Filter filter)
        {
            return await _httpService.PostAsync<BulkLeaveApplicablity, Filter>($"{_baseURL}/GetApplicableList?LeaveTypeCode={LeaveTypeCode}&LeaveGroupCode={LeaveGroupCode}&radioSelection={radioSelection}", filter);
        }

        async Task<DataSet> IBulkLeaveApplicablityService.GetApplicableList_Excel(int LeaveTypeCode, int LeaveGroupCode, int radioSelection, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetApplicableList_Excel?LeaveTypeCode={LeaveTypeCode}&radioSelection={radioSelection}&LeaveGroupCode={LeaveGroupCode}", filter);
        }

        async Task<ResponseEntity<List<BulkLeaveApplicablityList>>> IBulkLeaveApplicablityService.ImportFromExcel(BulkLeaveApplicablity model)
        {
            return await _httpService.PostAsync<ResponseEntity<List<BulkLeaveApplicablityList>>, BulkLeaveApplicablity>($"{_baseURL}/ImportFromExcel", model);
        }

        async Task<ResponseEntity> IBulkLeaveApplicablityService.ApplicabilitySave(BulkLeaveApplicablity model)
        {
            return await _httpService.PostAsync<ResponseEntity, BulkLeaveApplicablity>($"{_baseURL}/ApplicabilitySave", model);

        }
    }
}
