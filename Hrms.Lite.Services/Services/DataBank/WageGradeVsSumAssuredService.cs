using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Shared.DataBank;
using Hrms.Lite.Shared.General;
using System.Data;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DataBank
{
    public class WageGradeVsSumAssuredService : IWageGradeVsSumAssuredService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public WageGradeVsSumAssuredService(IServiceBase httpService)
        {
            _baseURL = "WageGradeVsSumAssured";
            _baseURL1 = "Common";
            _httpService = httpService;

        }




        public async Task<WageGradeVsSumAssured> GetList()
        {
            return await _httpService.GetAsync<WageGradeVsSumAssured>($"{_baseURL}/List");
        }



        public Task<ResponseEntity> Create(WageGradeVsSumAssured input)
        {
            return _httpService.PostAsync<ResponseEntity, WageGradeVsSumAssured>($"{_baseURL}/Create", input);
        }


        

        public async Task<DataSet> GetList_Excel()
        {
            return await _httpService.GetAsync<DataSet>($"{_baseURL}/GetList_Excel");
        }
    }
}
