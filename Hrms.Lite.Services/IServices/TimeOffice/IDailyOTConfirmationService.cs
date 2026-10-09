using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TimeOffice;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.TimeOffice
{
    public interface IDailyOTConfirmationService
    {
        Task<OverTimeApproval> GetOverTimeApprovalList(string Date,Filter filter);        
        Task<ResponseEntity> OverTimeApproval(OverTimeApproval model);
        Task<OverTimeApproval> GetOverTimeApprovedDetails(string Date,Filter filter);
        Task<DataSet> GetOverTimeApprovalList_Excel(string Date,Filter filter);
        Task<DataSet> GetOverTimeApprovedDetails_Excel(string Date,Filter filter);
    }
}
