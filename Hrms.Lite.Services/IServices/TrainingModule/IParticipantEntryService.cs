using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TrainingModule;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.TrainingModule
{
 public  interface IParticipantEntryService
    {
        public Task<ParticipantEntry> GetScheduleList(int ScheduleCode, Filter filter,int radiomode);
        public Task<ResponseEntity> Save(int ScheduleCode, ParticipantEntry model, int radiomode);
       
    }
}
