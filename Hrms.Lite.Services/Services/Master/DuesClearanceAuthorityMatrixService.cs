using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.Master;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.Master
{
    public class DuesClearanceAuthorityMatrixService: IDuesClearanceAuthorityMatrixService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public DuesClearanceAuthorityMatrixService(IServiceBase httpService)
        {
            _baseURL = "DuesClearanceAuthorityMatrix";
            _baseURL1 = "Common";
            _httpService = httpService;
        }
        public async Task<DuesClearanceAuthorityMatrix> GetList()
        {
            return await _httpService.GetAsync<DuesClearanceAuthorityMatrix>($"{_baseURL}/List");
        }

        public async Task<DuesClearanceAuthorityMatrix> GetClearanceVsAuthorityPopup(int LevelCode,  Filter filter)
        {
            return await _httpService.PostAsync<DuesClearanceAuthorityMatrix, Filter>($"{_baseURL}/GetClearanceVsAuthorityPopup?LevelCode={LevelCode}", filter);
        }
        public Task<ResponseEntity> Create(DuesClearanceAuthorityMatrix input)
        {
            return _httpService.PostAsync<ResponseEntity, DuesClearanceAuthorityMatrix>($"{_baseURL}/Create", input);
        }
        public async Task<DataSet> GetList_Excel()
        {
            return await _httpService.GetAsync<DataSet>($"{_baseURL}/GetList_Excel");
        }
    }
}
