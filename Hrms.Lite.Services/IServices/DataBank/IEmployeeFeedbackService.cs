using Hrms.Lite.Shared.DataBank;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank
{
  public interface IEmployeeFeedbackService
    {
        public Task<EmployeeFeedback> GetList(int radiomode);
        public Task<EmployeeFeedback> GetFeedbackDetails(Guid EmployeeGI,int FeedbackCode);
        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code);

        public Task<ResponseEntity> Create(EmployeeFeedback input);
    }
}
