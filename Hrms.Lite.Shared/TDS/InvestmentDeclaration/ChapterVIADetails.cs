using System;
using System.Collections.Generic;
using System.Text;
using Hrms.Lite.Shared.General;

namespace Hrms.Lite.Shared.TDS
{
  public  class ChapterVIADetails
    {
        public int TrnCode { get; set; }
        public decimal PPSAmt { get; set; }
        public decimal PPSAmtLmt { get; set; }
        public decimal PPSEAmt { get; set; }
        public decimal NPSAmt { get; set; }
        public decimal NPSAmtLmt { get; set; }
        public decimal NPSEAmt { get; set; }

        public decimal MedInsurAmt { get; set; }
        public decimal MedInsurParentAmt { get; set; }
        public Dropdown IsParentsSrCitizen { get; set; }
        public decimal MedExpSrCtznAmt { get; set; }
        public decimal MedExpSrCtznLmt { get; set; }
        
        public decimal MedExpSrCtznEAmt { get; set; }
        public decimal PrevHChkUp { get; set; }
        public decimal PrevHChkUpParents { get; set; }
        public Dropdown IsDepndHandicaped { get; set; }

        public Dropdown DisabilitySevere { get; set; }
        public Dropdown DependentRelation { get; set; }
        public Dropdown CriticalAilments { get; set; }
        public Dropdown RelationShip { get; set; }
        public int PatientAge { get; set; }
        public decimal DepndExpenditure { get; set; }
        public decimal EduLoanInterest { get; set; }
        public decimal EduLoanInterestEAmt { get; set; }
        public DateTime? EduLoanInterestDate { get; set; }
        public DateTime? HouseLoanDate { get; set; }
        public decimal HouseLoanAmt { get; set; }
        public decimal ValueofHouse { get; set; }
        public Dropdown IsOtherHouse { get; set; }
        public decimal InterestPaid { get; set; }
        public decimal InterestLmt { get; set; }
        public decimal InterestEAmt { get; set; }
     
        public DateTime? EEAHouseLoanDate { get; set; }
        public decimal EEAHouseLoanAmt { get; set; }
        public decimal EEAValueofHouse { get; set; }
        public Dropdown IsEEAOtherHouse { get; set; }
        public decimal EEAInterestPaid { get; set; }
        public decimal EEAInterestLmt { get; set; }
        public decimal EEAInterestEAmt { get; set; }
                //---Electric Car
        public DateTime? ECPurchaseDate { get; set; }
        public decimal ECLoanAmount { get; set; }
        public string ECBank { get; set; }
        public decimal ECInterest { get; set; }
        public decimal ECInterestLmt { get; set; }
        public decimal ECInterestEA { get; set; }
      

        public decimal SBAccInterest { get; set; }
        public decimal SBAccInterestLmt { get; set; }
        public decimal SBAccInterestEA { get; set; }

        public Dropdown NatureofDisability { get; set; }
        public Dropdown PercentageofDisability { get; set; }
    
        public decimal LeaveTravelAmt { get; set; }
        public int NoOfChildren { get; set; }
        public decimal UniformExpense { get; set; }
        //------------------DOC COUNT-----------------
        public int CCCDocCount { get; set; }
        public int CCDDocCount { get; set; }
        public int DDocCount { get; set; }
        public int DDDocCount { get; set; }
        public int DDBDocCount { get; set; }
        public int EDocCount { get; set; }
        public int EEDocCount { get; set; }
        public int EEADocCount { get; set; }
        public int EEBDocCount { get; set; }
        public int TTADocCount { get; set; }
        public int UDocCount { get; set; }
        public int LTDocCount { get; set; }
        public string VIASectionTab { get; set; }
        //------------------Approval status-----------------
        public string CCCAppStatus { get; set; }
        public string CCCApproverComments { get; set; }
        public string CCDAppStatus { get; set; }
        public string CCDApproverComments { get; set; }
        public string DAppStatus { get; set; }
        public string DApproverComments { get; set; }
        public string DDAppStatus { get; set; }
        public string DDApproverComments { get; set; }

        public string DDBAppStatus { get; set; }
        public string DDBApproverComments { get; set; }

        public string EAppStatus { get; set; }
        public string EApproverComments { get; set; }

        public string EEAppStatus { get; set; }
        public string EEApproverComments { get; set; }

        public string EEAAppStatus { get; set; }
        public string EEAApproverComments { get; set; }

        public string EEBAppStatus { get; set; }
        public string EEBApproverComments { get; set; }

        public string TTAAppStatus { get; set; }
        public string TTAApproverComments { get; set; }

        public string UAppStatus { get; set; }
        public string UApproverComments { get; set; }

        public string LTAppStatus { get; set; }
        public string LTApproverComments { get; set; }


        //------------------ACTUAL-----------------
        public decimal A_PPSAmt { get; set; }
        public decimal A_PPSAmtLmt { get; set; }
        public decimal A_PPSEAmt { get; set; }
        public decimal A_NPSAmt { get; set; }
        public decimal A_NPSAmtLmt { get; set; }
        public decimal A_NPSEAmt { get; set; }

        public decimal A_MedInsurAmt { get; set; }
        public decimal A_MedInsurParentAmt { get; set; }
        public Dropdown A_IsParentsSrCitizen { get; set; }
        public decimal A_MedExpSrCtznAmt { get; set; }
        public decimal A_MedExpSrCtznLmt { get; set; }

        public decimal A_MedExpSrCtznEAmt { get; set; }
        public decimal A_PrevHChkUp { get; set; }
        public decimal A_PrevHChkUpParents { get; set; }
        public Dropdown A_IsDepndHandicaped { get; set; }

        public Dropdown A_DisabilitySevere { get; set; }
        public Dropdown A_DependentRelation { get; set; }
        public Dropdown A_CriticalAilments { get; set; }
        public Dropdown A_RelationShip { get; set; }
        public int A_PatientAge { get; set; }
        public decimal A_DepndExpenditure { get; set; }
        public decimal A_EduLoanInterest { get; set; }
        public decimal A_EduLoanInterestEAmt { get; set; }
        public DateTime? A_EduLoanInterestDate { get; set; }
        public DateTime? A_HouseLoanDate { get; set; }
        public decimal A_HouseLoanAmt { get; set; }
        public decimal A_ValueofHouse { get; set; }
        public Dropdown A_IsOtherHouse { get; set; }
        public decimal A_InterestPaid { get; set; }
        public decimal A_InterestLmt { get; set; }
        public decimal A_InterestEAmt { get; set; }

        public DateTime? A_EEAHouseLoanDate { get; set; }
        public decimal A_EEAHouseLoanAmt { get; set; }
        public decimal A_EEAValueofHouse { get; set; }
        public Dropdown A_IsEEAOtherHouse { get; set; }
        public decimal A_EEAInterestPaid { get; set; }
        public decimal A_EEAInterestLmt { get; set; }
        public decimal A_EEAInterestEAmt { get; set; }
        //---Electric Car
        public DateTime? A_ECPurchaseDate { get; set; }
        public decimal A_ECLoanAmount { get; set; }
        public string A_ECBank { get; set; }
        public decimal A_ECInterest { get; set; }
        public decimal A_ECInterestLmt { get; set; }
        public decimal A_ECInterestEA { get; set; }


        public decimal A_SBAccInterest { get; set; }
        public decimal A_SBAccInterestLmt { get; set; }
        public decimal A_SBAccInterestEA { get; set; }

        public Dropdown A_NatureofDisability { get; set; }
        public Dropdown A_PercentageofDisability { get; set; }

        public decimal A_LeaveTravelAmt { get; set; }
        public int A_NoOfChildren { get; set; }
        public decimal A_UniformExpense { get; set; }

    }
}
