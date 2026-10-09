using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.Master;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.Master
{
    public class HRLetterSettingsService: IHRLetterSettingsService
    {
        private readonly string _baseURL;


        private readonly IServiceBase _httpService;
        public HRLetterSettingsService(IServiceBase httpService)
        {
            _baseURL = "HRLetterSettings";
            _httpService = httpService;
        }



        public async Task<ResponseEntity> HRLetterSettingsSave(HRLetterSettings model,int LetterType)
        {
            return await _httpService.PostAsync<ResponseEntity, HRLetterSettings>($"{_baseURL}/HRLetterSettingsSave?LetterType={LetterType}", model);
        }



        public async Task<HRLetterSettings> GetHRLetterSettingsDetails(int LetterType)
        {
            return await _httpService.GetAsync<HRLetterSettings>($"{_baseURL}/GetHRLetterSettingsDetails?LetterType={LetterType}");
        }




    }
}
