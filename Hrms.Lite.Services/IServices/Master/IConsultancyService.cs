using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
    public interface IConsultancyService
    {
        public Task<Consultancy> GetList(string TabIndex);
        public Task<ResponseEntity> Create(Consultancy input);
        public Task<Consultancy> GetConsultancyDetails(Guid ConsultancyGI, Guid LogGI, string Type);
        public Task<ResponseEntity> Delete(Consultancy model);
        public Task<ResponseEntity> ApproveOrReject(Consultancy model);
        public Task<DataSet> GetList_Excel(string TabIndex);
    }
}
