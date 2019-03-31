﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace P01_HospitalDatabase.Data.Models
{
    [Table("Diagnoses")]
    public class Diagnose
    {
        //[Key]
        public int DiagnoseId { get; set; }

        //[Required]
        [StringLength(50)]
        public string Name { get; set; }

        //[Required]
        //[StringLength(250)]
        public string Comments { get; set; }

        //[Required]
        public int PatientId { get; set; }

        //[ForeignKey(nameof(PatientId))]
        public Patient Patient { get; set; }

    }
}
