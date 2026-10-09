using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Shared.DataBank;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DataBank
{
    public class EconsoleService : IEconsoleService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public EconsoleService(IServiceBase httpService)
        {
            _baseURL = "Econsole";
            _httpService = httpService;
        }
        public async Task<Econsole> GetList(int tabIndex)
        {
            return await _httpService.GetAsync<Econsole>($"{_baseURL}/List?tabIndex={tabIndex}");
        }
        public async Task<Econsole> GetCombinationDetails(int id, int DojAppl, int BeforeAfter, string Date)
        {
            return await _httpService.GetAsync<Econsole>($"{_baseURL}/CombinationDetails?Typecode={id}&DojAppl={DojAppl}&BeforeAfter={BeforeAfter}&Date={Date}");
        }
        public async Task<ResponseEntity> Save(Econsole model)
        {
            return await _httpService.PostAsync<ResponseEntity, Econsole>($"{_baseURL}/Save", model);
        }
        public async Task<Econsole> GetEconsoleDetails(int Code)
        {
            return await _httpService.GetAsync<Econsole>($"{_baseURL}/GetEconsoleDetails?Code={Code}");
        }
        public async Task<ResponseEntity> Delete(int Code)
        {
            return await _httpService.DeleteAsync<ResponseEntity>($"{_baseURL}/Delete?Code={Code}");
        }
    }
}
