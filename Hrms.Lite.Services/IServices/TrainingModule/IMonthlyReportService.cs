using Hrms.Lite.Shared.TrainingModule;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.TrainingModule
{
   public interface IMonthlyReportService
    {
        public Task<MonthlyReport> GetMonthlyReportList(int Month);
        public Task<MonthlyReport> GetParticipantsSelectedDetails(int ScheduleCode, int Month);
        public Task<MonthlyReport> GetParticipantsCompletedDetails(int ScheduleCode, int Month);
        public Task<DataSet> ParticipantselectedExcel(int ScheduleCode, int Month);
        public Task<DataSet> ParticipantcompletedExcel(int ScheduleCode, int Month);
    }
}
