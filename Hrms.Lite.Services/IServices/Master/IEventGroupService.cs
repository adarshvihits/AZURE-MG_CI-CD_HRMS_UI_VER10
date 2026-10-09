using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
    public interface IEventGroupService
    {
        public Task<EventGroup> GetList(int tabIndex);
        Task<EventGroup> GetCombinationDetails(int id);
        Task<EventGroup> EmployeeList(string Mode,int EventGroupCode, Filter filter);
        Task<ResponseEntity> Save(EventGroup model);
        public Task<EventGroup> GetEventGroupDetails(int Code);
        public Task<ResponseEntity> Delete(int Code);

    }
}
