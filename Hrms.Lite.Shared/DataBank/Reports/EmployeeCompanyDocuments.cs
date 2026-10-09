using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.Reports
{
    public class EmployeeCompanyDocuments
    {
        public int EmployeeCode { get; set; }
        public Guid EmployeeGI { get; set; }
        public Filter filter { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public DateTime DOJ { get; set; }
        public string YOS { get; set; }
        public MasterBase DocumentType { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public string Type { get; set; }
        public List<EmployeeCompanyDocuments> EmployeeCompanyDocumentsList { get; set; }
        public int PreEnrollCode { get; set; }
        public int HistoryCode { get; set; }
        public int CompanyCode { get; set; }
        public int GroupcompanyCode { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; } 
        public List<EmployeeTransferList_List> EmployeeTransferList { get; set; }
        public List<EmployeeDeployment_List> GetDeploymentList { get; set; }
        public AppointmentLetter AppointmentLetterList { get; set; }
        public ExperienceLetter ExperienceletterList { get; set; }
        public List<EmployeeSalaryList_List> EmployeeSalaryList { get; set; }
        public FTCRevisionLetter FTCRevisionLetterList { get; set; }
        public PromotionLetter PromotionLetterList { get; set; }
        public IncrementLetter IncrementLetterList { get; set; }
        public List<EmployeeConfirmationList_List> EmployeeConfirmationList { get; set; }
        public StillWorkingLetter StillWorkingLetterList { get; set; }
        public AbscondingLetter AbscondingLetterList { get; set; }

    }

    public class EmployeeDeployment_List
    {
        public int EmployeeCode { get; set; }
        public Guid EmployeeGI { get; set; }
        public string EmployeeID { get; set; }
        public DateTime DOJ { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public int CompanyCode { get; set; }
        public DateTime DOE { get; set; }
        public int HistoryCode { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
    }



        public class EmployeeSalaryList_List
    {
        public int EmployeeCode { get; set; }
        public Guid EmployeeGI { get; set; }
        public Guid GroupCompanyGI { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public DateTime DOJ { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
    }

    public class Termination
    {
        public int EmployeeCode { get; set; }
        public Guid EmployeeGI { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public DateTime DOJ { get; set; }
        public int CompanyCode { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
        public List<Termination> TerminationList { get; set; }



    }

    public class EmployeeTransferList_List
    {
        public int EmployeeCode { get; set; }
        public Guid EmployeeGI { get; set; }
        public string EmployeeID { get; set; }
        public DateTime DOJ { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public int CompanyCode { get; set; }
        public DateTime DOE { get; set; }
        public int HistoryCode { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
    }





    public class AppointmentLetter
    {
        public int EmployeeCode { get; set; }
        public Guid EmployeeGI { get; set; }
        public int HistoryCode { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public DateTime DOJ { get; set; }
        public int CompanyCode { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
        public int GroupCompanyCode { get; set; }
        public List<AppointmentLetter> GetAppointmentLetterList { get; set; }
        public List<AppointmentLetter> GetProbationExtensionList { get; set; }


    }
    public class ExperienceLetter
    {
        public int EmployeeCode { get; set; }
        public Guid EmployeeGI { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public DateTime DOJ { get; set; }
        public int CompanyCode { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
        public int GroupCompanyCode { get; set; }


        public List<ExperienceLetter> GetExperienceLetterList { get; set; }



    }


    public class EmploymentCertificate
    {
        public int EmployeeCode { get; set; }
        public Guid EmployeeGI { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public DateTime DOJ { get; set; }
        public int CompanyCode { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
        public List<EmploymentCertificate> GetEmploymentCertificateList { get; set; }
    }


    public class FTCRevisionLetter
    {
        public int EmployeeCode { get; set; }
        public Guid EmployeeGI { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public DateTime DOJ { get; set; }
        public int CompanyCode { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
        public string CategoryName { get; set; }
        public int CategoryCode { get; set; }
        public int HistoryCode { get; set; }
        public List<FTCRevisionLetter> GetFTCRevisionLetterList { get; set; }



    }

    public class PromotionLetter
    {
        public int EmployeeCode { get; set; }
        public Guid EmployeeGI { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public DateTime DOJ { get; set; }
        public int CompanyCode { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
        public string CategoryName { get; set; }
        public int CategoryCode { get; set; }
        public int HistoryCode { get; set; }
        public List<PromotionLetter> GetPromotionLetterList { get; set; }



    }



    public class RelievingLetter
    {
        public int EmployeeCode { get; set; }
        public Guid EmployeeGI { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public DateTime DOJ { get; set; }
        public int CompanyCode { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
        public List<RelievingLetter> GetRelievingLetterList { get; set; }



    }




    public class IncrementLetter
    {
        public int EmployeeCode { get; set; }
        public Guid EmployeeGI { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public DateTime DOJ { get; set; }
        public int CompanyCode { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
        public int HistoryCode { get; set; }
        public DateTime OrderDate { get; set; }
        public List<IncrementLetter> GetIncrementLetterList { get; set; }

    }

    public class OfferLetter
    {
        public Filter Filter { get; set; }
        public Guid PreEnrollGI { get; set; }
        public File File { get; set; }
        public string Type { get; set; }
        public string AbsoluteUri { get; set; }
        public string Mode { get; set; }
        public int EmployeeCode { get; set; }
        public Url Url { get; set; }
        public Guid EmployeeGI { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public DateTime DOJ { get; set; }
        public int CompanyCode { get; set; }
        public int PreEnrollCode { get; set; }
        public int HistoryCode { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
        public List<OfferLetter> OfferLetterList { get; set; }
        public bool Applicable { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public int radiomode { get; set; }

    }
    public class EmployeeConfirmationList_List
    {
        public int EmployeeCode { get; set; }
        public Guid EmployeeGI { get; set; }
        public string EmployeeID { get; set; }
        public DateTime DOJ { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public int CompanyCode { get; set; }
        public DateTime DOE { get; set; }
        public int HistoryCode { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
    }

    public class StillWorkingLetter
    {
        public int EmployeeCode { get; set; }
        public Guid EmployeeGI { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public DateTime DOJ { get; set; }
        public int CompanyCode { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
        public int GroupCompanyCode { get; set; }
        public List<StillWorkingLetter> GetStillWorkingLetterList { get; set; }

    }

    public class AbscondingLetter
    {
        public int EmployeeCode { get; set; }
        public Guid EmployeeGI { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public DateTime DOJ { get; set; }
        public int CompanyCode { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
        public int GroupCompanyCode { get; set; }
        public List<AbscondingLetter> GetAbscondingLetterList { get; set; }

    }

}
