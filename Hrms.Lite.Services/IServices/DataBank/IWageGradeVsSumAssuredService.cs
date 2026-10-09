using Hrms.Lite.Shared.DataBank;
using Hrms.Lite.Shared.General;
using System.Data;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank
{
    public interface IWageGradeVsSumAssuredService
    {
        public Task<WageGradeVsSumAssured> GetList();
        public Task<ResponseEntity> Create(WageGradeVsSumAssured input);
        public Task<DataSet> GetList_Excel();
    }
}
