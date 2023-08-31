﻿using Medsoft.Models;

namespace Medsoft.Dto
{
    public class PatientDto
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public decimal IDNP { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public decimal Telephone { get; set; }
        public int Sex { get; set; }
        public int PatientState { get; set; }
    }
}
