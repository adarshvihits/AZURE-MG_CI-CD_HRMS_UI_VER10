using Hrms.Lite.Shared.DataBank.Reports;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank.Reports
{
   public interface ICelebrityListService
    {
        public Task<CelebrityList> GetCelebrityList(string FromDate, string ToDate, string TabIndex,Filter filter);
        public Task<DataSet> GetCelebrityList_Excel(string FromDate, string ToDate, string TabIndex, Filter filter);
    }
}
