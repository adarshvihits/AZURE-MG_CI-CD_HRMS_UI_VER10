using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.TimeOffice;
using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using Hrms.Lite.Shared.TimeOffice;
using Hrms.Lite.Shared.TimeOffice.Generic.Master;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.TimeOffice
{
    public class DutyRosterService : IDutyRosterService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public DutyRosterService(IServiceBase httpService)
        {
            _baseURL = "DutyRoster";
            _httpService = httpService;
        }
       public async Task<ShiftDetails> GetShiftDetails(int ShiftCode)
        {
           return await _httpService.GetAsync<ShiftDetails>($"{_baseURL}/GetShiftDetails?ShiftCode={ShiftCode}");
        }
        public async Task<DutyRoster> DutyRosterEntryList(int Month, int PayrollGroup, int CIRCLE, Filter filter)
        {
            return await _httpService.PostAsync<DutyRoster, Filter>($"{_baseURL}/DutyRosterEntryList?Month={Month}&PayrollGroup={PayrollGroup}&CIRCLE={CIRCLE}", filter);
        }
        public async Task<DataSet> DutyRosterEntryList_Excel(int Month, int PayrollGroup, int CIRCLE,int ShiftType, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/DutyRosterEntryList_Excel?Month={Month}&PayrollGroup={PayrollGroup}&CIRCLE={CIRCLE}&ShiftType={ShiftType}", filter);
        }
        public async Task<DutyRoster> DutyRosterDateWiseEntryList(int Month, int PayrollGroup, string Date, int CIRCLE, Filter filterdata)
        {
            return await _httpService.PostAsync<DutyRoster, Filter>($"{_baseURL}/DutyRosterDateWiseEntryList?Month={Month}&PayrollGroup={PayrollGroup}&Date={Date}&CIRCLE={CIRCLE}", filterdata);

        }
        public async Task<DutyRoster> EmployeeDutyRosterEditDetails(int Month, int PayrollGroup, string FROM, string TO, int CIRCLE, Guid EmployeeGI)
        {
            return await _httpService.GetAsync<DutyRoster>($"{_baseURL}/EmployeeDutyRosterEditDetails?Month={Month}&PayrollGroup={PayrollGroup}&FROM={FROM}&TO={TO}&CIRCLE={CIRCLE}&EmployeeGI={EmployeeGI}");
        }
        public async Task<ResponseEntity> DutyRosterSave(Employee_DutyRosterEditDetails model)
        {
            return await _httpService.PostAsync<ResponseEntity, Employee_DutyRosterEditDetails>($"{_baseURL}/EmployeeWise_DutyRoster", model);

        }

    public async    Task<DutyRoster> DutyRosterMailListBatchUpdate_Details(int Month, int PayrollGroup, string FROM, string TO, int CIRCLE , int ShiftCode, Filter filterdata)
        {
            return await _httpService.PostAsync<DutyRoster, Filter>($"{_baseURL}/DutyRosterMailListBatchUpdate_Details?Month={Month}&PayrollGroup={PayrollGroup}&FROM={FROM}&TO={TO}&CIRCLE={CIRCLE}&ShiftCode={ShiftCode}", filterdata);

        }

      public async  Task<ResponseEntity> DutyRoster_BatchUpDate(DutyRoster model)
        {
            return await _httpService.PostAsync<ResponseEntity, DutyRoster>($"{_baseURL}/DutyRoster_BatchUpDate", model);

        }

        async Task<DutyRoster> IDutyRosterService.DutyRosterDateWiseEntryList_BatchUpdate_Details(int Month, int PayrollGroup, string Date, int CIRCLE, Filter filterdata)
        {
            return await _httpService.PostAsync<DutyRoster, Filter>($"{_baseURL}/DutyRosterDateWiseEntryList_BatchUpdate_Details?Month={Month}&PayrollGroup={PayrollGroup}&Date={Date}&CIRCLE={CIRCLE}",filterdata );
        }

      public  async Task<DutyRoster> DutyRosterEmployeeWiseEntry_BatchUpdate_Details(int Month)
        {
            return await _httpService.GetAsync<DutyRoster>($"{_baseURL}/DutyRosterEmployeeWiseEntry_BatchUpdate_Details?Month={Month}");
        }

       public async Task<ResponseEntity> DutyRoster_Employeewise_BatchUpDate(DutyRoster model)
        {
            return await _httpService.PostAsync<ResponseEntity, DutyRoster>($"{_baseURL}/DutyRoster_Employeewise_BatchUpDate", model);
        }

     public async Task<IList<ShiftDetails>> GetShiftDetailsforPopUp()
        {
            return await _httpService.GetAsync<IList<ShiftDetails>>($"{_baseURL}/GetShiftDetailsforPopUp");

        }


        public async Task<ResponseEntity> DutyrosterLockCheck(int CircleCode, string SelectedDate)
        {
            return await _httpService.GetAsync<ResponseEntity>($"ESSPDutyRoster/DutyrosterLockCheck?CircleCode={CircleCode}&SelectedDate={SelectedDate}");
        }
        public async Task<DutyRoster> DutyRosterEntryList_Paging(int Month, int PayrollGroup, int CIRCLE,int ShiftType, Filter filter)
        {
            return await _httpService.PostAsync<DutyRoster, Filter>($"{_baseURL}/DutyRosterEntryList_Paging?Month={Month}&PayrollGroup={PayrollGroup}&CIRCLE={CIRCLE}&ShiftType={ShiftType}", filter);
        }
        public async Task<ResponseEntity<List<DutyRosterList>>> ImportFromExcel(DutyRoster model)
        {
            return await _httpService.PostAsync<ResponseEntity<List<DutyRosterList>>, DutyRoster>($"{_baseURL}/ImportFromExcel", model);

        }
        public async Task<ResponseEntity<List<DutyRosterList>>> ImportFromExcel_FromToDateUpdation(DutyRoster model)
        {
            return await _httpService.PostAsync<ResponseEntity<List<DutyRosterList>>, DutyRoster>($"{_baseURL}/ImportFromExcel_FromToDateUpdation", model);

        }
    }
}
