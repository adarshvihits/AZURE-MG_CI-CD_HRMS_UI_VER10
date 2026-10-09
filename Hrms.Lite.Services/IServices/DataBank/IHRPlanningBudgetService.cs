using Hrms.Lite.Shared.DashBoard;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank
{
    public interface IHRPlanningBudgetService
    {
        #region BudgetUpload
        public Task<HRPlanningBudget> GetBudgetUploadList(int HRPlanningCode, int DesignationCode);
        public Task<ResponseEntity<List<HRPlanningBudgetDetails>>> ImportFromExcel(HRPlanningBudget model);

        #endregion
        #region Approve
        public Task<HRPlanningBudget> GetBudgetUploadApprovalMainList();
        public Task<HRPlanningBudget> GetBudgetUploadApprovalList( int HRPlanningCode, int DesignationCode);
        public Task<ResponseEntity> BudgetApprove( HRPlanningBudget model, string Mode);
        #endregion
        #region Variance
        public Task<HRPlanningBudget> GetBudgetVarianceReport(int HRPlanningCode, int DesignationCode, int Type);
        public Task<ResponseEntity> CreateMRF(HRPlanningBudget model);
        #endregion
    }
}
