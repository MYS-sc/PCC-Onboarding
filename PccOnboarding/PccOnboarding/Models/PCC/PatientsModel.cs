using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PccOnboarding.Models.PCC
{
    public class PatientsModel
    {
        public string OrgUuid { get; set; }
        public bool NewClient { get; set; } = false;
        public string FacId { get; set; }
        public int PatientId { get; set; }
        public string MedicalRecordNumber { get; set; }
        public string MedicalNumber { get; set; }
        public string PatientExternalId { get; set; }
        public string PatientStatus { get; set; }
        public string LastName { get; set; }
        public string? FirstName { get; set; }
        public bool OutPatient { get; set; }
        public int OmpId { get; set; }
        public string BirthDate { get; set; }
        public string AdmissionDate { get; set; }
        public string AdmissionDateTime { get; set; }
        public string DischargeDate { get; set; }
        public string DischargeDateTime { get; set; }
        public bool WaitingList { get; set; }
        public bool HasPhoto { get; set; }
        public string Gender { get; set; }
        public string LanguageCode { get; set; }
        public string LanguageDesc { get; set; }
        public string Religion { get; set; }
        public string Citizenship { get; set; }
        public string MaritalStatus { get; set; }
        public bool Deceased { get; set; }
        public string DeathDateTime { get; set; }
        public string Occupation { get; set; }
        public string BirthPlace { get; set; }
        public string? MiddleName { get; set; }
        public int? ourId { get; set; } = null;
    }
}
