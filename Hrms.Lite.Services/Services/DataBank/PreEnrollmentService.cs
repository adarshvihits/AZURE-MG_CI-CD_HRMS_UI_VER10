using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.DataBank.Generic;
using Hrms.Lite.Shared.General;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DataBank
{
public  class PreEnrollmentService : IPreEnrollmentService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public PreEnrollmentService(IServiceBase httpService)
        {
            _baseURL = "PreEnrollment";
            _httpService = httpService;
        }

        async Task<CompensationStructure> IPreEnrollmentService.GetCompensationGrid(int GradeCode)
        {
            return await _httpService.GetAsync<CompensationStructure>($"{_baseURL}/GetAllowanceDeductionGrid?GradeCode={GradeCode}");

        }

        async Task<PreEnrollment> IPreEnrollmentService.AlreadyExistCheck(string Mobile, string PAN, string Aadhar)
        {
            return await _httpService.GetAsync<PreEnrollment>($"{_baseURL}/AlreadyExistCheck?Mobile={Mobile}&PAN={PAN}&Aadhar={Aadhar}");
        }
        async Task<PreEnrollment> IPreEnrollmentService.GetRenewalDate(int CategoryCode, string DOJ, string DOB)
        {
            return await _httpService.GetAsync<PreEnrollment>($"{_baseURL}/GetRenewalDate?CategoryCode={CategoryCode}&DOJ={DOJ}&DOB={DOB}");
        }
      
        async Task<ResponseEntity> IPreEnrollmentService.SavePreEnrollment(PreEnrollment PreEnrollment)
        {
            return await _httpService.PostAsync<ResponseEntity, PreEnrollment>($"{_baseURL}/SavePreEnrollment", PreEnrollment);
        }
        public Task<ResponseEntity> ApproveOrReject(Enrollment model)
        {
            return _httpService.PostAsync<ResponseEntity,Enrollment>($"{_baseURL}/ApproveRejectPreenroll",model);
        }
        async Task<CompensationStructure> IPreEnrollmentService.GetSalaryStructureAlwDedGrid(int SalaryStructureCode, decimal? CTC, decimal? GrossSalary, int GroupCompany, int LocationCode, int DepartmentCode,int VDAGroup)
        {
            return await _httpService.GetAsync<CompensationStructure>($"{_baseURL}/GetSalaryStructureAlwDedGrid?SalaryStructureCode={SalaryStructureCode}&CTC={CTC}&GrossSalary={GrossSalary}&GroupCompany={GroupCompany}&LocationCode={LocationCode}&DepartmentCode={DepartmentCode}&VDAGroup={VDAGroup}");

        }
        async Task<PreEnrollment> IPreEnrollmentService.GetMinCTC(int WageGradeCode,int LocationCode)
        {
            return await _httpService.GetAsync<PreEnrollment>($"{_baseURL}/GetMinCTC?WageGradeCode={WageGradeCode}&LocationCode={LocationCode}");
        }
        async Task<PreEnrollment> IPreEnrollmentService.GetApplicantDetails(Guid ApplicantGI)
        {
            return await _httpService.GetAsync<PreEnrollment>($"{_baseURL}/GetApplicantDetails?ApplicantGI={ApplicantGI}");
        }
    }

}
