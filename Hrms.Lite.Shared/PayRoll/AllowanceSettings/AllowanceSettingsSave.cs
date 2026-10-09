using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
namespace Hrms.Lite.Shared.PayRoll.AllowanceSettings
{
    public class AllowanceSettingsSave
    {
      //  public UserBase User { get; set; }
        public int AllowanceCode { get; set; }
        public int AttendanceStatusCode { get; set; }
        public Guid EmployeeGI { get; set; }
        public int CalcTypeCode { get; set; }
        public decimal? Rate { get; set; }
    }
}
