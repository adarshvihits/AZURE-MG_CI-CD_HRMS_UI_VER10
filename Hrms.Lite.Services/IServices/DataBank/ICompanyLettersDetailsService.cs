using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank
{
    public interface ICompanyLettersDetailsService
    {
        public Task<ResponseEntity<string>> GeneratePDF(int UserCode, int empCode, int LetterFormatCode, int GcCode, bool Flag);
    }
}
