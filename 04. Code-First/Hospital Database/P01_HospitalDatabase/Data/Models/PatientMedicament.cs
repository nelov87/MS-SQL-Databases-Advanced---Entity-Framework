using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace P01_HospitalDatabase.Data.Models
{

    public class PatientMedicament
    {
        //[Required]
        public int PatientId { get; set; }

        //[ForeignKey(nameof(PatientId))]
        public Patient Patient { get; set; }

        //[Required]
        public int MedicamentId { get; set; }

        //[ForeignKey(nameof(MedicamentId))]
        public Medicament Medicament { get; set; }


    }
}
