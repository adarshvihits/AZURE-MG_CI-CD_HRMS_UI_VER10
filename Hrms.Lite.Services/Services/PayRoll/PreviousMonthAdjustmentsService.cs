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
    public class PreviousMonthAdjustmentsService: IPreviousMonthAdjustmentsService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public PreviousMonthAdjustmentsService(IServiceBase httpService)
        {
            _baseURL = "PreviousMonthAdjustment";
            _httpService = httpService;
        }
        public async Task<PreviousMonthAdjustment> PrevMonthAdjList(int PayrollGroup, int Month, int EffecMonth,string  AdjustmentType, Filter Filter)
        {
            return await _httpService.PostAsync<PreviousMonthAdjustment, Filter>($"{_baseURL}/PrevMonthAdjList?PayrollGroup={PayrollGroup}&Month={Month}&EffecMonth={EffecMonth}&AdjustmentType={AdjustmentType}", Filter);
        }

        public async Task<ResponseEntity> EmployeeWisePrevMonthAdjSave(PreviousMonthAdjustment model)
        {
            return await _httpService.PostAsync<ResponseEntity, PreviousMonthAdjustment>($"{_baseURL}/EmployeeWisePrevMonthAdjSave", model);
        }
        public async Task<DataSet> GetPrevMonthAdj_Excel(int PayrollGroup, int Month, int EffecMonth, string AdjustmentType,Filter Filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetPrevMonthAdj_Excel?PayrollGroup={PayrollGroup}&Month={Month}&EffecMonth={EffecMonth}&AdjustmentType={AdjustmentType}", Filter);
        }
        public async Task<ResponseEntity<List<PreviousMonthAdjustment>>> ImportFromExcel(PreviousMonthAdjustment model)
        {
            return await _httpService.PostAsync<ResponseEntity<List<PreviousMonthAdjustment>>, PreviousMonthAdjustment>($"{_baseURL}/ImportFromExcel?Month={model.ProcessingMonth.Code}&EffecMonth={model.EffectiveMonth.Code}", model);

        }

       public async Task<PreviousMonthAdjustment> GetPrevMonthAdjReport(int Month, string AdjustmentType, Filter Filter)
        {
            return await _httpService.PostAsync<PreviousMonthAdjustment, Filter>($"{_baseURL}/GetPrevMonthAdjReport?&Month={Month}&AdjustmentType={AdjustmentType}", Filter);

        }

        public async Task<DataSet> GetPrevMonthAdjReportExcel( int Month, string AdjustmentType, Filter Filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetPrevMonthAdjReportExcel?Month={Month}&AdjustmentType={AdjustmentType}", Filter);

        }
    }
}
