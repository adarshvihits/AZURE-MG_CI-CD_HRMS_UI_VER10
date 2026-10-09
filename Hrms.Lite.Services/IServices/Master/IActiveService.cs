using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
    public interface IActivityService
    {
        public Task<Activity> GetList(string TabIndex);
        public Task<ResponseEntity> Create(Activity input);
        public Task<Activity> GetActivityDetails(Guid ActivityGI, Guid LogGI, string Type);
        public Task<ResponseEntity> Delete(Activity model);
        public Task<ResponseEntity> ApproveOrReject(Activity model);
        
    }
}
