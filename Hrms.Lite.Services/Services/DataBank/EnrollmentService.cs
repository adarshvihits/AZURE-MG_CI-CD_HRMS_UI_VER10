using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.DataBank.Generic;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DataBank
{
   public class EnrollmentService : IEnrollmentService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public EnrollmentService(IServiceBase httpService)
        {
            _baseURL = "Enrollment";
            _httpService = httpService;
        }
        async Task<CompensationStructure> IEnrollmentService.GetComponsationGrid(int GradeCode)
        {
            return await _httpService.GetAsync<CompensationStructure>($"{_baseURL}/GetAllowanceDeductionGrid?GradeCode={GradeCode}");

        }
        async Task<Enrollment> IEnrollmentService.GetPreEnrollmentDetails(Guid EnrollGI)
        {
            return await _httpService.GetAsync<Enrollment>($"{_baseURL}/GetPreEnrollment?EnrollGI={EnrollGI}");
        }

        async Task<Enrollment> IEnrollmentService.AlreadyExistCheck(Guid EmployeeGI, string Mobile, string PAN, string Aadhar, string PFNumber, string ESINumber, string UANNumber, string EmployeeID, string PunchingID,string AccountNo)
        {
            return await _httpService.GetAsync<Enrollment>($"{_baseURL}/AlreadyExistCheck?EmployeeGI={EmployeeGI}&Mobile={Mobile}&PAN={PAN}&Aadhar={Aadhar}&PFNumber={PFNumber}&ESINumber={ESINumber}&UANNumber={UANNumber}&EmployeeID={EmployeeID}&PunchingID={PunchingID}&AccountNo={AccountNo}");
        }

        async Task<ResponseEntity> IEnrollmentService.SaveEnrollment(Enrollment Enrollment)
        {
            return await _httpService.PostAsync<ResponseEntity, Enrollment>($"{_baseURL}/SaveEnrollment", Enrollment);
        }
        async Task<ResponseEntity> IEnrollmentService.RejectEnroll(PreEnrollment PreEnrollment)
        {
            return await _httpService.PostAsync<ResponseEntity, PreEnrollment>($"{_baseURL}/RejectEnroll", PreEnrollment);
        }
        async Task<CompensationStructure> IEnrollmentService.GetSalaryStructureAlwDedGrid(int PreEnrollCode, int SalaryStructureCode, decimal? CTC, decimal? GrossSalary, int GroupCompany, int LocationCode, int DepartmentCode, int VDAGroup)
        {
            return await _httpService.GetAsync<CompensationStructure>($"{_baseURL}/GetSalaryStructureAlwDedGrid?PreEnrollCode={PreEnrollCode}&SalaryStructureCode={SalaryStructureCode}&CTC={CTC}&GrossSalary={GrossSalary}&GroupCompany={GroupCompany}&LocationCode={LocationCode}&DepartmentCode={DepartmentCode}&VDAGroup={VDAGroup}");

        }

        async Task<Enrollment> IEnrollmentService.GetEnrollLeaveOpeningDetails(string DOJ, int LeaveGroup)
        {
            return await _httpService.GetAsync<Enrollment>($"{_baseURL}/GetEnrollLeaveOpeningDetails?DOJ={DOJ}&LeaveGroup={LeaveGroup}");

        }


        //Enroll Edit
        async Task<OnBoarding> IEnrollmentService.GetEnrollEditList(Filter Filter)        
        {
            return await _httpService.PostAsync<OnBoarding, Filter>($"{_baseURL}/GetEnrollEditList", Filter);
        }
        async Task<JoiningConfirmation> IEnrollmentService.GetEnrollEditDetails(Guid EnrollGI)
        {
            return await _httpService.GetAsync<JoiningConfirmation>($"{_baseURL}/GetEnrollEditDetails?EnrollGI={EnrollGI}");
        }

      
    }
}
