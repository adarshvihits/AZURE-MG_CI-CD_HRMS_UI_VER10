using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.PayRoll;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using Hrms.Lite.Shared.PayRoll;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.PayRoll
{
 public class ProcessReadinessCheckService:IProcessReadinessCheckService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public ProcessReadinessCheckService(IServiceBase httpService)
        {
            _baseURL = "ProcessReadinessCheck";
            _httpService = httpService;
        }

        public async Task<ProcessReadinessCheck> GetProcessReadinessCheckList(int Month, int PayrollGroup)
        {
            return await _httpService.GetAsync<ProcessReadinessCheck>($"{_baseURL}/GetProcessReadinessCheckList?PayrollGroup={PayrollGroup}&Month={Month}");
        }
        public async Task<DataSet> GetProcessReadinessCheckList_Excel(int SalPeriodCode, int PayrollGroup)
        {
            return await _httpService.GetAsync<DataSet>($"{_baseURL}/GetProcessReadinessCheckList_Excel?PayrollGroup={PayrollGroup}&SalPeriodCode={SalPeriodCode}");
        }

        //public async Task<ProcessReadinessCheck> GetSalaryProcessReadinessCheckList(int Month, int PayrollGroup)
        //{
        //    return await _httpService.GetAsync<ProcessReadinessCheck>($"{_baseURL}/GetSalaryProcessReadinessCheckList?PayrollGroup={PayrollGroup}&Month={Month}");
        //}

        public async Task<ProcessReadinessCheck> SalaryProcessReadinessCheckPopUp(int SalaryPeriodCode, int PayrollGroup, string TabName, string Description)
        {
            return await _httpService.GetAsync<ProcessReadinessCheck>($"{_baseURL}/SalaryProcessReadinessCheckPopUp?SalaryPeriodCode={SalaryPeriodCode}&PayrollGroup={PayrollGroup}&TabName={TabName}&Description={Description}");
        }


        public async Task<ProcessReadinessCheck> MonthlyAttendanceProcessingListPopUp(int Month, int PayrollGroup, int Tabindex, string Description, Filter filter)
        {
            return await _httpService.PostAsync<ProcessReadinessCheck,Filter>($"{_baseURL}/MonthlyAttendanceProcessingListPopUp?Month={Month}&PayrollGroup={PayrollGroup}&Tabindex={Tabindex}&Description={Description}",filter);
        }

       

        //public async Task<DataSet> GetSalaryProcessReadinessCheckList_Excel(int SalPeriodCode, int PayrollGroup)
        //{
        //    return await _httpService.GetAsync<DataSet>($"{_baseURL}/GetSalaryProcessReadinessCheckList_Excel?PayrollGroup={PayrollGroup}&SalPeriodCode={SalPeriodCode}");
        //}

        public async Task<ProcessReadinessCheck>GetBlankStatDaysPopList(Guid EmployeeGI, int Month,string Description)
        {
            return await _httpService.GetAsync<ProcessReadinessCheck>($"{_baseURL}/GetBlankStatDaysPopList?EmployeeGI={EmployeeGI}&Month={Month}&Description={Description}");
        }

    }
}
