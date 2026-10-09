using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.PayRoll;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.PayRoll
{
  public  class BonusUploadService: IBonusUploadService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public BonusUploadService(IServiceBase httpService)
        {
            _baseURL = "BonusUpload";
            _httpService = httpService;
        }
        public async Task<BonusUpload> BonusUploadList(int Month, int IncCategory, Filter filter)
        {
            return await _httpService.PostAsync<BonusUpload, Filter>($"{_baseURL}/BonusUploadList?Month={Month}&IncCategory={IncCategory}", filter);
        }
        public async Task<ResponseEntity> BonusUploadSave(BonusUpload model)
        {
            return await _httpService.PostAsync<ResponseEntity, BonusUpload>($"{_baseURL}/BonusUploadSave", model);
        }

        public async Task<DataSet> BonusUploadList_Excel(int Month, int IncCategory, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/BonusUploadList_Excel?Month={Month}&IncCategory={IncCategory}", filter);
        }
        public async Task<ResponseEntity<List<BonusUpload>>> ImportFromExcel(BonusUpload model)
        {
            return await _httpService.PostAsync<ResponseEntity<List<BonusUpload>>, BonusUpload>($"{_baseURL}/ImportFromExcel", model);

        }
    }
}
