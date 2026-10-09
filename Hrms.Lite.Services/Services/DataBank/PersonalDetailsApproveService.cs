using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Shared;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DataBank
{
    public class PersonalDetailsApproveService: IPersonalDetailsApproveService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public PersonalDetailsApproveService(IServiceBase httpService)
        {
            _baseURL = "PersonalDetails";
            _httpService = httpService;
        }
        public async Task<IEnumerable<PersonalDetailsList>> GetPersonalDetailsApprovalList(int SearchId, Filter Filter)
        {
            return await _httpService.PostAsync<IEnumerable<PersonalDetailsList>, Filter>($"{_baseURL}/GetPersonalDetailsApprovalList?SearchId={SearchId}", Filter);
        }
        public async Task<PersonalDetails> GetPersonalDetailsApprovalData(Guid EmployeeGI)
        {
            return await _httpService.GetAsync<PersonalDetails>($"{_baseURL}/GetPersonalDetailsApprovalData?EmployeeGI={EmployeeGI}");
        }
        public async Task<ResponseEntity> ApprovePersonalDetails(PersonalDetails model)
        {
            return await _httpService.PostAsync<ResponseEntity, PersonalDetails>($"{_baseURL}/ApprovePersonalDetails", model);
        }
        public async Task<File> DocumentView(Guid EmployeeGI, string DocType)
        {
            return await _httpService.GetAsync<File>($"{_baseURL}/DocumentView?EmployeeGI={EmployeeGI}&DocType={DocType}");
        }
    }
}
