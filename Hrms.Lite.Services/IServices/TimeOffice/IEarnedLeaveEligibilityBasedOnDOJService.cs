using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TimeOffice;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.TimeOffice
{
  public  interface IEarnedLeaveEligibilityBasedOnDOJService
    {
        Task<EarnedLeaveEligibility> EarnedLeaveEligibilityList(int Leave, int Month,int ConfirmationValue,Filter filter);
        Task<ResponseEntity> SaveEarnedLeaveEligibility(int Leave, int Month, Filter Filter);
        Task<ResponseEntity> EmployeeWiseSave(EarnedLeaveEligibility model);
        Task<DataSet> EarnedLeaveEligibilityList_Excel(int Leave, int Month, int ConfirmationValue, Filter filter);
        Task<ResponseEntity<EarnedLeaveEligibility>> ImportFromExcel(int? MonthCode, int? LeaveCode, EarnedLeaveEligibility model);

    }
}
