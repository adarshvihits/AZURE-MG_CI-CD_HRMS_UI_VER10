using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace Hrms.Lite.Services.IServices.Master
{
    public interface ISectionService
    {
        public Task<Section> GetList(string TabIndex);
        public Task<ResponseEntity> Create(Section Input);
        public Task<Section> GetSectionDetails(Guid SectionGI, Guid LogGI, string Type);
        public Task<ResponseEntity> ApproveOrReject(Section model);
        public Task<ResponseEntity> Delete(Section input);
        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code);
    }
}
