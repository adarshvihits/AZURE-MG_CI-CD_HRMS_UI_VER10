using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.Account;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;



namespace Hrms.Lite.Services.Services.General
{
    public class CipherService : ICipherService
    {
        private readonly IDataProtectionProvider _dataProtectionProvider;
        private const string Key = "Ihits-Cloud-HRMS-Enrcyption-Key";
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;
        public CipherService(IDataProtectionProvider dataProtectionProvider, IHttpContextAccessor httpContextAccessor)
        {
            _dataProtectionProvider = dataProtectionProvider;
            _httpContextAccessor = httpContextAccessor;
        }
        public string Encrypt(string input)
        {
            var protector = _dataProtectionProvider.CreateProtector(Key);
            return protector.Protect(input);
        }

        public string Decrypt(string cipherText)
        {
            var protector = _dataProtectionProvider.CreateProtector(Key);
            return protector.Unprotect(cipherText);
        }
        public string EncryptWithCustomKey(string input)
        {
            var CustomKey = JsonConvert.DeserializeObject<Login>(_session.GetString("User")).authUser.User.UserGuid;
            var protector = _dataProtectionProvider.CreateProtector(CustomKey.ToString());
            return protector.Protect(input);
        }

        public string DecryptWithCustomKey(string cipherText)
        {
            var CustomKey = JsonConvert.DeserializeObject<Login>(_session.GetString("User")).authUser.User.UserGuid;
            var protector = _dataProtectionProvider.CreateProtector(CustomKey.ToString());
            return protector.Unprotect(cipherText);
        }
    }
}
