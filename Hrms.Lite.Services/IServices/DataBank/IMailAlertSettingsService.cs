using Hrms.Lite.Shared.DataBank;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank
{
    public interface IMailAlertSettingsService
    {
        public Task<MailAlertSettings> GetList(int TypeCode, Filter filterdata);
        public Task<ResponseEntity> Create(MailAlertSettings input);
        public Task<DataSet> GetList_Excel(int TypeCode, Filter Filter);
    }
}
