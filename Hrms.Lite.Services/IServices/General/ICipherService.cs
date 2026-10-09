using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.General
{
    public interface ICipherService
    {
        public string Encrypt(string input);
        public string Decrypt(string cipherText);
        public string EncryptWithCustomKey(string input);
        public string DecryptWithCustomKey(string cipherText);
    }
}
