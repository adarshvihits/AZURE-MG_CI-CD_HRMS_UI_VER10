using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.DataBank.Generic;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank
{
  public interface IEnrollmentService
    {
        public Task<CompensationStructure> GetComponsationGrid(int GradeCode);
        public Task<CompensationStructure> GetSalaryStructureAlwDedGrid(int PreEnrollCode, int SalaryStructureCode, decimal? CTC, decimal? GrossSalary, int GroupCompany, int LocationCode, int DepartmentCode, int VDAGroup);
        public Task<Enrollment> GetPreEnrollmentDetails(Guid EnrollGI);
        public Task<Enrollment> AlreadyExistCheck(Guid EmployeeGI, string Mobile, string PAN, string Aadhar, string PFNumber, string ESINumber, string UANNumber, string EmployeeID, string PunchingID,string AccountNo);
        public Task<ResponseEntity> SaveEnrollment(Enrollment Enrollment);
        public Task<ResponseEntity> RejectEnroll(PreEnrollment Enrollment);
        Task<Enrollment> GetEnrollLeaveOpeningDetails(string DOJ, int LeaveGroup);
        //Enroll Edit
        Task<OnBoarding> GetEnrollEditList(Filter Filter);
        public Task<JoiningConfirmation> GetEnrollEditDetails(Guid EnrollGI);

    }
}
