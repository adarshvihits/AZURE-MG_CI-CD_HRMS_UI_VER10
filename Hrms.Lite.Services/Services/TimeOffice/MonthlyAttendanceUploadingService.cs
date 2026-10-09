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
    public class MonthlyAttendanceUploadingService : IMonthlyAttendanceUploadingService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public MonthlyAttendanceUploadingService(IServiceBase httpService)
        {
            _baseURL = "MonthlyAttendanceUploading";
            _httpService = httpService;
        }


        public async Task<MonthlyAttendanceUploading> GetEmployeeList(int Month, int PayrollGroup, Filter filter)
        {
            return await _httpService.PostAsync<MonthlyAttendanceUploading, Filter>($"{_baseURL}/GetEmployeeList?Month={Month}&PayrollGroup={PayrollGroup}", filter);

        }

        public async Task<ResponseEntity<MonthlyAttendanceUploading>> ImportFromExcel(int Month,int PayrollGroup , MonthlyAttendanceUploading model)
        {
            return await _httpService.PostAsync<ResponseEntity<MonthlyAttendanceUploading>, MonthlyAttendanceUploading>($"{_baseURL}/ImportFromExcel?Month={Month}&PayrollGroup={PayrollGroup}", model);
  
        }

        public async Task<ResponseEntity> Update(int Month, MonthlyAttendanceUploading_Data input)
        {
            return await _httpService.PostAsync<ResponseEntity, MonthlyAttendanceUploading_Data>($"{_baseURL}/Update?Month={Month}", input);

        }
        public async Task<DataSet> GetEmployeeList_Excel(int Month,int PayrollGroup,Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetEmployeeList_Excel?Month={Month}&PayrollGroup={PayrollGroup}", filter);
        }


    }
}
