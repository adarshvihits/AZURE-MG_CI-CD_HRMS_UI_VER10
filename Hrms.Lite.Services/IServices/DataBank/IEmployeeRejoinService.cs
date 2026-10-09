using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank
{
    public interface IEmployeeRejoinService
    {
        public Task<OnBoarding> GetRejoinList(Filter Filter);
        public Task<JoiningConfirmation> GetRejoinEmployeeDetails(Guid EmployeeGI);
        public Task<ResponseEntity<string>> Save(Enrollment Enrollment);
    }
}
