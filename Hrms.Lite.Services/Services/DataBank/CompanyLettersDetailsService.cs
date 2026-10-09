using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DataBank
{
    public class CompanyLettersDetailsService: ICompanyLettersDetailsService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public CompanyLettersDetailsService(IServiceBase httpService)
        {
            _baseURL = "CompanyLettersDetails";
            _httpService = httpService;
        }
        public async Task<ResponseEntity<string>> GeneratePDF(int UserCode, int empCode, int LetterFormatCode, int GcCode, bool Flag)
        {
            return await _httpService.GetAsync<ResponseEntity<string>>($"{_baseURL}/GeneratePDF?UserCode={UserCode}&empCode={empCode}&LetterFormatCode={LetterFormatCode}&GcCode={GcCode}&Flag={Flag}");
        }
    }
}
