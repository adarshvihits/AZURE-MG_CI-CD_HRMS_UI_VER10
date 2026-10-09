using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Shared.DataBank;
using Hrms.Lite.Shared.General;
using System.Data;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DataBank
{
    public class EmployeeReferenceSchemeService : IEmployeeReferenceSchemeService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public EmployeeReferenceSchemeService(IServiceBase httpService)
        {
            _baseURL = "EmployeeReferenceScheme";
            _baseURL1 = "Common";
            _httpService = httpService;

        }




        public async Task<EmployeeReferenceScheme> GetList()
        {
            return await _httpService.GetAsync<EmployeeReferenceScheme>($"{_baseURL}/List");
        }



        public Task<ResponseEntity> Create(EmployeeReferenceScheme input)
        {
            return _httpService.PostAsync<ResponseEntity, EmployeeReferenceScheme>($"{_baseURL}/Create", input);
        }




        public async Task<DataSet> GetList_Excel()
        {
            return await _httpService.GetAsync<DataSet>($"{_baseURL}/GetList_Excel");
        }


        public Task<ResponseEntity> CreateMaster(EmployeeReferenceScheme input)
        {
            return _httpService.PostAsync<ResponseEntity, EmployeeReferenceScheme>($"{_baseURL}/CreateMaster", input);
        }

    }
}
