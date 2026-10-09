using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.DataBank.Reports;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank
{
    public interface IResignationService
    {
        public Task<IList<ResignationListTabs>> GetResignationTabs();
        public Task<Resignation> GetResignationList(int Tabindex,Filter filter);
        public Task<IList<ApprovalPendingDetails>> GetPendingApprovalsList(Guid EmployeeGI);
        public Task<Resignation> GetNoticePeriodDetails(Guid EmployeeGI, string  ResignationDate, string LastWorkingDay);
        public Task<Resignation> GetResignationAcceptanceDetails(Guid ResignationGI);
        public Task<Resignation> GetExitChecklistDetail(Guid ResignationGI);
        public Task<Resignation> GetRelievingDetails(Guid ResignationGI);
        public Task<ResignationTracker> GetRelievingTracker(Guid ResignationGI);
        public Task<ResponseEntity> NewRequestSave(Resignation model);
        public Task<ResponseEntity> RequestAccepatance(Resignation model);
        public Task<ResponseEntity> ExitCheck(Resignation model);
        public Task<ResponseEntity> EmployeeRelievingSave(Resignation model);
        public Task<Resignation> GetResignationDeleteList(Filter filter);
        public Task<Resignation> GetResignationDeleteDetails(Guid EmployeeGI, Guid ResignationGI);
        public Task<ResponseEntity> Delete(Guid ResignationGI, string Remarks);

        public Task<Resignation> GetResignationAmendmentDetails(Guid ResignationGI);
        public Task<ResponseEntity> SaveResignationAmendment(Resignation model);
    }
}
