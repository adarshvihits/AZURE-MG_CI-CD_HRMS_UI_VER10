using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DataBank
{
    public class JoiningConfirmationService : IJoiningConfirmationService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public JoiningConfirmationService(IServiceBase httpService)
        {
            _baseURL = "JoiningConfirmation";
            _httpService = httpService;
        }

        async Task<ResponseEntity> IJoiningConfirmationService.ConfirmJoin(JoiningConfirmation Model)
        {
            return await _httpService.PostAsync<ResponseEntity, JoiningConfirmation>($"{_baseURL}/ConfirmJoin", Model);
        }
        async  Task<JoiningConfirmation> IJoiningConfirmationService.confirmJoinExistValidation(Guid EnrollGI)
        {
            return await _httpService.GetAsync<JoiningConfirmation>($"{_baseURL}/confirmJoinExistValidation?EnrollGI={EnrollGI}");
        }
        async Task<ResponseEntity> IJoiningConfirmationService.RejectJoin(JoiningConfirmation Input)
        {
            return await _httpService.PostAsync<ResponseEntity, JoiningConfirmation>($"{_baseURL}/RejectJoin", Input);
        }
        //async Task<ResponseEntity> IJoiningConfirmationService.ConfirmJoin(Guid PreEnrollGI)
        //{
        //    return await _httpService.PostAsync<ResponseEntity, JoiningConfirmation>($"{_baseURL}/ConfirmJoin", PreEnrollGI);
        //}

        async Task<JoiningConfirmation> IJoiningConfirmationService.GetJoiningCofirmDetails(Guid EnrollGI)
        {
            return await _httpService.GetAsync<JoiningConfirmation>($"{_baseURL}/GetJoiningConfirmationDetails?EnrollGI={EnrollGI}");
        }

        public async Task<EmployeeHeader> GetDuplicationDetails(string DocNmber, int TypeCode)
        {
            return await _httpService.GetAsync<EmployeeHeader>($"{_baseURL}/GetDuplicationDetails?DocNumber={DocNmber}&TypeCode={TypeCode}");
        }
    }
}
