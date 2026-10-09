using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
    public interface IReaderService
    {
        public Task<Reader> List(string TabIndex);
        public Task<Reader> GetReaderDetails(Guid ReaderGI, Guid LogGI, string Type);
        public Task<ResponseEntity> Create(Reader input);
        public Task<ResponseEntity> ApproveOrReject(Reader model);
        public Task<ResponseEntity> Delete(Reader model);
        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code);
    }
}
