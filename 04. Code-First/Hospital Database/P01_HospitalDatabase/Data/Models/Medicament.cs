﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace P01_HospitalDatabase.Data.Models
{
    [Table("Medicaments")]
    public class Medicament
    {
        //[Key]
        public int MedicamentId { get; set; }

        //[Required]
        //[StringLength(50)]
        public string Name { get; set; }

        public ICollection<PatientMedicament> Prescriptions { get; set; }


        public Medicament()
        {
            this.Prescriptions = new HashSet<PatientMedicament>();
        }
    }
}
