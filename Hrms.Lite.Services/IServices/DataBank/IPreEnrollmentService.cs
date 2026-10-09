using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.DataBank.Generic;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank
{
   public interface IPreEnrollmentService
    {
        public Task<ResponseEntity> SavePreEnrollment(PreEnrollment PreEnrollment);
        public Task<PreEnrollment> AlreadyExistCheck(string Mobile, string PAN, string Aadhar);
        public Task<PreEnrollment> GetRenewalDate(int CategoryCode, string DOJ, string DOB);
   
        
        public Task<CompensationStructure> GetCompensationGrid(int GradeCode);
        public Task<CompensationStructure> GetSalaryStructureAlwDedGrid(int SalaryStructureCode, decimal? CTC, decimal? GrossSalary, int GroupCompany, int LocationCode, int DepartmentCode, int VDAGroup);
        public Task<ResponseEntity> ApproveOrReject(Enrollment model);
        public Task<PreEnrollment> GetMinCTC(int WageGradeCode,int LocationCode);
        public Task<PreEnrollment> GetApplicantDetails(Guid ApplicantGI);
    }
}
