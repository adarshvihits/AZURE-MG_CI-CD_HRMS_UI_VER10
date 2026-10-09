using Hrms.Lite.Shared.DataBank;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank
{
    public interface IInstantMessagingService
    {
        public Task<InstantMessaging> GetMessageList();
        public Task<InstantMessaging> GetMessageDetails(int MessageCode);
        public Task<ResponseEntity> Message_Save(InstantMessaging model);
        public Task<InstantMessaging> GetEmployeesList(Filter filter,int MessageCode);
        public Task<ResponseEntity> Employee_Save(InstantMessaging model);
        public Task<ResponseEntity> Message_Send(int MessageCode);
    }
}
