using Hrms.Lite.Shared.DataBank;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank
{
    public interface IEmployeeReferenceSchemeService
    {
        public Task<EmployeeReferenceScheme> GetList();
        public Task<ResponseEntity> Create(EmployeeReferenceScheme input);
        public Task<DataSet> GetList_Excel();
        public Task<ResponseEntity> CreateMaster(EmployeeReferenceScheme input);
    }
}
