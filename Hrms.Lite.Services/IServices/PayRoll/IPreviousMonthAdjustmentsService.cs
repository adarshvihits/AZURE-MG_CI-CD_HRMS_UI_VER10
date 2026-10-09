using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.PayRoll
{
    public interface IPreviousMonthAdjustmentsService
    {
        public Task<PreviousMonthAdjustment> PrevMonthAdjList( int PayrollGroup, int Month, int EffecMonth, string AdjustmentType, Filter Filter);
        public Task<ResponseEntity> EmployeeWisePrevMonthAdjSave(PreviousMonthAdjustment model);
        Task<DataSet> GetPrevMonthAdj_Excel(int PayrollGroup, int Month, int EffecMonth, string AdjustmentType,Filter Filter);
        public Task<ResponseEntity<List<PreviousMonthAdjustment>>> ImportFromExcel(PreviousMonthAdjustment model);

        //-----------------------------------------------------REPORT-----------------------------------------------------------------------------------

        public Task<PreviousMonthAdjustment> GetPrevMonthAdjReport( int Month, string AdjustmentType, Filter Filter);
        Task<DataSet> GetPrevMonthAdjReportExcel( int Month, string AdjustmentType, Filter Filter);
    }
}
