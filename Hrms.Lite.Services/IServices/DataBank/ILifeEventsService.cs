using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank
{
    public interface ILifeEventsService
    {
        public Task<LifeEvents> GetCelebrities(int CelebrationCombo1, string Classification);
        public Task<LifeEvents> GetEventList(string Mode);
        public Task<LifeEvents> LifeEventDetails(Guid EventGI);
        public Task<ResponseEntity> Delete(Guid EventGI);
        public Task<ResponseEntity> Save(LifeEvents model);
    }
}
