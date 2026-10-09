using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using Hrms.Lite.Shared.PayRoll.Generic.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.PayRoll
{
  public class SlabDeductionConfirmation
    {

        public List<SlabDeductionConfirmation> SlabDeductionConfirmationList { get; set; }
        public int Month { get; set; }
        public PayrollGroup PayGroup { get; set; }
        public Guid EmployeeGI { get; set; }
        public int EmployeeCode { get; set; }
        public int DeductionCode { get; set; }
        public Filter filter { get; set; }

        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Location { get; set; }
        public string Designation { get; set; }
        public string Remarks { get; set; }
        public string Department { get; set; }   
        public Decimal GrossPay { get; set; }     
        public int Amount { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }




    }
}
