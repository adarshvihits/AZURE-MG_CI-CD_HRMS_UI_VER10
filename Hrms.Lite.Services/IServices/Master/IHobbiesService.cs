using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
    public interface IHobbiesService
    {
        public Task<Hobbies> GetList(string TabIndex);
        public Task<ResponseEntity> Create(Hobbies input);
        public Task<Hobbies> GetHobbyDetails(Guid HobbyGI, Guid LogGI, string Type);
        public Task<ResponseEntity> ApproveOrReject(Hobbies model);
        public Task<ResponseEntity> Delete(Hobbies input);
        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code);
    }
}
