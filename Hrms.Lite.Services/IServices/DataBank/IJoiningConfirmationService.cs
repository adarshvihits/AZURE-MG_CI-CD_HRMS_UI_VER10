using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank
{
   public interface IJoiningConfirmationService
    {
        public Task <JoiningConfirmation> GetJoiningCofirmDetails(Guid EnrollGI);
        public Task<ResponseEntity> ConfirmJoin(JoiningConfirmation Model);
        public Task<JoiningConfirmation> confirmJoinExistValidation(Guid EnrollGI);
        public Task<ResponseEntity> RejectJoin(JoiningConfirmation Input);
        public Task<EmployeeHeader> GetDuplicationDetails(string DocNmber, int TypeCode);

    }
}
