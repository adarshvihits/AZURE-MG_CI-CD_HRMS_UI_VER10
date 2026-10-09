using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
namespace Hrms.Lite.Services.IServices.DataBank
{
   public interface IInterviewSchedulingService
    {
        public Task<ApplicantTracking> GetInterviewSchedulingList( int Tabindex);
        Task<DataSet> GetInterviewSchedulingList_Excel( int Tabindex);
        Task<ResponseEntity> EmployeeWiseInterviewScheduling(ApplicantTracking model);
    }
}
