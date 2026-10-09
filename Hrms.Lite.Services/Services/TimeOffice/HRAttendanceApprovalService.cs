using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.TimeOffice;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using Hrms.Lite.Shared.TimeOffice;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.TimeOffice
{
    public class HRAttendanceApprovalService : IHRAttendanceApprovalService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public HRAttendanceApprovalService(IServiceBase httpService)
        {
            _baseURL = "HRAttendanceApproval";
            _httpService = httpService;
        }
        public async Task<HRAttendanceApproval> HRAttendanceApprovalList(string TabIndex, int LeavePeriodCode, int Level, int Month, int AuthEmpCode,int PayrollCode, Filter filter)
        {
            return await _httpService.PostAsync<HRAttendanceApproval, Filter>($"{_baseURL}/HRAttendanceApprovalList?TabIndex={TabIndex}&LeavePeriodCode={LeavePeriodCode}&Level={Level}&Month={Month}&AuthEmpCode={AuthEmpCode}&PayrollCode={PayrollCode}", filter);
        }
       
        public async Task<ResponseEntity> HRAttendanceApprove(HRAttendanceApproval model)
        {
            return await _httpService.PostAsync<ResponseEntity, HRAttendanceApproval > ($"{_baseURL}/HRAttendanceApprove",model);
        }

        public async Task<ResponseEntity> HRAttendanceReject(HRAttendanceApproval model)
        {
            return await _httpService.PostAsync<ResponseEntity, HRAttendanceApproval>($"{_baseURL}/HRAttendanceReject", model);
        }

        public async Task<DataSet> HRAttendanceApprovalList_Excel(string TabIndex, int LeavePeriodCode, int Level, int Month, int PayrollCode, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/HRAttendanceApprovalList_Excel?TabIndex={TabIndex}&LeavePeriodCode={LeavePeriodCode}&Level={Level}&Month={Month}&PayrollCode={PayrollCode}", filter);
        }

    }
}
