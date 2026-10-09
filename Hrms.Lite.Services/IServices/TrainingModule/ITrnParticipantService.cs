using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TrainingModule;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.TrainingModule
{
   public interface ITrnParticipantService
    {
        public Task<TrnParticipantSelection> GetScheduleList(int ScheduleCode, Filter filter);
        public Task<TrnParticipantSelection> GetFilteredList(int ScheduleCode, Filter filter);
        public Task<ResponseEntity> DeleteAll(int ScheduleCode);
        public Task<ResponseEntity> Save(int ScheduleCode, TrnParticipantSelection model);
        public Task<ResponseEntity> DeleteParticipant(Guid EmployeeGI, int ScheduleCode);
    }
}
