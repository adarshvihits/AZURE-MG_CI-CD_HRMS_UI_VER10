using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TimeOffice.Generic.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.PayRoll
{
    public class MonthlyPaymentRemittance
    {
        public Filter filter { get; set; }

        public string Mode { get; set; }

        public bool Active { get; set; }

        public string Remarks { get; set; }

        public Month Month { get; set; }

        public string VoucherNum { get; set; }

        public int PFEmployee { get; set; }

        public int VPF { get; set; }

        public int PF { get; set; }

        public int PFEPS { get; set; }

        public int PFadmin { get; set; }

        public int PFEdli { get; set; }

        public DateTime? Date { get; set; }

        public int TotalAmount { get; set; }

        public List<MonthlyPaymentRemittance> PFdetailsList { get; set; }

        public int ESIEmployee { get; set; }
        public int ESIEmployer { get; set; }

        public int LWFEmployee { get; set; }
        public int LWFEmployer { get; set; }

        public int PTAmountPaid { get; set; }

        public string FirstTab { get; set; }

        public int Empcode { get; set; }
        public EmployeeHeader EmployeeHeader { get; set; }
        public string Code { get; set; }

        public string EmployeeID { get; set; }
        public string EmpName { get; set; }

        public int TDSRemittedAmount { get; set; }

    }
}
