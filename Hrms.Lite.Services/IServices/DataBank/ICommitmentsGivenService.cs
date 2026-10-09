using Hrms.Lite.Shared.DataBank;
using Hrms.Lite.Shared.DataBank.Reports;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank
{
    public interface ICommitmentsGivenService
    {
        public Task<CommitmentsGiven> GetCommitmentsGivenList(int TabIndex, Filter filter);
        Task<DataSet> GetCommitmentsGiven_Excel(int TabIndex, Filter filter);
        Task<CommitmentsGiven> GetCommitmentsGivenPopUpDetails(int EmployeeCode);
        Task<DataSet> GetCommitmentsGivenPopUp_Excel(int EmployeeCode);
        public Task<ResponseEntity> Create(CommitmentsGiven input);
    }
}

