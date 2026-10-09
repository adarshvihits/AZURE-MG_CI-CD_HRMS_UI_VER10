using Hrms.Lite.Shared;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank
{
    public interface IPersonalDetailsApproveService
    {
        public Task<IEnumerable<PersonalDetailsList>> GetPersonalDetailsApprovalList(int SearchId, Filter Filter);
        public Task<PersonalDetails> GetPersonalDetailsApprovalData(Guid EmployeeGI);
        public Task<ResponseEntity> ApprovePersonalDetails(PersonalDetails model);
        Task<File> DocumentView(Guid EmployeeGI, string DocType);
    }

}
