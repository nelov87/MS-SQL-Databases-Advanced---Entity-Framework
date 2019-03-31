using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace P01_HospitalDatabase.Data.Models
{
    [Table("Visitations")]
    public class Visitation
    {
        //[Key]
        public int VisitationId { get; set; }

        //[Required]
        public DateTime Date { get; set; }

        //[Required]
        [StringLength(250)]
        public string Comments { get; set; }
        
        //[Required]
        public int PatientId { get; set; }
        //[ForeignKey(nameof(PatientId))]
        public Patient Patient { get; set; }

        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
    }
}
