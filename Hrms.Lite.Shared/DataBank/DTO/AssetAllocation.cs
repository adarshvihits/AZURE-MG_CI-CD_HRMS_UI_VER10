using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.DTO
{
    public class AssetAllocation
    {
        public Guid AssetAllocationGI { get; set; }
        public int AssetAllocationId { get; set; }
        public EmployeeHeader EmployeeHeader { get; set; }
        public string EmpId { get; set; }
        public string EmployeeName { get; set; }
        public Guid EmployeeGI { get; set; }
        public Designation Designation { get; set; }
        public Department Department { get; set; }
        public Location Location { get; set; }
        public Category Category { get; set; }
        public DateTime IssuedDate { get; set; }
        public DateTime RequestDate { get; set; }
        public string Ageing { get; set; }
        public string Status { get; set; }
        public int Tabindex { get; set; }
        public int SearchId { get; set; }
        public DateTime RequiredDate { get; set; }
        public string Description { get; set; }
        public AssetRequest AssetRequest { get; set; }
     
        public List<AssetAllocationList> AssetAllocationList { get; set; }
        public AssetAllocationFilterCombo AssetAllocationFilter { get; set; }
        public List<AssetAllocationTabs> AssetAllocationTabs { get; set; }
        public List<Asset> AssetList { get; set; }
        public int AllTabCount { get; set; }
        public int Tab1Count { get; set; }
        public int Tab2Count { get; set; }
        public int Tab3Count { get; set; }
        public int MoreTabCount { get; set; }
        public int PendingTabCount { get; set; }
        public  int EmpReqTabCount { get; set; }
        public int TabCode { get; set; }
        public int AssetCategoryCode { get; set; }
        public Filter Filter { get; set; }
       public Dropdown Asset { get; set; }
        public int TrnCode { get; set; }

    }
    public class AssetAllocationTabs
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        
    }
    public class AssetAllocationList
    {
        public Guid AssetAllocationGI { get; set; }
        public string EmpId { get; set; }
        public string EmployeeName { get; set; }
        public int EmployeeCode { get; set; }
        public Guid EmployeeGI { get; set; }
        public string AssetName { get; set; }
        public Designation Designation { get; set; }
        public Department Department { get; set; }
        public Location Location { get; set; }
        public Category Category { get; set; }
        public DateTime? IssuedDate { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? RequestDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public string Ageing { get; set; }
        public string Status { get; set; }
        public DateTime RequiredDate { get; set; }
        public int TrnCode { get; set; }
        public int AssetCode { get; set; }
        public string IsssueDept { get; set; }
        public DateTime PurchaseDate { get; set; }
        public int AssetCost { get; set; }
        public string ModelName { get; set; }
        public string Brand { get; set; }
        public string Specification { get; set; }

    }
    public class Asset
    {
        public string AssetCode { get; set; }
        public Category AssetCategory { get; set; }
        public string AssetName { get; set; }        
        public DateTime? DateOfPurchase { get; set; }
        public string Brand { get; set; }
        public string ModelNo { get; set; }
        public Decimal? Cost { get; set; }
        public Department IssuingDepartment { get; set; }
        public Condition Condition { get; set; }
        public string Specification { get; set; }
        public string Status { get; set; }
      
    }
    public class AssetRequest
    {
        public Asset Asset { get; set; }
        public AssetName AssetName { get; set; }
        public DateTime? AssetIssueDate { get; set; }
        public DateTime? AssetDueDate { get; set; }
        public DateTime? AssetReturnDate { get; set; }
        public List<AssetLog> AssetLog { get; set; }
        public int? Deduction { get; set; }
        public string ReasonForReturn { get; set; }
        public string Remarks { get; set; }
        public string CreatedBy { get; set; }
    }
    public class AssetLog
    {
        public Asset Asset { get; set; }
        public Guid AssetAllocationGI { get; set; }
        public DateTime GivenDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public DateTime? ActualReturnDate { get; set; }
    }
    
}
