using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank
{
    public interface IEmployeeVsGeoTaggingService
    {
        Task<DataSet> GetList_Excel(Filter filter);
        Task<EmployeeVsGeoTagging> GetList(Filter filter);
        Task<EmployeeVsGeoTagging> GetGeoTaggingDetails(Guid EmployeeGI);
        Task<ResponseEntity> GeoTaggingSave(EmployeeVsGeoTagging model);
    }
}
