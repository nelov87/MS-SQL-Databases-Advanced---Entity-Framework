using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace P01_HospitalDatabase.Data.Models
{
    [Table("Patients")]
    public class Patient
    {
        //[Key]
        public int PatientId { get; set; }

        //[Required]
        //[StringLength(50)]
        public string FirstName { get; set; }

        //[Required]
        //[StringLength(50)]
        public string LastName { get; set; }

        //[Required]
        //[StringLength(250)]
        public string Address { get; set; }

        //[Required]
        //[StringLength(250)]
        public string Email { get; set; }

        //[Required]
        //[Column(TypeName = "BIT")]
        public bool HasInsurance { get; set; }

        public ICollection<Visitation> Visitations { get; set; }

        public ICollection<Diagnose> Diagnoses { get; set; }

        public ICollection<PatientMedicament> Prescriptions { get; set; }


    public Patient()
        {
            this.Visitations = new HashSet<Visitation>();
            this.Diagnoses = new HashSet<Diagnose>();
            this.Prescriptions = new HashSet<PatientMedicament>();
        }
    }
}
