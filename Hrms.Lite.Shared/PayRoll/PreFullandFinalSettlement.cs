using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.PayRoll
{
    public class PreFullandFinalSettlement
    {
        public Guid PreSettlementGI { get; set; }
      
        public Dropdown PayrollMonth { get; set; }
        public PayrollGroup PayrollGroup { get; set; }
        public EmployeeHeader EmployeeHeader { get; set; }
        public DateTime ResignationDate { get; set; }
        public DateTime LWD { get; set; }
        public Filter Filter { get; set; }
        public decimal LOPDays { get; set; }
        
        public decimal PayDays { get; set; }
        public int TotalBasic { get; set; }
        public int EarnedBasic { get; set; }
        public int Held { get; set; }
        public int Release { get; set; }
        public string Remarks { get; set; }
        public string ApproverComments { get; set; }
        public int LoanAmount { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
        public string APPROVALSTATUS { get; set; }
        public string ApprovedBy { get; set; }
        public string ApprovedOn { get; set; }
        public int BANKTRANSFERSTATUS { get; set; }
        public string TRANSFERDATE { get; set; }
        public string ResigDate { get; set; }
        public string LstWD { get; set; }
        public List<PreFullandFinalSettlement> PreFullandFinalSettlementList { get; set; }
   
    }
}
